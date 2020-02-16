using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000006 RID: 6
	[AddComponentMenu("Pathfinding/AI/AIPath (2D,3D)")]
	public class AIPath : AIBase, IAstarAI
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003BFC File Offset: 0x00001FFC
		public override void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			if (clearPath)
			{
				this.interpolator.SetPath(null);
			}
			this.reachedEndOfPath = false;
			base.Teleport(newPosition, clearPath);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003C20 File Offset: 0x00002020
		public float remainingDistance
		{
			get
			{
				return (!this.interpolator.valid) ? float.PositiveInfinity : (this.interpolator.remainingDistance + this.movementPlane.ToPlane(this.interpolator.position - base.position).magnitude);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003C7C File Offset: 0x0000207C
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003C84 File Offset: 0x00002084
		public bool reachedEndOfPath { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003C8D File Offset: 0x0000208D
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003C9A File Offset: 0x0000209A
		public bool pathPending
		{
			get
			{
				return this.waitingForPathCalculation;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003CA2 File Offset: 0x000020A2
		public Vector3 steeringTarget
		{
			get
			{
				return (!this.interpolator.valid) ? base.position : this.interpolator.position;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003CCA File Offset: 0x000020CA
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003CD2 File Offset: 0x000020D2
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003CDB File Offset: 0x000020DB
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003CE3 File Offset: 0x000020E3
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003CEC File Offset: 0x000020EC
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003CF4 File Offset: 0x000020F4
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

		// Token: 0x06000076 RID: 118 RVA: 0x00003CFD File Offset: 0x000020FD
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolator.SetPath(null);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003D30 File Offset: 0x00002130
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003D34 File Offset: 0x00002134
		protected override void OnPathComplete(Path newPath)
		{
			ABPath abpath = newPath as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.waitingForPathCalculation = false;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = abpath;
			if (this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Add(this.path.vectorPath[0]);
			}
			this.interpolator.SetPath(this.path.vectorPath);
			ITransformedGraph transformedGraph = AstarData.GetGraph(this.path.path[0]) as ITransformedGraph;
			this.movementPlane = ((transformedGraph == null) ? ((!this.rotationIn2D) ? GraphTransform.identityTransform : new GraphTransform(Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(-90f, 270f, 90f), Vector3.one))) : transformedGraph.transform);
			this.reachedEndOfPath = false;
			this.interpolator.MoveToLocallyClosestPoint((this.GetFeetPosition() + abpath.originalStartPoint) * 0.5f, true, true);
			this.interpolator.MoveToLocallyClosestPoint(this.GetFeetPosition(), true, true);
			this.interpolator.MoveToCircleIntersection2D(base.position, this.pickNextWaypointDist, this.movementPlane);
			float remainingDistance = this.remainingDistance;
			if (remainingDistance <= this.endReachedDistance)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003ED8 File Offset: 0x000022D8
		protected override void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			float num = this.maxAcceleration;
			if (num < 0f)
			{
				num *= -this.maxSpeed;
			}
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 simulatedPosition = this.simulatedPosition;
			this.interpolator.MoveToCircleIntersection2D(simulatedPosition, this.pickNextWaypointDist, this.movementPlane);
			Vector2 deltaPosition = this.movementPlane.ToPlane(this.steeringTarget - simulatedPosition);
			float num2 = deltaPosition.magnitude + Mathf.Max(0f, this.interpolator.remainingDistance);
			bool reachedEndOfPath = this.reachedEndOfPath;
			this.reachedEndOfPath = (num2 <= this.endReachedDistance && this.interpolator.valid);
			if (!reachedEndOfPath && this.reachedEndOfPath)
			{
				this.OnTargetReached();
			}
			Vector2 vector = this.movementPlane.ToPlane(this.simulatedRotation * ((!this.rotationIn2D) ? Vector3.forward : Vector3.up));
			float num3;
			if (this.interpolator.valid && !base.isStopped)
			{
				num3 = ((num2 >= this.slowdownDistance) ? 1f : Mathf.Sqrt(num2 / this.slowdownDistance));
				if (this.reachedEndOfPath && this.whenCloseToDestination == CloseToDestinationMode.Stop)
				{
					this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
				}
				else
				{
					this.velocity2D += MovementUtilities.CalculateAccelerationToReachPoint(deltaPosition, deltaPosition.normalized * this.maxSpeed, this.velocity2D, num, this.rotationSpeed, this.maxSpeed, vector) * deltaTime;
				}
			}
			else
			{
				num3 = 1f;
				this.velocity2D -= Vector2.ClampMagnitude(this.velocity2D, num * deltaTime);
			}
			this.velocity2D = MovementUtilities.ClampVelocity(this.velocity2D, this.maxSpeed, num3, this.slowWhenNotFacingTarget, vector);
			base.ApplyGravity(deltaTime);
			if (this.rvoController != null && this.rvoController.enabled)
			{
				Vector3 pos = simulatedPosition + this.movementPlane.ToWorld(Vector2.ClampMagnitude(this.velocity2D, num2), 0f);
				this.rvoController.SetTarget(pos, this.velocity2D.magnitude, this.maxSpeed);
			}
			Vector2 p = this.lastDeltaPosition = base.CalculateDeltaToMoveThisFrame(this.movementPlane.ToPlane(simulatedPosition), num2, deltaTime);
			nextPosition = simulatedPosition + this.movementPlane.ToWorld(p, this.verticalVelocity * this.lastDeltaTime);
			this.CalculateNextRotation(num3, out nextRotation);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000041C4 File Offset: 0x000025C4
		protected virtual void CalculateNextRotation(float slowdown, out Quaternion nextRotation)
		{
			if (this.lastDeltaTime > 1E-05f)
			{
				Vector2 direction;
				if (this.rvoController != null && this.rvoController.enabled)
				{
					Vector2 b = this.lastDeltaPosition / this.lastDeltaTime;
					direction = Vector2.Lerp(this.velocity2D, b, 4f * b.magnitude / (this.maxSpeed + 0.0001f));
				}
				else
				{
					direction = this.velocity2D;
				}
				float num = this.rotationSpeed * Mathf.Max(0f, (slowdown - 0.3f) / 0.7f);
				nextRotation = base.SimulateRotationTowards(direction, num * this.lastDeltaTime);
			}
			else
			{
				nextRotation = base.rotation;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000428C File Offset: 0x0000268C
		protected override Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			if (this.constrainInsideGraph)
			{
				AIPath.cachedNNConstraint.tags = this.seeker.traversableTags;
				AIPath.cachedNNConstraint.graphMask = this.seeker.graphMask;
				AIPath.cachedNNConstraint.distanceXZ = true;
				Vector3 position2 = AstarPath.active.GetNearest(position, AIPath.cachedNNConstraint).position;
				Vector2 vector = this.movementPlane.ToPlane(position2 - position);
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
			positionChanged = false;
			return position;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004392 File Offset: 0x00002792
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			base.OnUpgradeSerializedData(version, unityThread);
			if (version < 1)
			{
				this.rotationSpeed *= 90f;
			}
			return 2;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000043B7 File Offset: 0x000027B7
		[Obsolete("When unifying the interfaces for different movement scripts, this property has been renamed to reachedEndOfPath.  [AstarUpgradable: 'TargetReached' -> 'reachedEndOfPath']")]
		public bool TargetReached
		{
			get
			{
				return this.reachedEndOfPath;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000043BF File Offset: 0x000027BF
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000043CD File Offset: 0x000027CD
		[Obsolete("This field has been renamed to #rotationSpeed and is now in degrees per second instead of a damping factor")]
		public float turningSpeed
		{
			get
			{
				return this.rotationSpeed / 90f;
			}
			set
			{
				this.rotationSpeed = value * 90f;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000043DC File Offset: 0x000027DC
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000043E4 File Offset: 0x000027E4
		[Obsolete("This member has been deprecated. Use 'maxSpeed' instead. [AstarUpgradable: 'speed' -> 'maxSpeed']")]
		public float speed
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000043F0 File Offset: 0x000027F0
		[Obsolete("Only exists for compatibility reasons. Use desiredVelocity or steeringTarget instead.")]
		public Vector3 targetDirection
		{
			get
			{
				return (this.steeringTarget - this.tr.position).normalized;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000441B File Offset: 0x0000281B
		[Obsolete("This method no longer calculates the velocity. Use the desiredVelocity property instead")]
		public Vector3 CalculateVelocity(Vector3 position)
		{
			return base.desiredVelocity;
		}

		// Token: 0x0400004A RID: 74
		public float maxAcceleration = -2.5f;

		// Token: 0x0400004B RID: 75
		[FormerlySerializedAs("turningSpeed")]
		public float rotationSpeed = 360f;

		// Token: 0x0400004C RID: 76
		public float slowdownDistance = 0.6f;

		// Token: 0x0400004D RID: 77
		public float pickNextWaypointDist = 2f;

		// Token: 0x0400004E RID: 78
		public float endReachedDistance = 0.2f;

		// Token: 0x0400004F RID: 79
		public bool alwaysDrawGizmos;

		// Token: 0x04000050 RID: 80
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x04000051 RID: 81
		public CloseToDestinationMode whenCloseToDestination;

		// Token: 0x04000052 RID: 82
		public bool constrainInsideGraph;

		// Token: 0x04000053 RID: 83
		protected Path path;

		// Token: 0x04000054 RID: 84
		public PathInterpolator interpolator = new PathInterpolator();

		// Token: 0x04000056 RID: 86
		private static NNConstraint cachedNNConstraint = NNConstraint.Default;
	}
}
