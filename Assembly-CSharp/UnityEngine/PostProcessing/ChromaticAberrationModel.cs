using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	public class ChromaticAberrationModel : PostProcessingModel
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000AC275 File Offset: 0x000AA675
		// (set) Token: 0x0600162B RID: 5675 RVA: 0x000AC27D File Offset: 0x000AA67D
		public ChromaticAberrationModel.Settings settings
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

		// Token: 0x0600162C RID: 5676 RVA: 0x000AC286 File Offset: 0x000AA686
		public override void Reset()
		{
			this.m_Settings = ChromaticAberrationModel.Settings.defaultSettings;
		}

		// Token: 0x04001391 RID: 5009
		[SerializeField]
		private ChromaticAberrationModel.Settings m_Settings = ChromaticAberrationModel.Settings.defaultSettings;

		// Token: 0x020002DD RID: 733
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700033D RID: 829
			// (get) Token: 0x0600162D RID: 5677 RVA: 0x000AC294 File Offset: 0x000AA694
			public static ChromaticAberrationModel.Settings defaultSettings
			{
				get
				{
					return new ChromaticAberrationModel.Settings
					{
						spectralTexture = null,
						intensity = 0.1f
					};
				}
			}

			// Token: 0x04001392 RID: 5010
			[Tooltip("Shift the hue of chromatic aberrations.")]
			public Texture2D spectralTexture;

			// Token: 0x04001393 RID: 5011
			[Range(0f, 1f)]
			[Tooltip("Amount of tangential distortion.")]
			public float intensity;
		}
	}
}
