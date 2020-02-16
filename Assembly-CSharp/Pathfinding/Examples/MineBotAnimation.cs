using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000096 RID: 150
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mine_bot_animation.php")]
	public class MineBotAnimation : VersionedMonoBehaviour
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x00024C2C File Offset: 0x0002302C
		protected override void Awake()
		{
			base.Awake();
			this.ai = base.GetComponent<IAstarAI>();
			this.tr = base.GetComponent<Transform>();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00024C4C File Offset: 0x0002304C
		private void Start()
		{
			this.anim["forward"].layer = 10;
			this.anim.Play("awake");
			this.anim.Play("forward");
			this.anim["awake"].wrapMode = WrapMode.Once;
			this.anim["awake"].speed = 0f;
			this.anim["awake"].normalizedTime = 1f;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00024CDC File Offset: 0x000230DC
		private void OnTargetReached()
		{
			if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
				this.lastTarget = this.tr.position;
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00024D50 File Offset: 0x00023150
		protected void Update()
		{
			if (this.ai.reachedEndOfPath)
			{
				if (!this.isAtDestination)
				{
					this.OnTargetReached();
				}
				this.isAtDestination = true;
			}
			else
			{
				this.isAtDestination = false;
			}
			Vector3 vector = this.tr.InverseTransformDirection(this.ai.velocity);
			vector.y = 0f;
			if (vector.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
			{
				this.anim.Blend("forward", 0f, 0.2f);
			}
			else
			{
				this.anim.Blend("forward", 1f, 0.2f);
				AnimationState animationState = this.anim["forward"];
				float z = vector.z;
				animationState.speed = z * this.animationSpeed;
			}
		}

		// Token: 0x040003F5 RID: 1013
		public Animation anim;

		// Token: 0x040003F6 RID: 1014
		public float sleepVelocity = 0.4f;

		// Token: 0x040003F7 RID: 1015
		public float animationSpeed = 0.2f;

		// Token: 0x040003F8 RID: 1016
		public GameObject endOfPathEffect;

		// Token: 0x040003F9 RID: 1017
		private bool isAtDestination;

		// Token: 0x040003FA RID: 1018
		private IAstarAI ai;

		// Token: 0x040003FB RID: 1019
		private Transform tr;

		// Token: 0x040003FC RID: 1020
		protected Vector3 lastTarget;
	}
}
