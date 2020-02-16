using System;
using UnityEngine;

// Token: 0x020005BB RID: 1467
public class BarScript : MonoBehaviour
{
	// Token: 0x06002351 RID: 9041 RVA: 0x001BF5C0 File Offset: 0x001BD9C0
	private void Start()
	{
		base.transform.localScale = new Vector3(0f, 1f, 1f);
	}

	// Token: 0x06002352 RID: 9042 RVA: 0x001BF5E4 File Offset: 0x001BD9E4
	private void Update()
	{
		base.transform.localScale = new Vector3(base.transform.localScale.x + this.Speed * Time.deltaTime, 1f, 1f);
		if ((double)base.transform.localScale.x > 0.1)
		{
			base.transform.localScale = new Vector3(0f, 1f, 1f);
		}
	}

	// Token: 0x04003D21 RID: 15649
	public float Speed;
}
