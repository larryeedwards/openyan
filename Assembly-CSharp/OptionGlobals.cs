using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public static class OptionGlobals
{
	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x000F739A File Offset: 0x000F579A
	// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x000F73BA File Offset: 0x000F57BA
	public static bool DisableBloom
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableBloom");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableBloom", value);
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x000F73DB File Offset: 0x000F57DB
	// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x000F73FB File Offset: 0x000F57FB
	public static int DisableFarAnimations
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DisableFarAnimations");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DisableFarAnimations", value);
		}
	}

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x06001ACA RID: 6858 RVA: 0x000F741C File Offset: 0x000F581C
	// (set) Token: 0x06001ACB RID: 6859 RVA: 0x000F743C File Offset: 0x000F583C
	public static bool DisableOutlines
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableOutlines");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableOutlines", value);
		}
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x06001ACC RID: 6860 RVA: 0x000F745D File Offset: 0x000F585D
	// (set) Token: 0x06001ACD RID: 6861 RVA: 0x000F747D File Offset: 0x000F587D
	public static bool DisablePostAliasing
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisablePostAliasing");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisablePostAliasing", value);
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x06001ACE RID: 6862 RVA: 0x000F749E File Offset: 0x000F589E
	// (set) Token: 0x06001ACF RID: 6863 RVA: 0x000F74BE File Offset: 0x000F58BE
	public static bool EnableShadows
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_EnableShadows");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_EnableShadows", value);
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x000F74DF File Offset: 0x000F58DF
	// (set) Token: 0x06001AD1 RID: 6865 RVA: 0x000F74FF File Offset: 0x000F58FF
	public static bool DisableObscurance
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DisableObscurance");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DisableObscurance", value);
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x000F7520 File Offset: 0x000F5920
	// (set) Token: 0x06001AD3 RID: 6867 RVA: 0x000F7540 File Offset: 0x000F5940
	public static int DrawDistance
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DrawDistance");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DrawDistance", value);
		}
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x000F7561 File Offset: 0x000F5961
	// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x000F7581 File Offset: 0x000F5981
	public static int DrawDistanceLimit
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit", value);
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x000F75A2 File Offset: 0x000F59A2
	// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x000F75C2 File Offset: 0x000F59C2
	public static bool Fog
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Fog");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Fog", value);
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x000F75E3 File Offset: 0x000F59E3
	// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x000F7603 File Offset: 0x000F5A03
	public static int FPSIndex
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FPSIndex");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FPSIndex", value);
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06001ADA RID: 6874 RVA: 0x000F7624 File Offset: 0x000F5A24
	// (set) Token: 0x06001ADB RID: 6875 RVA: 0x000F7644 File Offset: 0x000F5A44
	public static bool HighPopulation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_HighPopulation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_HighPopulation", value);
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06001ADC RID: 6876 RVA: 0x000F7665 File Offset: 0x000F5A65
	// (set) Token: 0x06001ADD RID: 6877 RVA: 0x000F7685 File Offset: 0x000F5A85
	public static int LowDetailStudents
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LowDetailStudents");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LowDetailStudents", value);
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06001ADE RID: 6878 RVA: 0x000F76A6 File Offset: 0x000F5AA6
	// (set) Token: 0x06001ADF RID: 6879 RVA: 0x000F76C6 File Offset: 0x000F5AC6
	public static int ParticleCount
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ParticleCount");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ParticleCount", value);
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x000F76E7 File Offset: 0x000F5AE7
	// (set) Token: 0x06001AE1 RID: 6881 RVA: 0x000F7707 File Offset: 0x000F5B07
	public static bool RimLight
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RimLight");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RimLight", value);
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06001AE2 RID: 6882 RVA: 0x000F7728 File Offset: 0x000F5B28
	// (set) Token: 0x06001AE3 RID: 6883 RVA: 0x000F7748 File Offset: 0x000F5B48
	public static bool DepthOfField
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DepthOfField");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DepthOfField", value);
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000F7769 File Offset: 0x000F5B69
	// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x000F7789 File Offset: 0x000F5B89
	public static int Sensitivity
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Sensitivity");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Sensitivity", value);
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000F77AA File Offset: 0x000F5BAA
	// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x000F77CA File Offset: 0x000F5BCA
	public static bool InvertAxis
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_InvertAxis");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_InvertAxis", value);
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000F77EB File Offset: 0x000F5BEB
	// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x000F780B File Offset: 0x000F5C0B
	public static bool TutorialsOff
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_TutorialsOff");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_TutorialsOff", value);
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001AEA RID: 6890 RVA: 0x000F782C File Offset: 0x000F5C2C
	// (set) Token: 0x06001AEB RID: 6891 RVA: 0x000F784C File Offset: 0x000F5C4C
	public static bool ToggleRun
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_ToggleRun");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_ToggleRun", value);
		}
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x000F7870 File Offset: 0x000F5C70
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableBloom");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableFarAnimations");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableOutlines");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisablePostAliasing");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EnableShadows");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DisableObscurance");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DrawDistance");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DrawDistanceLimit");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Fog");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FPSIndex");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_HighPopulation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LowDetailStudents");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ParticleCount");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RimLight");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DepthOfField");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Sensitivity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_InvertAxis");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_TutorialsOff");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ToggleRun");
	}

	// Token: 0x04001FBC RID: 8124
	private const string Str_DisableBloom = "DisableBloom";

	// Token: 0x04001FBD RID: 8125
	private const string Str_DisableFarAnimations = "DisableFarAnimations";

	// Token: 0x04001FBE RID: 8126
	private const string Str_DisableOutlines = "DisableOutlines";

	// Token: 0x04001FBF RID: 8127
	private const string Str_DisablePostAliasing = "DisablePostAliasing";

	// Token: 0x04001FC0 RID: 8128
	private const string Str_EnableShadows = "EnableShadows";

	// Token: 0x04001FC1 RID: 8129
	private const string Str_DisableObscurance = "DisableObscurance";

	// Token: 0x04001FC2 RID: 8130
	private const string Str_DrawDistance = "DrawDistance";

	// Token: 0x04001FC3 RID: 8131
	private const string Str_DrawDistanceLimit = "DrawDistanceLimit";

	// Token: 0x04001FC4 RID: 8132
	private const string Str_Fog = "Fog";

	// Token: 0x04001FC5 RID: 8133
	private const string Str_FPSIndex = "FPSIndex";

	// Token: 0x04001FC6 RID: 8134
	private const string Str_HighPopulation = "HighPopulation";

	// Token: 0x04001FC7 RID: 8135
	private const string Str_LowDetailStudents = "LowDetailStudents";

	// Token: 0x04001FC8 RID: 8136
	private const string Str_ParticleCount = "ParticleCount";

	// Token: 0x04001FC9 RID: 8137
	private const string Str_RimLight = "RimLight";

	// Token: 0x04001FCA RID: 8138
	private const string Str_DepthOfField = "DepthOfField";

	// Token: 0x04001FCB RID: 8139
	private const string Str_Sensitivity = "Sensitivity";

	// Token: 0x04001FCC RID: 8140
	private const string Str_InvertAxis = "InvertAxis";

	// Token: 0x04001FCD RID: 8141
	private const string Str_TutorialsOff = "TutorialsOff";

	// Token: 0x04001FCE RID: 8142
	private const string Str_ToggleRun = "ToggleRun";
}
