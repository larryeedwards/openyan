using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002F7 RID: 759
	[Serializable]
	public class ScreenSpaceReflectionModel : PostProcessingModel
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x000ACAD2 File Offset: 0x000AAED2
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x000ACADA File Offset: 0x000AAEDA
		public ScreenSpaceReflectionModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000ACAE3 File Offset: 0x000AAEE3
		public override void Reset()
		{
			this.m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;
		}

		// Token: 0x040013F0 RID: 5104
		[SerializeField]
		private ScreenSpaceReflectionModel.Settings m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;

		// Token: 0x020002F8 RID: 760
		public enum SSRResolution
		{
			// Token: 0x040013F2 RID: 5106
			High,
			// Token: 0x040013F3 RID: 5107
			Low = 2
		}

		// Token: 0x020002F9 RID: 761
		public enum SSRReflectionBlendType
		{
			// Token: 0x040013F5 RID: 5109
			PhysicallyBased,
			// Token: 0x040013F6 RID: 5110
			Additive
		}

		// Token: 0x020002FA RID: 762
		[Serializable]
		public struct IntensitySettings
		{
			// Token: 0x040013F7 RID: 5111
			[Tooltip("Nonphysical multiplier for the SSR reflections. 1.0 is physically based.")]
			[Range(0f, 2f)]
			public float reflectionMultiplier;

			// Token: 0x040013F8 RID: 5112
			[Tooltip("How far away from the maxDistance to begin fading SSR.")]
			[Range(0f, 1000f)]
			public float fadeDistance;

			// Token: 0x040013F9 RID: 5113
			[Tooltip("Amplify Fresnel fade out. Increase if floor reflections look good close to the surface and bad farther 'under' the floor.")]
			[Range(0f, 1f)]
			public float fresnelFade;

			// Token: 0x040013FA RID: 5114
			[Tooltip("Higher values correspond to a faster Fresnel fade as the reflection changes from the grazing angle.")]
			[Range(0.1f, 10f)]
			public float fresnelFadePower;
		}

		// Token: 0x020002FB RID: 763
		[Serializable]
		public struct ReflectionSettings
		{
			// Token: 0x040013FB RID: 5115
			[Tooltip("How the reflections are blended into the render.")]
			public ScreenSpaceReflectionModel.SSRReflectionBlendType blendType;

			// Token: 0x040013FC RID: 5116
			[Tooltip("Half resolution SSRR is much faster, but less accurate.")]
			public ScreenSpaceReflectionModel.SSRResolution reflectionQuality;

			// Token: 0x040013FD RID: 5117
			[Tooltip("Maximum reflection distance in world units.")]
			[Range(0.1f, 300f)]
			public float maxDistance;

			// Token: 0x040013FE RID: 5118
			[Tooltip("Max raytracing length.")]
			[Range(16f, 1024f)]
			public int iterationCount;

			// Token: 0x040013FF RID: 5119
			[Tooltip("Log base 2 of ray tracing coarse step size. Higher traces farther, lower gives better quality silhouettes.")]
			[Range(1f, 16f)]
			public int stepSize;

			// Token: 0x04001400 RID: 5120
			[Tooltip("Typical thickness of columns, walls, furniture, and other objects that reflection rays might pass behind.")]
			[Range(0.01f, 10f)]
			public float widthModifier;

			// Token: 0x04001401 RID: 5121
			[Tooltip("Blurriness of reflections.")]
			[Range(0.1f, 8f)]
			public float reflectionBlur;

			// Token: 0x04001402 RID: 5122
			[Tooltip("Disable for a performance gain in scenes where most glossy objects are horizontal, like floors, water, and tables. Leave on for scenes with glossy vertical objects.")]
			public bool reflectBackfaces;
		}

		// Token: 0x020002FC RID: 764
		[Serializable]
		public struct ScreenEdgeMask
		{
			// Token: 0x04001403 RID: 5123
			[Tooltip("Higher = fade out SSRR near the edge of the screen so that reflections don't pop under camera motion.")]
			[Range(0f, 1f)]
			public float intensity;
		}

		// Token: 0x020002FD RID: 765
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000356 RID: 854
			// (get) Token: 0x06001661 RID: 5729 RVA: 0x000ACAF0 File Offset: 0x000AAEF0
			public static ScreenSpaceReflectionModel.Settings defaultSettings
			{
				get
				{
					return new ScreenSpaceReflectionModel.Settings
					{
						reflection = new ScreenSpaceReflectionModel.ReflectionSettings
						{
							blendType = ScreenSpaceReflectionModel.SSRReflectionBlendType.PhysicallyBased,
							reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low,
							maxDistance = 100f,
							iterationCount = 256,
							stepSize = 3,
							widthModifier = 0.5f,
							reflectionBlur = 1f,
							reflectBackfaces = false
						},
						intensity = new ScreenSpaceReflectionModel.IntensitySettings
						{
							reflectionMultiplier = 1f,
							fadeDistance = 100f,
							fresnelFade = 1f,
							fresnelFadePower = 1f
						},
						screenEdgeMask = new ScreenSpaceReflectionModel.ScreenEdgeMask
						{
							intensity = 0.03f
						}
					};
				}
			}

			// Token: 0x04001404 RID: 5124
			public ScreenSpaceReflectionModel.ReflectionSettings reflection;

			// Token: 0x04001405 RID: 5125
			public ScreenSpaceReflectionModel.IntensitySettings intensity;

			// Token: 0x04001406 RID: 5126
			public ScreenSpaceReflectionModel.ScreenEdgeMask screenEdgeMask;
		}
	}
}
