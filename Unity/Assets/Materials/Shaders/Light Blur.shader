Shader "Hollow Knight/Light Blur" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _BlurInfo("Blur Info", Vector) = (0.00052083336,0.0009259259,0,0)
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