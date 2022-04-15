Shader "Mega Textures/Mega Shader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Normal] _BumpMap("Bumpmap", 2D) = "bump" {}
		_RoughnessTex("Roughness (R)", 2D) = "white" {}
		_MetallicTex("Metallic (R)", 2D) = "white" {}
		_OcclusionTex("Occlusion (R)", 2D) = "white" {}
		_NormalStrength("Normal Strength", Range(0.01,1)) = 1
		_OcclusionStrength("Occlusion Strength", Range(0,1)) = 1
		_Roughness ("Roughness", Range(0,1)) = 1
		_Metallic ("Metallic", Range(0,1)) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _RoughnessTex;
		sampler2D _MetallicTex;
		sampler2D _OcclusionTex;

		struct Input {
			float2 uv_MainTex;
			//float2 uv_BumpMap;
		};

		half _Roughness;
		half _Metallic;

		half _NormalStrength;

		half _OcclusionStrength;

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = tex2D(_MetallicTex, IN.uv_MainTex).r * _Metallic;
			o.Smoothness = 1 - ( tex2D(_RoughnessTex, IN.uv_MainTex).r * _Roughness );

			o.Normal = UnpackNormal( tex2D(_BumpMap, IN.uv_MainTex) );
			o.Normal.z /= _NormalStrength;

			o.Normal = normalize(o.Normal);

			o.Occlusion = lerp(1, tex2D(_OcclusionTex, IN.uv_MainTex).r, _OcclusionStrength);

			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
