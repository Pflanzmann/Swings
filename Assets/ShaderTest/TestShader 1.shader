Shader "Hidden/TestShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Texture", 2D) = "white" {}
		_Color1("Color 1", Color) = (0,0,1,1)
		_Color2("Color 2", Color) = (0,0,1,1)

			[Space]
			[Space]
			[Header(White Punkt)]
		_ColorPoint("Punkt Farbe", Color) = (1,1,1,1)
		_Value1("Punkt Position X", Range(0,1)) = 0.5
		_Value2("Punkt Position Y", Range(0,1)) = 0.75
			[Header(Misch relation von 0 bis 1 Header(des Punktes und dem Hintergrund)]
		_Value3("Punkt Distanz (min 0 - max 1) +", Range(-10,10)) = 0
		_Value4("Punkt Distanz (min 0 - max 1) *", Range(-10,10)) = 1
		_Value5("Punkt Farbe (ohne min - max) +", Range(-10,10)) = 0
		_Value6("Punkt Farbe (ohne min - max) *", Range(-10,10)) = 1

			[Space]
			[Space]
			[Header(Rand Effekt)]
		_ColorBorder("Rand Farbe", Color) = (0,0,0,0)
		_Value7("Rand Mittelpunkt Position X", Range(0,1)) = 0.5
		_Value8("Rand Mittelpunkt Position Y", Range(0,1)) = 0.5
			[Header(Misch relation von 0 bis 1 Header(des Randes und dem Hintergrund)]
		_Value9("Rand Distanz (min 0 - max 1) +", Range(-10,10)) = 0
		_Value10("Rand Distanz (min 0 - max 1) *", Range(-10,10)) = 1
		_Value11("Rand Farbe (ohne min - max) +", Range(-10,10)) = 0
		_Value12("Rand Farbe (ohne min - max) *", Range(-10,10)) = 1
				}
				SubShader
		{

			Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }

			// No culling or depth
			Cull Off ZWrite Off ZTest Always
			Blend One OneMinusSrcAlpha
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD;
				};

				struct v2f
				{
					float2 uv : TEXCOORD;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				fixed4 _Color1;
				fixed4 _Color2;
				fixed4 _ColorPoint;
				fixed4 _ColorBorder;
				float _Value1;
				float _Value2;
				float _Value3;
				float _Value4;
				float _Value5;
				float _Value6;
				float _Value7;
				float _Value8;
				float _Value9;
				float _Value10;
				float _Value11;
				float _Value12;


				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv.xy;
					return o;
				}


				fixed4 frag(v2f IN) : SV_Target
				{

				fixed4 col = tex2D(_MainTex, IN.uv);

				if (_Color1.a == 0 && _Color2.a == 0) {

					col.rgb *= col.a;
					return col;
				}
				fixed4 temp;

				float4 a = (0.2, _Value1, 0.2, _Value2);
				float pointDistance = distance((_Value1, _Value2, 0), IN.uv);
				float pointDistance2 = distance((_Value7, 0, _Value8), IN.uv);

				//Weißer Punkt und Farbe
				col.rgb = lerp(_ColorPoint.rgb, lerp(_Color1.rgb, _Color2.rgb, IN.uv.x), pointDistance2 / 0.5);

				//col.rgb = lerp(_Color1.rgb, _Color2.rgb, IN.uv.x);
				//col.rgb = lerp(_ColorPoint, col.rgb, (clamp((pointDistance + _Value3) * _Value4, 0, 1) + _Value5) * _Value6);

				//////Fresnel Effect
				//col.rgb = lerp(col.rgb, _ColorBorder, (clamp((pointDistance2 + _Value9) * _Value10, 0, 1) + _Value11) * _Value12);


				if (IN.uv.x < _Value1)
				{
					col.rgb = _ColorBorder;
				}

				if (IN.uv.y < _Value2)
				{
					col.rgb = _Color2;
				}

				//col.rgb = temp.rgb;
				col.rgb *= col.a;
				return pow(col, 2.2);
			}
			ENDCG
		}
		}
}
