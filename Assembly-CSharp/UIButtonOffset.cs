using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x06000CD1 RID: 3281 RVA: 0x0006AA58 File Offset: 0x00068E58
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0006AAA5 File Offset: 0x00068EA5
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0006AAC4 File Offset: 0x00068EC4
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0006AB18 File Offset: 0x00068F18
	private void OnPress(bool isPressed)
	{
		this.mPressed = isPressed;
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0006ABAC File Offset: 0x00068FAC
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0006AC13 File Offset: 0x00069013
	private void OnDragOver()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos + this.hover).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0006AC4D File Offset: 0x0006904D
	private void OnDragOut()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0006AC7C File Offset: 0x0006907C
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000B7D RID: 2941
	public Transform tweenTarget;

	// Token: 0x04000B7E RID: 2942
	public Vector3 hover = Vector3.zero;

	// Token: 0x04000B7F RID: 2943
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x04000B80 RID: 2944
	public float duration = 0.2f;

	// Token: 0x04000B81 RID: 2945
	[NonSerialized]
	private Vector3 mPos;

	// Token: 0x04000B82 RID: 2946
	[NonSerialized]
	private bool mStarted;

	// Token: 0x04000B83 RID: 2947
	[NonSerialized]
	private bool mPressed;
}
