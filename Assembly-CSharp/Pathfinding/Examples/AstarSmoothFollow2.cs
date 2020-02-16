using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000091 RID: 145
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_astar_smooth_follow2.php")]
	public class AstarSmoothFollow2 : MonoBehaviour
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x000244D0 File Offset: 0x000228D0
		private void LateUpdate()
		{
			Vector3 b;
			if (this.staticOffset)
			{
				b = this.target.position + new Vector3(0f, this.height, this.distance);
			}
			else if (this.followBehind)
			{
				b = this.target.TransformPoint(0f, this.height, -this.distance);
			}
			else
			{
				b = this.target.TransformPoint(0f, this.height, this.distance);
			}
			base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.damping);
			if (this.smoothRotation)
			{
				Quaternion b2 = Quaternion.LookRotation(this.target.position - base.transform.position, this.target.up);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * this.rotationDamping);
			}
			else
			{
				base.transform.LookAt(this.target, this.target.up);
			}
		}

		// Token: 0x040003D8 RID: 984
		public Transform target;

		// Token: 0x040003D9 RID: 985
		public float distance = 3f;

		// Token: 0x040003DA RID: 986
		public float height = 3f;

		// Token: 0x040003DB RID: 987
		public float damping = 5f;

		// Token: 0x040003DC RID: 988
		public bool smoothRotation = true;

		// Token: 0x040003DD RID: 989
		public bool followBehind = true;

		// Token: 0x040003DE RID: 990
		public float rotationDamping = 10f;

		// Token: 0x040003DF RID: 991
		public bool staticOffset;
	}
}
