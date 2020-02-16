using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000048 RID: 72
	[AddComponentMenu("Pathfinding/Link")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link.php")]
	public class NodeLink : GraphModifier
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00013FE0 File Offset: 0x000123E0
		public Transform Start
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00013FE8 File Offset: 0x000123E8
		public Transform End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00013FF0 File Offset: 0x000123F0
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
			}
			else
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00014027 File Offset: 0x00012427
		public void InternalOnPostScan()
		{
			this.Apply();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001402F File Offset: 0x0001242F
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0001405C File Offset: 0x0001245C
		public virtual void Apply()
		{
			if (this.Start == null || this.End == null || AstarPath.active == null)
			{
				return;
			}
			GraphNode node = AstarPath.active.GetNearest(this.Start.position).node;
			GraphNode node2 = AstarPath.active.GetNearest(this.End.position).node;
			if (node == null || node2 == null)
			{
				return;
			}
			if (this.deleteConnection)
			{
				node.RemoveConnection(node2);
				if (!this.oneWay)
				{
					node2.RemoveConnection(node);
				}
			}
			else
			{
				uint cost = (uint)Math.Round((double)((float)(node.position - node2.position).costMagnitude * this.costFactor));
				node.AddConnection(node2, cost);
				if (!this.oneWay)
				{
					node2.AddConnection(node, cost);
				}
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00014158 File Offset: 0x00012558
		public void OnDrawGizmos()
		{
			if (this.Start == null || this.End == null)
			{
				return;
			}
			Draw.Gizmos.Bezier(this.Start.position, this.End.position, (!this.deleteConnection) ? Color.green : Color.red);
		}

		// Token: 0x040001F9 RID: 505
		public Transform end;

		// Token: 0x040001FA RID: 506
		public float costFactor = 1f;

		// Token: 0x040001FB RID: 507
		public bool oneWay;

		// Token: 0x040001FC RID: 508
		public bool deleteConnection;
	}
}
