using System;

namespace Pathfinding
{
	// Token: 0x0200010A RID: 266
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x0004B9DC File Offset: 0x00049DDC
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.abPath = p;
			this.path = p;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0004BA03 File Offset: 0x00049E03
		public override bool TargetFound(PathNode node)
		{
			return node.node == this.abPath.endNode;
		}

		// Token: 0x040006B3 RID: 1715
		protected ABPath abPath;
	}
}
