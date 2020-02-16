using System;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000D2 RID: 210
	[Obsolete("Use RasterizationMesh instead")]
	public class ExtraMesh : RasterizationMesh
	{
		// Token: 0x06000844 RID: 2116 RVA: 0x0003CBE3 File Offset: 0x0003AFE3
		public ExtraMesh(Vector3[] vertices, int[] triangles, Bounds bounds) : base(vertices, triangles, bounds)
		{
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0003CBEE File Offset: 0x0003AFEE
		public ExtraMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix) : base(vertices, triangles, bounds, matrix)
		{
		}
	}
}
