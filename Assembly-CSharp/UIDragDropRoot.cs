using System;
using UnityEngine;

// Token: 0x020001B9 RID: 441
[AddComponentMenu("NGUI/Interaction/Drag and Drop Root")]
public class UIDragDropRoot : MonoBehaviour
{
	// Token: 0x06000D13 RID: 3347 RVA: 0x0006BA94 File Offset: 0x00069E94
	private void OnEnable()
	{
		UIDragDropRoot.root = base.transform;
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0006BAA1 File Offset: 0x00069EA1
	private void OnDisable()
	{
		if (UIDragDropRoot.root == base.transform)
		{
			UIDragDropRoot.root = null;
		}
	}

	// Token: 0x04000BAF RID: 2991
	public static Transform root;
}
