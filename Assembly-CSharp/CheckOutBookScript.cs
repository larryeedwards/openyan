using System;
using UnityEngine;

// Token: 0x02000360 RID: 864
public class CheckOutBookScript : MonoBehaviour
{
	// Token: 0x060017CA RID: 6090 RVA: 0x000BDDBF File Offset: 0x000BC1BF
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Book = true;
			this.UpdatePrompt();
		}
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x000BDDFC File Offset: 0x000BC1FC
	public void UpdatePrompt()
	{
		if (this.Prompt.Yandere.Inventory.Book)
		{
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
		else
		{
			this.Prompt.enabled = true;
			this.Prompt.Hide();
		}
	}

	// Token: 0x040017E5 RID: 6117
	public PromptScript Prompt;
}
