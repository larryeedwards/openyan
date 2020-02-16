using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002F1 RID: 753
	[Serializable]
	public class FogModel : PostProcessingModel
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x000AC9A6 File Offset: 0x000AADA6
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x000AC9AE File Offset: 0x000AADAE
		public FogModel.Settings settings
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

		// Token: 0x06001651 RID: 5713 RVA: 0x000AC9B7 File Offset: 0x000AADB7
		public override void Reset()
		{
			this.m_Settings = FogModel.Settings.defaultSettings;
		}

		// Token: 0x040013E5 RID: 5093
		[SerializeField]
		private FogModel.Settings m_Settings = FogModel.Settings.defaultSettings;

		// Token: 0x020002F2 RID: 754
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06001652 RID: 5714 RVA: 0x000AC9C4 File Offset: 0x000AADC4
			public static FogModel.Settings defaultSettings
			{
				get
				{
					return new FogModel.Settings
					{
						excludeSkybox = true
					};
				}
			}

			// Token: 0x040013E6 RID: 5094
			[Tooltip("Should the fog affect the skybox?")]
			public bool excludeSkybox;
		}
	}
}
