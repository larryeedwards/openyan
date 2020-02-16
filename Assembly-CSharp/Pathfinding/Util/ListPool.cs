using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000046 RID: 70
	public static class ListPool<T>
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000138B8 File Offset: 0x00011CB8
		public static List<T> Claim()
		{
			object obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				if (ListPool<T>.pool.Count > 0)
				{
					List<T> list = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
					ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
					ListPool<T>.inPool.Remove(list);
					result = list;
				}
				else
				{
					result = new List<T>();
				}
			}
			return result;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00013944 File Offset: 0x00011D44
		private static int FindCandidate(List<List<T>> pool, int capacity)
		{
			List<T> list = null;
			int result = -1;
			int num = 0;
			while (num < pool.Count && num < 8)
			{
				List<T> list2 = pool[pool.Count - 1 - num];
				if ((list == null || list2.Capacity > list.Capacity) && list2.Capacity < capacity * 16)
				{
					list = list2;
					result = pool.Count - 1 - num;
					if (list.Capacity >= capacity)
					{
						return result;
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000139C8 File Offset: 0x00011DC8
		public static List<T> Claim(int capacity)
		{
			object obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				List<List<T>> list = ListPool<T>.pool;
				int num = ListPool<T>.FindCandidate(ListPool<T>.pool, capacity);
				if (capacity > 5000)
				{
					int num2 = ListPool<T>.FindCandidate(ListPool<T>.largePool, capacity);
					if (num2 != -1)
					{
						list = ListPool<T>.largePool;
						num = num2;
					}
				}
				if (num == -1)
				{
					result = new List<T>(capacity);
				}
				else
				{
					List<T> list2 = list[num];
					ListPool<T>.inPool.Remove(list2);
					list[num] = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					result = list2;
				}
			}
			return result;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00013A88 File Offset: 0x00011E88
		public static void Warmup(int count, int size)
		{
			object obj = ListPool<T>.pool;
			lock (obj)
			{
				List<T>[] array = new List<T>[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = ListPool<T>.Claim(size);
				}
				for (int j = 0; j < count; j++)
				{
					ListPool<T>.Release(array[j]);
				}
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00013AFC File Offset: 0x00011EFC
		public static void Release(ref List<T> list)
		{
			ListPool<T>.Release(list);
			list = null;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00013B08 File Offset: 0x00011F08
		public static void Release(List<T> list)
		{
			list.ClearFast<T>();
			object obj = ListPool<T>.pool;
			lock (obj)
			{
				if (list.Capacity > 5000)
				{
					ListPool<T>.largePool.Add(list);
					if (ListPool<T>.largePool.Count > 8)
					{
						ListPool<T>.largePool.RemoveAt(0);
					}
				}
				else
				{
					ListPool<T>.pool.Add(list);
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00013B8C File Offset: 0x00011F8C
		public static void Clear()
		{
			object obj = ListPool<T>.pool;
			lock (obj)
			{
				ListPool<T>.pool.Clear();
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00013BCC File Offset: 0x00011FCC
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x040001F3 RID: 499
		private static readonly List<List<T>> pool = new List<List<T>>();

		// Token: 0x040001F4 RID: 500
		private static readonly List<List<T>> largePool = new List<List<T>>();

		// Token: 0x040001F5 RID: 501
		private static readonly HashSet<List<T>> inPool = new HashSet<List<T>>();

		// Token: 0x040001F6 RID: 502
		private const int MaxCapacitySearchLength = 8;

		// Token: 0x040001F7 RID: 503
		private const int LargeThreshold = 5000;

		// Token: 0x040001F8 RID: 504
		private const int MaxLargePoolSize = 8;
	}
}
