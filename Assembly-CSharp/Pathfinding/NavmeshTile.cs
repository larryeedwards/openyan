using System;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x020000BE RID: 190
	public class NavmeshTile : INavmeshHolder, ITransformedGraph, INavmesh
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x0003757A File Offset: 0x0003597A
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			x = this.x;
			z = this.z;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0003758C File Offset: 0x0003598C
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00037598 File Offset: 0x00035998
		public Int3 GetVertex(int index)
		{
			int num = index & 4095;
			return this.verts[num];
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000375BE File Offset: 0x000359BE
		public Int3 GetVertexInGraphSpace(int index)
		{
			return this.vertsInGraphSpace[index & 4095];
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000375D7 File Offset: 0x000359D7
		public GraphTransform transform
		{
			get
			{
				return this.graph.transform;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000375E4 File Offset: 0x000359E4
		public void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x04000512 RID: 1298
		public int[] tris;

		// Token: 0x04000513 RID: 1299
		public Int3[] verts;

		// Token: 0x04000514 RID: 1300
		public Int3[] vertsInGraphSpace;

		// Token: 0x04000515 RID: 1301
		public int x;

		// Token: 0x04000516 RID: 1302
		public int z;

		// Token: 0x04000517 RID: 1303
		public int w;

		// Token: 0x04000518 RID: 1304
		public int d;

		// Token: 0x04000519 RID: 1305
		public TriangleMeshNode[] nodes;

		// Token: 0x0400051A RID: 1306
		public BBTree bbTree;

		// Token: 0x0400051B RID: 1307
		public bool flag;

		// Token: 0x0400051C RID: 1308
		public NavmeshBase graph;
	}
}
