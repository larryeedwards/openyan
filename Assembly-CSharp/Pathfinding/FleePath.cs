using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000101 RID: 257
	public class FleePath : RandomPath
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x0004A0F4 File Offset: 0x000484F4
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool.GetPath<FleePath>();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0004A114 File Offset: 0x00048514
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = avoid - start;
			this.aim *= 10f;
			this.aim = start - this.aim;
		}
	}
}
