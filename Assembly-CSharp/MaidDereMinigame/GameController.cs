using System;
using System.Collections;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MaidDereMinigame
{
	// Token: 0x02000151 RID: 337
	public class GameController : MonoBehaviour
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00055DF0 File Offset: 0x000541F0
		public static GameController Instance
		{
			get
			{
				if (GameController.instance == null)
				{
					GameController.instance = UnityEngine.Object.FindObjectOfType<GameController>();
				}
				return GameController.instance;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00055E11 File Offset: 0x00054211
		public static SceneWrapper Scenes
		{
			get
			{
				return GameController.Instance.scenes;
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00055E1D File Offset: 0x0005421D
		public static void GoToExitScene(bool fadeOut = true)
		{
			GameController.Instance.StartCoroutine(GameController.Instance.FadeWithAction(delegate
			{
				PlayerGlobals.Money += GameController.Instance.totalPayout;
				if (SceneManager.GetActiveScene().name == "MaidMenuScene")
				{
					SceneManager.LoadScene("StreetScene");
				}
				else
				{
					SceneManager.LoadScene("CalendarScene");
				}
			}, fadeOut, true));
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00055E53 File Offset: 0x00054253
		private void Awake()
		{
			if (GameController.Instance != this)
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00055E88 File Offset: 0x00054288
		public static void SetPause(bool toPause)
		{
			if (GameController.PauseGame != null)
			{
				GameController.PauseGame(toPause);
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00055E9F File Offset: 0x0005429F
		public void LoadScene(SceneObject scene)
		{
			base.StartCoroutine(this.FadeWithAction(delegate
			{
				SceneManager.LoadScene("MaidGameScene");
			}, true, false));
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00055ED0 File Offset: 0x000542D0
		private IEnumerator FadeWithAction(Action PostFadeAction, bool doFadeOut = true, bool destroyGameController = false)
		{
			float timeToFade = 0f;
			if (doFadeOut)
			{
				while (timeToFade <= this.activeDifficultyVariables.transitionTime)
				{
					this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, timeToFade / this.activeDifficultyVariables.transitionTime));
					timeToFade += Time.deltaTime;
					yield return null;
				}
				this.spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				timeToFade = this.activeDifficultyVariables.transitionTime;
			}
			PostFadeAction();
			if (destroyGameController)
			{
				if (GameController.Instance.whiteFadeOutPost != null && doFadeOut)
				{
					GameController.Instance.whiteFadeOutPost.color = Color.white;
				}
				UnityEngine.Object.Destroy(GameController.Instance.gameObject);
				Camera.main.farClipPlane = 0f;
				GameController.instance = null;
			}
			else
			{
				while (timeToFade >= 0f)
				{
					this.spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, timeToFade / this.activeDifficultyVariables.transitionTime));
					timeToFade -= Time.deltaTime;
					yield return null;
				}
				this.spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
			}
			yield break;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00055F00 File Offset: 0x00054300
		public static void TimeUp()
		{
			GameController.SetPause(true);
			GameController.Instance.tipPage.Init();
			GameController.Instance.tipPage.DisplayTips(GameController.Instance.tips);
			UnityEngine.Object.FindObjectOfType<GameStarter>().GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00055F40 File Offset: 0x00054340
		public static void AddTip(float tip)
		{
			if (GameController.Instance.tips == null)
			{
				GameController.Instance.tips = new List<float>();
			}
			tip = Mathf.Floor(tip * 100f) / 100f;
			GameController.Instance.tips.Add(tip);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00055F90 File Offset: 0x00054390
		public static float GetTotalDollars()
		{
			float num = 0f;
			foreach (float num2 in GameController.Instance.tips)
			{
				float num3 = num2;
				num += Mathf.Floor(num3 * 100f) / 100f;
			}
			return num + GameController.Instance.activeDifficultyVariables.basePay;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00056018 File Offset: 0x00054418
		public static void AddAngryCustomer()
		{
			GameController.Instance.angryCustomers++;
			if (GameController.Instance.angryCustomers >= GameController.Instance.activeDifficultyVariables.failQuantity)
			{
				FailGame.GameFailed();
				GameController.SetPause(true);
			}
		}

		// Token: 0x0400083C RID: 2108
		private static GameController instance;

		// Token: 0x0400083D RID: 2109
		[Reorderable]
		public Sprites numbers;

		// Token: 0x0400083E RID: 2110
		public SceneWrapper scenes;

		// Token: 0x0400083F RID: 2111
		[Tooltip("Scene Object Reference to return to when the game ends.")]
		public SceneObject returnScene;

		// Token: 0x04000840 RID: 2112
		public SetupVariables activeDifficultyVariables;

		// Token: 0x04000841 RID: 2113
		public SetupVariables easyVariables;

		// Token: 0x04000842 RID: 2114
		public SetupVariables mediumVariables;

		// Token: 0x04000843 RID: 2115
		public SetupVariables hardVariables;

		// Token: 0x04000844 RID: 2116
		private List<float> tips;

		// Token: 0x04000845 RID: 2117
		private SpriteRenderer spriteRenderer;

		// Token: 0x04000846 RID: 2118
		private int angryCustomers;

		// Token: 0x04000847 RID: 2119
		[HideInInspector]
		public TipPage tipPage;

		// Token: 0x04000848 RID: 2120
		[HideInInspector]
		public float totalPayout;

		// Token: 0x04000849 RID: 2121
		[HideInInspector]
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x0400084A RID: 2122
		public static BoolParameterEvent PauseGame;
	}
}
