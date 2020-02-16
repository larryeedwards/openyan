using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
	// Token: 0x0600113C RID: 4412 RVA: 0x0008AC89 File Offset: 0x00089089
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0008AC9D File Offset: 0x0008909D
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.width = Mathf.RoundToInt(this.width);
			this.mWidget.height = Mathf.RoundToInt(this.height);
		}
	}

	// Token: 0x04000EE3 RID: 3811
	public float width = 1f;

	// Token: 0x04000EE4 RID: 3812
	public float height = 1f;

	// Token: 0x04000EE5 RID: 3813
	private UIWidget mWidget;
}
