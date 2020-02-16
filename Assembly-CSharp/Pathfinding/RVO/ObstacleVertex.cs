using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000069 RID: 105
	public class ObstacleVertex
	{
		// Token: 0x040002CD RID: 717
		public bool ignore;

		// Token: 0x040002CE RID: 718
		public Vector3 position;

		// Token: 0x040002CF RID: 719
		public Vector2 dir;

		// Token: 0x040002D0 RID: 720
		public float height;

		// Token: 0x040002D1 RID: 721
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x040002D2 RID: 722
		public ObstacleVertex next;

		// Token: 0x040002D3 RID: 723
		public ObstacleVertex prev;
	}
}
