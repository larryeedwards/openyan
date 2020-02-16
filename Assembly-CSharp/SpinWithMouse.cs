using System;
using UnityEngine;

// Token: 0x0200019F RID: 415
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x06000C82 RID: 3202 RVA: 0x0006874D File Offset: 0x00066B4D
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0006875C File Offset: 0x00066B5C
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
		}
		else
		{
			this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
		}
	}

	// Token: 0x04000B20 RID: 2848
	public Transform target;

	// Token: 0x04000B21 RID: 2849
	public float speed = 1f;

	// Token: 0x04000B22 RID: 2850
	private Transform mTrans;
}
