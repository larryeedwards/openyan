using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BF RID: 447
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0006CF14 File Offset: 0x0006B314
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

	// Token: 0x06000D3C RID: 3388 RVA: 0x0006CF58 File Offset: 0x0006B358
	private void OnHover(bool isOver)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (isOver)
		{
			EventDelegate.Execute(this.onHoverOver);
		}
		else
		{
			EventDelegate.Execute(this.onHoverOut);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0006CFB0 File Offset: 0x0006B3B0
	private void OnPress(bool pressed)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (pressed)
		{
			EventDelegate.Execute(this.onPress);
		}
		else
		{
			EventDelegate.Execute(this.onRelease);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0006D008 File Offset: 0x0006B408
	private void OnSelect(bool selected)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (selected)
		{
			EventDelegate.Execute(this.onSelect);
		}
		else
		{
			EventDelegate.Execute(this.onDeselect);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0006D05E File Offset: 0x0006B45E
	private void OnClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0006D093 File Offset: 0x0006B493
	private void OnDoubleClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDoubleClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0006D0C8 File Offset: 0x0006B4C8
	private void OnDragStart()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragStart);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0006D0F2 File Offset: 0x0006B4F2
	private void OnDragEnd()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragEnd);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0006D11C File Offset: 0x0006B51C
	private void OnDragOver(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOver);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0006D151 File Offset: 0x0006B551
	private void OnDragOut(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOut);
		UIEventTrigger.current = null;
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0006D186 File Offset: 0x0006B586
	private void OnDrag(Vector2 delta)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDrag);
		UIEventTrigger.current = null;
	}

	// Token: 0x04000BE8 RID: 3048
	public static UIEventTrigger current;

	// Token: 0x04000BE9 RID: 3049
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x04000BEA RID: 3050
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x04000BEB RID: 3051
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x04000BEC RID: 3052
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x04000BED RID: 3053
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x04000BEE RID: 3054
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x04000BEF RID: 3055
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04000BF0 RID: 3056
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x04000BF1 RID: 3057
	public List<EventDelegate> onDragStart = new List<EventDelegate>();

	// Token: 0x04000BF2 RID: 3058
	public List<EventDelegate> onDragEnd = new List<EventDelegate>();

	// Token: 0x04000BF3 RID: 3059
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x04000BF4 RID: 3060
	public List<EventDelegate> onDragOut = new List<EventDelegate>();

	// Token: 0x04000BF5 RID: 3061
	public List<EventDelegate> onDrag = new List<EventDelegate>();
}
