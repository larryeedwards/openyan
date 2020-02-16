using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000102 RID: 258
	public class FloodPath : Path
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0004A170 File Offset: 0x00048570
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0004A173 File Offset: 0x00048573
		public bool HasPathTo(GraphNode node)
		{
			return this.parents != null && this.parents.ContainsKey(node);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0004A18F File Offset: 0x0004858F
		public GraphNode GetParent(GraphNode node)
		{
			return this.parents[node];
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0004A1A0 File Offset: 0x000485A0
		public static FloodPath Construct(Vector3 start, OnPathDelegate callback = null)
		{
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0004A1BC File Offset: 0x000485BC
		public static FloodPath Construct(GraphNode start, OnPathDelegate callback = null)
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			FloodPath path = PathPool.GetPath<FloodPath>();
			path.Setup(start, callback);
			return path;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0004A1E9 File Offset: 0x000485E9
		protected void Setup(Vector3 start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = start;
			this.startPoint = start;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0004A207 File Offset: 0x00048607
		protected void Setup(GraphNode start, OnPathDelegate callback)
		{
			this.callback = callback;
			this.originalStartPoint = (Vector3)start.position;
			this.startNode = start;
			this.startPoint = (Vector3)start.position;
			this.heuristic = Heuristic.None;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0004A240 File Offset: 0x00048640
		protected override void Reset()
		{
			base.Reset();
			this.originalStartPoint = Vector3.zero;
			this.startPoint = Vector3.zero;
			this.startNode = null;
			this.parents = new Dictionary<GraphNode, GraphNode>();
			this.saveParents = true;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0004A278 File Offset: 0x00048678
		protected override void Prepare()
		{
			if (this.startNode == null)
			{
				this.nnConstraint.tags = this.enabledTags;
				NNInfo nearest = AstarPath.active.GetNearest(this.originalStartPoint, this.nnConstraint);
				this.startPoint = nearest.position;
				this.startNode = nearest.node;
			}
			else
			{
				this.startPoint = (Vector3)this.startNode.position;
			}
			if (this.startNode == null)
			{
				base.FailWithError("Couldn't find a close node to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004A324 File Offset: 0x00048724
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			pathNode.pathID = this.pathHandler.PathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.parents[this.startNode] = null;
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.CompleteState = PathCompleteState.Complete;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0004A3F8 File Offset: 0x000487F8
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.saveParents)
				{
					this.parents[this.currentR.node] = this.currentR.parent.node;
				}
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

		// Token: 0x0400068D RID: 1677
		public Vector3 originalStartPoint;

		// Token: 0x0400068E RID: 1678
		public Vector3 startPoint;

		// Token: 0x0400068F RID: 1679
		public GraphNode startNode;

		// Token: 0x04000690 RID: 1680
		public bool saveParents = true;

		// Token: 0x04000691 RID: 1681
		protected Dictionary<GraphNode, GraphNode> parents;
	}
}
