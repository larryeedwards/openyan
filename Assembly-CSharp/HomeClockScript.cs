using System;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class HomeClockScript : MonoBehaviour
{
	// Token: 0x06001C8E RID: 7310 RVA: 0x00102B2C File Offset: 0x00100F2C
	private void Start()
	{
		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);
		if (HomeGlobals.Night)
		{
			this.HourLabel.text = "8:00 PM";
		}
		else
		{
			this.HourLabel.text = ((!HomeGlobals.LateForSchool) ? "6:30 AM" : "7:30 AM");
		}
		this.UpdateMoneyLabel();
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x00102B98 File Offset: 0x00100F98
	private void Update()
	{
		if (this.ShakeMoney)
		{
			this.Shake = Mathf.MoveTowards(this.Shake, 0f, Time.deltaTime * 10f);
			this.MoneyLabel.transform.localPosition = new Vector3(1020f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 375f + UnityEngine.Random.Range(this.Shake * -1f, this.Shake * 1f), 0f);
			this.G = Mathf.MoveTowards(this.G, 0.75f, Time.deltaTime);
			this.B = Mathf.MoveTowards(this.B, 1f, Time.deltaTime);
			this.MoneyLabel.color = new Color(1f, this.G, this.B, 1f);
			if (this.Shake == 0f)
			{
				this.ShakeMoney = false;
			}
		}
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x00102CA4 File Offset: 0x001010A4
	private string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			return "SUNDAY";
		}
		if (weekday == DayOfWeek.Monday)
		{
			return "MONDAY";
		}
		if (weekday == DayOfWeek.Tuesday)
		{
			return "TUESDAY";
		}
		if (weekday == DayOfWeek.Wednesday)
		{
			return "WEDNESDAY";
		}
		if (weekday == DayOfWeek.Thursday)
		{
			return "THURSDAY";
		}
		if (weekday == DayOfWeek.Friday)
		{
			return "FRIDAY";
		}
		return "SATURDAY";
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x00102D04 File Offset: 0x00101104
	public void UpdateMoneyLabel()
	{
		this.MoneyLabel.text = "$" + PlayerGlobals.Money.ToString("F2");
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x00102D38 File Offset: 0x00101138
	public void MoneyFail()
	{
		this.ShakeMoney = true;
		this.Shake = 10f;
		this.G = 0f;
		this.B = 0f;
		this.MyAudio.Play();
	}

	// Token: 0x04002175 RID: 8565
	public UILabel MoneyLabel;

	// Token: 0x04002176 RID: 8566
	public UILabel HourLabel;

	// Token: 0x04002177 RID: 8567
	public UILabel DayLabel;

	// Token: 0x04002178 RID: 8568
	public AudioSource MyAudio;

	// Token: 0x04002179 RID: 8569
	public bool ShakeMoney;

	// Token: 0x0400217A RID: 8570
	public float Shake;

	// Token: 0x0400217B RID: 8571
	public float G;

	// Token: 0x0400217C RID: 8572
	public float B;
}
