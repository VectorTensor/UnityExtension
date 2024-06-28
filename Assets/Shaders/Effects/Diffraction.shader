Shader "Effects/Diffraction"
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
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normals : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float4 vertex2 : TEXCOORD2;
                float3 tangent : TEXCOORD3;
                float3 light: TEXCOORD4;
                
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex2 = mul(unity_MatrixMV,v.vertex);
                o.uv =v.uv;
                o.normals = mul((float3x3) UNITY_MATRIX_IT_MV,v.normals);
                o.tangent = mul((float3x3) UNITY_MATRIX_IT_MV, v.tangent);
                o.light= mul(unity_MatrixV,_WorldSpaceLightPos0);
                //o.normals = UnityObjectToWorldNormal(v.normals);
                return o;
            }
            float3 blend3(float3 x)
            {
                float3 y = 1-x*x;
                y = max(y,float3(0,0,0));
                return (y);
                
            }
            float3 Diffraction(
                float4 position_Camera,
                float3 normal,
                float3 tangent,
                uniform float r,
                uniform float d,
                uniform float4 hiliteColor,
                uniform float3 lightPosition,
                uniform float3 eyePostion
            )
            {

                float3 P = position_Camera;
                float3 L = normalize(lightPosition - P );
                float3 V = normalize(eyePostion - P);
                float3 H = L + V;
                float3 N = normal;
                float3 T = tangent;
                float u = dot(T,H) *d;
                float w = dot(N,H);
                float e = r * u/w;
                float c = exp(-e * e);
                float4 anis = hiliteColor * float4(c.x,c.x,c.x,1);

                if (u<0) u= -u;
                float4 cdiff = float4(0,0,0,1);
                for(int n =1 ;n<8;n++)
                {
                    float y = 2*u/n -1;
                    cdiff.xyz += blend3(float3(4*(y-0.75) ,4*(y-0.5),4*(y-0.25)));
                    
                    
                }

                return cdiff + anis;
                
                
                
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 color = float4(1,1,1,1);
                float3 diff = Diffraction(i.vertex2,i.normals,i.tangent,2,2,color,i.light,float3(0,0,0));
                return float4(diff,1);
            }
            ENDCG
        }
    }
}