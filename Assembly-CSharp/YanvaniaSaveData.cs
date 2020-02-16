using System;

// Token: 0x020004F0 RID: 1264
[Serializable]
public class YanvaniaSaveData
{
	// Token: 0x06001F9E RID: 8094 RVA: 0x001435F4 File Offset: 0x001419F4
	public static YanvaniaSaveData ReadFromGlobals()
	{
		return new YanvaniaSaveData
		{
			draculaDefeated = YanvaniaGlobals.DraculaDefeated,
			midoriEasterEgg = YanvaniaGlobals.MidoriEasterEgg
		};
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x0014361E File Offset: 0x00141A1E
	public static void WriteToGlobals(YanvaniaSaveData data)
	{
		YanvaniaGlobals.DraculaDefeated = data.draculaDefeated;
		YanvaniaGlobals.MidoriEasterEgg = data.midoriEasterEgg;
	}

	// Token: 0x04002B24 RID: 11044
	public bool draculaDefeated;

	// Token: 0x04002B25 RID: 11045
	public bool midoriEasterEgg;
}
