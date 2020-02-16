using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace AmplifyMotion
{
	// Token: 0x0200029B RID: 667
	internal class WorkerThreadPool
	{
		// Token: 0x0600155E RID: 5470 RVA: 0x000A5F8C File Offset: 0x000A438C
		internal void InitializeAsyncUpdateThreads(int threadCount, bool systemThreadPool)
		{
			if (systemThreadPool)
			{
				this.m_threadPoolFallback = true;
				return;
			}
			try
			{
				this.m_threadPoolSize = threadCount;
				this.m_threadStateQueues = new Queue<MotionState>[this.m_threadPoolSize];
				this.m_threadStateQueueLocks = new object[this.m_threadPoolSize];
				this.m_threadPool = new Thread[this.m_threadPoolSize];
				this.m_threadPoolTerminateSignal = new ManualResetEvent(false);
				this.m_threadPoolContinueSignals = new AutoResetEvent[this.m_threadPoolSize];
				this.m_threadPoolLock = new object();
				this.m_threadPoolIndex = 0;
				for (int i = 0; i < this.m_threadPoolSize; i++)
				{
					this.m_threadStateQueues[i] = new Queue<MotionState>(1024);
					this.m_threadStateQueueLocks[i] = new object();
					this.m_threadPoolContinueSignals[i] = new AutoResetEvent(false);
					this.m_threadPool[i] = new Thread(new ParameterizedThreadStart(WorkerThreadPool.AsyncUpdateThread));
					this.m_threadPool[i].Start(new KeyValuePair<object, int>(this, i));
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[AmplifyMotion] Non-critical error while initializing WorkerThreads. Falling back to using System.Threading.ThreadPool().\n" + ex.Message);
				this.m_threadPoolFallback = true;
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000A60C0 File Offset: 0x000A44C0
		internal void FinalizeAsyncUpdateThreads()
		{
			if (!this.m_threadPoolFallback)
			{
				this.m_threadPoolTerminateSignal.Set();
				for (int i = 0; i < this.m_threadPoolSize; i++)
				{
					if (this.m_threadPool[i].IsAlive)
					{
						this.m_threadPoolContinueSignals[i].Set();
						this.m_threadPool[i].Join();
						this.m_threadPool[i] = null;
					}
					object obj = this.m_threadStateQueueLocks[i];
					lock (obj)
					{
						while (this.m_threadStateQueues[i].Count > 0)
						{
							this.m_threadStateQueues[i].Dequeue().AsyncUpdate();
						}
					}
				}
				this.m_threadStateQueues = null;
				this.m_threadStateQueueLocks = null;
				this.m_threadPoolSize = 0;
				this.m_threadPool = null;
				this.m_threadPoolTerminateSignal = null;
				this.m_threadPoolContinueSignals = null;
				this.m_threadPoolLock = null;
				this.m_threadPoolIndex = 0;
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000A61C0 File Offset: 0x000A45C0
		internal void EnqueueAsyncUpdate(MotionState state)
		{
			if (!this.m_threadPoolFallback)
			{
				object obj = this.m_threadStateQueueLocks[this.m_threadPoolIndex];
				lock (obj)
				{
					this.m_threadStateQueues[this.m_threadPoolIndex].Enqueue(state);
				}
				this.m_threadPoolContinueSignals[this.m_threadPoolIndex].Set();
				this.m_threadPoolIndex++;
				if (this.m_threadPoolIndex >= this.m_threadPoolSize)
				{
					this.m_threadPoolIndex = 0;
				}
			}
			else
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(WorkerThreadPool.AsyncUpdateCallback), state);
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000A626C File Offset: 0x000A466C
		private static void AsyncUpdateCallback(object obj)
		{
			MotionState motionState = (MotionState)obj;
			motionState.AsyncUpdate();
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000A6288 File Offset: 0x000A4688
		private static void AsyncUpdateThread(object obj)
		{
			KeyValuePair<object, int> keyValuePair = (KeyValuePair<object, int>)obj;
			WorkerThreadPool workerThreadPool = (WorkerThreadPool)keyValuePair.Key;
			int value = keyValuePair.Value;
			for (;;)
			{
				try
				{
					workerThreadPool.m_threadPoolContinueSignals[value].WaitOne();
					if (workerThreadPool.m_threadPoolTerminateSignal.WaitOne(0))
					{
						break;
					}
					for (;;)
					{
						MotionState motionState = null;
						object obj2 = workerThreadPool.m_threadStateQueueLocks[value];
						lock (obj2)
						{
							if (workerThreadPool.m_threadStateQueues[value].Count > 0)
							{
								motionState = workerThreadPool.m_threadStateQueues[value].Dequeue();
							}
						}
						if (motionState == null)
						{
							break;
						}
						motionState.AsyncUpdate();
					}
				}
				catch (Exception ex)
				{
					if (ex.GetType() != typeof(ThreadAbortException))
					{
						Debug.LogWarning(ex);
					}
				}
			}
		}

		// Token: 0x04001248 RID: 4680
		private const int ThreadStateQueueCapacity = 1024;

		// Token: 0x04001249 RID: 4681
		internal Queue<MotionState>[] m_threadStateQueues;

		// Token: 0x0400124A RID: 4682
		internal object[] m_threadStateQueueLocks;

		// Token: 0x0400124B RID: 4683
		private int m_threadPoolSize;

		// Token: 0x0400124C RID: 4684
		private ManualResetEvent m_threadPoolTerminateSignal;

		// Token: 0x0400124D RID: 4685
		private AutoResetEvent[] m_threadPoolContinueSignals;

		// Token: 0x0400124E RID: 4686
		private Thread[] m_threadPool;

		// Token: 0x0400124F RID: 4687
		private bool m_threadPoolFallback;

		// Token: 0x04001250 RID: 4688
		internal object m_threadPoolLock;

		// Token: 0x04001251 RID: 4689
		internal int m_threadPoolIndex;
	}
}
