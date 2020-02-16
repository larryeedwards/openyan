using System;
using UnityEngine;

// Token: 0x0200056E RID: 1390
[Serializable]
public class RangeInt
{
	// Token: 0x06002213 RID: 8723 RVA: 0x0019B478 File Offset: 0x00199878
	public RangeInt(int value, int min, int max)
	{
		this.value = value;
		this.min = min;
		this.max = max;
	}

	// Token: 0x06002214 RID: 8724 RVA: 0x0019B495 File Offset: 0x00199895
	public RangeInt(int min, int max) : this(min, min, max)
	{
	}

	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x06002215 RID: 8725 RVA: 0x0019B4A0 File Offset: 0x001998A0
	// (set) Token: 0x06002216 RID: 8726 RVA: 0x0019B4A8 File Offset: 0x001998A8
	public int Value
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x06002217 RID: 8727 RVA: 0x0019B4B1 File Offset: 0x001998B1
	public int Min
	{
		get
		{
			return this.min;
		}
	}

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x06002218 RID: 8728 RVA: 0x0019B4B9 File Offset: 0x001998B9
	public int Max
	{
		get
		{
			return this.max;
		}
	}

	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x06002219 RID: 8729 RVA: 0x0019B4C1 File Offset: 0x001998C1
	public int Next
	{
		get
		{
			return (this.value != this.max) ? (this.value + 1) : this.min;
		}
	}

	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x0600221A RID: 8730 RVA: 0x0019B4E7 File Offset: 0x001998E7
	public int Previous
	{
		get
		{
			return (this.value != this.min) ? (this.value - 1) : this.max;
		}
	}

	// Token: 0x0400376B RID: 14187
	[SerializeField]
	private int value;

	// Token: 0x0400376C RID: 14188
	[SerializeField]
	private int min;

	// Token: 0x0400376D RID: 14189
	[SerializeField]
	private int max;
}
