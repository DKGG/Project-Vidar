Shader "Custom/DiscardFragment"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Value ("Value", Range(0, 1)) = 0.0
        _ColorInside ("Color inside", Color) = (1, 1, 1, 1)
        _ColorOutside ("Color outside", Color) = (1, 1, 1, 1)
         _Teste("opacidade", Range(0, 1)) = 1
    }
 
    SubShader
    {
            Tags { "RenderType"="Opaque" }
       
            CGPROGRAM
            #pragma surface surf Lambert
 
            struct Input {
                float2 uv_MainTex;
            };
 
            float _Value;
            fixed4 _ColorOutside;
            sampler2D _MainTex;
            fixed _Teste;
 
            void surf(Input IN, inout SurfaceOutput o) {
                //fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
                half4 c = tex2D(_MainTex, IN.uv_MainTex + _Time.y * 0.1);
 
                if(c.r < _Value)
                {
                    discard;
                }
 
                o.Albedo = _ColorOutside;
                o.Alpha = c.a - _Teste;
            }
 
            ENDCG
 
            Cull Front
 
            CGPROGRAM
            #pragma surface surf Lambert
 
            struct Input {
                float2 uv_MainTex;
            };
 
            fixed4 _ColorInside;
            float _Value;
            sampler2D _MainTex;
            fixed _Teste;
 
            void surf(Input IN, inout SurfaceOutput o) {
                //fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
                half4 c = tex2D(_MainTex, IN.uv_MainTex + _Time.y * 0.1);
 
                if(c.r < _Value)
                {
                    discard;
                }
 
                o.Albedo = _ColorInside;
                o.Alpha = c.a - _Teste;
            }
 
            ENDCG
    }
}