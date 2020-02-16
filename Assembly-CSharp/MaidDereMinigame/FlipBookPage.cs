using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x0200015C RID: 348
	public class FlipBookPage : MonoBehaviour
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x00056C63 File Offset: 0x00055063
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00056C7D File Offset: 0x0005507D
		public void Transition(bool toOpen)
		{
			this.animator.SetTrigger((!toOpen) ? "ClosePage" : "OpenPage");
			if (this.objectToActivate != null)
			{
				this.objectToActivate.SetActive(false);
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00056CBC File Offset: 0x000550BC
		public void SwitchSort()
		{
			this.spriteRenderer.sortingOrder = 10 - this.spriteRenderer.sortingOrder;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00056CD7 File Offset: 0x000550D7
		public void ObjectActive(bool toActive = true)
		{
			if (this.objectToActivate != null)
			{
				this.objectToActivate.SetActive(toActive);
			}
		}

		// Token: 0x0400087E RID: 2174
		[HideInInspector]
		public Animator animator;

		// Token: 0x0400087F RID: 2175
		[HideInInspector]
		public SpriteRenderer spriteRenderer;

		// Token: 0x04000880 RID: 2176
		public GameObject objectToActivate;
	}
}
