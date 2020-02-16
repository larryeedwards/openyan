using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000170 RID: 368
public class MGPMManagerScript : MonoBehaviour
{
	// Token: 0x06000BC9 RID: 3017 RVA: 0x000596E0 File Offset: 0x00057AE0
	private void Start()
	{
		if (GameGlobals.HardMode)
		{
			this.Jukebox.clip = this.HardModeVoice;
			this.WaterRenderer[0].material.color = Color.red;
			this.WaterRenderer[1].material.color = Color.red;
			this.RightArtwork.material.mainTexture = this.RightBloody;
			this.LeftArtwork.material.mainTexture = this.LeftBloody;
		}
		this.Miyuki.transform.localPosition = new Vector3(0f, -300f, 0f);
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
		this.StartGraphic.SetActive(false);
		this.Miyuki.Gameplay = false;
		this.ID = 1;
		while (this.ID < this.EnemySpawner.Length)
		{
			this.EnemySpawner[this.ID].enabled = false;
			this.ID++;
		}
		Time.timeScale = 1f;
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00059814 File Offset: 0x00057C14
	private void Update()
	{
		this.ScoreLabel.text = "Score: " + this.Score * this.Miyuki.Health;
		if (this.StageClear)
		{
			this.GameOverTimer += Time.deltaTime;
			if (this.GameOverTimer > 1f)
			{
				this.Miyuki.transform.localPosition = new Vector3(this.Miyuki.transform.localPosition.x, this.Miyuki.transform.localPosition.y + Time.deltaTime * 10f, this.Miyuki.transform.localPosition.z);
				if (!this.StageClearGraphic.activeInHierarchy)
				{
					this.StageClearGraphic.SetActive(true);
					this.Jukebox.clip = this.VictoryMusic;
					this.Jukebox.loop = false;
					this.Jukebox.volume = 1f;
					this.Jukebox.Play();
				}
				if (this.GameOverTimer > 9f)
				{
					this.FadeOut = true;
				}
			}
			if (this.FadeOut)
			{
				this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
				this.Jukebox.volume = 1f - this.Black.material.color.a;
				if (this.Black.material.color.a == 1f)
				{
					SceneManager.LoadScene("MiyukiThanksScene");
				}
			}
		}
		else if (!this.GameOver)
		{
			if (this.Intro)
			{
				if (this.FadeIn)
				{
					this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 0f, Time.deltaTime));
					if (this.Black.material.color.a == 0f)
					{
						this.Jukebox.Play();
						this.FadeIn = false;
					}
				}
				else
				{
					this.Miyuki.transform.localPosition = new Vector3(0f, Mathf.MoveTowards(this.Miyuki.transform.localPosition.y, -120f, Time.deltaTime * 60f), 0f);
					if (this.Miyuki.transform.localPosition.y == -120f)
					{
						if (!this.Jukebox.isPlaying)
						{
							this.Jukebox.loop = true;
							this.Jukebox.clip = this.BGM;
							this.Jukebox.Play();
							if (GameGlobals.HardMode)
							{
								this.Jukebox.pitch = 0.2f;
							}
						}
						this.StartGraphic.SetActive(true);
						this.Timer += Time.deltaTime;
						if ((double)this.Timer > 3.5)
						{
							this.StartGraphic.SetActive(false);
							this.ID = 1;
							while (this.ID < this.EnemySpawner.Length)
							{
								this.EnemySpawner[this.ID].enabled = true;
								this.ID++;
							}
							this.Miyuki.Gameplay = true;
							this.Intro = false;
						}
					}
				}
				if (Input.GetKeyDown("space"))
				{
					this.StartGraphic.SetActive(false);
					this.ID = 1;
					while (this.ID < this.EnemySpawner.Length)
					{
						this.EnemySpawner[this.ID].enabled = true;
						this.ID++;
					}
					this.Black.material.color = new Color(0f, 0f, 0f, 0f);
					this.Miyuki.Gameplay = true;
					this.Intro = false;
					this.Jukebox.loop = true;
					this.Jukebox.clip = this.BGM;
					this.Jukebox.Play();
					if (GameGlobals.HardMode)
					{
						this.Jukebox.pitch = 0.2f;
					}
				}
			}
		}
		else
		{
			this.GameOverTimer += Time.deltaTime;
			if (this.GameOverTimer > 3f)
			{
				if (!this.GameOverGraphic.activeInHierarchy)
				{
					this.GameOverGraphic.SetActive(true);
					this.Jukebox.clip = this.GameOverMusic;
					this.Jukebox.loop = false;
					this.Jukebox.Play();
				}
				else if (Input.anyKeyDown)
				{
					this.FadeOut = true;
				}
			}
			if (this.FadeOut)
			{
				this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
				this.Jukebox.volume = 1f - this.Black.material.color.a;
				if (this.Black.material.color.a == 1f)
				{
					SceneManager.LoadScene("MiyukiTitleScene");
				}
			}
		}
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00059E03 File Offset: 0x00058203
	public void BeginGameOver()
	{
		this.Jukebox.Stop();
		this.GameOver = true;
		this.Miyuki.enabled = false;
	}

	// Token: 0x040008F3 RID: 2291
	public MGPMSpawnerScript[] EnemySpawner;

	// Token: 0x040008F4 RID: 2292
	public MGPMMiyukiScript Miyuki;

	// Token: 0x040008F5 RID: 2293
	public GameObject StageClearGraphic;

	// Token: 0x040008F6 RID: 2294
	public GameObject GameOverGraphic;

	// Token: 0x040008F7 RID: 2295
	public GameObject StartGraphic;

	// Token: 0x040008F8 RID: 2296
	public Renderer[] WaterRenderer;

	// Token: 0x040008F9 RID: 2297
	public Renderer RightArtwork;

	// Token: 0x040008FA RID: 2298
	public Renderer LeftArtwork;

	// Token: 0x040008FB RID: 2299
	public Texture RightBloody;

	// Token: 0x040008FC RID: 2300
	public Texture LeftBloody;

	// Token: 0x040008FD RID: 2301
	public AudioSource Jukebox;

	// Token: 0x040008FE RID: 2302
	public AudioClip HardModeVoice;

	// Token: 0x040008FF RID: 2303
	public AudioClip GameOverMusic;

	// Token: 0x04000900 RID: 2304
	public AudioClip VictoryMusic;

	// Token: 0x04000901 RID: 2305
	public AudioClip FinalBoss;

	// Token: 0x04000902 RID: 2306
	public AudioClip BGM;

	// Token: 0x04000903 RID: 2307
	public Renderer Black;

	// Token: 0x04000904 RID: 2308
	public Text ScoreLabel;

	// Token: 0x04000905 RID: 2309
	public bool StageClear;

	// Token: 0x04000906 RID: 2310
	public bool GameOver;

	// Token: 0x04000907 RID: 2311
	public bool FadeOut;

	// Token: 0x04000908 RID: 2312
	public bool FadeIn;

	// Token: 0x04000909 RID: 2313
	public bool Intro;

	// Token: 0x0400090A RID: 2314
	public float GameOverTimer;

	// Token: 0x0400090B RID: 2315
	public float Timer;

	// Token: 0x0400090C RID: 2316
	public int Score;

	// Token: 0x0400090D RID: 2317
	public int ID;
}
