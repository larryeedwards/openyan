using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002DE RID: 734
	[Serializable]
	public class ColorGradingModel : PostProcessingModel
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x000AC2D1 File Offset: 0x000AA6D1
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x000AC2D9 File Offset: 0x000AA6D9
		public ColorGradingModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
				this.OnValidate();
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x000AC2E8 File Offset: 0x000AA6E8
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x000AC2F0 File Offset: 0x000AA6F0
		public bool isDirty { get; internal set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000AC2F9 File Offset: 0x000AA6F9
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x000AC301 File Offset: 0x000AA701
		public RenderTexture bakedLut { get; internal set; }

		// Token: 0x06001635 RID: 5685 RVA: 0x000AC30A File Offset: 0x000AA70A
		public override void Reset()
		{
			this.m_Settings = ColorGradingModel.Settings.defaultSettings;
			this.OnValidate();
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000AC31D File Offset: 0x000AA71D
		public override void OnValidate()
		{
			this.isDirty = true;
		}

		// Token: 0x04001394 RID: 5012
		[SerializeField]
		private ColorGradingModel.Settings m_Settings = ColorGradingModel.Settings.defaultSettings;

		// Token: 0x020002DF RID: 735
		public enum Tonemapper
		{
			// Token: 0x04001398 RID: 5016
			None,
			// Token: 0x04001399 RID: 5017
			ACES,
			// Token: 0x0400139A RID: 5018
			Neutral
		}

		// Token: 0x020002E0 RID: 736
		[Serializable]
		public struct TonemappingSettings
		{
			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06001637 RID: 5687 RVA: 0x000AC328 File Offset: 0x000AA728
			public static ColorGradingModel.TonemappingSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.TonemappingSettings
					{
						tonemapper = ColorGradingModel.Tonemapper.Neutral,
						neutralBlackIn = 0.02f,
						neutralWhiteIn = 10f,
						neutralBlackOut = 0f,
						neutralWhiteOut = 10f,
						neutralWhiteLevel = 5.3f,
						neutralWhiteClip = 10f
					};
				}
			}

			// Token: 0x0400139B RID: 5019
			[Tooltip("Tonemapping algorithm to use at the end of the color grading process. Use \"Neutral\" if you need a customizable tonemapper or \"Filmic\" to give a standard filmic look to your scenes.")]
			public ColorGradingModel.Tonemapper tonemapper;

			// Token: 0x0400139C RID: 5020
			[Range(-0.1f, 0.1f)]
			public float neutralBlackIn;

			// Token: 0x0400139D RID: 5021
			[Range(1f, 20f)]
			public float neutralWhiteIn;

			// Token: 0x0400139E RID: 5022
			[Range(-0.09f, 0.1f)]
			public float neutralBlackOut;

			// Token: 0x0400139F RID: 5023
			[Range(1f, 19f)]
			public float neutralWhiteOut;

			// Token: 0x040013A0 RID: 5024
			[Range(0.1f, 20f)]
			public float neutralWhiteLevel;

			// Token: 0x040013A1 RID: 5025
			[Range(1f, 10f)]
			public float neutralWhiteClip;
		}

		// Token: 0x020002E1 RID: 737
		[Serializable]
		public struct BasicSettings
		{
			// Token: 0x17000342 RID: 834
			// (get) Token: 0x06001638 RID: 5688 RVA: 0x000AC390 File Offset: 0x000AA790
			public static ColorGradingModel.BasicSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.BasicSettings
					{
						postExposure = 0f,
						temperature = 0f,
						tint = 0f,
						hueShift = 0f,
						saturation = 1f,
						contrast = 1f
					};
				}
			}

			// Token: 0x040013A2 RID: 5026
			[Tooltip("Adjusts the overall exposure of the scene in EV units. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
			public float postExposure;

			// Token: 0x040013A3 RID: 5027
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to a custom color temperature.")]
			public float temperature;

			// Token: 0x040013A4 RID: 5028
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
			public float tint;

			// Token: 0x040013A5 RID: 5029
			[Range(-180f, 180f)]
			[Tooltip("Shift the hue of all colors.")]
			public float hueShift;

			// Token: 0x040013A6 RID: 5030
			[Range(0f, 2f)]
			[Tooltip("Pushes the intensity of all colors.")]
			public float saturation;

			// Token: 0x040013A7 RID: 5031
			[Range(0f, 2f)]
			[Tooltip("Expands or shrinks the overall range of tonal values.")]
			public float contrast;
		}

		// Token: 0x020002E2 RID: 738
		[Serializable]
		public struct ChannelMixerSettings
		{
			// Token: 0x17000343 RID: 835
			// (get) Token: 0x06001639 RID: 5689 RVA: 0x000AC3F0 File Offset: 0x000AA7F0
			public static ColorGradingModel.ChannelMixerSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ChannelMixerSettings
					{
						red = new Vector3(1f, 0f, 0f),
						green = new Vector3(0f, 1f, 0f),
						blue = new Vector3(0f, 0f, 1f),
						currentEditingChannel = 0
					};
				}
			}

			// Token: 0x040013A8 RID: 5032
			public Vector3 red;

			// Token: 0x040013A9 RID: 5033
			public Vector3 green;

			// Token: 0x040013AA RID: 5034
			public Vector3 blue;

			// Token: 0x040013AB RID: 5035
			[HideInInspector]
			public int currentEditingChannel;
		}

		// Token: 0x020002E3 RID: 739
		[Serializable]
		public struct LogWheelsSettings
		{
			// Token: 0x17000344 RID: 836
			// (get) Token: 0x0600163A RID: 5690 RVA: 0x000AC460 File Offset: 0x000AA860
			public static ColorGradingModel.LogWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LogWheelsSettings
					{
						slope = Color.clear,
						power = Color.clear,
						offset = Color.clear
					};
				}
			}

			// Token: 0x040013AC RID: 5036
			[Trackball("GetSlopeValue")]
			public Color slope;

			// Token: 0x040013AD RID: 5037
			[Trackball("GetPowerValue")]
			public Color power;

			// Token: 0x040013AE RID: 5038
			[Trackball("GetOffsetValue")]
			public Color offset;
		}

		// Token: 0x020002E4 RID: 740
		[Serializable]
		public struct LinearWheelsSettings
		{
			// Token: 0x17000345 RID: 837
			// (get) Token: 0x0600163B RID: 5691 RVA: 0x000AC49C File Offset: 0x000AA89C
			public static ColorGradingModel.LinearWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LinearWheelsSettings
					{
						lift = Color.clear,
						gamma = Color.clear,
						gain = Color.clear
					};
				}
			}

			// Token: 0x040013AF RID: 5039
			[Trackball("GetLiftValue")]
			public Color lift;

			// Token: 0x040013B0 RID: 5040
			[Trackball("GetGammaValue")]
			public Color gamma;

			// Token: 0x040013B1 RID: 5041
			[Trackball("GetGainValue")]
			public Color gain;
		}

		// Token: 0x020002E5 RID: 741
		public enum ColorWheelMode
		{
			// Token: 0x040013B3 RID: 5043
			Linear,
			// Token: 0x040013B4 RID: 5044
			Log
		}

		// Token: 0x020002E6 RID: 742
		[Serializable]
		public struct ColorWheelsSettings
		{
			// Token: 0x17000346 RID: 838
			// (get) Token: 0x0600163C RID: 5692 RVA: 0x000AC4D8 File Offset: 0x000AA8D8
			public static ColorGradingModel.ColorWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ColorWheelsSettings
					{
						mode = ColorGradingModel.ColorWheelMode.Log,
						log = ColorGradingModel.LogWheelsSettings.defaultSettings,
						linear = ColorGradingModel.LinearWheelsSettings.defaultSettings
					};
				}
			}

			// Token: 0x040013B5 RID: 5045
			public ColorGradingModel.ColorWheelMode mode;

			// Token: 0x040013B6 RID: 5046
			[TrackballGroup]
			public ColorGradingModel.LogWheelsSettings log;

			// Token: 0x040013B7 RID: 5047
			[TrackballGroup]
			public ColorGradingModel.LinearWheelsSettings linear;
		}

		// Token: 0x020002E7 RID: 743
		[Serializable]
		public struct CurvesSettings
		{
			// Token: 0x17000347 RID: 839
			// (get) Token: 0x0600163D RID: 5693 RVA: 0x000AC510 File Offset: 0x000AA910
			public static ColorGradingModel.CurvesSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.CurvesSettings
					{
						master = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						red = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						green = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						blue = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						hueVShue = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						hueVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						satVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						lumVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						e_CurrentEditingCurve = 0,
						e_CurveY = true,
						e_CurveR = false,
						e_CurveG = false,
						e_CurveB = false
					};
				}
			}

			// Token: 0x040013B8 RID: 5048
			public ColorGradingCurve master;

			// Token: 0x040013B9 RID: 5049
			public ColorGradingCurve red;

			// Token: 0x040013BA RID: 5050
			public ColorGradingCurve green;

			// Token: 0x040013BB RID: 5051
			public ColorGradingCurve blue;

			// Token: 0x040013BC RID: 5052
			public ColorGradingCurve hueVShue;

			// Token: 0x040013BD RID: 5053
			public ColorGradingCurve hueVSsat;

			// Token: 0x040013BE RID: 5054
			public ColorGradingCurve satVSsat;

			// Token: 0x040013BF RID: 5055
			public ColorGradingCurve lumVSsat;

			// Token: 0x040013C0 RID: 5056
			[HideInInspector]
			public int e_CurrentEditingCurve;

			// Token: 0x040013C1 RID: 5057
			[HideInInspector]
			public bool e_CurveY;

			// Token: 0x040013C2 RID: 5058
			[HideInInspector]
			public bool e_CurveR;

			// Token: 0x040013C3 RID: 5059
			[HideInInspector]
			public bool e_CurveG;

			// Token: 0x040013C4 RID: 5060
			[HideInInspector]
			public bool e_CurveB;
		}

		// Token: 0x020002E8 RID: 744
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000348 RID: 840
			// (get) Token: 0x0600163E RID: 5694 RVA: 0x000AC7C0 File Offset: 0x000AABC0
			public static ColorGradingModel.Settings defaultSettings
			{
				get
				{
					return new ColorGradingModel.Settings
					{
						tonemapping = ColorGradingModel.TonemappingSettings.defaultSettings,
						basic = ColorGradingModel.BasicSettings.defaultSettings,
						channelMixer = ColorGradingModel.ChannelMixerSettings.defaultSettings,
						colorWheels = ColorGradingModel.ColorWheelsSettings.defaultSettings,
						curves = ColorGradingModel.CurvesSettings.defaultSettings
					};
				}
			}

			// Token: 0x040013C5 RID: 5061
			public ColorGradingModel.TonemappingSettings tonemapping;

			// Token: 0x040013C6 RID: 5062
			public ColorGradingModel.BasicSettings basic;

			// Token: 0x040013C7 RID: 5063
			public ColorGradingModel.ChannelMixerSettings channelMixer;

			// Token: 0x040013C8 RID: 5064
			public ColorGradingModel.ColorWheelsSettings colorWheels;

			// Token: 0x040013C9 RID: 5065
			public ColorGradingModel.CurvesSettings curves;
		}
	}
}
