using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002D3 RID: 723
	[Serializable]
	public class BloomModel : PostProcessingModel
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x000AC045 File Offset: 0x000AA445
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x000AC04D File Offset: 0x000AA44D
		public BloomModel.Settings settings
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

		// Token: 0x0600161A RID: 5658 RVA: 0x000AC056 File Offset: 0x000AA456
		public override void Reset()
		{
			this.m_Settings = BloomModel.Settings.defaultSettings;
		}

		// Token: 0x04001371 RID: 4977
		[SerializeField]
		private BloomModel.Settings m_Settings = BloomModel.Settings.defaultSettings;

		// Token: 0x020002D4 RID: 724
		[Serializable]
		public struct BloomSettings
		{
			// Token: 0x17000333 RID: 819
			// (get) Token: 0x0600161C RID: 5660 RVA: 0x000AC071 File Offset: 0x000AA471
			// (set) Token: 0x0600161B RID: 5659 RVA: 0x000AC063 File Offset: 0x000AA463
			public float thresholdLinear
			{
				get
				{
					return Mathf.GammaToLinearSpace(this.threshold);
				}
				set
				{
					this.threshold = Mathf.LinearToGammaSpace(value);
				}
			}

			// Token: 0x17000334 RID: 820
			// (get) Token: 0x0600161D RID: 5661 RVA: 0x000AC080 File Offset: 0x000AA480
			public static BloomModel.BloomSettings defaultSettings
			{
				get
				{
					return new BloomModel.BloomSettings
					{
						intensity = 0.5f,
						threshold = 1.1f,
						softKnee = 0.5f,
						radius = 4f,
						antiFlicker = false
					};
				}
			}

			// Token: 0x04001372 RID: 4978
			[Min(0f)]
			[Tooltip("Strength of the bloom filter.")]
			public float intensity;

			// Token: 0x04001373 RID: 4979
			[Min(0f)]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x04001374 RID: 4980
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual (0 = hard threshold, 1 = soft threshold).")]
			public float softKnee;

			// Token: 0x04001375 RID: 4981
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x04001376 RID: 4982
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;
		}

		// Token: 0x020002D5 RID: 725
		[Serializable]
		public struct LensDirtSettings
		{
			// Token: 0x17000335 RID: 821
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x000AC0D0 File Offset: 0x000AA4D0
			public static BloomModel.LensDirtSettings defaultSettings
			{
				get
				{
					return new BloomModel.LensDirtSettings
					{
						texture = null,
						intensity = 3f
					};
				}
			}

			// Token: 0x04001377 RID: 4983
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture texture;

			// Token: 0x04001378 RID: 4984
			[Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float intensity;
		}

		// Token: 0x020002D6 RID: 726
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000336 RID: 822
			// (get) Token: 0x0600161F RID: 5663 RVA: 0x000AC0FC File Offset: 0x000AA4FC
			public static BloomModel.Settings defaultSettings
			{
				get
				{
					return new BloomModel.Settings
					{
						bloom = BloomModel.BloomSettings.defaultSettings,
						lensDirt = BloomModel.LensDirtSettings.defaultSettings
					};
				}
			}

			// Token: 0x04001379 RID: 4985
			public BloomModel.BloomSettings bloom;

			// Token: 0x0400137A RID: 4986
			public BloomModel.LensDirtSettings lensDirt;
		}
	}
}
