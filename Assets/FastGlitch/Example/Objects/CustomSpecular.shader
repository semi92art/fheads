Shader "Custom/Specular" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_SpecColor("Specular Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Range(0.01,50)) = 10
	}
	SubShader{
	Pass {
	Tags { "LightMode" = "ForwardBase" }

		CGPROGRAM

		#pragma vertex vert  
		#pragma fragment frag 

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"

		uniform float4 _LightColor0;
		uniform float4 _Color;
		uniform float4 _SpecColor;
		uniform float _Shininess;

		struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;

		};
		struct v2f {
			float4 vertex : SV_POSITION;
			float4 col : COLOR;
			float3 diff : TEXCOORD0;
			LIGHTING_COORDS(1,2)
		};

		v2f vert(appdata i)
		{
			v2f o;

			float3 n = normalize(mul(i.normal, unity_WorldToObject));
			float3 v = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.vertex).xyz);
			float3 l = normalize(_WorldSpaceLightPos0.xyz);

			float3 diffuseReflection = _LightColor0.rgb * _Color.rgb * max(0.0, dot(n, l));
			float3 specularReflection = _LightColor0.rgb * _SpecColor.rgb * pow(max(0.0h,dot(reflect(-l, n), v)), _Shininess);

			o.col = float4(UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb + specularReflection, 1.0);
			o.diff = diffuseReflection;
			o.vertex = UnityObjectToClipPos(i.vertex);
			TRANSFER_VERTEX_TO_FRAGMENT(o)
			return o;
		}

		float4 frag(v2f i) : COLOR
		{
			i.col.rgb += i.diff.rgb * LIGHT_ATTENUATION(i);
			return i.col;
		}

		ENDCG
		}
	}
	Fallback "Specular"
}