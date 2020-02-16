using System;
using UnityEngine;

// Token: 0x0200035C RID: 860
public class ChangeTextureScript : MonoBehaviour
{
	// Token: 0x060017C0 RID: 6080 RVA: 0x000BD510 File Offset: 0x000BB910
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID == this.Textures.Length)
			{
				this.ID = 1;
			}
			this.MyRenderer.material.mainTexture = this.Textures[this.ID];
		}
	}

	// Token: 0x040017C3 RID: 6083
	public Renderer MyRenderer;

	// Token: 0x040017C4 RID: 6084
	public Texture[] Textures;

	// Token: 0x040017C5 RID: 6085
	public int ID = 1;
}
