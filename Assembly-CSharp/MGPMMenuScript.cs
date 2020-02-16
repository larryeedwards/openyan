using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000171 RID: 369
public class MGPMMenuScript : MonoBehaviour
{
	// Token: 0x06000BCD RID: 3021 RVA: 0x00059E2B File Offset: 0x0005822B
	private void Start()
	{
		this.Black.material.color = new Color(0f, 0f, 0f, 1f);
		Time.timeScale = 1f;
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00059E60 File Offset: 0x00058260
	private void Update()
	{
		this.Rotation -= Time.deltaTime * 3f;
		this.Background.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		if (this.FadeIn)
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 0f, Time.deltaTime));
			if (this.Black.material.color.a == 0f)
			{
				this.Jukebox.Play();
				this.FadeIn = false;
			}
		}
		if (this.FadeOut)
		{
			this.Black.material.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(this.Black.material.color.a, 1f, Time.deltaTime));
			this.Jukebox.volume = 1f - this.Black.material.color.a;
			if (this.Black.material.color.a == 1f)
			{
				if (this.ID == 4)
				{
					SceneManager.LoadScene("HomeScene");
				}
				else
				{
					GameGlobals.HardMode = this.HardMode;
					SceneManager.LoadScene("MiyukiGameplayScene");
				}
			}
		}
		if (!this.FadeOut && !this.FadeIn)
		{
			if (!this.HardMode && Input.GetKeyDown("h"))
			{
				AudioSource.PlayClipAtPoint(this.HardModeClip, base.transform.position);
				this.Logo.material.mainTexture = this.BloodyLogo;
				this.HardMode = true;
				this.Vibrate = 0.1f;
			}
			if (this.HardMode)
			{
				this.Jukebox.pitch = Mathf.MoveTowards(this.Jukebox.pitch, 0.1f, Time.deltaTime);
				this.BG.material.color = new Color(Mathf.MoveTowards(this.BG.material.color.r, 0.5f, Time.deltaTime * 0.5f), Mathf.MoveTowards(this.BG.material.color.g, 0f, Time.deltaTime), Mathf.MoveTowards(this.BG.material.color.b, 0f, Time.deltaTime), 1f);
				this.Logo.transform.localPosition = new Vector3(0f, 0.5f, 2f) + new Vector3(UnityEngine.Random.Range(this.Vibrate * -1f, this.Vibrate), UnityEngine.Random.Range(this.Vibrate * -1f, this.Vibrate), 0f);
				this.Vibrate = Mathf.MoveTowards(this.Vibrate, 0f, Time.deltaTime * 0.1f);
			}
			if (this.Jukebox.clip != this.BGM && !this.Jukebox.isPlaying)
			{
				this.Jukebox.loop = true;
				this.Jukebox.clip = this.BGM;
				this.Jukebox.Play();
			}
			if (!this.WindowDisplaying)
			{
				if (this.InputManager.TappedDown)
				{
					this.ID++;
					this.UpdateHighlight();
				}
				if (this.InputManager.TappedUp)
				{
					this.ID--;
					this.UpdateHighlight();
				}
				if (Input.GetButtonDown("A") || Input.GetKeyDown("z") || (Input.GetKeyDown("return") | Input.GetKeyDown("space")))
				{
					if (this.MainMenu.activeInHierarchy)
					{
						if (this.ID == 1)
						{
							this.DifficultySelect.SetActive(true);
							this.MainMenu.SetActive(false);
							this.ID = 2;
							this.UpdateHighlight();
						}
						else if (this.ID == 2)
						{
							this.Highlight.gameObject.SetActive(false);
							this.Controls.SetActive(true);
							this.WindowDisplaying = true;
						}
						else if (this.ID == 3)
						{
							this.Highlight.gameObject.SetActive(false);
							this.Credits.SetActive(true);
							this.WindowDisplaying = true;
						}
						else if (this.ID == 4)
						{
							this.FadeOut = true;
						}
					}
					else
					{
						if (this.ID == 2)
						{
							GameGlobals.EasyMode = false;
						}
						else
						{
							GameGlobals.EasyMode = true;
						}
						this.FadeOut = true;
					}
				}
			}
			else if (Input.GetButtonDown("B"))
			{
				this.Highlight.gameObject.SetActive(true);
				this.Controls.SetActive(false);
				this.Credits.SetActive(false);
				this.WindowDisplaying = false;
			}
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x0005A3C8 File Offset: 0x000587C8
	private void UpdateHighlight()
	{
		if (this.MainMenu.activeInHierarchy)
		{
			if (this.ID == 0)
			{
				this.ID = 4;
			}
			else if (this.ID == 5)
			{
				this.ID = 1;
			}
		}
		else if (this.ID == 1)
		{
			this.ID = 3;
		}
		else if (this.ID == 4)
		{
			this.ID = 2;
		}
		this.Highlight.transform.position = new Vector3(0f, -0.2f * (float)this.ID, this.Highlight.transform.position.z);
	}

	// Token: 0x0400090E RID: 2318
	public InputManagerScript InputManager;

	// Token: 0x0400090F RID: 2319
	public AudioSource Jukebox;

	// Token: 0x04000910 RID: 2320
	public AudioClip HardModeClip;

	// Token: 0x04000911 RID: 2321
	public bool WindowDisplaying;

	// Token: 0x04000912 RID: 2322
	public Transform Highlight;

	// Token: 0x04000913 RID: 2323
	public Transform Background;

	// Token: 0x04000914 RID: 2324
	public GameObject Controls;

	// Token: 0x04000915 RID: 2325
	public GameObject Credits;

	// Token: 0x04000916 RID: 2326
	public GameObject DifficultySelect;

	// Token: 0x04000917 RID: 2327
	public GameObject MainMenu;

	// Token: 0x04000918 RID: 2328
	public Renderer Black;

	// Token: 0x04000919 RID: 2329
	public Renderer Logo;

	// Token: 0x0400091A RID: 2330
	public Renderer BG;

	// Token: 0x0400091B RID: 2331
	public Texture BloodyLogo;

	// Token: 0x0400091C RID: 2332
	public AudioClip BGM;

	// Token: 0x0400091D RID: 2333
	public float Rotation;

	// Token: 0x0400091E RID: 2334
	public float Vibrate;

	// Token: 0x0400091F RID: 2335
	public bool HardMode;

	// Token: 0x04000920 RID: 2336
	public bool FadeOut;

	// Token: 0x04000921 RID: 2337
	public bool FadeIn;

	// Token: 0x04000922 RID: 2338
	public int ID;
}
