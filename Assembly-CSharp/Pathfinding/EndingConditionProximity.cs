using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200010B RID: 267
	public class EndingConditionProximity : ABPathEndingCondition
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0004BA18 File Offset: 0x00049E18
		public EndingConditionProximity(ABPath p, float maxDistance) : base(p)
		{
			this.maxDistance = maxDistance;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0004BA34 File Offset: 0x00049E34
		public override bool TargetFound(PathNode node)
		{
			return ((Vector3)node.node.position - this.abPath.originalEndPoint).sqrMagnitude <= this.maxDistance * this.maxDistance;
		}

		// Token: 0x040006B4 RID: 1716
		public float maxDistance = 10f;
	}
}
