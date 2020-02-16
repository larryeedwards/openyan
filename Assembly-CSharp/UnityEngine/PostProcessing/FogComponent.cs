using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002B3 RID: 691
	public sealed class FogComponent : PostProcessingComponentCommandBuffer<FogModel>
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000A9483 File Offset: 0x000A7883
		public override bool active
		{
			get
			{
				return base.model.enabled && this.context.isGBufferAvailable && RenderSettings.fog && !this.context.interrupted;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000A94C0 File Offset: 0x000A78C0
		public override string GetName()
		{
			return "Fog";
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000A94C7 File Offset: 0x000A78C7
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000A94CA File Offset: 0x000A78CA
		public override CameraEvent GetCameraEvent()
		{
			return CameraEvent.AfterImageEffectsOpaque;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000A94D0 File Offset: 0x000A78D0
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			FogModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Fog");
			material.shaderKeywords = null;
			Color value = (!GraphicsUtils.isLinearColorSpace) ? RenderSettings.fogColor : RenderSettings.fogColor.linear;
			material.SetColor(FogComponent.Uniforms._FogColor, value);
			material.SetFloat(FogComponent.Uniforms._Density, RenderSettings.fogDensity);
			material.SetFloat(FogComponent.Uniforms._Start, RenderSettings.fogStartDistance);
			material.SetFloat(FogComponent.Uniforms._End, RenderSettings.fogEndDistance);
			FogMode fogMode = RenderSettings.fogMode;
			if (fogMode != FogMode.Linear)
			{
				if (fogMode != FogMode.Exponential)
				{
					if (fogMode == FogMode.ExponentialSquared)
					{
						material.EnableKeyword("FOG_EXP2");
					}
				}
				else
				{
					material.EnableKeyword("FOG_EXP");
				}
			}
			else
			{
				material.EnableKeyword("FOG_LINEAR");
			}
			RenderTextureFormat format = (!this.context.isHdr) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
			cb.GetTemporaryRT(FogComponent.Uniforms._TempRT, this.context.width, this.context.height, 24, FilterMode.Bilinear, format);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, FogComponent.Uniforms._TempRT);
			cb.Blit(FogComponent.Uniforms._TempRT, BuiltinRenderTextureType.CameraTarget, material, (!settings.excludeSkybox) ? 0 : 1);
			cb.ReleaseTemporaryRT(FogComponent.Uniforms._TempRT);
		}

		// Token: 0x040012C6 RID: 4806
		private const string k_ShaderString = "Hidden/Post FX/Fog";

		// Token: 0x020002B4 RID: 692
		private static class Uniforms
		{
			// Token: 0x040012C7 RID: 4807
			internal static readonly int _FogColor = Shader.PropertyToID("_FogColor");

			// Token: 0x040012C8 RID: 4808
			internal static readonly int _Density = Shader.PropertyToID("_Density");

			// Token: 0x040012C9 RID: 4809
			internal static readonly int _Start = Shader.PropertyToID("_Start");

			// Token: 0x040012CA RID: 4810
			internal static readonly int _End = Shader.PropertyToID("_End");

			// Token: 0x040012CB RID: 4811
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");
		}
	}
}
