using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x020000F6 RID: 246
	[AddComponentMenu("Pathfinding/Navmesh/Navmesh Cut")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_cut.php")]
	public class NavmeshCut : NavmeshClipper
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x00047645 File Offset: 0x00045A45
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00047659 File Offset: 0x00045A59
		protected override void OnEnable()
		{
			base.OnEnable();
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			this.lastRotation = this.tr.rotation;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0004768C File Offset: 0x00045A8C
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000476A8 File Offset: 0x00045AA8
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00047714 File Offset: 0x00045B14
		public virtual void UsedForCut()
		{
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00047716 File Offset: 0x00045B16
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00047748 File Offset: 0x00045B48
		private void CalculateMeshContour()
		{
			if (this.mesh == null)
			{
				return;
			}
			NavmeshCut.edges.Clear();
			NavmeshCut.pointers.Clear();
			Vector3[] vertices = this.mesh.vertices;
			int[] triangles = this.mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				if (VectorMath.IsClockwiseXZ(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]))
				{
					int num = triangles[i];
					triangles[i] = triangles[i + 2];
					triangles[i + 2] = num;
				}
				NavmeshCut.edges[new Int2(triangles[i], triangles[i + 1])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 1], triangles[i + 2])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 2], triangles[i])] = i;
			}
			for (int j = 0; j < triangles.Length; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					if (!NavmeshCut.edges.ContainsKey(new Int2(triangles[j + (k + 1) % 3], triangles[j + k % 3])))
					{
						NavmeshCut.pointers[triangles[j + k % 3]] = triangles[j + (k + 1) % 3];
					}
				}
			}
			List<Vector3[]> list = new List<Vector3[]>();
			List<Vector3> list2 = ListPool<Vector3>.Claim();
			for (int l = 0; l < vertices.Length; l++)
			{
				if (NavmeshCut.pointers.ContainsKey(l))
				{
					list2.Clear();
					int num2 = l;
					do
					{
						int num3 = NavmeshCut.pointers[num2];
						if (num3 == -1)
						{
							break;
						}
						NavmeshCut.pointers[num2] = -1;
						list2.Add(vertices[num2]);
						num2 = num3;
						if (num2 == -1)
						{
							goto Block_9;
						}
					}
					while (num2 != l);
					IL_20C:
					if (list2.Count > 0)
					{
						list.Add(list2.ToArray());
						goto IL_227;
					}
					goto IL_227;
					Block_9:
					Debug.LogError("Invalid Mesh '" + this.mesh.name + " in " + base.gameObject.name);
					goto IL_20C;
				}
				IL_227:;
			}
			ListPool<Vector3>.Release(ref list2);
			this.contours = list.ToArray();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000479A0 File Offset: 0x00045DA0
		internal override Rect GetBounds(GraphTransform inverseTranform)
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Rect result = default(Rect);
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = inverseTranform.InverseTransform(list2[j]);
					if (j == 0)
					{
						result = new Rect(vector.x, vector.z, 0f, 0f);
					}
					else
					{
						result.xMax = Math.Max(result.xMax, vector.x);
						result.yMax = Math.Max(result.yMax, vector.z);
						result.xMin = Math.Min(result.xMin, vector.x);
						result.yMin = Math.Min(result.yMin, vector.z);
					}
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
			return result;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00047AA8 File Offset: 0x00045EA8
		public void GetContour(List<List<Vector3>> buffer)
		{
			if (this.circleResolution < 3)
			{
				this.circleResolution = 3;
			}
			NavmeshCut.MeshType meshType = this.type;
			if (meshType != NavmeshCut.MeshType.Rectangle)
			{
				if (meshType != NavmeshCut.MeshType.Circle)
				{
					if (meshType == NavmeshCut.MeshType.CustomMesh)
					{
						if (this.mesh != this.lastMesh || this.contours == null)
						{
							this.CalculateMeshContour();
							this.lastMesh = this.mesh;
						}
						if (this.contours != null)
						{
							bool reverse = this.meshScale < 0f;
							for (int i = 0; i < this.contours.Length; i++)
							{
								Vector3[] array = this.contours[i];
								List<Vector3> list = ListPool<Vector3>.Claim(array.Length);
								for (int j = 0; j < array.Length; j++)
								{
									list.Add(array[j] * this.meshScale);
								}
								this.TransformBuffer(list, reverse);
								buffer.Add(list);
							}
						}
					}
				}
				else
				{
					List<Vector3> list = ListPool<Vector3>.Claim(this.circleResolution);
					for (int k = 0; k < this.circleResolution; k++)
					{
						list.Add(new Vector3(Mathf.Cos((float)(k * 2) * 3.14159274f / (float)this.circleResolution), 0f, Mathf.Sin((float)(k * 2) * 3.14159274f / (float)this.circleResolution)) * this.circleRadius);
					}
					bool reverse = this.circleRadius < 0f;
					this.TransformBuffer(list, reverse);
					buffer.Add(list);
				}
			}
			else
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				list.Add(new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				bool reverse = this.rectangleSize.x < 0f ^ this.rectangleSize.y < 0f;
				this.TransformBuffer(list, reverse);
				buffer.Add(list);
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00047D44 File Offset: 0x00046144
		private void TransformBuffer(List<Vector3> buffer, bool reverse)
		{
			Vector3 vector = this.center;
			if (this.useRotationAndScale)
			{
				Matrix4x4 localToWorldMatrix = this.tr.localToWorldMatrix;
				for (int i = 0; i < buffer.Count; i++)
				{
					buffer[i] = localToWorldMatrix.MultiplyPoint3x4(buffer[i] + vector);
				}
				reverse ^= VectorMath.ReversesFaceOrientationsXZ(localToWorldMatrix);
			}
			else
			{
				vector += this.tr.position;
				for (int j = 0; j < buffer.Count; j++)
				{
					int index;
					buffer[index = j] = buffer[index] + vector;
				}
			}
			if (reverse)
			{
				buffer.Reverse();
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00047E04 File Offset: 0x00046204
		public void OnDrawGizmos()
		{
			if (this.tr == null)
			{
				this.tr = base.transform;
			}
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Gizmos.color = NavmeshCut.GizmoColor;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 from = list2[j];
					Vector3 to = list2[(j + 1) % list2.Count];
					Gizmos.DrawLine(from, to);
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00047EA8 File Offset: 0x000462A8
		internal float GetY(GraphTransform transform)
		{
			return transform.InverseTransform((!this.useRotationAndScale) ? (this.tr.position + this.center) : this.tr.TransformPoint(this.center)).y;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00047EFC File Offset: 0x000462FC
		public void OnDrawGizmosSelected()
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Color color = Color.Lerp(NavmeshCut.GizmoColor, Color.white, 0.5f);
			color.a *= 0.5f;
			Gizmos.color = color;
			NavmeshBase navmeshBase = (!(AstarPath.active != null)) ? null : (AstarPath.active.data.recastGraph ?? AstarPath.active.data.navmesh);
			GraphTransform graphTransform = (navmeshBase == null) ? GraphTransform.identityTransform : navmeshBase.transform;
			float y = this.GetY(graphTransform);
			float y2 = y - this.height * 0.5f;
			float y3 = y + this.height * 0.5f;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = graphTransform.InverseTransform(list2[j]);
					Vector3 vector2 = graphTransform.InverseTransform(list2[(j + 1) % list2.Count]);
					Vector3 point = vector;
					Vector3 point2 = vector2;
					Vector3 point3 = vector;
					Vector3 point4 = vector2;
					point.y = (point2.y = y2);
					point3.y = (point4.y = y3);
					Gizmos.DrawLine(graphTransform.Transform(point), graphTransform.Transform(point2));
					Gizmos.DrawLine(graphTransform.Transform(point3), graphTransform.Transform(point4));
					Gizmos.DrawLine(graphTransform.Transform(point), graphTransform.Transform(point3));
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x04000658 RID: 1624
		[Tooltip("Shape of the cut")]
		public NavmeshCut.MeshType type;

		// Token: 0x04000659 RID: 1625
		[Tooltip("The contour(s) of the mesh will be extracted. This mesh should only be a 2D surface, not a volume (see documentation).")]
		public Mesh mesh;

		// Token: 0x0400065A RID: 1626
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x0400065B RID: 1627
		public float circleRadius = 1f;

		// Token: 0x0400065C RID: 1628
		public int circleResolution = 6;

		// Token: 0x0400065D RID: 1629
		public float height = 1f;

		// Token: 0x0400065E RID: 1630
		[Tooltip("Scale of the custom mesh")]
		public float meshScale = 1f;

		// Token: 0x0400065F RID: 1631
		public Vector3 center;

		// Token: 0x04000660 RID: 1632
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x04000661 RID: 1633
		[Tooltip("Only makes a split in the navmesh, but does not remove the geometry to make a hole")]
		public bool isDual;

		// Token: 0x04000662 RID: 1634
		public bool cutsAddedGeom = true;

		// Token: 0x04000663 RID: 1635
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x04000664 RID: 1636
		[Tooltip("Includes rotation in calculations. This is slower since a lot more matrix multiplications are needed but gives more flexibility.")]
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x04000665 RID: 1637
		private Vector3[][] contours;

		// Token: 0x04000666 RID: 1638
		protected Transform tr;

		// Token: 0x04000667 RID: 1639
		private Mesh lastMesh;

		// Token: 0x04000668 RID: 1640
		private Vector3 lastPosition;

		// Token: 0x04000669 RID: 1641
		private Quaternion lastRotation;

		// Token: 0x0400066A RID: 1642
		private static readonly Dictionary<Int2, int> edges = new Dictionary<Int2, int>();

		// Token: 0x0400066B RID: 1643
		private static readonly Dictionary<int, int> pointers = new Dictionary<int, int>();

		// Token: 0x0400066C RID: 1644
		public static readonly Color GizmoColor = new Color(0.145098045f, 0.721568644f, 0.9372549f);

		// Token: 0x020000F7 RID: 247
		public enum MeshType
		{
			// Token: 0x0400066E RID: 1646
			Rectangle,
			// Token: 0x0400066F RID: 1647
			Circle,
			// Token: 0x04000670 RID: 1648
			CustomMesh
		}
	}
}
