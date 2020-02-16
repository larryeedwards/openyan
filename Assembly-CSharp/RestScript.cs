﻿using System;
using UnityEngine;

// Token: 0x020004BF RID: 1215
public class RestScript : MonoBehaviour
{
	// Token: 0x06001F1F RID: 7967 RVA: 0x0013DDEC File Offset: 0x0013C1EC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Portal.CanAttendClass = true;
			this.Portal.CheckForProblems();
			if (!this.Portal.CanAttendClass)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
			}
			else if (this.Portal.Clock.Period < 5)
			{
				this.Portal.Reputation.PendingRep -= 10f;
				this.Portal.Reputation.UpdateRep();
				this.Prompt.Yandere.Resting = true;
				if (this.Portal.Police.PoisonScene || (this.Portal.Police.SuicideScene && this.Portal.Police.Corpses - this.Portal.Police.HiddenCorpses > 0) || this.Portal.Police.Corpses - this.Portal.Police.HiddenCorpses > 0 || this.Portal.Reputation.Reputation <= -100f)
				{
					this.Portal.EndDay();
				}
				else
				{
					this.Portal.ClassDarkness.enabled = true;
					this.Portal.Clock.StopTime = true;
					this.Portal.Transition = true;
					this.Portal.FadeOut = true;
					this.Prompt.Yandere.Character.GetComponent<Animation>().CrossFade(this.Prompt.Yandere.IdleAnim);
					this.Prompt.Yandere.YandereVision = false;
					this.Prompt.Yandere.CanMove = false;
					this.Portal.EndEvents();
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else
			{
				this.Prompt.Yandere.Character.GetComponent<Animation>().CrossFade(this.Prompt.Yandere.IdleAnim);
				this.Prompt.Yandere.YandereVision = false;
				this.Prompt.Yandere.CanMove = false;
				this.Prompt.Yandere.Resting = true;
				this.Portal.EndDay();
			}
		}
	}

	// Token: 0x040029A1 RID: 10657
	public PortalScript Portal;

	// Token: 0x040029A2 RID: 10658
	public PromptScript Prompt;
}
