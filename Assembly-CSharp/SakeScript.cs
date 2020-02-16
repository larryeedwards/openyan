using System;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public class SakeScript : MonoBehaviour
{
	// Token: 0x06001F5F RID: 8031 RVA: 0x001409B9 File Offset: 0x0013EDB9
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Sake = true;
			this.UpdatePrompt();
		}
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x001409F4 File Offset: 0x0013EDF4
	public void UpdatePrompt()
	{
		if (this.Prompt.Yandere.Inventory.Sake)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			this.Prompt.enabled = true;
			this.Prompt.Hide();
		}
	}

	// Token: 0x04002A8B RID: 10891
	public PromptScript Prompt;
}
