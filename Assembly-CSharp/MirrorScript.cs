using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class MirrorScript : MonoBehaviour
{
	// Token: 0x06001DB3 RID: 7603 RVA: 0x0011A044 File Offset: 0x00118444
	private void Start()
	{
		this.Limit = this.Idles.Length - 1;
		if (ClubGlobals.Club == ClubType.Delinquent)
		{
			this.ID = 10;
			if (this.Prompt.Yandere.Persona != YanderePersonaType.Tough)
			{
				this.UpdatePersona();
			}
		}
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x0011A094 File Offset: 0x00118494
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Prompt.Yandere.Health > 0)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.ID++;
				if (this.ID == this.Limit)
				{
					this.ID = 0;
				}
				this.UpdatePersona();
			}
		}
		else if (this.Prompt.Circle[1].fillAmount == 0f && this.Prompt.Yandere.Health > 0)
		{
			this.Prompt.Circle[1].fillAmount = 1f;
			this.ID--;
			if (this.ID < 0)
			{
				this.ID = this.Limit - 1;
			}
			this.UpdatePersona();
		}
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x0011A194 File Offset: 0x00118594
	private void UpdatePersona()
	{
		if (!this.Prompt.Yandere.Carrying)
		{
			this.Prompt.Yandere.NotificationManager.PersonaName = this.Personas[this.ID];
			this.Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Persona);
			this.Prompt.Yandere.IdleAnim = this.Idles[this.ID];
			this.Prompt.Yandere.WalkAnim = this.Walks[this.ID];
			this.Prompt.Yandere.UpdatePersona(this.ID);
		}
		this.Prompt.Yandere.OriginalIdleAnim = this.Idles[this.ID];
		this.Prompt.Yandere.OriginalWalkAnim = this.Walks[this.ID];
		this.Prompt.Yandere.StudentManager.UpdatePerception();
	}

	// Token: 0x0400254B RID: 9547
	public PromptScript Prompt;

	// Token: 0x0400254C RID: 9548
	public string[] Personas;

	// Token: 0x0400254D RID: 9549
	public string[] Idles;

	// Token: 0x0400254E RID: 9550
	public string[] Walks;

	// Token: 0x0400254F RID: 9551
	public int ID;

	// Token: 0x04002550 RID: 9552
	public int Limit;
}
