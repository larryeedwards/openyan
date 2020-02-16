using System;
using UnityEngine;

// Token: 0x02000459 RID: 1113
public class MaskingTapeScript : MonoBehaviour
{
	// Token: 0x06001D91 RID: 7569 RVA: 0x00118084 File Offset: 0x00116484
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Inventory.MaskingTape = true;
			this.Box.Prompt.enabled = true;
			this.Box.enabled = true;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002509 RID: 9481
	public CarryableCardboardBoxScript Box;

	// Token: 0x0400250A RID: 9482
	public PromptScript Prompt;
}
