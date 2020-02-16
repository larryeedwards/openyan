using System;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class ClockScript : MonoBehaviour
{
	// Token: 0x060017E7 RID: 6119 RVA: 0x000C0100 File Offset: 0x000BE500
	private void Start()
	{
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f);
		this.PeriodLabel.text = "BEFORE CLASS";
		this.PresentTime = this.StartHour * 60f;
		if (PlayerPrefs.GetInt("LoadingSave") == 1)
		{
			int profile = GameGlobals.Profile;
			int @int = PlayerPrefs.GetInt("SaveSlot");
			Debug.Log(string.Concat(new object[]
			{
				"Loading time! Profile_",
				profile,
				"_Slot_",
				@int,
				"_Time is ",
				PlayerPrefs.GetFloat(string.Concat(new object[]
				{
					"Profile_",
					profile,
					"_Slot_",
					@int,
					"_Time"
				}))
			}));
			this.PresentTime = PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				profile,
				"_Slot_",
				@int,
				"_Time"
			}));
			this.Weekday = PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				profile,
				"_Slot_",
				@int,
				"_Weekday"
			}));
			if (this.Weekday == 1)
			{
				DateGlobals.Weekday = DayOfWeek.Monday;
			}
			else if (this.Weekday == 2)
			{
				DateGlobals.Weekday = DayOfWeek.Tuesday;
			}
			else if (this.Weekday == 3)
			{
				DateGlobals.Weekday = DayOfWeek.Wednesday;
			}
			else if (this.Weekday == 4)
			{
				DateGlobals.Weekday = DayOfWeek.Thursday;
			}
			else if (this.Weekday == 5)
			{
				DateGlobals.Weekday = DayOfWeek.Friday;
			}
		}
		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
		}
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		if (SchoolGlobals.SchoolAtmosphere < 0.5f)
		{
			this.BloomEffect.bloomIntensity = 0.2f;
			this.BloomEffect.bloomThreshhold = 0f;
			this.Police.Darkness.enabled = true;
			this.Police.Darkness.color = new Color(this.Police.Darkness.color.r, this.Police.Darkness.color.g, this.Police.Darkness.color.b, 1f);
			this.FadeIn = true;
		}
		else
		{
			this.BloomEffect.bloomIntensity = 10f;
			this.BloomEffect.bloomThreshhold = 0f;
			this.UpdateBloom = true;
		}
		this.BloomEffect.bloomThreshhold = 0f;
		this.DayLabel.text = this.GetWeekdayText(DateGlobals.Weekday);
		this.MainLight.color = new Color(1f, 1f, 1f, 1f);
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
		if (ClubGlobals.GetClubClosed(ClubType.Photography) || StudentGlobals.GetStudentGrudge(56) || StudentGlobals.GetStudentGrudge(57) || StudentGlobals.GetStudentGrudge(58) || StudentGlobals.GetStudentGrudge(59) || StudentGlobals.GetStudentGrudge(60))
		{
			this.IgnorePhotographyClub = true;
		}
		this.MissionMode = MissionModeGlobals.MissionMode;
		this.HourTime = this.PresentTime / 60f;
		this.Hour = Mathf.Floor(this.PresentTime / 60f);
		this.Minute = Mathf.Floor((this.PresentTime / 60f - this.Hour) * 60f);
		this.UpdateClock();
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x000C050C File Offset: 0x000BE90C
	private void Update()
	{
		if (this.FadeIn && Time.deltaTime < 1f)
		{
			this.Police.Darkness.color = new Color(this.Police.Darkness.color.r, this.Police.Darkness.color.g, this.Police.Darkness.color.b, Mathf.MoveTowards(this.Police.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Police.Darkness.color.a == 0f)
			{
				this.Police.Darkness.enabled = false;
				this.FadeIn = false;
			}
		}
		if (!this.MissionMode && this.CameraTimer < 1f)
		{
			this.CameraTimer += Time.deltaTime;
			if (this.CameraTimer > 1f && !this.StudentManager.MemorialScene.enabled)
			{
				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.CanMove = true;
			}
		}
		if (this.PresentTime < 1080f)
		{
			if (this.UpdateBloom)
			{
				this.BloomEffect.bloomIntensity = Mathf.MoveTowards(this.BloomEffect.bloomIntensity, 0.2f, Time.deltaTime * 5f);
				if (this.BloomEffect.bloomIntensity == 0.2f)
				{
					this.UpdateBloom = false;
				}
			}
		}
		else if (this.LoveManager.WaitingToConfess)
		{
			if (!this.StopTime)
			{
				this.LoveManager.BeginConfession();
			}
		}
		else if (!this.Police.FadeOut && !this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.DelinquentFighting && !this.Yandere.Pickpocketing && !this.Yandere.Noticed)
		{
			this.Police.DayOver = true;
			this.Yandere.StudentManager.StopMoving();
			this.Police.Darkness.enabled = true;
			this.Police.FadeOut = true;
			this.StopTime = true;
		}
		if (!this.StopTime)
		{
			if (this.Period == 3)
			{
				this.PresentTime += Time.deltaTime * 0.0166666675f * this.TimeSpeed * 0.5f;
			}
			else
			{
				this.PresentTime += Time.deltaTime * 0.0166666675f * this.TimeSpeed;
			}
		}
		this.HourTime = this.PresentTime / 60f;
		this.Hour = Mathf.Floor(this.PresentTime / 60f);
		this.Minute = Mathf.Floor((this.PresentTime / 60f - this.Hour) * 60f);
		if (this.Minute != this.LastMinute)
		{
			this.UpdateClock();
		}
		this.MinuteHand.localEulerAngles = new Vector3(this.MinuteHand.localEulerAngles.x, this.MinuteHand.localEulerAngles.y, this.Minute * 6f);
		this.HourHand.localEulerAngles = new Vector3(this.HourHand.localEulerAngles.x, this.HourHand.localEulerAngles.y, this.Hour * 30f);
		if (this.LateStudent && this.HourTime > 7.9f)
		{
			this.ActivateLateStudent();
		}
		if (this.HourTime < 8.5f)
		{
			if (this.Period < 1)
			{
				this.PeriodLabel.text = "BEFORE CLASS";
				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 13f)
		{
			if (this.Period < 2)
			{
				this.PeriodLabel.text = "CLASS TIME";
				this.ActivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 13.5f)
		{
			if (this.Period < 3)
			{
				this.PeriodLabel.text = "LUNCH TIME";
				this.StudentManager.DramaPhase = 0;
				this.StudentManager.UpdateDrama();
				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 15.5f)
		{
			if (this.Period < 4)
			{
				this.PeriodLabel.text = "CLASS TIME";
				this.ActivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.HourTime < 16f)
		{
			if (this.Period < 5)
			{
				foreach (GameObject gameObject in this.StudentManager.Graffiti)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
				this.PeriodLabel.text = "CLEANING TIME";
				this.DeactivateTrespassZones();
				this.Period++;
			}
		}
		else if (this.Period < 6)
		{
			this.PeriodLabel.text = "AFTER SCHOOL";
			this.StudentManager.DramaPhase = 0;
			this.StudentManager.UpdateDrama();
			this.Period++;
		}
		if (!this.IgnorePhotographyClub && this.HourTime > 16.75f && this.StudentManager.SleuthPhase < 4)
		{
			this.StudentManager.SleuthPhase = 3;
			this.StudentManager.UpdateSleuths();
		}
		this.Sun.eulerAngles = new Vector3(this.Sun.eulerAngles.x, this.Sun.eulerAngles.y, -45f + 90f * (this.PresentTime - 420f) / 660f);
		if (!this.Horror)
		{
			if (this.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position))
			{
				this.AmbientLightDim = Mathf.MoveTowards(this.AmbientLightDim, 0.1f, Time.deltaTime);
			}
			else
			{
				this.AmbientLightDim = Mathf.MoveTowards(this.AmbientLightDim, 0.75f, Time.deltaTime);
			}
			if (this.PresentTime > 930f)
			{
				this.DayProgress = (this.PresentTime - 930f) / 150f;
				this.MainLight.color = new Color(1f - 0.149019614f * this.DayProgress, 1f - 0.403921574f * this.DayProgress, 1f - 0.709803939f * this.DayProgress);
				RenderSettings.ambientLight = new Color(1f - 0.149019614f * this.DayProgress - (1f - this.AmbientLightDim) * (1f - this.DayProgress), 1f - 0.403921574f * this.DayProgress - (1f - this.AmbientLightDim) * (1f - this.DayProgress), 1f - 0.709803939f * this.DayProgress - (1f - this.AmbientLightDim) * (1f - this.DayProgress));
				this.SkyboxColor = new Color(1f - 0.149019614f * this.DayProgress - 0.5f * (1f - this.DayProgress), 1f - 0.403921574f * this.DayProgress - 0.5f * (1f - this.DayProgress), 1f - 0.709803939f * this.DayProgress - 0.5f * (1f - this.DayProgress));
				RenderSettings.skybox.SetColor("_Tint", new Color(this.SkyboxColor.r, this.SkyboxColor.g, this.SkyboxColor.b));
			}
			else
			{
				RenderSettings.ambientLight = new Color(this.AmbientLightDim, this.AmbientLightDim, this.AmbientLightDim);
			}
		}
		if (this.TimeSkip)
		{
			if (this.HalfwayTime == 0f)
			{
				this.HalfwayTime = this.PresentTime + (this.TargetTime - this.PresentTime) * 0.5f;
				this.Yandere.TimeSkipHeight = this.Yandere.transform.position.y;
				this.Yandere.Phone.SetActive(true);
				this.Yandere.TimeSkipping = true;
				this.Yandere.CanMove = false;
				this.Blur.enabled = true;
				if (this.Yandere.Armed)
				{
					this.Yandere.Unequip();
				}
			}
			if (Time.timeScale < 25f)
			{
				Time.timeScale += 1f;
			}
			this.Yandere.Character.GetComponent<Animation>()["f02_timeSkip_00"].speed = 1f / Time.timeScale;
			this.Blur.blurAmount = 0.92f * (Time.timeScale / 100f);
			if (this.PresentTime > this.TargetTime)
			{
				this.EndTimeSkip();
			}
			if (this.Yandere.CameraEffects.Streaks.color.a > 0f || this.Yandere.CameraEffects.MurderStreaks.color.a > 0f || this.Yandere.NearSenpai || Input.GetButtonDown("Start"))
			{
				this.EndTimeSkip();
			}
		}
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x000C0F9C File Offset: 0x000BF39C
	public void EndTimeSkip()
	{
		this.PromptParent.localScale = new Vector3(1f, 1f, 1f);
		this.Yandere.Phone.SetActive(false);
		this.Yandere.TimeSkipping = false;
		this.Blur.enabled = false;
		Time.timeScale = 1f;
		this.TimeSkip = false;
		this.HalfwayTime = 0f;
		if (!this.Yandere.Noticed && !this.Police.FadeOut)
		{
			this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMoveTimer = 0.5f;
		}
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x000C1058 File Offset: 0x000BF458
	public string GetWeekdayText(DayOfWeek weekday)
	{
		if (weekday == DayOfWeek.Sunday)
		{
			this.Weekday = 0;
			return "SUNDAY";
		}
		if (weekday == DayOfWeek.Monday)
		{
			this.Weekday = 1;
			return "MONDAY";
		}
		if (weekday == DayOfWeek.Tuesday)
		{
			this.Weekday = 2;
			return "TUESDAY";
		}
		if (weekday == DayOfWeek.Wednesday)
		{
			this.Weekday = 3;
			return "WEDNESDAY";
		}
		if (weekday == DayOfWeek.Thursday)
		{
			this.Weekday = 4;
			return "THURSDAY";
		}
		if (weekday == DayOfWeek.Friday)
		{
			this.Weekday = 5;
			return "FRIDAY";
		}
		this.Weekday = 6;
		return "SATURDAY";
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x000C10E8 File Offset: 0x000BF4E8
	private void ActivateTrespassZones()
	{
		this.SchoolBell.Play();
		foreach (Collider collider in this.TrespassZones)
		{
			collider.enabled = true;
		}
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x000C1128 File Offset: 0x000BF528
	public void DeactivateTrespassZones()
	{
		this.Yandere.Trespassing = false;
		this.SchoolBell.Play();
		foreach (Collider collider in this.TrespassZones)
		{
			if (!collider.GetComponent<TrespassScript>().OffLimits)
			{
				collider.enabled = false;
			}
		}
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000C1184 File Offset: 0x000BF584
	public void ActivateLateStudent()
	{
		if (this.StudentManager.Students[7] != null)
		{
			this.StudentManager.Students[7].gameObject.SetActive(true);
			this.StudentManager.Students[7].Pathfinding.speed = 4f;
			this.StudentManager.Students[7].Spawned = true;
			this.StudentManager.Students[7].Hurry = true;
		}
		this.LateStudent = false;
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x000C120C File Offset: 0x000BF60C
	public void NightLighting()
	{
		this.MainLight.color = new Color(0.25f, 0.25f, 0.5f);
		RenderSettings.ambientLight = new Color(0.25f, 0.25f, 0.5f);
		this.SkyboxColor = new Color(0.1f, 0.1f, 0.2f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.1f, 0.1f, 0.2f));
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000C1290 File Offset: 0x000BF690
	private void UpdateClock()
	{
		this.LastMinute = this.Minute;
		Debug.Log("Updating clock!");
		if (this.Hour == 0f || this.Hour == 12f)
		{
			this.HourNumber = "12";
		}
		else if (this.Hour < 12f)
		{
			this.HourNumber = this.Hour.ToString("f0");
		}
		else
		{
			this.HourNumber = (this.Hour - 12f).ToString("f0");
		}
		if (this.Minute < 10f)
		{
			this.MinuteNumber = "0" + this.Minute.ToString("f0");
		}
		else
		{
			this.MinuteNumber = this.Minute.ToString("f0");
		}
		this.TimeText = this.HourNumber + ":" + this.MinuteNumber + ((this.Hour >= 12f) ? " PM" : " AM");
		this.TimeLabel.text = this.TimeText;
	}

	// Token: 0x04001828 RID: 6184
	private string MinuteNumber = string.Empty;

	// Token: 0x04001829 RID: 6185
	private string HourNumber = string.Empty;

	// Token: 0x0400182A RID: 6186
	public Collider[] TrespassZones;

	// Token: 0x0400182B RID: 6187
	public StudentManagerScript StudentManager;

	// Token: 0x0400182C RID: 6188
	public LoveManagerScript LoveManager;

	// Token: 0x0400182D RID: 6189
	public YandereScript Yandere;

	// Token: 0x0400182E RID: 6190
	public PoliceScript Police;

	// Token: 0x0400182F RID: 6191
	public ClockScript Clock;

	// Token: 0x04001830 RID: 6192
	public Bloom BloomEffect;

	// Token: 0x04001831 RID: 6193
	public MotionBlur Blur;

	// Token: 0x04001832 RID: 6194
	public Transform PromptParent;

	// Token: 0x04001833 RID: 6195
	public Transform MinuteHand;

	// Token: 0x04001834 RID: 6196
	public Transform HourHand;

	// Token: 0x04001835 RID: 6197
	public Transform Sun;

	// Token: 0x04001836 RID: 6198
	public GameObject SunFlare;

	// Token: 0x04001837 RID: 6199
	public UILabel PeriodLabel;

	// Token: 0x04001838 RID: 6200
	public UILabel TimeLabel;

	// Token: 0x04001839 RID: 6201
	public UILabel DayLabel;

	// Token: 0x0400183A RID: 6202
	public Light MainLight;

	// Token: 0x0400183B RID: 6203
	public float HalfwayTime;

	// Token: 0x0400183C RID: 6204
	public float PresentTime;

	// Token: 0x0400183D RID: 6205
	public float TargetTime;

	// Token: 0x0400183E RID: 6206
	public float StartTime;

	// Token: 0x0400183F RID: 6207
	public float HourTime;

	// Token: 0x04001840 RID: 6208
	public float AmbientLightDim;

	// Token: 0x04001841 RID: 6209
	public float CameraTimer;

	// Token: 0x04001842 RID: 6210
	public float DayProgress;

	// Token: 0x04001843 RID: 6211
	public float LastMinute;

	// Token: 0x04001844 RID: 6212
	public float StartHour;

	// Token: 0x04001845 RID: 6213
	public float TimeSpeed;

	// Token: 0x04001846 RID: 6214
	public float Minute;

	// Token: 0x04001847 RID: 6215
	public float Timer;

	// Token: 0x04001848 RID: 6216
	public float Hour;

	// Token: 0x04001849 RID: 6217
	public PhaseOfDay Phase;

	// Token: 0x0400184A RID: 6218
	public int Period;

	// Token: 0x0400184B RID: 6219
	public int Weekday;

	// Token: 0x0400184C RID: 6220
	public int ID;

	// Token: 0x0400184D RID: 6221
	public string TimeText = string.Empty;

	// Token: 0x0400184E RID: 6222
	public bool IgnorePhotographyClub;

	// Token: 0x0400184F RID: 6223
	public bool LateStudent;

	// Token: 0x04001850 RID: 6224
	public bool UpdateBloom;

	// Token: 0x04001851 RID: 6225
	public bool MissionMode;

	// Token: 0x04001852 RID: 6226
	public bool StopTime;

	// Token: 0x04001853 RID: 6227
	public bool TimeSkip;

	// Token: 0x04001854 RID: 6228
	public bool FadeIn;

	// Token: 0x04001855 RID: 6229
	public bool Horror;

	// Token: 0x04001856 RID: 6230
	public AudioSource SchoolBell;

	// Token: 0x04001857 RID: 6231
	public Color SkyboxColor;
}
