using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class GrainModel : PostProcessingModel
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x000AC9F5 File Offset: 0x000AADF5
		// (set) Token: 0x06001655 RID: 5717 RVA: 0x000AC9FD File Offset: 0x000AADFD
		public GrainModel.Settings settings
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

		// Token: 0x06001656 RID: 5718 RVA: 0x000ACA06 File Offset: 0x000AAE06
		public override void Reset()
		{
			this.m_Settings = GrainModel.Settings.defaultSettings;
		}

		// Token: 0x040013E7 RID: 5095
		[SerializeField]
		private GrainModel.Settings m_Settings = GrainModel.Settings.defaultSettings;

		// Token: 0x020002F4 RID: 756
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000352 RID: 850
			// (get) Token: 0x06001657 RID: 5719 RVA: 0x000ACA14 File Offset: 0x000AAE14
			public static GrainModel.Settings defaultSettings
			{
				get
				{
					return new GrainModel.Settings
					{
						colored = true,
						intensity = 0.5f,
						size = 1f,
						luminanceContribution = 0.8f
					};
				}
			}

			// Token: 0x040013E8 RID: 5096
			[Tooltip("Enable the use of colored grain.")]
			public bool colored;

			// Token: 0x040013E9 RID: 5097
			[Range(0f, 1f)]
			[Tooltip("Grain strength. Higher means more visible grain.")]
			public float intensity;

			// Token: 0x040013EA RID: 5098
			[Range(0.3f, 3f)]
			[Tooltip("Grain particle size.")]
			public float size;

			// Token: 0x040013EB RID: 5099
			[Range(0f, 1f)]
			[Tooltip("Controls the noisiness response curve based on scene luminance. Lower values mean less noise in dark areas.")]
			public float luminanceContribution;
		}
	}
}
