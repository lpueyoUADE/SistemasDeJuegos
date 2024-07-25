// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ShaderRTShipRender"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		[NoScaleOffset]_FXTexture("FX Texture", 2D) = "white" {}
		[NoScaleOffset][Normal]_DistortionNormal("Distortion Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		Stencil
		{
			Ref [_Stencil]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
			CompFront [_StencilComp]
			PassFront [_StencilOp]
			FailFront Keep
			ZFailFront Keep
			CompBack Always
			PassBack Keep
			FailBack Keep
			ZFailBack Keep
		}


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		
		Pass
		{
			Name "Default"
		CGPROGRAM
			
			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			#include "UnityStandardUtils.cginc"
			#include "UnityShaderVariables.cginc"

			
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
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform sampler2D _FXTexture;
			uniform sampler2D _DistortionNormal;
			uniform float4 _FXTexture_ST;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				OUT.worldPosition = IN.vertex;
				
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode2 = tex2D( _MainTex, uv_MainTex );
				float2 uv_FXTexture = IN.texcoord.xy * _FXTexture_ST.xy + _FXTexture_ST.zw;
				float2 MainUvs222_g3 = uv_FXTexture;
				float4 tex2DNode65_g3 = tex2D( _DistortionNormal, MainUvs222_g3 );
				float4 appendResult82_g3 = (float4(0.0 , tex2DNode65_g3.g , 0.0 , tex2DNode65_g3.r));
				float2 temp_output_84_0_g3 = (UnpackScaleNormal( appendResult82_g3, 1.0 )).xy;
				float2 temp_output_71_0_g3 = ( temp_output_84_0_g3 + MainUvs222_g3 );
				float4 tex2DNode96_g3 = tex2D( _FXTexture, temp_output_71_0_g3 );
				float2 temp_cast_0 = (70.0).xx;
				float2 break16_g1 = ( IN.texcoord.xy * temp_cast_0 );
				float temp_output_64_0 = sin( _Time.y );
				float2 appendResult7_g1 = (float2(( break16_g1.x + ( temp_output_64_0 * step( 1.0 , ( break16_g1.y % 2.0 ) ) ) ) , ( break16_g1.y + ( temp_output_64_0 * step( 1.0 , ( break16_g1.x % 2.0 ) ) ) )));
				float temp_output_2_0_g1 = 0.9;
				float2 appendResult11_g2 = (float2(temp_output_2_0_g1 , temp_output_2_0_g1));
				float temp_output_17_0_g2 = length( ( (frac( appendResult7_g1 )*2.0 + -1.0) / appendResult11_g2 ) );
				float4 temp_output_44_0 = ( tex2DNode2 * 8.0 * saturate( ( ( 1.0 - temp_output_17_0_g2 ) / fwidth( temp_output_17_0_g2 ) ) ) );
				float4 TechnoDotPattern51 = temp_output_44_0;
				float4 temp_output_192_0_g3 = tex2DNode2;
				float4 color38 = IsGammaSpace() ? float4(0.1764706,0.6392157,0.5843138,1) : float4(0.02624122,0.3662527,0.3005439,1);
				float4 DotsA75 = ( temp_output_44_0 * color38 );
				float4 color73 = IsGammaSpace() ? float4(1,0.6392157,0.5843138,1) : float4(1,0.3662527,0.3005439,1);
				float4 DotsB76 = ( temp_output_44_0 * color73 );
				float2 texCoord15 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_cast_1 = (0.5).xx;
				float temp_output_35_0 = saturate( cos( ( ( _Time.y + distance( texCoord15 , temp_cast_1 ) ) * 2.0 ) ) );
				float4 lerpResult71 = lerp( DotsA75 , DotsB76 , temp_output_35_0);
				float4 lerpResult82 = lerp( float4( 0,0,0,0 ) , lerpResult71 , temp_output_35_0);
				float4 SphereEffect39 = lerpResult82;
				float4 lerpResult28 = lerp( tex2DNode2 , ( ( tex2DNode96_g3 * TechnoDotPattern51 ) + temp_output_192_0_g3 ) , SphereEffect39);
				
				half4 color = lerpResult28;
				
				#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
32;188;1940;1158;1304.212;-515.8582;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;79;-509.582,-126.3316;Inherit;False;1915.16;705.2499;Dots effect;15;51;45;76;75;72;74;38;44;73;50;64;62;58;56;81;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-459.582,311.6854;Inherit;False;Constant;_DotsSpeed;DotsSpeed;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;80;-509.3096,-508.492;Inherit;False;1915.496;382.7309;Result;9;2;0;28;4;43;40;6;52;1;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;41;-507.7021,581.4673;Inherit;False;2168.854;568.265;Sphere "opacity";16;39;82;71;35;77;78;33;26;27;25;10;14;15;16;11;83;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;58;-299.582,316.6854;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-402.0112,-458.492;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-419.7021,654.4673;Inherit;False;Constant;_TechnoSpeed;TechnoSpeed;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-120.301,200.1001;Inherit;False;Constant;_Tiling;Tiling;2;0;Create;True;0;0;0;False;0;False;70;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;64;-106.4187,316.7134;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-204.1208,-448.9914;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-396.7021,907.4673;Inherit;False;Constant;_Dist;Dist;2;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-457.7021,760.4673;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;45;141.709,89.90204;Inherit;False;Constant;_Intensity;Intensity;2;0;Create;True;0;0;0;False;0;False;8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;14;-144.7021,832.4673;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;81;159.9804,-41.1911;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-126.9021,660.4673;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;50;115.0521,219.011;Inherit;False;Dots Pattern;-1;;1;7d8d5e315fd9002418fb41741d3a59cb;1,22,0;5;21;FLOAT2;0,0;False;3;FLOAT2;8,8;False;2;FLOAT;0.9;False;4;FLOAT;0.5;False;5;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;145.7695,865.7323;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;99.76953,979.7323;Inherit;False;Constant;_Freq;Freq;2;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;38;487.3742,168.7416;Inherit;False;Constant;_TechnoColorA;TechnoColorA;2;0;Create;True;0;0;0;False;0;False;0.1764706,0.6392157,0.5843138,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;73;490.9426,348.9183;Inherit;False;Constant;_TechnoColorB;TechnoColorB;2;0;Create;True;0;0;0;False;0;False;1,0.6392157,0.5843138,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;519.3718,-8.098068;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;345.7696,934.7323;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;797.9427,319.9183;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;799.9427,96.91852;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;75;1054.577,121.0351;Inherit;False;DotsA;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;76;1077.385,324.0054;Inherit;False;DotsB;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CosOpNode;33;493.665,951.7002;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;78;613.3448,754.4496;Inherit;False;76;DotsB;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;77;611.3448,668.4496;Inherit;False;75;DotsA;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;35;609.7272,951.348;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;83;1051.162,984.059;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;71;838.3657,715.5887;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;82;1145.162,797.059;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;51;800.6008,-76.33165;Inherit;False;TechnoDotPattern;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;1443.552,860.3592;Inherit;False;SphereEffect;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;52;189.8601,-298.183;Inherit;False;51;TechnoDotPattern;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;6;453.5397,-367.7611;Inherit;False;UI-Sprite Effect Layer;1;;3;789bf62641c5cfe4ab7126850acc22b8;18,74,0,204,0,191,1,225,1,242,0,237,0,249,0,186,0,177,0,182,0,229,0,92,0,98,0,234,0,126,0,129,1,130,0,31,1;18;192;COLOR;1,1,1,1;False;39;COLOR;1,1,1,1;False;37;SAMPLER2D;;False;218;FLOAT2;0,0;False;239;FLOAT2;0,0;False;181;FLOAT2;0,0;False;75;SAMPLER2D;;False;80;FLOAT;1;False;183;FLOAT2;0,0;False;188;SAMPLER2D;;False;33;SAMPLER2D;;False;248;FLOAT2;0,0;False;233;SAMPLER2D;;False;101;SAMPLER2D;;False;57;FLOAT4;0,0,0,0;False;40;FLOAT;0;False;231;FLOAT;1;False;30;FLOAT;1;False;2;COLOR;0;FLOAT2;172
Node;AmplifyShaderEditor.WireNode;43;709.2914,-424.5467;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;40;770.5613,-299.6224;Inherit;False;39;SphereEffect;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;28;987.8948,-435.2005;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;4;-459.3096,-375.5281;Inherit;True;Property;_RenderTexture;RenderTexture;0;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1228.186,-435.1699;Float;False;True;-1;2;ASEMaterialInspector;0;4;ShaderRTShipRender;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;False;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;True;0;True;-9;False;False;False;False;False;False;False;True;True;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;58;0;56;0
WireConnection;64;0;58;0
WireConnection;2;0;1;0
WireConnection;14;0;15;0
WireConnection;14;1;16;0
WireConnection;81;0;2;0
WireConnection;10;0;11;0
WireConnection;50;3;62;0
WireConnection;50;4;64;0
WireConnection;50;5;64;0
WireConnection;25;0;10;0
WireConnection;25;1;14;0
WireConnection;44;0;81;0
WireConnection;44;1;45;0
WireConnection;44;2;50;0
WireConnection;26;0;25;0
WireConnection;26;1;27;0
WireConnection;74;0;44;0
WireConnection;74;1;73;0
WireConnection;72;0;44;0
WireConnection;72;1;38;0
WireConnection;75;0;72;0
WireConnection;76;0;74;0
WireConnection;33;0;26;0
WireConnection;35;0;33;0
WireConnection;83;0;35;0
WireConnection;71;0;77;0
WireConnection;71;1;78;0
WireConnection;71;2;35;0
WireConnection;82;1;71;0
WireConnection;82;2;83;0
WireConnection;51;0;44;0
WireConnection;39;0;82;0
WireConnection;6;192;2;0
WireConnection;6;39;52;0
WireConnection;43;0;2;0
WireConnection;28;0;43;0
WireConnection;28;1;6;0
WireConnection;28;2;40;0
WireConnection;0;0;28;0
ASEEND*/
//CHKSM=E2886B9821303C3ABD8FF595D00735ABA1F4DD97