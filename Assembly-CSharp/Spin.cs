using System;
using UnityEngine;

// Token: 0x0200019E RID: 414
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x06000C7D RID: 3197 RVA: 0x00068659 File Offset: 0x00066A59
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00068673 File Offset: 0x00066A73
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
		}
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x000686A6 File Offset: 0x00066AA6
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x000686C4 File Offset: 0x00066AC4
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion rhs = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * rhs;
		}
		else
		{
			this.mRb.MoveRotation(this.mRb.rotation * rhs);
		}
	}

	// Token: 0x04000B1C RID: 2844
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x04000B1D RID: 2845
	public bool ignoreTimeScale;

	// Token: 0x04000B1E RID: 2846
	private Rigidbody mRb;

	// Token: 0x04000B1F RID: 2847
	private Transform mTrans;
}
