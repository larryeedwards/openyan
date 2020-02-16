using System;
using UnityEngine;

// Token: 0x020003C8 RID: 968
[Serializable]
public class WeekEventTime : IScheduledEventTime
{
	// Token: 0x0600197E RID: 6526 RVA: 0x000EDA35 File Offset: 0x000EBE35
	public WeekEventTime(int week)
	{
		this.week = week;
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x0600197F RID: 6527 RVA: 0x000EDA44 File Offset: 0x000EBE44
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Week;
		}
	}

	// Token: 0x06001980 RID: 6528 RVA: 0x000EDA47 File Offset: 0x000EBE47
	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week;
	}

	// Token: 0x06001981 RID: 6529 RVA: 0x000EDA57 File Offset: 0x000EBE57
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		return currentTime.Week < this.week;
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x000EDA67 File Offset: 0x000EBE67
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		return currentTime.Week > this.week;
	}

	// Token: 0x04001E3B RID: 7739
	[SerializeField]
	private int week;
}
