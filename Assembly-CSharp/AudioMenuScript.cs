using System;
using UnityEngine;

// Token: 0x02000331 RID: 817
public class AudioMenuScript : MonoBehaviour
{
	// Token: 0x06001737 RID: 5943 RVA: 0x000B69B6 File Offset: 0x000B4DB6
	private void Start()
	{
		this.UpdateText();
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x000B69C0 File Offset: 0x000B4DC0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			this.CustomMusicMenu.SetActive(true);
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
		if (this.Selected == 1)
		{
			if (this.InputManager.TappedRight)
			{
				if (this.Jukebox.Volume < 1f)
				{
					this.Jukebox.Volume += 0.05f;
				}
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				if (this.Jukebox.Volume > 0f)
				{
					this.Jukebox.Volume -= 0.05f;
				}
				this.UpdateText();
			}
		}
		else if (this.Selected == 2 && (this.InputManager.TappedRight || this.InputManager.TappedLeft))
		{
			this.Jukebox.StartStopMusic();
			this.UpdateText();
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.PauseScreen.ScreenBlur.enabled = true;
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x000B6BC8 File Offset: 0x000B4FC8
	public void UpdateText()
	{
		if (this.Jukebox != null)
		{
			this.CurrentTrackLabel.text = "Current Track: " + this.Jukebox.BGM;
			this.MusicVolumeLabel.text = string.Empty + (this.Jukebox.Volume * 10f).ToString("F1");
			if (this.Jukebox.Volume == 0f)
			{
				this.MusicOnOffLabel.text = "Off";
			}
			else
			{
				this.MusicOnOffLabel.text = "On";
			}
		}
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000B6C78 File Offset: 0x000B5078
	private void UpdateHighlight()
	{
		if (this.Selected == 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 1;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 440f - 60f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x04001691 RID: 5777
	public InputManagerScript InputManager;

	// Token: 0x04001692 RID: 5778
	public PauseScreenScript PauseScreen;

	// Token: 0x04001693 RID: 5779
	public PromptBarScript PromptBar;

	// Token: 0x04001694 RID: 5780
	public JukeboxScript Jukebox;

	// Token: 0x04001695 RID: 5781
	public UILabel CurrentTrackLabel;

	// Token: 0x04001696 RID: 5782
	public UILabel MusicVolumeLabel;

	// Token: 0x04001697 RID: 5783
	public UILabel MusicOnOffLabel;

	// Token: 0x04001698 RID: 5784
	public int SelectionLimit = 5;

	// Token: 0x04001699 RID: 5785
	public int Selected = 1;

	// Token: 0x0400169A RID: 5786
	public Transform Highlight;

	// Token: 0x0400169B RID: 5787
	public GameObject CustomMusicMenu;
}
