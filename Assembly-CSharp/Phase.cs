using System;
using UnityEngine;

// Token: 0x0200048A RID: 1162
[Serializable]
public class Phase
{
	// Token: 0x06001E37 RID: 7735 RVA: 0x00125C2F File Offset: 0x0012402F
	public Phase(PhaseOfDay type)
	{
		this.type = type;
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x06001E38 RID: 7736 RVA: 0x00125C3E File Offset: 0x0012403E
	public PhaseOfDay Type
	{
		get
		{
			return this.type;
		}
	}

	// Token: 0x040026D1 RID: 9937
	[SerializeField]
	private PhaseOfDay type;
}
