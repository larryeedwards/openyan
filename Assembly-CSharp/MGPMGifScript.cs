using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class MGPMGifScript : MonoBehaviour
{
	// Token: 0x06000BC7 RID: 3015 RVA: 0x00059658 File Offset: 0x00057A58
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.ID++;
			if (this.ID == this.Frames.Length)
			{
				this.ID = 0;
			}
			this.MyRenderer.material.mainTexture = this.Frames[this.ID];
			this.Timer = 0f;
		}
	}

	// Token: 0x040008EE RID: 2286
	public Texture[] Frames;

	// Token: 0x040008EF RID: 2287
	public Renderer MyRenderer;

	// Token: 0x040008F0 RID: 2288
	public float Timer;

	// Token: 0x040008F1 RID: 2289
	public float FPS;

	// Token: 0x040008F2 RID: 2290
	public int ID;
}
