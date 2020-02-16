using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200015B RID: 347
	public class FlipBook : MonoBehaviour
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00056908 File Offset: 0x00054D08
		public static FlipBook Instance
		{
			get
			{
				if (FlipBook.instance == null)
				{
					FlipBook.instance = UnityEngine.Object.FindObjectOfType<FlipBook>();
				}
				return FlipBook.instance;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00056929 File Offset: 0x00054D29
		private void Awake()
		{
			base.StartCoroutine(this.OpenBook());
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00056938 File Offset: 0x00054D38
		private IEnumerator OpenBook()
		{
			yield return new WaitForSeconds(1f);
			this.FlipToPage(1);
			yield break;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00056953 File Offset: 0x00054D53
		private void Update()
		{
			if (this.stopInputs)
			{
				return;
			}
			if (this.curPage > 1 && Input.GetButtonDown("B") && this.canGoBack)
			{
				this.FlipToPage(1);
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0005698E File Offset: 0x00054D8E
		public void StopInputs()
		{
			this.stopInputs = true;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00056997 File Offset: 0x00054D97
		public void FlipToPage(int page)
		{
			SFXController.PlaySound(SFXController.Sounds.PageTurn);
			base.StartCoroutine(this.FlipToPageRoutine(page));
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x000569B0 File Offset: 0x00054DB0
		private IEnumerator FlipToPageRoutine(int page)
		{
			bool forward = this.curPage < page;
			this.canGoBack = false;
			if (forward)
			{
				while (this.curPage < page)
				{
					this.flipBookPages[this.curPage++].Transition(forward);
				}
				yield return new WaitForSeconds(0.4f);
				this.flipBookPages[this.curPage].ObjectActive(true);
			}
			else
			{
				this.flipBookPages[this.curPage].ObjectActive(false);
				while (this.curPage > page)
				{
					this.flipBookPages[--this.curPage].Transition(forward);
				}
				yield return new WaitForSeconds(0.6f);
				this.flipBookPages[this.curPage].ObjectActive(true);
			}
			this.canGoBack = true;
			yield break;
		}

		// Token: 0x04000879 RID: 2169
		private static FlipBook instance;

		// Token: 0x0400087A RID: 2170
		public List<FlipBookPage> flipBookPages;

		// Token: 0x0400087B RID: 2171
		private int curPage;

		// Token: 0x0400087C RID: 2172
		private bool canGoBack;

		// Token: 0x0400087D RID: 2173
		private bool stopInputs;
	}
}
