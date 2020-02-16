using System;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class BloodyScreamScript : MonoBehaviour
{
	// Token: 0x0600175F RID: 5983 RVA: 0x000B8754 File Offset: 0x000B6B54
	private void Start()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.Screams[UnityEngine.Random.Range(0, this.Screams.Length)];
		component.Play();
	}

	// Token: 0x040016E3 RID: 5859
	public AudioClip[] Screams;
}
