// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SpacePP"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_SpaceColored("SpaceColored", 2D) = "white" {}
		_SpaceOnlyStars("SpaceOnlyStars", 2D) = "white" {}
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
				float4 ase_texcoord4 : TEXCOORD4;
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform sampler2D _SpaceColored;
			uniform float4 _SpaceColored_ST;
			uniform sampler2D _SpaceOnlyStars;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord4 = screenPos;
				
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
				float4 Screen31 = tex2D( _MainTex, uv_MainTex );
				float2 uv_SpaceColored = i.uv.xy * _SpaceColored_ST.xy + _SpaceColored_ST.zw;
				float4 SpaceBackground30 = tex2D( _SpaceColored, uv_SpaceColored );
				float2 texCoord49 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float mulTime46 = _Time.y * ( 1.0 * 0.001 );
				float4 SpaceStars44 = tex2D( _SpaceOnlyStars, ( texCoord49 + ( 1 + mulTime46 ) ) );
				float4 screenPos = i.ase_texcoord4;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float clampDepth77 = Linear01Depth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
				float4 lerpResult78 = lerp( Screen31 , ( ( SpaceBackground30 + ( SpaceStars44 * (0.65 + (saturate( sin( _Time.y ) ) - 0.0) * (1.0 - 0.65) / (1.0 - 0.0)) ) ) * 0.25 ) , saturate( ( clampDepth77 / 2.0 ) ));
				

				finalColor = lerpResult78;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
2720;17;1626;1093;824.0294;261.0926;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;45;-705,-100.4147;Inherit;False;1759.449;939.1739;Set;6;29;2;31;1;30;59;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;59;-658.338,235.9822;Inherit;False;1618.117;543.5445;Background stars with a little movement;10;49;43;44;47;58;57;46;50;56;55;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-595.0796,585.6124;Inherit;False;Constant;_Speed;Speed;2;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-580.2086,674.7075;Inherit;False;Constant;_SpeedMultiplier;SpeedMultiplier;2;0;Create;True;0;0;0;False;0;False;0.001;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-281.8808,621.4131;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;83;-1236.444,879.5286;Inherit;False;2413.22;709.9964;Result;18;71;70;72;73;54;76;36;69;80;63;65;79;35;64;81;77;78;0;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;50;-73.88058,429.3658;Inherit;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;0;False;0;False;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;46;-103.1417,638.1615;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;49;-13.36388,298.4631;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;56;99.59622,511.1231;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;71;-1186.444,1323.101;Inherit;False;Constant;_StarsGlowSpeed;StarsGlowSpeed;2;0;Create;True;0;0;0;False;0;False;1;0;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;247.2486,424.1498;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;70;-896.444,1325.101;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;43;421.6604,377.2881;Inherit;True;Property;_SpaceOnlyStars;SpaceOnlyStars;1;0;Create;True;0;0;0;False;0;False;-1;34d77054fea59914c918139d27964c85;34d77054fea59914c918139d27964c85;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;72;-718.444,1325.101;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;73;-543.4442,1327.101;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-231.6936,36.5853;Inherit;True;Property;_SpaceColored;SpaceColored;0;0;Create;True;0;0;0;False;0;False;-1;4986f0179406f5f4cb177c091ecbc2b1;4986f0179406f5f4cb177c091ecbc2b1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;44;721.4794,371.7956;Inherit;False;SpaceStars;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;76;-329.101,1371.898;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.65;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-655,-41.5;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;77.3064,35.5853;Inherit;False;SpaceBackground;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;54;-308.8843,1142.615;Inherit;False;44;SpaceStars;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-91.10096,1302.898;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;36;-149.4495,1037.528;Inherit;False;30;SpaceBackground;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;80;136.1419,1473.525;Inherit;False;Constant;_DepthIntensity;DepthIntensity;2;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-528.3,-37.9;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;77;133.2098,1380.594;Inherit;False;1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;31;-214.6936,-50.4147;Inherit;False;Screen;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;63;109.8991,1055.898;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;65;94.89913,1221.898;Inherit;False;Constant;_Intensity;Intensity;2;0;Create;True;0;0;0;False;0;False;0.25;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;79;351.1419,1401.525;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;262.8991,1121.898;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;81;476.1419,1412.525;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;461.5504,1002.529;Inherit;False;31;Screen;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;78;726.9156,1091.486;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;887.7764,1095.784;Float;False;True;-1;2;ASEMaterialInspector;0;2;SpacePP;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;57;0;47;0
WireConnection;57;1;58;0
WireConnection;46;0;57;0
WireConnection;56;0;50;1
WireConnection;56;1;46;0
WireConnection;55;0;49;0
WireConnection;55;1;56;0
WireConnection;70;0;71;0
WireConnection;43;1;55;0
WireConnection;72;0;70;0
WireConnection;73;0;72;0
WireConnection;44;0;43;0
WireConnection;76;0;73;0
WireConnection;30;0;29;0
WireConnection;69;0;54;0
WireConnection;69;1;76;0
WireConnection;2;0;1;0
WireConnection;31;0;2;0
WireConnection;63;0;36;0
WireConnection;63;1;69;0
WireConnection;79;0;77;0
WireConnection;79;1;80;0
WireConnection;64;0;63;0
WireConnection;64;1;65;0
WireConnection;81;0;79;0
WireConnection;78;0;35;0
WireConnection;78;1;64;0
WireConnection;78;2;81;0
WireConnection;0;0;78;0
ASEEND*/
//CHKSM=9F78DA43D6602BA5ED7C5DD10519C8EAF5CA0979