using System;
using UnityEngine;

// Token: 0x0200054A RID: 1354
public class TextureManagerScript : MonoBehaviour
{
	// Token: 0x06002184 RID: 8580 RVA: 0x00195270 File Offset: 0x00193670
	public Texture2D MergeTextures(Texture2D BackgroundTex, Texture2D TopTex)
	{
		Texture2D texture2D = new Texture2D(1024, 1024);
		Color32[] pixels = BackgroundTex.GetPixels32();
		Color32[] pixels2 = TopTex.GetPixels32();
		for (int i = 0; i < pixels2.Length; i++)
		{
			if (pixels2[i].a != 0)
			{
				pixels[i] = pixels2[i];
			}
		}
		texture2D.SetPixels32(pixels);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x04003616 RID: 13846
	public Texture[] UniformTextures;

	// Token: 0x04003617 RID: 13847
	public Texture[] CasualTextures;

	// Token: 0x04003618 RID: 13848
	public Texture[] SocksTextures;

	// Token: 0x04003619 RID: 13849
	public Texture2D PurpleStockings;

	// Token: 0x0400361A RID: 13850
	public Texture2D GreenStockings;

	// Token: 0x0400361B RID: 13851
	public Texture2D Base2D;

	// Token: 0x0400361C RID: 13852
	public Texture2D Overlay2D;
}
