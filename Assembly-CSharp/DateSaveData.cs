using System;

// Token: 0x020004E1 RID: 1249
[Serializable]
public class DateSaveData
{
	// Token: 0x06001F71 RID: 8049 RVA: 0x001412A0 File Offset: 0x0013F6A0
	public static DateSaveData ReadFromGlobals()
	{
		return new DateSaveData
		{
			week = DateGlobals.Week,
			weekday = DateGlobals.Weekday
		};
	}

	// Token: 0x06001F72 RID: 8050 RVA: 0x001412CA File Offset: 0x0013F6CA
	public static void WriteToGlobals(DateSaveData data)
	{
		DateGlobals.Week = data.week;
		DateGlobals.Weekday = data.weekday;
	}

	// Token: 0x04002AA7 RID: 10919
	public int week;

	// Token: 0x04002AA8 RID: 10920
	public DayOfWeek weekday;
}
