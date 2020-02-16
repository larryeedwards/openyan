using System;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class CreepyArmScript : MonoBehaviour
{
	// Token: 0x06001858 RID: 6232 RVA: 0x000D51B8 File Offset: 0x000D35B8
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime * 0.1f, base.transform.position.z);
	}
}
