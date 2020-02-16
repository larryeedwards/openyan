using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000032 RID: 50
	public class GraphUpdateShape
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000EB9B File Offset: 0x0000CF9B
		public GraphUpdateShape()
		{
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000EBC4 File Offset: 0x0000CFC4
		public GraphUpdateShape(Vector3[] points, bool convex, Matrix4x4 matrix, float minimumHeight)
		{
			this.convex = convex;
			this.points = points;
			this.origin = matrix.MultiplyPoint3x4(Vector3.zero);
			this.right = matrix.MultiplyPoint3x4(Vector3.right) - this.origin;
			this.up = matrix.MultiplyPoint3x4(Vector3.up) - this.origin;
			this.forward = matrix.MultiplyPoint3x4(Vector3.forward) - this.origin;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000EC77 File Offset: 0x0000D077
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000EC7F File Offset: 0x0000D07F
		public Vector3[] points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				if (this.convex)
				{
					this.CalculateConvexHull();
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000EC99 File Offset: 0x0000D099
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000ECA1 File Offset: 0x0000D0A1
		public bool convex
		{
			get
			{
				return this._convex;
			}
			set
			{
				if (this._convex != value && value)
				{
					this.CalculateConvexHull();
				}
				this._convex = value;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000ECC2 File Offset: 0x0000D0C2
		private void CalculateConvexHull()
		{
			this._convexPoints = ((this.points == null) ? null : Polygon.ConvexHullXZ(this.points));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000ECE8 File Offset: 0x0000D0E8
		public Bounds GetBounds()
		{
			return GraphUpdateShape.GetBounds((!this.convex) ? this.points : this._convexPoints, this.right, this.up, this.forward, this.origin, this.minimumHeight);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000ED34 File Offset: 0x0000D134
		public static Bounds GetBounds(Vector3[] points, Matrix4x4 matrix, float minimumHeight)
		{
			Vector3 b = matrix.MultiplyPoint3x4(Vector3.zero);
			Vector3 vector = matrix.MultiplyPoint3x4(Vector3.right) - b;
			Vector3 vector2 = matrix.MultiplyPoint3x4(Vector3.up) - b;
			Vector3 vector3 = matrix.MultiplyPoint3x4(Vector3.forward) - b;
			return GraphUpdateShape.GetBounds(points, vector, vector2, vector3, b, minimumHeight);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000ED94 File Offset: 0x0000D194
		private static Bounds GetBounds(Vector3[] points, Vector3 right, Vector3 up, Vector3 forward, Vector3 origin, float minimumHeight)
		{
			if (points == null || points.Length == 0)
			{
				return default(Bounds);
			}
			float num = points[0].y;
			float num2 = points[0].y;
			for (int i = 0; i < points.Length; i++)
			{
				num = Mathf.Min(num, points[i].y);
				num2 = Mathf.Max(num2, points[i].y);
			}
			float num3 = Mathf.Max(minimumHeight - (num2 - num), 0f) * 0.5f;
			num -= num3;
			num2 += num3;
			Vector3 vector = right * points[0].x + up * points[0].y + forward * points[0].z;
			Vector3 vector2 = vector;
			for (int j = 0; j < points.Length; j++)
			{
				Vector3 a = right * points[j].x + forward * points[j].z;
				Vector3 rhs = a + up * num;
				Vector3 rhs2 = a + up * num2;
				vector = Vector3.Min(vector, rhs);
				vector = Vector3.Min(vector, rhs2);
				vector2 = Vector3.Max(vector2, rhs);
				vector2 = Vector3.Max(vector2, rhs2);
			}
			return new Bounds((vector + vector2) * 0.5f + origin, vector2 - vector);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000EF2E File Offset: 0x0000D32E
		public bool Contains(GraphNode node)
		{
			return this.Contains((Vector3)node.position);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000EF44 File Offset: 0x0000D344
		public bool Contains(Vector3 point)
		{
			point -= this.origin;
			Vector3 p = new Vector3(Vector3.Dot(point, this.right) / this.right.sqrMagnitude, 0f, Vector3.Dot(point, this.forward) / this.forward.sqrMagnitude);
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPointXZ(this._points, p);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (VectorMath.RightOrColinearXZ(this._convexPoints[i], this._convexPoints[num], p))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x04000171 RID: 369
		private Vector3[] _points;

		// Token: 0x04000172 RID: 370
		private Vector3[] _convexPoints;

		// Token: 0x04000173 RID: 371
		private bool _convex;

		// Token: 0x04000174 RID: 372
		private Vector3 right = Vector3.right;

		// Token: 0x04000175 RID: 373
		private Vector3 forward = Vector3.forward;

		// Token: 0x04000176 RID: 374
		private Vector3 up = Vector3.up;

		// Token: 0x04000177 RID: 375
		private Vector3 origin;

		// Token: 0x04000178 RID: 376
		public float minimumHeight;
	}
}
