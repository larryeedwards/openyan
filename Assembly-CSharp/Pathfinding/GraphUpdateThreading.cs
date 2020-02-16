using System;

namespace Pathfinding
{
	// Token: 0x02000020 RID: 32
	public enum GraphUpdateThreading
	{
		// Token: 0x040000D7 RID: 215
		UnityThread,
		// Token: 0x040000D8 RID: 216
		SeparateThread,
		// Token: 0x040000D9 RID: 217
		UnityInit,
		// Token: 0x040000DA RID: 218
		UnityPost = 4,
		// Token: 0x040000DB RID: 219
		SeparateAndUnityInit = 3
	}
}
