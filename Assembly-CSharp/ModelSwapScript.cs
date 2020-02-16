using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005BF RID: 1471
public class ModelSwapScript : MonoBehaviour
{
	// Token: 0x0600235C RID: 9052 RVA: 0x001BF820 File Offset: 0x001BDC20
	public void Update()
	{
		if (Input.GetKeyDown("z"))
		{
		}
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x001BF831 File Offset: 0x001BDC31
	public void Attach(GameObject Attachment, bool Inactives)
	{
		base.StartCoroutine(this.Attach_Threat(this.PelvisRoot, Attachment, Inactives));
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x001BF848 File Offset: 0x001BDC48
	public IEnumerator Attach_Threat(Transform PelvisRoot, GameObject Attachment, bool Inactives)
	{
		Attachment.transform.SetParent(PelvisRoot);
		PelvisRoot.localEulerAngles = Vector3.zero;
		PelvisRoot.localPosition = Vector3.zero;
		Transform[] StudentBones = PelvisRoot.GetComponentsInChildren<Transform>(Inactives);
		Transform[] Bones = Attachment.GetComponentsInChildren<Transform>(Inactives);
		foreach (Transform transform in Bones)
		{
			foreach (Transform transform2 in StudentBones)
			{
				if (transform.name == transform2.name)
				{
					transform.SetParent(transform2);
					transform.localEulerAngles = Vector3.zero;
					transform.localPosition = Vector3.zero;
				}
			}
		}
		yield return null;
		yield break;
	}

	// Token: 0x04003D29 RID: 15657
	public Transform PelvisRoot;

	// Token: 0x04003D2A RID: 15658
	public GameObject Attachment;
}
