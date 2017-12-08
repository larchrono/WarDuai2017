﻿Shader "Custom/Mask"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Mask ("Mask Texture", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
        }
        Lighting On
 		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
        	setTexture [_Mask] {combine texture}
        	SetTexture [_MainTex] {combine texture, previous}
        }
    }
}