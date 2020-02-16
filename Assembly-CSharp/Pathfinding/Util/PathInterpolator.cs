using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200004F RID: 79
	public class PathInterpolator
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000159DC File Offset: 0x00013DDC
		public virtual Vector3 position
		{
			get
			{
				float t = (this.currentSegmentLength <= 0.0001f) ? 0f : ((this.currentDistance - this.distanceToSegmentStart) / this.currentSegmentLength);
				return Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], t);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00015A42 File Offset: 0x00013E42
		public Vector3 tangent
		{
			get
			{
				return this.path[this.segmentIndex + 1] - this.path[this.segmentIndex];
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00015A6D File Offset: 0x00013E6D
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00015A7C File Offset: 0x00013E7C
		public float remainingDistance
		{
			get
			{
				return this.totalDistance - this.distance;
			}
			set
			{
				this.distance = this.totalDistance - value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00015A8C File Offset: 0x00013E8C
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00015A94 File Offset: 0x00013E94
		public float distance
		{
			get
			{
				return this.currentDistance;
			}
			set
			{
				this.currentDistance = value;
				while (this.currentDistance < this.distanceToSegmentStart && this.segmentIndex > 0)
				{
					this.PrevSegment();
				}
				while (this.currentDistance > this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.path.Count - 2)
				{
					this.NextSegment();
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00015B0B File Offset: 0x00013F0B
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00015B13 File Offset: 0x00013F13
		public int segmentIndex { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00015B1C File Offset: 0x00013F1C
		public bool valid
		{
			get
			{
				return this.path != null;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00015B2C File Offset: 0x00013F2C
		public void SetPath(List<Vector3> path)
		{
			this.path = path;
			this.currentDistance = 0f;
			this.segmentIndex = 0;
			this.distanceToSegmentStart = 0f;
			if (path == null)
			{
				this.totalDistance = float.PositiveInfinity;
				this.currentSegmentLength = float.PositiveInfinity;
				return;
			}
			if (path.Count < 2)
			{
				throw new ArgumentException("Path must have a length of at least 2");
			}
			this.currentSegmentLength = (path[1] - path[0]).magnitude;
			this.totalDistance = 0f;
			Vector3 b = path[0];
			for (int i = 1; i < path.Count; i++)
			{
				Vector3 vector = path[i];
				this.totalDistance += (vector - b).magnitude;
				b = vector;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00015C04 File Offset: 0x00014004
		public void MoveToSegment(int index, float fractionAlongSegment)
		{
			if (this.path == null)
			{
				return;
			}
			if (index < 0 || index >= this.path.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			while (this.segmentIndex > index)
			{
				this.PrevSegment();
			}
			while (this.segmentIndex < index)
			{
				this.NextSegment();
			}
			this.distance = this.distanceToSegmentStart + Mathf.Clamp01(fractionAlongSegment) * this.currentSegmentLength;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00015C8C File Offset: 0x0001408C
		public void MoveToClosestPoint(Vector3 point)
		{
			if (this.path == null)
			{
				return;
			}
			float num = float.PositiveInfinity;
			float fractionAlongSegment = 0f;
			int index = 0;
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				float num2 = VectorMath.ClosestPointOnLineFactor(this.path[i], this.path[i + 1], point);
				Vector3 b = Vector3.Lerp(this.path[i], this.path[i + 1], num2);
				float sqrMagnitude = (point - b).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					fractionAlongSegment = num2;
					index = i;
				}
			}
			this.MoveToSegment(index, fractionAlongSegment);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00015D44 File Offset: 0x00014144
		public void MoveToLocallyClosestPoint(Vector3 point, bool allowForwards = true, bool allowBackwards = true)
		{
			if (this.path == null)
			{
				return;
			}
			while (allowForwards && this.segmentIndex < this.path.Count - 2 && (this.path[this.segmentIndex + 1] - point).sqrMagnitude <= (this.path[this.segmentIndex] - point).sqrMagnitude)
			{
				this.NextSegment();
			}
			while (allowBackwards && this.segmentIndex > 0 && (this.path[this.segmentIndex - 1] - point).sqrMagnitude <= (this.path[this.segmentIndex] - point).sqrMagnitude)
			{
				this.PrevSegment();
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			float num4 = float.PositiveInfinity;
			if (this.segmentIndex > 0)
			{
				num = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], point);
				num3 = (Vector3.Lerp(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], num) - point).sqrMagnitude;
			}
			if (this.segmentIndex < this.path.Count - 1)
			{
				num2 = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], point);
				num4 = (Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], num2) - point).sqrMagnitude;
			}
			if (num3 < num4)
			{
				this.MoveToSegment(this.segmentIndex - 1, num);
			}
			else
			{
				this.MoveToSegment(this.segmentIndex, num2);
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00015F6C File Offset: 0x0001436C
		public void MoveToCircleIntersection2D(Vector3 circleCenter3D, float radius, IMovementPlane transform)
		{
			if (this.path == null)
			{
				return;
			}
			while (this.segmentIndex < this.path.Count - 2 && VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], circleCenter3D) > 1f)
			{
				this.NextSegment();
			}
			Vector2 vector = transform.ToPlane(circleCenter3D);
			while (this.segmentIndex < this.path.Count - 2 && (transform.ToPlane(this.path[this.segmentIndex + 1]) - vector).sqrMagnitude <= radius * radius)
			{
				this.NextSegment();
			}
			float fractionAlongSegment = VectorMath.LineCircleIntersectionFactor(vector, transform.ToPlane(this.path[this.segmentIndex]), transform.ToPlane(this.path[this.segmentIndex + 1]), radius);
			this.MoveToSegment(this.segmentIndex, fractionAlongSegment);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001608C File Offset: 0x0001448C
		protected virtual void PrevSegment()
		{
			this.segmentIndex--;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
			this.distanceToSegmentStart -= this.currentSegmentLength;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000160F4 File Offset: 0x000144F4
		protected virtual void NextSegment()
		{
			this.segmentIndex++;
			this.distanceToSegmentStart += this.currentSegmentLength;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
		}

		// Token: 0x0400021C RID: 540
		private List<Vector3> path;

		// Token: 0x0400021D RID: 541
		private float distanceToSegmentStart;

		// Token: 0x0400021E RID: 542
		private float currentDistance;

		// Token: 0x0400021F RID: 543
		private float currentSegmentLength = float.PositiveInfinity;

		// Token: 0x04000220 RID: 544
		private float totalDistance = float.PositiveInfinity;
	}
}
