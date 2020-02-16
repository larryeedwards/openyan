using System;
using UnityEngine;

// Token: 0x0200039F RID: 927
public class DrillScript : MonoBehaviour
{
	// Token: 0x060018F3 RID: 6387 RVA: 0x000E6430 File Offset: 0x000E4830
	private void LateUpdate()
	{
		base.transform.Rotate(Vector3.up * Time.deltaTime * 3600f);
	}
}
