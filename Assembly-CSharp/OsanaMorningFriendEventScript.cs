using System;
using UnityEngine;

// Token: 0x020004CB RID: 1227
public class OsanaMorningFriendEventScript : MonoBehaviour
{
	// Token: 0x04002A10 RID: 10768
	public RivalMorningEventManagerScript OtherEvent;

	// Token: 0x04002A11 RID: 10769
	public StudentManagerScript StudentManager;

	// Token: 0x04002A12 RID: 10770
	public EndOfDayScript EndOfDay;

	// Token: 0x04002A13 RID: 10771
	public JukeboxScript Jukebox;

	// Token: 0x04002A14 RID: 10772
	public UILabel EventSubtitle;

	// Token: 0x04002A15 RID: 10773
	public YandereScript Yandere;

	// Token: 0x04002A16 RID: 10774
	public ClockScript Clock;

	// Token: 0x04002A17 RID: 10775
	public SpyScript Spy;

	// Token: 0x04002A18 RID: 10776
	public StudentScript CurrentSpeaker;

	// Token: 0x04002A19 RID: 10777
	public StudentScript Friend;

	// Token: 0x04002A1A RID: 10778
	public StudentScript Rival;

	// Token: 0x04002A1B RID: 10779
	public Transform Epicenter;

	// Token: 0x04002A1C RID: 10780
	public Transform[] Location;

	// Token: 0x04002A1D RID: 10781
	public AudioClip SpeechClip;

	// Token: 0x04002A1E RID: 10782
	public string[] SpeechText;

	// Token: 0x04002A1F RID: 10783
	public float[] SpeechTime;

	// Token: 0x04002A20 RID: 10784
	public string[] EventAnim;

	// Token: 0x04002A21 RID: 10785
	public int[] Speaker;

	// Token: 0x04002A22 RID: 10786
	public AudioClip InterruptedClip;

	// Token: 0x04002A23 RID: 10787
	public string[] InterruptedSpeech;

	// Token: 0x04002A24 RID: 10788
	public float[] InterruptedTime;

	// Token: 0x04002A25 RID: 10789
	public string[] InterruptedAnim;

	// Token: 0x04002A26 RID: 10790
	public int[] InterruptedSpeaker;

	// Token: 0x04002A27 RID: 10791
	public AudioClip AltSpeechClip;

	// Token: 0x04002A28 RID: 10792
	public string[] AltSpeechText;

	// Token: 0x04002A29 RID: 10793
	public float[] AltSpeechTime;

	// Token: 0x04002A2A RID: 10794
	public string[] AltEventAnim;

	// Token: 0x04002A2B RID: 10795
	public int[] AltSpeaker;

	// Token: 0x04002A2C RID: 10796
	public GameObject AlarmDisc;

	// Token: 0x04002A2D RID: 10797
	public GameObject VoiceClip;

	// Token: 0x04002A2E RID: 10798
	public Quaternion targetRotation;

	// Token: 0x04002A2F RID: 10799
	public float Distance;

	// Token: 0x04002A30 RID: 10800
	public float Scale;

	// Token: 0x04002A31 RID: 10801
	public float Timer;

	// Token: 0x04002A32 RID: 10802
	public DayOfWeek EventDay;

	// Token: 0x04002A33 RID: 10803
	public int SpeechPhase = 1;

	// Token: 0x04002A34 RID: 10804
	public int FriendID = 6;

	// Token: 0x04002A35 RID: 10805
	public int RivalID = 11;

	// Token: 0x04002A36 RID: 10806
	public int Phase;

	// Token: 0x04002A37 RID: 10807
	public int Frame;

	// Token: 0x04002A38 RID: 10808
	public Vector3 OriginalPosition;

	// Token: 0x04002A39 RID: 10809
	public Vector3 OriginalRotation;

	// Token: 0x04002A3A RID: 10810
	public bool LosingFriend;
}
