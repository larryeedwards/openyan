using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A6 RID: 166
	internal class LinkedLevelNode
	{
		// Token: 0x04000470 RID: 1136
		public Vector3 position;

		// Token: 0x04000471 RID: 1137
		public bool walkable;

		// Token: 0x04000472 RID: 1138
		public RaycastHit hit;

		// Token: 0x04000473 RID: 1139
		public float height;

		// Token: 0x04000474 RID: 1140
		public LinkedLevelNode next;
	}
}
