using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020005B0 RID: 1456
public class YanvaniaTitleScript : MonoBehaviour
{
	// Token: 0x06002326 RID: 8998 RVA: 0x001BA978 File Offset: 0x001B8D78
	private void Start()
	{
		this.Midori.transform.localPosition = new Vector3(1540f, 0f, 0f);
		this.Midori.transform.localEulerAngles = Vector3.zero;
		this.Midori.gameObject.SetActive(false);
		if (YanvaniaGlobals.DraculaDefeated)
		{
			TaskGlobals.SetTaskStatus(38, 2);
			this.SkipButton.SetActive(true);
			this.Logo.gameObject.SetActive(false);
		}
		else
		{
			this.SkipButton.SetActive(false);
		}
		this.Prologue.gameObject.SetActive(false);
		this.Prologue.localPosition = new Vector3(this.Prologue.localPosition.x, -2665f, this.Prologue.localPosition.z);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		this.Buttons.alpha = 0f;
		this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 0f);
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x001BAB0C File Offset: 0x001B8F0C
	private void Update()
	{
		if (!this.Logo.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.M))
		{
			YanvaniaGlobals.DraculaDefeated = true;
			YanvaniaGlobals.MidoriEasterEgg = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.End))
		{
			YanvaniaGlobals.DraculaDefeated = true;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			YanvaniaGlobals.DraculaDefeated = false;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.FadeOut)
		{
			if (this.Darkness.color.a > 0f)
			{
				if (Input.GetButtonDown("A"))
				{
					this.Skip();
				}
				if (!this.ErrorWindow.activeInHierarchy)
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
				}
			}
			else if (this.Darkness.color.a <= 0f)
			{
				if (!YanvaniaGlobals.MidoriEasterEgg)
				{
					if (YanvaniaGlobals.DraculaDefeated)
					{
						if (!this.Prologue.gameObject.activeInHierarchy)
						{
							this.Prologue.gameObject.SetActive(true);
							component.volume = 0.5f;
							component.loop = true;
							component.clip = this.BGM;
							component.Play();
						}
						if (Input.GetButtonDown("B"))
						{
							this.Prologue.localPosition = new Vector3(this.Prologue.localPosition.x, 2501f, this.Prologue.localPosition.z);
							this.Prologue.GetComponent<AudioSource>().Stop();
						}
						if (this.Prologue.localPosition.y > 2500f)
						{
							if (component.isPlaying)
							{
								this.Midori.mainTexture = this.SadMidori;
								Time.timeScale = 1f;
								this.Midori.gameObject.GetComponent<AudioSource>().Stop();
								component.Stop();
							}
							if (!this.ErrorLeave)
							{
								this.ErrorWindow.SetActive(true);
								this.ErrorWindow.transform.localScale = Vector3.Lerp(this.ErrorWindow.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
								if (this.ErrorWindow.transform.localScale.x > 0.9f && Input.anyKeyDown)
								{
									AudioSource component2 = this.ErrorWindow.GetComponent<AudioSource>();
									component2.clip = this.ExitSound;
									component2.Play();
									this.ErrorLeave = true;
								}
							}
							else
							{
								this.FadeOut = true;
							}
						}
						else
						{
							this.Prologue.localPosition = new Vector3(this.Prologue.localPosition.x, this.Prologue.localPosition.y + Time.deltaTime * this.ScrollSpeed, this.Prologue.localPosition.z);
							if (Input.GetKeyDown(KeyCode.Equals))
							{
								Time.timeScale = 100f;
							}
							if (Input.GetKeyDown(KeyCode.Minus))
							{
								Time.timeScale = 1f;
							}
						}
					}
					else if (!component.isPlaying)
					{
						if (this.Logo.color.a == 0f)
						{
							component.Play();
						}
						else
						{
							component.loop = true;
							component.clip = this.BGM;
							component.Play();
						}
					}
					else if (component.clip != this.BGM)
					{
						this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, this.Logo.color.a + Time.deltaTime);
						if (Input.GetButtonDown("A"))
						{
							this.Skip();
						}
					}
					else if (!this.FadeButtons)
					{
						this.Controls.alpha = Mathf.MoveTowards(this.Controls.alpha, 0f, Time.deltaTime);
						this.Credits.alpha = Mathf.MoveTowards(this.Credits.alpha, 0f, Time.deltaTime);
						if (this.Controls.alpha == 0f && this.Credits.alpha == 0f)
						{
							this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, -100f - 100f * (float)this.Selected, this.Highlight.localPosition.z);
							this.Buttons.alpha += Time.deltaTime;
							if (this.Buttons.alpha >= 1f)
							{
								if (Input.GetButtonDown("A"))
								{
									UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.Highlight.position, Quaternion.identity);
									if (this.Selected == 1 || this.Selected == 4)
									{
										this.FadeOut = true;
									}
									if (this.Selected == 2 || this.Selected == 3)
									{
										this.FadeButtons = true;
									}
								}
								AudioSource component3 = this.Highlight.gameObject.GetComponent<AudioSource>();
								if (this.InputManager.TappedUp)
								{
									component3.Play();
									this.Selected--;
									if (this.Selected < 1)
									{
										this.Selected = 4;
									}
								}
								if (this.InputManager.TappedDown)
								{
									component3.Play();
									this.Selected++;
									if (this.Selected > 4)
									{
										this.Selected = 1;
									}
								}
							}
						}
					}
					else
					{
						this.Buttons.alpha -= Time.deltaTime;
						if (this.Buttons.alpha == 0f)
						{
							if (this.Selected == 2)
							{
								this.Controls.alpha = Mathf.MoveTowards(this.Controls.alpha, 1f, Time.deltaTime);
							}
							else
							{
								this.Credits.alpha = Mathf.MoveTowards(this.Credits.alpha, 1f, Time.deltaTime);
							}
						}
						if ((this.Controls.alpha == 1f || this.Credits.alpha == 1f) && Input.GetButtonDown("B"))
						{
							UnityEngine.Object.Instantiate<GameObject>(this.ButtonEffect, this.BackButtons[this.Selected].position, Quaternion.identity);
							this.FadeButtons = false;
						}
					}
				}
				else
				{
					this.Prologue.GetComponent<AudioSource>().enabled = false;
					this.Midori.gameObject.SetActive(true);
					this.ScrollSpeed = 60f;
					this.Midori.transform.localPosition = new Vector3(Mathf.Lerp(this.Midori.transform.localPosition.x, 875f, Time.deltaTime * 2f), this.Midori.transform.localPosition.y, this.Midori.transform.localPosition.z);
					this.Midori.transform.localEulerAngles = new Vector3(this.Midori.transform.localEulerAngles.x, this.Midori.transform.localEulerAngles.y, Mathf.Lerp(this.Midori.transform.localEulerAngles.z, 45f, Time.deltaTime * 2f));
					if (this.Midori.gameObject.GetComponent<AudioSource>().time > 3f)
					{
						YanvaniaGlobals.MidoriEasterEgg = false;
					}
				}
			}
		}
		else
		{
			this.ErrorWindow.transform.localScale = Vector3.Lerp(this.ErrorWindow.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			component.volume -= Time.deltaTime;
			if (this.Darkness.color.a >= 1f)
			{
				if (YanvaniaGlobals.DraculaDefeated)
				{
					SceneManager.LoadScene("HomeScene");
				}
				else if (this.Selected == 1)
				{
					SceneManager.LoadScene("YanvaniaScene");
				}
				else
				{
					SceneManager.LoadScene("HomeScene");
				}
			}
		}
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x001BB4EC File Offset: 0x001B98EC
	private void Skip()
	{
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
		this.Logo.color = new Color(this.Logo.color.r, this.Logo.color.g, this.Logo.color.b, 1f);
		AudioSource component = base.GetComponent<AudioSource>();
		component.loop = true;
		component.clip = this.BGM;
		component.Play();
	}

	// Token: 0x04003C64 RID: 15460
	public InputManagerScript InputManager;

	// Token: 0x04003C65 RID: 15461
	public GameObject ButtonEffect;

	// Token: 0x04003C66 RID: 15462
	public GameObject ErrorWindow;

	// Token: 0x04003C67 RID: 15463
	public GameObject SkipButton;

	// Token: 0x04003C68 RID: 15464
	public Transform Highlight;

	// Token: 0x04003C69 RID: 15465
	public Transform Prologue;

	// Token: 0x04003C6A RID: 15466
	public UIPanel Controls;

	// Token: 0x04003C6B RID: 15467
	public UIPanel Credits;

	// Token: 0x04003C6C RID: 15468
	public UIPanel Buttons;

	// Token: 0x04003C6D RID: 15469
	public UISprite Darkness;

	// Token: 0x04003C6E RID: 15470
	public UITexture Midori;

	// Token: 0x04003C6F RID: 15471
	public UITexture Logo;

	// Token: 0x04003C70 RID: 15472
	public AudioClip SelectSound;

	// Token: 0x04003C71 RID: 15473
	public AudioClip ExitSound;

	// Token: 0x04003C72 RID: 15474
	public AudioClip BGM;

	// Token: 0x04003C73 RID: 15475
	public Transform[] BackButtons;

	// Token: 0x04003C74 RID: 15476
	public Texture SadMidori;

	// Token: 0x04003C75 RID: 15477
	public bool FadeButtons;

	// Token: 0x04003C76 RID: 15478
	public bool ErrorLeave;

	// Token: 0x04003C77 RID: 15479
	public bool FadeOut;

	// Token: 0x04003C78 RID: 15480
	public float ScrollSpeed;

	// Token: 0x04003C79 RID: 15481
	public int Selected = 1;
}
