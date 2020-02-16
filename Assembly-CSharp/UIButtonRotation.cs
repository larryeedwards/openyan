using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x0006ACCC File Offset: 0x000690CC
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0006AD19 File Offset: 0x00069119
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0006AD38 File Offset: 0x00069138
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0006AD8C File Offset: 0x0006918C
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0006AE24 File Offset: 0x00069224
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0006AE90 File Offset: 0x00069290
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04000B84 RID: 2948
	public Transform tweenTarget;

	// Token: 0x04000B85 RID: 2949
	public Vector3 hover = Vector3.zero;

	// Token: 0x04000B86 RID: 2950
	public Vector3 pressed = Vector3.zero;

	// Token: 0x04000B87 RID: 2951
	public float duration = 0.2f;

	// Token: 0x04000B88 RID: 2952
	private Quaternion mRot;

	// Token: 0x04000B89 RID: 2953
	private bool mStarted;
}
