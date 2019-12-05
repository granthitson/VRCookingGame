Shader "Custom/blendShader"
{
	Properties{
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_AltTex ("Alt (RGB)", 2D) = "white" {} // Add another texture property
	_LerpValue ("Lerp Value", Range(0.0, 1.0)) = 0 // Add an interpolation property
	}

		SubShader{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque"}
			LOD 200

		CGPROGRAM
		#pragma surface surf Lambert alpha:blend

		sampler2D _MainTex;
		sampler2D _AltTex; // define the properties in the shader program
		fixed4 _Color;
		float _LerpValue;

		struct Input {
			float2 uv_MainTex;
			float2 uv_AltTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c1 = tex2D(_MainTex, IN.uv_MainTex); // sample a color from the main tex
			fixed4 c2 = tex2D(_AltTex, IN.uv_AltTex); // sample a color from the alt tex
			fixed4 c = lerp(c1, c2, _LerpValue); // interpolate
			o.Albedo = c.rgb; // apply the color
			o.Alpha = c.a;
		}
		ENDCG
	}
    FallBack "Diffuse"
}
