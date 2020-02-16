using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000002 RID: 2
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020B6 File Offset: 0x000004B6
		private void OnEnable()
		{
			this.ai = base.GetComponent<IAstarAI>();
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Combine(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F6 File Offset: 0x000004F6
		private void OnDisable()
		{
			if (this.ai != null)
			{
				IAstarAI astarAI = this.ai;
				astarAI.onSearchPath = (Action)Delegate.Remove(astarAI.onSearchPath, new Action(this.Update));
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000212A File Offset: 0x0000052A
		private void Update()
		{
			if (this.target != null && this.ai != null)
			{
				this.ai.destination = this.target.position;
			}
		}

		// Token: 0x04000001 RID: 1
		public Transform target;

		// Token: 0x04000002 RID: 2
		public IAstarAI ai;
	}
}
