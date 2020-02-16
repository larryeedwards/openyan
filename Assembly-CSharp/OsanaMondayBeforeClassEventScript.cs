using System;
using UnityEngine;

// Token: 0x020004C9 RID: 1225
public class OsanaMondayBeforeClassEventScript : MonoBehaviour
{
	// Token: 0x06001F3A RID: 7994 RVA: 0x0013F668 File Offset: 0x0013DA68
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Bentos[1].SetActive(false);
		this.Bentos[2].SetActive(false);
		if (DateGlobals.Weekday != DayOfWeek.Monday)
		{
			base.enabled = false;
		}
	}

	// Token: 0x040029FA RID: 10746
	public StudentManagerScript StudentManager;

	// Token: 0x040029FB RID: 10747
	public EventManagerScript NextEvent;

	// Token: 0x040029FC RID: 10748
	public JukeboxScript Jukebox;

	// Token: 0x040029FD RID: 10749
	public UILabel EventSubtitle;

	// Token: 0x040029FE RID: 10750
	public YandereScript Yandere;

	// Token: 0x040029FF RID: 10751
	public ClockScript Clock;

	// Token: 0x04002A00 RID: 10752
	public StudentScript Rival;

	// Token: 0x04002A01 RID: 10753
	public Transform Destination;

	// Token: 0x04002A02 RID: 10754
	public AudioClip SpeechClip;

	// Token: 0x04002A03 RID: 10755
	public string[] SpeechText;

	// Token: 0x04002A04 RID: 10756
	public float[] SpeechTime;

	// Token: 0x04002A05 RID: 10757
	public GameObject AlarmDisc;

	// Token: 0x04002A06 RID: 10758
	public GameObject VoiceClip;

	// Token: 0x04002A07 RID: 10759
	public GameObject[] Bentos;

	// Token: 0x04002A08 RID: 10760
	public bool EventActive;

	// Token: 0x04002A09 RID: 10761
	public float Distance;

	// Token: 0x04002A0A RID: 10762
	public float Scale;

	// Token: 0x04002A0B RID: 10763
	public float Timer;

	// Token: 0x04002A0C RID: 10764
	public int SpeechPhase = 1;

	// Token: 0x04002A0D RID: 10765
	public int RivalID = 11;

	// Token: 0x04002A0E RID: 10766
	public int Phase;

	// Token: 0x04002A0F RID: 10767
	public int Frame;
}
