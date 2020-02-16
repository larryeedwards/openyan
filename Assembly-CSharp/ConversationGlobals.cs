using System;
using System.Collections.Generic;

// Token: 0x020003F0 RID: 1008
public static class ConversationGlobals
{
	// Token: 0x06001A53 RID: 6739 RVA: 0x000F5E17 File Offset: 0x000F4217
	public static bool GetTopicDiscovered(int topicID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscovered_",
			topicID.ToString()
		}));
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x000F5E58 File Offset: 0x000F4258
	public static void SetTopicDiscovered(int topicID, bool value)
	{
		string text = topicID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicDiscovered_", text);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicDiscovered_",
			text
		}), value);
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x000F5EC4 File Offset: 0x000F42C4
	public static int[] KeysOfTopicDiscovered()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile + "_TopicDiscovered_");
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x000F5EE4 File Offset: 0x000F42E4
	public static bool GetTopicLearnedByStudent(int topicID, int studentID)
	{
		return GlobalsHelper.GetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicLearnedByStudent_",
			topicID.ToString(),
			'_',
			studentID.ToString()
		}));
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x000F5F48 File Offset: 0x000F4348
	public static void SetTopicLearnedByStudent(int topicID, int studentID, bool value)
	{
		string text = topicID.ToString();
		string text2 = studentID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_", text + '^' + text2);
		GlobalsHelper.SetBool(string.Concat(new object[]
		{
			"Profile_",
			GameGlobals.Profile,
			"_TopicLearnedByStudent_",
			text,
			'_',
			text2
		}), value);
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x000F5FE0 File Offset: 0x000F43E0
	public static IntAndIntPair[] KeysOfTopicLearnedByStudent()
	{
		KeyValuePair<int, int>[] keys = KeysHelper.GetKeys<int, int>("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_");
		IntAndIntPair[] array = new IntAndIntPair[keys.Length];
		for (int i = 0; i < keys.Length; i++)
		{
			KeyValuePair<int, int> keyValuePair = keys[i];
			array[i] = new IntAndIntPair(keyValuePair.Key, keyValuePair.Value);
		}
		return array;
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x000F6050 File Offset: 0x000F4450
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile + "_TopicDiscovered_", ConversationGlobals.KeysOfTopicDiscovered());
		foreach (IntAndIntPair intAndIntPair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			Globals.Delete(string.Concat(new object[]
			{
				"Profile_",
				GameGlobals.Profile,
				"_TopicLearnedByStudent_",
				intAndIntPair.first.ToString(),
				'_',
				intAndIntPair.second.ToString()
			}));
		}
		KeysHelper.Delete("Profile_" + GameGlobals.Profile + "_TopicLearnedByStudent_");
	}

	// Token: 0x04001F8A RID: 8074
	private const string Str_TopicDiscovered = "TopicDiscovered_";

	// Token: 0x04001F8B RID: 8075
	private const string Str_TopicLearnedByStudent = "TopicLearnedByStudent_";
}
