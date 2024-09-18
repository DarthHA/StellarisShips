sampler uImage0 : register(s0);


//命中中心
float2 center;
//内圈完全绘制
float r1;
//外圈做透明度插值
float r2;
//没用的颜色
float4 color;


float4 PixelShaderFunction(float2 texCoords : TEXCOORD0) : COLOR
{
    // 获取像素相对于中心的偏移
    float2 delta = texCoords - center;
    float distance = length(delta);
    float a;
    if (distance < r1)
    {
        if (distance > r1 - r2)
        {
            a = (distance - r1 + r2) / r2;
        }
        else
        {
            a = 0;
        }
    }

    if (distance > r1 + r2)
        return 0;
    if (distance > r1 && distance < r1 + r2)
    {
        a = (r1 + r2 - distance) / r2;
    }
    return tex2D(uImage0, float2(texCoords.x, texCoords.y)) * a * color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}