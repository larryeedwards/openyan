using System;

// Token: 0x02000347 RID: 839
[Serializable]
public class BucketGas : BucketContents
{
	// Token: 0x17000379 RID: 889
	// (get) Token: 0x0600177F RID: 6015 RVA: 0x000B9134 File Offset: 0x000B7534
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Gas;
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06001780 RID: 6016 RVA: 0x000B9137 File Offset: 0x000B7537
	public override bool IsCleaningAgent
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06001781 RID: 6017 RVA: 0x000B913A File Offset: 0x000B753A
	public override bool IsFlammable
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x000B913D File Offset: 0x000B753D
	public override bool CanBeLifted(int strength)
	{
		return true;
	}
}
