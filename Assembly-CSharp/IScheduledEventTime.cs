using System;

// Token: 0x020003C4 RID: 964
public interface IScheduledEventTime
{
	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x0600196B RID: 6507
	ScheduledEventTimeType ScheduleType { get; }

	// Token: 0x0600196C RID: 6508
	bool OccurringNow(DateAndTime currentTime);

	// Token: 0x0600196D RID: 6509
	bool OccursInTheFuture(DateAndTime currentTime);

	// Token: 0x0600196E RID: 6510
	bool OccurredInThePast(DateAndTime currentTime);
}
