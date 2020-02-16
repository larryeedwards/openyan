using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000018 RID: 24
	public class GraphUpdateObject
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00007648 File Offset: 0x00005A48
		public GraphUpdateObject()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007677 File Offset: 0x00005A77
		public GraphUpdateObject(Bounds b)
		{
			this.bounds = b;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000076B0 File Offset: 0x00005AB0
		public virtual void WillUpdateNode(GraphNode node)
		{
			if (this.trackChangedNodes && node != null)
			{
				if (this.changedNodes == null)
				{
					this.changedNodes = ListPool<GraphNode>.Claim();
					this.backupData = ListPool<uint>.Claim();
					this.backupPositionData = ListPool<Int3>.Claim();
				}
				this.changedNodes.Add(node);
				this.backupPositionData.Add(node.position);
				this.backupData.Add(node.Penalty);
				this.backupData.Add(node.Flags);
				GridNode gridNode = node as GridNode;
				if (gridNode != null)
				{
					this.backupData.Add((uint)gridNode.InternalGridFlags);
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007758 File Offset: 0x00005B58
		public virtual void RevertFromBackup()
		{
			if (!this.trackChangedNodes)
			{
				throw new InvalidOperationException("Changed nodes have not been tracked, cannot revert from backup. Please set trackChangedNodes to true before applying the update.");
			}
			if (this.changedNodes == null)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < this.changedNodes.Count; i++)
			{
				this.changedNodes[i].Penalty = this.backupData[num];
				num++;
				this.changedNodes[i].Flags = this.backupData[num];
				num++;
				GridNode gridNode = this.changedNodes[i] as GridNode;
				if (gridNode != null)
				{
					gridNode.InternalGridFlags = (ushort)this.backupData[num];
					num++;
				}
				this.changedNodes[i].position = this.backupPositionData[i];
			}
			ListPool<GraphNode>.Release(ref this.changedNodes);
			ListPool<uint>.Release(ref this.backupData);
			ListPool<Int3>.Release(ref this.backupPositionData);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000785C File Offset: 0x00005C5C
		public virtual void Apply(GraphNode node)
		{
			if (this.shape == null || this.shape.Contains(node))
			{
				node.Penalty = (uint)((ulong)node.Penalty + (ulong)((long)this.addPenalty));
				if (this.modifyWalkability)
				{
					node.Walkable = this.setWalkability;
				}
				if (this.modifyTag)
				{
					node.Tag = (uint)this.setTag;
				}
			}
		}

		// Token: 0x040000C1 RID: 193
		public Bounds bounds;

		// Token: 0x040000C2 RID: 194
		public bool requiresFloodFill = true;

		// Token: 0x040000C3 RID: 195
		public bool updatePhysics = true;

		// Token: 0x040000C4 RID: 196
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x040000C5 RID: 197
		public bool updateErosion = true;

		// Token: 0x040000C6 RID: 198
		public NNConstraint nnConstraint = NNConstraint.None;

		// Token: 0x040000C7 RID: 199
		public int addPenalty;

		// Token: 0x040000C8 RID: 200
		public bool modifyWalkability;

		// Token: 0x040000C9 RID: 201
		public bool setWalkability;

		// Token: 0x040000CA RID: 202
		public bool modifyTag;

		// Token: 0x040000CB RID: 203
		public int setTag;

		// Token: 0x040000CC RID: 204
		public bool trackChangedNodes;

		// Token: 0x040000CD RID: 205
		public List<GraphNode> changedNodes;

		// Token: 0x040000CE RID: 206
		private List<uint> backupData;

		// Token: 0x040000CF RID: 207
		private List<Int3> backupPositionData;

		// Token: 0x040000D0 RID: 208
		public GraphUpdateShape shape;
	}
}
