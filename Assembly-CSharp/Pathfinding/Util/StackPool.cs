using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000055 RID: 85
	public static class StackPool<T>
	{
		// Token: 0x060003A6 RID: 934 RVA: 0x0001715C File Offset: 0x0001555C
		public static Stack<T> Claim()
		{
			if (StackPool<T>.pool.Count > 0)
			{
				Stack<T> result = StackPool<T>.pool[StackPool<T>.pool.Count - 1];
				StackPool<T>.pool.RemoveAt(StackPool<T>.pool.Count - 1);
				return result;
			}
			return new Stack<T>();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000171B0 File Offset: 0x000155B0
		public static void Warmup(int count)
		{
			Stack<T>[] array = new Stack<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = StackPool<T>.Claim();
			}
			for (int j = 0; j < count; j++)
			{
				StackPool<T>.Release(array[j]);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000171F8 File Offset: 0x000155F8
		public static void Release(Stack<T> stack)
		{
			for (int i = 0; i < StackPool<T>.pool.Count; i++)
			{
				if (StackPool<T>.pool[i] == stack)
				{
					Debug.LogError("The Stack is released even though it is inside the pool");
				}
			}
			stack.Clear();
			StackPool<T>.pool.Add(stack);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001724C File Offset: 0x0001564C
		public static void Clear()
		{
			StackPool<T>.pool.Clear();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00017258 File Offset: 0x00015658
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x04000235 RID: 565
		private static readonly List<Stack<T>> pool = new List<Stack<T>>();
	}
}
