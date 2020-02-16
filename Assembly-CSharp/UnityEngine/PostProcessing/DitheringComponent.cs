using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020002AF RID: 687
	public sealed class DitheringComponent : PostProcessingComponentRenderTexture<DitheringModel>
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000A8D0A File Offset: 0x000A710A
		public override bool active
		{
			get
			{
				return base.model.enabled && !this.context.interrupted;
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000A8D2D File Offset: 0x000A712D
		public override void OnDisable()
		{
			this.noiseTextures = null;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000A8D38 File Offset: 0x000A7138
		private void LoadNoiseTextures()
		{
			this.noiseTextures = new Texture2D[64];
			for (int i = 0; i < 64; i++)
			{
				this.noiseTextures[i] = Resources.Load<Texture2D>("Bluenoise64/LDR_LLL1_" + i);
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000A8D84 File Offset: 0x000A7184
		public override void Prepare(Material uberMaterial)
		{
			if (++this.textureIndex >= 64)
			{
				this.textureIndex = 0;
			}
			float value = Random.value;
			float value2 = Random.value;
			if (this.noiseTextures == null)
			{
				this.LoadNoiseTextures();
			}
			Texture2D texture2D = this.noiseTextures[this.textureIndex];
			uberMaterial.EnableKeyword("DITHERING");
			uberMaterial.SetTexture(DitheringComponent.Uniforms._DitheringTex, texture2D);
			uberMaterial.SetVector(DitheringComponent.Uniforms._DitheringCoords, new Vector4((float)this.context.width / (float)texture2D.width, (float)this.context.height / (float)texture2D.height, value, value2));
		}

		// Token: 0x040012B0 RID: 4784
		private Texture2D[] noiseTextures;

		// Token: 0x040012B1 RID: 4785
		private int textureIndex;

		// Token: 0x040012B2 RID: 4786
		private const int k_TextureCount = 64;

		// Token: 0x020002B0 RID: 688
		private static class Uniforms
		{
			// Token: 0x040012B3 RID: 4787
			internal static readonly int _DitheringTex = Shader.PropertyToID("_DitheringTex");

			// Token: 0x040012B4 RID: 4788
			internal static readonly int _DitheringCoords = Shader.PropertyToID("_DitheringCoords");
		}
	}
}
