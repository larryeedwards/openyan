using System;

namespace Pathfinding
{
	// Token: 0x02000060 RID: 96
	public interface ITraversalProvider
	{
		// Token: 0x06000411 RID: 1041
		bool CanTraverse(Path path, GraphNode node);

		// Token: 0x06000412 RID: 1042
		uint GetTraversalCost(Path path, GraphNode node);
	}
}
