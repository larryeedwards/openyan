using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class LagPosition : MonoBehaviour
{
	// Token: 0x06000C61 RID: 3169 RVA: 0x00067EBC File Offset: 0x000662BC
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00067ECC File Offset: 0x000662CC
	private void Interpolate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(delta * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(delta * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(delta * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00067FBB File Offset: 0x000663BB
	private void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00067FC9 File Offset: 0x000663C9
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.ResetPosition();
		}
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00067FDC File Offset: 0x000663DC
	private void Start()
	{
		this.mStarted = true;
		this.ResetPosition();
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00067FEB File Offset: 0x000663EB
	public void ResetPosition()
	{
		this.mAbsolute = this.mTrans.position;
		this.mRelative = this.mTrans.localPosition;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0006800F File Offset: 0x0006640F
	private void Update()
	{
		this.Interpolate((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
	}

	// Token: 0x04000B00 RID: 2816
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x04000B01 RID: 2817
	public bool ignoreTimeScale;

	// Token: 0x04000B02 RID: 2818
	private Transform mTrans;

	// Token: 0x04000B03 RID: 2819
	private Vector3 mRelative;

	// Token: 0x04000B04 RID: 2820
	private Vector3 mAbsolute;

	// Token: 0x04000B05 RID: 2821
	private bool mStarted;
}
