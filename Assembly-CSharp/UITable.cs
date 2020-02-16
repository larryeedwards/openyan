using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001E3 RID: 483
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	// Token: 0x170001A0 RID: 416
	// (set) Token: 0x06000E5E RID: 3678 RVA: 0x0007451A File Offset: 0x0007291A
	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x00074530 File Offset: 0x00072930
	public List<Transform> GetChildList()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && NGUITools.GetActive(child.gameObject)))
			{
				list.Add(child);
			}
		}
		if (this.sorting != UITable.Sorting.None)
		{
			if (this.sorting == UITable.Sorting.Alphabetic)
			{
				List<Transform> list2 = list;
				if (UITable.<>f__mg$cache0 == null)
				{
					UITable.<>f__mg$cache0 = new Comparison<Transform>(UIGrid.SortByName);
				}
				list2.Sort(UITable.<>f__mg$cache0);
			}
			else if (this.sorting == UITable.Sorting.Horizontal)
			{
				List<Transform> list3 = list;
				if (UITable.<>f__mg$cache1 == null)
				{
					UITable.<>f__mg$cache1 = new Comparison<Transform>(UIGrid.SortHorizontal);
				}
				list3.Sort(UITable.<>f__mg$cache1);
			}
			else if (this.sorting == UITable.Sorting.Vertical)
			{
				List<Transform> list4 = list;
				if (UITable.<>f__mg$cache2 == null)
				{
					UITable.<>f__mg$cache2 = new Comparison<Transform>(UIGrid.SortVertical);
				}
				list4.Sort(UITable.<>f__mg$cache2);
			}
			else if (this.onCustomSort != null)
			{
				list.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(list);
			}
		}
		return list;
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00074661 File Offset: 0x00072A61
	protected virtual void Sort(List<Transform> list)
	{
		if (UITable.<>f__mg$cache3 == null)
		{
			UITable.<>f__mg$cache3 = new Comparison<Transform>(UIGrid.SortByName);
		}
		list.Sort(UITable.<>f__mg$cache3);
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00074686 File Offset: 0x00072A86
	protected virtual void Start()
	{
		this.Init();
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0007469B File Offset: 0x00072A9B
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x000746B5 File Offset: 0x00072AB5
	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x000746CF File Offset: 0x00072ACF
	private void OnValidate()
	{
		if (!Application.isPlaying && NGUITools.GetActive(this))
		{
			this.Reposition();
		}
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x000746EC File Offset: 0x00072AEC
	protected void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !this.hideInactive);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.cellAlignment);
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x -= Mathf.Lerp(0f, bounds2.max.x - bounds2.min.x - bounds3.max.x + bounds3.min.x, pivotOffset.x) - this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += Mathf.Lerp(bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y, 0f, pivotOffset.y) - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + bounds2.extents.y - bounds2.center.y;
				localPosition.y -= Mathf.Lerp(0f, bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y, pivotOffset.y) - this.padding.y;
			}
			num += bounds3.size.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			Bounds bounds5 = NGUIMath.CalculateRelativeWidgetBounds(base.transform);
			float num7 = Mathf.Lerp(0f, bounds5.size.x, pivotOffset.x);
			float num8 = Mathf.Lerp(-bounds5.size.y, 0f, pivotOffset.y);
			Transform transform3 = base.transform;
			for (int k = 0; k < transform3.childCount; k++)
			{
				Transform child = transform3.GetChild(k);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
					SpringPosition springPosition = component;
					springPosition.target.x = springPosition.target.x - num7;
					SpringPosition springPosition2 = component;
					springPosition2.target.y = springPosition2.target.y - num8;
					component.enabled = true;
				}
				else
				{
					Vector3 localPosition2 = child.localPosition;
					localPosition2.x -= num7;
					localPosition2.y -= num8;
					child.localPosition = localPosition2;
				}
			}
		}
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00074C18 File Offset: 0x00073018
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.Init();
		}
		this.mReposition = false;
		Transform transform = base.transform;
		List<Transform> childList = this.GetChildList();
		if (childList.Count > 0)
		{
			this.RepositionVariableSize(childList);
		}
		if (this.keepWithinPanel && this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x04000D0E RID: 3342
	public int columns;

	// Token: 0x04000D0F RID: 3343
	public UITable.Direction direction;

	// Token: 0x04000D10 RID: 3344
	public UITable.Sorting sorting;

	// Token: 0x04000D11 RID: 3345
	public UIWidget.Pivot pivot;

	// Token: 0x04000D12 RID: 3346
	public UIWidget.Pivot cellAlignment;

	// Token: 0x04000D13 RID: 3347
	public bool hideInactive = true;

	// Token: 0x04000D14 RID: 3348
	public bool keepWithinPanel;

	// Token: 0x04000D15 RID: 3349
	public Vector2 padding = Vector2.zero;

	// Token: 0x04000D16 RID: 3350
	public UITable.OnReposition onReposition;

	// Token: 0x04000D17 RID: 3351
	public Comparison<Transform> onCustomSort;

	// Token: 0x04000D18 RID: 3352
	protected UIPanel mPanel;

	// Token: 0x04000D19 RID: 3353
	protected bool mInitDone;

	// Token: 0x04000D1A RID: 3354
	protected bool mReposition;

	// Token: 0x04000D1B RID: 3355
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache0;

	// Token: 0x04000D1C RID: 3356
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache1;

	// Token: 0x04000D1D RID: 3357
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache2;

	// Token: 0x04000D1E RID: 3358
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache3;

	// Token: 0x020001E4 RID: 484
	// (Invoke) Token: 0x06000E68 RID: 3688
	public delegate void OnReposition();

	// Token: 0x020001E5 RID: 485
	public enum Direction
	{
		// Token: 0x04000D20 RID: 3360
		Down,
		// Token: 0x04000D21 RID: 3361
		Up
	}

	// Token: 0x020001E6 RID: 486
	public enum Sorting
	{
		// Token: 0x04000D23 RID: 3363
		None,
		// Token: 0x04000D24 RID: 3364
		Alphabetic,
		// Token: 0x04000D25 RID: 3365
		Horizontal,
		// Token: 0x04000D26 RID: 3366
		Vertical,
		// Token: 0x04000D27 RID: 3367
		Custom
	}
}
