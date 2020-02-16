using System;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class DelinquentVoicesScript : MonoBehaviour
{
	// Token: 0x060018BA RID: 6330 RVA: 0x000DEFAF File Offset: 0x000DD3AF
	private void Start()
	{
		this.Timer = 5f;
	}

	// Token: 0x060018BB RID: 6331 RVA: 0x000DEFBC File Offset: 0x000DD3BC
	private void Update()
	{
		if (this.Radio.MyAudio.isPlaying && this.Yandere.CanMove && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 5f)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer == 0f && ClubGlobals.Club != ClubType.Delinquent)
			{
				if (this.Yandere.Container == null)
				{
					while (this.RandomID == this.LastID)
					{
						this.RandomID = UnityEngine.Random.Range(0, this.Subtitle.DelinquentAnnoyClips.Length);
					}
					this.LastID = this.RandomID;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentAnnoy, this.RandomID, 3f);
				}
				else
				{
					while (this.RandomID == this.LastID)
					{
						this.RandomID = UnityEngine.Random.Range(0, this.Subtitle.DelinquentCaseClips.Length);
					}
					this.LastID = this.RandomID;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentCase, this.RandomID, 3f);
				}
				this.Timer = 5f;
			}
		}
	}

	// Token: 0x04001C26 RID: 7206
	public YandereScript Yandere;

	// Token: 0x04001C27 RID: 7207
	public RadioScript Radio;

	// Token: 0x04001C28 RID: 7208
	public SubtitleScript Subtitle;

	// Token: 0x04001C29 RID: 7209
	public float Timer;

	// Token: 0x04001C2A RID: 7210
	public int RandomID;

	// Token: 0x04001C2B RID: 7211
	public int LastID;
}
