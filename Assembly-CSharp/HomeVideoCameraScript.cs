using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class HomeVideoCameraScript : MonoBehaviour
{
	// Token: 0x06001CD2 RID: 7378 RVA: 0x00108E80 File Offset: 0x00107280
	private void Update()
	{
		if (!this.TextSet && !HomeGlobals.Night)
		{
			this.Prompt.Label[0].text = "     Only Available At Night";
		}
		if (!HomeGlobals.Night)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.HomeCamera.Destination = this.HomeCamera.Destinations[11];
			this.HomeCamera.Target = this.HomeCamera.Targets[11];
			this.HomeCamera.ID = 11;
			this.HomePrisonerChan.LookAhead = true;
			this.HomeYandere.CanMove = false;
			this.HomeYandere.gameObject.SetActive(false);
		}
		if (this.HomeCamera.ID == 11 && !this.HomePrisoner.Bantering)
		{
			this.Timer += Time.deltaTime;
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.Timer > 2f && !this.AudioPlayed)
			{
				this.Subtitle.text = "...daddy...please...help...I'm scared...I don't wanna die...";
				this.AudioPlayed = true;
				component.Play();
			}
			if (this.Timer > 2f + component.clip.length)
			{
				this.Subtitle.text = string.Empty;
			}
			if (this.Timer > 3f + component.clip.length)
			{
				this.HomeDarkness.FadeSlow = true;
				this.HomeDarkness.FadeOut = true;
			}
		}
	}

	// Token: 0x0400227F RID: 8831
	public HomePrisonerChanScript HomePrisonerChan;

	// Token: 0x04002280 RID: 8832
	public HomeDarknessScript HomeDarkness;

	// Token: 0x04002281 RID: 8833
	public HomePrisonerScript HomePrisoner;

	// Token: 0x04002282 RID: 8834
	public HomeYandereScript HomeYandere;

	// Token: 0x04002283 RID: 8835
	public HomeCameraScript HomeCamera;

	// Token: 0x04002284 RID: 8836
	public PromptScript Prompt;

	// Token: 0x04002285 RID: 8837
	public UILabel Subtitle;

	// Token: 0x04002286 RID: 8838
	public bool AudioPlayed;

	// Token: 0x04002287 RID: 8839
	public bool TextSet;

	// Token: 0x04002288 RID: 8840
	public float Timer;
}
