using System;
using UnityEngine;

// Token: 0x020001A9 RID: 425
[AddComponentMenu("NGUI/Interaction/Button Activate")]
public class UIButtonActivate : MonoBehaviour
{
	// Token: 0x06000CB0 RID: 3248 RVA: 0x0006A065 File Offset: 0x00068465
	private void OnClick()
	{
		if (this.target != null)
		{
			NGUITools.SetActive(this.target, this.state);
		}
	}

	// Token: 0x04000B5B RID: 2907
	public GameObject target;

	// Token: 0x04000B5C RID: 2908
	public bool state = true;
}
