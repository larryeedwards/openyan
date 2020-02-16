using System;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class TextMessageManagerScript : MonoBehaviour
{
	// Token: 0x0600217D RID: 8573 RVA: 0x00194FCC File Offset: 0x001933CC
	private void Update()
	{
		if (Input.GetButtonDown("B"))
		{
			UnityEngine.Object.Destroy(this.NewMessage);
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.Sideways = true;
			this.ServicesMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x00195074 File Offset: 0x00193474
	public void SpawnMessage(int ServiceID)
	{
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[1].text = "Exit";
		this.PromptBar.UpdateButtons();
		this.PauseScreen.Sideways = false;
		this.ServicesMenu.SetActive(false);
		base.gameObject.SetActive(true);
		if (this.NewMessage != null)
		{
			UnityEngine.Object.Destroy(this.NewMessage);
		}
		this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.Message);
		this.NewMessage.transform.parent = base.transform;
		this.NewMessage.transform.localPosition = new Vector3(-225f, -275f, 0f);
		this.NewMessage.transform.localEulerAngles = Vector3.zero;
		this.NewMessage.transform.localScale = new Vector3(1f, 1f, 1f);
		this.MessageText = this.Messages[ServiceID];
		if (ServiceID == 7 || ServiceID == 4)
		{
			this.MessageHeight = 11;
		}
		else
		{
			this.MessageHeight = 5;
		}
		this.NewMessage.GetComponent<UISprite>().height = 36 + 36 * this.MessageHeight;
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.MessageText;
	}

	// Token: 0x04003606 RID: 13830
	public PauseScreenScript PauseScreen;

	// Token: 0x04003607 RID: 13831
	public PromptBarScript PromptBar;

	// Token: 0x04003608 RID: 13832
	public GameObject ServicesMenu;

	// Token: 0x04003609 RID: 13833
	public string[] Messages;

	// Token: 0x0400360A RID: 13834
	private GameObject NewMessage;

	// Token: 0x0400360B RID: 13835
	public GameObject Message;

	// Token: 0x0400360C RID: 13836
	public int MessageHeight;

	// Token: 0x0400360D RID: 13837
	public string MessageText = string.Empty;
}
