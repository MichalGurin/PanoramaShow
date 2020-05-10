Shader "Unlit/Pano360"
{
   Properties
   {
       _MainTex ("Base (RGB)", 2D) = "black" {}
	   _SecTex("Sec Albedo (RGB)", 2D) = "black" {}

	   _Blend("Blend value", Range(0,1)) = 0.0
   }   
   
   SubShader 
   {
      Tags { "RenderType" = "Opaque" }
	  Cull Off ZWrite Off Lighting Off
      
	  CGPROGRAM
      #pragma surface surf SimpleLambert
      half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
      {
         half4 c;
         c.rgb = s.Albedo;
		 c.a = 1;
         return c;
      }
      
      sampler2D _MainTex;
	  sampler2D _SecTex;
      struct Input
      {
         float2 uv_MainTex;
      };

	  half _Blend;
      void surf (Input IN, inout SurfaceOutput o)
      {
		 IN.uv_MainTex.x = 1 - IN.uv_MainTex.x;

         fixed4 texture1 = tex2D(_MainTex, IN.uv_MainTex);
		 fixed4 texture2 = tex2D(_SecTex, IN.uv_MainTex);
		 fixed4 result = lerp(texture1, texture2, _Blend);

         o.Albedo = result.rgb;
         o.Alpha = 1;
      }
      ENDCG
   }
   Fallback "Diffuse"
}