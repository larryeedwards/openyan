using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x0200004E RID: 78
	public static class ObjectPoolSimple<T> where T : class, new()
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0001587C File Offset: 0x00013C7C
		public static T Claim()
		{
			object obj = ObjectPoolSimple<T>.pool;
			T result;
			lock (obj)
			{
				if (ObjectPoolSimple<T>.pool.Count > 0)
				{
					T t = ObjectPoolSimple<T>.pool[ObjectPoolSimple<T>.pool.Count - 1];
					ObjectPoolSimple<T>.pool.RemoveAt(ObjectPoolSimple<T>.pool.Count - 1);
					ObjectPoolSimple<T>.inPool.Remove(t);
					result = t;
				}
				else
				{
					result = Activator.CreateInstance<T>();
				}
			}
			return result;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00015908 File Offset: 0x00013D08
		public static void Release(ref T obj)
		{
			object obj2 = ObjectPoolSimple<T>.pool;
			lock (obj2)
			{
				ObjectPoolSimple<T>.pool.Add(obj);
			}
			obj = (T)((object)null);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001595C File Offset: 0x00013D5C
		public static void Clear()
		{
			object obj = ObjectPoolSimple<T>.pool;
			lock (obj)
			{
				ObjectPoolSimple<T>.pool.Clear();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001599C File Offset: 0x00013D9C
		public static int GetSize()
		{
			return ObjectPoolSimple<T>.pool.Count;
		}

		// Token: 0x0400021A RID: 538
		private static List<T> pool = new List<T>();

		// Token: 0x0400021B RID: 539
		private static readonly HashSet<T> inPool = new HashSet<T>();
	}
}
