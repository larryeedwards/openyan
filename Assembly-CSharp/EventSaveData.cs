using System;

// Token: 0x020004E3 RID: 1251
[Serializable]
public class EventSaveData
{
	// Token: 0x06001F77 RID: 8055 RVA: 0x00141658 File Offset: 0x0013FA58
	public static EventSaveData ReadFromGlobals()
	{
		return new EventSaveData
		{
			befriendConversation = EventGlobals.BefriendConversation,
			event1 = EventGlobals.Event1,
			event2 = EventGlobals.Event2,
			kidnapConversation = EventGlobals.KidnapConversation,
			livingRoom = EventGlobals.LivingRoom
		};
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x001416A3 File Offset: 0x0013FAA3
	public static void WriteToGlobals(EventSaveData data)
	{
		EventGlobals.BefriendConversation = data.befriendConversation;
		EventGlobals.Event1 = data.event1;
		EventGlobals.Event2 = data.event2;
		EventGlobals.KidnapConversation = data.kidnapConversation;
		EventGlobals.LivingRoom = data.livingRoom;
	}

	// Token: 0x04002AB1 RID: 10929
	public bool befriendConversation;

	// Token: 0x04002AB2 RID: 10930
	public bool event1;

	// Token: 0x04002AB3 RID: 10931
	public bool event2;

	// Token: 0x04002AB4 RID: 10932
	public bool kidnapConversation;

	// Token: 0x04002AB5 RID: 10933
	public bool livingRoom;
}
