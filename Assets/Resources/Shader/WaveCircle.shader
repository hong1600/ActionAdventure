Shader "UI/WaveCircleVertical"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Fill ("Fill Amount", Range(0,1)) = 1       // 게이지 채움 비율
        _Speed ("Speed", Float) = 1                 // 물결 속도
        _Frequency ("Frequency", Float) = 10        // 물결 주기
        _Amplitude ("Amplitude", Float) = 0.02      // 물결 크기
        _WaveRange ("Wave Range", Range(0,0.2)) = 0.05 // 경계선 아래 파형 적용 범위
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
        Lighting Off
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            float _Fill;
            float _Speed;
            float _Frequency;
            float _Amplitude;
            float _WaveRange;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 tex = tex2D(_MainTex, uv);
            
                // 알파 없는 부분(원 밖)은 제거
                if (tex.a < 0.1)
                    discard;
            
                // Fill (아래=0, 위=1 기준)
                float fillLine = _Fill;
            
                // 파형 마스크
                float waveMask = 0;
                if (_Fill < 1.0)
                {
                    waveMask = smoothstep(fillLine - _WaveRange, fillLine, uv.y);
                }
            
                // 파형 적용
                float wave = 0;
                if (waveMask > 0)
                {
                    wave = sin(uv.x * _Frequency + _Time.y * _Speed) * _Amplitude * waveMask;
                    uv.y += wave;
                }
            
                // Fill 위쪽 제거
                if (uv.y > fillLine)
                    discard;
            
                return tex * i.color;
            } 
            ENDCG
        }
    }
}
