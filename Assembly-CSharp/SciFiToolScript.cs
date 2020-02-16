using System;
using UnityEngine;

// Token: 0x020004FD RID: 1277
public class SciFiToolScript : MonoBehaviour
{
	// Token: 0x06001FCE RID: 8142 RVA: 0x001461D7 File Offset: 0x001445D7
	private void Start()
	{
		this.Target = this.Student.StudentManager.ToolTarget;
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x001461F0 File Offset: 0x001445F0
	private void Update()
	{
		if ((double)Vector3.Distance(this.Tip.position, this.Target.position) < 0.1)
		{
			this.Sparks.Play();
		}
		else
		{
			this.Sparks.Stop();
		}
	}

	// Token: 0x04002BB7 RID: 11191
	public StudentScript Student;

	// Token: 0x04002BB8 RID: 11192
	public ParticleSystem Sparks;

	// Token: 0x04002BB9 RID: 11193
	public Transform Target;

	// Token: 0x04002BBA RID: 11194
	public Transform Tip;
}
