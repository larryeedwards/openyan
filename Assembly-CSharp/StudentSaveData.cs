using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004EE RID: 1262
[Serializable]
public class StudentSaveData
{
	// Token: 0x06001F98 RID: 8088 RVA: 0x001427F8 File Offset: 0x00140BF8
	public static StudentSaveData ReadFromGlobals()
	{
		StudentSaveData studentSaveData = new StudentSaveData();
		studentSaveData.customSuitor = StudentGlobals.CustomSuitor;
		studentSaveData.customSuitorAccessory = StudentGlobals.CustomSuitorAccessory;
		studentSaveData.customSuitorBlonde = StudentGlobals.CustomSuitorBlonde;
		studentSaveData.customSuitorEyewear = StudentGlobals.CustomSuitorEyewear;
		studentSaveData.customSuitorHair = StudentGlobals.CustomSuitorHair;
		studentSaveData.customSuitorJewelry = StudentGlobals.CustomSuitorJewelry;
		studentSaveData.customSuitorTan = StudentGlobals.CustomSuitorTan;
		studentSaveData.expelProgress = StudentGlobals.ExpelProgress;
		studentSaveData.femaleUniform = StudentGlobals.FemaleUniform;
		studentSaveData.maleUniform = StudentGlobals.MaleUniform;
		foreach (int num in StudentGlobals.KeysOfStudentAccessory())
		{
			studentSaveData.studentAccessory.Add(num, StudentGlobals.GetStudentAccessory(num));
		}
		foreach (int num2 in StudentGlobals.KeysOfStudentArrested())
		{
			if (StudentGlobals.GetStudentArrested(num2))
			{
				studentSaveData.studentArrested.Add(num2);
			}
		}
		foreach (int num3 in StudentGlobals.KeysOfStudentBroken())
		{
			if (StudentGlobals.GetStudentBroken(num3))
			{
				studentSaveData.studentBroken.Add(num3);
			}
		}
		foreach (int num4 in StudentGlobals.KeysOfStudentBustSize())
		{
			studentSaveData.studentBustSize.Add(num4, StudentGlobals.GetStudentBustSize(num4));
		}
		foreach (int num5 in StudentGlobals.KeysOfStudentColor())
		{
			studentSaveData.studentColor.Add(num5, StudentGlobals.GetStudentColor(num5));
		}
		foreach (int num6 in StudentGlobals.KeysOfStudentDead())
		{
			if (StudentGlobals.GetStudentDead(num6))
			{
				studentSaveData.studentDead.Add(num6);
			}
		}
		foreach (int num8 in StudentGlobals.KeysOfStudentDying())
		{
			if (StudentGlobals.GetStudentDying(num8))
			{
				studentSaveData.studentDying.Add(num8);
			}
		}
		foreach (int num10 in StudentGlobals.KeysOfStudentExpelled())
		{
			if (StudentGlobals.GetStudentExpelled(num10))
			{
				studentSaveData.studentExpelled.Add(num10);
			}
		}
		foreach (int num12 in StudentGlobals.KeysOfStudentExposed())
		{
			if (StudentGlobals.GetStudentExposed(num12))
			{
				studentSaveData.studentExposed.Add(num12);
			}
		}
		foreach (int num14 in StudentGlobals.KeysOfStudentEyeColor())
		{
			studentSaveData.studentEyeColor.Add(num14, StudentGlobals.GetStudentEyeColor(num14));
		}
		foreach (int num16 in StudentGlobals.KeysOfStudentGrudge())
		{
			if (StudentGlobals.GetStudentGrudge(num16))
			{
				studentSaveData.studentGrudge.Add(num16);
			}
		}
		foreach (int num18 in StudentGlobals.KeysOfStudentHairstyle())
		{
			studentSaveData.studentHairstyle.Add(num18, StudentGlobals.GetStudentHairstyle(num18));
		}
		foreach (int num20 in StudentGlobals.KeysOfStudentKidnapped())
		{
			if (StudentGlobals.GetStudentKidnapped(num20))
			{
				studentSaveData.studentKidnapped.Add(num20);
			}
		}
		foreach (int num22 in StudentGlobals.KeysOfStudentMissing())
		{
			if (StudentGlobals.GetStudentMissing(num22))
			{
				studentSaveData.studentMissing.Add(num22);
			}
		}
		foreach (int num24 in StudentGlobals.KeysOfStudentName())
		{
			studentSaveData.studentName.Add(num24, StudentGlobals.GetStudentName(num24));
		}
		foreach (int num26 in StudentGlobals.KeysOfStudentPhotographed())
		{
			if (StudentGlobals.GetStudentPhotographed(num26))
			{
				studentSaveData.studentPhotographed.Add(num26);
			}
		}
		foreach (int num28 in StudentGlobals.KeysOfStudentReplaced())
		{
			if (StudentGlobals.GetStudentReplaced(num28))
			{
				studentSaveData.studentReplaced.Add(num28);
			}
		}
		foreach (int num30 in StudentGlobals.KeysOfStudentReputation())
		{
			studentSaveData.studentReputation.Add(num30, StudentGlobals.GetStudentReputation(num30));
		}
		foreach (int num32 in StudentGlobals.KeysOfStudentSanity())
		{
			studentSaveData.studentSanity.Add(num32, StudentGlobals.GetStudentSanity(num32));
		}
		return studentSaveData;
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x00142D14 File Offset: 0x00141114
	public static void WriteToGlobals(StudentSaveData data)
	{
		StudentGlobals.CustomSuitor = data.customSuitor;
		StudentGlobals.CustomSuitorAccessory = data.customSuitorAccessory;
		StudentGlobals.CustomSuitorBlonde = data.customSuitorBlonde;
		StudentGlobals.CustomSuitorEyewear = data.customSuitorEyewear;
		StudentGlobals.CustomSuitorHair = data.customSuitorHair;
		StudentGlobals.CustomSuitorJewelry = data.customSuitorJewelry;
		StudentGlobals.CustomSuitorTan = data.customSuitorTan;
		StudentGlobals.ExpelProgress = data.expelProgress;
		StudentGlobals.FemaleUniform = data.femaleUniform;
		StudentGlobals.MaleUniform = data.maleUniform;
		foreach (KeyValuePair<int, string> keyValuePair in data.studentAccessory)
		{
			StudentGlobals.SetStudentAccessory(keyValuePair.Key, keyValuePair.Value);
		}
		foreach (int studentID in data.studentArrested)
		{
			StudentGlobals.SetStudentArrested(studentID, true);
		}
		foreach (int studentID2 in data.studentBroken)
		{
			StudentGlobals.SetStudentBroken(studentID2, true);
		}
		foreach (KeyValuePair<int, float> keyValuePair2 in data.studentBustSize)
		{
			StudentGlobals.SetStudentBustSize(keyValuePair2.Key, keyValuePair2.Value);
		}
		foreach (KeyValuePair<int, Color> keyValuePair3 in data.studentColor)
		{
			StudentGlobals.SetStudentColor(keyValuePair3.Key, keyValuePair3.Value);
		}
		foreach (int studentID3 in data.studentDead)
		{
			StudentGlobals.SetStudentDead(studentID3, true);
		}
		foreach (int studentID4 in data.studentDying)
		{
			StudentGlobals.SetStudentDying(studentID4, true);
		}
		foreach (int studentID5 in data.studentExpelled)
		{
			StudentGlobals.SetStudentExpelled(studentID5, true);
		}
		foreach (int studentID6 in data.studentExposed)
		{
			StudentGlobals.SetStudentExposed(studentID6, true);
		}
		foreach (KeyValuePair<int, Color> keyValuePair4 in data.studentEyeColor)
		{
			StudentGlobals.SetStudentEyeColor(keyValuePair4.Key, keyValuePair4.Value);
		}
		foreach (int studentID7 in data.studentGrudge)
		{
			StudentGlobals.SetStudentGrudge(studentID7, true);
		}
		foreach (KeyValuePair<int, string> keyValuePair5 in data.studentHairstyle)
		{
			StudentGlobals.SetStudentHairstyle(keyValuePair5.Key, keyValuePair5.Value);
		}
		foreach (int studentID8 in data.studentKidnapped)
		{
			StudentGlobals.SetStudentKidnapped(studentID8, true);
		}
		foreach (int studentID9 in data.studentMissing)
		{
			StudentGlobals.SetStudentMissing(studentID9, true);
		}
		foreach (KeyValuePair<int, string> keyValuePair6 in data.studentName)
		{
			StudentGlobals.SetStudentName(keyValuePair6.Key, keyValuePair6.Value);
		}
		foreach (int studentID10 in data.studentPhotographed)
		{
			StudentGlobals.SetStudentPhotographed(studentID10, true);
		}
		foreach (int studentID11 in data.studentReplaced)
		{
			StudentGlobals.SetStudentReplaced(studentID11, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair7 in data.studentReputation)
		{
			StudentGlobals.SetStudentReputation(keyValuePair7.Key, keyValuePair7.Value);
		}
		foreach (KeyValuePair<int, float> keyValuePair8 in data.studentSanity)
		{
			StudentGlobals.SetStudentSanity(keyValuePair8.Key, keyValuePair8.Value);
		}
	}

	// Token: 0x04002B02 RID: 11010
	public bool customSuitor;

	// Token: 0x04002B03 RID: 11011
	public int customSuitorAccessory;

	// Token: 0x04002B04 RID: 11012
	public int customSuitorBlonde;

	// Token: 0x04002B05 RID: 11013
	public int customSuitorEyewear;

	// Token: 0x04002B06 RID: 11014
	public int customSuitorHair;

	// Token: 0x04002B07 RID: 11015
	public int customSuitorJewelry;

	// Token: 0x04002B08 RID: 11016
	public bool customSuitorTan;

	// Token: 0x04002B09 RID: 11017
	public int expelProgress;

	// Token: 0x04002B0A RID: 11018
	public int femaleUniform;

	// Token: 0x04002B0B RID: 11019
	public int maleUniform;

	// Token: 0x04002B0C RID: 11020
	public IntAndStringDictionary studentAccessory = new IntAndStringDictionary();

	// Token: 0x04002B0D RID: 11021
	public IntHashSet studentArrested = new IntHashSet();

	// Token: 0x04002B0E RID: 11022
	public IntHashSet studentBroken = new IntHashSet();

	// Token: 0x04002B0F RID: 11023
	public IntAndFloatDictionary studentBustSize = new IntAndFloatDictionary();

	// Token: 0x04002B10 RID: 11024
	public IntAndColorDictionary studentColor = new IntAndColorDictionary();

	// Token: 0x04002B11 RID: 11025
	public IntHashSet studentDead = new IntHashSet();

	// Token: 0x04002B12 RID: 11026
	public IntHashSet studentDying = new IntHashSet();

	// Token: 0x04002B13 RID: 11027
	public IntHashSet studentExpelled = new IntHashSet();

	// Token: 0x04002B14 RID: 11028
	public IntHashSet studentExposed = new IntHashSet();

	// Token: 0x04002B15 RID: 11029
	public IntAndColorDictionary studentEyeColor = new IntAndColorDictionary();

	// Token: 0x04002B16 RID: 11030
	public IntHashSet studentGrudge = new IntHashSet();

	// Token: 0x04002B17 RID: 11031
	public IntAndStringDictionary studentHairstyle = new IntAndStringDictionary();

	// Token: 0x04002B18 RID: 11032
	public IntHashSet studentKidnapped = new IntHashSet();

	// Token: 0x04002B19 RID: 11033
	public IntHashSet studentMissing = new IntHashSet();

	// Token: 0x04002B1A RID: 11034
	public IntAndStringDictionary studentName = new IntAndStringDictionary();

	// Token: 0x04002B1B RID: 11035
	public IntHashSet studentPhotographed = new IntHashSet();

	// Token: 0x04002B1C RID: 11036
	public IntHashSet studentReplaced = new IntHashSet();

	// Token: 0x04002B1D RID: 11037
	public IntAndIntDictionary studentReputation = new IntAndIntDictionary();

	// Token: 0x04002B1E RID: 11038
	public IntAndFloatDictionary studentSanity = new IntAndFloatDictionary();

	// Token: 0x04002B1F RID: 11039
	public IntHashSet studentSlave = new IntHashSet();
}
