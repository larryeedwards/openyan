using System;
using System.Collections.Generic;
using MaidDereMinigame.Malee;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000165 RID: 357
	public class FoodMenu : MonoBehaviour
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000576A4 File Offset: 0x00055AA4
		public static FoodMenu Instance
		{
			get
			{
				if (FoodMenu.instance == null)
				{
					FoodMenu.instance = UnityEngine.Object.FindObjectOfType<FoodMenu>();
				}
				return FoodMenu.instance;
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000576C8 File Offset: 0x00055AC8
		private void Awake()
		{
			this.SetMenuIcons();
			this.menuSelectorTarget = this.menuSlots[0].position.x;
			this.startY = this.menuSelector.position.y;
			this.startZ = this.menuSelector.position.z;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0005772C File Offset: 0x00055B2C
		public void SetMenuIcons()
		{
			this.menuSlots = new List<Transform>();
			for (int i = 0; i < this.menuSlotParent.childCount; i++)
			{
				Transform child = this.menuSlotParent.GetChild(i);
				this.menuSlots.Add(child);
				if (this.foodItems.Count >= i)
				{
					child.GetChild(0).GetComponent<SpriteRenderer>().sprite = this.foodItems[i].largeSprite;
				}
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000577AC File Offset: 0x00055BAC
		public void SetActive(int index)
		{
			this.menuSelectorTarget = this.menuSlots[index].position.x;
			this.interpolator = 0f;
			this.activeIndex = index;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000577EC File Offset: 0x00055BEC
		public Food GetActiveFood()
		{
			Food food = UnityEngine.Object.Instantiate<Food>(this.foodItems[this.activeIndex]);
			food.name = this.foodItems[this.activeIndex].name;
			return food;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00057830 File Offset: 0x00055C30
		public Food GetRandomFood()
		{
			int index = UnityEngine.Random.Range(0, this.foodItems.Count);
			Food food = UnityEngine.Object.Instantiate<Food>(this.foodItems[index]);
			food.name = this.foodItems[index].name;
			return food;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0005787C File Offset: 0x00055C7C
		private void Update()
		{
			if (this.interpolator < 1f)
			{
				float x = Mathf.Lerp(this.menuSelector.position.x, this.menuSelectorTarget, this.interpolator);
				this.menuSelector.position = new Vector3(x, this.startY, this.startZ);
				this.interpolator += Time.deltaTime * this.selectorMoveSpeed;
			}
			else
			{
				this.menuSelector.transform.position = new Vector3(this.menuSelectorTarget, this.startY, this.startZ);
			}
			if (YandereController.rightButton)
			{
				this.IncrementSelection();
			}
			else if (YandereController.leftButton)
			{
				this.DecrementSelection();
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00057945 File Offset: 0x00055D45
		private void IncrementSelection()
		{
			this.SetActive((this.activeIndex + 1) % this.menuSlots.Count);
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00057968 File Offset: 0x00055D68
		private void DecrementSelection()
		{
			if (this.activeIndex == 0)
			{
				this.SetActive(this.menuSlots.Count - 1);
			}
			else
			{
				this.SetActive(this.activeIndex - 1);
			}
			SFXController.PlaySound(SFXController.Sounds.MenuSelect);
		}

		// Token: 0x0400089C RID: 2204
		private static FoodMenu instance;

		// Token: 0x0400089D RID: 2205
		[Reorderable]
		public Foods foodItems;

		// Token: 0x0400089E RID: 2206
		public Transform menuSelector;

		// Token: 0x0400089F RID: 2207
		public Transform menuSlotParent;

		// Token: 0x040008A0 RID: 2208
		public float selectorMoveSpeed = 3f;

		// Token: 0x040008A1 RID: 2209
		private List<Transform> menuSlots;

		// Token: 0x040008A2 RID: 2210
		private float menuSelectorTarget;

		// Token: 0x040008A3 RID: 2211
		private float startY;

		// Token: 0x040008A4 RID: 2212
		private float startZ;

		// Token: 0x040008A5 RID: 2213
		private float interpolator;

		// Token: 0x040008A6 RID: 2214
		private int activeIndex;
	}
}
