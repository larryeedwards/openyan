using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002EC RID: 748
	[Serializable]
	public class DitheringModel : PostProcessingModel
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000AC8A1 File Offset: 0x000AACA1
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x000AC8A9 File Offset: 0x000AACA9
		public DitheringModel.Settings settings
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

		// Token: 0x06001647 RID: 5703 RVA: 0x000AC8B2 File Offset: 0x000AACB2
		public override void Reset()
		{
			this.m_Settings = DitheringModel.Settings.defaultSettings;
		}

		// Token: 0x040013D5 RID: 5077
		[SerializeField]
		private DitheringModel.Settings m_Settings = DitheringModel.Settings.defaultSettings;

		// Token: 0x020002ED RID: 749
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700034C RID: 844
			// (get) Token: 0x06001648 RID: 5704 RVA: 0x000AC8C0 File Offset: 0x000AACC0
			public static DitheringModel.Settings defaultSettings
			{
				get
				{
					return default(DitheringModel.Settings);
				}
			}
		}
	}
}
