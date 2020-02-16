using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000D9 RID: 217
	public struct CompactVoxelSpan
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x0003CD39 File Offset: 0x0003B139
		public CompactVoxelSpan(ushort bottom, uint height)
		{
			this.con = 24u;
			this.y = bottom;
			this.h = height;
			this.reg = 0;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0003CD58 File Offset: 0x0003B158
		public void SetConnection(int dir, uint value)
		{
			int num = dir * 6;
			this.con = (uint)(((ulong)this.con & (ulong)(~(63L << (num & 31)))) | (ulong)((ulong)(value & 63u) << num));
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0003CD8C File Offset: 0x0003B18C
		public int GetConnection(int dir)
		{
			return (int)this.con >> dir * 6 & 63;
		}

		// Token: 0x0400059A RID: 1434
		public ushort y;

		// Token: 0x0400059B RID: 1435
		public uint con;

		// Token: 0x0400059C RID: 1436
		public uint h;

		// Token: 0x0400059D RID: 1437
		public int reg;
	}
}
