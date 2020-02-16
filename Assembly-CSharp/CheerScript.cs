using System;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class CheerScript : MonoBehaviour
{
	// Token: 0x060017CD RID: 6093 RVA: 0x000BDE60 File Offset: 0x000BC260
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 5f)
		{
			this.MyAudio.clip = this.Cheers[UnityEngine.Random.Range(1, this.Cheers.Length)];
			this.MyAudio.Play();
			this.Timer = 0f;
		}
	}

	// Token: 0x040017E6 RID: 6118
	public AudioSource MyAudio;

	// Token: 0x040017E7 RID: 6119
	public AudioClip[] Cheers;

	// Token: 0x040017E8 RID: 6120
	public float Timer;
}
