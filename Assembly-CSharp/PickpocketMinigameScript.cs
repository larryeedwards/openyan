using System;
using UnityEngine;

// Token: 0x02000494 RID: 1172
public class PickpocketMinigameScript : MonoBehaviour
{
	// Token: 0x06001E72 RID: 7794 RVA: 0x0012ADBC File Offset: 0x001291BC
	private void Start()
	{
		base.transform.localScale = Vector3.zero;
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		this.Circle.enabled = false;
		this.BG.enabled = false;
	}

	// Token: 0x06001E73 RID: 7795 RVA: 0x0012AE2C File Offset: 0x0012922C
	private void Update()
	{
		if (this.Show)
		{
			if (this.PickpocketSpot != null)
			{
				this.Yandere.MoveTowardsTarget(this.PickpocketSpot.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.PickpocketSpot.rotation, Time.deltaTime * 10f);
			}
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			this.Timer += Time.deltaTime;
			Debug.Log(string.Concat(new object[]
			{
				"Starting Alerts is: ",
				this.StartingAlerts,
				". Yandere's current Alerts are: ",
				this.Yandere.Alerts
			}));
			if (this.Timer > 1f)
			{
				if (this.ButtonID == 0 && this.Yandere.Alerts == this.StartingAlerts)
				{
					this.ChooseButton();
					this.Timer = 0f;
				}
				else
				{
					this.Yandere.Caught = true;
					this.Failure = true;
					this.End();
				}
			}
			else if (this.ButtonID > 0)
			{
				this.Circle.fillAmount = 1f - this.Timer / 1f;
				if ((Input.GetButtonDown("A") && this.CurrentButton != "A") || (Input.GetButtonDown("B") && this.CurrentButton != "B") || (Input.GetButtonDown("X") && this.CurrentButton != "X") || (Input.GetButtonDown("Y") && this.CurrentButton != "Y"))
				{
					this.Yandere.Caught = true;
					this.Failure = true;
					this.End();
				}
				else if (Input.GetButtonDown(this.CurrentButton))
				{
					this.ButtonPrompts[this.ButtonID].enabled = false;
					this.Circle.enabled = false;
					this.BG.enabled = false;
					this.ButtonID = 0;
					this.Timer = 0f;
					this.Progress++;
					if (this.Progress == 5)
					{
						if (this.Sabotage)
						{
							this.Yandere.NotificationManager.CustomText = "Sabotage Success";
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						}
						else
						{
							this.Yandere.NotificationManager.CustomText = "Pickpocket Success";
							this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
						}
						this.Yandere.Pickpocketing = false;
						this.Yandere.CanMove = true;
						this.Success = true;
						this.End();
					}
				}
			}
		}
		else if (base.transform.localScale.x > 0.1f)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (base.transform.localScale.x < 0.1f)
			{
				base.transform.localScale = Vector3.zero;
			}
		}
	}

	// Token: 0x06001E74 RID: 7796 RVA: 0x0012B1E4 File Offset: 0x001295E4
	private void ChooseButton()
	{
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		int buttonID = this.ButtonID;
		while (this.ButtonID == buttonID)
		{
			this.ButtonID = UnityEngine.Random.Range(1, 5);
		}
		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}
		this.ButtonPrompts[this.ButtonID].enabled = true;
		this.Circle.enabled = true;
		this.BG.enabled = true;
	}

	// Token: 0x06001E75 RID: 7797 RVA: 0x0012B2E4 File Offset: 0x001296E4
	public void End()
	{
		Debug.Log("Ending minigame.");
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		this.Circle.enabled = false;
		this.BG.enabled = false;
		this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		this.Progress = 0;
		this.ButtonID = 0;
		this.Show = false;
		this.Timer = 0f;
	}

	// Token: 0x04002752 RID: 10066
	public Transform PickpocketSpot;

	// Token: 0x04002753 RID: 10067
	public UISprite[] ButtonPrompts;

	// Token: 0x04002754 RID: 10068
	public UISprite Circle;

	// Token: 0x04002755 RID: 10069
	public UISprite BG;

	// Token: 0x04002756 RID: 10070
	public YandereScript Yandere;

	// Token: 0x04002757 RID: 10071
	public string CurrentButton = string.Empty;

	// Token: 0x04002758 RID: 10072
	public bool NotNurse;

	// Token: 0x04002759 RID: 10073
	public bool Sabotage;

	// Token: 0x0400275A RID: 10074
	public bool Failure;

	// Token: 0x0400275B RID: 10075
	public bool Success;

	// Token: 0x0400275C RID: 10076
	public bool Show;

	// Token: 0x0400275D RID: 10077
	public int StartingAlerts;

	// Token: 0x0400275E RID: 10078
	public int ButtonID;

	// Token: 0x0400275F RID: 10079
	public int Progress;

	// Token: 0x04002760 RID: 10080
	public int ID;

	// Token: 0x04002761 RID: 10081
	public float Timer;
}
