using System;
using UnityEngine;

// Token: 0x020001AA RID: 426
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x000694D3 File Offset: 0x000678D3
	// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x000694DB File Offset: 0x000678DB
	public UIButtonColor.State state
	{
		get
		{
			return this.mState;
		}
		set
		{
			this.SetState(value, false);
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000694E5 File Offset: 0x000678E5
	// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x00069500 File Offset: 0x00067900
	public Color defaultColor
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mDefaultColor;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			this.mDefaultColor = value;
			UIButtonColor.State state = this.mState;
			this.mState = UIButtonColor.State.Disabled;
			this.SetState(state, false);
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0006953B File Offset: 0x0006793B
	// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00069543 File Offset: 0x00067943
	public virtual bool isEnabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0006954C File Offset: 0x0006794C
	public void ResetDefaultColor()
	{
		this.defaultColor = this.mStartingColor;
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0006955A File Offset: 0x0006795A
	public void CacheDefaultColor()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0006956D File Offset: 0x0006796D
	private void Start()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
		if (!this.isEnabled)
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00069594 File Offset: 0x00067994
	protected virtual void OnInit()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null && !Application.isPlaying)
		{
			this.tweenTarget = base.gameObject;
		}
		if (this.tweenTarget != null)
		{
			this.mWidget = this.tweenTarget.GetComponent<UIWidget>();
		}
		if (this.mWidget != null)
		{
			this.mDefaultColor = this.mWidget.color;
			this.mStartingColor = this.mDefaultColor;
		}
		else if (this.tweenTarget != null)
		{
			Renderer component = this.tweenTarget.GetComponent<Renderer>();
			if (component != null)
			{
				this.mDefaultColor = ((!Application.isPlaying) ? component.sharedMaterial.color : component.material.color);
				this.mStartingColor = this.mDefaultColor;
			}
			else
			{
				Light component2 = this.tweenTarget.GetComponent<Light>();
				if (component2 != null)
				{
					this.mDefaultColor = component2.color;
					this.mStartingColor = this.mDefaultColor;
				}
				else
				{
					this.tweenTarget = null;
					this.mInitDone = false;
				}
			}
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x000696CC File Offset: 0x00067ACC
	protected virtual void OnEnable()
	{
		if (this.mInitDone)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.pressed == base.gameObject)
			{
				this.OnPress(true);
			}
			else if (UICamera.currentTouch.current == base.gameObject)
			{
				this.OnHover(true);
			}
		}
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00069748 File Offset: 0x00067B48
	protected virtual void OnDisable()
	{
		if (this.mInitDone && this.mState != UIButtonColor.State.Normal)
		{
			this.SetState(UIButtonColor.State.Normal, true);
			if (this.tweenTarget != null)
			{
				TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
				if (component != null)
				{
					component.value = this.mDefaultColor;
					component.enabled = false;
				}
			}
		}
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x000697B0 File Offset: 0x00067BB0
	protected virtual void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState((!isOver) ? UIButtonColor.State.Normal : UIButtonColor.State.Hover, false);
			}
		}
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00069800 File Offset: 0x00067C00
	protected virtual void OnPress(bool isPressed)
	{
		if (this.isEnabled && UICamera.currentTouch != null)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				if (isPressed)
				{
					this.SetState(UIButtonColor.State.Pressed, false);
				}
				else if (UICamera.currentTouch.current == base.gameObject)
				{
					if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
					{
						this.SetState(UIButtonColor.State.Hover, false);
					}
					else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == base.gameObject)
					{
						this.SetState(UIButtonColor.State.Hover, false);
					}
					else
					{
						this.SetState(UIButtonColor.State.Normal, false);
					}
				}
				else
				{
					this.SetState(UIButtonColor.State.Normal, false);
				}
			}
		}
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x000698CA File Offset: 0x00067CCA
	protected virtual void OnDragOver()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Pressed, false);
			}
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00069901 File Offset: 0x00067D01
	protected virtual void OnDragOut()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00069938 File Offset: 0x00067D38
	public virtual void SetState(UIButtonColor.State state, bool instant)
	{
		if (!this.mInitDone)
		{
			this.mInitDone = true;
			this.OnInit();
		}
		if (this.mState != state)
		{
			this.mState = state;
			this.UpdateColor(instant);
		}
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x0006996C File Offset: 0x00067D6C
	public void UpdateColor(bool instant)
	{
		if (this.tweenTarget != null)
		{
			TweenColor tweenColor;
			switch (this.mState)
			{
			case UIButtonColor.State.Hover:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.hover);
				break;
			case UIButtonColor.State.Pressed:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.pressed);
				break;
			case UIButtonColor.State.Disabled:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.disabledColor);
				break;
			default:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.mDefaultColor);
				break;
			}
			if (instant && tweenColor != null)
			{
				tweenColor.value = tweenColor.to;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x04000B5D RID: 2909
	public GameObject tweenTarget;

	// Token: 0x04000B5E RID: 2910
	public Color hover = new Color(0.882352948f, 0.784313738f, 0.5882353f, 1f);

	// Token: 0x04000B5F RID: 2911
	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.482352942f, 1f);

	// Token: 0x04000B60 RID: 2912
	public Color disabledColor = Color.grey;

	// Token: 0x04000B61 RID: 2913
	public float duration = 0.2f;

	// Token: 0x04000B62 RID: 2914
	[NonSerialized]
	protected Color mStartingColor;

	// Token: 0x04000B63 RID: 2915
	[NonSerialized]
	protected Color mDefaultColor;

	// Token: 0x04000B64 RID: 2916
	[NonSerialized]
	protected bool mInitDone;

	// Token: 0x04000B65 RID: 2917
	[NonSerialized]
	protected UIWidget mWidget;

	// Token: 0x04000B66 RID: 2918
	[NonSerialized]
	protected UIButtonColor.State mState;

	// Token: 0x020001AB RID: 427
	public enum State
	{
		// Token: 0x04000B68 RID: 2920
		Normal,
		// Token: 0x04000B69 RID: 2921
		Hover,
		// Token: 0x04000B6A RID: 2922
		Pressed,
		// Token: 0x04000B6B RID: 2923
		Disabled
	}
}
