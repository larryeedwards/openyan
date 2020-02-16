using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000095 RID: 149
	[RequireComponent(typeof(Seeker))]
	[Obsolete("This script has been replaced by Pathfinding.Examples.MineBotAnimation. Any uses of this script in the Unity editor will be automatically replaced by one AIPath component and one MineBotAnimation component.")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_a_i.php")]
	public class MineBotAI : AIPath
	{
		// Token: 0x040003F1 RID: 1009
		public Animation anim;

		// Token: 0x040003F2 RID: 1010
		public float sleepVelocity = 0.4f;

		// Token: 0x040003F3 RID: 1011
		public float animationSpeed = 0.2f;

		// Token: 0x040003F4 RID: 1012
		public GameObject endOfPathEffect;
	}
}
