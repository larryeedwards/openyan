using System;
using UnityEngine;

// Token: 0x020004B9 RID: 1209
public class RandomSoundScript : MonoBehaviour
{
	// Token: 0x06001F0F RID: 7951 RVA: 0x0013C984 File Offset: 0x0013AD84
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Clips[UnityEngine.Random.Range(1, this.Clips.Length)];
		component.Play();
	}

	// Token: 0x0400297B RID: 10619
	public AudioClip[] Clips;
}
