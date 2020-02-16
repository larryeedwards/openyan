using System;
using UnityEngine;

// Token: 0x0200052B RID: 1323
public class StruggleBarScript : MonoBehaviour
{
	// Token: 0x06002074 RID: 8308 RVA: 0x001538CD File Offset: 0x00151CCD
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.ChooseButton();
	}

	// Token: 0x06002075 RID: 8309 RVA: 0x001538E8 File Offset: 0x00151CE8
	private void Update()
	{
		if (this.Struggling)
		{
			this.Intensity = Mathf.MoveTowards(this.Intensity, 1f, Time.deltaTime);
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.Spikes.localEulerAngles = new Vector3(this.Spikes.localEulerAngles.x, this.Spikes.localEulerAngles.y, this.Spikes.localEulerAngles.z - Time.deltaTime * 360f);
			this.Victory -= Time.deltaTime * 10f * this.Strength * this.Intensity;
			if (ClubGlobals.Club == ClubType.MartialArts)
			{
				this.Victory = 100f;
			}
			if (Input.GetButtonDown(this.CurrentButton))
			{
				if (this.Invincible)
				{
					this.Victory += 100f;
				}
				this.Victory += Time.deltaTime * (500f + (float)(ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus) * 150f) * this.Intensity;
			}
			if (this.Victory >= 100f)
			{
				this.Victory = 100f;
			}
			if (this.Victory <= -100f)
			{
				this.Victory = -100f;
			}
			UISprite uisprite = this.ButtonPrompts[this.ButtonID];
			uisprite.transform.localPosition = new Vector3(Mathf.Lerp(uisprite.transform.localPosition.x, this.Victory * 6.5f, Time.deltaTime * 10f), uisprite.transform.localPosition.y, uisprite.transform.localPosition.z);
			this.Spikes.localPosition = new Vector3(uisprite.transform.localPosition.x, this.Spikes.localPosition.y, this.Spikes.localPosition.z);
			if (this.Victory == 100f)
			{
				Debug.Log("Yandere-chan just won a struggle against " + this.Student.Name + ".");
				this.Yandere.Won = true;
				this.Student.Lost = true;
				this.Struggling = false;
				this.Victory = 0f;
			}
			else if (this.Victory == -100f)
			{
				if (!this.Invincible)
				{
					this.HeroWins();
				}
			}
			else
			{
				this.ButtonTimer += Time.deltaTime;
				if (this.ButtonTimer >= 1f)
				{
					this.ChooseButton();
					this.ButtonTimer = 0f;
					this.Intensity = 0f;
				}
			}
		}
		else if (base.transform.localScale.x > 0.1f)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
		else
		{
			base.transform.localScale = Vector3.zero;
			if (!this.Yandere.AttackManager.Censor)
			{
				base.gameObject.SetActive(false);
			}
			else
			{
				if (this.AttackTimer == 0f)
				{
					this.Yandere.Blur.enabled = true;
					this.Yandere.Blur.blurSize = 0f;
					this.Yandere.Blur.blurIterations = 0;
				}
				this.AttackTimer += Time.deltaTime;
				if (this.AttackTimer < 2.5f)
				{
					this.Yandere.Blur.blurSize = Mathf.MoveTowards(this.Yandere.Blur.blurSize, 10f, Time.deltaTime * 10f);
					if (this.Yandere.Blur.blurSize > (float)this.Yandere.Blur.blurIterations)
					{
						this.Yandere.Blur.blurIterations++;
					}
				}
				else
				{
					this.Yandere.Blur.blurSize = Mathf.Lerp(this.Yandere.Blur.blurSize, 0f, Time.deltaTime * 10f);
					if (this.Yandere.Blur.blurSize < (float)this.Yandere.Blur.blurIterations)
					{
						this.Yandere.Blur.blurIterations--;
					}
					if (this.AttackTimer >= 3f)
					{
						base.gameObject.SetActive(false);
						this.Yandere.Blur.enabled = false;
						this.Yandere.Blur.blurSize = 0f;
						this.Yandere.Blur.blurIterations = 0;
						this.AttackTimer = 0f;
					}
				}
			}
		}
	}

	// Token: 0x06002076 RID: 8310 RVA: 0x00153E3C File Offset: 0x0015223C
	public void HeroWins()
	{
		if (this.Yandere.Armed)
		{
			this.Yandere.EquippedWeapon.Drop();
		}
		this.Yandere.Lost = true;
		this.Student.Won = true;
		this.Struggling = false;
		this.Victory = 0f;
	}

	// Token: 0x06002077 RID: 8311 RVA: 0x00153E94 File Offset: 0x00152294
	private void ChooseButton()
	{
		int buttonID = this.ButtonID;
		for (int i = 1; i < 5; i++)
		{
			this.ButtonPrompts[i].enabled = false;
			this.ButtonPrompts[i].transform.localPosition = this.ButtonPrompts[buttonID].transform.localPosition;
		}
		while (this.ButtonID == buttonID)
		{
			this.ButtonID = UnityEngine.Random.Range(1, 5);
		}
		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}
		this.ButtonPrompts[this.ButtonID].enabled = true;
	}

	// Token: 0x04002DC1 RID: 11713
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x04002DC2 RID: 11714
	public PromptSwapScript ButtonPrompt;

	// Token: 0x04002DC3 RID: 11715
	public UISprite[] ButtonPrompts;

	// Token: 0x04002DC4 RID: 11716
	public YandereScript Yandere;

	// Token: 0x04002DC5 RID: 11717
	public StudentScript Student;

	// Token: 0x04002DC6 RID: 11718
	public Transform Spikes;

	// Token: 0x04002DC7 RID: 11719
	public string CurrentButton = string.Empty;

	// Token: 0x04002DC8 RID: 11720
	public bool Struggling;

	// Token: 0x04002DC9 RID: 11721
	public bool Invincible;

	// Token: 0x04002DCA RID: 11722
	public float AttackTimer;

	// Token: 0x04002DCB RID: 11723
	public float ButtonTimer;

	// Token: 0x04002DCC RID: 11724
	public float Intensity;

	// Token: 0x04002DCD RID: 11725
	public float Strength = 1f;

	// Token: 0x04002DCE RID: 11726
	public float Struggle;

	// Token: 0x04002DCF RID: 11727
	public float Victory;

	// Token: 0x04002DD0 RID: 11728
	public int ButtonID;
}
