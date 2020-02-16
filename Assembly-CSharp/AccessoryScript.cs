using System;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class AccessoryScript : MonoBehaviour
{
	// Token: 0x060016E2 RID: 5858 RVA: 0x000AFCB8 File Offset: 0x000AE0B8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Prompt.MyCollider.enabled = false;
			base.transform.parent = this.Target;
			base.transform.localPosition = new Vector3(this.X, this.Y, this.Z);
			base.transform.localEulerAngles = Vector3.zero;
			base.enabled = false;
		}
	}

	// Token: 0x040014A5 RID: 5285
	public PromptScript Prompt;

	// Token: 0x040014A6 RID: 5286
	public Transform Target;

	// Token: 0x040014A7 RID: 5287
	public float X;

	// Token: 0x040014A8 RID: 5288
	public float Y;

	// Token: 0x040014A9 RID: 5289
	public float Z;
}
