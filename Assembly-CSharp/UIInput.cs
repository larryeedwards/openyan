using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000267 RID: 615
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06001300 RID: 4864 RVA: 0x00095029 File Offset: 0x00093429
	// (set) Token: 0x06001301 RID: 4865 RVA: 0x00095042 File Offset: 0x00093442
	public string defaultText
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06001302 RID: 4866 RVA: 0x00095062 File Offset: 0x00093462
	// (set) Token: 0x06001303 RID: 4867 RVA: 0x0009507B File Offset: 0x0009347B
	public Color defaultColor
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultColor;
		}
		set
		{
			this.mDefaultColor = value;
			if (!this.isSelected)
			{
				this.label.color = value;
			}
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06001304 RID: 4868 RVA: 0x0009509B File Offset: 0x0009349B
	public bool inputShouldBeHidden
	{
		get
		{
			return this.hideInput && this.label != null && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06001305 RID: 4869 RVA: 0x000950D8 File Offset: 0x000934D8
	// (set) Token: 0x06001306 RID: 4870 RVA: 0x000950E0 File Offset: 0x000934E0
	[Obsolete("Use UIInput.value instead")]
	public string text
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

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06001307 RID: 4871 RVA: 0x000950E9 File Offset: 0x000934E9
	// (set) Token: 0x06001308 RID: 4872 RVA: 0x00095102 File Offset: 0x00093502
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0009510C File Offset: 0x0009350C
	public void Set(string value, bool notify = true)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (value == this.value)
		{
			return;
		}
		UIInput.mDrawStart = 0;
		value = this.Validate(value);
		if (this.mValue != value)
		{
			this.mValue = value;
			this.mLoadSavedValue = false;
			if (this.isSelected)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.mSelectionStart = 0;
					this.mSelectionEnd = 0;
				}
				else
				{
					this.mSelectionStart = value.Length;
					this.mSelectionEnd = this.mSelectionStart;
				}
			}
			else if (this.mStarted)
			{
				this.SaveToPlayerPrefs(value);
			}
			this.UpdateLabel();
			if (notify)
			{
				this.ExecuteOnChange();
			}
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x0600130A RID: 4874 RVA: 0x000951D4 File Offset: 0x000935D4
	// (set) Token: 0x0600130B RID: 4875 RVA: 0x000951DC File Offset: 0x000935DC
	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x0600130C RID: 4876 RVA: 0x000951E5 File Offset: 0x000935E5
	// (set) Token: 0x0600130D RID: 4877 RVA: 0x000951F2 File Offset: 0x000935F2
	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x0600130E RID: 4878 RVA: 0x0009521B File Offset: 0x0009361B
	// (set) Token: 0x0600130F RID: 4879 RVA: 0x0009523E File Offset: 0x0009363E
	public int cursorPosition
	{
		get
		{
			return (!this.isSelected) ? this.value.Length : this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06001310 RID: 4880 RVA: 0x00095258 File Offset: 0x00093658
	// (set) Token: 0x06001311 RID: 4881 RVA: 0x0009527B File Offset: 0x0009367B
	public int selectionStart
	{
		get
		{
			return (!this.isSelected) ? this.value.Length : this.mSelectionStart;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionStart = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06001312 RID: 4882 RVA: 0x00095295 File Offset: 0x00093695
	// (set) Token: 0x06001313 RID: 4883 RVA: 0x000952B8 File Offset: 0x000936B8
	public int selectionEnd
	{
		get
		{
			return (!this.isSelected) ? this.value.Length : this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06001314 RID: 4884 RVA: 0x000952D2 File Offset: 0x000936D2
	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000952DC File Offset: 0x000936DC
	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		foreach (char c in val)
		{
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000953AC File Offset: 0x000937AC
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.selectOnTab != null)
		{
			UIKeyNavigation uikeyNavigation = base.GetComponent<UIKeyNavigation>();
			if (uikeyNavigation == null)
			{
				uikeyNavigation = base.gameObject.AddComponent<UIKeyNavigation>();
				uikeyNavigation.onDown = this.selectOnTab;
			}
			this.selectOnTab = null;
			NGUITools.SetDirty(this);
		}
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
		}
		else
		{
			this.value = this.mValue.Replace("\\n", "\n");
		}
		this.mStarted = true;
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x00095458 File Offset: 0x00093858
	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.mEllipsis = this.label.overflowEllipsis;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mAlignment = this.label.alignment;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x00095518 File Offset: 0x00093918
	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
			}
			else
			{
				PlayerPrefs.SetString(this.savedAs, val);
			}
		}
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x00095554 File Offset: 0x00093954
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			if (this.label != null)
			{
				this.label.supportEncoding = false;
			}
			if (this.mOnGUI == null)
			{
				this.mOnGUI = base.gameObject.AddComponent<UIInputOnGUI>();
			}
			this.OnSelectEvent();
		}
		else
		{
			if (this.mOnGUI != null)
			{
				UnityEngine.Object.Destroy(this.mOnGUI);
				this.mOnGUI = null;
			}
			this.OnDeselectEvent();
		}
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000955DC File Offset: 0x000939DC
	protected void OnSelectEvent()
	{
		this.mSelectTime = Time.frameCount;
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.mEllipsis = this.label.overflowEllipsis;
			this.label.overflowEllipsis = false;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mSelectMe = Time.frameCount;
		}
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x00095660 File Offset: 0x00093A60
	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.label.overflowEllipsis = this.mEllipsis;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = IMECompositionMode.Auto;
			this.label.alignment = this.mAlignment;
		}
		UIInput.selection = null;
		this.UpdateLabel();
		if (this.submitOnUnselect)
		{
			this.Submit();
		}
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x00095744 File Offset: 0x00093B44
	protected virtual void Update()
	{
		if (!this.isSelected || this.mSelectTime == Time.frameCount)
		{
			return;
		}
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
		{
			this.mSelectMe = -1;
			this.mSelectionEnd = ((!string.IsNullOrEmpty(this.mValue)) ? this.mValue.Length : 0);
			UIInput.mDrawStart = 0;
			this.mSelectionStart = ((!this.selectAllTextOnFocus) ? this.mSelectionEnd : 0);
			this.label.color = this.activeTextColor;
			Vector2 compositionCursorPos = (!(UICamera.current != null) || !(UICamera.current.cachedCamera != null)) ? this.label.worldCorners[0] : UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]);
			compositionCursorPos.y = (float)Screen.height - compositionCursorPos.y;
			Input.imeCompositionMode = IMECompositionMode.On;
			Input.compositionCursorPos = compositionCursorPos;
			this.UpdateLabel();
			if (string.IsNullOrEmpty(Input.inputString))
			{
				return;
			}
		}
		string compositionString = Input.compositionString;
		if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
		{
			foreach (char c in Input.inputString)
			{
				if (c >= ' ')
				{
					if (c != '')
					{
						if (c != '')
						{
							if (c != '')
							{
								if (c != '')
								{
									if (c != '')
									{
										this.Insert(c.ToString());
									}
								}
							}
						}
					}
				}
			}
		}
		if (UIInput.mLastIME != compositionString)
		{
			this.mSelectionEnd = ((!string.IsNullOrEmpty(compositionString)) ? (this.mValue.Length + compositionString.Length) : this.mSelectionStart);
			UIInput.mLastIME = compositionString;
			this.UpdateLabel();
			this.ExecuteOnChange();
		}
		if (this.mCaret != null && this.mNextBlink < RealTime.time)
		{
			this.mNextBlink = RealTime.time + 0.5f;
			this.mCaret.enabled = !this.mCaret.enabled;
		}
		if (this.isSelected && this.mLastAlpha != this.label.finalAlpha)
		{
			this.UpdateLabel();
		}
		if (this.mCam == null)
		{
			this.mCam = UICamera.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mCam != null)
		{
			bool flag = false;
			if (this.label.multiLine)
			{
				bool flag2 = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
				if (this.onReturnKey == UIInput.OnReturnKey.Submit)
				{
					flag = flag2;
				}
				else
				{
					flag = !flag2;
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey0) || (this.mCam.submitKey0 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey0;
					this.Submit();
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey1) || (this.mCam.submitKey1 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey1;
					this.Submit();
				}
			}
			if (!this.mCam.useKeyboard && UICamera.GetKeyUp(KeyCode.Tab))
			{
				this.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x00095BE4 File Offset: 0x00093FE4
	private void OnKey(KeyCode key)
	{
		int frameCount = Time.frameCount;
		if (UIInput.mIgnoreKey == frameCount)
		{
			return;
		}
		if (this.mCam != null && (key == this.mCam.cancelKey0 || key == this.mCam.cancelKey1))
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
		}
		else if (key == KeyCode.Tab)
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x00095C78 File Offset: 0x00094078
	protected void DoBackspace()
	{
		if (!string.IsNullOrEmpty(this.mValue))
		{
			if (this.mSelectionStart == this.mSelectionEnd)
			{
				if (this.mSelectionStart < 1)
				{
					return;
				}
				this.mSelectionEnd--;
			}
			this.Insert(string.Empty);
		}
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x00095CCC File Offset: 0x000940CC
	public virtual bool ProcessEvent(Event ev)
	{
		if (this.label == null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer;
		bool flag2 = (!flag) ? ((ev.modifiers & EventModifiers.Control) != EventModifiers.None) : ((ev.modifiers & EventModifiers.Command) != EventModifiers.None);
		if ((ev.modifiers & EventModifiers.Alt) != EventModifiers.None)
		{
			flag2 = false;
		}
		bool flag3 = (ev.modifiers & EventModifiers.Shift) != EventModifiers.None;
		KeyCode keyCode = ev.keyCode;
		switch (keyCode)
		{
		case KeyCode.UpArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.UpArrow);
				if (this.mSelectionEnd != 0)
				{
					this.mSelectionEnd += UIInput.mDrawStart;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.DownArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.DownArrow);
				if (this.mSelectionEnd != this.label.processedText.Length)
				{
					this.mSelectionEnd += UIInput.mDrawStart;
				}
				else
				{
					this.mSelectionEnd = this.mValue.Length;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.RightArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = Mathf.Min(this.mSelectionEnd + 1, this.mValue.Length);
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.LeftArrow:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = Mathf.Max(this.mSelectionEnd - 1, 0);
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		default:
			switch (keyCode)
			{
			case KeyCode.A:
				if (flag2)
				{
					ev.Use();
					this.mSelectionStart = 0;
					this.mSelectionEnd = this.mValue.Length;
					this.UpdateLabel();
				}
				return true;
			default:
				switch (keyCode)
				{
				case KeyCode.V:
					if (flag2)
					{
						ev.Use();
						this.Insert(NGUITools.clipboard);
					}
					return true;
				default:
					if (keyCode == KeyCode.Backspace)
					{
						ev.Use();
						this.DoBackspace();
						return true;
					}
					if (keyCode != KeyCode.Delete)
					{
						return false;
					}
					ev.Use();
					if (!string.IsNullOrEmpty(this.mValue))
					{
						if (this.mSelectionStart == this.mSelectionEnd)
						{
							if (this.mSelectionStart >= this.mValue.Length)
							{
								return true;
							}
							this.mSelectionEnd++;
						}
						this.Insert(string.Empty);
					}
					return true;
				case KeyCode.X:
					if (flag2)
					{
						ev.Use();
						NGUITools.clipboard = this.GetSelection();
						this.Insert(string.Empty);
					}
					return true;
				}
				break;
			case KeyCode.C:
				if (flag2)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
				}
				return true;
			}
			break;
		case KeyCode.Home:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				if (this.label.multiLine)
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.Home);
				}
				else
				{
					this.mSelectionEnd = 0;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.End:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				if (this.label.multiLine)
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.End);
				}
				else
				{
					this.mSelectionEnd = this.mValue.Length;
				}
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.PageUp:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = 0;
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		case KeyCode.PageDown:
			ev.Use();
			if (!string.IsNullOrEmpty(this.mValue))
			{
				this.mSelectionEnd = this.mValue.Length;
				if (!flag3)
				{
					this.mSelectionStart = this.mSelectionEnd;
				}
				this.UpdateLabel();
			}
			return true;
		}
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x00096188 File Offset: 0x00094588
	protected virtual void Insert(string text)
	{
		string leftText = this.GetLeftText();
		string rightText = this.GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			char c = text[i];
			if (c == '\b')
			{
				this.DoBackspace();
			}
			else
			{
				if (this.characterLimit > 0 && stringBuilder.Length + length >= this.characterLimit)
				{
					break;
				}
				if (this.onValidate != null)
				{
					c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				else if (this.validation != UIInput.Validation.None)
				{
					c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
			}
			i++;
		}
		this.mSelectionStart = stringBuilder.Length;
		this.mSelectionEnd = this.mSelectionStart;
		int j = 0;
		int length3 = rightText.Length;
		while (j < length3)
		{
			char c2 = rightText[j];
			if (this.onValidate != null)
			{
				c2 = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c2 = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			j++;
		}
		this.mValue = stringBuilder.ToString();
		this.UpdateLabel();
		this.ExecuteOnChange();
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x00096340 File Offset: 0x00094740
	protected string GetLeftText()
	{
		int num = Mathf.Min(new int[]
		{
			this.mSelectionStart,
			this.mSelectionEnd,
			this.mValue.Length
		});
		return (!string.IsNullOrEmpty(this.mValue) && num >= 0) ? this.mValue.Substring(0, num) : string.Empty;
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x000963A8 File Offset: 0x000947A8
	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length) ? this.mValue.Substring(num) : string.Empty;
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x00096400 File Offset: 0x00094800
	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
		{
			return string.Empty;
		}
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		int num2 = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return this.mValue.Substring(num, num2 - num);
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x00096468 File Offset: 0x00094868
	protected int GetCharUnderMouse()
	{
		Vector3[] worldCorners = this.label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane plane = new Plane(worldCorners[0], worldCorners[1], worldCorners[2]);
		float distance;
		return (!plane.Raycast(currentRay, out distance)) ? 0 : (UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(currentRay.GetPoint(distance), false));
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x000964E4 File Offset: 0x000948E4
	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && this.isSelected && this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				this.selectionStart = this.mSelectionEnd;
			}
		}
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x0009655E File Offset: 0x0009495E
	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x00096592 File Offset: 0x00094992
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x0009659C File Offset: 0x0009499C
	protected virtual void Cleanup()
	{
		if (this.mHighlight)
		{
			this.mHighlight.enabled = false;
		}
		if (this.mCaret)
		{
			this.mCaret.enabled = false;
		}
		if (this.mBlankTex)
		{
			NGUITools.Destroy(this.mBlankTex);
			this.mBlankTex = null;
		}
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x00096604 File Offset: 0x00094A04
	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0009665C File Offset: 0x00094A5C
	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((!flag || isSelected) ? this.activeTextColor : this.mDefaultColor);
			string text;
			if (flag)
			{
				text = ((!isSelected) ? this.mDefaultText : string.Empty);
				this.label.alignment = this.mAlignment;
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = string.Empty;
					string str = "*";
					if (this.label.bitmapFont != null && this.label.bitmapFont.bmFont != null && this.label.bitmapFont.bmFont.GetGlyph(42) == null)
					{
						str = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += str;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = (!isSelected) ? 0 : Mathf.Min(text.Length, this.cursorPosition);
				string str2 = text.Substring(0, num);
				if (isSelected)
				{
					str2 += Input.compositionString;
				}
				text = str2 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.label.alignment = this.mAlignment;
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.label.alignment = NGUIText.Alignment.Right;
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.label.alignment = this.mAlignment;
				}
			}
			this.label.text = text;
			if (isSelected)
			{
				int num3 = this.mSelectionStart - UIInput.mDrawStart;
				int num4 = this.mSelectionEnd - UIInput.mDrawStart;
				if (this.mBlankTex == null)
				{
					this.mBlankTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							this.mBlankTex.SetPixel(k, j, Color.white);
						}
					}
					this.mBlankTex.Apply();
				}
				if (num3 != num4)
				{
					if (this.mHighlight == null)
					{
						this.mHighlight = this.label.cachedGameObject.AddWidget(int.MaxValue);
						this.mHighlight.name = "Input Highlight";
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.fillGeometry = false;
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.SetAnchor(this.label.cachedTransform);
					}
					else
					{
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.MarkAsChanged();
						this.mHighlight.enabled = true;
					}
				}
				if (this.mCaret == null)
				{
					this.mCaret = this.label.cachedGameObject.AddWidget(int.MaxValue);
					this.mCaret.name = "Input Caret";
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.fillGeometry = false;
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.SetAnchor(this.label.cachedTransform);
				}
				else
				{
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.MarkAsChanged();
					this.mCaret.enabled = true;
				}
				if (num3 != num4)
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
					this.mHighlight.enabled = this.mHighlight.geometry.hasVertices;
				}
				else
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, null, this.caretColor, this.selectionColor);
					if (this.mHighlight != null)
					{
						this.mHighlight.enabled = false;
					}
				}
				this.mNextBlink = RealTime.time + 0.5f;
				this.mLastAlpha = this.label.finalAlpha;
			}
			else
			{
				this.Cleanup();
			}
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x00096C0C File Offset: 0x0009500C
	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Filename)
		{
			if (ch == ':')
			{
				return '\0';
			}
			if (ch == '/')
			{
				return '\0';
			}
			if (ch == '\\')
			{
				return '\0';
			}
			if (ch == '<')
			{
				return '\0';
			}
			if (ch == '>')
			{
				return '\0';
			}
			if (ch == '|')
			{
				return '\0';
			}
			if (ch == '^')
			{
				return '\0';
			}
			if (ch == '*')
			{
				return '\0';
			}
			if (ch == ';')
			{
				return '\0';
			}
			if (ch == '"')
			{
				return '\0';
			}
			if (ch == '`')
			{
				return '\0';
			}
			if (ch == '\t')
			{
				return '\0';
			}
			if (ch == '\n')
			{
				return '\0';
			}
			return ch;
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length <= 0) ? ' ' : text[Mathf.Clamp(pos, 0, text.Length - 1)];
			char c2 = (text.Length <= 0) ? '\n' : text[Mathf.Clamp(pos + 1, 0, text.Length - 1)];
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x00096F06 File Offset: 0x00095306
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x00096F3F File Offset: 0x0009533F
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x00096F48 File Offset: 0x00095348
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x00096F58 File Offset: 0x00095358
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			string text = this.mValue.Replace("\\n", "\n");
			this.mValue = string.Empty;
			this.value = ((!PlayerPrefs.HasKey(this.savedAs)) ? text : PlayerPrefs.GetString(this.savedAs));
		}
	}

	// Token: 0x0400104C RID: 4172
	public static UIInput current;

	// Token: 0x0400104D RID: 4173
	public static UIInput selection;

	// Token: 0x0400104E RID: 4174
	public UILabel label;

	// Token: 0x0400104F RID: 4175
	public UIInput.InputType inputType;

	// Token: 0x04001050 RID: 4176
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x04001051 RID: 4177
	public UIInput.KeyboardType keyboardType;

	// Token: 0x04001052 RID: 4178
	public bool hideInput;

	// Token: 0x04001053 RID: 4179
	[NonSerialized]
	public bool selectAllTextOnFocus = true;

	// Token: 0x04001054 RID: 4180
	public bool submitOnUnselect;

	// Token: 0x04001055 RID: 4181
	public UIInput.Validation validation;

	// Token: 0x04001056 RID: 4182
	public int characterLimit;

	// Token: 0x04001057 RID: 4183
	public string savedAs;

	// Token: 0x04001058 RID: 4184
	[HideInInspector]
	[SerializeField]
	private GameObject selectOnTab;

	// Token: 0x04001059 RID: 4185
	public Color activeTextColor = Color.white;

	// Token: 0x0400105A RID: 4186
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x0400105B RID: 4187
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x0400105C RID: 4188
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x0400105D RID: 4189
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x0400105E RID: 4190
	public UIInput.OnValidate onValidate;

	// Token: 0x0400105F RID: 4191
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x04001060 RID: 4192
	[NonSerialized]
	protected string mDefaultText = string.Empty;

	// Token: 0x04001061 RID: 4193
	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	// Token: 0x04001062 RID: 4194
	[NonSerialized]
	protected float mPosition;

	// Token: 0x04001063 RID: 4195
	[NonSerialized]
	protected bool mDoInit = true;

	// Token: 0x04001064 RID: 4196
	[NonSerialized]
	protected NGUIText.Alignment mAlignment = NGUIText.Alignment.Left;

	// Token: 0x04001065 RID: 4197
	[NonSerialized]
	protected bool mLoadSavedValue = true;

	// Token: 0x04001066 RID: 4198
	protected static int mDrawStart;

	// Token: 0x04001067 RID: 4199
	protected static string mLastIME = string.Empty;

	// Token: 0x04001068 RID: 4200
	[NonSerialized]
	protected int mSelectionStart;

	// Token: 0x04001069 RID: 4201
	[NonSerialized]
	protected int mSelectionEnd;

	// Token: 0x0400106A RID: 4202
	[NonSerialized]
	protected UITexture mHighlight;

	// Token: 0x0400106B RID: 4203
	[NonSerialized]
	protected UITexture mCaret;

	// Token: 0x0400106C RID: 4204
	[NonSerialized]
	protected Texture2D mBlankTex;

	// Token: 0x0400106D RID: 4205
	[NonSerialized]
	protected float mNextBlink;

	// Token: 0x0400106E RID: 4206
	[NonSerialized]
	protected float mLastAlpha;

	// Token: 0x0400106F RID: 4207
	[NonSerialized]
	protected string mCached = string.Empty;

	// Token: 0x04001070 RID: 4208
	[NonSerialized]
	protected int mSelectMe = -1;

	// Token: 0x04001071 RID: 4209
	[NonSerialized]
	protected int mSelectTime = -1;

	// Token: 0x04001072 RID: 4210
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x04001073 RID: 4211
	[NonSerialized]
	private UIInputOnGUI mOnGUI;

	// Token: 0x04001074 RID: 4212
	[NonSerialized]
	private UICamera mCam;

	// Token: 0x04001075 RID: 4213
	[NonSerialized]
	private bool mEllipsis;

	// Token: 0x04001076 RID: 4214
	private static int mIgnoreKey;

	// Token: 0x02000268 RID: 616
	public enum InputType
	{
		// Token: 0x04001078 RID: 4216
		Standard,
		// Token: 0x04001079 RID: 4217
		AutoCorrect,
		// Token: 0x0400107A RID: 4218
		Password
	}

	// Token: 0x02000269 RID: 617
	public enum Validation
	{
		// Token: 0x0400107C RID: 4220
		None,
		// Token: 0x0400107D RID: 4221
		Integer,
		// Token: 0x0400107E RID: 4222
		Float,
		// Token: 0x0400107F RID: 4223
		Alphanumeric,
		// Token: 0x04001080 RID: 4224
		Username,
		// Token: 0x04001081 RID: 4225
		Name,
		// Token: 0x04001082 RID: 4226
		Filename
	}

	// Token: 0x0200026A RID: 618
	public enum KeyboardType
	{
		// Token: 0x04001084 RID: 4228
		Default,
		// Token: 0x04001085 RID: 4229
		ASCIICapable,
		// Token: 0x04001086 RID: 4230
		NumbersAndPunctuation,
		// Token: 0x04001087 RID: 4231
		URL,
		// Token: 0x04001088 RID: 4232
		NumberPad,
		// Token: 0x04001089 RID: 4233
		PhonePad,
		// Token: 0x0400108A RID: 4234
		NamePhonePad,
		// Token: 0x0400108B RID: 4235
		EmailAddress
	}

	// Token: 0x0200026B RID: 619
	public enum OnReturnKey
	{
		// Token: 0x0400108D RID: 4237
		Default,
		// Token: 0x0400108E RID: 4238
		Submit,
		// Token: 0x0400108F RID: 4239
		NewLine
	}

	// Token: 0x0200026C RID: 620
	// (Invoke) Token: 0x06001332 RID: 4914
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
