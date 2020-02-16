using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002FE RID: 766
	[Serializable]
	public class UserLutModel : PostProcessingModel
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x000ACBD5 File Offset: 0x000AAFD5
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x000ACBDD File Offset: 0x000AAFDD
		public UserLutModel.Settings settings
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

		// Token: 0x06001665 RID: 5733 RVA: 0x000ACBE6 File Offset: 0x000AAFE6
		public override void Reset()
		{
			this.m_Settings = UserLutModel.Settings.defaultSettings;
		}

		// Token: 0x04001407 RID: 5127
		[SerializeField]
		private UserLutModel.Settings m_Settings = UserLutModel.Settings.defaultSettings;

		// Token: 0x020002FF RID: 767
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000358 RID: 856
			// (get) Token: 0x06001666 RID: 5734 RVA: 0x000ACBF4 File Offset: 0x000AAFF4
			public static UserLutModel.Settings defaultSettings
			{
				get
				{
					return new UserLutModel.Settings
					{
						lut = null,
						contribution = 1f
					};
				}
			}

			// Token: 0x04001408 RID: 5128
			[Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
			public Texture2D lut;

			// Token: 0x04001409 RID: 5129
			[Range(0f, 1f)]
			[Tooltip("Blending factor.")]
			public float contribution;
		}
	}
}
