﻿using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class PickpocketScript : MonoBehaviour
{
	// Token: 0x06001E77 RID: 7799 RVA: 0x0012B390 File Offset: 0x00129790
	private void Start()
	{
		if (this.Student.StudentID != 71)
		{
			this.Prompt.transform.parent.gameObject.SetActive(false);
			base.enabled = false;
		}
		else
		{
			this.PickpocketMinigame = this.Student.StudentManager.PickpocketMinigame;
			if (this.Student.StudentID == this.Student.StudentManager.NurseID)
			{
				this.ID = 2;
			}
			else if (ClubGlobals.GetClubClosed(this.Student.OriginalClub))
			{
				this.Prompt.transform.parent.gameObject.SetActive(false);
				base.enabled = false;
			}
			else
			{
				this.Prompt.Label[3].text = "     Steal Shed Key";
				this.NotNurse = true;
			}
		}
	}

	// Token: 0x06001E78 RID: 7800 RVA: 0x0012B474 File Offset: 0x00129874
	private void Update()
	{
		if (this.Prompt.transform.parent != null)
		{
			if (this.Student.Routine)
			{
				if (this.Student.DistanceToDestination > 0.5f)
				{
					if (this.Prompt.enabled)
					{
						this.Prompt.Hide();
						this.Prompt.enabled = false;
						this.PickpocketPanel.enabled = false;
					}
					if (this.Student.Yandere.Pickpocketing && this.PickpocketMinigame.ID == this.ID)
					{
						this.Prompt.Yandere.Caught = true;
						this.PickpocketMinigame.End();
						this.Punish();
					}
				}
				else
				{
					this.PickpocketPanel.enabled = true;
					if (this.Student.Yandere.PickUp == null && this.Student.Yandere.Pursuer == null)
					{
						this.Prompt.enabled = true;
					}
					else
					{
						this.Prompt.enabled = false;
						this.Prompt.Hide();
					}
					this.Timer += Time.deltaTime * this.Student.CharacterAnimation[this.Student.PatrolAnim].speed;
					this.TimeBar.fillAmount = 1f - this.Timer / this.Student.CharacterAnimation[this.Student.PatrolAnim].length;
					if (this.Timer > this.Student.CharacterAnimation[this.Student.PatrolAnim].length)
					{
						if (this.Student.Yandere.Pickpocketing && this.PickpocketMinigame.ID == this.ID)
						{
							this.Prompt.Yandere.Caught = true;
							this.PickpocketMinigame.End();
							this.Punish();
						}
						this.Timer = 0f;
					}
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.PickpocketPanel.enabled = false;
				if (this.Student.Yandere.Pickpocketing && this.PickpocketMinigame.ID == this.ID)
				{
					this.Prompt.Yandere.Caught = true;
					this.PickpocketMinigame.End();
					this.Punish();
				}
			}
			if (this.Prompt.Circle[3].fillAmount == 0f)
			{
				this.Prompt.Circle[3].fillAmount = 1f;
				if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
				{
					this.PickpocketMinigame.StartingAlerts = this.Prompt.Yandere.Alerts;
					this.PickpocketMinigame.PickpocketSpot = this.PickpocketSpot;
					this.PickpocketMinigame.NotNurse = this.NotNurse;
					this.PickpocketMinigame.Show = true;
					this.PickpocketMinigame.ID = this.ID;
					this.Student.Yandere.CharacterAnimation.CrossFade("f02_pickpocketing_00");
					this.Student.Yandere.Pickpocketing = true;
					this.Student.Yandere.EmptyHands();
					this.Student.Yandere.CanMove = false;
				}
			}
			if (this.PickpocketMinigame != null && this.PickpocketMinigame.ID == this.ID)
			{
				if (this.PickpocketMinigame.Success)
				{
					this.PickpocketMinigame.Success = false;
					this.PickpocketMinigame.ID = 0;
					this.Succeed();
					this.PickpocketPanel.enabled = false;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					this.Key.SetActive(false);
					base.enabled = false;
				}
				if (this.PickpocketMinigame.Failure)
				{
					this.PickpocketMinigame.Failure = false;
					this.PickpocketMinigame.ID = 0;
					this.Punish();
				}
			}
			if (!this.Student.Alive)
			{
				base.transform.position = new Vector3(this.Student.transform.position.x, this.Student.transform.position.y + 1f, this.Student.transform.position.z);
				this.Prompt.gameObject.GetComponent<BoxCollider>().isTrigger = false;
				this.Prompt.gameObject.GetComponent<Rigidbody>().isKinematic = false;
				this.Prompt.gameObject.GetComponent<Rigidbody>().useGravity = true;
				this.Prompt.enabled = true;
				base.transform.parent = null;
			}
		}
		else if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			this.Succeed();
			this.Prompt.Hide();
			this.PickpocketPanel.enabled = false;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			this.Key.SetActive(false);
			base.enabled = false;
		}
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x0012BA10 File Offset: 0x00129E10
	private void Punish()
	{
		Debug.Log("Punishing Yandere-chan for pickpocketing.");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, this.Student.Yandere.transform.position + Vector3.up, Quaternion.identity);
		gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
		if (!this.NotNurse && !this.Prompt.Yandere.Egg)
		{
			Debug.Log("A faculty member saw pickpocketing.");
			this.Student.Witnessed = StudentWitnessType.Theft;
			this.Student.SenpaiNoticed();
			this.Student.CameraEffects.MurderWitnessed();
			this.Student.Concern = 5;
		}
		else
		{
			this.Student.Witnessed = StudentWitnessType.Pickpocketing;
			this.Student.CameraEffects.Alarm();
			this.Student.Alarm += 200f;
		}
		this.Timer = 0f;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.PickpocketPanel.enabled = false;
		this.Student.CharacterAnimation[this.Student.PatrolAnim].time = 0f;
		this.Student.PatrolTimer = 0f;
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x0012BB60 File Offset: 0x00129F60
	private void Succeed()
	{
		if (this.ID == 1)
		{
			this.Student.StudentManager.ShedDoor.Prompt.Label[0].text = "     Open";
			this.Student.StudentManager.ShedDoor.Locked = false;
			this.Student.ClubManager.Padlock.SetActive(false);
			this.Student.Yandere.Inventory.ShedKey = true;
		}
		else
		{
			this.Student.StudentManager.CabinetDoor.Prompt.Label[0].text = "     Open";
			this.Student.StudentManager.CabinetDoor.Locked = false;
			this.Student.Yandere.Inventory.CabinetKey = true;
		}
	}

	// Token: 0x04002762 RID: 10082
	public PickpocketMinigameScript PickpocketMinigame;

	// Token: 0x04002763 RID: 10083
	public StudentScript Student;

	// Token: 0x04002764 RID: 10084
	public PromptScript Prompt;

	// Token: 0x04002765 RID: 10085
	public UIPanel PickpocketPanel;

	// Token: 0x04002766 RID: 10086
	public UISprite TimeBar;

	// Token: 0x04002767 RID: 10087
	public Transform PickpocketSpot;

	// Token: 0x04002768 RID: 10088
	public GameObject AlarmDisc;

	// Token: 0x04002769 RID: 10089
	public GameObject Key;

	// Token: 0x0400276A RID: 10090
	public float Timer;

	// Token: 0x0400276B RID: 10091
	public int ID = 1;

	// Token: 0x0400276C RID: 10092
	public bool NotNurse;

	// Token: 0x0400276D RID: 10093
	public bool Test;
}
