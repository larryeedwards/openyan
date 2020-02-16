using System;

namespace Pathfinding
{
	// Token: 0x02000100 RID: 256
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00049B75 File Offset: 0x00047F75
		public EndingConditionDistance(Path p, int maxGScore) : base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00049B8D File Offset: 0x00047F8D
		public override bool TargetFound(PathNode node)
		{
			return (ulong)node.G >= (ulong)((long)this.maxGScore);
		}

		// Token: 0x0400068C RID: 1676
		public int maxGScore = 100;
	}
}
