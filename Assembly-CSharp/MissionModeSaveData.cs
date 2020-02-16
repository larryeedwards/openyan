using System;
using System.Collections.Generic;

// Token: 0x020004E6 RID: 1254
[Serializable]
public class MissionModeSaveData
{
	// Token: 0x06001F80 RID: 8064 RVA: 0x001417BC File Offset: 0x0013FBBC
	public static MissionModeSaveData ReadFromGlobals()
	{
		MissionModeSaveData missionModeSaveData = new MissionModeSaveData();
		foreach (int num in MissionModeGlobals.KeysOfMissionCondition())
		{
			missionModeSaveData.missionCondition.Add(num, MissionModeGlobals.GetMissionCondition(num));
		}
		missionModeSaveData.missionDifficulty = MissionModeGlobals.MissionDifficulty;
		missionModeSaveData.missionMode = MissionModeGlobals.MissionMode;
		missionModeSaveData.missionRequiredClothing = MissionModeGlobals.MissionRequiredClothing;
		missionModeSaveData.missionRequiredDisposal = MissionModeGlobals.MissionRequiredDisposal;
		missionModeSaveData.missionRequiredWeapon = MissionModeGlobals.MissionRequiredWeapon;
		missionModeSaveData.missionTarget = MissionModeGlobals.MissionTarget;
		missionModeSaveData.missionTargetName = MissionModeGlobals.MissionTargetName;
		missionModeSaveData.nemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
		return missionModeSaveData;
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x00141858 File Offset: 0x0013FC58
	public static void WriteToGlobals(MissionModeSaveData data)
	{
		foreach (KeyValuePair<int, int> keyValuePair in data.missionCondition)
		{
			MissionModeGlobals.SetMissionCondition(keyValuePair.Key, keyValuePair.Value);
		}
		MissionModeGlobals.MissionDifficulty = data.missionDifficulty;
		MissionModeGlobals.MissionMode = data.missionMode;
		MissionModeGlobals.MissionRequiredClothing = data.missionRequiredClothing;
		MissionModeGlobals.MissionRequiredDisposal = data.missionRequiredDisposal;
		MissionModeGlobals.MissionRequiredWeapon = data.missionRequiredWeapon;
		MissionModeGlobals.MissionTarget = data.missionTarget;
		MissionModeGlobals.MissionTargetName = data.missionTargetName;
		MissionModeGlobals.NemesisDifficulty = data.nemesisDifficulty;
	}

	// Token: 0x04002ABC RID: 10940
	public IntAndIntDictionary missionCondition = new IntAndIntDictionary();

	// Token: 0x04002ABD RID: 10941
	public int missionDifficulty;

	// Token: 0x04002ABE RID: 10942
	public bool missionMode;

	// Token: 0x04002ABF RID: 10943
	public int missionRequiredClothing;

	// Token: 0x04002AC0 RID: 10944
	public int missionRequiredDisposal;

	// Token: 0x04002AC1 RID: 10945
	public int missionRequiredWeapon;

	// Token: 0x04002AC2 RID: 10946
	public int missionTarget;

	// Token: 0x04002AC3 RID: 10947
	public string missionTargetName = string.Empty;

	// Token: 0x04002AC4 RID: 10948
	public int nemesisDifficulty;
}
