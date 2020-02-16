using System;
using UnityEngine;

// Token: 0x02000410 RID: 1040
public class HeadsetScript : MonoBehaviour
{
	// Token: 0x06001C74 RID: 7284 RVA: 0x000FF1B8 File Offset: 0x000FD5B8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Headset = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040020E7 RID: 8423
	public PromptScript Prompt;
}
