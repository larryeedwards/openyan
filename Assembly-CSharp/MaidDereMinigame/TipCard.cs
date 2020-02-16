using System;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200016A RID: 362
	public class TipCard : MonoBehaviour
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x00057C5C File Offset: 0x0005605C
		public void SetTip(float tip)
		{
			if (tip == 0f)
			{
				base.gameObject.SetActive(false);
			}
			string text = string.Format("{0:#.00}", tip).Replace(".", string.Empty);
			string text2 = string.Empty;
			for (int i = text.Length - 1; i >= 0; i--)
			{
				text2 += text[i];
			}
			for (int j = 0; j < text2.Length; j++)
			{
				int index = -1;
				int.TryParse(text2[j].ToString(), out index);
				this.digits[j].sprite = GameController.Instance.numbers[index];
			}
			if (text2.Length <= 3)
			{
				this.digits[3].sprite = GameController.Instance.numbers[10];
				this.dollarSign.gameObject.SetActive(false);
			}
			if (text2.Length <= 4 && this.digits.Count > 4)
			{
				this.digits[4].sprite = GameController.Instance.numbers[10];
				this.dollarSign.gameObject.SetActive(false);
				if (text2.Length < 4)
				{
					this.digits[3].sprite = GameController.Instance.numbers[10];
					this.digits[4].gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x040008BE RID: 2238
		[Reorderable]
		public SpriteRenderers digits;

		// Token: 0x040008BF RID: 2239
		public SpriteRenderer dollarSign;
	}
}
