using System;
using UnityEngine;

// Token: 0x0200044A RID: 1098
public class LaptopScript : MonoBehaviour
{
	// Token: 0x06001D62 RID: 7522 RVA: 0x0011323C File Offset: 0x0011163C
	private void Start()
	{
		if (SchoolGlobals.SCP)
		{
			this.LaptopScreen.localScale = Vector3.zero;
			this.LaptopCamera.enabled = false;
			this.SCP.SetActive(false);
			base.enabled = false;
		}
		else
		{
			this.SCPRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
			Animation component = this.SCP.GetComponent<Animation>();
			component["f02_scp_00"].speed = 0f;
			component["f02_scp_00"].time = 0f;
			this.MyAudio = base.GetComponent<AudioSource>();
		}
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x001132E0 File Offset: 0x001116E0
	private void Update()
	{
		if (this.FirstFrame == 2)
		{
			this.LaptopCamera.enabled = false;
		}
		this.FirstFrame++;
		if (!this.Off)
		{
			Animation component = this.SCP.GetComponent<Animation>();
			if (!this.React)
			{
				if (this.Yandere.transform.position.x > base.transform.position.x + 1f && Vector3.Distance(this.Yandere.transform.position, new Vector3(base.transform.position.x, 4f, base.transform.position.z)) < 2f && this.Yandere.Followers == 0)
				{
					this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
					component["f02_scp_00"].time = 0f;
					this.LaptopCamera.enabled = true;
					component.Play();
					this.Hair.enabled = true;
					this.Jukebox.Dip = 0.5f;
					this.MyAudio.Play();
					this.React = true;
				}
			}
			else
			{
				this.MyAudio.pitch = Time.timeScale;
				this.MyAudio.volume = 1f;
				if (this.Yandere.transform.position.y > base.transform.position.y + 3f || this.Yandere.transform.position.y < base.transform.position.y - 3f)
				{
					this.MyAudio.volume = 0f;
				}
				for (int i = 0; i < this.Cues.Length; i++)
				{
					if (this.MyAudio.time > this.Cues[i])
					{
						this.EventSubtitle.text = this.Subs[i];
					}
				}
				if (this.MyAudio.time >= this.MyAudio.clip.length - 1f || this.MyAudio.time == 0f)
				{
					component["f02_scp_00"].speed = 1f;
					this.Timer += Time.deltaTime;
				}
				else
				{
					component["f02_scp_00"].time = this.MyAudio.time;
				}
				if (this.Timer > 1f || Vector3.Distance(this.Yandere.transform.position, new Vector3(base.transform.position.x, 4f, base.transform.position.z)) > 5f)
				{
					this.TurnOff();
				}
			}
			if (this.Yandere.StudentManager.Clock.HourTime > 16f || this.Yandere.Police.FadeOut)
			{
				this.TurnOff();
			}
		}
		else if (this.LaptopScreen.localScale.x > 0.1f)
		{
			this.LaptopScreen.localScale = Vector3.Lerp(this.LaptopScreen.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
		else if (base.enabled)
		{
			this.LaptopScreen.localScale = Vector3.zero;
			this.Hair.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x001136D8 File Offset: 0x00111AD8
	private void TurnOff()
	{
		this.MyAudio.clip = this.ShutDown;
		this.MyAudio.Play();
		this.EventSubtitle.text = string.Empty;
		SchoolGlobals.SCP = true;
		this.LaptopCamera.enabled = false;
		this.Jukebox.Dip = 1f;
		this.React = false;
		this.Off = true;
	}

	// Token: 0x0400245D RID: 9309
	public SkinnedMeshRenderer SCPRenderer;

	// Token: 0x0400245E RID: 9310
	public Camera LaptopCamera;

	// Token: 0x0400245F RID: 9311
	public JukeboxScript Jukebox;

	// Token: 0x04002460 RID: 9312
	public YandereScript Yandere;

	// Token: 0x04002461 RID: 9313
	public AudioSource MyAudio;

	// Token: 0x04002462 RID: 9314
	public DynamicBone Hair;

	// Token: 0x04002463 RID: 9315
	public Transform LaptopScreen;

	// Token: 0x04002464 RID: 9316
	public AudioClip ShutDown;

	// Token: 0x04002465 RID: 9317
	public GameObject SCP;

	// Token: 0x04002466 RID: 9318
	public bool React;

	// Token: 0x04002467 RID: 9319
	public bool Off;

	// Token: 0x04002468 RID: 9320
	public float[] Cues;

	// Token: 0x04002469 RID: 9321
	public string[] Subs;

	// Token: 0x0400246A RID: 9322
	public Mesh[] Uniforms;

	// Token: 0x0400246B RID: 9323
	public int FirstFrame;

	// Token: 0x0400246C RID: 9324
	public float Timer;

	// Token: 0x0400246D RID: 9325
	public UILabel EventSubtitle;
}
