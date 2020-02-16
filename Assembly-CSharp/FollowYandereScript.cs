using System;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class FollowYandereScript : MonoBehaviour
{
	// Token: 0x060019A9 RID: 6569 RVA: 0x000F0074 File Offset: 0x000EE474
	private void Update()
	{
		base.transform.position = new Vector3(this.Yandere.position.x, base.transform.position.y, this.Yandere.position.z);
	}

	// Token: 0x04001EBC RID: 7868
	public Transform Yandere;
}
