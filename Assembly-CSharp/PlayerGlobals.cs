using System;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public static class PlayerGlobals
{
	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x06001AED RID: 6893 RVA: 0x000F7AB7 File Offset: 0x000F5EB7
	// (set) Token: 0x06001AEE RID: 6894 RVA: 0x000F7AD7 File Offset: 0x000F5ED7
	public static float Money
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Money");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Money", value);
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000F7AF8 File Offset: 0x000F5EF8
	// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x000F7B18 File Offset: 0x000F5F18
	public static int Alerts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Alerts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Alerts", value);
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000F7B39 File Offset: 0x000F5F39
	// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x000F7B59 File Offset: 0x000F5F59
	public static int Enlightenment
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Enlightenment");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Enlightenment", value);
		}
	}

	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000F7B7A File Offset: 0x000F5F7A
	// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x000F7B9A File Offset: 0x000F5F9A
	public static int EnlightenmentBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus", value);
		}
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000F7BBB File Offset: 0x000F5FBB
	// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x000F7BDB File Offset: 0x000F5FDB
	public static int Friends
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Friends");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Friends", value);
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x000F7BFC File Offset: 0x000F5FFC
	// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x000F7C1C File Offset: 0x000F601C
	public static bool Headset
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Headset");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Headset", value);
		}
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x000F7C3D File Offset: 0x000F603D
	// (set) Token: 0x06001AFA RID: 6906 RVA: 0x000F7C5D File Offset: 0x000F605D
	public static bool FakeID
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_FakeID");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_FakeID", value);
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000F7C7E File Offset: 0x000F607E
	// (set) Token: 0x06001AFC RID: 6908 RVA: 0x000F7C9E File Offset: 0x000F609E
	public static bool RaibaruLoner
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_RaibaruLoner");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_RaibaruLoner", value);
		}
	}

	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x06001AFD RID: 6909 RVA: 0x000F7CBF File Offset: 0x000F60BF
	// (set) Token: 0x06001AFE RID: 6910 RVA: 0x000F7CDF File Offset: 0x000F60DF
	public static int Kills
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Kills");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Kills", value);
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000F7D00 File Offset: 0x000F6100
	// (set) Token: 0x06001B00 RID: 6912 RVA: 0x000F7D20 File Offset: 0x000F6120
	public static int Numbness
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Numbness");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Numbness", value);
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000F7D41 File Offset: 0x000F6141
	// (set) Token: 0x06001B02 RID: 6914 RVA: 0x000F7D61 File Offset: 0x000F6161
	public static int NumbnessBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_NumbnessBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_NumbnessBonus", value);
		}
	}

	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000F7D82 File Offset: 0x000F6182
	// (set) Token: 0x06001B04 RID: 6916 RVA: 0x000F7DA2 File Offset: 0x000F61A2
	public static int PantiesEquipped
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PantiesEquipped");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PantiesEquipped", value);
		}
	}

	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000F7DC3 File Offset: 0x000F61C3
	// (set) Token: 0x06001B06 RID: 6918 RVA: 0x000F7DE3 File Offset: 0x000F61E3
	public static int PantyShots
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_PantyShots");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_PantyShots", value);
		}
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x000F7E04 File Offset: 0x000F6204
	public static bool GetPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Photo_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x000F7E44 File Offset: 0x000F6244
	public static void SetPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_Photo_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Photo_",
			text
		}), value);
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x000F7EB0 File Offset: 0x000F62B0
	public static int[] KeysOfPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_Photo_");
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x000F7ED0 File Offset: 0x000F62D0
	public static bool GetPhotoOnCorkboard(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoOnCorkboard_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x000F7F10 File Offset: 0x000F6310
	public static void SetPhotoOnCorkboard(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoOnCorkboard_",
			text
		}), value);
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x000F7F7C File Offset: 0x000F637C
	public static int[] KeysOfPhotoOnCorkboard()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_");
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x000F7F9C File Offset: 0x000F639C
	public static Vector2 GetPhotoPosition(int photoID)
	{
		return GlobalsHelper.GetVector2(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoPosition_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x000F7FDC File Offset: 0x000F63DC
	public static void SetPhotoPosition(int photoID, Vector2 value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoPosition_", text);
		GlobalsHelper.SetVector2(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoPosition_",
			text
		}), value);
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x000F8048 File Offset: 0x000F6448
	public static int[] KeysOfPhotoPosition()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoPosition_");
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000F8068 File Offset: 0x000F6468
	public static float GetPhotoRotation(int photoID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoRotation_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000F80A8 File Offset: 0x000F64A8
	public static void SetPhotoRotation(int photoID, float value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_PhotoRotation_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_PhotoRotation_",
			text
		}), value);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000F8114 File Offset: 0x000F6514
	public static int[] KeysOfPhotoRotation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_PhotoRotation_");
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000F8134 File Offset: 0x000F6534
	// (set) Token: 0x06001B14 RID: 6932 RVA: 0x000F8154 File Offset: 0x000F6554
	public static float Reputation
	{
		get
		{
			return PlayerPrefs.GetFloat("Profile_" + GameGlobals.Profile + "_Reputation");
		}
		set
		{
			PlayerPrefs.SetFloat("Profile_" + GameGlobals.Profile + "_Reputation", value);
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000F8175 File Offset: 0x000F6575
	// (set) Token: 0x06001B16 RID: 6934 RVA: 0x000F8195 File Offset: 0x000F6595
	public static int Seduction
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_Seduction");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_Seduction", value);
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000F81B6 File Offset: 0x000F65B6
	// (set) Token: 0x06001B18 RID: 6936 RVA: 0x000F81D6 File Offset: 0x000F65D6
	public static int SeductionBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SeductionBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SeductionBonus", value);
		}
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x000F81F7 File Offset: 0x000F65F7
	public static bool GetSenpaiPhoto(int photoID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SenpaiPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x000F8238 File Offset: 0x000F6638
	public static void SetSenpaiPhoto(int photoID, bool value)
	{
		string text = photoID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_SenpaiPhoto_",
			text
		}), value);
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x000F82A4 File Offset: 0x000F66A4
	public static int GetBullyPhoto(int photoID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BullyPhoto_",
			photoID.ToString()
		}));
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x000F82E3 File Offset: 0x000F66E3
	public static void SetBullyPhoto(int photoID, int value)
	{
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_BullyPhoto_",
			photoID.ToString()
		}), value);
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x000F8323 File Offset: 0x000F6723
	public static int[] KeysOfSenpaiPhoto()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_");
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x06001B1E RID: 6942 RVA: 0x000F8343 File Offset: 0x000F6743
	// (set) Token: 0x06001B1F RID: 6943 RVA: 0x000F8363 File Offset: 0x000F6763
	public static int SenpaiShots
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiShots");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiShots", value);
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000F8384 File Offset: 0x000F6784
	// (set) Token: 0x06001B21 RID: 6945 RVA: 0x000F83A4 File Offset: 0x000F67A4
	public static int SocialBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SocialBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SocialBonus", value);
		}
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06001B22 RID: 6946 RVA: 0x000F83C5 File Offset: 0x000F67C5
	// (set) Token: 0x06001B23 RID: 6947 RVA: 0x000F83E5 File Offset: 0x000F67E5
	public static int SpeedBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SpeedBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SpeedBonus", value);
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06001B24 RID: 6948 RVA: 0x000F8406 File Offset: 0x000F6806
	// (set) Token: 0x06001B25 RID: 6949 RVA: 0x000F8426 File Offset: 0x000F6826
	public static int StealthBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StealthBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StealthBonus", value);
		}
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x000F8447 File Offset: 0x000F6847
	public static bool GetStudentFriend(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentFriend_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x000F8488 File Offset: 0x000F6888
	public static void SetStudentFriend(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentFriend_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentFriend_",
			text
		}), value);
	}

	// Token: 0x06001B28 RID: 6952 RVA: 0x000F84F4 File Offset: 0x000F68F4
	public static int[] KeysOfStudentFriend()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentFriend_");
	}

	// Token: 0x06001B29 RID: 6953 RVA: 0x000F8514 File Offset: 0x000F6914
	public static bool GetStudentPantyShot(string studentName)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPantyShot_",
			studentName
		}));
	}

	// Token: 0x06001B2A RID: 6954 RVA: 0x000F8548 File Offset: 0x000F6948
	public static void SetStudentPantyShot(string studentName, bool value)
	{
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPantyShot_", studentName);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPantyShot_",
			studentName
		}), value);
	}

	// Token: 0x06001B2B RID: 6955 RVA: 0x000F85A6 File Offset: 0x000F69A6
	public static string[] KeysOfStudentPantyShot()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_StudentPantyShot_");
	}

	// Token: 0x06001B2C RID: 6956 RVA: 0x000F85C6 File Offset: 0x000F69C6
	public static string[] KeysOfShrineCollectible()
	{
		return KeysHelper.GetStringKeys("Profile_" + GameGlobals.Profile + "_ShrineCollectible");
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x000F85E6 File Offset: 0x000F69E6
	public static bool GetShrineCollectible(int ID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ShrineCollectible",
			ID.ToString()
		}));
	}

	// Token: 0x06001B2E RID: 6958 RVA: 0x000F8628 File Offset: 0x000F6A28
	public static void SetShrineCollectible(int ID, bool value)
	{
		string text = ID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_ShrineCollectible", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_ShrineCollectible",
			text
		}), value);
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x06001B2F RID: 6959 RVA: 0x000F8694 File Offset: 0x000F6A94
	// (set) Token: 0x06001B30 RID: 6960 RVA: 0x000F86B4 File Offset: 0x000F6AB4
	public static bool UsingGamepad
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_UsingGamepad");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_UsingGamepad", value);
		}
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x000F86D8 File Offset: 0x000F6AD8
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Money");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Alerts");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Enlightenment");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_EnlightenmentBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Friends");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Headset");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FakeID");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_RaibaruLoner");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Kills");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Numbness");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_NumbnessBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PantiesEquipped");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_PantyShots");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_Photo_", PlayerGlobals.KeysOfPhoto());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoOnCorkboard_", PlayerGlobals.KeysOfPhotoOnCorkboard());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoPosition_", PlayerGlobals.KeysOfPhotoPosition());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_PhotoRotation_", PlayerGlobals.KeysOfPhotoRotation());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Reputation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Seduction");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SeductionBonus");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_SenpaiPhoto_", PlayerGlobals.KeysOfSenpaiPhoto());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiShots");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SocialBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SpeedBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StealthBonus");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentFriend_", PlayerGlobals.KeysOfStudentFriend());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPantyShot_", PlayerGlobals.KeysOfStudentPantyShot());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_ShrineCollectible", PlayerGlobals.KeysOfShrineCollectible());
	}

	// Token: 0x04001FCF RID: 8143
	private const string Str_Money = "Money";

	// Token: 0x04001FD0 RID: 8144
	private const string Str_Alerts = "Alerts";

	// Token: 0x04001FD1 RID: 8145
	private const string Str_BullyPhoto = "BullyPhoto_";

	// Token: 0x04001FD2 RID: 8146
	private const string Str_Enlightenment = "Enlightenment";

	// Token: 0x04001FD3 RID: 8147
	private const string Str_EnlightenmentBonus = "EnlightenmentBonus";

	// Token: 0x04001FD4 RID: 8148
	private const string Str_Friends = "Friends";

	// Token: 0x04001FD5 RID: 8149
	private const string Str_Headset = "Headset";

	// Token: 0x04001FD6 RID: 8150
	private const string Str_FakeID = "FakeID";

	// Token: 0x04001FD7 RID: 8151
	private const string Str_RaibaruLoner = "RaibaruLoner";

	// Token: 0x04001FD8 RID: 8152
	private const string Str_Kills = "Kills";

	// Token: 0x04001FD9 RID: 8153
	private const string Str_Numbness = "Numbness";

	// Token: 0x04001FDA RID: 8154
	private const string Str_NumbnessBonus = "NumbnessBonus";

	// Token: 0x04001FDB RID: 8155
	private const string Str_PantiesEquipped = "PantiesEquipped";

	// Token: 0x04001FDC RID: 8156
	private const string Str_PantyShots = "PantyShots";

	// Token: 0x04001FDD RID: 8157
	private const string Str_Photo = "Photo_";

	// Token: 0x04001FDE RID: 8158
	private const string Str_PhotoOnCorkboard = "PhotoOnCorkboard_";

	// Token: 0x04001FDF RID: 8159
	private const string Str_PhotoPosition = "PhotoPosition_";

	// Token: 0x04001FE0 RID: 8160
	private const string Str_PhotoRotation = "PhotoRotation_";

	// Token: 0x04001FE1 RID: 8161
	private const string Str_Reputation = "Reputation";

	// Token: 0x04001FE2 RID: 8162
	private const string Str_Seduction = "Seduction";

	// Token: 0x04001FE3 RID: 8163
	private const string Str_SeductionBonus = "SeductionBonus";

	// Token: 0x04001FE4 RID: 8164
	private const string Str_SenpaiPhoto = "SenpaiPhoto_";

	// Token: 0x04001FE5 RID: 8165
	private const string Str_SenpaiShots = "SenpaiShots";

	// Token: 0x04001FE6 RID: 8166
	private const string Str_SocialBonus = "SocialBonus";

	// Token: 0x04001FE7 RID: 8167
	private const string Str_SpeedBonus = "SpeedBonus";

	// Token: 0x04001FE8 RID: 8168
	private const string Str_StealthBonus = "StealthBonus";

	// Token: 0x04001FE9 RID: 8169
	private const string Str_StudentFriend = "StudentFriend_";

	// Token: 0x04001FEA RID: 8170
	private const string Str_StudentPantyShot = "StudentPantyShot_";

	// Token: 0x04001FEB RID: 8171
	private const string Str_ShrineCollectible = "ShrineCollectible";

	// Token: 0x04001FEC RID: 8172
	private const string Str_UsingGamepad = "UsingGamepad";
}
