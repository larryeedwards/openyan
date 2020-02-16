using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x020001E7 RID: 487
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00074CF7 File Offset: 0x000730F7
	// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00074D18 File Offset: 0x00073118
	public bool value
	{
		get
		{
			return (!this.mStarted) ? this.startsActive : this.mIsActive;
		}
		set
		{
			if (!this.mStarted)
			{
				this.startsActive = value;
			}
			else if (this.group == 0 || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value, true);
			}
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00074D6C File Offset: 0x0007316C
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00074DAF File Offset: 0x000731AF
	// (set) Token: 0x06000E70 RID: 3696 RVA: 0x00074DB7 File Offset: 0x000731B7
	[Obsolete("Use 'value' instead")]
	public bool isChecked
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

	// Token: 0x06000E71 RID: 3697 RVA: 0x00074DC0 File Offset: 0x000731C0
	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < UIToggle.list.size; i++)
		{
			UIToggle uitoggle = UIToggle.list[i];
			if (uitoggle != null && uitoggle.group == group && uitoggle.mIsActive)
			{
				return uitoggle;
			}
		}
		return null;
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00074E1A File Offset: 0x0007321A
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x00074E27 File Offset: 0x00073227
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x00074E38 File Offset: 0x00073238
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.startsChecked)
		{
			this.startsChecked = false;
			this.startsActive = true;
		}
		if (!Application.isPlaying)
		{
			if (this.checkSprite != null && this.activeSprite == null)
			{
				this.activeSprite = this.checkSprite;
				this.checkSprite = null;
			}
			if (this.checkAnimation != null && this.activeAnimation == null)
			{
				this.activeAnimation = this.checkAnimation;
				this.checkAnimation = null;
			}
			if (Application.isPlaying && this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!this.invertSpriteState) ? ((!this.startsActive) ? 0f : 1f) : ((!this.startsActive) ? 1f : 0f));
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				this.eventReceiver = null;
				this.functionName = null;
			}
		}
		else
		{
			this.mIsActive = !this.startsActive;
			this.mStarted = true;
			bool flag = this.instantTween;
			this.instantTween = true;
			this.Set(this.startsActive, true);
			this.instantTween = flag;
		}
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x00074FA3 File Offset: 0x000733A3
	private void OnClick()
	{
		if (base.enabled && this.isColliderEnabled && UICamera.currentTouchID != -2)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x00074FD8 File Offset: 0x000733D8
	public void Set(bool state, bool notify = true)
	{
		if (this.validator != null && !this.validator(state))
		{
			return;
		}
		if (!this.mStarted)
		{
			this.mIsActive = state;
			this.startsActive = state;
			if (this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!this.invertSpriteState) ? ((!state) ? 0f : 1f) : ((!state) ? 1f : 0f));
			}
		}
		else if (this.mIsActive != state)
		{
			if (this.group != 0 && state)
			{
				int i = 0;
				int size = UIToggle.list.size;
				while (i < size)
				{
					UIToggle uitoggle = UIToggle.list[i];
					if (uitoggle != this && uitoggle.group == this.group)
					{
						uitoggle.Set(false, true);
					}
					if (UIToggle.list.size != size)
					{
						size = UIToggle.list.size;
						i = 0;
					}
					else
					{
						i++;
					}
				}
			}
			this.mIsActive = state;
			if (this.activeSprite != null)
			{
				if (this.instantTween || !NGUITools.GetActive(this))
				{
					this.activeSprite.alpha = ((!this.invertSpriteState) ? ((!this.mIsActive) ? 0f : 1f) : ((!this.mIsActive) ? 1f : 0f));
				}
				else
				{
					TweenAlpha.Begin(this.activeSprite.gameObject, 0.15f, (!this.invertSpriteState) ? ((!this.mIsActive) ? 0f : 1f) : ((!this.mIsActive) ? 1f : 0f), 0f);
				}
			}
			if (notify && UIToggle.current == null)
			{
				UIToggle uitoggle2 = UIToggle.current;
				UIToggle.current = this;
				if (EventDelegate.IsValid(this.onChange))
				{
					EventDelegate.Execute(this.onChange);
				}
				else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
				{
					this.eventReceiver.SendMessage(this.functionName, this.mIsActive, SendMessageOptions.DontRequireReceiver);
				}
				UIToggle.current = uitoggle2;
			}
			if (this.animator != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animator, null, (!state) ? AnimationOrTween.Direction.Reverse : AnimationOrTween.Direction.Forward, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
				if (activeAnimation != null && (this.instantTween || !NGUITools.GetActive(this)))
				{
					activeAnimation.Finish();
				}
			}
			else if (this.activeAnimation != null)
			{
				ActiveAnimation activeAnimation2 = ActiveAnimation.Play(this.activeAnimation, null, (!state) ? AnimationOrTween.Direction.Reverse : AnimationOrTween.Direction.Forward, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
				if (activeAnimation2 != null && (this.instantTween || !NGUITools.GetActive(this)))
				{
					activeAnimation2.Finish();
				}
			}
			else if (this.tween != null)
			{
				bool active = NGUITools.GetActive(this);
				if (this.tween.tweenGroup != 0)
				{
					UITweener[] componentsInChildren = this.tween.GetComponentsInChildren<UITweener>(true);
					int j = 0;
					int num = componentsInChildren.Length;
					while (j < num)
					{
						UITweener uitweener = componentsInChildren[j];
						if (uitweener.tweenGroup == this.tween.tweenGroup)
						{
							uitweener.Play(state);
							if (this.instantTween || !active)
							{
								uitweener.tweenFactor = ((!state) ? 0f : 1f);
							}
						}
						j++;
					}
				}
				else
				{
					this.tween.Play(state);
					if (this.instantTween || !active)
					{
						this.tween.tweenFactor = ((!state) ? 0f : 1f);
					}
				}
			}
		}
	}

	// Token: 0x04000D28 RID: 3368
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x04000D29 RID: 3369
	public static UIToggle current;

	// Token: 0x04000D2A RID: 3370
	public int group;

	// Token: 0x04000D2B RID: 3371
	public UIWidget activeSprite;

	// Token: 0x04000D2C RID: 3372
	public bool invertSpriteState;

	// Token: 0x04000D2D RID: 3373
	public Animation activeAnimation;

	// Token: 0x04000D2E RID: 3374
	public Animator animator;

	// Token: 0x04000D2F RID: 3375
	public UITweener tween;

	// Token: 0x04000D30 RID: 3376
	public bool startsActive;

	// Token: 0x04000D31 RID: 3377
	public bool instantTween;

	// Token: 0x04000D32 RID: 3378
	public bool optionCanBeNone;

	// Token: 0x04000D33 RID: 3379
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04000D34 RID: 3380
	public UIToggle.Validate validator;

	// Token: 0x04000D35 RID: 3381
	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	// Token: 0x04000D36 RID: 3382
	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	// Token: 0x04000D37 RID: 3383
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000D38 RID: 3384
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	// Token: 0x04000D39 RID: 3385
	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	// Token: 0x04000D3A RID: 3386
	private bool mIsActive = true;

	// Token: 0x04000D3B RID: 3387
	private bool mStarted;

	// Token: 0x020001E8 RID: 488
	// (Invoke) Token: 0x06000E79 RID: 3705
	public delegate bool Validate(bool choice);
}
