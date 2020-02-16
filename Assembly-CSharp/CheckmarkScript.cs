using System;
using UnityEngine;

// Token: 0x020005BC RID: 1468
public class CheckmarkScript : MonoBehaviour
{
	// Token: 0x06002354 RID: 9044 RVA: 0x001BF674 File Offset: 0x001BDA74
	private void Start()
	{
		while (this.ID < this.Checkmarks.Length)
		{
			this.Checkmarks[this.ID].SetActive(false);
			this.ID++;
		}
		this.ID = 0;
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x001BF6C4 File Offset: 0x001BDAC4
	private void Update()
	{
		if (Input.GetKeyDown("space") && this.ButtonPresses < 26)
		{
			this.ButtonPresses++;
			this.ID = UnityEngine.Random.Range(0, this.Checkmarks.Length - 4);
			while (this.Checkmarks[this.ID].active)
			{
				this.ID = UnityEngine.Random.Range(0, this.Checkmarks.Length - 4);
			}
			this.Checkmarks[this.ID].SetActive(true);
		}
	}

	// Token: 0x04003D22 RID: 15650
	public GameObject[] Checkmarks;

	// Token: 0x04003D23 RID: 15651
	public int ButtonPresses;

	// Token: 0x04003D24 RID: 15652
	public int ID;
}
