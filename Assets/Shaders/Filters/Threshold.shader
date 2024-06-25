Shader "Filter/Threshold"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold("Threshold",Range(0,1)) = 0.2
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
            float4 _MainTex_ST;
            float _Threshold;
            float3 ThresholdColor(float3 col, float threshold)
            {

                float3 color = float3(0,0,0);
                float gray = (col.x + col.y + col.z)/3.0;

                if(gray > threshold)
                {

                    color = float3(1,1,1);
                    
                }
                else
                {
                    color = float3(0,0,0);
                    
                }
                return color;
                
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float3 col = tex2D(_MainTex, i.uv);
                col = ThresholdColor(col,_Threshold);
                
                return float4(col,1);
            }

            
            ENDCG
        }
    }
}