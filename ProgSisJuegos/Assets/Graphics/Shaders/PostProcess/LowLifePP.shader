// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LowLifePP"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_Color("Color", Color) = (1,0,0,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_PulseMaxIntensity("PulseMaxIntensity", Range( 0 , 1)) = 0.5
		_PulseSpeed("PulseSpeed", Range( 1 , 20)) = 10
		_BrokenOverlayIntensity("BrokenOverlayIntensity", Range( 0 , 1)) = 0.5
		_PulseIntensity("PulseIntensity", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform sampler2D _TextureSample1;
			uniform float4 _TextureSample1_ST;
			uniform float4 _Color;
			uniform float _PulseSpeed;
			uniform float _PulseIntensity;
			uniform float _PulseMaxIntensity;
			uniform float _BrokenOverlayIntensity;


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_MainTex = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 Screen27 = tex2D( _MainTex, uv_MainTex );
				float2 uv_TextureSample1 = i.uv.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
				float4 BrokenGlassTexture47 = ( 1.0 - tex2D( _TextureSample1, uv_TextureSample1 ) );
				float mulTime9 = _Time.y * _PulseSpeed;
				float clampResult111 = clamp( _PulseIntensity , 0.0 , _PulseMaxIntensity );
				float4 lerpResult59 = lerp( Screen27 , _Color , ( saturate( sin( mulTime9 ) ) * clampResult111 ));
				float4 Overlay92 = lerpResult59;
				float4 lerpResult115 = lerp( Screen27 , ( Screen27 * ( BrokenGlassTexture47 + Overlay92 ) ) , _BrokenOverlayIntensity);
				

				finalColor = lerpResult115;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
448;73;1471;995;1377.173;-110.6638;1.063142;False;False
Node;AmplifyShaderEditor.CommentaryNode;91;-1406.474,768.2723;Inherit;False;1661.618;767.7968;Pulsating overlay;12;92;112;59;61;106;3;111;19;108;7;9;109;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-1313.357,1171.791;Inherit;False;Property;_PulseSpeed;PulseSpeed;3;0;Create;True;0;0;0;False;0;False;10;1;1;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-1221.834,-74.17757;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;9;-1023.441,1178.363;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;7;-864.1132,1177.688;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;108;-1098.357,1393.791;Inherit;False;Property;_PulseMaxIntensity;PulseMaxIntensity;2;0;Create;True;0;0;0;False;0;False;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-1084.922,1284.834;Inherit;False;Property;_PulseIntensity;PulseIntensity;5;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1093.876,-75.79476;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;19;-725.4022,1178.261;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;111;-730.3457,1282.767;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-779.4626,-72.98762;Inherit;False;Screen;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;106;-552.1169,861.9581;Inherit;False;27;Screen;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;3;-603.1938,960.0315;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-529.764,1174.997;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;46;-580.1827,-79.4341;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;0;False;0;False;-1;55487161b295a5549b1c18b3c22adc45;55487161b295a5549b1c18b3c22adc45;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;59;-252.753,947.4274;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;85;-297.7665,-75.02258;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;63;-1408.748,203.4864;Inherit;False;1660.353;566.9591;Broken overlay + end result;8;0;115;110;117;120;114;116;107;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;92;10.61374,938.9172;Inherit;False;Overlay;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;47;-143.4899,-77.10793;Inherit;False;BrokenGlassTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;107;-1175.036,591.6077;Inherit;False;92;Overlay;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;116;-1237.935,488.1197;Inherit;False;47;BrokenGlassTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;120;-963.5858,526.2974;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;114;-1172.634,382.5457;Inherit;False;27;Screen;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;-676.2734,483.3662;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;110;-787.4563,611.6259;Inherit;False;Property;_BrokenOverlayIntensity;BrokenOverlayIntensity;4;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;115;-426.1602,401.6053;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-248.5102,400.5095;Float;False;True;-1;2;ASEMaterialInspector;0;2;LowLifePP;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;9;0;109;0
WireConnection;7;0;9;0
WireConnection;2;0;1;0
WireConnection;19;0;7;0
WireConnection;111;0;112;0
WireConnection;111;2;108;0
WireConnection;27;0;2;0
WireConnection;61;0;19;0
WireConnection;61;1;111;0
WireConnection;59;0;106;0
WireConnection;59;1;3;0
WireConnection;59;2;61;0
WireConnection;85;0;46;0
WireConnection;92;0;59;0
WireConnection;47;0;85;0
WireConnection;120;0;116;0
WireConnection;120;1;107;0
WireConnection;117;0;114;0
WireConnection;117;1;120;0
WireConnection;115;0;114;0
WireConnection;115;1;117;0
WireConnection;115;2;110;0
WireConnection;0;0;115;0
ASEEND*/
//CHKSM=F24E9C04BDAECB711D7C989496504B2F2635EC30