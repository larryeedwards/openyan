using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004F3 RID: 1267
public class SaveLoadMenuScript : MonoBehaviour
{
	// Token: 0x06001FB0 RID: 8112 RVA: 0x00143D1E File Offset: 0x0014211E
	public void Start()
	{
		if (GameGlobals.Profile == 0)
		{
			GameGlobals.Profile = 1;
		}
		this.Profile = GameGlobals.Profile;
		this.ConfirmWindow.SetActive(false);
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x00143D48 File Offset: 0x00142148
	public void Update()
	{
		if (!this.ConfirmWindow.activeInHierarchy)
		{
			if (this.InputManager.TappedUp)
			{
				this.Row--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedDown)
			{
				this.Row++;
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedLeft)
			{
				this.Column--;
				this.UpdateHighlight();
			}
			else if (this.InputManager.TappedRight)
			{
				this.Column++;
				this.UpdateHighlight();
			}
		}
		if (this.GrabScreenshot)
		{
			if (GameGlobals.Profile == 0)
			{
				GameGlobals.Profile = 1;
				this.Profile = 1;
			}
			this.PauseScreen.ScreenBlur.enabled = true;
			this.UICamera.enabled = true;
			this.StudentManager.Save();
			base.StartCoroutine(this.GetThumbnails());
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YanderePosX"
			}), this.PauseScreen.Yandere.transform.position.x);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YanderePosY"
			}), this.PauseScreen.Yandere.transform.position.y);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YanderePosZ"
			}), this.PauseScreen.Yandere.transform.position.z);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YandereRotX"
			}), this.PauseScreen.Yandere.transform.eulerAngles.x);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YandereRotY"
			}), this.PauseScreen.Yandere.transform.eulerAngles.y);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_YandereRotZ"
			}), this.PauseScreen.Yandere.transform.eulerAngles.z);
			PlayerPrefs.SetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Time"
			}), this.Clock.PresentTime);
			if (DateGlobals.Weekday == DayOfWeek.Monday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 1);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Tuesday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 2);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Wednesday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 3);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Thursday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 4);
			}
			else if (DateGlobals.Weekday == DayOfWeek.Friday)
			{
				PlayerPrefs.SetInt(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					this.Selected,
					"_Weekday"
				}), 5);
			}
			this.GrabScreenshot = false;
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.Loading)
			{
				if (this.DataLabels[this.Selected].text != "No Data")
				{
					if (!this.ConfirmWindow.activeInHierarchy)
					{
						this.AreYouSureLabel.text = "Are you sure you'd like to load?";
						this.ConfirmWindow.SetActive(true);
					}
					else if (this.DataLabels[this.Selected].text != "No Data")
					{
						PlayerPrefs.SetInt("LoadingSave", 1);
						PlayerPrefs.SetInt("SaveSlot", this.Selected);
						SceneManager.LoadScene("LoadingScene");
					}
				}
			}
			else if (this.Saving)
			{
				if (!this.ConfirmWindow.activeInHierarchy)
				{
					this.AreYouSureLabel.text = "Are you sure you'd like to save?";
					this.ConfirmWindow.SetActive(true);
				}
				else
				{
					this.ConfirmWindow.SetActive(false);
					PlayerPrefs.SetInt("SaveSlot", this.Selected);
					PlayerPrefs.SetString(string.Concat(new object[]
					{
						"Profile_",
						this.Profile,
						"_Slot_",
						this.Selected,
						"_DateTime"
					}), DateTime.Now.ToString());
					ScreenCapture.CaptureScreenshot(string.Concat(new object[]
					{
						Application.streamingAssetsPath,
						"/SaveData/Profile_",
						this.Profile,
						"/Slot_",
						this.Selected,
						"/Thumbnail.png"
					}));
					this.PauseScreen.ScreenBlur.enabled = false;
					this.UICamera.enabled = false;
					this.GrabScreenshot = true;
				}
			}
		}
		if (Input.GetButtonDown("X") && this.DataLabels[this.Selected].text != "No Data")
		{
			PlayerPrefs.SetInt("SaveSlot", this.Selected);
			this.StudentManager.Load();
			if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 1)
			{
				DateGlobals.Weekday = DayOfWeek.Monday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 2)
			{
				DateGlobals.Weekday = DayOfWeek.Tuesday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 3)
			{
				DateGlobals.Weekday = DayOfWeek.Wednesday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 4)
			{
				DateGlobals.Weekday = DayOfWeek.Tuesday;
			}
			else if (PlayerPrefs.GetInt(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Weekday"
			})) == 5)
			{
				DateGlobals.Weekday = DayOfWeek.Wednesday;
			}
			this.Clock.PresentTime = PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				this.Selected,
				"_Time"
			}), this.Clock.PresentTime);
			this.Clock.DayLabel.text = this.Clock.GetWeekdayText(DateGlobals.Weekday);
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
			this.PauseScreen.ExitPhone();
		}
		if (Input.GetButtonDown("B"))
		{
			if (this.ConfirmWindow.activeInHierarchy)
			{
				this.ConfirmWindow.SetActive(false);
			}
			else
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;
				base.gameObject.SetActive(false);
				this.PauseScreen.PromptBar.ClearButtons();
				this.PauseScreen.PromptBar.Label[0].text = "Accept";
				this.PauseScreen.PromptBar.Label[1].text = "Exit";
				this.PauseScreen.PromptBar.Label[4].text = "Choose";
				this.PauseScreen.PromptBar.UpdateButtons();
				this.PauseScreen.PromptBar.Show = true;
			}
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x00144804 File Offset: 0x00142C04
	public IEnumerator GetThumbnails()
	{
		for (int ID = 1; ID < 11; ID++)
		{
			if (PlayerPrefs.GetString(string.Concat(new object[]
			{
				"Profile_",
				this.Profile,
				"_Slot_",
				ID,
				"_DateTime"
			})) != string.Empty)
			{
				this.DataLabels[ID].text = PlayerPrefs.GetString(string.Concat(new object[]
				{
					"Profile_",
					this.Profile,
					"_Slot_",
					ID,
					"_DateTime"
				}));
				string path = string.Concat(new object[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/SaveData/Profile_",
					this.Profile,
					"/Slot_",
					ID,
					"/Thumbnail.png"
				});
				WWW www = new WWW(path);
				yield return www;
				if (www.error == null)
				{
					this.Thumbnails[ID].mainTexture = www.texture;
				}
				else
				{
					Debug.Log("Could not retrieve the thumbnail. Maybe it was deleted from Streaming Assets?");
				}
			}
			else
			{
				this.DataLabels[ID].text = "No Data";
			}
		}
		yield break;
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x00144820 File Offset: 0x00142C20
	public void UpdateHighlight()
	{
		if (this.Row < 1)
		{
			this.Row = 2;
		}
		else if (this.Row > 2)
		{
			this.Row = 1;
		}
		if (this.Column < 1)
		{
			this.Column = 5;
		}
		else if (this.Column > 5)
		{
			this.Column = 1;
		}
		this.Highlight.localPosition = new Vector3((float)(-510 + 170 * this.Column), (float)(313 - 226 * this.Row), this.Highlight.localPosition.z);
		this.Selected = this.Column + (this.Row - 1) * 5;
	}

	// Token: 0x04002B3F RID: 11071
	public StudentManagerScript StudentManager;

	// Token: 0x04002B40 RID: 11072
	public InputManagerScript InputManager;

	// Token: 0x04002B41 RID: 11073
	public PauseScreenScript PauseScreen;

	// Token: 0x04002B42 RID: 11074
	public GameObject ConfirmWindow;

	// Token: 0x04002B43 RID: 11075
	public ClockScript Clock;

	// Token: 0x04002B44 RID: 11076
	public UILabel AreYouSureLabel;

	// Token: 0x04002B45 RID: 11077
	public UILabel Header;

	// Token: 0x04002B46 RID: 11078
	public UITexture[] Thumbnails;

	// Token: 0x04002B47 RID: 11079
	public UILabel[] DataLabels;

	// Token: 0x04002B48 RID: 11080
	public Transform Highlight;

	// Token: 0x04002B49 RID: 11081
	public Camera UICamera;

	// Token: 0x04002B4A RID: 11082
	public bool GrabScreenshot;

	// Token: 0x04002B4B RID: 11083
	public bool Loading;

	// Token: 0x04002B4C RID: 11084
	public bool Saving;

	// Token: 0x04002B4D RID: 11085
	public int Profile;

	// Token: 0x04002B4E RID: 11086
	public int Row = 1;

	// Token: 0x04002B4F RID: 11087
	public int Column = 1;

	// Token: 0x04002B50 RID: 11088
	public int Selected = 1;
}
