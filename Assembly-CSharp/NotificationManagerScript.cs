using System;
using UnityEngine;

// Token: 0x02000475 RID: 1141
public class NotificationManagerScript : MonoBehaviour
{
	// Token: 0x06001DF9 RID: 7673 RVA: 0x00121804 File Offset: 0x0011FC04
	private void Awake()
	{
		this.NotificationMessages = new NotificationTypeAndStringDictionary
		{
			{
				NotificationType.Bloody,
				"Visibly Bloody"
			},
			{
				NotificationType.Body,
				"Near Body"
			},
			{
				NotificationType.Insane,
				"Visibly Insane"
			},
			{
				NotificationType.Armed,
				"Visibly Armed"
			},
			{
				NotificationType.Lewd,
				"Visibly Lewd"
			},
			{
				NotificationType.Intrude,
				"Intruding"
			},
			{
				NotificationType.Late,
				"Late For Class"
			},
			{
				NotificationType.Info,
				"Learned New Info"
			},
			{
				NotificationType.Topic,
				"Learned New Topic: "
			},
			{
				NotificationType.Opinion,
				"Learned Opinion on: "
			},
			{
				NotificationType.Complete,
				"Mission Complete"
			},
			{
				NotificationType.Exfiltrate,
				"Leave School"
			},
			{
				NotificationType.Evidence,
				"Evidence Recorded"
			},
			{
				NotificationType.ClassSoon,
				"Class Begins Soon"
			},
			{
				NotificationType.ClassNow,
				"Class Begins Now"
			},
			{
				NotificationType.Eavesdropping,
				"Eavesdropping"
			},
			{
				NotificationType.Clothing,
				"Cannot Attack; No Spare Clothing"
			},
			{
				NotificationType.Persona,
				"Persona"
			},
			{
				NotificationType.Custom,
				this.CustomText
			}
		};
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x00121910 File Offset: 0x0011FD10
	private void Update()
	{
		if (this.NotificationParent.localPosition.y > 0.001f + -0.049f * (float)this.NotificationsSpawned)
		{
			this.NotificationParent.localPosition = new Vector3(this.NotificationParent.localPosition.x, Mathf.Lerp(this.NotificationParent.localPosition.y, -0.049f * (float)this.NotificationsSpawned, Time.deltaTime * 10f), this.NotificationParent.localPosition.z);
		}
		if (this.Phase == 1)
		{
			if (this.Clock.HourTime > 8.4f)
			{
				this.Yandere.StudentManager.TutorialWindow.ShowClassMessage = true;
				this.DisplayNotification(NotificationType.ClassSoon);
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.Clock.HourTime > 8.5f)
			{
				this.DisplayNotification(NotificationType.ClassNow);
				this.Phase++;
			}
		}
		else if (this.Phase == 3)
		{
			if (this.Clock.HourTime > 13.4f)
			{
				this.DisplayNotification(NotificationType.ClassSoon);
				this.Phase++;
			}
		}
		else if (this.Phase == 4 && this.Clock.HourTime > 13.5f)
		{
			this.DisplayNotification(NotificationType.ClassNow);
			this.Phase++;
		}
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x00121AB0 File Offset: 0x0011FEB0
	public void DisplayNotification(NotificationType Type)
	{
		if (!this.Yandere.Egg)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Notification);
			NotificationScript component = gameObject.GetComponent<NotificationScript>();
			gameObject.transform.parent = this.NotificationParent;
			gameObject.transform.localPosition = new Vector3(0f, 0.60275f + 0.049f * (float)this.NotificationsSpawned, 0f);
			gameObject.transform.localEulerAngles = Vector3.zero;
			component.NotificationManager = this;
			string text;
			bool flag = this.NotificationMessages.TryGetValue(Type, out text);
			if (Type != NotificationType.Persona && Type != NotificationType.Custom)
			{
				string str = string.Empty;
				if (Type == NotificationType.Topic || Type == NotificationType.Opinion)
				{
					str = this.TopicName;
				}
				component.Label.text = text + str;
			}
			else if (Type == NotificationType.Custom)
			{
				component.Label.text = this.CustomText;
			}
			else
			{
				component.Label.text = this.PersonaName + " " + text;
			}
			this.NotificationsSpawned++;
			component.ID = this.NotificationsSpawned;
		}
	}

	// Token: 0x0400261A RID: 9754
	public YandereScript Yandere;

	// Token: 0x0400261B RID: 9755
	public Transform NotificationSpawnPoint;

	// Token: 0x0400261C RID: 9756
	public Transform NotificationParent;

	// Token: 0x0400261D RID: 9757
	public GameObject Notification;

	// Token: 0x0400261E RID: 9758
	public int NotificationsSpawned;

	// Token: 0x0400261F RID: 9759
	public int Phase = 1;

	// Token: 0x04002620 RID: 9760
	public ClockScript Clock;

	// Token: 0x04002621 RID: 9761
	public string PersonaName;

	// Token: 0x04002622 RID: 9762
	public string CustomText;

	// Token: 0x04002623 RID: 9763
	public string TopicName;

	// Token: 0x04002624 RID: 9764
	public string[] ClubNames;

	// Token: 0x04002625 RID: 9765
	private NotificationTypeAndStringDictionary NotificationMessages;
}
