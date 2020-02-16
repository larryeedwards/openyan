using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020003EA RID: 1002
public static class KeysHelper
{
	// Token: 0x060019F7 RID: 6647 RVA: 0x000F487C File Offset: 0x000F2C7C
	public static int[] GetIntegerKeys(string key)
	{
		string keyList = KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key));
		string[] array = KeysHelper.SplitList(keyList);
		string[] array2 = array;
		if (KeysHelper.<>f__mg$cache0 == null)
		{
			KeysHelper.<>f__mg$cache0 = new Converter<string, int>(int.Parse);
		}
		return Array.ConvertAll<string, int>(array2, KeysHelper.<>f__mg$cache0);
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x000F48C4 File Offset: 0x000F2CC4
	public static string[] GetStringKeys(string key)
	{
		string keyList = KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key));
		return KeysHelper.SplitList(keyList);
	}

	// Token: 0x060019F9 RID: 6649 RVA: 0x000F48E8 File Offset: 0x000F2CE8
	public static T[] GetEnumKeys<T>(string key) where T : struct, IConvertible
	{
		string keyList = KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key));
		string[] array = KeysHelper.SplitList(keyList);
		return Array.ConvertAll<string, T>(array, (string str) => (T)((object)Enum.Parse(typeof(T), str)));
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x000F491C File Offset: 0x000F2D1C
	public static KeyValuePair<T, U>[] GetKeys<T, U>(string key) where T : struct where U : struct
	{
		string keyList = KeysHelper.GetKeyList(KeysHelper.GetKeyListKey(key));
		string[] array = KeysHelper.SplitList(keyList);
		KeyValuePair<T, U>[] array2 = new KeyValuePair<T, U>[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string[] array3 = array[i].Split(new char[]
			{
				'^'
			});
			array2[i] = new KeyValuePair<T, U>((T)((object)int.Parse(array3[0])), (U)((object)int.Parse(array3[1])));
		}
		return array2;
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x000F49A8 File Offset: 0x000F2DA8
	public static void AddIfMissing(string key, string id)
	{
		string keyListKey = KeysHelper.GetKeyListKey(key);
		string keyList = KeysHelper.GetKeyList(keyListKey);
		string[] keyListStrings = KeysHelper.SplitList(keyList);
		if (!KeysHelper.HasKey(keyListStrings, id))
		{
			KeysHelper.AppendKey(keyListKey, keyList, id);
		}
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x000F49E0 File Offset: 0x000F2DE0
	public static void Delete(string key)
	{
		string keyListKey = KeysHelper.GetKeyListKey(key);
		Globals.Delete(keyListKey);
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x000F49FA File Offset: 0x000F2DFA
	private static string GetKeyListKey(string key)
	{
		return key + "Keys";
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x000F4A07 File Offset: 0x000F2E07
	private static string GetKeyList(string keyListKey)
	{
		return PlayerPrefs.GetString(keyListKey);
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x000F4A0F File Offset: 0x000F2E0F
	private static string[] SplitList(string keyList)
	{
		return (keyList.Length <= 0) ? new string[0] : keyList.Split(new char[]
		{
			'|'
		});
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x000F4A39 File Offset: 0x000F2E39
	private static int FindKey(string[] keyListStrings, string key)
	{
		return Array.IndexOf<string>(keyListStrings, key);
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x000F4A42 File Offset: 0x000F2E42
	private static bool HasKey(string[] keyListStrings, string key)
	{
		return KeysHelper.FindKey(keyListStrings, key) > -1;
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x000F4A50 File Offset: 0x000F2E50
	private static void AppendKey(string keyListKey, string keyList, string key)
	{
		string value = (keyList.Length != 0) ? (keyList + '|' + key) : (keyList + key);
		PlayerPrefs.SetString(keyListKey, value);
	}

	// Token: 0x04001F68 RID: 8040
	private const string KeyListPrefix = "Keys";

	// Token: 0x04001F69 RID: 8041
	private const char KeyListSeparator = '|';

	// Token: 0x04001F6A RID: 8042
	public const char PairSeparator = '^';

	// Token: 0x04001F6B RID: 8043
	[CompilerGenerated]
	private static Converter<string, int> <>f__mg$cache0;
}
