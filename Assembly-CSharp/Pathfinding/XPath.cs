using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000108 RID: 264
	public class XPath : ABPath
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0004B7E4 File Offset: 0x00049BE4
		public new static XPath Construct(Vector3 start, Vector3 end, OnPathDelegate callback = null)
		{
			XPath path = PathPool.GetPath<XPath>();
			path.Setup(start, end, callback);
			path.endingCondition = new ABPathEndingCondition(path);
			return path;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0004B80D File Offset: 0x00049C0D
		protected override void Reset()
		{
			base.Reset();
			this.endingCondition = null;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0004B81C File Offset: 0x00049C1C
		protected override bool EndPointGridGraphSpecialCase(GraphNode endNode)
		{
			return false;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0004B820 File Offset: 0x00049C20
		protected override void CompletePathIfStartIsValidTarget()
		{
			PathNode pathNode = this.pathHandler.GetPathNode(this.startNode);
			if (this.endingCondition.TargetFound(pathNode))
			{
				this.ChangeEndNode(this.startNode);
				this.Trace(pathNode);
				base.CompleteState = PathCompleteState.Complete;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0004B86C File Offset: 0x00049C6C
		private void ChangeEndNode(GraphNode target)
		{
			if (this.endNode != null && this.endNode != this.startNode)
			{
				PathNode pathNode = this.pathHandler.GetPathNode(this.endNode);
				PathNode pathNode2 = pathNode;
				bool flag = false;
				pathNode.flag2 = flag;
				pathNode2.flag1 = flag;
			}
			this.endNode = target;
			this.endPoint = (Vector3)target.position;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0004B8D0 File Offset: 0x00049CD0
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
				this.currentR.node.Open(this, this.currentR, this.pathHandler);
				if (this.pathHandler.heap.isEmpty)
				{
					base.FailWithError("Searched whole area but could not find target");
					return;
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
				this.ChangeEndNode(this.currentR.node);
				this.Trace(this.currentR);
			}
		}

		// Token: 0x040006B1 RID: 1713
		public PathEndingCondition endingCondition;
	}
}
