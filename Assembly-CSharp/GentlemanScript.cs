using System;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class GentlemanScript : MonoBehaviour
{
	// Token: 0x060019DA RID: 6618 RVA: 0x000F3B24 File Offset: 0x000F1F24
	private void Update()
	{
		if (Input.GetButtonDown("RB"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (!component.isPlaying)
			{
				component.clip = this.Clips[UnityEngine.Random.Range(0, this.Clips.Length - 1)];
				component.Play();
				this.Yandere.Sanity += 10f;
			}
		}
	}

	// Token: 0x04001F58 RID: 8024
	public YandereScript Yandere;

	// Token: 0x04001F59 RID: 8025
	public AudioClip[] Clips;
}
