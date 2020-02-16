using System;
using UnityEngine;

// Token: 0x020001A4 RID: 420
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Interaction/Envelop Content")]
public class EnvelopContent : MonoBehaviour
{
	// Token: 0x06000C91 RID: 3217 RVA: 0x00068BA0 File Offset: 0x00066FA0
	private void Start()
	{
		this.mStarted = true;
		this.Execute();
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x00068BAF File Offset: 0x00066FAF
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.Execute();
		}
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x00068BC4 File Offset: 0x00066FC4
	[ContextMenu("Execute")]
	public void Execute()
	{
		if (this.targetRoot == base.transform)
		{
			Debug.LogError("Target Root object cannot be the same object that has Envelop Content. Make it a sibling instead.", this);
		}
		else if (NGUITools.IsChild(this.targetRoot, base.transform))
		{
			Debug.LogError("Target Root object should not be a parent of Envelop Content. Make it a sibling instead.", this);
		}
		else
		{
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(base.transform.parent, this.targetRoot, !this.ignoreDisabled, true);
			float num = bounds.min.x + (float)this.padLeft;
			float num2 = bounds.min.y + (float)this.padBottom;
			float num3 = bounds.max.x + (float)this.padRight;
			float num4 = bounds.max.y + (float)this.padTop;
			UIWidget component = base.GetComponent<UIWidget>();
			component.SetRect(num, num2, num3 - num, num4 - num2);
			base.BroadcastMessage("UpdateAnchors", SendMessageOptions.DontRequireReceiver);
			NGUITools.UpdateWidgetCollider(base.gameObject);
		}
	}

	// Token: 0x04000B30 RID: 2864
	public Transform targetRoot;

	// Token: 0x04000B31 RID: 2865
	public int padLeft;

	// Token: 0x04000B32 RID: 2866
	public int padRight;

	// Token: 0x04000B33 RID: 2867
	public int padBottom;

	// Token: 0x04000B34 RID: 2868
	public int padTop;

	// Token: 0x04000B35 RID: 2869
	public bool ignoreDisabled = true;

	// Token: 0x04000B36 RID: 2870
	private bool mStarted;
}
