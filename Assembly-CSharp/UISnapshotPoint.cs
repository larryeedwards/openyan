using System;
using UnityEngine;

// Token: 0x02000225 RID: 549
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	// Token: 0x060010DE RID: 4318 RVA: 0x0008AB8B File Offset: 0x00088F8B
	private void Start()
	{
		if (base.tag != "EditorOnly")
		{
			base.tag = "EditorOnly";
		}
	}

	// Token: 0x04000EAB RID: 3755
	public bool isOrthographic = true;

	// Token: 0x04000EAC RID: 3756
	public float nearClip = -100f;

	// Token: 0x04000EAD RID: 3757
	public float farClip = 100f;

	// Token: 0x04000EAE RID: 3758
	[Range(10f, 80f)]
	public int fieldOfView = 35;

	// Token: 0x04000EAF RID: 3759
	public float orthoSize = 30f;

	// Token: 0x04000EB0 RID: 3760
	public Texture2D thumbnail;
}
