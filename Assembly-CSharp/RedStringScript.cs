using System;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class RedStringScript : MonoBehaviour
{
	// Token: 0x06001F13 RID: 7955 RVA: 0x0013CA38 File Offset: 0x0013AE38
	private void LateUpdate()
	{
		base.transform.LookAt(this.Target.position);
		base.transform.localScale = new Vector3(1f, 1f, Vector3.Distance(base.transform.position, this.Target.position));
	}

	// Token: 0x0400297F RID: 10623
	public Transform Target;
}
