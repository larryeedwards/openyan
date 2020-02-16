using System;

namespace Pathfinding
{
	// Token: 0x020000E8 RID: 232
	public interface IPathModifier
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060008D9 RID: 2265
		int Order { get; }

		// Token: 0x060008DA RID: 2266
		void Apply(Path path);

		// Token: 0x060008DB RID: 2267
		void PreProcess(Path path);
	}
}
