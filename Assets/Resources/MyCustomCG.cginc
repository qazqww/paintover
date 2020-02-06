// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

#ifndef UNITY_SPRITES_INCLUDED
#define UNITY_SPRITES_INCLUDED

#include "UnityCG.cginc"

#ifdef UNITY_INSTANCING_ENABLED

    UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
        // SpriteRenderer.Color while Non-Batched/Instanced.
        UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
        // this could be smaller but that's how bit each entry is regardless of type
        UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
    UNITY_INSTANCING_BUFFER_END(PerDrawSprite)

    #define _RendererColor  UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
    #define _Flip           UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)

#endif // instancing

CBUFFER_START(UnityPerDrawSprite)
#ifndef UNITY_INSTANCING_ENABLED
    fixed4 _RendererColor;
    fixed2 _Flip;
#endif
    float _EnableExternalAlpha;
CBUFFER_END

// Material Color.
fixed4 _Color;

fixed _DissolveSpeed;

fixed _DissolveAmount;

float _ColorR;
float _ColorG;
float _ColorB;

// 디졸브 쉐이더에 적용할 컬러의 범위를 설정하기 위해 추가하였습니다.
float _DissolveRampSize;



struct appdata_t
{
    float4 vertex   : POSITION;
    float4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float4 vertex   : SV_POSITION;
    fixed4 color    : COLOR;
    float2 texcoord : TEXCOORD0;
    UNITY_VERTEX_OUTPUT_STEREO
};


inline float4 UnityFlipSprite(in float3 pos, in fixed2 flip)
{
    return float4(pos.xy * flip, pos.z, 1.0);
}


v2f SpriteVert(appdata_t IN)
{
    v2f OUT;
    UNITY_SETUP_INSTANCE_ID (IN);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

    OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
    OUT.vertex = UnityObjectToClipPos(OUT.vertex);
    OUT.texcoord = IN.texcoord;
    OUT.color = IN.color * _Color * _RendererColor;

    #ifdef PIXELSNAP_ON
    OUT.vertex = UnityPixelSnap (OUT.vertex);
    #endif

    return OUT;

}

sampler2D _MainTex;
sampler2D _AlphaTex;
sampler2D _DissolveTex;
// 디졸브 쉐이더에 적용할 컬러 텍스쳐입니다.
sampler2D _DissolveRamp;

fixed4 SampleSpriteTexture (float2 uv)
{
    fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
    fixed4 alpha = tex2D (_AlphaTex, uv);
    color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif

    return color;
}

fixed3 AddDissolve( fixed3 c, v2f IN )
{
	float2 distortionuv = IN.texcoord + _Time.x * _DissolveSpeed;
	fixed dissolveColor = tex2D(_DissolveTex, distortionuv).r;

	// 아래의 함수는 시점과 종점을 받고, 디졸브 간격을 조정하기 위해 작성한 코드입니다. 
	float ramp = smoothstep(_DissolveAmount, _DissolveAmount + _DissolveRampSize, dissolveColor);
	float3 rampColor = tex2D(_DissolveRamp, float2(ramp, 0));
	float3 rampContribution = rampColor * (1 - ramp);
	c = c.rgb + rampContribution;
	clip(dissolveColor - _DissolveAmount);

	return c;
}

fixed3 AddRGB( fixed3 c )
{
	c.r += _ColorR;
	c.g += _ColorG;
	c.b += _ColorB;
	return c;
}



fixed4 SpriteFrag(v2f IN) : SV_Target
{
    fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
	fixed dissolveColor = tex2D(_DissolveTex, IN.texcoord).r;
	// 디졸브 효과 추가
	c.rgb = AddDissolve(c.rgb, IN);
	// rgb 색상을 제어하기 위한 요소 추가
	c.rgb = AddRGB( c.rgb );

	c.rgb *= c.a;
	return c;
}

#endif // UNITY_SPRITES_INCLUDED
