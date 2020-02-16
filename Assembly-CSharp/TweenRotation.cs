using System;
using UnityEngine;

// Token: 0x0200023C RID: 572
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x17000225 RID: 549
	// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0008CC5B File Offset: 0x0008B05B
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0008CC80 File Offset: 0x0008B080
	// (set) Token: 0x060011A6 RID: 4518 RVA: 0x0008CC88 File Offset: 0x0008B088
	[Obsolete("Use 'value' instead")]
	public Quaternion rotation
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0008CC91 File Offset: 0x0008B091
	// (set) Token: 0x060011A8 RID: 4520 RVA: 0x0008CC9E File Offset: 0x0008B09E
	public Quaternion value
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x0008CCAC File Offset: 0x0008B0AC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = ((!this.quaternionLerp) ? Quaternion.Euler(new Vector3(Mathf.Lerp(this.from.x, this.to.x, factor), Mathf.Lerp(this.from.y, this.to.y, factor), Mathf.Lerp(this.from.z, this.to.z, factor))) : Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor));
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x0008CD4C File Offset: 0x0008B14C
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration, 0f);
		tweenRotation.from = tweenRotation.value.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x0008CDA8 File Offset: 0x0008B1A8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x0008CDCC File Offset: 0x0008B1CC
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x0008CDED File Offset: 0x0008B1ED
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x0008CE00 File Offset: 0x0008B200
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}

	// Token: 0x04000F2C RID: 3884
	public Vector3 from;

	// Token: 0x04000F2D RID: 3885
	public Vector3 to;

	// Token: 0x04000F2E RID: 3886
	public bool quaternionLerp;

	// Token: 0x04000F2F RID: 3887
	private Transform mTrans;
}
