using System;

// Token: 0x02000583 RID: 1411
public class SerializablePair<T, U>
{
	// Token: 0x06002239 RID: 8761 RVA: 0x0019B9D4 File Offset: 0x00199DD4
	public SerializablePair(T first, U second)
	{
		this.first = first;
		this.second = second;
	}

	// Token: 0x0600223A RID: 8762 RVA: 0x0019B9EC File Offset: 0x00199DEC
	public SerializablePair() : this(default(T), default(U))
	{
	}

	// Token: 0x04003775 RID: 14197
	public T first;

	// Token: 0x04003776 RID: 14198
	public U second;
}
