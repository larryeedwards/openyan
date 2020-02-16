using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000D3 RID: 211
	public class RasterizationMesh
	{
		// Token: 0x06000846 RID: 2118 RVA: 0x0003CA99 File Offset: 0x0003AE99
		public RasterizationMesh()
		{
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0003CAA4 File Offset: 0x0003AEA4
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds)
		{
			this.matrix = Matrix4x4.identity;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0003CAF8 File Offset: 0x0003AEF8
		public RasterizationMesh(Vector3[] vertices, int[] triangles, Bounds bounds, Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.vertices = vertices;
			this.numVertices = vertices.Length;
			this.triangles = triangles;
			this.numTriangles = triangles.Length;
			this.bounds = bounds;
			this.original = null;
			this.area = 0;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0003CB48 File Offset: 0x0003AF48
		public void RecalculateBounds()
		{
			Bounds bounds = new Bounds(this.matrix.MultiplyPoint3x4(this.vertices[0]), Vector3.zero);
			for (int i = 1; i < this.numVertices; i++)
			{
				bounds.Encapsulate(this.matrix.MultiplyPoint3x4(this.vertices[i]));
			}
			this.bounds = bounds;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0003CBBE File Offset: 0x0003AFBE
		public void Pool()
		{
			if (this.pool)
			{
				ArrayPool<int>.Release(ref this.triangles, false);
				ArrayPool<Vector3>.Release(ref this.vertices, false);
			}
		}

		// Token: 0x04000584 RID: 1412
		public MeshFilter original;

		// Token: 0x04000585 RID: 1413
		public int area;

		// Token: 0x04000586 RID: 1414
		public Vector3[] vertices;

		// Token: 0x04000587 RID: 1415
		public int[] triangles;

		// Token: 0x04000588 RID: 1416
		public int numVertices;

		// Token: 0x04000589 RID: 1417
		public int numTriangles;

		// Token: 0x0400058A RID: 1418
		public Bounds bounds;

		// Token: 0x0400058B RID: 1419
		public Matrix4x4 matrix;

		// Token: 0x0400058C RID: 1420
		public bool pool;
	}
}
