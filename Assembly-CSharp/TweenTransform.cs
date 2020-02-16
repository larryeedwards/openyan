using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x060011BC RID: 4540 RVA: 0x0008CF98 File Offset: 0x0008B398
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x0008D15D File Offset: 0x0008B55D
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0008D168 File Offset: 0x0008B568
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration, 0f);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x04000F35 RID: 3893
	public Transform from;

	// Token: 0x04000F36 RID: 3894
	public Transform to;

	// Token: 0x04000F37 RID: 3895
	public bool parentWhenFinished;

	// Token: 0x04000F38 RID: 3896
	private Transform mTrans;

	// Token: 0x04000F39 RID: 3897
	private Vector3 mPos;

	// Token: 0x04000F3A RID: 3898
	private Quaternion mRot;

	// Token: 0x04000F3B RID: 3899
	private Vector3 mScale;
}
