using System;
using System.Threading;

namespace Pathfinding
{
	// Token: 0x02000056 RID: 86
	internal class ThreadControlQueue
	{
		// Token: 0x060003AB RID: 939 RVA: 0x00017264 File Offset: 0x00015664
		public ThreadControlQueue(int numReceivers)
		{
			this.numReceivers = numReceivers;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001728A File Offset: 0x0001568A
		public bool IsEmpty
		{
			get
			{
				return this.head == null;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00017295 File Offset: 0x00015695
		public bool IsTerminating
		{
			get
			{
				return this.terminate;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000172A0 File Offset: 0x000156A0
		public void Block()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = true;
				this.block.Reset();
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000172EC File Offset: 0x000156EC
		public void Unblock()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = false;
				this.block.Set();
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00017338 File Offset: 0x00015738
		public void Lock()
		{
			Monitor.Enter(this.lockObj);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00017345 File Offset: 0x00015745
		public void Unlock()
		{
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00017354 File Offset: 0x00015754
		public bool AllReceiversBlocked
		{
			get
			{
				object obj = this.lockObj;
				bool result;
				lock (obj)
				{
					result = (this.blocked && this.blockedReceivers == this.numReceivers);
				}
				return result;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000173A8 File Offset: 0x000157A8
		public void PushFront(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						path.next = this.head;
						this.head = path;
					}
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00017454 File Offset: 0x00015854
		public void Push(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						this.tail.next = path;
						this.tail = path;
					}
				}
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00017500 File Offset: 0x00015900
		private void Starving()
		{
			this.starving = true;
			this.block.Reset();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00017518 File Offset: 0x00015918
		public void TerminateReceivers()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.terminate = true;
				this.block.Set();
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00017564 File Offset: 0x00015964
		public Path Pop()
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				while (this.blocked || this.starving)
				{
					this.blockedReceivers++;
					if (this.blockedReceivers > this.numReceivers)
					{
						throw new InvalidOperationException(string.Concat(new object[]
						{
							"More receivers are blocked than specified in constructor (",
							this.blockedReceivers,
							" > ",
							this.numReceivers,
							")"
						}));
					}
					Monitor.Exit(this.lockObj);
					this.block.WaitOne();
					Monitor.Enter(this.lockObj);
					if (this.terminate)
					{
						throw new ThreadControlQueue.QueueTerminationException();
					}
					this.blockedReceivers--;
					if (this.head == null)
					{
						this.Starving();
					}
				}
				Path path = this.head;
				Path next = this.head.next;
				if (next == null)
				{
					this.tail = null;
				}
				this.head.next = null;
				this.head = next;
				result = path;
			}
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000176E0 File Offset: 0x00015AE0
		public void ReceiverTerminated()
		{
			Monitor.Enter(this.lockObj);
			this.blockedReceivers++;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00017708 File Offset: 0x00015B08
		public Path PopNoBlock(bool blockedBefore)
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				if (this.blocked || this.starving)
				{
					if (!blockedBefore)
					{
						this.blockedReceivers++;
						if (this.terminate)
						{
							throw new ThreadControlQueue.QueueTerminationException();
						}
						if (this.blockedReceivers != this.numReceivers)
						{
							if (this.blockedReceivers > this.numReceivers)
							{
								throw new InvalidOperationException(string.Concat(new object[]
								{
									"More receivers are blocked than specified in constructor (",
									this.blockedReceivers,
									" > ",
									this.numReceivers,
									")"
								}));
							}
						}
					}
					result = null;
				}
				else
				{
					if (blockedBefore)
					{
						this.blockedReceivers--;
					}
					Path path = this.head;
					Path next = this.head.next;
					if (next == null)
					{
						this.tail = null;
					}
					this.head.next = null;
					this.head = next;
					result = path;
				}
			}
			return result;
		}

		// Token: 0x04000236 RID: 566
		private Path head;

		// Token: 0x04000237 RID: 567
		private Path tail;

		// Token: 0x04000238 RID: 568
		private readonly object lockObj = new object();

		// Token: 0x04000239 RID: 569
		private readonly int numReceivers;

		// Token: 0x0400023A RID: 570
		private bool blocked;

		// Token: 0x0400023B RID: 571
		private int blockedReceivers;

		// Token: 0x0400023C RID: 572
		private bool starving;

		// Token: 0x0400023D RID: 573
		private bool terminate;

		// Token: 0x0400023E RID: 574
		private ManualResetEvent block = new ManualResetEvent(true);

		// Token: 0x02000057 RID: 87
		public class QueueTerminationException : Exception
		{
		}
	}
}
