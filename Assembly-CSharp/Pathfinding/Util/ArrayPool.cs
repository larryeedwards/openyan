using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000035 RID: 53
	public static class ArrayPool<T>
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000FF4C File Offset: 0x0000E34C
		public static T[] Claim(int minimumLength)
		{
			if (minimumLength <= 0)
			{
				return ArrayPool<T>.ClaimWithExactLength(0);
			}
			int num = 0;
			while (1 << num < minimumLength && num < 30)
			{
				num++;
			}
			if (num == 30)
			{
				throw new ArgumentException("Too high minimum length");
			}
			object obj = ArrayPool<T>.pool;
			lock (obj)
			{
				if (ArrayPool<T>.pool[num] == null)
				{
					ArrayPool<T>.pool[num] = new Stack<T[]>();
				}
				if (ArrayPool<T>.pool[num].Count > 0)
				{
					T[] array = ArrayPool<T>.pool[num].Pop();
					ArrayPool<T>.inPool.Remove(array);
					return array;
				}
			}
			return new T[1 << num];
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00010018 File Offset: 0x0000E418
		public static T[] ClaimWithExactLength(int length)
		{
			bool flag = length != 0 && (length & length - 1) == 0;
			if (flag)
			{
				return ArrayPool<T>.Claim(length);
			}
			if (length <= 256)
			{
				object obj = ArrayPool<T>.pool;
				lock (obj)
				{
					Stack<T[]> stack = ArrayPool<T>.exactPool[length];
					if (stack != null && stack.Count > 0)
					{
						return stack.Pop();
					}
				}
			}
			return new T[length];
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000100AC File Offset: 0x0000E4AC
		public static void Release(ref T[] array, bool allowNonPowerOfTwo = false)
		{
			if (array == null)
			{
				return;
			}
			if (array.GetType() != typeof(T[]))
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expected array type ",
					typeof(T[]).Name,
					" but found ",
					array.GetType().Name,
					"\nAre you using the correct generic class?\n"
				}));
			}
			bool flag = array.Length != 0 && (array.Length & array.Length - 1) == 0;
			if (!flag && !allowNonPowerOfTwo && array.Length != 0)
			{
				throw new ArgumentException("Length is not a power of 2");
			}
			object obj = ArrayPool<T>.pool;
			lock (obj)
			{
				if (flag)
				{
					int num = 0;
					while (1 << num < array.Length && num < 30)
					{
						num++;
					}
					if (ArrayPool<T>.pool[num] == null)
					{
						ArrayPool<T>.pool[num] = new Stack<T[]>();
					}
					ArrayPool<T>.pool[num].Push(array);
				}
				else if (array.Length <= 256)
				{
					Stack<T[]> stack = ArrayPool<T>.exactPool[array.Length];
					if (stack == null)
					{
						stack = (ArrayPool<T>.exactPool[array.Length] = new Stack<T[]>());
					}
					stack.Push(array);
				}
			}
			array = null;
		}

		// Token: 0x04000182 RID: 386
		private const int MaximumExactArrayLength = 256;

		// Token: 0x04000183 RID: 387
		private static readonly Stack<T[]>[] pool = new Stack<T[]>[31];

		// Token: 0x04000184 RID: 388
		private static readonly Stack<T[]>[] exactPool = new Stack<T[]>[257];

		// Token: 0x04000185 RID: 389
		private static readonly HashSet<T[]> inPool = new HashSet<T[]>();
	}
}
