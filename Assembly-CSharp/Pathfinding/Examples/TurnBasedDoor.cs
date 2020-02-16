using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200008A RID: 138
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(SingleNodeBlocker))]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_turn_based_door.php")]
	public class TurnBasedDoor : MonoBehaviour
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x00022879 File Offset: 0x00020C79
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.blocker = base.GetComponent<SingleNodeBlocker>();
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00022893 File Offset: 0x00020C93
		private void Start()
		{
			this.blocker.BlockAtCurrentPosition();
			this.animator.CrossFade("close", 0.2f);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000228B5 File Offset: 0x00020CB5
		public void Close()
		{
			base.StartCoroutine(this.WaitAndClose());
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000228C4 File Offset: 0x00020CC4
		private IEnumerator WaitAndClose()
		{
			List<SingleNodeBlocker> selector = new List<SingleNodeBlocker>
			{
				this.blocker
			};
			GraphNode node = AstarPath.active.GetNearest(base.transform.position).node;
			if (this.blocker.manager.NodeContainsAnyExcept(node, selector))
			{
				this.animator.CrossFade("blocked", 0.2f);
			}
			while (this.blocker.manager.NodeContainsAnyExcept(node, selector))
			{
				yield return null;
			}
			this.open = false;
			this.animator.CrossFade("close", 0.2f);
			this.blocker.BlockAtCurrentPosition();
			yield break;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000228DF File Offset: 0x00020CDF
		public void Open()
		{
			base.StopAllCoroutines();
			this.animator.CrossFade("open", 0.2f);
			this.open = true;
			this.blocker.Unblock();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002290E File Offset: 0x00020D0E
		public void Toggle()
		{
			if (this.open)
			{
				this.Close();
			}
			else
			{
				this.Open();
			}
		}

		// Token: 0x040003AB RID: 939
		private Animator animator;

		// Token: 0x040003AC RID: 940
		private SingleNodeBlocker blocker;

		// Token: 0x040003AD RID: 941
		private bool open;
	}
}
