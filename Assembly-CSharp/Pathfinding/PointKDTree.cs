using System;
using System.Collections.Generic;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x020000C1 RID: 193
	public class PointKDTree
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x00037EB4 File Offset: 0x000362B4
		public PointKDTree()
		{
			this.tree[1] = new PointKDTree.Node
			{
				data = this.GetOrCreateList()
			};
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00037F11 File Offset: 0x00036311
		public void Add(GraphNode node)
		{
			this.numNodes++;
			this.Add(node, 1, 0);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00037F2C File Offset: 0x0003632C
		public void Rebuild(GraphNode[] nodes, int start, int end)
		{
			if (start < 0 || end < start || end > nodes.Length)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < this.tree.Length; i++)
			{
				GraphNode[] data = this.tree[i].data;
				if (data != null)
				{
					for (int j = 0; j < 21; j++)
					{
						data[j] = null;
					}
					this.arrayCache.Push(data);
					this.tree[i].data = null;
				}
			}
			this.numNodes = end - start;
			this.Build(1, new List<GraphNode>(nodes), start, end);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00037FD4 File Offset: 0x000363D4
		private GraphNode[] GetOrCreateList()
		{
			return (this.arrayCache.Count <= 0) ? new GraphNode[21] : this.arrayCache.Pop();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00038000 File Offset: 0x00036400
		private int Size(int index)
		{
			return (this.tree[index].data == null) ? (this.Size(2 * index) + this.Size(2 * index + 1)) : ((int)this.tree[index].count);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00038050 File Offset: 0x00036450
		private void CollectAndClear(int index, List<GraphNode> buffer)
		{
			GraphNode[] data = this.tree[index].data;
			ushort count = this.tree[index].count;
			if (data != null)
			{
				this.tree[index] = default(PointKDTree.Node);
				for (int i = 0; i < (int)count; i++)
				{
					buffer.Add(data[i]);
					data[i] = null;
				}
				this.arrayCache.Push(data);
			}
			else
			{
				this.CollectAndClear(index * 2, buffer);
				this.CollectAndClear(index * 2 + 1, buffer);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000380E7 File Offset: 0x000364E7
		private static int MaxAllowedSize(int numNodes, int depth)
		{
			return Math.Min(5 * numNodes / 2 >> depth, 3 * numNodes / 4);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000380FD File Offset: 0x000364FD
		private void Rebalance(int index)
		{
			this.CollectAndClear(index, this.largeList);
			this.Build(index, this.largeList, 0, this.largeList.Count);
			this.largeList.ClearFast<GraphNode>();
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00038130 File Offset: 0x00036530
		private void EnsureSize(int index)
		{
			if (index >= this.tree.Length)
			{
				PointKDTree.Node[] array = new PointKDTree.Node[Math.Max(index + 1, this.tree.Length * 2)];
				this.tree.CopyTo(array, 0);
				this.tree = array;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00038178 File Offset: 0x00036578
		private void Build(int index, List<GraphNode> nodes, int start, int end)
		{
			this.EnsureSize(index);
			if (end - start <= 10)
			{
				GraphNode[] array = this.tree[index].data = this.GetOrCreateList();
				this.tree[index].count = (ushort)(end - start);
				for (int i = start; i < end; i++)
				{
					array[i - start] = nodes[i];
				}
			}
			else
			{
				Int3 position;
				Int3 lhs = position = nodes[start].position;
				for (int j = start; j < end; j++)
				{
					Int3 position2 = nodes[j].position;
					position = new Int3(Math.Min(position.x, position2.x), Math.Min(position.y, position2.y), Math.Min(position.z, position2.z));
					lhs = new Int3(Math.Max(lhs.x, position2.x), Math.Max(lhs.y, position2.y), Math.Max(lhs.z, position2.z));
				}
				Int3 @int = lhs - position;
				int num = (@int.x <= @int.y) ? ((@int.y <= @int.z) ? 2 : 1) : ((@int.x <= @int.z) ? 2 : 0);
				nodes.Sort(start, end - start, PointKDTree.comparers[num]);
				int num2 = (start + end) / 2;
				this.tree[index].split = (nodes[num2 - 1].position[num] + nodes[num2].position[num] + 1) / 2;
				this.tree[index].splitAxis = (byte)num;
				this.Build(index * 2, nodes, start, num2);
				this.Build(index * 2 + 1, nodes, num2, end);
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00038388 File Offset: 0x00036788
		private void Add(GraphNode point, int index, int depth = 0)
		{
			while (this.tree[index].data == null)
			{
				index = 2 * index + ((point.position[(int)this.tree[index].splitAxis] >= this.tree[index].split) ? 1 : 0);
				depth++;
			}
			GraphNode[] data = this.tree[index].data;
			PointKDTree.Node[] array = this.tree;
			int num = index;
			ushort count;
			array[num].count = (count = array[num].count) + 1;
			data[(int)count] = point;
			if (this.tree[index].count >= 21)
			{
				int num2 = 0;
				while (depth - num2 > 0 && this.Size(index >> num2) > PointKDTree.MaxAllowedSize(this.numNodes, depth - num2))
				{
					num2++;
				}
				this.Rebalance(index >> num2);
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0003847C File Offset: 0x0003687C
		public GraphNode GetNearest(Int3 point, NNConstraint constraint)
		{
			GraphNode result = null;
			long maxValue = long.MaxValue;
			this.GetNearestInternal(1, point, constraint, ref result, ref maxValue);
			return result;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000384A4 File Offset: 0x000368A4
		private void GetNearestInternal(int index, Int3 point, NNConstraint constraint, ref GraphNode best, ref long bestSqrDist)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					long sqrMagnitudeLong = (data[i].position - point).sqrMagnitudeLong;
					if (sqrMagnitudeLong < bestSqrDist && (constraint == null || constraint.Suitable(data[i])))
					{
						bestSqrDist = sqrMagnitudeLong;
						best = data[i];
					}
				}
			}
			else
			{
				long num = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
				int num2 = 2 * index + ((num >= 0L) ? 1 : 0);
				this.GetNearestInternal(num2, point, constraint, ref best, ref bestSqrDist);
				if (num * num < bestSqrDist)
				{
					this.GetNearestInternal(num2 ^ 1, point, constraint, ref best, ref bestSqrDist);
				}
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0003859E File Offset: 0x0003699E
		public void GetInRange(Int3 point, long sqrRadius, List<GraphNode> buffer)
		{
			this.GetInRangeInternal(1, point, sqrRadius, buffer);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000385AC File Offset: 0x000369AC
		private void GetInRangeInternal(int index, Int3 point, long sqrRadius, List<GraphNode> buffer)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					long sqrMagnitudeLong = (data[i].position - point).sqrMagnitudeLong;
					if (sqrMagnitudeLong < sqrRadius)
					{
						buffer.Add(data[i]);
					}
				}
			}
			else
			{
				long num = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
				int num2 = 2 * index + ((num >= 0L) ? 1 : 0);
				this.GetInRangeInternal(num2, point, sqrRadius, buffer);
				if (num * num < sqrRadius)
				{
					this.GetInRangeInternal(num2 ^ 1, point, sqrRadius, buffer);
				}
			}
		}

		// Token: 0x04000529 RID: 1321
		public const int LeafSize = 10;

		// Token: 0x0400052A RID: 1322
		public const int LeafArraySize = 21;

		// Token: 0x0400052B RID: 1323
		private PointKDTree.Node[] tree = new PointKDTree.Node[16];

		// Token: 0x0400052C RID: 1324
		private int numNodes;

		// Token: 0x0400052D RID: 1325
		private readonly List<GraphNode> largeList = new List<GraphNode>();

		// Token: 0x0400052E RID: 1326
		private readonly Stack<GraphNode[]> arrayCache = new Stack<GraphNode[]>();

		// Token: 0x0400052F RID: 1327
		private static readonly IComparer<GraphNode>[] comparers = new IComparer<GraphNode>[]
		{
			new PointKDTree.CompareX(),
			new PointKDTree.CompareY(),
			new PointKDTree.CompareZ()
		};

		// Token: 0x020000C2 RID: 194
		private struct Node
		{
			// Token: 0x04000530 RID: 1328
			public GraphNode[] data;

			// Token: 0x04000531 RID: 1329
			public int split;

			// Token: 0x04000532 RID: 1330
			public ushort count;

			// Token: 0x04000533 RID: 1331
			public byte splitAxis;
		}

		// Token: 0x020000C3 RID: 195
		private class CompareX : IComparer<GraphNode>
		{
			// Token: 0x060007EE RID: 2030 RVA: 0x000386B7 File Offset: 0x00036AB7
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.x.CompareTo(rhs.position.x);
			}
		}

		// Token: 0x020000C4 RID: 196
		private class CompareY : IComparer<GraphNode>
		{
			// Token: 0x060007F0 RID: 2032 RVA: 0x000386DC File Offset: 0x00036ADC
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.y.CompareTo(rhs.position.y);
			}
		}

		// Token: 0x020000C5 RID: 197
		private class CompareZ : IComparer<GraphNode>
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x00038701 File Offset: 0x00036B01
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.z.CompareTo(rhs.position.z);
			}
		}
	}
}
