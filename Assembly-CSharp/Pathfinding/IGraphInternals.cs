using System;
using System.Collections.Generic;
using Pathfinding.Serialization;

namespace Pathfinding
{
	// Token: 0x0200009C RID: 156
	public interface IGraphInternals
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005EE RID: 1518
		// (set) Token: 0x060005EF RID: 1519
		string SerializedEditorSettings { get; set; }

		// Token: 0x060005F0 RID: 1520
		void OnDestroy();

		// Token: 0x060005F1 RID: 1521
		void DestroyAllNodes();

		// Token: 0x060005F2 RID: 1522
		IEnumerable<Progress> ScanInternal();

		// Token: 0x060005F3 RID: 1523
		void SerializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x060005F4 RID: 1524
		void DeserializeExtraInfo(GraphSerializationContext ctx);

		// Token: 0x060005F5 RID: 1525
		void PostDeserialization(GraphSerializationContext ctx);

		// Token: 0x060005F6 RID: 1526
		void DeserializeSettingsCompatibility(GraphSerializationContext ctx);
	}
}
