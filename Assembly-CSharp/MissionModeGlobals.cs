using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public static class MissionModeGlobals
{
	// Token: 0x06001AB0 RID: 6832 RVA: 0x000F71E1 File Offset: 0x000F55E1
	public static int GetMissionCondition(int id)
	{
		return PlayerPrefs.GetInt("MissionCondition_" + id.ToString());
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x000F7200 File Offset: 0x000F5600
	public static void SetMissionCondition(int id, int value)
	{
		string text = id.ToString();
		KeysHelper.AddIfMissing("MissionCondition_", text);
		PlayerPrefs.SetInt("MissionCondition_" + text, value);
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x000F7237 File Offset: 0x000F5637
	public static int[] KeysOfMissionCondition()
	{
		return KeysHelper.GetIntegerKeys("MissionCondition_");
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x000F7243 File Offset: 0x000F5643
	// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x000F724F File Offset: 0x000F564F
	public static int MissionDifficulty
	{
		get
		{
			return PlayerPrefs.GetInt("MissionDifficulty");
		}
		set
		{
			PlayerPrefs.SetInt("MissionDifficulty", value);
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x000F725C File Offset: 0x000F565C
	// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x000F7268 File Offset: 0x000F5668
	public static bool MissionMode
	{
		get
		{
			return GlobalsHelper.GetBool("MissionMode");
		}
		set
		{
			GlobalsHelper.SetBool("MissionMode", value);
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000F7275 File Offset: 0x000F5675
	// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x000F7281 File Offset: 0x000F5681
	public static bool MultiMission
	{
		get
		{
			return GlobalsHelper.GetBool("MultiMission");
		}
		set
		{
			GlobalsHelper.SetBool("MultiMission", value);
		}
	}

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000F728E File Offset: 0x000F568E
	// (set) Token: 0x06001ABA RID: 6842 RVA: 0x000F729A File Offset: 0x000F569A
	public static int MissionRequiredClothing
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredClothing");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredClothing", value);
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x06001ABB RID: 6843 RVA: 0x000F72A7 File Offset: 0x000F56A7
	// (set) Token: 0x06001ABC RID: 6844 RVA: 0x000F72B3 File Offset: 0x000F56B3
	public static int MissionRequiredDisposal
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredDisposal");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredDisposal", value);
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x06001ABD RID: 6845 RVA: 0x000F72C0 File Offset: 0x000F56C0
	// (set) Token: 0x06001ABE RID: 6846 RVA: 0x000F72CC File Offset: 0x000F56CC
	public static int MissionRequiredWeapon
	{
		get
		{
			return PlayerPrefs.GetInt("MissionRequiredWeapon");
		}
		set
		{
			PlayerPrefs.SetInt("MissionRequiredWeapon", value);
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06001ABF RID: 6847 RVA: 0x000F72D9 File Offset: 0x000F56D9
	// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x000F72E5 File Offset: 0x000F56E5
	public static int MissionTarget
	{
		get
		{
			return PlayerPrefs.GetInt("MissionTarget");
		}
		set
		{
			PlayerPrefs.SetInt("MissionTarget", value);
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000F72F2 File Offset: 0x000F56F2
	// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x000F72FE File Offset: 0x000F56FE
	public static string MissionTargetName
	{
		get
		{
			return PlayerPrefs.GetString("MissionTargetName");
		}
		set
		{
			PlayerPrefs.SetString("MissionTargetName", value);
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000F730B File Offset: 0x000F570B
	// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x000F7317 File Offset: 0x000F5717
	public static int NemesisDifficulty
	{
		get
		{
			return PlayerPrefs.GetInt("NemesisDifficulty");
		}
		set
		{
			PlayerPrefs.SetInt("NemesisDifficulty", value);
		}
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x000F7324 File Offset: 0x000F5724
	public static void DeleteAll()
	{
		Globals.DeleteCollection("MissionCondition_", MissionModeGlobals.KeysOfMissionCondition());
		Globals.Delete("MissionDifficulty");
		Globals.Delete("MissionMode");
		Globals.Delete("MissionRequiredClothing");
		Globals.Delete("MissionRequiredDisposal");
		Globals.Delete("MissionRequiredWeapon");
		Globals.Delete("MissionTarget");
		Globals.Delete("MissionTargetName");
		Globals.Delete("NemesisDifficulty");
		Globals.Delete("MultiMission");
	}

	// Token: 0x04001FB2 RID: 8114
	private const string Str_MissionCondition = "MissionCondition_";

	// Token: 0x04001FB3 RID: 8115
	private const string Str_MissionDifficulty = "MissionDifficulty";

	// Token: 0x04001FB4 RID: 8116
	private const string Str_MissionMode = "MissionMode";

	// Token: 0x04001FB5 RID: 8117
	private const string Str_MissionRequiredClothing = "MissionRequiredClothing";

	// Token: 0x04001FB6 RID: 8118
	private const string Str_MissionRequiredDisposal = "MissionRequiredDisposal";

	// Token: 0x04001FB7 RID: 8119
	private const string Str_MissionRequiredWeapon = "MissionRequiredWeapon";

	// Token: 0x04001FB8 RID: 8120
	private const string Str_MissionTarget = "MissionTarget";

	// Token: 0x04001FB9 RID: 8121
	private const string Str_MissionTargetName = "MissionTargetName";

	// Token: 0x04001FBA RID: 8122
	private const string Str_NemesisDifficulty = "NemesisDifficulty";

	// Token: 0x04001FBB RID: 8123
	private const string Str_MultiMission = "MultiMission";
}
