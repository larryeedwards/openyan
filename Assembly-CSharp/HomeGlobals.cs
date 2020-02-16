using System;

// Token: 0x020003F5 RID: 1013
public static class HomeGlobals
{
	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000F7057 File Offset: 0x000F5457
	// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x000F7077 File Offset: 0x000F5477
	public static bool LateForSchool
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LateForSchool");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LateForSchool", value);
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000F7098 File Offset: 0x000F5498
	// (set) Token: 0x06001AAA RID: 6826 RVA: 0x000F70B8 File Offset: 0x000F54B8
	public static bool Night
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Night");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Night", value);
		}
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x06001AAB RID: 6827 RVA: 0x000F70D9 File Offset: 0x000F54D9
	// (set) Token: 0x06001AAC RID: 6828 RVA: 0x000F70F9 File Offset: 0x000F54F9
	public static bool StartInBasement
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_StartInBasement");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_StartInBasement", value);
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x06001AAD RID: 6829 RVA: 0x000F711A File Offset: 0x000F551A
	// (set) Token: 0x06001AAE RID: 6830 RVA: 0x000F713A File Offset: 0x000F553A
	public static bool MiyukiDefeated
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MiyukiDefeated");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MiyukiDefeated", value);
		}
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x000F715C File Offset: 0x000F555C
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LateForSchool");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Night");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StartInBasement");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MiyukiDefeated");
	}

	// Token: 0x04001FAE RID: 8110
	private const string Str_LateForSchool = "LateForSchool";

	// Token: 0x04001FAF RID: 8111
	private const string Str_Night = "Night";

	// Token: 0x04001FB0 RID: 8112
	private const string Str_StartInBasement = "StartInBasement";

	// Token: 0x04001FB1 RID: 8113
	private const string Str_MiyukiDefeated = "MiyukiDefeated";
}
