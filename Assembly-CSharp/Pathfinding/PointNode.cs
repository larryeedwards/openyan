using System;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AE RID: 174
	public class PointNode : GraphNode
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x00014613 File Offset: 0x00012A13
		public PointNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001461C File Offset: 0x00012A1C
		public void SetPosition(Int3 value)
		{
			this.position = value;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00014628 File Offset: 0x00012A28
		public override void GetConnections(Action<GraphNode> action)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				action(this.connections[i].node);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00014674 File Offset: 0x00012A74
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					this.connections[i].node.RemoveConnection(this);
				}
			}
			this.connections = null;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000146CC File Offset: 0x00012ACC
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				PathNode pathNode2 = handler.GetPathNode(node);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					node.UpdateRecursiveG(path, pathNode2, handler);
				}
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00014748 File Offset: 0x00012B48
		public override bool ContainsConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return false;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00014798 File Offset: 0x00012B98
		public override void AddConnection(GraphNode node, uint cost)
		{
			if (node == null)
			{
				throw new ArgumentNullException();
			}
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node == node)
					{
						this.connections[i].cost = cost;
						return;
					}
				}
			}
			int num = (this.connections == null) ? 0 : this.connections.Length;
			Connection[] array = new Connection[num + 1];
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, byte.MaxValue);
			this.connections = array;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00014870 File Offset: 0x00012C70
		public override void RemoveConnection(GraphNode node)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					int num = this.connections.Length;
					Connection[] array = new Connection[num - 1];
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					this.connections = array;
					return;
				}
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001493C File Offset: 0x00012D3C
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (path.CanTraverse(node))
				{
					PathNode pathNode2 = handler.GetPathNode(node);
					if (pathNode2.pathID != handler.PathID)
					{
						pathNode2.parent = pathNode;
						pathNode2.pathID = handler.PathID;
						pathNode2.cost = this.connections[i].cost;
						pathNode2.H = path.CalculateHScore(node);
						pathNode2.UpdateG(path);
						handler.heap.Add(pathNode2);
					}
					else
					{
						uint cost = this.connections[i].cost;
						if (pathNode.G + cost + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = cost;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00014A38 File Offset: 0x00012E38
		public override int GetGizmoHashCode()
		{
			int num = base.GetGizmoHashCode();
			if (this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					num ^= 17 * this.connections[i].GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00014A8E File Offset: 0x00012E8E
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00014AA3 File Offset: 0x00012EA3
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00014AB8 File Offset: 0x00012EB8
		public override void SerializeReferences(GraphSerializationContext ctx)
		{
			if (this.connections == null)
			{
				ctx.writer.Write(-1);
			}
			else
			{
				ctx.writer.Write(this.connections.Length);
				for (int i = 0; i < this.connections.Length; i++)
				{
					ctx.SerializeNodeReference(this.connections[i].node);
					ctx.writer.Write(this.connections[i].cost);
				}
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00014B40 File Offset: 0x00012F40
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
			}
			else
			{
				this.connections = new Connection[num];
				for (int i = 0; i < num; i++)
				{
					this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), byte.MaxValue);
				}
			}
		}

		// Token: 0x040004A8 RID: 1192
		public Connection[] connections;

		// Token: 0x040004A9 RID: 1193
		public GameObject gameObject;
	}
}
