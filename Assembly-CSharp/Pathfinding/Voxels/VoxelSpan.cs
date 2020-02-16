using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000DA RID: 218
	public class VoxelSpan
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x0003CD9E File Offset: 0x0003B19E
		public VoxelSpan(uint b, uint t, int area)
		{
			this.bottom = b;
			this.top = t;
			this.area = area;
		}

		// Token: 0x0400059E RID: 1438
		public uint bottom;

		// Token: 0x0400059F RID: 1439
		public uint top;

		// Token: 0x040005A0 RID: 1440
		public VoxelSpan next;

		// Token: 0x040005A1 RID: 1441
		public int area;
	}
}
