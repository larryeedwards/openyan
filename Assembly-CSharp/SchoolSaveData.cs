using System;

// Token: 0x020004EC RID: 1260
[Serializable]
public class SchoolSaveData
{
	// Token: 0x06001F92 RID: 8082 RVA: 0x0014246C File Offset: 0x0014086C
	public static SchoolSaveData ReadFromGlobals()
	{
		SchoolSaveData schoolSaveData = new SchoolSaveData();
		foreach (int num in SchoolGlobals.KeysOfDemonActive())
		{
			if (SchoolGlobals.GetDemonActive(num))
			{
				schoolSaveData.demonActive.Add(num);
			}
		}
		foreach (int num2 in SchoolGlobals.KeysOfGardenGraveOccupied())
		{
			if (SchoolGlobals.GetGardenGraveOccupied(num2))
			{
				schoolSaveData.gardenGraveOccupied.Add(num2);
			}
		}
		schoolSaveData.kidnapVictim = SchoolGlobals.KidnapVictim;
		schoolSaveData.population = SchoolGlobals.Population;
		schoolSaveData.roofFence = SchoolGlobals.RoofFence;
		schoolSaveData.schoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
		schoolSaveData.schoolAtmosphereSet = SchoolGlobals.SchoolAtmosphereSet;
		schoolSaveData.scp = SchoolGlobals.SCP;
		return schoolSaveData;
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x0014253C File Offset: 0x0014093C
	public static void WriteToGlobals(SchoolSaveData data)
	{
		foreach (int demonID in data.demonActive)
		{
			SchoolGlobals.SetDemonActive(demonID, true);
		}
		foreach (int graveID in data.gardenGraveOccupied)
		{
			SchoolGlobals.SetGardenGraveOccupied(graveID, true);
		}
		SchoolGlobals.KidnapVictim = data.kidnapVictim;
		SchoolGlobals.Population = data.population;
		SchoolGlobals.RoofFence = data.roofFence;
		SchoolGlobals.SchoolAtmosphere = data.schoolAtmosphere;
		SchoolGlobals.SchoolAtmosphereSet = data.schoolAtmosphereSet;
		SchoolGlobals.SCP = data.scp;
	}

	// Token: 0x04002AF3 RID: 10995
	public IntHashSet demonActive = new IntHashSet();

	// Token: 0x04002AF4 RID: 10996
	public IntHashSet gardenGraveOccupied = new IntHashSet();

	// Token: 0x04002AF5 RID: 10997
	public int kidnapVictim;

	// Token: 0x04002AF6 RID: 10998
	public int population;

	// Token: 0x04002AF7 RID: 10999
	public bool roofFence;

	// Token: 0x04002AF8 RID: 11000
	public float schoolAtmosphere;

	// Token: 0x04002AF9 RID: 11001
	public bool schoolAtmosphereSet;

	// Token: 0x04002AFA RID: 11002
	public bool scp;
}
