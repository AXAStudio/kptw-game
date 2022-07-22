Shader "Custom/Circle" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		[Toggle(_DEBUG)] debugOn("Debug", Float) = 0
	}

	SubShader{

		Tags{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		ColorMask RGB
		Cull Off
		ZWrite Off
		ZTest Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma multi_compile_fwdbase // useful to have shadows 
			#pragma shader_feature ____ _DEBUG 

			#pragma target 3.0

			struct v2f {
				float4 pos : 		SV_POSITION;
				float3 worldPos : 	TEXCOORD0;
				float3 normal : 	TEXCOORD1;
				float2 texcoord : 	TEXCOORD2;
				float3 viewDir: 	TEXCOORD3;
				float4 screenPos : 	TEXCOORD4;
				float4 color: 		COLOR;
			};


			uniform float4 _MainTex_ST;
			sampler2D _MainTex;

			v2f vert(appdata_full v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);

				o.normal.xyz = UnityObjectToWorldNormal(v.normal);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.viewDir.xyz = WorldSpaceViewDir(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.screenPos = ComputeScreenPos(o.pos);
				o.color = v.color;
				return o;
			}

			float4 frag(v2f o) : COLOR{

				// Usually you'll see 'v2f i' for Input, but this saves time when I copy stuff between 
				// vert and frag functions.

				float2 off = o.texcoord.xy - 0.5;
				off *= off;

				o.viewDir.xyz = normalize(o.viewDir.xyz);
				float2 duv = o.screenPos.xy / o.screenPos.w;

				float4 col = o.color * tex2D(_MainTex, o.texcoord.xy);

				col.a *= saturate(1 - (off.x + off.y) * 4);

				#if _DEBUG 
				col.a = 1;
				#endif

				return col;
			}
			ENDCG
		}
	}
	Fallback "Legacy Shaders/Transparent/VertexLit"

	//CdustomEditor "CircleDrawerGUI"
}