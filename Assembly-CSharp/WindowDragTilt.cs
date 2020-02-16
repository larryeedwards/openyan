using System;
using UnityEngine;

// Token: 0x020001A3 RID: 419
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x06000C8E RID: 3214 RVA: 0x00068AE0 File Offset: 0x00066EE0
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00068B00 File Offset: 0x00066F00
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x04000B2B RID: 2859
	public int updateOrder;

	// Token: 0x04000B2C RID: 2860
	public float degrees = 30f;

	// Token: 0x04000B2D RID: 2861
	private Vector3 mLastPos;

	// Token: 0x04000B2E RID: 2862
	private Transform mTrans;

	// Token: 0x04000B2F RID: 2863
	private float mAngle;
}
