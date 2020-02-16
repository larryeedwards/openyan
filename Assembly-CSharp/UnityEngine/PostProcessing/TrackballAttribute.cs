using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200029E RID: 670
	public sealed class TrackballAttribute : PropertyAttribute
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x000A639A File Offset: 0x000A479A
		public TrackballAttribute(string method)
		{
			this.method = method;
		}

		// Token: 0x04001255 RID: 4693
		public readonly string method;
	}
}
