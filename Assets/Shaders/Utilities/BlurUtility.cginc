
const float PI = 3.14159265359;
const float E = 2.71828182846;

float gaussian2D(int x, int y, float sigma, float A=1) {
    
    float exponent = -((pow(x, 2) + pow(y, 2)) / (2 * pow(sigma, 2)));
    return A * exp(exponent);
}