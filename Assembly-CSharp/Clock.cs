using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200056B RID: 1387
[Serializable]
public class Clock
{
	// Token: 0x060021F5 RID: 8693 RVA: 0x0019B005 File Offset: 0x00199405
	public Clock(int hours, int minutes, int seconds, float currentSecond)
	{
		this.hours = hours;
		this.minutes = minutes;
		this.seconds = seconds;
		this.currentSecond = currentSecond;
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x0019B02A File Offset: 0x0019942A
	public Clock(int hours, int minutes, int seconds) : this(hours, minutes, seconds, 0f)
	{
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x0019B03A File Offset: 0x0019943A
	public Clock() : this(0, 0, 0, 0f)
	{
	}

	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0019B04A File Offset: 0x0019944A
	public int Hours24
	{
		get
		{
			return this.hours;
		}
	}

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x060021F9 RID: 8697 RVA: 0x0019B054 File Offset: 0x00199454
	public int Hours12
	{
		get
		{
			int num = this.hours % 12;
			return (num != 0) ? num : 12;
		}
	}

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x060021FA RID: 8698 RVA: 0x0019B079 File Offset: 0x00199479
	public int Minutes
	{
		get
		{
			return this.minutes;
		}
	}

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x060021FB RID: 8699 RVA: 0x0019B081 File Offset: 0x00199481
	public int Seconds
	{
		get
		{
			return this.seconds;
		}
	}

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x060021FC RID: 8700 RVA: 0x0019B089 File Offset: 0x00199489
	public float CurrentSecond
	{
		get
		{
			return this.currentSecond;
		}
	}

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x060021FD RID: 8701 RVA: 0x0019B091 File Offset: 0x00199491
	public int TotalSeconds
	{
		get
		{
			return this.hours * 3600 + this.minutes * 60 + this.seconds;
		}
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x060021FE RID: 8702 RVA: 0x0019B0B0 File Offset: 0x001994B0
	public float PreciseTotalSeconds
	{
		get
		{
			return (float)this.TotalSeconds + this.currentSecond;
		}
	}

	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x060021FF RID: 8703 RVA: 0x0019B0C0 File Offset: 0x001994C0
	public bool IsAM
	{
		get
		{
			return this.hours < 12;
		}
	}

	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x06002200 RID: 8704 RVA: 0x0019B0CC File Offset: 0x001994CC
	public TimeOfDay TimeOfDay
	{
		get
		{
			if (this.hours < 3)
			{
				return TimeOfDay.Midnight;
			}
			if (this.hours < 6)
			{
				return TimeOfDay.EarlyMorning;
			}
			if (this.hours < 9)
			{
				return TimeOfDay.Morning;
			}
			if (this.hours < 12)
			{
				return TimeOfDay.LateMorning;
			}
			if (this.hours < 15)
			{
				return TimeOfDay.Noon;
			}
			if (this.hours < 18)
			{
				return TimeOfDay.Afternoon;
			}
			if (this.hours < 21)
			{
				return TimeOfDay.Evening;
			}
			return TimeOfDay.Night;
		}
	}

	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x06002201 RID: 8705 RVA: 0x0019B141 File Offset: 0x00199541
	public string TimeOfDayString
	{
		get
		{
			return Clock.TimeOfDayStrings[this.TimeOfDay];
		}
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x0019B153 File Offset: 0x00199553
	public bool IsBefore(Clock clock)
	{
		return this.TotalSeconds < clock.TotalSeconds;
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x0019B163 File Offset: 0x00199563
	public bool IsAfter(Clock clock)
	{
		return this.TotalSeconds > clock.TotalSeconds;
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x0019B173 File Offset: 0x00199573
	public void IncrementHour()
	{
		this.hours++;
		if (this.hours == 24)
		{
			this.hours = 0;
		}
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x0019B197 File Offset: 0x00199597
	public void IncrementMinute()
	{
		this.minutes++;
		if (this.minutes == 60)
		{
			this.IncrementHour();
			this.minutes = 0;
		}
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x0019B1C1 File Offset: 0x001995C1
	public void IncrementSecond()
	{
		this.seconds++;
		if (this.seconds == 60)
		{
			this.IncrementMinute();
			this.seconds = 0;
		}
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x0019B1EB File Offset: 0x001995EB
	public void Tick(float dt)
	{
		this.currentSecond += dt;
		while (this.currentSecond >= 1f)
		{
			this.IncrementSecond();
			this.currentSecond -= 1f;
		}
	}

	// Token: 0x04003763 RID: 14179
	[SerializeField]
	private int hours;

	// Token: 0x04003764 RID: 14180
	[SerializeField]
	private int minutes;

	// Token: 0x04003765 RID: 14181
	[SerializeField]
	private int seconds;

	// Token: 0x04003766 RID: 14182
	[SerializeField]
	private float currentSecond;

	// Token: 0x04003767 RID: 14183
	private static readonly Dictionary<TimeOfDay, string> TimeOfDayStrings = new Dictionary<TimeOfDay, string>
	{
		{
			TimeOfDay.Midnight,
			"Midnight"
		},
		{
			TimeOfDay.EarlyMorning,
			"Early Morning"
		},
		{
			TimeOfDay.Morning,
			"Morning"
		},
		{
			TimeOfDay.LateMorning,
			"Late Morning"
		},
		{
			TimeOfDay.Noon,
			"Noon"
		},
		{
			TimeOfDay.Afternoon,
			"Afternoon"
		},
		{
			TimeOfDay.Evening,
			"Evening"
		},
		{
			TimeOfDay.Night,
			"Night"
		}
	};
}
