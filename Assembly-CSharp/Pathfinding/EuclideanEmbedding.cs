using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class EuclideanEmbedding
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0003591F File Offset: 0x00033D1F
		private uint GetRandom()
		{
			this.rval = 12820163u * this.rval + 1140671485u;
			return this.rval;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00035940 File Offset: 0x00033D40
		private void EnsureCapacity(int index)
		{
			if (index > this.maxNodeIndex)
			{
				object obj = this.lockObj;
				lock (obj)
				{
					if (index > this.maxNodeIndex)
					{
						if (index >= this.costs.Length)
						{
							uint[] array = new uint[Math.Max(index * 2, this.pivots.Length * 2)];
							for (int i = 0; i < this.costs.Length; i++)
							{
								array[i] = this.costs[i];
							}
							this.costs = array;
						}
						this.maxNodeIndex = index;
					}
				}
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000359E8 File Offset: 0x00033DE8
		public uint GetHeuristic(int nodeIndex1, int nodeIndex2)
		{
			nodeIndex1 *= this.pivotCount;
			nodeIndex2 *= this.pivotCount;
			if (nodeIndex1 >= this.costs.Length || nodeIndex2 >= this.costs.Length)
			{
				this.EnsureCapacity((nodeIndex1 <= nodeIndex2) ? nodeIndex2 : nodeIndex1);
			}
			uint num = 0u;
			for (int i = 0; i < this.pivotCount; i++)
			{
				uint num2 = (uint)Math.Abs((int)(this.costs[nodeIndex1 + i] - this.costs[nodeIndex2 + i]));
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00035A78 File Offset: 0x00033E78
		private void GetClosestWalkableNodesToChildrenRecursively(Transform tr, List<GraphNode> nodes)
		{
			IEnumerator enumerator = tr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					NNInfo nearest = AstarPath.active.GetNearest(transform.position, NNConstraint.Default);
					if (nearest.node != null && nearest.node.Walkable)
					{
						nodes.Add(nearest.node);
					}
					this.GetClosestWalkableNodesToChildrenRecursively(transform, nodes);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00035B1C File Offset: 0x00033F1C
		private void PickNRandomNodes(int count, List<GraphNode> buffer)
		{
			int n = 0;
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				graphs[i].GetNodes(delegate(GraphNode node)
				{
					if (!node.Destroyed && node.Walkable)
					{
						n++;
						if ((ulong)this.GetRandom() % (ulong)((long)n) < (ulong)((long)count))
						{
							if (buffer.Count < count)
							{
								buffer.Add(node);
							}
							else
							{
								buffer[(int)((ulong)this.GetRandom() % (ulong)((long)buffer.Count))] = node;
							}
						}
					}
				});
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00035B80 File Offset: 0x00033F80
		private GraphNode PickAnyWalkableNode()
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			GraphNode first = null;
			for (int i = 0; i < graphs.Length; i++)
			{
				graphs[i].GetNodes(delegate(GraphNode node)
				{
					if (node != null && node.Walkable && first == null)
					{
						first = node;
					}
				});
			}
			return first;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00035BD4 File Offset: 0x00033FD4
		public void RecalculatePivots()
		{
			if (this.mode == HeuristicOptimizationMode.None)
			{
				this.pivotCount = 0;
				this.pivots = null;
				return;
			}
			this.rval = (uint)this.seed;
			List<GraphNode> list = ListPool<GraphNode>.Claim();
			switch (this.mode)
			{
			case HeuristicOptimizationMode.Random:
				this.PickNRandomNodes(this.spreadOutCount, list);
				break;
			case HeuristicOptimizationMode.RandomSpreadOut:
			{
				if (this.pivotPointRoot != null)
				{
					this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, list);
				}
				if (list.Count == 0)
				{
					GraphNode graphNode = this.PickAnyWalkableNode();
					if (graphNode == null)
					{
						Debug.LogError("Could not find any walkable node in any of the graphs.");
						ListPool<GraphNode>.Release(ref list);
						return;
					}
					list.Add(graphNode);
				}
				int num = this.spreadOutCount - list.Count;
				for (int i = 0; i < num; i++)
				{
					list.Add(null);
				}
				break;
			}
			case HeuristicOptimizationMode.Custom:
				if (this.pivotPointRoot == null)
				{
					throw new Exception("heuristicOptimizationMode is HeuristicOptimizationMode.Custom, but no 'customHeuristicOptimizationPivotsRoot' is set");
				}
				this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, list);
				break;
			default:
				throw new Exception("Invalid HeuristicOptimizationMode: " + this.mode);
			}
			this.pivots = list.ToArray();
			ListPool<GraphNode>.Release(ref list);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00035D20 File Offset: 0x00034120
		public void RecalculateCosts()
		{
			EuclideanEmbedding.<RecalculateCosts>c__AnonStorey2 <RecalculateCosts>c__AnonStorey = new EuclideanEmbedding.<RecalculateCosts>c__AnonStorey2();
			<RecalculateCosts>c__AnonStorey.$this = this;
			if (this.pivots == null)
			{
				this.RecalculatePivots();
			}
			if (this.mode == HeuristicOptimizationMode.None)
			{
				return;
			}
			this.pivotCount = 0;
			for (int i = 0; i < this.pivots.Length; i++)
			{
				if (this.pivots[i] != null && (this.pivots[i].Destroyed || !this.pivots[i].Walkable))
				{
					throw new Exception("Invalid pivot nodes (destroyed or unwalkable)");
				}
			}
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int j = 0; j < this.pivots.Length; j++)
				{
					if (this.pivots[j] == null)
					{
						throw new Exception("Invalid pivot nodes (null)");
					}
				}
			}
			Debug.Log("Recalculating costs...");
			this.pivotCount = this.pivots.Length;
			<RecalculateCosts>c__AnonStorey.startCostCalculation = null;
			<RecalculateCosts>c__AnonStorey.numComplete = 0;
			<RecalculateCosts>c__AnonStorey.onComplete = delegate(Path path)
			{
				<RecalculateCosts>c__AnonStorey.numComplete++;
				if (<RecalculateCosts>c__AnonStorey.numComplete == <RecalculateCosts>c__AnonStorey.$this.pivotCount)
				{
					<RecalculateCosts>c__AnonStorey.$this.ApplyGridGraphEndpointSpecialCase();
				}
			};
			<RecalculateCosts>c__AnonStorey.startCostCalculation = delegate(int pivotIndex)
			{
				GraphNode pivot = <RecalculateCosts>c__AnonStorey.$this.pivots[pivotIndex];
				FloodPath floodPath = null;
				floodPath = FloodPath.Construct(pivot, <RecalculateCosts>c__AnonStorey.onComplete);
				floodPath.immediateCallback = delegate(Path _p)
				{
					_p.Claim(<RecalculateCosts>c__AnonStorey.$this);
					MeshNode meshNode = pivot as MeshNode;
					uint costOffset = 0u;
					if (meshNode != null && meshNode.connections != null)
					{
						for (int l = 0; l < meshNode.connections.Length; l++)
						{
							costOffset = Math.Max(costOffset, meshNode.connections[l].cost);
						}
					}
					NavGraph[] graphs = AstarPath.active.graphs;
					for (int m = graphs.Length - 1; m >= 0; m--)
					{
						graphs[m].GetNodes(delegate(GraphNode node)
						{
							int num6 = node.NodeIndex * <RecalculateCosts>c__AnonStorey.pivotCount + pivotIndex;
							<RecalculateCosts>c__AnonStorey.EnsureCapacity(num6);
							PathNode pathNode = ((IPathInternals)floodPath).PathHandler.GetPathNode(node);
							if (costOffset > 0u)
							{
								<RecalculateCosts>c__AnonStorey.costs[num6] = ((pathNode.pathID != floodPath.pathID || pathNode.parent == null) ? 0u : Math.Max(pathNode.parent.G - costOffset, 0u));
							}
							else
							{
								<RecalculateCosts>c__AnonStorey.costs[num6] = ((pathNode.pathID != floodPath.pathID) ? 0u : pathNode.G);
							}
						});
					}
					if (<RecalculateCosts>c__AnonStorey.mode == HeuristicOptimizationMode.RandomSpreadOut && pivotIndex < <RecalculateCosts>c__AnonStorey.pivots.Length - 1)
					{
						if (<RecalculateCosts>c__AnonStorey.pivots[pivotIndex + 1] == null)
						{
							int num = -1;
							uint num2 = 0u;
							int num3 = <RecalculateCosts>c__AnonStorey.maxNodeIndex / <RecalculateCosts>c__AnonStorey.pivotCount;
							for (int n = 1; n < num3; n++)
							{
								uint num4 = 1073741824u;
								for (int num5 = 0; num5 <= pivotIndex; num5++)
								{
									num4 = Math.Min(num4, <RecalculateCosts>c__AnonStorey.costs[n * <RecalculateCosts>c__AnonStorey.pivotCount + num5]);
								}
								GraphNode node2 = ((IPathInternals)floodPath).PathHandler.GetPathNode(n).node;
								if ((num4 > num2 || num == -1) && node2 != null && !node2.Destroyed && node2.Walkable)
								{
									num = n;
									num2 = num4;
								}
							}
							if (num == -1)
							{
								Debug.LogError("Failed generating random pivot points for heuristic optimizations");
								return;
							}
							<RecalculateCosts>c__AnonStorey.pivots[pivotIndex + 1] = ((IPathInternals)floodPath).PathHandler.GetPathNode(num).node;
						}
						<RecalculateCosts>c__AnonStorey.startCostCalculation(pivotIndex + 1);
					}
					_p.Release(<RecalculateCosts>c__AnonStorey.$this, false);
				};
				AstarPath.StartPath(floodPath, true);
			};
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int k = 0; k < this.pivots.Length; k++)
				{
					<RecalculateCosts>c__AnonStorey.startCostCalculation(k);
				}
			}
			else
			{
				<RecalculateCosts>c__AnonStorey.startCostCalculation(0);
			}
			this.dirty = false;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00035E84 File Offset: 0x00034284
		private void ApplyGridGraphEndpointSpecialCase()
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (gridGraph != null)
				{
					GridNode[] nodes = gridGraph.nodes;
					int num = (gridGraph.neighbours != NumNeighbours.Four) ? ((gridGraph.neighbours != NumNeighbours.Eight) ? 6 : 8) : 4;
					for (int j = 0; j < gridGraph.depth; j++)
					{
						for (int k = 0; k < gridGraph.width; k++)
						{
							GridNode gridNode = nodes[j * gridGraph.width + k];
							if (!gridNode.Walkable)
							{
								int num2 = gridNode.NodeIndex * this.pivotCount;
								for (int l = 0; l < this.pivotCount; l++)
								{
									this.costs[num2 + l] = uint.MaxValue;
								}
								for (int m = 0; m < num; m++)
								{
									int num3;
									int num4;
									if (gridGraph.neighbours == NumNeighbours.Six)
									{
										num3 = k + gridGraph.neighbourXOffsets[GridGraph.hexagonNeighbourIndices[m]];
										num4 = j + gridGraph.neighbourZOffsets[GridGraph.hexagonNeighbourIndices[m]];
									}
									else
									{
										num3 = k + gridGraph.neighbourXOffsets[m];
										num4 = j + gridGraph.neighbourZOffsets[m];
									}
									if (num3 >= 0 && num4 >= 0 && num3 < gridGraph.width && num4 < gridGraph.depth)
									{
										GridNode gridNode2 = gridGraph.nodes[num4 * gridGraph.width + num3];
										if (gridNode2.Walkable)
										{
											for (int n = 0; n < this.pivotCount; n++)
											{
												uint val = this.costs[gridNode2.NodeIndex * this.pivotCount + n] + gridGraph.neighbourCosts[m];
												this.costs[num2 + n] = Math.Min(this.costs[num2 + n], val);
											}
										}
									}
								}
								for (int num5 = 0; num5 < this.pivotCount; num5++)
								{
									if (this.costs[num2 + num5] == 4294967295u)
									{
										this.costs[num2 + num5] = 0u;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000360C8 File Offset: 0x000344C8
		public void OnDrawGizmos()
		{
			if (this.pivots != null)
			{
				for (int i = 0; i < this.pivots.Length; i++)
				{
					Gizmos.color = new Color(0.623529434f, 0.368627459f, 0.7607843f, 0.8f);
					if (this.pivots[i] != null && !this.pivots[i].Destroyed)
					{
						Gizmos.DrawCube((Vector3)this.pivots[i].position, Vector3.one);
					}
				}
			}
		}

		// Token: 0x040004EB RID: 1259
		public HeuristicOptimizationMode mode;

		// Token: 0x040004EC RID: 1260
		public int seed;

		// Token: 0x040004ED RID: 1261
		public Transform pivotPointRoot;

		// Token: 0x040004EE RID: 1262
		public int spreadOutCount = 1;

		// Token: 0x040004EF RID: 1263
		[NonSerialized]
		public bool dirty;

		// Token: 0x040004F0 RID: 1264
		private uint[] costs = new uint[8];

		// Token: 0x040004F1 RID: 1265
		private int maxNodeIndex;

		// Token: 0x040004F2 RID: 1266
		private int pivotCount;

		// Token: 0x040004F3 RID: 1267
		private GraphNode[] pivots;

		// Token: 0x040004F4 RID: 1268
		private const uint ra = 12820163u;

		// Token: 0x040004F5 RID: 1269
		private const uint rc = 1140671485u;

		// Token: 0x040004F6 RID: 1270
		private uint rval;

		// Token: 0x040004F7 RID: 1271
		private object lockObj = new object();
	}
}
