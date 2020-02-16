using System;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class DetectionCameraScript : MonoBehaviour
{
	// Token: 0x060018CE RID: 6350 RVA: 0x000E00A4 File Offset: 0x000DE4A4
	private void Update()
	{
		base.transform.position = this.YandereChan.transform.position + Vector3.up * 100f;
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}

	// Token: 0x04001C60 RID: 7264
	public Transform YandereChan;
}
