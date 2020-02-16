using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x020000F3 RID: 243
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_navmesh_add.php")]
	public class NavmeshAdd : NavmeshClipper
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0004703C File Offset: 0x0004543C
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000470A8 File Offset: 0x000454A8
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000470C4 File Offset: 0x000454C4
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000470D8 File Offset: 0x000454D8
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00047107 File Offset: 0x00045507
		public Vector3 Center
		{
			get
			{
				return this.tr.position + ((!this.useRotationAndScale) ? this.center : this.tr.TransformPoint(this.center));
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00047140 File Offset: 0x00045540
		[ContextMenu("Rebuild Mesh")]
		public void RebuildMesh()
		{
			if (this.type == NavmeshAdd.MeshType.CustomMesh)
			{
				if (this.mesh == null)
				{
					this.verts = null;
					this.tris = null;
				}
				else
				{
					this.verts = this.mesh.vertices;
					this.tris = this.mesh.triangles;
				}
			}
			else
			{
				if (this.verts == null || this.verts.Length != 4 || this.tris == null || this.tris.Length != 6)
				{
					this.verts = new Vector3[4];
					this.tris = new int[6];
				}
				this.tris[0] = 0;
				this.tris[1] = 1;
				this.tris[2] = 2;
				this.tris[3] = 0;
				this.tris[4] = 2;
				this.tris[5] = 3;
				this.verts[0] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[1] = new Vector3(this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[2] = new Vector3(this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				this.verts[3] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0004731C File Offset: 0x0004571C
		internal override Rect GetBounds(GraphTransform inverseTransform)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			Int3[] array = ArrayPool<Int3>.Claim((this.verts == null) ? 0 : this.verts.Length);
			int[] array2;
			this.GetMesh(ref array, out array2, inverseTransform);
			Rect result = default(Rect);
			for (int i = 0; i < array2.Length; i++)
			{
				Vector3 vector = (Vector3)array[array2[i]];
				if (i == 0)
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
			ArrayPool<Int3>.Release(ref array, false);
			return result;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00047430 File Offset: 0x00045830
		public void GetMesh(ref Int3[] vbuffer, out int[] tbuffer, GraphTransform inverseTransform = null)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			if (this.verts == null)
			{
				tbuffer = ArrayPool<int>.Claim(0);
				return;
			}
			if (vbuffer == null || vbuffer.Length < this.verts.Length)
			{
				if (vbuffer != null)
				{
					ArrayPool<Int3>.Release(ref vbuffer, false);
				}
				vbuffer = ArrayPool<Int3>.Claim(this.verts.Length);
			}
			tbuffer = this.tris;
			if (this.useRotationAndScale)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.tr.position + this.center, this.tr.rotation, this.tr.localScale * this.meshScale);
				for (int i = 0; i < this.verts.Length; i++)
				{
					Vector3 vector = matrix4x.MultiplyPoint3x4(this.verts[i]);
					if (inverseTransform != null)
					{
						vector = inverseTransform.InverseTransform(vector);
					}
					vbuffer[i] = (Int3)vector;
				}
			}
			else
			{
				Vector3 a = this.tr.position + this.center;
				for (int j = 0; j < this.verts.Length; j++)
				{
					Vector3 vector2 = a + this.verts[j] * this.meshScale;
					if (inverseTransform != null)
					{
						vector2 = inverseTransform.InverseTransform(vector2);
					}
					vbuffer[j] = (Int3)vector2;
				}
			}
		}

		// Token: 0x04000643 RID: 1603
		public NavmeshAdd.MeshType type;

		// Token: 0x04000644 RID: 1604
		public Mesh mesh;

		// Token: 0x04000645 RID: 1605
		private Vector3[] verts;

		// Token: 0x04000646 RID: 1606
		private int[] tris;

		// Token: 0x04000647 RID: 1607
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x04000648 RID: 1608
		public float meshScale = 1f;

		// Token: 0x04000649 RID: 1609
		public Vector3 center;

		// Token: 0x0400064A RID: 1610
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x0400064B RID: 1611
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x0400064C RID: 1612
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x0400064D RID: 1613
		protected Transform tr;

		// Token: 0x0400064E RID: 1614
		private Vector3 lastPosition;

		// Token: 0x0400064F RID: 1615
		private Quaternion lastRotation;

		// Token: 0x04000650 RID: 1616
		public static readonly Color GizmoColor = new Color(0.368627459f, 0.9372549f, 0.145098045f);

		// Token: 0x020000F4 RID: 244
		public enum MeshType
		{
			// Token: 0x04000652 RID: 1618
			Rectangle,
			// Token: 0x04000653 RID: 1619
			CustomMesh
		}
	}
}
