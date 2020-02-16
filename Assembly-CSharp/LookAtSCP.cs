using System;
using UnityEngine;

// Token: 0x020005CE RID: 1486
public class LookAtSCP : MonoBehaviour
{
	// Token: 0x06002387 RID: 9095 RVA: 0x001C194F File Offset: 0x001BFD4F
	private void Start()
	{
		if (this.SCP == null)
		{
			this.SCP = GameObject.Find("SCPTarget").transform;
		}
		base.transform.LookAt(this.SCP);
	}

	// Token: 0x06002388 RID: 9096 RVA: 0x001C1988 File Offset: 0x001BFD88
	private void LateUpdate()
	{
		base.transform.LookAt(this.SCP);
	}

	// Token: 0x04003D83 RID: 15747
	public Transform SCP;
}
