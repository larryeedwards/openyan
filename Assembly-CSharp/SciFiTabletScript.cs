using System;
using UnityEngine;

// Token: 0x020004FB RID: 1275
public class SciFiTabletScript : MonoBehaviour
{
	// Token: 0x06001FC8 RID: 8136 RVA: 0x0014604E File Offset: 0x0014444E
	private void Start()
	{
		this.Holograms = this.Student.StudentManager.Holograms;
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x00146068 File Offset: 0x00144468
	private void Update()
	{
		if ((double)Vector3.Distance(this.Finger.position, base.transform.position) < 0.1)
		{
			if (!this.Updated)
			{
				this.Holograms.UpdateHolograms();
				this.Updated = true;
			}
		}
		else
		{
			this.Updated = false;
		}
	}

	// Token: 0x04002BAF RID: 11183
	public StudentScript Student;

	// Token: 0x04002BB0 RID: 11184
	public HologramScript Holograms;

	// Token: 0x04002BB1 RID: 11185
	public Transform Finger;

	// Token: 0x04002BB2 RID: 11186
	public bool Updated;
}
