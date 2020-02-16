using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class MGPMExplosionScript : MonoBehaviour
{
	// Token: 0x06000BC5 RID: 3013 RVA: 0x000595C4 File Offset: 0x000579C4
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

	// Token: 0x040008E9 RID: 2281
	public Renderer MyRenderer;

	// Token: 0x040008EA RID: 2282
	public Texture[] Sprite;

	// Token: 0x040008EB RID: 2283
	public float Timer;

	// Token: 0x040008EC RID: 2284
	public float FPS;

	// Token: 0x040008ED RID: 2285
	public int Frame;
}
