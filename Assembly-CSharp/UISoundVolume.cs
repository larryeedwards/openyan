using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
[RequireComponent(typeof(UISlider))]
[AddComponentMenu("NGUI/Interaction/Sound Volume")]
public class UISoundVolume : MonoBehaviour
{
	// Token: 0x06000E5B RID: 3675 RVA: 0x000744B8 File Offset: 0x000728B8
	private void Awake()
	{
		UISlider component = base.GetComponent<UISlider>();
		component.value = NGUITools.soundVolume;
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnChange));
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x000744EF File Offset: 0x000728EF
	private void OnChange()
	{
		NGUITools.soundVolume = UIProgressBar.current.value;
	}
}
