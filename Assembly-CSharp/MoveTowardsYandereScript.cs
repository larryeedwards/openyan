using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class MoveTowardsYandereScript : MonoBehaviour
{
	// Token: 0x06001DC4 RID: 7620 RVA: 0x0011AE19 File Offset: 0x00119219
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>().Spine[3];
		this.Distance = Vector3.Distance(base.transform.position, this.Yandere.position);
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x0011AE58 File Offset: 0x00119258
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.position) > this.Distance * 0.5f && base.transform.position.y < this.Yandere.position.y + 0.5f)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + Time.deltaTime, base.transform.position.z);
		}
		this.Speed += Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.Yandere.position, this.Speed * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.Yandere.position) == 0f)
		{
			this.Smoke.emission.enabled = false;
		}
	}

	// Token: 0x0400256D RID: 9581
	public ParticleSystem Smoke;

	// Token: 0x0400256E RID: 9582
	public Transform Yandere;

	// Token: 0x0400256F RID: 9583
	public float Distance;

	// Token: 0x04002570 RID: 9584
	public float Speed;

	// Token: 0x04002571 RID: 9585
	public bool Fall;
}
