using System;
using UnityEngine;

// Token: 0x0200034D RID: 845
public class BushSpawnerScript : MonoBehaviour
{
	// Token: 0x06001799 RID: 6041 RVA: 0x000BAEC8 File Offset: 0x000B92C8
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Bush, new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 4f), UnityEngine.Random.Range(-16f, 16f)), Quaternion.identity);
		}
	}

	// Token: 0x04001767 RID: 5991
	public GameObject Bush;

	// Token: 0x04001768 RID: 5992
	public bool Begin;
}
