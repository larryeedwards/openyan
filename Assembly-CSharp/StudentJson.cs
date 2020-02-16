using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000441 RID: 1089
[Serializable]
public class StudentJson : JsonData
{
	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00111062 File Offset: 0x0010F462
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Students.json");
		}
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x00111074 File Offset: 0x0010F474
	public static StudentJson[] LoadFromJson(string path)
	{
		StudentJson[] array = new StudentJson[101];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new StudentJson();
		}
		foreach (Dictionary<string, object> dictionary in JsonData.Deserialize(path))
		{
			int num = TFUtils.LoadInt(dictionary, "ID");
			if (num == 0)
			{
				break;
			}
			StudentJson studentJson = array[num];
			studentJson.name = TFUtils.LoadString(dictionary, "Name");
			studentJson.gender = TFUtils.LoadInt(dictionary, "Gender");
			studentJson.classID = TFUtils.LoadInt(dictionary, "Class");
			studentJson.seat = TFUtils.LoadInt(dictionary, "Seat");
			studentJson.club = (ClubType)TFUtils.LoadInt(dictionary, "Club");
			studentJson.persona = (PersonaType)TFUtils.LoadInt(dictionary, "Persona");
			studentJson.crush = TFUtils.LoadInt(dictionary, "Crush");
			studentJson.breastSize = TFUtils.LoadFloat(dictionary, "BreastSize");
			studentJson.strength = TFUtils.LoadInt(dictionary, "Strength");
			studentJson.hairstyle = TFUtils.LoadString(dictionary, "Hairstyle");
			studentJson.color = TFUtils.LoadString(dictionary, "Color");
			studentJson.eyes = TFUtils.LoadString(dictionary, "Eyes");
			studentJson.eyeType = TFUtils.LoadString(dictionary, "EyeType");
			studentJson.stockings = TFUtils.LoadString(dictionary, "Stockings");
			studentJson.accessory = TFUtils.LoadString(dictionary, "Accessory");
			studentJson.info = TFUtils.LoadString(dictionary, "Info");
			if (GameGlobals.LoveSick && studentJson.name == "Mai Waifu")
			{
				studentJson.name = "Mai Wakabayashi";
			}
			if (OptionGlobals.HighPopulation && studentJson.name == "Unknown")
			{
				studentJson.name = "Random";
			}
			float[] array3 = StudentJson.ConstructTempFloatArray(TFUtils.LoadString(dictionary, "ScheduleTime"));
			string[] array4 = StudentJson.ConstructTempStringArray(TFUtils.LoadString(dictionary, "ScheduleDestination"));
			string[] array5 = StudentJson.ConstructTempStringArray(TFUtils.LoadString(dictionary, "ScheduleAction"));
			studentJson.scheduleBlocks = new ScheduleBlock[array3.Length];
			for (int k = 0; k < studentJson.scheduleBlocks.Length; k++)
			{
				studentJson.scheduleBlocks[k] = new ScheduleBlock(array3[k], array4[k], array5[k]);
			}
			if (num == 10 || num == 11)
			{
				for (int l = 0; l < studentJson.scheduleBlocks.Length; l++)
				{
					studentJson.scheduleBlocks[l] = null;
				}
			}
			studentJson.success = true;
		}
		return array;
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00111326 File Offset: 0x0010F726
	// (set) Token: 0x06001D1F RID: 7455 RVA: 0x0011132E File Offset: 0x0010F72E
	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = value;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00111337 File Offset: 0x0010F737
	public int Gender
	{
		get
		{
			return this.gender;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x06001D21 RID: 7457 RVA: 0x0011133F File Offset: 0x0010F73F
	// (set) Token: 0x06001D22 RID: 7458 RVA: 0x00111347 File Offset: 0x0010F747
	public int Class
	{
		get
		{
			return this.classID;
		}
		set
		{
			this.classID = value;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00111350 File Offset: 0x0010F750
	// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00111358 File Offset: 0x0010F758
	public int Seat
	{
		get
		{
			return this.seat;
		}
		set
		{
			this.seat = value;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00111361 File Offset: 0x0010F761
	public ClubType Club
	{
		get
		{
			return this.club;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x06001D26 RID: 7462 RVA: 0x00111369 File Offset: 0x0010F769
	// (set) Token: 0x06001D27 RID: 7463 RVA: 0x00111371 File Offset: 0x0010F771
	public PersonaType Persona
	{
		get
		{
			return this.persona;
		}
		set
		{
			this.persona = value;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0011137A File Offset: 0x0010F77A
	public int Crush
	{
		get
		{
			return this.crush;
		}
	}

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00111382 File Offset: 0x0010F782
	// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0011138A File Offset: 0x0010F78A
	public float BreastSize
	{
		get
		{
			return this.breastSize;
		}
		set
		{
			this.breastSize = value;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06001D2B RID: 7467 RVA: 0x00111393 File Offset: 0x0010F793
	// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0011139B File Offset: 0x0010F79B
	public int Strength
	{
		get
		{
			return this.strength;
		}
		set
		{
			this.strength = value;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06001D2D RID: 7469 RVA: 0x001113A4 File Offset: 0x0010F7A4
	// (set) Token: 0x06001D2E RID: 7470 RVA: 0x001113AC File Offset: 0x0010F7AC
	public string Hairstyle
	{
		get
		{
			return this.hairstyle;
		}
		set
		{
			this.hairstyle = value;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06001D2F RID: 7471 RVA: 0x001113B5 File Offset: 0x0010F7B5
	public string Color
	{
		get
		{
			return this.color;
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x06001D30 RID: 7472 RVA: 0x001113BD File Offset: 0x0010F7BD
	public string Eyes
	{
		get
		{
			return this.eyes;
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x06001D31 RID: 7473 RVA: 0x001113C5 File Offset: 0x0010F7C5
	public string EyeType
	{
		get
		{
			return this.eyeType;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x06001D32 RID: 7474 RVA: 0x001113CD File Offset: 0x0010F7CD
	public string Stockings
	{
		get
		{
			return this.stockings;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x06001D33 RID: 7475 RVA: 0x001113D5 File Offset: 0x0010F7D5
	// (set) Token: 0x06001D34 RID: 7476 RVA: 0x001113DD File Offset: 0x0010F7DD
	public string Accessory
	{
		get
		{
			return this.accessory;
		}
		set
		{
			this.accessory = value;
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x06001D35 RID: 7477 RVA: 0x001113E6 File Offset: 0x0010F7E6
	public string Info
	{
		get
		{
			return this.info;
		}
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x06001D36 RID: 7478 RVA: 0x001113EE File Offset: 0x0010F7EE
	public ScheduleBlock[] ScheduleBlocks
	{
		get
		{
			return this.scheduleBlocks;
		}
	}

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x06001D37 RID: 7479 RVA: 0x001113F6 File Offset: 0x0010F7F6
	public bool Success
	{
		get
		{
			return this.success;
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x00111400 File Offset: 0x0010F800
	private static float[] ConstructTempFloatArray(string str)
	{
		string[] array = str.Split(new char[]
		{
			'_'
		});
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = float.Parse(array[i]);
		}
		return array2;
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x00111448 File Offset: 0x0010F848
	private static string[] ConstructTempStringArray(string str)
	{
		return str.Split(new char[]
		{
			'_'
		});
	}

	// Token: 0x040023E2 RID: 9186
	[SerializeField]
	private string name;

	// Token: 0x040023E3 RID: 9187
	[SerializeField]
	private int gender;

	// Token: 0x040023E4 RID: 9188
	[SerializeField]
	private int classID;

	// Token: 0x040023E5 RID: 9189
	[SerializeField]
	private int seat;

	// Token: 0x040023E6 RID: 9190
	[SerializeField]
	private ClubType club;

	// Token: 0x040023E7 RID: 9191
	[SerializeField]
	private PersonaType persona;

	// Token: 0x040023E8 RID: 9192
	[SerializeField]
	private int crush;

	// Token: 0x040023E9 RID: 9193
	[SerializeField]
	private float breastSize;

	// Token: 0x040023EA RID: 9194
	[SerializeField]
	private int strength;

	// Token: 0x040023EB RID: 9195
	[SerializeField]
	private string hairstyle;

	// Token: 0x040023EC RID: 9196
	[SerializeField]
	private string color;

	// Token: 0x040023ED RID: 9197
	[SerializeField]
	private string eyes;

	// Token: 0x040023EE RID: 9198
	[SerializeField]
	private string eyeType;

	// Token: 0x040023EF RID: 9199
	[SerializeField]
	private string stockings;

	// Token: 0x040023F0 RID: 9200
	[SerializeField]
	private string accessory;

	// Token: 0x040023F1 RID: 9201
	[SerializeField]
	private string info;

	// Token: 0x040023F2 RID: 9202
	[SerializeField]
	private ScheduleBlock[] scheduleBlocks;

	// Token: 0x040023F3 RID: 9203
	[SerializeField]
	private bool success;
}
