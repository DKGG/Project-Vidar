Shader "Custom/RimMiscellaneous"
{
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _RimValue("Rim value", Range(0, 5)) = 0.5
        _AuraBehavior("aura Behavior", Range(0, 5)) = 1.0
        _Movement("Movement", Range(0,1)) = 0
        _Alpha("Alpha", Range(0, 1)) = 1
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader{
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        fixed _RimValue;
        fixed _Movement;
        fixed _AuraBehavior;
        fixed _Alpha;

        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldNormal;
        };

        fixed4 _Color;

        void surf(Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex + _Time.y * _Movement);
            o.Albedo = c.rgb * _Color;

            float3 normal = normalize(IN.worldNormal);
            float3 dir = normalize(IN.viewDir);
            float val = _AuraBehavior - (abs(dot(dir, normal)));
            float rim = val * val * _RimValue;
            o.Alpha = c.a * _Alpha;
            o.Emission = rim ;

            //o.Alpha = c.a; colocar rimLigth no o.emission
            //ver o outro rimLigth
        }
        ENDCG
    }
    FallBack "Diffuse"
}