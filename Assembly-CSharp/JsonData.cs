using System;
using System.Collections.Generic;
using System.IO;
using JsonFx.Json;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public abstract class JsonData
{
	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06001D19 RID: 7449 RVA: 0x0011102E File Offset: 0x0010F42E
	protected static string FolderPath
	{
		get
		{
			return Path.Combine(Application.streamingAssetsPath, "JSON");
		}
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x00111040 File Offset: 0x0010F440
	protected static Dictionary<string, object>[] Deserialize(string filename)
	{
		string value = File.ReadAllText(filename);
		return JsonReader.Deserialize<Dictionary<string, object>[]>(value);
	}
}
