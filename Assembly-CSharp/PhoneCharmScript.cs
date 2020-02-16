using System;
using UnityEngine;

// Token: 0x0200048D RID: 1165
public class PhoneCharmScript : MonoBehaviour
{
	// Token: 0x06001E42 RID: 7746 RVA: 0x001265C4 File Offset: 0x001249C4
	private void Update()
	{
		base.transform.eulerAngles = new Vector3(90f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
	}
}
