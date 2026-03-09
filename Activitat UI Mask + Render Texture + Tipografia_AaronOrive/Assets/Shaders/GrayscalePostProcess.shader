// Shader de post-procès per convertir la imatge a escala de grisos (o efecte sepia)
// Compatible amb Built-in Render Pipeline de Unity
// Usa'l en un Material i assigna'l al camp PostProcessMaterial del script ScreenshotRenderTexture

Shader "Custom/GrayscalePostProcess"
{
    Properties
    {
        _MainTex    ("Source Texture", 2D)     = "white" {}
        _Intensity  ("Gray Intensity", Range(0,1)) = 0.85
        _Contrast   ("Contrast",       Range(0.5, 2.0)) = 1.2
        _Brightness ("Brightness",     Range(-0.5, 0.5)) = 0.0
        _Sepia      ("Sepia Tint",     Range(0,1)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Intensity;
            float _Contrast;
            float _Brightness;
            float _Sepia;

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Lluminositat percebuda
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));

                // Barreja color original + gris
                float3 grayColor = lerp(col.rgb, float3(gray, gray, gray), _Intensity);

                // Tint sepia
                float3 sepiaColor = float3(
                    dot(grayColor, float3(0.393, 0.769, 0.189)),
                    dot(grayColor, float3(0.349, 0.686, 0.168)),
                    dot(grayColor, float3(0.272, 0.534, 0.131))
                );
                float3 finalColor = lerp(grayColor, sepiaColor, _Sepia);

                // Contrast i Brightness
                finalColor = (finalColor - 0.5) * _Contrast + 0.5 + _Brightness;
                finalColor = saturate(finalColor);

                return fixed4(finalColor, col.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
