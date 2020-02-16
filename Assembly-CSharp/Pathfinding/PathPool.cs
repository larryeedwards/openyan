using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000050 RID: 80
	public static class PathPool
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0001615C File Offset: 0x0001455C
		public static void Pool(Path path)
		{
			object obj = PathPool.pool;
			lock (obj)
			{
				if (((IPathInternals)path).Pooled)
				{
					throw new ArgumentException("The path is already pooled.");
				}
				Stack<Path> stack;
				if (!PathPool.pool.TryGetValue(path.GetType(), out stack))
				{
					stack = new Stack<Path>();
					PathPool.pool[path.GetType()] = stack;
				}
				((IPathInternals)path).Pooled = true;
				((IPathInternals)path).OnEnterPool();
				stack.Push(path);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000161EC File Offset: 0x000145EC
		public static int GetTotalCreated(Type type)
		{
			int result;
			if (PathPool.totalCreated.TryGetValue(type, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00016210 File Offset: 0x00014610
		public static int GetSize(Type type)
		{
			Stack<Path> stack;
			if (PathPool.pool.TryGetValue(type, out stack))
			{
				return stack.Count;
			}
			return 0;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00016238 File Offset: 0x00014638
		public static T GetPath<T>() where T : Path, new()
		{
			object obj = PathPool.pool;
			T result;
			lock (obj)
			{
				Stack<Path> stack;
				T t;
				if (PathPool.pool.TryGetValue(typeof(T), out stack) && stack.Count > 0)
				{
					t = (stack.Pop() as T);
				}
				else
				{
					t = Activator.CreateInstance<T>();
					if (!PathPool.totalCreated.ContainsKey(typeof(T)))
					{
						PathPool.totalCreated[typeof(T)] = 0;
					}
					Dictionary<Type, int> dictionary;
					(dictionary = PathPool.totalCreated)[typeof(T)] = dictionary[typeof(T)] + 1;
				}
				t.Pooled = false;
				t.Reset();
				result = t;
			}
			return result;
		}

		// Token: 0x04000222 RID: 546
		private static readonly Dictionary<Type, Stack<Path>> pool = new Dictionary<Type, Stack<Path>>();

		// Token: 0x04000223 RID: 547
		private static readonly Dictionary<Type, int> totalCreated = new Dictionary<Type, int>();
	}
}
