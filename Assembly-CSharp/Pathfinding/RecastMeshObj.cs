using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000CA RID: 202
	[AddComponentMenu("Pathfinding/Navmesh/RecastMeshObj")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_mesh_obj.php")]
	public class RecastMeshObj : VersionedMonoBehaviour
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x00039D00 File Offset: 0x00038100
		public static void GetAllInBounds(List<RecastMeshObj> buffer, Bounds bounds)
		{
			if (!Application.isPlaying)
			{
				RecastMeshObj[] array = UnityEngine.Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].RecalculateBounds();
					if (array[i].GetBounds().Intersects(bounds))
					{
						buffer.Add(array[i]);
					}
				}
				return;
			}
			if (Time.timeSinceLevelLoad == 0f)
			{
				RecastMeshObj[] array2 = UnityEngine.Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Register();
				}
			}
			for (int k = 0; k < RecastMeshObj.dynamicMeshObjs.Count; k++)
			{
				if (RecastMeshObj.dynamicMeshObjs[k].GetBounds().Intersects(bounds))
				{
					buffer.Add(RecastMeshObj.dynamicMeshObjs[k]);
				}
			}
			Rect rect = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			RecastMeshObj.tree.QueryInBounds(rect, buffer);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00039E52 File Offset: 0x00038252
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00039E5C File Offset: 0x0003825C
		private void Register()
		{
			if (this.registered)
			{
				return;
			}
			this.registered = true;
			this.area = Mathf.Clamp(this.area, -1, 33554432);
			Renderer component = base.GetComponent<Renderer>();
			Collider component2 = base.GetComponent<Collider>();
			if (component == null && component2 == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component3 = base.GetComponent<MeshFilter>();
			if (component != null && component3 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			this.bounds = ((!(component != null)) ? component2.bounds : component.bounds);
			this._dynamic = this.dynamic;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Add(this);
			}
			else
			{
				RecastMeshObj.tree.Insert(this);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00039F44 File Offset: 0x00038344
		private void RecalculateBounds()
		{
			Renderer component = base.GetComponent<Renderer>();
			Collider collider = this.GetCollider();
			if (component == null && collider == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			if (component != null && component2 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			this.bounds = ((!(component != null)) ? collider.bounds : component.bounds);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00039FCF File Offset: 0x000383CF
		public Bounds GetBounds()
		{
			if (this._dynamic)
			{
				this.RecalculateBounds();
			}
			return this.bounds;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00039FE8 File Offset: 0x000383E8
		public MeshFilter GetMeshFilter()
		{
			return base.GetComponent<MeshFilter>();
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00039FF0 File Offset: 0x000383F0
		public Collider GetCollider()
		{
			return base.GetComponent<Collider>();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00039FF8 File Offset: 0x000383F8
		private void OnDisable()
		{
			this.registered = false;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Remove(this);
			}
			else if (!RecastMeshObj.tree.Remove(this))
			{
				throw new Exception("Could not remove RecastMeshObj from tree even though it should exist in it. Has the object moved without being marked as dynamic?");
			}
			this._dynamic = this.dynamic;
		}

		// Token: 0x04000545 RID: 1349
		protected static RecastBBTree tree = new RecastBBTree();

		// Token: 0x04000546 RID: 1350
		protected static List<RecastMeshObj> dynamicMeshObjs = new List<RecastMeshObj>();

		// Token: 0x04000547 RID: 1351
		[HideInInspector]
		public Bounds bounds;

		// Token: 0x04000548 RID: 1352
		public bool dynamic = true;

		// Token: 0x04000549 RID: 1353
		public int area;

		// Token: 0x0400054A RID: 1354
		private bool _dynamic;

		// Token: 0x0400054B RID: 1355
		private bool registered;
	}
}
