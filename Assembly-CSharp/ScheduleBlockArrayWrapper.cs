using System;

// Token: 0x02000565 RID: 1381
[Serializable]
public class ScheduleBlockArrayWrapper : ArrayWrapper<ScheduleBlock>
{
	// Token: 0x060021E7 RID: 8679 RVA: 0x0019AC0D File Offset: 0x0019900D
	public ScheduleBlockArrayWrapper(int size) : base(size)
	{
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x0019AC16 File Offset: 0x00199016
	public ScheduleBlockArrayWrapper(ScheduleBlock[] elements) : base(elements)
	{
	}
}
