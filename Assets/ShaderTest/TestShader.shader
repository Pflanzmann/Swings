Shader "Hidden/TestShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Texture", 2D) = "white" {}
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		_Color3("Color 3", Color) = (1,1,1,1)
		_Dissolve("_Dissolve", Float) = 0
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
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};


				float _Dissolve;
				sampler2D _MainTex;
				sampler2D _NoiseTex;
				fixed4 _Color1;
				fixed4 _Color2;
				fixed4 _Color3;



				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}


				fixed4 frag(v2f IN) : SV_Target
				{				
				fixed4 col = tex2D(_MainTex, IN.uv);

				if(_Color1.a == 0 && _Color2.a == 0){
					
					col.rgb *= col.a;
					return col;
				}


				fixed4 temp;
				//Weißer Punkt und Farbe
				 temp.rgb = lerp(_Color3.rgb, lerp(_Color1.rgb, _Color2.rgb, IN.uv.x), distance((0.5, 0.5), IN.uv) / 0.5);


				 //col.a = lerp(_Color2, _Color1, _Dissolve);

				col.rgb = temp.rgb;
				col.rgb *= col.a;

				return col;
			}
			ENDCG
		}
		}
}
