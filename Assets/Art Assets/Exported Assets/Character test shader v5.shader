// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32577,y:32735,varname:node_4013,prsc:2|diff-1705-OUT;n:type:ShaderForge.SFN_OneMinus,id:2696,x:29542,y:32816,varname:node_2696,prsc:2|IN-3060-OUT;n:type:ShaderForge.SFN_Fresnel,id:1715,x:29779,y:32217,varname:node_1715,prsc:2|EXP-9106-OUT;n:type:ShaderForge.SFN_Clamp01,id:1705,x:32380,y:32657,varname:node_1705,prsc:2|IN-7169-OUT;n:type:ShaderForge.SFN_Get,id:5704,x:29758,y:32395,varname:node_5704,prsc:2|IN-3502-OUT;n:type:ShaderForge.SFN_Multiply,id:2581,x:30010,y:32290,varname:node_2581,prsc:2|A-1715-OUT,B-5704-OUT;n:type:ShaderForge.SFN_Slider,id:9851,x:29843,y:32513,ptovrint:False,ptlb:Rim Intensity,ptin:_RimIntensity,varname:node_9851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5.5,max:10;n:type:ShaderForge.SFN_Multiply,id:7721,x:30243,y:32383,varname:node_7721,prsc:2|A-2581-OUT,B-9851-OUT;n:type:ShaderForge.SFN_Slider,id:9106,x:29451,y:32244,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_9106,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:10;n:type:ShaderForge.SFN_Clamp01,id:9034,x:29716,y:32816,varname:node_9034,prsc:2|IN-2696-OUT;n:type:ShaderForge.SFN_Set,id:8561,x:30770,y:32372,varname:rimMask,prsc:2|IN-2693-OUT;n:type:ShaderForge.SFN_Set,id:8218,x:31346,y:32921,varname:slimeNoise,prsc:2|IN-9009-OUT;n:type:ShaderForge.SFN_Get,id:7166,x:31974,y:32627,varname:node_7166,prsc:2|IN-4944-OUT;n:type:ShaderForge.SFN_Add,id:2693,x:30486,y:32245,varname:node_2693,prsc:2|A-1715-OUT,B-7721-OUT;n:type:ShaderForge.SFN_Get,id:6936,x:30392,y:33119,varname:node_6936,prsc:2|IN-8561-OUT;n:type:ShaderForge.SFN_OneMinus,id:5376,x:30617,y:33119,varname:node_5376,prsc:2|IN-6936-OUT;n:type:ShaderForge.SFN_Multiply,id:146,x:30897,y:33019,varname:node_146,prsc:2|A-4580-OUT,B-5376-OUT;n:type:ShaderForge.SFN_Get,id:1702,x:31974,y:32711,varname:node_1702,prsc:2|IN-8218-OUT;n:type:ShaderForge.SFN_Add,id:7169,x:32198,y:32657,varname:node_7169,prsc:2|A-7166-OUT,B-1702-OUT;n:type:ShaderForge.SFN_Color,id:9302,x:30987,y:32006,ptovrint:False,ptlb:Rim Color,ptin:_RimColor,varname:node_9302,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.75,c2:1,c3:0.8034483,c4:1;n:type:ShaderForge.SFN_Color,id:3476,x:30897,y:32835,ptovrint:False,ptlb:Noise Color,ptin:_NoiseColor,varname:node_3476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5659825,c2:0.7794118,c3:0.1604671,c4:1;n:type:ShaderForge.SFN_Set,id:4944,x:31422,y:32037,varname:rimColor,prsc:2|IN-4035-OUT;n:type:ShaderForge.SFN_Multiply,id:4035,x:31230,y:32037,varname:node_4035,prsc:2|A-9302-RGB,B-8783-OUT;n:type:ShaderForge.SFN_Clamp01,id:9699,x:30770,y:32148,varname:node_9699,prsc:2|IN-2693-OUT;n:type:ShaderForge.SFN_Multiply,id:9009,x:31169,y:32921,varname:node_9009,prsc:2|A-3476-RGB,B-146-OUT;n:type:ShaderForge.SFN_Tex2d,id:892,x:31295,y:33677,ptovrint:False,ptlb:Noise Tex,ptin:_NoiseTex,varname:node_892,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-7321-OUT;n:type:ShaderForge.SFN_Set,id:3502,x:31467,y:33677,varname:noiseTexture,prsc:2|IN-892-RGB;n:type:ShaderForge.SFN_Get,id:4846,x:29144,y:32816,varname:node_4846,prsc:2|IN-3502-OUT;n:type:ShaderForge.SFN_Time,id:8530,x:29989,y:33809,varname:node_8530,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:7992,x:30594,y:33673,varname:node_7992,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:7321,x:31074,y:33677,varname:node_7321,prsc:2|A-2456-OUT,B-888-OUT;n:type:ShaderForge.SFN_Add,id:888,x:30826,y:33762,varname:node_888,prsc:2|A-7992-V,B-8409-OUT;n:type:ShaderForge.SFN_Sin,id:1478,x:30424,y:33488,varname:node_1478,prsc:2|IN-7845-T;n:type:ShaderForge.SFN_Time,id:7845,x:30237,y:33488,varname:node_7845,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:9114,x:30594,y:33488,varname:node_9114,prsc:2,frmn:-1,frmx:1,tomn:-0.1,tomx:0.2|IN-1478-OUT;n:type:ShaderForge.SFN_Add,id:2456,x:30826,y:33599,varname:node_2456,prsc:2|A-9114-OUT,B-7992-U;n:type:ShaderForge.SFN_Slider,id:5973,x:29832,y:33982,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5973,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.2564103,max:1;n:type:ShaderForge.SFN_Multiply,id:8758,x:30218,y:33890,varname:node_8758,prsc:2|A-8530-T,B-5973-OUT;n:type:ShaderForge.SFN_Posterize,id:6109,x:29920,y:32859,varname:node_6109,prsc:2|IN-9034-OUT,STPS-2012-OUT;n:type:ShaderForge.SFN_Vector1,id:2012,x:29716,y:32948,varname:node_2012,prsc:2,v1:2;n:type:ShaderForge.SFN_Posterize,id:8783,x:30987,y:32174,varname:node_8783,prsc:2|IN-9699-OUT,STPS-4561-OUT;n:type:ShaderForge.SFN_Vector1,id:4561,x:30770,y:32293,varname:node_4561,prsc:2,v1:4;n:type:ShaderForge.SFN_Cos,id:6545,x:30396,y:33890,varname:node_6545,prsc:2|IN-8758-OUT;n:type:ShaderForge.SFN_RemapRange,id:8409,x:30594,y:33890,varname:node_8409,prsc:2,frmn:-1,frmx:1,tomn:-0.1,tomx:0.2|IN-6545-OUT;n:type:ShaderForge.SFN_Posterize,id:3060,x:29368,y:32816,varname:node_3060,prsc:2|IN-4846-OUT,STPS-9727-OUT;n:type:ShaderForge.SFN_Vector1,id:9727,x:29165,y:32895,varname:node_9727,prsc:2,v1:4;n:type:ShaderForge.SFN_OneMinus,id:3780,x:29920,y:33038,varname:node_3780,prsc:2|IN-6109-OUT;n:type:ShaderForge.SFN_Multiply,id:4745,x:30165,y:33090,varname:node_4745,prsc:2|A-3780-OUT,B-3746-OUT;n:type:ShaderForge.SFN_Vector1,id:3746,x:29920,y:33210,varname:node_3746,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Add,id:4580,x:30392,y:32929,varname:node_4580,prsc:2|A-6109-OUT,B-4745-OUT;proporder:9302-3476-9851-9106-892-5973;pass:END;sub:END;*/

Shader "Shader Forge/Character test shader v5" {
    Properties {
        _RimColor ("Rim Color", Color) = (0.75,1,0.8034483,1)
        _NoiseColor ("Noise Color", Color) = (0.5659825,0.7794118,0.1604671,1)
        _RimIntensity ("Rim Intensity", Range(0, 10)) = 5.5
        _Fresnel ("Fresnel", Range(0, 10)) = 3
        _NoiseTex ("Noise Tex", 2D) = "white" {}
        _Speed ("Speed", Range(0, 1)) = 0.2564103
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float _RimIntensity;
            uniform float _Fresnel;
            uniform float4 _RimColor;
            uniform float4 _NoiseColor;
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_1715 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel);
                float4 node_7845 = _Time + _TimeEditor;
                float4 node_8530 = _Time + _TimeEditor;
                float2 node_7321 = float2(((sin(node_7845.g)*0.15+0.05)+i.uv0.r),(i.uv0.g+(cos((node_8530.g*_Speed))*0.15+0.05)));
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(node_7321, _NoiseTex));
                float3 noiseTexture = _NoiseTex_var.rgb;
                float3 node_2693 = (node_1715+((node_1715*noiseTexture)*_RimIntensity));
                float node_4561 = 4.0;
                float3 rimColor = (_RimColor.rgb*floor(saturate(node_2693) * node_4561) / (node_4561 - 1));
                float node_9727 = 4.0;
                float node_2012 = 2.0;
                float3 node_6109 = floor(saturate((1.0 - floor(noiseTexture * node_9727) / (node_9727 - 1))) * node_2012) / (node_2012 - 1);
                float3 rimMask = node_2693;
                float3 slimeNoise = (_NoiseColor.rgb*((node_6109+((1.0 - node_6109)*0.3))*(1.0 - rimMask)));
                float3 diffuseColor = saturate((rimColor+slimeNoise));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float _RimIntensity;
            uniform float _Fresnel;
            uniform float4 _RimColor;
            uniform float4 _NoiseColor;
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_1715 = pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel);
                float4 node_7845 = _Time + _TimeEditor;
                float4 node_8530 = _Time + _TimeEditor;
                float2 node_7321 = float2(((sin(node_7845.g)*0.15+0.05)+i.uv0.r),(i.uv0.g+(cos((node_8530.g*_Speed))*0.15+0.05)));
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(node_7321, _NoiseTex));
                float3 noiseTexture = _NoiseTex_var.rgb;
                float3 node_2693 = (node_1715+((node_1715*noiseTexture)*_RimIntensity));
                float node_4561 = 4.0;
                float3 rimColor = (_RimColor.rgb*floor(saturate(node_2693) * node_4561) / (node_4561 - 1));
                float node_9727 = 4.0;
                float node_2012 = 2.0;
                float3 node_6109 = floor(saturate((1.0 - floor(noiseTexture * node_9727) / (node_9727 - 1))) * node_2012) / (node_2012 - 1);
                float3 rimMask = node_2693;
                float3 slimeNoise = (_NoiseColor.rgb*((node_6109+((1.0 - node_6109)*0.3))*(1.0 - rimMask)));
                float3 diffuseColor = saturate((rimColor+slimeNoise));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
