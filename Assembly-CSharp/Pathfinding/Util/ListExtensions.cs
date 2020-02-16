using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000036 RID: 54
	public static class ListExtensions
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00010240 File Offset: 0x0000E640
		public static T[] ToArrayFromPool<T>(this List<T> list)
		{
			T[] array = ArrayPool<T>.ClaimWithExactLength(list.Count);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0001027C File Offset: 0x0000E67C
		public static void ClearFast<T>(this List<T> list)
		{
			if (list.Count * 2 < list.Capacity)
			{
				list.RemoveRange(0, list.Count);
			}
			else
			{
				list.Clear();
			}
		}
	}
}
