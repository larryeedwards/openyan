using System;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class UIShowControlScheme : MonoBehaviour
{
	// Token: 0x06000E4A RID: 3658 RVA: 0x000743ED File Offset: 0x000727ED
	private void OnEnable()
	{
		UICamera.onSchemeChange = (UICamera.OnSchemeChange)Delegate.Combine(UICamera.onSchemeChange, new UICamera.OnSchemeChange(this.OnScheme));
		this.OnScheme();
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x00074415 File Offset: 0x00072815
	private void OnDisable()
	{
		UICamera.onSchemeChange = (UICamera.OnSchemeChange)Delegate.Remove(UICamera.onSchemeChange, new UICamera.OnSchemeChange(this.OnScheme));
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x00074438 File Offset: 0x00072838
	private void OnScheme()
	{
		if (this.target != null)
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			if (currentScheme == UICamera.ControlScheme.Mouse)
			{
				this.target.SetActive(this.mouse);
			}
			else if (currentScheme == UICamera.ControlScheme.Touch)
			{
				this.target.SetActive(this.touch);
			}
			else if (currentScheme == UICamera.ControlScheme.Controller)
			{
				this.target.SetActive(this.controller);
			}
		}
	}

	// Token: 0x04000D02 RID: 3330
	public GameObject target;

	// Token: 0x04000D03 RID: 3331
	public bool mouse;

	// Token: 0x04000D04 RID: 3332
	public bool touch;

	// Token: 0x04000D05 RID: 3333
	public bool controller = true;
}
