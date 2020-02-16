using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200016C RID: 364
	public class TipPage : MonoBehaviour
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00057E10 File Offset: 0x00056210
		public void Init()
		{
			this.cards = new List<TipCard>();
			IEnumerator enumerator = base.transform.GetChild(0).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					IEnumerator enumerator2 = transform.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							Transform transform2 = (Transform)obj2;
							this.cards.Add(transform2.GetComponent<TipCard>());
						}
					}
					finally
					{
						IDisposable disposable;
						if ((disposable = (enumerator2 as IDisposable)) != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			base.gameObject.SetActive(false);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00057EE8 File Offset: 0x000562E8
		public void DisplayTips(List<float> tips)
		{
			if (tips == null)
			{
				tips = new List<float>();
			}
			base.gameObject.SetActive(true);
			float num = 0f;
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (tips.Count > i)
				{
					this.cards[i].SetTip(tips[i]);
					num += tips[i];
				}
				else
				{
					this.cards[i].SetTip(0f);
				}
			}
			float basePay = GameController.Instance.activeDifficultyVariables.basePay;
			GameController.Instance.totalPayout = num + basePay;
			this.wageCard.SetTip(basePay);
			this.totalCard.SetTip(num + basePay);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00057FB0 File Offset: 0x000563B0
		private void Update()
		{
			if (this.stopInteraction)
			{
				return;
			}
			if (Input.GetButtonDown("A"))
			{
				GameController.GoToExitScene(true);
				this.stopInteraction = true;
			}
		}

		// Token: 0x040008C0 RID: 2240
		public TipCard wageCard;

		// Token: 0x040008C1 RID: 2241
		public TipCard totalCard;

		// Token: 0x040008C2 RID: 2242
		private List<TipCard> cards;

		// Token: 0x040008C3 RID: 2243
		private bool stopInteraction;
	}
}
