using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200044D RID: 1101
public class LifeNoteScript : MonoBehaviour
{
	// Token: 0x06001D6B RID: 7531 RVA: 0x00113900 File Offset: 0x00111D00
	private void Start()
	{
		Application.targetFrameRate = 60;
		this.Label.text = this.Lines[this.ID];
		this.Controls.SetActive(false);
		this.Label.gameObject.SetActive(false);
		this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		this.BackgroundArt.localPosition = new Vector3(0f, -540f, 0f);
		this.BackgroundArt.localScale = new Vector3(2.5f, 2.5f, 1f);
		this.TextWindow.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x001139D0 File Offset: 0x00111DD0
	private void Update()
	{
		if (this.Controls.activeInHierarchy)
		{
			if (this.Typewriter.mCurrentOffset == 1)
			{
				if (this.Reds[this.ID])
				{
					this.Label.color = new Color(1f, 0f, 0f, 1f);
				}
				else
				{
					this.Label.color = new Color(1f, 1f, 1f, 1f);
				}
			}
			if (Input.GetButtonDown("A") || this.AutoTimer > 0.5f)
			{
				if (this.ID < this.Lines.Length - 1)
				{
					if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
					{
						this.Typewriter.Finish();
					}
					else
					{
						this.ID++;
						this.Alpha = (float)this.Alphas[this.ID];
						this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
						this.Typewriter.ResetToBeginning();
						this.Typewriter.mFullText = this.Lines[this.ID];
						this.Label.text = string.Empty;
						this.Spoke = false;
						this.Frame = 0;
						if (this.Alphas[this.ID] == 1)
						{
							this.Jukebox.Stop();
						}
						else if (!this.Jukebox.isPlaying)
						{
							this.Jukebox.Play();
						}
						if (this.ID == 17)
						{
							this.SFXAudioSource.clip = this.SFX[1];
							this.SFXAudioSource.Play();
						}
						if (this.ID == 18)
						{
							this.SFXAudioSource.clip = this.SFX[2];
							this.SFXAudioSource.Play();
						}
						if (this.ID > 25)
						{
							this.Typewriter.charsPerSecond = 15;
						}
						this.AutoTimer = 0f;
					}
				}
				else if (!this.FinalDarkness.enabled)
				{
					this.FinalDarkness.enabled = true;
					this.Alpha = 0f;
				}
			}
			if (!this.Spoke && !this.SFXAudioSource.isPlaying)
			{
				this.MyAudio.clip = this.Voices[this.ID];
				this.MyAudio.Play();
				this.Spoke = true;
			}
			if (this.Auto && this.Typewriter.mCurrentOffset == this.Typewriter.mFullText.Length && !this.SFXAudioSource.isPlaying && !this.MyAudio.isPlaying)
			{
				this.AutoTimer += Time.deltaTime;
			}
			if (this.FinalDarkness.enabled)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.2f);
				this.FinalDarkness.color = new Color(0f, 0f, 0f, this.Alpha);
				if (this.Alpha == 1f)
				{
					SceneManager.LoadScene("HomeScene");
				}
			}
		}
		if (this.TextWindow.color.a < 1f)
		{
			if (Input.GetButtonDown("A"))
			{
				this.Darkness.color = new Color(0f, 0f, 0f, 0f);
				this.BackgroundArt.localPosition = new Vector3(0f, 0f, 0f);
				this.BackgroundArt.localScale = new Vector3(1f, 1f, 1f);
				this.TextWindow.color = new Color(1f, 1f, 1f, 1f);
				this.Label.color = new Color(1f, 1f, 1f, 0f);
				this.Label.gameObject.SetActive(true);
				this.Controls.SetActive(true);
				this.Timer = 0f;
			}
			this.Timer += Time.deltaTime;
			if (this.Timer > 6f)
			{
				this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
				this.TextWindow.color = new Color(1f, 1f, 1f, this.Alpha);
				if (this.TextWindow.color.a == 1f && !this.Typewriter.mActive)
				{
					this.Label.color = new Color(1f, 1f, 1f, 0f);
					this.Label.gameObject.SetActive(true);
					this.Controls.SetActive(true);
					this.Timer = 0f;
				}
			}
			else if (this.Timer > 2f)
			{
				this.BackgroundArt.localScale = Vector3.Lerp(this.BackgroundArt.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * (this.Timer - 2f));
				this.BackgroundArt.localPosition = Vector3.Lerp(this.BackgroundArt.localPosition, new Vector3(0f, 0f, 0f), Time.deltaTime * (this.Timer - 2f));
			}
			else if (this.Timer > 0f)
			{
				this.Darkness.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			}
		}
	}

	// Token: 0x04002472 RID: 9330
	public UITexture Darkness;

	// Token: 0x04002473 RID: 9331
	public UITexture TextWindow;

	// Token: 0x04002474 RID: 9332
	public UITexture FinalDarkness;

	// Token: 0x04002475 RID: 9333
	public Transform BackgroundArt;

	// Token: 0x04002476 RID: 9334
	public TypewriterEffect Typewriter;

	// Token: 0x04002477 RID: 9335
	public GameObject Controls;

	// Token: 0x04002478 RID: 9336
	public AudioSource MyAudio;

	// Token: 0x04002479 RID: 9337
	public AudioClip[] Voices;

	// Token: 0x0400247A RID: 9338
	public string[] Lines;

	// Token: 0x0400247B RID: 9339
	public int[] Alphas;

	// Token: 0x0400247C RID: 9340
	public bool[] Reds;

	// Token: 0x0400247D RID: 9341
	public UILabel Label;

	// Token: 0x0400247E RID: 9342
	public float Timer;

	// Token: 0x0400247F RID: 9343
	public int Frame;

	// Token: 0x04002480 RID: 9344
	public int ID;

	// Token: 0x04002481 RID: 9345
	public float AutoTimer;

	// Token: 0x04002482 RID: 9346
	public float Alpha;

	// Token: 0x04002483 RID: 9347
	public string Text;

	// Token: 0x04002484 RID: 9348
	public AudioClip[] SFX;

	// Token: 0x04002485 RID: 9349
	public bool Spoke;

	// Token: 0x04002486 RID: 9350
	public bool Auto;

	// Token: 0x04002487 RID: 9351
	public AudioSource SFXAudioSource;

	// Token: 0x04002488 RID: 9352
	public AudioSource Jukebox;
}
