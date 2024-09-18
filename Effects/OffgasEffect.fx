sampler uImage0 : register(s0);

float4 color; //颜色
float r; //0-1之间，为偏移量


float4 PixelShaderFunction(float2 coords : TEXCOORD0) : COLOR0
{
    float y = coords.y;
    y += r;
    if (y > 1)
        y--;
    return tex2D(uImage0, float2(coords.x, y)) * color;

}



technique Technique1
{
    pass TrailEffect
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
