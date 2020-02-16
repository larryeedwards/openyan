using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000093 RID: 147
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_dynamic_grid_obstacle.php")]
	public class DynamicGridObstacle : GraphModifier
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00024754 File Offset: 0x00022B54
		private Bounds bounds
		{
			get
			{
				if (this.coll != null)
				{
					return this.coll.bounds;
				}
				Bounds bounds = this.coll2D.bounds;
				bounds.extents += new Vector3(0f, 0f, 10000f);
				return bounds;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x000247B1 File Offset: 0x00022BB1
		private bool colliderEnabled
		{
			get
			{
				return (!(this.coll != null)) ? this.coll2D.enabled : this.coll.enabled;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000247E0 File Offset: 0x00022BE0
		protected override void Awake()
		{
			base.Awake();
			this.coll = base.GetComponent<Collider>();
			this.coll2D = base.GetComponent<Collider2D>();
			this.tr = base.transform;
			if (this.coll == null && this.coll2D == null)
			{
				throw new Exception("A collider or 2D collider must be attached to the GameObject(" + base.gameObject.name + ") for the DynamicGridObstacle to work");
			}
			this.prevBounds = this.bounds;
			this.prevRotation = this.tr.rotation;
			this.prevEnabled = false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002487D File Offset: 0x00022C7D
		public override void OnPostScan()
		{
			this.prevEnabled = this.colliderEnabled;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002488C File Offset: 0x00022C8C
		private void Update()
		{
			if (this.coll == null && this.coll2D == null)
			{
				Debug.LogError("Removed collider from DynamicGridObstacle", this);
				base.enabled = false;
				return;
			}
			if (AstarPath.active == null || AstarPath.active.isScanning || Time.realtimeSinceStartup - this.lastCheckTime < this.checkTime || !Application.isPlaying)
			{
				return;
			}
			this.lastCheckTime = Time.realtimeSinceStartup;
			if (this.colliderEnabled)
			{
				Bounds bounds = this.bounds;
				Quaternion rotation = this.tr.rotation;
				Vector3 vector = this.prevBounds.min - bounds.min;
				Vector3 vector2 = this.prevBounds.max - bounds.max;
				float magnitude = bounds.extents.magnitude;
				float num = magnitude * Quaternion.Angle(this.prevRotation, rotation) * 0.0174532924f;
				if (vector.sqrMagnitude > this.updateError * this.updateError || vector2.sqrMagnitude > this.updateError * this.updateError || num > this.updateError || !this.prevEnabled)
				{
					this.DoUpdateGraphs();
				}
			}
			else if (this.prevEnabled)
			{
				this.DoUpdateGraphs();
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000249F8 File Offset: 0x00022DF8
		protected override void OnDisable()
		{
			base.OnDisable();
			if (AstarPath.active != null && Application.isPlaying)
			{
				GraphUpdateObject ob = new GraphUpdateObject(this.prevBounds);
				AstarPath.active.UpdateGraphs(ob);
				this.prevEnabled = false;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00024A44 File Offset: 0x00022E44
		public void DoUpdateGraphs()
		{
			if (this.coll == null && this.coll2D == null)
			{
				return;
			}
			if (!this.colliderEnabled)
			{
				AstarPath.active.UpdateGraphs(this.prevBounds);
			}
			else
			{
				Bounds bounds = this.bounds;
				Bounds bounds2 = bounds;
				bounds2.Encapsulate(this.prevBounds);
				if (DynamicGridObstacle.BoundsVolume(bounds2) < DynamicGridObstacle.BoundsVolume(bounds) + DynamicGridObstacle.BoundsVolume(this.prevBounds))
				{
					AstarPath.active.UpdateGraphs(bounds2);
				}
				else
				{
					AstarPath.active.UpdateGraphs(this.prevBounds);
					AstarPath.active.UpdateGraphs(bounds);
				}
				this.prevBounds = bounds;
			}
			this.prevEnabled = this.colliderEnabled;
			this.prevRotation = this.tr.rotation;
			this.lastCheckTime = Time.realtimeSinceStartup;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00024B24 File Offset: 0x00022F24
		private static float BoundsVolume(Bounds b)
		{
			return Math.Abs(b.size.x * b.size.y * b.size.z);
		}

		// Token: 0x040003E6 RID: 998
		private Collider coll;

		// Token: 0x040003E7 RID: 999
		private Collider2D coll2D;

		// Token: 0x040003E8 RID: 1000
		private Transform tr;

		// Token: 0x040003E9 RID: 1001
		public float updateError = 1f;

		// Token: 0x040003EA RID: 1002
		public float checkTime = 0.2f;

		// Token: 0x040003EB RID: 1003
		private Bounds prevBounds;

		// Token: 0x040003EC RID: 1004
		private Quaternion prevRotation;

		// Token: 0x040003ED RID: 1005
		private bool prevEnabled;

		// Token: 0x040003EE RID: 1006
		private float lastCheckTime = -9999f;
	}
}
