using System;
using Pathfinding.RVO;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000004 RID: 4
	[RequireComponent(typeof(Seeker))]
	public abstract class AIBase : VersionedMonoBehaviour
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002258 File Offset: 0x00000658
		protected AIBase()
		{
			this.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002309 File Offset: 0x00000709
		public Vector3 position
		{
			get
			{
				return (!this.updatePosition) ? this.simulatedPosition : this.tr.position;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000232C File Offset: 0x0000072C
		public Quaternion rotation
		{
			get
			{
				return (!this.updateRotation) ? this.simulatedRotation : this.tr.rotation;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000234F File Offset: 0x0000074F
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002357 File Offset: 0x00000757
		private protected bool usingGravity { protected get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002360 File Offset: 0x00000760
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000238C File Offset: 0x0000078C
		[Obsolete("Use the destination property or the AIDestinationSetter component instead")]
		public Transform target
		{
			get
			{
				AIDestinationSetter component = base.GetComponent<AIDestinationSetter>();
				return (!(component != null)) ? null : component.target;
			}
			set
			{
				this.targetCompatibility = null;
				AIDestinationSetter aidestinationSetter = base.GetComponent<AIDestinationSetter>();
				if (aidestinationSetter == null)
				{
					aidestinationSetter = base.gameObject.AddComponent<AIDestinationSetter>();
				}
				aidestinationSetter.target = value;
				this.destination = ((!(value != null)) ? new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity) : value.position);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023F7 File Offset: 0x000007F7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000023FF File Offset: 0x000007FF
		public Vector3 destination { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002408 File Offset: 0x00000808
		public Vector3 velocity
		{
			get
			{
				return (this.lastDeltaTime <= 1E-06f) ? Vector3.zero : ((this.prevPosition1 - this.prevPosition2) / this.lastDeltaTime);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002440 File Offset: 0x00000840
		public Vector3 desiredVelocity
		{
			get
			{
				return (this.lastDeltaTime <= 1E-05f) ? Vector3.zero : this.movementPlane.ToWorld(this.lastDeltaPosition / this.lastDeltaTime, this.verticalVelocity);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000247E File Offset: 0x0000087E
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002486 File Offset: 0x00000886
		public bool isStopped { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000248F File Offset: 0x0000088F
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002497 File Offset: 0x00000897
		public Action onSearchPath { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024A0 File Offset: 0x000008A0
		protected virtual bool shouldRecalculatePath
		{
			get
			{
				return Time.time - this.lastRepath >= this.repathRate && !this.waitingForPathCalculation && this.canSearch && !float.IsPositiveInfinity(this.destination.x);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F4 File Offset: 0x000008F4
		public virtual void FindComponents()
		{
			this.tr = base.transform;
			this.seeker = base.GetComponent<Seeker>();
			this.rvoController = base.GetComponent<RVOController>();
			this.controller = base.GetComponent<CharacterController>();
			this.rigid = base.GetComponent<Rigidbody>();
			this.rigid2D = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002549 File Offset: 0x00000949
		protected virtual void OnEnable()
		{
			this.FindComponents();
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.Init();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000257F File Offset: 0x0000097F
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.Init();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000258E File Offset: 0x0000098E
		private void Init()
		{
			if (this.startHasRun)
			{
				this.Teleport(this.position, false);
				this.lastRepath = float.NegativeInfinity;
				if (this.shouldRecalculatePath)
				{
					this.SearchPath();
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025C4 File Offset: 0x000009C4
		public virtual void Teleport(Vector3 newPosition, bool clearPath = true)
		{
			if (clearPath)
			{
				this.CancelCurrentPathRequest();
			}
			this.simulatedPosition = newPosition;
			this.prevPosition2 = newPosition;
			this.prevPosition1 = newPosition;
			if (this.updatePosition)
			{
				this.tr.position = newPosition;
			}
			if (this.rvoController != null)
			{
				this.rvoController.Move(Vector3.zero);
			}
			if (clearPath)
			{
				this.SearchPath();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000263A File Offset: 0x00000A3A
		protected void CancelCurrentPathRequest()
		{
			this.waitingForPathCalculation = false;
			if (this.seeker != null)
			{
				this.seeker.CancelCurrentPathRequest(true);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002660 File Offset: 0x00000A60
		protected virtual void OnDisable()
		{
			this.CancelCurrentPathRequest();
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.velocity2D = Vector3.zero;
			this.accumulatedMovementDelta = Vector3.zero;
			this.verticalVelocity = 0f;
			this.lastDeltaTime = 0f;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026CC File Offset: 0x00000ACC
		protected virtual void Update()
		{
			if (this.shouldRecalculatePath)
			{
				this.SearchPath();
			}
			this.usingGravity = (!(this.gravity == Vector3.zero) && (!this.updatePosition || ((this.rigid == null || this.rigid.isKinematic) && (this.rigid2D == null || this.rigid2D.isKinematic))));
			if (this.rigid == null && this.rigid2D == null && this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027A0 File Offset: 0x00000BA0
		protected virtual void FixedUpdate()
		{
			if ((!(this.rigid == null) || !(this.rigid2D == null)) && this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.fixedDeltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027F1 File Offset: 0x00000BF1
		public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			this.lastDeltaTime = deltaTime;
			this.MovementUpdateInternal(deltaTime, out nextPosition, out nextRotation);
		}

		// Token: 0x06000022 RID: 34
		protected abstract void MovementUpdateInternal(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation);

		// Token: 0x06000023 RID: 35 RVA: 0x00002803 File Offset: 0x00000C03
		protected virtual void CalculatePathRequestEndpoints(out Vector3 start, out Vector3 end)
		{
			start = this.GetFeetPosition();
			end = this.destination;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002820 File Offset: 0x00000C20
		public virtual void SearchPath()
		{
			if (float.IsPositiveInfinity(this.destination.x))
			{
				return;
			}
			if (this.onSearchPath != null)
			{
				this.onSearchPath();
			}
			this.lastRepath = Time.time;
			this.waitingForPathCalculation = true;
			this.seeker.CancelCurrentPathRequest(true);
			Vector3 start;
			Vector3 end;
			this.CalculatePathRequestEndpoints(out start, out end);
			this.seeker.StartPath(start, end);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002894 File Offset: 0x00000C94
		public virtual Vector3 GetFeetPosition()
		{
			if (this.rvoController != null && this.rvoController.enabled && this.rvoController.movementPlane == MovementPlane.XZ)
			{
				return this.position + this.rotation * Vector3.up * (this.rvoController.center - this.rvoController.height * 0.5f);
			}
			if (this.controller != null && this.controller.enabled && this.updatePosition)
			{
				return this.tr.TransformPoint(this.controller.center) - Vector3.up * this.controller.height * 0.5f;
			}
			return this.position;
		}

		// Token: 0x06000026 RID: 38
		protected abstract void OnPathComplete(Path newPath);

		// Token: 0x06000027 RID: 39 RVA: 0x00002980 File Offset: 0x00000D80
		public void SetPath(Path path)
		{
			if (path.PipelineState == PathState.Created)
			{
				this.lastRepath = Time.time;
				this.waitingForPathCalculation = true;
				this.seeker.CancelCurrentPathRequest(true);
				this.seeker.StartPath(path, null);
			}
			else
			{
				if (path.PipelineState != PathState.Returned)
				{
					throw new ArgumentException("You must call the SetPath method with a path that either has been completely calculated or one whose path calculation has not been started at all. It looks like the path calculation for the path you tried to use has been started, but is not yet finished.");
				}
				if (this.seeker.GetCurrentPath() == path)
				{
					throw new ArgumentException("If you calculate the path using seeker.StartPath then this script will pick up the calculated path anyway as it listens for all paths the Seeker finishes calculating. You should not call SetPath in that case.");
				}
				this.seeker.CancelCurrentPathRequest(true);
				this.OnPathComplete(path);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A1C File Offset: 0x00000E1C
		protected void ApplyGravity(float deltaTime)
		{
			if (this.usingGravity)
			{
				float num;
				this.velocity2D += this.movementPlane.ToPlane(deltaTime * ((!float.IsNaN(this.gravity.x)) ? this.gravity : Physics.gravity), out num);
				this.verticalVelocity += num;
			}
			else
			{
				this.verticalVelocity = 0f;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A9C File Offset: 0x00000E9C
		protected Vector2 CalculateDeltaToMoveThisFrame(Vector2 position, float distanceToEndOfPath, float deltaTime)
		{
			if (this.rvoController != null && this.rvoController.enabled)
			{
				return this.movementPlane.ToPlane(this.rvoController.CalculateMovementDelta(this.movementPlane.ToWorld(position, 0f), deltaTime));
			}
			return Vector2.ClampMagnitude(this.velocity2D * deltaTime, distanceToEndOfPath);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B05 File Offset: 0x00000F05
		public Quaternion SimulateRotationTowards(Vector3 direction, float maxDegrees)
		{
			return this.SimulateRotationTowards(this.movementPlane.ToPlane(direction), maxDegrees);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B1C File Offset: 0x00000F1C
		protected Quaternion SimulateRotationTowards(Vector2 direction, float maxDegrees)
		{
			if (direction != Vector2.zero)
			{
				Quaternion quaternion = Quaternion.LookRotation(this.movementPlane.ToWorld(direction, 0f), this.movementPlane.ToWorld(Vector2.zero, 1f));
				if (this.rotationIn2D)
				{
					quaternion *= Quaternion.Euler(90f, 0f, 0f);
				}
				return Quaternion.RotateTowards(this.simulatedRotation, quaternion, maxDegrees);
			}
			return this.simulatedRotation;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B9F File Offset: 0x00000F9F
		public virtual void Move(Vector3 deltaPosition)
		{
			this.accumulatedMovementDelta += deltaPosition;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BB3 File Offset: 0x00000FB3
		public virtual void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
		{
			this.FinalizeRotation(nextRotation);
			this.FinalizePosition(nextPosition);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BC4 File Offset: 0x00000FC4
		private void FinalizeRotation(Quaternion nextRotation)
		{
			this.simulatedRotation = nextRotation;
			if (this.updateRotation)
			{
				if (this.rigid != null)
				{
					this.rigid.MoveRotation(nextRotation);
				}
				else if (this.rigid2D != null)
				{
					this.rigid2D.MoveRotation(nextRotation.eulerAngles.z);
				}
				else
				{
					this.tr.rotation = nextRotation;
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C44 File Offset: 0x00001044
		private void FinalizePosition(Vector3 nextPosition)
		{
			Vector3 vector = this.simulatedPosition;
			bool flag = false;
			if (this.controller != null && this.controller.enabled && this.updatePosition)
			{
				this.tr.position = vector;
				this.controller.Move(nextPosition - vector + this.accumulatedMovementDelta);
				vector = this.tr.position;
				if (this.controller.isGrounded)
				{
					this.verticalVelocity = 0f;
				}
			}
			else
			{
				float lastElevation;
				this.movementPlane.ToPlane(vector, out lastElevation);
				vector = nextPosition + this.accumulatedMovementDelta;
				if (this.usingGravity)
				{
					vector = this.RaycastPosition(vector, lastElevation);
				}
				flag = true;
			}
			bool flag2 = false;
			vector = this.ClampToNavmesh(vector, out flag2);
			if ((flag || flag2) && this.updatePosition)
			{
				if (this.rigid != null)
				{
					this.rigid.MovePosition(vector);
				}
				else if (this.rigid2D != null)
				{
					this.rigid2D.MovePosition(vector);
				}
				else
				{
					this.tr.position = vector;
				}
			}
			this.accumulatedMovementDelta = Vector3.zero;
			this.simulatedPosition = vector;
			this.UpdateVelocity();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DA0 File Offset: 0x000011A0
		protected void UpdateVelocity()
		{
			int frameCount = Time.frameCount;
			if (frameCount != this.prevFrame)
			{
				this.prevPosition2 = this.prevPosition1;
			}
			this.prevPosition1 = this.position;
			this.prevFrame = frameCount;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002DDE File Offset: 0x000011DE
		protected virtual Vector3 ClampToNavmesh(Vector3 position, out bool positionChanged)
		{
			positionChanged = false;
			return position;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002DE4 File Offset: 0x000011E4
		protected Vector3 RaycastPosition(Vector3 position, float lastElevation)
		{
			float num;
			this.movementPlane.ToPlane(position, out num);
			float num2 = this.centerOffset + Mathf.Max(0f, lastElevation - num);
			Vector3 vector = this.movementPlane.ToWorld(Vector2.zero, num2);
			RaycastHit raycastHit;
			if (Physics.Raycast(position + vector, -vector, out raycastHit, num2, this.groundMask, QueryTriggerInteraction.Ignore))
			{
				this.verticalVelocity *= Math.Max(0f, 1f - 5f * this.lastDeltaTime);
				return raycastHit.point;
			}
			return position;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002E7F File Offset: 0x0000127F
		protected virtual void OnDrawGizmosSelected()
		{
			if (Application.isPlaying)
			{
				this.FindComponents();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E94 File Offset: 0x00001294
		protected virtual void OnDrawGizmos()
		{
			if (!Application.isPlaying || !base.enabled)
			{
				this.FindComponents();
			}
			bool flag = !(this.gravity == Vector3.zero) && (!this.updatePosition || ((this.rigid == null || this.rigid.isKinematic) && (this.rigid2D == null || this.rigid2D.isKinematic)));
			if (flag && (this.controller == null || !this.controller.enabled))
			{
				Gizmos.color = AIBase.GizmoColorRaycast;
				Gizmos.DrawLine(this.position, this.position + base.transform.up * this.centerOffset);
				Gizmos.DrawLine(this.position - base.transform.right * 0.1f, this.position + base.transform.right * 0.1f);
				Gizmos.DrawLine(this.position - base.transform.forward * 0.1f, this.position + base.transform.forward * 0.1f);
			}
			if (!float.IsPositiveInfinity(this.destination.x) && Application.isPlaying)
			{
				Draw.Gizmos.CircleXZ(this.destination, 0.2f, Color.blue, 0f, 6.28318548f);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003053 File Offset: 0x00001453
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.targetCompatibility != null)
			{
				this.target = this.targetCompatibility;
			}
			return 1;
		}

		// Token: 0x04000008 RID: 8
		public float repathRate = 0.5f;

		// Token: 0x04000009 RID: 9
		[FormerlySerializedAs("repeatedlySearchPaths")]
		public bool canSearch = true;

		// Token: 0x0400000A RID: 10
		public bool canMove = true;

		// Token: 0x0400000B RID: 11
		[FormerlySerializedAs("speed")]
		public float maxSpeed = 1f;

		// Token: 0x0400000C RID: 12
		public Vector3 gravity = new Vector3(float.NaN, float.NaN, float.NaN);

		// Token: 0x0400000D RID: 13
		public LayerMask groundMask = -1;

		// Token: 0x0400000E RID: 14
		public float centerOffset = 1f;

		// Token: 0x0400000F RID: 15
		public bool rotationIn2D;

		// Token: 0x04000010 RID: 16
		protected Vector3 simulatedPosition;

		// Token: 0x04000011 RID: 17
		protected Quaternion simulatedRotation;

		// Token: 0x04000012 RID: 18
		private Vector3 accumulatedMovementDelta = Vector3.zero;

		// Token: 0x04000013 RID: 19
		protected Vector2 velocity2D;

		// Token: 0x04000014 RID: 20
		protected float verticalVelocity;

		// Token: 0x04000015 RID: 21
		public Seeker seeker;

		// Token: 0x04000016 RID: 22
		public Transform tr;

		// Token: 0x04000017 RID: 23
		protected Rigidbody rigid;

		// Token: 0x04000018 RID: 24
		protected Rigidbody2D rigid2D;

		// Token: 0x04000019 RID: 25
		protected CharacterController controller;

		// Token: 0x0400001A RID: 26
		protected RVOController rvoController;

		// Token: 0x0400001B RID: 27
		public IMovementPlane movementPlane = GraphTransform.identityTransform;

		// Token: 0x0400001C RID: 28
		[NonSerialized]
		public bool updatePosition = true;

		// Token: 0x0400001D RID: 29
		[NonSerialized]
		public bool updateRotation = true;

		// Token: 0x0400001F RID: 31
		protected float lastDeltaTime;

		// Token: 0x04000020 RID: 32
		protected int prevFrame;

		// Token: 0x04000021 RID: 33
		protected Vector3 prevPosition1;

		// Token: 0x04000022 RID: 34
		protected Vector3 prevPosition2;

		// Token: 0x04000023 RID: 35
		protected Vector2 lastDeltaPosition;

		// Token: 0x04000024 RID: 36
		protected bool waitingForPathCalculation;

		// Token: 0x04000025 RID: 37
		protected float lastRepath = float.NegativeInfinity;

		// Token: 0x04000026 RID: 38
		[FormerlySerializedAs("target")]
		[SerializeField]
		[HideInInspector]
		private Transform targetCompatibility;

		// Token: 0x04000027 RID: 39
		private bool startHasRun;

		// Token: 0x0400002B RID: 43
		protected static readonly Color GizmoColorRaycast = new Color(0.4627451f, 0.807843149f, 0.4392157f);
	}
}
