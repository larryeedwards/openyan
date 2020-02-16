using System;
using System.Collections.Generic;

// Token: 0x020004EF RID: 1263
[Serializable]
public class TaskSaveData
{
	// Token: 0x06001F9B RID: 8091 RVA: 0x001433E4 File Offset: 0x001417E4
	public static TaskSaveData ReadFromGlobals()
	{
		TaskSaveData taskSaveData = new TaskSaveData();
		foreach (int num in TaskGlobals.KeysOfGuitarPhoto())
		{
			if (TaskGlobals.GetGuitarPhoto(num))
			{
				taskSaveData.guitarPhoto.Add(num);
			}
		}
		foreach (int num2 in TaskGlobals.KeysOfKittenPhoto())
		{
			if (TaskGlobals.GetKittenPhoto(num2))
			{
				taskSaveData.kittenPhoto.Add(num2);
			}
		}
		foreach (int num3 in TaskGlobals.KeysOfHorudaPhoto())
		{
			if (TaskGlobals.GetHorudaPhoto(num3))
			{
				taskSaveData.horudaPhoto.Add(num3);
			}
		}
		foreach (int num4 in TaskGlobals.KeysOfTaskStatus())
		{
			taskSaveData.taskStatus.Add(num4, TaskGlobals.GetTaskStatus(num4));
		}
		return taskSaveData;
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x001434EC File Offset: 0x001418EC
	public static void WriteToGlobals(TaskSaveData data)
	{
		foreach (int photoID in data.kittenPhoto)
		{
			TaskGlobals.SetKittenPhoto(photoID, true);
		}
		foreach (int photoID2 in data.guitarPhoto)
		{
			TaskGlobals.SetGuitarPhoto(photoID2, true);
		}
		foreach (KeyValuePair<int, int> keyValuePair in data.taskStatus)
		{
			TaskGlobals.SetTaskStatus(keyValuePair.Key, keyValuePair.Value);
		}
	}

	// Token: 0x04002B20 RID: 11040
	public IntHashSet guitarPhoto = new IntHashSet();

	// Token: 0x04002B21 RID: 11041
	public IntHashSet kittenPhoto = new IntHashSet();

	// Token: 0x04002B22 RID: 11042
	public IntHashSet horudaPhoto = new IntHashSet();

	// Token: 0x04002B23 RID: 11043
	public IntAndIntDictionary taskStatus = new IntAndIntDictionary();
}
