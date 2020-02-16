using System;
using UnityEngine;

// Token: 0x02000188 RID: 392
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x06000C35 RID: 3125 RVA: 0x00066744 File Offset: 0x00064B44
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = UnityEngine.Object.Instantiate<GameObject>(this.mPrefab, transform.position, transform.rotation);
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000AB5 RID: 2741
	public InvBaseItem.Slot slot;

	// Token: 0x04000AB6 RID: 2742
	private GameObject mPrefab;

	// Token: 0x04000AB7 RID: 2743
	private GameObject mChild;
}
