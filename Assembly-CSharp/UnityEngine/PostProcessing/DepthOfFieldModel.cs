using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002E9 RID: 745
	[Serializable]
	public class DepthOfFieldModel : PostProcessingModel
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000AC825 File Offset: 0x000AAC25
		// (set) Token: 0x06001641 RID: 5697 RVA: 0x000AC82D File Offset: 0x000AAC2D
		public DepthOfFieldModel.Settings settings
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

		// Token: 0x06001642 RID: 5698 RVA: 0x000AC836 File Offset: 0x000AAC36
		public override void Reset()
		{
			this.m_Settings = DepthOfFieldModel.Settings.defaultSettings;
		}

		// Token: 0x040013CA RID: 5066
		[SerializeField]
		private DepthOfFieldModel.Settings m_Settings = DepthOfFieldModel.Settings.defaultSettings;

		// Token: 0x020002EA RID: 746
		public enum KernelSize
		{
			// Token: 0x040013CC RID: 5068
			Small,
			// Token: 0x040013CD RID: 5069
			Medium,
			// Token: 0x040013CE RID: 5070
			Large,
			// Token: 0x040013CF RID: 5071
			VeryLarge
		}

		// Token: 0x020002EB RID: 747
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700034A RID: 842
			// (get) Token: 0x06001643 RID: 5699 RVA: 0x000AC844 File Offset: 0x000AAC44
			public static DepthOfFieldModel.Settings defaultSettings
			{
				get
				{
					return new DepthOfFieldModel.Settings
					{
						focusDistance = 10f,
						aperture = 5.6f,
						focalLength = 50f,
						useCameraFov = false,
						kernelSize = DepthOfFieldModel.KernelSize.Medium
					};
				}
			}

			// Token: 0x040013D0 RID: 5072
			[Min(0.1f)]
			[Tooltip("Distance to the point of focus.")]
			public float focusDistance;

			// Token: 0x040013D1 RID: 5073
			[Range(0.05f, 32f)]
			[Tooltip("Ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
			public float aperture;

			// Token: 0x040013D2 RID: 5074
			[Range(1f, 300f)]
			[Tooltip("Distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
			public float focalLength;

			// Token: 0x040013D3 RID: 5075
			[Tooltip("Calculate the focal length automatically from the field-of-view value set on the camera. Using this setting isn't recommended.")]
			public bool useCameraFov;

			// Token: 0x040013D4 RID: 5076
			[Tooltip("Convolution kernel size of the bokeh filter, which determines the maximum radius of bokeh. It also affects the performance (the larger the kernel is, the longer the GPU time is required).")]
			public DepthOfFieldModel.KernelSize kernelSize;
		}
	}
}
