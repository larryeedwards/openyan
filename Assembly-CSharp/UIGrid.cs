using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001C1 RID: 449
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x17000178 RID: 376
	// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0006D39C File Offset: 0x0006B79C
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

	// Token: 0x06000D52 RID: 3410 RVA: 0x0006D3B4 File Offset: 0x0006B7B4
	public List<Transform> GetChildList()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && child.gameObject.activeSelf))
			{
				list.Add(child);
			}
		}
		if (this.sorting != UIGrid.Sorting.None && this.arrangement != UIGrid.Arrangement.CellSnap)
		{
			if (this.sorting == UIGrid.Sorting.Alphabetic)
			{
				List<Transform> list2 = list;
				if (UIGrid.<>f__mg$cache0 == null)
				{
					UIGrid.<>f__mg$cache0 = new Comparison<Transform>(UIGrid.SortByName);
				}
				list2.Sort(UIGrid.<>f__mg$cache0);
			}
			else if (this.sorting == UIGrid.Sorting.Horizontal)
			{
				List<Transform> list3 = list;
				if (UIGrid.<>f__mg$cache1 == null)
				{
					UIGrid.<>f__mg$cache1 = new Comparison<Transform>(UIGrid.SortHorizontal);
				}
				list3.Sort(UIGrid.<>f__mg$cache1);
			}
			else if (this.sorting == UIGrid.Sorting.Vertical)
			{
				List<Transform> list4 = list;
				if (UIGrid.<>f__mg$cache2 == null)
				{
					UIGrid.<>f__mg$cache2 = new Comparison<Transform>(UIGrid.SortVertical);
				}
				list4.Sort(UIGrid.<>f__mg$cache2);
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

	// Token: 0x06000D53 RID: 3411 RVA: 0x0006D4F4 File Offset: 0x0006B8F4
	public Transform GetChild(int index)
	{
		List<Transform> childList = this.GetChildList();
		return (index >= childList.Count) ? null : childList[index];
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0006D521 File Offset: 0x0006B921
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0006D52F File Offset: 0x0006B92F
	[Obsolete("Use gameObject.AddChild or transform.parent = gridTransform")]
	public void AddChild(Transform trans)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0006D555 File Offset: 0x0006B955
	[Obsolete("Use gameObject.AddChild or transform.parent = gridTransform")]
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0006D57C File Offset: 0x0006B97C
	public bool RemoveChild(Transform t)
	{
		List<Transform> childList = this.GetChildList();
		if (childList.Remove(t))
		{
			this.ResetPosition(childList);
			return true;
		}
		return false;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0006D5A6 File Offset: 0x0006B9A6
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0006D5C0 File Offset: 0x0006B9C0
	protected virtual void Start()
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		bool flag = this.animateSmoothly;
		this.animateSmoothly = false;
		this.Reposition();
		this.animateSmoothly = flag;
		base.enabled = false;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0006D600 File Offset: 0x0006BA00
	protected virtual void Update()
	{
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0006D60F File Offset: 0x0006BA0F
	private void OnValidate()
	{
		if (!Application.isPlaying && NGUITools.GetActive(this))
		{
			this.Reposition();
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0006D62C File Offset: 0x0006BA2C
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0006D640 File Offset: 0x0006BA40
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0006D670 File Offset: 0x0006BA70
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0006D69E File Offset: 0x0006BA9E
	protected virtual void Sort(List<Transform> list)
	{
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0006D6A0 File Offset: 0x0006BAA0
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(base.gameObject))
		{
			this.Init();
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGrid.Sorting.None)
			{
				this.sorting = UIGrid.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this);
		}
		List<Transform> childList = this.GetChildList();
		this.ResetPosition(childList);
		if (this.keepWithinPanel)
		{
			this.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0006D738 File Offset: 0x0006BB38
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0006D788 File Offset: 0x0006BB88
	protected virtual void ResetPosition(List<Transform> list)
	{
		this.mReposition = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = base.transform;
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			Transform transform2 = list[i];
			Vector3 vector = transform2.localPosition;
			float z = vector.z;
			if (this.arrangement == UIGrid.Arrangement.CellSnap)
			{
				if (this.cellWidth > 0f)
				{
					vector.x = Mathf.Round(vector.x / this.cellWidth) * this.cellWidth;
				}
				if (this.cellHeight > 0f)
				{
					vector.y = Mathf.Round(vector.y / this.cellHeight) * this.cellHeight;
				}
			}
			else
			{
				vector = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
			}
			if (this.animateSmoothly && Application.isPlaying && (this.pivot != UIWidget.Pivot.TopLeft || Vector3.SqrMagnitude(transform2.localPosition - vector) >= 0.0001f))
			{
				SpringPosition springPosition = SpringPosition.Begin(transform2.gameObject, vector, 15f);
				springPosition.updateScrollView = true;
				springPosition.ignoreTimeScale = true;
			}
			else
			{
				transform2.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= this.maxPerLine && this.maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
			i++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGrid.Arrangement.Horizontal)
			{
				num5 = Mathf.Lerp(0f, (float)num3 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num4) * this.cellHeight, 0f, pivotOffset.y);
			}
			else
			{
				num5 = Mathf.Lerp(0f, (float)num4 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num3) * this.cellHeight, 0f, pivotOffset.y);
			}
			for (int j = 0; j < transform.childCount; j++)
			{
				Transform child = transform.GetChild(j);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
					SpringPosition springPosition2 = component;
					springPosition2.target.x = springPosition2.target.x - num5;
					SpringPosition springPosition3 = component;
					springPosition3.target.y = springPosition3.target.y - num6;
					component.enabled = true;
				}
				else
				{
					Vector3 localPosition = child.localPosition;
					localPosition.x -= num5;
					localPosition.y -= num6;
					child.localPosition = localPosition;
				}
			}
		}
	}

	// Token: 0x04000C00 RID: 3072
	public UIGrid.Arrangement arrangement;

	// Token: 0x04000C01 RID: 3073
	public UIGrid.Sorting sorting;

	// Token: 0x04000C02 RID: 3074
	public UIWidget.Pivot pivot;

	// Token: 0x04000C03 RID: 3075
	public int maxPerLine;

	// Token: 0x04000C04 RID: 3076
	public float cellWidth = 200f;

	// Token: 0x04000C05 RID: 3077
	public float cellHeight = 200f;

	// Token: 0x04000C06 RID: 3078
	public bool animateSmoothly;

	// Token: 0x04000C07 RID: 3079
	public bool hideInactive;

	// Token: 0x04000C08 RID: 3080
	public bool keepWithinPanel;

	// Token: 0x04000C09 RID: 3081
	public UIGrid.OnReposition onReposition;

	// Token: 0x04000C0A RID: 3082
	public Comparison<Transform> onCustomSort;

	// Token: 0x04000C0B RID: 3083
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x04000C0C RID: 3084
	protected bool mReposition;

	// Token: 0x04000C0D RID: 3085
	protected UIPanel mPanel;

	// Token: 0x04000C0E RID: 3086
	protected bool mInitDone;

	// Token: 0x04000C0F RID: 3087
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache0;

	// Token: 0x04000C10 RID: 3088
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache1;

	// Token: 0x04000C11 RID: 3089
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache2;

	// Token: 0x020001C2 RID: 450
	// (Invoke) Token: 0x06000D64 RID: 3428
	public delegate void OnReposition();

	// Token: 0x020001C3 RID: 451
	public enum Arrangement
	{
		// Token: 0x04000C13 RID: 3091
		Horizontal,
		// Token: 0x04000C14 RID: 3092
		Vertical,
		// Token: 0x04000C15 RID: 3093
		CellSnap
	}

	// Token: 0x020001C4 RID: 452
	public enum Sorting
	{
		// Token: 0x04000C17 RID: 3095
		None,
		// Token: 0x04000C18 RID: 3096
		Alphabetic,
		// Token: 0x04000C19 RID: 3097
		Horizontal,
		// Token: 0x04000C1A RID: 3098
		Vertical,
		// Token: 0x04000C1B RID: 3099
		Custom
	}
}
