using System;

// Token: 0x020004E5 RID: 1253
[Serializable]
public class HomeSaveData
{
	// Token: 0x06001F7D RID: 8061 RVA: 0x00141744 File Offset: 0x0013FB44
	public static HomeSaveData ReadFromGlobals()
	{
		return new HomeSaveData
		{
			lateForSchool = HomeGlobals.LateForSchool,
			night = HomeGlobals.Night,
			startInBasement = HomeGlobals.StartInBasement
		};
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x00141779 File Offset: 0x0013FB79
	public static void WriteToGlobals(HomeSaveData data)
	{
		HomeGlobals.LateForSchool = data.lateForSchool;
		HomeGlobals.Night = data.night;
		HomeGlobals.StartInBasement = data.startInBasement;
	}

	// Token: 0x04002AB9 RID: 10937
	public bool lateForSchool;

	// Token: 0x04002ABA RID: 10938
	public bool night;

	// Token: 0x04002ABB RID: 10939
	public bool startInBasement;
}
