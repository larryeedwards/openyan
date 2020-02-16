using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000011 RID: 17
	public struct GraphHitInfo
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00007379 File Offset: 0x00005779
		public GraphHitInfo(Vector3 point)
		{
			this.tangentOrigin = Vector3.zero;
			this.origin = Vector3.zero;
			this.point = point;
			this.node = null;
			this.tangent = Vector3.zero;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000073AC File Offset: 0x000057AC
		public float distance
		{
			get
			{
				return (this.point - this.origin).magnitude;
			}
		}

		// Token: 0x040000AB RID: 171
		public Vector3 origin;

		// Token: 0x040000AC RID: 172
		public Vector3 point;

		// Token: 0x040000AD RID: 173
		public GraphNode node;

		// Token: 0x040000AE RID: 174
		public Vector3 tangentOrigin;

		// Token: 0x040000AF RID: 175
		public Vector3 tangent;
	}
}
