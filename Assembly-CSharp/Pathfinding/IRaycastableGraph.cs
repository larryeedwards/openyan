using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200001A RID: 26
	public interface IRaycastableGraph
	{
		// Token: 0x0600012F RID: 303
		bool Linecast(Vector3 start, Vector3 end);

		// Token: 0x06000130 RID: 304
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint);

		// Token: 0x06000131 RID: 305
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit);

		// Token: 0x06000132 RID: 306
		bool Linecast(Vector3 start, Vector3 end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace);
	}
}
