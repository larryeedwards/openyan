using System;
using UnityEngine;

// Token: 0x02000330 RID: 816
public class AudioListenerScript : MonoBehaviour
{
	// Token: 0x06001734 RID: 5940 RVA: 0x000B6960 File Offset: 0x000B4D60
	private void Start()
	{
		this.mainCamera = Camera.main;
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x000B696D File Offset: 0x000B4D6D
	private void Update()
	{
		base.transform.position = this.Target.position;
		base.transform.eulerAngles = this.mainCamera.transform.eulerAngles;
	}

	// Token: 0x0400168F RID: 5775
	public Transform Target;

	// Token: 0x04001690 RID: 5776
	public Camera mainCamera;
}
