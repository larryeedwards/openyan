using System;
using UnityEngine;

// Token: 0x02000197 RID: 407
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x00068044 File Offset: 0x00066444
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00068054 File Offset: 0x00066454
	private void Interpolate(float delta)
	{
		if (this.mTrans != null)
		{
			Transform parent = this.mTrans.parent;
			if (parent != null)
			{
				this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
				this.mTrans.rotation = this.mAbsolute;
			}
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x000680C5 File Offset: 0x000664C5
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x000680F5 File Offset: 0x000664F5
	private void Update()
	{
		this.Interpolate((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
	}

	// Token: 0x04000B06 RID: 2822
	public float speed = 10f;

	// Token: 0x04000B07 RID: 2823
	public bool ignoreTimeScale;

	// Token: 0x04000B08 RID: 2824
	private Transform mTrans;

	// Token: 0x04000B09 RID: 2825
	private Quaternion mRelative;

	// Token: 0x04000B0A RID: 2826
	private Quaternion mAbsolute;
}
