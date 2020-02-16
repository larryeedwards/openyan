using System;
using UnityEngine;

// Token: 0x0200055A RID: 1370
public class TranquilizerScript : MonoBehaviour
{
	// Token: 0x060021C1 RID: 8641 RVA: 0x0019907C File Offset: 0x0019747C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Tranquilizer = true;
			this.Prompt.Yandere.StudentManager.UpdateAllBentos();
			this.Prompt.Yandere.TheftTimer = 0.1f;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040036CA RID: 14026
	public PromptScript Prompt;
}
