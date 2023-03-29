Shader "RufatShaderlab/Glitch"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
	fixed _Amount;
	fixed _Frame;
	fixed2 _Pixel;
	fixed4 _MainTex_TexelSize;

	struct appdata
	{
		fixed4 pos : POSITION;
		fixed2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f
	{
		fixed4 pos : POSITION;
		fixed4 uv : TEXCOORD0;
#ifdef PIXEL
		fixed4 uv1 : TEXCOORD1;
#endif
		UNITY_VERTEX_OUTPUT_STEREO
	};

	half r(half2 v)
	{
		return frac(sin(dot(v * floor(_Time.y * 12.), half2(127.1, 311.7))) * 43758.5453123);
	}

	v2f vert(appdata i)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(v2f, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = UnityObjectToClipPos(i.pos);
		o.uv.xy = UnityStereoTransformScreenSpaceTex(i.uv);
		o.uv.zw = fixed2(o.uv.x + _Amount, o.uv.x - _Amount);
#ifdef PIXEL
		o.uv1 = floor(fixed4(i.uv.xy * half2(24.0h, 9.0h), i.uv.xy * half2(8.0h, 4.0h)));
#endif
		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
#ifdef FRAME
		i.uv.xwz += sin(i.uv.y * 10.0h + _Time.y) * step(0.3h, sin(_Time.y + 4.0h * _CosTime.w)) * _Frame;
#endif
#ifdef PIXEL
		fixed lineNoise = pow(r(floor(i.uv1.xy)), 8.0) * pow(r(floor(i.uv1.zw)), 3.0);
		i.uv.x += lineNoise * _Pixel.x;
		i.uv.w -= lineNoise * _Pixel.y;
#endif
		return fixed4(UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.zy).r, UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xy).g, UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.wy).b, 1.0h);
	}
	ENDCG

	Subshader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma shader_feature_local FRAME
			#pragma shader_feature_local PIXEL
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			ENDCG
		}
	}
	Fallback off
}