using System;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public static class GameGlobals
{
	// Token: 0x170003DA RID: 986
	// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000F6B83 File Offset: 0x000F4F83
	// (set) Token: 0x06001A8B RID: 6795 RVA: 0x000F6B8F File Offset: 0x000F4F8F
	public static int Profile
	{
		get
		{
			return PlayerPrefs.GetInt("Profile");
		}
		set
		{
			PlayerPrefs.SetInt("Profile", value);
		}
	}

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x06001A8C RID: 6796 RVA: 0x000F6B9C File Offset: 0x000F4F9C
	// (set) Token: 0x06001A8D RID: 6797 RVA: 0x000F6BBC File Offset: 0x000F4FBC
	public static bool LoveSick
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LoveSick");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LoveSick", value);
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x06001A8E RID: 6798 RVA: 0x000F6BDD File Offset: 0x000F4FDD
	// (set) Token: 0x06001A8F RID: 6799 RVA: 0x000F6BFD File Offset: 0x000F4FFD
	public static bool MasksBanned
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MasksBanned");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MasksBanned", value);
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x06001A90 RID: 6800 RVA: 0x000F6C1E File Offset: 0x000F501E
	// (set) Token: 0x06001A91 RID: 6801 RVA: 0x000F6C3E File Offset: 0x000F503E
	public static bool Paranormal
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Paranormal");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Paranormal", value);
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06001A92 RID: 6802 RVA: 0x000F6C5F File Offset: 0x000F505F
	// (set) Token: 0x06001A93 RID: 6803 RVA: 0x000F6C7F File Offset: 0x000F507F
	public static bool EasyMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EasyMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EasyMode", value);
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06001A94 RID: 6804 RVA: 0x000F6CA0 File Offset: 0x000F50A0
	// (set) Token: 0x06001A95 RID: 6805 RVA: 0x000F6CC0 File Offset: 0x000F50C0
	public static bool HardMode
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HardMode");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HardMode", value);
		}
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06001A96 RID: 6806 RVA: 0x000F6CE1 File Offset: 0x000F50E1
	// (set) Token: 0x06001A97 RID: 6807 RVA: 0x000F6D01 File Offset: 0x000F5101
	public static bool EmptyDemon
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EmptyDemon");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EmptyDemon", value);
		}
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000F6D22 File Offset: 0x000F5122
	// (set) Token: 0x06001A99 RID: 6809 RVA: 0x000F6D42 File Offset: 0x000F5142
	public static bool CensorBlood
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CensorBlood");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CensorBlood", value);
		}
	}

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000F6D63 File Offset: 0x000F5163
	// (set) Token: 0x06001A9B RID: 6811 RVA: 0x000F6D83 File Offset: 0x000F5183
	public static bool SpareUniform
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SpareUniform");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SpareUniform", value);
		}
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x06001A9C RID: 6812 RVA: 0x000F6DA4 File Offset: 0x000F51A4
	// (set) Token: 0x06001A9D RID: 6813 RVA: 0x000F6DC4 File Offset: 0x000F51C4
	public static bool BlondeHair
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_BlondeHair");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_BlondeHair", value);
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06001A9E RID: 6814 RVA: 0x000F6DE5 File Offset: 0x000F51E5
	// (set) Token: 0x06001A9F RID: 6815 RVA: 0x000F6E05 File Offset: 0x000F5205
	public static bool SenpaiMourning
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SenpaiMourning");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SenpaiMourning", value);
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000F6E26 File Offset: 0x000F5226
	// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x000F6E32 File Offset: 0x000F5232
	public static int RivalEliminationID
	{
		get
		{
			return PlayerPrefs.GetInt("RivalEliminationID");
		}
		set
		{
			PlayerPrefs.SetInt("RivalEliminationID", value);
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000F6E3F File Offset: 0x000F523F
	// (set) Token: 0x06001AA3 RID: 6819 RVA: 0x000F6E5F File Offset: 0x000F525F
	public static bool ReputationsInitialized
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ReputationsInitialized");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ReputationsInitialized", value);
		}
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000F6E80 File Offset: 0x000F5280
	// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x000F6EA0 File Offset: 0x000F52A0
	public static bool AnswerSheetUnavailable
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable", value);
		}
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x000F6EC4 File Offset: 0x000F52C4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LoveSick");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MasksBanned");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Paranormal");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EasyMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HardMode");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EmptyDemon");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CensorBlood");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SpareUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BlondeHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiMourning");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RivalEliminationID");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ReputationsInitialized");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_AnswerSheetUnavailable");
	}

	// Token: 0x04001FA0 RID: 8096
	private const string Str_Profile = "Profile";

	// Token: 0x04001FA1 RID: 8097
	private const string Str_LoveSick = "LoveSick";

	// Token: 0x04001FA2 RID: 8098
	private const string Str_MasksBanned = "MasksBanned";

	// Token: 0x04001FA3 RID: 8099
	private const string Str_Paranormal = "Paranormal";

	// Token: 0x04001FA4 RID: 8100
	private const string Str_EasyMode = "EasyMode";

	// Token: 0x04001FA5 RID: 8101
	private const string Str_HardMode = "HardMode";

	// Token: 0x04001FA6 RID: 8102
	private const string Str_EmptyDemon = "EmptyDemon";

	// Token: 0x04001FA7 RID: 8103
	private const string Str_CensorBlood = "CensorBlood";

	// Token: 0x04001FA8 RID: 8104
	private const string Str_SpareUniform = "SpareUniform";

	// Token: 0x04001FA9 RID: 8105
	private const string Str_BlondeHair = "BlondeHair";

	// Token: 0x04001FAA RID: 8106
	private const string Str_SenpaiMourning = "SenpaiMourning";

	// Token: 0x04001FAB RID: 8107
	private const string Str_RivalEliminationID = "RivalEliminationID";

	// Token: 0x04001FAC RID: 8108
	private const string Str_ReputationsInitialized = "ReputationsInitialized";

	// Token: 0x04001FAD RID: 8109
	private const string Str_AnswerSheetUnavailable = "AnswerSheetUnavailable";
}
