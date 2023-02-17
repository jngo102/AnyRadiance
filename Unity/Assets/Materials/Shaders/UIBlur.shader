Shader "UI/Blur/UIBlur" {
    Properties{
        _TintColor("Tint Color", Vector) = (1,1,1,0.2)
        _Size("Spacing", Range(0, 70)) = 5
        _Vibrancy("Vibrancy", Range(0, 2)) = 0.2
        [HideInInspector] _StencilComp("Stencil Comparison", Float) = 8
        [HideInInspector] _Stencil("Stencil ID", Float) = 0
        [HideInInspector] _StencilOp("Stencil Operation", Float) = 0
        [HideInInspector] _StencilWriteMask("Stencil Write Mask", Float) = 255
        [HideInInspector] _StencilReadMask("Stencil Read Mask", Float) = 255
    }
        //DummyShaderTextExporter
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200
            CGPROGRAM
    #pragma surface surf Standard
    #pragma target 3.0

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                o.Albedo = 1;
            }
            ENDCG
    }
}