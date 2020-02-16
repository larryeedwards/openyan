using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000154 RID: 340
	public class GameStarter : MonoBehaviour
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x000563F8 File Offset: 0x000547F8
		private void Awake()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.audioSource = base.GetComponent<AudioSource>();
			base.StartCoroutine(this.CountdownToStart());
			GameController.Instance.tipPage = this.tipPage;
			GameController.Instance.whiteFadeOutPost = this.whiteFadeOutPost;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0005644C File Offset: 0x0005484C
		public void SetGameTime(float gameTime)
		{
			int num = Mathf.CeilToInt(gameTime);
			if ((float)num == 10f)
			{
				SFXController.PlaySound(SFXController.Sounds.ClockTick);
			}
			if (gameTime > 3f)
			{
				return;
			}
			switch (num)
			{
			case 1:
			case 2:
			case 3:
				this.spriteRenderer.sprite = this.numbers[num];
				break;
			default:
				this.EndGame();
				break;
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000564BE File Offset: 0x000548BE
		public void EndGame()
		{
			base.StartCoroutine(this.EndGameRoutine());
			SFXController.PlaySound(SFXController.Sounds.GameSuccess);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000564D4 File Offset: 0x000548D4
		private IEnumerator CountdownToStart()
		{
			yield return new WaitForSeconds(GameController.Instance.activeDifficultyVariables.transitionTime);
			SFXController.PlaySound(SFXController.Sounds.Countdown);
			while (this.timeToStart > 0)
			{
				yield return new WaitForSeconds(1f);
				this.timeToStart--;
				this.spriteRenderer.sprite = this.numbers[this.timeToStart];
			}
			yield return new WaitForSeconds(1f);
			GameController.SetPause(false);
			this.spriteRenderer.sprite = null;
			yield break;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000564F0 File Offset: 0x000548F0
		private IEnumerator EndGameRoutine()
		{
			GameController.SetPause(true);
			this.spriteRenderer.sprite = this.timeUp;
			yield return new WaitForSeconds(1f);
			UnityEngine.Object.FindObjectOfType<InteractionMenu>().gameObject.SetActive(false);
			GameController.TimeUp();
			yield break;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0005650B File Offset: 0x0005490B
		public void SetAudioPitch(float value)
		{
			this.audioSource.pitch = value;
		}

		// Token: 0x0400085A RID: 2138
		public List<Sprite> numbers;

		// Token: 0x0400085B RID: 2139
		public SpriteRenderer whiteFadeOutPost;

		// Token: 0x0400085C RID: 2140
		public Sprite timeUp;

		// Token: 0x0400085D RID: 2141
		public TipPage tipPage;

		// Token: 0x0400085E RID: 2142
		private AudioSource audioSource;

		// Token: 0x0400085F RID: 2143
		private SpriteRenderer spriteRenderer;

		// Token: 0x04000860 RID: 2144
		private int timeToStart = 3;
	}
}
