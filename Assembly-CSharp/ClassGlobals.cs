using System;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public static class ClassGlobals
{
	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x06001A0D RID: 6669 RVA: 0x000F4C31 File Offset: 0x000F3031
	// (set) Token: 0x06001A0E RID: 6670 RVA: 0x000F4C51 File Offset: 0x000F3051
	public static int Biology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Biology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Biology", value);
		}
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000F4C72 File Offset: 0x000F3072
	// (set) Token: 0x06001A10 RID: 6672 RVA: 0x000F4C92 File Offset: 0x000F3092
	public static int BiologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BiologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BiologyBonus", value);
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06001A11 RID: 6673 RVA: 0x000F4CB3 File Offset: 0x000F30B3
	// (set) Token: 0x06001A12 RID: 6674 RVA: 0x000F4CD3 File Offset: 0x000F30D3
	public static int BiologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_BiologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_BiologyGrade", value);
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000F4CF4 File Offset: 0x000F30F4
	// (set) Token: 0x06001A14 RID: 6676 RVA: 0x000F4D14 File Offset: 0x000F3114
	public static int Chemistry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Chemistry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Chemistry", value);
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000F4D35 File Offset: 0x000F3135
	// (set) Token: 0x06001A16 RID: 6678 RVA: 0x000F4D55 File Offset: 0x000F3155
	public static int ChemistryBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ChemistryBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ChemistryBonus", value);
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000F4D76 File Offset: 0x000F3176
	// (set) Token: 0x06001A18 RID: 6680 RVA: 0x000F4D96 File Offset: 0x000F3196
	public static int ChemistryGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ChemistryGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ChemistryGrade", value);
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000F4DB7 File Offset: 0x000F31B7
	// (set) Token: 0x06001A1A RID: 6682 RVA: 0x000F4DD7 File Offset: 0x000F31D7
	public static int Language
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Language");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Language", value);
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000F4DF8 File Offset: 0x000F31F8
	// (set) Token: 0x06001A1C RID: 6684 RVA: 0x000F4E18 File Offset: 0x000F3218
	public static int LanguageBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LanguageBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LanguageBonus", value);
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000F4E39 File Offset: 0x000F3239
	// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000F4E59 File Offset: 0x000F3259
	public static int LanguageGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_LanguageGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_LanguageGrade", value);
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000F4E7A File Offset: 0x000F327A
	// (set) Token: 0x06001A20 RID: 6688 RVA: 0x000F4E9A File Offset: 0x000F329A
	public static int Physical
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Physical");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Physical", value);
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000F4EBB File Offset: 0x000F32BB
	// (set) Token: 0x06001A22 RID: 6690 RVA: 0x000F4EDB File Offset: 0x000F32DB
	public static int PhysicalBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PhysicalBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PhysicalBonus", value);
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x06001A23 RID: 6691 RVA: 0x000F4EFC File Offset: 0x000F32FC
	// (set) Token: 0x06001A24 RID: 6692 RVA: 0x000F4F1C File Offset: 0x000F331C
	public static int PhysicalGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PhysicalGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PhysicalGrade", value);
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000F4F3D File Offset: 0x000F333D
	// (set) Token: 0x06001A26 RID: 6694 RVA: 0x000F4F5D File Offset: 0x000F335D
	public static int Psychology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Psychology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Psychology", value);
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06001A27 RID: 6695 RVA: 0x000F4F7E File Offset: 0x000F337E
	// (set) Token: 0x06001A28 RID: 6696 RVA: 0x000F4F9E File Offset: 0x000F339E
	public static int PsychologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PsychologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PsychologyBonus", value);
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x06001A29 RID: 6697 RVA: 0x000F4FBF File Offset: 0x000F33BF
	// (set) Token: 0x06001A2A RID: 6698 RVA: 0x000F4FDF File Offset: 0x000F33DF
	public static int PsychologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PsychologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PsychologyGrade", value);
		}
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x000F5000 File Offset: 0x000F3400
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Biology");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BiologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BiologyGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Chemistry");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ChemistryBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ChemistryGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Language");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LanguageBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LanguageGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Physical");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PhysicalBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PhysicalGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Psychology");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PsychologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PsychologyGrade");
	}

	// Token: 0x04001F6D RID: 8045
	private const string Str_Biology = "Biology";

	// Token: 0x04001F6E RID: 8046
	private const string Str_BiologyBonus = "BiologyBonus";

	// Token: 0x04001F6F RID: 8047
	private const string Str_BiologyGrade = "BiologyGrade";

	// Token: 0x04001F70 RID: 8048
	private const string Str_Chemistry = "Chemistry";

	// Token: 0x04001F71 RID: 8049
	private const string Str_ChemistryBonus = "ChemistryBonus";

	// Token: 0x04001F72 RID: 8050
	private const string Str_ChemistryGrade = "ChemistryGrade";

	// Token: 0x04001F73 RID: 8051
	private const string Str_Language = "Language";

	// Token: 0x04001F74 RID: 8052
	private const string Str_LanguageBonus = "LanguageBonus";

	// Token: 0x04001F75 RID: 8053
	private const string Str_LanguageGrade = "LanguageGrade";

	// Token: 0x04001F76 RID: 8054
	private const string Str_Physical = "Physical";

	// Token: 0x04001F77 RID: 8055
	private const string Str_PhysicalBonus = "PhysicalBonus";

	// Token: 0x04001F78 RID: 8056
	private const string Str_PhysicalGrade = "PhysicalGrade";

	// Token: 0x04001F79 RID: 8057
	private const string Str_Psychology = "Psychology";

	// Token: 0x04001F7A RID: 8058
	private const string Str_PsychologyBonus = "PsychologyBonus";

	// Token: 0x04001F7B RID: 8059
	private const string Str_PsychologyGrade = "PsychologyGrade";
}
