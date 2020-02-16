using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B0 RID: 176
	public class TriangleMeshNode : MeshNode
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00030E65 File Offset: 0x0002F265
		public TriangleMeshNode(AstarPath astar) : base(astar)
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00030E6E File Offset: 0x0002F26E
		public static INavmeshHolder GetNavmeshHolder(uint graphIndex)
		{
			return TriangleMeshNode._navmeshHolders[(int)graphIndex];
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00030E78 File Offset: 0x0002F278
		public static void SetNavmeshHolder(int graphIndex, INavmeshHolder graph)
		{
			object obj = TriangleMeshNode.lockObject;
			lock (obj)
			{
				if (graphIndex >= TriangleMeshNode._navmeshHolders.Length)
				{
					INavmeshHolder[] array = new INavmeshHolder[graphIndex + 1];
					TriangleMeshNode._navmeshHolders.CopyTo(array, 0);
					TriangleMeshNode._navmeshHolders = array;
				}
				TriangleMeshNode._navmeshHolders[graphIndex] = graph;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00030EE0 File Offset: 0x0002F2E0
		public void UpdatePositionFromVertices()
		{
			Int3 lhs;
			Int3 rhs;
			Int3 rhs2;
			this.GetVertices(out lhs, out rhs, out rhs2);
			this.position = (lhs + rhs + rhs2) * 0.333333f;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00030F16 File Offset: 0x0002F316
		public int GetVertexIndex(int i)
		{
			return (i != 0) ? ((i != 1) ? this.v2 : this.v1) : this.v0;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00030F41 File Offset: 0x0002F341
		public int GetVertexArrayIndex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexArrayIndex((i != 0) ? ((i != 1) ? this.v2 : this.v1) : this.v0);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00030F7C File Offset: 0x0002F37C
		public void GetVertices(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertex(this.v0);
			v1 = navmeshHolder.GetVertex(this.v1);
			v2 = navmeshHolder.GetVertex(this.v2);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00030FCC File Offset: 0x0002F3CC
		public void GetVerticesInGraphSpace(out Int3 v0, out Int3 v1, out Int3 v2)
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			v0 = navmeshHolder.GetVertexInGraphSpace(this.v0);
			v1 = navmeshHolder.GetVertexInGraphSpace(this.v1);
			v2 = navmeshHolder.GetVertexInGraphSpace(this.v2);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0003101B File Offset: 0x0002F41B
		public override Int3 GetVertex(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertex(this.GetVertexIndex(i));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00031034 File Offset: 0x0002F434
		public Int3 GetVertexInGraphSpace(int i)
		{
			return TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).GetVertexInGraphSpace(this.GetVertexIndex(i));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0003104D File Offset: 0x0002F44D
		public override int GetVertexCount()
		{
			return 3;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00031050 File Offset: 0x0002F450
		public override Vector3 ClosestPointOnNode(Vector3 p)
		{
			Int3 ob;
			Int3 ob2;
			Int3 ob3;
			this.GetVertices(out ob, out ob2, out ob3);
			return Polygon.ClosestPointOnTriangle((Vector3)ob, (Vector3)ob2, (Vector3)ob3, p);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00031084 File Offset: 0x0002F484
		internal Int3 ClosestPointOnNodeXZInGraphSpace(Vector3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			p = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p);
			Vector3 ob = Polygon.ClosestPointOnTriangleXZ((Vector3)@int, (Vector3)int2, (Vector3)int3, p);
			Int3 int4 = (Int3)ob;
			if (this.ContainsPointInGraphSpace(int4))
			{
				return int4;
			}
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i != 0 || j != 0)
					{
						Int3 int5 = new Int3(int4.x + i, int4.y, int4.z + j);
						if (this.ContainsPointInGraphSpace(int5))
						{
							return int5;
						}
					}
				}
			}
			long sqrMagnitudeLong = (@int - int4).sqrMagnitudeLong;
			long sqrMagnitudeLong2 = (int2 - int4).sqrMagnitudeLong;
			long sqrMagnitudeLong3 = (int3 - int4).sqrMagnitudeLong;
			return (sqrMagnitudeLong >= sqrMagnitudeLong2) ? ((sqrMagnitudeLong2 >= sqrMagnitudeLong3) ? int3 : int2) : ((sqrMagnitudeLong >= sqrMagnitudeLong3) ? int3 : @int);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000311BC File Offset: 0x0002F5BC
		public override Vector3 ClosestPointOnNodeXZ(Vector3 p)
		{
			Int3 ob;
			Int3 ob2;
			Int3 ob3;
			this.GetVertices(out ob, out ob2, out ob3);
			return Polygon.ClosestPointOnTriangleXZ((Vector3)ob, (Vector3)ob2, (Vector3)ob3, p);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000311ED File Offset: 0x0002F5ED
		public override bool ContainsPoint(Vector3 p)
		{
			return this.ContainsPointInGraphSpace((Int3)TriangleMeshNode.GetNavmeshHolder(base.GraphIndex).transform.InverseTransform(p));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00031210 File Offset: 0x0002F610
		public override bool ContainsPointInGraphSpace(Int3 p)
		{
			Int3 @int;
			Int3 int2;
			Int3 int3;
			this.GetVerticesInGraphSpace(out @int, out int2, out int3);
			return (long)(int2.x - @int.x) * (long)(p.z - @int.z) - (long)(p.x - @int.x) * (long)(int2.z - @int.z) <= 0L && (long)(int3.x - int2.x) * (long)(p.z - int2.z) - (long)(p.x - int2.x) * (long)(int3.z - int2.z) <= 0L && (long)(@int.x - int3.x) * (long)(p.z - int3.z) - (long)(p.x - int3.x) * (long)(@int.z - int3.z) <= 0L;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00031310 File Offset: 0x0002F710
		public override void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			if (this.connections == null)
			{
				return;
			}
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

		// Token: 0x06000748 RID: 1864 RVA: 0x00031398 File Offset: 0x0002F798
		public override void Open(Path path, PathNode pathNode, PathHandler handler)
		{
			if (this.connections == null)
			{
				return;
			}
			bool flag = pathNode.flag2;
			for (int i = this.connections.Length - 1; i >= 0; i--)
			{
				Connection connection = this.connections[i];
				GraphNode node = connection.node;
				if (path.CanTraverse(connection.node))
				{
					PathNode pathNode2 = handler.GetPathNode(connection.node);
					if (pathNode2 != pathNode.parent)
					{
						uint num = connection.cost;
						if (flag || pathNode2.flag2)
						{
							num = path.GetConnectionSpecialCost(this, connection.node, num);
						}
						if (pathNode2.pathID != handler.PathID)
						{
							pathNode2.node = connection.node;
							pathNode2.parent = pathNode;
							pathNode2.pathID = handler.PathID;
							pathNode2.cost = num;
							pathNode2.H = path.CalculateHScore(node);
							pathNode2.UpdateG(path);
							handler.heap.Add(pathNode2);
						}
						else if (pathNode.G + num + path.GetTraversalCost(node) < pathNode2.G)
						{
							pathNode2.cost = num;
							pathNode2.parent = pathNode;
							node.UpdateRecursiveG(path, pathNode2, handler);
						}
					}
				}
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000314EC File Offset: 0x0002F8EC
		public int SharedEdge(GraphNode other)
		{
			int result = -1;
			for (int i = 0; i < this.connections.Length; i++)
			{
				if (this.connections[i].node == other)
				{
					result = (int)this.connections[i].shapeEdge;
				}
			}
			return result;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00031540 File Offset: 0x0002F940
		public override bool GetPortal(GraphNode toNode, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			int num;
			int num2;
			return this.GetPortal(toNode, left, right, backwards, out num, out num2);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0003155C File Offset: 0x0002F95C
		public bool GetPortal(GraphNode toNode, List<Vector3> left, List<Vector3> right, bool backwards, out int aIndex, out int bIndex)
		{
			aIndex = -1;
			bIndex = -1;
			if (backwards || toNode.GraphIndex != base.GraphIndex)
			{
				return false;
			}
			TriangleMeshNode triangleMeshNode = toNode as TriangleMeshNode;
			int num = this.SharedEdge(triangleMeshNode);
			if (num == 255)
			{
				return false;
			}
			if (num == -1)
			{
				for (int i = 0; i < this.connections.Length; i++)
				{
					if (this.connections[i].node.GraphIndex != base.GraphIndex)
					{
						NodeLink3Node nodeLink3Node = this.connections[i].node as NodeLink3Node;
						if (nodeLink3Node != null && nodeLink3Node.GetOther(this) == triangleMeshNode)
						{
							nodeLink3Node.GetPortal(triangleMeshNode, left, right, false);
							return true;
						}
					}
				}
				return false;
			}
			aIndex = num;
			bIndex = (num + 1) % this.GetVertexCount();
			Int3 vertex = this.GetVertex(num);
			Int3 vertex2 = this.GetVertex((num + 1) % this.GetVertexCount());
			int num2 = this.GetVertexIndex(0) >> 12 & 524287;
			int num3 = triangleMeshNode.GetVertexIndex(0) >> 12 & 524287;
			if (num2 != num3)
			{
				INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
				int num4;
				int num5;
				navmeshHolder.GetTileCoordinates(num2, out num4, out num5);
				int num6;
				int num7;
				navmeshHolder.GetTileCoordinates(num3, out num6, out num7);
				int i2;
				if (Math.Abs(num4 - num6) == 1)
				{
					i2 = 2;
				}
				else
				{
					if (Math.Abs(num5 - num7) != 1)
					{
						return false;
					}
					i2 = 0;
				}
				int num8 = triangleMeshNode.SharedEdge(this);
				if (num8 == 255)
				{
					throw new Exception("Connection used edge in one direction, but not in the other direction. Has the wrong overload of AddConnection been used?");
				}
				if (num8 != -1)
				{
					int num9 = Math.Min(vertex[i2], vertex2[i2]);
					int num10 = Math.Max(vertex[i2], vertex2[i2]);
					Int3 vertex3 = triangleMeshNode.GetVertex(num8);
					Int3 vertex4 = triangleMeshNode.GetVertex((num8 + 1) % triangleMeshNode.GetVertexCount());
					num9 = Math.Max(num9, Math.Min(vertex3[i2], vertex4[i2]));
					num10 = Math.Min(num10, Math.Max(vertex3[i2], vertex4[i2]));
					if (vertex[i2] < vertex2[i2])
					{
						vertex[i2] = num9;
						vertex2[i2] = num10;
					}
					else
					{
						vertex[i2] = num10;
						vertex2[i2] = num9;
					}
				}
			}
			if (left != null)
			{
				left.Add((Vector3)vertex);
				right.Add((Vector3)vertex2);
			}
			return true;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000317F4 File Offset: 0x0002FBF4
		public override float SurfaceArea()
		{
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (float)Math.Abs(VectorMath.SignedTriangleAreaTimes2XZ(navmeshHolder.GetVertex(this.v0), navmeshHolder.GetVertex(this.v1), navmeshHolder.GetVertex(this.v2))) * 0.5f;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00031844 File Offset: 0x0002FC44
		public override Vector3 RandomPointOnSurface()
		{
			float value;
			float value2;
			do
			{
				value = UnityEngine.Random.value;
				value2 = UnityEngine.Random.value;
			}
			while (value + value2 > 1f);
			INavmeshHolder navmeshHolder = TriangleMeshNode.GetNavmeshHolder(base.GraphIndex);
			return (Vector3)(navmeshHolder.GetVertex(this.v1) - navmeshHolder.GetVertex(this.v0)) * value + (Vector3)(navmeshHolder.GetVertex(this.v2) - navmeshHolder.GetVertex(this.v0)) * value2 + (Vector3)navmeshHolder.GetVertex(this.v0);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000318E1 File Offset: 0x0002FCE1
		public override void SerializeNode(GraphSerializationContext ctx)
		{
			base.SerializeNode(ctx);
			ctx.writer.Write(this.v0);
			ctx.writer.Write(this.v1);
			ctx.writer.Write(this.v2);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0003191D File Offset: 0x0002FD1D
		public override void DeserializeNode(GraphSerializationContext ctx)
		{
			base.DeserializeNode(ctx);
			this.v0 = ctx.reader.ReadInt32();
			this.v1 = ctx.reader.ReadInt32();
			this.v2 = ctx.reader.ReadInt32();
		}

		// Token: 0x040004AA RID: 1194
		public int v0;

		// Token: 0x040004AB RID: 1195
		public int v1;

		// Token: 0x040004AC RID: 1196
		public int v2;

		// Token: 0x040004AD RID: 1197
		protected static INavmeshHolder[] _navmeshHolders = new INavmeshHolder[0];

		// Token: 0x040004AE RID: 1198
		protected static readonly object lockObject = new object();
	}
}
