using System;
using System.IO;
using UnityEngine;

// Token: 0x020004F4 RID: 1268
public class SaveLoadScript : MonoBehaviour
{
	// Token: 0x06001FB5 RID: 8117 RVA: 0x00144B20 File Offset: 0x00142F20
	private void DetermineFilePath()
	{
		this.SaveProfile = GameGlobals.Profile;
		this.SaveSlot = PlayerPrefs.GetInt("SaveSlot");
		this.SaveFilePath = string.Concat(new object[]
		{
			Application.streamingAssetsPath,
			"/SaveData/Profile_",
			this.SaveProfile,
			"/Slot_",
			this.SaveSlot,
			"/Student_",
			this.Student.StudentID,
			"_Data.txt"
		});
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x00144BB0 File Offset: 0x00142FB0
	public void SaveData()
	{
		this.DetermineFilePath();
		this.SerializedData = JsonUtility.ToJson(this.Student);
		File.WriteAllText(this.SaveFilePath, this.SerializedData);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posX"
		}), base.transform.position.x);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posY"
		}), base.transform.position.y);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_posZ"
		}), base.transform.position.z);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotX"
		}), base.transform.eulerAngles.x);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotY"
		}), base.transform.eulerAngles.y);
		PlayerPrefs.SetFloat(string.Concat(new object[]
		{
			"Profile_",
			this.SaveProfile,
			"_Slot_",
			this.SaveSlot,
			"Student_",
			this.Student.StudentID,
			"_rotZ"
		}), base.transform.eulerAngles.z);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x00144E94 File Offset: 0x00143294
	public void LoadData()
	{
		this.DetermineFilePath();
		if (File.Exists(this.SaveFilePath))
		{
			base.transform.position = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posX"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posY"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"_Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_posZ"
			})));
			base.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotX"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotY"
			})), PlayerPrefs.GetFloat(string.Concat(new object[]
			{
				"Profile_",
				this.SaveProfile,
				"Slot_",
				this.SaveSlot,
				"Student_",
				this.Student.StudentID,
				"_rotZ"
			})));
			string json = File.ReadAllText(this.SaveFilePath);
			JsonUtility.FromJsonOverwrite(json, this.Student);
		}
	}

	// Token: 0x04002B51 RID: 11089
	public StudentScript Student;

	// Token: 0x04002B52 RID: 11090
	public string SerializedData;

	// Token: 0x04002B53 RID: 11091
	public string SaveFilePath;

	// Token: 0x04002B54 RID: 11092
	public int SaveProfile;

	// Token: 0x04002B55 RID: 11093
	public int SaveSlot;
}
