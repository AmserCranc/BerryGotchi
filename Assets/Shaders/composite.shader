Shader "Bach/composite"
{
    Properties
    {
        _GameTex ("GameTex", 2D) = "white" {}
        _UITex ("UITex", 2D) = "black" {}
        _UIBarsMask ("UIBarMask", 2D) = "black" {}

        _BarBottom ("Base of bars", Float) = 0

        _FoodLevel ("Food level", Float) = 0
        _EnergyLevel ("Energy level", Float) = 0
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
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
            };

            sampler2D _GameTex;
            sampler2D _UITex;
            sampler2D _UIBarsMask;

            float _BarBottom;
            float _EnergyLevel;
            float _FoodLevel;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Returns 0 or 1 mask for red bar
            float DoRedBar(float2 uv)
            {
                return step(_BarBottom + _EnergyLevel, uv.y);
            }

            // Returns 0 or 1 mask for green bar
            float DoGreenBar(float2 uv)
            {
                return step(_BarBottom + _FoodLevel, uv.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float4 mask = tex2D(_UIBarsMask, uv);
                float maskRed = mask.r;
                float maskGrn = mask.g;

                float4 gameCol = tex2D(_GameTex, uv);
                float4 uiCol   = tex2D(_UITex, uv);

                float4 colourOut;

                // --- Bar rendering ---
                if (maskRed > 0.5)
                {
                    float m = DoRedBar(uv);
                    colourOut = float4(m, 0, 0, 1); // red bar
                }
                else if (maskGrn > 0.5)
                {
                    float m = DoGreenBar(uv);
                    colourOut = float4(0, m, 0, 1); // green bar
                }
                else
                {
                    // --- UI over game ---
                    colourOut = (uiCol.a > 0.001) ? uiCol : gameCol;
                }

                return colourOut;
            }

            ENDCG
        }
    }
}