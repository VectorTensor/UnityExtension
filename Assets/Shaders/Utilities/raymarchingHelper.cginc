
#define  MAX_STEPS 100 
#define MAX_DIST 100 
#define SURF_DIST 1e-3

float TorusDist(float3 p)
{

    float d ;
    d = length(float2(length(p.xz) - .5, p.y))-0.1;
    return d;
    
    
}
float sdCone( float3 p, float2 c, float h )
{
  // c is the sin/cos of the angle, h is height
  // Alternatively pass q instead of (c,h),
  // which is the point at the base in 2D
  float2 q = h*float2(c.x/c.y,-1.0);
    
  float2 w = float2( length(p.xz), p.y );
  float2 a = w - q*clamp( dot(w,q)/dot(q,q), 0.0, 1.0 );
  float2 b = w - q*float2( clamp( w.x/q.x, 0.0, 1.0 ), 1.0 );
  float k = sign( q.y );
  float d = min(dot( a, a ),dot(b, b));
  float s = max( k*(w.x*q.y-w.y*q.x),k*(w.y-q.y)  );
  return sqrt(d)*sign(s);
}


