using System;
using UnityEngine;

// Token: 0x02000513 RID: 1299
public class SodaScript : MonoBehaviour
{
	// Token: 0x06002024 RID: 8228 RVA: 0x0014EA0C File Offset: 0x0014CE0C
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Soda = true;
			this.Prompt.Yandere.StudentManager.TaskManager.UpdateTaskStatus();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002CDC RID: 11484
	public PromptScript Prompt;
}
