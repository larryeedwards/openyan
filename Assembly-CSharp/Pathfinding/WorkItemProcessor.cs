using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005B RID: 91
	internal class WorkItemProcessor : IWorkItemContext
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x000178FE File Offset: 0x00015CFE
		public WorkItemProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00017918 File Offset: 0x00015D18
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00017920 File Offset: 0x00015D20
		public bool workItemsInProgressRightNow { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00017929 File Offset: 0x00015D29
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00017931 File Offset: 0x00015D31
		public bool workItemsInProgress { get; private set; }

		// Token: 0x060003C8 RID: 968 RVA: 0x0001793A File Offset: 0x00015D3A
		void IWorkItemContext.QueueFloodFill()
		{
			this.queuedWorkItemFloodFill = true;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00017943 File Offset: 0x00015D43
		public void EnsureValidFloodFill()
		{
			if (this.queuedWorkItemFloodFill)
			{
				this.astar.FloodFill();
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001795B File Offset: 0x00015D5B
		public void OnFloodFill()
		{
			this.queuedWorkItemFloodFill = false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00017964 File Offset: 0x00015D64
		public void AddWorkItem(AstarWorkItem item)
		{
			this.workItems.Enqueue(item);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00017974 File Offset: 0x00015D74
		public bool ProcessWorkItems(bool force)
		{
			if (this.workItemsInProgressRightNow)
			{
				throw new Exception("Processing work items recursively. Please do not wait for other work items to be completed inside work items. If you think this is not caused by any of your scripts, this might be a bug.");
			}
			this.workItemsInProgressRightNow = true;
			this.astar.data.LockGraphStructure(true);
			while (this.workItems.Count > 0)
			{
				if (!this.workItemsInProgress)
				{
					this.workItemsInProgress = true;
					this.queuedWorkItemFloodFill = false;
				}
				AstarWorkItem value = this.workItems[0];
				if (value.init != null)
				{
					value.init();
					value.init = null;
				}
				if (value.initWithContext != null)
				{
					value.initWithContext(this);
					value.initWithContext = null;
				}
				this.workItems[0] = value;
				bool flag;
				try
				{
					if (value.update != null)
					{
						flag = value.update(force);
					}
					else
					{
						flag = (value.updateWithContext == null || value.updateWithContext(this, force));
					}
				}
				catch
				{
					this.workItems.Dequeue();
					this.workItemsInProgressRightNow = false;
					this.astar.data.UnlockGraphStructure();
					throw;
				}
				if (!flag)
				{
					if (force)
					{
						Debug.LogError("Misbehaving WorkItem. 'force'=true but the work item did not complete.\nIf force=true is passed to a WorkItem it should always return true.");
					}
					this.workItemsInProgressRightNow = false;
					this.astar.data.UnlockGraphStructure();
					return false;
				}
				this.workItems.Dequeue();
			}
			this.EnsureValidFloodFill();
			this.workItemsInProgressRightNow = false;
			this.workItemsInProgress = false;
			this.astar.data.UnlockGraphStructure();
			return true;
		}

		// Token: 0x04000244 RID: 580
		private readonly AstarPath astar;

		// Token: 0x04000245 RID: 581
		private readonly WorkItemProcessor.IndexedQueue<AstarWorkItem> workItems = new WorkItemProcessor.IndexedQueue<AstarWorkItem>();

		// Token: 0x04000246 RID: 582
		private bool queuedWorkItemFloodFill;

		// Token: 0x0200005C RID: 92
		private class IndexedQueue<T>
		{
			// Token: 0x1700009B RID: 155
			public T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					return this.buffer[(this.start + index) % this.buffer.Length];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this.buffer[(this.start + index) % this.buffer.Length] = value;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x00017B9B File Offset: 0x00015F9B
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x00017BA3 File Offset: 0x00015FA3
			public int Count { get; private set; }

			// Token: 0x060003D2 RID: 978 RVA: 0x00017BAC File Offset: 0x00015FAC
			public void Enqueue(T item)
			{
				if (this.Count == this.buffer.Length)
				{
					T[] array = new T[this.buffer.Length * 2];
					for (int i = 0; i < this.Count; i++)
					{
						array[i] = this[i];
					}
					this.buffer = array;
					this.start = 0;
				}
				this.buffer[(this.start + this.Count) % this.buffer.Length] = item;
				this.Count++;
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x00017C40 File Offset: 0x00016040
			public T Dequeue()
			{
				if (this.Count == 0)
				{
					throw new InvalidOperationException();
				}
				T result = this.buffer[this.start];
				this.start = (this.start + 1) % this.buffer.Length;
				this.Count--;
				return result;
			}

			// Token: 0x04000248 RID: 584
			private T[] buffer = new T[4];

			// Token: 0x04000249 RID: 585
			private int start;
		}
	}
}
