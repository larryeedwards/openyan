using System;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class EventEditorScript : MonoBehaviour
{
	// Token: 0x0600190E RID: 6414 RVA: 0x000E7A0E File Offset: 0x000E5E0E
	private void Awake()
	{
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x0600190F RID: 6415 RVA: 0x000E7A1C File Offset: 0x000E5E1C
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.Label[4].text = string.Empty;
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001910 RID: 6416 RVA: 0x000E7A7C File Offset: 0x000E5E7C
	private void HandleInput()
	{
		bool buttonDown = Input.GetButtonDown("B");
		if (buttonDown)
		{
			this.mainPanel.gameObject.SetActive(true);
			this.eventPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x000E7ABC File Offset: 0x000E5EBC
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001D14 RID: 7444
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001D15 RID: 7445
	[SerializeField]
	private UIPanel eventPanel;

	// Token: 0x04001D16 RID: 7446
	[SerializeField]
	private UILabel titleLabel;

	// Token: 0x04001D17 RID: 7447
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001D18 RID: 7448
	private InputManagerScript inputManager;
}
