using System;
using System.Collections.Generic;
using System.IO;
using JsonFx.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003A5 RID: 933
public class EditorManagerScript : MonoBehaviour
{
	// Token: 0x06001907 RID: 6407 RVA: 0x000E77D0 File Offset: 0x000E5BD0
	private void Awake()
	{
		this.buttonIndex = 0;
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x000E77E4 File Offset: 0x000E5BE4
	private void Start()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x000E7844 File Offset: 0x000E5C44
	private void OnEnable()
	{
		this.promptBar.Label[0].text = "Select";
		this.promptBar.Label[1].text = "Exit";
		this.promptBar.Label[4].text = "Choose";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x000E78A4 File Offset: 0x000E5CA4
	public static Dictionary<string, object>[] DeserializeJson(string filename)
	{
		string path = Path.Combine(Application.streamingAssetsPath, Path.Combine("JSON", filename));
		string value = File.ReadAllText(path);
		return JsonReader.Deserialize<Dictionary<string, object>[]>(value);
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x000E78D4 File Offset: 0x000E5CD4
	private void HandleInput()
	{
		bool buttonDown = Input.GetButtonDown("B");
		if (buttonDown)
		{
			SceneManager.LoadScene("TitleScene");
		}
		bool tappedUp = this.inputManager.TappedUp;
		bool tappedDown = this.inputManager.TappedDown;
		if (tappedUp)
		{
			this.buttonIndex = ((this.buttonIndex <= 0) ? 2 : (this.buttonIndex - 1));
		}
		else if (tappedDown)
		{
			this.buttonIndex = ((this.buttonIndex >= 2) ? 0 : (this.buttonIndex + 1));
		}
		bool flag = tappedUp || tappedDown;
		if (flag)
		{
			Transform transform = this.cursorLabel.transform;
			transform.localPosition = new Vector3(transform.localPosition.x, 100f - (float)this.buttonIndex * 100f, transform.localPosition.z);
		}
		bool buttonDown2 = Input.GetButtonDown("A");
		if (buttonDown2)
		{
			this.editorPanels[this.buttonIndex].gameObject.SetActive(true);
			this.mainPanel.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x000E79FE File Offset: 0x000E5DFE
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001D0D RID: 7437
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001D0E RID: 7438
	[SerializeField]
	private UIPanel[] editorPanels;

	// Token: 0x04001D0F RID: 7439
	[SerializeField]
	private UILabel cursorLabel;

	// Token: 0x04001D10 RID: 7440
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001D11 RID: 7441
	private int buttonIndex;

	// Token: 0x04001D12 RID: 7442
	private const int ButtonCount = 3;

	// Token: 0x04001D13 RID: 7443
	private InputManagerScript inputManager;
}
