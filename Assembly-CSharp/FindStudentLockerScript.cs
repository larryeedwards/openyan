using System;
using UnityEngine;

// Token: 0x020003D3 RID: 979
public class FindStudentLockerScript : MonoBehaviour
{
	// Token: 0x060019A0 RID: 6560 RVA: 0x000EF914 File Offset: 0x000EDD14
	private void Update()
	{
		if (this.Prompt.DistanceSqr < 5f)
		{
			this.TutorialWindow.ShowLockerMessage = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.PauseScreen.StudentInfoMenu.FindingLocker = true;
			this.Prompt.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
			this.Prompt.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
			this.Prompt.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
			this.Prompt.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
			this.Prompt.StartCoroutine(this.Prompt.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
			this.Prompt.Yandere.PauseScreen.MainMenu.SetActive(false);
			this.Prompt.Yandere.PauseScreen.Panel.enabled = true;
			this.Prompt.Yandere.PauseScreen.Sideways = true;
			this.Prompt.Yandere.PauseScreen.Show = true;
			Time.timeScale = 0.0001f;
			this.Prompt.Yandere.PromptBar.ClearButtons();
			this.Prompt.Yandere.PromptBar.Label[1].text = "Cancel";
			this.Prompt.Yandere.PromptBar.UpdateButtons();
			this.Prompt.Yandere.PromptBar.Show = true;
		}
	}

	// Token: 0x04001EA2 RID: 7842
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04001EA3 RID: 7843
	public PromptScript Prompt;
}
