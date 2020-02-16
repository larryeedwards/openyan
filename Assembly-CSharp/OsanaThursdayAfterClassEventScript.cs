using System;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class OsanaThursdayAfterClassEventScript : MonoBehaviour
{
	// Token: 0x04002A3B RID: 10811
	public StudentManagerScript StudentManager;

	// Token: 0x04002A3C RID: 10812
	public PhoneMinigameScript PhoneMinigame;

	// Token: 0x04002A3D RID: 10813
	public JukeboxScript Jukebox;

	// Token: 0x04002A3E RID: 10814
	public UILabel EventSubtitle;

	// Token: 0x04002A3F RID: 10815
	public YandereScript Yandere;

	// Token: 0x04002A40 RID: 10816
	public ClockScript Clock;

	// Token: 0x04002A41 RID: 10817
	public StudentScript Friend;

	// Token: 0x04002A42 RID: 10818
	public StudentScript Rival;

	// Token: 0x04002A43 RID: 10819
	public Transform FriendLocation;

	// Token: 0x04002A44 RID: 10820
	public Transform Location;

	// Token: 0x04002A45 RID: 10821
	public AudioClip[] SpeechClip;

	// Token: 0x04002A46 RID: 10822
	public string[] SpeechText;

	// Token: 0x04002A47 RID: 10823
	public string[] EventAnim;

	// Token: 0x04002A48 RID: 10824
	public GameObject AlarmDisc;

	// Token: 0x04002A49 RID: 10825
	public GameObject VoiceClip;

	// Token: 0x04002A4A RID: 10826
	public float FriendWarningTimer;

	// Token: 0x04002A4B RID: 10827
	public float Distance;

	// Token: 0x04002A4C RID: 10828
	public float Scale;

	// Token: 0x04002A4D RID: 10829
	public float Timer;

	// Token: 0x04002A4E RID: 10830
	public DayOfWeek EventDay;

	// Token: 0x04002A4F RID: 10831
	public int FriendID = 10;

	// Token: 0x04002A50 RID: 10832
	public int RivalID = 11;

	// Token: 0x04002A51 RID: 10833
	public int Phase;

	// Token: 0x04002A52 RID: 10834
	public int Frame;

	// Token: 0x04002A53 RID: 10835
	public bool FriendWarned;

	// Token: 0x04002A54 RID: 10836
	public bool Sabotaged;

	// Token: 0x04002A55 RID: 10837
	public Vector3 OriginalPosition;

	// Token: 0x04002A56 RID: 10838
	public Vector3 OriginalRotation;
}
