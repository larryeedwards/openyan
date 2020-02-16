using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200046C RID: 1132
public class MusicMenuScript : MonoBehaviour
{
	// Token: 0x06001DCE RID: 7630 RVA: 0x0011C1DC File Offset: 0x0011A5DC
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.AudioMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedDown)
		{
			this.Selected++;
			this.UpdateHighlight();
		}
		if (Input.GetButtonDown("A"))
		{
			base.StartCoroutine(this.DownloadCoroutine());
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

	// Token: 0x06001DCF RID: 7631 RVA: 0x0011C318 File Offset: 0x0011A718
	private IEnumerator DownloadCoroutine()
	{
		WWW CurrentDownload = new WWW(string.Concat(new object[]
		{
			"File:///",
			Application.streamingAssetsPath,
			"/Music/track",
			this.Selected,
			".ogg"
		}));
		yield return CurrentDownload;
		this.CustomMusic = CurrentDownload.GetAudioClipCompressed();
		this.Jukebox.Custom.clip = this.CustomMusic;
		this.Jukebox.PlayCustom();
		yield break;
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x0011C334 File Offset: 0x0011A734
	private void UpdateHighlight()
	{
		if (this.Selected < 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 0;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 365f - 80f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x0400258C RID: 9612
	public InputManagerScript InputManager;

	// Token: 0x0400258D RID: 9613
	public PauseScreenScript PauseScreen;

	// Token: 0x0400258E RID: 9614
	public PromptBarScript PromptBar;

	// Token: 0x0400258F RID: 9615
	public GameObject AudioMenu;

	// Token: 0x04002590 RID: 9616
	public JukeboxScript Jukebox;

	// Token: 0x04002591 RID: 9617
	public int SelectionLimit = 9;

	// Token: 0x04002592 RID: 9618
	public int Selected;

	// Token: 0x04002593 RID: 9619
	public Transform Highlight;

	// Token: 0x04002594 RID: 9620
	public string path = string.Empty;

	// Token: 0x04002595 RID: 9621
	public AudioClip CustomMusic;
}
