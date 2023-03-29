Shader "RufatShaderlab/DOF/BumpedSpecular"
{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
		_BumpTex("Normal Map", 2D) = "bump" {}
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Glossiness("Glossiness", Range(0.01,100)) = 0.03
	}
		SubShader{
			Tags {"RenderType" = "Opaque"}
			LOD 150
			Pass
			{
				Tags { "LightMode" = "ForwardBase" }
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma multi_compile_fwdbase
				#pragma multi_compile_instancing
				#pragma multi_compile_fog

				# include "UnityCG.cginc"
				#include "AutoLight.cginc"

				UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
				UNITY_DECLARE_SCREENSPACE_TEXTURE(_BumpTex);
				half4 _MainTex_ST;
				half4 _BumpTex_ST;
				half _Glossiness;
				half _Focus;
				half _Aperture;
				half4 _Color;
				half4 _LightColor0;
				half4 _SpecColor;

				struct appdata
				{
					half4 vertex : POSITION;
					half2 uv : TEXCOORD0;
	#ifdef LIGHTMAP_ON
					half2 luv : TEXCOORD1;
	#endif
					half3 normal : NORMAL;
					half4 tangent : TANGENT;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					half4 pos : SV_POSITION;
					half3 uv : TEXCOORD0;
	#ifdef LIGHTMAP_ON
					half3 fogCoord : TEXCOORD1;
	#else
					half4 normal : TEXCOORD1;
					half4 tangent : TEXCOORD2;
					half4 bitangent : TEXCOORD3;
					half4 fogCoord : TEXCOORD4;
					SHADOW_COORDS(5)
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
					o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
					o.pos = UnityObjectToClipPos(v.vertex);
					half a = -UnityObjectToViewPos(v.vertex).z;
					o.uv.z = saturate(abs((1.0h - clamp(max(a / _Focus, _Focus / a), 0.0h, 20.0h)) * _Aperture));
	#ifdef LIGHTMAP_ON
					o.fogCoord.yz = v.luv * unity_LightmapST.xy + unity_LightmapST.zw;
	#else
					half3 viewDirection = _WorldSpaceCameraPos - mul(unity_ObjectToWorld, v.vertex).xyz;
					o.normal = half4(UnityObjectToWorldNormal(v.normal), viewDirection.x);
					o.tangent = half4(normalize(mul(unity_ObjectToWorld, half4(v.tangent.xyz, 0.0h)).xyz), viewDirection.y);
					o.bitangent = half4(cross(o.normal.xyz, o.tangent.xyz) * v.tangent.w * unity_WorldTransformParams.w, viewDirection.z);
					o.fogCoord.yzw = ShadeSH9(half4(o.normal.xyz, 1.0h));
					TRANSFER_SHADOW(o);
	#endif
					UNITY_TRANSFER_FOG(o, o.pos);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					UNITY_SETUP_INSTANCE_ID(i);
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
					fixed4 color = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xy);
	#ifdef LIGHTMAP_ON
					color.rgb *= DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.fogCoord.yz)) * _Color.rgb;
	#else
					fixed4 encodedNormal = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_BumpTex, _BumpTex_ST.xy * i.uv.xy + _BumpTex_ST.zw);
					fixed3 viewDirection = fixed3(i.normal.w, i.tangent.w, i.bitangent.w);
					fixed3 normalDirection = normalize(mul(UnpackNormal(encodedNormal), fixed3x3(i.tangent.xyz, i.bitangent.xyz, i.normal.xyz)));
					fixed3 diffuseReflection = _LightColor0.rgb * max(0.0h, dot(normalDirection, _WorldSpaceLightPos0.xyz));
					fixed3 specularReflection = _SpecColor.rgb * _LightColor0.rgb * pow(saturate(dot(normalDirection, normalize(_WorldSpaceLightPos0.xyz + viewDirection))), _Glossiness);
					color.rgb *= ((specularReflection * color.a + diffuseReflection) * SHADOW_ATTENUATION(i) + i.fogCoord.yzw) * _Color.rgb;
	#endif
					UNITY_APPLY_FOG(i.fogCoord, color);
					return fixed4(color.rgb, i.uv.z);
				}
				ENDCG
			}


			Pass
			{
				Tags { "LightMode" = "ShadowCaster" }

				ZWrite On ZTest LEqual Cull Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_shadowcaster
				#include "UnityCG.cginc"

				struct v2f {
					V2F_SHADOW_CASTER;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				v2f vert(appdata_base v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					SHADOW_CASTER_FRAGMENT(i)
				}
				ENDCG
			}
		}
}