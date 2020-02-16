using System;
using UnityEngine;

// Token: 0x0200023B RID: 571
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06001196 RID: 4502 RVA: 0x0008CA50 File Offset: 0x0008AE50
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

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06001197 RID: 4503 RVA: 0x0008CA75 File Offset: 0x0008AE75
	// (set) Token: 0x06001198 RID: 4504 RVA: 0x0008CA7D File Offset: 0x0008AE7D
	[Obsolete("Use 'value' instead")]
	public Vector3 position
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

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06001199 RID: 4505 RVA: 0x0008CA86 File Offset: 0x0008AE86
	// (set) Token: 0x0600119A RID: 4506 RVA: 0x0008CAB0 File Offset: 0x0008AEB0
	public Vector3 value
	{
		get
		{
			return (!this.worldSpace) ? this.cachedTransform.localPosition : this.cachedTransform.position;
		}
		set
		{
			if (this.mRect == null || !this.mRect.isAnchored || this.worldSpace)
			{
				if (this.worldSpace)
				{
					this.cachedTransform.position = value;
				}
				else
				{
					this.cachedTransform.localPosition = value;
				}
			}
			else
			{
				value -= this.cachedTransform.localPosition;
				NGUIMath.MoveRect(this.mRect, value.x, value.y);
			}
		}
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0008CB42 File Offset: 0x0008AF42
	private void Awake()
	{
		this.mRect = base.GetComponent<UIRect>();
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x0008CB50 File Offset: 0x0008AF50
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x0008CB7C File Offset: 0x0008AF7C
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration, 0f);
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0008CBC8 File Offset: 0x0008AFC8
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos, bool worldSpace)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration, 0f);
		tweenPosition.worldSpace = worldSpace;
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x0008CC1B File Offset: 0x0008B01B
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x0008CC29 File Offset: 0x0008B029
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0008CC37 File Offset: 0x0008B037
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x0008CC45 File Offset: 0x0008B045
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000F27 RID: 3879
	public Vector3 from;

	// Token: 0x04000F28 RID: 3880
	public Vector3 to;

	// Token: 0x04000F29 RID: 3881
	[HideInInspector]
	public bool worldSpace;

	// Token: 0x04000F2A RID: 3882
	private Transform mTrans;

	// Token: 0x04000F2B RID: 3883
	private UIRect mRect;
}
