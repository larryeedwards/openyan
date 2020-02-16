using System;
using UnityEngine;

// Token: 0x020003B9 RID: 953
[Serializable]
public abstract class Entity
{
	// Token: 0x06001953 RID: 6483 RVA: 0x000EC7BE File Offset: 0x000EABBE
	public Entity(GenderType gender)
	{
		this.gender = gender;
		this.deathType = DeathType.None;
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x06001954 RID: 6484 RVA: 0x000EC7D4 File Offset: 0x000EABD4
	public GenderType Gender
	{
		get
		{
			return this.gender;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x06001955 RID: 6485 RVA: 0x000EC7DC File Offset: 0x000EABDC
	// (set) Token: 0x06001956 RID: 6486 RVA: 0x000EC7E4 File Offset: 0x000EABE4
	public DeathType DeathType
	{
		get
		{
			return this.deathType;
		}
		set
		{
			this.deathType = value;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001957 RID: 6487
	public abstract EntityType EntityType { get; }

	// Token: 0x04001DE4 RID: 7652
	[SerializeField]
	private GenderType gender;

	// Token: 0x04001DE5 RID: 7653
	[SerializeField]
	private DeathType deathType;
}
