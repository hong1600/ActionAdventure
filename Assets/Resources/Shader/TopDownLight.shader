Shader "Unlit/LightBeam_DualFan"
{
    Properties
    {
        _MainTex("Noise (optional)", 2D) = "white" {}
        _Color("Beam Color (HDR)", Color) = (1,1,0.85,1)
        _Intensity("Intensity", Range(0,5)) = 1.6
        _Opacity("Opacity", Range(0,1)) = 1

        // overall tilt of the whole beam set
        _BaseAngleDeg("Base Tilt (deg)", Range(-60,60)) = -8

        // symmetric fan: beams open to both left/right from center
        _FanAngleDeg("Fan Angle (deg)", Range(0,45)) = 16
        _FanByY("Fan Growth by Y", Range(0,2)) = 1.0   // how much it opens as going down

        // vertical fade softness
        _Softness("Edge Softness", Range(0.1,5)) = 2.0

        // band (streak) look
        _BandAmt("Band Amount", Range(0,1)) = 0.55
        _BandFreq("Band Freq", Range(2,20)) = 10
        _BandSkewY("Band Skew Y", Range(0,6)) = 2.0    // add a little y to avoid perfect parallel lines
        _BandJitter("Band Jitter", Range(0,2)) = 0.6

        // scroll control
        _Static("Static Mode (0/1)", Range(0,1)) = 1
        _ScrollY("Scroll Speed Y", Range(-2,2)) = -0.2

        // center pos (0..1)
        _CenterX("Center X", Range(0,1)) = 0.5
        _CenterY("Center Y", Range(0,1)) = 0.55
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend One One
        Cull Off
        Lighting Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;
            float _Intensity, _Opacity;
            float _BaseAngleDeg, _FanAngleDeg, _FanByY, _Softness;
            float _BandAmt, _BandFreq, _BandSkewY, _BandJitter;
            float _Static, _ScrollY;
            float _CenterX, _CenterY;

            struct appdata { float4 vertex:POSITION; float2 uv:TEXCOORD0; };
            struct v2f { float4 pos:SV_POSITION; float2 uv:TEXCOORD0; };

            v2f vert(appdata v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 rotate(float2 p, float rad){
                float s = sin(rad), c = cos(rad);
                return float2(c*p.x - s*p.y, s*p.x + c*p.y);
            }

            float verticalMask01(float y01, float softness){
                float m = saturate(1.0 - y01);
                return pow(m, softness);
            }

            fixed4 frag(v2f i):SV_Target
            {
                // base uv
                float2 uv01 = i.uv;

                // optional scroll for noise sampling only
                float2 nUV = uv01;
                if (_Static < 0.5) nUV.y += _Time.y * _ScrollY;

                // center to -1..1 space
                float2 p = float2(uv01.x - _CenterX, uv01.y - _CenterY) * 2.0;

                // overall tilt for the whole set
                float2 pr = rotate(p, radians(_BaseAngleDeg));

                // normalized vertical (0..1) after tilt, used for fade and fan growth
                float y01 = saturate( (pr.y + 1.0) * 0.5 );

                // ---- dual fan rotation ----
                // sign of x determines left/right branch
                float side = (pr.x >= 0.0) ? 1.0 : -1.0;

                // fan angle grows as we go down (y01 up)
                float fanDeg = _FanAngleDeg * (1.0 + _FanByY * y01);
                float2 pfl = pr;

                // rotate each side around local origin by +/- fanDeg
                pfl = rotate(pfl, radians(side * fanDeg));

                // ---- band generation ----
                // add slight y skew to avoid perfectly even spacing
                float n = tex2D(_MainTex, nUV).r;
                float freq = _BandFreq + _BandJitter * (n*2-1);
                float bands = 0.5 + 0.5 * sin(pfl.x * freq + pfl.y * _BandSkewY + n*3.14);

                float bandMix = lerp(1.0, bands, _BandAmt);

                // vertical fade along tilted axis (use original tilted y)
                float vm = verticalMask01(y01, _Softness);

                float beam = saturate(vm * bandMix) * _Opacity;

                fixed3 col = _Color.rgb * _Intensity * beam;
                return fixed4(col, 1);
            }
            ENDCG
        }
    }
}
