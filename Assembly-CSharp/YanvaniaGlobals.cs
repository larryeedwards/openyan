using System;

// Token: 0x02000400 RID: 1024
public static class YanvaniaGlobals
{
	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000FB7D1 File Offset: 0x000F9BD1
	// (set) Token: 0x06001BEF RID: 7151 RVA: 0x000FB7F1 File Offset: 0x000F9BF1
	public static bool DraculaDefeated
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_DraculaDefeated");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_DraculaDefeated", value);
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000FB812 File Offset: 0x000F9C12
	// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x000FB832 File Offset: 0x000F9C32
	public static bool MidoriEasterEgg
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg", value);
		}
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x000FB853 File Offset: 0x000F9C53
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_DraculaDefeated");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MidoriEasterEgg");
	}

	// Token: 0x04002038 RID: 8248
	private const string Str_DraculaDefeated = "DraculaDefeated";

	// Token: 0x04002039 RID: 8249
	private const string Str_MidoriEasterEgg = "MidoriEasterEgg";
}
