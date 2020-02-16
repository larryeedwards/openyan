using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200010D RID: 269
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Navmesh")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_navmesh.php")]
	public class RVONavmesh : GraphModifier
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0004BA99 File Offset: 0x00049E99
		public override void OnPostCacheLoad()
		{
			this.OnLatePostScan();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0004BAA1 File Offset: 0x00049EA1
		public override void OnGraphsPostUpdate()
		{
			this.OnLatePostScan();
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0004BAAC File Offset: 0x00049EAC
		public override void OnLatePostScan()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.RemoveObstacles();
			NavGraph[] graphs = AstarPath.active.graphs;
			RVOSimulator active = RVOSimulator.active;
			if (active == null)
			{
				throw new NullReferenceException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.lastSim = active.GetSimulator();
			for (int i = 0; i < graphs.Length; i++)
			{
				RecastGraph recastGraph = graphs[i] as RecastGraph;
				INavmesh navmesh = graphs[i] as INavmesh;
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (recastGraph != null)
				{
					foreach (NavmeshTile navmesh2 in recastGraph.GetTiles())
					{
						this.AddGraphObstacles(this.lastSim, navmesh2);
					}
				}
				else if (navmesh != null)
				{
					this.AddGraphObstacles(this.lastSim, navmesh);
				}
				else if (gridGraph != null)
				{
					this.AddGraphObstacles(this.lastSim, gridGraph);
				}
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0004BB9F File Offset: 0x00049F9F
		protected override void OnDisable()
		{
			base.OnDisable();
			this.RemoveObstacles();
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0004BBB0 File Offset: 0x00049FB0
		public void RemoveObstacles()
		{
			if (this.lastSim != null)
			{
				for (int i = 0; i < this.obstacles.Count; i++)
				{
					this.lastSim.RemoveObstacle(this.obstacles[i]);
				}
				this.lastSim = null;
			}
			this.obstacles.Clear();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0004BC10 File Offset: 0x0004A010
		private void AddGraphObstacles(Simulator sim, GridGraph grid)
		{
			bool reverse = Vector3.Dot(grid.transform.TransformVector(Vector3.up), (sim.movementPlane != MovementPlane.XY) ? Vector3.up : Vector3.back) > 0f;
			GraphUtilities.GetContours(grid, delegate(Vector3[] vertices)
			{
				if (reverse)
				{
					Array.Reverse(vertices);
				}
				this.obstacles.Add(sim.AddObstacle(vertices, this.wallHeight, true));
			}, this.wallHeight * 0.4f, null);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0004BC94 File Offset: 0x0004A094
		private void AddGraphObstacles(Simulator simulator, INavmesh navmesh)
		{
			GraphUtilities.GetContours(navmesh, delegate(List<Int3> vertices, bool cycle)
			{
				Vector3[] array = new Vector3[vertices.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (Vector3)vertices[i];
				}
				ListPool<Int3>.Release(vertices);
				this.obstacles.Add(simulator.AddObstacle(array, this.wallHeight, cycle));
			});
		}

		// Token: 0x040006C8 RID: 1736
		public float wallHeight = 5f;

		// Token: 0x040006C9 RID: 1737
		private readonly List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040006CA RID: 1738
		private Simulator lastSim;
	}
}
