using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000FF RID: 255
	public class ConstantPath : Path
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00049810 File Offset: 0x00047C10
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00049814 File Offset: 0x00047C14
		public static ConstantPath Construct(Vector3 start, int maxGScore, OnPathDelegate callback = null)
		{
			ConstantPath path = PathPool.GetPath<ConstantPath>();
			path.Setup(start, maxGScore, callback);
			return path;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00049831 File Offset: 0x00047C31
		protected void Setup(Vector3 start, int maxGScore, OnPathDelegate callback)
		{
			this.callback = callback;
			this.startPoint = start;
			this.originalStartPoint = this.startPoint;
			this.endingCondition = new EndingConditionDistance(this, maxGScore);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0004985A File Offset: 0x00047C5A
		protected override void OnEnterPool()
		{
			base.OnEnterPool();
			if (this.allNodes != null)
			{
				ListPool<GraphNode>.Release(ref this.allNodes);
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00049878 File Offset: 0x00047C78
		protected override void Reset()
		{
			base.Reset();
			this.allNodes = ListPool<GraphNode>.Claim();
			this.endingCondition = null;
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000498B8 File Offset: 0x00047CB8
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			this.startNode = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint).node;
			if (this.startNode == null)
			{
				base.FailWithError("Could not find close node to the start point");
				return;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00049914 File Offset: 0x00047D14
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			pathNode.flag1 = true;
			this.allNodes.Add(this.startNode);
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000499F0 File Offset: 0x00047DF0
		protected override void Cleanup()
		{
			int count = this.allNodes.Count;
			for (int i = 0; i < count; i++)
			{
				this.pathHandler.GetPathNode(this.allNodes[i]).flag1 = false;
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00049A38 File Offset: 0x00047E38
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if (this.endingCondition.TargetFound(this.currentR))
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				if (!this.currentR.flag1)
				{
					this.allNodes.Add(this.currentR.node);
					this.currentR.flag1 = true;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.CompleteState = PathCompleteState.Complete;
					break;
				}
				this.currentR = this.pathHandler.heap.Remove();
				if (num > 500)
				{
					if (DateTime.UtcNow.Ticks >= targetTick)
					{
						return;
					}
					num = 0;
					if (this.searchedNodes > 1000000)
					{
						throw new Exception("Probable infinite loop. Over 1,000,000 nodes searched");
					}
				}
				num++;
			}
		}

		// Token: 0x04000687 RID: 1671
		public GraphNode startNode;

		// Token: 0x04000688 RID: 1672
		public Vector3 startPoint;

		// Token: 0x04000689 RID: 1673
		public Vector3 originalStartPoint;

		// Token: 0x0400068A RID: 1674
		public List<GraphNode> allNodes;

		// Token: 0x0400068B RID: 1675
		public PathEndingCondition endingCondition;
	}
}
