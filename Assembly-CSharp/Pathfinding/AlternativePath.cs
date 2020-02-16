using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E6 RID: 230
	[AddComponentMenu("Pathfinding/Modifiers/Alternative Path")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_alternative_path.php")]
	[Serializable]
	public class AlternativePath : MonoModifier
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00044E8E File Offset: 0x0004328E
		public override int Order
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00044E92 File Offset: 0x00043292
		public override void Apply(Path p)
		{
			if (this == null)
			{
				return;
			}
			this.ApplyNow(p.path);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00044EAD File Offset: 0x000432AD
		protected void OnDestroy()
		{
			this.destroyed = true;
			this.ClearOnDestroy();
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00044EBC File Offset: 0x000432BC
		private void ClearOnDestroy()
		{
			this.InversePrevious();
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00044EC4 File Offset: 0x000432C4
		private void InversePrevious()
		{
			if (this.prevNodes != null)
			{
				bool flag = false;
				for (int i = 0; i < this.prevNodes.Count; i++)
				{
					if ((ulong)this.prevNodes[i].Penalty < (ulong)((long)this.prevPenalty))
					{
						flag = true;
						this.prevNodes[i].Penalty = 0u;
					}
					else
					{
						this.prevNodes[i].Penalty = (uint)((ulong)this.prevNodes[i].Penalty - (ulong)((long)this.prevPenalty));
					}
				}
				if (flag)
				{
					Debug.LogWarning("Penalty for some nodes has been reset while the AlternativePath modifier was active (possibly because of a graph update). Some penalties might be incorrect (they may be lower than expected for the affected nodes)");
				}
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00044F70 File Offset: 0x00043370
		private void ApplyNow(List<GraphNode> nodes)
		{
			this.InversePrevious();
			this.prevNodes.Clear();
			if (this.destroyed)
			{
				return;
			}
			if (nodes != null)
			{
				int num = this.rnd.Next(this.randomStep);
				for (int i = num; i < nodes.Count; i += this.rnd.Next(1, this.randomStep))
				{
					nodes[i].Penalty = (uint)((ulong)nodes[i].Penalty + (ulong)((long)this.penalty));
					this.prevNodes.Add(nodes[i]);
				}
			}
			this.prevPenalty = this.penalty;
		}

		// Token: 0x040005FE RID: 1534
		public int penalty = 1000;

		// Token: 0x040005FF RID: 1535
		public int randomStep = 10;

		// Token: 0x04000600 RID: 1536
		private List<GraphNode> prevNodes = new List<GraphNode>();

		// Token: 0x04000601 RID: 1537
		private int prevPenalty;

		// Token: 0x04000602 RID: 1538
		private readonly System.Random rnd = new System.Random();

		// Token: 0x04000603 RID: 1539
		private bool destroyed;
	}
}
