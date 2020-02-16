using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000D1 RID: 209
	public struct LinkedVoxelSpan
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0003CA5C File Offset: 0x0003AE5C
		public LinkedVoxelSpan(uint bottom, uint top, int area)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = -1;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0003CA7A File Offset: 0x0003AE7A
		public LinkedVoxelSpan(uint bottom, uint top, int area, int next)
		{
			this.bottom = bottom;
			this.top = top;
			this.area = area;
			this.next = next;
		}

		// Token: 0x04000580 RID: 1408
		public uint bottom;

		// Token: 0x04000581 RID: 1409
		public uint top;

		// Token: 0x04000582 RID: 1410
		public int next;

		// Token: 0x04000583 RID: 1411
		public int area;
	}
}
