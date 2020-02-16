using System;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class OsanaFridayBeforeClassEvent1Script : MonoBehaviour
{
	// Token: 0x06001F36 RID: 7990 RVA: 0x0013F5E6 File Offset: 0x0013D9E6
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		if (DateGlobals.Weekday != this.EventDay)
		{
			base.enabled = false;
		}
		this.Yoogle.SetActive(false);
	}

	// Token: 0x040029C4 RID: 10692
	public OsanaFridayBeforeClassEvent2Script OtherEvent;

	// Token: 0x040029C5 RID: 10693
	public StudentManagerScript StudentManager;

	// Token: 0x040029C6 RID: 10694
	public JukeboxScript Jukebox;

	// Token: 0x040029C7 RID: 10695
	public UILabel EventSubtitle;

	// Token: 0x040029C8 RID: 10696
	public YandereScript Yandere;

	// Token: 0x040029C9 RID: 10697
	public ClockScript Clock;

	// Token: 0x040029CA RID: 10698
	public StudentScript Rival;

	// Token: 0x040029CB RID: 10699
	public Transform Location;

	// Token: 0x040029CC RID: 10700
	public AudioClip[] SpeechClip;

	// Token: 0x040029CD RID: 10701
	public string[] SpeechText;

	// Token: 0x040029CE RID: 10702
	public string EventAnim;

	// Token: 0x040029CF RID: 10703
	public GameObject AlarmDisc;

	// Token: 0x040029D0 RID: 10704
	public GameObject VoiceClip;

	// Token: 0x040029D1 RID: 10705
	public GameObject Yoogle;

	// Token: 0x040029D2 RID: 10706
	public float Distance;

	// Token: 0x040029D3 RID: 10707
	public float Scale;

	// Token: 0x040029D4 RID: 10708
	public float Timer;

	// Token: 0x040029D5 RID: 10709
	public DayOfWeek EventDay;

	// Token: 0x040029D6 RID: 10710
	public int RivalID = 11;

	// Token: 0x040029D7 RID: 10711
	public int Phase;

	// Token: 0x040029D8 RID: 10712
	public int Frame;

	// Token: 0x040029D9 RID: 10713
	public Vector3 OriginalPosition;

	// Token: 0x040029DA RID: 10714
	public Vector3 OriginalRotation;
}
