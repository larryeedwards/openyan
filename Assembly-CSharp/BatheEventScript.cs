using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class BatheEventScript : MonoBehaviour
{
	// Token: 0x0600173F RID: 5951 RVA: 0x000B6FFF File Offset: 0x000B53FF
	private void Start()
	{
		this.RivalPhone.SetActive(false);
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x000B7024 File Offset: 0x000B5424
	private void Update()
	{
		if (!this.Clock.StopTime && !this.EventActive && this.Clock.HourTime > this.EventTime)
		{
			this.EventStudent = this.StudentManager.Students[30];
			if (this.EventStudent != null && !this.EventStudent.Distracted && !this.EventStudent.Talking && !this.EventStudent.Meeting && this.EventStudent.Indoors)
			{
				if (!this.EventStudent.WitnessedMurder)
				{
					this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;
					this.EventStudent.CurrentDestination = this.StudentManager.FemaleStripSpot;
					this.EventStudent.Pathfinding.target = this.StudentManager.FemaleStripSpot;
					this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.WalkAnim);
					this.EventStudent.Pathfinding.canSearch = true;
					this.EventStudent.Pathfinding.canMove = true;
					this.EventStudent.Pathfinding.speed = 1f;
					this.EventStudent.SpeechLines.Stop();
					this.EventStudent.DistanceToDestination = 100f;
					this.EventStudent.SmartPhone.SetActive(false);
					this.EventStudent.Obstacle.checkTime = 99f;
					this.EventStudent.InEvent = true;
					this.EventStudent.Private = true;
					this.EventStudent.Prompt.Hide();
					this.EventStudent.Hearts.Stop();
					this.EventActive = true;
					if (this.EventStudent.Following)
					{
						this.EventStudent.Pathfinding.canMove = true;
						this.EventStudent.Pathfinding.speed = 1f;
						this.EventStudent.Following = false;
						this.EventStudent.Routine = true;
						this.Yandere.Followers--;
						this.EventStudent.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3f);
						this.EventStudent.Prompt.Label[0].text = "     Talk";
					}
				}
				else
				{
					base.enabled = false;
				}
			}
		}
		if (this.EventActive)
		{
			if (this.Clock.HourTime > this.EventTime + 1f || this.EventStudent.WitnessedMurder || this.EventStudent.Splashed || this.EventStudent.Alarmed || this.EventStudent.Dying || !this.EventStudent.Alive)
			{
				this.EndEvent();
			}
			else
			{
				if (this.EventStudent.DistanceToDestination < 0.5f)
				{
					if (this.EventPhase == 1)
					{
						this.EventStudent.Routine = false;
						this.EventStudent.BathePhase = 1;
						this.EventStudent.Wet = true;
						this.EventPhase++;
					}
					else if (this.EventPhase == 2)
					{
						if (this.EventStudent.BathePhase == 4)
						{
							this.RivalPhone.SetActive(true);
							this.EventPhase++;
						}
					}
					else if (this.EventPhase == 3 && !this.EventStudent.Wet)
					{
						this.EndEvent();
					}
				}
				if (this.EventPhase == 4)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > this.CurrentClipLength + 1f)
					{
						this.EventStudent.Routine = true;
						this.EndEvent();
					}
				}
				float num = Vector3.Distance(this.Yandere.transform.position, this.EventStudent.transform.position);
				if (num < 11f)
				{
					if (num < 10f)
					{
						float num2 = Mathf.Abs((num - 10f) * 0.2f);
						if (num2 < 0f)
						{
							num2 = 0f;
						}
						if (num2 > 1f)
						{
							num2 = 1f;
						}
						this.EventSubtitle.transform.localScale = new Vector3(num2, num2, num2);
					}
					else
					{
						this.EventSubtitle.transform.localScale = Vector3.zero;
					}
				}
			}
		}
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x000B74CC File Offset: 0x000B58CC
	private void EndEvent()
	{
		if (!this.EventOver)
		{
			if (this.VoiceClip != null)
			{
				UnityEngine.Object.Destroy(this.VoiceClip);
			}
			this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];
			this.EventStudent.Obstacle.checkTime = 1f;
			if (!this.EventStudent.Dying)
			{
				this.EventStudent.Prompt.enabled = true;
				this.EventStudent.Pathfinding.canSearch = true;
				this.EventStudent.Pathfinding.canMove = true;
				this.EventStudent.Pathfinding.speed = 1f;
				this.EventStudent.TargetDistance = 1f;
				this.EventStudent.Private = false;
			}
			this.EventStudent.InEvent = false;
			this.EventSubtitle.text = string.Empty;
			this.StudentManager.UpdateStudents(0);
		}
		this.EventActive = false;
		base.enabled = false;
	}

	// Token: 0x040016A7 RID: 5799
	public StudentManagerScript StudentManager;

	// Token: 0x040016A8 RID: 5800
	public YandereScript Yandere;

	// Token: 0x040016A9 RID: 5801
	public ClockScript Clock;

	// Token: 0x040016AA RID: 5802
	public StudentScript EventStudent;

	// Token: 0x040016AB RID: 5803
	public UILabel EventSubtitle;

	// Token: 0x040016AC RID: 5804
	public AudioClip[] EventClip;

	// Token: 0x040016AD RID: 5805
	public string[] EventSpeech;

	// Token: 0x040016AE RID: 5806
	public string[] EventAnim;

	// Token: 0x040016AF RID: 5807
	public GameObject RivalPhone;

	// Token: 0x040016B0 RID: 5808
	public GameObject VoiceClip;

	// Token: 0x040016B1 RID: 5809
	public bool EventActive;

	// Token: 0x040016B2 RID: 5810
	public bool EventOver;

	// Token: 0x040016B3 RID: 5811
	public float EventTime = 15.1f;

	// Token: 0x040016B4 RID: 5812
	public int EventPhase = 1;

	// Token: 0x040016B5 RID: 5813
	public DayOfWeek EventDay = DayOfWeek.Thursday;

	// Token: 0x040016B6 RID: 5814
	public Vector3 OriginalPosition;

	// Token: 0x040016B7 RID: 5815
	public float CurrentClipLength;

	// Token: 0x040016B8 RID: 5816
	public float Timer;
}
