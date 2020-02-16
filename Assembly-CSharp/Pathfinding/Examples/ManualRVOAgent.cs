using System;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000094 RID: 148
	[RequireComponent(typeof(RVOController))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_manual_r_v_o_agent.php")]
	public class ManualRVOAgent : MonoBehaviour
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x00024B78 File Offset: 0x00022F78
		private void Awake()
		{
			this.rvo = base.GetComponent<RVOController>();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00024B88 File Offset: 0x00022F88
		private void Update()
		{
			float axis = Input.GetAxis("Horizontal");
			float axis2 = Input.GetAxis("Vertical");
			Vector3 vector = new Vector3(axis, 0f, axis2) * this.speed;
			this.rvo.velocity = vector;
			base.transform.position += vector * Time.deltaTime;
		}

		// Token: 0x040003EF RID: 1007
		private RVOController rvo;

		// Token: 0x040003F0 RID: 1008
		public float speed = 1f;
	}
}
