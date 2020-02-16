using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000110 RID: 272
	[ExecuteInEditMode]
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Simulator")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_simulator.php")]
	public class RVOSimulator : VersionedMonoBehaviour
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0004C489 File Offset: 0x0004A889
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0004C490 File Offset: 0x0004A890
		public static RVOSimulator active { get; private set; }

		// Token: 0x060009EC RID: 2540 RVA: 0x0004C498 File Offset: 0x0004A898
		public Simulator GetSimulator()
		{
			if (this.simulator == null)
			{
				this.Awake();
			}
			return this.simulator;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0004C4B1 File Offset: 0x0004A8B1
		private void OnEnable()
		{
			RVOSimulator.active = this;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0004C4BC File Offset: 0x0004A8BC
		protected override void Awake()
		{
			base.Awake();
			if (this.simulator == null && Application.isPlaying)
			{
				int workers = AstarPath.CalculateThreadCount(this.workerThreads);
				this.simulator = new Simulator(workers, this.doubleBuffering, this.movementPlane);
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0004C508 File Offset: 0x0004A908
		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.desiredSimulationFPS < 1)
			{
				this.desiredSimulationFPS = 1;
			}
			Simulator simulator = this.GetSimulator();
			simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			simulator.symmetryBreakingBias = this.symmetryBreakingBias;
			simulator.Update();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0004C55F File Offset: 0x0004A95F
		private void OnDestroy()
		{
			RVOSimulator.active = null;
			if (this.simulator != null)
			{
				this.simulator.OnDestroy();
			}
		}

		// Token: 0x040006D8 RID: 1752
		[Tooltip("Desired FPS for rvo simulation. It is usually not necessary to run a crowd simulation at a very high fps.\nUsually 10-30 fps is enough, but can be increased for better quality.\nThe rvo simulation will never run at a higher fps than the game")]
		public int desiredSimulationFPS = 20;

		// Token: 0x040006D9 RID: 1753
		[Tooltip("Number of RVO worker threads. If set to None, no multithreading will be used.")]
		public ThreadCount workerThreads = ThreadCount.Two;

		// Token: 0x040006DA RID: 1754
		[Tooltip("Calculate local avoidance in between frames.\nThis can increase jitter in the agents' movement so use it only if you really need the performance boost. It will also reduce the responsiveness of the agents to the commands you send to them.")]
		public bool doubleBuffering;

		// Token: 0x040006DB RID: 1755
		[Tooltip("Bias agents to pass each other on the right side.\nIf the desired velocity of an agent puts it on a collision course with another agent or an obstacle its desired velocity will be rotated this number of radians (1 radian is approximately 57°) to the right. This helps to break up symmetries and makes it possible to resolve some situations much faster.\n\nWhen many agents have the same goal this can however have the side effect that the group clustered around the target point may as a whole start to spin around the target point.")]
		[Range(0f, 0.2f)]
		public float symmetryBreakingBias = 0.1f;

		// Token: 0x040006DC RID: 1756
		[Tooltip("Determines if the XY (2D) or XZ (3D) plane is used for movement")]
		public MovementPlane movementPlane;

		// Token: 0x040006DD RID: 1757
		public bool drawObstacles;

		// Token: 0x040006DE RID: 1758
		private Simulator simulator;
	}
}
