using System;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class RivalEditorScript : MonoBehaviour
{
	// Token: 0x06001913 RID: 6419 RVA: 0x000E7ACC File Offset: 0x000E5ECC
	private void Awake()
	{
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001914 RID: 6420 RVA: 0x000E7ADC File Offset: 0x000E5EDC
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x000E7B3C File Offset: 0x000E5F3C
	private void HandleInput()
	{
		bool buttonDown = Input.GetButtonDown("B");
		if (buttonDown)
		{
			this.mainPanel.gameObject.SetActive(true);
			this.rivalPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x000E7B7C File Offset: 0x000E5F7C
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001D19 RID: 7449
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001D1A RID: 7450
	[SerializeField]
	private UIPanel rivalPanel;

	// Token: 0x04001D1B RID: 7451
	[SerializeField]
	private UILabel titleLabel;

	// Token: 0x04001D1C RID: 7452
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001D1D RID: 7453
	private InputManagerScript inputManager;
}
