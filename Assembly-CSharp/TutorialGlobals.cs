using System;

// Token: 0x02000402 RID: 1026
public static class TutorialGlobals
{
	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000FB981 File Offset: 0x000F9D81
	// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x000FB9A1 File Offset: 0x000F9DA1
	public static bool IgnoreClothing
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClothing");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClothing", value);
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x000FB9C2 File Offset: 0x000F9DC2
	// (set) Token: 0x06001BFA RID: 7162 RVA: 0x000FB9E2 File Offset: 0x000F9DE2
	public static bool IgnoreCouncil
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreCouncil");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreCouncil", value);
		}
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x06001BFB RID: 7163 RVA: 0x000FBA03 File Offset: 0x000F9E03
	// (set) Token: 0x06001BFC RID: 7164 RVA: 0x000FBA23 File Offset: 0x000F9E23
	public static bool IgnoreTeacher
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreTeacher");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreTeacher", value);
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x06001BFD RID: 7165 RVA: 0x000FBA44 File Offset: 0x000F9E44
	// (set) Token: 0x06001BFE RID: 7166 RVA: 0x000FBA64 File Offset: 0x000F9E64
	public static bool IgnoreLocker
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreLocker");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreLocker", value);
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x06001BFF RID: 7167 RVA: 0x000FBA85 File Offset: 0x000F9E85
	// (set) Token: 0x06001C00 RID: 7168 RVA: 0x000FBAA5 File Offset: 0x000F9EA5
	public static bool IgnorePolice
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePolice");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePolice", value);
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000FBAC6 File Offset: 0x000F9EC6
	// (set) Token: 0x06001C02 RID: 7170 RVA: 0x000FBAE6 File Offset: 0x000F9EE6
	public static bool IgnoreSanity
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreSanity");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreSanity", value);
		}
	}

	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000FBB07 File Offset: 0x000F9F07
	// (set) Token: 0x06001C04 RID: 7172 RVA: 0x000FBB27 File Offset: 0x000F9F27
	public static bool IgnoreSenpai
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreSenpai");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreSenpai", value);
		}
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000FBB48 File Offset: 0x000F9F48
	// (set) Token: 0x06001C06 RID: 7174 RVA: 0x000FBB68 File Offset: 0x000F9F68
	public static bool IgnoreVision
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreVision");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreVision", value);
		}
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000FBB89 File Offset: 0x000F9F89
	// (set) Token: 0x06001C08 RID: 7176 RVA: 0x000FBBA9 File Offset: 0x000F9FA9
	public static bool IgnoreWeapon
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreWeapon");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreWeapon", value);
		}
	}

	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x06001C09 RID: 7177 RVA: 0x000FBBCA File Offset: 0x000F9FCA
	// (set) Token: 0x06001C0A RID: 7178 RVA: 0x000FBBEA File Offset: 0x000F9FEA
	public static bool IgnoreBlood
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreBlood");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreBlood", value);
		}
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000FBC0B File Offset: 0x000FA00B
	// (set) Token: 0x06001C0C RID: 7180 RVA: 0x000FBC2B File Offset: 0x000FA02B
	public static bool IgnoreClass
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass", value);
		}
	}

	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x06001C0D RID: 7181 RVA: 0x000FBC4C File Offset: 0x000FA04C
	// (set) Token: 0x06001C0E RID: 7182 RVA: 0x000FBC6C File Offset: 0x000FA06C
	public static bool IgnorePhoto
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePhoto");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePhoto", value);
		}
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x06001C0F RID: 7183 RVA: 0x000FBC8D File Offset: 0x000FA08D
	// (set) Token: 0x06001C10 RID: 7184 RVA: 0x000FBCAD File Offset: 0x000FA0AD
	public static bool IgnoreClub
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClub");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClub", value);
		}
	}

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000FBCCE File Offset: 0x000FA0CE
	// (set) Token: 0x06001C12 RID: 7186 RVA: 0x000FBCEE File Offset: 0x000FA0EE
	public static bool IgnoreInfo
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreInfo");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreInfo", value);
		}
	}

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x06001C13 RID: 7187 RVA: 0x000FBD0F File Offset: 0x000FA10F
	// (set) Token: 0x06001C14 RID: 7188 RVA: 0x000FBD2F File Offset: 0x000FA12F
	public static bool IgnorePool
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnorePool");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnorePool", value);
		}
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x06001C15 RID: 7189 RVA: 0x000FBD50 File Offset: 0x000FA150
	// (set) Token: 0x06001C16 RID: 7190 RVA: 0x000FBD70 File Offset: 0x000FA170
	public static bool IgnoreRep
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_IgnoreClass", value);
		}
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000FBD94 File Offset: 0x000FA194
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClothing");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreCouncil");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreTeacher");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreLocker");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePolice");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreSanity");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreSenpai");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreVision");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreWeapon");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreBlood");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClass");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePhoto");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClub");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreInfo");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnorePool");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_IgnoreClass");
	}

	// Token: 0x0400203B RID: 8251
	private const string Str_IgnoreClothing = "IgnoreClothing";

	// Token: 0x0400203C RID: 8252
	private const string Str_IgnoreCouncil = "IgnoreCouncil";

	// Token: 0x0400203D RID: 8253
	private const string Str_IgnoreTeacher = "IgnoreTeacher";

	// Token: 0x0400203E RID: 8254
	private const string Str_IgnoreLocker = "IgnoreLocker";

	// Token: 0x0400203F RID: 8255
	private const string Str_IgnorePolice = "IgnorePolice";

	// Token: 0x04002040 RID: 8256
	private const string Str_IgnoreSanity = "IgnoreSanity";

	// Token: 0x04002041 RID: 8257
	private const string Str_IgnoreSenpai = "IgnoreSenpai";

	// Token: 0x04002042 RID: 8258
	private const string Str_IgnoreVision = "IgnoreVision";

	// Token: 0x04002043 RID: 8259
	private const string Str_IgnoreWeapon = "IgnoreWeapon";

	// Token: 0x04002044 RID: 8260
	private const string Str_IgnoreBlood = "IgnoreBlood";

	// Token: 0x04002045 RID: 8261
	private const string Str_IgnoreClass = "IgnoreClass";

	// Token: 0x04002046 RID: 8262
	private const string Str_IgnorePhoto = "IgnorePhoto";

	// Token: 0x04002047 RID: 8263
	private const string Str_IgnoreClub = "IgnoreClub";

	// Token: 0x04002048 RID: 8264
	private const string Str_IgnoreInfo = "IgnoreInfo";

	// Token: 0x04002049 RID: 8265
	private const string Str_IgnorePool = "IgnorePool";

	// Token: 0x0400204A RID: 8266
	private const string Str_IgnoreRep = "IgnoreClass";
}
