using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F1 RID: 241
	[Serializable]
	public class StartEndModifier : PathModifier
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00046B08 File Offset: 0x00044F08
		public override int Order
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00046B0C File Offset: 0x00044F0C
		public override void Apply(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null || abpath.vectorPath.Count == 0)
			{
				return;
			}
			if (abpath.vectorPath.Count == 1 && !this.addPoints)
			{
				abpath.vectorPath.Add(abpath.vectorPath[0]);
			}
			bool flag;
			Vector3 vector = this.Snap(abpath, this.exactStartPoint, true, out flag);
			bool flag2;
			Vector3 vector2 = this.Snap(abpath, this.exactEndPoint, false, out flag2);
			if ((flag || this.addPoints) && this.exactStartPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Insert(0, vector);
			}
			else
			{
				abpath.vectorPath[0] = vector;
			}
			if ((flag2 || this.addPoints) && this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Add(vector2);
			}
			else
			{
				abpath.vectorPath[abpath.vectorPath.Count - 1] = vector2;
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00046C10 File Offset: 0x00045010
		private Vector3 Snap(ABPath path, StartEndModifier.Exactness mode, bool start, out bool forceAddPoint)
		{
			int num = (!start) ? (path.path.Count - 1) : 0;
			GraphNode graphNode = path.path[num];
			Vector3 vector = (Vector3)graphNode.position;
			forceAddPoint = false;
			switch (mode)
			{
			case StartEndModifier.Exactness.SnapToNode:
				return vector;
			case StartEndModifier.Exactness.Original:
			case StartEndModifier.Exactness.Interpolate:
			case StartEndModifier.Exactness.NodeConnection:
			{
				Vector3 vector2;
				if (start)
				{
					vector2 = ((this.adjustStartPoint == null) ? path.originalStartPoint : this.adjustStartPoint());
				}
				else
				{
					vector2 = path.originalEndPoint;
				}
				switch (mode)
				{
				case StartEndModifier.Exactness.Original:
					return this.GetClampedPoint(vector, vector2, graphNode);
				case StartEndModifier.Exactness.Interpolate:
				{
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + ((!start) ? -1 : 1), 0, path.path.Count - 1)];
					return VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode2.position, vector2);
				}
				case StartEndModifier.Exactness.NodeConnection:
				{
					this.connectionBuffer = (this.connectionBuffer ?? new List<GraphNode>());
					this.connectionBufferAddDelegate = (this.connectionBufferAddDelegate ?? new Action<GraphNode>(this.connectionBuffer.Add));
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + ((!start) ? -1 : 1), 0, path.path.Count - 1)];
					graphNode.GetConnections(this.connectionBufferAddDelegate);
					Vector3 result = vector;
					float num2 = float.PositiveInfinity;
					for (int i = this.connectionBuffer.Count - 1; i >= 0; i--)
					{
						GraphNode graphNode3 = this.connectionBuffer[i];
						Vector3 vector3 = VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode3.position, vector2);
						float sqrMagnitude = (vector3 - vector2).sqrMagnitude;
						if (sqrMagnitude < num2)
						{
							result = vector3;
							num2 = sqrMagnitude;
							forceAddPoint = (graphNode3 != graphNode2);
						}
					}
					this.connectionBuffer.Clear();
					return result;
				}
				}
				throw new ArgumentException("Cannot reach this point, but the compiler is not smart enough to realize that.");
			}
			case StartEndModifier.Exactness.ClosestOnNode:
				return (!start) ? path.endPoint : path.startPoint;
			default:
				throw new ArgumentException("Invalid mode");
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00046E48 File Offset: 0x00045248
		protected Vector3 GetClampedPoint(Vector3 from, Vector3 to, GraphNode hint)
		{
			Vector3 vector = to;
			RaycastHit raycastHit;
			if (this.useRaycasting && Physics.Linecast(from, to, out raycastHit, this.mask))
			{
				vector = raycastHit.point;
			}
			if (this.useGraphRaycasting && hint != null)
			{
				IRaycastableGraph raycastableGraph = AstarData.GetGraph(hint) as IRaycastableGraph;
				GraphHitInfo graphHitInfo;
				if (raycastableGraph != null && raycastableGraph.Linecast(from, vector, hint, out graphHitInfo))
				{
					vector = graphHitInfo.point;
				}
			}
			return vector;
		}

		// Token: 0x04000634 RID: 1588
		public bool addPoints;

		// Token: 0x04000635 RID: 1589
		public StartEndModifier.Exactness exactStartPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x04000636 RID: 1590
		public StartEndModifier.Exactness exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x04000637 RID: 1591
		public Func<Vector3> adjustStartPoint;

		// Token: 0x04000638 RID: 1592
		public bool useRaycasting;

		// Token: 0x04000639 RID: 1593
		public LayerMask mask = -1;

		// Token: 0x0400063A RID: 1594
		public bool useGraphRaycasting;

		// Token: 0x0400063B RID: 1595
		private List<GraphNode> connectionBuffer;

		// Token: 0x0400063C RID: 1596
		private Action<GraphNode> connectionBufferAddDelegate;

		// Token: 0x020000F2 RID: 242
		public enum Exactness
		{
			// Token: 0x0400063E RID: 1598
			SnapToNode,
			// Token: 0x0400063F RID: 1599
			Original,
			// Token: 0x04000640 RID: 1600
			Interpolate,
			// Token: 0x04000641 RID: 1601
			ClosestOnNode,
			// Token: 0x04000642 RID: 1602
			NodeConnection
		}
	}
}
