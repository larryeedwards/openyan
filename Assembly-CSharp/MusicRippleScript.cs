using System;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class MusicRippleScript : MonoBehaviour
{
	// Token: 0x06000BF0 RID: 3056 RVA: 0x0005E1D4 File Offset: 0x0005C5D4
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.Timer = 0f;
			this.Frame++;
			if (this.Frame == this.Sprite.Length)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				this.MyRenderer.material.mainTexture = this.Sprite[this.Frame];
			}
		}
	}

	// Token: 0x040009B1 RID: 2481
	public Renderer MyRenderer;

	// Token: 0x040009B2 RID: 2482
	public Texture[] Sprite;

	// Token: 0x040009B3 RID: 2483
	public float Timer;

	// Token: 0x040009B4 RID: 2484
	public float FPS;

	// Token: 0x040009B5 RID: 2485
	public int Frame;
}
