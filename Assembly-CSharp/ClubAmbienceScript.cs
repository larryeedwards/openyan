using System;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class ClubAmbienceScript : MonoBehaviour
{
	// Token: 0x060017F4 RID: 6132 RVA: 0x000C14C8 File Offset: 0x000BF8C8
	private void Update()
	{
		if (this.Yandere.position.y > base.transform.position.y - 0.1f && this.Yandere.position.y < base.transform.position.y + 0.1f)
		{
			if (Vector3.Distance(base.transform.position, this.Yandere.position) < 4f)
			{
				this.CreateAmbience = true;
				this.EffectJukebox = true;
			}
			else
			{
				this.CreateAmbience = false;
			}
		}
		if (this.EffectJukebox)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.CreateAmbience)
			{
				component.volume = Mathf.MoveTowards(component.volume, this.MaxVolume, Time.deltaTime * 0.1f);
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, this.ClubDip, Time.deltaTime * 0.1f);
			}
			else
			{
				component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime * 0.1f);
				this.Jukebox.ClubDip = Mathf.MoveTowards(this.Jukebox.ClubDip, 0f, Time.deltaTime * 0.1f);
				if (this.Jukebox.ClubDip == 0f)
				{
					this.EffectJukebox = false;
				}
			}
		}
	}

	// Token: 0x0400185C RID: 6236
	public JukeboxScript Jukebox;

	// Token: 0x0400185D RID: 6237
	public Transform Yandere;

	// Token: 0x0400185E RID: 6238
	public bool CreateAmbience;

	// Token: 0x0400185F RID: 6239
	public bool EffectJukebox;

	// Token: 0x04001860 RID: 6240
	public float ClubDip;

	// Token: 0x04001861 RID: 6241
	public float MaxVolume;
}
