Shader "Raymarching/Torus"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _MainColor("Color",Color)= (1,1,1,1)
        
        _GlossPower("Gloss Power", Float) = 400 
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
             "Queue" = "Geometry"
        }

        Pass
        {
            
            Tags
            {
                "LightMode" = "ForwardBase"
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "../Utilities/raymarchingHelper.cginc"
            #include "../Utilities/LighningUtils.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                
                float3 ro : TEXCOORD1;
                float3 hitPos : TEXCOORD2;
                float3 viewdir : TEXCOORD3;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float _GlossPower;
                
            float RayMarch(float3 ro, float3 rd)
            {
                float d0 = 0;
                float dS;
                for (int i=0;i<= MAX_STEPS;i++)
                {
                    float3 p = ro + d0 *rd;
                    dS = TorusDist(p);
                    d0 +=dS;
                    if(dS<SURF_DIST || d0 > MAX_DIST) break;
                    
                }
                return d0;
            }
            float3 GetNormal(float3 p)
            {

                float2 e = float2(1e-2,0);
                float3 n = TorusDist(p) - float3(

                TorusDist(p- e.xyy),
                TorusDist(p-e.yxy),
                TorusDist(p-e.yyx)
                );
                return normalize(n);
                
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.ro =mul(unity_WorldToObject, float4(_WorldSpaceCameraPos,1));
                o.hitPos = v.vertex;
                o.viewdir = UnityObjectToWorldDir(v.vertex);
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5;
                float3 ro = i.ro ;// i.ro;
                float3 rd = normalize(i.hitPos- ro);
                float d = RayMarch(ro, rd);
                

                float view = normalize(i.viewdir);
                float4 col = 0;
                if (d<MAX_DIST)
                {
                    float3 p = ro + rd * d;
                    float3 n = GetNormal(p);
                    float4 diffuse = GetBasicShading(n);
                    float4 spec = GetSpecularShading(n,diffuse,view,_GlossPower);
                    col = _MainColor*diffuse + spec;
                    
                }
                else
                {
                    discard;
                }
                return col;
            }
            
            ENDCG
        }
    }
}
