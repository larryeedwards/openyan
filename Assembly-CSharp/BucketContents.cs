using System;

// Token: 0x02000345 RID: 837
public abstract class BucketContents
{
	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06001771 RID: 6001
	public abstract BucketContentsType Type { get; }

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06001772 RID: 6002
	public abstract bool IsCleaningAgent { get; }

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06001773 RID: 6003
	public abstract bool IsFlammable { get; }

	// Token: 0x06001774 RID: 6004
	public abstract bool CanBeLifted(int strength);
}
