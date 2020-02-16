using System;
using UnityEngine;

// Token: 0x020003D2 RID: 978
public class FavorMenuScript : MonoBehaviour
{
	// Token: 0x0600199D RID: 6557 RVA: 0x000EF66C File Offset: 0x000EDA6C
	private void Update()
	{
		if (this.InputManager.TappedRight)
		{
			this.ID++;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedLeft)
		{
			this.ID--;
			this.UpdateHighlight();
		}
		if (Input.GetButtonDown("A"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			if (this.ID != 1)
			{
				if (this.ID == 2)
				{
					this.ServicesMenu.UpdatePantyCount();
					this.ServicesMenu.UpdateList();
					this.ServicesMenu.UpdateDesc();
					this.ServicesMenu.gameObject.SetActive(true);
					base.gameObject.SetActive(false);
				}
				else if (this.ID == 3)
				{
					this.DropsMenu.UpdatePantyCount();
					this.DropsMenu.UpdateList();
					this.DropsMenu.UpdateDesc();
					this.DropsMenu.gameObject.SetActive(true);
					base.gameObject.SetActive(false);
				}
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x000EF87C File Offset: 0x000EDC7C
	private void UpdateHighlight()
	{
		if (this.ID > 3)
		{
			this.ID = 1;
		}
		else if (this.ID < 1)
		{
			this.ID = 3;
		}
		this.Highlight.transform.localPosition = new Vector3(-500f + 250f * (float)this.ID, this.Highlight.transform.localPosition.y, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x04001E9A RID: 7834
	public InputManagerScript InputManager;

	// Token: 0x04001E9B RID: 7835
	public PauseScreenScript PauseScreen;

	// Token: 0x04001E9C RID: 7836
	public ServicesScript ServicesMenu;

	// Token: 0x04001E9D RID: 7837
	public SchemesScript SchemesMenu;

	// Token: 0x04001E9E RID: 7838
	public DropsScript DropsMenu;

	// Token: 0x04001E9F RID: 7839
	public PromptBarScript PromptBar;

	// Token: 0x04001EA0 RID: 7840
	public Transform Highlight;

	// Token: 0x04001EA1 RID: 7841
	public int ID = 1;
}
