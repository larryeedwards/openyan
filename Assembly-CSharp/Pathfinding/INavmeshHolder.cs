using System;

namespace Pathfinding
{
	// Token: 0x020000AF RID: 175
	public interface INavmeshHolder : ITransformedGraph, INavmesh
	{
		// Token: 0x06000733 RID: 1843
		Int3 GetVertex(int i);

		// Token: 0x06000734 RID: 1844
		Int3 GetVertexInGraphSpace(int i);

		// Token: 0x06000735 RID: 1845
		int GetVertexArrayIndex(int index);

		// Token: 0x06000736 RID: 1846
		void GetTileCoordinates(int tileIndex, out int x, out int z);
	}
}
