using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000051 RID: 81
	[Obsolete("Generic version is now obsolete to trade an extremely tiny performance decrease for a large decrease in boilerplate for Path classes")]
	public static class PathPool<T> where T : Path, new()
	{
		// Token: 0x06000386 RID: 902 RVA: 0x0001633A File Offset: 0x0001473A
		public static void Recycle(T path)
		{
			PathPool.Pool(path);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00016348 File Offset: 0x00014748
		public static void Warmup(int count, int length)
		{
			ListPool<GraphNode>.Warmup(count, length);
			ListPool<Vector3>.Warmup(count, length);
			Path[] array = new Path[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = PathPool<T>.GetPath();
				array[i].Claim(array);
			}
			for (int j = 0; j < count; j++)
			{
				array[j].Release(array, false);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000163AE File Offset: 0x000147AE
		public static int GetTotalCreated()
		{
			return PathPool.GetTotalCreated(typeof(T));
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000163BF File Offset: 0x000147BF
		public static int GetSize()
		{
			return PathPool.GetSize(typeof(T));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000163D0 File Offset: 0x000147D0
		[Obsolete("Use PathPool.GetPath<T> instead of PathPool<T>.GetPath")]
		public static T GetPath()
		{
			return PathPool.GetPath<T>();
		}
	}
}
