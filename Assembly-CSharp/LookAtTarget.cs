using System;
using UnityEngine;

// Token: 0x02000199 RID: 409
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x06000C70 RID: 3184 RVA: 0x0006814F File Offset: 0x0006654F
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x00068160 File Offset: 0x00066560
	private void LateUpdate()
	{
		if (this.target != null)
		{
			if (this.SnapTo)
			{
				base.transform.LookAt(this.target);
			}
			else
			{
				Vector3 forward = this.target.position - this.mTrans.position;
				float magnitude = forward.magnitude;
				if (magnitude > 0.001f)
				{
					Quaternion b = Quaternion.LookRotation(forward);
					this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, b, Mathf.Clamp01(this.speed * Time.deltaTime));
				}
			}
		}
	}

	// Token: 0x04000B0C RID: 2828
	public int level;

	// Token: 0x04000B0D RID: 2829
	public Transform target;

	// Token: 0x04000B0E RID: 2830
	public float speed = 8f;

	// Token: 0x04000B0F RID: 2831
	public bool SnapTo;

	// Token: 0x04000B10 RID: 2832
	private Transform mTrans;
}
