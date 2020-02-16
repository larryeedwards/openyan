using System;
using UnityEngine;

// Token: 0x020003FA RID: 1018
public static class SaveFileGlobals
{
	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x06001B39 RID: 6969 RVA: 0x000F8B7F File Offset: 0x000F6F7F
	// (set) Token: 0x06001B3A RID: 6970 RVA: 0x000F8B9F File Offset: 0x000F6F9F
	public static int CurrentSaveFile
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentSaveFile");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentSaveFile", value);
		}
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x000F8BC0 File Offset: 0x000F6FC0
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentSaveFile");
	}

	// Token: 0x04001FF0 RID: 8176
	private const string Str_CurrentSaveFile = "CurrentSaveFile";
}
