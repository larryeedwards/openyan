using System;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class DemonPortalScript : MonoBehaviour
{
	// Token: 0x060018C1 RID: 6337 RVA: 0x000DF3D8 File Offset: 0x000DD7D8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
			this.Yandere.CanMove = false;
			UnityEngine.Object.Instantiate<GameObject>(this.DarkAura, this.Yandere.transform.position + Vector3.up * 0.81f, Quaternion.identity);
			this.Timer += Time.deltaTime;
		}
		this.DemonRealmAudio.volume = Mathf.MoveTowards(this.DemonRealmAudio.volume, (this.Yandere.transform.position.y <= 1000f) ? 0f : 0.5f, Time.deltaTime * 0.1f);
		if (this.Timer > 0f)
		{
			if (this.Yandere.transform.position.y > 1000f)
			{
				this.Timer += Time.deltaTime;
				if (this.Timer > 4f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
					if (this.Darkness.color.a == 1f)
					{
						this.Yandere.transform.position = new Vector3(12f, 0f, 28f);
						this.Yandere.Character.SetActive(true);
						this.Yandere.SetAnimationLayers();
						this.HeartbeatCamera.SetActive(true);
						this.FPS.SetActive(true);
						this.HUD.SetActive(true);
					}
				}
				else if (this.Timer > 1f)
				{
					this.Yandere.Character.SetActive(false);
				}
			}
			else
			{
				this.Jukebox.Volume = Mathf.MoveTowards(this.Jukebox.Volume, 0.5f, Time.deltaTime * 0.5f);
				if (this.Jukebox.Volume == 0.5f)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
					if (this.Darkness.color.a == 0f)
					{
						this.Darkness.enabled = false;
						this.Yandere.CanMove = true;
						this.Clock.StopTime = false;
						this.Timer = 0f;
					}
				}
			}
		}
	}

	// Token: 0x04001C37 RID: 7223
	public YandereScript Yandere;

	// Token: 0x04001C38 RID: 7224
	public JukeboxScript Jukebox;

	// Token: 0x04001C39 RID: 7225
	public PromptScript Prompt;

	// Token: 0x04001C3A RID: 7226
	public ClockScript Clock;

	// Token: 0x04001C3B RID: 7227
	public AudioSource DemonRealmAudio;

	// Token: 0x04001C3C RID: 7228
	public GameObject HeartbeatCamera;

	// Token: 0x04001C3D RID: 7229
	public GameObject DarkAura;

	// Token: 0x04001C3E RID: 7230
	public GameObject FPS;

	// Token: 0x04001C3F RID: 7231
	public GameObject HUD;

	// Token: 0x04001C40 RID: 7232
	public UISprite Darkness;

	// Token: 0x04001C41 RID: 7233
	public float Timer;
}
