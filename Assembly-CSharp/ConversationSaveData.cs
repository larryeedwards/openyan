using System;

// Token: 0x020004E0 RID: 1248
[Serializable]
public class ConversationSaveData
{
	// Token: 0x06001F6E RID: 8046 RVA: 0x0014114C File Offset: 0x0013F54C
	public static ConversationSaveData ReadFromGlobals()
	{
		ConversationSaveData conversationSaveData = new ConversationSaveData();
		foreach (int num in ConversationGlobals.KeysOfTopicDiscovered())
		{
			if (ConversationGlobals.GetTopicDiscovered(num))
			{
				conversationSaveData.topicDiscovered.Add(num);
			}
		}
		foreach (IntAndIntPair intAndIntPair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			if (ConversationGlobals.GetTopicLearnedByStudent(intAndIntPair.first, intAndIntPair.second))
			{
				conversationSaveData.topicLearnedByStudent.Add(intAndIntPair);
			}
		}
		return conversationSaveData;
	}

	// Token: 0x06001F6F RID: 8047 RVA: 0x001411E4 File Offset: 0x0013F5E4
	public static void WriteToGlobals(ConversationSaveData data)
	{
		foreach (int topicID in data.topicDiscovered)
		{
			ConversationGlobals.SetTopicDiscovered(topicID, true);
		}
		foreach (IntAndIntPair intAndIntPair in data.topicLearnedByStudent)
		{
			ConversationGlobals.SetTopicLearnedByStudent(intAndIntPair.first, intAndIntPair.second, true);
		}
	}

	// Token: 0x04002AA5 RID: 10917
	public IntHashSet topicDiscovered = new IntHashSet();

	// Token: 0x04002AA6 RID: 10918
	public IntAndIntPairHashSet topicLearnedByStudent = new IntAndIntPairHashSet();
}
