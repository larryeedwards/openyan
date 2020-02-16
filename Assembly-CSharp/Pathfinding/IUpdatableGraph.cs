using System;

namespace Pathfinding
{
	// Token: 0x02000017 RID: 23
	public interface IUpdatableGraph
	{
		// Token: 0x06000125 RID: 293
		void UpdateArea(GraphUpdateObject o);

		// Token: 0x06000126 RID: 294
		void UpdateAreaInit(GraphUpdateObject o);

		// Token: 0x06000127 RID: 295
		void UpdateAreaPost(GraphUpdateObject o);

		// Token: 0x06000128 RID: 296
		GraphUpdateThreading CanUpdateAsync(GraphUpdateObject o);
	}
}
