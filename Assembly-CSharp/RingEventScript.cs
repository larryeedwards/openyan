using System;
using UnityEngine;

// Token: 0x020004C1 RID: 1217
public class RingEventScript : MonoBehaviour
{
	// Token: 0x06001F27 RID: 7975 RVA: 0x0013E532 File Offset: 0x0013C932
	private void Start()
	{
		this.HoldingPosition = new Vector3(0.0075f, -0.0355f, 0.0175f);
		this.HoldingRotation = new Vector3(15f, -70f, -135f);
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x0013E568 File Offset: 0x0013C968
	private void Update()
	{
		if (!this.Clock.StopTime && !this.EventActive && this.Clock.HourTime > this.EventTime)
		{
			this.EventStudent = this.StudentManager.Students[2];
			if (this.EventStudent != null && !this.EventStudent.Distracted && !this.EventStudent.Talking)
			{
				if (!this.EventStudent.WitnessedMurder && !this.EventStudent.Bullied)
				{
					if (this.EventStudent.Cosmetic.FemaleAccessories[3].activeInHierarchy)
					{
						if (SchemeGlobals.GetSchemeStage(2) < 100)
						{
							this.RingPrompt = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<PromptScript>();
							this.RingCollider = this.EventStudent.Cosmetic.FemaleAccessories[3].GetComponent<BoxCollider>();
							this.OriginalPosition = this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition;
							this.EventStudent.CurrentDestination = this.EventStudent.Destinations[this.EventStudent.Phase];
							this.EventStudent.Pathfinding.target = this.EventStudent.Destinations[this.EventStudent.Phase];
							this.EventStudent.Obstacle.checkTime = 99f;
							this.EventStudent.InEvent = true;
							this.EventStudent.Private = true;
							this.EventStudent.Prompt.Hide();
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
					else
					{
						base.enabled = false;
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
			if (this.EventStudent.DistanceToDestination < 0.5f)
			{
				this.EventStudent.Pathfinding.canSearch = false;
				this.EventStudent.Pathfinding.canMove = false;
			}
			if (this.EventStudent.Alarmed && this.Yandere.TheftTimer > 0f)
			{
				Debug.Log("Event ended because Sakyu saw theft.");
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
				this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.RingCollider.gameObject.SetActive(true);
				this.RingCollider.enabled = false;
				this.Yandere.Inventory.Ring = false;
				this.EndEvent();
			}
			else if (this.Clock.HourTime > this.EventTime + 0.5f || this.EventStudent.WitnessedMurder || this.EventStudent.Splashed || this.EventStudent.Alarmed || this.EventStudent.Dying || !this.EventStudent.Alive)
			{
				this.EndEvent();
			}
			else if (!this.EventStudent.Pathfinding.canMove)
			{
				if (this.EventPhase == 1)
				{
					this.Timer += Time.deltaTime;
					this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventAnim[0]);
					this.EventPhase++;
				}
				else if (this.EventPhase == 2)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length)
					{
						this.EventStudent.Character.GetComponent<Animation>().CrossFade(this.EventStudent.EatAnim);
						this.EventStudent.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0f);
						this.EventStudent.Bento.transform.localEulerAngles = new Vector3(0f, 165f, 82.5f);
						this.EventStudent.Chopsticks[0].SetActive(true);
						this.EventStudent.Chopsticks[1].SetActive(true);
						this.EventStudent.Bento.SetActive(true);
						this.EventStudent.Lid.SetActive(false);
						this.RingCollider.enabled = true;
						this.EventPhase++;
						this.Timer = 0f;
					}
					else if (this.Timer > 4f)
					{
						if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = null;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.position = new Vector3(-2.707666f, 12.4695f, -31.136f);
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.eulerAngles = new Vector3(-20f, 180f, 0f);
						}
					}
					else if (this.Timer > 2.5f)
					{
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
						this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
					}
				}
				else if (this.EventPhase == 3)
				{
					if (this.Clock.HourTime > 13.375f)
					{
						this.EventStudent.Bento.SetActive(false);
						this.EventStudent.Chopsticks[0].SetActive(false);
						this.EventStudent.Chopsticks[1].SetActive(false);
						if (this.RingCollider != null)
						{
							this.RingCollider.enabled = false;
						}
						if (this.RingPrompt != null)
						{
							this.RingPrompt.Hide();
							this.RingPrompt.enabled = false;
						}
						this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].time = this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].length;
						this.EventStudent.Character.GetComponent<Animation>()[this.EventAnim[0]].speed = -1f;
						this.EventStudent.Character.GetComponent<Animation>().CrossFade((!(this.EventStudent.Cosmetic.FemaleAccessories[3] != null)) ? this.EventAnim[1] : this.EventAnim[0]);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 4)
				{
					this.Timer += Time.deltaTime;
					if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
					{
						if (this.Timer > 2f)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.RightHand;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.HoldingPosition;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localEulerAngles = this.HoldingRotation;
						}
						if (this.Timer > 3f)
						{
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
							this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
							this.RingCollider.enabled = false;
						}
						if (this.Timer > 6f)
						{
							this.EndEvent();
						}
					}
					else if (this.Timer > 1.5f && this.Yandere.transform.position.z < 0f)
					{
						this.EventSubtitle.text = this.EventSpeech[0];
						AudioClipPlayer.Play(this.EventClip[0], this.EventStudent.transform.position + Vector3.up, 5f, 10f, out this.VoiceClip, out this.CurrentClipLength);
						this.EventPhase++;
					}
				}
				else if (this.EventPhase == 5)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 9.5f)
					{
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

	// Token: 0x06001F29 RID: 7977 RVA: 0x0013F038 File Offset: 0x0013D438
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
			}
			this.EventStudent.Pathfinding.speed = 1f;
			this.EventStudent.TargetDistance = 0.5f;
			this.EventStudent.InEvent = false;
			this.EventStudent.Private = false;
			this.EventSubtitle.text = string.Empty;
			this.StudentManager.UpdateStudents(0);
		}
		this.EventActive = false;
		base.enabled = false;
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x0013F154 File Offset: 0x0013D554
	public void ReturnRing()
	{
		if (this.EventStudent.Cosmetic.FemaleAccessories[3] != null)
		{
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.parent = this.EventStudent.LeftMiddleFinger;
			this.EventStudent.Cosmetic.FemaleAccessories[3].transform.localPosition = this.OriginalPosition;
			this.RingCollider.enabled = false;
			this.RingPrompt.Hide();
			this.RingPrompt.enabled = false;
		}
	}

	// Token: 0x040029A8 RID: 10664
	public StudentManagerScript StudentManager;

	// Token: 0x040029A9 RID: 10665
	public YandereScript Yandere;

	// Token: 0x040029AA RID: 10666
	public ClockScript Clock;

	// Token: 0x040029AB RID: 10667
	public StudentScript EventStudent;

	// Token: 0x040029AC RID: 10668
	public UILabel EventSubtitle;

	// Token: 0x040029AD RID: 10669
	public AudioClip[] EventClip;

	// Token: 0x040029AE RID: 10670
	public string[] EventSpeech;

	// Token: 0x040029AF RID: 10671
	public string[] EventAnim;

	// Token: 0x040029B0 RID: 10672
	public GameObject VoiceClip;

	// Token: 0x040029B1 RID: 10673
	public bool EventActive;

	// Token: 0x040029B2 RID: 10674
	public bool EventOver;

	// Token: 0x040029B3 RID: 10675
	public float EventTime = 13.1f;

	// Token: 0x040029B4 RID: 10676
	public int EventPhase = 1;

	// Token: 0x040029B5 RID: 10677
	public Vector3 OriginalPosition;

	// Token: 0x040029B6 RID: 10678
	public Vector3 HoldingPosition;

	// Token: 0x040029B7 RID: 10679
	public Vector3 HoldingRotation;

	// Token: 0x040029B8 RID: 10680
	public float CurrentClipLength;

	// Token: 0x040029B9 RID: 10681
	public float Timer;

	// Token: 0x040029BA RID: 10682
	public PromptScript RingPrompt;

	// Token: 0x040029BB RID: 10683
	public Collider RingCollider;
}
