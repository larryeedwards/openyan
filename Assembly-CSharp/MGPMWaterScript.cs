using System;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class MGPMWaterScript : MonoBehaviour
{
	// Token: 0x06000BE0 RID: 3040 RVA: 0x0005BC4C File Offset: 0x0005A04C
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > this.FPS)
		{
			this.Timer = 0f;
			this.Frame++;
			if (this.Frame == this.Sprite.Length)
			{
				this.Frame = 0;
			}
			this.MyRenderer.material.mainTexture = this.Sprite[this.Frame];
		}
		base.transform.localPosition = new Vector3(0f, base.transform.localPosition.y - this.Speed * Time.deltaTime, 3f);
		if (base.transform.localPosition.y < -640f)
		{
			base.transform.localPosition = new Vector3(0f, base.transform.localPosition.y + 1280f, 3f);
		}
	}

	// Token: 0x04000959 RID: 2393
	public Renderer MyRenderer;

	// Token: 0x0400095A RID: 2394
	public Texture[] Sprite;

	// Token: 0x0400095B RID: 2395
	public float Speed;

	// Token: 0x0400095C RID: 2396
	public float Timer;

	// Token: 0x0400095D RID: 2397
	public float FPS;

	// Token: 0x0400095E RID: 2398
	public int Frame;
}
