using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
[Serializable]
public class BucketWater : BucketContents
{
	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001776 RID: 6006 RVA: 0x000B90F4 File Offset: 0x000B74F4
	// (set) Token: 0x06001777 RID: 6007 RVA: 0x000B90FC File Offset: 0x000B74FC
	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp01(value);
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001778 RID: 6008 RVA: 0x000B910A File Offset: 0x000B750A
	// (set) Token: 0x06001779 RID: 6009 RVA: 0x000B9112 File Offset: 0x000B7512
	public bool HasBleach
	{
		get
		{
			return this.hasBleach;
		}
		set
		{
			this.hasBleach = value;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x0600177A RID: 6010 RVA: 0x000B911B File Offset: 0x000B751B
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Water;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x0600177B RID: 6011 RVA: 0x000B911E File Offset: 0x000B751E
	public override bool IsCleaningAgent
	{
		get
		{
			return this.hasBleach;
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x0600177C RID: 6012 RVA: 0x000B9126 File Offset: 0x000B7526
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000B9129 File Offset: 0x000B7529
	public override bool CanBeLifted(int strength)
	{
		return true;
	}

	// Token: 0x04001736 RID: 5942
	[SerializeField]
	private float bloodiness;

	// Token: 0x04001737 RID: 5943
	[SerializeField]
	private bool hasBleach;
}
