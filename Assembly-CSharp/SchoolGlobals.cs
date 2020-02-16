using System;
using UnityEngine;

// Token: 0x020003FC RID: 1020
public static class SchoolGlobals
{
	// Token: 0x06001B50 RID: 6992 RVA: 0x000F9158 File Offset: 0x000F7558
	public static bool GetDemonActive(int demonID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_DemonActive_",
			demonID.ToString()
		}));
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x000F9198 File Offset: 0x000F7598
	public static void SetDemonActive(int demonID, bool value)
	{
		string text = demonID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_DemonActive_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_DemonActive_",
			text
		}), value);
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x000F9204 File Offset: 0x000F7604
	public static int[] KeysOfDemonActive()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_DemonActive_");
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x000F9224 File Offset: 0x000F7624
	public static bool GetGardenGraveOccupied(int graveID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GardenGraveOccupied_",
			graveID.ToString()
		}));
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000F9264 File Offset: 0x000F7664
	public static void SetGardenGraveOccupied(int graveID, bool value)
	{
		string text = graveID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GardenGraveOccupied_",
			text
		}), value);
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000F92D0 File Offset: 0x000F76D0
	public static int[] KeysOfGardenGraveOccupied()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_");
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x06001B56 RID: 6998 RVA: 0x000F92F0 File Offset: 0x000F76F0
	// (set) Token: 0x06001B57 RID: 6999 RVA: 0x000F9310 File Offset: 0x000F7710
	public static int KidnapVictim
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_KidnapVictim");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_KidnapVictim", value);
		}
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x06001B58 RID: 7000 RVA: 0x000F9331 File Offset: 0x000F7731
	// (set) Token: 0x06001B59 RID: 7001 RVA: 0x000F9351 File Offset: 0x000F7751
	public static int Population
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Population");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Population", value);
		}
	}

	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000F9372 File Offset: 0x000F7772
	// (set) Token: 0x06001B5B RID: 7003 RVA: 0x000F9392 File Offset: 0x000F7792
	public static bool RoofFence
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RoofFence");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RoofFence", value);
		}
	}

	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000F93B3 File Offset: 0x000F77B3
	// (set) Token: 0x06001B5D RID: 7005 RVA: 0x000F93D3 File Offset: 0x000F77D3
	public static float SchoolAtmosphere
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere", value);
		}
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x06001B5E RID: 7006 RVA: 0x000F93F4 File Offset: 0x000F77F4
	// (set) Token: 0x06001B5F RID: 7007 RVA: 0x000F9414 File Offset: 0x000F7814
	public static bool SchoolAtmosphereSet
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet", value);
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000F9435 File Offset: 0x000F7835
	// (set) Token: 0x06001B61 RID: 7009 RVA: 0x000F9455 File Offset: 0x000F7855
	public static bool ReactedToGameLeader
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader", value);
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x06001B62 RID: 7010 RVA: 0x000F9476 File Offset: 0x000F7876
	// (set) Token: 0x06001B63 RID: 7011 RVA: 0x000F9496 File Offset: 0x000F7896
	public static bool HighSecurity
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HighSecurity");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HighSecurity", value);
		}
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000F94B7 File Offset: 0x000F78B7
	// (set) Token: 0x06001B65 RID: 7013 RVA: 0x000F94D7 File Offset: 0x000F78D7
	public static bool SCP
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_SCP");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_SCP", value);
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000F94F8 File Offset: 0x000F78F8
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_DemonActive_", SchoolGlobals.KeysOfDemonActive());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GardenGraveOccupied_", SchoolGlobals.KeysOfGardenGraveOccupied());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_KidnapVictim");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Population");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RoofFence");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SchoolAtmosphere");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SchoolAtmosphereSet");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ReactedToGameLeader");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HighSecurity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SCP");
	}

	// Token: 0x04001FF8 RID: 8184
	private const string Str_DemonActive = "DemonActive_";

	// Token: 0x04001FF9 RID: 8185
	private const string Str_GardenGraveOccupied = "GardenGraveOccupied_";

	// Token: 0x04001FFA RID: 8186
	private const string Str_KidnapVictim = "KidnapVictim";

	// Token: 0x04001FFB RID: 8187
	private const string Str_Population = "Population";

	// Token: 0x04001FFC RID: 8188
	private const string Str_RoofFence = "RoofFence";

	// Token: 0x04001FFD RID: 8189
	private const string Str_SchoolAtmosphere = "SchoolAtmosphere";

	// Token: 0x04001FFE RID: 8190
	private const string Str_SchoolAtmosphereSet = "SchoolAtmosphereSet";

	// Token: 0x04001FFF RID: 8191
	private const string Str_ReactedToGameLeader = "ReactedToGameLeader";

	// Token: 0x04002000 RID: 8192
	private const string Str_SCP = "SCP";

	// Token: 0x04002001 RID: 8193
	private const string Str_HighSecurity = "HighSecurity";
}
