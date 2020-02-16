using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
[RequireComponent(typeof(UIInput))]
public class UIInputOnGUI : MonoBehaviour
{
	// Token: 0x06001336 RID: 4918 RVA: 0x00096FD1 File Offset: 0x000953D1
	private void Awake()
	{
		this.mInput = base.GetComponent<UIInput>();
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x00096FDF File Offset: 0x000953DF
	private void OnGUI()
	{
		if (Event.current.rawType == EventType.KeyDown)
		{
			this.mInput.ProcessEvent(Event.current);
		}
	}

	// Token: 0x04001090 RID: 4240
	[NonSerialized]
	private UIInput mInput;
}
