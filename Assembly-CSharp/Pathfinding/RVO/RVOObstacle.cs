using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200010E RID: 270
	public abstract class RVOObstacle : VersionedMonoBehaviour
	{
		// Token: 0x060009D7 RID: 2519
		protected abstract void CreateObstacles();

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060009D8 RID: 2520
		protected abstract bool ExecuteInEditor { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060009D9 RID: 2521
		protected abstract bool LocalCoordinates { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060009DA RID: 2522
		protected abstract bool StaticObstacle { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060009DB RID: 2523
		protected abstract float Height { get; }

		// Token: 0x060009DC RID: 2524
		protected abstract bool AreGizmosDirty();

		// Token: 0x060009DD RID: 2525 RVA: 0x0004BD96 File Offset: 0x0004A196
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0004BD9F File Offset: 0x0004A19F
		public void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0004BDA8 File Offset: 0x0004A1A8
		public void OnDrawGizmos(bool selected)
		{
			this.gizmoDrawing = true;
			Gizmos.color = new Color(0.615f, 1f, 0.06f, (!selected) ? 0.7f : 1f);
			MovementPlane movementPlane = (!(RVOSimulator.active != null)) ? MovementPlane.XZ : RVOSimulator.active.movementPlane;
			Vector3 vector = (movementPlane != MovementPlane.XZ) ? (-Vector3.forward) : Vector3.up;
			if (this.gizmoVerts == null || this.AreGizmosDirty() || this._obstacleMode != this.obstacleMode)
			{
				this._obstacleMode = this.obstacleMode;
				if (this.gizmoVerts == null)
				{
					this.gizmoVerts = new List<Vector3[]>();
				}
				else
				{
					this.gizmoVerts.Clear();
				}
				this.CreateObstacles();
			}
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < this.gizmoVerts.Count; i++)
			{
				Vector3[] array = this.gizmoVerts[i];
				int j = 0;
				int num = array.Length - 1;
				while (j < array.Length)
				{
					Gizmos.DrawLine(matrix.MultiplyPoint3x4(array[j]), matrix.MultiplyPoint3x4(array[num]));
					num = j++;
				}
				if (selected)
				{
					int k = 0;
					int num2 = array.Length - 1;
					while (k < array.Length)
					{
						Vector3 vector2 = matrix.MultiplyPoint3x4(array[num2]);
						Vector3 vector3 = matrix.MultiplyPoint3x4(array[k]);
						if (movementPlane != MovementPlane.XY)
						{
							Gizmos.DrawLine(vector2 + vector * this.Height, vector3 + vector * this.Height);
							Gizmos.DrawLine(vector2, vector2 + vector * this.Height);
						}
						Vector3 vector4 = (vector2 + vector3) * 0.5f;
						Vector3 normalized = (vector3 - vector2).normalized;
						if (!(normalized == Vector3.zero))
						{
							Vector3 vector5 = Vector3.Cross(vector, normalized);
							Gizmos.DrawLine(vector4, vector4 + vector5);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f + normalized * 0.5f);
							Gizmos.DrawLine(vector4 + vector5, vector4 + vector5 * 0.5f - normalized * 0.5f);
						}
						num2 = k++;
					}
				}
			}
			this.gizmoDrawing = false;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0004C071 File Offset: 0x0004A471
		protected virtual Matrix4x4 GetMatrix()
		{
			return (!this.LocalCoordinates) ? Matrix4x4.identity : base.transform.localToWorldMatrix;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0004C094 File Offset: 0x0004A494
		public void OnDisable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnEnable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.RemoveObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0004C0F8 File Offset: 0x0004A4F8
		public void OnEnable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnDisable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					ObstacleVertex obstacleVertex = this.addedObstacles[i];
					ObstacleVertex obstacleVertex2 = obstacleVertex;
					do
					{
						obstacleVertex.layer = this.layer;
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacleVertex2);
					this.sim.AddObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0004C183 File Offset: 0x0004A583
		public void Start()
		{
			this.addedObstacles = new List<ObstacleVertex>();
			this.sourceObstacles = new List<Vector3[]>();
			this.prevUpdateMatrix = this.GetMatrix();
			this.CreateObstacles();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004C1B0 File Offset: 0x0004A5B0
		public void Update()
		{
			Matrix4x4 matrix = this.GetMatrix();
			if (matrix != this.prevUpdateMatrix)
			{
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.UpdateObstacle(this.addedObstacles[i], this.sourceObstacles[i], matrix);
				}
				this.prevUpdateMatrix = matrix;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0004C21C File Offset: 0x0004A61C
		protected void FindSimulator()
		{
			if (RVOSimulator.active == null)
			{
				throw new InvalidOperationException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.sim = RVOSimulator.active.GetSimulator();
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0004C24C File Offset: 0x0004A64C
		protected void AddObstacle(Vector3[] vertices, float height)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices Must Not Be Null");
			}
			if (height < 0f)
			{
				throw new ArgumentOutOfRangeException("Height must be non-negative");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("An obstacle must have at least two vertices");
			}
			if (this.sim == null)
			{
				this.FindSimulator();
			}
			if (this.gizmoDrawing)
			{
				Vector3[] array = new Vector3[vertices.Length];
				this.WindCorrectly(vertices);
				Array.Copy(vertices, array, vertices.Length);
				this.gizmoVerts.Add(array);
				return;
			}
			if (vertices.Length == 2)
			{
				this.AddObstacleInternal(vertices, height);
				return;
			}
			this.WindCorrectly(vertices);
			this.AddObstacleInternal(vertices, height);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0004C2F8 File Offset: 0x0004A6F8
		private void AddObstacleInternal(Vector3[] vertices, float height)
		{
			this.addedObstacles.Add(this.sim.AddObstacle(vertices, height, this.GetMatrix(), this.layer, true));
			this.sourceObstacles.Add(vertices);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0004C32C File Offset: 0x0004A72C
		private void WindCorrectly(Vector3[] vertices)
		{
			int num = 0;
			float num2 = float.PositiveInfinity;
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < vertices.Length; i++)
			{
				float x = matrix.MultiplyPoint3x4(vertices[i]).x;
				if (x < num2)
				{
					num = i;
					num2 = x;
				}
			}
			Vector3 a = matrix.MultiplyPoint3x4(vertices[(num - 1 + vertices.Length) % vertices.Length]);
			Vector3 b = matrix.MultiplyPoint3x4(vertices[num]);
			Vector3 c = matrix.MultiplyPoint3x4(vertices[(num + 1) % vertices.Length]);
			MovementPlane movementPlane;
			if (this.sim != null)
			{
				movementPlane = this.sim.movementPlane;
			}
			else if (RVOSimulator.active)
			{
				movementPlane = RVOSimulator.active.movementPlane;
			}
			else
			{
				movementPlane = MovementPlane.XZ;
			}
			if (movementPlane == MovementPlane.XY)
			{
				a.z = a.y;
				b.z = b.y;
				c.z = c.y;
			}
			if (VectorMath.IsClockwiseXZ(a, b, c) != (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepIn))
			{
				Array.Reverse(vertices);
			}
		}

		// Token: 0x040006CB RID: 1739
		public RVOObstacle.ObstacleVertexWinding obstacleMode;

		// Token: 0x040006CC RID: 1740
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x040006CD RID: 1741
		protected Simulator sim;

		// Token: 0x040006CE RID: 1742
		private List<ObstacleVertex> addedObstacles;

		// Token: 0x040006CF RID: 1743
		private List<Vector3[]> sourceObstacles;

		// Token: 0x040006D0 RID: 1744
		private bool gizmoDrawing;

		// Token: 0x040006D1 RID: 1745
		private List<Vector3[]> gizmoVerts;

		// Token: 0x040006D2 RID: 1746
		private RVOObstacle.ObstacleVertexWinding _obstacleMode;

		// Token: 0x040006D3 RID: 1747
		private Matrix4x4 prevUpdateMatrix;

		// Token: 0x0200010F RID: 271
		public enum ObstacleVertexWinding
		{
			// Token: 0x040006D5 RID: 1749
			KeepOut,
			// Token: 0x040006D6 RID: 1750
			KeepIn
		}
	}
}
