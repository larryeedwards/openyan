using System;
using UnityEngine;

// Token: 0x020004C4 RID: 1220
public class RivalDeskScript : MonoBehaviour
{
	// Token: 0x06001F31 RID: 7985 RVA: 0x0013F4A5 File Offset: 0x0013D8A5
	private void Start()
	{
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001F32 RID: 7986 RVA: 0x0013F4BC File Offset: 0x0013D8BC
	private void Update()
	{
		if (!this.Prompt.Yandere.Inventory.AnswerSheet && this.Prompt.Yandere.Inventory.DuplicateSheet)
		{
			this.Prompt.enabled = true;
			if (this.Clock.HourTime > 13f)
			{
				this.Prompt.HideButton[0] = false;
				if (this.Clock.HourTime > 13.5f)
				{
					SchemeGlobals.SetSchemeStage(5, 100);
					this.Schemes.UpdateInstructions();
					this.Prompt.HideButton[0] = true;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(5, 9);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.DuplicateSheet = false;
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Cheating = true;
				base.enabled = false;
			}
		}
	}

	// Token: 0x040029C0 RID: 10688
	public SchemesScript Schemes;

	// Token: 0x040029C1 RID: 10689
	public ClockScript Clock;

	// Token: 0x040029C2 RID: 10690
	public PromptScript Prompt;

	// Token: 0x040029C3 RID: 10691
	public bool Cheating;
}
