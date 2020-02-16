using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000086 RID: 134
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_local_space_graph.php")]
	public class LocalSpaceGraph : VersionedMonoBehaviour
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00022670 File Offset: 0x00020A70
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00022678 File Offset: 0x00020A78
		public GraphTransform transformation { get; private set; }

		// Token: 0x0600058B RID: 1419 RVA: 0x00022681 File Offset: 0x00020A81
		private void Start()
		{
			this.originalMatrix = base.transform.worldToLocalMatrix;
			base.transform.hasChanged = true;
			this.Refresh();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000226A6 File Offset: 0x00020AA6
		public void Refresh()
		{
			if (base.transform.hasChanged)
			{
				this.transformation = new GraphTransform(base.transform.localToWorldMatrix * this.originalMatrix);
				base.transform.hasChanged = false;
			}
		}

		// Token: 0x040003A4 RID: 932
		private Matrix4x4 originalMatrix;
	}
}
