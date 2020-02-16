using System;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public static class DatingGlobals
{
	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001A63 RID: 6755 RVA: 0x000F62A5 File Offset: 0x000F46A5
	// (set) Token: 0x06001A64 RID: 6756 RVA: 0x000F62C5 File Offset: 0x000F46C5
	public static float Affection
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Affection");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Affection", value);
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001A65 RID: 6757 RVA: 0x000F62E6 File Offset: 0x000F46E6
	// (set) Token: 0x06001A66 RID: 6758 RVA: 0x000F6306 File Offset: 0x000F4706
	public static float AffectionLevel
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_AffectionLevel");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_AffectionLevel", value);
		}
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x000F6327 File Offset: 0x000F4727
	public static bool GetComplimentGiven(int complimentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ComplimentGiven_",
			complimentID.ToString()
		}));
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x000F6368 File Offset: 0x000F4768
	public static void SetComplimentGiven(int complimentID, bool value)
	{
		string text = complimentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ComplimentGiven_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ComplimentGiven_",
			text
		}), value);
	}

	// Token: 0x06001A69 RID: 6761 RVA: 0x000F63D4 File Offset: 0x000F47D4
	public static int[] KeysOfComplimentGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_ComplimentGiven_");
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x000F63F4 File Offset: 0x000F47F4
	public static bool GetSuitorCheck(int checkID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorCheck_",
			checkID.ToString()
		}));
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x000F6434 File Offset: 0x000F4834
	public static void SetSuitorCheck(int checkID, bool value)
	{
		string text = checkID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SuitorCheck_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorCheck_",
			text
		}), value);
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x000F64A0 File Offset: 0x000F48A0
	public static int[] KeysOfSuitorCheck()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SuitorCheck_");
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001A6D RID: 6765 RVA: 0x000F64C0 File Offset: 0x000F48C0
	// (set) Token: 0x06001A6E RID: 6766 RVA: 0x000F64E0 File Offset: 0x000F48E0
	public static int SuitorProgress
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SuitorProgress");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SuitorProgress", value);
		}
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x000F6501 File Offset: 0x000F4901
	public static int GetSuitorTrait(int traitID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorTrait_",
			traitID.ToString()
		}));
	}

	// Token: 0x06001A70 RID: 6768 RVA: 0x000F6540 File Offset: 0x000F4940
	public static void SetSuitorTrait(int traitID, int value)
	{
		string text = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SuitorTrait_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SuitorTrait_",
			text
		}), value);
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x000F65AC File Offset: 0x000F49AC
	public static int[] KeysOfSuitorTrait()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SuitorTrait_");
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x000F65CC File Offset: 0x000F49CC
	public static bool GetTopicDiscussed(int topicID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscussed_",
			topicID.ToString()
		}));
	}

	// Token: 0x06001A73 RID: 6771 RVA: 0x000F660C File Offset: 0x000F4A0C
	public static void SetTopicDiscussed(int topicID, bool value)
	{
		string text = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicDiscussed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscussed_",
			text
		}), value);
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x000F6678 File Offset: 0x000F4A78
	public static int[] KeysOfTopicDiscussed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TopicDiscussed_");
	}

	// Token: 0x06001A75 RID: 6773 RVA: 0x000F6698 File Offset: 0x000F4A98
	public static int GetTraitDemonstrated(int traitID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TraitDemonstrated_",
			traitID.ToString()
		}));
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x000F66D8 File Offset: 0x000F4AD8
	public static void SetTraitDemonstrated(int traitID, int value)
	{
		string text = traitID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TraitDemonstrated_",
			text
		}), value);
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000F6744 File Offset: 0x000F4B44
	public static int[] KeysOfTraitDemonstrated()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_");
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001A78 RID: 6776 RVA: 0x000F6764 File Offset: 0x000F4B64
	// (set) Token: 0x06001A79 RID: 6777 RVA: 0x000F6784 File Offset: 0x000F4B84
	public static int RivalSabotaged
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_RivalSabotaged");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_RivalSabotaged", value);
		}
	}

	// Token: 0x06001A7A RID: 6778 RVA: 0x000F67A8 File Offset: 0x000F4BA8
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Affection");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AffectionLevel");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ComplimentGiven_", DatingGlobals.KeysOfComplimentGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SuitorCheck_", DatingGlobals.KeysOfSuitorCheck());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SuitorProgress");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RivalSabotaged");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SuitorTrait_", DatingGlobals.KeysOfSuitorTrait());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TopicDiscussed_", DatingGlobals.KeysOfTopicDiscussed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TraitDemonstrated_", DatingGlobals.KeysOfTraitDemonstrated());
	}

	// Token: 0x04001F90 RID: 8080
	private const string Str_Affection = "Affection";

	// Token: 0x04001F91 RID: 8081
	private const string Str_AffectionLevel = "AffectionLevel";

	// Token: 0x04001F92 RID: 8082
	private const string Str_ComplimentGiven = "ComplimentGiven_";

	// Token: 0x04001F93 RID: 8083
	private const string Str_SuitorCheck = "SuitorCheck_";

	// Token: 0x04001F94 RID: 8084
	private const string Str_SuitorProgress = "SuitorProgress";

	// Token: 0x04001F95 RID: 8085
	private const string Str_SuitorTrait = "SuitorTrait_";

	// Token: 0x04001F96 RID: 8086
	private const string Str_TopicDiscussed = "TopicDiscussed_";

	// Token: 0x04001F97 RID: 8087
	private const string Str_TraitDemonstrated = "TraitDemonstrated_";

	// Token: 0x04001F98 RID: 8088
	private const string Str_RivalSabotaged = "RivalSabotaged";
}
