using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000040 RID: 64
	internal class GraphUpdateProcessor
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00011EE0 File Offset: 0x000102E0
		public GraphUpdateProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002E0 RID: 736 RVA: 0x00011F4C File Offset: 0x0001034C
		// (remove) Token: 0x060002E1 RID: 737 RVA: 0x00011F84 File Offset: 0x00010384
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action OnGraphsUpdated;

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00011FBA File Offset: 0x000103BA
		public bool IsAnyGraphUpdateQueued
		{
			get
			{
				return this.graphUpdateQueue.Count > 0;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00011FCA File Offset: 0x000103CA
		public bool IsAnyGraphUpdateInProgress
		{
			get
			{
				return this.anyGraphUpdateInProgress;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011FD2 File Offset: 0x000103D2
		public AstarWorkItem GetWorkItem()
		{
			return new AstarWorkItem(new Action(this.QueueGraphUpdatesInternal), new Func<bool, bool>(this.ProcessGraphUpdates));
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011FF4 File Offset: 0x000103F4
		public void EnableMultithreading()
		{
			if (this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
			{
				this.graphUpdateThread = new Thread(new ThreadStart(this.ProcessGraphUpdatesAsync));
				this.graphUpdateThread.IsBackground = true;
				this.graphUpdateThread.Priority = System.Threading.ThreadPriority.Lowest;
				this.graphUpdateThread.Start();
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00012058 File Offset: 0x00010458
		public void DisableMultithreading()
		{
			if (this.graphUpdateThread != null && this.graphUpdateThread.IsAlive)
			{
				this.exitAsyncThread.Set();
				if (!this.graphUpdateThread.Join(5000))
				{
					UnityEngine.Debug.LogError("Graph update thread did not exit in 5 seconds");
				}
				this.graphUpdateThread = null;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000120B2 File Offset: 0x000104B2
		public void AddToQueue(GraphUpdateObject ob)
		{
			this.graphUpdateQueue.Enqueue(ob);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000120C0 File Offset: 0x000104C0
		private void QueueGraphUpdatesInternal()
		{
			bool flag = false;
			while (this.graphUpdateQueue.Count > 0)
			{
				GraphUpdateObject graphUpdateObject = this.graphUpdateQueue.Dequeue();
				if (graphUpdateObject.requiresFloodFill)
				{
					flag = true;
				}
				IEnumerator enumerator = this.astar.data.GetUpdateableGraphs().GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						IUpdatableGraph updatableGraph = (IUpdatableGraph)obj;
						NavGraph graph = updatableGraph as NavGraph;
						if (graphUpdateObject.nnConstraint == null || graphUpdateObject.nnConstraint.SuitableGraph(this.astar.data.GetGraphIndex(graph), graph))
						{
							GraphUpdateProcessor.GUOSingle item = default(GraphUpdateProcessor.GUOSingle);
							item.order = GraphUpdateProcessor.GraphUpdateOrder.GraphUpdate;
							item.obj = graphUpdateObject;
							item.graph = updatableGraph;
							this.graphUpdateQueueRegular.Enqueue(item);
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (flag)
			{
				GraphUpdateProcessor.GUOSingle item2 = default(GraphUpdateProcessor.GUOSingle);
				item2.order = GraphUpdateProcessor.GraphUpdateOrder.FloodFill;
				this.graphUpdateQueueRegular.Enqueue(item2);
			}
			GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
			this.anyGraphUpdateInProgress = true;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000121F0 File Offset: 0x000105F0
		private bool ProcessGraphUpdates(bool force)
		{
			if (force)
			{
				this.asyncGraphUpdatesComplete.WaitOne();
			}
			else if (!this.asyncGraphUpdatesComplete.WaitOne(0))
			{
				return false;
			}
			this.ProcessPostUpdates();
			if (!this.ProcessRegularUpdates(force))
			{
				return false;
			}
			GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
			if (this.OnGraphsUpdated != null)
			{
				this.OnGraphsUpdated();
			}
			this.anyGraphUpdateInProgress = false;
			return true;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00012260 File Offset: 0x00010660
		private bool ProcessRegularUpdates(bool force)
		{
			while (this.graphUpdateQueueRegular.Count > 0)
			{
				GraphUpdateProcessor.GUOSingle item = this.graphUpdateQueueRegular.Peek();
				GraphUpdateThreading graphUpdateThreading = (item.order != GraphUpdateProcessor.GraphUpdateOrder.FloodFill) ? item.graph.CanUpdateAsync(item.obj) : GraphUpdateThreading.SeparateThread;
				if (force || !Application.isPlaying || this.graphUpdateThread == null || !this.graphUpdateThread.IsAlive)
				{
					graphUpdateThreading &= (GraphUpdateThreading)(-2);
				}
				if ((graphUpdateThreading & GraphUpdateThreading.UnityInit) != GraphUpdateThreading.UnityThread)
				{
					if (this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
					item.graph.UpdateAreaInit(item.obj);
				}
				if ((graphUpdateThreading & GraphUpdateThreading.SeparateThread) != GraphUpdateThreading.UnityThread)
				{
					this.graphUpdateQueueRegular.Dequeue();
					this.graphUpdateQueueAsync.Enqueue(item);
					if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread && this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
				}
				else
				{
					if (this.StartAsyncUpdatesIfQueued())
					{
						return false;
					}
					this.graphUpdateQueueRegular.Dequeue();
					if (item.order == GraphUpdateProcessor.GraphUpdateOrder.FloodFill)
					{
						this.FloodFill();
					}
					else
					{
						try
						{
							item.graph.UpdateArea(item.obj);
						}
						catch (Exception arg)
						{
							UnityEngine.Debug.LogError("Error while updating graphs\n" + arg);
						}
					}
					if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread)
					{
						item.graph.UpdateAreaPost(item.obj);
					}
				}
			}
			return !this.StartAsyncUpdatesIfQueued();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000123E0 File Offset: 0x000107E0
		private bool StartAsyncUpdatesIfQueued()
		{
			if (this.graphUpdateQueueAsync.Count > 0)
			{
				this.asyncGraphUpdatesComplete.Reset();
				this.graphUpdateAsyncEvent.Set();
				return true;
			}
			return false;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00012410 File Offset: 0x00010810
		private void ProcessPostUpdates()
		{
			while (this.graphUpdateQueuePost.Count > 0)
			{
				GraphUpdateProcessor.GUOSingle guosingle = this.graphUpdateQueuePost.Dequeue();
				GraphUpdateThreading graphUpdateThreading = guosingle.graph.CanUpdateAsync(guosingle.obj);
				if ((graphUpdateThreading & GraphUpdateThreading.UnityPost) != GraphUpdateThreading.UnityThread)
				{
					try
					{
						guosingle.graph.UpdateAreaPost(guosingle.obj);
					}
					catch (Exception arg)
					{
						UnityEngine.Debug.LogError("Error while updating graphs (post step)\n" + arg);
					}
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001249C File Offset: 0x0001089C
		private void ProcessGraphUpdatesAsync()
		{
			AutoResetEvent[] waitHandles = new AutoResetEvent[]
			{
				this.graphUpdateAsyncEvent,
				this.exitAsyncThread
			};
			for (;;)
			{
				int num = WaitHandle.WaitAny(waitHandles);
				if (num == 1)
				{
					break;
				}
				while (this.graphUpdateQueueAsync.Count > 0)
				{
					GraphUpdateProcessor.GUOSingle item = this.graphUpdateQueueAsync.Dequeue();
					try
					{
						if (item.order == GraphUpdateProcessor.GraphUpdateOrder.GraphUpdate)
						{
							item.graph.UpdateArea(item.obj);
							this.graphUpdateQueuePost.Enqueue(item);
						}
						else
						{
							if (item.order != GraphUpdateProcessor.GraphUpdateOrder.FloodFill)
							{
								throw new NotSupportedException(string.Empty + item.order);
							}
							this.FloodFill();
						}
					}
					catch (Exception arg)
					{
						UnityEngine.Debug.LogError("Exception while updating graphs:\n" + arg);
					}
				}
				this.asyncGraphUpdatesComplete.Set();
			}
			this.graphUpdateQueueAsync.Clear();
			this.asyncGraphUpdatesComplete.Set();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000125AC File Offset: 0x000109AC
		public void FloodFill(GraphNode seed)
		{
			this.FloodFill(seed, this.lastUniqueAreaIndex + 1u);
			this.lastUniqueAreaIndex += 1u;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000125CC File Offset: 0x000109CC
		public void FloodFill(GraphNode seed, uint area)
		{
			if (area > 131071u)
			{
				UnityEngine.Debug.LogError("Too high area index - The maximum area index is " + 131071u);
				return;
			}
			if (area < 0u)
			{
				UnityEngine.Debug.LogError("Too low area index - The minimum area index is 0");
				return;
			}
			Stack<GraphNode> stack = StackPool<GraphNode>.Claim();
			stack.Push(seed);
			seed.Area = area;
			while (stack.Count > 0)
			{
				stack.Pop().FloodFill(stack, area);
			}
			StackPool<GraphNode>.Release(stack);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00012648 File Offset: 0x00010A48
		public void FloodFill()
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
			foreach (NavGraph navGraph in graphs)
			{
				if (navGraph != null)
				{
					navGraph.GetNodes(delegate(GraphNode node)
					{
						node.Area = 0u;
					});
				}
			}
			this.lastUniqueAreaIndex = 0u;
			uint area = 0u;
			int forcedSmallAreas = 0;
			Stack<GraphNode> stack = StackPool<GraphNode>.Claim();
			foreach (NavGraph navGraph2 in graphs)
			{
				if (navGraph2 != null)
				{
					navGraph2.GetNodes(delegate(GraphNode node)
					{
						if (node.Walkable && node.Area == 0u)
						{
							uint area;
							area += 1u;
							area = area;
							if (area > 131071u)
							{
								area -= 1u;
								area = area;
								if (forcedSmallAreas == 0)
								{
									forcedSmallAreas = 1;
								}
								forcedSmallAreas++;
							}
							stack.Clear();
							stack.Push(node);
							int num = 1;
							node.Area = area;
							while (stack.Count > 0)
							{
								num++;
								stack.Pop().FloodFill(stack, area);
							}
						}
					});
				}
			}
			this.lastUniqueAreaIndex = area;
			if (forcedSmallAreas > 0)
			{
				UnityEngine.Debug.LogError(string.Concat(new object[]
				{
					forcedSmallAreas,
					" areas had to share IDs. This usually doesn't affect pathfinding in any significant way (you might get 'Searched whole area but could not find target' as a reason for path failure) however some path requests may take longer to calculate (specifically those that fail with the 'Searched whole area' error).The maximum number of areas is ",
					131071u,
					"."
				}));
			}
			StackPool<GraphNode>.Release(stack);
		}

		// Token: 0x040001D8 RID: 472
		private readonly AstarPath astar;

		// Token: 0x040001D9 RID: 473
		private Thread graphUpdateThread;

		// Token: 0x040001DA RID: 474
		private bool anyGraphUpdateInProgress;

		// Token: 0x040001DB RID: 475
		private readonly Queue<GraphUpdateObject> graphUpdateQueue = new Queue<GraphUpdateObject>();

		// Token: 0x040001DC RID: 476
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueueAsync = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x040001DD RID: 477
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueuePost = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x040001DE RID: 478
		private readonly Queue<GraphUpdateProcessor.GUOSingle> graphUpdateQueueRegular = new Queue<GraphUpdateProcessor.GUOSingle>();

		// Token: 0x040001DF RID: 479
		private readonly ManualResetEvent asyncGraphUpdatesComplete = new ManualResetEvent(true);

		// Token: 0x040001E0 RID: 480
		private readonly AutoResetEvent graphUpdateAsyncEvent = new AutoResetEvent(false);

		// Token: 0x040001E1 RID: 481
		private readonly AutoResetEvent exitAsyncThread = new AutoResetEvent(false);

		// Token: 0x040001E2 RID: 482
		private uint lastUniqueAreaIndex;

		// Token: 0x02000041 RID: 65
		private enum GraphUpdateOrder
		{
			// Token: 0x040001E5 RID: 485
			GraphUpdate,
			// Token: 0x040001E6 RID: 486
			FloodFill
		}

		// Token: 0x02000042 RID: 66
		private struct GUOSingle
		{
			// Token: 0x040001E7 RID: 487
			public GraphUpdateProcessor.GraphUpdateOrder order;

			// Token: 0x040001E8 RID: 488
			public IUpdatableGraph graph;

			// Token: 0x040001E9 RID: 489
			public GraphUpdateObject obj;
		}
	}
}
