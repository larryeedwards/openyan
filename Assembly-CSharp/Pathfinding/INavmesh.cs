using System;

namespace Pathfinding
{
	// Token: 0x020000AA RID: 170
	public interface INavmesh
	{
		// Token: 0x060006EC RID: 1772
		void GetNodes(Action<GraphNode> del);
	}
}
