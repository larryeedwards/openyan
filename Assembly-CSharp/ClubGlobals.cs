using System;

// Token: 0x020003EE RID: 1006
public static class ClubGlobals
{
	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001A2C RID: 6700 RVA: 0x000F51CF File Offset: 0x000F35CF
	// (set) Token: 0x06001A2D RID: 6701 RVA: 0x000F51EF File Offset: 0x000F35EF
	public static ClubType Club
	{
		get
		{
			return GlobalsHelper.GetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_Club");
		}
		set
		{
			GlobalsHelper.SetEnum<ClubType>("Profile_" + GameGlobals.Profile + "_Club", value);
		}
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x000F5210 File Offset: 0x000F3610
	public static bool GetClubClosed(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_ClubClosed_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x000F525C File Offset: 0x000F365C
	public static void SetClubClosed(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ClubClosed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ClubClosed_",
			text
		}), value);
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x000F52CA File Offset: 0x000F36CA
	public static ClubType[] KeysOfClubClosed()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_ClubClosed_");
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x000F52EC File Offset: 0x000F36EC
	public static bool GetClubKicked(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_ClubKicked_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x000F5338 File Offset: 0x000F3738
	public static void SetClubKicked(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ClubKicked_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ClubKicked_",
			text
		}), value);
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x000F53A6 File Offset: 0x000F37A6
	public static ClubType[] KeysOfClubKicked()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_ClubKicked_");
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x000F53C8 File Offset: 0x000F37C8
	public static bool GetQuitClub(ClubType clubID)
	{
		object[] array = new object[4];
		array[0] = "Profile_";
		array[1] = GameGlobals.Profile;
		array[2] = "_QuitClub_";
		int num = 3;
		int num2 = (int)clubID;
		array[num] = num2.ToString();
		return GlobalsHelper.GetBool(string.Concat(array));
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x000F5414 File Offset: 0x000F3814
	public static void SetQuitClub(ClubType clubID, bool value)
	{
		int num = (int)clubID;
		string text = num.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_QuitClub_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_QuitClub_",
			text
		}), value);
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x000F5482 File Offset: 0x000F3882
	public static ClubType[] KeysOfQuitClub()
	{
		return KeysHelper.GetEnumKeys<ClubType>("Profile_" + GameGlobals.Profile + "_QuitClub_");
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x000F54A4 File Offset: 0x000F38A4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Club");
		foreach (ClubType clubType in ClubGlobals.KeysOfClubClosed())
		{
			object[] array2 = new object[4];
			array2[0] = "Profile_";
			array2[1] = GameGlobals.Profile;
			array2[2] = "_ClubClosed_";
			int num = 3;
			int num2 = (int)clubType;
			array2[num] = num2.ToString();
			Globals.Delete(string.Concat(array2));
		}
		foreach (ClubType clubType2 in ClubGlobals.KeysOfClubKicked())
		{
			object[] array4 = new object[4];
			array4[0] = "Profile_";
			array4[1] = GameGlobals.Profile;
			array4[2] = "_ClubKicked_";
			int num3 = 3;
			int num4 = (int)clubType2;
			array4[num3] = num4.ToString();
			Globals.Delete(string.Concat(array4));
		}
		foreach (ClubType clubType3 in ClubGlobals.KeysOfQuitClub())
		{
			object[] array6 = new object[4];
			array6[0] = "Profile_";
			array6[1] = GameGlobals.Profile;
			array6[2] = "_QuitClub_";
			int num5 = 3;
			int num6 = (int)clubType3;
			array6[num5] = num6.ToString();
			Globals.Delete(string.Concat(array6));
		}
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_ClubClosed_");
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_ClubKicked_");
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_QuitClub_");
	}

	// Token: 0x04001F7C RID: 8060
	private const string Str_Club = "Club";

	// Token: 0x04001F7D RID: 8061
	private const string Str_ClubClosed = "ClubClosed_";

	// Token: 0x04001F7E RID: 8062
	private const string Str_ClubKicked = "ClubKicked_";

	// Token: 0x04001F7F RID: 8063
	private const string Str_QuitClub = "QuitClub_";
}
