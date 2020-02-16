using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Viewport Camera")]
public class UIViewport : MonoBehaviour
{
	// Token: 0x06001493 RID: 5267 RVA: 0x0009F7E8 File Offset: 0x0009DBE8
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		if (this.sourceCamera == null)
		{
			this.sourceCamera = Camera.main;
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x0009F814 File Offset: 0x0009DC14
	private void LateUpdate()
	{
		if (this.topLeft != null && this.bottomRight != null)
		{
			if (this.topLeft.gameObject.activeInHierarchy)
			{
				Vector3 vector = this.sourceCamera.WorldToScreenPoint(this.topLeft.position);
				Vector3 vector2 = this.sourceCamera.WorldToScreenPoint(this.bottomRight.position);
				Rect rect = new Rect(vector.x / (float)Screen.width, vector2.y / (float)Screen.height, (vector2.x - vector.x) / (float)Screen.width, (vector.y - vector2.y) / (float)Screen.height);
				float num = this.fullSize * rect.height;
				if (rect != this.mCam.rect)
				{
					this.mCam.rect = rect;
				}
				if (this.mCam.orthographicSize != num)
				{
					this.mCam.orthographicSize = num;
				}
				this.mCam.enabled = true;
			}
			else
			{
				this.mCam.enabled = false;
			}
		}
	}

	// Token: 0x04001178 RID: 4472
	public Camera sourceCamera;

	// Token: 0x04001179 RID: 4473
	public Transform topLeft;

	// Token: 0x0400117A RID: 4474
	public Transform bottomRight;

	// Token: 0x0400117B RID: 4475
	public float fullSize = 1f;

	// Token: 0x0400117C RID: 4476
	private Camera mCam;
}
