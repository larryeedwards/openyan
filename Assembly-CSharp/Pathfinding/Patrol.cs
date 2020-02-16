using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000003 RID: 3
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_patrol.php")]
	public class Patrol : VersionedMonoBehaviour
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002171 File Offset: 0x00000571
		protected override void Awake()
		{
			base.Awake();
			this.agent = base.GetComponent<IAstarAI>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000588
		private void Update()
		{
			if (this.targets.Length == 0)
			{
				return;
			}
			bool flag = false;
			if (this.agent.reachedEndOfPath && !this.agent.pathPending && float.IsPositiveInfinity(this.switchTime))
			{
				this.switchTime = Time.time + this.delay;
			}
			if (Time.time >= this.switchTime)
			{
				this.index++;
				flag = true;
				this.switchTime = float.PositiveInfinity;
			}
			this.index %= this.targets.Length;
			this.agent.destination = this.targets[this.index].position;
			if (flag)
			{
				this.agent.SearchPath();
			}
		}

		// Token: 0x04000003 RID: 3
		public Transform[] targets;

		// Token: 0x04000004 RID: 4
		public float delay;

		// Token: 0x04000005 RID: 5
		private int index;

		// Token: 0x04000006 RID: 6
		private IAstarAI agent;

		// Token: 0x04000007 RID: 7
		private float switchTime = float.PositiveInfinity;
	}
}
