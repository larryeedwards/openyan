using System;
using UnityEngine;

// Token: 0x02000348 RID: 840
[Serializable]
public class BucketWeights : BucketContents
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001784 RID: 6020 RVA: 0x000B9148 File Offset: 0x000B7548
	// (set) Token: 0x06001785 RID: 6021 RVA: 0x000B9150 File Offset: 0x000B7550
	public int Count
	{
		get
		{
			return this.count;
		}
		set
		{
			this.count = ((value >= 0) ? value : 0);
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001786 RID: 6022 RVA: 0x000B9166 File Offset: 0x000B7566
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Weights;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001787 RID: 6023 RVA: 0x000B9169 File Offset: 0x000B7569
	public override bool IsCleaningAgent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06001788 RID: 6024 RVA: 0x000B916C File Offset: 0x000B756C
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x000B916F File Offset: 0x000B756F
	public override bool CanBeLifted(int strength)
	{
		return strength > 0;
	}

	// Token: 0x04001738 RID: 5944
	[SerializeField]
	private int count;
}
