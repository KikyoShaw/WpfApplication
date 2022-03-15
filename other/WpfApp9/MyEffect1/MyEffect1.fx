sampler2D input : register(s0);
float4 transparentColor : register(c0);

bool equals(float f1, float f2)
{
    float f = abs(f1 - f2);
    return f < 0.10f;//0.0001f;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 result = tex2D(input, uv);
    float2 uv2 = uv;
    uv2.x += result.w / 2;

    float4 result2 = tex2D(input, uv2);

    if (uv.x > result.w / 2)
    {
        result.r = 0;
        result.g = 0;
        result.b = 0;
        result.a = 0;
        return result;
    }
    float a = result.r;
    if (a < 0.1f)
    {
        result.r = 0;
        result.g = 0;
        result.b = 0;
        result.a = 0;
        return result;
    }
    result.a = a;
    result.r = result2.r;
    result.g = result2.g;
    result.b = result2.b;

    //if ((equals(result.r, transparentColor.r)) &&
    //    (equals(result.g, transparentColor.g)) &&
    //    (equals(result.b, transparentColor.b)))
    //{
    //    result.r = 0;
    //    result.g = 0;
    //    result.b = 0;
    //    result.a = 0;
    //}

    return result;
}