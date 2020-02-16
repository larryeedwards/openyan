using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000014 RID: 20
	public struct NNInfoInternal
	{
		// Token: 0x0600011C RID: 284 RVA: 0x0000751F File Offset: 0x0000591F
		public NNInfoInternal(GraphNode node)
		{
			this.node = node;
			this.constrainedNode = null;
			this.clampedPosition = Vector3.zero;
			this.constClampedPosition = Vector3.zero;
			this.UpdateInfo();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000754C File Offset: 0x0000594C
		public void UpdateInfo()
		{
			this.clampedPosition = ((this.node == null) ? Vector3.zero : ((Vector3)this.node.position));
			this.constClampedPosition = ((this.constrainedNode == null) ? Vector3.zero : ((Vector3)this.constrainedNode.position));
		}

		// Token: 0x040000B9 RID: 185
		public GraphNode node;

		// Token: 0x040000BA RID: 186
		public GraphNode constrainedNode;

		// Token: 0x040000BB RID: 187
		public Vector3 clampedPosition;

		// Token: 0x040000BC RID: 188
		public Vector3 constClampedPosition;
	}
}
