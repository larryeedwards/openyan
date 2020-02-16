using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002D7 RID: 727
	[Serializable]
	public class BuiltinDebugViewsModel : PostProcessingModel
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x000AC13D File Offset: 0x000AA53D
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x000AC145 File Offset: 0x000AA545
		public BuiltinDebugViewsModel.Settings settings
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x000AC14E File Offset: 0x000AA54E
		public bool willInterrupt
		{
			get
			{
				return !this.IsModeActive(BuiltinDebugViewsModel.Mode.None) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.LogLut) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.UserLut);
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000AC18E File Offset: 0x000AA58E
		public override void Reset()
		{
			this.settings = BuiltinDebugViewsModel.Settings.defaultSettings;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000AC19B File Offset: 0x000AA59B
		public bool IsModeActive(BuiltinDebugViewsModel.Mode mode)
		{
			return this.m_Settings.mode == mode;
		}

		// Token: 0x0400137B RID: 4987
		[SerializeField]
		private BuiltinDebugViewsModel.Settings m_Settings = BuiltinDebugViewsModel.Settings.defaultSettings;

		// Token: 0x020002D8 RID: 728
		[Serializable]
		public struct DepthSettings
		{
			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06001626 RID: 5670 RVA: 0x000AC1AC File Offset: 0x000AA5AC
			public static BuiltinDebugViewsModel.DepthSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.DepthSettings
					{
						scale = 1f
					};
				}
			}

			// Token: 0x0400137C RID: 4988
			[Range(0f, 1f)]
			[Tooltip("Scales the camera far plane before displaying the depth map.")]
			public float scale;
		}

		// Token: 0x020002D9 RID: 729
		[Serializable]
		public struct MotionVectorsSettings
		{
			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06001627 RID: 5671 RVA: 0x000AC1D0 File Offset: 0x000AA5D0
			public static BuiltinDebugViewsModel.MotionVectorsSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.MotionVectorsSettings
					{
						sourceOpacity = 1f,
						motionImageOpacity = 0f,
						motionImageAmplitude = 16f,
						motionVectorsOpacity = 1f,
						motionVectorsResolution = 24,
						motionVectorsAmplitude = 64f
					};
				}
			}

			// Token: 0x0400137D RID: 4989
			[Range(0f, 1f)]
			[Tooltip("Opacity of the source render.")]
			public float sourceOpacity;

			// Token: 0x0400137E RID: 4990
			[Range(0f, 1f)]
			[Tooltip("Opacity of the per-pixel motion vector colors.")]
			public float motionImageOpacity;

			// Token: 0x0400137F RID: 4991
			[Min(0f)]
			[Tooltip("Because motion vectors are mainly very small vectors, you can use this setting to make them more visible.")]
			public float motionImageAmplitude;

			// Token: 0x04001380 RID: 4992
			[Range(0f, 1f)]
			[Tooltip("Opacity for the motion vector arrows.")]
			public float motionVectorsOpacity;

			// Token: 0x04001381 RID: 4993
			[Range(8f, 64f)]
			[Tooltip("The arrow density on screen.")]
			public int motionVectorsResolution;

			// Token: 0x04001382 RID: 4994
			[Min(0f)]
			[Tooltip("Tweaks the arrows length.")]
			public float motionVectorsAmplitude;
		}

		// Token: 0x020002DA RID: 730
		public enum Mode
		{
			// Token: 0x04001384 RID: 4996
			None,
			// Token: 0x04001385 RID: 4997
			Depth,
			// Token: 0x04001386 RID: 4998
			Normals,
			// Token: 0x04001387 RID: 4999
			MotionVectors,
			// Token: 0x04001388 RID: 5000
			AmbientOcclusion,
			// Token: 0x04001389 RID: 5001
			EyeAdaptation,
			// Token: 0x0400138A RID: 5002
			FocusPlane,
			// Token: 0x0400138B RID: 5003
			PreGradingLog,
			// Token: 0x0400138C RID: 5004
			LogLut,
			// Token: 0x0400138D RID: 5005
			UserLut
		}

		// Token: 0x020002DB RID: 731
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700033B RID: 827
			// (get) Token: 0x06001628 RID: 5672 RVA: 0x000AC22C File Offset: 0x000AA62C
			public static BuiltinDebugViewsModel.Settings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.Settings
					{
						mode = BuiltinDebugViewsModel.Mode.None,
						depth = BuiltinDebugViewsModel.DepthSettings.defaultSettings,
						motionVectors = BuiltinDebugViewsModel.MotionVectorsSettings.defaultSettings
					};
				}
			}

			// Token: 0x0400138E RID: 5006
			public BuiltinDebugViewsModel.Mode mode;

			// Token: 0x0400138F RID: 5007
			public BuiltinDebugViewsModel.DepthSettings depth;

			// Token: 0x04001390 RID: 5008
			public BuiltinDebugViewsModel.MotionVectorsSettings motionVectors;
		}
	}
}
