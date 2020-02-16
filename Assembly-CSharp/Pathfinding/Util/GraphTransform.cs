using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000BA RID: 186
	public class GraphTransform : IMovementPlane, ITransform
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0003665C File Offset: 0x00034A5C
		public GraphTransform(Matrix4x4 matrix)
		{
			this.matrix = matrix;
			this.inverseMatrix = matrix.inverse;
			this.identity = matrix.isIdentity;
			this.onlyTranslational = GraphTransform.MatrixIsTranslational(matrix);
			this.up = matrix.MultiplyVector(Vector3.up).normalized;
			this.translation = matrix.MultiplyPoint3x4(Vector3.zero);
			this.i3translation = (Int3)this.translation;
			this.rotation = Quaternion.LookRotation(this.TransformVector(Vector3.forward), this.TransformVector(Vector3.up));
			this.inverseRotation = Quaternion.Inverse(this.rotation);
			this.isXY = (this.rotation == Quaternion.Euler(-90f, 0f, 0f));
			this.isXZ = (this.rotation == Quaternion.Euler(0f, 0f, 0f));
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00036755 File Offset: 0x00034B55
		public Vector3 WorldUpAtGraphPosition(Vector3 point)
		{
			return this.up;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00036760 File Offset: 0x00034B60
		private static bool MatrixIsTranslational(Matrix4x4 matrix)
		{
			return matrix.GetColumn(0) == new Vector4(1f, 0f, 0f, 0f) && matrix.GetColumn(1) == new Vector4(0f, 1f, 0f, 0f) && matrix.GetColumn(2) == new Vector4(0f, 0f, 1f, 0f) && matrix.m33 == 1f;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00036800 File Offset: 0x00034C00
		public Vector3 Transform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point + this.translation;
			}
			return this.matrix.MultiplyPoint3x4(point);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00036834 File Offset: 0x00034C34
		public Vector3 TransformVector(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point;
			}
			return this.matrix.MultiplyVector(point);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00036860 File Offset: 0x00034C60
		public void Transform(Int3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.i3translation;
				}
			}
			else
			{
				for (int j = arr.Length - 1; j >= 0; j--)
				{
					arr[j] = (Int3)this.matrix.MultiplyPoint3x4((Vector3)arr[j]);
				}
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000368F8 File Offset: 0x00034CF8
		public void Transform(Vector3[] arr)
		{
			if (this.onlyTranslational)
			{
				for (int i = arr.Length - 1; i >= 0; i--)
				{
					arr[i] += this.translation;
				}
			}
			else
			{
				for (int j = arr.Length - 1; j >= 0; j--)
				{
					arr[j] = this.matrix.MultiplyPoint3x4(arr[j]);
				}
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00036984 File Offset: 0x00034D84
		public Vector3 InverseTransform(Vector3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.translation;
			}
			return this.inverseMatrix.MultiplyPoint3x4(point);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000369B8 File Offset: 0x00034DB8
		public Int3 InverseTransform(Int3 point)
		{
			if (this.onlyTranslational)
			{
				return point - this.i3translation;
			}
			return (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)point);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000369F8 File Offset: 0x00034DF8
		public void InverseTransform(Int3[] arr)
		{
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				arr[i] = (Int3)this.inverseMatrix.MultiplyPoint3x4((Vector3)arr[i]);
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00036A4B File Offset: 0x00034E4B
		public static GraphTransform operator *(GraphTransform lhs, Matrix4x4 rhs)
		{
			return new GraphTransform(lhs.matrix * rhs);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00036A5E File Offset: 0x00034E5E
		public static GraphTransform operator *(Matrix4x4 lhs, GraphTransform rhs)
		{
			return new GraphTransform(lhs * rhs.matrix);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00036A74 File Offset: 0x00034E74
		public Bounds Transform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center + this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.Transform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.Transform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.Transform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.Transform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00036D04 File Offset: 0x00035104
		public Bounds InverseTransform(Bounds bounds)
		{
			if (this.onlyTranslational)
			{
				return new Bounds(bounds.center - this.translation, bounds.size);
			}
			Vector3[] array = ArrayPool<Vector3>.Claim(8);
			Vector3 extents = bounds.extents;
			array[0] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, extents.z));
			array[1] = this.InverseTransform(bounds.center + new Vector3(extents.x, extents.y, -extents.z));
			array[2] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, extents.z));
			array[3] = this.InverseTransform(bounds.center + new Vector3(extents.x, -extents.y, -extents.z));
			array[4] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, extents.z));
			array[5] = this.InverseTransform(bounds.center + new Vector3(-extents.x, extents.y, -extents.z));
			array[6] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, extents.z));
			array[7] = this.InverseTransform(bounds.center + new Vector3(-extents.x, -extents.y, -extents.z));
			Vector3 vector = array[0];
			Vector3 vector2 = array[0];
			for (int i = 1; i < 8; i++)
			{
				vector = Vector3.Min(vector, array[i]);
				vector2 = Vector3.Max(vector2, array[i]);
			}
			ArrayPool<Vector3>.Release(ref array, false);
			return new Bounds((vector + vector2) * 0.5f, vector2 - vector);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00036F94 File Offset: 0x00035394
		Vector2 IMovementPlane.ToPlane(Vector3 point)
		{
			if (this.isXY)
			{
				return new Vector2(point.x, point.y);
			}
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			return new Vector2(point.x, point.z);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00036FEC File Offset: 0x000353EC
		Vector2 IMovementPlane.ToPlane(Vector3 point, out float elevation)
		{
			if (!this.isXZ)
			{
				point = this.inverseRotation * point;
			}
			elevation = point.y;
			return new Vector2(point.x, point.z);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00037023 File Offset: 0x00035423
		Vector3 IMovementPlane.ToWorld(Vector2 point, float elevation)
		{
			return this.rotation * new Vector3(point.x, elevation, point.y);
		}

		// Token: 0x040004F8 RID: 1272
		public readonly bool identity;

		// Token: 0x040004F9 RID: 1273
		public readonly bool onlyTranslational;

		// Token: 0x040004FA RID: 1274
		private readonly bool isXY;

		// Token: 0x040004FB RID: 1275
		private readonly bool isXZ;

		// Token: 0x040004FC RID: 1276
		private readonly Matrix4x4 matrix;

		// Token: 0x040004FD RID: 1277
		private readonly Matrix4x4 inverseMatrix;

		// Token: 0x040004FE RID: 1278
		private readonly Vector3 up;

		// Token: 0x040004FF RID: 1279
		private readonly Vector3 translation;

		// Token: 0x04000500 RID: 1280
		private readonly Int3 i3translation;

		// Token: 0x04000501 RID: 1281
		private readonly Quaternion rotation;

		// Token: 0x04000502 RID: 1282
		private readonly Quaternion inverseRotation;

		// Token: 0x04000503 RID: 1283
		public static readonly GraphTransform identityTransform = new GraphTransform(Matrix4x4.identity);
	}
}
