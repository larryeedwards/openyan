using System;
using UnityEngine;

// Token: 0x0200020D RID: 525
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : MonoBehaviour
{
	// Token: 0x0600101A RID: 4122 RVA: 0x00083171 File Offset: 0x00081571
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIScrollView>();
		this.mTrans = base.transform;
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x00083197 File Offset: 0x00081597
	private void Update()
	{
		this.AdvanceTowardsPosition();
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000831A0 File Offset: 0x000815A0
	protected virtual void AdvanceTowardsPosition()
	{
		float deltaTime = RealTime.deltaTime;
		bool flag = false;
		Vector3 localPosition = this.mTrans.localPosition;
		Vector3 vector = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		if ((vector - this.target).sqrMagnitude < 0.01f)
		{
			vector = this.target;
			base.enabled = false;
			flag = true;
		}
		this.mTrans.localPosition = vector;
		Vector3 vector2 = vector - localPosition;
		Vector2 clipOffset = this.mPanel.clipOffset;
		clipOffset.x -= vector2.x;
		clipOffset.y -= vector2.y;
		this.mPanel.clipOffset = clipOffset;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (flag && this.onFinished != null)
		{
			SpringPanel.current = this;
			this.onFinished();
			SpringPanel.current = null;
		}
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x000832AC File Offset: 0x000816AC
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		springPanel.onFinished = null;
		springPanel.enabled = true;
		return springPanel;
	}

	// Token: 0x04000E15 RID: 3605
	public static SpringPanel current;

	// Token: 0x04000E16 RID: 3606
	public Vector3 target = Vector3.zero;

	// Token: 0x04000E17 RID: 3607
	public float strength = 10f;

	// Token: 0x04000E18 RID: 3608
	public SpringPanel.OnFinished onFinished;

	// Token: 0x04000E19 RID: 3609
	private UIPanel mPanel;

	// Token: 0x04000E1A RID: 3610
	private Transform mTrans;

	// Token: 0x04000E1B RID: 3611
	private UIScrollView mDrag;

	// Token: 0x0200020E RID: 526
	// (Invoke) Token: 0x0600101F RID: 4127
	public delegate void OnFinished();
}
