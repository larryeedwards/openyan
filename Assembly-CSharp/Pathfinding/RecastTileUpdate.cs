using System;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000099 RID: 153
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdate")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_tile_update.php")]
	public class RecastTileUpdate : MonoBehaviour
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060005DD RID: 1501 RVA: 0x0002517C File Offset: 0x0002357C
		// (remove) Token: 0x060005DE RID: 1502 RVA: 0x000251B0 File Offset: 0x000235B0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Bounds> OnNeedUpdates;

		// Token: 0x060005DF RID: 1503 RVA: 0x000251E4 File Offset: 0x000235E4
		private void Start()
		{
			this.ScheduleUpdate();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000251EC File Offset: 0x000235EC
		private void OnDestroy()
		{
			this.ScheduleUpdate();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000251F4 File Offset: 0x000235F4
		public void ScheduleUpdate()
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				if (RecastTileUpdate.OnNeedUpdates != null)
				{
					RecastTileUpdate.OnNeedUpdates(component.bounds);
				}
			}
			else if (RecastTileUpdate.OnNeedUpdates != null)
			{
				RecastTileUpdate.OnNeedUpdates(new Bounds(base.transform.position, Vector3.zero));
			}
		}
	}
}
