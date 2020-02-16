using System;
using UnityEngine;

// Token: 0x0200019D RID: 413
[RequireComponent(typeof(UIWidget))]
public class SetColorPickerColor : MonoBehaviour
{
	// Token: 0x06000C7B RID: 3195 RVA: 0x000685E8 File Offset: 0x000669E8
	public void SetToCurrent()
	{
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		if (UIColorPicker.current != null)
		{
			this.mWidget.color = UIColorPicker.current.value;
		}
	}

	// Token: 0x04000B1B RID: 2843
	[NonSerialized]
	private UIWidget mWidget;
}
