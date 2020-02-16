using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000442 RID: 1090
[Serializable]
public class CreditJson : JsonData
{
	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00111463 File Offset: 0x0010F863
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Credits.json");
		}
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x00111474 File Offset: 0x0010F874
	public static CreditJson[] LoadFromJson(string path)
	{
		List<CreditJson> list = new List<CreditJson>();
		foreach (Dictionary<string, object> dictionary in JsonData.Deserialize(path))
		{
			list.Add(new CreditJson
			{
				name = TFUtils.LoadString(dictionary, "Name"),
				size = TFUtils.LoadInt(dictionary, "Size")
			});
		}
		return list.ToArray();
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x06001D3D RID: 7485 RVA: 0x001114DF File Offset: 0x0010F8DF
	public string Name
	{
		get
		{
			return this.name;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x06001D3E RID: 7486 RVA: 0x001114E7 File Offset: 0x0010F8E7
	public int Size
	{
		get
		{
			return this.size;
		}
	}

	// Token: 0x040023F4 RID: 9204
	[SerializeField]
	private string name;

	// Token: 0x040023F5 RID: 9205
	[SerializeField]
	private int size;
}
