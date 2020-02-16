using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Examples;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000008 RID: 8
	[AddComponentMenu("Pathfinding/AI/RichAI (3D, for navmesh)")]
	public class RichAI : AIBase, IAstarAI
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000044B8 File Offset: 0x000028B8
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000044C0 File Offset: 0x000028C0
		public bool traversingOffMeshLink { get; protected set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000044C9 File Offset: 0x000028C9
		public float remainingDistance
		{
			get
			{
				return this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, this.richPath.Endpoint);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000044E8 File Offset: 0x000028E8
		public bool reachedEndOfPath
		{
			get
			{
				return this.approachingPathEndpoint && this.distanceToSteeringTarget < this.endReachedDistance;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004506 File Offset: 0x00002906
		public bool hasPath
		{
			get
			{
				return this.richPath.GetCurrentPart() != null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004519 File Offset: 0x00002919
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation || this.delayUpdatePath;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000452F File Offset: 0x0000292F
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004537 File Offset: 0x00002937
		public Vector3 steeringTarget { get; protected set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004540 File Offset: 0x00002940
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004548 File Offset: 0x00002948
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.maxSpeed;
			}
			set
			{
				this.maxSpeed = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004551 File Offset: 0x00002951
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004559 File Offset: 0x00002959
		bool IAstarAI.canSearch
		{
			get
			{
				return this.canSearch;
			}
			set
			{
				this.canSearch = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004562 File Offset: 0x00002962
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000456A File Offset: 0x0000296A
		bool IAstarAI.canMove
		{
			get
			{
				return this.canMove;
			}
			set
			{
				this.canMove = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004573 File Offset: 0x00002973
		Vector3 IAstarAI.position
		{
			get
			{
				return this.tr.position;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004580 File Offset: 0x00002980
		public bool approachingPartEndpoint
		{
			get
			{
				return this.lastCorner && this.nextCorners.Count == 1;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000459E File Offset: 0x0000299E
		public bool approachingPathEndpoint
		{
			get
			{
				return this.approachingPartEndpoint && this.richPath.IsLastPart;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000045BC File Offset: 0x000029BC
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			NNInfo nninfo = (!(AstarPath.active != null)) ? default(NNInfo) : AstarPath.active.GetNearest(newPosition);
			float elevation;
			this.movementPlane.ToPlane(newPosition, out elevation);
			newPosition = this.movementPlane.ToWorld(this.movementPlane.ToPlane((nninfo.node == null) ? newPosition : nninfo.position), elevation);
			if (clearPath)
			{
				this.richPath.Clear();
			}
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000464D File Offset: 0x00002A4D
		protected override void OnDisable()
		{
			base.OnDisable();
			this.lastCorner = false;
			this.distanceToSteeringTarget = float.PositiveInfinity;
			this.traversingOffMeshLink = false;
			this.delayUpdatePath = false;
			base.StopAllCoroutines();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000467B File Offset: 0x00002A7B
		protected override bool shouldRecalculatePath
		{
			get
			{
				return base.shouldRecalculatePath && !this.traversingOffMeshLink;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004694 File Offset: 0x00002A94
		public override void SearchPath()
		{
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
			}
			else
			{
				base.SearchPath();
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000046B4 File Offset: 0x00002AB4
		protected override void OnPathComplete(Path p)
		{
			this.waitingForPathCalculation = false;
			p.Claim(this);
			if (p.error)
			{
				p.Release(this, false);
				return;
			}
			if (this.traversingOffMeshLink)
			{
				this.delayUpdatePath = true;
			}
			else
			{
				this.richPath.Initialize(this.seeker, p, true, this.funnelSimplification);
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					if (this.updatePosition)
					{
						this.simulatedPosition = this.tr.position;
					}
					Vector2 b = this.movementPlane.ToPlane(this.UpdateTarget(richFunnel));
					if (this.lastCorner && this.nextCorners.Count == 1)
					{
						this.steeringTarget = this.nextCorners[0];
						Vector2 a = this.movementPlane.ToPlane(this.steeringTarget);
						this.distanceToSteeringTarget = (a - b).magnitude;
						if (this.distanceToSteeringTarget <= this.endReachedDistance)
						{
							this.NextPart();
						}
					}
				}
			}
			p.Release(this, false);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000047D0 File Offset: 0x00002BD0
		protected void NextPart()
		{
			if (!this.richPath.CompletedAllParts)
			{
				if (!this.richPath.IsLastPart)
				{
					this.lastCorner = false;
				}
				this.richPath.NextPart();
				if (this.richPath.CompletedAllParts)
				{
					this.OnTargetReached();
				}
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004825 File Offset: 0x00002C25
		protected virtual void OnTargetReached()
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004828 File Offset: 0x00002C28
		protected virtual Vector3 UpdateTarget(RichFunnel fn)
		{
			this.nextCorners.Clear();
			bool flag;
			Vector3 result = fn.Update(this.simulatedPosition, this.nextCorners, 2, out this.lastCorner, out flag);
			if (flag && !this.waitingForPathCalculation && this.canSearch)
			{
				this.SearchPath();
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004880 File Offset: 0x00002C80
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			RichPathPart currentPart = this.richPath.GetCurrentPart();
			if (currentPart is RichSpecial)
			{
				if (!this.traversingOffMeshLink)
				{
					base.StartCoroutine(this.TraverseSpecial(currentPart as RichSpecial));
				}
				Vector3 simulatedPosition = this.simulatedPosition;
				this.steeringTarget = simulatedPosition;
				nextPosition = simulatedPosition;
				nextRotation = base.rotation;
			}
			else
			{
				RichFunnel richFunnel = currentPart as RichFunnel;
				if (richFunnel != null && !base.isStopped)
				{
					this.TraverseFunnel(richFunnel, deltaTime, out nextPosition, out nextRotation);
				}
				else
				{
					this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, this.acceleration * deltaTime);
					this.FinalMovement(this.simulatedPosition, deltaTime, float.PositiveInfinity, 1f, out nextPosition, out nextRotation);
					this.steeringTarget = this.simulatedPosition;
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004990 File Offset: 0x00002D90
		private void TraverseFunnel(RichFunnel fn, float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector3 vector = this.UpdateTarget(fn);
			float elevation;
			Vector2 vector2 = this.movementPlane.ToPlane(vector, out elevation);
			if (Time.frameCount % 5 == 0 && this.wallForce > 0f && this.wallDist > 0f)
			{
				this.wallBuffer.Clear();
				fn.FindWalls(this.wallBuffer, this.wallDist);
			}
			this.steeringTarget = this.nextCorners[0];
			Vector2 vector3 = this.movementPlane.ToPlane(this.steeringTarget);
			Vector2 vector4 = vector3 - vector2;
			Vector2 vector5 = VectorMath.Normalize(vector4, out this.distanceToSteeringTarget);
			Vector2 a = this.CalculateWallForce(vector2, elevation, vector5);
			Vector2 targetVelocity;
			if (this.approachingPartEndpoint)
			{
				targetVelocity = ((this.slowdownTime <= 0f) ? (vector5 * this.maxSpeed) : Vector2.zero);
				a *= Math.Min(this.distanceToSteeringTarget / 0.5f, 1f);
				if (this.distanceToSteeringTarget <= this.endReachedDistance)
				{
					this.NextPart();
				}
			}
			else
			{
				Vector2 a2 = (this.nextCorners.Count <= 1) ? (vector2 + 2f * vector4) : this.movementPlane.ToPlane(this.nextCorners[1]);
				targetVelocity = (a2 - vector3).normalized * this.maxSpeed;
			}
			Vector2 forwardsVector = this.movementPlane.ToPlane(this.simulatedRotation * ((!this.rotationIn2D) ? Vector3.forward : Vector3.up));
			Vector2 a3 = MovementUtilities.CalculateAccelerationToReachPoint(vector3 - vector2, targetVelocity, this.velocity2D, this.acceleration, this.rotationSpeed, this.maxSpeed, forwardsVector);
			this.velocity2D += (a3 + a * this.wallForce) * deltaTime;
			float num = this.distanceToSteeringTarget + Vector3.Distance(this.steeringTarget, fn.exactEnd);
			float slowdownFactor = (num >= this.maxSpeed * this.slowdownTime) ? 1f : Mathf.Sqrt(num / (this.maxSpeed * this.slowdownTime));
			this.FinalMovement(vector, deltaTime, num, slowdownFactor, out nextPosition, out nextRotation);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004BF8 File Offset: 0x00002FF8
		private void FinalMovement(Vector3 position3D, float deltaTime, float distanceToEndOfPath, float slowdownFactor, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			Vector2 forward = this.movementPlane.ToPlane(this.simulatedRotation * ((!this.rotationIn2D) ? Vector3.forward : Vector3.up));
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, slowdownFactor, this.slowWhenNotFacingTarget, forward);
			base.ApplyGravity(deltaTime);
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 pos = position3D + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, distanceToEndOfPath), 0f);
				this.rvoController.SetTarget(pos, this.velocity2D.magnitude, this.maxSpeed);
			}
			Vector2 vector = this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(this.movementPlane.ToPlane(position3D), distanceToEndOfPath, deltaTime);
			float num = (!this.approachingPartEndpoint) ? 1f : Mathf.Clamp01(1.1f * slowdownFactor - 0.1f);
			nextRotation = base.SimulateRotationTowards(vector, this.rotationSpeed * num * deltaTime);
			nextPosition = position3D + this.movementPlane.ToWorld(vector, this.verticalVelocity * deltaTime);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004D40 File Offset: 0x00003140
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.richPath != null)
			{
				RichFunnel richFunnel = this.richPath.GetCurrentPart() as RichFunnel;
				if (richFunnel != null)
				{
					Vector3 a = richFunnel.ClampToNavmesh(position);
					Vector2 vector = this.movementPlane.ToPlane(a - position);
					float sqrMagnitude = vector.sqrMagnitude;
					if (sqrMagnitude > 1.00000011E-06f)
					{
						this.velocity2D -= vector * Vector2.Dot(vector, this.velocity2D) / sqrMagnitude;
						if (this.rvoController != null && this.rvoController.enabled)
						{
							this.rvoController.SetCollisionNormal(vector);
						}
						positionChanged = true;
						return position + this.movementPlane.ToWorld(vector, 0f);
					}
				}
			}
			positionChanged = false;
			return position;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004E18 File Offset: 0x00003218
		private Vector2 CalculateWallForce(Vector2 position, float elevation, Vector2 directionToTarget)
		{
			if (this.wallForce <= 0f || this.wallDist <= 0f)
			{
				return Vector2.zero;
			}
			float num = 0f;
			float num2 = 0f;
			Vector3 vector = this.movementPlane.ToWorld(position, elevation);
			for (int i = 0; i < this.wallBuffer.Count; i += 2)
			{
				Vector3 a = VectorMath.ClosestPointOnSegment(this.wallBuffer[i], this.wallBuffer[i + 1], vector);
				float sqrMagnitude = (a - vector).sqrMagnitude;
				if (sqrMagnitude <= this.wallDist * this.wallDist)
				{
					Vector2 normalized = this.movementPlane.ToPlane(this.wallBuffer[i + 1] - this.wallBuffer[i]).normalized;
					float num3 = Vector2.Dot(directionToTarget, normalized);
					float num4 = 1f - Math.Max(0f, 2f * (sqrMagnitude / (this.wallDist * this.wallDist)) - 1f);
					if (num3 > 0f)
					{
						num2 = Math.Max(num2, num3 * num4);
					}
					else
					{
						num = Math.Max(num, -num3 * num4);
					}
				}
			}
			Vector2 a2 = new Vector2(directionToTarget.y, -directionToTarget.x);
			return a2 * (num2 - num);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004F88 File Offset: 0x00003388
		protected virtual IEnumerator TraverseSpecial(RichSpecial link)
		{
			this.traversingOffMeshLink = true;
			this.velocity2D = Vector3.zero;
			IEnumerator offMeshLinkCoroutine = (this.onTraverseOffMeshLink == null) ? this.TraverseOffMeshLinkFallback(link) : this.onTraverseOffMeshLink(link);
			yield return base.StartCoroutine(offMeshLinkCoroutine);
			this.traversingOffMeshLink = false;
			this.NextPart();
			if (this.delayUpdatePath)
			{
				this.delayUpdatePath = false;
				if (this.canSearch)
				{
					this.SearchPath();
				}
			}
			yield break;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004FAC File Offset: 0x000033AC
		protected IEnumerator TraverseOffMeshLinkFallback(RichSpecial link)
		{
			float duration = (this.maxSpeed <= 0f) ? 1f : (Vector3.Distance(link.second.position, link.first.position) / this.maxSpeed);
			float startTime = Time.time;
			for (;;)
			{
				Vector3 pos = Vector3.Lerp(link.first.position, link.second.position, Mathf.InverseLerp(startTime, startTime + duration, Time.time));
				if (this.updatePosition)
				{
					this.tr.position = pos;
				}
				else
				{
					this.simulatedPosition = pos;
				}
				if (Time.time >= startTime + duration)
				{
					break;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004FD0 File Offset: 0x000033D0
		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();
			if (this.tr != null)
			{
				Gizmos.color = RichAI.GizmoColorPath;
				Vector3 from = base.position;
				for (int i = 0; i < this.nextCorners.Count; i++)
				{
					Gizmos.DrawLine(from, this.nextCorners[i]);
					from = this.nextCorners[i];
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005040 File Offset: 0x00003440
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.animCompatibility != null)
			{
				this.anim = this.animCompatibility;
			}
			return base.OnUpgradeSerializedData(version, unityThread);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000506D File Offset: 0x0000346D
		[Obsolete("Use SearchPath instead. [AstarUpgradable: 'UpdatePath' -> 'SearchPath']")]
		public void UpdatePath()
		{
			this.SearchPath();
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005075 File Offset: 0x00003475
		[Obsolete("Use velocity instead (lowercase 'v'). [AstarUpgradable: 'Velocity' -> 'velocity']")]
		public Vector3 Velocity
		{
			get
			{
				return base.velocity;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000507D File Offset: 0x0000347D
		[Obsolete("Use steeringTarget instead. [AstarUpgradable: 'NextWaypoint' -> 'steeringTarget']")]
		public Vector3 NextWaypoint
		{
			get
			{
				return this.steeringTarget;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005085 File Offset: 0x00003485
		[Obsolete("Use Vector3.Distance(transform.position, ai.steeringTarget) instead.")]
		public float DistanceToNextWaypoint
		{
			get
			{
				return this.distanceToSteeringTarget;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000508D File Offset: 0x0000348D
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005095 File Offset: 0x00003495
		[Obsolete("Use canSearch instead. [AstarUpgradable: 'repeatedlySearchPaths' -> 'canSearch']")]
		public bool repeatedlySearchPaths
		{
			get
			{
				return this.canSearch;
			}
			set
			{
				this.canSearch = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000509E File Offset: 0x0000349E
		[Obsolete("When unifying the interfaces for different movement scripts, this property has been renamed to reachedEndOfPath (lowercase t).  [AstarUpgradable: 'TargetReached' -> 'reachedEndOfPath']")]
		public bool TargetReached
		{
			get
			{
				return this.reachedEndOfPath;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000050A6 File Offset: 0x000034A6
		[Obsolete("Use pathPending instead (lowercase 'p'). [AstarUpgradable: 'PathPending' -> 'pathPending']")]
		public bool PathPending
		{
			get
			{
				return this.pathPending;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000050AE File Offset: 0x000034AE
		[Obsolete("Use approachingPartEndpoint (lowercase 'a') instead")]
		public bool ApproachingPartEndpoint
		{
			get
			{
				return this.approachingPartEndpoint;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000050B6 File Offset: 0x000034B6
		[Obsolete("Use approachingPathEndpoint (lowercase 'a') instead")]
		public bool ApproachingPathEndpoint
		{
			get
			{
				return this.approachingPathEndpoint;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000050BE File Offset: 0x000034BE
		[Obsolete("This property has been renamed to 'traversingOffMeshLink'. [AstarUpgradable: 'TraversingSpecial' -> 'traversingOffMeshLink']")]
		public bool TraversingSpecial
		{
			get
			{
				return this.traversingOffMeshLink;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000050C6 File Offset: 0x000034C6
		[Obsolete("This property has been renamed to steeringTarget")]
		public Vector3 TargetPoint
		{
			get
			{
				return this.steeringTarget;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000050D0 File Offset: 0x000034D0
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000050FC File Offset: 0x000034FC
		[Obsolete("Use the onTraverseOffMeshLink event or the ... component instead. Setting this value will add a ... component")]
		public Animation anim
		{
			get
			{
				AnimationLinkTraverser component = base.GetComponent<AnimationLinkTraverser>();
				return (!(component != null)) ? null : component.anim;
			}
			set
			{
				this.animCompatibility = null;
				AnimationLinkTraverser animationLinkTraverser = base.GetComponent<AnimationLinkTraverser>();
				if (animationLinkTraverser == null)
				{
					animationLinkTraverser = base.gameObject.AddComponent<AnimationLinkTraverser>();
				}
				animationLinkTraverser.anim = value;
			}
		}

		// Token: 0x04000057 RID: 87
		public float acceleration = 5f;

		// Token: 0x04000058 RID: 88
		public float rotationSpeed = 360f;

		// Token: 0x04000059 RID: 89
		public float slowdownTime = 0.5f;

		// Token: 0x0400005A RID: 90
		public float endReachedDistance = 0.01f;

		// Token: 0x0400005B RID: 91
		public float wallForce = 3f;

		// Token: 0x0400005C RID: 92
		public float wallDist = 1f;

		// Token: 0x0400005D RID: 93
		public bool funnelSimplification;

		// Token: 0x0400005E RID: 94
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x0400005F RID: 95
		public Func<RichSpecial, IEnumerator> onTraverseOffMeshLink;

		// Token: 0x04000060 RID: 96
		protected readonly RichPath richPath = new RichPath();

		// Token: 0x04000061 RID: 97
		protected bool delayUpdatePath;

		// Token: 0x04000062 RID: 98
		protected bool lastCorner;

		// Token: 0x04000063 RID: 99
		protected float distanceToSteeringTarget = float.PositiveInfinity;

		// Token: 0x04000064 RID: 100
		protected readonly List<Vector3> nextCorners = new List<Vector3>();

		// Token: 0x04000065 RID: 101
		protected readonly List<Vector3> wallBuffer = new List<Vector3>();

		// Token: 0x04000068 RID: 104
		protected static readonly Color GizmoColorPath = new Color(0.03137255f, 0.305882365f, 0.7607843f);

		// Token: 0x04000069 RID: 105
		[FormerlySerializedAs("anim")]
		[SerializeField]
		[HideInInspector]
		private Animation animCompatibility;
	}
}
