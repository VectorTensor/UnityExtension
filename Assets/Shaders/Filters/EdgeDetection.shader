Shader "Filter/EdgeDetection"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #pragma fragment fragX

            #include "UnityCG.cginc"

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
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;

            static const float3x3 sobel_x = float3x3(
                -1,  0,  1,
                -2,  0,  2,
                -1,  0,  1);

            
            static const float3x3 sobel_y = float3x3(-1, -2, -1,
         0,  0,  0,
         1,  2,  1);

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 fragX(v2f i) : SV_Target
            {
                float3 luminance_X= float3(0,0,0);
                float3 luminance_Y=float3(0,0,0);
                
                int count = 0;
                for(int x =-1 ; x<=1;x++)
                {

                    for(int y =-1 ;y<=1;y++)
                    {
                        
                        float filterX = sobel_x[x+1][y+1]; 
                        float filterY = sobel_y[x+1][y+1];
                        float2 uv = float2(i.uv.x + _MainTex_TexelSize.x*x,i.uv.y+_MainTex_TexelSize.y*y );
                        float3 colX = tex2D(_MainTex,uv);
                        float3 colY = tex2D(_MainTex,uv);
                        luminance_X += colX*filterX;
                        luminance_Y += colY*filterY;

                        count++;
                        
                    }
                    
                }
                float3 luminace = luminance_X * luminance_X +luminance_Y * luminance_Y;
                luminace = sqrt(luminace);
                float4 act = tex2D(_MainTex,i.uv);
                act.xyz = act.xyz-luminace;
                
                
                return float4(act);
                
                
            }
            ENDCG
        }

    }
}