using System;
using UnityEngine;

// Token: 0x02000545 RID: 1349
public class TaskPickupScript : MonoBehaviour
{
	// Token: 0x06002170 RID: 8560 RVA: 0x001948CA File Offset: 0x00192CCA
	private void Update()
	{
		if (this.Prompt.Circle[this.ButtonID].fillAmount == 0f)
		{
			this.Prompt.Yandere.StudentManager.TaskManager.CheckTaskPickups();
		}
	}

	// Token: 0x040035EE RID: 13806
	public PromptScript Prompt;

	// Token: 0x040035EF RID: 13807
	public int ButtonID = 3;
}
