using System;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class DateReverseScript : MonoBehaviour
{
	// Token: 0x0600188A RID: 6282 RVA: 0x000D83DB File Offset: 0x000D67DB
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000D83E8 File Offset: 0x000D67E8
	private void Update()
	{
		this.LifeTime += Time.deltaTime;
		this.Timer += Time.deltaTime * 2f;
		if (this.Timer > this.TimeLimit)
		{
			if (this.LifeTime < 10f)
			{
				if (this.TimeLimit > Time.deltaTime)
				{
					this.TimeLimit *= 0.9f;
				}
			}
			else
			{
				this.TimeLimit *= 1.1f;
				if (this.TimeLimit >= 1f)
				{
					this.MyAudio.clip = this.Finish;
					this.Label.color = new Color(1f, 0f, 0f, 1f);
					base.enabled = false;
				}
			}
			this.Timer = 0f;
			this.Day--;
			if (this.Day == 0)
			{
				this.Day = 28;
				this.Month--;
				if (this.Month == 0)
				{
					this.Month = 12;
					this.Year--;
				}
			}
			if (this.Day == 1 || this.Day == 21)
			{
				this.Prefix = "st";
			}
			else if (this.Day == 2 || this.Day == 22)
			{
				this.Prefix = "nd";
			}
			else if (this.Day == 3 || this.Day == 23)
			{
				this.Prefix = "rd";
			}
			else
			{
				this.Prefix = "th";
			}
			this.Label.text = string.Concat(new object[]
			{
				this.MonthName[this.Month],
				" ",
				this.Day,
				this.Prefix,
				", ",
				this.Year
			});
			this.MyAudio.Play();
		}
	}

	// Token: 0x04001B3A RID: 6970
	public AudioSource MyAudio;

	// Token: 0x04001B3B RID: 6971
	public string[] MonthName;

	// Token: 0x04001B3C RID: 6972
	public string Prefix;

	// Token: 0x04001B3D RID: 6973
	public UILabel Label;

	// Token: 0x04001B3E RID: 6974
	public AudioClip Finish;

	// Token: 0x04001B3F RID: 6975
	public float TimeLimit;

	// Token: 0x04001B40 RID: 6976
	public float LifeTime;

	// Token: 0x04001B41 RID: 6977
	public float Timer;

	// Token: 0x04001B42 RID: 6978
	public int Month;

	// Token: 0x04001B43 RID: 6979
	public int Year;

	// Token: 0x04001B44 RID: 6980
	public int Day;
}
