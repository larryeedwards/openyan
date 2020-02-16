using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002C4 RID: 708
	public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x000AB8CC File Offset: 0x000A9CCC
		public override bool active
		{
			get
			{
				UserLutModel.Settings settings = base.model.settings;
				return base.model.enabled && settings.lut != null && settings.contribution > 0f && settings.lut.height == (int)Mathf.Sqrt((float)settings.lut.width) && !this.context.interrupted;
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000AB950 File Offset: 0x000A9D50
		public override void Prepare(Material uberMaterial)
		{
			UserLutModel.Settings settings = base.model.settings;
			uberMaterial.EnableKeyword("USER_LUT");
			uberMaterial.SetTexture(UserLutComponent.Uniforms._UserLut, settings.lut);
			uberMaterial.SetVector(UserLutComponent.Uniforms._UserLut_Params, new Vector4(1f / (float)settings.lut.width, 1f / (float)settings.lut.height, (float)settings.lut.height - 1f, settings.contribution));
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000AB9D8 File Offset: 0x000A9DD8
		public void OnGUI()
		{
			UserLutModel.Settings settings = base.model.settings;
			Rect position = new Rect(this.context.viewport.x * (float)Screen.width + 8f, 8f, (float)settings.lut.width, (float)settings.lut.height);
			GUI.DrawTexture(position, settings.lut);
		}

		// Token: 0x020002C5 RID: 709
		private static class Uniforms
		{
			// Token: 0x04001342 RID: 4930
			internal static readonly int _UserLut = Shader.PropertyToID("_UserLut");

			// Token: 0x04001343 RID: 4931
			internal static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");
		}
	}
}
