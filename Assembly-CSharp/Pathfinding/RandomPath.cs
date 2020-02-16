using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000107 RID: 263
	public class RandomPath : ABPath
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x00049BA2 File Offset: 0x00047FA2
		public RandomPath()
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00049BC0 File Offset: 0x00047FC0
		[Obsolete("This constructor is obsolete. Please use the pooling API and the Construct methods")]
		public RandomPath(Vector3 start, int length, OnPathDelegate callback = null)
		{
			throw new Exception("This constructor is obsolete. Please use the pooling API and the Setup methods");
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00049BE8 File Offset: 0x00047FE8
		internal override bool FloodingPath
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00049BEB File Offset: 0x00047FEB
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00049BF0 File Offset: 0x00047FF0
		protected override void Reset()
		{
			base.Reset();
			this.searchLength = 5000;
			this.spread = 5000;
			this.aimStrength = 0f;
			this.chosenNodeR = null;
			this.maxGScoreNodeR = null;
			this.maxGScore = 0;
			this.aim = Vector3.zero;
			this.nodesEvaluatedRep = 0;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00049C4C File Offset: 0x0004804C
		public static RandomPath Construct(Vector3 start, int length, OnPathDelegate callback = null)
		{
			RandomPath path = PathPool.GetPath<RandomPath>();
			path.Setup(start, length, callback);
			return path;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00049C6C File Offset: 0x0004806C
		protected RandomPath Setup(Vector3 start, int length, OnPathDelegate callback)
		{
			this.callback = callback;
			this.searchLength = length;
			this.originalStartPoint = start;
			this.originalEndPoint = Vector3.zero;
			this.startPoint = start;
			this.endPoint = Vector3.zero;
			this.startIntPoint = (Int3)start;
			return this;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00049CB8 File Offset: 0x000480B8
		protected override void ReturnPath()
		{
			if (this.path != null && this.path.Count > 0)
			{
				this.endNode = this.path[this.path.Count - 1];
				this.endPoint = (Vector3)this.endNode.position;
				this.originalEndPoint = this.endPoint;
				this.hTarget = this.endNode.position;
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00049D4C File Offset: 0x0004814C
		protected override void Prepare()
		{
			this.nnConstraint.tags = this.enabledTags;
			NNInfo nearest = AstarPath.active.GetNearest(this.startPoint, this.nnConstraint);
			this.startPoint = nearest.position;
			this.endPoint = this.startPoint;
			this.startIntPoint = (Int3)this.startPoint;
			this.hTarget = (Int3)this.aim;
			this.startNode = nearest.node;
			this.endNode = this.startNode;
			if (this.startNode == null || this.endNode == null)
			{
				base.FailWithError("Couldn't find close nodes to the start point");
				return;
			}
			if (!base.CanTraverse(this.startNode))
			{
				base.FailWithError("The node closest to the start point could not be traversed");
				return;
			}
			this.heuristicScale = this.aimStrength;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00049E20 File Offset: 0x00048220
		protected override void Initialize()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			pathNode.node = this.startNode;
			if (this.searchLength + this.spread <= 0)
			{
				base.CompleteState = PathCompleteState.Complete;
				this.Trace(pathNode);
				return;
			}
			pathNode.pathID = base.pathID;
			pathNode.parent = null;
			pathNode.cost = 0u;
			pathNode.G = base.GetTraversalCost(this.startNode);
			pathNode.H = base.CalculateHScore(this.startNode);
			this.startNode.Open(this, pathNode, this.pathHandler);
			this.searchedNodes++;
			if (this.pathHandler.heap.isEmpty)
			{
				base.FailWithError("No open points, the start node didn't open any nodes");
				return;
			}
			this.currentR = this.pathHandler.heap.Remove();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00049F04 File Offset: 0x00048304
		protected override void CalculateStep(long targetTick)
		{
			int num = 0;
			while (base.CompleteState == PathCompleteState.NotCalculated)
			{
				this.searchedNodes++;
				if ((ulong)this.currentR.G >= (ulong)((long)this.searchLength))
				{
					if ((ulong)this.currentR.G > (ulong)((long)(this.searchLength + this.spread)))
					{
						if (this.chosenNodeR == null)
						{
							this.chosenNodeR = this.currentR;
						}
						base.CompleteState = PathCompleteState.Complete;
						break;
					}
					this.nodesEvaluatedRep++;
					if (this.rnd.NextDouble() <= (double)(1f / (float)this.nodesEvaluatedRep))
					{
						this.chosenNodeR = this.currentR;
					}
				}
				else if ((ulong)this.currentR.G > (ulong)((long)this.maxGScore))
				{
					this.maxGScore = (int)this.currentR.G;
					this.maxGScoreNodeR = this.currentR;
				}
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					if (this.chosenNodeR != null)
					{
						base.CompleteState = PathCompleteState.Complete;
					}
					else if (this.maxGScoreNodeR != null)
					{
						this.chosenNodeR = this.maxGScoreNodeR;
						base.CompleteState = PathCompleteState.Complete;
					}
					else
					{
						base.FailWithError("Not a single node found to search");
					}
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
			if (base.CompleteState == PathCompleteState.Complete)
			{
				this.Trace(this.chosenNodeR);
			}
		}

		// Token: 0x040006A8 RID: 1704
		public int searchLength;

		// Token: 0x040006A9 RID: 1705
		public int spread = 5000;

		// Token: 0x040006AA RID: 1706
		public float aimStrength;

		// Token: 0x040006AB RID: 1707
		private PathNode chosenNodeR;

		// Token: 0x040006AC RID: 1708
		private PathNode maxGScoreNodeR;

		// Token: 0x040006AD RID: 1709
		private int maxGScore;

		// Token: 0x040006AE RID: 1710
		public Vector3 aim;

		// Token: 0x040006AF RID: 1711
		private int nodesEvaluatedRep;

		// Token: 0x040006B0 RID: 1712
		private readonly System.Random rnd = new System.Random();
	}
}
