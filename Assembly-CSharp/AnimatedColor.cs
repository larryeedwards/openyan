using System;
using UnityEngine;

// Token: 0x0200022D RID: 557
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x06001139 RID: 4409 RVA: 0x0008AC44 File Offset: 0x00089044
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0008AC58 File Offset: 0x00089058
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x04000EE1 RID: 3809
	public Color color = Color.white;

	// Token: 0x04000EE2 RID: 3810
	private UIWidget mWidget;
}
