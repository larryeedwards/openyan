using System;
using UnityEngine;

// Token: 0x02000549 RID: 1353
public class TextureCycleScript : MonoBehaviour
{
	// Token: 0x06002180 RID: 8576 RVA: 0x001951E1 File Offset: 0x001935E1
	private void Awake()
	{
	}

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x06002181 RID: 8577 RVA: 0x001951E3 File Offset: 0x001935E3
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x001951F4 File Offset: 0x001935F4
	private void Update()
	{
		this.ID++;
		if (this.ID > 1)
		{
			this.ID = 0;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.Sprite.mainTexture = this.Textures[this.Frame];
	}

	// Token: 0x0400360E RID: 13838
	public UITexture Sprite;

	// Token: 0x0400360F RID: 13839
	[SerializeField]
	private int Start;

	// Token: 0x04003610 RID: 13840
	[SerializeField]
	private int Frame;

	// Token: 0x04003611 RID: 13841
	[SerializeField]
	private int Limit;

	// Token: 0x04003612 RID: 13842
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x04003613 RID: 13843
	[SerializeField]
	private float CurrentSeconds;

	// Token: 0x04003614 RID: 13844
	[SerializeField]
	private Texture[] Textures;

	// Token: 0x04003615 RID: 13845
	public int ID;
}
