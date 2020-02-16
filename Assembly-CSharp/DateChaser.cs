using System;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class DateChaser : MonoBehaviour
{
	// Token: 0x06001884 RID: 6276 RVA: 0x000D821C File Offset: 0x000D661C
	private static DateTime fromUnix(long unix)
	{
		return DateChaser.epoch.AddSeconds((double)unix);
	}

	// Token: 0x06001885 RID: 6277 RVA: 0x000D8238 File Offset: 0x000D6638
	private void Start()
	{
		Application.targetFrameRate = 60;
		Time.timeScale = 1f;
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x000D824C File Offset: 0x000D664C
	private void Update()
	{
		if (this.Animate)
		{
			float num = Time.time - this.startTime;
			this.CurrentDate = (int)Mathf.Lerp((float)this.startDate, (float)this.endDate, this.curve.Evaluate(num / this.generalDuration));
			DateTime dateTime = DateChaser.fromUnix((long)this.CurrentDate);
			string text = (dateTime.Day != 22 && dateTime.Day != 2) ? ((dateTime.Day != 3) ? ((dateTime.Day != 1) ? "th" : "st") : "rd") : "nd";
			this.CurrentTimeString = string.Format("{0} {1}{2}, {3}", new object[]
			{
				this.monthNames[dateTime.Month - 1],
				dateTime.Day,
				text,
				dateTime.Year
			});
			if (this.lastFrameDay != dateTime.Day)
			{
				this.onDayTick(dateTime.Day);
			}
			this.lastFrameDay = dateTime.Day;
			this.Timer += Time.deltaTime;
		}
		else
		{
			this.startTime = Time.time;
			this.CurrentDate = this.startDate;
		}
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x000D83A9 File Offset: 0x000D67A9
	private void onDayTick(int day)
	{
		this.Label.text = this.CurrentTimeString;
	}

	// Token: 0x04001B2C RID: 6956
	public int CurrentDate;

	// Token: 0x04001B2D RID: 6957
	public string CurrentTimeString;

	// Token: 0x04001B2E RID: 6958
	[Header("Epoch timestamps")]
	[SerializeField]
	private int startDate = 1581724799;

	// Token: 0x04001B2F RID: 6959
	[SerializeField]
	private int endDate = 1421366399;

	// Token: 0x04001B30 RID: 6960
	[Space(5f)]
	[Header("Settings")]
	[SerializeField]
	private float generalDuration = 10f;

	// Token: 0x04001B31 RID: 6961
	[SerializeField]
	private AnimationCurve curve;

	// Token: 0x04001B32 RID: 6962
	public bool Animate;

	// Token: 0x04001B33 RID: 6963
	private float startTime;

	// Token: 0x04001B34 RID: 6964
	private string[] monthNames = new string[]
	{
		"January",
		"February",
		"March",
		"April",
		"May",
		"June",
		"July",
		"August",
		"September",
		"October",
		"November",
		"December"
	};

	// Token: 0x04001B35 RID: 6965
	private int lastFrameDay;

	// Token: 0x04001B36 RID: 6966
	private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	// Token: 0x04001B37 RID: 6967
	public UILabel Label;

	// Token: 0x04001B38 RID: 6968
	public float Timer;

	// Token: 0x04001B39 RID: 6969
	public int Stage;
}
