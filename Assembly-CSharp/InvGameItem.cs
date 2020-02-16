using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200018D RID: 397
[Serializable]
public class InvGameItem
{
	// Token: 0x06000C4A RID: 3146 RVA: 0x00066C9E File Offset: 0x0006509E
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x00066CBB File Offset: 0x000650BB
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00066CDF File Offset: 0x000650DF
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000C4D RID: 3149 RVA: 0x00066CE7 File Offset: 0x000650E7
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00066D0B File Offset: 0x0006510B
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00066D40 File Offset: 0x00065140
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00066E3C File Offset: 0x0006523C
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959u);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935u);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783u);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231u);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111u);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935u);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519u);
				break;
			}
			return result;
		}
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x00066F7C File Offset: 0x0006537C
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			List<InvStat> list2 = list;
			if (InvGameItem.<>f__mg$cache0 == null)
			{
				InvGameItem.<>f__mg$cache0 = new Comparison<InvStat>(InvStat.CompareArmor);
			}
			list2.Sort(InvGameItem.<>f__mg$cache0);
		}
		return list;
	}

	// Token: 0x04000AD4 RID: 2772
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x04000AD5 RID: 2773
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x04000AD6 RID: 2774
	public int itemLevel = 1;

	// Token: 0x04000AD7 RID: 2775
	private InvBaseItem mBaseItem;

	// Token: 0x04000AD8 RID: 2776
	[CompilerGenerated]
	private static Comparison<InvStat> <>f__mg$cache0;

	// Token: 0x0200018E RID: 398
	public enum Quality
	{
		// Token: 0x04000ADA RID: 2778
		Broken,
		// Token: 0x04000ADB RID: 2779
		Cursed,
		// Token: 0x04000ADC RID: 2780
		Damaged,
		// Token: 0x04000ADD RID: 2781
		Worn,
		// Token: 0x04000ADE RID: 2782
		Sturdy,
		// Token: 0x04000ADF RID: 2783
		Polished,
		// Token: 0x04000AE0 RID: 2784
		Improved,
		// Token: 0x04000AE1 RID: 2785
		Crafted,
		// Token: 0x04000AE2 RID: 2786
		Superior,
		// Token: 0x04000AE3 RID: 2787
		Enchanted,
		// Token: 0x04000AE4 RID: 2788
		Epic,
		// Token: 0x04000AE5 RID: 2789
		Legendary,
		// Token: 0x04000AE6 RID: 2790
		_LastDoNotUse
	}
}
