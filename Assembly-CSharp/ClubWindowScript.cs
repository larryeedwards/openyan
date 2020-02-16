using System;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class ClubWindowScript : MonoBehaviour
{
	// Token: 0x060017FF RID: 6143 RVA: 0x000C3DB0 File Offset: 0x000C21B0
	private void Start()
	{
		this.Window.SetActive(false);
		if (SchoolGlobals.SchoolAtmosphere <= 0.9f)
		{
			this.ActivityDescs[7] = this.LowAtmosphereDesc;
		}
		else if (SchoolGlobals.SchoolAtmosphere <= 0.8f)
		{
			this.ActivityDescs[7] = this.MedAtmosphereDesc;
		}
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x000C3E08 File Offset: 0x000C2208
	private void Update()
	{
		if (this.Window.activeInHierarchy)
		{
			if (this.Timer > 0.5f)
			{
				if (Input.GetButtonDown("A"))
				{
					if (!this.Quitting && !this.Activity)
					{
						ClubGlobals.Club = this.Club;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
						this.ClubManager.ActivateClubBenefit();
					}
					else if (this.Quitting)
					{
						this.ClubManager.DeactivateClubBenefit();
						ClubGlobals.SetQuitClub(this.Club, true);
						ClubGlobals.Club = ClubType.None;
						this.Yandere.ClubAccessory();
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
						this.Yandere.StudentManager.UpdateBooths();
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
					}
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 2;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}
				if (Input.GetButtonDown("B"))
				{
					if (!this.Quitting && !this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubJoin;
					}
					else if (this.Quitting)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubQuit;
						this.Quitting = false;
					}
					else if (this.Activity)
					{
						this.Yandere.TargetStudent.Interaction = StudentInteractionType.ClubActivity;
						this.Activity = false;
					}
					this.Yandere.TargetStudent.TalkTimer = 100f;
					this.Yandere.TargetStudent.ClubPhase = 3;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					this.Window.SetActive(false);
				}
				if (Input.GetButtonDown("X") && !this.Quitting && !this.Activity)
				{
					if (!this.Warning.activeInHierarchy)
					{
						this.ClubInfo.SetActive(false);
						this.Warning.SetActive(true);
					}
					else
					{
						this.ClubInfo.SetActive(true);
						this.Warning.SetActive(false);
					}
				}
			}
			this.Timer += Time.deltaTime;
		}
		if (this.PerformingActivity)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(this.ActivityWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
		}
		else if (this.ActivityWindow.localScale.x > 0.1f)
		{
			this.ActivityWindow.localScale = Vector3.Lerp(this.ActivityWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
		else if (this.ActivityWindow.localScale.x != 0f)
		{
			this.ActivityWindow.localScale = Vector3.zero;
		}
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x000C416C File Offset: 0x000C256C
	public void UpdateWindow()
	{
		this.ClubName.text = this.ClubNames[(int)this.Club];
		if (!this.Quitting && !this.Activity)
		{
			this.ClubDesc.text = this.ClubDescs[(int)this.Club];
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Refuse";
			this.PromptBar.Label[2].text = "More Info";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you join the " + this.ClubNames[(int)this.Club] + "?";
		}
		else if (this.Activity)
		{
			this.ClubDesc.text = "Club activities last until 6:00 PM. If you choose to participate in club activities now, the day will end.\n\nIf you don't join by 5:30 PM, you won't be able to participate in club activities today.\n\nIf you don't participate in club activities at least once a week, you will be removed from the club.";
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Yes";
			this.PromptBar.Label[1].text = "No";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you participate in club activities?";
		}
		else if (this.Quitting)
		{
			this.ClubDesc.text = "Are you sure you want to quit this club? If you quit, you will never be allowed to return.";
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.Label[1].text = "Deny";
			this.PromptBar.UpdateButtons();
			this.PromptBar.Show = true;
			this.BottomLabel.text = "Will you quit the " + this.ClubNames[(int)this.Club] + "?";
		}
		this.ClubInfo.SetActive(true);
		this.Warning.SetActive(false);
		this.Window.SetActive(true);
		this.Timer = 0f;
	}

	// Token: 0x040018AD RID: 6317
	public ClubManagerScript ClubManager;

	// Token: 0x040018AE RID: 6318
	public PromptBarScript PromptBar;

	// Token: 0x040018AF RID: 6319
	public YandereScript Yandere;

	// Token: 0x040018B0 RID: 6320
	public Transform ActivityWindow;

	// Token: 0x040018B1 RID: 6321
	public GameObject ClubInfo;

	// Token: 0x040018B2 RID: 6322
	public GameObject Window;

	// Token: 0x040018B3 RID: 6323
	public GameObject Warning;

	// Token: 0x040018B4 RID: 6324
	public string[] ActivityDescs;

	// Token: 0x040018B5 RID: 6325
	public string[] ClubNames;

	// Token: 0x040018B6 RID: 6326
	public string[] ClubDescs;

	// Token: 0x040018B7 RID: 6327
	public string MedAtmosphereDesc;

	// Token: 0x040018B8 RID: 6328
	public string LowAtmosphereDesc;

	// Token: 0x040018B9 RID: 6329
	public UILabel ActivityLabel;

	// Token: 0x040018BA RID: 6330
	public UILabel BottomLabel;

	// Token: 0x040018BB RID: 6331
	public UILabel ClubName;

	// Token: 0x040018BC RID: 6332
	public UILabel ClubDesc;

	// Token: 0x040018BD RID: 6333
	public bool PerformingActivity;

	// Token: 0x040018BE RID: 6334
	public bool Activity;

	// Token: 0x040018BF RID: 6335
	public bool Quitting;

	// Token: 0x040018C0 RID: 6336
	public float Timer;

	// Token: 0x040018C1 RID: 6337
	public ClubType Club;
}
