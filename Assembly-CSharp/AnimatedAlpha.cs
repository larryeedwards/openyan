using System;
using UnityEngine;

// Token: 0x0200022C RID: 556
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06001136 RID: 4406 RVA: 0x0008ABC0 File Offset: 0x00088FC0
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0008ABE0 File Offset: 0x00088FE0
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x04000EDE RID: 3806
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x04000EDF RID: 3807
	private UIWidget mWidget;

	// Token: 0x04000EE0 RID: 3808
	private UIPanel mPanel;
}
