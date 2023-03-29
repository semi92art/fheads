Shader "SupGames/Mobile/Pixellate"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE

	#include "UnityCG.cginc"

	UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
	fixed2 _Dencity;
	struct appdata {
		fixed4 pos : POSITION;
		fixed2 uv  : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};
	struct v2f
	{
		fixed4 pos : SV_POSITION;
		fixed2 uv  : TEXCOORD0;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2f vert(appdata v)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(v2f, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = UnityObjectToClipPos(v.pos);
		o.uv = v.uv;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		return UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, round(UnityStereoTransformScreenSpaceTex(i.uv) * _Dencity) / _Dencity);
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
