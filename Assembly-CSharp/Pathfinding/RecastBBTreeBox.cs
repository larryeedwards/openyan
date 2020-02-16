using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000C7 RID: 199
	public class RecastBBTreeBox
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00038B84 File Offset: 0x00036F84
		public RecastBBTreeBox(RecastMeshObj mesh)
		{
			this.mesh = mesh;
			Vector3 min = mesh.bounds.min;
			Vector3 max = mesh.bounds.max;
			this.rect = Rect.MinMaxRect(min.x, min.z, max.x, max.z);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00038BDD File Offset: 0x00036FDD
		public bool Contains(Vector3 p)
		{
			return this.rect.Contains(p);
		}

		// Token: 0x04000535 RID: 1333
		public Rect rect;

		// Token: 0x04000536 RID: 1334
		public RecastMeshObj mesh;

		// Token: 0x04000537 RID: 1335
		public RecastBBTreeBox c1;

		// Token: 0x04000538 RID: 1336
		public RecastBBTreeBox c2;
	}
}
