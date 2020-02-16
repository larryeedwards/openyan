using System;
using UnityEngine;

// Token: 0x020003F9 RID: 1017
public static class PoseModeGlobals
{
	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06001B32 RID: 6962 RVA: 0x000F8A55 File Offset: 0x000F6E55
	// (set) Token: 0x06001B33 RID: 6963 RVA: 0x000F8A75 File Offset: 0x000F6E75
	public static Vector3 PosePosition
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PosePosition");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PosePosition", value);
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000F8A96 File Offset: 0x000F6E96
	// (set) Token: 0x06001B35 RID: 6965 RVA: 0x000F8AB6 File Offset: 0x000F6EB6
	public static Vector3 PoseRotation
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PoseRotation");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PoseRotation", value);
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000F8AD7 File Offset: 0x000F6ED7
	// (set) Token: 0x06001B37 RID: 6967 RVA: 0x000F8AF7 File Offset: 0x000F6EF7
	public static Vector3 PoseScale
	{
		get
		{
			return GlobalsHelper.GetVector3("Profile_" + GameGlobals.Profile + "_PoseScale");
		}
		set
		{
			GlobalsHelper.SetVector3("Profile_" + GameGlobals.Profile + "_PoseScale", value);
		}
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x000F8B18 File Offset: 0x000F6F18
	public static void DeleteAll()
	{
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PosePosition");
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PoseRotation");
		GlobalsHelper.DeleteVector3("Profile_" + GameGlobals.Profile + "_PoseScale");
	}

	// Token: 0x04001FED RID: 8173
	private const string Str_PosePosition = "PosePosition";

	// Token: 0x04001FEE RID: 8174
	private const string Str_PoseRotation = "PoseRotation";

	// Token: 0x04001FEF RID: 8175
	private const string Str_PoseScale = "PoseScale";
}
