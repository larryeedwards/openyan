using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200014C RID: 332
	[CreateAssetMenu(fileName = "New Food Item", menuName = "Food")]
	public class Food : ScriptableObject
	{
		// Token: 0x04000832 RID: 2098
		public Sprite largeSprite;

		// Token: 0x04000833 RID: 2099
		public Sprite smallSprite;

		// Token: 0x04000834 RID: 2100
		public float cookTimeMultiplier = 1f;
	}
}
