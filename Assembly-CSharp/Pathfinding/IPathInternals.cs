using System;

namespace Pathfinding
{
	// Token: 0x02000063 RID: 99
	internal interface IPathInternals
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600044F RID: 1103
		PathHandler PathHandler { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000450 RID: 1104
		// (set) Token: 0x06000451 RID: 1105
		bool Pooled { get; set; }

		// Token: 0x06000452 RID: 1106
		void AdvanceState(PathState s);

		// Token: 0x06000453 RID: 1107
		void OnEnterPool();

		// Token: 0x06000454 RID: 1108
		void Reset();

		// Token: 0x06000455 RID: 1109
		void ReturnPath();

		// Token: 0x06000456 RID: 1110
		void PrepareBase(PathHandler handler);

		// Token: 0x06000457 RID: 1111
		void Prepare();

		// Token: 0x06000458 RID: 1112
		void Initialize();

		// Token: 0x06000459 RID: 1113
		void Cleanup();

		// Token: 0x0600045A RID: 1114
		void CalculateStep(long targetTick);
	}
}
