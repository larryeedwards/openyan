using System;
using UnityEngine;

// Token: 0x02000512 RID: 1298
public class SniperScript : MonoBehaviour
{
	// Token: 0x06002022 RID: 8226 RVA: 0x0014E970 File Offset: 0x0014CD70
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 10f)
		{
			if (this.StudentManager.Students[10] != null)
			{
				this.StudentManager.Students[10].BecomeRagdoll();
			}
			if (this.StudentManager.Students[11] != null)
			{
				this.StudentManager.Students[11].BecomeRagdoll();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002CDA RID: 11482
	public StudentManagerScript StudentManager;

	// Token: 0x04002CDB RID: 11483
	public float Timer;
}
