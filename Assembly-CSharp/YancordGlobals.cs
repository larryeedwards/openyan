using System;
using UnityEngine;

// Token: 0x02000404 RID: 1028
public static class YancordGlobals
{
	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06001C49 RID: 7241 RVA: 0x000FC879 File Offset: 0x000FAC79
	// (set) Token: 0x06001C4A RID: 7242 RVA: 0x000FC899 File Offset: 0x000FAC99
	public static bool JoinedYancord
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_JoinedYancord");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_JoinedYancord", value);
		}
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06001C4B RID: 7243 RVA: 0x000FC8BA File Offset: 0x000FACBA
	// (set) Token: 0x06001C4C RID: 7244 RVA: 0x000FC8DA File Offset: 0x000FACDA
	public static int CurrentConversation
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentConversation");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentConversation", value);
		}
	}

	// Token: 0x06001C4D RID: 7245 RVA: 0x000FC8FB File Offset: 0x000FACFB
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_JoinedYancord");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentConversation");
	}

	// Token: 0x04002063 RID: 8291
	private const string Str_JoinedYancord = "JoinedYancord";

	// Token: 0x04002064 RID: 8292
	private const string Str_CurrentConversation = "CurrentConversation";
}
