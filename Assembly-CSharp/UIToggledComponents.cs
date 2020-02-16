using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E9 RID: 489
[ExecuteInEditMode]
[RequireComponent(typeof(UIToggle))]
[AddComponentMenu("NGUI/Interaction/Toggled Components")]
public class UIToggledComponents : MonoBehaviour
{
	// Token: 0x06000E7D RID: 3709 RVA: 0x00075424 File Offset: 0x00073824
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

	// Token: 0x06000E7E RID: 3710 RVA: 0x000754C0 File Offset: 0x000738C0
	public void Toggle()
	{
		if (base.enabled)
		{
			for (int i = 0; i < this.activate.Count; i++)
			{
				MonoBehaviour monoBehaviour = this.activate[i];
				monoBehaviour.enabled = UIToggle.current.value;
			}
			for (int j = 0; j < this.deactivate.Count; j++)
			{
				MonoBehaviour monoBehaviour2 = this.deactivate[j];
				monoBehaviour2.enabled = !UIToggle.current.value;
			}
		}
	}

	// Token: 0x04000D3C RID: 3388
	public List<MonoBehaviour> activate;

	// Token: 0x04000D3D RID: 3389
	public List<MonoBehaviour> deactivate;

	// Token: 0x04000D3E RID: 3390
	[HideInInspector]
	[SerializeField]
	private MonoBehaviour target;

	// Token: 0x04000D3F RID: 3391
	[HideInInspector]
	[SerializeField]
	private bool inverse;
}
