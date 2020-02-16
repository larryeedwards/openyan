using System;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding.Legacy
{
	// Token: 0x020000E0 RID: 224
	[AddComponentMenu("Pathfinding/Legacy/Local Avoidance/Legacy RVO Controller")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_legacy_1_1_legacy_r_v_o_controller.php")]
	public class LegacyRVOController : RVOController
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x00043898 File Offset: 0x00041C98
		public void Update()
		{
			if (base.rvoAgent == null)
			{
				return;
			}
			Vector3 a = this.tr.position + base.CalculateMovementDelta(Time.deltaTime);
			RaycastHit raycastHit;
			if (this.mask != 0 && Physics.Raycast(a + Vector3.up * this.height * 0.5f, Vector3.down, out raycastHit, float.PositiveInfinity, this.mask))
			{
				a.y = raycastHit.point.y;
			}
			else
			{
				a.y = 0f;
			}
			this.tr.position = a + Vector3.up * (this.height * 0.5f - this.center);
			if (this.enableRotation && base.velocity != Vector3.zero)
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(base.velocity), Time.deltaTime * this.rotationSpeed * Mathf.Min(base.velocity.magnitude, 0.2f));
			}
		}

		// Token: 0x040005D1 RID: 1489
		[Tooltip("Layer mask for the ground. The RVOController will raycast down to check for the ground to figure out where to place the agent")]
		public new LayerMask mask = -1;

		// Token: 0x040005D2 RID: 1490
		public new bool enableRotation = true;

		// Token: 0x040005D3 RID: 1491
		public new float rotationSpeed = 30f;
	}
}
