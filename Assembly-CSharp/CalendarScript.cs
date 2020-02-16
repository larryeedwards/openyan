using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000350 RID: 848
public class CalendarScript : MonoBehaviour
{
	// Token: 0x0600179E RID: 6046 RVA: 0x000BB058 File Offset: 0x000B9458
	private void Start()
	{
		Debug.Log("Upon entering the Calendar screen, DateGlobals.Weekday is: " + DateGlobals.Weekday);
		this.LoveSickCheck();
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			OptionGlobals.EnableShadows = false;
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
			PlayerGlobals.Money = 10f;
		}
		if (SchoolGlobals.SchoolAtmosphere > 1f)
		{
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		if (DateGlobals.Weekday > DayOfWeek.Thursday)
		{
			DateGlobals.Weekday = DayOfWeek.Sunday;
			Globals.DeleteAll();
		}
		if (DateGlobals.PassDays < 1)
		{
			DateGlobals.PassDays = 1;
		}
		DateGlobals.DayPassed = true;
		this.Sun.color = new Color(this.Sun.color.r, this.Sun.color.g, this.Sun.color.b, SchoolGlobals.SchoolAtmosphere);
		this.Cloud.color = new Color(this.Cloud.color.r, this.Cloud.color.g, this.Cloud.color.b, 1f - SchoolGlobals.SchoolAtmosphere);
		this.AtmosphereLabel.text = (SchoolGlobals.SchoolAtmosphere * 100f).ToString("f0") + "%";
		float num = 1f - SchoolGlobals.SchoolAtmosphere;
		this.GrayscaleEffect.desaturation = num;
		this.Vignette.intensity = num * 5f;
		this.Vignette.blur = num;
		this.Vignette.chromaticAberration = num;
		this.Continue.transform.localPosition = new Vector3(this.Continue.transform.localPosition.x, -610f, this.Continue.transform.localPosition.z);
		this.Challenge.ViewButton.SetActive(false);
		this.Challenge.LargeIcon.color = new Color(this.Challenge.LargeIcon.color.r, this.Challenge.LargeIcon.color.g, this.Challenge.LargeIcon.color.b, 0f);
		this.Challenge.Panels[1].alpha = 0.5f;
		this.Challenge.Shadow.color = new Color(this.Challenge.Shadow.color.r, this.Challenge.Shadow.color.g, this.Challenge.Shadow.color.b, 0f);
		this.ChallengePanel.alpha = 0f;
		this.CalendarPanel.alpha = 1f;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		Time.timeScale = 1f;
		this.Highlight.localPosition = new Vector3(-600f + 200f * (float)DateGlobals.Weekday, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
		if (DateGlobals.Weekday == DayOfWeek.Saturday)
		{
			this.Highlight.localPosition = new Vector3(-1125f, this.Highlight.localPosition.y, this.Highlight.localPosition.z);
		}
		if (DateGlobals.Week == 2)
		{
			this.DayNumber[1].text = "11";
			this.DayNumber[2].text = "12";
			this.DayNumber[3].text = "13";
			this.DayNumber[4].text = "14";
			this.DayNumber[5].text = "15";
			this.DayNumber[6].text = "16";
			this.DayNumber[7].text = "17";
		}
		this.WeekNumber.text = "Week " + DateGlobals.Week;
		this.LoveSickCheck();
	}

	// Token: 0x0600179F RID: 6047 RVA: 0x000BB508 File Offset: 0x000B9908
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
			if (this.Darkness.color.a < 0f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
			}
			if (this.Timer > 1f)
			{
				if (!this.Incremented)
				{
					while (DateGlobals.PassDays > 0)
					{
						DateGlobals.Weekday++;
						DateGlobals.PassDays--;
					}
					this.Target = 200f * (float)DateGlobals.Weekday;
					if (DateGlobals.Weekday > DayOfWeek.Saturday)
					{
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						DateGlobals.Weekday = DayOfWeek.Sunday;
						this.Target = 0f;
					}
					Debug.Log("And, as of now, DateGlobals.Weekday is: " + DateGlobals.Weekday);
					this.Incremented = true;
					base.GetComponent<AudioSource>().Play();
				}
				else
				{
					this.Highlight.localPosition = new Vector3(Mathf.Lerp(this.Highlight.localPosition.x, -600f + this.Target, Time.deltaTime * 10f), this.Highlight.localPosition.y, this.Highlight.localPosition.z);
				}
				if (this.Timer > 2f)
				{
					this.Continue.localPosition = new Vector3(this.Continue.localPosition.x, Mathf.Lerp(this.Continue.localPosition.y, -500f, Time.deltaTime * 10f), this.Continue.localPosition.z);
					if (!this.Switch)
					{
						if (!this.ConfirmationWindow.activeInHierarchy)
						{
							if (Input.GetButtonDown("A"))
							{
								this.FadeOut = true;
							}
							if (Input.GetButtonDown("Y"))
							{
								this.Switch = true;
							}
							if (Input.GetButtonDown("B"))
							{
								this.ConfirmationWindow.SetActive(true);
							}
							if (Input.GetKeyDown(KeyCode.Z))
							{
								if (SchoolGlobals.SchoolAtmosphere > 0f)
								{
									SchoolGlobals.SchoolAtmosphere -= 0.1f;
								}
								else
								{
									SchoolGlobals.SchoolAtmosphere = 100f;
								}
								SceneManager.LoadScene(SceneManager.GetActiveScene().name);
							}
						}
						else
						{
							if (Input.GetButtonDown("A"))
							{
								this.FadeOut = true;
								this.Reset = true;
							}
							if (Input.GetButtonDown("B"))
							{
								this.ConfirmationWindow.SetActive(false);
							}
						}
					}
				}
			}
		}
		else
		{
			this.Continue.localPosition = new Vector3(this.Continue.localPosition.x, Mathf.Lerp(this.Continue.localPosition.y, -610f, Time.deltaTime * 10f), this.Continue.localPosition.z);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Reset)
				{
					int profile = GameGlobals.Profile;
					Globals.DeleteAll();
					PlayerPrefs.SetInt("ProfileCreated_" + profile, 1);
					GameGlobals.Profile = profile;
					GameGlobals.LoveSick = this.LoveSick;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else
				{
					if (HomeGlobals.Night)
					{
						HomeGlobals.Night = false;
					}
					if (DateGlobals.Weekday == DayOfWeek.Saturday)
					{
						SceneManager.LoadScene("BusStopScene");
					}
					else
					{
						if (DateGlobals.Weekday == DayOfWeek.Sunday)
						{
							HomeGlobals.Night = true;
						}
						SceneManager.LoadScene("HomeScene");
					}
				}
			}
		}
		if (this.Switch)
		{
			if (this.Phase == 1)
			{
				this.CalendarPanel.alpha -= Time.deltaTime;
				if (this.CalendarPanel.alpha <= 0f)
				{
					this.Phase++;
				}
			}
			else
			{
				this.ChallengePanel.alpha += Time.deltaTime;
				if (this.ChallengePanel.alpha >= 1f)
				{
					this.Challenge.enabled = true;
					base.enabled = false;
					this.Switch = false;
					this.Phase = 1;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			DateGlobals.Weekday = DayOfWeek.Monday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			DateGlobals.Weekday = DayOfWeek.Tuesday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			DateGlobals.Weekday = DayOfWeek.Wednesday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			DateGlobals.Weekday = DayOfWeek.Thursday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			DateGlobals.Weekday = DayOfWeek.Friday;
			this.Target = 200f * (float)DateGlobals.Weekday;
		}
	}

	// Token: 0x060017A0 RID: 6048 RVA: 0x000BBB6C File Offset: 0x000B9F6C
	public void LoveSickCheck()
	{
		if (GameGlobals.LoveSick)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
			this.LoveSick = true;
			Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
			GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
			foreach (GameObject gameObject in array)
			{
				UISprite component = gameObject.GetComponent<UISprite>();
				if (component != null)
				{
					component.color = new Color(1f, 0f, 0f, component.color.a);
				}
				UITexture component2 = gameObject.GetComponent<UITexture>();
				if (component2 != null)
				{
					component2.color = new Color(1f, 0f, 0f, component2.color.a);
				}
				UILabel component3 = gameObject.GetComponent<UILabel>();
				if (component3 != null)
				{
					if (component3.color != Color.black)
					{
						component3.color = new Color(1f, 0f, 0f, component3.color.a);
					}
					if (component3.text == "?")
					{
						component3.color = new Color(1f, 0f, 0f, component3.color.a);
					}
				}
			}
			this.Darkness.color = Color.black;
			this.AtmosphereLabel.enabled = false;
			this.Cloud.enabled = false;
			this.Sun.enabled = false;
		}
	}

	// Token: 0x0400176D RID: 5997
	public SelectiveGrayscale GrayscaleEffect;

	// Token: 0x0400176E RID: 5998
	public ChallengeScript Challenge;

	// Token: 0x0400176F RID: 5999
	public Vignetting Vignette;

	// Token: 0x04001770 RID: 6000
	public GameObject ConfirmationWindow;

	// Token: 0x04001771 RID: 6001
	public UILabel AtmosphereLabel;

	// Token: 0x04001772 RID: 6002
	public UIPanel ChallengePanel;

	// Token: 0x04001773 RID: 6003
	public UIPanel CalendarPanel;

	// Token: 0x04001774 RID: 6004
	public UISprite Darkness;

	// Token: 0x04001775 RID: 6005
	public UITexture Cloud;

	// Token: 0x04001776 RID: 6006
	public UITexture Sun;

	// Token: 0x04001777 RID: 6007
	public Transform Highlight;

	// Token: 0x04001778 RID: 6008
	public Transform Continue;

	// Token: 0x04001779 RID: 6009
	public UILabel[] DayNumber;

	// Token: 0x0400177A RID: 6010
	public UILabel WeekNumber;

	// Token: 0x0400177B RID: 6011
	public bool Incremented;

	// Token: 0x0400177C RID: 6012
	public bool LoveSick;

	// Token: 0x0400177D RID: 6013
	public bool FadeOut;

	// Token: 0x0400177E RID: 6014
	public bool Switch;

	// Token: 0x0400177F RID: 6015
	public bool Reset;

	// Token: 0x04001780 RID: 6016
	public float Timer;

	// Token: 0x04001781 RID: 6017
	public float Target;

	// Token: 0x04001782 RID: 6018
	public int Phase = 1;
}
