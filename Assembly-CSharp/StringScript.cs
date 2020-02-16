using System;
using UnityEngine;

// Token: 0x0200052A RID: 1322
public class StringScript : MonoBehaviour
{
	// Token: 0x06002071 RID: 8305 RVA: 0x001537FE File Offset: 0x00151BFE
	private void Start()
	{
		if (this.ArrayID == 0)
		{
			this.Target.position = this.Origin.position;
		}
	}

	// Token: 0x06002072 RID: 8306 RVA: 0x00153824 File Offset: 0x00151C24
	private void Update()
	{
		this.String.position = this.Origin.position;
		this.String.LookAt(this.Target);
		this.String.localScale = new Vector3(this.String.localScale.x, this.String.localScale.y, Vector3.Distance(this.Origin.position, this.Target.position) * 0.5f);
	}

	// Token: 0x04002DBD RID: 11709
	public Transform Origin;

	// Token: 0x04002DBE RID: 11710
	public Transform Target;

	// Token: 0x04002DBF RID: 11711
	public Transform String;

	// Token: 0x04002DC0 RID: 11712
	public int ArrayID;
}
