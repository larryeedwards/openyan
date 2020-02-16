using System;
using UnityEngine;

// Token: 0x02000454 RID: 1108
public class LockpickScript : MonoBehaviour
{
	// Token: 0x06001D80 RID: 7552 RVA: 0x00115D30 File Offset: 0x00114130
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			InventoryScript inventory = this.Prompt.Yandere.Inventory;
			inventory.LockPick = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040024CE RID: 9422
	public PromptScript Prompt;
}
