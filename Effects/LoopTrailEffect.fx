sampler uImage0 : register(s0);


float4x4 uTransform;
float4 color;
float laserLength;
float imageLength;
float progress;

struct VSInput 
{
	float2 Pos : POSITION0;
	float4 Color : COLOR0;
	float3 Texcoord : TEXCOORD0;
};

struct PSInput 
{
	float4 Pos : SV_POSITION;
	float4 Color : COLOR0;
	float3 Texcoord : TEXCOORD0;
};


float4 PixelShaderFunction(PSInput input) : COLOR0 
{
	float3 coord = input.Texcoord;
    float y = coord.y;
    float x = coord.x * laserLength + progress;
    x -= (int) (x / imageLength) * imageLength;
    if (x < 0)
        x = x + imageLength;
    x /= imageLength;
    float4 c = tex2D(uImage0, float2(x, y));
    return c * color;
}

PSInput VertexShaderFunction(VSInput input)  
{
	PSInput output;
	output.Color = input.Color;
	output.Texcoord = input.Texcoord;
	output.Pos = mul(float4(input.Pos, 0, 1), uTransform);
	return output;
}


technique Technique1 
{
	pass LoopTrailEffedt
	{
		VertexShader = compile vs_2_0 VertexShaderFunction();
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}