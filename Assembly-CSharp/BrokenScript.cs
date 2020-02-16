using System;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class BrokenScript : MonoBehaviour
{
	// Token: 0x0600176E RID: 5998 RVA: 0x000B8D6C File Offset: 0x000B716C
	private void Start()
	{
		this.HairPhysics[0].enabled = false;
		this.HairPhysics[1].enabled = false;
		this.PermanentAngleR = this.TwintailR.eulerAngles;
		this.PermanentAngleL = this.TwintailL.eulerAngles;
		this.Subtitle = GameObject.Find("EventSubtitle").GetComponent<UILabel>();
		this.Yandere = GameObject.Find("YandereChan");
	}

	// Token: 0x0600176F RID: 5999 RVA: 0x000B8DDC File Offset: 0x000B71DC
	private void Update()
	{
		if (!this.Done)
		{
			float num = Vector3.Distance(this.Yandere.transform.position, base.transform.root.position);
			if (num < 6f)
			{
				if (num < 5f)
				{
					if (!this.Hunting)
					{
						this.Timer += Time.deltaTime;
						if (this.VoiceClip == null)
						{
							this.Subtitle.text = string.Empty;
						}
						if (this.Timer > 5f)
						{
							this.Timer = 0f;
							this.Subtitle.text = this.MutterTexts[this.ID];
							AudioClipPlayer.PlayAttached(this.Mutters[this.ID], base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
							this.ID++;
							if (this.ID == this.Mutters.Length)
							{
								this.ID = 1;
							}
						}
					}
					else if (!this.Began)
					{
						if (this.VoiceClip != null)
						{
							UnityEngine.Object.Destroy(this.VoiceClip);
						}
						this.Subtitle.text = "Do it.";
						AudioClipPlayer.PlayAttached(this.DoIt, base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
						this.Began = true;
					}
					else if (this.VoiceClip == null)
					{
						this.Subtitle.text = "...kill...kill...kill...";
						AudioClipPlayer.PlayAttached(this.KillKillKill, base.transform.position, base.transform, 1f, 5f, out this.VoiceClip, this.Yandere.transform.position.y);
					}
					float num2 = Mathf.Abs((num - 5f) * 0.2f);
					num2 = ((num2 <= 1f) ? num2 : 1f);
					this.Subtitle.transform.localScale = new Vector3(num2, num2, num2);
				}
				else
				{
					this.Subtitle.transform.localScale = Vector3.zero;
				}
			}
		}
		Vector3 eulerAngles = this.TwintailR.eulerAngles;
		Vector3 eulerAngles2 = this.TwintailL.eulerAngles;
		eulerAngles.x = this.PermanentAngleR.x;
		eulerAngles.z = this.PermanentAngleR.z;
		eulerAngles2.x = this.PermanentAngleL.x;
		eulerAngles2.z = this.PermanentAngleL.z;
		this.TwintailR.eulerAngles = eulerAngles;
		this.TwintailL.eulerAngles = eulerAngles2;
	}

	// Token: 0x0400171D RID: 5917
	public DynamicBone[] HairPhysics;

	// Token: 0x0400171E RID: 5918
	public string[] MutterTexts;

	// Token: 0x0400171F RID: 5919
	public AudioClip[] Mutters;

	// Token: 0x04001720 RID: 5920
	public Vector3 PermanentAngleR;

	// Token: 0x04001721 RID: 5921
	public Vector3 PermanentAngleL;

	// Token: 0x04001722 RID: 5922
	public Transform TwintailR;

	// Token: 0x04001723 RID: 5923
	public Transform TwintailL;

	// Token: 0x04001724 RID: 5924
	public AudioClip KillKillKill;

	// Token: 0x04001725 RID: 5925
	public AudioClip Stab;

	// Token: 0x04001726 RID: 5926
	public AudioClip DoIt;

	// Token: 0x04001727 RID: 5927
	public GameObject VoiceClip;

	// Token: 0x04001728 RID: 5928
	public GameObject Yandere;

	// Token: 0x04001729 RID: 5929
	public UILabel Subtitle;

	// Token: 0x0400172A RID: 5930
	public AudioSource MyAudio;

	// Token: 0x0400172B RID: 5931
	public bool Hunting;

	// Token: 0x0400172C RID: 5932
	public bool Stabbed;

	// Token: 0x0400172D RID: 5933
	public bool Began;

	// Token: 0x0400172E RID: 5934
	public bool Done;

	// Token: 0x0400172F RID: 5935
	public float SuicideTimer;

	// Token: 0x04001730 RID: 5936
	public float Timer;

	// Token: 0x04001731 RID: 5937
	public int ID = 1;
}
