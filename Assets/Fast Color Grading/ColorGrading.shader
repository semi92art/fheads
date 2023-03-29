Shader "SupGames/Mobile/ColorGrading"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	#define huevec fixed3(0.57735h, 0.57735h, 0.57735h)
	#define satur fixed3(0.299h, 0.587h, 0.114h)
	struct appdata 
	{
		fixed4 pos : POSITION;
		fixed2 uv : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f 
	{
		fixed4 pos : SV_POSITION;
		fixed4 uv  : TEXCOORD0;
#if defined(SHARPEN)
		fixed4  uv1 : TEXCOORD1;
#endif
#if defined(BLUR)
		fixed4  uv2 : TEXCOORD2;
#endif
		UNITY_VERTEX_OUTPUT_STEREO
	};

	UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
	UNITY_DECLARE_SCREENSPACE_TEXTURE(_MaskTex);
	uniform fixed4 _Color;
	uniform fixed _HueCos;
	uniform fixed _HueSin;
	uniform fixed3 _HueVector;
	uniform fixed _Contrast;
	uniform fixed _Brightness;
	uniform fixed _Saturation;
	uniform fixed _Blur;
	uniform fixed _CentralFactor;
	uniform fixed _SideFactor;
	uniform fixed4 _VignetteColor;
	uniform fixed _VignetteAmount;
	uniform fixed _VignetteSoftness;
	uniform fixed4 _MainTex_TexelSize;

	v2f vert(appdata i) 
	{
		v2f o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(v2f, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		o.pos = UnityObjectToClipPos(i.pos);
		o.uv.xy = UnityStereoTransformScreenSpaceTex(i.uv);
		o.uv.zw = i.uv - 0.5h;
#if defined(SHARPEN)
		o.uv1 = fixed4(o.uv.xy - _MainTex_TexelSize.xy, o.uv.xy + _MainTex_TexelSize.xy);
#endif
#if defined(BLUR)
		o.uv2 = fixed4(o.uv.xy - _MainTex_TexelSize.xy * _Blur, o.uv.xy + _MainTex_TexelSize.xy * _Blur);
#endif
		return o;
	} 

	fixed4 fragFilter(v2f i) : SV_Target 
	{
		UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
		fixed4 c = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv.xy);
#if defined(SHARPEN)
		c *= _CentralFactor;
		c -= UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv1.xy) * _SideFactor;
		c -= UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv1.xw) * _SideFactor;
		c -= UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv1.zy) * _SideFactor;
		c -= UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv1.zw) * _SideFactor;
#endif
#if defined(BLUR)
		fixed4 m = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MaskTex, i.uv.xy);
		fixed4 b = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv2.xy);
		b += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv2.xw);
		b += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv2.zy);
		b += UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.uv2.zw);
		c = lerp(c, b * 0.25h, m.r);
#endif
		c.rgb = c.rgb * _HueCos + cross(huevec, c.rgb) * _HueSin + dot(huevec, c.rgb) * _HueVector;
		c.rgb = (c.rgb - 0.5h) * _Contrast + _Brightness;
		c.rgb = lerp(dot(c.rgb, satur), c.rgb, _Saturation) * _Color.rgb;
#if defined(VIGNETTE)
		c.rgb = lerp(_VignetteColor.rgb, c.rgb, smoothstep(_VignetteAmount, _VignetteSoftness, sqrt(dot(i.uv.zw, i.uv.zw))));
#endif
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
	      #pragma fragment fragFilter
	      #pragma fragmentoption ARB_precision_hint_fastest
		  #pragma shader_feature_local BLUR
		  #pragma shader_feature_local SHARPEN
		  #pragma shader_feature_local VIGNETTE
	      ENDCG
	  	}
	}
	Fallback off
}