using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CF RID: 463
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Popup List")]
public class UIPopupList : UIWidgetContainer
{
	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0006F3A6 File Offset: 0x0006D7A6
	// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0006F3E0 File Offset: 0x0006D7E0
	public UnityEngine.Object ambigiousFont
	{
		get
		{
			if (this.trueTypeFont != null)
			{
				return this.trueTypeFont;
			}
			if (this.bitmapFont != null)
			{
				return this.bitmapFont;
			}
			return this.font;
		}
		set
		{
			if (value is Font)
			{
				this.trueTypeFont = (value as Font);
				this.bitmapFont = null;
				this.font = null;
			}
			else if (value is UIFont)
			{
				this.bitmapFont = (value as UIFont);
				this.trueTypeFont = null;
				this.font = null;
			}
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0006F43C File Offset: 0x0006D83C
	// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0006F444 File Offset: 0x0006D844
	[Obsolete("Use EventDelegate.Add(popup.onChange, YourCallback) instead, and UIPopupList.current.value to determine the state")]
	public UIPopupList.LegacyEvent onSelectionChange
	{
		get
		{
			return this.mLegacyEvent;
		}
		set
		{
			this.mLegacyEvent = value;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0006F44D File Offset: 0x0006D84D
	public static bool isOpen
	{
		get
		{
			return UIPopupList.current != null && (UIPopupList.mChild != null || UIPopupList.mFadeOutComplete > Time.unscaledTime);
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0006F481 File Offset: 0x0006D881
	// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0006F489 File Offset: 0x0006D889
	public virtual string value
	{
		get
		{
			return this.mSelectedItem;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0006F494 File Offset: 0x0006D894
	public virtual object data
	{
		get
		{
			int num = this.items.IndexOf(this.mSelectedItem);
			return (num <= -1 || num >= this.itemData.Count) ? null : this.itemData[num];
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0006F4E0 File Offset: 0x0006D8E0
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

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0006F523 File Offset: 0x0006D923
	protected bool isValid
	{
		get
		{
			return this.bitmapFont != null || this.trueTypeFont != null;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0006F545 File Offset: 0x0006D945
	protected int activeFontSize
	{
		get
		{
			return (!(this.trueTypeFont != null) && !(this.bitmapFont == null)) ? this.bitmapFont.defaultSize : this.fontSize;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0006F580 File Offset: 0x0006D980
	protected float activeFontScale
	{
		get
		{
			return (!(this.trueTypeFont != null) && !(this.bitmapFont == null)) ? ((float)this.fontSize / (float)this.bitmapFont.defaultSize) : 1f;
		}
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x0006F5D0 File Offset: 0x0006D9D0
	public void Set(string value, bool notify = true)
	{
		if (this.mSelectedItem != value)
		{
			this.mSelectedItem = value;
			if (this.mSelectedItem == null)
			{
				return;
			}
			if (notify && this.mSelectedItem != null)
			{
				this.TriggerCallbacks();
			}
			if (!this.keepValue)
			{
				this.mSelectedItem = null;
			}
		}
	}

	// Token: 0x06000DCF RID: 3535 RVA: 0x0006F62A File Offset: 0x0006DA2A
	public virtual void Clear()
	{
		this.items.Clear();
		this.itemData.Clear();
	}

	// Token: 0x06000DD0 RID: 3536 RVA: 0x0006F642 File Offset: 0x0006DA42
	public virtual void AddItem(string text)
	{
		this.items.Add(text);
		this.itemData.Add(text);
	}

	// Token: 0x06000DD1 RID: 3537 RVA: 0x0006F65C File Offset: 0x0006DA5C
	public virtual void AddItem(string text, object data)
	{
		this.items.Add(text);
		this.itemData.Add(data);
	}

	// Token: 0x06000DD2 RID: 3538 RVA: 0x0006F678 File Offset: 0x0006DA78
	public virtual void RemoveItem(string text)
	{
		int num = this.items.IndexOf(text);
		if (num != -1)
		{
			this.items.RemoveAt(num);
			this.itemData.RemoveAt(num);
		}
	}

	// Token: 0x06000DD3 RID: 3539 RVA: 0x0006F6B4 File Offset: 0x0006DAB4
	public virtual void RemoveItemByData(object data)
	{
		int num = this.itemData.IndexOf(data);
		if (num != -1)
		{
			this.items.RemoveAt(num);
			this.itemData.RemoveAt(num);
		}
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x0006F6F0 File Offset: 0x0006DAF0
	protected void TriggerCallbacks()
	{
		if (!this.mExecuting)
		{
			this.mExecuting = true;
			UIPopupList uipopupList = UIPopupList.current;
			UIPopupList.current = this;
			if (this.mLegacyEvent != null)
			{
				this.mLegacyEvent(this.mSelectedItem);
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				EventDelegate.Execute(this.onChange);
			}
			else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
			{
				this.eventReceiver.SendMessage(this.functionName, this.mSelectedItem, SendMessageOptions.DontRequireReceiver);
			}
			UIPopupList.current = uipopupList;
			this.mExecuting = false;
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x0006F7A0 File Offset: 0x0006DBA0
	protected virtual void OnEnable()
	{
		if (EventDelegate.IsValid(this.onChange))
		{
			this.eventReceiver = null;
			this.functionName = null;
		}
		if (this.font != null)
		{
			if (this.font.isDynamic)
			{
				this.trueTypeFont = this.font.dynamicFont;
				this.fontStyle = this.font.dynamicFontStyle;
				this.mUseDynamicFont = true;
			}
			else if (this.bitmapFont == null)
			{
				this.bitmapFont = this.font;
				this.mUseDynamicFont = false;
			}
			this.font = null;
		}
		if (this.textScale != 0f)
		{
			this.fontSize = ((!(this.bitmapFont != null)) ? 16 : Mathf.RoundToInt((float)this.bitmapFont.defaultSize * this.textScale));
			this.textScale = 0f;
		}
		if (this.trueTypeFont == null && this.bitmapFont != null && this.bitmapFont.isDynamic)
		{
			this.trueTypeFont = this.bitmapFont.dynamicFont;
			this.bitmapFont = null;
		}
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x0006F8E0 File Offset: 0x0006DCE0
	protected virtual void OnValidate()
	{
		Font x = this.trueTypeFont;
		UIFont uifont = this.bitmapFont;
		this.bitmapFont = null;
		this.trueTypeFont = null;
		if (x != null && (uifont == null || !this.mUseDynamicFont))
		{
			this.bitmapFont = null;
			this.trueTypeFont = x;
			this.mUseDynamicFont = true;
		}
		else if (uifont != null)
		{
			if (uifont.isDynamic)
			{
				this.trueTypeFont = uifont.dynamicFont;
				this.fontStyle = uifont.dynamicFontStyle;
				this.fontSize = uifont.defaultSize;
				this.mUseDynamicFont = true;
			}
			else
			{
				this.bitmapFont = uifont;
				this.mUseDynamicFont = false;
			}
		}
		else
		{
			this.trueTypeFont = x;
			this.mUseDynamicFont = true;
		}
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0006F9B0 File Offset: 0x0006DDB0
	public virtual void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		this.mStarted = true;
		if (this.keepValue)
		{
			string value = this.mSelectedItem;
			this.mSelectedItem = null;
			this.value = value;
		}
		else
		{
			this.mSelectedItem = null;
		}
		if (this.textLabel != null)
		{
			EventDelegate.Add(this.onChange, new EventDelegate.Callback(this.textLabel.SetCurrentSelection));
			this.textLabel = null;
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0006FA31 File Offset: 0x0006DE31
	protected virtual void OnLocalize()
	{
		if (this.isLocalized)
		{
			this.TriggerCallbacks();
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0006FA44 File Offset: 0x0006DE44
	protected virtual void Highlight(UILabel lbl, bool instant)
	{
		if (this.mHighlight != null)
		{
			this.mHighlightedLabel = lbl;
			Vector3 highlightPosition = this.GetHighlightPosition();
			if (!instant && this.isAnimated)
			{
				TweenPosition.Begin(this.mHighlight.gameObject, 0.1f, highlightPosition).method = UITweener.Method.EaseOut;
				if (!this.mTweening)
				{
					this.mTweening = true;
					base.StartCoroutine("UpdateTweenPosition");
				}
			}
			else
			{
				this.mHighlight.cachedTransform.localPosition = highlightPosition;
			}
		}
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0006FAD4 File Offset: 0x0006DED4
	protected virtual Vector3 GetHighlightPosition()
	{
		if (this.mHighlightedLabel == null || this.mHighlight == null)
		{
			return Vector3.zero;
		}
		Vector4 border = this.mHighlight.border;
		float num = (!(this.atlas != null)) ? 1f : this.atlas.pixelSize;
		float num2 = border.x * num;
		float y = border.w * num;
		return this.mHighlightedLabel.cachedTransform.localPosition + new Vector3(-num2, y, 1f);
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0006FB74 File Offset: 0x0006DF74
	protected virtual IEnumerator UpdateTweenPosition()
	{
		if (this.mHighlight != null && this.mHighlightedLabel != null)
		{
			TweenPosition tp = this.mHighlight.GetComponent<TweenPosition>();
			while (tp != null && tp.enabled)
			{
				tp.to = this.GetHighlightPosition();
				yield return null;
			}
		}
		this.mTweening = false;
		yield break;
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x0006FB90 File Offset: 0x0006DF90
	protected virtual void OnItemHover(GameObject go, bool isOver)
	{
		if (isOver)
		{
			UILabel component = go.GetComponent<UILabel>();
			this.Highlight(component, false);
		}
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0006FBB2 File Offset: 0x0006DFB2
	protected virtual void OnItemPress(GameObject go, bool isPressed)
	{
		if (isPressed && this.selection == UIPopupList.Selection.OnPress)
		{
			this.OnItemClick(go);
		}
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0006FBCC File Offset: 0x0006DFCC
	protected virtual void OnItemClick(GameObject go)
	{
		this.Select(go.GetComponent<UILabel>(), true);
		UIEventListener component = go.GetComponent<UIEventListener>();
		this.value = (component.parameter as string);
		UIPlaySound[] components = base.GetComponents<UIPlaySound>();
		int i = 0;
		int num = components.Length;
		while (i < num)
		{
			UIPlaySound uiplaySound = components[i];
			if (uiplaySound.trigger == UIPlaySound.Trigger.OnClick)
			{
				NGUITools.PlaySound(uiplaySound.audioClip, uiplaySound.volume, 1f);
			}
			i++;
		}
		this.CloseSelf();
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0006FC4B File Offset: 0x0006E04B
	private void Select(UILabel lbl, bool instant)
	{
		this.Highlight(lbl, instant);
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0006FC58 File Offset: 0x0006E058
	protected virtual void OnNavigate(KeyCode key)
	{
		if (base.enabled && UIPopupList.current == this)
		{
			int num = this.mLabelList.IndexOf(this.mHighlightedLabel);
			if (num == -1)
			{
				num = 0;
			}
			if (key == KeyCode.UpArrow)
			{
				if (num > 0)
				{
					this.Select(this.mLabelList[num - 1], false);
				}
			}
			else if (key == KeyCode.DownArrow && num + 1 < this.mLabelList.Count)
			{
				this.Select(this.mLabelList[num + 1], false);
			}
		}
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0006FD00 File Offset: 0x0006E100
	protected virtual void OnKey(KeyCode key)
	{
		if (base.enabled && UIPopupList.current == this && (key == UICamera.current.cancelKey0 || key == UICamera.current.cancelKey1))
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x0006FD4F File Offset: 0x0006E14F
	protected virtual void OnDisable()
	{
		this.CloseSelf();
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0006FD58 File Offset: 0x0006E158
	protected virtual void OnSelect(bool isSelected)
	{
		if (!isSelected)
		{
			GameObject selectedObject = UICamera.selectedObject;
			if (selectedObject == null || (!(selectedObject == UIPopupList.mChild) && (!(UIPopupList.mChild != null) || !(selectedObject != null) || !NGUITools.IsChild(UIPopupList.mChild.transform, selectedObject.transform))))
			{
				this.CloseSelf();
			}
		}
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0006FDC9 File Offset: 0x0006E1C9
	public static void Close()
	{
		if (UIPopupList.current != null)
		{
			UIPopupList.current.CloseSelf();
			UIPopupList.current = null;
		}
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0006FDEC File Offset: 0x0006E1EC
	public virtual void CloseSelf()
	{
		if (UIPopupList.mChild != null && UIPopupList.current == this)
		{
			base.StopCoroutine("CloseIfUnselected");
			this.mSelection = null;
			this.mLabelList.Clear();
			if (this.isAnimated)
			{
				UIWidget[] componentsInChildren = UIPopupList.mChild.GetComponentsInChildren<UIWidget>();
				int i = 0;
				int num = componentsInChildren.Length;
				while (i < num)
				{
					UIWidget uiwidget = componentsInChildren[i];
					Color color = uiwidget.color;
					color.a = 0f;
					TweenColor.Begin(uiwidget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
					i++;
				}
				Collider[] componentsInChildren2 = UIPopupList.mChild.GetComponentsInChildren<Collider>();
				int j = 0;
				int num2 = componentsInChildren2.Length;
				while (j < num2)
				{
					componentsInChildren2[j].enabled = false;
					j++;
				}
				UnityEngine.Object.Destroy(UIPopupList.mChild, 0.15f);
				UIPopupList.mFadeOutComplete = Time.unscaledTime + Mathf.Max(0.1f, 0.15f);
			}
			else
			{
				UnityEngine.Object.Destroy(UIPopupList.mChild);
				UIPopupList.mFadeOutComplete = Time.unscaledTime + 0.1f;
			}
			this.mBackground = null;
			this.mHighlight = null;
			UIPopupList.mChild = null;
			UIPopupList.current = null;
		}
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0006FF28 File Offset: 0x0006E328
	protected virtual void AnimateColor(UIWidget widget)
	{
		Color color = widget.color;
		widget.color = new Color(color.r, color.g, color.b, 0f);
		TweenColor.Begin(widget.gameObject, 0.15f, color).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x0006FF78 File Offset: 0x0006E378
	protected virtual void AnimatePosition(UIWidget widget, bool placeAbove, float bottom)
	{
		Vector3 localPosition = widget.cachedTransform.localPosition;
		Vector3 localPosition2 = (!placeAbove) ? new Vector3(localPosition.x, 0f, localPosition.z) : new Vector3(localPosition.x, bottom, localPosition.z);
		widget.cachedTransform.localPosition = localPosition2;
		GameObject gameObject = widget.gameObject;
		TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x0006FFF0 File Offset: 0x0006E3F0
	protected virtual void AnimateScale(UIWidget widget, bool placeAbove, float bottom)
	{
		GameObject gameObject = widget.gameObject;
		Transform cachedTransform = widget.cachedTransform;
		float num = (float)this.activeFontSize * this.activeFontScale + this.mBgBorder * 2f;
		cachedTransform.localScale = new Vector3(1f, num / (float)widget.height, 1f);
		TweenScale.Begin(gameObject, 0.15f, Vector3.one).method = UITweener.Method.EaseOut;
		if (placeAbove)
		{
			Vector3 localPosition = cachedTransform.localPosition;
			cachedTransform.localPosition = new Vector3(localPosition.x, localPosition.y - (float)widget.height + num, localPosition.z);
			TweenPosition.Begin(gameObject, 0.15f, localPosition).method = UITweener.Method.EaseOut;
		}
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x000700A4 File Offset: 0x0006E4A4
	protected void Animate(UIWidget widget, bool placeAbove, float bottom)
	{
		this.AnimateColor(widget);
		this.AnimatePosition(widget, placeAbove, bottom);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x000700B8 File Offset: 0x0006E4B8
	protected virtual void OnClick()
	{
		if (this.mOpenFrame == Time.frameCount)
		{
			return;
		}
		if (UIPopupList.mChild == null)
		{
			if (this.openOn == UIPopupList.OpenOn.DoubleClick || this.openOn == UIPopupList.OpenOn.Manual)
			{
				return;
			}
			if (this.openOn == UIPopupList.OpenOn.RightClick && UICamera.currentTouchID != -2)
			{
				return;
			}
			this.Show();
		}
		else if (this.mHighlightedLabel != null)
		{
			this.OnItemPress(this.mHighlightedLabel.gameObject, true);
		}
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00070146 File Offset: 0x0006E546
	protected virtual void OnDoubleClick()
	{
		if (this.openOn == UIPopupList.OpenOn.DoubleClick)
		{
			this.Show();
		}
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0007015C File Offset: 0x0006E55C
	private IEnumerator CloseIfUnselected()
	{
		GameObject sel;
		do
		{
			yield return null;
			sel = UICamera.selectedObject;
		}
		while (!(sel != this.mSelection) || (!(sel == null) && (sel == UIPopupList.mChild || NGUITools.IsChild(UIPopupList.mChild.transform, sel.transform))));
		this.CloseSelf();
		yield break;
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x00070178 File Offset: 0x0006E578
	public virtual void Show()
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && UIPopupList.mChild == null && this.isValid && this.items.Count > 0)
		{
			this.mLabelList.Clear();
			base.StopCoroutine("CloseIfUnselected");
			UICamera.selectedObject = (UICamera.hoveredObject ?? base.gameObject);
			this.mSelection = UICamera.selectedObject;
			this.source = this.mSelection;
			if (this.source == null)
			{
				Debug.LogError("Popup list needs a source object...");
				return;
			}
			this.mOpenFrame = Time.frameCount;
			if (this.mPanel == null)
			{
				this.mPanel = UIPanel.Find(base.transform);
				if (this.mPanel == null)
				{
					return;
				}
			}
			UIPopupList.mChild = new GameObject("Drop-down List");
			UIPopupList.mChild.layer = base.gameObject.layer;
			if (this.separatePanel)
			{
				if (base.GetComponent<Collider>() != null)
				{
					Rigidbody rigidbody = UIPopupList.mChild.AddComponent<Rigidbody>();
					rigidbody.isKinematic = true;
				}
				else if (base.GetComponent<Collider2D>() != null)
				{
					Rigidbody2D rigidbody2D = UIPopupList.mChild.AddComponent<Rigidbody2D>();
					rigidbody2D.isKinematic = true;
				}
				UIPanel uipanel = UIPopupList.mChild.AddComponent<UIPanel>();
				uipanel.depth = 1000000;
				uipanel.sortingOrder = this.mPanel.sortingOrder;
			}
			UIPopupList.current = this;
			Transform transform = (!this.separatePanel) ? this.mPanel.cachedTransform : (this.mPanel.GetComponentInParent<UIRoot>() ?? this.mPanel).transform;
			Transform transform2 = UIPopupList.mChild.transform;
			transform2.parent = transform;
			Vector3 vector;
			Vector3 vector2;
			if (this.openOn == UIPopupList.OpenOn.Manual && this.mSelection != base.gameObject)
			{
				this.startingPosition = UICamera.lastEventPosition;
				vector = transform.InverseTransformPoint(this.mPanel.anchorCamera.ScreenToWorldPoint(this.startingPosition));
				vector2 = vector;
				transform2.localPosition = vector;
				this.startingPosition = transform2.position;
			}
			else
			{
				Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, base.transform, false, false);
				vector = bounds.min;
				vector2 = bounds.max;
				transform2.localPosition = vector;
				this.startingPosition = transform2.position;
			}
			base.StartCoroutine("CloseIfUnselected");
			transform2.localRotation = Quaternion.identity;
			transform2.localScale = Vector3.one;
			int num = (!this.separatePanel) ? NGUITools.CalculateNextDepth(this.mPanel.gameObject) : 0;
			if (this.background2DSprite != null)
			{
				UI2DSprite ui2DSprite = UIPopupList.mChild.AddWidget(num);
				ui2DSprite.sprite2D = this.background2DSprite;
				this.mBackground = ui2DSprite;
			}
			else
			{
				if (!(this.atlas != null))
				{
					return;
				}
				this.mBackground = UIPopupList.mChild.AddSprite(this.atlas, this.backgroundSprite, num);
			}
			bool flag = this.position == UIPopupList.Position.Above;
			if (this.position == UIPopupList.Position.Auto)
			{
				UICamera uicamera = UICamera.FindCameraForLayer(this.mSelection.layer);
				if (uicamera != null)
				{
					flag = (uicamera.cachedCamera.WorldToViewportPoint(this.startingPosition).y < 0.5f);
				}
			}
			this.mBackground.pivot = UIWidget.Pivot.TopLeft;
			this.mBackground.color = this.backgroundColor;
			Vector4 border = this.mBackground.border;
			this.mBgBorder = border.y;
			this.mBackground.cachedTransform.localPosition = new Vector3(0f, (!flag) ? ((float)this.overlap) : (border.y * 2f - (float)this.overlap), 0f);
			if (this.highlight2DSprite != null)
			{
				UI2DSprite ui2DSprite2 = UIPopupList.mChild.AddWidget(num + 1);
				ui2DSprite2.sprite2D = this.highlight2DSprite;
				this.mHighlight = ui2DSprite2;
			}
			else
			{
				if (!(this.atlas != null))
				{
					return;
				}
				this.mHighlight = UIPopupList.mChild.AddSprite(this.atlas, this.highlightSprite, num + 1);
			}
			float num2 = 0f;
			float num3 = 0f;
			if (this.mHighlight.hasBorder)
			{
				num2 = this.mHighlight.border.w;
				num3 = this.mHighlight.border.x;
			}
			this.mHighlight.pivot = UIWidget.Pivot.TopLeft;
			this.mHighlight.color = this.highlightColor;
			float num4 = (float)this.activeFontSize;
			float activeFontScale = this.activeFontScale;
			float num5 = num4 * activeFontScale;
			float num6 = num5 + this.padding.y;
			float num7 = 0f;
			float num8 = (!flag) ? (-this.padding.y - border.y + (float)this.overlap) : (border.y - this.padding.y - (float)this.overlap);
			float num9 = border.y * 2f + this.padding.y;
			List<UILabel> list = new List<UILabel>();
			if (!this.items.Contains(this.mSelectedItem))
			{
				this.mSelectedItem = null;
			}
			int i = 0;
			int count = this.items.Count;
			while (i < count)
			{
				string text = this.items[i];
				UILabel uilabel = UIPopupList.mChild.AddWidget(this.mBackground.depth + 2);
				uilabel.name = i.ToString();
				uilabel.pivot = UIWidget.Pivot.TopLeft;
				uilabel.bitmapFont = this.bitmapFont;
				uilabel.trueTypeFont = this.trueTypeFont;
				uilabel.fontSize = this.fontSize;
				uilabel.fontStyle = this.fontStyle;
				uilabel.text = ((!this.isLocalized) ? text : Localization.Get(text, true));
				uilabel.modifier = this.textModifier;
				uilabel.color = this.textColor;
				uilabel.cachedTransform.localPosition = new Vector3(border.x + this.padding.x - uilabel.pivotOffset.x, num8, -1f);
				uilabel.overflowMethod = UILabel.Overflow.ResizeFreely;
				uilabel.alignment = this.alignment;
				list.Add(uilabel);
				num9 += num6;
				num8 -= num6;
				num7 = Mathf.Max(num7, uilabel.printedSize.x);
				UIEventListener uieventListener = UIEventListener.Get(uilabel.gameObject);
				uieventListener.onHover = new UIEventListener.BoolDelegate(this.OnItemHover);
				uieventListener.onPress = new UIEventListener.BoolDelegate(this.OnItemPress);
				uieventListener.onClick = new UIEventListener.VoidDelegate(this.OnItemClick);
				uieventListener.parameter = text;
				if (this.mSelectedItem == text || (i == 0 && string.IsNullOrEmpty(this.mSelectedItem)))
				{
					this.Highlight(uilabel, true);
				}
				this.mLabelList.Add(uilabel);
				i++;
			}
			num7 = Mathf.Max(num7, vector2.x - vector.x - (border.x + this.padding.x) * 2f);
			float num10 = num7;
			Vector3 vector3 = new Vector3(num10 * 0.5f, -num5 * 0.5f, 0f);
			Vector3 vector4 = new Vector3(num10, num5 + this.padding.y, 1f);
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				UILabel uilabel2 = list[j];
				NGUITools.AddWidgetCollider(uilabel2.gameObject);
				uilabel2.autoResizeBoxCollider = false;
				BoxCollider component = uilabel2.GetComponent<BoxCollider>();
				if (component != null)
				{
					vector3.z = component.center.z;
					component.center = vector3;
					component.size = vector4;
				}
				else
				{
					BoxCollider2D component2 = uilabel2.GetComponent<BoxCollider2D>();
					component2.offset = vector3;
					component2.size = vector4;
				}
				j++;
			}
			int width = Mathf.RoundToInt(num7);
			num7 += (border.x + this.padding.x) * 2f;
			num8 -= border.y;
			this.mBackground.width = Mathf.RoundToInt(num7);
			this.mBackground.height = Mathf.RoundToInt(num9);
			int k = 0;
			int count3 = list.Count;
			while (k < count3)
			{
				UILabel uilabel3 = list[k];
				uilabel3.overflowMethod = UILabel.Overflow.ShrinkContent;
				uilabel3.width = width;
				k++;
			}
			float num11 = (!(this.atlas != null)) ? 2f : (2f * this.atlas.pixelSize);
			float f = num7 - (border.x + this.padding.x) * 2f + num3 * num11;
			float f2 = num5 + num2 * num11;
			this.mHighlight.width = Mathf.RoundToInt(f);
			this.mHighlight.height = Mathf.RoundToInt(f2);
			if (this.isAnimated)
			{
				this.AnimateColor(this.mBackground);
				if (Time.timeScale == 0f || Time.timeScale >= 0.1f)
				{
					float bottom = num8 + num5;
					this.Animate(this.mHighlight, flag, bottom);
					int l = 0;
					int count4 = list.Count;
					while (l < count4)
					{
						this.Animate(list[l], flag, bottom);
						l++;
					}
					this.AnimateScale(this.mBackground, flag, bottom);
				}
			}
			if (flag)
			{
				vector.y = vector2.y - border.y;
				vector2.y = vector.y + (float)this.mBackground.height;
				vector2.x = vector.x + (float)this.mBackground.width;
				transform2.localPosition = new Vector3(vector.x, vector2.y - border.y, vector.z);
			}
			else
			{
				vector2.y = vector.y + border.y;
				vector.y = vector2.y - (float)this.mBackground.height;
				vector2.x = vector.x + (float)this.mBackground.width;
			}
			Transform parent = this.mPanel.cachedTransform.parent;
			if (parent != null)
			{
				vector = this.mPanel.cachedTransform.TransformPoint(vector);
				vector2 = this.mPanel.cachedTransform.TransformPoint(vector2);
				vector = parent.InverseTransformPoint(vector);
				vector2 = parent.InverseTransformPoint(vector2);
				float pixelSizeAdjustment = UIRoot.GetPixelSizeAdjustment(base.gameObject);
				vector /= pixelSizeAdjustment;
				vector2 /= pixelSizeAdjustment;
			}
			Vector3 b = (!this.mPanel.hasClipping) ? this.mPanel.CalculateConstrainOffset(vector, vector2) : Vector3.zero;
			Vector3 localPosition = transform2.localPosition + b;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			transform2.localPosition = localPosition;
		}
		else
		{
			this.OnSelect(false);
		}
	}

	// Token: 0x04000C72 RID: 3186
	public static UIPopupList current;

	// Token: 0x04000C73 RID: 3187
	protected static GameObject mChild;

	// Token: 0x04000C74 RID: 3188
	protected static float mFadeOutComplete;

	// Token: 0x04000C75 RID: 3189
	private const float animSpeed = 0.15f;

	// Token: 0x04000C76 RID: 3190
	public UIAtlas atlas;

	// Token: 0x04000C77 RID: 3191
	public UIFont bitmapFont;

	// Token: 0x04000C78 RID: 3192
	public Font trueTypeFont;

	// Token: 0x04000C79 RID: 3193
	public int fontSize = 16;

	// Token: 0x04000C7A RID: 3194
	public FontStyle fontStyle;

	// Token: 0x04000C7B RID: 3195
	public string backgroundSprite;

	// Token: 0x04000C7C RID: 3196
	public string highlightSprite;

	// Token: 0x04000C7D RID: 3197
	public Sprite background2DSprite;

	// Token: 0x04000C7E RID: 3198
	public Sprite highlight2DSprite;

	// Token: 0x04000C7F RID: 3199
	public UIPopupList.Position position;

	// Token: 0x04000C80 RID: 3200
	public UIPopupList.Selection selection;

	// Token: 0x04000C81 RID: 3201
	public NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	// Token: 0x04000C82 RID: 3202
	public List<string> items = new List<string>();

	// Token: 0x04000C83 RID: 3203
	public List<object> itemData = new List<object>();

	// Token: 0x04000C84 RID: 3204
	public Vector2 padding = new Vector3(4f, 4f);

	// Token: 0x04000C85 RID: 3205
	public Color textColor = Color.white;

	// Token: 0x04000C86 RID: 3206
	public Color backgroundColor = Color.white;

	// Token: 0x04000C87 RID: 3207
	public Color highlightColor = new Color(0.882352948f, 0.784313738f, 0.5882353f, 1f);

	// Token: 0x04000C88 RID: 3208
	public bool isAnimated = true;

	// Token: 0x04000C89 RID: 3209
	public bool isLocalized;

	// Token: 0x04000C8A RID: 3210
	public UILabel.Modifier textModifier;

	// Token: 0x04000C8B RID: 3211
	public bool separatePanel = true;

	// Token: 0x04000C8C RID: 3212
	public int overlap;

	// Token: 0x04000C8D RID: 3213
	public UIPopupList.OpenOn openOn;

	// Token: 0x04000C8E RID: 3214
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04000C8F RID: 3215
	[HideInInspector]
	[SerializeField]
	protected string mSelectedItem;

	// Token: 0x04000C90 RID: 3216
	[HideInInspector]
	[SerializeField]
	protected UIPanel mPanel;

	// Token: 0x04000C91 RID: 3217
	[HideInInspector]
	[SerializeField]
	protected UIBasicSprite mBackground;

	// Token: 0x04000C92 RID: 3218
	[HideInInspector]
	[SerializeField]
	protected UIBasicSprite mHighlight;

	// Token: 0x04000C93 RID: 3219
	[HideInInspector]
	[SerializeField]
	protected UILabel mHighlightedLabel;

	// Token: 0x04000C94 RID: 3220
	[HideInInspector]
	[SerializeField]
	protected List<UILabel> mLabelList = new List<UILabel>();

	// Token: 0x04000C95 RID: 3221
	[HideInInspector]
	[SerializeField]
	protected float mBgBorder;

	// Token: 0x04000C96 RID: 3222
	[Tooltip("Whether the selection will be persistent even after the popup list is closed. By default the selection is cleared when the popup is closed so that the same selection can be chosen again the next time the popup list is opened. If enabled, the selection will persist, but selecting the same choice in succession will not result in the onChange notification being triggered more than once.")]
	public bool keepValue;

	// Token: 0x04000C97 RID: 3223
	[NonSerialized]
	protected GameObject mSelection;

	// Token: 0x04000C98 RID: 3224
	[NonSerialized]
	protected int mOpenFrame;

	// Token: 0x04000C99 RID: 3225
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04000C9A RID: 3226
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnSelectionChange";

	// Token: 0x04000C9B RID: 3227
	[HideInInspector]
	[SerializeField]
	private float textScale;

	// Token: 0x04000C9C RID: 3228
	[HideInInspector]
	[SerializeField]
	private UIFont font;

	// Token: 0x04000C9D RID: 3229
	[HideInInspector]
	[SerializeField]
	private UILabel textLabel;

	// Token: 0x04000C9E RID: 3230
	[NonSerialized]
	public Vector3 startingPosition;

	// Token: 0x04000C9F RID: 3231
	private UIPopupList.LegacyEvent mLegacyEvent;

	// Token: 0x04000CA0 RID: 3232
	[NonSerialized]
	protected bool mExecuting;

	// Token: 0x04000CA1 RID: 3233
	protected bool mUseDynamicFont;

	// Token: 0x04000CA2 RID: 3234
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x04000CA3 RID: 3235
	protected bool mTweening;

	// Token: 0x04000CA4 RID: 3236
	public GameObject source;

	// Token: 0x020001D0 RID: 464
	public enum Position
	{
		// Token: 0x04000CA6 RID: 3238
		Auto,
		// Token: 0x04000CA7 RID: 3239
		Above,
		// Token: 0x04000CA8 RID: 3240
		Below
	}

	// Token: 0x020001D1 RID: 465
	public enum Selection
	{
		// Token: 0x04000CAA RID: 3242
		OnPress,
		// Token: 0x04000CAB RID: 3243
		OnClick
	}

	// Token: 0x020001D2 RID: 466
	public enum OpenOn
	{
		// Token: 0x04000CAD RID: 3245
		ClickOrTap,
		// Token: 0x04000CAE RID: 3246
		RightClick,
		// Token: 0x04000CAF RID: 3247
		DoubleClick,
		// Token: 0x04000CB0 RID: 3248
		Manual
	}

	// Token: 0x020001D3 RID: 467
	// (Invoke) Token: 0x06000DF0 RID: 3568
	public delegate void LegacyEvent(string val);
}
