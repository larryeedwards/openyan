using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000120 RID: 288
	public class GraphGizmoHelper : IAstarPooledObject, IDisposable
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x0004ED2B File Offset: 0x0004D12B
		public GraphGizmoHelper()
		{
			this.drawConnection = new Action<GraphNode>(this.DrawConnection);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0004ED45 File Offset: 0x0004D145
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x0004ED4D File Offset: 0x0004D14D
		public RetainedGizmos.Hasher hasher { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0004ED56 File Offset: 0x0004D156
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x0004ED5E File Offset: 0x0004D15E
		public RetainedGizmos.Builder builder { get; private set; }

		// Token: 0x06000A43 RID: 2627 RVA: 0x0004ED68 File Offset: 0x0004D168
		public void Init(AstarPath active, RetainedGizmos.Hasher hasher, RetainedGizmos gizmos)
		{
			if (active != null)
			{
				this.debugData = active.debugPathData;
				this.debugPathID = active.debugPathID;
				this.debugMode = active.debugMode;
				this.debugFloor = active.debugFloor;
				this.debugRoof = active.debugRoof;
				this.showSearchTree = (active.showSearchTree && this.debugData != null);
			}
			this.gizmos = gizmos;
			this.hasher = hasher;
			this.builder = ObjectPool<RetainedGizmos.Builder>.Claim();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0004EDF8 File Offset: 0x0004D1F8
		public void OnEnterPool()
		{
			RetainedGizmos.Builder builder = this.builder;
			ObjectPool<RetainedGizmos.Builder>.Release(ref builder);
			this.builder = null;
			this.debugData = null;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0004EE24 File Offset: 0x0004D224
		public void DrawConnections(GraphNode node)
		{
			if (this.showSearchTree)
			{
				if (GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID))
				{
					PathNode pathNode = this.debugData.GetPathNode(node);
					if (pathNode.parent != null)
					{
						this.builder.DrawLine((Vector3)node.position, (Vector3)this.debugData.GetPathNode(node).parent.node.position, this.NodeColor(node));
					}
				}
			}
			else
			{
				this.drawConnectionColor = this.NodeColor(node);
				this.drawConnectionStart = (Vector3)node.position;
				node.GetConnections(this.drawConnection);
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0004EED7 File Offset: 0x0004D2D7
		private void DrawConnection(GraphNode other)
		{
			this.builder.DrawLine(this.drawConnectionStart, Vector3.Lerp((Vector3)other.position, this.drawConnectionStart, 0.5f), this.drawConnectionColor);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0004EF0C File Offset: 0x0004D30C
		public Color NodeColor(GraphNode node)
		{
			if (this.showSearchTree && !GraphGizmoHelper.InSearchTree(node, this.debugData, this.debugPathID))
			{
				return Color.clear;
			}
			Color result;
			if (node.Walkable)
			{
				GraphDebugMode graphDebugMode = this.debugMode;
				switch (graphDebugMode)
				{
				case GraphDebugMode.Penalty:
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (node.Penalty - this.debugFloor) / (this.debugRoof - this.debugFloor));
					break;
				case GraphDebugMode.Connections:
					result = AstarColor.NodeConnection;
					break;
				case GraphDebugMode.Tags:
					result = AstarColor.GetAreaColor(node.Tag);
					break;
				default:
					if (graphDebugMode != GraphDebugMode.Areas)
					{
						if (this.debugData == null)
						{
							result = AstarColor.NodeConnection;
						}
						else
						{
							PathNode pathNode = this.debugData.GetPathNode(node);
							float num;
							if (this.debugMode == GraphDebugMode.G)
							{
								num = pathNode.G;
							}
							else if (this.debugMode == GraphDebugMode.H)
							{
								num = pathNode.H;
							}
							else
							{
								num = pathNode.F;
							}
							result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, (num - this.debugFloor) / (this.debugRoof - this.debugFloor));
						}
					}
					else
					{
						result = AstarColor.GetAreaColor(node.Area);
					}
					break;
				}
			}
			else
			{
				result = AstarColor.UnwalkableNode;
			}
			return result;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0004F068 File Offset: 0x0004D468
		public static bool InSearchTree(GraphNode node, PathHandler handler, ushort pathID)
		{
			return handler.GetPathNode(node).pathID == pathID;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0004F079 File Offset: 0x0004D479
		public void DrawWireTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
		{
			this.builder.DrawLine(a, b, color);
			this.builder.DrawLine(b, c, color);
			this.builder.DrawLine(c, a, color);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0004F0A8 File Offset: 0x0004D4A8
		public void DrawTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			List<int> list = ListPool<int>.Claim(numTriangles);
			for (int i = 0; i < numTriangles * 3; i++)
			{
				list.Add(i);
			}
			this.builder.DrawMesh(this.gizmos, vertices, list, colors);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0004F0F4 File Offset: 0x0004D4F4
		public void DrawWireTriangles(Vector3[] vertices, Color[] colors, int numTriangles)
		{
			for (int i = 0; i < numTriangles; i++)
			{
				this.DrawWireTriangle(vertices[i * 3], vertices[i * 3 + 1], vertices[i * 3 + 2], colors[i * 3]);
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0004F155 File Offset: 0x0004D555
		public void Submit()
		{
			this.builder.Submit(this.gizmos, this.hasher);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0004F170 File Offset: 0x0004D570
		void IDisposable.Dispose()
		{
			GraphGizmoHelper graphGizmoHelper = this;
			this.Submit();
			ObjectPool<GraphGizmoHelper>.Release(ref graphGizmoHelper);
		}

		// Token: 0x0400070B RID: 1803
		private RetainedGizmos gizmos;

		// Token: 0x0400070C RID: 1804
		private PathHandler debugData;

		// Token: 0x0400070D RID: 1805
		private ushort debugPathID;

		// Token: 0x0400070E RID: 1806
		private GraphDebugMode debugMode;

		// Token: 0x0400070F RID: 1807
		private bool showSearchTree;

		// Token: 0x04000710 RID: 1808
		private float debugFloor;

		// Token: 0x04000711 RID: 1809
		private float debugRoof;

		// Token: 0x04000713 RID: 1811
		private Vector3 drawConnectionStart;

		// Token: 0x04000714 RID: 1812
		private Color drawConnectionColor;

		// Token: 0x04000715 RID: 1813
		private readonly Action<GraphNode> drawConnection;
	}
}
