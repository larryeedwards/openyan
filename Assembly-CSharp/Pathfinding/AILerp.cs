using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000005 RID: 5
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/AI/AILerp (2D,3D)")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_lerp.php")]
	public class AILerp : VersionedMonoBehaviour, IAstarAI
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00003094 File Offset: 0x00001494
		protected AILerp()
		{
			this.destination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003134 File Offset: 0x00001534
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000313C File Offset: 0x0000153C
		public bool reachedEndOfPath { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003145 File Offset: 0x00001545
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000314D File Offset: 0x0000154D
		public Vector3 destination { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003158 File Offset: 0x00001558
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003184 File Offset: 0x00001584
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000031EF File Offset: 0x000015EF
		public Vector3 position
		{
			get
			{
				return (!this.updatePosition) ? this.simulatedPosition : this.tr.position;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003212 File Offset: 0x00001612
		public Quaternion rotation
		{
			get
			{
				return (!this.updateRotation) ? this.simulatedRotation : this.tr.rotation;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003235 File Offset: 0x00001635
		void IAstarAI.Move(Vector3 deltaPosition)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003237 File Offset: 0x00001637
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000323F File Offset: 0x0000163F
		float IAstarAI.maxSpeed
		{
			get
			{
				return this.speed;
			}
			set
			{
				this.speed = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003248 File Offset: 0x00001648
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003250 File Offset: 0x00001650
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003259 File Offset: 0x00001659
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003261 File Offset: 0x00001661
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000326A File Offset: 0x0000166A
		Vector3 IAstarAI.velocity
		{
			get
			{
				return (Time.deltaTime <= 1E-05f) ? Vector3.zero : ((this.previousPosition1 - this.previousPosition2) / Time.deltaTime);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000032A0 File Offset: 0x000016A0
		Vector3 IAstarAI.desiredVelocity
		{
			get
			{
				return ((IAstarAI)this).velocity;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000032A8 File Offset: 0x000016A8
		Vector3 IAstarAI.steeringTarget
		{
			get
			{
				return (!this.interpolator.valid) ? this.simulatedPosition : (this.interpolator.position + this.interpolator.tangent);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000032E0 File Offset: 0x000016E0
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000032F7 File Offset: 0x000016F7
		public float remainingDistance
		{
			get
			{
				return Mathf.Max(this.interpolator.remainingDistance, 0f);
			}
			set
			{
				this.interpolator.remainingDistance = Mathf.Max(value, 0f);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000330F File Offset: 0x0000170F
		public bool hasPath
		{
			get
			{
				return this.interpolator.valid;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000331C File Offset: 0x0000171C
		public bool pathPending
		{
			get
			{
				return !this.canSearchAgain;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003327 File Offset: 0x00001727
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000332F File Offset: 0x0000172F
		public bool isStopped { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003338 File Offset: 0x00001738
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00003340 File Offset: 0x00001740
		public Action onSearchPath { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00003349 File Offset: 0x00001749
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
			this.seeker = base.GetComponent<Seeker>();
			this.seeker.startEndModifier.adjustStartPoint = (() => this.simulatedPosition);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003385 File Offset: 0x00001785
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.Init();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003394 File Offset: 0x00001794
		protected virtual void OnEnable()
		{
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
			this.Init();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000033C4 File Offset: 0x000017C4
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

		// Token: 0x06000056 RID: 86 RVA: 0x000033FC File Offset: 0x000017FC
		public void OnDisable()
		{
			if (this.seeker != null)
			{
				this.seeker.CancelCurrentPathRequest(true);
			}
			this.canSearchAgain = true;
			if (this.path != null)
			{
				this.path.Release(this, false);
			}
			this.path = null;
			this.interpolator.SetPath(null);
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003480 File Offset: 0x00001880
		public void Teleport(Vector3 position, bool clearPath = true)
		{
			if (clearPath)
			{
				this.interpolator.SetPath(null);
			}
			this.previousPosition2 = position;
			this.previousPosition1 = position;
			this.simulatedPosition = position;
			if (this.updatePosition)
			{
				this.tr.position = position;
			}
			this.reachedEndOfPath = false;
			if (clearPath)
			{
				this.SearchPath();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000034E4 File Offset: 0x000018E4
		protected virtual bool shouldRecalculatePath
		{
			get
			{
				return Time.time - this.lastRepath >= this.repathRate && this.canSearchAgain && this.canSearch && !float.IsPositiveInfinity(this.destination.x);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003537 File Offset: 0x00001937
		[Obsolete("Use SearchPath instead")]
		public virtual void ForceSearchPath()
		{
			this.SearchPath();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003540 File Offset: 0x00001940
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
			Vector3 feetPosition = this.GetFeetPosition();
			this.canSearchAgain = false;
			this.seeker.StartPath(feetPosition, this.destination);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000035A8 File Offset: 0x000019A8
		public virtual void OnTargetReached()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000035AC File Offset: 0x000019AC
		protected virtual void OnPathComplete(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null)
			{
				throw new Exception("This function only handles ABPaths, do not use special path types");
			}
			this.canSearchAgain = true;
			abpath.Claim(this);
			if (abpath.error)
			{
				abpath.Release(this, false);
				return;
			}
			if (this.interpolatePathSwitches)
			{
				this.ConfigurePathSwitchInterpolation();
			}
			ABPath abpath2 = this.path;
			this.path = abpath;
			this.reachedEndOfPath = false;
			if (this.path.vectorPath != null && this.path.vectorPath.Count == 1)
			{
				this.path.vectorPath.Insert(0, this.GetFeetPosition());
			}
			this.ConfigureNewPath();
			if (abpath2 != null)
			{
				abpath2.Release(this, false);
			}
			if (this.interpolator.remainingDistance < 0.0001f && !this.reachedEndOfPath)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003698 File Offset: 0x00001A98
		public void SetPath(Path path)
		{
			if (path.PipelineState == PathState.Created)
			{
				this.lastRepath = Time.time;
				this.canSearchAgain = false;
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

		// Token: 0x0600005E RID: 94 RVA: 0x00003734 File Offset: 0x00001B34
		protected virtual void ConfigurePathSwitchInterpolation()
		{
			bool flag = this.interpolator.valid && this.interpolator.remainingDistance < 0.0001f;
			if (this.interpolator.valid && !flag)
			{
				this.previousMovementOrigin = this.interpolator.position;
				this.previousMovementDirection = this.interpolator.tangent.normalized * this.interpolator.remainingDistance;
				this.pathSwitchInterpolationTime = 0f;
			}
			else
			{
				this.previousMovementOrigin = Vector3.zero;
				this.previousMovementDirection = Vector3.zero;
				this.pathSwitchInterpolationTime = float.PositiveInfinity;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000037E8 File Offset: 0x00001BE8
		public virtual Vector3 GetFeetPosition()
		{
			return this.position;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000037F0 File Offset: 0x00001BF0
		protected virtual void ConfigureNewPath()
		{
			bool valid = this.interpolator.valid;
			Vector3 vector = (!valid) ? Vector3.zero : this.interpolator.tangent;
			this.interpolator.SetPath(this.path.vectorPath);
			this.interpolator.MoveToClosestPoint(this.GetFeetPosition());
			if (this.interpolatePathSwitches && this.switchPathInterpolationSpeed > 0.01f && valid)
			{
				float num = Mathf.Max(-Vector3.Dot(vector.normalized, this.interpolator.tangent.normalized), 0f);
				this.interpolator.distance -= this.speed * num * (1f / this.switchPathInterpolationSpeed);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000038C0 File Offset: 0x00001CC0
		protected virtual void Update()
		{
			if (this.shouldRecalculatePath)
			{
				this.SearchPath();
			}
			if (this.canMove)
			{
				Vector3 nextPosition;
				Quaternion nextRotation;
				this.MovementUpdate(Time.deltaTime, out nextPosition, out nextRotation);
				this.FinalizeMovement(nextPosition, nextRotation);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003900 File Offset: 0x00001D00
		public void MovementUpdate(float deltaTime, out Vector3 nextPosition, out Quaternion nextRotation)
		{
			if (this.updatePosition)
			{
				this.simulatedPosition = this.tr.position;
			}
			if (this.updateRotation)
			{
				this.simulatedRotation = this.tr.rotation;
			}
			Vector3 direction;
			nextPosition = this.CalculateNextPosition(out direction, (!this.isStopped) ? deltaTime : 0f);
			if (this.enableRotation)
			{
				nextRotation = this.SimulateRotationTowards(direction, deltaTime);
			}
			else
			{
				nextRotation = this.simulatedRotation;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003994 File Offset: 0x00001D94
		public void FinalizeMovement(Vector3 nextPosition, Quaternion nextRotation)
		{
			this.previousPosition2 = this.previousPosition1;
			this.simulatedPosition = nextPosition;
			this.previousPosition1 = nextPosition;
			this.simulatedRotation = nextRotation;
			if (this.updatePosition)
			{
				this.tr.position = nextPosition;
			}
			if (this.updateRotation)
			{
				this.tr.rotation = nextRotation;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000039F4 File Offset: 0x00001DF4
		private Quaternion SimulateRotationTowards(Vector3 direction, float deltaTime)
		{
			if (direction != Vector3.zero)
			{
				Quaternion quaternion = Quaternion.LookRotation(direction, (!this.rotationIn2D) ? Vector3.up : Vector3.back);
				if (this.rotationIn2D)
				{
					quaternion *= Quaternion.Euler(90f, 0f, 0f);
				}
				return Quaternion.Slerp(this.simulatedRotation, quaternion, deltaTime * this.rotationSpeed);
			}
			return this.simulatedRotation;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A74 File Offset: 0x00001E74
		protected virtual Vector3 CalculateNextPosition(out Vector3 direction, float deltaTime)
		{
			if (!this.interpolator.valid)
			{
				direction = Vector3.zero;
				return this.simulatedPosition;
			}
			this.interpolator.distance += deltaTime * this.speed;
			if (this.interpolator.remainingDistance < 0.0001f && !this.reachedEndOfPath)
			{
				this.reachedEndOfPath = true;
				this.OnTargetReached();
			}
			direction = this.interpolator.tangent;
			this.pathSwitchInterpolationTime += deltaTime;
			float num = this.switchPathInterpolationSpeed * this.pathSwitchInterpolationTime;
			if (this.interpolatePathSwitches && num < 1f)
			{
				Vector3 a = this.previousMovementOrigin + Vector3.ClampMagnitude(this.previousMovementDirection, this.speed * this.pathSwitchInterpolationTime);
				return Vector3.Lerp(a, this.interpolator.position, num);
			}
			return this.interpolator.position;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B6F File Offset: 0x00001F6F
		protected override int OnUpgradeSerializedData(int version, bool unityThread)
		{
			if (unityThread && this.targetCompatibility != null)
			{
				this.target = this.targetCompatibility;
			}
			return 2;
		}

		// Token: 0x0400002C RID: 44
		public float repathRate = 0.5f;

		// Token: 0x0400002D RID: 45
		public bool canSearch = true;

		// Token: 0x0400002E RID: 46
		public bool canMove = true;

		// Token: 0x0400002F RID: 47
		public float speed = 3f;

		// Token: 0x04000030 RID: 48
		public bool enableRotation = true;

		// Token: 0x04000031 RID: 49
		public bool rotationIn2D;

		// Token: 0x04000032 RID: 50
		public float rotationSpeed = 10f;

		// Token: 0x04000033 RID: 51
		public bool interpolatePathSwitches = true;

		// Token: 0x04000034 RID: 52
		public float switchPathInterpolationSpeed = 5f;

		// Token: 0x04000037 RID: 55
		[NonSerialized]
		public bool updatePosition = true;

		// Token: 0x04000038 RID: 56
		[NonSerialized]
		public bool updateRotation = true;

		// Token: 0x0400003B RID: 59
		protected Seeker seeker;

		// Token: 0x0400003C RID: 60
		protected Transform tr;

		// Token: 0x0400003D RID: 61
		protected float lastRepath = -9999f;

		// Token: 0x0400003E RID: 62
		protected ABPath path;

		// Token: 0x0400003F RID: 63
		protected bool canSearchAgain = true;

		// Token: 0x04000040 RID: 64
		protected Vector3 previousMovementOrigin;

		// Token: 0x04000041 RID: 65
		protected Vector3 previousMovementDirection;

		// Token: 0x04000042 RID: 66
		protected float pathSwitchInterpolationTime;

		// Token: 0x04000043 RID: 67
		protected PathInterpolator interpolator = new PathInterpolator();

		// Token: 0x04000044 RID: 68
		private bool startHasRun;

		// Token: 0x04000045 RID: 69
		private Vector3 previousPosition1;

		// Token: 0x04000046 RID: 70
		private Vector3 previousPosition2;

		// Token: 0x04000047 RID: 71
		private Vector3 simulatedPosition;

		// Token: 0x04000048 RID: 72
		private Quaternion simulatedRotation;

		// Token: 0x04000049 RID: 73
		[FormerlySerializedAs("target")]
		[SerializeField]
		[HideInInspector]
		private Transform targetCompatibility;
	}
}
