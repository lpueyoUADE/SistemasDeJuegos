// Upgrade NOTE: upgraded instancing buffer 'CustomOrbWeaverHit' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/OrbWeaverHit"
{
	Properties
	{
		_EnergyColor1("EnergyColor", Color) = (0,1,0.1258547,0)
		_BodyColor1("BodyColor", Color) = (1,0,0.1181884,0)
		_Scale1("Scale", Range( 0.1 , 25)) = 2
		_Speed1("Speed", Range( 0.1 , 10)) = 1.2
		_VertexScale1("VertexScale", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
		};

		uniform float _Speed1;
		uniform float _Scale1;
		uniform float _VertexScale1;
		uniform float4 _BodyColor1;

		UNITY_INSTANCING_BUFFER_START(CustomOrbWeaverHit)
			UNITY_DEFINE_INSTANCED_PROP(float4, _EnergyColor1)
#define _EnergyColor1_arr CustomOrbWeaverHit
		UNITY_INSTANCING_BUFFER_END(CustomOrbWeaverHit)


		float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }

		float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }

		float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }

		float snoise( float3 v )
		{
			const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
			float3 i = floor( v + dot( v, C.yyy ) );
			float3 x0 = v - i + dot( i, C.xxx );
			float3 g = step( x0.yzx, x0.xyz );
			float3 l = 1.0 - g;
			float3 i1 = min( g.xyz, l.zxy );
			float3 i2 = max( g.xyz, l.zxy );
			float3 x1 = x0 - i1 + C.xxx;
			float3 x2 = x0 - i2 + C.yyy;
			float3 x3 = x0 - 0.5;
			i = mod3D289( i);
			float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
			float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
			float4 x_ = floor( j / 7.0 );
			float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
			float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
			float4 h = 1.0 - abs( x ) - abs( y );
			float4 b0 = float4( x.xy, y.xy );
			float4 b1 = float4( x.zw, y.zw );
			float4 s0 = floor( b0 ) * 2.0 + 1.0;
			float4 s1 = floor( b1 ) * 2.0 + 1.0;
			float4 sh = -step( h, 0.0 );
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
			float3 g0 = float3( a0.xy, h.x );
			float3 g1 = float3( a0.zw, h.y );
			float3 g2 = float3( a1.xy, h.z );
			float3 g3 = float3( a1.zw, h.w );
			float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
			g0 *= norm.x;
			g1 *= norm.y;
			g2 *= norm.z;
			g3 *= norm.w;
			float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
			m = m* m;
			m = m* m;
			float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
			return 42.0 * dot( m, px);
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float mulTime7 = _Time.y * _Speed1;
			float simplePerlin3D5 = snoise( ( ase_vertex3Pos + mulTime7 )*_Scale1 );
			simplePerlin3D5 = simplePerlin3D5*0.5 + 0.5;
			v.vertex.xyz += ( simplePerlin3D5 * ( ase_vertex3Pos * float3(1.5,1.5,1.5) ) * _VertexScale1 );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _EnergyColor1_Instance = UNITY_ACCESS_INSTANCED_PROP(_EnergyColor1_arr, _EnergyColor1);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float mulTime7 = _Time.y * _Speed1;
			float simplePerlin3D5 = snoise( ( ase_vertex3Pos + mulTime7 )*_Scale1 );
			simplePerlin3D5 = simplePerlin3D5*0.5 + 0.5;
			float4 lerpResult4 = lerp( _EnergyColor1_Instance , _BodyColor1 , simplePerlin3D5);
			o.Albedo = lerpResult4.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
2557;6;1920;1053;1252.862;222.0368;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;9;-1577.763,515.0143;Inherit;False;Property;_Speed1;Speed;3;0;Create;True;0;0;0;False;0;False;1.2;1.2;0.1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;7;-1282.309,521.1788;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;10;-1297.996,364.8439;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;16;-549.6897,818.0776;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;12;-624.8083,1007.031;Inherit;False;Constant;_Vector1;Vector 0;0;0;Create;True;0;0;0;False;0;False;1.5,1.5,1.5;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;8;-912.7924,497.2199;Inherit;False;Property;_Scale1;Scale;2;0;Create;True;0;0;0;False;0;False;2;2;0.1;25;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-1039.879,368.3962;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;2;-421.2001,-100.7201;Inherit;False;InstancedProperty;_EnergyColor1;EnergyColor;0;0;Create;True;0;0;0;False;0;False;0,1,0.1258547,0;1,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-381.8083,1070.031;Inherit;False;Property;_VertexScale1;VertexScale;4;0;Create;True;0;0;0;False;0;False;1;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-217.8083,853.0309;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;5;-533.1296,359.4485;Inherit;True;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-445.4177,130.3949;Inherit;False;Property;_BodyColor1;BodyColor;1;0;Create;True;0;0;0;False;0;False;1,0,0.1181884,0;0,0.5890684,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;4;-33.1177,276.152;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;44.57146,754.5087;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;407.1573,202.3822;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/OrbWeaverHit;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;9;0
WireConnection;6;0;10;0
WireConnection;6;1;7;0
WireConnection;14;0;16;0
WireConnection;14;1;12;0
WireConnection;5;0;6;0
WireConnection;5;1;8;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;4;2;5;0
WireConnection;15;0;5;0
WireConnection;15;1;14;0
WireConnection;15;2;13;0
WireConnection;0;0;4;0
WireConnection;0;11;15;0
ASEEND*/
//CHKSM=8362C6331D96A6A870DEEA4DF9B4C51B6FFAD100