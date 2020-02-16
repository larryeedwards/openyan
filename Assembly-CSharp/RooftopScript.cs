using System;
using UnityEngine;

// Token: 0x020004D5 RID: 1237
public class RooftopScript : MonoBehaviour
{
	// Token: 0x06001F4E RID: 8014 RVA: 0x0014015C File Offset: 0x0013E55C
	private void Start()
	{
		if (SchoolGlobals.RoofFence)
		{
			foreach (GameObject gameObject in this.DumpPoints)
			{
				gameObject.SetActive(false);
			}
			this.Railing.SetActive(false);
			this.Fence.SetActive(true);
		}
	}

	// Token: 0x04002A6E RID: 10862
	public GameObject[] DumpPoints;

	// Token: 0x04002A6F RID: 10863
	public GameObject Railing;

	// Token: 0x04002A70 RID: 10864
	public GameObject Fence;
}
