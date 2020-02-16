using System;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public static class CollectibleGlobals
{
	// Token: 0x06001A38 RID: 6712 RVA: 0x000F5656 File Offset: 0x000F3A56
	public static bool GetHeadmasterTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x000F5698 File Offset: 0x000F3A98
	public static void SetHeadmasterTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HeadmasterTapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeCollected_",
			text
		}), value);
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000F5704 File Offset: 0x000F3B04
	public static bool GetHeadmasterTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x000F5744 File Offset: 0x000F3B44
	public static void SetHeadmasterTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_HeadmasterTapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_HeadmasterTapeListened_",
			text
		}), value);
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x000F57B0 File Offset: 0x000F3BB0
	public static bool GetBasementTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x000F57F0 File Offset: 0x000F3BF0
	public static void SetBasementTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeCollected_",
			text
		}), value);
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x000F585C File Offset: 0x000F3C5C
	public static int[] KeysOfBasementTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_");
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x000F587C File Offset: 0x000F3C7C
	public static bool GetBasementTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x000F58BC File Offset: 0x000F3CBC
	public static void SetBasementTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_BasementTapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BasementTapeListened_",
			text
		}), value);
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x000F5928 File Offset: 0x000F3D28
	public static int[] KeysOfBasementTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_BasementTapeListened_");
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x000F5948 File Offset: 0x000F3D48
	public static bool GetMangaCollected(int mangaID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_MangaCollected_",
			mangaID.ToString()
		}));
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x000F5988 File Offset: 0x000F3D88
	public static void SetMangaCollected(int mangaID, bool value)
	{
		string text = mangaID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_MangaCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_MangaCollected_",
			text
		}), value);
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x000F59F4 File Offset: 0x000F3DF4
	public static bool GetGiftPurchased(int giftID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftPurchased_",
			giftID.ToString()
		}));
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x000F5A34 File Offset: 0x000F3E34
	public static void SetGiftPurchased(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_GiftPurchased_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_GiftPurchased_",
			text
		}), value);
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001A46 RID: 6726 RVA: 0x000F5AA0 File Offset: 0x000F3EA0
	// (set) Token: 0x06001A47 RID: 6727 RVA: 0x000F5AC0 File Offset: 0x000F3EC0
	public static int MatchmakingGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MatchmakingGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MatchmakingGifts", value);
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06001A48 RID: 6728 RVA: 0x000F5AE1 File Offset: 0x000F3EE1
	// (set) Token: 0x06001A49 RID: 6729 RVA: 0x000F5B01 File Offset: 0x000F3F01
	public static int SenpaiGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiGifts", value);
		}
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x000F5B22 File Offset: 0x000F3F22
	public static int[] KeysOfMangaCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_MangaCollected_");
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x000F5B42 File Offset: 0x000F3F42
	public static int[] KeysOfGiftPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_GiftPurchased_");
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000F5B62 File Offset: 0x000F3F62
	public static bool GetTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeCollected_",
			tapeID.ToString()
		}));
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x000F5BA4 File Offset: 0x000F3FA4
	public static void SetTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TapeCollected_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeCollected_",
			text
		}), value);
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x000F5C10 File Offset: 0x000F4010
	public static int[] KeysOfTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TapeCollected_");
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000F5C30 File Offset: 0x000F4030
	public static bool GetTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeListened_",
			tapeID.ToString()
		}));
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000F5C70 File Offset: 0x000F4070
	public static void SetTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TapeListened_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TapeListened_",
			text
		}), value);
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x000F5CDC File Offset: 0x000F40DC
	public static int[] KeysOfTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TapeListened_");
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x000F5CFC File Offset: 0x000F40FC
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_BasementTapeCollected_", CollectibleGlobals.KeysOfBasementTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_BasementTapeListened_", CollectibleGlobals.KeysOfBasementTapeListened());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_MangaCollected_", CollectibleGlobals.KeysOfMangaCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_GiftPurchased_", CollectibleGlobals.KeysOfGiftPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TapeCollected_", CollectibleGlobals.KeysOfTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TapeListened_", CollectibleGlobals.KeysOfTapeListened());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MatchmakingGifts");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiGifts");
	}

	// Token: 0x04001F80 RID: 8064
	private const string Str_HeadmasterTapeCollected = "HeadmasterTapeCollected_";

	// Token: 0x04001F81 RID: 8065
	private const string Str_HeadmasterTapeListened = "HeadmasterTapeListened_";

	// Token: 0x04001F82 RID: 8066
	private const string Str_BasementTapeCollected = "BasementTapeCollected_";

	// Token: 0x04001F83 RID: 8067
	private const string Str_BasementTapeListened = "BasementTapeListened_";

	// Token: 0x04001F84 RID: 8068
	private const string Str_MangaCollected = "MangaCollected_";

	// Token: 0x04001F85 RID: 8069
	private const string Str_GiftPurchased = "GiftPurchased_";

	// Token: 0x04001F86 RID: 8070
	private const string Str_MatchmakingGifts = "MatchmakingGifts";

	// Token: 0x04001F87 RID: 8071
	private const string Str_SenpaiGifts = "SenpaiGifts";

	// Token: 0x04001F88 RID: 8072
	private const string Str_TapeCollected = "TapeCollected_";

	// Token: 0x04001F89 RID: 8073
	private const string Str_TapeListened = "TapeListened_";
}
