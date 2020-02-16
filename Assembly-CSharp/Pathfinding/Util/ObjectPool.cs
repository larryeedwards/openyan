using System;

namespace Pathfinding.Util
{
	// Token: 0x0200004D RID: 77
	public static class ObjectPool<T> where T : class, IAstarPooledObject, new()
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0001584C File Offset: 0x00013C4C
		public static T Claim()
		{
			return ObjectPoolSimple<T>.Claim();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00015854 File Offset: 0x00013C54
		public static void Release(ref T obj)
		{
			T t = obj;
			ObjectPoolSimple<T>.Release(ref obj);
			t.OnEnterPool();
		}
	}
}
