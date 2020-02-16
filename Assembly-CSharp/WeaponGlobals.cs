using System;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public static class WeaponGlobals
{
	// Token: 0x06001BF3 RID: 7155 RVA: 0x000FB891 File Offset: 0x000F9C91
	public static int GetWeaponStatus(int weaponID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_WeaponStatus_",
			weaponID.ToString()
		}));
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x000FB8D0 File Offset: 0x000F9CD0
	public static void SetWeaponStatus(int weaponID, int value)
	{
		string text = weaponID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_WeaponStatus_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_WeaponStatus_",
			text
		}), value);
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000FB93C File Offset: 0x000F9D3C
	public static int[] KeysOfWeaponStatus()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_WeaponStatus_");
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000FB95C File Offset: 0x000F9D5C
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_WeaponStatus_", WeaponGlobals.KeysOfWeaponStatus());
	}

	// Token: 0x0400203A RID: 8250
	private const string Str_WeaponStatus = "WeaponStatus_";
}
