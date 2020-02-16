using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000D0 RID: 208
	public class VoxelArea
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x0003C334 File Offset: 0x0003A734
		public VoxelArea(int width, int depth)
		{
			this.width = width;
			this.depth = depth;
			int num = width * depth;
			this.compactCells = new CompactVoxelCell[num];
			this.linkedSpans = new LinkedVoxelSpan[(int)((float)num * 8f) + 15 & -16];
			this.ResetLinkedVoxelSpans();
			int[] array = new int[4];
			array[0] = -1;
			array[2] = 1;
			this.DirectionX = array;
			this.DirectionZ = new int[]
			{
				0,
				width,
				0,
				-width
			};
			this.VectorDirection = new Vector3[]
			{
				Vector3.left,
				Vector3.forward,
				Vector3.right,
				Vector3.back
			};
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0003C410 File Offset: 0x0003A810
		public void Reset()
		{
			this.ResetLinkedVoxelSpans();
			for (int i = 0; i < this.compactCells.Length; i++)
			{
				this.compactCells[i].count = 0u;
				this.compactCells[i].index = 0u;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0003C460 File Offset: 0x0003A860
		private void ResetLinkedVoxelSpans()
		{
			int num = this.linkedSpans.Length;
			this.linkedSpanCount = this.width * this.depth;
			LinkedVoxelSpan linkedVoxelSpan = new LinkedVoxelSpan(uint.MaxValue, uint.MaxValue, -1, -1);
			for (int i = 0; i < num; i++)
			{
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
				i++;
				this.linkedSpans[i] = linkedVoxelSpan;
			}
			this.removedStackCount = 0;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0003C60C File Offset: 0x0003AA0C
		public int GetSpanCountAll()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295u)
				{
					num++;
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0003C678 File Offset: 0x0003AA78
		public int GetSpanCount()
		{
			int num = 0;
			int num2 = this.width * this.depth;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				while (num3 != -1 && this.linkedSpans[num3].bottom != 4294967295u)
				{
					if (this.linkedSpans[num3].area != 0)
					{
						num++;
					}
					num3 = this.linkedSpans[num3].next;
				}
			}
			return num;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0003C6FC File Offset: 0x0003AAFC
		private void PushToSpanRemovedStack(int index)
		{
			if (this.removedStackCount == this.removedStack.Length)
			{
				int[] dst = new int[this.removedStackCount * 4];
				Buffer.BlockCopy(this.removedStack, 0, dst, 0, this.removedStackCount * 4);
				this.removedStack = dst;
			}
			this.removedStack[this.removedStackCount] = index;
			this.removedStackCount++;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0003C764 File Offset: 0x0003AB64
		public void AddLinkedSpan(int index, uint bottom, uint top, int area, int voxelWalkableClimb)
		{
			if (this.linkedSpans[index].bottom == 4294967295u)
			{
				this.linkedSpans[index] = new LinkedVoxelSpan(bottom, top, area);
				return;
			}
			int num = -1;
			int num2 = index;
			while (index != -1)
			{
				if (this.linkedSpans[index].bottom > top)
				{
					break;
				}
				if (this.linkedSpans[index].top < bottom)
				{
					num = index;
					index = this.linkedSpans[index].next;
				}
				else
				{
					bottom = Math.Min(this.linkedSpans[index].bottom, bottom);
					top = Math.Max(this.linkedSpans[index].top, top);
					if (Math.Abs((int)(top - this.linkedSpans[index].top)) <= voxelWalkableClimb)
					{
						area = Math.Max(area, this.linkedSpans[index].area);
					}
					int next = this.linkedSpans[index].next;
					if (num != -1)
					{
						this.linkedSpans[num].next = next;
						this.PushToSpanRemovedStack(index);
						index = next;
					}
					else
					{
						if (next == -1)
						{
							this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area);
							return;
						}
						this.linkedSpans[num2] = this.linkedSpans[next];
						this.PushToSpanRemovedStack(next);
					}
				}
			}
			if (this.linkedSpanCount >= this.linkedSpans.Length)
			{
				LinkedVoxelSpan[] array = this.linkedSpans;
				int num3 = this.linkedSpanCount;
				int num4 = this.removedStackCount;
				this.linkedSpans = new LinkedVoxelSpan[this.linkedSpans.Length * 2];
				this.ResetLinkedVoxelSpans();
				this.linkedSpanCount = num3;
				this.removedStackCount = num4;
				for (int i = 0; i < this.linkedSpanCount; i++)
				{
					this.linkedSpans[i] = array[i];
				}
				Debug.Log("Layer estimate too low, doubling size of buffer.\nThis message is harmless.");
			}
			int num5;
			if (this.removedStackCount > 0)
			{
				this.removedStackCount--;
				num5 = this.removedStack[this.removedStackCount];
			}
			else
			{
				num5 = this.linkedSpanCount;
				this.linkedSpanCount++;
			}
			if (num != -1)
			{
				this.linkedSpans[num5] = new LinkedVoxelSpan(bottom, top, area, this.linkedSpans[num].next);
				this.linkedSpans[num].next = num5;
			}
			else
			{
				this.linkedSpans[num5] = this.linkedSpans[num2];
				this.linkedSpans[num2] = new LinkedVoxelSpan(bottom, top, area, num5);
			}
		}

		// Token: 0x0400056B RID: 1387
		public const uint MaxHeight = 65536u;

		// Token: 0x0400056C RID: 1388
		public const int MaxHeightInt = 65536;

		// Token: 0x0400056D RID: 1389
		public const uint InvalidSpanValue = 4294967295u;

		// Token: 0x0400056E RID: 1390
		public const float AvgSpanLayerCountEstimate = 8f;

		// Token: 0x0400056F RID: 1391
		public readonly int width;

		// Token: 0x04000570 RID: 1392
		public readonly int depth;

		// Token: 0x04000571 RID: 1393
		public CompactVoxelSpan[] compactSpans;

		// Token: 0x04000572 RID: 1394
		public CompactVoxelCell[] compactCells;

		// Token: 0x04000573 RID: 1395
		public int compactSpanCount;

		// Token: 0x04000574 RID: 1396
		public ushort[] tmpUShortArr;

		// Token: 0x04000575 RID: 1397
		public int[] areaTypes;

		// Token: 0x04000576 RID: 1398
		public ushort[] dist;

		// Token: 0x04000577 RID: 1399
		public ushort maxDistance;

		// Token: 0x04000578 RID: 1400
		public int maxRegions;

		// Token: 0x04000579 RID: 1401
		public int[] DirectionX;

		// Token: 0x0400057A RID: 1402
		public int[] DirectionZ;

		// Token: 0x0400057B RID: 1403
		public Vector3[] VectorDirection;

		// Token: 0x0400057C RID: 1404
		private int linkedSpanCount;

		// Token: 0x0400057D RID: 1405
		public LinkedVoxelSpan[] linkedSpans;

		// Token: 0x0400057E RID: 1406
		private int[] removedStack = new int[128];

		// Token: 0x0400057F RID: 1407
		private int removedStackCount;
	}
}
