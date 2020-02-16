using System;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public static class ApplicationGlobals
{
	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x06001A0A RID: 6666 RVA: 0x000F4BD0 File Offset: 0x000F2FD0
	// (set) Token: 0x06001A0B RID: 6667 RVA: 0x000F4BF0 File Offset: 0x000F2FF0
	public static float VersionNumber
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_VersionNumber");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_VersionNumber", value);
		}
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x000F4C11 File Offset: 0x000F3011
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_VersionNumber");
	}

	// Token: 0x04001F6C RID: 8044
	private const string Str_VersionNumber = "VersionNumber";
}
