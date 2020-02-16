using System;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class DirectionalMicScript : MonoBehaviour
{
	// Token: 0x060018DB RID: 6363 RVA: 0x000E3F54 File Offset: 0x000E2354
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			InventoryScript inventory = this.Prompt.Yandere.Inventory;
			inventory.DirectionalMic = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001C97 RID: 7319
	public PromptScript Prompt;
}
