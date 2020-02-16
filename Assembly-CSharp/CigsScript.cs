using System;
using UnityEngine;

// Token: 0x02000365 RID: 869
public class CigsScript : MonoBehaviour
{
	// Token: 0x060017D6 RID: 6102 RVA: 0x000BE4E0 File Offset: 0x000BC8E0
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			SchemeGlobals.SetSchemeStage(3, 3);
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.Inventory.Cigs = true;
			UnityEngine.Object.Destroy(base.gameObject);
			this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}

	// Token: 0x040017FC RID: 6140
	public PromptScript Prompt;
}
