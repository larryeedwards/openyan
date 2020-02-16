using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public class CameraDistanceDisableScript : MonoBehaviour
{
	// Token: 0x060017A7 RID: 6055 RVA: 0x000BC6A4 File Offset: 0x000BAAA4
	private void Update()
	{
		if (Vector3.Distance(this.Yandere.position, this.RenderTarget.position) > 15f)
		{
			this.MyCamera.enabled = false;
		}
		else
		{
			this.MyCamera.enabled = true;
		}
	}

	// Token: 0x0400179B RID: 6043
	public Transform RenderTarget;

	// Token: 0x0400179C RID: 6044
	public Transform Yandere;

	// Token: 0x0400179D RID: 6045
	public Camera MyCamera;
}
