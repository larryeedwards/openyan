using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200000F RID: 15
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_a_i.php")]
	public class TurnBasedAI : VersionedMonoBehaviour
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00007110 File Offset: 0x00005510
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007120 File Offset: 0x00005520
		protected override void Awake()
		{
			base.Awake();
			this.traversalProvider = new BlockManager.TraversalProvider(this.blockManager, BlockManager.BlockMode.AllExceptSelector, new List<SingleNodeBlocker>
			{
				this.blocker
			});
		}

		// Token: 0x04000098 RID: 152
		public int movementPoints = 2;

		// Token: 0x04000099 RID: 153
		public BlockManager blockManager;

		// Token: 0x0400009A RID: 154
		public SingleNodeBlocker blocker;

		// Token: 0x0400009B RID: 155
		public GraphNode targetNode;

		// Token: 0x0400009C RID: 156
		public BlockManager.TraversalProvider traversalProvider;
	}
}
