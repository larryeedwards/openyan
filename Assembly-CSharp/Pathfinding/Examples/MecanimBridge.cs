using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x0200008E RID: 142
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_mecanim_bridge.php")]
	public class MecanimBridge : VersionedMonoBehaviour
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x00023218 File Offset: 0x00021618
		protected override void Awake()
		{
			base.Awake();
			this.ai = base.GetComponent<IAstarAI>();
			this.anim = base.GetComponent<Animator>();
			this.tr = base.transform;
			this.footTransforms = new Transform[]
			{
				this.anim.GetBoneTransform(HumanBodyBones.LeftFoot),
				this.anim.GetBoneTransform(HumanBodyBones.RightFoot)
			};
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002327C File Offset: 0x0002167C
		private void Update()
		{
			AIBase aibase = this.ai as AIBase;
			aibase.canMove = false;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002329C File Offset: 0x0002169C
		private Vector3 CalculateBlendPoint()
		{
			if (this.footTransforms[0] == null || this.footTransforms[1] == null)
			{
				return this.tr.position;
			}
			Vector3 position = this.footTransforms[0].position;
			Vector3 position2 = this.footTransforms[1].position;
			Vector3 vector = (position - this.prevFootPos[0]) / Time.deltaTime;
			Vector3 vector2 = (position2 - this.prevFootPos[1]) / Time.deltaTime;
			float num = vector.magnitude + vector2.magnitude;
			float t = (num <= 0f) ? 0.5f : (vector.magnitude / num);
			this.prevFootPos[0] = position;
			this.prevFootPos[1] = position2;
			return Vector3.Lerp(position, position2, t);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000233A0 File Offset: 0x000217A0
		private void OnAnimatorMove()
		{
			Vector3 vector;
			Quaternion quaternion;
			this.ai.MovementUpdate(Time.deltaTime, out vector, out quaternion);
			Vector3 desiredVelocity = this.ai.desiredVelocity;
			Vector3 direction = desiredVelocity;
			direction.y = 0f;
			this.anim.SetFloat("InputMagnitude", (!this.ai.reachedEndOfPath && direction.magnitude >= 0.1f) ? 1f : 0f);
			Vector3 b = this.tr.InverseTransformDirection(direction);
			this.smoothedVelocity = Vector3.Lerp(this.smoothedVelocity, b, (this.velocitySmoothing <= 0f) ? 1f : (Time.deltaTime / this.velocitySmoothing));
			if (this.smoothedVelocity.magnitude < 0.4f)
			{
				this.smoothedVelocity = this.smoothedVelocity.normalized * 0.4f;
			}
			this.anim.SetFloat("X", this.smoothedVelocity.x);
			this.anim.SetFloat("Y", this.smoothedVelocity.z);
			Quaternion quaternion2 = this.RotateTowards(direction, Time.deltaTime * (this.ai as AIPath).rotationSpeed);
			vector = this.ai.position;
			quaternion = this.ai.rotation;
			vector = MecanimBridge.RotatePointAround(vector, this.CalculateBlendPoint(), quaternion2 * Quaternion.Inverse(quaternion));
			quaternion = quaternion2;
			quaternion = this.anim.deltaRotation * quaternion;
			Vector3 deltaPosition = this.anim.deltaPosition;
			deltaPosition.y = desiredVelocity.y * Time.deltaTime;
			vector += deltaPosition;
			this.ai.FinalizeMovement(vector, quaternion);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00023565 File Offset: 0x00021965
		private static Vector3 RotatePointAround(Vector3 point, Vector3 around, Quaternion rotation)
		{
			return rotation * (point - around) + around;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002357C File Offset: 0x0002197C
		protected virtual Quaternion RotateTowards(Vector3 direction, float maxDegrees)
		{
			if (direction != Vector3.zero)
			{
				Quaternion to = Quaternion.LookRotation(direction);
				return Quaternion.RotateTowards(this.tr.rotation, to, maxDegrees);
			}
			return this.tr.rotation;
		}

		// Token: 0x040003BB RID: 955
		public float velocitySmoothing = 1f;

		// Token: 0x040003BC RID: 956
		private IAstarAI ai;

		// Token: 0x040003BD RID: 957
		private Animator anim;

		// Token: 0x040003BE RID: 958
		private Transform tr;

		// Token: 0x040003BF RID: 959
		private Vector3 smoothedVelocity;

		// Token: 0x040003C0 RID: 960
		private Vector3[] prevFootPos = new Vector3[2];

		// Token: 0x040003C1 RID: 961
		private Transform[] footTransforms;
	}
}
