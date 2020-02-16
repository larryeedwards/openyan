using System;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000047 RID: 71
	public static class MovementUtilities
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00013BF8 File Offset: 0x00011FF8
		public static Vector2 ClampVelocity(Vector2 velocity, float maxSpeed, float slowdownFactor, bool slowWhenNotFacingTarget, Vector2 forward)
		{
			float num = maxSpeed * slowdownFactor;
			if (slowWhenNotFacingTarget && (forward.x != 0f || forward.y != 0f))
			{
				float num2;
				Vector2 lhs = VectorMath.Normalize(velocity, out num2);
				float num3 = Vector2.Dot(lhs, forward);
				float num4 = Mathf.Clamp(num3 + 0.707f, 0.2f, 1f);
				num *= num4;
				num2 = Mathf.Min(num2, num);
				float num5 = Mathf.Acos(Mathf.Clamp(num3, -1f, 1f));
				num5 = Mathf.Min(num5, (20f + 180f * (1f - slowdownFactor * slowdownFactor)) * 0.0174532924f);
				float num6 = Mathf.Sin(num5);
				float num7 = Mathf.Cos(num5);
				num6 *= Mathf.Sign(lhs.x * forward.y - lhs.y * forward.x);
				return new Vector2(forward.x * num7 + forward.y * num6, forward.y * num7 - forward.x * num6) * num2;
			}
			return Vector2.ClampMagnitude(velocity, num);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00013D1C File Offset: 0x0001211C
		public static Vector2 CalculateAccelerationToReachPoint(Vector2 deltaPosition, Vector2 targetVelocity, Vector2 currentVelocity, float forwardsAcceleration, float rotationSpeed, float maxSpeed, Vector2 forwardsVector)
		{
			if (forwardsAcceleration <= 0f)
			{
				return Vector2.zero;
			}
			float magnitude = currentVelocity.magnitude;
			float a = magnitude * rotationSpeed * 0.0174532924f;
			a = Mathf.Max(a, forwardsAcceleration);
			deltaPosition = VectorMath.ComplexMultiplyConjugate(deltaPosition, forwardsVector);
			targetVelocity = VectorMath.ComplexMultiplyConjugate(targetVelocity, forwardsVector);
			currentVelocity = VectorMath.ComplexMultiplyConjugate(currentVelocity, forwardsVector);
			float num = 1f / (forwardsAcceleration * forwardsAcceleration);
			float num2 = 1f / (forwardsAcceleration * forwardsAcceleration);
			if (targetVelocity == Vector2.zero)
			{
				float num3 = 0.01f;
				float num4 = 10f;
				while (num4 - num3 > 0.01f)
				{
					float num5 = (num4 + num3) * 0.5f;
					Vector2 a2 = (6f * deltaPosition - 4f * num5 * currentVelocity) / (num5 * num5);
					Vector2 a3 = 6f * (num5 * currentVelocity - 2f * deltaPosition) / (num5 * num5 * num5);
					Vector2 vector = a2 + a3 * num5;
					if (a2.x * a2.x * num + a2.y * a2.y * num2 > 1f || vector.x * vector.x * num + vector.y * vector.y * num2 > 1f)
					{
						num3 = num5;
					}
					else
					{
						num4 = num5;
					}
				}
				Vector2 a4 = (6f * deltaPosition - 4f * num4 * currentVelocity) / (num4 * num4);
				a4.y *= 2f;
				float num6 = a4.x * a4.x * num + a4.y * a4.y * num2;
				if (num6 > 1f)
				{
					a4 /= Mathf.Sqrt(num6);
				}
				return VectorMath.ComplexMultiply(a4, forwardsVector);
			}
			float num7;
			Vector2 a5 = VectorMath.Normalize(targetVelocity, out num7);
			float magnitude2 = deltaPosition.magnitude;
			Vector2 a6 = ((deltaPosition - a5 * Math.Min(0.5f * magnitude2 * num7 / (magnitude + num7), maxSpeed * 1.5f)).normalized * maxSpeed - currentVelocity) * 10f;
			float num8 = a6.x * a6.x * num + a6.y * a6.y * num2;
			if (num8 > 1f)
			{
				a6 /= Mathf.Sqrt(num8);
			}
			return VectorMath.ComplexMultiply(a6, forwardsVector);
		}
	}
}
