using System;
using UnityEngine;

// Token: 0x020005AC RID: 1452
public class YanvaniaJukeboxScript : MonoBehaviour
{
	// Token: 0x0600231A RID: 8986 RVA: 0x001B99D4 File Offset: 0x001B7DD4
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (component.time + Time.deltaTime > component.clip.length)
		{
			component.clip = ((!this.Boss) ? this.ApproachMain : this.BossMain);
			component.loop = true;
			component.Play();
		}
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x001B9A34 File Offset: 0x001B7E34
	public void BossBattle()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.BossIntro;
		component.loop = false;
		component.volume = 0.25f;
		component.Play();
		this.Boss = true;
	}

	// Token: 0x04003C3D RID: 15421
	public AudioClip BossIntro;

	// Token: 0x04003C3E RID: 15422
	public AudioClip BossMain;

	// Token: 0x04003C3F RID: 15423
	public AudioClip ApproachIntro;

	// Token: 0x04003C40 RID: 15424
	public AudioClip ApproachMain;

	// Token: 0x04003C41 RID: 15425
	public bool Boss;
}
