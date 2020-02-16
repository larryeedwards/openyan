using System;
using System.Collections.Generic;
using Pathfinding.WindowsStore;

namespace Pathfinding.Serialization
{
	// Token: 0x02000076 RID: 118
	public class GraphMeta
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0001D9D8 File Offset: 0x0001BDD8
		public Type GetGraphType(int index)
		{
			if (string.IsNullOrEmpty(this.typeNames[index]))
			{
				return null;
			}
			Type type = WindowsStoreCompatibility.GetTypeInfo(typeof(AstarPath)).Assembly.GetType(this.typeNames[index]);
			if (!object.Equals(type, null))
			{
				return type;
			}
			throw new Exception("No graph of type '" + this.typeNames[index] + "' could be created, type does not exist");
		}

		// Token: 0x0400033D RID: 829
		public Version version;

		// Token: 0x0400033E RID: 830
		public int graphs;

		// Token: 0x0400033F RID: 831
		public List<string> guids;

		// Token: 0x04000340 RID: 832
		public List<string> typeNames;
	}
}
