Shader "SupGames/Mobile/ChromaticAberration"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"
	#define hal fixed2(0.5h, 0.5h)

	UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
	fixed _RedX;
	fixed _RedY;
	fixed _GreenX;
	fixed _GreenY;
	fixed _BlueX;
	fixed _BlueY;
	fixed _Distortion;
	fixed _Coefficent;
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
		fixed2 uv1 : TEXCOORD1;
		UNITY_VERTEX_OUTPUT_STEREO
	};


	v2f vert(appdata i)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(v2f, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = UnityObjectToClipPos(i.pos);
		o.uv.x = i.uv.x + _GreenX * _MainTex_TexelSize.x;
		o.uv.y = i.uv.y + _GreenY * _MainTex_TexelSize.y;
		o.uv.z = i.uv.x + _RedX * _MainTex_TexelSize.x - 0.5h;
		o.uv.w = i.uv.y + _RedY * _MainTex_TexelSize.y - 0.5h;
		o.uv1.x = i.uv.x + _BlueX * _MainTex_TexelSize.x - 0.5h;
		o.uv1.y = i.uv.y + _BlueY * _MainTex_TexelSize.y - 0.5h;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		fixed4 c = fixed4(0.0h,0.0h,0.0h,1.0h);
		c.g = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoTransformScreenSpaceTex(i.uv.xy)).g;

		fixed r2 = dot(i.uv.zw, i.uv.zw);
		fixed2 uv = (1.0h + r2 * _Distortion * sqrt(r2)) * i.uv.zw + hal;
		c.r = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoTransformScreenSpaceTex(uv)).r;

		r2 = dot(i.uv1.xy, i.uv1.xy);
		uv = (1.0h - r2 * _Distortion * sqrt(r2)) * i.uv1.xy + hal;
		c.b = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoTransformScreenSpaceTex(uv)).b;
		return c;
	}
	ENDCG

	Subshader
	{
		Pass
		{
		  ZTest Always Cull Off ZWrite Off
		  Fog { Mode off }
		  CGPROGRAM
		  #pragma vertex vert
		  #pragma fragment frag
		  #pragma fragmentoption ARB_precision_hint_fastest
		  ENDCG
		}
	}
	Fallback off
}