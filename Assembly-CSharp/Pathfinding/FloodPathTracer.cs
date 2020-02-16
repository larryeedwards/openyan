using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000104 RID: 260
	public class FloodPathTracer : ABPath
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0004A52E File Offset: 0x0004892E
		protected override bool hasEndPoint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0004A534 File Offset: 0x00048934
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool.GetPath<FloodPathTracer>();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0004A551 File Offset: 0x00048951
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.PipelineState < PathState.Returned)
			{
				throw new ArgumentException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004A591 File Offset: 0x00048991
		protected override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0004A5A0 File Offset: 0x000489A0
		protected override void Initialize()
		{
			if (this.startNode != null && this.flood.HasPathTo(this.startNode))
			{
				this.Trace(this.startNode);
				base.CompleteState = PathCompleteState.Complete;
			}
			else
			{
				base.FailWithError("Could not find valid start node");
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0004A5F1 File Offset: 0x000489F1
		protected override void CalculateStep(long targetTick)
		{
			if (!base.IsDone())
			{
				throw new Exception("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0004A60C File Offset: 0x00048A0C
		public void Trace(GraphNode from)
		{
			GraphNode graphNode = from;
			int num = 0;
			while (graphNode != null)
			{
				this.path.Add(graphNode);
				this.vectorPath.Add((Vector3)graphNode.position);
				graphNode = this.flood.GetParent(graphNode);
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (FloodPathTracer.cs, Trace function)");
					break;
				}
			}
		}

		// Token: 0x04000693 RID: 1683
		protected FloodPath flood;
	}
}
