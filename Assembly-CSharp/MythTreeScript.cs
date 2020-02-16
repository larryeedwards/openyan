using System;
using UnityEngine;

// Token: 0x0200046E RID: 1134
public class MythTreeScript : MonoBehaviour
{
	// Token: 0x06001DD5 RID: 7637 RVA: 0x0011C683 File Offset: 0x0011AA83
	private void Start()
	{
		if (SchemeGlobals.GetSchemeStage(2) > 2)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x0011C698 File Offset: 0x0011AA98
	private void Update()
	{
		if (!this.Spoken)
		{
			if (this.Yandere.Inventory.Ring && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 5f)
			{
				this.EventSubtitle.transform.localScale = new Vector3(1f, 1f, 1f);
				this.EventSubtitle.text = "...that...ring...";
				this.Jukebox.Dip = 0.5f;
				this.Spoken = true;
				this.MyAudio.Play();
			}
		}
		else if (!this.MyAudio.isPlaying)
		{
			this.EventSubtitle.transform.localScale = Vector3.zero;
			this.EventSubtitle.text = string.Empty;
			this.Jukebox.Dip = 1f;
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x0400259A RID: 9626
	public UILabel EventSubtitle;

	// Token: 0x0400259B RID: 9627
	public JukeboxScript Jukebox;

	// Token: 0x0400259C RID: 9628
	public YandereScript Yandere;

	// Token: 0x0400259D RID: 9629
	public bool Spoken;

	// Token: 0x0400259E RID: 9630
	public AudioSource MyAudio;
}
