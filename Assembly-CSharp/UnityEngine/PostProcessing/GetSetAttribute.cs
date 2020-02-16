using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200029C RID: 668
	public sealed class GetSetAttribute : PropertyAttribute
	{
		// Token: 0x06001563 RID: 5475 RVA: 0x000A637C File Offset: 0x000A477C
		public GetSetAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x04001252 RID: 4690
		public readonly string name;

		// Token: 0x04001253 RID: 4691
		public bool dirty;
	}
}
