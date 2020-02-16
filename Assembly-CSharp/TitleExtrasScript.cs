using System;
using UnityEngine;

// Token: 0x0200054E RID: 1358
public class TitleExtrasScript : MonoBehaviour
{
	// Token: 0x0600218F RID: 8591 RVA: 0x00195BAC File Offset: 0x00193FAC
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
	}

	// Token: 0x06002190 RID: 8592 RVA: 0x00195BF4 File Offset: 0x00193FF4
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		}
	}

	// Token: 0x04003635 RID: 13877
	public bool Show;
}
