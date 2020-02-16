using System;
using UnityEngine;

// Token: 0x020003BC RID: 956
[Serializable]
public class RivalData
{
	// Token: 0x0600195B RID: 6491 RVA: 0x000EC8FE File Offset: 0x000EACFE
	public RivalData(int week)
	{
		this.week = week;
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x0600195C RID: 6492 RVA: 0x000EC90D File Offset: 0x000EAD0D
	public int Week
	{
		get
		{
			return this.week;
		}
	}

	// Token: 0x04001DFB RID: 7675
	[SerializeField]
	private int week;
}
