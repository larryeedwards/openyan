using System;
using System.Collections.Generic;

// Token: 0x020004E2 RID: 1250
[Serializable]
public class DatingSaveData
{
	// Token: 0x06001F74 RID: 8052 RVA: 0x00141324 File Offset: 0x0013F724
	public static DatingSaveData ReadFromGlobals()
	{
		DatingSaveData datingSaveData = new DatingSaveData();
		datingSaveData.affection = DatingGlobals.Affection;
		datingSaveData.affectionLevel = DatingGlobals.AffectionLevel;
		foreach (int num in DatingGlobals.KeysOfComplimentGiven())
		{
			if (DatingGlobals.GetComplimentGiven(num))
			{
				datingSaveData.complimentGiven.Add(num);
			}
		}
		foreach (int num2 in DatingGlobals.KeysOfSuitorCheck())
		{
			if (DatingGlobals.GetSuitorCheck(num2))
			{
				datingSaveData.suitorCheck.Add(num2);
			}
		}
		datingSaveData.suitorProgress = DatingGlobals.SuitorProgress;
		foreach (int num3 in DatingGlobals.KeysOfSuitorTrait())
		{
			datingSaveData.suitorTrait.Add(num3, DatingGlobals.GetSuitorTrait(num3));
		}
		foreach (int num4 in DatingGlobals.KeysOfTopicDiscussed())
		{
			if (DatingGlobals.GetTopicDiscussed(num4))
			{
				datingSaveData.topicDiscussed.Add(num4);
			}
		}
		foreach (int num5 in DatingGlobals.KeysOfTraitDemonstrated())
		{
			datingSaveData.traitDemonstrated.Add(num5, DatingGlobals.GetTraitDemonstrated(num5));
		}
		return datingSaveData;
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x00141488 File Offset: 0x0013F888
	public static void WriteToGlobals(DatingSaveData data)
	{
		DatingGlobals.Affection = data.affection;
		DatingGlobals.AffectionLevel = data.affectionLevel;
		foreach (int complimentID in data.complimentGiven)
		{
			DatingGlobals.SetComplimentGiven(complimentID, true);
		}
		foreach (int checkID in data.suitorCheck)
		{
			DatingGlobals.SetSuitorCheck(checkID, true);
		}
		DatingGlobals.SuitorProgress = data.suitorProgress;
		foreach (KeyValuePair<int, int> keyValuePair in data.suitorTrait)
		{
			DatingGlobals.SetSuitorTrait(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (int topicID in data.topicDiscussed)
		{
			DatingGlobals.SetTopicDiscussed(topicID, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in data.traitDemonstrated)
		{
			DatingGlobals.SetTraitDemonstrated(keyValuePair2.Key, keyValuePair2.Value);
		}
	}

	// Token: 0x04002AA9 RID: 10921
	public float affection;

	// Token: 0x04002AAA RID: 10922
	public float affectionLevel;

	// Token: 0x04002AAB RID: 10923
	public IntHashSet complimentGiven = new IntHashSet();

	// Token: 0x04002AAC RID: 10924
	public IntHashSet suitorCheck = new IntHashSet();

	// Token: 0x04002AAD RID: 10925
	public int suitorProgress;

	// Token: 0x04002AAE RID: 10926
	public IntAndIntDictionary suitorTrait = new IntAndIntDictionary();

	// Token: 0x04002AAF RID: 10927
	public IntHashSet topicDiscussed = new IntHashSet();

	// Token: 0x04002AB0 RID: 10928
	public IntAndIntDictionary traitDemonstrated = new IntAndIntDictionary();
}
