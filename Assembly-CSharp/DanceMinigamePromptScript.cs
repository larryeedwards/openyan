using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class DanceMinigamePromptScript : MonoBehaviour
{
	// Token: 0x06001882 RID: 6274 RVA: 0x000D7FBC File Offset: 0x000D63BC
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.transform.position = this.PlayerLocation.position;
			this.Prompt.Yandere.transform.rotation = this.PlayerLocation.rotation;
			this.Prompt.Yandere.CharacterAnimation.Play("f02_danceMachineIdle_00");
			this.Prompt.Yandere.StudentManager.Clock.StopTime = true;
			this.Prompt.Yandere.MyController.enabled = false;
			this.Prompt.Yandere.HeartCamera.enabled = false;
			this.Prompt.Yandere.HUD.enabled = false;
			this.Prompt.Yandere.CanMove = false;
			this.Prompt.Yandere.enabled = false;
			this.Prompt.Yandere.Jukebox.LastVolume = this.Prompt.Yandere.Jukebox.Volume;
			this.Prompt.Yandere.Jukebox.Volume = 0f;
			this.Prompt.Yandere.HUD.transform.parent.gameObject.SetActive(false);
			this.Prompt.Yandere.MainCamera.gameObject.SetActive(false);
			this.OriginalRenderer.enabled = false;
			Physics.SyncTransforms();
			this.DanceMinigame.SetActive(true);
			this.DanceManager.BeginMinigame();
			this.StudentManager.DisableEveryone();
		}
	}

	// Token: 0x04001B25 RID: 6949
	public StudentManagerScript StudentManager;

	// Token: 0x04001B26 RID: 6950
	public Renderer OriginalRenderer;

	// Token: 0x04001B27 RID: 6951
	public DDRManager DanceManager;

	// Token: 0x04001B28 RID: 6952
	public PromptScript Prompt;

	// Token: 0x04001B29 RID: 6953
	public ClockScript Clock;

	// Token: 0x04001B2A RID: 6954
	public GameObject DanceMinigame;

	// Token: 0x04001B2B RID: 6955
	public Transform PlayerLocation;
}
