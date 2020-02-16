using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000C RID: 12
	public class RichSpecial : RichPathPart
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00006A0F File Offset: 0x00004E0F
		public override void OnEnterPool()
		{
			this.nodeLink = null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006A18 File Offset: 0x00004E18
		public RichSpecial Initialize(NodeLink2 nodeLink, GraphNode first)
		{
			this.nodeLink = nodeLink;
			if (first == nodeLink.startNode)
			{
				this.first = nodeLink.StartTransform;
				this.second = nodeLink.EndTransform;
				this.reverse = false;
			}
			else
			{
				this.first = nodeLink.EndTransform;
				this.second = nodeLink.StartTransform;
				this.reverse = true;
			}
			return this;
		}

		// Token: 0x0400007E RID: 126
		public NodeLink2 nodeLink;

		// Token: 0x0400007F RID: 127
		public Transform first;

		// Token: 0x04000080 RID: 128
		public Transform second;

		// Token: 0x04000081 RID: 129
		public bool reverse;
	}
}
