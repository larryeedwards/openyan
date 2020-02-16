using System;

// Token: 0x0200018F RID: 399
[Serializable]
public class InvStat
{
	// Token: 0x06000C53 RID: 3155 RVA: 0x000670BF File Offset: 0x000654BF
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x000670D0 File Offset: 0x000654D0
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00067144 File Offset: 0x00065544
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00067218 File Offset: 0x00065618
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x04000AE7 RID: 2791
	public InvStat.Identifier id;

	// Token: 0x04000AE8 RID: 2792
	public InvStat.Modifier modifier;

	// Token: 0x04000AE9 RID: 2793
	public int amount;

	// Token: 0x02000190 RID: 400
	public enum Identifier
	{
		// Token: 0x04000AEB RID: 2795
		Strength,
		// Token: 0x04000AEC RID: 2796
		Constitution,
		// Token: 0x04000AED RID: 2797
		Agility,
		// Token: 0x04000AEE RID: 2798
		Intelligence,
		// Token: 0x04000AEF RID: 2799
		Damage,
		// Token: 0x04000AF0 RID: 2800
		Crit,
		// Token: 0x04000AF1 RID: 2801
		Armor,
		// Token: 0x04000AF2 RID: 2802
		Health,
		// Token: 0x04000AF3 RID: 2803
		Mana,
		// Token: 0x04000AF4 RID: 2804
		Other
	}

	// Token: 0x02000191 RID: 401
	public enum Modifier
	{
		// Token: 0x04000AF6 RID: 2806
		Added,
		// Token: 0x04000AF7 RID: 2807
		Percent
	}
}
