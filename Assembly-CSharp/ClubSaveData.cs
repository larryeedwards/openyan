using System;

// Token: 0x020004DE RID: 1246
[Serializable]
public class ClubSaveData
{
	// Token: 0x06001F68 RID: 8040 RVA: 0x00140C30 File Offset: 0x0013F030
	public static ClubSaveData ReadFromGlobals()
	{
		ClubSaveData clubSaveData = new ClubSaveData();
		clubSaveData.club = ClubGlobals.Club;
		foreach (ClubType clubType in ClubGlobals.KeysOfClubClosed())
		{
			if (ClubGlobals.GetClubClosed(clubType))
			{
				clubSaveData.clubClosed.Add(clubType);
			}
		}
		foreach (ClubType clubType2 in ClubGlobals.KeysOfClubKicked())
		{
			if (ClubGlobals.GetClubKicked(clubType2))
			{
				clubSaveData.clubKicked.Add(clubType2);
			}
		}
		foreach (ClubType clubType3 in ClubGlobals.KeysOfQuitClub())
		{
			if (ClubGlobals.GetQuitClub(clubType3))
			{
				clubSaveData.quitClub.Add(clubType3);
			}
		}
		return clubSaveData;
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x00140D08 File Offset: 0x0013F108
	public static void WriteToGlobals(ClubSaveData data)
	{
		ClubGlobals.Club = data.club;
		foreach (ClubType clubID in data.clubClosed)
		{
			ClubGlobals.SetClubClosed(clubID, true);
		}
		foreach (ClubType clubID2 in data.clubKicked)
		{
			ClubGlobals.SetClubKicked(clubID2, true);
		}
		foreach (ClubType clubID3 in data.quitClub)
		{
			ClubGlobals.SetQuitClub(clubID3, true);
		}
	}

	// Token: 0x04002A9C RID: 10908
	public ClubType club;

	// Token: 0x04002A9D RID: 10909
	public ClubTypeHashSet clubClosed = new ClubTypeHashSet();

	// Token: 0x04002A9E RID: 10910
	public ClubTypeHashSet clubKicked = new ClubTypeHashSet();

	// Token: 0x04002A9F RID: 10911
	public ClubTypeHashSet quitClub = new ClubTypeHashSet();
}
