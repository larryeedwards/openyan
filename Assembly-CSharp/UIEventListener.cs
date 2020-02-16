using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06001074 RID: 4212 RVA: 0x0008A518 File Offset: 0x00088918
	private bool isColliderEnabled
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

	// Token: 0x06001075 RID: 4213 RVA: 0x0008A55B File Offset: 0x0008895B
	private void OnSubmit()
	{
		if (this.isColliderEnabled && this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0008A584 File Offset: 0x00088984
	private void OnClick()
	{
		if (this.isColliderEnabled && this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0008A5AD File Offset: 0x000889AD
	private void OnDoubleClick()
	{
		if (this.isColliderEnabled && this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0008A5D6 File Offset: 0x000889D6
	private void OnHover(bool isOver)
	{
		if (this.isColliderEnabled && this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0008A600 File Offset: 0x00088A00
	private void OnPress(bool isPressed)
	{
		if (this.isColliderEnabled && this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0008A62A File Offset: 0x00088A2A
	private void OnSelect(bool selected)
	{
		if (this.isColliderEnabled && this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0008A654 File Offset: 0x00088A54
	private void OnScroll(float delta)
	{
		if (this.isColliderEnabled && this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0008A67E File Offset: 0x00088A7E
	private void OnDragStart()
	{
		if (this.onDragStart != null)
		{
			this.onDragStart(base.gameObject);
		}
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0008A69C File Offset: 0x00088A9C
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x0008A6BB File Offset: 0x00088ABB
	private void OnDragOver()
	{
		if (this.isColliderEnabled && this.onDragOver != null)
		{
			this.onDragOver(base.gameObject);
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x0008A6E4 File Offset: 0x00088AE4
	private void OnDragOut()
	{
		if (this.isColliderEnabled && this.onDragOut != null)
		{
			this.onDragOut(base.gameObject);
		}
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x0008A70D File Offset: 0x00088B0D
	private void OnDragEnd()
	{
		if (this.onDragEnd != null)
		{
			this.onDragEnd(base.gameObject);
		}
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x0008A72B File Offset: 0x00088B2B
	private void OnDrop(GameObject go)
	{
		if (this.isColliderEnabled && this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x0008A755 File Offset: 0x00088B55
	private void OnKey(KeyCode key)
	{
		if (this.isColliderEnabled && this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x06001083 RID: 4227 RVA: 0x0008A77F File Offset: 0x00088B7F
	private void OnTooltip(bool show)
	{
		if (this.isColliderEnabled && this.onTooltip != null)
		{
			this.onTooltip(base.gameObject, show);
		}
	}

	// Token: 0x06001084 RID: 4228 RVA: 0x0008A7AC File Offset: 0x00088BAC
	public void Clear()
	{
		this.onSubmit = null;
		this.onClick = null;
		this.onDoubleClick = null;
		this.onHover = null;
		this.onPress = null;
		this.onSelect = null;
		this.onScroll = null;
		this.onDragStart = null;
		this.onDrag = null;
		this.onDragOver = null;
		this.onDragOut = null;
		this.onDragEnd = null;
		this.onDrop = null;
		this.onKey = null;
		this.onTooltip = null;
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x0008A824 File Offset: 0x00088C24
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x04000E77 RID: 3703
	public object parameter;

	// Token: 0x04000E78 RID: 3704
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04000E79 RID: 3705
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04000E7A RID: 3706
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x04000E7B RID: 3707
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x04000E7C RID: 3708
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x04000E7D RID: 3709
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x04000E7E RID: 3710
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x04000E7F RID: 3711
	public UIEventListener.VoidDelegate onDragStart;

	// Token: 0x04000E80 RID: 3712
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x04000E81 RID: 3713
	public UIEventListener.VoidDelegate onDragOver;

	// Token: 0x04000E82 RID: 3714
	public UIEventListener.VoidDelegate onDragOut;

	// Token: 0x04000E83 RID: 3715
	public UIEventListener.VoidDelegate onDragEnd;

	// Token: 0x04000E84 RID: 3716
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x04000E85 RID: 3717
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x04000E86 RID: 3718
	public UIEventListener.BoolDelegate onTooltip;

	// Token: 0x0200021A RID: 538
	// (Invoke) Token: 0x06001087 RID: 4231
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x0200021B RID: 539
	// (Invoke) Token: 0x0600108B RID: 4235
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x0200021C RID: 540
	// (Invoke) Token: 0x0600108F RID: 4239
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x0200021D RID: 541
	// (Invoke) Token: 0x06001093 RID: 4243
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x0200021E RID: 542
	// (Invoke) Token: 0x06001097 RID: 4247
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x0200021F RID: 543
	// (Invoke) Token: 0x0600109B RID: 4251
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
