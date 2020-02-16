using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005F RID: 95
	public abstract class MeshNode : GraphNode
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x00017D16 File Offset: 0x00016116
		protected MeshNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060003FF RID: 1023
		public abstract Int3 GetVertex(int i);

		// Token: 0x06000400 RID: 1024
		public abstract int GetVertexCount();

		// Token: 0x06000401 RID: 1025
		public abstract Vector3 ClosestPointOnNode(Vector3 p);

		// Token: 0x06000402 RID: 1026
		public abstract Vector3 ClosestPointOnNodeXZ(Vector3 p);

		// Token: 0x06000403 RID: 1027 RVA: 0x00017D20 File Offset: 0x00016120
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse && this.connections != null)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node != null)
					{
						this.connections[i].node.RemoveConnection(this);
					}
				}
			}
			ArrayPool<Connection>.Release(ref this.connections, true);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017D90 File Offset: 0x00016190
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

		// Token: 0x06000405 RID: 1029 RVA: 0x00017DDC File Offset: 0x000161DC
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			if (this.connections == null)
			{
				return;
			}
			for (int i = 0; i < this.connections.Length; i++)
			{
				GraphNode node = this.connections[i].node;
				if (node.Area != region)
				{
					node.Area = region;
					stack.Push(node);
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00017E3C File Offset: 0x0001623C
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == node)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00017E7C File Offset: 0x0001627C
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

		// Token: 0x06000408 RID: 1032 RVA: 0x00017EF5 File Offset: 0x000162F5
		public override void AddConnection(GraphNode node, uint cost)
		{
			this.AddConnection(node, cost, -1);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00017F00 File Offset: 0x00016300
		public void AddConnection(GraphNode node, uint cost, int shapeEdge)
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
						this.connections[i].shapeEdge = ((shapeEdge < 0) ? this.connections[i].shapeEdge : ((byte)shapeEdge));
						return;
					}
				}
			}
			int num = (this.connections == null) ? 0 : this.connections.Length;
			Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num + 1);
			for (int j = 0; j < num; j++)
			{
				array[j] = this.connections[j];
			}
			array[num] = new Connection(node, cost, (byte)shapeEdge);
			if (this.connections != null)
			{
				ArrayPool<Connection>.Release(ref this.connections, true);
			}
			this.connections = array;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001801C File Offset: 0x0001641C
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
					Connection[] array = ArrayPool<Connection>.ClaimWithExactLength(num - 1);
					for (int j = 0; j < i; j++)
					{
						array[j] = this.connections[j];
					}
					for (int k = i + 1; k < num; k++)
					{
						array[k - 1] = this.connections[k];
					}
					if (this.connections != null)
					{
						ArrayPool<Connection>.Release(ref this.connections, true);
					}
					this.connections = array;
					return;
				}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000180FE File Offset: 0x000164FE
		public virtual bool ContainsPoint(Int3 point)
		{
			return this.ContainsPoint((Vector3)point);
		}

		// Token: 0x0600040C RID: 1036
		public abstract bool ContainsPoint(Vector3 point);

		// Token: 0x0600040D RID: 1037
		public abstract bool ContainsPointInGraphSpace(Int3 point);

		// Token: 0x0600040E RID: 1038 RVA: 0x0001810C File Offset: 0x0001650C
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

		// Token: 0x0600040F RID: 1039 RVA: 0x00018164 File Offset: 0x00016564
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
					ctx.writer.Write(this.connections[i].shapeEdge);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00018208 File Offset: 0x00016608
		public override void DeserializeReferences(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.connections = null;
			}
			else
			{
				this.connections = ArrayPool<Connection>.ClaimWithExactLength(num);
				for (int i = 0; i < num; i++)
				{
					this.connections[i] = new Connection(ctx.DeserializeNodeReference(), ctx.reader.ReadUInt32(), (!(ctx.meta.version < AstarSerializer.V4_1_0)) ? ctx.reader.ReadByte() : byte.MaxValue);
				}
			}
		}

		// Token: 0x04000260 RID: 608
		public Connection[] connections;
	}
}
