using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200041D RID: 1053
public class HomeDarknessScript : MonoBehaviour
{
	// Token: 0x06001C9E RID: 7326 RVA: 0x00103264 File Offset: 0x00101664
	private void Start()
	{
		if (GameGlobals.LoveSick)
		{
			this.Sprite.color = new Color(0f, 0f, 0f, 1f);
		}
		this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 1f);
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x001032F0 File Offset: 0x001016F0
	private void Update()
	{
		if (this.FadeOut)
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a + Time.deltaTime * ((!this.FadeSlow) ? 1f : 0.2f));
			if (this.Sprite.color.a >= 1f)
			{
				if (this.HomeCamera.ID != 2)
				{
					if (this.HomeCamera.ID == 3)
					{
						if (this.Cyberstalking)
						{
							SceneManager.LoadScene("CalendarScene");
						}
						else
						{
							SceneManager.LoadScene("YancordScene");
						}
					}
					else if (this.HomeCamera.ID == 5)
					{
						if (this.HomeVideoGames.ID == 1)
						{
							SceneManager.LoadScene("YanvaniaTitleScene");
						}
						else
						{
							SceneManager.LoadScene("MiyukiTitleScene");
						}
					}
					else if (this.HomeCamera.ID == 9)
					{
						SceneManager.LoadScene("CalendarScene");
					}
					else if (this.HomeCamera.ID == 10)
					{
						StudentGlobals.SetStudentKidnapped(SchoolGlobals.KidnapVictim, false);
						StudentGlobals.SetStudentSlave(SchoolGlobals.KidnapVictim);
						this.CheckForOsanaThursday();
					}
					else if (this.HomeCamera.ID == 11)
					{
						EventGlobals.KidnapConversation = true;
						SceneManager.LoadScene("PhoneScene");
					}
					else if (this.HomeCamera.ID == 12)
					{
						SceneManager.LoadScene("LifeNoteScene");
					}
					else if (this.HomeExit.ID == 1)
					{
						this.CheckForOsanaThursday();
					}
					else if (this.HomeExit.ID == 2)
					{
						SceneManager.LoadScene("StreetScene");
					}
					else if (this.HomeExit.ID == 3)
					{
						if (this.HomeYandere.transform.position.y > -5f)
						{
							this.HomeYandere.transform.position = new Vector3(-2f, -10f, -2f);
							this.HomeYandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
							this.HomeYandere.CanMove = true;
							this.FadeOut = false;
							this.HomeCamera.Destinations[0].position = new Vector3(2.425f, -8f, 0f);
							this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
							this.HomeCamera.transform.position = this.HomeCamera.Destination.position;
							this.HomeCamera.Target = this.HomeCamera.Targets[0];
							this.HomeCamera.Focus.position = this.HomeCamera.Target.position;
							this.BasementLabel.text = "Upstairs";
							this.HomeCamera.DayLight.SetActive(true);
							Physics.SyncTransforms();
						}
						else
						{
							this.HomeYandere.transform.position = new Vector3(-1.6f, 0f, -1.6f);
							this.HomeYandere.transform.eulerAngles = new Vector3(0f, 45f, 0f);
							this.HomeYandere.CanMove = true;
							this.FadeOut = false;
							this.HomeCamera.Destinations[0].position = new Vector3(-2.0615f, 2f, 2.418f);
							this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
							this.HomeCamera.transform.position = this.HomeCamera.Destination.position;
							this.HomeCamera.Target = this.HomeCamera.Targets[0];
							this.HomeCamera.Focus.position = this.HomeCamera.Target.position;
							this.BasementLabel.text = "Basement";
							if (HomeGlobals.Night)
							{
								this.HomeCamera.DayLight.SetActive(false);
							}
							Physics.SyncTransforms();
						}
					}
				}
				else
				{
					SceneManager.LoadScene("CalendarScene");
				}
			}
		}
		else
		{
			this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, this.Sprite.color.a - Time.deltaTime);
			if (this.Sprite.color.a < 0f)
			{
				this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0f);
			}
		}
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x0010385F File Offset: 0x00101C5F
	private void CheckForOsanaThursday()
	{
		Debug.Log("Time to check if we need to display the Osana-walks-to-school cutscene...");
		if (this.InputDevice.Type == InputDeviceType.Gamepad)
		{
			PlayerGlobals.UsingGamepad = true;
		}
		else
		{
			PlayerGlobals.UsingGamepad = false;
		}
		SceneManager.LoadScene("LoadingScene");
	}

	// Token: 0x0400218B RID: 8587
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x0400218C RID: 8588
	public HomeYandereScript HomeYandere;

	// Token: 0x0400218D RID: 8589
	public HomeCameraScript HomeCamera;

	// Token: 0x0400218E RID: 8590
	public HomeExitScript HomeExit;

	// Token: 0x0400218F RID: 8591
	public InputDeviceScript InputDevice;

	// Token: 0x04002190 RID: 8592
	public UILabel BasementLabel;

	// Token: 0x04002191 RID: 8593
	public UISprite Sprite;

	// Token: 0x04002192 RID: 8594
	public bool Cyberstalking;

	// Token: 0x04002193 RID: 8595
	public bool FadeSlow;

	// Token: 0x04002194 RID: 8596
	public bool FadeOut;
}
