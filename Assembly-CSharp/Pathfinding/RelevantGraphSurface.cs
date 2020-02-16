using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F8 RID: 248
	[AddComponentMenu("Pathfinding/Navmesh/RelevantGraphSurface")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_relevant_graph_surface.php")]
	public class RelevantGraphSurface : VersionedMonoBehaviour
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000480F1 File Offset: 0x000464F1
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000480F9 File Offset: 0x000464F9
		public RelevantGraphSurface Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00048101 File Offset: 0x00046501
		public RelevantGraphSurface Prev
		{
			get
			{
				return this.prev;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00048109 File Offset: 0x00046509
		public static RelevantGraphSurface Root
		{
			get
			{
				return RelevantGraphSurface.root;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00048110 File Offset: 0x00046510
		public void UpdatePosition()
		{
			this.position = base.transform.position;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00048123 File Offset: 0x00046523
		private void OnEnable()
		{
			this.UpdatePosition();
			if (RelevantGraphSurface.root == null)
			{
				RelevantGraphSurface.root = this;
			}
			else
			{
				this.next = RelevantGraphSurface.root;
				RelevantGraphSurface.root.prev = this;
				RelevantGraphSurface.root = this;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00048164 File Offset: 0x00046564
		private void OnDisable()
		{
			if (RelevantGraphSurface.root == this)
			{
				RelevantGraphSurface.root = this.next;
				if (RelevantGraphSurface.root != null)
				{
					RelevantGraphSurface.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00048200 File Offset: 0x00046600
		public static void UpdateAllPositions()
		{
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.root;
			while (relevantGraphSurface != null)
			{
				relevantGraphSurface.UpdatePosition();
				relevantGraphSurface = relevantGraphSurface.Next;
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00048234 File Offset: 0x00046634
		public static void FindAllGraphSurfaces()
		{
			RelevantGraphSurface[] array = UnityEngine.Object.FindObjectsOfType(typeof(RelevantGraphSurface)) as RelevantGraphSurface[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnDisable();
				array[i].OnEnable();
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0004827C File Offset: 0x0004667C
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(0.223529413f, 0.827451f, 0.180392161f, 0.4f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000482EC File Offset: 0x000466EC
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.223529413f, 0.827451f, 0.180392161f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x04000671 RID: 1649
		private static RelevantGraphSurface root;

		// Token: 0x04000672 RID: 1650
		public float maxRange = 1f;

		// Token: 0x04000673 RID: 1651
		private RelevantGraphSurface prev;

		// Token: 0x04000674 RID: 1652
		private RelevantGraphSurface next;

		// Token: 0x04000675 RID: 1653
		private Vector3 position;
	}
}
