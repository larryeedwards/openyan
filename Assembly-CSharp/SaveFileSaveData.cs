using System;

// Token: 0x020004EA RID: 1258
[Serializable]
public class SaveFileSaveData
{
	// Token: 0x06001F8C RID: 8076 RVA: 0x001420C8 File Offset: 0x001404C8
	public static SaveFileSaveData ReadFromGlobals()
	{
		return new SaveFileSaveData
		{
			currentSaveFile = SaveFileGlobals.CurrentSaveFile
		};
	}

	// Token: 0x06001F8D RID: 8077 RVA: 0x001420E7 File Offset: 0x001404E7
	public static void WriteToGlobals(SaveFileSaveData data)
	{
		SaveFileGlobals.CurrentSaveFile = data.currentSaveFile;
	}

	// Token: 0x04002AEB RID: 10987
	public int currentSaveFile;
}
