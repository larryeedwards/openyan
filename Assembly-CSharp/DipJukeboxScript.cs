using System;
using UnityEngine;

// Token: 0x02000397 RID: 919
public class DipJukeboxScript : MonoBehaviour
{
	// Token: 0x060018D9 RID: 6361 RVA: 0x000E3E54 File Offset: 0x000E2254
	private void Update()
	{
		if (this.MyAudio.isPlaying)
		{
			float num = Vector3.Distance(this.Yandere.position, base.transform.position);
			if (num < 8f)
			{
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, (7f - num) * 0.25f * this.Jukebox.Volume, Time.deltaTime);
				if (this.Jukebox.ClubDip < 0f)
				{
					this.Jukebox.ClubDip = 0f;
				}
				if (this.Jukebox.ClubDip > this.Jukebox.Volume)
				{
					this.Jukebox.ClubDip = this.Jukebox.Volume;
				}
			}
		}
		else if (this.MyAudio.isPlaying)
		{
			this.Jukebox.ClubDip = 0f;
		}
	}

	// Token: 0x04001C94 RID: 7316
	public JukeboxScript Jukebox;

	// Token: 0x04001C95 RID: 7317
	public AudioSource MyAudio;

	// Token: 0x04001C96 RID: 7318
	public Transform Yandere;
}
