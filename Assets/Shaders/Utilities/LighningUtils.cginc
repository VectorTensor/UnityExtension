
#include "Lighting.cginc"

float4 GetFlatShading(float3 normals)
{

    float3 ambient = ShadeSH9(half4(normals, 1));
    float3 diffuse = _LightColor0* max(0, dot(normals, _WorldSpaceLightPos0.xyz));
    return float4(ambient + diffuse, 1.0f); 
    
}