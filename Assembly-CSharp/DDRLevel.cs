using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class DDRLevel : ScriptableObject
{
	// Token: 0x04000730 RID: 1840
	public string LevelName;

	// Token: 0x04000731 RID: 1841
	public AudioClip Song;

	// Token: 0x04000732 RID: 1842
	public Sprite LevelIcon;

	// Token: 0x04000733 RID: 1843
	public DDRTrack[] Tracks;

	// Token: 0x04000734 RID: 1844
	[Header("Points per score")]
	public int PerfectScorePoints;

	// Token: 0x04000735 RID: 1845
	public int GreatScorePoints;

	// Token: 0x04000736 RID: 1846
	public int GoodScorePoints;

	// Token: 0x04000737 RID: 1847
	public int OkScorePoints;

	// Token: 0x04000738 RID: 1848
	public int EarlyScorePoints;

	// Token: 0x04000739 RID: 1849
	public int MissScorePoints;
}
