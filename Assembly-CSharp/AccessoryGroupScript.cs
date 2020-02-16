using System;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class AccessoryGroupScript : MonoBehaviour
{
	// Token: 0x060016E0 RID: 5856 RVA: 0x000AFC7C File Offset: 0x000AE07C
	public void SetPartsActive(bool active)
	{
		foreach (GameObject gameObject in this.Parts)
		{
			gameObject.SetActive(active);
		}
	}

	// Token: 0x040014A4 RID: 5284
	public GameObject[] Parts;
}
