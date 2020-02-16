using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200042A RID: 1066
public class HomeVideoGamesScript : MonoBehaviour
{
	// Token: 0x06001CD4 RID: 7380 RVA: 0x00109040 File Offset: 0x00107440
	private void Start()
	{
		if (TaskGlobals.GetTaskStatus(38) == 0)
		{
			this.TitleScreens[1] = this.TitleScreens[5];
			UILabel uilabel = this.GameTitles[1];
			uilabel.text = this.GameTitles[5].text;
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		}
		this.TitleScreen.mainTexture = this.TitleScreens[1];
	}

	// Token: 0x06001CD5 RID: 7381 RVA: 0x001090D4 File Offset: 0x001074D4
	private void Update()
	{
		if (this.HomeCamera.Destination == this.HomeCamera.Destinations[5])
		{
			if (Input.GetKeyDown("y"))
			{
				TaskGlobals.SetTaskStatus(38, 1);
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			this.TV.localScale = Vector3.Lerp(this.TV.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			if (!this.HomeYandere.CanMove)
			{
				if (!this.HomeDarkness.FadeOut)
				{
					if (this.InputManager.TappedDown)
					{
						this.ID++;
						if (this.ID > 5)
						{
							this.ID = 1;
						}
						this.TitleScreen.mainTexture = this.TitleScreens[this.ID];
						this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 150f - (float)this.ID * 50f, this.Highlight.localPosition.z);
					}
					if (this.InputManager.TappedUp)
					{
						this.ID--;
						if (this.ID < 1)
						{
							this.ID = 5;
						}
						this.TitleScreen.mainTexture = this.TitleScreens[this.ID];
						this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 150f - (float)this.ID * 50f, this.Highlight.localPosition.z);
					}
					if (Input.GetButtonDown("A") && this.GameTitles[this.ID].color.a == 1f)
					{
						Transform transform = this.HomeCamera.Targets[5];
						transform.localPosition = new Vector3(transform.localPosition.x, 1.153333f, transform.localPosition.z);
						this.HomeDarkness.Sprite.color = new Color(this.HomeDarkness.Sprite.color.r, this.HomeDarkness.Sprite.color.g, this.HomeDarkness.Sprite.color.b, -1f);
						this.HomeDarkness.FadeOut = true;
						this.HomeWindow.Show = false;
						this.PromptBar.Show = false;
						this.HomeCamera.ID = 5;
					}
					if (Input.GetButtonDown("B"))
					{
						this.Quit();
					}
				}
				else
				{
					Transform transform2 = this.HomeCamera.Destinations[5];
					Transform transform3 = this.HomeCamera.Targets[5];
					transform2.position = new Vector3(Mathf.Lerp(transform2.position.x, transform3.position.x, Time.deltaTime * 0.75f), Mathf.Lerp(transform2.position.y, transform3.position.y, Time.deltaTime * 10f), Mathf.Lerp(transform2.position.z, transform3.position.z, Time.deltaTime * 10f));
				}
			}
		}
		else
		{
			this.TV.localScale = Vector3.Lerp(this.TV.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
	}

	// Token: 0x06001CD6 RID: 7382 RVA: 0x001094B8 File Offset: 0x001078B8
	public void Quit()
	{
		this.Controller.transform.localPosition = new Vector3(0.20385f, 0.0595f, 0.0215f);
		this.Controller.transform.localEulerAngles = new Vector3(-90f, -90f, 0f);
		this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
		this.HomeCamera.Target = this.HomeCamera.Targets[0];
		this.HomeYandere.CanMove = true;
		this.HomeYandere.enabled = true;
		this.HomeWindow.Show = false;
		this.HomeCamera.PlayMusic();
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
	}

	// Token: 0x04002289 RID: 8841
	public InputManagerScript InputManager;

	// Token: 0x0400228A RID: 8842
	public HomeDarknessScript HomeDarkness;

	// Token: 0x0400228B RID: 8843
	public HomeYandereScript HomeYandere;

	// Token: 0x0400228C RID: 8844
	public HomeCameraScript HomeCamera;

	// Token: 0x0400228D RID: 8845
	public HomeWindowScript HomeWindow;

	// Token: 0x0400228E RID: 8846
	public PromptBarScript PromptBar;

	// Token: 0x0400228F RID: 8847
	public Texture[] TitleScreens;

	// Token: 0x04002290 RID: 8848
	public UITexture TitleScreen;

	// Token: 0x04002291 RID: 8849
	public GameObject Controller;

	// Token: 0x04002292 RID: 8850
	public Transform Highlight;

	// Token: 0x04002293 RID: 8851
	public UILabel[] GameTitles;

	// Token: 0x04002294 RID: 8852
	public Transform TV;

	// Token: 0x04002295 RID: 8853
	public int ID = 1;
}
