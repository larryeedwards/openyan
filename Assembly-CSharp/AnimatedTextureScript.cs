using System;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class AnimatedTextureScript : MonoBehaviour
{
	// Token: 0x060016F2 RID: 5874 RVA: 0x000B168A File Offset: 0x000AFA8A
	private void Awake()
	{
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x060016F3 RID: 5875 RVA: 0x000B168C File Offset: 0x000AFA8C
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x000B169C File Offset: 0x000AFA9C
	private void Update()
	{
		this.CurrentSeconds += Time.unscaledDeltaTime;
		while (this.CurrentSeconds >= this.SecondsPerFrame)
		{
			this.CurrentSeconds -= this.SecondsPerFrame;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.MyRenderer.material.mainTexture = this.Image[this.Frame];
	}

	// Token: 0x040014D5 RID: 5333
	[SerializeField]
	private Renderer MyRenderer;

	// Token: 0x040014D6 RID: 5334
	[SerializeField]
	private int Start;

	// Token: 0x040014D7 RID: 5335
	[SerializeField]
	private int Frame;

	// Token: 0x040014D8 RID: 5336
	[SerializeField]
	private int Limit;

	// Token: 0x040014D9 RID: 5337
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x040014DA RID: 5338
	[SerializeField]
	private float CurrentSeconds;

	// Token: 0x040014DB RID: 5339
	public Texture[] Image;
}
