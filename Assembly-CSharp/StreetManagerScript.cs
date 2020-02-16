using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000526 RID: 1318
public class StreetManagerScript : MonoBehaviour
{
	// Token: 0x06002060 RID: 8288 RVA: 0x00151C9C File Offset: 0x0015009C
	private void Start()
	{
		this.MaidAnimation["f02_faceCouncilGrace_00"].layer = 1;
		this.MaidAnimation.Play("f02_faceCouncilGrace_00");
		this.MaidAnimation["f02_faceCouncilGrace_00"].weight = 1f;
		this.Gossip1["f02_socialSit_00"].layer = 1;
		this.Gossip1.Play("f02_socialSit_00");
		this.Gossip1["f02_socialSit_00"].weight = 1f;
		this.Gossip2["f02_socialSit_00"].layer = 1;
		this.Gossip2.Play("f02_socialSit_00");
		this.Gossip2["f02_socialSit_00"].weight = 1f;
		for (int i = 2; i < 5; i++)
		{
			this.Civilian[i]["f02_smile_00"].layer = 1;
			this.Civilian[i].Play("f02_smile_00");
			this.Civilian[i]["f02_smile_00"].weight = 1f;
		}
		this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		this.CurrentlyActiveJukebox = this.JukeboxNight;
		this.Alpha = 1f;
		if (StudentGlobals.GetStudentDead(30) || StudentGlobals.GetStudentKidnapped(30) || StudentGlobals.GetStudentBroken(81))
		{
			this.Couple.SetActive(false);
		}
		this.Sunlight.shadows = LightShadows.None;
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x00151E3C File Offset: 0x0015023C
	private void Update()
	{
		if (Input.GetKeyDown("m"))
		{
			PlayerGlobals.Money += 1f;
			this.Clock.UpdateMoneyLabel();
			if (this.JukeboxNight.isPlaying)
			{
				this.JukeboxNight.Stop();
				this.JukeboxDay.Stop();
			}
			else
			{
				this.JukeboxNight.Play();
				this.JukeboxDay.Stop();
			}
		}
		if (Input.GetKeyDown("f"))
		{
			PlayerGlobals.FakeID = !PlayerGlobals.FakeID;
			this.StreetShopInterface.UpdateFakeID();
		}
		this.Timer += Time.deltaTime;
		if (this.Timer > 0.5f)
		{
			if (this.Alpha == 1f)
			{
				this.JukeboxNight.volume = 0.5f;
				this.JukeboxNight.Play();
				this.JukeboxDay.volume = 0f;
				this.JukeboxDay.Play();
			}
			if (!this.FadeOut)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime);
				this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
			}
			else
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
				this.CurrentlyActiveJukebox.volume = (1f - this.Alpha) * 0.5f;
				if (this.GoToCafe)
				{
					this.Darkness.color = new Color(1f, 1f, 1f, this.Alpha);
					if (this.Alpha == 1f)
					{
						SceneManager.LoadScene("MaidMenuScene");
					}
				}
				else
				{
					this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
					if (this.Alpha == 1f)
					{
						SceneManager.LoadScene("HomeScene");
					}
				}
			}
		}
		if (!this.FadeOut && !this.BinocularCamera.gameObject.activeInHierarchy)
		{
			if (Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) > 5f)
			{
				this.DesiredValue = 0.5f;
			}
			else
			{
				this.DesiredValue = Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) * 0.1f;
			}
			if (this.Day)
			{
				this.JukeboxDay.volume = Mathf.Lerp(this.JukeboxDay.volume, this.DesiredValue, Time.deltaTime * 10f);
				this.JukeboxNight.volume = Mathf.Lerp(this.JukeboxNight.volume, 0f, Time.deltaTime * 10f);
			}
			else
			{
				this.JukeboxDay.volume = Mathf.Lerp(this.JukeboxDay.volume, 0f, Time.deltaTime * 10f);
				this.JukeboxNight.volume = Mathf.Lerp(this.JukeboxNight.volume, this.DesiredValue, Time.deltaTime * 10f);
			}
			if (Vector3.Distance(this.Yandere.position, this.Yakuza.transform.position) < 1f && !this.Threatened)
			{
				this.Threatened = true;
				this.Yakuza.Play();
			}
		}
		if (Input.GetKeyDown("space"))
		{
			this.Day = !this.Day;
			if (this.Day)
			{
				this.Clock.HourLabel.text = "12:00 PM";
				this.Sunlight.shadows = LightShadows.Soft;
			}
			else
			{
				this.Clock.HourLabel.text = "8:00 PM";
				this.Sunlight.shadows = LightShadows.None;
			}
		}
		if (this.Day)
		{
			this.CurrentlyActiveJukebox = this.JukeboxDay;
			this.Rotation = Mathf.Lerp(this.Rotation, 45f, Time.deltaTime * 10f);
			this.StarAlpha = Mathf.Lerp(this.StarAlpha, 0f, Time.deltaTime * 10f);
		}
		else
		{
			this.CurrentlyActiveJukebox = this.JukeboxNight;
			this.Rotation = Mathf.Lerp(this.Rotation, -45f, Time.deltaTime * 10f);
			this.StarAlpha = Mathf.Lerp(this.StarAlpha, 1f, Time.deltaTime * 10f);
		}
		this.Sun.transform.eulerAngles = new Vector3(this.Rotation, this.Rotation, 0f);
		this.Stars.material.SetColor("_TintColor", new Color(1f, 1f, 1f, this.StarAlpha));
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x00152355 File Offset: 0x00150755
	private void LateUpdate()
	{
		this.Hips.LookAt(this.BinocularCamera.position);
	}

	// Token: 0x04002D5C RID: 11612
	public StreetShopInterfaceScript StreetShopInterface;

	// Token: 0x04002D5D RID: 11613
	public Transform BinocularCamera;

	// Token: 0x04002D5E RID: 11614
	public Transform Yandere;

	// Token: 0x04002D5F RID: 11615
	public Transform Hips;

	// Token: 0x04002D60 RID: 11616
	public Transform Sun;

	// Token: 0x04002D61 RID: 11617
	public Animation MaidAnimation;

	// Token: 0x04002D62 RID: 11618
	public Animation Gossip1;

	// Token: 0x04002D63 RID: 11619
	public Animation Gossip2;

	// Token: 0x04002D64 RID: 11620
	public AudioSource CurrentlyActiveJukebox;

	// Token: 0x04002D65 RID: 11621
	public AudioSource JukeboxNight;

	// Token: 0x04002D66 RID: 11622
	public AudioSource JukeboxDay;

	// Token: 0x04002D67 RID: 11623
	public AudioSource Yakuza;

	// Token: 0x04002D68 RID: 11624
	public HomeClockScript Clock;

	// Token: 0x04002D69 RID: 11625
	public Animation[] Civilian;

	// Token: 0x04002D6A RID: 11626
	public GameObject Couple;

	// Token: 0x04002D6B RID: 11627
	public UISprite Darkness;

	// Token: 0x04002D6C RID: 11628
	public Renderer Stars;

	// Token: 0x04002D6D RID: 11629
	public Light Sunlight;

	// Token: 0x04002D6E RID: 11630
	public bool Threatened;

	// Token: 0x04002D6F RID: 11631
	public bool GoToCafe;

	// Token: 0x04002D70 RID: 11632
	public bool FadeOut;

	// Token: 0x04002D71 RID: 11633
	public bool Day;

	// Token: 0x04002D72 RID: 11634
	public float Rotation;

	// Token: 0x04002D73 RID: 11635
	public float Timer;

	// Token: 0x04002D74 RID: 11636
	public float DesiredValue;

	// Token: 0x04002D75 RID: 11637
	public float StarAlpha;

	// Token: 0x04002D76 RID: 11638
	public float Alpha;
}
