Shader "RufatShaderlab/DepthOfField"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
	UNITY_DECLARE_SCREENSPACE_TEXTURE(_BlurTex);
	UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

	half4 _MainTex_TexelSize;
	half4 _MainTex_ST;
	uniform half _BlurAmount;
	half _Focus;
	half _Aperture;


	struct appdata
	{
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f
	{
		half4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	struct v2fb
	{
		half4 pos : SV_POSITION;
		half4  uv : TEXCOORD0;
#if !defined(ISDEPTH)
		half2  uv1 : TEXCOORD1;
		half4  uv2 : TEXCOORD2;
#endif
		UNITY_VERTEX_OUTPUT_STEREO
	};

	v2fb vertBlur(appdata i)
	{
		v2fb o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(v2fb, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = UnityObjectToClipPos(i.pos);
		half2 offset = _MainTex_TexelSize.xy * _BlurAmount * (1.0h / _MainTex_ST.xy);
		o.uv = half4(UnityStereoScreenSpaceUVAdjust(i.uv - offset, _MainTex_ST), UnityStereoScreenSpaceUVAdjust(i.uv + offset, _MainTex_ST));
#if !defined(ISDEPTH)
		o.uv1 = i.uv;
		o.uv2 = half4(-offset, offset);
#endif
		return o;
	}

	v2f vert(appdata i)
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(v2f, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = UnityObjectToClipPos(i.pos);
		o.uv = UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST);
		return o;
	}

	fixed4 fragBlur(v2fb i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
#ifdef ISDEPTH
		fixed4 result = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xy);
		result += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xw);
		result += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.zy);
		result += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.zw);
#else
		fixed a1 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xy).a;
		fixed a2 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xw).a;
		fixed a3 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.zy).a;
		fixed a4 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.zw).a;
		fixed4 result = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv1 + i.uv2.xy * a1, _MainTex_ST));
		result += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv1 + i.uv2.xw * a2, _MainTex_ST));
		result += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv1 + i.uv2.zy * a3, _MainTex_ST));
		result += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv1 + i.uv2.zw * a4, _MainTex_ST));
#endif
		return result * 0.25h;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		fixed4 c = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv);
		fixed4 b = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_BlurTex, i.uv);
#ifdef ISDEPTH
		fixed depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv));
		return lerp(c, b, saturate(abs((1.0h - clamp(max(depth / _Focus, _Focus / depth), 0.0h, 20.0h)) * _Aperture)));
#endif
		return	lerp(c, b, min(c.a, b.a));
	}
		ENDCG

	SubShader
	{
		Pass //0
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma vertex vertBlur
			#pragma fragment fragBlur
			#pragma shader_feature_local ISDEPTH
			#pragma fragmentoption ARB_precision_hint_fastest
			ENDCG
		}

		Pass //1
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature_local ISDEPTH
			#pragma fragmentoption ARB_precision_hint_fastest
			ENDCG
		}
	}
}