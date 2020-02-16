using System;

namespace Pathfinding
{
	// Token: 0x020000FB RID: 251
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class UniqueComponentAttribute : Attribute
	{
		// Token: 0x0400067A RID: 1658
		public string tag;
	}
}
