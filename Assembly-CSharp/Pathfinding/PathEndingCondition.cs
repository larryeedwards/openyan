using System;

namespace Pathfinding
{
	// Token: 0x02000109 RID: 265
	public abstract class PathEndingCondition
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x00049B4D File Offset: 0x00047F4D
		protected PathEndingCondition()
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00049B55 File Offset: 0x00047F55
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("p");
			}
			this.path = p;
		}

		// Token: 0x060009A9 RID: 2473
		public abstract bool TargetFound(PathNode node);

		// Token: 0x040006B2 RID: 1714
		protected Path path;
	}
}
