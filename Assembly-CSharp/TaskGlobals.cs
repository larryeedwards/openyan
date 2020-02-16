using System;
using UnityEngine;

// Token: 0x020003FF RID: 1023
public static class TaskGlobals
{
	// Token: 0x06001BE1 RID: 7137 RVA: 0x000FB407 File Offset: 0x000F9807
	public static bool GetGuitarPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GuitarPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000FB448 File Offset: 0x000F9848
	public static void SetGuitarPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GuitarPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GuitarPhoto_",
			text
		}), value);
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000FB4B4 File Offset: 0x000F98B4
	public static int[] KeysOfGuitarPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GuitarPhoto_");
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000FB4D4 File Offset: 0x000F98D4
	public static bool GetKittenPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_KittenPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000FB514 File Offset: 0x000F9914
	public static void SetKittenPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_KittenPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_KittenPhoto_",
			text
		}), value);
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000FB580 File Offset: 0x000F9980
	public static int[] KeysOfKittenPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_KittenPhoto_");
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000FB5A0 File Offset: 0x000F99A0
	public static bool GetHorudaPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HorudaPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000FB5E0 File Offset: 0x000F99E0
	public static void SetHorudaPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HorudaPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HorudaPhoto_",
			text
		}), value);
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x000FB64C File Offset: 0x000F9A4C
	public static int[] KeysOfHorudaPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_HorudaPhoto_");
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x000FB66C File Offset: 0x000F9A6C
	public static int GetTaskStatus(int taskID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TaskStatus_",
			taskID.ToString()
		}));
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x000FB6AC File Offset: 0x000F9AAC
	public static void SetTaskStatus(int taskID, int value)
	{
		string text = taskID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TaskStatus_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TaskStatus_",
			text
		}), value);
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000FB718 File Offset: 0x000F9B18
	public static int[] KeysOfTaskStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TaskStatus_");
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x000FB738 File Offset: 0x000F9B38
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GuitarPhoto_", TaskGlobals.KeysOfGuitarPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_KittenPhoto_", TaskGlobals.KeysOfKittenPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_HorudaPhoto_", TaskGlobals.KeysOfHorudaPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TaskStatus_", TaskGlobals.KeysOfTaskStatus());
	}

	// Token: 0x04002034 RID: 8244
	private const string Str_GuitarPhoto = "GuitarPhoto_";

	// Token: 0x04002035 RID: 8245
	private const string Str_KittenPhoto = "KittenPhoto_";

	// Token: 0x04002036 RID: 8246
	private const string Str_HorudaPhoto = "HorudaPhoto_";

	// Token: 0x04002037 RID: 8247
	private const string Str_TaskStatus = "TaskStatus_";
}
