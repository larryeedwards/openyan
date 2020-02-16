﻿using System;
using System.Collections.Generic;

// Token: 0x020004EB RID: 1259
[Serializable]
public class SchemeSaveData
{
	// Token: 0x06001F8F RID: 8079 RVA: 0x00142134 File Offset: 0x00140534
	public static SchemeSaveData ReadFromGlobals()
	{
		SchemeSaveData schemeSaveData = new SchemeSaveData();
		schemeSaveData.currentScheme = SchemeGlobals.CurrentScheme;
		schemeSaveData.darkSecret = SchemeGlobals.DarkSecret;
		foreach (int num in SchemeGlobals.KeysOfSchemePreviousStage())
		{
			schemeSaveData.schemePreviousStage.Add(num, SchemeGlobals.GetSchemePreviousStage(num));
		}
		foreach (int num2 in SchemeGlobals.KeysOfSchemeStage())
		{
			schemeSaveData.schemeStage.Add(num2, SchemeGlobals.GetSchemeStage(num2));
		}
		foreach (int num3 in SchemeGlobals.KeysOfSchemeStatus())
		{
			if (SchemeGlobals.GetSchemeStatus(num3))
			{
				schemeSaveData.schemeStatus.Add(num3);
			}
		}
		foreach (int num4 in SchemeGlobals.KeysOfSchemeUnlocked())
		{
			if (SchemeGlobals.GetSchemeUnlocked(num4))
			{
				schemeSaveData.schemeUnlocked.Add(num4);
			}
		}
		foreach (int num5 in SchemeGlobals.KeysOfServicePurchased())
		{
			if (SchemeGlobals.GetServicePurchased(num5))
			{
				schemeSaveData.servicePurchased.Add(num5);
			}
		}
		return schemeSaveData;
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x0014228C File Offset: 0x0014068C
	public static void WriteToGlobals(SchemeSaveData data)
	{
		SchemeGlobals.CurrentScheme = data.currentScheme;
		SchemeGlobals.DarkSecret = data.darkSecret;
		foreach (KeyValuePair<int, int> keyValuePair in data.schemePreviousStage)
		{
			SchemeGlobals.SetSchemePreviousStage(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in data.schemeStage)
		{
			SchemeGlobals.SetSchemeStage(keyValuePair2.Key, keyValuePair2.Value);
		}
		foreach (int schemeID in data.schemeStatus)
		{
			SchemeGlobals.SetSchemeStatus(schemeID, true);
		}
		foreach (int schemeID2 in data.schemeUnlocked)
		{
			SchemeGlobals.SetSchemeUnlocked(schemeID2, true);
		}
		foreach (int serviceID in data.servicePurchased)
		{
			SchemeGlobals.SetServicePurchased(serviceID, true);
		}
	}

	// Token: 0x04002AEC RID: 10988
	public int currentScheme;

	// Token: 0x04002AED RID: 10989
	public bool darkSecret;

	// Token: 0x04002AEE RID: 10990
	public IntAndIntDictionary schemePreviousStage = new IntAndIntDictionary();

	// Token: 0x04002AEF RID: 10991
	public IntAndIntDictionary schemeStage = new IntAndIntDictionary();

	// Token: 0x04002AF0 RID: 10992
	public IntHashSet schemeStatus = new IntHashSet();

	// Token: 0x04002AF1 RID: 10993
	public IntHashSet schemeUnlocked = new IntHashSet();

	// Token: 0x04002AF2 RID: 10994
	public IntHashSet servicePurchased = new IntHashSet();
}
