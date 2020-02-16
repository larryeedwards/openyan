using System;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class ComputerGamesScript : MonoBehaviour
{
	// Token: 0x06001812 RID: 6162 RVA: 0x000C6E90 File Offset: 0x000C5290
	private void Start()
	{
		this.GameWindow.gameObject.SetActive(false);
		this.DeactivateAllBenefits();
		this.OriginalColor = this.Yandere.PowerUp.color;
		if (ClubGlobals.Club == ClubType.Gaming)
		{
			this.EnableGames();
		}
		else
		{
			this.DisableGames();
		}
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x000C6EE8 File Offset: 0x000C52E8
	private void Update()
	{
		if (this.ShowWindow)
		{
			this.GameWindow.localScale = Vector3.Lerp(this.GameWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (this.InputManager.TappedUp)
			{
				this.Subject--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Subject++;
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				this.ShowWindow = false;
				this.PlayGames();
				this.PromptBar.ClearButtons();
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}
			if (Input.GetButtonDown("B"))
			{
				this.Yandere.CanMove = true;
				this.ShowWindow = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = false;
			}
		}
		else if (this.GameWindow.localScale.x > 0.1f)
		{
			this.GameWindow.localScale = Vector3.Lerp(this.GameWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
		else
		{
			this.GameWindow.localScale = Vector3.zero;
			this.GameWindow.gameObject.SetActive(false);
		}
		if (this.Gaming)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.ComputerGames[this.GameID].transform.position.x, this.Yandere.transform.position.y, this.ComputerGames[this.GameID].transform.position.z) - this.Yandere.transform.position);
			this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
			this.Yandere.MoveTowardsTarget(new Vector3(24.32233f, 4f, 12.58998f));
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f)
			{
				this.Yandere.PowerUp.transform.parent.gameObject.SetActive(true);
				this.Yandere.MyController.radius = 0.2f;
				this.Yandere.CanMove = true;
				this.Yandere.EmptyHands();
				this.Gaming = false;
				this.ActivateBenefit();
			}
		}
		else if (this.Timer < 5f)
		{
			this.ID = 1;
			while (this.ID < this.ComputerGames.Length)
			{
				PromptScript promptScript = this.ComputerGames[this.ID];
				if (promptScript.Circle[0].fillAmount == 0f)
				{
					promptScript.Circle[0].fillAmount = 1f;
					if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
					{
						this.GameID = this.ID;
						if (this.ID == 1)
						{
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Confirm";
							this.PromptBar.Label[1].text = "Back";
							this.PromptBar.Label[4].text = "Select";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
							this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.IdleAnim);
							this.Yandere.CanMove = false;
							this.GameWindow.gameObject.SetActive(true);
							this.ShowWindow = true;
						}
						else
						{
							this.PlayGames();
						}
					}
				}
				this.ID++;
			}
		}
		if (this.Yandere.PowerUp.gameObject.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			this.Yandere.PowerUp.transform.localPosition = new Vector3(this.Yandere.PowerUp.transform.localPosition.x, this.Yandere.PowerUp.transform.localPosition.y + Time.deltaTime, this.Yandere.PowerUp.transform.localPosition.z);
			this.Yandere.PowerUp.transform.LookAt(this.MainCamera.position);
			this.Yandere.PowerUp.transform.localEulerAngles = new Vector3(this.Yandere.PowerUp.transform.localEulerAngles.x, this.Yandere.PowerUp.transform.localEulerAngles.y + 180f, this.Yandere.PowerUp.transform.localEulerAngles.z);
			if (this.Yandere.PowerUp.color != new Color(1f, 1f, 1f, 1f))
			{
				this.Yandere.PowerUp.color = this.OriginalColor;
			}
			else
			{
				this.Yandere.PowerUp.color = new Color(1f, 1f, 1f, 1f);
			}
			if (this.Timer > 6f)
			{
				this.Yandere.PowerUp.transform.parent.gameObject.SetActive(false);
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x000C7530 File Offset: 0x000C5930
	public void EnableGames()
	{
		for (int i = 1; i < this.ComputerGames.Length; i++)
		{
			this.ComputerGames[i].enabled = true;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x000C7570 File Offset: 0x000C5970
	private void PlayGames()
	{
		this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_playingGames_00");
		this.Yandere.MyController.radius = 0.1f;
		this.Yandere.CanMove = false;
		this.Gaming = true;
		this.DisableGames();
		this.UpdateImage();
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000C75CB File Offset: 0x000C59CB
	private void UpdateImage()
	{
		this.MyTexture.mainTexture = this.Textures[this.Subject];
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x000C75E8 File Offset: 0x000C59E8
	public void DisableGames()
	{
		for (int i = 1; i < this.ComputerGames.Length; i++)
		{
			this.ComputerGames[i].enabled = false;
			this.ComputerGames[i].Hide();
		}
		if (!this.Gaming)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x000C7640 File Offset: 0x000C5A40
	private void EnableChairs()
	{
		for (int i = 1; i < this.Chairs.Length; i++)
		{
			this.Chairs[i].enabled = true;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x000C7680 File Offset: 0x000C5A80
	private void DisableChairs()
	{
		for (int i = 1; i < this.Chairs.Length; i++)
		{
			this.Chairs[i].enabled = false;
		}
	}

	// Token: 0x0600181A RID: 6170 RVA: 0x000C76B4 File Offset: 0x000C5AB4
	private void ActivateBenefit()
	{
		if (this.Subject == 1)
		{
			ClassGlobals.BiologyBonus = 1;
		}
		else if (this.Subject == 2)
		{
			ClassGlobals.ChemistryBonus = 1;
		}
		else if (this.Subject == 3)
		{
			ClassGlobals.LanguageBonus = 1;
		}
		else if (this.Subject == 4)
		{
			ClassGlobals.PsychologyBonus = 1;
		}
		else if (this.Subject == 5)
		{
			ClassGlobals.PhysicalBonus = 1;
		}
		else if (this.Subject == 6)
		{
			PlayerGlobals.SeductionBonus = 1;
		}
		else if (this.Subject == 7)
		{
			PlayerGlobals.NumbnessBonus = 1;
		}
		else if (this.Subject == 8)
		{
			PlayerGlobals.SocialBonus = 1;
		}
		else if (this.Subject == 9)
		{
			PlayerGlobals.StealthBonus = 1;
		}
		else if (this.Subject == 10)
		{
			PlayerGlobals.SpeedBonus = 1;
		}
		else if (this.Subject == 11)
		{
			PlayerGlobals.EnlightenmentBonus = 1;
		}
		if (this.Poison != null)
		{
			this.Poison.Start();
		}
		this.StudentManager.UpdatePerception();
		this.Yandere.UpdateNumbness();
		this.Police.UpdateCorpses();
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000C77FC File Offset: 0x000C5BFC
	private void DeactivateBenefit()
	{
		if (this.Subject == 1)
		{
			ClassGlobals.BiologyBonus = 0;
		}
		else if (this.Subject == 2)
		{
			ClassGlobals.ChemistryBonus = 0;
		}
		else if (this.Subject == 3)
		{
			ClassGlobals.LanguageBonus = 0;
		}
		else if (this.Subject == 4)
		{
			ClassGlobals.PsychologyBonus = 0;
		}
		else if (this.Subject == 5)
		{
			ClassGlobals.PhysicalBonus = 0;
		}
		else if (this.Subject == 6)
		{
			PlayerGlobals.SeductionBonus = 0;
		}
		else if (this.Subject == 7)
		{
			PlayerGlobals.NumbnessBonus = 0;
		}
		else if (this.Subject == 8)
		{
			PlayerGlobals.SocialBonus = 0;
		}
		else if (this.Subject == 9)
		{
			PlayerGlobals.StealthBonus = 0;
		}
		else if (this.Subject == 10)
		{
			PlayerGlobals.SpeedBonus = 0;
		}
		else if (this.Subject == 11)
		{
			PlayerGlobals.EnlightenmentBonus = 0;
		}
		if (this.Poison != null)
		{
			this.Poison.Start();
		}
		this.StudentManager.UpdatePerception();
		this.Yandere.UpdateNumbness();
		this.Police.UpdateCorpses();
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x000C7944 File Offset: 0x000C5D44
	public void DeactivateAllBenefits()
	{
		ClassGlobals.BiologyBonus = 0;
		ClassGlobals.ChemistryBonus = 0;
		ClassGlobals.LanguageBonus = 0;
		ClassGlobals.PsychologyBonus = 0;
		ClassGlobals.PhysicalBonus = 0;
		PlayerGlobals.SeductionBonus = 0;
		PlayerGlobals.NumbnessBonus = 0;
		PlayerGlobals.SocialBonus = 0;
		PlayerGlobals.StealthBonus = 0;
		PlayerGlobals.SpeedBonus = 0;
		PlayerGlobals.EnlightenmentBonus = 0;
		if (this.Poison != null)
		{
			this.Poison.Start();
		}
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x000C79B0 File Offset: 0x000C5DB0
	private void UpdateHighlight()
	{
		if (this.Subject < 1)
		{
			this.Subject = 11;
		}
		else if (this.Subject > 11)
		{
			this.Subject = 1;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 250f - (float)this.Subject * 50f, this.Highlight.localPosition.z);
		this.DescLabel.text = this.Descriptions[this.Subject];
	}

	// Token: 0x040018F0 RID: 6384
	public PromptScript[] ComputerGames;

	// Token: 0x040018F1 RID: 6385
	public Collider[] Chairs;

	// Token: 0x040018F2 RID: 6386
	public StudentManagerScript StudentManager;

	// Token: 0x040018F3 RID: 6387
	public InputManagerScript InputManager;

	// Token: 0x040018F4 RID: 6388
	public PromptBarScript PromptBar;

	// Token: 0x040018F5 RID: 6389
	public YandereScript Yandere;

	// Token: 0x040018F6 RID: 6390
	public PoliceScript Police;

	// Token: 0x040018F7 RID: 6391
	public PoisonScript Poison;

	// Token: 0x040018F8 RID: 6392
	public Quaternion targetRotation;

	// Token: 0x040018F9 RID: 6393
	public Transform GameWindow;

	// Token: 0x040018FA RID: 6394
	public Transform MainCamera;

	// Token: 0x040018FB RID: 6395
	public Transform Highlight;

	// Token: 0x040018FC RID: 6396
	public bool ShowWindow;

	// Token: 0x040018FD RID: 6397
	public bool Gaming;

	// Token: 0x040018FE RID: 6398
	public float Timer;

	// Token: 0x040018FF RID: 6399
	public int Subject = 1;

	// Token: 0x04001900 RID: 6400
	public int GameID;

	// Token: 0x04001901 RID: 6401
	public int ID = 1;

	// Token: 0x04001902 RID: 6402
	public Color OriginalColor;

	// Token: 0x04001903 RID: 6403
	public string[] Descriptions;

	// Token: 0x04001904 RID: 6404
	public UITexture MyTexture;

	// Token: 0x04001905 RID: 6405
	public Texture[] Textures;

	// Token: 0x04001906 RID: 6406
	public UILabel DescLabel;
}
