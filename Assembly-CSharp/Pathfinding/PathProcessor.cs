using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000052 RID: 82
	public class PathProcessor
	{
		// Token: 0x0600038B RID: 907 RVA: 0x000163D8 File Offset: 0x000147D8
		internal PathProcessor(AstarPath astar, PathReturnQueue returnQueue, int processors, bool multithreaded)
		{
			this.astar = astar;
			this.returnQueue = returnQueue;
			if (processors < 0)
			{
				throw new ArgumentOutOfRangeException("processors");
			}
			if (!multithreaded && processors != 1)
			{
				throw new Exception("Only a single non-multithreaded processor is allowed");
			}
			this.queue = new ThreadControlQueue(processors);
			this.pathHandlers = new PathHandler[processors];
			for (int i = 0; i < processors; i++)
			{
				this.pathHandlers[i] = new PathHandler(i, processors);
			}
			if (multithreaded)
			{
				this.threads = new Thread[processors];
				for (int j = 0; j < processors; j++)
				{
					PathHandler pathHandler = this.pathHandlers[j];
					this.threads[j] = new Thread(delegate()
					{
						this.CalculatePathsThreaded(pathHandler);
					});
					this.threads[j].Name = "Pathfinding Thread " + j;
					this.threads[j].IsBackground = true;
					this.threads[j].Start();
				}
			}
			else
			{
				this.threadCoroutine = this.CalculatePaths(this.pathHandlers[0]);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600038C RID: 908 RVA: 0x00016524 File Offset: 0x00014924
		// (remove) Token: 0x0600038D RID: 909 RVA: 0x0001655C File Offset: 0x0001495C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Path> OnPathPreSearch;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600038E RID: 910 RVA: 0x00016594 File Offset: 0x00014994
		// (remove) Token: 0x0600038F RID: 911 RVA: 0x000165CC File Offset: 0x000149CC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<Path> OnPathPostSearch;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000390 RID: 912 RVA: 0x00016604 File Offset: 0x00014A04
		// (remove) Token: 0x06000391 RID: 913 RVA: 0x0001663C File Offset: 0x00014A3C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnQueueUnblocked;

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00016672 File Offset: 0x00014A72
		public int NumThreads
		{
			get
			{
				return this.pathHandlers.Length;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001667C File Offset: 0x00014A7C
		public bool IsUsingMultithreading
		{
			get
			{
				return this.threads != null;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001668C File Offset: 0x00014A8C
		private int Lock(bool block)
		{
			this.queue.Block();
			if (block)
			{
				while (!this.queue.AllReceiversBlocked)
				{
					if (this.IsUsingMultithreading)
					{
						Thread.Sleep(1);
					}
					else
					{
						this.TickNonMultithreaded();
					}
				}
			}
			this.nextLockID++;
			this.locks.Add(this.nextLockID);
			return this.nextLockID;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00016700 File Offset: 0x00014B00
		private void Unlock(int id)
		{
			if (!this.locks.Remove(id))
			{
				throw new ArgumentException("This lock has already been released");
			}
			if (this.locks.Count == 0)
			{
				if (this.OnQueueUnblocked != null)
				{
					this.OnQueueUnblocked();
				}
				this.queue.Unblock();
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001675A File Offset: 0x00014B5A
		public PathProcessor.GraphUpdateLock PausePathfinding(bool block)
		{
			return new PathProcessor.GraphUpdateLock(this, block);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00016764 File Offset: 0x00014B64
		public void TickNonMultithreaded()
		{
			if (this.threadCoroutine != null)
			{
				try
				{
					this.threadCoroutine.MoveNext();
				}
				catch (Exception ex)
				{
					this.threadCoroutine = null;
					if (!(ex is ThreadControlQueue.QueueTerminationException))
					{
						UnityEngine.Debug.LogException(ex);
						UnityEngine.Debug.LogError("Unhandled exception during pathfinding. Terminating.");
						this.queue.TerminateReceivers();
						try
						{
							this.queue.PopNoBlock(false);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000167F4 File Offset: 0x00014BF4
		public void JoinThreads()
		{
			if (this.threads != null)
			{
				for (int i = 0; i < this.threads.Length; i++)
				{
					if (!this.threads[i].Join(50))
					{
						UnityEngine.Debug.LogError("Could not terminate pathfinding thread[" + i + "] in 50ms, trying Thread.Abort");
						this.threads[i].Abort();
					}
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00016860 File Offset: 0x00014C60
		public void AbortThreads()
		{
			if (this.threads == null)
			{
				return;
			}
			for (int i = 0; i < this.threads.Length; i++)
			{
				if (this.threads[i] != null && this.threads[i].IsAlive)
				{
					this.threads[i].Abort();
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000168C0 File Offset: 0x00014CC0
		public int GetNewNodeIndex()
		{
			return (this.nodeIndexPool.Count <= 0) ? this.nextNodeIndex++ : this.nodeIndexPool.Pop();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00016900 File Offset: 0x00014D00
		public void InitializeNode(GraphNode node)
		{
			if (!this.queue.AllReceiversBlocked)
			{
				throw new Exception("Trying to initialize a node when it is not safe to initialize any nodes. Must be done during a graph update. See http://arongranberg.com/astar/docs/graph-updates.php#direct");
			}
			for (int i = 0; i < this.pathHandlers.Length; i++)
			{
				this.pathHandlers[i].InitializeNode(node);
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00016950 File Offset: 0x00014D50
		public void DestroyNode(GraphNode node)
		{
			if (node.NodeIndex == -1)
			{
				return;
			}
			this.nodeIndexPool.Push(node.NodeIndex);
			for (int i = 0; i < this.pathHandlers.Length; i++)
			{
				this.pathHandlers[i].DestroyNode(node);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000169A4 File Offset: 0x00014DA4
		private void CalculatePathsThreaded(PathHandler pathHandler)
		{
			try
			{
				long num = 100000L;
				long targetTick = DateTime.UtcNow.Ticks + num;
				for (;;)
				{
					Path path = this.queue.Pop();
					IPathInternals pathInternals = path;
					pathInternals.PrepareBase(pathHandler);
					pathInternals.AdvanceState(PathState.Processing);
					if (this.OnPathPreSearch != null)
					{
						this.OnPathPreSearch(path);
					}
					long ticks = DateTime.UtcNow.Ticks;
					pathInternals.Prepare();
					if (!path.IsDone())
					{
						this.astar.debugPathData = pathInternals.PathHandler;
						this.astar.debugPathID = path.pathID;
						pathInternals.Initialize();
						while (!path.IsDone())
						{
							pathInternals.CalculateStep(targetTick);
							targetTick = DateTime.UtcNow.Ticks + num;
							if (this.queue.IsTerminating)
							{
								path.FailWithError("AstarPath object destroyed");
							}
						}
						path.duration = (float)(DateTime.UtcNow.Ticks - ticks) * 0.0001f;
					}
					pathInternals.Cleanup();
					if (path.immediateCallback != null)
					{
						path.immediateCallback(path);
					}
					if (this.OnPathPostSearch != null)
					{
						this.OnPathPostSearch(path);
					}
					this.returnQueue.Enqueue(path);
					pathInternals.AdvanceState(PathState.ReturnQueue);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is ThreadControlQueue.QueueTerminationException)
				{
					if (this.astar.logPathResults == PathLog.Heavy)
					{
						UnityEngine.Debug.LogWarning("Shutting down pathfinding thread #" + pathHandler.threadID);
					}
					return;
				}
				UnityEngine.Debug.LogException(ex);
				UnityEngine.Debug.LogError("Unhandled exception during pathfinding. Terminating.");
				this.queue.TerminateReceivers();
			}
			UnityEngine.Debug.LogError("Error : This part should never be reached.");
			this.queue.ReceiverTerminated();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00016B98 File Offset: 0x00014F98
		private IEnumerator CalculatePaths(PathHandler pathHandler)
		{
			long maxTicks = (long)(this.astar.maxFrameTime * 10000f);
			long targetTick = DateTime.UtcNow.Ticks + maxTicks;
			for (;;)
			{
				Path p = null;
				bool blockedBefore = false;
				while (p == null)
				{
					try
					{
						p = this.queue.PopNoBlock(blockedBefore);
						blockedBefore |= (p == null);
					}
					catch (ThreadControlQueue.QueueTerminationException)
					{
						yield break;
					}
					if (p == null)
					{
						yield return null;
					}
				}
				IPathInternals ip = p;
				maxTicks = (long)(this.astar.maxFrameTime * 10000f);
				ip.PrepareBase(pathHandler);
				ip.AdvanceState(PathState.Processing);
				Action<Path> tmpOnPathPreSearch = this.OnPathPreSearch;
				if (tmpOnPathPreSearch != null)
				{
					tmpOnPathPreSearch(p);
				}
				long startTicks = DateTime.UtcNow.Ticks;
				long totalTicks = 0L;
				ip.Prepare();
				if (!p.IsDone())
				{
					this.astar.debugPathData = ip.PathHandler;
					this.astar.debugPathID = p.pathID;
					ip.Initialize();
					while (!p.IsDone())
					{
						ip.CalculateStep(targetTick);
						if (p.IsDone())
						{
							break;
						}
						totalTicks += DateTime.UtcNow.Ticks - startTicks;
						yield return null;
						startTicks = DateTime.UtcNow.Ticks;
						if (this.queue.IsTerminating)
						{
							p.FailWithError("AstarPath object destroyed");
						}
						targetTick = DateTime.UtcNow.Ticks + maxTicks;
					}
					totalTicks += DateTime.UtcNow.Ticks - startTicks;
					p.duration = (float)totalTicks * 0.0001f;
				}
				ip.Cleanup();
				OnPathDelegate tmpImmediateCallback = p.immediateCallback;
				if (tmpImmediateCallback != null)
				{
					tmpImmediateCallback(p);
				}
				Action<Path> tmpOnPathPostSearch = this.OnPathPostSearch;
				if (tmpOnPathPostSearch != null)
				{
					tmpOnPathPostSearch(p);
				}
				this.returnQueue.Enqueue(p);
				ip.AdvanceState(PathState.ReturnQueue);
				if (DateTime.UtcNow.Ticks > targetTick)
				{
					yield return null;
					targetTick = DateTime.UtcNow.Ticks + maxTicks;
				}
			}
			yield break;
		}

		// Token: 0x04000227 RID: 551
		internal readonly ThreadControlQueue queue;

		// Token: 0x04000228 RID: 552
		private readonly AstarPath astar;

		// Token: 0x04000229 RID: 553
		private readonly PathReturnQueue returnQueue;

		// Token: 0x0400022A RID: 554
		private readonly PathHandler[] pathHandlers;

		// Token: 0x0400022B RID: 555
		private readonly Thread[] threads;

		// Token: 0x0400022C RID: 556
		private IEnumerator threadCoroutine;

		// Token: 0x0400022D RID: 557
		private int nextNodeIndex = 1;

		// Token: 0x0400022E RID: 558
		private readonly Stack<int> nodeIndexPool = new Stack<int>();

		// Token: 0x0400022F RID: 559
		private readonly List<int> locks = new List<int>();

		// Token: 0x04000230 RID: 560
		private int nextLockID;

		// Token: 0x02000053 RID: 83
		public struct GraphUpdateLock
		{
			// Token: 0x0600039F RID: 927 RVA: 0x00016BBA File Offset: 0x00014FBA
			public GraphUpdateLock(PathProcessor pathProcessor, bool block)
			{
				this.pathProcessor = pathProcessor;
				this.id = pathProcessor.Lock(block);
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x00016BD0 File Offset: 0x00014FD0
			public bool Held
			{
				get
				{
					return this.pathProcessor != null && this.pathProcessor.locks.Contains(this.id);
				}
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00016BF6 File Offset: 0x00014FF6
			public void Release()
			{
				this.pathProcessor.Unlock(this.id);
			}

			// Token: 0x04000231 RID: 561
			private PathProcessor pathProcessor;

			// Token: 0x04000232 RID: 562
			private int id;
		}
	}
}
