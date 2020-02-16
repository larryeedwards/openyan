using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000087 RID: 135
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_local_space_rich_a_i.php")]
	public class LocalSpaceRichAI : RichAI
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x000226ED File Offset: 0x00020AED
		private void RefreshTransform()
		{
			this.graph.Refresh();
			this.richPath.transform = this.graph.transformation;
			this.movementPlane = this.graph.transformation;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00022721 File Offset: 0x00020B21
		protected override void Start()
		{
			this.RefreshTransform();
			base.Start();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00022730 File Offset: 0x00020B30
		protected override void CalculatePathRequestEndpoints(out Vector3 start, out Vector3 end)
		{
			this.RefreshTransform();
			base.CalculatePathRequestEndpoints(out start, out end);
			start = this.graph.transformation.InverseTransform(start);
			end = this.graph.transformation.InverseTransform(end);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00022783 File Offset: 0x00020B83
		protected override void Update()
		{
			this.RefreshTransform();
			base.Update();
		}

		// Token: 0x040003A6 RID: 934
		public LocalSpaceGraph graph;
	}
}
