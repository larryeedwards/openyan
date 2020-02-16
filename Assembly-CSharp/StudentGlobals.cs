using System;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public static class StudentGlobals
{
	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000F98E3 File Offset: 0x000F7CE3
	// (set) Token: 0x06001B77 RID: 7031 RVA: 0x000F9903 File Offset: 0x000F7D03
	public static bool CustomSuitor
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitor");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitor", value);
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x06001B78 RID: 7032 RVA: 0x000F9924 File Offset: 0x000F7D24
	// (set) Token: 0x06001B79 RID: 7033 RVA: 0x000F9944 File Offset: 0x000F7D44
	public static int CustomSuitorAccessory
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory", value);
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x06001B7A RID: 7034 RVA: 0x000F9965 File Offset: 0x000F7D65
	// (set) Token: 0x06001B7B RID: 7035 RVA: 0x000F9985 File Offset: 0x000F7D85
	public static int CustomSuitorBlonde
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde", value);
		}
	}

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000F99A6 File Offset: 0x000F7DA6
	// (set) Token: 0x06001B7D RID: 7037 RVA: 0x000F99C6 File Offset: 0x000F7DC6
	public static int CustomSuitorEyewear
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear", value);
		}
	}

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x06001B7E RID: 7038 RVA: 0x000F99E7 File Offset: 0x000F7DE7
	// (set) Token: 0x06001B7F RID: 7039 RVA: 0x000F9A07 File Offset: 0x000F7E07
	public static int CustomSuitorHair
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorHair");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorHair", value);
		}
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x06001B80 RID: 7040 RVA: 0x000F9A28 File Offset: 0x000F7E28
	// (set) Token: 0x06001B81 RID: 7041 RVA: 0x000F9A48 File Offset: 0x000F7E48
	public static int CustomSuitorJewelry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry", value);
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x06001B82 RID: 7042 RVA: 0x000F9A69 File Offset: 0x000F7E69
	// (set) Token: 0x06001B83 RID: 7043 RVA: 0x000F9A89 File Offset: 0x000F7E89
	public static bool CustomSuitorTan
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorTan");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSuitorTan", value);
		}
	}

	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x06001B84 RID: 7044 RVA: 0x000F9AAA File Offset: 0x000F7EAA
	// (set) Token: 0x06001B85 RID: 7045 RVA: 0x000F9ACA File Offset: 0x000F7ECA
	public static int ExpelProgress
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_ExpelProgress");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_ExpelProgress", value);
		}
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x06001B86 RID: 7046 RVA: 0x000F9AEB File Offset: 0x000F7EEB
	// (set) Token: 0x06001B87 RID: 7047 RVA: 0x000F9B0B File Offset: 0x000F7F0B
	public static int FemaleUniform
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FemaleUniform");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FemaleUniform", value);
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x06001B88 RID: 7048 RVA: 0x000F9B2C File Offset: 0x000F7F2C
	// (set) Token: 0x06001B89 RID: 7049 RVA: 0x000F9B4C File Offset: 0x000F7F4C
	public static int MaleUniform
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MaleUniform");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MaleUniform", value);
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x06001B8A RID: 7050 RVA: 0x000F9B6D File Offset: 0x000F7F6D
	// (set) Token: 0x06001B8B RID: 7051 RVA: 0x000F9B8D File Offset: 0x000F7F8D
	public static int MemorialStudents
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudents");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudents", value);
		}
	}

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x06001B8C RID: 7052 RVA: 0x000F9BAE File Offset: 0x000F7FAE
	// (set) Token: 0x06001B8D RID: 7053 RVA: 0x000F9BCE File Offset: 0x000F7FCE
	public static int MemorialStudent1
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent1");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent1", value);
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x06001B8E RID: 7054 RVA: 0x000F9BEF File Offset: 0x000F7FEF
	// (set) Token: 0x06001B8F RID: 7055 RVA: 0x000F9C0F File Offset: 0x000F800F
	public static int MemorialStudent2
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent2");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent2", value);
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x06001B90 RID: 7056 RVA: 0x000F9C30 File Offset: 0x000F8030
	// (set) Token: 0x06001B91 RID: 7057 RVA: 0x000F9C50 File Offset: 0x000F8050
	public static int MemorialStudent3
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent3");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent3", value);
		}
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x06001B92 RID: 7058 RVA: 0x000F9C71 File Offset: 0x000F8071
	// (set) Token: 0x06001B93 RID: 7059 RVA: 0x000F9C91 File Offset: 0x000F8091
	public static int MemorialStudent4
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent4");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent4", value);
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000F9CB2 File Offset: 0x000F80B2
	// (set) Token: 0x06001B95 RID: 7061 RVA: 0x000F9CD2 File Offset: 0x000F80D2
	public static int MemorialStudent5
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent5");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent5", value);
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x06001B96 RID: 7062 RVA: 0x000F9CF3 File Offset: 0x000F80F3
	// (set) Token: 0x06001B97 RID: 7063 RVA: 0x000F9D13 File Offset: 0x000F8113
	public static int MemorialStudent6
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent6");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent6", value);
		}
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x06001B98 RID: 7064 RVA: 0x000F9D34 File Offset: 0x000F8134
	// (set) Token: 0x06001B99 RID: 7065 RVA: 0x000F9D54 File Offset: 0x000F8154
	public static int MemorialStudent7
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent7");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent7", value);
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x06001B9A RID: 7066 RVA: 0x000F9D75 File Offset: 0x000F8175
	// (set) Token: 0x06001B9B RID: 7067 RVA: 0x000F9D95 File Offset: 0x000F8195
	public static int MemorialStudent8
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent8");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent8", value);
		}
	}

	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x06001B9C RID: 7068 RVA: 0x000F9DB6 File Offset: 0x000F81B6
	// (set) Token: 0x06001B9D RID: 7069 RVA: 0x000F9DD6 File Offset: 0x000F81D6
	public static int MemorialStudent9
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent9");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_MemorialStudent9", value);
		}
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x000F9DF7 File Offset: 0x000F81F7
	public static string GetStudentAccessory(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentAccessory_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x000F9E38 File Offset: 0x000F8238
	public static void SetStudentAccessory(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentAccessory_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentAccessory_",
			text
		}), value);
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x000F9EA4 File Offset: 0x000F82A4
	public static int[] KeysOfStudentAccessory()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentAccessory_");
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x000F9EC4 File Offset: 0x000F82C4
	public static bool GetStudentArrested(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentArrested_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BA2 RID: 7074 RVA: 0x000F9F04 File Offset: 0x000F8304
	public static void SetStudentArrested(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentArrested_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentArrested_",
			text
		}), value);
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000F9F70 File Offset: 0x000F8370
	public static int[] KeysOfStudentArrested()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentArrested_");
	}

	// Token: 0x06001BA4 RID: 7076 RVA: 0x000F9F90 File Offset: 0x000F8390
	public static bool GetStudentBroken(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBroken_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BA5 RID: 7077 RVA: 0x000F9FD0 File Offset: 0x000F83D0
	public static void SetStudentBroken(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentBroken_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBroken_",
			text
		}), value);
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x000FA03C File Offset: 0x000F843C
	public static int[] KeysOfStudentBroken()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentBroken_");
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x000FA05C File Offset: 0x000F845C
	public static float GetStudentBustSize(int studentID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBustSize_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x000FA09C File Offset: 0x000F849C
	public static void SetStudentBustSize(int studentID, float value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentBustSize_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentBustSize_",
			text
		}), value);
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x000FA108 File Offset: 0x000F8508
	public static int[] KeysOfStudentBustSize()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentBustSize_");
	}

	// Token: 0x06001BAA RID: 7082 RVA: 0x000FA128 File Offset: 0x000F8528
	public static Color GetStudentColor(int studentID)
	{
		return GlobalsHelper.GetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentColor_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x000FA168 File Offset: 0x000F8568
	public static void SetStudentColor(int studentID, Color value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentColor_", text);
		GlobalsHelper.SetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentColor_",
			text
		}), value);
	}

	// Token: 0x06001BAC RID: 7084 RVA: 0x000FA1D4 File Offset: 0x000F85D4
	public static int[] KeysOfStudentColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentColor_");
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x000FA1F4 File Offset: 0x000F85F4
	public static bool GetStudentDead(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDead_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x000FA234 File Offset: 0x000F8634
	public static void SetStudentDead(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentDead_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDead_",
			text
		}), value);
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x000FA2A0 File Offset: 0x000F86A0
	public static int[] KeysOfStudentDead()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentDead_");
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x000FA2C0 File Offset: 0x000F86C0
	public static bool GetStudentDying(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDying_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x000FA300 File Offset: 0x000F8700
	public static void SetStudentDying(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentDying_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentDying_",
			text
		}), value);
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000FA36C File Offset: 0x000F876C
	public static int[] KeysOfStudentDying()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentDying_");
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x000FA38C File Offset: 0x000F878C
	public static bool GetStudentExpelled(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExpelled_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000FA3CC File Offset: 0x000F87CC
	public static void SetStudentExpelled(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentExpelled_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExpelled_",
			text
		}), value);
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000FA438 File Offset: 0x000F8838
	public static int[] KeysOfStudentExpelled()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentExpelled_");
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x000FA458 File Offset: 0x000F8858
	public static bool GetStudentExposed(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExposed_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x000FA498 File Offset: 0x000F8898
	public static void SetStudentExposed(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentExposed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentExposed_",
			text
		}), value);
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x000FA504 File Offset: 0x000F8904
	public static int[] KeysOfStudentExposed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentExposed_");
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x000FA524 File Offset: 0x000F8924
	public static Color GetStudentEyeColor(int studentID)
	{
		return GlobalsHelper.GetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentEyeColor_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x000FA564 File Offset: 0x000F8964
	public static void SetStudentEyeColor(int studentID, Color value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentEyeColor_", text);
		GlobalsHelper.SetColor(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentEyeColor_",
			text
		}), value);
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x000FA5D0 File Offset: 0x000F89D0
	public static int[] KeysOfStudentEyeColor()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentEyeColor_");
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x000FA5F0 File Offset: 0x000F89F0
	public static bool GetStudentGrudge(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentGrudge_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x000FA630 File Offset: 0x000F8A30
	public static void SetStudentGrudge(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentGrudge_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentGrudge_",
			text
		}), value);
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x000FA69C File Offset: 0x000F8A9C
	public static int[] KeysOfStudentGrudge()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentGrudge_");
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x000FA6BC File Offset: 0x000F8ABC
	public static string GetStudentHairstyle(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentHairstyle_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x000FA6FC File Offset: 0x000F8AFC
	public static void SetStudentHairstyle(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentHairstyle_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentHairstyle_",
			text
		}), value);
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x000FA768 File Offset: 0x000F8B68
	public static int[] KeysOfStudentHairstyle()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentHairstyle_");
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000FA788 File Offset: 0x000F8B88
	public static bool GetStudentKidnapped(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentKidnapped_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000FA7C8 File Offset: 0x000F8BC8
	public static void SetStudentKidnapped(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentKidnapped_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentKidnapped_",
			text
		}), value);
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000FA834 File Offset: 0x000F8C34
	public static int[] KeysOfStudentKidnapped()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentKidnapped_");
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000FA854 File Offset: 0x000F8C54
	public static bool GetStudentMissing(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentMissing_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000FA894 File Offset: 0x000F8C94
	public static void SetStudentMissing(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentMissing_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentMissing_",
			text
		}), value);
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000FA900 File Offset: 0x000F8D00
	public static int[] KeysOfStudentMissing()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentMissing_");
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x000FA920 File Offset: 0x000F8D20
	public static string GetStudentName(int studentID)
	{
		return PlayerPrefs.GetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentName_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x000FA960 File Offset: 0x000F8D60
	public static void SetStudentName(int studentID, string value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentName_", text);
		PlayerPrefs.SetString(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentName_",
			text
		}), value);
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000FA9CC File Offset: 0x000F8DCC
	public static int[] KeysOfStudentName()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentName_");
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x000FA9EC File Offset: 0x000F8DEC
	public static bool GetStudentPhotographed(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhotographed_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000FAA2C File Offset: 0x000F8E2C
	public static void SetStudentPhotographed(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentPhotographed_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentPhotographed_",
			text
		}), value);
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000FAA98 File Offset: 0x000F8E98
	public static int[] KeysOfStudentPhotographed()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentPhotographed_");
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x000FAAB8 File Offset: 0x000F8EB8
	public static bool GetStudentReplaced(int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReplaced_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x000FAAF8 File Offset: 0x000F8EF8
	public static void SetStudentReplaced(int studentID, bool value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentReplaced_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReplaced_",
			text
		}), value);
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000FAB64 File Offset: 0x000F8F64
	public static int[] KeysOfStudentReplaced()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentReplaced_");
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x000FAB84 File Offset: 0x000F8F84
	public static int GetStudentReputation(int studentID)
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReputation_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000FABC4 File Offset: 0x000F8FC4
	public static void SetStudentReputation(int studentID, int value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentReputation_", text);
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentReputation_",
			text
		}), value);
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x000FAC30 File Offset: 0x000F9030
	public static int[] KeysOfStudentReputation()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentReputation_");
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000FAC50 File Offset: 0x000F9050
	public static float GetStudentSanity(int studentID)
	{
		return PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentSanity_",
			studentID.ToString()
		}));
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000FAC90 File Offset: 0x000F9090
	public static void SetStudentSanity(int studentID, float value)
	{
		string text = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_StudentSanity_", text);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_StudentSanity_",
			text
		}), value);
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000FACFC File Offset: 0x000F90FC
	public static int[] KeysOfStudentSanity()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentSanity_");
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x000FAD1C File Offset: 0x000F911C
	public static int GetStudentSlave()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StudentSlave");
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x000FAD3C File Offset: 0x000F913C
	public static int GetStudentFragileSlave()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_StudentFragileSlave");
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000FAD5C File Offset: 0x000F915C
	public static void SetStudentSlave(int studentID)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StudentSlave", studentID);
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x000FAD7D File Offset: 0x000F917D
	public static void SetStudentFragileSlave(int studentID)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_StudentFragileSlave", studentID);
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x000FAD9E File Offset: 0x000F919E
	public static int[] KeysOfStudentSlave()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_StudentSlave");
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x000FADBE File Offset: 0x000F91BE
	public static int GetFragileTarget()
	{
		return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_FragileTarget");
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x000FADDE File Offset: 0x000F91DE
	public static void SetFragileTarget(int value)
	{
		PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_FragileTarget", value);
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000FADFF File Offset: 0x000F91FF
	public static Vector3 GetReputationTriangle(int studentID)
	{
		return GlobalsHelper.GetVector3(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Student_",
			studentID,
			"_ReputatonTriangle"
		}));
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000FAE40 File Offset: 0x000F9240
	public static void SetReputationTriangle(int studentID, Vector3 triangle)
	{
		GlobalsHelper.SetVector3(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_Student_",
			studentID,
			"_ReputatonTriangle"
		}), triangle);
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000FAE8C File Offset: 0x000F928C
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorAccessory");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorBlonde");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorEyewear");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorJewelry");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSuitorTan");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_ExpelProgress");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FemaleUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MaleUniform");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StudentSlave");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_StudentFragileSlave");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_FragileTarget");
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentAccessory_", StudentGlobals.KeysOfStudentAccessory());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentArrested_", StudentGlobals.KeysOfStudentArrested());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentBroken_", StudentGlobals.KeysOfStudentBroken());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentBustSize_", StudentGlobals.KeysOfStudentBustSize());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_StudentColor_", StudentGlobals.KeysOfStudentColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentDead_", StudentGlobals.KeysOfStudentDead());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentDying_", StudentGlobals.KeysOfStudentDying());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentExpelled_", StudentGlobals.KeysOfStudentExpelled());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentExposed_", StudentGlobals.KeysOfStudentExposed());
		GlobalsHelper.DeleteColorCollection("Profile_" + GameGlobals.Profile + "_StudentEyeColor_", StudentGlobals.KeysOfStudentEyeColor());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentGrudge_", StudentGlobals.KeysOfStudentGrudge());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentHairstyle_", StudentGlobals.KeysOfStudentHairstyle());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentKidnapped_", StudentGlobals.KeysOfStudentKidnapped());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentMissing_", StudentGlobals.KeysOfStudentMissing());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentName_", StudentGlobals.KeysOfStudentName());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentPhotographed_", StudentGlobals.KeysOfStudentPhotographed());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentReplaced_", StudentGlobals.KeysOfStudentReplaced());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentReputation_", StudentGlobals.KeysOfStudentReputation());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentSanity_", StudentGlobals.KeysOfStudentSanity());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_StudentSlave", StudentGlobals.KeysOfStudentSlave());
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudents");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent3");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent4");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent5");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent6");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent7");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent8");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_MemorialStudent9");
	}

	// Token: 0x04002009 RID: 8201
	private const string Str_CustomSuitor = "CustomSuitor";

	// Token: 0x0400200A RID: 8202
	private const string Str_CustomSuitorAccessory = "CustomSuitorAccessory";

	// Token: 0x0400200B RID: 8203
	private const string Str_CustomSuitorBlonde = "CustomSuitorBlonde";

	// Token: 0x0400200C RID: 8204
	private const string Str_CustomSuitorEyewear = "CustomSuitorEyewear";

	// Token: 0x0400200D RID: 8205
	private const string Str_CustomSuitorHair = "CustomSuitorHair";

	// Token: 0x0400200E RID: 8206
	private const string Str_CustomSuitorJewelry = "CustomSuitorJewelry";

	// Token: 0x0400200F RID: 8207
	private const string Str_CustomSuitorTan = "CustomSuitorTan";

	// Token: 0x04002010 RID: 8208
	private const string Str_ExpelProgress = "ExpelProgress";

	// Token: 0x04002011 RID: 8209
	private const string Str_FemaleUniform = "FemaleUniform";

	// Token: 0x04002012 RID: 8210
	private const string Str_MaleUniform = "MaleUniform";

	// Token: 0x04002013 RID: 8211
	private const string Str_StudentAccessory = "StudentAccessory_";

	// Token: 0x04002014 RID: 8212
	private const string Str_StudentArrested = "StudentArrested_";

	// Token: 0x04002015 RID: 8213
	private const string Str_StudentBroken = "StudentBroken_";

	// Token: 0x04002016 RID: 8214
	private const string Str_StudentBustSize = "StudentBustSize_";

	// Token: 0x04002017 RID: 8215
	private const string Str_StudentColor = "StudentColor_";

	// Token: 0x04002018 RID: 8216
	private const string Str_StudentDead = "StudentDead_";

	// Token: 0x04002019 RID: 8217
	private const string Str_StudentDying = "StudentDying_";

	// Token: 0x0400201A RID: 8218
	private const string Str_StudentExpelled = "StudentExpelled_";

	// Token: 0x0400201B RID: 8219
	private const string Str_StudentExposed = "StudentExposed_";

	// Token: 0x0400201C RID: 8220
	private const string Str_StudentEyeColor = "StudentEyeColor_";

	// Token: 0x0400201D RID: 8221
	private const string Str_StudentGrudge = "StudentGrudge_";

	// Token: 0x0400201E RID: 8222
	private const string Str_StudentHairstyle = "StudentHairstyle_";

	// Token: 0x0400201F RID: 8223
	private const string Str_StudentKidnapped = "StudentKidnapped_";

	// Token: 0x04002020 RID: 8224
	private const string Str_StudentMissing = "StudentMissing_";

	// Token: 0x04002021 RID: 8225
	private const string Str_StudentName = "StudentName_";

	// Token: 0x04002022 RID: 8226
	private const string Str_StudentPhotographed = "StudentPhotographed_";

	// Token: 0x04002023 RID: 8227
	private const string Str_StudentReplaced = "StudentReplaced_";

	// Token: 0x04002024 RID: 8228
	private const string Str_StudentReputation = "StudentReputation_";

	// Token: 0x04002025 RID: 8229
	private const string Str_StudentSanity = "StudentSanity_";

	// Token: 0x04002026 RID: 8230
	private const string Str_StudentSlave = "StudentSlave";

	// Token: 0x04002027 RID: 8231
	private const string Str_StudentFragileSlave = "StudentFragileSlave";

	// Token: 0x04002028 RID: 8232
	private const string Str_FragileTarget = "FragileTarget";

	// Token: 0x04002029 RID: 8233
	private const string Str_ReputationTriangle = "ReputatonTriangle";

	// Token: 0x0400202A RID: 8234
	private const string Str_MemorialStudents = "MemorialStudents";

	// Token: 0x0400202B RID: 8235
	private const string Str_MemorialStudent1 = "MemorialStudent1";

	// Token: 0x0400202C RID: 8236
	private const string Str_MemorialStudent2 = "MemorialStudent2";

	// Token: 0x0400202D RID: 8237
	private const string Str_MemorialStudent3 = "MemorialStudent3";

	// Token: 0x0400202E RID: 8238
	private const string Str_MemorialStudent4 = "MemorialStudent4";

	// Token: 0x0400202F RID: 8239
	private const string Str_MemorialStudent5 = "MemorialStudent5";

	// Token: 0x04002030 RID: 8240
	private const string Str_MemorialStudent6 = "MemorialStudent6";

	// Token: 0x04002031 RID: 8241
	private const string Str_MemorialStudent7 = "MemorialStudent7";

	// Token: 0x04002032 RID: 8242
	private const string Str_MemorialStudent8 = "MemorialStudent8";

	// Token: 0x04002033 RID: 8243
	private const string Str_MemorialStudent9 = "MemorialStudent9";
}
