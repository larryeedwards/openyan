using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x06000C75 RID: 3189 RVA: 0x0006827C File Offset: 0x0006667C
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0006829C File Offset: 0x0006669C
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 vector = UICamera.lastEventPosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float x = Mathf.Clamp((vector.x - num) / num / this.range, -1f, 1f);
		float y = Mathf.Clamp((vector.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x04000B11 RID: 2833
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x04000B12 RID: 2834
	public float range = 1f;

	// Token: 0x04000B13 RID: 2835
	private Transform mTrans;

	// Token: 0x04000B14 RID: 2836
	private Quaternion mStart;

	// Token: 0x04000B15 RID: 2837
	private Vector2 mRot = Vector2.zero;
}
