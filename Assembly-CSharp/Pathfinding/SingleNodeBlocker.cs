using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000115 RID: 277
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_single_node_blocker.php")]
	public class SingleNodeBlocker : VersionedMonoBehaviour
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0004CA90 File Offset: 0x0004AE90
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0004CA98 File Offset: 0x0004AE98
		public GraphNode lastBlocked { get; private set; }

		// Token: 0x06000A06 RID: 2566 RVA: 0x0004CAA1 File Offset: 0x0004AEA1
		public void BlockAtCurrentPosition()
		{
			this.BlockAt(base.transform.position);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0004CAB4 File Offset: 0x0004AEB4
		public void BlockAt(Vector3 position)
		{
			this.Unblock();
			GraphNode node = AstarPath.active.GetNearest(position, NNConstraint.None).node;
			if (node != null)
			{
				this.Block(node);
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0004CAED File Offset: 0x0004AEED
		public void Block(GraphNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.manager.InternalBlock(node, this);
			this.lastBlocked = node;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0004CB14 File Offset: 0x0004AF14
		public void Unblock()
		{
			if (this.lastBlocked == null || this.lastBlocked.Destroyed)
			{
				this.lastBlocked = null;
				return;
			}
			this.manager.InternalUnblock(this.lastBlocked, this);
			this.lastBlocked = null;
		}

		// Token: 0x040006EA RID: 1770
		public BlockManager manager;
	}
}
