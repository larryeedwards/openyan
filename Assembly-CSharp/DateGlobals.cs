using System;
using UnityEngine;

// Token: 0x020003F1 RID: 1009
public static class DateGlobals
{
	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000F611B File Offset: 0x000F451B
	// (set) Token: 0x06001A5B RID: 6747 RVA: 0x000F613B File Offset: 0x000F453B
	public static int Week
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Week");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Week", value);
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x06001A5C RID: 6748 RVA: 0x000F615C File Offset: 0x000F455C
	// (set) Token: 0x06001A5D RID: 6749 RVA: 0x000F617C File Offset: 0x000F457C
	public static DayOfWeek Weekday
	{
		get
		{
			return GlobalsHelper.GetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_Weekday");
		}
		set
		{
			GlobalsHelper.SetEnum<DayOfWeek>("Profile_" + GameGlobals.Profile + "_Weekday", value);
		}
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x06001A5E RID: 6750 RVA: 0x000F619D File Offset: 0x000F459D
	// (set) Token: 0x06001A5F RID: 6751 RVA: 0x000F61BD File Offset: 0x000F45BD
	public static int PassDays
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PassDays");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PassDays", value);
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06001A60 RID: 6752 RVA: 0x000F61DE File Offset: 0x000F45DE
	// (set) Token: 0x06001A61 RID: 6753 RVA: 0x000F61FE File Offset: 0x000F45FE
	public static bool DayPassed
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DayPassed");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DayPassed", value);
		}
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x000F6220 File Offset: 0x000F4620
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Week");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Weekday");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PassDays");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DayPassed");
	}

	// Token: 0x04001F8C RID: 8076
	private const string Str_Week = "Week";

	// Token: 0x04001F8D RID: 8077
	private const string Str_Weekday = "Weekday";

	// Token: 0x04001F8E RID: 8078
	private const string Str_PassDays = "PassDays";

	// Token: 0x04001F8F RID: 8079
	private const string Str_DayPassed = "DayPassed";
}
