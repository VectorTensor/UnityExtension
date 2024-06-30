Shader "Effects/Images_blend"
{
    Properties
    {
        _Texture1 ("Texture 1", 2D) = "white" {}
        _Texture2 ("Texture 2", 2D) = "white" {}
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
                float3 normals : NORMAL;
                
                
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normals : TEXCOORD1;
                float3 normaldir : TEXCOORD2;
                float3 vertex2: TEXCOORD3;
            };
            
            sampler2D _Texture1;
            sampler2D _Texture2;
            

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normals = mul((float3x3) unity_ObjectToWorld , v.normals);
                float3 x = mul(unity_ObjectToWorld,v.vertex);

                float3 cam = _WorldSpaceCameraPos;
                o.normaldir = normalize(x - cam);
                o.vertex2 = x;
                
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 normVect = normalize(i.normals);
                float angle = dot(i.vertex2, i.normaldir);
                float3 col1  = tex2D(_Texture1,i.uv);
                float3 col2 = tex2D(_Texture2,i.uv);
                float3 col = lerp(col1,col2,angle);
                
                return float4(col,1);
                
            }
            ENDCG
        }
    }
}