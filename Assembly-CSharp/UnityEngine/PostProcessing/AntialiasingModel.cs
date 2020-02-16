using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002CB RID: 715
	[Serializable]
	public class AntialiasingModel : PostProcessingModel
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000ABCCA File Offset: 0x000AA0CA
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x000ABCD2 File Offset: 0x000AA0D2
		public AntialiasingModel.Settings settings
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

		// Token: 0x06001611 RID: 5649 RVA: 0x000ABCDB File Offset: 0x000AA0DB
		public override void Reset()
		{
			this.m_Settings = AntialiasingModel.Settings.defaultSettings;
		}

		// Token: 0x04001356 RID: 4950
		[SerializeField]
		private AntialiasingModel.Settings m_Settings = AntialiasingModel.Settings.defaultSettings;

		// Token: 0x020002CC RID: 716
		public enum Method
		{
			// Token: 0x04001358 RID: 4952
			Fxaa,
			// Token: 0x04001359 RID: 4953
			Taa
		}

		// Token: 0x020002CD RID: 717
		public enum FxaaPreset
		{
			// Token: 0x0400135B RID: 4955
			ExtremePerformance,
			// Token: 0x0400135C RID: 4956
			Performance,
			// Token: 0x0400135D RID: 4957
			Default,
			// Token: 0x0400135E RID: 4958
			Quality,
			// Token: 0x0400135F RID: 4959
			ExtremeQuality
		}

		// Token: 0x020002CE RID: 718
		[Serializable]
		public struct FxaaQualitySettings
		{
			// Token: 0x04001360 RID: 4960
			[Tooltip("The amount of desired sub-pixel aliasing removal. Effects the sharpeness of the output.")]
			[Range(0f, 1f)]
			public float subpixelAliasingRemovalAmount;

			// Token: 0x04001361 RID: 4961
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.063f, 0.333f)]
			public float edgeDetectionThreshold;

			// Token: 0x04001362 RID: 4962
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0f, 0.0833f)]
			public float minimumRequiredLuminance;

			// Token: 0x04001363 RID: 4963
			public static AntialiasingModel.FxaaQualitySettings[] presets = new AntialiasingModel.FxaaQualitySettings[]
			{
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0f,
					edgeDetectionThreshold = 0.333f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.25f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.75f,
					edgeDetectionThreshold = 0.166f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.0625f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.063f,
					minimumRequiredLuminance = 0.0312f
				}
			};
		}

		// Token: 0x020002CF RID: 719
		[Serializable]
		public struct FxaaConsoleSettings
		{
			// Token: 0x04001364 RID: 4964
			[Tooltip("The amount of spread applied to the sampling coordinates while sampling for subpixel information.")]
			[Range(0.33f, 0.5f)]
			public float subpixelSpreadAmount;

			// Token: 0x04001365 RID: 4965
			[Tooltip("This value dictates how sharp the edges in the image are kept; a higher value implies sharper edges.")]
			[Range(2f, 8f)]
			public float edgeSharpnessAmount;

			// Token: 0x04001366 RID: 4966
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.125f, 0.25f)]
			public float edgeDetectionThreshold;

			// Token: 0x04001367 RID: 4967
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0.04f, 0.06f)]
			public float minimumRequiredLuminance;

			// Token: 0x04001368 RID: 4968
			public static AntialiasingModel.FxaaConsoleSettings[] presets = new AntialiasingModel.FxaaConsoleSettings[]
			{
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.05f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 4f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 2f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				}
			};
		}

		// Token: 0x020002D0 RID: 720
		[Serializable]
		public struct FxaaSettings
		{
			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06001614 RID: 5652 RVA: 0x000ABF94 File Offset: 0x000AA394
			public static AntialiasingModel.FxaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.FxaaSettings
					{
						preset = AntialiasingModel.FxaaPreset.Default
					};
				}
			}

			// Token: 0x04001369 RID: 4969
			public AntialiasingModel.FxaaPreset preset;
		}

		// Token: 0x020002D1 RID: 721
		[Serializable]
		public struct TaaSettings
		{
			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06001615 RID: 5653 RVA: 0x000ABFB4 File Offset: 0x000AA3B4
			public static AntialiasingModel.TaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.TaaSettings
					{
						jitterSpread = 0.75f,
						sharpen = 0.3f,
						stationaryBlending = 0.95f,
						motionBlending = 0.85f
					};
				}
			}

			// Token: 0x0400136A RID: 4970
			[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable but blurrier output.")]
			[Range(0.1f, 1f)]
			public float jitterSpread;

			// Token: 0x0400136B RID: 4971
			[Tooltip("Controls the amount of sharpening applied to the color buffer.")]
			[Range(0f, 3f)]
			public float sharpen;

			// Token: 0x0400136C RID: 4972
			[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float stationaryBlending;

			// Token: 0x0400136D RID: 4973
			[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 0.99f)]
			public float motionBlending;
		}

		// Token: 0x020002D2 RID: 722
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06001616 RID: 5654 RVA: 0x000ABFFC File Offset: 0x000AA3FC
			public static AntialiasingModel.Settings defaultSettings
			{
				get
				{
					return new AntialiasingModel.Settings
					{
						method = AntialiasingModel.Method.Fxaa,
						fxaaSettings = AntialiasingModel.FxaaSettings.defaultSettings,
						taaSettings = AntialiasingModel.TaaSettings.defaultSettings
					};
				}
			}

			// Token: 0x0400136E RID: 4974
			public AntialiasingModel.Method method;

			// Token: 0x0400136F RID: 4975
			public AntialiasingModel.FxaaSettings fxaaSettings;

			// Token: 0x04001370 RID: 4976
			public AntialiasingModel.TaaSettings taaSettings;
		}
	}
}
