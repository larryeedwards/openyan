using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000168 RID: 360
	public class Meter : MonoBehaviour
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00057AFC File Offset: 0x00055EFC
		private void Awake()
		{
			this.startPos = this.fillBar.transform.localPosition.x;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00057B28 File Offset: 0x00055F28
		public void SetFill(float interpolater)
		{
			float num = Mathf.Lerp(this.emptyPos, this.startPos, interpolater);
			num = Mathf.Round(num * 50f) / 50f;
			this.fillBar.transform.localPosition = new Vector3(num, 0f, 0f);
		}

		// Token: 0x040008B8 RID: 2232
		public SpriteRenderer fillBar;

		// Token: 0x040008B9 RID: 2233
		public float emptyPos;

		// Token: 0x040008BA RID: 2234
		private float startPos;
	}
}
