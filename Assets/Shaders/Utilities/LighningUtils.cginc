
#include "Lighting.cginc"

// Get ambient and diffuse lighting
float4 GetBasicShading(float3 normals)
{

    float3 ambient = ShadeSH9(half4(normals, 1));
    float3 diffuse = _LightColor0* max(0, dot(normals, _WorldSpaceLightPos0.xyz));
    return float4(ambient + diffuse, 1.0f); 
    
}

float4 GetSpecularShading(float3 normals,float4 diffuse, float3 viewDir,float glossPower)
{
    float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
    float specular = max(0,dot(normals,halfVector)) * diffuse;
    specular = pow(specular,glossPower);
    float3 specularColor = _LightColor0 * specular;
    return float4(specularColor,1);
    
}

float4 GetFresnelShading(float fresnelPower,float3 normals, float3 viewDir)
{

    float fresnel = 1 - max(0,dot(normals,viewDir));
    fresnel = pow(fresnel,fresnelPower);
    float3 fresnelColor = _LightColor0 * fresnel;
    return float4(fresnelColor,1);
    
}