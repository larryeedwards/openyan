using System;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class RoseBushScript : MonoBehaviour
{
	// Token: 0x06001F50 RID: 8016 RVA: 0x001401BC File Offset: 0x0013E5BC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.Rose = true;
			base.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04002A71 RID: 10865
	public PromptScript Prompt;
}
