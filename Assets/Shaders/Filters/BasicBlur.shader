Shader "Filters/BasicBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "Red" {}
        _KernelSize("Blur strength",Int) = 3
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "../Utilities//BlurUtility.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float2 _MainTex_TexelSize;
            float4 _MainTex_ST;
            int _KernelSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 col = float3(0,0,0);
                
                for(int x =0 ;x<_KernelSize;x++)
                {
                    for(int y =0;y<_KernelSize;y++)
                    {

                        float2 uv = float2(i.uv.x +_MainTex_TexelSize.x *x,i.uv.y + _MainTex_TexelSize.y * y  );
                        float filter = gaussian2D(x,y,_KernelSize);
                        col += tex2D(_MainTex,uv).xyz*filter;
                        
                    }
                    
                }
                col /= _KernelSize*_KernelSize;
                
                return float4(col, 1.0f);
            }
            ENDCG
        }
    }
}