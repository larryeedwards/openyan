using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020001CB RID: 459
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0006E2B6 File Offset: 0x0006C6B6
	private bool dualState
	{
		get
		{
			return this.trigger == Trigger.OnPress || this.trigger == Trigger.OnHover;
		}
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x0006E2D0 File Offset: 0x0006C6D0
	private void Awake()
	{
		UIButton component = base.GetComponent<UIButton>();
		if (component != null)
		{
			this.dragHighlight = component.dragHighlight;
		}
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0006E32C File Offset: 0x0006C72C
	private void Start()
	{
		this.mStarted = true;
		if (this.target == null && this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (this.animator != null)
		{
			if (this.animator.enabled)
			{
				this.animator.enabled = false;
			}
			return;
		}
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null && this.target.enabled)
		{
			this.target.enabled = false;
		}
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0006E3E8 File Offset: 0x0006C7E8
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (this.trigger == Trigger.OnPress || this.trigger == Trigger.OnPressTrue)
			{
				this.mActivated = (UICamera.currentTouch.pressed == base.gameObject);
			}
			if (this.trigger == Trigger.OnHover || this.trigger == Trigger.OnHoverTrue)
			{
				this.mActivated = (UICamera.currentTouch.current == base.gameObject);
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0006E4AC File Offset: 0x0006C8AC
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x0006E4E4 File Offset: 0x0006C8E4
	private void OnHover(bool isOver)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
		{
			this.Play(isOver, this.dualState);
		}
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0006E53C File Offset: 0x0006C93C
	private void OnPress(bool isPressed)
	{
		if (!base.enabled)
		{
			return;
		}
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed))
		{
			this.Play(isPressed, this.dualState);
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0006E5AB File Offset: 0x0006C9AB
	private void OnClick()
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x0006E5E4 File Offset: 0x0006C9E4
	private void OnDoubleClick()
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0006E620 File Offset: 0x0006CA20
	private void OnSelect(bool isSelected)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected))
		{
			this.Play(isSelected, this.dualState);
		}
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0006E67C File Offset: 0x0006CA7C
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value, this.dualState);
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0006E700 File Offset: 0x0006CB00
	private void OnDragOver()
	{
		if (base.enabled && this.dualState)
		{
			if (UICamera.currentTouch.dragged == base.gameObject)
			{
				this.Play(true, true);
			}
			else if (this.dragHighlight && this.trigger == Trigger.OnPress)
			{
				this.Play(true, true);
			}
		}
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0006E769 File Offset: 0x0006CB69
	private void OnDragOut()
	{
		if (base.enabled && this.dualState && UICamera.hoveredObject != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0006E79E File Offset: 0x0006CB9E
	private void OnDrop(GameObject go)
	{
		if (base.enabled && this.trigger == Trigger.OnPress && UICamera.currentTouch.dragged != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0006E7D9 File Offset: 0x0006CBD9
	public void Play(bool forward)
	{
		this.Play(forward, true);
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0006E7E4 File Offset: 0x0006CBE4
	public void Play(bool forward, bool onlyIfDifferent)
	{
		if (this.target || this.animator)
		{
			if (onlyIfDifferent)
			{
				if (this.mActivated == forward)
				{
					return;
				}
				this.mActivated = forward;
			}
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			AnimationOrTween.Direction direction = (AnimationOrTween.Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = (!this.target) ? ActiveAnimation.Play(this.animator, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished) : ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation != null)
			{
				if (this.resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < this.onFinished.Count; i++)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
			}
		}
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0006E910 File Offset: 0x0006CD10
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x0006E919 File Offset: 0x0006CD19
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x0006E924 File Offset: 0x0006CD24
	private void OnFinished()
	{
		if (UIPlayAnimation.current == null)
		{
			UIPlayAnimation.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
			}
			this.eventReceiver = null;
			UIPlayAnimation.current = null;
		}
	}

	// Token: 0x04000C43 RID: 3139
	public static UIPlayAnimation current;

	// Token: 0x04000C44 RID: 3140
	public Animation target;

	// Token: 0x04000C45 RID: 3141
	public Animator animator;

	// Token: 0x04000C46 RID: 3142
	public string clipName;

	// Token: 0x04000C47 RID: 3143
	public Trigger trigger;

	// Token: 0x04000C48 RID: 3144
	public AnimationOrTween.Direction playDirection = AnimationOrTween.Direction.Forward;

	// Token: 0x04000C49 RID: 3145
	public bool resetOnPlay;

	// Token: 0x04000C4A RID: 3146
	public bool clearSelection;

	// Token: 0x04000C4B RID: 3147
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04000C4C RID: 3148
	public DisableCondition disableWhenFinished;

	// Token: 0x04000C4D RID: 3149
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04000C4E RID: 3150
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000C4F RID: 3151
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x04000C50 RID: 3152
	private bool mStarted;

	// Token: 0x04000C51 RID: 3153
	private bool mActivated;

	// Token: 0x04000C52 RID: 3154
	private bool dragHighlight;
}
