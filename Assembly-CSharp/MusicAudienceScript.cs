using System;
using UnityEngine;

// Token: 0x02000178 RID: 376
public class MusicAudienceScript : MonoBehaviour
{
	// Token: 0x06000BE2 RID: 3042 RVA: 0x0005BD5E File Offset: 0x0005A15E
	private void Start()
	{
		this.JumpStrength += UnityEngine.Random.Range(-0.0001f, 0.0001f);
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0005BD7C File Offset: 0x0005A17C
	private void Update()
	{
		if (this.MusicMinigame.Health >= this.Threshold)
		{
			this.Minimum = Mathf.MoveTowards(this.Minimum, 0.2f, Time.deltaTime * 0.1f);
		}
		else
		{
			this.Minimum = Mathf.MoveTowards(this.Minimum, 0f, Time.deltaTime * 0.1f);
		}
		base.transform.localPosition += new Vector3(0f, this.Jump, 0f);
		this.Jump -= Time.deltaTime * 0.01f;
		if (base.transform.localPosition.y < this.Minimum)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.Minimum, 0f);
			this.Jump = this.JumpStrength;
		}
	}

	// Token: 0x0400095F RID: 2399
	public MusicMinigameScript MusicMinigame;

	// Token: 0x04000960 RID: 2400
	public float JumpStrength;

	// Token: 0x04000961 RID: 2401
	public float Threshold;

	// Token: 0x04000962 RID: 2402
	public float Minimum;

	// Token: 0x04000963 RID: 2403
	public float Jump;
}
