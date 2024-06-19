// Upgrade NOTE: upgraded instancing buffer 'OrbWeaverCharging' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "OrbWeaverCharging"
{
	Properties
	{
		_Metallic("Metallic", Float) = 0.5
		_Smoothness("Smoothness", Float) = 0.6
		_VertexScale("VertexScale", Range( 0 , 1)) = 0.2
		_Scale("Scale", Range( 0.1 , 5)) = 2
		_Speed("Speed", Range( 1 , 10)) = 1.2
		_EnergyColor("EnergyColor", Color) = (1,1,0,0)
		_BodyColor("BodyColor", Color) = (0,0.5890684,1,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
		};

		uniform float _Speed;
		uniform float _Scale;
		uniform float _VertexScale;
		uniform float4 _BodyColor;
		uniform float _Metallic;
		uniform float _Smoothness;

		UNITY_INSTANCING_BUFFER_START(OrbWeaverCharging)
			UNITY_DEFINE_INSTANCED_PROP(float4, _EnergyColor)
#define _EnergyColor_arr OrbWeaverCharging
		UNITY_INSTANCING_BUFFER_END(OrbWeaverCharging)


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
			float mulTime3 = _Time.y * _Speed;
			float simplePerlin3D2 = snoise( ( ase_vertex3Pos + mulTime3 )*_Scale );
			simplePerlin3D2 = simplePerlin3D2*0.5 + 0.5;
			float temp_output_8_0 = saturate( simplePerlin3D2 );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( temp_output_8_0 * ( ase_vertexNormal * float3(1,1,1) ) * _VertexScale );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _EnergyColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_EnergyColor_arr, _EnergyColor);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float mulTime3 = _Time.y * _Speed;
			float simplePerlin3D2 = snoise( ( ase_vertex3Pos + mulTime3 )*_Scale );
			simplePerlin3D2 = simplePerlin3D2*0.5 + 0.5;
			float temp_output_8_0 = saturate( simplePerlin3D2 );
			float4 lerpResult12 = lerp( _EnergyColor_Instance , _BodyColor , temp_output_8_0);
			o.Albedo = lerpResult12.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
358;595;1731;735;614.857;12.38937;1.199466;True;False
Node;AmplifyShaderEditor.RangedFloatNode;4;-1328.843,254.0043;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;1.2;1.2;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;9;-970.66,-62.04797;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;3;-965.9424,262.5195;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-713.66,-0.04797363;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1004.546,419.3452;Inherit;False;Property;_Scale;Scale;4;0;Create;True;0;0;0;False;0;False;2;2;0.1;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-469.7635,185.7866;Inherit;True;Simplex3D;True;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;15;65.56403,290.8574;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;17;31.71338,473.9336;Inherit;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;0;False;0;False;1,1,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;1;-465.7057,-238.0301;Inherit;False;InstancedProperty;_EnergyColor;EnergyColor;6;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;408.7134,294.9336;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;11;-490.9108,-53.04514;Inherit;False;Property;_BodyColor;BodyColor;7;0;Create;True;0;0;0;False;0;False;0,0.5890684,1,0;0,0.5890684,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;244.7134,511.9336;Inherit;False;Property;_VertexScale;VertexScale;3;0;Create;True;0;0;0;False;0;False;0.2;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;8;-97.99527,71.03828;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;144.3095,-118.2464;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;13;717.7134,-46.06641;Inherit;False;Property;_Metallic;Metallic;1;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;664.6917,205.9442;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;14;690.7134,48.93359;Inherit;False;Property;_Smoothness;Smoothness;2;0;Create;True;0;0;0;False;0;False;0.6;0.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1008.05,-109.0902;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;OrbWeaverCharging;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;AlphaTest;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;4;0
WireConnection;10;0;9;0
WireConnection;10;1;3;0
WireConnection;2;0;10;0
WireConnection;2;1;6;0
WireConnection;16;0;15;0
WireConnection;16;1;17;0
WireConnection;8;0;2;0
WireConnection;12;0;1;0
WireConnection;12;1;11;0
WireConnection;12;2;8;0
WireConnection;19;0;8;0
WireConnection;19;1;16;0
WireConnection;19;2;18;0
WireConnection;0;0;12;0
WireConnection;0;3;13;0
WireConnection;0;4;14;0
WireConnection;0;11;19;0
ASEEND*/
//CHKSM=E54938C134ED2A3E76623076FE49A7ABE7E0C357