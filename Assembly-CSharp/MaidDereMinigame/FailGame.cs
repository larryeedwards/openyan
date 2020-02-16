using System;
using System.Collections;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000164 RID: 356
	public class FailGame : MonoBehaviour
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000572CF File Offset: 0x000556CF
		public static FailGame Instance
		{
			get
			{
				if (FailGame.instance == null)
				{
					FailGame.instance = UnityEngine.Object.FindObjectOfType<FailGame>();
				}
				return FailGame.instance;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000572F0 File Offset: 0x000556F0
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.textRenderer = base.transform.GetChild(0).GetComponent<SpriteRenderer>();
			this.targetTransitionTime = GameController.Instance.activeDifficultyVariables.transitionTime * this.fadeMultiplier;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0005733C File Offset: 0x0005573C
		public static void GameFailed()
		{
			FailGame.Instance.StartCoroutine(FailGame.Instance.GameFailedRoutine());
			FailGame.Instance.StartCoroutine(FailGame.Instance.SlowPitch());
			SFXController.PlaySound(SFXController.Sounds.GameFail);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00057370 File Offset: 0x00055770
		private IEnumerator GameFailedRoutine()
		{
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			yield return null;
			this.textRenderer.color = Color.white;
			while (this.transitionTime < this.targetTransitionTime)
			{
				this.transitionTime += Time.deltaTime;
				yield return null;
			}
			base.transform.GetChild(1).gameObject.SetActive(true);
			while (!Input.anyKeyDown)
			{
				yield return null;
			}
			while (this.transitionTime < this.targetTransitionTime)
			{
				this.transitionTime += Time.deltaTime;
				float opacity = Mathf.Lerp(0f, 1f, this.transitionTime / this.targetTransitionTime);
				this.spriteRenderer.color = new Color(0f, 0f, 0f, opacity);
				yield return null;
			}
			GameController.GoToExitScene(false);
			yield break;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0005738C File Offset: 0x0005578C
		private IEnumerator SlowPitch()
		{
			GameStarter starter = UnityEngine.Object.FindObjectOfType<GameStarter>();
			float timeToZero = 5f;
			while (timeToZero > 0f)
			{
				starter.SetAudioPitch(Mathf.Lerp(0f, 1f, timeToZero / 5f));
				timeToZero -= Time.deltaTime;
				yield return null;
			}
			starter.SetAudioPitch(0f);
			yield break;
		}

		// Token: 0x04000896 RID: 2198
		private static FailGame instance;

		// Token: 0x04000897 RID: 2199
		public float fadeMultiplier = 2f;

		// Token: 0x04000898 RID: 2200
		private SpriteRenderer spriteRenderer;

		// Token: 0x04000899 RID: 2201
		private SpriteRenderer textRenderer;

		// Token: 0x0400089A RID: 2202
		private float targetTransitionTime;

		// Token: 0x0400089B RID: 2203
		private float transitionTime;
	}
}
