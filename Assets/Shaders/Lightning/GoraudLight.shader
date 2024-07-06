Shader "Lightning/GoraudLight"
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
            // make fog work
            #pragma multi_compile_fog

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
                float4 diffuseLight: TEXCOORD1;
                float4 specularLight: TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainColor;
            float _GlossPower;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                float3 normalWS = UnityObjectToWorldNormal(v.normalOS);
                float3 viewWS = normalize(WorldSpaceViewDir(v.vertex));
                o.diffuseLight = GetBasicShading(normalWS);
                o.specularLight  = float4(GetSpecularShading(normalWS,o.diffuseLight,viewWS,_GlossPower));
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                return col * _MainColor * i.diffuseLight + i.specularLight; 
                return col;
                
            }
            ENDCG
        }
    }
}