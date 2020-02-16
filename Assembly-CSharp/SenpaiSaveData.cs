using System;

// Token: 0x020004ED RID: 1261
[Serializable]
public class SenpaiSaveData
{
	// Token: 0x06001F95 RID: 8085 RVA: 0x00142648 File Offset: 0x00140A48
	public static SenpaiSaveData ReadFromGlobals()
	{
		return new SenpaiSaveData
		{
			customSenpai = SenpaiGlobals.CustomSenpai,
			senpaiEyeColor = SenpaiGlobals.SenpaiEyeColor,
			senpaiEyeWear = SenpaiGlobals.SenpaiEyeWear,
			senpaiFacialHair = SenpaiGlobals.SenpaiFacialHair,
			senpaiHairColor = SenpaiGlobals.SenpaiHairColor,
			senpaiHairStyle = SenpaiGlobals.SenpaiHairStyle,
			senpaiSkinColor = SenpaiGlobals.SenpaiSkinColor
		};
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x001426AC File Offset: 0x00140AAC
	public static void WriteToGlobals(SenpaiSaveData data)
	{
		SenpaiGlobals.CustomSenpai = data.customSenpai;
		SenpaiGlobals.SenpaiEyeColor = data.senpaiEyeColor;
		SenpaiGlobals.SenpaiEyeWear = data.senpaiEyeWear;
		SenpaiGlobals.SenpaiFacialHair = data.senpaiFacialHair;
		SenpaiGlobals.SenpaiHairColor = data.senpaiHairColor;
		SenpaiGlobals.SenpaiHairStyle = data.senpaiHairStyle;
		SenpaiGlobals.SenpaiSkinColor = data.senpaiSkinColor;
	}

	// Token: 0x04002AFB RID: 11003
	public bool customSenpai;

	// Token: 0x04002AFC RID: 11004
	public string senpaiEyeColor = string.Empty;

	// Token: 0x04002AFD RID: 11005
	public int senpaiEyeWear;

	// Token: 0x04002AFE RID: 11006
	public int senpaiFacialHair;

	// Token: 0x04002AFF RID: 11007
	public string senpaiHairColor = string.Empty;

	// Token: 0x04002B00 RID: 11008
	public int senpaiHairStyle;

	// Token: 0x04002B01 RID: 11009
	public int senpaiSkinColor;
}
