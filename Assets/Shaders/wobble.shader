Shader "Unlit/wobble"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}        

        _Amp("Amplitude", Float) = 5
        _Freq("Frequency", Float) = 0.05
        _Speed("Speed", Float) = 2
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float _Amp;
            float _Freq;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float row = uv.y / _MainTex_TexelSize.y;

                float offsetPixel = sin(row * _Freq + _Time.y * _Speed) * _Amp;

                float offsetUV = offsetPixel * _MainTex_TexelSize.x;

                uv.x += offsetUV;

                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}