using System;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public class PeekScript : MonoBehaviour
{
	// Token: 0x06001E35 RID: 7733 RVA: 0x00125A3C File Offset: 0x00123E3C
	private void Start()
	{
		this.Prompt.Door = true;
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x00125A4C File Offset: 0x00123E4C
	private void Update()
	{
		float num = Vector3.Distance(base.transform.position, this.Prompt.Yandere.transform.position);
		if (num < 2f)
		{
			this.Prompt.Yandere.StudentManager.TutorialWindow.ShowInfoMessage = true;
		}
		if (this.InfoChanWindow.Drop)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Prompt.Yandere.Chased && this.Prompt.Yandere.Chasers == 0)
			{
				this.Prompt.Yandere.CanMove = false;
				this.PeekCamera.SetActive(true);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[1].text = "Stop";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
		if (this.PeekCamera.activeInHierarchy)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 5f && !this.Spoke)
			{
				this.Subtitle.UpdateLabel(SubtitleType.InfoNotice, 0, 6.5f);
				this.Spoke = true;
				base.GetComponent<AudioSource>().Play();
			}
			if (Input.GetButtonDown("B"))
			{
				this.Prompt.Yandere.CanMove = true;
				this.PeekCamera.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
				this.Timer = 0f;
			}
		}
	}

	// Token: 0x040026C1 RID: 9921
	public InfoChanWindowScript InfoChanWindow;

	// Token: 0x040026C2 RID: 9922
	public PromptBarScript PromptBar;

	// Token: 0x040026C3 RID: 9923
	public SubtitleScript Subtitle;

	// Token: 0x040026C4 RID: 9924
	public JukeboxScript Jukebox;

	// Token: 0x040026C5 RID: 9925
	public PromptScript Prompt;

	// Token: 0x040026C6 RID: 9926
	public GameObject PeekCamera;

	// Token: 0x040026C7 RID: 9927
	public bool Spoke;

	// Token: 0x040026C8 RID: 9928
	public float Timer;
}
