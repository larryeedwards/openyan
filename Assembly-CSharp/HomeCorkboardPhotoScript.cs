using System;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class HomeCorkboardPhotoScript : MonoBehaviour
{
	// Token: 0x06001C94 RID: 7316 RVA: 0x00102D78 File Offset: 0x00101178
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 4)
		{
			base.transform.localScale = new Vector3(Mathf.MoveTowards(base.transform.localScale.x, 1f, Time.deltaTime * 10f), Mathf.MoveTowards(base.transform.localScale.y, 1f, Time.deltaTime * 10f), Mathf.MoveTowards(base.transform.localScale.z, 1f, Time.deltaTime * 10f));
		}
	}

	// Token: 0x0400217D RID: 8573
	public int ArrayID;

	// Token: 0x0400217E RID: 8574
	public int ID;
}
