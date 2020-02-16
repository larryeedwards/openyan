using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinding.Util
{
	// Token: 0x02000119 RID: 281
	public class ParallelWorkQueue<T>
	{
		// Token: 0x06000A16 RID: 2582 RVA: 0x0004D123 File Offset: 0x0004B523
		public ParallelWorkQueue(Queue<T> queue)
		{
			this.queue = queue;
			this.initialCount = queue.Count;
			this.threadCount = Math.Min(this.initialCount, Math.Max(1, AstarPath.CalculateThreadCount(ThreadCount.AutomaticHighLoad)));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0004D15C File Offset: 0x0004B55C
		public IEnumerable<int> Run(int progressTimeoutMillis)
		{
			if (this.initialCount != this.queue.Count)
			{
				throw new InvalidOperationException("Queue has been modified since the constructor");
			}
			if (this.initialCount == 0)
			{
				yield break;
			}
			this.waitEvents = new ManualResetEvent[this.threadCount];
			for (int i = 0; i < this.waitEvents.Length; i++)
			{
				this.waitEvents[i] = new ManualResetEvent(false);
				ThreadPool.QueueUserWorkItem(delegate(object threadIndex)
				{
					this.RunTask((int)threadIndex);
				}, i);
			}
			while (!WaitHandle.WaitAll(this.waitEvents, progressTimeoutMillis))
			{
				object obj = this.queue;
				int count;
				lock (obj)
				{
					count = this.queue.Count;
				}
				yield return this.initialCount - count;
			}
			if (this.innerException != null)
			{
				throw this.innerException;
			}
			yield break;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0004D188 File Offset: 0x0004B588
		private void RunTask(int threadIndex)
		{
			try
			{
				for (;;)
				{
					object obj = this.queue;
					T arg;
					lock (obj)
					{
						if (this.queue.Count == 0)
						{
							break;
						}
						arg = this.queue.Dequeue();
					}
					this.action(arg, threadIndex);
				}
			}
			catch (Exception ex)
			{
				this.innerException = ex;
				object obj2 = this.queue;
				lock (obj2)
				{
					this.queue.Clear();
				}
			}
			finally
			{
				this.waitEvents[threadIndex].Set();
			}
		}

		// Token: 0x040006EE RID: 1774
		public Action<T, int> action;

		// Token: 0x040006EF RID: 1775
		public readonly int threadCount;

		// Token: 0x040006F0 RID: 1776
		private readonly Queue<T> queue;

		// Token: 0x040006F1 RID: 1777
		private readonly int initialCount;

		// Token: 0x040006F2 RID: 1778
		private ManualResetEvent[] waitEvents;

		// Token: 0x040006F3 RID: 1779
		private Exception innerException;
	}
}
