using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EA RID: 490
[AddComponentMenu("NGUI/Interaction/Toggled Objects")]
public class UIToggledObjects : MonoBehaviour
{
	// Token: 0x06000E80 RID: 3712 RVA: 0x00075558 File Offset: 0x00073958
	private void Awake()
	{
		if (this.target != null)
		{
			if (this.activate.Count == 0 && this.deactivate.Count == 0)
			{
				if (this.inverse)
				{
					this.deactivate.Add(this.target);
				}
				else
				{
					this.activate.Add(this.target);
				}
			}
			else
			{
				this.target = null;
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.Toggle));
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x000755F4 File Offset: 0x000739F4
	public void Toggle()
	{
		bool value = UIToggle.current.value;
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				this.Set(this.activate[i], value);
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				this.Set(this.deactivate[j], !value);
			}
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00075678 File Offset: 0x00073A78
	private void Set(GameObject go, bool state)
	{
		if (go != null)
		{
			NGUITools.SetActive(go, state);
		}
	}

	// Token: 0x04000D40 RID: 3392
	public List<GameObject> activate;

	// Token: 0x04000D41 RID: 3393
	public List<GameObject> deactivate;

	// Token: 0x04000D42 RID: 3394
	[HideInInspector]
	[SerializeField]
	private GameObject target;

	// Token: 0x04000D43 RID: 3395
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
