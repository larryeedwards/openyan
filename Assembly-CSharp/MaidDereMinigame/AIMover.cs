using System;
using UnityEngine;

namespace MaidDereMinigame
{
	// Token: 0x02000144 RID: 324
	public abstract class AIMover : MonoBehaviour
	{
		// Token: 0x06000B21 RID: 2849
		public abstract ControlInput GetInput();

		// Token: 0x06000B22 RID: 2850 RVA: 0x0005416C File Offset: 0x0005256C
		private void FixedUpdate()
		{
			ControlInput input = this.GetInput();
			base.transform.Translate(new Vector2(input.horizontal, 0f) * Time.fixedDeltaTime * this.moveSpeed);
		}

		// Token: 0x04000804 RID: 2052
		protected float moveSpeed = 3f;
	}
}
