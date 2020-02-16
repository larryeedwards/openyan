using System;
using UnityEngine;

// Token: 0x0200056C RID: 1388
[Serializable]
public class DateAndTime
{
	// Token: 0x06002209 RID: 8713 RVA: 0x0019B2A1 File Offset: 0x001996A1
	public DateAndTime(int week, DayOfWeek weekday, Clock clock)
	{
		this.week = week;
		this.weekday = weekday;
		this.clock = clock;
	}

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x0600220A RID: 8714 RVA: 0x0019B2BE File Offset: 0x001996BE
	public int Week
	{
		get
		{
			return this.week;
		}
	}

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x0600220B RID: 8715 RVA: 0x0019B2C6 File Offset: 0x001996C6
	public DayOfWeek Weekday
	{
		get
		{
			return this.weekday;
		}
	}

	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x0600220C RID: 8716 RVA: 0x0019B2CE File Offset: 0x001996CE
	public Clock Clock
	{
		get
		{
			return this.clock;
		}
	}

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x0600220D RID: 8717 RVA: 0x0019B2D8 File Offset: 0x001996D8
	public int TotalSeconds
	{
		get
		{
			int num = this.week * 604800;
			int num2 = (int)(this.weekday * (DayOfWeek)86400);
			int totalSeconds = this.clock.TotalSeconds;
			return num + num2 + totalSeconds;
		}
	}

	// Token: 0x0600220E RID: 8718 RVA: 0x0019B310 File Offset: 0x00199710
	public void IncrementWeek()
	{
		this.week++;
	}

	// Token: 0x0600220F RID: 8719 RVA: 0x0019B320 File Offset: 0x00199720
	public void IncrementWeekday()
	{
		int num = (int)this.weekday;
		num++;
		if (num == 7)
		{
			this.IncrementWeek();
			num = 0;
		}
		this.weekday = (DayOfWeek)num;
	}

	// Token: 0x06002210 RID: 8720 RVA: 0x0019B350 File Offset: 0x00199750
	public void Tick(float dt)
	{
		int hours = this.clock.Hours24;
		this.clock.Tick(dt);
		int hours2 = this.clock.Hours24;
		if (hours2 < hours)
		{
			this.IncrementWeekday();
		}
	}

	// Token: 0x04003768 RID: 14184
	[SerializeField]
	private int week;

	// Token: 0x04003769 RID: 14185
	[SerializeField]
	private DayOfWeek weekday;

	// Token: 0x0400376A RID: 14186
	[SerializeField]
	private Clock clock;
}
