using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	public class VignetteModel : PostProcessingModel
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x000ACC31 File Offset: 0x000AB031
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x000ACC39 File Offset: 0x000AB039
		public VignetteModel.Settings settings
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

		// Token: 0x0600166A RID: 5738 RVA: 0x000ACC42 File Offset: 0x000AB042
		public override void Reset()
		{
			this.m_Settings = VignetteModel.Settings.defaultSettings;
		}

		// Token: 0x0400140A RID: 5130
		[SerializeField]
		private VignetteModel.Settings m_Settings = VignetteModel.Settings.defaultSettings;

		// Token: 0x02000301 RID: 769
		public enum Mode
		{
			// Token: 0x0400140C RID: 5132
			Classic,
			// Token: 0x0400140D RID: 5133
			Masked
		}

		// Token: 0x02000302 RID: 770
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700035A RID: 858
			// (get) Token: 0x0600166B RID: 5739 RVA: 0x000ACC50 File Offset: 0x000AB050
			public static VignetteModel.Settings defaultSettings
			{
				get
				{
					return new VignetteModel.Settings
					{
						mode = VignetteModel.Mode.Classic,
						color = new Color(0f, 0f, 0f, 1f),
						center = new Vector2(0.5f, 0.5f),
						intensity = 0.45f,
						smoothness = 0.2f,
						roundness = 1f,
						mask = null,
						opacity = 1f,
						rounded = false
					};
				}
			}

			// Token: 0x0400140E RID: 5134
			[Tooltip("Use the \"Classic\" mode for parametric controls. Use the \"Masked\" mode to use your own texture mask.")]
			public VignetteModel.Mode mode;

			// Token: 0x0400140F RID: 5135
			[ColorUsage(false)]
			[Tooltip("Vignette color. Use the alpha channel for transparency.")]
			public Color color;

			// Token: 0x04001410 RID: 5136
			[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
			public Vector2 center;

			// Token: 0x04001411 RID: 5137
			[Range(0f, 1f)]
			[Tooltip("Amount of vignetting on screen.")]
			public float intensity;

			// Token: 0x04001412 RID: 5138
			[Range(0.01f, 1f)]
			[Tooltip("Smoothness of the vignette borders.")]
			public float smoothness;

			// Token: 0x04001413 RID: 5139
			[Range(0f, 1f)]
			[Tooltip("Lower values will make a square-ish vignette.")]
			public float roundness;

			// Token: 0x04001414 RID: 5140
			[Tooltip("A black and white mask to use as a vignette.")]
			public Texture mask;

			// Token: 0x04001415 RID: 5141
			[Range(0f, 1f)]
			[Tooltip("Mask opacity.")]
			public float opacity;

			// Token: 0x04001416 RID: 5142
			[Tooltip("Should the vignette be perfectly round or be dependent on the current aspect ratio?")]
			public bool rounded;
		}
	}
}
