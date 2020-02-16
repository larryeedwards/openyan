using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	public class AmbientOcclusionModel : PostProcessingModel
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x000ABC42 File Offset: 0x000AA042
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x000ABC4A File Offset: 0x000AA04A
		public AmbientOcclusionModel.Settings settings
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

		// Token: 0x0600160C RID: 5644 RVA: 0x000ABC53 File Offset: 0x000AA053
		public override void Reset()
		{
			this.m_Settings = AmbientOcclusionModel.Settings.defaultSettings;
		}

		// Token: 0x04001349 RID: 4937
		[SerializeField]
		private AmbientOcclusionModel.Settings m_Settings = AmbientOcclusionModel.Settings.defaultSettings;

		// Token: 0x020002C9 RID: 713
		public enum SampleCount
		{
			// Token: 0x0400134B RID: 4939
			Lowest = 3,
			// Token: 0x0400134C RID: 4940
			Low = 6,
			// Token: 0x0400134D RID: 4941
			Medium = 10,
			// Token: 0x0400134E RID: 4942
			High = 16
		}

		// Token: 0x020002CA RID: 714
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700032D RID: 813
			// (get) Token: 0x0600160D RID: 5645 RVA: 0x000ABC60 File Offset: 0x000AA060
			public static AmbientOcclusionModel.Settings defaultSettings
			{
				get
				{
					return new AmbientOcclusionModel.Settings
					{
						intensity = 1f,
						radius = 0.3f,
						sampleCount = AmbientOcclusionModel.SampleCount.Medium,
						downsampling = true,
						forceForwardCompatibility = false,
						ambientOnly = false,
						highPrecision = false
					};
				}
			}

			// Token: 0x0400134F RID: 4943
			[Range(0f, 4f)]
			[Tooltip("Degree of darkness produced by the effect.")]
			public float intensity;

			// Token: 0x04001350 RID: 4944
			[Min(0.0001f)]
			[Tooltip("Radius of sample points, which affects extent of darkened areas.")]
			public float radius;

			// Token: 0x04001351 RID: 4945
			[Tooltip("Number of sample points, which affects quality and performance.")]
			public AmbientOcclusionModel.SampleCount sampleCount;

			// Token: 0x04001352 RID: 4946
			[Tooltip("Halves the resolution of the effect to increase performance at the cost of visual quality.")]
			public bool downsampling;

			// Token: 0x04001353 RID: 4947
			[Tooltip("Forces compatibility with Forward rendered objects when working with the Deferred rendering path.")]
			public bool forceForwardCompatibility;

			// Token: 0x04001354 RID: 4948
			[Tooltip("Enables the ambient-only mode in that the effect only affects ambient lighting. This mode is only available with the Deferred rendering path and HDR rendering.")]
			public bool ambientOnly;

			// Token: 0x04001355 RID: 4949
			[Tooltip("Toggles the use of a higher precision depth texture with the forward rendering path (may impact performances). Has no effect with the deferred rendering path.")]
			public bool highPrecision;
		}
	}
}
