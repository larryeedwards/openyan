using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002F5 RID: 757
	[Serializable]
	public class MotionBlurModel : PostProcessingModel
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000ACA69 File Offset: 0x000AAE69
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x000ACA71 File Offset: 0x000AAE71
		public MotionBlurModel.Settings settings
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

		// Token: 0x0600165B RID: 5723 RVA: 0x000ACA7A File Offset: 0x000AAE7A
		public override void Reset()
		{
			this.m_Settings = MotionBlurModel.Settings.defaultSettings;
		}

		// Token: 0x040013EC RID: 5100
		[SerializeField]
		private MotionBlurModel.Settings m_Settings = MotionBlurModel.Settings.defaultSettings;

		// Token: 0x020002F6 RID: 758
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000354 RID: 852
			// (get) Token: 0x0600165C RID: 5724 RVA: 0x000ACA88 File Offset: 0x000AAE88
			public static MotionBlurModel.Settings defaultSettings
			{
				get
				{
					return new MotionBlurModel.Settings
					{
						shutterAngle = 270f,
						sampleCount = 10,
						frameBlending = 0f
					};
				}
			}

			// Token: 0x040013ED RID: 5101
			[Range(0f, 360f)]
			[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
			public float shutterAngle;

			// Token: 0x040013EE RID: 5102
			[Range(4f, 32f)]
			[Tooltip("The amount of sample points, which affects quality and performances.")]
			public int sampleCount;

			// Token: 0x040013EF RID: 5103
			[Range(0f, 1f)]
			[Tooltip("The strength of multiple frame blending. The opacity of preceding frames are determined from this coefficient and time differences.")]
			public float frameBlending;
		}
	}
}
