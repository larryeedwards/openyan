using System;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
public class RiggedAttacher : MonoBehaviour
{
	// Token: 0x06002362 RID: 9058 RVA: 0x001BFA68 File Offset: 0x001BDE68
	private void Start()
	{
		this.Attaching(this.BasePelvisRoot, this.AttachmentPelvisRoot);
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x001BFA7C File Offset: 0x001BDE7C
	private void Attaching(Transform Base, Transform Attachment)
	{
		Attachment.transform.SetParent(Base);
		Base.localEulerAngles = Vector3.zero;
		Base.localPosition = Vector3.zero;
		Transform[] componentsInChildren = Base.GetComponentsInChildren<Transform>();
		Transform[] componentsInChildren2 = Attachment.GetComponentsInChildren<Transform>();
		foreach (Transform transform in componentsInChildren2)
		{
			foreach (Transform transform2 in componentsInChildren)
			{
				if (transform.name == transform2.name)
				{
					transform.SetParent(transform2);
					transform.localEulerAngles = Vector3.zero;
					transform.localPosition = Vector3.zero;
				}
			}
		}
	}

	// Token: 0x04003D2C RID: 15660
	public Transform BasePelvisRoot;

	// Token: 0x04003D2D RID: 15661
	public Transform AttachmentPelvisRoot;
}
