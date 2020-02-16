using System;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class PoliceWalk : MonoBehaviour
{
	// Token: 0x06001E94 RID: 7828 RVA: 0x0012F228 File Offset: 0x0012D628
	private void Update()
	{
		Vector3 position = base.transform.position;
		position.z += Time.deltaTime;
		base.transform.position = position;
	}
}
