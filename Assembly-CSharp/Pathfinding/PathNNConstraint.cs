using System;

namespace Pathfinding
{
	// Token: 0x02000013 RID: 19
	public class PathNNConstraint : NNConstraint
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000074E4 File Offset: 0x000058E4
		public new static PathNNConstraint Default
		{
			get
			{
				return new PathNNConstraint
				{
					constrainArea = true
				};
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000074FF File Offset: 0x000058FF
		public virtual void SetStart(GraphNode node)
		{
			if (node != null)
			{
				this.area = (int)node.Area;
			}
			else
			{
				this.constrainArea = false;
			}
		}
	}
}
