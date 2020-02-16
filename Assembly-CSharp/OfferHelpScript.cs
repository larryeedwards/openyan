using System;
using UnityEngine;

// Token: 0x0200047B RID: 1147
public class OfferHelpScript : MonoBehaviour
{
	// Token: 0x06001E0B RID: 7691 RVA: 0x00122189 File Offset: 0x00120589
	private void Start()
	{
		this.Prompt.enabled = true;
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x00122198 File Offset: 0x00120598
	private void Update()
	{
		if (!this.Unable)
		{
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Jukebox.Dip = 0.1f;
					this.Yandere.EmptyHands();
					this.Yandere.CanMove = false;
					this.Student = this.StudentManager.Students[this.EventStudentID];
					this.Student.Prompt.Label[0].text = "     Talk";
					this.Student.Pushable = false;
					this.Student.Meeting = false;
					this.Student.Routine = false;
					this.Student.MeetTimer = 0f;
					this.Offering = true;
				}
			}
			if (this.Offering)
			{
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, base.transform.rotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(base.transform.position + Vector3.down);
				Quaternion b = Quaternion.LookRotation(this.Yandere.transform.position - this.Student.transform.position);
				this.Student.transform.rotation = Quaternion.Slerp(this.Student.transform.rotation, b, Time.deltaTime * 10f);
				Animation component = this.Yandere.Character.GetComponent<Animation>();
				Animation component2 = this.Student.Character.GetComponent<Animation>();
				if (!this.Spoken)
				{
					if (this.EventSpeaker[this.EventPhase] == 1)
					{
						component.CrossFade(this.EventAnim[this.EventPhase]);
						component2.CrossFade(this.Student.IdleAnim, 1f);
					}
					else
					{
						component2.CrossFade(this.EventAnim[this.EventPhase]);
						component.CrossFade(this.Yandere.IdleAnim, 1f);
					}
					this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
					this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
					AudioSource component3 = base.GetComponent<AudioSource>();
					component3.clip = this.EventClip[this.EventPhase];
					component3.Play();
					this.Spoken = true;
				}
				else
				{
					if (!this.Yandere.PauseScreen.Show && Input.GetButtonDown("A"))
					{
						this.Timer += this.EventClip[this.EventPhase].length + 1f;
					}
					if (this.EventSpeaker[this.EventPhase] == 1)
					{
						if (component[this.EventAnim[this.EventPhase]].time >= component[this.EventAnim[this.EventPhase]].length)
						{
							component.CrossFade(this.Yandere.IdleAnim);
						}
					}
					else if (component2[this.EventAnim[this.EventPhase]].time >= component2[this.EventAnim[this.EventPhase]].length)
					{
						component2.CrossFade(this.Student.IdleAnim);
					}
					this.Timer += Time.deltaTime;
					if (this.Timer > this.EventClip[this.EventPhase].length)
					{
						Debug.Log("Emptying string.");
						this.EventSubtitle.text = string.Empty;
					}
					else
					{
						this.EventSubtitle.text = this.EventSpeech[this.EventPhase];
					}
					if (this.Timer > this.EventClip[this.EventPhase].length + 1f)
					{
						if (this.EventStudentID == 5 && this.EventPhase == 2)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.Targeting = true;
							base.StartCoroutine(this.Yandere.PauseScreen.PhotoGallery.GetPhotos());
							this.Yandere.PauseScreen.PhotoGallery.gameObject.SetActive(true);
							this.Yandere.PauseScreen.PhotoGallery.NamingBully = true;
							this.Yandere.PauseScreen.MainMenu.SetActive(false);
							this.Yandere.PauseScreen.Panel.enabled = true;
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PauseScreen.Show = true;
							Time.timeScale = 0.0001f;
							this.Yandere.PauseScreen.PhotoGallery.UpdateButtonPrompts();
							this.Offering = false;
						}
						else
						{
							this.Continue();
						}
					}
				}
			}
			else if (this.StudentManager.Students[this.EventStudentID].Pushed || !this.StudentManager.Students[this.EventStudentID].Alive)
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x00122730 File Offset: 0x00120B30
	public void UpdateLocation()
	{
		Debug.Log("The ''Offer Help'' prompt was told to update its location.");
		this.Student = this.StudentManager.Students[this.EventStudentID];
		if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[7])
		{
			base.transform.position = this.Locations[1].position;
			base.transform.eulerAngles = this.Locations[1].eulerAngles;
		}
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[8])
		{
			base.transform.position = this.Locations[2].position;
			base.transform.eulerAngles = this.Locations[2].eulerAngles;
		}
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[9])
		{
			base.transform.position = this.Locations[3].position;
			base.transform.eulerAngles = this.Locations[3].eulerAngles;
		}
		else if (this.Student.CurrentDestination == this.StudentManager.MeetSpots.List[10])
		{
			base.transform.position = this.Locations[4].position;
			base.transform.eulerAngles = this.Locations[4].eulerAngles;
		}
		if (this.EventStudentID == 30)
		{
			if (!PlayerGlobals.GetStudentFriend(30))
			{
				this.Prompt.Label[0].text = "     Must Befriend Student First";
				this.Unable = true;
			}
			this.Prompt.MyCollider.enabled = true;
		}
		else if (this.EventStudentID == 5)
		{
			this.Prompt.MyCollider.enabled = true;
		}
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x00122938 File Offset: 0x00120D38
	public void Continue()
	{
		Debug.Log("Proceeding to next line.");
		this.Offering = true;
		this.Spoken = false;
		this.EventPhase++;
		this.Timer = 0f;
		if (this.EventStudentID == 30 && this.EventPhase == 14)
		{
			if (!ConversationGlobals.GetTopicDiscovered(23))
			{
				this.Yandere.NotificationManager.TopicName = "Family";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
				ConversationGlobals.SetTopicDiscovered(23, true);
			}
			if (!ConversationGlobals.GetTopicLearnedByStudent(23, this.EventStudentID))
			{
				this.Yandere.NotificationManager.TopicName = "Family";
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
				ConversationGlobals.SetTopicLearnedByStudent(23, this.EventStudentID, true);
			}
		}
		if (this.EventPhase == this.EventSpeech.Length)
		{
			if (this.EventStudentID == 30)
			{
				SchemeGlobals.SetSchemeStage(6, 5);
			}
			this.Student.CurrentDestination = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.target = this.Student.Destinations[this.Student.Phase];
			this.Student.Pathfinding.canSearch = true;
			this.Student.Pathfinding.canMove = true;
			this.Student.Routine = true;
			this.Yandere.CanMove = true;
			this.Jukebox.Dip = 1f;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002641 RID: 9793
	public StudentManagerScript StudentManager;

	// Token: 0x04002642 RID: 9794
	public JukeboxScript Jukebox;

	// Token: 0x04002643 RID: 9795
	public StudentScript Student;

	// Token: 0x04002644 RID: 9796
	public YandereScript Yandere;

	// Token: 0x04002645 RID: 9797
	public PromptScript Prompt;

	// Token: 0x04002646 RID: 9798
	public UILabel EventSubtitle;

	// Token: 0x04002647 RID: 9799
	public Transform[] Locations;

	// Token: 0x04002648 RID: 9800
	public AudioClip[] EventClip;

	// Token: 0x04002649 RID: 9801
	public string[] EventSpeech;

	// Token: 0x0400264A RID: 9802
	public string[] EventAnim;

	// Token: 0x0400264B RID: 9803
	public int[] EventSpeaker;

	// Token: 0x0400264C RID: 9804
	public bool Offering;

	// Token: 0x0400264D RID: 9805
	public bool Spoken;

	// Token: 0x0400264E RID: 9806
	public bool Unable;

	// Token: 0x0400264F RID: 9807
	public int EventStudentID;

	// Token: 0x04002650 RID: 9808
	public int EventPhase = 1;

	// Token: 0x04002651 RID: 9809
	public float Timer;
}
