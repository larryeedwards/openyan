using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001A8 RID: 424
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00069A58 File Offset: 0x00067E58
	// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x00069AB4 File Offset: 0x00067EB4
	public override bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider component = base.gameObject.GetComponent<Collider>();
			if (component && component.enabled)
			{
				return true;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 && component2.enabled;
		}
		set
		{
			if (this.isEnabled != value)
			{
				Collider component = base.gameObject.GetComponent<Collider>();
				if (component != null)
				{
					component.enabled = value;
					UIButton[] components = base.GetComponents<UIButton>();
					foreach (UIButton uibutton in components)
					{
						uibutton.SetState((!value) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal, false);
					}
				}
				else
				{
					Collider2D component2 = base.GetComponent<Collider2D>();
					if (component2 != null)
					{
						component2.enabled = value;
						UIButton[] components2 = base.GetComponents<UIButton>();
						foreach (UIButton uibutton2 in components2)
						{
							uibutton2.SetState((!value) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal, false);
						}
					}
					else
					{
						base.enabled = value;
					}
				}
			}
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00069B95 File Offset: 0x00067F95
	// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x00069BB0 File Offset: 0x00067FB0
	public string normalSprite
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.mSprite != null && !string.IsNullOrEmpty(this.mNormalSprite) && this.mNormalSprite == this.mSprite.spriteName)
			{
				this.mNormalSprite = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite);
			}
			else
			{
				this.mNormalSprite = value;
				if (this.mState == UIButtonColor.State.Normal)
				{
					this.SetSprite(value);
				}
			}
		}
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00069C41 File Offset: 0x00068041
	// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00069C5C File Offset: 0x0006805C
	public Sprite normalSprite2D
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite2D;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.mSprite2D != null && this.mNormalSprite2D == this.mSprite2D.sprite2D)
			{
				this.mNormalSprite2D = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite);
			}
			else
			{
				this.mNormalSprite2D = value;
				if (this.mState == UIButtonColor.State.Normal)
				{
					this.SetSprite(value);
				}
			}
		}
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00069CE0 File Offset: 0x000680E0
	protected override void OnInit()
	{
		base.OnInit();
		this.mSprite = (this.mWidget as UISprite);
		this.mSprite2D = (this.mWidget as UI2DSprite);
		if (this.mSprite != null)
		{
			this.mNormalSprite = this.mSprite.spriteName;
		}
		if (this.mSprite2D != null)
		{
			this.mNormalSprite2D = this.mSprite2D.sprite2D;
		}
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00069D59 File Offset: 0x00068159
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			if (this.mInitDone)
			{
				this.OnHover(UICamera.hoveredObject == base.gameObject);
			}
		}
		else
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00069D94 File Offset: 0x00068194
	protected override void OnDragOver()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00069DCC File Offset: 0x000681CC
	protected override void OnDragOut()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00069E04 File Offset: 0x00068204
	protected virtual void OnClick()
	{
		if (UIButton.current == null && this.isEnabled && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00069E5C File Offset: 0x0006825C
	public override void SetState(UIButtonColor.State state, bool immediate)
	{
		base.SetState(state, immediate);
		if (this.mSprite != null)
		{
			switch (state)
			{
			case UIButtonColor.State.Normal:
				this.SetSprite(this.mNormalSprite);
				break;
			case UIButtonColor.State.Hover:
				this.SetSprite((!string.IsNullOrEmpty(this.hoverSprite)) ? this.hoverSprite : this.mNormalSprite);
				break;
			case UIButtonColor.State.Pressed:
				this.SetSprite(this.pressedSprite);
				break;
			case UIButtonColor.State.Disabled:
				this.SetSprite(this.disabledSprite);
				break;
			}
		}
		else if (this.mSprite2D != null)
		{
			switch (state)
			{
			case UIButtonColor.State.Normal:
				this.SetSprite(this.mNormalSprite2D);
				break;
			case UIButtonColor.State.Hover:
				this.SetSprite((!(this.hoverSprite2D == null)) ? this.hoverSprite2D : this.mNormalSprite2D);
				break;
			case UIButtonColor.State.Pressed:
				this.SetSprite(this.pressedSprite2D);
				break;
			case UIButtonColor.State.Disabled:
				this.SetSprite(this.disabledSprite2D);
				break;
			}
		}
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00069F90 File Offset: 0x00068390
	protected void SetSprite(string sp)
	{
		if (this.mSprite != null && !string.IsNullOrEmpty(sp) && this.mSprite.spriteName != sp)
		{
			this.mSprite.spriteName = sp;
			if (this.pixelSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00069FF4 File Offset: 0x000683F4
	protected void SetSprite(Sprite sp)
	{
		if (sp != null && this.mSprite2D != null && this.mSprite2D.sprite2D != sp)
		{
			this.mSprite2D.sprite2D = sp;
			if (this.pixelSnap)
			{
				this.mSprite2D.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04000B4D RID: 2893
	public static UIButton current;

	// Token: 0x04000B4E RID: 2894
	public bool dragHighlight;

	// Token: 0x04000B4F RID: 2895
	public string hoverSprite;

	// Token: 0x04000B50 RID: 2896
	public string pressedSprite;

	// Token: 0x04000B51 RID: 2897
	public string disabledSprite;

	// Token: 0x04000B52 RID: 2898
	public Sprite hoverSprite2D;

	// Token: 0x04000B53 RID: 2899
	public Sprite pressedSprite2D;

	// Token: 0x04000B54 RID: 2900
	public Sprite disabledSprite2D;

	// Token: 0x04000B55 RID: 2901
	public bool pixelSnap;

	// Token: 0x04000B56 RID: 2902
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04000B57 RID: 2903
	[NonSerialized]
	private UISprite mSprite;

	// Token: 0x04000B58 RID: 2904
	[NonSerialized]
	private UI2DSprite mSprite2D;

	// Token: 0x04000B59 RID: 2905
	[NonSerialized]
	private string mNormalSprite;

	// Token: 0x04000B5A RID: 2906
	[NonSerialized]
	private Sprite mNormalSprite2D;
}
