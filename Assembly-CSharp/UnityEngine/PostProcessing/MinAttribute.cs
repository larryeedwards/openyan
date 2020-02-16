using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200029D RID: 669
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06001564 RID: 5476 RVA: 0x000A638B File Offset: 0x000A478B
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04001254 RID: 4692
		public readonly float min;
	}
}
