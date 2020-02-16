using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200014E RID: 334
	[RequireComponent(typeof(SpriteRenderer))]
	public class FoodInstance : MonoBehaviour
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x00055C69 File Offset: 0x00054069
		private void Start()
		{
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
			this.spriteRenderer.sprite = this.food.smallSprite;
			this.heat = this.timeToCool;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00055C99 File Offset: 0x00054099
		private void Update()
		{
			this.heat -= Time.deltaTime;
			this.warmthMeter.SetFill(this.heat / this.timeToCool);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00055CC5 File Offset: 0x000540C5
		public void SetHeat(float newHeat)
		{
			this.heat = newHeat;
		}

		// Token: 0x04000835 RID: 2101
		public Food food;

		// Token: 0x04000836 RID: 2102
		public Meter warmthMeter;

		// Token: 0x04000837 RID: 2103
		public float timeToCool = 30f;

		// Token: 0x04000838 RID: 2104
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x04000839 RID: 2105
		private float heat;
	}
}
