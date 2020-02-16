using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class HologramScript : MonoBehaviour
{
	// Token: 0x06001C86 RID: 7302 RVA: 0x00101E9C File Offset: 0x0010029C
	public void UpdateHolograms()
	{
		foreach (GameObject gameObject in this.Holograms)
		{
			gameObject.SetActive(this.TrueFalse());
		}
	}

	// Token: 0x06001C87 RID: 7303 RVA: 0x00101ED4 File Offset: 0x001002D4
	private bool TrueFalse()
	{
		return UnityEngine.Random.value >= 0.5f;
	}

	// Token: 0x04002145 RID: 8517
	public GameObject[] Holograms;
}
