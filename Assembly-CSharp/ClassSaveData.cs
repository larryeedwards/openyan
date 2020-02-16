using System;

// Token: 0x020004DD RID: 1245
[Serializable]
public class ClassSaveData
{
	// Token: 0x06001F65 RID: 8037 RVA: 0x00140A98 File Offset: 0x0013EE98
	public static ClassSaveData ReadFromGlobals()
	{
		return new ClassSaveData
		{
			biology = ClassGlobals.Biology,
			biologyBonus = ClassGlobals.BiologyBonus,
			biologyGrade = ClassGlobals.BiologyGrade,
			chemistry = ClassGlobals.Chemistry,
			chemistryBonus = ClassGlobals.ChemistryBonus,
			chemistryGrade = ClassGlobals.ChemistryGrade,
			language = ClassGlobals.Language,
			languageBonus = ClassGlobals.LanguageBonus,
			languageGrade = ClassGlobals.LanguageGrade,
			physical = ClassGlobals.Physical,
			physicalBonus = ClassGlobals.PhysicalBonus,
			physicalGrade = ClassGlobals.PhysicalGrade,
			psychology = ClassGlobals.Psychology,
			psychologyBonus = ClassGlobals.PsychologyBonus,
			psychologyGrade = ClassGlobals.PsychologyGrade
		};
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x00140B54 File Offset: 0x0013EF54
	public static void WriteToGlobals(ClassSaveData data)
	{
		ClassGlobals.Biology = data.biology;
		ClassGlobals.BiologyBonus = data.biologyBonus;
		ClassGlobals.BiologyGrade = data.biologyGrade;
		ClassGlobals.Chemistry = data.chemistry;
		ClassGlobals.ChemistryBonus = data.chemistryBonus;
		ClassGlobals.ChemistryGrade = data.chemistryGrade;
		ClassGlobals.Language = data.language;
		ClassGlobals.LanguageBonus = data.languageBonus;
		ClassGlobals.LanguageGrade = data.languageGrade;
		ClassGlobals.Physical = data.physical;
		ClassGlobals.PhysicalBonus = data.physicalBonus;
		ClassGlobals.PhysicalGrade = data.physicalGrade;
		ClassGlobals.Psychology = data.psychology;
		ClassGlobals.PsychologyBonus = data.psychologyBonus;
		ClassGlobals.PsychologyGrade = data.psychologyGrade;
	}

	// Token: 0x04002A8D RID: 10893
	public int biology;

	// Token: 0x04002A8E RID: 10894
	public int biologyBonus;

	// Token: 0x04002A8F RID: 10895
	public int biologyGrade;

	// Token: 0x04002A90 RID: 10896
	public int chemistry;

	// Token: 0x04002A91 RID: 10897
	public int chemistryBonus;

	// Token: 0x04002A92 RID: 10898
	public int chemistryGrade;

	// Token: 0x04002A93 RID: 10899
	public int language;

	// Token: 0x04002A94 RID: 10900
	public int languageBonus;

	// Token: 0x04002A95 RID: 10901
	public int languageGrade;

	// Token: 0x04002A96 RID: 10902
	public int physical;

	// Token: 0x04002A97 RID: 10903
	public int physicalBonus;

	// Token: 0x04002A98 RID: 10904
	public int physicalGrade;

	// Token: 0x04002A99 RID: 10905
	public int psychology;

	// Token: 0x04002A9A RID: 10906
	public int psychologyBonus;

	// Token: 0x04002A9B RID: 10907
	public int psychologyGrade;
}
