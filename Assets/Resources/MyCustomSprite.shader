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
		
		// ������ ȿ�� ����
		_DissolveAmount("Dissolove Amount", Range(0, 1)) = 0

		// ������ ȿ���� ����� �ؽ�ó (������ �̹���)
		_DissolveTex("DissolveTex", 2D) = "white" {}

		_DissolveRamp("DissolveRamp", 2D) = "white" {}

		_DissolveSpeed("DissolveSpeed", Range(0, 5)) = 0

		// ������ ���̴��� ������ ���� �̹����� ���� ��
		_DissolveRampSize("DissolveRampSize", Float) = 0.2

		// RGB ������ ������ �� ����� �� �ֵ��� �߰�
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
