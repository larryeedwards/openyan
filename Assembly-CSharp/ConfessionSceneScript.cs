using System;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class ConfessionSceneScript : MonoBehaviour
{
	// Token: 0x06001820 RID: 6176 RVA: 0x000C7A69 File Offset: 0x000C5E69
	private void Start()
	{
		Time.timeScale = 1f;
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x000C7A78 File Offset: 0x000C5E78
	private void Update()
	{
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 0f, Time.deltaTime);
			this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0f, Time.deltaTime);
			if (this.Darkness.color.a == 1f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					this.BloomEffect.bloomIntensity = 1f;
					this.BloomEffect.bloomThreshhold = 0f;
					this.BloomEffect.bloomBlurIterations = 1;
					this.Suitor = this.StudentManager.Students[this.LoveManager.SuitorID];
					this.Rival = this.StudentManager.Students[this.LoveManager.RivalID];
					this.Rival.transform.position = this.RivalSpot.position;
					this.Rival.transform.eulerAngles = this.RivalSpot.eulerAngles;
					this.Suitor.Cosmetic.MyRenderer.materials[this.Suitor.Cosmetic.FaceID].SetFloat("_BlendAmount", 1f);
					this.Suitor.transform.eulerAngles = this.StudentManager.SuitorConfessionSpot.eulerAngles;
					this.Suitor.transform.position = this.StudentManager.SuitorConfessionSpot.position;
					this.Suitor.Character.GetComponent<Animation>().Play(this.Suitor.IdleAnim);
					this.MythBlossoms.emission.rateOverTime = 100f;
					this.HeartBeatCamera.SetActive(false);
					this.ConfessionBG.SetActive(true);
					base.GetComponent<AudioSource>().Play();
					this.MainCamera.position = this.CameraDestinations[1].position;
					this.MainCamera.eulerAngles = this.CameraDestinations[1].eulerAngles;
					this.Timer = 0f;
					this.Phase++;
				}
			}
		}
		else if (this.Phase == 2)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (!this.ShowLabel)
				{
					this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, 0f, Time.deltaTime));
					if (this.Label.color.a == 0f)
					{
						if (this.TextPhase < 5)
						{
							this.MainCamera.position = this.CameraDestinations[this.TextPhase].position;
							this.MainCamera.eulerAngles = this.CameraDestinations[this.TextPhase].eulerAngles;
							if (this.TextPhase == 4 && !this.Kissing)
							{
								ParticleSystem.EmissionModule emission = this.Suitor.Hearts.emission;
								emission.enabled = true;
								emission.rateOverTime = 10f;
								this.Suitor.Hearts.Play();
								ParticleSystem.EmissionModule emission2 = this.Rival.Hearts.emission;
								emission2.enabled = true;
								emission2.rateOverTime = 10f;
								this.Rival.Hearts.Play();
								this.Suitor.Character.transform.localScale = new Vector3(1f, 1f, 1f);
								this.Suitor.Character.GetComponent<Animation>().Play("kiss_00");
								this.Suitor.transform.position = this.KissSpot.position;
								this.Rival.Character.GetComponent<Animation>()[this.Rival.ShyAnim].weight = 0f;
								this.Rival.Character.GetComponent<Animation>().Play("f02_kiss_00");
								this.Kissing = true;
							}
							this.Label.text = this.Text[this.TextPhase];
							this.ShowLabel = true;
						}
						else
						{
							this.Phase++;
						}
					}
				}
				else
				{
					this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, Mathf.MoveTowards(this.Label.color.a, 1f, Time.deltaTime));
					if (this.Label.color.a == 1f)
					{
						if (!this.PromptBar.Show)
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}
						if (Input.GetButtonDown("A"))
						{
							this.TextPhase++;
							this.ShowLabel = false;
						}
					}
				}
			}
		}
		else if (this.Phase == 3)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 1f)
				{
					DatingGlobals.SuitorProgress = 2;
					this.Suitor.Character.transform.localScale = new Vector3(0.94f, 0.94f, 0.94f);
					this.PromptBar.ClearButtons();
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = false;
					this.ConfessionBG.SetActive(false);
					this.Yandere.FixCamera();
					this.Phase++;
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			this.Panel.alpha = Mathf.MoveTowards(this.Panel.alpha, 1f, Time.deltaTime);
			if (this.Darkness.color.a == 0f)
			{
				this.StudentManager.ComeBack();
				this.Yandere.RPGCamera.enabled = true;
				this.Yandere.CanMove = true;
				this.HeartBeatCamera.SetActive(true);
				this.MythBlossoms.emission.rateOverTime = 20f;
				this.Clock.StopTime = false;
				base.enabled = false;
				this.Suitor.CoupleID = this.LoveManager.SuitorID;
				this.Rival.CoupleID = this.LoveManager.RivalID;
			}
		}
		if (this.Kissing)
		{
			Animation component = this.Suitor.Character.GetComponent<Animation>();
			if (component["kiss_00"].time >= component["kiss_00"].length)
			{
				component.CrossFade(this.Suitor.IdleAnim);
				this.Rival.Character.GetComponent<Animation>().CrossFade(this.Rival.IdleAnim);
				this.Kissing = false;
			}
		}
	}

	// Token: 0x04001907 RID: 6407
	public Transform[] CameraDestinations;

	// Token: 0x04001908 RID: 6408
	public StudentManagerScript StudentManager;

	// Token: 0x04001909 RID: 6409
	public LoveManagerScript LoveManager;

	// Token: 0x0400190A RID: 6410
	public PromptBarScript PromptBar;

	// Token: 0x0400190B RID: 6411
	public JukeboxScript Jukebox;

	// Token: 0x0400190C RID: 6412
	public YandereScript Yandere;

	// Token: 0x0400190D RID: 6413
	public ClockScript Clock;

	// Token: 0x0400190E RID: 6414
	public Bloom BloomEffect;

	// Token: 0x0400190F RID: 6415
	public StudentScript Suitor;

	// Token: 0x04001910 RID: 6416
	public StudentScript Rival;

	// Token: 0x04001911 RID: 6417
	public ParticleSystem MythBlossoms;

	// Token: 0x04001912 RID: 6418
	public GameObject HeartBeatCamera;

	// Token: 0x04001913 RID: 6419
	public GameObject ConfessionBG;

	// Token: 0x04001914 RID: 6420
	public Transform MainCamera;

	// Token: 0x04001915 RID: 6421
	public Transform RivalSpot;

	// Token: 0x04001916 RID: 6422
	public Transform KissSpot;

	// Token: 0x04001917 RID: 6423
	public string[] Text;

	// Token: 0x04001918 RID: 6424
	public UISprite Darkness;

	// Token: 0x04001919 RID: 6425
	public UILabel Label;

	// Token: 0x0400191A RID: 6426
	public UIPanel Panel;

	// Token: 0x0400191B RID: 6427
	public bool ShowLabel;

	// Token: 0x0400191C RID: 6428
	public bool Kissing;

	// Token: 0x0400191D RID: 6429
	public int TextPhase = 1;

	// Token: 0x0400191E RID: 6430
	public int Phase = 1;

	// Token: 0x0400191F RID: 6431
	public float Timer;
}
