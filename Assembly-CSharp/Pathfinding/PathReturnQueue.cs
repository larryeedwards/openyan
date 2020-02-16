using System;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000054 RID: 84
	internal class PathReturnQueue
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00017027 File Offset: 0x00015427
		public PathReturnQueue(object pathsClaimedSilentlyBy)
		{
			this.pathsClaimedSilentlyBy = pathsClaimedSilentlyBy;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00017044 File Offset: 0x00015444
		public void Enqueue(Path path)
		{
			object obj = this.pathReturnQueue;
			lock (obj)
			{
				this.pathReturnQueue.Enqueue(path);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00017088 File Offset: 0x00015488
		public void ReturnPaths(bool timeSlice)
		{
			long num = (!timeSlice) ? 0L : (DateTime.UtcNow.Ticks + 10000L);
			int num2 = 0;
			for (;;)
			{
				object obj = this.pathReturnQueue;
				Path path;
				lock (obj)
				{
					if (this.pathReturnQueue.Count == 0)
					{
						return;
					}
					path = this.pathReturnQueue.Dequeue();
				}
				((IPathInternals)path).ReturnPath();
				((IPathInternals)path).AdvanceState(PathState.Returned);
				path.Release(this.pathsClaimedSilentlyBy, true);
				num2++;
				if (num2 > 5 && timeSlice)
				{
					num2 = 0;
					if (DateTime.UtcNow.Ticks >= num)
					{
						break;
					}
				}
			}
		}

		// Token: 0x04000233 RID: 563
		private Queue<Path> pathReturnQueue = new Queue<Path>();

		// Token: 0x04000234 RID: 564
		private object pathsClaimedSilentlyBy;
	}
}
