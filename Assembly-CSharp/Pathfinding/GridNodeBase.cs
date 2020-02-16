using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000AD RID: 173
	public abstract class GridNodeBase : GraphNode
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0002C760 File Offset: 0x0002AB60
		protected GridNodeBase(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0002C769 File Offset: 0x0002AB69
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0002C777 File Offset: 0x0002AB77
		public int NodeInGridIndex
		{
			get
			{
				return this.nodeInGridIndex & 16777215;
			}
			set
			{
				this.nodeInGridIndex = ((this.nodeInGridIndex & -16777216) | value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0002C78D File Offset: 0x0002AB8D
		public int XCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex % GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0002C7A6 File Offset: 0x0002ABA6
		public int ZCoordinateInGrid
		{
			get
			{
				return this.NodeInGridIndex / GridNode.GetGridGraph(base.GraphIndex).width;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0002C7BF File Offset: 0x0002ABBF
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0002C7D3 File Offset: 0x0002ABD3
		public bool WalkableErosion
		{
			get
			{
				return (this.gridFlags & 256) != 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -257) | ((!value) ? 0 : 256));
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0002C7FA File Offset: 0x0002ABFA
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0002C80E File Offset: 0x0002AC0E
		public bool TmpWalkable
		{
			get
			{
				return (this.gridFlags & 512) != 0;
			}
			set
			{
				this.gridFlags = (ushort)(((int)this.gridFlags & -513) | ((!value) ? 0 : 512));
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600071C RID: 1820
		public abstract bool HasConnectionsToAllEightNeighbours { get; }

		// Token: 0x0600071D RID: 1821 RVA: 0x0002C838 File Offset: 0x0002AC38
		public override float SurfaceArea()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			return gridGraph.nodeSize * gridGraph.nodeSize;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0002C860 File Offset: 0x0002AC60
		public override Vector3 RandomPointOnSurface()
		{
			GridGraph gridGraph = GridNode.GetGridGraph(base.GraphIndex);
			Vector3 a = gridGraph.transform.InverseTransform((Vector3)this.position);
			return gridGraph.transform.Transform(a + new Vector3(UnityEngine.Random.value - 0.5f, 0f, UnityEngine.Random.value - 0.5f));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0002C8C4 File Offset: 0x0002ACC4
		public override int GetGizmoHashCode()
		{
			int gizmoHashCode = base.GetGizmoHashCode();
			return gizmoHashCode ^ (int)(109 * this.gridFlags);
		}

		// Token: 0x06000720 RID: 1824
		public abstract GridNodeBase GetNeighbourAlongDirection(int direction);

		// Token: 0x06000721 RID: 1825 RVA: 0x0002C8E8 File Offset: 0x0002ACE8
		public override bool ContainsConnection(GraphNode node)
		{
			for (int i = 0; i < 8; i++)
			{
				if (node == this.GetNeighbourAlongDirection(i))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0002C917 File Offset: 0x0002AD17
		public override void AddConnection(GraphNode node, uint cost)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections with your current settings.\nPlease disable ASTAR_GRID_NO_CUSTOM_CONNECTIONS in the Optimizations tab in the A* Inspector");
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0002C923 File Offset: 0x0002AD23
		public override void RemoveConnection(GraphNode node)
		{
			throw new NotImplementedException("GridNodes do not have support for adding manual connections with your current settings.\nPlease disable ASTAR_GRID_NO_CUSTOM_CONNECTIONS in the Optimizations tab in the A* Inspector");
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0002C92F File Offset: 0x0002AD2F
		public void ClearCustomConnections(bool alsoReverse)
		{
		}

		// Token: 0x040004A0 RID: 1184
		private const int GridFlagsWalkableErosionOffset = 8;

		// Token: 0x040004A1 RID: 1185
		private const int GridFlagsWalkableErosionMask = 256;

		// Token: 0x040004A2 RID: 1186
		private const int GridFlagsWalkableTmpOffset = 9;

		// Token: 0x040004A3 RID: 1187
		private const int GridFlagsWalkableTmpMask = 512;

		// Token: 0x040004A4 RID: 1188
		protected const int NodeInGridIndexLayerOffset = 24;

		// Token: 0x040004A5 RID: 1189
		protected const int NodeInGridIndexMask = 16777215;

		// Token: 0x040004A6 RID: 1190
		protected int nodeInGridIndex;

		// Token: 0x040004A7 RID: 1191
		protected ushort gridFlags;
	}
}
