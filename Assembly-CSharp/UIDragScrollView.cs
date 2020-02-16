using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	// Token: 0x06000D32 RID: 3378 RVA: 0x0006CBE8 File Offset: 0x0006AFE8
	private void OnEnable()
	{
		this.mTrans = base.transform;
		if (this.scrollView == null && this.draggablePanel != null)
		{
			this.scrollView = this.draggablePanel;
			this.draggablePanel = null;
		}
		if (this.mStarted && (this.mAutoFind || this.mScroll == null))
		{
			this.FindScrollView();
		}
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0006CC63 File Offset: 0x0006B063
	private void Start()
	{
		this.mStarted = true;
		this.FindScrollView();
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0006CC74 File Offset: 0x0006B074
	private void FindScrollView()
	{
		UIScrollView uiscrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
		if (this.scrollView == null || (this.mAutoFind && uiscrollView != this.scrollView))
		{
			this.scrollView = uiscrollView;
			this.mAutoFind = true;
		}
		else if (this.scrollView == uiscrollView)
		{
			this.mAutoFind = true;
		}
		this.mScroll = this.scrollView;
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0006CCF4 File Offset: 0x0006B0F4
	private void OnDisable()
	{
		if (this.mPressed && this.mScroll != null && this.mScroll.GetComponentInChildren<UIWrapContent>() == null)
		{
			this.mScroll.Press(false);
			this.mScroll = null;
		}
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0006CD48 File Offset: 0x0006B148
	private void OnPress(bool pressed)
	{
		this.mPressed = pressed;
		if (this.mAutoFind && this.mScroll != this.scrollView)
		{
			this.mScroll = this.scrollView;
			this.mAutoFind = false;
		}
		if (this.scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.scrollView.Press(pressed);
			if (!pressed && this.mAutoFind)
			{
				this.scrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
				this.mScroll = this.scrollView;
			}
		}
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0006CDF5 File Offset: 0x0006B1F5
	private void OnDrag(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Drag();
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0006CE1D File Offset: 0x0006B21D
	private void OnScroll(float delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Scroll(delta);
		}
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0006CE46 File Offset: 0x0006B246
	public void OnPan(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.OnPan(delta);
		}
	}

	// Token: 0x04000BE1 RID: 3041
	public UIScrollView scrollView;

	// Token: 0x04000BE2 RID: 3042
	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	// Token: 0x04000BE3 RID: 3043
	private Transform mTrans;

	// Token: 0x04000BE4 RID: 3044
	private UIScrollView mScroll;

	// Token: 0x04000BE5 RID: 3045
	private bool mAutoFind;

	// Token: 0x04000BE6 RID: 3046
	private bool mStarted;

	// Token: 0x04000BE7 RID: 3047
	[NonSerialized]
	private bool mPressed;
}
