using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A7 RID: 167
	public class LevelGridNode : GridNodeBase
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0002C931 File Offset: 0x0002AD31
		public LevelGridNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0002C93A File Offset: 0x0002AD3A
		public static LayerGridGraph GetGridGraph(uint graphIndex)
		{
			return LevelGridNode._gridGraphs[(int)graphIndex];
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0002C944 File Offset: 0x0002AD44
		public static void SetGridGraph(int graphIndex, LayerGridGraph graph)
		{
			GridNode.SetGridGraph(graphIndex, graph);
			if (LevelGridNode._gridGraphs.Length <= graphIndex)
			{
				LayerGridGraph[] array = new LayerGridGraph[graphIndex + 1];
				for (int i = 0; i < LevelGridNode._gridGraphs.Length; i++)
				{
					array[i] = LevelGridNode._gridGraphs[i];
				}
				LevelGridNode._gridGraphs = array;
			}
			LevelGridNode._gridGraphs[graphIndex] = graph;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0002C99E File Offset: 0x0002AD9E
		public void ResetAllGridConnections()
		{
			this.gridConnections = ulong.MaxValue;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0002C9A8 File Offset: 0x0002ADA8
		public bool HasAnyGridConnections()
		{
			return this.gridConnections != ulong.MaxValue;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0002C9B7 File Offset: 0x0002ADB7
		public override bool HasConnectionsToAllEightNeighbours
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0002C9BA File Offset: 0x0002ADBA
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0002C9C5 File Offset: 0x0002ADC5
		public int LayerCoordinateInGrid
		{
			get
			{
				return this.nodeInGridIndex >> 24;
			}
			set
			{
				this.nodeInGridIndex = ((this.nodeInGridIndex & 16777215) | value << 24);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0002C9DE File Offset: 0x0002ADDE
		public void SetPosition(Int3 position)
		{
			this.position = position;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0002C9E7 File Offset: 0x0002ADE7
		public override int GetGizmoHashCode()
		{
			return base.GetGizmoHashCode() ^ (int)(805306457UL * this.gridConnections);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002CA00 File Offset: 0x0002AE00
		public override GridNodeBase GetNeighbourAlongDirection(int direction)
		{
			if (this.GetConnection(direction))
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				return gridGraph.nodes[base.NodeInGridIndex + gridGraph.neighbourOffsets[direction] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * this.GetConnectionValue(direction)];
			}
			return null;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0002CA54 File Offset: 0x0002AE54
		public override void ClearConnections(bool alsoReverse)
		{
			if (alsoReverse)
			{
				LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
				int[] neighbourOffsets = gridGraph.neighbourOffsets;
				LevelGridNode[] nodes = gridGraph.nodes;
				for (int i = 0; i < 4; i++)
				{
					int connectionValue = this.GetConnectionValue(i);
					if (connectionValue != 255)
					{
						LevelGridNode levelGridNode = nodes[base.NodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
						if (levelGridNode != null)
						{
							levelGridNode.SetConnectionValue((i + 2) % 4, 255);
						}
					}
				}
			}
			this.ResetAllGridConnections();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002CAE8 File Offset: 0x0002AEE8
		public override void GetConnections(Action<GraphNode> action)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (levelGridNode != null)
					{
						action(levelGridNode);
					}
				}
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0002CB6C File Offset: 0x0002AF6C
		public override void FloodFill(Stack<GraphNode> stack, uint region)
		{
			int nodeInGridIndex = base.NodeInGridIndex;
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (levelGridNode != null && levelGridNode.Area != region)
					{
						levelGridNode.Area = region;
						stack.Push(levelGridNode);
					}
				}
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002CC05 File Offset: 0x0002B005
		public bool GetConnection(int i)
		{
			return (this.gridConnections >> i * 8 & 255UL) != 255UL;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002CC26 File Offset: 0x0002B026
		public void SetConnectionValue(int dir, int value)
		{
			this.gridConnections = ((this.gridConnections & ~(255UL << dir * 8)) | (ulong)((ulong)((long)value) << dir * 8));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002CC4D File Offset: 0x0002B04D
		public int GetConnectionValue(int dir)
		{
			return (int)(this.gridConnections >> dir * 8 & 255UL);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0002CC64 File Offset: 0x0002B064
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (backwards)
			{
				return true;
			}
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255 && other == nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue])
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
			return false;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0002CD70 File Offset: 0x0002B170
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			handler.heap.Add(pathNode);
			pathNode.UpdateG(path);
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					LevelGridNode levelGridNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					PathNode pathNode2 = handler.GetPathNode(levelGridNode);
					if (pathNode2 != null && pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
					{
						levelGridNode.UpdateRecursiveG(path, pathNode2, handler);
					}
				}
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0002CE34 File Offset: 0x0002B234
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			LayerGridGraph gridGraph = LevelGridNode.GetGridGraph(base.GraphIndex);
			int[] neighbourOffsets = gridGraph.neighbourOffsets;
			uint[] neighbourCosts = gridGraph.neighbourCosts;
			LevelGridNode[] nodes = gridGraph.nodes;
			int nodeInGridIndex = base.NodeInGridIndex;
			for (int i = 0; i < 4; i++)
			{
				int connectionValue = this.GetConnectionValue(i);
				if (connectionValue != 255)
				{
					GraphNode graphNode = nodes[nodeInGridIndex + neighbourOffsets[i] + gridGraph.lastScannedWidth * gridGraph.lastScannedDepth * connectionValue];
					if (path.CanTraverse(graphNode))
					{
						PathNode pathNode2 = handler.GetPathNode(graphNode);
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = neighbourCosts[i];
							pathNode2.H = path.CalculateHScore(graphNode);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else
						{
							uint num = neighbourCosts[i];
							if (pathNode.G + num + path.GetTraversalCost(graphNode) < pathNode2.G)
							{
								pathNode2.cost = num;
								pathNode2.parent = pathNode;
								graphNode.UpdateRecursiveG(path, pathNode2, handler);
							}
						}
					}
				}
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0002CF69 File Offset: 0x0002B369
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.SerializeInt3(this.position);
			ctx.writer.Write(this.gridFlags);
			ctx.writer.Write(this.gridConnections);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0002CFA0 File Offset: 0x0002B3A0
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.position = ctx.DeserializeInt3();
			this.gridFlags = ctx.reader.ReadUInt16();
			if (ctx.meta.version < AstarSerializer.V3_9_0)
			{
				this.gridConnections = ((ulong)ctx.reader.ReadUInt32() | 18446744069414584320UL);
			}
			else
			{
				this.gridConnections = ctx.reader.ReadUInt64();
			}
		}

		// Token: 0x04000475 RID: 1141
		private static LayerGridGraph[] _gridGraphs = new LayerGridGraph[0];

		// Token: 0x04000476 RID: 1142
		public ulong gridConnections;

		// Token: 0x04000477 RID: 1143
		protected static LayerGridGraph[] gridGraphs;

		// Token: 0x04000478 RID: 1144
		public const int NoConnection = 255;

		// Token: 0x04000479 RID: 1145
		public const int ConnectionMask = 255;

		// Token: 0x0400047A RID: 1146
		private const int ConnectionStride = 8;

		// Token: 0x0400047B RID: 1147
		public const int MaxLayerCount = 255;
	}
}
