using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000443 RID: 1091
[Serializable]
public class TopicJson : JsonData
{
	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x06001D40 RID: 7488 RVA: 0x001114F7 File Offset: 0x0010F8F7
	public static string FilePath
	{
		get
		{
			return Path.Combine(JsonData.FolderPath, "Topics.json");
		}
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00111508 File Offset: 0x0010F908
	public static TopicJson[] LoadFromJson(string path)
	{
		TopicJson[] array = new TopicJson[101];
		foreach (Dictionary<string, object> d in JsonData.Deserialize(path))
		{
			int num = TFUtils.LoadInt(d, "ID");
			if (num == 0)
			{
				break;
			}
			array[num] = new TopicJson();
			TopicJson topicJson = array[num];
			topicJson.topics = new int[26];
			for (int j = 1; j <= 25; j++)
			{
				topicJson.topics[j] = TFUtils.LoadInt(d, j.ToString());
			}
		}
		return array;
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x06001D42 RID: 7490 RVA: 0x001115A7 File Offset: 0x0010F9A7
	public int[] Topics
	{
		get
		{
			return this.topics;
		}
	}

	// Token: 0x040023F6 RID: 9206
	[SerializeField]
	private int[] topics;
}
