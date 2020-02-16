using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200030A RID: 778
	public class PostProcessingProfile : ScriptableObject
	{
		// Token: 0x0400143C RID: 5180
		public BuiltinDebugViewsModel debugViews = new BuiltinDebugViewsModel();

		// Token: 0x0400143D RID: 5181
		public FogModel fog = new FogModel();

		// Token: 0x0400143E RID: 5182
		public AntialiasingModel antialiasing = new AntialiasingModel();

		// Token: 0x0400143F RID: 5183
		public AmbientOcclusionModel ambientOcclusion = new AmbientOcclusionModel();

		// Token: 0x04001440 RID: 5184
		public ScreenSpaceReflectionModel screenSpaceReflection = new ScreenSpaceReflectionModel();

		// Token: 0x04001441 RID: 5185
		public DepthOfFieldModel depthOfField = new DepthOfFieldModel();

		// Token: 0x04001442 RID: 5186
		public MotionBlurModel motionBlur = new MotionBlurModel();

		// Token: 0x04001443 RID: 5187
		public EyeAdaptationModel eyeAdaptation = new EyeAdaptationModel();

		// Token: 0x04001444 RID: 5188
		public BloomModel bloom = new BloomModel();

		// Token: 0x04001445 RID: 5189
		public ColorGradingModel colorGrading = new ColorGradingModel();

		// Token: 0x04001446 RID: 5190
		public UserLutModel userLut = new UserLutModel();

		// Token: 0x04001447 RID: 5191
		public ChromaticAberrationModel chromaticAberration = new ChromaticAberrationModel();

		// Token: 0x04001448 RID: 5192
		public GrainModel grain = new GrainModel();

		// Token: 0x04001449 RID: 5193
		public VignetteModel vignette = new VignetteModel();

		// Token: 0x0400144A RID: 5194
		public DitheringModel dithering = new DitheringModel();
	}
}
