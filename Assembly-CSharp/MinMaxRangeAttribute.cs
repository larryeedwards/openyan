using System;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class MinMaxRangeAttribute : PropertyAttribute
{
	// Token: 0x06000F29 RID: 3881 RVA: 0x00079348 File Offset: 0x00077748
	public MinMaxRangeAttribute(float minLimit, float maxLimit)
	{
		this.minLimit = minLimit;
		this.maxLimit = maxLimit;
	}

	// Token: 0x04000DB5 RID: 3509
	public float minLimit;

	// Token: 0x04000DB6 RID: 3510
	public float maxLimit;
}
