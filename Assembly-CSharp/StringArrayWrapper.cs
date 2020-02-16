using System;

// Token: 0x02000566 RID: 1382
[Serializable]
public class StringArrayWrapper : ArrayWrapper<string>
{
	// Token: 0x060021E9 RID: 8681 RVA: 0x0019AC1F File Offset: 0x0019901F
	public StringArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x0019AC28 File Offset: 0x00199028
	public StringArrayWrapper(string[] elements) : base(elements)
	{
	}
}
