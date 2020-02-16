using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C6 RID: 454
[AddComponentMenu("NGUI/Interaction/Key Binding")]
public class UIKeyBinding : MonoBehaviour
{
	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0006DCF4 File Offset: 0x0006C0F4
	public string captionText
	{
		get
		{
			string text = NGUITools.KeyToCaption(this.keyCode);
			if (this.modifier == UIKeyBinding.Modifier.Alt)
			{
				return "Alt+" + text;
			}
			if (this.modifier == UIKeyBinding.Modifier.Control)
			{
				return "Control+" + text;
			}
			if (this.modifier == UIKeyBinding.Modifier.Shift)
			{
				return "Shift+" + text;
			}
			return text;
		}
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0006DD58 File Offset: 0x0006C158
	public static bool IsBound(KeyCode key)
	{
		int i = 0;
		int count = UIKeyBinding.mList.Count;
		while (i < count)
		{
			UIKeyBinding uikeyBinding = UIKeyBinding.mList[i];
			if (uikeyBinding != null && uikeyBinding.keyCode == key)
			{
				return true;
			}
			i++;
		}
		return false;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0006DDA9 File Offset: 0x0006C1A9
	protected virtual void OnEnable()
	{
		UIKeyBinding.mList.Add(this);
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0006DDB6 File Offset: 0x0006C1B6
	protected virtual void OnDisable()
	{
		UIKeyBinding.mList.Remove(this);
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0006DDC4 File Offset: 0x0006C1C4
	protected virtual void Start()
	{
		UIInput component = base.GetComponent<UIInput>();
		this.mIsInput = (component != null);
		if (component != null)
		{
			EventDelegate.Add(component.onSubmit, new EventDelegate.Callback(this.OnSubmit));
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0006DE0A File Offset: 0x0006C20A
	protected virtual void OnSubmit()
	{
		if (UICamera.currentKey == this.keyCode && this.IsModifierActive())
		{
			this.mIgnoreUp = true;
		}
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0006DE2E File Offset: 0x0006C22E
	protected virtual bool IsModifierActive()
	{
		return UIKeyBinding.IsModifierActive(this.modifier);
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0006DE3C File Offset: 0x0006C23C
	public static bool IsModifierActive(UIKeyBinding.Modifier modifier)
	{
		if (modifier == UIKeyBinding.Modifier.Any)
		{
			return true;
		}
		if (modifier == UIKeyBinding.Modifier.Alt)
		{
			if (UICamera.GetKey(KeyCode.LeftAlt) || UICamera.GetKey(KeyCode.RightAlt))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.Control)
		{
			if (UICamera.GetKey(KeyCode.LeftControl) || UICamera.GetKey(KeyCode.RightControl))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.Shift)
		{
			if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
			{
				return true;
			}
		}
		else if (modifier == UIKeyBinding.Modifier.None)
		{
			return !UICamera.GetKey(KeyCode.LeftAlt) && !UICamera.GetKey(KeyCode.RightAlt) && !UICamera.GetKey(KeyCode.LeftControl) && !UICamera.GetKey(KeyCode.RightControl) && !UICamera.GetKey(KeyCode.LeftShift) && !UICamera.GetKey(KeyCode.RightShift);
		}
		return false;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0006DF78 File Offset: 0x0006C378
	protected virtual void Update()
	{
		if (UICamera.inputHasFocus)
		{
			return;
		}
		if (this.keyCode == KeyCode.None || !this.IsModifierActive())
		{
			return;
		}
		bool flag = UICamera.GetKeyDown(this.keyCode);
		bool flag2 = UICamera.GetKeyUp(this.keyCode);
		if (flag)
		{
			this.mPress = true;
		}
		if (this.action == UIKeyBinding.Action.PressAndClick || this.action == UIKeyBinding.Action.All)
		{
			if (flag)
			{
				UICamera.currentTouchID = -1;
				UICamera.currentKey = this.keyCode;
				this.OnBindingPress(true);
			}
			if (this.mPress && flag2)
			{
				UICamera.currentTouchID = -1;
				UICamera.currentKey = this.keyCode;
				this.OnBindingPress(false);
				this.OnBindingClick();
			}
		}
		if ((this.action == UIKeyBinding.Action.Select || this.action == UIKeyBinding.Action.All) && flag2)
		{
			if (this.mIsInput)
			{
				if (!this.mIgnoreUp && !UICamera.inputHasFocus && this.mPress)
				{
					UICamera.selectedObject = base.gameObject;
				}
				this.mIgnoreUp = false;
			}
			else if (this.mPress)
			{
				UICamera.hoveredObject = base.gameObject;
			}
		}
		if (flag2)
		{
			this.mPress = false;
		}
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0006E0BD File Offset: 0x0006C4BD
	protected virtual void OnBindingPress(bool pressed)
	{
		UICamera.Notify(base.gameObject, "OnPress", pressed);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0006E0D5 File Offset: 0x0006C4D5
	protected virtual void OnBindingClick()
	{
		UICamera.Notify(base.gameObject, "OnClick", null);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0006E0E8 File Offset: 0x0006C4E8
	public override string ToString()
	{
		return UIKeyBinding.GetString(this.keyCode, this.modifier);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0006E0FB File Offset: 0x0006C4FB
	public static string GetString(KeyCode keyCode, UIKeyBinding.Modifier modifier)
	{
		return (modifier == UIKeyBinding.Modifier.None) ? keyCode.ToString() : (modifier + "+" + keyCode);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0006E12C File Offset: 0x0006C52C
	public static bool GetKeyCode(string text, out KeyCode key, out UIKeyBinding.Modifier modifier)
	{
		key = KeyCode.None;
		modifier = UIKeyBinding.Modifier.None;
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		if (text.Contains("+"))
		{
			string[] array = text.Split(new char[]
			{
				'+'
			});
			try
			{
				modifier = (UIKeyBinding.Modifier)Enum.Parse(typeof(UIKeyBinding.Modifier), array[0]);
				key = (KeyCode)Enum.Parse(typeof(KeyCode), array[1]);
			}
			catch (Exception)
			{
				return false;
			}
		}
		else
		{
			modifier = UIKeyBinding.Modifier.None;
			try
			{
				key = (KeyCode)Enum.Parse(typeof(KeyCode), text);
			}
			catch (Exception)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0006E1F8 File Offset: 0x0006C5F8
	public static UIKeyBinding.Modifier GetActiveModifier()
	{
		UIKeyBinding.Modifier result = UIKeyBinding.Modifier.None;
		if (UICamera.GetKey(KeyCode.LeftAlt) || UICamera.GetKey(KeyCode.RightAlt))
		{
			result = UIKeyBinding.Modifier.Alt;
		}
		else if (UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift))
		{
			result = UIKeyBinding.Modifier.Shift;
		}
		else if (UICamera.GetKey(KeyCode.LeftControl) || UICamera.GetKey(KeyCode.RightControl))
		{
			result = UIKeyBinding.Modifier.Control;
		}
		return result;
	}

	// Token: 0x04000C22 RID: 3106
	private static List<UIKeyBinding> mList = new List<UIKeyBinding>();

	// Token: 0x04000C23 RID: 3107
	public KeyCode keyCode;

	// Token: 0x04000C24 RID: 3108
	public UIKeyBinding.Modifier modifier;

	// Token: 0x04000C25 RID: 3109
	public UIKeyBinding.Action action;

	// Token: 0x04000C26 RID: 3110
	[NonSerialized]
	private bool mIgnoreUp;

	// Token: 0x04000C27 RID: 3111
	[NonSerialized]
	private bool mIsInput;

	// Token: 0x04000C28 RID: 3112
	[NonSerialized]
	private bool mPress;

	// Token: 0x020001C7 RID: 455
	public enum Action
	{
		// Token: 0x04000C2A RID: 3114
		PressAndClick,
		// Token: 0x04000C2B RID: 3115
		Select,
		// Token: 0x04000C2C RID: 3116
		All
	}

	// Token: 0x020001C8 RID: 456
	public enum Modifier
	{
		// Token: 0x04000C2E RID: 3118
		Any,
		// Token: 0x04000C2F RID: 3119
		Shift,
		// Token: 0x04000C30 RID: 3120
		Control,
		// Token: 0x04000C31 RID: 3121
		Alt,
		// Token: 0x04000C32 RID: 3122
		None
	}
}
