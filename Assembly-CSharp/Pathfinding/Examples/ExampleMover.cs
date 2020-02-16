using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200008D RID: 141
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_example_mover.php")]
	public class ExampleMover : MonoBehaviour
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x000231AC File Offset: 0x000215AC
		private void Awake()
		{
			this.agent = base.GetComponent<RVOExampleAgent>();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000231BA File Offset: 0x000215BA
		private void Start()
		{
			this.agent.SetTarget(this.target.position);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000231D2 File Offset: 0x000215D2
		private void LateUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				this.agent.SetTarget(this.target.position);
			}
		}

		// Token: 0x040003B9 RID: 953
		private RVOExampleAgent agent;

		// Token: 0x040003BA RID: 954
		public Transform target;
	}
}
