using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000015 RID: 21
	public struct NNInfo
	{
		// Token: 0x0600011E RID: 286 RVA: 0x000075AF File Offset: 0x000059AF
		public NNInfo(NNInfoInternal internalInfo)
		{
			this.node = internalInfo.node;
			this.position = internalInfo.clampedPosition;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000075CB File Offset: 0x000059CB
		[Obsolete("This field has been renamed to 'position'")]
		public Vector3 clampedPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000075D3 File Offset: 0x000059D3
		public static explicit operator Vector3(NNInfo ob)
		{
			return ob.position;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000075DC File Offset: 0x000059DC
		public static explicit operator GraphNode(NNInfo ob)
		{
			return ob.node;
		}

		// Token: 0x040000BD RID: 189
		public readonly GraphNode node;

		// Token: 0x040000BE RID: 190
		public readonly Vector3 position;
	}
}
