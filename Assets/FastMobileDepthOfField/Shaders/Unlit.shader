Shader "RufatShaderlab/DOF/Unlit"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_fwdbase
			#pragma multi_compile_instancing
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
			half4 _MainTex_ST;
			half _Focus;
			half _Aperture;
			half4 _Color;


			struct appdata
			{
				half4 vertex : POSITION;
				half2 uv : TEXCOORD0;
#ifdef LIGHTMAP_ON
				half2 luv : TEXCOORD1;
#endif
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct v2f
			{
				half4 vertex : SV_POSITION;
				half3 uv : TEXCOORD0;
#ifdef LIGHTMAP_ON
				half2 lightMap : TEXCOORD1;
				UNITY_FOG_COORDS(2)
#else
				UNITY_FOG_COORDS(1)
#endif
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert(appdata v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.uv.xy = TRANSFORM_TEX(v.uv.xy,_MainTex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				half a = -UnityObjectToViewPos(v.vertex).z;
				o.uv.z = saturate(abs((1.0h - clamp(max(a / _Focus, _Focus / a), 0.0h, 20.0h)) * _Aperture));
#ifdef LIGHTMAP_ON
				o.lightMap = v.luv * unity_LightmapST.xy + unity_LightmapST.zw;
#endif
				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 color = fixed4(tex2D(_MainTex, i.uv.xy).rgb * _Color.rgb, 1.0h);
#ifdef LIGHTMAP_ON
				color.rgb *= DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.lightMap)) * _Color.rgb;
#endif
				UNITY_APPLY_FOG(i.fogCoord, color);
				return fixed4(color.rgb, i.uv.z);
			}
			ENDCG
		}

		Pass
		{
			Tags {"LightMode" = "ShadowCaster"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"

			struct v2f {
				V2F_SHADOW_CASTER;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
		}
	}
}