using System;
using UnityEngine;

// Token: 0x020003C6 RID: 966
[Serializable]
public class TimeOfDayEventTime : IScheduledEventTime
{
	// Token: 0x06001974 RID: 6516 RVA: 0x000ED868 File Offset: 0x000EBC68
	public TimeOfDayEventTime(int week, DayOfWeek weekday, TimeOfDay timeOfDay)
	{
		this.week = week;
		this.weekday = weekday;
		this.timeOfDay = timeOfDay;
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06001975 RID: 6517 RVA: 0x000ED885 File Offset: 0x000EBC85
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.TimeOfDay;
		}
	}

	// Token: 0x06001976 RID: 6518 RVA: 0x000ED888 File Offset: 0x000EBC88
	public bool OccurringNow(DateAndTime currentTime)
	{
		bool flag = currentTime.Week == this.week;
		bool flag2 = currentTime.Weekday == this.weekday;
		bool flag3 = currentTime.Clock.TimeOfDay == this.timeOfDay;
		return flag && flag2 && flag3;
	}

	// Token: 0x06001977 RID: 6519 RVA: 0x000ED8D8 File Offset: 0x000EBCD8
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week < this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TimeOfDay < this.timeOfDay;
		}
		return currentTime.Weekday < this.weekday;
	}

	// Token: 0x06001978 RID: 6520 RVA: 0x000ED938 File Offset: 0x000EBD38
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week > this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TimeOfDay > this.timeOfDay;
		}
		return currentTime.Weekday > this.weekday;
	}

	// Token: 0x04001E36 RID: 7734
	[SerializeField]
	private int week;

	// Token: 0x04001E37 RID: 7735
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04001E38 RID: 7736
	[SerializeField]
	private TimeOfDay timeOfDay;
}
