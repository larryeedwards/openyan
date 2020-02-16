using System;
using UnityEngine;

// Token: 0x020004C2 RID: 1218
public class RingScript : MonoBehaviour
{
	// Token: 0x06001F2C RID: 7980 RVA: 0x0013F1F4 File Offset: 0x0013D5F4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			SchemeGlobals.SetSchemeStage(2, 2);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Ring = true;
			this.Prompt.Yandere.TheftTimer = 0.1f;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040029BC RID: 10684
	public PromptScript Prompt;
}
