using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200014B RID: 331
	public class Bubble : MonoBehaviour
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x00055B4C File Offset: 0x00053F4C
		private void Awake()
		{
			this.foodRenderer.sprite = null;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00055B5A File Offset: 0x00053F5A
		private void OnEnable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Combine(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00055B7C File Offset: 0x00053F7C
		private void OnDisable()
		{
			GameController.PauseGame = (BoolParameterEvent)Delegate.Remove(GameController.PauseGame, new BoolParameterEvent(this.Pause));
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00055BA0 File Offset: 0x00053FA0
		public void Pause(bool toPause)
		{
			if (toPause)
			{
				base.GetComponent<SpriteRenderer>().enabled = false;
				this.foodRenderer.gameObject.SetActive(false);
			}
			else
			{
				base.GetComponent<SpriteRenderer>().enabled = true;
				this.foodRenderer.gameObject.SetActive(true);
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00055BF2 File Offset: 0x00053FF2
		public void BubbleReachedMax()
		{
			this.foodRenderer.gameObject.SetActive(true);
			this.foodRenderer.sprite = this.food.largeSprite;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00055C1B File Offset: 0x0005401B
		public void BubbleClosing()
		{
			this.foodRenderer.gameObject.SetActive(false);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00055C2E File Offset: 0x0005402E
		public void KillBubble()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04000830 RID: 2096
		[HideInInspector]
		public Food food;

		// Token: 0x04000831 RID: 2097
		public SpriteRenderer foodRenderer;
	}
}
