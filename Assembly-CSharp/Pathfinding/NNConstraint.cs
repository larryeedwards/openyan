using System;

namespace Pathfinding
{
	// Token: 0x02000012 RID: 18
	public class NNConstraint
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000740B File Offset: 0x0000580B
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return (this.graphMask >> graphIndex & 1) != 0;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007420 File Offset: 0x00005820
		public virtual bool Suitable(GraphNode node)
		{
			return (!this.constrainWalkability || node.Walkable == this.walkable) && (!this.constrainArea || this.area < 0 || (ulong)node.Area == (ulong)((long)this.area)) && (!this.constrainTags || (this.tags >> (int)node.Tag & 1) != 0);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000749C File Offset: 0x0000589C
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000074A4 File Offset: 0x000058A4
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x040000B0 RID: 176
		public int graphMask = -1;

		// Token: 0x040000B1 RID: 177
		public bool constrainArea;

		// Token: 0x040000B2 RID: 178
		public int area = -1;

		// Token: 0x040000B3 RID: 179
		public bool constrainWalkability = true;

		// Token: 0x040000B4 RID: 180
		public bool walkable = true;

		// Token: 0x040000B5 RID: 181
		public bool distanceXZ;

		// Token: 0x040000B6 RID: 182
		public bool constrainTags = true;

		// Token: 0x040000B7 RID: 183
		public int tags = -1;

		// Token: 0x040000B8 RID: 184
		public bool constrainDistance = true;
	}
}
