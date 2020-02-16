using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004F5 RID: 1269
public class SceneLoader : MonoBehaviour
{
	// Token: 0x06001FB9 RID: 8121 RVA: 0x00145134 File Offset: 0x00143534
	private void Start()
	{
		Time.timeScale = 1f;
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
			PlayerGlobals.Money = 10f;
		}
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
			this.loadingText.color = new Color(1f, 0f, 0f, 1f);
			this.crashText.color = new Color(1f, 0f, 0f, 1f);
			this.KeyboardGraphic.color = new Color(1f, 0f, 0f, 1f);
			this.ControllerLines.color = new Color(1f, 0f, 0f, 1f);
			this.LightAnimation.SetActive(false);
			this.DarkAnimation.SetActive(true);
			for (int i = 1; i < this.ControllerText.Length; i++)
			{
				this.ControllerText[i].color = new Color(1f, 0f, 0f, 1f);
			}
			for (int i = 1; i < this.KeyboardText.Length; i++)
			{
				this.KeyboardText[i].color = new Color(1f, 0f, 0f, 1f);
			}
		}
		if (PlayerGlobals.UsingGamepad)
		{
			this.Keyboard.SetActive(false);
			this.Gamepad.SetActive(true);
		}
		if (!this.Debugging)
		{
			base.StartCoroutine(this.LoadNewScene());
		}
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x0014530C File Offset: 0x0014370C
	private void Update()
	{
		if (this.Debugging)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 10f)
			{
				this.Debugging = false;
				base.StartCoroutine(this.LoadNewScene());
			}
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x0014535C File Offset: 0x0014375C
	private IEnumerator LoadNewScene()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync("SchoolScene");
		while (!async.isDone)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x04002B56 RID: 11094
	[SerializeField]
	private UILabel loadingText;

	// Token: 0x04002B57 RID: 11095
	[SerializeField]
	private UILabel crashText;

	// Token: 0x04002B58 RID: 11096
	private float timer;

	// Token: 0x04002B59 RID: 11097
	public UILabel[] ControllerText;

	// Token: 0x04002B5A RID: 11098
	public UILabel[] KeyboardText;

	// Token: 0x04002B5B RID: 11099
	public GameObject LightAnimation;

	// Token: 0x04002B5C RID: 11100
	public GameObject DarkAnimation;

	// Token: 0x04002B5D RID: 11101
	public GameObject Keyboard;

	// Token: 0x04002B5E RID: 11102
	public GameObject Gamepad;

	// Token: 0x04002B5F RID: 11103
	public UITexture ControllerLines;

	// Token: 0x04002B60 RID: 11104
	public UITexture KeyboardGraphic;

	// Token: 0x04002B61 RID: 11105
	public bool Debugging;

	// Token: 0x04002B62 RID: 11106
	public float Timer;
}
