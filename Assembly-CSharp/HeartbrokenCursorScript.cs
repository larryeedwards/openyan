using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000411 RID: 1041
public class HeartbrokenCursorScript : MonoBehaviour
{
	// Token: 0x06001C76 RID: 7286 RVA: 0x000FF234 File Offset: 0x000FD634
	private void Start()
	{
		this.Darkness.transform.localPosition = new Vector3(this.Darkness.transform.localPosition.x, this.Darkness.transform.localPosition.y, -989f);
		this.Continue.color = new Color(this.Continue.color.r, this.Continue.color.g, this.Continue.color.b, 0f);
	}

	// Token: 0x06001C77 RID: 7287 RVA: 0x000FF2DC File Offset: 0x000FD6DC
	private void Update()
	{
		this.StudentManager.Yandere.Twitch = Vector3.Lerp(this.StudentManager.Yandere.Twitch, Vector3.zero, Time.deltaTime * 10f);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, Mathf.Lerp(base.transform.localPosition.y, 255f - (float)this.Selected * 50f, Time.deltaTime * 10f), base.transform.localPosition.z);
		if (!this.FadeOut)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.MyLabel.color.a >= 1f)
			{
				if (this.InputManager.TappedDown)
				{
					this.Selected++;
					if (this.Selected > this.Options)
					{
						this.Selected = 1;
					}
					component.clip = this.MoveSound;
					component.Play();
				}
				if (this.InputManager.TappedUp)
				{
					this.Selected--;
					if (this.Selected < 1)
					{
						this.Selected = this.Options;
					}
					component.clip = this.MoveSound;
					component.Play();
				}
				this.Continue.color = new Color(this.Continue.color.r, this.Continue.color.g, this.Continue.color.b, (this.Selected == 4) ? 0f : 1f);
				if (Input.GetButtonDown("A"))
				{
					this.Nudge = true;
					if (this.Selected != 4)
					{
						component.clip = this.SelectSound;
						component.Play();
						this.FadeOut = true;
					}
				}
			}
		}
		else
		{
			this.Heartbroken.GetComponent<AudioSource>().volume -= Time.deltaTime;
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				if (this.Selected == 1)
				{
					for (int i = 0; i < this.StudentManager.NPCsTotal; i++)
					{
						if (StudentGlobals.GetStudentDying(i))
						{
							StudentGlobals.SetStudentDying(i, false);
						}
					}
					SceneManager.LoadScene("LoadingScene");
				}
				else if (this.Selected == 2)
				{
					this.LoveSick = GameGlobals.LoveSick;
					Globals.DeleteAll();
					GameGlobals.LoveSick = this.LoveSick;
					SceneManager.LoadScene("CalendarScene");
				}
				else if (this.Selected == 3)
				{
					SceneManager.LoadScene("TitleScene");
				}
			}
		}
		if (this.Nudge)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + Time.deltaTime * 250f, base.transform.localPosition.y, base.transform.localPosition.z);
			if (base.transform.localPosition.x > -225f)
			{
				base.transform.localPosition = new Vector3(-225f, base.transform.localPosition.y, base.transform.localPosition.z);
				this.Nudge = false;
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x - Time.deltaTime * 250f, base.transform.localPosition.y, base.transform.localPosition.z);
			if (base.transform.localPosition.x < -250f)
			{
				base.transform.localPosition = new Vector3(-250f, base.transform.localPosition.y, base.transform.localPosition.z);
			}
		}
	}

	// Token: 0x040020E8 RID: 8424
	public StudentManagerScript StudentManager;

	// Token: 0x040020E9 RID: 8425
	public InputManagerScript InputManager;

	// Token: 0x040020EA RID: 8426
	public HeartbrokenScript Heartbroken;

	// Token: 0x040020EB RID: 8427
	public UISprite Darkness;

	// Token: 0x040020EC RID: 8428
	public UILabel Continue;

	// Token: 0x040020ED RID: 8429
	public UILabel MyLabel;

	// Token: 0x040020EE RID: 8430
	public bool LoveSick;

	// Token: 0x040020EF RID: 8431
	public bool FadeOut;

	// Token: 0x040020F0 RID: 8432
	public bool Nudge;

	// Token: 0x040020F1 RID: 8433
	public int CracksSpawned;

	// Token: 0x040020F2 RID: 8434
	public int Selected = 1;

	// Token: 0x040020F3 RID: 8435
	public int Options = 4;

	// Token: 0x040020F4 RID: 8436
	public int LastRandomCrack;

	// Token: 0x040020F5 RID: 8437
	public int RandomCrack;

	// Token: 0x040020F6 RID: 8438
	public AudioClip SelectSound;

	// Token: 0x040020F7 RID: 8439
	public AudioClip MoveSound;

	// Token: 0x040020F8 RID: 8440
	public VibrateScript[] Vibrations;

	// Token: 0x040020F9 RID: 8441
	public AudioClip[] CrackSound;

	// Token: 0x040020FA RID: 8442
	public GameObject[] Cracks;
}
