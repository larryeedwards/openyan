using System;
using UnityEngine;

// Token: 0x020005BE RID: 1470
public class LargeTextScript : MonoBehaviour
{
	// Token: 0x06002359 RID: 9049 RVA: 0x001BF7C7 File Offset: 0x001BDBC7
	private void Start()
	{
		this.Label.text = this.String[this.ID];
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x001BF7E1 File Offset: 0x001BDBE1
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.ID++;
			this.Label.text = this.String[this.ID];
		}
	}

	// Token: 0x04003D26 RID: 15654
	public UILabel Label;

	// Token: 0x04003D27 RID: 15655
	public string[] String;

	// Token: 0x04003D28 RID: 15656
	public int ID;
}
