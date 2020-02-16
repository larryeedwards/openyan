using System;
using UnityEngine;

// Token: 0x020003C5 RID: 965
[Serializable]
public class SpecificEventTime : IScheduledEventTime
{
	// Token: 0x0600196F RID: 6511 RVA: 0x000ED6FD File Offset: 0x000EBAFD
	public SpecificEventTime(int week, DayOfWeek weekday, Clock startClock, Clock endClock)
	{
		this.week = week;
		this.weekday = weekday;
		this.startClock = startClock;
		this.endClock = endClock;
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06001970 RID: 6512 RVA: 0x000ED722 File Offset: 0x000EBB22
	public ScheduledEventTimeType ScheduleType
	{
		get
		{
			return ScheduledEventTimeType.Specific;
		}
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x000ED728 File Offset: 0x000EBB28
	public bool OccurringNow(DateAndTime currentTime)
	{
		bool flag = currentTime.Week == this.week;
		bool flag2 = currentTime.Weekday == this.weekday;
		Clock clock = currentTime.Clock;
		bool flag3 = clock.TotalSeconds >= this.startClock.TotalSeconds && clock.TotalSeconds < this.endClock.TotalSeconds;
		return flag && flag2 && flag3;
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x000ED798 File Offset: 0x000EBB98
	public bool OccursInTheFuture(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week < this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TotalSeconds < this.startClock.TotalSeconds;
		}
		return currentTime.Weekday < this.weekday;
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x000ED800 File Offset: 0x000EBC00
	public bool OccurredInThePast(DateAndTime currentTime)
	{
		if (currentTime.Week != this.week)
		{
			return currentTime.Week > this.week;
		}
		if (currentTime.Weekday == this.weekday)
		{
			return currentTime.Clock.TotalSeconds >= this.endClock.TotalSeconds;
		}
		return currentTime.Weekday > this.weekday;
	}

	// Token: 0x04001E32 RID: 7730
	[SerializeField]
	private int week;

	// Token: 0x04001E33 RID: 7731
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x04001E34 RID: 7732
	[SerializeField]
	private Clock startClock;

	// Token: 0x04001E35 RID: 7733
	[SerializeField]
	private Clock endClock;
}
