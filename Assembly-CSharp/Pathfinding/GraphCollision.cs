using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public class GraphCollision
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00025CC8 File Offset: 0x000240C8
		public void Initialize(GraphTransform transform, float scale)
		{
			this.up = (transform.Transform(Vector3.up) - transform.Transform(Vector3.zero)).normalized;
			this.upheight = this.up * this.height;
			this.finalRadius = this.diameter * scale * 0.5f;
			this.finalRaycastRadius = this.thickRaycastDiameter * scale * 0.5f;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00025D40 File Offset: 0x00024140
		public bool Check(Vector3 position)
		{
			if (!this.collisionCheck)
			{
				return true;
			}
			if (this.use2D)
			{
				ColliderType colliderType = this.type;
				if (colliderType != ColliderType.Capsule && colliderType != ColliderType.Sphere)
				{
					return Physics2D.OverlapPoint(position, this.mask) == null;
				}
				return Physics2D.OverlapCircle(position, this.finalRadius, this.mask) == null;
			}
			else
			{
				position += this.up * this.collisionOffset;
				ColliderType colliderType2 = this.type;
				if (colliderType2 == ColliderType.Capsule)
				{
					return !Physics.CheckCapsule(position, position + this.upheight, this.finalRadius, this.mask, QueryTriggerInteraction.Collide);
				}
				if (colliderType2 == ColliderType.Sphere)
				{
					return !Physics.CheckSphere(position, this.finalRadius, this.mask, QueryTriggerInteraction.Collide);
				}
				RayDirection rayDirection = this.rayDirection;
				if (rayDirection == RayDirection.Both)
				{
					return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Collide) && !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Collide);
				}
				if (rayDirection != RayDirection.Up)
				{
					return !Physics.Raycast(position + this.upheight, -this.up, this.height, this.mask, QueryTriggerInteraction.Collide);
				}
				return !Physics.Raycast(position, this.up, this.height, this.mask, QueryTriggerInteraction.Collide);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00025EF8 File Offset: 0x000242F8
		public Vector3 CheckHeight(Vector3 position)
		{
			RaycastHit raycastHit;
			bool flag;
			return this.CheckHeight(position, out raycastHit, out flag);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00025F10 File Offset: 0x00024310
		public Vector3 CheckHeight(Vector3 position, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return position;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(position + this.up * this.fromHeight, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return VectorMath.ClosestPointOnLine(ray.origin, ray.origin + ray.direction, hit.point);
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			else
			{
				if (Physics.Raycast(position + this.up * this.fromHeight, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return hit.point;
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			return position;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00026034 File Offset: 0x00024434
		public Vector3 Raycast(Vector3 origin, out RaycastHit hit, out bool walkable)
		{
			walkable = true;
			if (!this.heightCheck || this.use2D)
			{
				hit = default(RaycastHit);
				return origin - this.up * this.fromHeight;
			}
			if (this.thickRaycast)
			{
				Ray ray = new Ray(origin, -this.up);
				if (Physics.SphereCast(ray, this.finalRaycastRadius, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return VectorMath.ClosestPointOnLine(ray.origin, ray.origin + ray.direction, hit.point);
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			else
			{
				if (Physics.Raycast(origin, -this.up, out hit, this.fromHeight + 0.005f, this.heightMask, QueryTriggerInteraction.Collide))
				{
					return hit.point;
				}
				walkable &= !this.unwalkableWhenNoGround;
			}
			return origin - this.up * this.fromHeight;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00026158 File Offset: 0x00024558
		public RaycastHit[] CheckHeightAll(Vector3 position)
		{
			if (!this.heightCheck || this.use2D)
			{
				return new RaycastHit[]
				{
					new RaycastHit
					{
						point = position,
						distance = 0f
					}
				};
			}
			if (this.thickRaycast)
			{
				return new RaycastHit[0];
			}
			List<RaycastHit> list = new List<RaycastHit>();
			Vector3 vector = position + this.up * this.fromHeight;
			Vector3 vector2 = Vector3.zero;
			int num = 0;
			for (;;)
			{
				RaycastHit item;
				bool flag;
				this.Raycast(vector, out item, out flag);
				if (item.transform == null)
				{
					break;
				}
				if (item.point != vector2 || list.Count == 0)
				{
					vector = item.point - this.up * 0.005f;
					vector2 = item.point;
					num = 0;
					list.Add(item);
				}
				else
				{
					vector -= this.up * 0.001f;
					num++;
					if (num > 10)
					{
						goto Block_5;
					}
				}
			}
			goto IL_15A;
			Block_5:
			Debug.LogError(string.Concat(new object[]
			{
				"Infinite Loop when raycasting. Please report this error (arongranberg.com)\n",
				vector,
				" : ",
				vector2
			}));
			IL_15A:
			return list.ToArray();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000262C8 File Offset: 0x000246C8
		public void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.type = (ColliderType)ctx.reader.ReadInt32();
			this.diameter = ctx.reader.ReadSingle();
			this.height = ctx.reader.ReadSingle();
			this.collisionOffset = ctx.reader.ReadSingle();
			this.rayDirection = (RayDirection)ctx.reader.ReadInt32();
			this.mask = ctx.reader.ReadInt32();
			this.heightMask = ctx.reader.ReadInt32();
			this.fromHeight = ctx.reader.ReadSingle();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastDiameter = ctx.reader.ReadSingle();
			this.unwalkableWhenNoGround = ctx.reader.ReadBoolean();
			this.use2D = ctx.reader.ReadBoolean();
			this.collisionCheck = ctx.reader.ReadBoolean();
			this.heightCheck = ctx.reader.ReadBoolean();
		}

		// Token: 0x0400041A RID: 1050
		public ColliderType type = ColliderType.Capsule;

		// Token: 0x0400041B RID: 1051
		public float diameter = 1f;

		// Token: 0x0400041C RID: 1052
		public float height = 2f;

		// Token: 0x0400041D RID: 1053
		public float collisionOffset;

		// Token: 0x0400041E RID: 1054
		public RayDirection rayDirection = RayDirection.Both;

		// Token: 0x0400041F RID: 1055
		public LayerMask mask;

		// Token: 0x04000420 RID: 1056
		public LayerMask heightMask = -1;

		// Token: 0x04000421 RID: 1057
		public float fromHeight = 100f;

		// Token: 0x04000422 RID: 1058
		public bool thickRaycast;

		// Token: 0x04000423 RID: 1059
		public float thickRaycastDiameter = 1f;

		// Token: 0x04000424 RID: 1060
		public bool unwalkableWhenNoGround = true;

		// Token: 0x04000425 RID: 1061
		public bool use2D;

		// Token: 0x04000426 RID: 1062
		public bool collisionCheck = true;

		// Token: 0x04000427 RID: 1063
		public bool heightCheck = true;

		// Token: 0x04000428 RID: 1064
		public Vector3 up;

		// Token: 0x04000429 RID: 1065
		private Vector3 upheight;

		// Token: 0x0400042A RID: 1066
		private float finalRadius;

		// Token: 0x0400042B RID: 1067
		private float finalRaycastRadius;

		// Token: 0x0400042C RID: 1068
		public const float RaycastErrorMargin = 0.005f;
	}
}
