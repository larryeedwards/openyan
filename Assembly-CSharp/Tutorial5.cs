using System;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class Tutorial5 : MonoBehaviour
{
	// Token: 0x06000C85 RID: 3205 RVA: 0x00068810 File Offset: 0x00066C10
	public void SetDurationToCurrentProgress()
	{
		UITweener[] componentsInChildren = base.GetComponentsInChildren<UITweener>();
		foreach (UITweener uitweener in componentsInChildren)
		{
			uitweener.duration = Mathf.Lerp(2f, 0.5f, UIProgressBar.current.value);
		}
	}
}
