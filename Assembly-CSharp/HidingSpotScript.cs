using System;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class HidingSpotScript : MonoBehaviour
{
	// Token: 0x06001C84 RID: 7300 RVA: 0x00101CD4 File Offset: 0x001000D4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0 && this.Prompt.Yandere.Pursuer == null)
			{
				this.Prompt.Yandere.MyController.center = new Vector3(this.Prompt.Yandere.MyController.center.x, 0.3f, this.Prompt.Yandere.MyController.center.z);
				this.Prompt.Yandere.MyController.radius = 0f;
				this.Prompt.Yandere.MyController.height = 0.5f;
				this.Prompt.Yandere.HideAnim = this.AnimName;
				this.Prompt.Yandere.HidingSpot = this.Spot;
				this.Prompt.Yandere.ExitSpot = this.Exit;
				this.Prompt.Yandere.CanMove = false;
				this.Prompt.Yandere.Hiding = true;
				this.Prompt.Yandere.EmptyHands();
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
	}

	// Token: 0x04002140 RID: 8512
	public PromptBarScript PromptBar;

	// Token: 0x04002141 RID: 8513
	public PromptScript Prompt;

	// Token: 0x04002142 RID: 8514
	public Transform Exit;

	// Token: 0x04002143 RID: 8515
	public Transform Spot;

	// Token: 0x04002144 RID: 8516
	public string AnimName;
}
