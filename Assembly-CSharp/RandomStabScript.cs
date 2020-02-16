using System;
using UnityEngine;

// Token: 0x020004BA RID: 1210
public class RandomStabScript : MonoBehaviour
{
	// Token: 0x06001F11 RID: 7953 RVA: 0x0013C9C4 File Offset: 0x0013ADC4
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.Biting)
		{
			component.clip = this.Stabs[UnityEngine.Random.Range(0, this.Stabs.Length)];
			component.Play();
		}
		else
		{
			component.clip = this.Bite;
			component.pitch = UnityEngine.Random.Range(0.5f, 1f);
			component.Play();
		}
	}

	// Token: 0x0400297C RID: 10620
	public AudioClip[] Stabs;

	// Token: 0x0400297D RID: 10621
	public AudioClip Bite;

	// Token: 0x0400297E RID: 10622
	public bool Biting;
}
