using System;

namespace Pathfinding
{
	// Token: 0x0200005D RID: 93
	public struct Connection
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00017C96 File Offset: 0x00016096
		public Connection(GraphNode node, uint cost, byte shapeEdge = 255)
		{
			this.node = node;
			this.cost = cost;
			this.shapeEdge = shapeEdge;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00017CAD File Offset: 0x000160AD
		public override int GetHashCode()
		{
			return this.node.GetHashCode() ^ (int)this.cost;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00017CC4 File Offset: 0x000160C4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Connection connection = (Connection)obj;
			return connection.node == this.node && connection.cost == this.cost && connection.shapeEdge == this.shapeEdge;
		}

		// Token: 0x0400024B RID: 587
		public GraphNode node;

		// Token: 0x0400024C RID: 588
		public uint cost;

		// Token: 0x0400024D RID: 589
		public byte shapeEdge;
	}
}
