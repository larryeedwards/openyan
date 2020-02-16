using System;
using UnityEngine;

// Token: 0x020003C7 RID: 967
[Serializable]
public class DayEventTime : IScheduledEventTime
{
	// Token: 0x06001979 RID: 6521 RVA: 0x000ED998 File Offset: 0x000EBD98
	public DayEventTime(int week, DayOfWeek weekday)
	{
		this.week = week;
		this.weekday = weekday;
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x0600197A RID: 6522 RVA: 0x000ED9AE File Offset: 0x000EBDAE
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Day;
		}
	}

	// Token: 0x0600197B RID: 6523 RVA: 0x000ED9B1 File Offset: 0x000EBDB1
	public bool OccurringNow(DateAndTime currentTime)
	{
		return currentTime.Week == this.week && currentTime.Weekday == this.weekday;
	}

	// Token: 0x0600197C RID: 6524 RVA: 0x000ED9D5 File Offset: 0x000EBDD5
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday < this.weekday;
		}
		return currentTime.Week < this.week;
	}

	// Token: 0x0600197D RID: 6525 RVA: 0x000EDA05 File Offset: 0x000EBE05
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week == this.week)
		{
			return currentTime.Weekday > this.weekday;
		}
		return currentTime.Week > this.week;
	}

	// Token: 0x04001E39 RID: 7737
	[SerializeField]
	private int week;

	// Token: 0x04001E3A RID: 7738
	[SerializeField]
	private DayOfWeek weekday;
}
