using System;
using UnityEngine;

// Token: 0x020003C1 RID: 961
public class EventManagerScript : MonoBehaviour
{
	// Token: 0x06001966 RID: 6502 RVA: 0x000ECA30 File Offset: 0x000EAE30
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		if (DateGlobals.Weekday == DayOfWeek.Monday)
		{
			this.EventCheck = true;
		}
		if (this.OsanaID == 3)
		{
			if (DateGlobals.Weekday != DayOfWeek.Thursday)
			{
				base.enabled = false;
			}
			else
			{
				this.EventCheck = true;
			}
		}
		this.NoteLocker.Prompt.enabled = true;
		this.NoteLocker.CanLeaveNote = true;
		if (this.EventStudent1 == 11)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x06001967 RID: 6503 RVA: 0x000ECAC0 File Offset: 0x000EAEC0
	private void Update()
	{
		if (!this.Clock.StopTime && this.EventCheck && this.Clock.HourTime > this.StartTime)
		{
			if (this.EventStudent[1] == null)
			{
				this.EventStudent[1] = this.StudentManager.Students[this.EventStudent1];
			}
			else if (!this.EventStudent[1].Alive)
			{
				this.EventCheck = false;
				base.enabled = false;
			}
			if (this.EventStudent[2] == null)
			{
				this.EventStudent[2] = this.StudentManager.Students[this.EventStudent2];
			}
			else if (!this.EventStudent[2].Alive)
			{
				this.EventCheck = false;
				base.enabled = false;
			}
			if (this.EventStudent[1] != null && this.EventStudent[2] != null && !this.EventStudent[1].Slave && !this.EventStudent[2].Slave && this.EventStudent[1].Indoors && !this.EventStudent[1].Wet && (this.OsanaID < 2 || (this.OsanaID > 1 && Vector3.Distance(this.EventStudent[1].transform.position, this.EventLocation[1].position) < 1f)))
			{
				this.StartTimer += Time.deltaTime;
				if (this.StartTimer > 1f && this.EventStudent[1].Routine && this.EventStudent[2].Routine && !this.EventStudent[1].InEvent && !this.EventStudent[2].InEvent)
				{
					this.EventStudent[1].CurrentDestination = this.EventLocation[1];
					this.EventStudent[1].Pathfinding.target = this.EventLocation[1];
					this.EventStudent[1].EventManager = this;
					this.EventStudent[1].InEvent = true;
					this.EventStudent[1].EmptyHands();
					if (!this.Osana)
					{
						this.EventStudent[2].CurrentDestination = this.EventLocation[2];
						this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
						this.EventStudent[2].EventManager = this;
						this.EventStudent[2].InEvent = true;
					}
					else
					{
						Debug.Log("One of Osana's ''talk privately with Raibaru'' events is beginning.");
					}
					this.EventStudent[2].EmptyHands();
					this.EventStudent[1].SpeechLines.Stop();
					this.EventStudent[2].SpeechLines.Stop();
					this.EventCheck = false;
					this.EventOn = true;
				}
			}
		}
		if (this.EventOn)
		{
			float num = Vector3.Distance(this.Yandere.transform.position, this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position);
			if (this.Clock.HourTime > this.EndTime || this.EventStudent[1].WitnessedCorpse || this.EventStudent[2].WitnessedCorpse || this.EventStudent[1].Dying || this.EventStudent[2].Dying || this.EventStudent[1].Splashed || this.EventStudent[2].Splashed || this.EventStudent[1].Alarmed || this.EventStudent[2].Alarmed)
			{
				this.EndEvent();
			}
			else
			{
				if (this.Osana && this.EventStudent[1].DistanceToDestination < 1f)
				{
					this.EventStudent[2].CurrentDestination = this.EventLocation[2];
					this.EventStudent[2].Pathfinding.target = this.EventLocation[2];
					this.EventStudent[2].EventManager = this;
					this.EventStudent[2].InEvent = true;
				}
				if (!this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[1].Private)
				{
					this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
					this.EventStudent[1].Private = true;
					this.StudentManager.UpdateStudents(0);
				}
				if (Vector3.Distance(this.EventStudent[2].transform.position, this.EventLocation[2].position) < 1f && !this.EventStudent[2].Pathfinding.canMove && !this.StopWalking)
				{
					this.StopWalking = true;
					this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
					this.EventStudent[2].Private = true;
					this.StudentManager.UpdateStudents(0);
				}
				if (this.StopWalking && this.EventPhase == 1)
				{
					this.EventStudent[2].CharacterAnimation.CrossFade(this.EventStudent[2].IdleAnim);
				}
				if (Vector3.Distance(this.EventStudent[1].transform.position, this.EventLocation[1].position) < 1f && !this.EventStudent[1].Pathfinding.canMove && !this.EventStudent[2].Pathfinding.canMove)
				{
					if (this.EventPhase == 1)
					{
						this.EventStudent[1].CharacterAnimation.CrossFade(this.EventStudent[1].IdleAnim);
					}
					if (this.Osana)
					{
						this.SettleFriend();
					}
					if (!this.Spoken)
					{
						this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation.CrossFade(this.EventAnim[this.EventPhase]);
						if (num < 10f)
						{
							this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
						}
						AudioClipPlayer.Play(this.EventClip[this.EventPhase], this.EventStudent[this.EventSpeaker[this.EventPhase]].transform.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
						this.Spoken = true;
					}
					else
					{
						this.Timer += Time.deltaTime;
						if (this.Timer > this.EventClip[this.EventPhase].length)
						{
							this.EventSubtitle.text = string.Empty;
						}
						if (this.Yandere.transform.position.y < this.EventStudent[1].transform.position.y - 1f)
						{
							this.EventSubtitle.transform.localScale = Vector3.zero;
						}
						else if (num < 10f)
						{
							this.Scale = Mathf.Abs((num - 10f) * 0.2f);
							if (this.Scale < 0f)
							{
								this.Scale = 0f;
							}
							if (this.Scale > 1f)
							{
								this.Scale = 1f;
							}
							this.Jukebox.Dip = 1f - 0.5f * this.Scale;
							this.EventSubtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
						}
						else
						{
							this.EventSubtitle.transform.localScale = Vector3.zero;
						}
						Animation characterAnimation = this.EventStudent[this.EventSpeaker[this.EventPhase]].CharacterAnimation;
						if (characterAnimation[this.EventAnim[this.EventPhase]].time >= characterAnimation[this.EventAnim[this.EventPhase]].length - 1f)
						{
							characterAnimation.CrossFade(this.EventStudent[this.EventSpeaker[this.EventPhase]].IdleAnim, 1f);
						}
						if (this.Timer > this.EventClip[this.EventPhase].length + 1f)
						{
							this.Spoken = false;
							this.EventPhase++;
							this.Timer = 0f;
							if (this.EventPhase == this.EventSpeech.Length)
							{
								this.EndEvent();
							}
						}
						if (!this.Suitor && this.Yandere.transform.position.y > this.EventStudent[1].transform.position.y - 1f && this.EventPhase == 7 && num < 5f)
						{
							if (this.EventStudent1 == 25)
							{
								if (!EventGlobals.Event1)
								{
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
									EventGlobals.Event1 = true;
								}
							}
							else if (this.OsanaID < 2 && !EventGlobals.OsanaEvent2)
							{
								this.Yandere.NotificationManager.DisplayNotification(NotificationType.Info);
								EventGlobals.OsanaEvent2 = true;
							}
						}
					}
					if (base.enabled)
					{
						if (num < 3f)
						{
							this.Yandere.Eavesdropping = true;
						}
						else
						{
							this.Yandere.Eavesdropping = false;
						}
					}
				}
			}
		}
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x000ED4C4 File Offset: 0x000EB8C4
	private void SettleFriend()
	{
		this.EventStudent[2].MoveTowardsTarget(this.EventLocation[2].position);
		float num = Quaternion.Angle(this.EventStudent[2].transform.rotation, this.EventLocation[2].rotation);
		if (num > 1f)
		{
			this.EventStudent[2].transform.rotation = Quaternion.Slerp(this.EventStudent[2].transform.rotation, this.EventLocation[2].rotation, 10f * Time.deltaTime);
		}
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x000ED55C File Offset: 0x000EB95C
	public void EndEvent()
	{
		if (this.VoiceClip != null)
		{
			UnityEngine.Object.Destroy(this.VoiceClip);
		}
		this.EventStudent[1].CurrentDestination = this.EventStudent[1].Destinations[this.EventStudent[1].Phase];
		this.EventStudent[1].Pathfinding.target = this.EventStudent[1].Destinations[this.EventStudent[1].Phase];
		this.EventStudent[1].EventManager = null;
		this.EventStudent[1].InEvent = false;
		this.EventStudent[1].Private = false;
		this.EventStudent[2].CurrentDestination = this.EventStudent[2].Destinations[this.EventStudent[2].Phase];
		this.EventStudent[2].Pathfinding.target = this.EventStudent[2].Destinations[this.EventStudent[2].Phase];
		this.EventStudent[2].EventManager = null;
		this.EventStudent[2].InEvent = false;
		this.EventStudent[2].Private = false;
		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents(0);
		}
		this.Jukebox.Dip = 1f;
		this.Yandere.Eavesdropping = false;
		this.EventSubtitle.text = string.Empty;
		this.EventCheck = false;
		this.EventOn = false;
		base.enabled = false;
	}

	// Token: 0x04001E0E RID: 7694
	public StudentManagerScript StudentManager;

	// Token: 0x04001E0F RID: 7695
	public NoteLockerScript NoteLocker;

	// Token: 0x04001E10 RID: 7696
	public UILabel EventSubtitle;

	// Token: 0x04001E11 RID: 7697
	public YandereScript Yandere;

	// Token: 0x04001E12 RID: 7698
	public JukeboxScript Jukebox;

	// Token: 0x04001E13 RID: 7699
	public ClockScript Clock;

	// Token: 0x04001E14 RID: 7700
	public StudentScript[] EventStudent;

	// Token: 0x04001E15 RID: 7701
	public Transform[] EventLocation;

	// Token: 0x04001E16 RID: 7702
	public AudioClip[] EventClip;

	// Token: 0x04001E17 RID: 7703
	public string[] EventSpeech;

	// Token: 0x04001E18 RID: 7704
	public string[] EventAnim;

	// Token: 0x04001E19 RID: 7705
	public int[] EventSpeaker;

	// Token: 0x04001E1A RID: 7706
	public GameObject VoiceClip;

	// Token: 0x04001E1B RID: 7707
	public bool StopWalking;

	// Token: 0x04001E1C RID: 7708
	public bool EventCheck;

	// Token: 0x04001E1D RID: 7709
	public bool EventOn;

	// Token: 0x04001E1E RID: 7710
	public bool Suitor;

	// Token: 0x04001E1F RID: 7711
	public bool Spoken;

	// Token: 0x04001E20 RID: 7712
	public bool Osana;

	// Token: 0x04001E21 RID: 7713
	public float StartTimer;

	// Token: 0x04001E22 RID: 7714
	public float Timer;

	// Token: 0x04001E23 RID: 7715
	public float Scale;

	// Token: 0x04001E24 RID: 7716
	public float StartTime = 13.01f;

	// Token: 0x04001E25 RID: 7717
	public float EndTime = 13.5f;

	// Token: 0x04001E26 RID: 7718
	public int EventStudent1;

	// Token: 0x04001E27 RID: 7719
	public int EventStudent2;

	// Token: 0x04001E28 RID: 7720
	public int EventPhase;

	// Token: 0x04001E29 RID: 7721
	public int OsanaID = 1;
}
