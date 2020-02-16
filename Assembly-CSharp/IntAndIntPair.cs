using System;

// Token: 0x02000584 RID: 1412
[Serializable]
public class IntAndIntPair : SerializablePair<int, int>
{
	// Token: 0x0600223B RID: 8763 RVA: 0x0019BA11 File Offset: 0x00199E11
	public IntAndIntPair(int first, int second) : base(first, second)
	{
	}

	// Token: 0x0600223C RID: 8764 RVA: 0x0019BA1B File Offset: 0x00199E1B
	public IntAndIntPair() : base(0, 0)
	{
	}
}
