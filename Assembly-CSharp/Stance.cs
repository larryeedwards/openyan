using System;
using UnityEngine;

// Token: 0x020003BE RID: 958
[Serializable]
public class Stance
{
	// Token: 0x0600195D RID: 6493 RVA: 0x000EC915 File Offset: 0x000EAD15
	public Stance(StanceType initialStance)
	{
		this.current = initialStance;
		this.previous = initialStance;
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x0600195E RID: 6494 RVA: 0x000EC92B File Offset: 0x000EAD2B
	// (set) Token: 0x0600195F RID: 6495 RVA: 0x000EC933 File Offset: 0x000EAD33
	public StanceType Current
	{
		get
		{
			return this.current;
		}
		set
		{
			this.previous = this.current;
			this.current = value;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001960 RID: 6496 RVA: 0x000EC948 File Offset: 0x000EAD48
	public StanceType Previous
	{
		get
		{
			return this.previous;
		}
	}

	// Token: 0x04001E00 RID: 7680
	[SerializeField]
	private StanceType current;

	// Token: 0x04001E01 RID: 7681
	[SerializeField]
	private StanceType previous;
}
