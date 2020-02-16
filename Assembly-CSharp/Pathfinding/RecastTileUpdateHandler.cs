using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009A RID: 154
	[AddComponentMenu("Pathfinding/Navmesh/RecastTileUpdateHandler")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_recast_tile_update_handler.php")]
	public class RecastTileUpdateHandler : MonoBehaviour
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x0002527A File Offset: 0x0002367A
		public void SetGraph(RecastGraph graph)
		{
			this.graph = graph;
			if (graph == null)
			{
				return;
			}
			this.dirtyTiles = new bool[graph.tileXCount * graph.tileZCount];
			this.anyDirtyTiles = false;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000252AC File Offset: 0x000236AC
		public void ScheduleUpdate(Bounds bounds)
		{
			if (this.graph == null)
			{
				if (AstarPath.active != null)
				{
					this.SetGraph(AstarPath.active.data.recastGraph);
				}
				if (this.graph == null)
				{
					Debug.LogError("Received tile update request (from RecastTileUpdate), but no RecastGraph could be found to handle it");
					return;
				}
			}
			int num = Mathf.CeilToInt(this.graph.characterRadius / this.graph.cellSize);
			int num2 = num + 3;
			bounds.Expand(new Vector3((float)num2, 0f, (float)num2) * this.graph.cellSize * 2f);
			IntRect touchingTiles = this.graph.GetTouchingTiles(bounds);
			if (touchingTiles.Width * touchingTiles.Height > 0)
			{
				if (!this.anyDirtyTiles)
				{
					this.earliestDirty = Time.time;
					this.anyDirtyTiles = true;
				}
				for (int i = touchingTiles.ymin; i <= touchingTiles.ymax; i++)
				{
					for (int j = touchingTiles.xmin; j <= touchingTiles.xmax; j++)
					{
						this.dirtyTiles[i * this.graph.tileXCount + j] = true;
					}
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000253E6 File Offset: 0x000237E6
		private void OnEnable()
		{
			RecastTileUpdate.OnNeedUpdates += this.ScheduleUpdate;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000253F9 File Offset: 0x000237F9
		private void OnDisable()
		{
			RecastTileUpdate.OnNeedUpdates -= this.ScheduleUpdate;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002540C File Offset: 0x0002380C
		private void Update()
		{
			if (this.anyDirtyTiles && Time.time - this.earliestDirty >= this.maxThrottlingDelay && this.graph != null)
			{
				this.UpdateDirtyTiles();
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00025444 File Offset: 0x00023844
		public void UpdateDirtyTiles()
		{
			if (this.graph == null)
			{
				new InvalidOperationException("No graph is set on this object");
			}
			if (this.graph.tileXCount * this.graph.tileZCount != this.dirtyTiles.Length)
			{
				Debug.LogError("Graph has changed dimensions. Clearing queued graph updates and resetting.");
				this.SetGraph(this.graph);
				return;
			}
			for (int i = 0; i < this.graph.tileZCount; i++)
			{
				for (int j = 0; j < this.graph.tileXCount; j++)
				{
					if (this.dirtyTiles[i * this.graph.tileXCount + j])
					{
						this.dirtyTiles[i * this.graph.tileXCount + j] = false;
						Bounds tileBounds = this.graph.GetTileBounds(j, i, 1, 1);
						tileBounds.extents *= 0.5f;
						GraphUpdateObject graphUpdateObject = new GraphUpdateObject(tileBounds);
						graphUpdateObject.nnConstraint.graphMask = 1 << (int)this.graph.graphIndex;
						AstarPath.active.UpdateGraphs(graphUpdateObject);
					}
				}
			}
			this.anyDirtyTiles = false;
		}

		// Token: 0x04000403 RID: 1027
		private RecastGraph graph;

		// Token: 0x04000404 RID: 1028
		private bool[] dirtyTiles;

		// Token: 0x04000405 RID: 1029
		private bool anyDirtyTiles;

		// Token: 0x04000406 RID: 1030
		private float earliestDirty = float.NegativeInfinity;

		// Token: 0x04000407 RID: 1031
		public float maxThrottlingDelay = 0.5f;
	}
}
