using System;
using UnityEngine;

// Token: 0x020004D2 RID: 1234
public class RivalPhoneScript : MonoBehaviour
{
	// Token: 0x06001F45 RID: 8005 RVA: 0x0013F728 File Offset: 0x0013DB28
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (SchemeGlobals.GetSchemeStage(1) == 4)
			{
				SchemeGlobals.SetSchemeStage(1, 5);
				this.Prompt.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
			this.Prompt.Yandere.Inventory.Schemes.UpdateInstructions();
			this.Prompt.Yandere.RivalPhoneTexture = this.MyRenderer.material.mainTexture;
			this.Prompt.Yandere.Inventory.RivalPhone = true;
			this.Prompt.enabled = false;
			base.enabled = false;
			base.gameObject.SetActive(false);
			this.Stolen = true;
		}
	}

	// Token: 0x04002A57 RID: 10839
	public Renderer MyRenderer;

	// Token: 0x04002A58 RID: 10840
	public PromptScript Prompt;

	// Token: 0x04002A59 RID: 10841
	public bool LewdPhotos;

	// Token: 0x04002A5A RID: 10842
	public bool Stolen;
}
