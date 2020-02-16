using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000112 RID: 274
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_block_manager.php")]
	public class BlockManager : VersionedMonoBehaviour
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0004C742 File Offset: 0x0004AB42
		private void Start()
		{
			if (!AstarPath.active)
			{
				throw new Exception("No AstarPath object in the scene");
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0004C760 File Offset: 0x0004AB60
		public bool NodeContainsAnyOf(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker objA = list[i];
				for (int j = 0; j < selector.Count; j++)
				{
					if (object.ReferenceEquals(objA, selector[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0004C7D0 File Offset: 0x0004ABD0
		public bool NodeContainsAnyExcept(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker objA = list[i];
				bool flag = false;
				for (int j = 0; j < selector.Count; j++)
				{
					if (object.ReferenceEquals(objA, selector[j]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0004C854 File Offset: 0x0004AC54
		public void InternalBlock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate()
			{
				List<SingleNodeBlocker> list;
				if (!this.blocked.TryGetValue(node, out list))
				{
					List<SingleNodeBlocker> list2 = ListPool<SingleNodeBlocker>.Claim();
					this.blocked[node] = list2;
					list = list2;
				}
				list.Add(blocker);
			}, null));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0004C898 File Offset: 0x0004AC98
		public void InternalUnblock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate()
			{
				List<SingleNodeBlocker> list;
				if (this.blocked.TryGetValue(node, out list))
				{
					list.Remove(blocker);
					if (list.Count == 0)
					{
						this.blocked.Remove(node);
						ListPool<SingleNodeBlocker>.Release(ref list);
					}
				}
			}, null));
		}

		// Token: 0x040006E2 RID: 1762
		private Dictionary<GraphNode, List<SingleNodeBlocker>> blocked = new Dictionary<GraphNode, List<SingleNodeBlocker>>();

		// Token: 0x02000113 RID: 275
		public enum BlockMode
		{
			// Token: 0x040006E4 RID: 1764
			AllExceptSelector,
			// Token: 0x040006E5 RID: 1765
			OnlySelector
		}

		// Token: 0x02000114 RID: 276
		public class TraversalProvider : ITraversalProvider
		{
			// Token: 0x060009FE RID: 2558 RVA: 0x0004C8DC File Offset: 0x0004ACDC
			public TraversalProvider(BlockManager blockManager, BlockManager.BlockMode mode, List<SingleNodeBlocker> selector)
			{
				if (blockManager == null)
				{
					throw new ArgumentNullException("blockManager");
				}
				if (selector == null)
				{
					throw new ArgumentNullException("selector");
				}
				this.blockManager = blockManager;
				this.mode = mode;
				this.selector = selector;
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060009FF RID: 2559 RVA: 0x0004C92C File Offset: 0x0004AD2C
			// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0004C934 File Offset: 0x0004AD34
			public BlockManager.BlockMode mode { get; private set; }

			// Token: 0x06000A01 RID: 2561 RVA: 0x0004C940 File Offset: 0x0004AD40
			public bool CanTraverse(Path path, GraphNode node)
			{
				if (!node.Walkable || (path.enabledTags >> (int)node.Tag & 1) == 0)
				{
					return false;
				}
				if (this.mode == BlockManager.BlockMode.OnlySelector)
				{
					return !this.blockManager.NodeContainsAnyOf(node, this.selector);
				}
				return !this.blockManager.NodeContainsAnyExcept(node, this.selector);
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x0004C9A8 File Offset: 0x0004ADA8
			public uint GetTraversalCost(Path path, GraphNode node)
			{
				return path.GetTagPenalty((int)node.Tag) + node.Penalty;
			}

			// Token: 0x040006E6 RID: 1766
			private readonly BlockManager blockManager;

			// Token: 0x040006E8 RID: 1768
			private readonly List<SingleNodeBlocker> selector;
		}
	}
}
