using System;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class IDCardScript : MonoBehaviour
{
	// Token: 0x06001CEB RID: 7403 RVA: 0x0010AB0C File Offset: 0x00108F0C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.Prompt.Yandere.StolenObject = base.gameObject;
			if (!this.Fake)
			{
				this.Prompt.Yandere.Inventory.IDCard = true;
				this.Prompt.Yandere.TheftTimer = 1f;
			}
			else
			{
				this.Prompt.Yandere.Inventory.FakeID = true;
			}
			this.Prompt.Hide();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040022BE RID: 8894
	public PromptScript Prompt;

	// Token: 0x040022BF RID: 8895
	public bool Fake;
}
