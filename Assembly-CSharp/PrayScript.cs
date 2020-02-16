using System;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class PrayScript : MonoBehaviour
{
	// Token: 0x06001EC4 RID: 7876 RVA: 0x00135638 File Offset: 0x00133A38
	private void Start()
	{
		if (StudentGlobals.GetStudentDead(39))
		{
			this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 0.5f);
		}
		this.PrayWindow.localScale = Vector3.zero;
		if (MissionModeGlobals.MissionMode)
		{
			this.Disable();
		}
		if (GameGlobals.LoveSick)
		{
			this.Disable();
		}
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x001356CF File Offset: 0x00133ACF
	private void Disable()
	{
		this.GenderPrompt.gameObject.SetActive(false);
		base.enabled = false;
		this.Prompt.enabled = false;
		this.Prompt.Hide();
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x00135700 File Offset: 0x00133B00
	private void Update()
	{
		if (!this.FemaleVictimChecked)
		{
			if (this.StudentManager.Students[39] != null && !this.StudentManager.Students[39].Alive)
			{
				this.FemaleVictimChecked = true;
				this.Victims++;
			}
		}
		else if (this.StudentManager.Students[39] == null)
		{
			this.FemaleVictimChecked = false;
			this.Victims--;
		}
		if (!this.MaleVictimChecked)
		{
			if (this.StudentManager.Students[37] != null && !this.StudentManager.Students[37].Alive)
			{
				this.MaleVictimChecked = true;
				this.Victims++;
			}
		}
		else if (this.StudentManager.Students[37] == null)
		{
			this.MaleVictimChecked = false;
			this.Victims--;
		}
		if (this.JustSummoned)
		{
			this.StudentManager.UpdateMe(this.StudentID);
			this.JustSummoned = false;
		}
		if (this.GenderPrompt.Circle[0].fillAmount == 0f)
		{
			this.GenderPrompt.Circle[0].fillAmount = 1f;
			if (!this.SpawnMale)
			{
				this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, (!StudentGlobals.GetStudentDead(37)) ? 1f : 0.5f);
				this.GenderPrompt.Label[0].text = "     Male Victim";
				this.SpawnMale = true;
			}
			else
			{
				this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, (!StudentGlobals.GetStudentDead(39)) ? 1f : 0.5f);
				this.GenderPrompt.Label[0].text = "     Female Victim";
				this.SpawnMale = false;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TargetStudent = this.Student;
				this.StudentManager.DisablePrompts();
				this.PrayWindow.gameObject.SetActive(true);
				this.Show = true;
				this.Yandere.ShoulderCamera.OverShoulder = true;
				this.Yandere.WeaponMenu.KeyboardShow = false;
				this.Yandere.Obscurance.enabled = false;
				this.Yandere.WeaponMenu.Show = false;
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				this.Yandere.Talking = true;
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.StudentNumber = ((!this.SpawnMale) ? 39 : 37);
				if (this.StudentManager.Students[this.StudentNumber] != null)
				{
					if (!this.StudentManager.Students[this.StudentNumber].gameObject.activeInHierarchy)
					{
						this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 0.5f);
					}
					else
					{
						this.VictimLabel.color = new Color(this.VictimLabel.color.r, this.VictimLabel.color.g, this.VictimLabel.color.b, 1f);
					}
				}
			}
		}
		if (!this.Show)
		{
			if (this.PrayWindow.gameObject.activeInHierarchy)
			{
				if (this.PrayWindow.localScale.x > 0.1f)
				{
					this.PrayWindow.localScale = Vector3.Lerp(this.PrayWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
				}
				else
				{
					this.PrayWindow.localScale = Vector3.zero;
					this.PrayWindow.gameObject.SetActive(false);
				}
			}
		}
		else
		{
			this.PrayWindow.localScale = Vector3.Lerp(this.PrayWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.InputManager.TappedUp)
			{
				this.Selected--;
				if (this.Selected == 7)
				{
					this.Selected = 6;
				}
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				if (this.Selected == 7)
				{
					this.Selected = 8;
				}
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				if (this.Selected == 1)
				{
					if (!this.Yandere.SanityBased)
					{
						this.SanityLabel.text = "Disable Sanity Anims";
						this.Yandere.SanityBased = true;
					}
					else
					{
						this.SanityLabel.text = "Enable Sanity Anims";
						this.Yandere.SanityBased = false;
					}
					this.Exit();
				}
				else if (this.Selected == 2)
				{
					this.Yandere.Sanity -= 50f;
					this.Exit();
				}
				else if (this.Selected == 3)
				{
					if (this.VictimLabel.color.a == 1f && this.StudentManager.NPCsSpawned >= this.StudentManager.NPCsTotal)
					{
						if (this.SpawnMale)
						{
							this.MaleVictimChecked = false;
							this.StudentID = 37;
						}
						else
						{
							this.FemaleVictimChecked = false;
							this.StudentID = 39;
						}
						if (this.StudentManager.Students[this.StudentID] != null)
						{
							UnityEngine.Object.Destroy(this.StudentManager.Students[this.StudentID].gameObject);
						}
						this.StudentManager.Students[this.StudentID] = null;
						this.StudentManager.ForceSpawn = true;
						this.StudentManager.SpawnPositions[this.StudentID] = this.SummonSpot;
						this.StudentManager.SpawnID = this.StudentID;
						this.StudentManager.SpawnStudent(this.StudentManager.SpawnID);
						this.StudentManager.SpawnID = 0;
						this.Police.Corpses -= this.Victims;
						this.Victims = 0;
						this.JustSummoned = true;
						this.Exit();
					}
				}
				else if (this.Selected == 4)
				{
					this.SpawnWeapons();
					this.Exit();
				}
				else if (this.Selected == 5)
				{
					if (this.Yandere.Gloved)
					{
						this.Yandere.Gloves.Blood.enabled = false;
					}
					if (this.Yandere.CurrentUniformOrigin == 1)
					{
						this.StudentManager.OriginalUniforms++;
					}
					else
					{
						this.StudentManager.NewUniforms++;
					}
					this.Police.BloodyClothing = 0;
					this.Yandere.Bloodiness = 0f;
					this.Yandere.Sanity = 100f;
					this.Exit();
				}
				else if (this.Selected == 6)
				{
					this.WeaponManager.CleanWeapons();
					this.Exit();
				}
				else if (this.Selected == 8)
				{
					this.Exit();
				}
			}
		}
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x00135FDC File Offset: 0x001343DC
	private void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 8;
		}
		else if (this.Selected > 8)
		{
			this.Selected = 1;
		}
		this.Highlight.transform.localPosition = new Vector3(this.Highlight.transform.localPosition.x, 225f - 50f * (float)this.Selected, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x0013606C File Offset: 0x0013446C
	private void Exit()
	{
		this.Selected = 1;
		this.UpdateHighlight();
		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.StudentManager.EnablePrompts();
		this.Yandere.TargetStudent = null;
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.Show = false;
		this.Uses++;
		if (this.Uses > 9)
		{
			this.FemaleTurtle.SetActive(true);
		}
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x001360F4 File Offset: 0x001344F4
	public void SpawnWeapons()
	{
		for (int i = 1; i < 6; i++)
		{
			if (this.Weapon[i] != null)
			{
				this.Weapon[i].transform.position = this.WeaponSpot[i].position;
			}
		}
	}

	// Token: 0x04002883 RID: 10371
	public StudentManagerScript StudentManager;

	// Token: 0x04002884 RID: 10372
	public WeaponManagerScript WeaponManager;

	// Token: 0x04002885 RID: 10373
	public InputManagerScript InputManager;

	// Token: 0x04002886 RID: 10374
	public PromptBarScript PromptBar;

	// Token: 0x04002887 RID: 10375
	public StudentScript Student;

	// Token: 0x04002888 RID: 10376
	public YandereScript Yandere;

	// Token: 0x04002889 RID: 10377
	public PoliceScript Police;

	// Token: 0x0400288A RID: 10378
	public UILabel SanityLabel;

	// Token: 0x0400288B RID: 10379
	public UILabel VictimLabel;

	// Token: 0x0400288C RID: 10380
	public PromptScript GenderPrompt;

	// Token: 0x0400288D RID: 10381
	public PromptScript Prompt;

	// Token: 0x0400288E RID: 10382
	public Transform PrayWindow;

	// Token: 0x0400288F RID: 10383
	public Transform SummonSpot;

	// Token: 0x04002890 RID: 10384
	public Transform Highlight;

	// Token: 0x04002891 RID: 10385
	public Transform[] WeaponSpot;

	// Token: 0x04002892 RID: 10386
	public GameObject[] Weapon;

	// Token: 0x04002893 RID: 10387
	public GameObject FemaleTurtle;

	// Token: 0x04002894 RID: 10388
	public int StudentNumber;

	// Token: 0x04002895 RID: 10389
	public int StudentID;

	// Token: 0x04002896 RID: 10390
	public int Selected;

	// Token: 0x04002897 RID: 10391
	public int Victims;

	// Token: 0x04002898 RID: 10392
	public int Uses;

	// Token: 0x04002899 RID: 10393
	public bool FemaleVictimChecked;

	// Token: 0x0400289A RID: 10394
	public bool MaleVictimChecked;

	// Token: 0x0400289B RID: 10395
	public bool JustSummoned;

	// Token: 0x0400289C RID: 10396
	public bool SpawnMale;

	// Token: 0x0400289D RID: 10397
	public bool Show;
}
