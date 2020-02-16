using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000166 RID: 358
	public class InteractionMenu : MonoBehaviour
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000579AA File Offset: 0x00055DAA
		public static InteractionMenu Instance
		{
			get
			{
				if (InteractionMenu.instance == null)
				{
					InteractionMenu.instance = UnityEngine.Object.FindObjectOfType<InteractionMenu>();
				}
				return InteractionMenu.instance;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000579CB File Offset: 0x00055DCB
		private void Awake()
		{
			InteractionMenu.SetAButton(InteractionMenu.AButtonText.None);
			InteractionMenu.SetBButton(false);
			InteractionMenu.SetADButton(true);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000579E0 File Offset: 0x00055DE0
		public static void SetAButton(InteractionMenu.AButtonText text)
		{
			for (int i = 0; i < InteractionMenu.Instance.aButtonSprites.Length; i++)
			{
				if (i == (int)text)
				{
					InteractionMenu.Instance.aButtonSprites[i].gameObject.SetActive(true);
				}
				else
				{
					InteractionMenu.Instance.aButtonSprites[i].gameObject.SetActive(false);
				}
			}
			foreach (SpriteRenderer spriteRenderer in InteractionMenu.Instance.aButtons)
			{
				spriteRenderer.gameObject.SetActive(text != InteractionMenu.AButtonText.None);
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00057A7C File Offset: 0x00055E7C
		public static void SetBButton(bool on)
		{
			foreach (SpriteRenderer spriteRenderer in InteractionMenu.Instance.backButtons)
			{
				spriteRenderer.gameObject.SetActive(on);
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00057AB8 File Offset: 0x00055EB8
		public static void SetADButton(bool on)
		{
			foreach (SpriteRenderer spriteRenderer in InteractionMenu.Instance.moveButtons)
			{
				spriteRenderer.gameObject.SetActive(on);
			}
		}

		// Token: 0x040008A7 RID: 2215
		private static InteractionMenu instance;

		// Token: 0x040008A8 RID: 2216
		public GameObject interactObject;

		// Token: 0x040008A9 RID: 2217
		public GameObject backObject;

		// Token: 0x040008AA RID: 2218
		public GameObject moveObject;

		// Token: 0x040008AB RID: 2219
		public SpriteRenderer[] aButtons;

		// Token: 0x040008AC RID: 2220
		public SpriteRenderer[] aButtonSprites;

		// Token: 0x040008AD RID: 2221
		public SpriteRenderer[] backButtons;

		// Token: 0x040008AE RID: 2222
		public SpriteRenderer[] moveButtons;

		// Token: 0x02000167 RID: 359
		public enum AButtonText
		{
			// Token: 0x040008B0 RID: 2224
			ChoosePlate,
			// Token: 0x040008B1 RID: 2225
			GrabPlate,
			// Token: 0x040008B2 RID: 2226
			KitchenMenu,
			// Token: 0x040008B3 RID: 2227
			PlaceOrder,
			// Token: 0x040008B4 RID: 2228
			TakeOrder,
			// Token: 0x040008B5 RID: 2229
			TossPlate,
			// Token: 0x040008B6 RID: 2230
			GiveFood,
			// Token: 0x040008B7 RID: 2231
			None
		}
	}
}
