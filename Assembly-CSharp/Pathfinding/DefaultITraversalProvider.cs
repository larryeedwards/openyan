using System;

namespace Pathfinding
{
	// Token: 0x02000061 RID: 97
	public static class DefaultITraversalProvider
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x000182A8 File Offset: 0x000166A8
		public static bool CanTraverse(Path path, GraphNode node)
		{
			return node.Walkable && (path.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000182D0 File Offset: 0x000166D0
		public static uint GetTraversalCost(Path path, GraphNode node)
		{
			return path.GetTagPenalty((int)node.Tag) + node.Penalty;
		}
	}
}
