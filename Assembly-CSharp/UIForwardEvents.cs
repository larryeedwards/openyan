using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x06000D47 RID: 3399 RVA: 0x0006D1B8 File Offset: 0x0006B5B8
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0006D1ED File Offset: 0x0006B5ED
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0006D222 File Offset: 0x0006B622
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0006D251 File Offset: 0x0006B651
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0006D280 File Offset: 0x0006B680
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0006D2B5 File Offset: 0x0006B6B5
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0006D2EA File Offset: 0x0006B6EA
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0006D31A File Offset: 0x0006B71A
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0006D349 File Offset: 0x0006B749
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x04000BF6 RID: 3062
	public GameObject target;

	// Token: 0x04000BF7 RID: 3063
	public bool onHover;

	// Token: 0x04000BF8 RID: 3064
	public bool onPress;

	// Token: 0x04000BF9 RID: 3065
	public bool onClick;

	// Token: 0x04000BFA RID: 3066
	public bool onDoubleClick;

	// Token: 0x04000BFB RID: 3067
	public bool onSelect;

	// Token: 0x04000BFC RID: 3068
	public bool onDrag;

	// Token: 0x04000BFD RID: 3069
	public bool onDrop;

	// Token: 0x04000BFE RID: 3070
	public bool onSubmit;

	// Token: 0x04000BFF RID: 3071
	public bool onScroll;
}
