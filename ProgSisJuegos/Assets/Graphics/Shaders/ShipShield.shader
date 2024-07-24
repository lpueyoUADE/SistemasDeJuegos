// Upgrade NOTE: upgraded instancing buffer 'ShipShield' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ShipShield"
{
	Properties
	{
		_ShieldColor("_ShieldColor", Color) = (0,1,0,0)
		_IntegrityIndicatorSpeed("IntegrityIndicatorSpeed", Range( 0 , 50)) = 5
		_FoamFallof("FoamFallof", Float) = 1
		_ShieldBorderStart("_ShieldBorderStart", Color) = (0,1,0,0)
		_ShieldBorderEnd("_ShieldBorderEnd", Color) = (1,0,0,0)
		_FoamOffset("FoamOffset", Range( 0 , 1)) = 0.48
		_Opacity("Opacity", Range( 0 , 1)) = 0.1
		_FoamDistance("FoamDistance", Range( 0 , 1)) = 0.5
		_FresnelScale("FresnelScale", Float) = 2
		_FresnelPower("FresnelPower", Float) = 10
		_IntegrityIndicatorWidth("IntegrityIndicatorWidth", Range( 0 , 1)) = 0.5
		_ShipIntegrityColor("_ShipIntegrityColor", Color) = (1,0,0.0379858,0)
		_ShieldLerpBorder("_ShieldLerpBorder", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
			float3 worldNormal;
			float3 viewDir;
		};

		uniform float4 _ShipIntegrityColor;
		uniform float _IntegrityIndicatorWidth;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _FoamDistance;
		uniform float _FoamFallof;
		uniform float _FoamOffset;
		uniform float4 _ShieldBorderEnd;
		uniform float4 _ShieldBorderStart;
		uniform float _ShieldLerpBorder;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform float _Opacity;

		UNITY_INSTANCING_BUFFER_START(ShipShield)
			UNITY_DEFINE_INSTANCED_PROP(float4, _ShieldColor)
#define _ShieldColor_arr ShipShield
			UNITY_DEFINE_INSTANCED_PROP(float, _IntegrityIndicatorSpeed)
#define _IntegrityIndicatorSpeed_arr ShipShield
		UNITY_INSTANCING_BUFFER_END(ShipShield)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 _ShieldColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_ShieldColor_arr, _ShieldColor);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float _IntegrityIndicatorSpeed_Instance = UNITY_ACCESS_INSTANCED_PROP(_IntegrityIndicatorSpeed_arr, _IntegrityIndicatorSpeed);
			float mulTime8 = _Time.y * _IntegrityIndicatorSpeed_Instance;
			float IntegrityLine65 = ( cos( ( ase_vertex3Pos.x + mulTime8 ) ) - _IntegrityIndicatorWidth );
			float4 lerpResult63 = lerp( _ShipIntegrityColor , _ShieldColor_Instance , IntegrityLine65);
			float4 ShipIntegrityLerp71 = lerpResult63;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth30 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth30 = abs( ( screenDepth30 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( 1.0 ) );
			float temp_output_38_0 = ( 1.0 - saturate( ( pow( ( distanceDepth30 / _FoamDistance ) , _FoamFallof ) - _FoamOffset ) ) );
			float4 lerpResult74 = lerp( _ShieldBorderEnd , _ShieldBorderStart , _ShieldLerpBorder);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV19 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode19 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV19, _FresnelPower ) );
			float fresnelNdotV25 = dot( ase_worldNormal, -i.viewDir );
			float fresnelNode25 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV25, _FresnelPower ) );
			float temp_output_41_0 = saturate( ( ( fresnelNode19 * fresnelNode25 ) + ( temp_output_38_0 + _Opacity ) ) );
			float4 lerpResult47 = lerp( ( ShipIntegrityLerp71 + temp_output_38_0 ) , lerpResult74 , temp_output_41_0);
			o.Emission = saturate( lerpResult47 ).rgb;
			o.Alpha = temp_output_41_0;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = worldViewDir;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
32;188;1940;1158;1210.26;2243.833;3.958383;True;False
Node;AmplifyShaderEditor.CommentaryNode;67;81.06042,-1151.603;Inherit;False;2349.133;522.5557;color de "línea" interna del escudo, indica la integridad de la nave;13;66;61;71;63;18;65;56;7;57;55;54;8;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;51;150.8612,306.0604;Inherit;False;1666;392;Depth fade;11;30;32;31;34;33;36;35;37;38;39;40;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;3;168.1158,-842.3439;Inherit;False;InstancedProperty;_IntegrityIndicatorSpeed;IntegrityIndicatorSpeed;1;0;Create;True;0;0;0;False;0;False;5;5;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;200.8612,484.0604;Inherit;False;Property;_FoamDistance;FoamDistance;7;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;54;445.389,-990.4368;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;8;443.9915,-837.8435;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;30;222.8612,356.0604;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;31;576.8612,393.0604;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;649.9854,-966.876;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;561.8612,490.0604;Inherit;False;Property;_FoamFallof;FoamFallof;2;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;705.5131,-846.7268;Inherit;False;Property;_IntegrityIndicatorWidth;IntegrityIndicatorWidth;10;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;7;839.9349,-970.553;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;881.8613,543.0604;Inherit;False;Property;_FoamOffset;FoamOffset;5;0;Create;True;0;0;0;False;0;False;0.48;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;33;975.8613,424.0604;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;50;379.0504,-354.2564;Inherit;False;1240;579.8798;Fresnel;7;23;26;27;24;19;25;28;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;56;1033.613,-967.628;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;35;1224.861,418.0604;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;23;429.0504,-13.37666;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;27;564.0504,-198.3767;Inherit;False;Property;_FresnelPower;FresnelPower;9;0;Create;True;0;0;0;False;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;598.0504,-298.3766;Inherit;False;Property;_FresnelScale;FresnelScale;8;0;Create;True;0;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;37;1352.861,419.0604;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;65;1209.475,-970.87;Inherit;False;IntegrityLine;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;24;643.0504,-10.37666;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;38;1478.861,419.0604;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;19;922.9777,-304.2564;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;1293.861,582.0604;Inherit;False;Property;_Opacity;Opacity;6;0;Create;True;0;0;0;False;0;False;0.1;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;1533.065,-719.4986;Inherit;False;65;IntegrityLine;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;61;1513.76,-1068.265;Inherit;False;Property;_ShipIntegrityColor;_ShipIntegrityColor;11;0;Create;True;0;0;0;False;0;False;1,0,0.0379858,0;0,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;25;918.0503,-28.37662;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;1510.945,-895.8803;Inherit;False;InstancedProperty;_ShieldColor;_ShieldColor;0;0;Create;True;0;0;0;False;0;False;0,1,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;1384.05,-76.37666;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;63;1839.899,-934.4816;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;1664.861,439.0604;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;71;2143.023,-935.3488;Inherit;False;ShipIntegrityLerp;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;52;2314.094,-342.1529;Inherit;False;1304.186;691.0664;Color de los bordes - "indican" el tiempo restante del escudo;8;74;73;48;49;47;20;72;75;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WireNode;70;1788.605,-26.13855;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;2012.995,421.3187;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;64;2167.135,-193.7165;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;72;2347.656,-284.6287;Inherit;False;71;ShipIntegrityLerp;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;41;2158.122,419.8408;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;73;2384.996,-176.5759;Inherit;False;Property;_ShieldBorderEnd;_ShieldBorderEnd;4;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;75;2367.753,227.1951;Inherit;False;Property;_ShieldLerpBorder;_ShieldLerpBorder;12;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;48;2391.721,42.78195;Inherit;False;Property;_ShieldBorderStart;_ShieldBorderStart;3;0;Create;True;0;0;0;False;0;False;0,1,0,0;0,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;74;2733.753,0.1950314;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;69;2940.422,346.2066;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;2601.463,-276.2545;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;47;3178.41,-291.0019;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;68;3557.864,624.8329;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;49;3404.281,-292.1529;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3795.078,-305.472;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;ShipShield;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;3;0
WireConnection;31;0;30;0
WireConnection;31;1;32;0
WireConnection;55;0;54;1
WireConnection;55;1;8;0
WireConnection;7;0;55;0
WireConnection;33;0;31;0
WireConnection;33;1;34;0
WireConnection;56;0;7;0
WireConnection;56;1;57;0
WireConnection;35;0;33;0
WireConnection;35;1;36;0
WireConnection;37;0;35;0
WireConnection;65;0;56;0
WireConnection;24;0;23;0
WireConnection;38;0;37;0
WireConnection;19;2;26;0
WireConnection;19;3;27;0
WireConnection;25;4;24;0
WireConnection;25;2;26;0
WireConnection;25;3;27;0
WireConnection;28;0;19;0
WireConnection;28;1;25;0
WireConnection;63;0;61;0
WireConnection;63;1;18;0
WireConnection;63;2;66;0
WireConnection;40;0;38;0
WireConnection;40;1;39;0
WireConnection;71;0;63;0
WireConnection;70;0;38;0
WireConnection;42;0;28;0
WireConnection;42;1;40;0
WireConnection;64;0;70;0
WireConnection;41;0;42;0
WireConnection;74;0;73;0
WireConnection;74;1;48;0
WireConnection;74;2;75;0
WireConnection;69;0;41;0
WireConnection;20;0;72;0
WireConnection;20;1;64;0
WireConnection;47;0;20;0
WireConnection;47;1;74;0
WireConnection;47;2;69;0
WireConnection;68;0;41;0
WireConnection;49;0;47;0
WireConnection;0;2;49;0
WireConnection;0;9;68;0
ASEEND*/
//CHKSM=7BB2A8CA922EF6686BE27D49F9575C1476093FEA