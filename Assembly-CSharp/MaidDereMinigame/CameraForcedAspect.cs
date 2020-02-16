using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200014F RID: 335
	[RequireComponent(typeof(Camera))]
	public class CameraForcedAspect : MonoBehaviour
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x00055CEB File Offset: 0x000540EB
		private void Awake()
		{
			this.cam = base.GetComponent<Camera>();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00055CFC File Offset: 0x000540FC
		private void Start()
		{
			float num = this.targetAspect.x / this.targetAspect.y;
			float num2 = (float)Screen.width / (float)Screen.height;
			float num3 = num2 / num;
			if (num3 < 1f)
			{
				Rect rect = this.cam.rect;
				rect.width = 1f;
				rect.height = num3;
				rect.x = 0f;
				rect.y = (1f - num3) / 2f;
				this.cam.rect = rect;
			}
			else
			{
				Rect rect2 = this.cam.rect;
				float num4 = 1f / num3;
				rect2.width = num4;
				rect2.height = 1f;
				rect2.x = (1f - num4) / 2f;
				rect2.y = 0f;
				this.cam.rect = rect2;
			}
		}

		// Token: 0x0400083A RID: 2106
		public Vector2 targetAspect = new Vector2(16f, 9f);

		// Token: 0x0400083B RID: 2107
		private Camera cam;
	}
}
