using System;
using UnityEngine;

// Token: 0x020004C3 RID: 1219
public class RivalBagScript : MonoBehaviour
{
	// Token: 0x06001F2E RID: 7982 RVA: 0x0013F27D File Offset: 0x0013D67D
	private void Start()
	{
		this.Prompt.enabled = false;
		this.Prompt.Hide();
		base.enabled = false;
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x0013F2A0 File Offset: 0x0013D6A0
	private void Update()
	{
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[0] = true;
		}
		else if (this.Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.HideButton[0] = false;
		}
		else
		{
			this.Prompt.HideButton[0] = true;
		}
		if (this.Prompt.Yandere.Inventory.Cigs)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(3, 4);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.Cigs = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
		if (this.Clock.Period == 2 || this.Clock.Period == 4)
		{
			this.Prompt.HideButton[1] = true;
		}
		else if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.HideButton[1] = false;
		}
		else
		{
			this.Prompt.HideButton[1] = true;
		}
		if (this.Prompt.Yandere.Inventory.Ring)
		{
			this.Prompt.enabled = true;
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				SchemeGlobals.SetSchemeStage(2, 3);
				this.Schemes.UpdateInstructions();
				this.Prompt.Yandere.Inventory.Ring = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				base.enabled = false;
			}
		}
	}

	// Token: 0x040029BD RID: 10685
	public SchemesScript Schemes;

	// Token: 0x040029BE RID: 10686
	public ClockScript Clock;

	// Token: 0x040029BF RID: 10687
	public PromptScript Prompt;
}
