using System;
using UnityEngine;

// Token: 0x0200051E RID: 1310
public class StalkerIntroCameraScript : MonoBehaviour
{
	// Token: 0x06002046 RID: 8262 RVA: 0x0015021C File Offset: 0x0014E61C
	private void Update()
	{
		if (this.YandereAnim["f02_wallJump_00"].time > this.YandereAnim["f02_wallJump_00"].length)
		{
			this.Speed += Time.deltaTime;
			this.Yandere.position = Vector3.Lerp(this.Yandere.position, new Vector3(14.33333f, 0f, 15f), Time.deltaTime * this.Speed);
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(13.75f, 1.4f, 14.5f), Time.deltaTime * this.Speed);
			base.transform.eulerAngles = Vector3.Lerp(base.transform.eulerAngles, new Vector3(15f, 180f, 0f), Time.deltaTime * this.Speed);
		}
	}

	// Token: 0x04002D29 RID: 11561
	public Animation YandereAnim;

	// Token: 0x04002D2A RID: 11562
	public Transform Yandere;

	// Token: 0x04002D2B RID: 11563
	public float Speed;
}
