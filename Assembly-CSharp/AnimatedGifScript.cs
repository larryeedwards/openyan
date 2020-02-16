using System;
using UnityEngine;

// Token: 0x0200031D RID: 797
public class AnimatedGifScript : MonoBehaviour
{
	// Token: 0x060016EE RID: 5870 RVA: 0x000B15D7 File Offset: 0x000AF9D7
	private void Awake()
	{
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x060016EF RID: 5871 RVA: 0x000B15D9 File Offset: 0x000AF9D9
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000B15E8 File Offset: 0x000AF9E8
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
		this.Sprite.spriteName = this.SpriteName + this.Frame.ToString();
	}

	// Token: 0x040014CE RID: 5326
	[SerializeField]
	private UISprite Sprite;

	// Token: 0x040014CF RID: 5327
	[SerializeField]
	private string SpriteName;

	// Token: 0x040014D0 RID: 5328
	[SerializeField]
	private int Start;

	// Token: 0x040014D1 RID: 5329
	[SerializeField]
	private int Frame;

	// Token: 0x040014D2 RID: 5330
	[SerializeField]
	private int Limit;

	// Token: 0x040014D3 RID: 5331
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x040014D4 RID: 5332
	[SerializeField]
	private float CurrentSeconds;
}
