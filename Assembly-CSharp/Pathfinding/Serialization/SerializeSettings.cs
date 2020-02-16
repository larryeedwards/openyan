using System;

namespace Pathfinding.Serialization
{
	// Token: 0x02000077 RID: 119
	public class SerializeSettings
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001DA60 File Offset: 0x0001BE60
		public static SerializeSettings Settings
		{
			get
			{
				return new SerializeSettings
				{
					nodes = false
				};
			}
		}

		// Token: 0x04000341 RID: 833
		public bool nodes = true;

		// Token: 0x04000342 RID: 834
		[Obsolete("There is no support for pretty printing the json anymore")]
		public bool prettyPrint;

		// Token: 0x04000343 RID: 835
		public bool editorSettings;
	}
}
