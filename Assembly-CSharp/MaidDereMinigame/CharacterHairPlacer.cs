using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000140 RID: 320
	public class CharacterHairPlacer : MonoBehaviour
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00053E94 File Offset: 0x00052294
		private void Awake()
		{
			int num = UnityEngine.Random.Range(0, this.hairSprites.Length);
			this.hairInstance = new GameObject("Hair", new Type[]
			{
				typeof(SpriteRenderer)
			}).GetComponent<SpriteRenderer>();
			Transform transform = this.hairInstance.transform;
			transform.parent = base.transform;
			transform.localPosition = new Vector3(0f, 0f, -0.1f);
			this.hairInstance.sprite = this.hairSprites[num];
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00053F20 File Offset: 0x00052320
		public void WalkPose(float height)
		{
			this.hairInstance.transform.localPosition = new Vector3(0f, height, this.hairInstance.transform.localPosition.z);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00053F60 File Offset: 0x00052360
		public void HairPose(string point)
		{
			string[] array = point.Split(new char[]
			{
				','
			});
			float num;
			float.TryParse(array[0], out num);
			float y;
			float.TryParse(array[1], out y);
			this.hairInstance.transform.localPosition = new Vector3((!this.hairInstance.flipX) ? num : (-num), y, this.hairInstance.transform.localPosition.z);
		}

		// Token: 0x040007E1 RID: 2017
		public Sprite[] hairSprites;

		// Token: 0x040007E2 RID: 2018
		[HideInInspector]
		public SpriteRenderer hairInstance;
	}
}
