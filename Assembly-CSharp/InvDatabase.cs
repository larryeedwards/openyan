using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018B RID: 395
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Item Database")]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0006683F File Offset: 0x00064C3F
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x06000C39 RID: 3129 RVA: 0x00066860 File Offset: 0x00064C60
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00066868 File Offset: 0x00064C68
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000C3B RID: 3131 RVA: 0x00066870 File Offset: 0x00064C70
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x000668B8 File Offset: 0x00064CB8
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x000668F8 File Offset: 0x00064CF8
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		return (!(database != null)) ? null : database.GetItem(id32 & 65535);
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00066930 File Offset: 0x00064D30
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x000669A4 File Offset: 0x00064DA4
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return invDatabase.databaseID << 16 | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x04000ACD RID: 2765
	private static InvDatabase[] mList;

	// Token: 0x04000ACE RID: 2766
	private static bool mIsDirty = true;

	// Token: 0x04000ACF RID: 2767
	public int databaseID;

	// Token: 0x04000AD0 RID: 2768
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x04000AD1 RID: 2769
	public UIAtlas iconAtlas;
}
