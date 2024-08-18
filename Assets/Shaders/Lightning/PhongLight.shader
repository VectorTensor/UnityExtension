Shader "Lightning/PhongLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor("Color",Color)= (1,1,1,1)
        _GlossPower("Gloss Power", Float) = 400 
        _FresnelPower("Fresnel Power", Float) = 5
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
            #define SPECULAR 

            #include "UnityCG.cginc"
            #include "../Utilities/LighningUtils.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normalOS: NORMAL;
                
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normalWS : TEXCOORD1;
                float3 viewWS : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float _GlossPower;
            float _FresnelPower;
            

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normalWS = UnityObjectToWorldNormal(v.normalOS);
                
                o.viewWS = normalize(WorldSpaceViewDir(v.vertex));
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 normal = normalize(i.normalWS);
                float3 view = normalize(i.viewWS);
                
                float4 col = tex2D(_MainTex, i.uv);
                float4 diffuseLight = GetBasicShading(normal);
                float4 specularLight = float4(0,0,0,1);// = GetSpecularShading(normal,diffuseLight,view,_GlossPower);
                
                float4 fresnelLight = float4(0,0,0,1);// = GetFresnelShading(_FresnelPower,normal,view);
                if (_GlossPower != 0)
                {
                    
                    specularLight = GetSpecularShading(normal,diffuseLight,view,_GlossPower);
                }
                if(_FresnelPower != 0)
                {
                    
                    fresnelLight = GetFresnelShading(_FresnelPower,normal,view);
                }
                
                
                return col * _MainColor * diffuseLight + specularLight+fresnelLight; 
                return col;
                
            }
            ENDCG
        }
    }
}