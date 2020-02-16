using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000189 RID: 393
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000AB8 RID: 2744
	public int id16;

	// Token: 0x04000AB9 RID: 2745
	public string name;

	// Token: 0x04000ABA RID: 2746
	public string description;

	// Token: 0x04000ABB RID: 2747
	public InvBaseItem.Slot slot;

	// Token: 0x04000ABC RID: 2748
	public int minItemLevel = 1;

	// Token: 0x04000ABD RID: 2749
	public int maxItemLevel = 50;

	// Token: 0x04000ABE RID: 2750
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x04000ABF RID: 2751
	public GameObject attachment;

	// Token: 0x04000AC0 RID: 2752
	public Color color = Color.white;

	// Token: 0x04000AC1 RID: 2753
	public UIAtlas iconAtlas;

	// Token: 0x04000AC2 RID: 2754
	public string iconName = string.Empty;

	// Token: 0x0200018A RID: 394
	public enum Slot
	{
		// Token: 0x04000AC4 RID: 2756
		None,
		// Token: 0x04000AC5 RID: 2757
		Weapon,
		// Token: 0x04000AC6 RID: 2758
		Shield,
		// Token: 0x04000AC7 RID: 2759
		Body,
		// Token: 0x04000AC8 RID: 2760
		Shoulders,
		// Token: 0x04000AC9 RID: 2761
		Bracers,
		// Token: 0x04000ACA RID: 2762
		Boots,
		// Token: 0x04000ACB RID: 2763
		Trinket,
		// Token: 0x04000ACC RID: 2764
		_LastDoNotUse
	}
}
