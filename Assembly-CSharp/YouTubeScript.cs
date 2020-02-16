using System;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
public class YouTubeScript : MonoBehaviour
{
	// Token: 0x0600234A RID: 9034 RVA: 0x001BEBCC File Offset: 0x001BCFCC
	private void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			this.Strength += Time.deltaTime;
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(0f, 1.15f, 1f), Time.deltaTime * this.Strength);
		}
	}

	// Token: 0x04003D11 RID: 15633
	public float Strength;

	// Token: 0x04003D12 RID: 15634
	public bool Begin;
}
