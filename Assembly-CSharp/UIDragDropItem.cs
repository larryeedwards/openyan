using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001B7 RID: 439
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	// Token: 0x06000CFF RID: 3327 RVA: 0x00067571 File Offset: 0x00065971
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
		this.mCollider = base.gameObject.GetComponent<Collider>();
		this.mCollider2D = base.gameObject.GetComponent<Collider2D>();
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x000675A1 File Offset: 0x000659A1
	protected virtual void OnEnable()
	{
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x000675A3 File Offset: 0x000659A3
	protected virtual void OnDisable()
	{
		if (this.mDragging)
		{
			this.StopDragging(UICamera.hoveredObject);
		}
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x000675BB File Offset: 0x000659BB
	protected virtual void Start()
	{
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x000675D8 File Offset: 0x000659D8
	protected virtual void OnPress(bool isPressed)
	{
		if (!this.interactable || UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (isPressed)
		{
			if (!this.mPressed)
			{
				this.mTouch = UICamera.currentTouch;
				this.mDragStartTime = RealTime.time + this.pressAndHoldDelay;
				this.mPressed = true;
			}
		}
		else if (this.mPressed && this.mTouch == UICamera.currentTouch)
		{
			this.mPressed = false;
			this.mTouch = null;
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0006766C File Offset: 0x00065A6C
	protected virtual void Update()
	{
		if (this.restriction == UIDragDropItem.Restriction.PressAndHold && this.mPressed && !this.mDragging && this.mDragStartTime < RealTime.time)
		{
			this.StartDragging();
		}
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x000676A8 File Offset: 0x00065AA8
	protected virtual void OnDragStart()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (this.restriction != UIDragDropItem.Restriction.None)
		{
			if (this.restriction == UIDragDropItem.Restriction.Horizontal)
			{
				Vector2 totalDelta = this.mTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.Vertical)
			{
				Vector2 totalDelta2 = this.mTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.PressAndHold)
			{
				return;
			}
		}
		this.StartDragging();
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x00067774 File Offset: 0x00065B74
	public virtual void StartDragging()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!this.mDragging)
		{
			if (this.cloneOnDrag)
			{
				this.mPressed = false;
				GameObject gameObject = base.transform.parent.gameObject.AddChild(base.gameObject);
				gameObject.transform.localPosition = base.transform.localPosition;
				gameObject.transform.localRotation = base.transform.localRotation;
				gameObject.transform.localScale = base.transform.localScale;
				UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
				if (component != null)
				{
					component.defaultColor = base.GetComponent<UIButtonColor>().defaultColor;
				}
				if (this.mTouch != null && this.mTouch.pressed == base.gameObject)
				{
					this.mTouch.current = gameObject;
					this.mTouch.pressed = gameObject;
					this.mTouch.dragged = gameObject;
					this.mTouch.last = gameObject;
				}
				UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
				component2.mTouch = this.mTouch;
				component2.mPressed = true;
				component2.mDragging = true;
				component2.Start();
				component2.OnClone(base.gameObject);
				component2.OnDragDropStart();
				if (UICamera.currentTouch == null)
				{
					UICamera.currentTouch = this.mTouch;
				}
				this.mTouch = null;
				UICamera.Notify(base.gameObject, "OnPress", false);
				UICamera.Notify(base.gameObject, "OnHover", false);
			}
			else
			{
				this.mDragging = true;
				this.OnDragDropStart();
			}
		}
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x00067915 File Offset: 0x00065D15
	protected virtual void OnClone(GameObject original)
	{
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x00067918 File Offset: 0x00065D18
	protected virtual void OnDrag(Vector2 delta)
	{
		if (!this.interactable)
		{
			return;
		}
		if (!this.mDragging || !base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (this.mRoot != null)
		{
			this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
		}
		else
		{
			this.OnDragDropMove(delta);
		}
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0006798C File Offset: 0x00065D8C
	protected virtual void OnDragEnd()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		this.StopDragging(UICamera.hoveredObject);
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x000679C1 File Offset: 0x00065DC1
	public void StopDragging(GameObject go = null)
	{
		if (this.mDragging)
		{
			this.mDragging = false;
			this.OnDragDropRelease(go);
		}
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x000679DC File Offset: 0x00065DDC
	protected virtual void OnDragDropStart()
	{
		if (!UIDragDropItem.draggedItems.Contains(this))
		{
			UIDragDropItem.draggedItems.Add(this);
		}
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = false;
		}
		if (this.mButton != null)
		{
			this.mButton.isEnabled = false;
		}
		else if (this.mCollider != null)
		{
			this.mCollider.enabled = false;
		}
		else if (this.mCollider2D != null)
		{
			this.mCollider2D.enabled = false;
		}
		this.mParent = this.mTrans.parent;
		this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
		this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
		this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
		if (UIDragDropRoot.root != null)
		{
			this.mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		localPosition.z = 0f;
		this.mTrans.localPosition = localPosition;
		TweenPosition component = base.GetComponent<TweenPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = base.GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if (this.mTable != null)
		{
			this.mTable.repositionNow = true;
		}
		if (this.mGrid != null)
		{
			this.mGrid.repositionNow = true;
		}
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x00067B83 File Offset: 0x00065F83
	protected virtual void OnDragDropMove(Vector2 delta)
	{
		this.mTrans.localPosition += this.mTrans.InverseTransformDirection(delta);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x00067BAC File Offset: 0x00065FAC
	protected virtual void OnDragDropRelease(GameObject surface)
	{
		if (!this.cloneOnDrag)
		{
			UIDragScrollView[] componentsInChildren = base.GetComponentsInChildren<UIDragScrollView>();
			foreach (UIDragScrollView uidragScrollView in componentsInChildren)
			{
				uidragScrollView.scrollView = null;
			}
			if (this.mButton != null)
			{
				this.mButton.isEnabled = true;
			}
			else if (this.mCollider != null)
			{
				this.mCollider.enabled = true;
			}
			else if (this.mCollider2D != null)
			{
				this.mCollider2D.enabled = true;
			}
			UIDragDropContainer uidragDropContainer = (!surface) ? null : NGUITools.FindInParents<UIDragDropContainer>(surface);
			if (uidragDropContainer != null)
			{
				this.mTrans.parent = ((!(uidragDropContainer.reparentTarget != null)) ? uidragDropContainer.transform : uidragDropContainer.reparentTarget);
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.z = 0f;
				this.mTrans.localPosition = localPosition;
			}
			else
			{
				this.mTrans.parent = this.mParent;
			}
			this.mParent = this.mTrans.parent;
			this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
			this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
			if (this.mDragScrollView != null)
			{
				base.Invoke("EnableDragScrollView", 0.001f);
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if (this.mTable != null)
			{
				this.mTable.repositionNow = true;
			}
			if (this.mGrid != null)
			{
				this.mGrid.repositionNow = true;
			}
		}
		else
		{
			NGUITools.Destroy(base.gameObject);
		}
		this.OnDragDropEnd();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x00067D8E File Offset: 0x0006618E
	protected virtual void OnDragDropEnd()
	{
		UIDragDropItem.draggedItems.Remove(this);
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x00067D9C File Offset: 0x0006619C
	protected void EnableDragScrollView()
	{
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = true;
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x00067DBB File Offset: 0x000661BB
	protected void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			this.StopDragging(null);
		}
	}

	// Token: 0x04000B98 RID: 2968
	public UIDragDropItem.Restriction restriction;

	// Token: 0x04000B99 RID: 2969
	public bool cloneOnDrag;

	// Token: 0x04000B9A RID: 2970
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x04000B9B RID: 2971
	public bool interactable = true;

	// Token: 0x04000B9C RID: 2972
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x04000B9D RID: 2973
	[NonSerialized]
	protected Transform mParent;

	// Token: 0x04000B9E RID: 2974
	[NonSerialized]
	protected Collider mCollider;

	// Token: 0x04000B9F RID: 2975
	[NonSerialized]
	protected Collider2D mCollider2D;

	// Token: 0x04000BA0 RID: 2976
	[NonSerialized]
	protected UIButton mButton;

	// Token: 0x04000BA1 RID: 2977
	[NonSerialized]
	protected UIRoot mRoot;

	// Token: 0x04000BA2 RID: 2978
	[NonSerialized]
	protected UIGrid mGrid;

	// Token: 0x04000BA3 RID: 2979
	[NonSerialized]
	protected UITable mTable;

	// Token: 0x04000BA4 RID: 2980
	[NonSerialized]
	protected float mDragStartTime;

	// Token: 0x04000BA5 RID: 2981
	[NonSerialized]
	protected UIDragScrollView mDragScrollView;

	// Token: 0x04000BA6 RID: 2982
	[NonSerialized]
	protected bool mPressed;

	// Token: 0x04000BA7 RID: 2983
	[NonSerialized]
	protected bool mDragging;

	// Token: 0x04000BA8 RID: 2984
	[NonSerialized]
	protected UICamera.MouseOrTouch mTouch;

	// Token: 0x04000BA9 RID: 2985
	public static List<UIDragDropItem> draggedItems = new List<UIDragDropItem>();

	// Token: 0x020001B8 RID: 440
	public enum Restriction
	{
		// Token: 0x04000BAB RID: 2987
		None,
		// Token: 0x04000BAC RID: 2988
		Horizontal,
		// Token: 0x04000BAD RID: 2989
		Vertical,
		// Token: 0x04000BAE RID: 2990
		PressAndHold
	}
}
