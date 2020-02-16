using System;
using UnityEngine;

// Token: 0x020001B6 RID: 438
[AddComponentMenu("NGUI/Interaction/Drag and Drop Container")]
public class UIDragDropContainer : MonoBehaviour
{
	// Token: 0x06000CFD RID: 3325 RVA: 0x0006BA6D File Offset: 0x00069E6D
	protected virtual void Start()
	{
		if (this.reparentTarget == null)
		{
			this.reparentTarget = base.transform;
		}
	}

	// Token: 0x04000B97 RID: 2967
	public Transform reparentTarget;
}
