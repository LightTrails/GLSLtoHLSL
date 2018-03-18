Shader "Custom/1"
{
		Properties
	{
		_MainTex("Texture", 2D) = "blue" {}
		_InputTime("Time", Range(0, 120)) = 0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		Tags{ "Queue" = "Transparent" }
		LOD 100

	Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
		float4 vertex : SV_POSITION;
	};

	float _InputTime;
	sampler2D _MainTex;
	float4 _MainTex_ST;

	

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}

		fixed4 frag(v2f input) : SV_Target	
	{
	//Tweakable parameters
	float waveStrength = 0.02;
	float frequency = 30.0;
	float waveSpeed = 5.0;
	float4 sunlightColor = float4(1.0, 0.91, 0.75, 1.0);
	float sunlightStrength = 5.0;
	//

	float2 tapPoint = float2(0 / _ScreenParams.x, 0 / _ScreenParams.y);
	float2 uv = (_ScreenParams.xy * input.uv) / _ScreenParams.xy;
	float fmodifiedTime = _InputTime * waveSpeed;
	float aspectRatio = _ScreenParams.x / _ScreenParams.y;
	float2 distVec = uv - tapPoint;
	distVec.x *= aspectRatio;
	float distance = length(distVec);
	float2 newTexCoord = uv;

	float multiplier = (distance < 1.0) ? ((distance - 1.0)*(distance - 1.0)) : 0.0;
	float addend = (sin(frequency*distance - fmodifiedTime) + 1.0) * waveStrength * multiplier;
	newTexCoord += addend;

	float4 colorToAdd = sunlightColor * sunlightStrength * addend;

	return tex2D(_MainTex, newTexCoord) + colorToAdd;
}			
	
		ENDCG
	}
	}
}