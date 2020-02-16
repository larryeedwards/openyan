using System;
using UnityEngine;

// Token: 0x020004C7 RID: 1223
public class OsanaFridayBeforeClassEvent2Script : MonoBehaviour
{
	// Token: 0x040029DB RID: 10715
	public OsanaFridayBeforeClassEvent1Script OtherEvent;

	// Token: 0x040029DC RID: 10716
	public StudentManagerScript StudentManager;

	// Token: 0x040029DD RID: 10717
	public AudioSoftwareScript AudioSoftware;

	// Token: 0x040029DE RID: 10718
	public JukeboxScript Jukebox;

	// Token: 0x040029DF RID: 10719
	public UILabel EventSubtitle;

	// Token: 0x040029E0 RID: 10720
	public YandereScript Yandere;

	// Token: 0x040029E1 RID: 10721
	public ClockScript Clock;

	// Token: 0x040029E2 RID: 10722
	public SpyScript Spy;

	// Token: 0x040029E3 RID: 10723
	public StudentScript Ganguro;

	// Token: 0x040029E4 RID: 10724
	public StudentScript Friend;

	// Token: 0x040029E5 RID: 10725
	public StudentScript Rival;

	// Token: 0x040029E6 RID: 10726
	public Transform[] Location;

	// Token: 0x040029E7 RID: 10727
	public AudioClip[] SpeechClip;

	// Token: 0x040029E8 RID: 10728
	public string[] SpeechText;

	// Token: 0x040029E9 RID: 10729
	public float[] SpeechTime;

	// Token: 0x040029EA RID: 10730
	public string[] EventAnim;

	// Token: 0x040029EB RID: 10731
	public GameObject AlarmDisc;

	// Token: 0x040029EC RID: 10732
	public GameObject VoiceClip;

	// Token: 0x040029ED RID: 10733
	public Quaternion targetRotation;

	// Token: 0x040029EE RID: 10734
	public float Distance;

	// Token: 0x040029EF RID: 10735
	public float Scale;

	// Token: 0x040029F0 RID: 10736
	public float Timer;

	// Token: 0x040029F1 RID: 10737
	public DayOfWeek EventDay;

	// Token: 0x040029F2 RID: 10738
	public int SpeechPhase = 1;

	// Token: 0x040029F3 RID: 10739
	public int GanguroID = 81;

	// Token: 0x040029F4 RID: 10740
	public int FriendID = 10;

	// Token: 0x040029F5 RID: 10741
	public int RivalID = 11;

	// Token: 0x040029F6 RID: 10742
	public int Phase;

	// Token: 0x040029F7 RID: 10743
	public int Frame;

	// Token: 0x040029F8 RID: 10744
	public Vector3 OriginalPosition;

	// Token: 0x040029F9 RID: 10745
	public Vector3 OriginalRotation;
}
