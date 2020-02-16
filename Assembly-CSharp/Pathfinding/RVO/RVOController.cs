using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200010C RID: 268
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Controller")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_controller.php")]
	public class RVOController : VersionedMonoBehaviour
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00043245 File Offset: 0x00041645
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0004324D File Offset: 0x0004164D
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public LayerMask mask
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0004324F File Offset: 0x0004164F
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00043252 File Offset: 0x00041652
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public bool enableRotation
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00043254 File Offset: 0x00041654
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0004325B File Offset: 0x0004165B
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float rotationSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0004325D File Offset: 0x0004165D
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00043264 File Offset: 0x00041664
		[Obsolete("This field is obsolete in version 4.0 and will not affect anything. Use the LegacyRVOController if you need the old behaviour")]
		public float maxSpeed
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00043266 File Offset: 0x00041666
		public MovementPlane movementPlane
		{
			get
			{
				if (this.simulator != null)
				{
					return this.simulator.movementPlane;
				}
				if (RVOSimulator.active)
				{
					return RVOSimulator.active.movementPlane;
				}
				return MovementPlane.XZ;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0004329A File Offset: 0x0004169A
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x000432A2 File Offset: 0x000416A2
		public IAgent rvoAgent { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x000432AB File Offset: 0x000416AB
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x000432B3 File Offset: 0x000416B3
		public Simulator simulator { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000432BC File Offset: 0x000416BC
		public Vector3 position
		{
			get
			{
				return this.To3D(this.rvoAgent.Position, this.rvoAgent.ElevationCoordinate);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000432DC File Offset: 0x000416DC
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00043315 File Offset: 0x00041715
		public Vector3 velocity
		{
			get
			{
				float num = (Time.deltaTime <= 0.0001f) ? 0.02f : Time.deltaTime;
				return this.CalculateMovementDelta(num) / num;
			}
			set
			{
				this.rvoAgent.ForceSetVelocity(this.To2D(value));
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0004332C File Offset: 0x0004172C
		public Vector3 CalculateMovementDelta(float deltaTime)
		{
			if (this.rvoAgent == null)
			{
				return Vector3.zero;
			}
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D((this.ai == null) ? this.tr.position : this.ai.position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000433A3 File Offset: 0x000417A3
		public Vector3 CalculateMovementDelta(Vector3 position, float deltaTime)
		{
			return this.To3D(Vector2.ClampMagnitude(this.rvoAgent.CalculatedTargetPoint - this.To2D(position), this.rvoAgent.CalculatedSpeed * deltaTime), 0f);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000433D9 File Offset: 0x000417D9
		public void SetCollisionNormal(Vector3 normal)
		{
			this.rvoAgent.SetCollisionNormal(this.To2D(normal));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x000433ED File Offset: 0x000417ED
		[Obsolete("Set the 'velocity' property instead")]
		public void ForceSetVelocity(Vector3 velocity)
		{
			this.velocity = velocity;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000433F8 File Offset: 0x000417F8
		public Vector2 To2D(Vector3 p)
		{
			float num;
			return this.To2D(p, out num);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00043410 File Offset: 0x00041810
		public Vector2 To2D(Vector3 p, out float elevation)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				elevation = -p.z;
				return new Vector2(p.x, p.y);
			}
			elevation = p.y;
			return new Vector2(p.x, p.z);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00043463 File Offset: 0x00041863
		public Vector3 To3D(Vector2 p, float elevationCoordinate)
		{
			if (this.movementPlane == MovementPlane.XY)
			{
				return new Vector3(p.x, p.y, -elevationCoordinate);
			}
			return new Vector3(p.x, elevationCoordinate, p.y);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0004349B File Offset: 0x0004189B
		private void OnDisable()
		{
			if (this.simulator == null)
			{
				return;
			}
			this.simulator.RemoveAgent(this.rvoAgent);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000434BC File Offset: 0x000418BC
		private void OnEnable()
		{
			this.tr = base.transform;
			this.ai = base.GetComponent<IAstarAI>();
			if (RVOSimulator.active == null)
			{
				Debug.LogError("No RVOSimulator component found in the scene. Please add one.");
				base.enabled = false;
			}
			else
			{
				this.simulator = RVOSimulator.active.GetSimulator();
				if (this.rvoAgent != null)
				{
					this.simulator.AddAgent(this.rvoAgent);
				}
				else
				{
					this.rvoAgent = this.simulator.AddAgent(Vector2.zero, 0f);
					this.rvoAgent.PreCalculationCallback = new Action(this.UpdateAgentProperties);
				}
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0004356C File Offset: 0x0004196C
		protected void UpdateAgentProperties()
		{
			this.rvoAgent.Radius = Mathf.Max(0.001f, this.radius);
			this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
			this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			this.rvoAgent.Locked = this.locked;
			this.rvoAgent.MaxNeighbours = this.maxNeighbours;
			this.rvoAgent.DebugDraw = this.debug;
			this.rvoAgent.Layer = this.layer;
			this.rvoAgent.CollidesWith = this.collidesWith;
			this.rvoAgent.Priority = this.priority;
			float num;
			this.rvoAgent.Position = this.To2D((this.ai == null) ? this.tr.position : this.ai.position, out num);
			if (this.movementPlane == MovementPlane.XZ)
			{
				this.rvoAgent.Height = this.height;
				this.rvoAgent.ElevationCoordinate = num + this.center - 0.5f * this.height;
			}
			else
			{
				this.rvoAgent.Height = 1f;
				this.rvoAgent.ElevationCoordinate = 0f;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000436B6 File Offset: 0x00041AB6
		public void SetTarget(Vector3 pos, float speed, float maxSpeed)
		{
			if (this.simulator == null)
			{
				return;
			}
			this.rvoAgent.SetTarget(this.To2D(pos), speed, maxSpeed);
			if (this.lockWhenNotMoving)
			{
				this.locked = (speed < 0.001f);
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000436F4 File Offset: 0x00041AF4
		public void Move(Vector3 vel)
		{
			if (this.simulator == null)
			{
				return;
			}
			Vector2 b = this.To2D(vel);
			float magnitude = b.magnitude;
			this.rvoAgent.SetTarget(this.To2D((this.ai == null) ? this.tr.position : this.ai.position) + b, magnitude, magnitude);
			if (this.lockWhenNotMoving)
			{
				this.locked = (magnitude < 0.001f);
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00043775 File Offset: 0x00041B75
		[Obsolete("Use transform.position instead, the RVOController can now handle that without any issues.")]
		public void Teleport(Vector3 pos)
		{
			this.tr.position = pos;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00043784 File Offset: 0x00041B84
		private void OnDrawGizmos()
		{
			Color color = RVOController.GizmoColor * ((!this.locked) ? 1f : 0.5f);
			Vector3 vector = (this.ai == null) ? base.transform.position : this.ai.position;
			if (this.movementPlane == MovementPlane.XY)
			{
				Draw.Gizmos.Cylinder(vector, Vector3.forward, 0f, this.radius, color);
			}
			else
			{
				Draw.Gizmos.Cylinder(vector + this.To3D(Vector2.zero, this.center - this.height * 0.5f), this.To3D(Vector2.zero, 1f), this.height, this.radius, color);
			}
		}

		// Token: 0x040006B5 RID: 1717
		[Tooltip("Radius of the agent")]
		public float radius = 0.5f;

		// Token: 0x040006B6 RID: 1718
		[Tooltip("Height of the agent. In world units")]
		public float height = 2f;

		// Token: 0x040006B7 RID: 1719
		[Tooltip("A locked unit cannot move. Other units will still avoid it. But avoidance quality is not the best")]
		public bool locked;

		// Token: 0x040006B8 RID: 1720
		[Tooltip("Automatically set #locked to true when desired velocity is approximately zero")]
		public bool lockWhenNotMoving;

		// Token: 0x040006B9 RID: 1721
		[Tooltip("How far into the future to look for collisions with other agents (in seconds)")]
		public float agentTimeHorizon = 2f;

		// Token: 0x040006BA RID: 1722
		[Tooltip("How far into the future to look for collisions with obstacles (in seconds)")]
		public float obstacleTimeHorizon = 2f;

		// Token: 0x040006BB RID: 1723
		[Tooltip("Max number of other agents to take into account.\nA smaller value can reduce CPU load, a higher value can lead to better local avoidance quality.")]
		public int maxNeighbours = 10;

		// Token: 0x040006BC RID: 1724
		public RVOLayer layer = RVOLayer.DefaultAgent;

		// Token: 0x040006BD RID: 1725
		[EnumFlag]
		public RVOLayer collidesWith = (RVOLayer)(-1);

		// Token: 0x040006BE RID: 1726
		[HideInInspector]
		[Obsolete]
		public float wallAvoidForce = 1f;

		// Token: 0x040006BF RID: 1727
		[HideInInspector]
		[Obsolete]
		public float wallAvoidFalloff = 1f;

		// Token: 0x040006C0 RID: 1728
		[Tooltip("How strongly other agents will avoid this agent")]
		[Range(0f, 1f)]
		public float priority = 0.5f;

		// Token: 0x040006C1 RID: 1729
		[Tooltip("Center of the agent relative to the pivot point of this game object")]
		public float center = 1f;

		// Token: 0x040006C4 RID: 1732
		protected Transform tr;

		// Token: 0x040006C5 RID: 1733
		protected IAstarAI ai;

		// Token: 0x040006C6 RID: 1734
		public bool debug;

		// Token: 0x040006C7 RID: 1735
		private static readonly Color GizmoColor = new Color(0.9411765f, 0.8352941f, 0.117647059f);
	}
}
