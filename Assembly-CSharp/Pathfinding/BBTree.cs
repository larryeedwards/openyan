using System;
using System.Diagnostics;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B4 RID: 180
	public class BBTree : IAstarPooledObject
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00034880 File Offset: 0x00032C80
		public Rect Size
		{
			get
			{
				if (this.count == 0)
				{
					return new Rect(0f, 0f, 0f, 0f);
				}
				IntRect rect = this.tree[0].rect;
				return Rect.MinMaxRect((float)rect.xmin * 0.001f, (float)rect.ymin * 0.001f, (float)rect.xmax * 0.001f, (float)rect.ymax * 0.001f);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00034904 File Offset: 0x00032D04
		public void Clear()
		{
			this.count = 0;
			this.leafNodes = 0;
			if (this.tree != null)
			{
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
			}
			if (this.nodeLookup != null)
			{
				for (int i = 0; i < this.nodeLookup.Length; i++)
				{
					this.nodeLookup[i] = null;
				}
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
			}
			this.tree = ArrayPool<BBTree.BBTreeBox>.Claim(0);
			this.nodeLookup = ArrayPool<TriangleMeshNode>.Claim(0);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00034987 File Offset: 0x00032D87
		void IAstarPooledObject.OnEnterPool()
		{
			this.Clear();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00034990 File Offset: 0x00032D90
		private void EnsureCapacity(int c)
		{
			if (c > this.tree.Length)
			{
				BBTree.BBTreeBox[] array = ArrayPool<BBTree.BBTreeBox>.Claim(c);
				this.tree.CopyTo(array, 0);
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
				this.tree = array;
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000349D4 File Offset: 0x00032DD4
		private void EnsureNodeCapacity(int c)
		{
			if (c > this.nodeLookup.Length)
			{
				TriangleMeshNode[] array = ArrayPool<TriangleMeshNode>.Claim(c);
				this.nodeLookup.CopyTo(array, 0);
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
				this.nodeLookup = array;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00034A18 File Offset: 0x00032E18
		private int GetBox(IntRect rect)
		{
			if (this.count >= this.tree.Length)
			{
				this.EnsureCapacity(this.count + 1);
			}
			this.tree[this.count] = new BBTree.BBTreeBox(rect);
			this.count++;
			return this.count - 1;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00034A78 File Offset: 0x00032E78
		public void RebuildFrom(TriangleMeshNode[] nodes)
		{
			this.Clear();
			if (nodes.Length == 0)
			{
				return;
			}
			this.EnsureCapacity(Mathf.CeilToInt((float)nodes.Length * 2.1f));
			this.EnsureNodeCapacity(Mathf.CeilToInt((float)nodes.Length * 1.1f));
			int[] array = ArrayPool<int>.Claim(nodes.Length);
			for (int i = 0; i < nodes.Length; i++)
			{
				array[i] = i;
			}
			IntRect[] array2 = ArrayPool<IntRect>.Claim(nodes.Length);
			for (int j = 0; j < nodes.Length; j++)
			{
				Int3 @int;
				Int3 int2;
				Int3 int3;
				nodes[j].GetVertices(out @int, out int2, out int3);
				IntRect intRect = new IntRect(@int.x, @int.z, @int.x, @int.z);
				intRect = intRect.ExpandToContain(int2.x, int2.z);
				intRect = intRect.ExpandToContain(int3.x, int3.z);
				array2[j] = intRect;
			}
			this.RebuildFromInternal(nodes, array, array2, 0, nodes.Length, false);
			ArrayPool<int>.Release(ref array, false);
			ArrayPool<IntRect>.Release(ref array2, false);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00034B88 File Offset: 0x00032F88
		private static int SplitByX(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.x > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00034BD8 File Offset: 0x00032FD8
		private static int SplitByZ(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.z > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00034C28 File Offset: 0x00033028
		private int RebuildFromInternal(TriangleMeshNode[] nodes, int[] permutation, IntRect[] nodeBounds, int from, int to, bool odd)
		{
			IntRect rect = BBTree.NodeBounds(permutation, nodeBounds, from, to);
			int box = this.GetBox(rect);
			if (to - from <= 4)
			{
				int num = this.tree[box].nodeOffset = this.leafNodes * 4;
				this.EnsureNodeCapacity(num + 4);
				this.leafNodes++;
				for (int i = 0; i < 4; i++)
				{
					this.nodeLookup[num + i] = ((i >= to - from) ? null : nodes[permutation[from + i]]);
				}
				return box;
			}
			int num2;
			if (odd)
			{
				int divider = (rect.xmin + rect.xmax) / 2;
				num2 = BBTree.SplitByX(nodes, permutation, from, to, divider);
			}
			else
			{
				int divider2 = (rect.ymin + rect.ymax) / 2;
				num2 = BBTree.SplitByZ(nodes, permutation, from, to, divider2);
			}
			if (num2 == from || num2 == to)
			{
				if (!odd)
				{
					int divider3 = (rect.xmin + rect.xmax) / 2;
					num2 = BBTree.SplitByX(nodes, permutation, from, to, divider3);
				}
				else
				{
					int divider4 = (rect.ymin + rect.ymax) / 2;
					num2 = BBTree.SplitByZ(nodes, permutation, from, to, divider4);
				}
				if (num2 == from || num2 == to)
				{
					num2 = (from + to) / 2;
				}
			}
			this.tree[box].left = this.RebuildFromInternal(nodes, permutation, nodeBounds, from, num2, !odd);
			this.tree[box].right = this.RebuildFromInternal(nodes, permutation, nodeBounds, num2, to, !odd);
			return box;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00034DDC File Offset: 0x000331DC
		private static IntRect NodeBounds(int[] permutation, IntRect[] nodeBounds, int from, int to)
		{
			IntRect result = nodeBounds[permutation[from]];
			for (int i = from + 1; i < to; i++)
			{
				IntRect intRect = nodeBounds[permutation[i]];
				result.xmin = Math.Min(result.xmin, intRect.xmin);
				result.ymin = Math.Min(result.ymin, intRect.ymin);
				result.xmax = Math.Max(result.xmax, intRect.xmax);
				result.ymax = Math.Max(result.ymax, intRect.ymax);
			}
			return result;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00034E84 File Offset: 0x00033284
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugRect(IntRect rect)
		{
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymin), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymax), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmin, 0f, (float)rect.ymax), Color.white);
			UnityEngine.Debug.DrawLine(new Vector3((float)rect.xmax, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00034F8C File Offset: 0x0003338C
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugNode(TriangleMeshNode node, float yoffset, Color color)
		{
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(1) + Vector3.up * yoffset, (Vector3)node.GetVertex(2) + Vector3.up * yoffset, color);
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(0) + Vector3.up * yoffset, (Vector3)node.GetVertex(1) + Vector3.up * yoffset, color);
			UnityEngine.Debug.DrawLine((Vector3)node.GetVertex(2) + Vector3.up * yoffset, (Vector3)node.GetVertex(0) + Vector3.up * yoffset, color);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00035053 File Offset: 0x00033453
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, out float distance)
		{
			distance = float.PositiveInfinity;
			return this.QueryClosest(p, constraint, ref distance, new NNInfoInternal(null));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003506C File Offset: 0x0003346C
		public NNInfoInternal QueryClosestXZ(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosestXZ(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000350C8 File Offset: 0x000334C8
		private void SearchBoxClosestXZ(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				int num = 0;
				while (num < 4 && array[bbtreeBox.nodeOffset + num] != null)
				{
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + num];
					if (constraint == null || constraint.Suitable(triangleMeshNode))
					{
						Vector3 constClampedPosition = triangleMeshNode.ClosestPointOnNodeXZ(p);
						float num2 = (constClampedPosition.x - p.x) * (constClampedPosition.x - p.x) + (constClampedPosition.z - p.z) * (constClampedPosition.z - p.z);
						if (nnInfo.constrainedNode == null || num2 < closestSqrDist - 1E-06f || (num2 <= closestSqrDist + 1E-06f && Mathf.Abs(constClampedPosition.y - p.y) < Mathf.Abs(nnInfo.constClampedPosition.y - p.y)))
						{
							nnInfo.constrainedNode = triangleMeshNode;
							nnInfo.constClampedPosition = constClampedPosition;
							closestSqrDist = num2;
						}
					}
					num++;
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num3;
				float num4;
				this.GetOrderedChildren(ref left, ref right, out num3, out num4, p);
				if (num3 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num4 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003524C File Offset: 0x0003364C
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosest(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000352A8 File Offset: 0x000336A8
		private void SearchBoxClosest(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				int num = 0;
				while (num < 4 && array[bbtreeBox.nodeOffset + num] != null)
				{
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + num];
					Vector3 vector = triangleMeshNode.ClosestPointOnNode(p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (sqrMagnitude < closestSqrDist)
					{
						if (constraint == null || constraint.Suitable(triangleMeshNode))
						{
							nnInfo.constrainedNode = triangleMeshNode;
							nnInfo.constClampedPosition = vector;
							closestSqrDist = sqrMagnitude;
						}
					}
					num++;
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num2;
				float num3;
				this.GetOrderedChildren(ref left, ref right, out num2, out num3, p);
				if (num2 < closestSqrDist)
				{
					this.SearchBoxClosest(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num3 < closestSqrDist)
				{
					this.SearchBoxClosest(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000353B0 File Offset: 0x000337B0
		private void GetOrderedChildren(ref int first, ref int second, out float firstDist, out float secondDist, Vector3 p)
		{
			firstDist = BBTree.SquaredRectPointDistance(this.tree[first].rect, p);
			secondDist = BBTree.SquaredRectPointDistance(this.tree[second].rect, p);
			if (secondDist < firstDist)
			{
				int num = first;
				first = second;
				second = num;
				float num2 = firstDist;
				firstDist = secondDist;
				secondDist = num2;
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00035414 File Offset: 0x00033814
		public TriangleMeshNode QueryInside(Vector3 p, NNConstraint constraint)
		{
			return (this.count == 0 || !this.tree[0].Contains(p)) ? null : this.SearchBoxInside(0, p, constraint);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00035448 File Offset: 0x00033848
		private TriangleMeshNode SearchBoxInside(int boxi, Vector3 p, NNConstraint constraint)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				int num = 0;
				while (num < 4 && array[bbtreeBox.nodeOffset + num] != null)
				{
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + num];
					if (triangleMeshNode.ContainsPoint((Int3)p))
					{
						if (constraint == null || constraint.Suitable(triangleMeshNode))
						{
							return triangleMeshNode;
						}
					}
					num++;
				}
			}
			else
			{
				if (this.tree[bbtreeBox.left].Contains(p))
				{
					TriangleMeshNode triangleMeshNode2 = this.SearchBoxInside(bbtreeBox.left, p, constraint);
					if (triangleMeshNode2 != null)
					{
						return triangleMeshNode2;
					}
				}
				if (this.tree[bbtreeBox.right].Contains(p))
				{
					TriangleMeshNode triangleMeshNode3 = this.SearchBoxInside(bbtreeBox.right, p, constraint);
					if (triangleMeshNode3 != null)
					{
						return triangleMeshNode3;
					}
				}
			}
			return null;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00035548 File Offset: 0x00033948
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
			if (this.count == 0)
			{
				return;
			}
			this.OnDrawGizmos(0, 0);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003557C File Offset: 0x0003397C
		private void OnDrawGizmos(int boxi, int depth)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			Vector3 a = (Vector3)new Int3(bbtreeBox.rect.xmin, 0, bbtreeBox.rect.ymin);
			Vector3 vector = (Vector3)new Int3(bbtreeBox.rect.xmax, 0, bbtreeBox.rect.ymax);
			Vector3 vector2 = (a + vector) * 0.5f;
			Vector3 size = (vector - vector2) * 2f;
			size = new Vector3(size.x, 1f, size.z);
			vector2.y += (float)(depth * 2);
			Gizmos.color = AstarMath.IntToColor(depth, 1f);
			Gizmos.DrawCube(vector2, size);
			if (!bbtreeBox.IsLeaf)
			{
				this.OnDrawGizmos(bbtreeBox.left, depth + 1);
				this.OnDrawGizmos(bbtreeBox.right, depth + 1);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00035678 File Offset: 0x00033A78
		private static bool NodeIntersectsCircle(TriangleMeshNode node, Vector3 p, float radius)
		{
			return float.IsPositiveInfinity(radius) || (p - node.ClosestPointOnNode(p)).sqrMagnitude < radius * radius;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000356AC File Offset: 0x00033AAC
		private static bool RectIntersectsCircle(IntRect r, Vector3 p, float radius)
		{
			if (float.IsPositiveInfinity(radius))
			{
				return true;
			}
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z) < radius * radius;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00035790 File Offset: 0x00033B90
		private static float SquaredRectPointDistance(IntRect r, Vector3 p)
		{
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z);
		}

		// Token: 0x040004DD RID: 1245
		private BBTree.BBTreeBox[] tree;

		// Token: 0x040004DE RID: 1246
		private TriangleMeshNode[] nodeLookup;

		// Token: 0x040004DF RID: 1247
		private int count;

		// Token: 0x040004E0 RID: 1248
		private int leafNodes;

		// Token: 0x040004E1 RID: 1249
		private const int MaximumLeafSize = 4;

		// Token: 0x020000B5 RID: 181
		private struct BBTreeBox
		{
			// Token: 0x060007A3 RID: 1955 RVA: 0x00035864 File Offset: 0x00033C64
			public BBTreeBox(IntRect rect)
			{
				this.nodeOffset = -1;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x00035890 File Offset: 0x00033C90
			public BBTreeBox(int nodeOffset, IntRect rect)
			{
				this.nodeOffset = nodeOffset;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000358BB File Offset: 0x00033CBB
			public bool IsLeaf
			{
				get
				{
					return this.nodeOffset >= 0;
				}
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x000358CC File Offset: 0x00033CCC
			public bool Contains(Vector3 point)
			{
				Int3 @int = (Int3)point;
				return this.rect.Contains(@int.x, @int.z);
			}

			// Token: 0x040004E2 RID: 1250
			public IntRect rect;

			// Token: 0x040004E3 RID: 1251
			public int nodeOffset;

			// Token: 0x040004E4 RID: 1252
			public int left;

			// Token: 0x040004E5 RID: 1253
			public int right;
		}
	}
}
