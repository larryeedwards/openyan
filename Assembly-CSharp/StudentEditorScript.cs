using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class StudentEditorScript : MonoBehaviour
{
	// Token: 0x06001918 RID: 6424 RVA: 0x000E7B8C File Offset: 0x000E5F8C
	private void Awake()
	{
		Dictionary<string, object>[] array = EditorManagerScript.DeserializeJson("Students.json");
		this.students = new StudentEditorScript.StudentData[array.Length];
		for (int i = 0; i < this.students.Length; i++)
		{
			this.students[i] = StudentEditorScript.StudentData.Deserialize(array[i]);
		}
		Array.Sort<StudentEditorScript.StudentData>(this.students, (StudentEditorScript.StudentData a, StudentEditorScript.StudentData b) => a.id - b.id);
		for (int j = 0; j < this.students.Length; j++)
		{
			StudentEditorScript.StudentData studentData = this.students[j];
			UILabel uilabel = UnityEngine.Object.Instantiate<UILabel>(this.studentLabelTemplate, this.listLabelsOrigin);
			uilabel.text = "(" + studentData.id.ToString() + ") " + studentData.name;
			Transform transform = uilabel.transform;
			transform.localPosition = new Vector3(transform.localPosition.x + (float)(uilabel.width / 2), transform.localPosition.y - (float)(j * uilabel.height), transform.localPosition.z);
			uilabel.gameObject.SetActive(true);
		}
		this.studentIndex = 0;
		this.bodyLabel.text = StudentEditorScript.GetStudentText(this.students[this.studentIndex]);
		this.inputManager = UnityEngine.Object.FindObjectOfType<InputManagerScript>();
	}

	// Token: 0x06001919 RID: 6425 RVA: 0x000E7CFC File Offset: 0x000E60FC
	private void OnEnable()
	{
		this.promptBar.Label[0].text = string.Empty;
		this.promptBar.Label[1].text = "Back";
		this.promptBar.UpdateButtons();
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x000E7D38 File Offset: 0x000E6138
	private static ScheduleBlock[] DeserializeScheduleBlocks(Dictionary<string, object> dict)
	{
		string[] array = TFUtils.LoadString(dict, "ScheduleTime").Split(new char[]
		{
			'_'
		});
		string[] array2 = TFUtils.LoadString(dict, "ScheduleDestination").Split(new char[]
		{
			'_'
		});
		string[] array3 = TFUtils.LoadString(dict, "ScheduleAction").Split(new char[]
		{
			'_'
		});
		ScheduleBlock[] array4 = new ScheduleBlock[array.Length];
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i] = new ScheduleBlock(float.Parse(array[i]), array2[i], array3[i]);
		}
		return array4;
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x000E7DD8 File Offset: 0x000E61D8
	private static string GetStudentText(StudentEditorScript.StudentData data)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(string.Concat(new object[]
		{
			data.name,
			" (",
			data.id,
			"):\n"
		}));
		stringBuilder.Append("- Gender: " + ((!data.isMale) ? "Female" : "Male") + "\n");
		stringBuilder.Append("- Class: " + data.attendanceInfo.classNumber + "\n");
		stringBuilder.Append("- Seat: " + data.attendanceInfo.seatNumber + "\n");
		stringBuilder.Append("- Club: " + data.attendanceInfo.club + "\n");
		stringBuilder.Append("- Persona: " + data.personality.persona + "\n");
		stringBuilder.Append("- Crush: " + data.personality.crush + "\n");
		stringBuilder.Append("- Breast size: " + data.cosmetics.breastSize + "\n");
		stringBuilder.Append("- Strength: " + data.stats.strength + "\n");
		stringBuilder.Append("- Hairstyle: " + data.cosmetics.hairstyle + "\n");
		stringBuilder.Append("- Color: " + data.cosmetics.color + "\n");
		stringBuilder.Append("- Eyes: " + data.cosmetics.eyes + "\n");
		stringBuilder.Append("- Stockings: " + data.cosmetics.stockings + "\n");
		stringBuilder.Append("- Accessory: " + data.cosmetics.accessory + "\n");
		stringBuilder.Append("- Schedule blocks: ");
		foreach (ScheduleBlock scheduleBlock in data.scheduleBlocks)
		{
			stringBuilder.Append(string.Concat(new object[]
			{
				"[",
				scheduleBlock.time,
				", ",
				scheduleBlock.destination,
				", ",
				scheduleBlock.action,
				"]"
			}));
		}
		stringBuilder.Append("\n");
		stringBuilder.Append("- Info: \"" + data.info + "\"\n");
		return stringBuilder.ToString();
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000E80B0 File Offset: 0x000E64B0
	private void HandleInput()
	{
		bool buttonDown = Input.GetButtonDown("B");
		if (buttonDown)
		{
			this.mainPanel.gameObject.SetActive(true);
			this.studentPanel.gameObject.SetActive(false);
		}
		int num = 0;
		int num2 = this.students.Length - 1;
		bool tappedUp = this.inputManager.TappedUp;
		bool tappedDown = this.inputManager.TappedDown;
		if (tappedUp)
		{
			this.studentIndex = ((this.studentIndex <= num) ? num2 : (this.studentIndex - 1));
		}
		else if (tappedDown)
		{
			this.studentIndex = ((this.studentIndex >= num2) ? num : (this.studentIndex + 1));
		}
		bool flag = tappedUp || tappedDown;
		if (flag)
		{
			this.bodyLabel.text = StudentEditorScript.GetStudentText(this.students[this.studentIndex]);
		}
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x000E8199 File Offset: 0x000E6599
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x04001D1E RID: 7454
	[SerializeField]
	private UIPanel mainPanel;

	// Token: 0x04001D1F RID: 7455
	[SerializeField]
	private UIPanel studentPanel;

	// Token: 0x04001D20 RID: 7456
	[SerializeField]
	private UILabel bodyLabel;

	// Token: 0x04001D21 RID: 7457
	[SerializeField]
	private Transform listLabelsOrigin;

	// Token: 0x04001D22 RID: 7458
	[SerializeField]
	private UILabel studentLabelTemplate;

	// Token: 0x04001D23 RID: 7459
	[SerializeField]
	private PromptBarScript promptBar;

	// Token: 0x04001D24 RID: 7460
	private StudentEditorScript.StudentData[] students;

	// Token: 0x04001D25 RID: 7461
	private int studentIndex;

	// Token: 0x04001D26 RID: 7462
	private InputManagerScript inputManager;

	// Token: 0x020003A9 RID: 937
	private class StudentAttendanceInfo
	{
		// Token: 0x06001920 RID: 6432 RVA: 0x000E81B8 File Offset: 0x000E65B8
		public static StudentEditorScript.StudentAttendanceInfo Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentAttendanceInfo
			{
				classNumber = TFUtils.LoadInt(dict, "Class"),
				seatNumber = TFUtils.LoadInt(dict, "Seat"),
				club = TFUtils.LoadInt(dict, "Club")
			};
		}

		// Token: 0x04001D28 RID: 7464
		public int classNumber;

		// Token: 0x04001D29 RID: 7465
		public int seatNumber;

		// Token: 0x04001D2A RID: 7466
		public int club;
	}

	// Token: 0x020003AA RID: 938
	private class StudentPersonality
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x000E8208 File Offset: 0x000E6608
		public static StudentEditorScript.StudentPersonality Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentPersonality
			{
				persona = (PersonaType)TFUtils.LoadInt(dict, "Persona"),
				crush = TFUtils.LoadInt(dict, "Crush")
			};
		}

		// Token: 0x04001D2B RID: 7467
		public PersonaType persona;

		// Token: 0x04001D2C RID: 7468
		public int crush;
	}

	// Token: 0x020003AB RID: 939
	private class StudentStats
	{
		// Token: 0x06001924 RID: 6436 RVA: 0x000E8248 File Offset: 0x000E6648
		public static StudentEditorScript.StudentStats Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentStats
			{
				strength = TFUtils.LoadInt(dict, "Strength")
			};
		}

		// Token: 0x04001D2D RID: 7469
		public int strength;
	}

	// Token: 0x020003AC RID: 940
	private class StudentCosmetics
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x000E8278 File Offset: 0x000E6678
		public static StudentEditorScript.StudentCosmetics Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentCosmetics
			{
				breastSize = TFUtils.LoadFloat(dict, "BreastSize"),
				hairstyle = TFUtils.LoadString(dict, "Hairstyle"),
				color = TFUtils.LoadString(dict, "Color"),
				eyes = TFUtils.LoadString(dict, "Eyes"),
				stockings = TFUtils.LoadString(dict, "Stockings"),
				accessory = TFUtils.LoadString(dict, "Accessory")
			};
		}

		// Token: 0x04001D2E RID: 7470
		public float breastSize;

		// Token: 0x04001D2F RID: 7471
		public string hairstyle;

		// Token: 0x04001D30 RID: 7472
		public string color;

		// Token: 0x04001D31 RID: 7473
		public string eyes;

		// Token: 0x04001D32 RID: 7474
		public string stockings;

		// Token: 0x04001D33 RID: 7475
		public string accessory;
	}

	// Token: 0x020003AD RID: 941
	private class StudentData
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x000E82FC File Offset: 0x000E66FC
		public static StudentEditorScript.StudentData Deserialize(Dictionary<string, object> dict)
		{
			return new StudentEditorScript.StudentData
			{
				id = TFUtils.LoadInt(dict, "ID"),
				name = TFUtils.LoadString(dict, "Name"),
				isMale = (TFUtils.LoadInt(dict, "Gender") == 1),
				attendanceInfo = StudentEditorScript.StudentAttendanceInfo.Deserialize(dict),
				personality = StudentEditorScript.StudentPersonality.Deserialize(dict),
				stats = StudentEditorScript.StudentStats.Deserialize(dict),
				cosmetics = StudentEditorScript.StudentCosmetics.Deserialize(dict),
				scheduleBlocks = StudentEditorScript.DeserializeScheduleBlocks(dict),
				info = TFUtils.LoadString(dict, "Info")
			};
		}

		// Token: 0x04001D34 RID: 7476
		public int id;

		// Token: 0x04001D35 RID: 7477
		public string name;

		// Token: 0x04001D36 RID: 7478
		public bool isMale;

		// Token: 0x04001D37 RID: 7479
		public StudentEditorScript.StudentAttendanceInfo attendanceInfo;

		// Token: 0x04001D38 RID: 7480
		public StudentEditorScript.StudentPersonality personality;

		// Token: 0x04001D39 RID: 7481
		public StudentEditorScript.StudentStats stats;

		// Token: 0x04001D3A RID: 7482
		public StudentEditorScript.StudentCosmetics cosmetics;

		// Token: 0x04001D3B RID: 7483
		public ScheduleBlock[] scheduleBlocks;

		// Token: 0x04001D3C RID: 7484
		public string info;
	}
}
