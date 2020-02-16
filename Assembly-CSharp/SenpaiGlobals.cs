using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public static class SenpaiGlobals
{
	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000F963B File Offset: 0x000F7A3B
	// (set) Token: 0x06001B68 RID: 7016 RVA: 0x000F965B File Offset: 0x000F7A5B
	public static bool CustomSenpai
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_CustomSenpai");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_CustomSenpai", value);
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000F967C File Offset: 0x000F7A7C
	// (set) Token: 0x06001B6A RID: 7018 RVA: 0x000F969C File Offset: 0x000F7A9C
	public static string SenpaiEyeColor
	{
		get
		{
			return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor");
		}
		set
		{
			PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor", value);
		}
	}

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000F96BD File Offset: 0x000F7ABD
	// (set) Token: 0x06001B6C RID: 7020 RVA: 0x000F96DD File Offset: 0x000F7ADD
	public static int SenpaiEyeWear
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear", value);
		}
	}

	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000F96FE File Offset: 0x000F7AFE
	// (set) Token: 0x06001B6E RID: 7022 RVA: 0x000F971E File Offset: 0x000F7B1E
	public static int SenpaiFacialHair
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair", value);
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000F973F File Offset: 0x000F7B3F
	// (set) Token: 0x06001B70 RID: 7024 RVA: 0x000F975F File Offset: 0x000F7B5F
	public static string SenpaiHairColor
	{
		get
		{
			return PlayerPrefs.GetString("Profile_" + GameGlobals.Profile + "_SenpaiHairColor");
		}
		set
		{
			PlayerPrefs.SetString("Profile_" + GameGlobals.Profile + "_SenpaiHairColor", value);
		}
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000F9780 File Offset: 0x000F7B80
	// (set) Token: 0x06001B72 RID: 7026 RVA: 0x000F97A0 File Offset: 0x000F7BA0
	public static int SenpaiHairStyle
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle", value);
		}
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x06001B73 RID: 7027 RVA: 0x000F97C1 File Offset: 0x000F7BC1
	// (set) Token: 0x06001B74 RID: 7028 RVA: 0x000F97E1 File Offset: 0x000F7BE1
	public static int SenpaiSkinColor
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor", value);
		}
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x000F9804 File Offset: 0x000F7C04
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_CustomSenpai");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiEyeColor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiEyeWear");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiFacialHair");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiHairColor");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiHairStyle");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_SenpaiSkinColor");
	}

	// Token: 0x04002002 RID: 8194
	private const string Str_CustomSenpai = "CustomSenpai";

	// Token: 0x04002003 RID: 8195
	private const string Str_SenpaiEyeColor = "SenpaiEyeColor";

	// Token: 0x04002004 RID: 8196
	private const string Str_SenpaiEyeWear = "SenpaiEyeWear";

	// Token: 0x04002005 RID: 8197
	private const string Str_SenpaiFacialHair = "SenpaiFacialHair";

	// Token: 0x04002006 RID: 8198
	private const string Str_SenpaiHairColor = "SenpaiHairColor";

	// Token: 0x04002007 RID: 8199
	private const string Str_SenpaiHairStyle = "SenpaiHairStyle";

	// Token: 0x04002008 RID: 8200
	private const string Str_SenpaiSkinColor = "SenpaiSkinColor";
}
