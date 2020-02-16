using System;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class ClimbFollowScript : MonoBehaviour
{
	// Token: 0x06000C14 RID: 3092 RVA: 0x00065B98 File Offset: 0x00063F98
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, this.Yandere.position.y, base.transform.position.z);
	}

	// Token: 0x04000A97 RID: 2711
	public Transform Yandere;
}
