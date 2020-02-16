using System;

// Token: 0x020003F3 RID: 1011
public static class EventGlobals
{
	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000F68DC File Offset: 0x000F4CDC
	// (set) Token: 0x06001A7C RID: 6780 RVA: 0x000F68FC File Offset: 0x000F4CFC
	public static bool BefriendConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_BefriendConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_BefriendConversation", value);
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001A7D RID: 6781 RVA: 0x000F691D File Offset: 0x000F4D1D
	// (set) Token: 0x06001A7E RID: 6782 RVA: 0x000F693D File Offset: 0x000F4D3D
	public static bool OsanaEvent1
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent1");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent1", value);
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000F695E File Offset: 0x000F4D5E
	// (set) Token: 0x06001A80 RID: 6784 RVA: 0x000F697E File Offset: 0x000F4D7E
	public static bool OsanaEvent2
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent2");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_OsanaEvent2", value);
		}
	}

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001A81 RID: 6785 RVA: 0x000F699F File Offset: 0x000F4D9F
	// (set) Token: 0x06001A82 RID: 6786 RVA: 0x000F69BF File Offset: 0x000F4DBF
	public static bool Event1
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Event1");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Event1", value);
		}
	}

	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000F69E0 File Offset: 0x000F4DE0
	// (set) Token: 0x06001A84 RID: 6788 RVA: 0x000F6A00 File Offset: 0x000F4E00
	public static bool Event2
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_Event2");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_Event2", value);
		}
	}

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06001A85 RID: 6789 RVA: 0x000F6A21 File Offset: 0x000F4E21
	// (set) Token: 0x06001A86 RID: 6790 RVA: 0x000F6A41 File Offset: 0x000F4E41
	public static bool KidnapConversation
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_KidnapConversation");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_KidnapConversation", value);
		}
	}

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000F6A62 File Offset: 0x000F4E62
	// (set) Token: 0x06001A88 RID: 6792 RVA: 0x000F6A82 File Offset: 0x000F4E82
	public static bool LivingRoom
	{
		get
		{
			return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile + "_LivingRoom");
		}
		set
		{
			GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile + "_LivingRoom", value);
		}
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000F6AA4 File Offset: 0x000F4EA4
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile + "_BefriendConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaEvent1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_OsanaEvent2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Event1");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_Event2");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_KidnapConversation");
		Globals.Delete("Profile_" + GameGlobals.Profile + "_LivingRoom");
	}

	// Token: 0x04001F99 RID: 8089
	private const string Str_BefriendConversation = "BefriendConversation";

	// Token: 0x04001F9A RID: 8090
	private const string Str_Event1 = "Event1";

	// Token: 0x04001F9B RID: 8091
	private const string Str_Event2 = "Event2";

	// Token: 0x04001F9C RID: 8092
	private const string Str_OsanaEvent1 = "OsanaEvent1";

	// Token: 0x04001F9D RID: 8093
	private const string Str_OsanaEvent2 = "OsanaEvent2";

	// Token: 0x04001F9E RID: 8094
	private const string Str_KidnapConversation = "KidnapConversation";

	// Token: 0x04001F9F RID: 8095
	private const string Str_LivingRoom = "LivingRoom";
}
