using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pathfinding.Examples
{
	// Token: 0x02000089 RID: 137
	[RequireComponent(typeof(Animator))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_hexagon_trigger.php")]
	public class HexagonTrigger : MonoBehaviour
	{
		// Token: 0x06000596 RID: 1430 RVA: 0x000227A5 File Offset: 0x00020BA5
		private void Awake()
		{
			this.anim = base.GetComponent<Animator>();
			this.button.interactable = false;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000227C0 File Offset: 0x00020BC0
		private void OnTriggerEnter(Collider coll)
		{
			TurnBasedAI componentInParent = coll.GetComponentInParent<TurnBasedAI>();
			GraphNode node = AstarPath.active.GetNearest(base.transform.position).node;
			if (componentInParent != null && componentInParent.targetNode == node)
			{
				this.button.interactable = true;
				this.visible = true;
				this.anim.CrossFade("show", 0.1f);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00022832 File Offset: 0x00020C32
		private void OnTriggerExit(Collider coll)
		{
			if (coll.GetComponentInParent<TurnBasedAI>() != null && this.visible)
			{
				this.button.interactable = false;
				this.anim.CrossFade("hide", 0.1f);
			}
		}

		// Token: 0x040003A8 RID: 936
		public Button button;

		// Token: 0x040003A9 RID: 937
		private Animator anim;

		// Token: 0x040003AA RID: 938
		private bool visible;
	}
}
