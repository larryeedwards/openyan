using System;

namespace Pathfinding.Voxels
{
	// Token: 0x020000D8 RID: 216
	public struct CompactVoxelCell
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x0003CD29 File Offset: 0x0003B129
		public CompactVoxelCell(uint i, uint c)
		{
			this.index = i;
			this.count = c;
		}

		// Token: 0x04000598 RID: 1432
		public uint index;

		// Token: 0x04000599 RID: 1433
		public uint count;
	}
}
