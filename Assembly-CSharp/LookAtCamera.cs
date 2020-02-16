using System;
using UnityEngine;

// Token: 0x020005CD RID: 1485
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x06002384 RID: 9092 RVA: 0x001C18BD File Offset: 0x001BFCBD
	private void Start()
	{
		if (this.cameraToLookAt == null)
		{
			this.cameraToLookAt = Camera.main;
		}
	}

	// Token: 0x06002385 RID: 9093 RVA: 0x001C18DC File Offset: 0x001BFCDC
	private void Update()
	{
		Vector3 b = new Vector3(0f, this.cameraToLookAt.transform.position.y - base.transform.position.y, 0f);
		base.transform.LookAt(this.cameraToLookAt.transform.position - b);
	}

	// Token: 0x04003D82 RID: 15746
	public Camera cameraToLookAt;
}
