using System;
using UnityEngine;

// Token: 0x02000275 RID: 629
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x060013BB RID: 5051 RVA: 0x000996C6 File Offset: 0x00097AC6
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x000996EC File Offset: 0x00097AEC
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = this.mCam.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num3))
		{
			this.mCam.orthographicSize = num3;
		}
	}

	// Token: 0x040010D6 RID: 4310
	private Camera mCam;

	// Token: 0x040010D7 RID: 4311
	private Transform mTrans;
}
