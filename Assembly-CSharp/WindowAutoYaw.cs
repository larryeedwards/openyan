using System;
using UnityEngine;

// Token: 0x020001A2 RID: 418
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x06000C8A RID: 3210 RVA: 0x00068A1A File Offset: 0x00066E1A
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x00068A2C File Offset: 0x00066E2C
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x00068A64 File Offset: 0x00066E64
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x04000B27 RID: 2855
	public int updateOrder;

	// Token: 0x04000B28 RID: 2856
	public Camera uiCamera;

	// Token: 0x04000B29 RID: 2857
	public float yawAmount = 20f;

	// Token: 0x04000B2A RID: 2858
	private Transform mTrans;
}
