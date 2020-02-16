using System;
using UnityEngine;

// Token: 0x02000235 RID: 565
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06001173 RID: 4467 RVA: 0x0008C134 File Offset: 0x0008A534
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

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06001174 RID: 4468 RVA: 0x0008C159 File Offset: 0x0008A559
	// (set) Token: 0x06001175 RID: 4469 RVA: 0x0008C161 File Offset: 0x0008A561
	[Obsolete("Use 'value' instead")]
	public int height
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

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06001176 RID: 4470 RVA: 0x0008C16A File Offset: 0x0008A56A
	// (set) Token: 0x06001177 RID: 4471 RVA: 0x0008C177 File Offset: 0x0008A577
	public int value
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0008C188 File Offset: 0x0008A588
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

	// Token: 0x06001179 RID: 4473 RVA: 0x0008C20C File Offset: 0x0008A60C
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration, 0f);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0008C25D File Offset: 0x0008A65D
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0008C26B File Offset: 0x0008A66B
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0008C279 File Offset: 0x0008A679
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0008C287 File Offset: 0x0008A687
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04000F08 RID: 3848
	public int from = 100;

	// Token: 0x04000F09 RID: 3849
	public int to = 100;

	// Token: 0x04000F0A RID: 3850
	public bool updateTable;

	// Token: 0x04000F0B RID: 3851
	private UIWidget mWidget;

	// Token: 0x04000F0C RID: 3852
	private UITable mTable;
}
