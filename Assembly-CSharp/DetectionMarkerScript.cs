using System;
using UnityEngine;

// Token: 0x02000395 RID: 917
[Serializable]
public class DetectionMarkerScript : MonoBehaviour
{
	// Token: 0x060018D0 RID: 6352 RVA: 0x000E0124 File Offset: 0x000DE524
	private void Start()
	{
		base.transform.LookAt(new Vector3(this.Target.position.x, base.transform.position.y, this.Target.position.z));
		this.Tex.transform.localScale = new Vector3(1f, 0f, 1f);
		base.transform.localScale = new Vector3(1f, 1f, 1f);
		this.Tex.color = new Color(this.Tex.color.r, this.Tex.color.g, this.Tex.color.b, 0f);
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x000E0210 File Offset: 0x000DE610
	private void Update()
	{
		if (this.Tex.color.a > 0f && base.transform != null && this.Target != null)
		{
			base.transform.LookAt(new Vector3(this.Target.position.x, base.transform.position.y, this.Target.position.z));
		}
	}

	// Token: 0x04001C61 RID: 7265
	public Transform Target;

	// Token: 0x04001C62 RID: 7266
	public UITexture Tex;
}
