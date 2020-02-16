using System;
using UnityEngine;

// Token: 0x02000240 RID: 576
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	// Token: 0x1700022E RID: 558
	// (get) Token: 0x060011CA RID: 4554 RVA: 0x0008D355 File Offset: 0x0008B755
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x060011CB RID: 4555 RVA: 0x0008D37A File Offset: 0x0008B77A
	// (set) Token: 0x060011CC RID: 4556 RVA: 0x0008D382 File Offset: 0x0008B782
	[Obsolete("Use 'value' instead")]
	public int width
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

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x060011CD RID: 4557 RVA: 0x0008D38B File Offset: 0x0008B78B
	// (set) Token: 0x060011CE RID: 4558 RVA: 0x0008D398 File Offset: 0x0008B798
	public int value
	{
		get
		{
			return this.cachedWidget.width;
		}
		set
		{
			this.cachedWidget.width = value;
		}
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x0008D3A8 File Offset: 0x0008B7A8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0008D42C File Offset: 0x0008B82C
	public static TweenWidth Begin(UIWidget widget, float duration, int width)
	{
		TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(widget.gameObject, duration, 0f);
		tweenWidth.from = widget.width;
		tweenWidth.to = width;
		if (duration <= 0f)
		{
			tweenWidth.Sample(1f, true);
			tweenWidth.enabled = false;
		}
		return tweenWidth;
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x0008D47D File Offset: 0x0008B87D
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x0008D48B File Offset: 0x0008B88B
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x0008D499 File Offset: 0x0008B899
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x0008D4A7 File Offset: 0x0008B8A7
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000F3F RID: 3903
	public int from = 100;

	// Token: 0x04000F40 RID: 3904
	public int to = 100;

	// Token: 0x04000F41 RID: 3905
	public bool updateTable;

	// Token: 0x04000F42 RID: 3906
	private UIWidget mWidget;

	// Token: 0x04000F43 RID: 3907
	private UITable mTable;
}
