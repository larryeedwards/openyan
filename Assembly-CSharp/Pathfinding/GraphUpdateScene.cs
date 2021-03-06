﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000031 RID: 49
	[AddComponentMenu("Pathfinding/GraphUpdateScene")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_graph_update_scene.php")]
	public class GraphUpdateScene : GraphModifier
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000E146 File Offset: 0x0000C546
		public void Start()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (!this.firstApplied && this.applyOnStart)
			{
				this.Apply();
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E16F File Offset: 0x0000C56F
		public override void OnPostScan()
		{
			if (this.applyOnScan)
			{
				this.Apply();
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000E184 File Offset: 0x0000C584
		public virtual void InvertSettings()
		{
			this.setWalkability = !this.setWalkability;
			this.penaltyDelta = -this.penaltyDelta;
			if (this.setTagInvert == 0)
			{
				this.setTagInvert = this.setTag;
				this.setTag = 0;
			}
			else
			{
				this.setTag = this.setTagInvert;
				this.setTagInvert = 0;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000E1E3 File Offset: 0x0000C5E3
		public void RecalcConvex()
		{
			this.convexPoints = ((!this.convex) ? null : Polygon.ConvexHullXZ(this.points));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E207 File Offset: 0x0000C607
		[Obsolete("World space can no longer be used as it does not work well with rotated graphs. Use transform.InverseTransformPoint to transform points to local space.", true)]
		private void ToggleUseWorldSpace()
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E209 File Offset: 0x0000C609
		[Obsolete("The Y coordinate is no longer important. Use the position of the object instead", true)]
		public void LockToY()
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E20C File Offset: 0x0000C60C
		public Bounds GetBounds()
		{
			if (this.points == null || this.points.Length == 0)
			{
				Collider component = base.GetComponent<Collider>();
				Collider2D component2 = base.GetComponent<Collider2D>();
				Renderer component3 = base.GetComponent<Renderer>();
				Bounds bounds;
				if (component != null)
				{
					bounds = component.bounds;
				}
				else if (component2 != null)
				{
					bounds = component2.bounds;
					bounds.size = new Vector3(bounds.size.x, bounds.size.y, Mathf.Max(bounds.size.z, 1f));
				}
				else
				{
					if (!(component3 != null))
					{
						return new Bounds(Vector3.zero, Vector3.zero);
					}
					bounds = component3.bounds;
				}
				if (this.legacyMode && bounds.size.y < this.minBoundsHeight)
				{
					bounds.size = new Vector3(bounds.size.x, this.minBoundsHeight, bounds.size.z);
				}
				return bounds;
			}
			return GraphUpdateShape.GetBounds((!this.convex) ? this.points : this.convexPoints, (!this.legacyMode || !this.legacyUseWorldSpace) ? base.transform.localToWorldMatrix : Matrix4x4.identity, this.minBoundsHeight);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E390 File Offset: 0x0000C790
		public void Apply()
		{
			if (AstarPath.active == null)
			{
				Debug.LogError("There is no AstarPath object in the scene", this);
				return;
			}
			GraphUpdateObject graphUpdateObject;
			if (this.points == null || this.points.Length == 0)
			{
				PolygonCollider2D component = base.GetComponent<PolygonCollider2D>();
				if (component != null)
				{
					Vector2[] array = component.points;
					Vector3[] array2 = new Vector3[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						Vector2 vector = array[i] + component.offset;
						array2[i] = new Vector3(vector.x, 0f, vector.y);
					}
					Matrix4x4 matrix = base.transform.localToWorldMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 0f, 0f), Vector3.one);
					GraphUpdateShape shape = new GraphUpdateShape(this.points, this.convex, matrix, this.minBoundsHeight);
					graphUpdateObject = new GraphUpdateObject(this.GetBounds());
					graphUpdateObject.shape = shape;
				}
				else
				{
					Bounds bounds = this.GetBounds();
					if (bounds.center == Vector3.zero && bounds.size == Vector3.zero)
					{
						Debug.LogError("Cannot apply GraphUpdateScene, no points defined and no renderer or collider attached", this);
						return;
					}
					graphUpdateObject = new GraphUpdateObject(bounds);
				}
			}
			else
			{
				GraphUpdateShape graphUpdateShape;
				if (this.legacyMode && !this.legacyUseWorldSpace)
				{
					Vector3[] array3 = new Vector3[this.points.Length];
					for (int j = 0; j < this.points.Length; j++)
					{
						array3[j] = base.transform.TransformPoint(this.points[j]);
					}
					graphUpdateShape = new GraphUpdateShape(array3, this.convex, Matrix4x4.identity, this.minBoundsHeight);
				}
				else
				{
					graphUpdateShape = new GraphUpdateShape(this.points, this.convex, (!this.legacyMode || !this.legacyUseWorldSpace) ? base.transform.localToWorldMatrix : Matrix4x4.identity, this.minBoundsHeight);
				}
				Bounds bounds2 = graphUpdateShape.GetBounds();
				graphUpdateObject = new GraphUpdateObject(bounds2);
				graphUpdateObject.shape = graphUpdateShape;
			}
			this.firstApplied = true;
			graphUpdateObject.modifyWalkability = this.modifyWalkability;
			graphUpdateObject.setWalkability = this.setWalkability;
			graphUpdateObject.addPenalty = this.penaltyDelta;
			graphUpdateObject.updatePhysics = this.updatePhysics;
			graphUpdateObject.updateErosion = this.updateErosion;
			graphUpdateObject.resetPenaltyOnPhysics = this.resetPenaltyOnPhysics;
			graphUpdateObject.modifyTag = this.modifyTag;
			graphUpdateObject.setTag = this.setTag;
			AstarPath.active.UpdateGraphs(graphUpdateObject);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E65C File Offset: 0x0000CA5C
		private void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E665 File Offset: 0x0000CA65
		private void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E670 File Offset: 0x0000CA70
		private void OnDrawGizmos(bool selected)
		{
			Color color = (!selected) ? new Color(0.8901961f, 0.239215687f, 0.08627451f, 0.9f) : new Color(0.8901961f, 0.239215687f, 0.08627451f, 1f);
			if (selected)
			{
				Gizmos.color = Color.Lerp(color, new Color(1f, 1f, 1f, 0.2f), 0.9f);
				Bounds bounds = this.GetBounds();
				Gizmos.DrawCube(bounds.center, bounds.size);
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
			if (this.points == null)
			{
				return;
			}
			if (this.convex)
			{
				color.a *= 0.5f;
			}
			Gizmos.color = color;
			Matrix4x4 matrix4x = (!this.legacyMode || !this.legacyUseWorldSpace) ? base.transform.localToWorldMatrix : Matrix4x4.identity;
			if (this.convex)
			{
				color.r -= 0.1f;
				color.g -= 0.2f;
				color.b -= 0.1f;
				Gizmos.color = color;
			}
			if (selected || !this.convex)
			{
				for (int i = 0; i < this.points.Length; i++)
				{
					Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.points[i]), matrix4x.MultiplyPoint3x4(this.points[(i + 1) % this.points.Length]));
				}
			}
			if (this.convex)
			{
				if (this.convexPoints == null)
				{
					this.RecalcConvex();
				}
				Gizmos.color = ((!selected) ? new Color(0.8901961f, 0.239215687f, 0.08627451f, 0.9f) : new Color(0.8901961f, 0.239215687f, 0.08627451f, 1f));
				for (int j = 0; j < this.convexPoints.Length; j++)
				{
					Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.convexPoints[j]), matrix4x.MultiplyPoint3x4(this.convexPoints[(j + 1) % this.convexPoints.Length]));
				}
			}
			Vector3[] array = (!this.convex) ? this.points : this.convexPoints;
			if (selected && array != null && array.Length > 0)
			{
				Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
				float num = array[0].y;
				float num2 = array[0].y;
				for (int k = 0; k < array.Length; k++)
				{
					num = Mathf.Min(num, array[k].y);
					num2 = Mathf.Max(num2, array[k].y);
				}
				float num3 = Mathf.Max(this.minBoundsHeight - (num2 - num), 0f) * 0.5f;
				num -= num3;
				num2 += num3;
				for (int l = 0; l < array.Length; l++)
				{
					int num4 = (l + 1) % array.Length;
					Vector3 from = matrix4x.MultiplyPoint3x4(array[l] + Vector3.up * (num - array[l].y));
					Vector3 vector = matrix4x.MultiplyPoint3x4(array[l] + Vector3.up * (num2 - array[l].y));
					Vector3 to = matrix4x.MultiplyPoint3x4(array[num4] + Vector3.up * (num - array[num4].y));
					Vector3 to2 = matrix4x.MultiplyPoint3x4(array[num4] + Vector3.up * (num2 - array[num4].y));
					Gizmos.DrawLine(from, vector);
					Gizmos.DrawLine(from, to);
					Gizmos.DrawLine(vector, to2);
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EAE4 File Offset: 0x0000CEE4
		public void DisableLegacyMode()
		{
			if (this.legacyMode)
			{
				this.legacyMode = false;
				if (this.legacyUseWorldSpace)
				{
					this.legacyUseWorldSpace = false;
					for (int i = 0; i < this.points.Length; i++)
					{
						this.points[i] = base.transform.InverseTransformPoint(this.points[i]);
					}
					this.RecalcConvex();
				}
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EB61 File Offset: 0x0000CF61
		protected override void Awake()
		{
			if (this.serializedVersion == 0)
			{
				if (this.points != null && this.points.Length > 0)
				{
					this.legacyMode = true;
				}
				this.serializedVersion = 1;
			}
			base.Awake();
		}

		// Token: 0x0400015E RID: 350
		public Vector3[] points;

		// Token: 0x0400015F RID: 351
		private Vector3[] convexPoints;

		// Token: 0x04000160 RID: 352
		public bool convex = true;

		// Token: 0x04000161 RID: 353
		public float minBoundsHeight = 1f;

		// Token: 0x04000162 RID: 354
		public int penaltyDelta;

		// Token: 0x04000163 RID: 355
		public bool modifyWalkability;

		// Token: 0x04000164 RID: 356
		public bool setWalkability;

		// Token: 0x04000165 RID: 357
		public bool applyOnStart = true;

		// Token: 0x04000166 RID: 358
		public bool applyOnScan = true;

		// Token: 0x04000167 RID: 359
		public bool updatePhysics;

		// Token: 0x04000168 RID: 360
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x04000169 RID: 361
		public bool updateErosion = true;

		// Token: 0x0400016A RID: 362
		public bool modifyTag;

		// Token: 0x0400016B RID: 363
		public int setTag;

		// Token: 0x0400016C RID: 364
		[HideInInspector]
		public bool legacyMode;

		// Token: 0x0400016D RID: 365
		private int setTagInvert;

		// Token: 0x0400016E RID: 366
		private bool firstApplied;

		// Token: 0x0400016F RID: 367
		[SerializeField]
		private int serializedVersion;

		// Token: 0x04000170 RID: 368
		[SerializeField]
		[FormerlySerializedAs("useWorldSpace")]
		private bool legacyUseWorldSpace;
	}
}
