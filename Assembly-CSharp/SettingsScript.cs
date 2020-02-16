using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000504 RID: 1284
public class SettingsScript : MonoBehaviour
{
	// Token: 0x06001FE6 RID: 8166 RVA: 0x001471B8 File Offset: 0x001455B8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			OptionGlobals.DepthOfField = !OptionGlobals.DepthOfField;
			this.QualityManager.ToggleExperiment();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			OptionGlobals.RimLight = !OptionGlobals.RimLight;
			this.QualityManager.RimLight();
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			this.ToggleBackground();
		}
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			this.UpdateHighlight();
		}
		else if (this.InputManager.TappedDown)
		{
			this.Selected++;
			this.UpdateHighlight();
		}
		if (this.Selected == 1)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.ParticleCount++;
				this.QualityManager.UpdateParticles();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.ParticleCount--;
				this.QualityManager.UpdateParticles();
				this.UpdateText();
			}
		}
		else if (this.Selected == 2)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableOutlines = !OptionGlobals.DisableOutlines;
				this.UpdateText();
				this.QualityManager.UpdateOutlines();
			}
		}
		else if (this.Selected == 3)
		{
			if (this.InputManager.TappedRight)
			{
				if (QualitySettings.antiAliasing > 0)
				{
					QualitySettings.antiAliasing *= 2;
				}
				else
				{
					QualitySettings.antiAliasing = 2;
				}
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				if (QualitySettings.antiAliasing > 0)
				{
					QualitySettings.antiAliasing /= 2;
				}
				else
				{
					QualitySettings.antiAliasing = 0;
				}
				this.UpdateText();
			}
		}
		else if (this.Selected == 4)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisablePostAliasing = !OptionGlobals.DisablePostAliasing;
				this.UpdateText();
				this.QualityManager.UpdatePostAliasing();
			}
		}
		else if (this.Selected == 5)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableBloom = !OptionGlobals.DisableBloom;
				this.UpdateText();
				this.QualityManager.UpdateBloom();
			}
		}
		else if (this.Selected == 6)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.LowDetailStudents--;
				this.QualityManager.UpdateLowDetailStudents();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.LowDetailStudents++;
				this.QualityManager.UpdateLowDetailStudents();
				this.UpdateText();
			}
		}
		else if (this.Selected == 7)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.DrawDistance += 10;
				this.QualityManager.UpdateDrawDistance();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.DrawDistance -= 10;
				this.QualityManager.UpdateDrawDistance();
				this.UpdateText();
			}
		}
		else if (this.Selected == 8)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.Fog = !OptionGlobals.Fog;
				this.UpdateText();
				this.QualityManager.UpdateFog();
			}
		}
		else if (this.Selected == 9)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.ToggleRun = !OptionGlobals.ToggleRun;
				this.UpdateText();
				this.QualityManager.ToggleRun();
			}
		}
		else if (this.Selected == 10)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.DisableFarAnimations++;
				this.QualityManager.UpdateAnims();
				this.UpdateText();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableFarAnimations--;
				this.QualityManager.UpdateAnims();
				this.UpdateText();
			}
		}
		else if (this.Selected == 11)
		{
			if (this.InputManager.TappedRight)
			{
				OptionGlobals.FPSIndex++;
				this.QualityManager.UpdateFPSIndex();
			}
			else if (this.InputManager.TappedLeft)
			{
				OptionGlobals.FPSIndex--;
				this.QualityManager.UpdateFPSIndex();
			}
			this.UpdateText();
		}
		else if (this.Selected == 12)
		{
			if (this.InputManager.TappedRight)
			{
				if (OptionGlobals.Sensitivity < 10)
				{
					OptionGlobals.Sensitivity++;
				}
			}
			else if (this.InputManager.TappedLeft && OptionGlobals.Sensitivity > 1)
			{
				OptionGlobals.Sensitivity--;
			}
			this.UpdateText();
		}
		else if (this.Selected == 13)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.InvertAxis = !OptionGlobals.InvertAxis;
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 14)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.TutorialsOff = !OptionGlobals.TutorialsOff;
				if (SceneManager.GetActiveScene().name == "SchoolScene")
				{
					this.PauseScreen.Yandere.StudentManager.TutorialWindow.enabled = !OptionGlobals.TutorialsOff;
				}
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 15)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				Screen.SetResolution(Screen.width, Screen.height, !Screen.fullScreen);
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 16)
		{
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.DisableObscurance = !OptionGlobals.DisableObscurance;
				this.QualityManager.UpdateObscurance();
				this.UpdateText();
			}
			this.UpdateText();
		}
		else if (this.Selected == 17)
		{
			this.WarningMessage.SetActive(true);
			if (this.InputManager.TappedRight || this.InputManager.TappedLeft)
			{
				OptionGlobals.EnableShadows = !OptionGlobals.EnableShadows;
				this.QualityManager.UpdateShadows();
				this.UpdateText();
			}
			this.UpdateText();
		}
		if (this.Selected != 17)
		{
			this.WarningMessage.SetActive(false);
		}
		if (Input.GetKeyDown("l"))
		{
			OptionGlobals.ParticleCount = 1;
			OptionGlobals.DisableOutlines = true;
			QualitySettings.antiAliasing = 0;
			OptionGlobals.DisablePostAliasing = true;
			OptionGlobals.DisableBloom = true;
			OptionGlobals.LowDetailStudents = 1;
			OptionGlobals.DrawDistance = 50;
			OptionGlobals.EnableShadows = false;
			OptionGlobals.DisableFarAnimations = 1;
			OptionGlobals.RimLight = false;
			OptionGlobals.DepthOfField = false;
			this.QualityManager.UpdateFog();
			this.QualityManager.UpdateAnims();
			this.QualityManager.UpdateBloom();
			this.QualityManager.UpdateFPSIndex();
			this.QualityManager.UpdateShadows();
			this.QualityManager.UpdateParticles();
			this.QualityManager.UpdatePostAliasing();
			this.QualityManager.UpdateDrawDistance();
			this.QualityManager.UpdateLowDetailStudents();
			this.QualityManager.UpdateOutlines();
			this.UpdateText();
		}
		if (Input.GetButtonDown("B"))
		{
			this.WarningMessage.SetActive(false);
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[4].text = "Choose";
			this.PromptBar.UpdateButtons();
			if (this.PauseScreen.ScreenBlur != null)
			{
				this.PauseScreen.ScreenBlur.enabled = true;
			}
			this.PauseScreen.MainMenu.SetActive(true);
			this.PauseScreen.Sideways = false;
			this.PauseScreen.PressedB = true;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001FE7 RID: 8167 RVA: 0x00147AA8 File Offset: 0x00145EA8
	public void UpdateText()
	{
		if (OptionGlobals.ParticleCount == 3)
		{
			this.ParticleLabel.text = "High";
		}
		else if (OptionGlobals.ParticleCount == 2)
		{
			this.ParticleLabel.text = "Low";
		}
		else if (OptionGlobals.ParticleCount == 1)
		{
			this.ParticleLabel.text = "None";
		}
		this.FPSCapLabel.text = QualityManagerScript.FPSStrings[OptionGlobals.FPSIndex];
		this.OutlinesLabel.text = ((!OptionGlobals.DisableOutlines) ? "On" : "Off");
		this.AliasingLabel.text = QualitySettings.antiAliasing + "x";
		this.PostAliasingLabel.text = ((!OptionGlobals.DisablePostAliasing) ? "On" : "Off");
		this.BloomLabel.text = ((!OptionGlobals.DisableBloom) ? "On" : "Off");
		this.LowDetailLabel.text = ((OptionGlobals.LowDetailStudents != 0) ? ((OptionGlobals.LowDetailStudents * 10).ToString() + "m") : "Off");
		this.FarAnimsLabel.text = ((OptionGlobals.DisableFarAnimations != 0) ? ((OptionGlobals.DisableFarAnimations * 5).ToString() + "m") : "Off");
		this.DrawDistanceLabel.text = OptionGlobals.DrawDistance + "m";
		this.FogLabel.text = ((!OptionGlobals.Fog) ? "Off" : "On");
		this.ToggleRunLabel.text = ((!OptionGlobals.ToggleRun) ? "Hold" : "Toggle");
		this.SensitivityLabel.text = string.Empty + OptionGlobals.Sensitivity;
		this.InvertAxisLabel.text = ((!OptionGlobals.InvertAxis) ? "No" : "Yes");
		this.DisableTutorialsLabel.text = ((!OptionGlobals.TutorialsOff) ? "No" : "Yes");
		this.WindowedMode.text = ((!Screen.fullScreen) ? "Yes" : "No");
		this.AmbientObscurance.text = ((!OptionGlobals.DisableObscurance) ? "On" : "Off");
		this.ShadowsLabel.text = ((!OptionGlobals.EnableShadows) ? "No" : "Yes");
	}

	// Token: 0x06001FE8 RID: 8168 RVA: 0x00147D68 File Offset: 0x00146168
	private void UpdateHighlight()
	{
		if (this.Selected == 0)
		{
			this.Selected = this.SelectionLimit;
		}
		else if (this.Selected > this.SelectionLimit)
		{
			this.Selected = 1;
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 430f - 50f * (float)this.Selected, this.Highlight.localPosition.z);
	}

	// Token: 0x06001FE9 RID: 8169 RVA: 0x00147DF2 File Offset: 0x001461F2
	public void ToggleBackground()
	{
		OptionGlobals.DrawDistanceLimit = 350;
		OptionGlobals.DrawDistance = 350;
		this.QualityManager.UpdateDrawDistance();
		this.Background.SetActive(false);
	}

	// Token: 0x04002BEA RID: 11242
	public QualityManagerScript QualityManager;

	// Token: 0x04002BEB RID: 11243
	public InputManagerScript InputManager;

	// Token: 0x04002BEC RID: 11244
	public PauseScreenScript PauseScreen;

	// Token: 0x04002BED RID: 11245
	public PromptBarScript PromptBar;

	// Token: 0x04002BEE RID: 11246
	public UILabel DrawDistanceLabel;

	// Token: 0x04002BEF RID: 11247
	public UILabel PostAliasingLabel;

	// Token: 0x04002BF0 RID: 11248
	public UILabel LowDetailLabel;

	// Token: 0x04002BF1 RID: 11249
	public UILabel AliasingLabel;

	// Token: 0x04002BF2 RID: 11250
	public UILabel OutlinesLabel;

	// Token: 0x04002BF3 RID: 11251
	public UILabel ParticleLabel;

	// Token: 0x04002BF4 RID: 11252
	public UILabel BloomLabel;

	// Token: 0x04002BF5 RID: 11253
	public UILabel FogLabel;

	// Token: 0x04002BF6 RID: 11254
	public UILabel ToggleRunLabel;

	// Token: 0x04002BF7 RID: 11255
	public UILabel FarAnimsLabel;

	// Token: 0x04002BF8 RID: 11256
	public UILabel FPSCapLabel;

	// Token: 0x04002BF9 RID: 11257
	public UILabel SensitivityLabel;

	// Token: 0x04002BFA RID: 11258
	public UILabel InvertAxisLabel;

	// Token: 0x04002BFB RID: 11259
	public UILabel DisableTutorialsLabel;

	// Token: 0x04002BFC RID: 11260
	public UILabel WindowedMode;

	// Token: 0x04002BFD RID: 11261
	public UILabel AmbientObscurance;

	// Token: 0x04002BFE RID: 11262
	public UILabel ShadowsLabel;

	// Token: 0x04002BFF RID: 11263
	public int SelectionLimit = 2;

	// Token: 0x04002C00 RID: 11264
	public int Selected = 1;

	// Token: 0x04002C01 RID: 11265
	public Transform CloudSystem;

	// Token: 0x04002C02 RID: 11266
	public Transform Highlight;

	// Token: 0x04002C03 RID: 11267
	public GameObject Background;

	// Token: 0x04002C04 RID: 11268
	public GameObject WarningMessage;
}
