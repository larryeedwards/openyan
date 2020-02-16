using System;

namespace Pathfinding
{
	// Token: 0x0200005A RID: 90
	public interface IWorkItemContext
	{
		// Token: 0x060003C1 RID: 961
		void QueueFloodFill();

		// Token: 0x060003C2 RID: 962
		void EnsureValidFloodFill();
	}
}
