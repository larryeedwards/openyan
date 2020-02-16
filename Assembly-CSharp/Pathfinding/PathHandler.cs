using System;
using System.Text;

namespace Pathfinding
{
	// Token: 0x02000065 RID: 101
	public class PathHandler
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x00019086 File Offset: 0x00017486
		public PathHandler(int threadID, int totalThreadCount)
		{
			this.threadID = threadID;
			this.totalThreadCount = totalThreadCount;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000190C3 File Offset: 0x000174C3
		public ushort PathID
		{
			get
			{
				return this.pathID;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000190CB File Offset: 0x000174CB
		public void InitializeForPath(Path p)
		{
			this.pathID = p.pathID;
			this.heap.Clear();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000190E4 File Offset: 0x000174E4
		public void DestroyNode(GraphNode node)
		{
			PathNode pathNode = this.GetPathNode(node);
			pathNode.node = null;
			pathNode.parent = null;
			pathNode.pathID = 0;
			pathNode.G = 0u;
			pathNode.H = 0u;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001911C File Offset: 0x0001751C
		public void InitializeNode(GraphNode node)
		{
			int nodeIndex = node.NodeIndex;
			if (nodeIndex >= this.nodes.Length)
			{
				PathNode[] array = new PathNode[Math.Max(128, this.nodes.Length * 2)];
				this.nodes.CopyTo(array, 0);
				for (int i = this.nodes.Length; i < array.Length; i++)
				{
					array[i] = new PathNode();
				}
				this.nodes = array;
			}
			this.nodes[nodeIndex].node = node;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001919D File Offset: 0x0001759D
		public PathNode GetPathNode(int nodeIndex)
		{
			return this.nodes[nodeIndex];
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000191A7 File Offset: 0x000175A7
		public PathNode GetPathNode(GraphNode node)
		{
			return this.nodes[node.NodeIndex];
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000191B8 File Offset: 0x000175B8
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					this.nodes[i].pathID = 0;
				}
			}
		}

		// Token: 0x04000289 RID: 649
		private ushort pathID;

		// Token: 0x0400028A RID: 650
		public readonly int threadID;

		// Token: 0x0400028B RID: 651
		public readonly int totalThreadCount;

		// Token: 0x0400028C RID: 652
		public readonly BinaryHeap heap = new BinaryHeap(128);

		// Token: 0x0400028D RID: 653
		public PathNode[] nodes = new PathNode[0];

		// Token: 0x0400028E RID: 654
		public readonly StringBuilder DebugStringBuilder = new StringBuilder();
	}
}
