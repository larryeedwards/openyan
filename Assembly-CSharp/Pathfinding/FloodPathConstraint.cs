using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000103 RID: 259
	public class FloodPathConstraint : NNConstraint
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x0004A4EA File Offset: 0x000488EA
		public FloodPathConstraint(FloodPath path)
		{
			if (path == null)
			{
				Debug.LogWarning("FloodPathConstraint should not be used with a NULL path");
			}
			this.path = path;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0004A509 File Offset: 0x00048909
		public override bool Suitable(GraphNode node)
		{
			return base.Suitable(node) && this.path.HasPathTo(node);
		}

		// Token: 0x04000692 RID: 1682
		private readonly FloodPath path;
	}
}
