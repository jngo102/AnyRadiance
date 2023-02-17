Shader "Hidden/ColorCorrectionCurvesSimple" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "" {}
        _RgbTex("_RgbTex (RGB)", 2D) = "" {}
    }
        //DummyShaderTextExporter
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200
            CGPROGRAM
    #pragma surface surf Standard
    #pragma target 3.0

            sampler2D _MainTex;
            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
    }
}