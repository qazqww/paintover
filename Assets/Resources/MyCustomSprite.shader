// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/MySpriteEdgeCol"
{
    Properties
    {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
		
		// 디졸브 효과 강도
		_DissolveAmount("Dissolove Amount", Range(0, 1)) = 0

		// 디졸브 효과에 사용할 텍스처 (노이즈 이미지)
		_DissolveTex("DissolveTex", 2D) = "white" {}

		_DissolveRamp("DissolveRamp", 2D) = "white" {}

		_DissolveSpeed("DissolveSpeed", Range(0, 5)) = 0

		// 디졸브 쉐이더에 적용할 색상 이미지의 범위 값
		_DissolveRampSize("DissolveRampSize", Float) = 0.2

		// RGB 색상을 변경할 때 사용할 수 있도록 추가
		_ColorR("ColorR", Range(0,1)) = 0
		_ColorG("ColorG", Range(0,1)) = 0
		_ColorB("ColorB", Range(0,1)) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        //Blend One OneMinusSrcAlpha
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
			CGPROGRAM
			#pragma vertex SpriteVert
			#pragma fragment SpriteFrag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "MyCustomCG.cginc"
			ENDCG
        }
    }
	Fallback "Transparent/VertexLit"
}
