using System;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public static class SchemeGlobals
{
	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x06001B3C RID: 6972 RVA: 0x000F8BE0 File Offset: 0x000F6FE0
	// (set) Token: 0x06001B3D RID: 6973 RVA: 0x000F8C00 File Offset: 0x000F7000
	public static int CurrentScheme
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CurrentScheme");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CurrentScheme", value);
		}
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000F8C21 File Offset: 0x000F7021
	// (set) Token: 0x06001B3F RID: 6975 RVA: 0x000F8C41 File Offset: 0x000F7041
	public static bool DarkSecret
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DarkSecret");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DarkSecret", value);
		}
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x000F8C62 File Offset: 0x000F7062
	public static int GetSchemePreviousStage(int schemeID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemePreviousStage_",
			schemeID.ToString()
		}));
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x000F8CA4 File Offset: 0x000F70A4
	public static void SetSchemePreviousStage(int schemeID, int value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemePreviousStage_",
			text
		}), value);
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x000F8D10 File Offset: 0x000F7110
	public static int[] KeysOfSchemePreviousStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_");
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x000F8D30 File Offset: 0x000F7130
	public static int GetSchemeStage(int schemeID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStage_",
			schemeID.ToString()
		}));
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000F8D70 File Offset: 0x000F7170
	public static void SetSchemeStage(int schemeID, int value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeStage_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStage_",
			text
		}), value);
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x000F8DDC File Offset: 0x000F71DC
	public static int[] KeysOfSchemeStage()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeStage_");
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x000F8DFC File Offset: 0x000F71FC
	public static bool GetSchemeStatus(int schemeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStatus_",
			schemeID.ToString()
		}));
	}

	// Token: 0x06001B47 RID: 6983 RVA: 0x000F8E3C File Offset: 0x000F723C
	public static void SetSchemeStatus(int schemeID, bool value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeStatus_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeStatus_",
			text
		}), value);
	}

	// Token: 0x06001B48 RID: 6984 RVA: 0x000F8EA8 File Offset: 0x000F72A8
	public static int[] KeysOfSchemeStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeStatus_");
	}

	// Token: 0x06001B49 RID: 6985 RVA: 0x000F8EC8 File Offset: 0x000F72C8
	public static bool GetSchemeUnlocked(int schemeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeUnlocked_",
			schemeID.ToString()
		}));
	}

	// Token: 0x06001B4A RID: 6986 RVA: 0x000F8F08 File Offset: 0x000F7308
	public static void SetSchemeUnlocked(int schemeID, bool value)
	{
		string text = schemeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SchemeUnlocked_",
			text
		}), value);
	}

	// Token: 0x06001B4B RID: 6987 RVA: 0x000F8F74 File Offset: 0x000F7374
	public static int[] KeysOfSchemeUnlocked()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_");
	}

	// Token: 0x06001B4C RID: 6988 RVA: 0x000F8F94 File Offset: 0x000F7394
	public static bool GetServicePurchased(int serviceID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ServicePurchased_",
			serviceID.ToString()
		}));
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x000F8FD4 File Offset: 0x000F73D4
	public static void SetServicePurchased(int serviceID, bool value)
	{
		string text = serviceID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ServicePurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ServicePurchased_",
			text
		}), value);
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x000F9040 File Offset: 0x000F7440
	public static int[] KeysOfServicePurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_ServicePurchased_");
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x000F9060 File Offset: 0x000F7460
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CurrentScheme");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DarkSecret");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemePreviousStage_", SchemeGlobals.KeysOfSchemePreviousStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeStage_", SchemeGlobals.KeysOfSchemeStage());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeStatus_", SchemeGlobals.KeysOfSchemeStatus());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SchemeUnlocked_", SchemeGlobals.KeysOfSchemeUnlocked());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ServicePurchased_", SchemeGlobals.KeysOfServicePurchased());
	}

	// Token: 0x04001FF1 RID: 8177
	private const string Str_CurrentScheme = "CurrentScheme";

	// Token: 0x04001FF2 RID: 8178
	private const string Str_DarkSecret = "DarkSecret";

	// Token: 0x04001FF3 RID: 8179
	private const string Str_SchemePreviousStage = "SchemePreviousStage_";

	// Token: 0x04001FF4 RID: 8180
	private const string Str_SchemeStage = "SchemeStage_";

	// Token: 0x04001FF5 RID: 8181
	private const string Str_SchemeStatus = "SchemeStatus_";

	// Token: 0x04001FF6 RID: 8182
	private const string Str_SchemeUnlocked = "SchemeUnlocked_";

	// Token: 0x04001FF7 RID: 8183
	private const string Str_ServicePurchased = "ServicePurchased_";
}
