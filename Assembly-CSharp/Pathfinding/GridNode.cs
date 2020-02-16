using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AC RID: 172
	public class GridNode : GridNodeBase
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0003068C File Offset: 0x0002EA8C
		public GridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00030695 File Offset: 0x0002EA95
		public static GridGraph GetGridGraph(uint graphIndex)
		{
			return GridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000306A0 File Offset: 0x0002EAA0
		public static void SetGridGraph(int graphIndex, GridGraph graph)
		{
			if (GridNode._gridGraphs.Length <= graphIndex)
			{
				GridGraph[] array = new GridGraph[graphIndex + 1];
				for (int i = 0; i < GridNode._gridGraphs.Length; i++)
				{
					array[i] = GridNode._gridGraphs[i];
				}
				GridNode._gridGraphs = array;
			}
			GridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x000306F3 File Offset: 0x0002EAF3
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x000306FB File Offset: 0x0002EAFB
		internal ushort InternalGridFlags
		{
			get
			{
				return this.gridFlags;
			}
			set
			{
				this.gridFlags = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00030704 File Offset: 0x0002EB04
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return (this.InternalGridFlags & 255) == 255;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00030719 File Offset: 0x0002EB19
		public bool HasConnectionInDirection(int dir)
		{
			return (this.gridFlags >> dir & 1) != 0;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0003072E File Offset: 0x0002EB2E
		[Obsolete("Use HasConnectionInDirection")]
		public bool GetConnectionInternal(int dir)
		{
			return this.HasConnectionInDirection(dir);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00030737 File Offset: 0x0002EB37
		public void SetConnectionInternal(int dir, bool value)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & ~(1 << dir)) | ((!value) ? 0 : 1) << 0 << (dir & 31));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00030763 File Offset: 0x0002EB63
		public void SetAllConnectionInternal(int connections)
		{
			this.gridFlags = (ushort)(((int)this.gridFlags & -256) | connections << 0);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0003077C File Offset: 0x0002EB7C
		public void ResetConnectionsInternal()
		{
			this.gridFlags = (ushort)((int)this.gridFlags & -256);
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00030791 File Offset: 0x0002EB91
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x000307A5 File Offset: 0x0002EBA5
		public bool EdgeNode
		{
			get
			{
				return (this.gridFlags & 1024) != 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -1025) | ((!value) ? 0 : 1024));
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000307CC File Offset: 0x0002EBCC
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.HasConnectionInDirection(direction))
			{
				GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction]];
			}
			return null;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0003080C File Offset: 0x0002EC0C
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				for (int i = 0; i < 8; i++)
				{
					GridNode gridNode = this.GetNeighbourAlongDirection(i) as GridNode;
					if (gridNode != null)
					{
						gridNode.SetConnectionInternal((i >= 4) ? ((i - 2) % 4 + 4) : ((i + 2) % 4), false);
					}
				}
			}
			this.ResetConnectionsInternal();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0003086C File Offset: 0x0002EC6C
		public override void GetConnections(Action<GraphNode> action)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null)
					{
						action(gridNode);
					}
				}
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000308D0 File Offset: 0x0002ECD0
		public Vector3 ClosestPointOnNode(Vector3 p)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			p = gridGraph.transform.InverseTransform(p);
			int num = base.NodeInGridIndex % gridGraph.width;
			int num2 = base.NodeInGridIndex / gridGraph.width;
			float y = gridGraph.transform.InverseTransform((Vector3)this.position).y;
			Vector3 point = new Vector3(Mathf.Clamp(p.x, (float)num, (float)num + 1f), y, Mathf.Clamp(p.z, (float)num2, (float)num2 + 1f));
			return gridGraph.transform.Transform(point);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00030974 File Offset: 0x0002ED74
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				if (this.HasConnectionInDirection(i) && other == nodes[base.NodeInGridIndex + neighbourOffsets[i]])
				{
					Vector3 a = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector.Normalize();
					vector *= gridGraph.nodeSize * 0.5f;
					left.Add(a - vector);
					right.Add(a + vector);
					return true;
				}
			}
			for (int j = 4; j < 8; j++)
			{
				if (this.HasConnectionInDirection(j) && other == nodes[base.NodeInGridIndex + neighbourOffsets[j]])
				{
					bool flag = false;
					bool flag2 = false;
					if (this.HasConnectionInDirection(j - 4))
					{
						GridNode gridNode = nodes[base.NodeInGridIndex + neighbourOffsets[j - 4]];
						if (gridNode.Walkable && gridNode.HasConnectionInDirection((j - 4 + 1) % 4))
						{
							flag = true;
						}
					}
					if (this.HasConnectionInDirection((j - 4 + 1) % 4))
					{
						GridNode gridNode2 = nodes[base.NodeInGridIndex + neighbourOffsets[(j - 4 + 1) % 4]];
						if (gridNode2.Walkable && gridNode2.HasConnectionInDirection(j - 4))
						{
							flag2 = true;
						}
					}
					Vector3 a2 = (Vector3)(this.position + other.position) * 0.5f;
					Vector3 vector2 = Vector3.Cross(gridGraph.collision.up, (Vector3)(other.position - this.position));
					vector2.Normalize();
					vector2 *= gridGraph.nodeSize * 1.4142f;
					left.Add(a2 - ((!flag2) ? Vector3.zero : vector2));
					right.Add(a2 + ((!flag) ? Vector3.zero : vector2));
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00030BCC File Offset: 0x0002EFCC
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (gridNode != null && gridNode.Area != region)
					{
						gridNode.Area = region;
						stack.Push(gridNode);
					}
				}
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00030C4C File Offset: 0x0002F04C
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			GridNode[] nodes = gridGraph.nodes;
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			ushort pathID = handler.PathID;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					PathNode pathNode2 = handler.GetPathNode(gridNode);
					if (pathNode2.parent == pathNode && pathNode2.pathID == pathID)
					{
						gridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00030CF4 File Offset: 0x0002F0F4
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			ushort pathID = handler.PathID;
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			GridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 8; i++)
			{
				if (this.HasConnectionInDirection(i))
				{
					GridNode gridNode = nodes[nodeInGridIndex + neighbourOffsets[i]];
					if (path.CanTraverse(gridNode))
					{
						PathNode pathNode2 = handler.GetPathNode(gridNode);
						uint num = neighbourCosts[i];
						if (pathNode2.pathID != pathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = pathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(gridNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(gridNode) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							gridNode.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00030E0C File Offset: 0x0002F20C
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00030E32 File Offset: 0x0002F232
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
		}

		// Token: 0x0400049A RID: 1178
		private static GridGraph[] _gridGraphs = new GridGraph[0];

		// Token: 0x0400049B RID: 1179
		private const int GridFlagsConnectionOffset = 0;

		// Token: 0x0400049C RID: 1180
		private const int GridFlagsConnectionBit0 = 1;

		// Token: 0x0400049D RID: 1181
		private const int GridFlagsConnectionMask = 255;

		// Token: 0x0400049E RID: 1182
		private const int GridFlagsEdgeNodeOffset = 10;

		// Token: 0x0400049F RID: 1183
		private const int GridFlagsEdgeNodeMask = 1024;
	}
}
