using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x17000228 RID: 552
	// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0008CE31 File Offset: 0x0008B231
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

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0008CE56 File Offset: 0x0008B256
	// (set) Token: 0x060011B2 RID: 4530 RVA: 0x0008CE63 File Offset: 0x0008B263
	public Vector3 value
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0008CE71 File Offset: 0x0008B271
	// (set) Token: 0x060011B4 RID: 4532 RVA: 0x0008CE79 File Offset: 0x0008B279
	[Obsolete("Use 'value' instead")]
	public Vector3 scale
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

	// Token: 0x060011B5 RID: 4533 RVA: 0x0008CE84 File Offset: 0x0008B284
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x0008CF0C File Offset: 0x0008B30C
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration, 0f);
		tweenScale.from = tweenScale.value;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x0008CF58 File Offset: 0x0008B358
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x0008CF66 File Offset: 0x0008B366
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x0008CF74 File Offset: 0x0008B374
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x0008CF82 File Offset: 0x0008B382
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000F30 RID: 3888
	public Vector3 from = Vector3.one;

	// Token: 0x04000F31 RID: 3889
	public Vector3 to = Vector3.one;

	// Token: 0x04000F32 RID: 3890
	public bool updateTable;

	// Token: 0x04000F33 RID: 3891
	private Transform mTrans;

	// Token: 0x04000F34 RID: 3892
	private UITable mTable;
}
