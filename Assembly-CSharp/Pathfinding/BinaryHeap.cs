using System;

namespace Pathfinding
{
	// Token: 0x0200003A RID: 58
	public class BinaryHeap
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00011388 File Offset: 0x0000F788
		public BinaryHeap(int capacity)
		{
			capacity = BinaryHeap.RoundUpToNextMultipleMod1(capacity);
			this.heap = new BinaryHeap.Tuple[capacity];
			this.numberOfItems = 0;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000113B6 File Offset: 0x0000F7B6
		public bool isEmpty
		{
			get
			{
				return this.numberOfItems <= 0;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000113C4 File Offset: 0x0000F7C4
		private static int RoundUpToNextMultipleMod1(int v)
		{
			return v + (4 - (v - 1) % 4) % 4;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000113D4 File Offset: 0x0000F7D4
		public void Clear()
		{
			for (int i = 0; i < this.numberOfItems; i++)
			{
				this.heap[i].node.heapIndex = ushort.MaxValue;
			}
			this.numberOfItems = 0;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001141A File Offset: 0x0000F81A
		internal PathNode GetNode(int i)
		{
			return this.heap[i].node;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001142D File Offset: 0x0000F82D
		internal void SetF(int i, uint f)
		{
			this.heap[i].F = f;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00011444 File Offset: 0x0000F844
		private void Expand()
		{
			int num = Math.Max(this.heap.Length + 4, Math.Min(65533, (int)Math.Round((double)((float)this.heap.Length * this.growthFactor))));
			num = BinaryHeap.RoundUpToNextMultipleMod1(num);
			if (num > 65534)
			{
				throw new Exception("Binary Heap Size really large (>65534). A heap size this large is probably the cause of pathfinding running in an infinite loop. ");
			}
			BinaryHeap.Tuple[] array = new BinaryHeap.Tuple[num];
			this.heap.CopyTo(array, 0);
			this.heap = array;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000114BC File Offset: 0x0000F8BC
		public void Add(PathNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.heapIndex != 65535)
			{
				this.DecreaseKey(this.heap[(int)node.heapIndex], node.heapIndex);
				return;
			}
			if (this.numberOfItems == this.heap.Length)
			{
				this.Expand();
			}
			this.DecreaseKey(new BinaryHeap.Tuple(0u, node), (ushort)this.numberOfItems);
			this.numberOfItems++;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011548 File Offset: 0x0000F948
		private void DecreaseKey(BinaryHeap.Tuple node, ushort index)
		{
			int num = (int)index;
			uint num2 = node.F = node.node.F;
			uint g = node.node.G;
			while (num != 0)
			{
				int num3 = (num - 1) / 4;
				if (num2 >= this.heap[num3].F && (num2 != this.heap[num3].F || g <= this.heap[num3].node.G))
				{
					break;
				}
				this.heap[num] = this.heap[num3];
				this.heap[num].node.heapIndex = (ushort)num;
				num = num3;
			}
			this.heap[num] = node;
			node.node.heapIndex = (ushort)num;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00011644 File Offset: 0x0000FA44
		public PathNode Remove()
		{
			PathNode node = this.heap[0].node;
			node.heapIndex = ushort.MaxValue;
			this.numberOfItems--;
			if (this.numberOfItems == 0)
			{
				return node;
			}
			BinaryHeap.Tuple tuple = this.heap[this.numberOfItems];
			uint g = tuple.node.G;
			int num = 0;
			for (;;)
			{
				int num2 = num;
				uint num3 = tuple.F;
				int num4 = num2 * 4 + 1;
				if (num4 <= this.numberOfItems)
				{
					uint f = this.heap[num4].F;
					uint f2 = this.heap[num4 + 1].F;
					uint f3 = this.heap[num4 + 2].F;
					uint f4 = this.heap[num4 + 3].F;
					if (num4 < this.numberOfItems && (f < num3 || (f == num3 && this.heap[num4].node.G < g)))
					{
						num3 = f;
						num = num4;
					}
					if (num4 + 1 < this.numberOfItems && (f2 < num3 || (f2 == num3 && this.heap[num4 + 1].node.G < ((num != num2) ? this.heap[num].node.G : g))))
					{
						num3 = f2;
						num = num4 + 1;
					}
					if (num4 + 2 < this.numberOfItems && (f3 < num3 || (f3 == num3 && this.heap[num4 + 2].node.G < ((num != num2) ? this.heap[num].node.G : g))))
					{
						num3 = f3;
						num = num4 + 2;
					}
					if (num4 + 3 < this.numberOfItems && (f4 < num3 || (f4 == num3 && this.heap[num4 + 3].node.G < ((num != num2) ? this.heap[num].node.G : g))))
					{
						num = num4 + 3;
					}
				}
				if (num2 == num)
				{
					break;
				}
				this.heap[num2] = this.heap[num];
				this.heap[num2].node.heapIndex = (ushort)num2;
			}
			this.heap[num] = tuple;
			tuple.node.heapIndex = (ushort)num;
			return node;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011920 File Offset: 0x0000FD20
		private void Validate()
		{
			for (int i = 1; i < this.numberOfItems; i++)
			{
				int num = (i - 1) / 4;
				if (this.heap[num].F > this.heap[i].F)
				{
					throw new Exception(string.Concat(new object[]
					{
						"Invalid state at ",
						i,
						":",
						num,
						" ( ",
						this.heap[num].F,
						" > ",
						this.heap[i].F,
						" ) "
					}));
				}
				if ((int)this.heap[i].node.heapIndex != i)
				{
					throw new Exception("Invalid heap index");
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00011A18 File Offset: 0x0000FE18
		public void Rebuild()
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				BinaryHeap.Tuple tuple = this.heap[i];
				uint f = tuple.F;
				while (num != 1)
				{
					int num2 = num / 4;
					if (f >= this.heap[num2].F)
					{
						break;
					}
					this.heap[num] = this.heap[num2];
					this.heap[num].node.heapIndex = (ushort)num;
					this.heap[num2] = tuple;
					this.heap[num2].node.heapIndex = (ushort)num2;
					num = num2;
				}
			}
		}

		// Token: 0x040001BE RID: 446
		public int numberOfItems;

		// Token: 0x040001BF RID: 447
		public float growthFactor = 2f;

		// Token: 0x040001C0 RID: 448
		private const int D = 4;

		// Token: 0x040001C1 RID: 449
		private const bool SortGScores = true;

		// Token: 0x040001C2 RID: 450
		public const ushort NotInHeap = 65535;

		// Token: 0x040001C3 RID: 451
		private BinaryHeap.Tuple[] heap;

		// Token: 0x0200003B RID: 59
		private struct Tuple
		{
			// Token: 0x060002C2 RID: 706 RVA: 0x00011AF6 File Offset: 0x0000FEF6
			public Tuple(uint f, PathNode node)
			{
				this.F = f;
				this.node = node;
			}

			// Token: 0x040001C4 RID: 452
			public PathNode node;

			// Token: 0x040001C5 RID: 453
			public uint F;
		}
	}
}
