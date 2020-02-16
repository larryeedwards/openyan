using System;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class PortalScript : MonoBehaviour
{
	// Token: 0x06001E9C RID: 7836 RVA: 0x0012F460 File Offset: 0x0012D860
	private void Start()
	{
		this.ClassDarkness.enabled = false;
	}

	// Token: 0x06001E9D RID: 7837 RVA: 0x0012F470 File Offset: 0x0012D870
	private void Update()
	{
		if (this.Clock.HourTime > 8.52f && this.Clock.HourTime < 8.53f && !this.Yandere.InClass && !this.LateReport1)
		{
			this.LateReport1 = true;
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Late);
		}
		if (this.Clock.HourTime > 13.52f && this.Clock.HourTime < 13.53f && !this.Yandere.InClass && !this.LateReport2)
		{
			this.LateReport2 = true;
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Late);
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.CheckForLateness();
			this.Reputation.UpdateRep();
			bool flag = false;
			if (this.Police.PoisonScene || (this.Police.SuicideScene && this.Police.Corpses - this.Police.HiddenCorpses > 0) || this.Police.Corpses - this.Police.HiddenCorpses > 0 || this.Reputation.Reputation <= -100f)
			{
				this.EndDay();
			}
			else if (this.Clock.HourTime < 15.5f)
			{
				if (!this.Police.Show)
				{
					bool flag2 = false;
					if (this.StudentManager.Teachers[21] != null && this.StudentManager.Teachers[21].DistanceToDestination < 1f)
					{
						flag2 = true;
					}
					if (flag2)
					{
					}
					if (this.Late > 0 && flag2)
					{
						this.Yandere.Subtitle.UpdateLabel(SubtitleType.TeacherLateReaction, this.Late, 5.5f);
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.ShoulderCamera.Scolding = true;
						this.Yandere.ShoulderCamera.Teacher = this.Teacher;
					}
					else
					{
						this.ClassDarkness.enabled = true;
						this.Transition = true;
						this.FadeOut = true;
					}
					this.Clock.StopTime = true;
				}
				else
				{
					this.EndDay();
				}
			}
			else if (this.Yandere.Inventory.RivalPhone && !this.StudentManager.RivalEliminated)
			{
				this.Yandere.NotificationManager.CustomText = "Put your rival's phone on her desk!";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				this.Yandere.NotificationManager.CustomText = "You are carrying stolen property!";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				flag = true;
			}
			else
			{
				this.EndDay();
			}
			if (!flag)
			{
				this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
				this.Yandere.YandereVision = false;
				this.Yandere.CanMove = false;
				if (this.Clock.HourTime < 15.5f)
				{
					this.Yandere.InClass = true;
					if (this.Clock.HourTime < 8.5f)
					{
						this.EndEvents();
					}
					else
					{
						this.EndLaterEvents();
					}
				}
			}
		}
		if (this.Transition)
		{
			if (this.FadeOut)
			{
				if (this.LoveManager.HoldingHands)
				{
					this.LoveManager.Rival.transform.position = new Vector3(0f, 0f, -50f);
				}
				this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, this.ClassDarkness.color.a + Time.deltaTime);
				if (this.ClassDarkness.color.a >= 1f)
				{
					if (this.Yandere.Resting)
					{
						this.Yandere.IdleAnim = "f02_idleShort_00";
						this.Yandere.WalkAnim = "f02_newWalk_00";
						this.Yandere.OriginalIdleAnim = this.Yandere.IdleAnim;
						this.Yandere.OriginalWalkAnim = this.Yandere.WalkAnim;
						this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
						this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0f);
						this.Yandere.Resting = false;
						this.Yandere.Health = 10;
						this.FadeOut = false;
						this.Proceed = true;
					}
					else
					{
						if (this.Yandere.Armed)
						{
							this.Yandere.Unequip();
						}
						this.HeartbeatCamera.SetActive(false);
						this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, 1f);
						this.FadeOut = false;
						this.Proceed = false;
						this.Yandere.RPGCamera.enabled = false;
						this.PromptBar.Label[4].text = "Choose";
						this.PromptBar.Label[5].text = "Allocate";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
						this.Class.StudyPoints = ((PlayerGlobals.PantiesEquipped != 11) ? 5 : 10);
						this.Class.StudyPoints -= this.Late;
						this.Class.UpdateLabel();
						this.Class.gameObject.SetActive(true);
						this.Class.Show = true;
						if (this.Police.Show)
						{
							this.Police.Timer = 1E-06f;
						}
					}
				}
			}
			else if (this.Proceed)
			{
				if (this.ClassDarkness.color.a >= 1f)
				{
					this.HeartbeatCamera.SetActive(true);
					this.Clock.enabled = true;
					this.Yandere.FixCamera();
					this.Yandere.RPGCamera.enabled = false;
					if (this.Clock.HourTime < 13f)
					{
						if (this.StudentManager.Bully)
						{
							this.StudentManager.UpdateGrafitti();
						}
						this.Yandere.Incinerator.Timer -= 780f - this.Clock.PresentTime;
						this.DelinquentManager.CheckTime();
						this.Clock.PresentTime = 780f;
					}
					else
					{
						this.Yandere.Incinerator.Timer -= 930f - this.Clock.PresentTime;
						this.DelinquentManager.CheckTime();
						this.Clock.PresentTime = 930f;
					}
					this.Clock.HourTime = this.Clock.PresentTime / 60f;
					this.StudentManager.AttendClass();
				}
				this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, this.ClassDarkness.color.a - Time.deltaTime);
				if (this.ClassDarkness.color.a <= 0f)
				{
					this.ClassDarkness.enabled = false;
					this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, 0f);
					this.Clock.StopTime = false;
					this.Transition = false;
					this.Proceed = false;
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.InClass = false;
					this.Yandere.CanMove = true;
					this.StudentManager.ResumeMovement();
					if (!MissionModeGlobals.MissionMode)
					{
						if (this.Headmaster.activeInHierarchy)
						{
							this.Headmaster.SetActive(false);
						}
						else
						{
							this.Headmaster.SetActive(true);
						}
					}
				}
			}
		}
		if (this.Clock.HourTime > 15.5f)
		{
			if (base.transform.position.z < 0f)
			{
				this.StudentManager.RemovePapersFromDesks();
				if (!MissionModeGlobals.MissionMode)
				{
					this.MapMarker.material.mainTexture = this.HomeMapMarker;
					base.transform.position = new Vector3(0f, 1f, -75f);
					this.Prompt.Label[0].text = "     Go Home";
					this.Prompt.enabled = true;
				}
				else
				{
					base.transform.position = new Vector3(0f, -10f, 0f);
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
		}
		else if (!this.Yandere.Police.FadeOut && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 1.4f)
		{
			this.CanAttendClass = true;
			this.CheckForProblems();
			if (!this.CanAttendClass)
			{
				if (this.Timer == 0f)
				{
					if (this.Yandere.Armed)
					{
						this.Yandere.NotificationManager.CustomText = "Carrying Weapon";
					}
					else if (this.Yandere.Bloodiness > 0f)
					{
						this.Yandere.NotificationManager.CustomText = "Bloody";
					}
					else if (this.Yandere.Sanity < 33.333f)
					{
						this.Yandere.NotificationManager.CustomText = "Visibly Insane";
					}
					else if (this.Yandere.Attacking)
					{
						this.Yandere.NotificationManager.CustomText = "In Combat";
					}
					else if (this.Yandere.Dragging || this.Yandere.Carrying)
					{
						this.Yandere.NotificationManager.CustomText = "Holding Corpse";
					}
					else if (this.Yandere.PickUp != null)
					{
						this.Yandere.NotificationManager.CustomText = "Carrying Item";
					}
					else if (this.Yandere.Chased || this.Yandere.Chasers > 0)
					{
						this.Yandere.NotificationManager.CustomText = "Chased";
					}
					else if (this.StudentManager.Reporter)
					{
						this.Yandere.NotificationManager.CustomText = "Murder being reported";
					}
					else if (this.StudentManager.MurderTakingPlace)
					{
						this.Yandere.NotificationManager.CustomText = "Murder taking place";
					}
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					this.Yandere.NotificationManager.CustomText = "Cannot attend class. Reason:";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				}
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Timer += Time.deltaTime;
				if (this.Timer > 5f)
				{
					this.Timer = 0f;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x00130128 File Offset: 0x0012E528
	public void CheckForProblems()
	{
		if (this.Yandere.Armed || this.Yandere.Bloodiness > 0f || this.Yandere.Sanity < 33.333f || this.Yandere.Attacking || this.Yandere.Dragging || this.Yandere.Carrying || this.Yandere.PickUp != null || this.Yandere.Chased || this.Yandere.Chasers > 0 || this.StudentManager.Reporter != null || this.StudentManager.MurderTakingPlace)
		{
			this.CanAttendClass = false;
		}
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x00130204 File Offset: 0x0012E604
	public void EndDay()
	{
		this.StudentManager.StopMoving();
		this.Yandere.StopLaughing();
		this.Yandere.EmptyHands();
		this.Clock.StopTime = true;
		this.Police.Darkness.enabled = true;
		this.Police.FadeOut = true;
		this.Police.DayOver = true;
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x00130268 File Offset: 0x0012E668
	private void CheckForLateness()
	{
		this.Late = 0;
		if (this.Clock.HourTime < 13f)
		{
			if (this.Clock.HourTime < 8.52f)
			{
				this.Late = 0;
			}
			else if (this.Clock.HourTime < 10f)
			{
				this.Late = 1;
			}
			else if (this.Clock.HourTime < 11f)
			{
				this.Late = 2;
			}
			else if (this.Clock.HourTime < 12f)
			{
				this.Late = 3;
			}
			else if (this.Clock.HourTime < 13f)
			{
				this.Late = 4;
			}
		}
		else if (this.Clock.HourTime < 13.52f)
		{
			this.Late = 0;
		}
		else if (this.Clock.HourTime < 14f)
		{
			this.Late = 1;
		}
		else if (this.Clock.HourTime < 14.5f)
		{
			this.Late = 2;
		}
		else if (this.Clock.HourTime < 15f)
		{
			this.Late = 3;
		}
		else if (this.Clock.HourTime < 15.5f)
		{
			this.Late = 4;
		}
		this.Reputation.PendingRep -= (float)(5 * this.Late);
		if (this.Late > 0)
		{
		}
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x00130404 File Offset: 0x0012E804
	public void EndEvents()
	{
		for (int i = 0; i < this.MorningEvents.Length; i++)
		{
			if (this.MorningEvents[i].enabled)
			{
				this.MorningEvents[i].EndEvent();
			}
		}
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x00130449 File Offset: 0x0012E849
	public void EndLaterEvents()
	{
	}

	// Token: 0x0400280A RID: 10250
	public RivalMorningEventManagerScript[] MorningEvents;

	// Token: 0x0400280B RID: 10251
	public OsanaMorningFriendEventScript[] FriendEvents;

	// Token: 0x0400280C RID: 10252
	public OsanaMondayBeforeClassEventScript OsanaEvent;

	// Token: 0x0400280D RID: 10253
	public OsanaFridayBeforeClassEvent1Script OsanaFridayEvent1;

	// Token: 0x0400280E RID: 10254
	public OsanaFridayBeforeClassEvent2Script OsanaFridayEvent2;

	// Token: 0x0400280F RID: 10255
	public OsanaFridayLunchEventScript OsanaFridayLunchEvent;

	// Token: 0x04002810 RID: 10256
	public OsanaClubEventScript OsanaClubEvent;

	// Token: 0x04002811 RID: 10257
	public OsanaPoolEventScript OsanaPoolEvent;

	// Token: 0x04002812 RID: 10258
	public DelinquentManagerScript DelinquentManager;

	// Token: 0x04002813 RID: 10259
	public StudentManagerScript StudentManager;

	// Token: 0x04002814 RID: 10260
	public LoveManagerScript LoveManager;

	// Token: 0x04002815 RID: 10261
	public ReputationScript Reputation;

	// Token: 0x04002816 RID: 10262
	public PromptBarScript PromptBar;

	// Token: 0x04002817 RID: 10263
	public YandereScript Yandere;

	// Token: 0x04002818 RID: 10264
	public PoliceScript Police;

	// Token: 0x04002819 RID: 10265
	public PromptScript Prompt;

	// Token: 0x0400281A RID: 10266
	public ClassScript Class;

	// Token: 0x0400281B RID: 10267
	public ClockScript Clock;

	// Token: 0x0400281C RID: 10268
	public GameObject HeartbeatCamera;

	// Token: 0x0400281D RID: 10269
	public GameObject Headmaster;

	// Token: 0x0400281E RID: 10270
	public UISprite ClassDarkness;

	// Token: 0x0400281F RID: 10271
	public Texture HomeMapMarker;

	// Token: 0x04002820 RID: 10272
	public Renderer MapMarker;

	// Token: 0x04002821 RID: 10273
	public Transform Teacher;

	// Token: 0x04002822 RID: 10274
	public bool CanAttendClass;

	// Token: 0x04002823 RID: 10275
	public bool LateReport1;

	// Token: 0x04002824 RID: 10276
	public bool LateReport2;

	// Token: 0x04002825 RID: 10277
	public bool Transition;

	// Token: 0x04002826 RID: 10278
	public bool FadeOut;

	// Token: 0x04002827 RID: 10279
	public bool Proceed;

	// Token: 0x04002828 RID: 10280
	public float Timer;

	// Token: 0x04002829 RID: 10281
	public int Late;
}
