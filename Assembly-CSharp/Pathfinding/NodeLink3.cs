using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200004B RID: 75
	[AddComponentMenu("Pathfinding/Link3")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_node_link3.php")]
	public class NodeLink3 : GraphModifier
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00014D34 File Offset: 0x00013134
		public static NodeLink3 GetNodeLink(GraphNode node)
		{
			NodeLink3 result;
			NodeLink3.reference.TryGetValue(node, out result);
			return result;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00014D50 File Offset: 0x00013150
		public Transform StartTransform
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00014D58 File Offset: 0x00013158
		public Transform EndTransform
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00014D60 File Offset: 0x00013160
		public GraphNode StartNode
		{
			get
			{
				return this.startNode;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00014D68 File Offset: 0x00013168
		public GraphNode EndNode
		{
			get
			{
				return this.endNode;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00014D70 File Offset: 0x00013170
		public override void OnPostScan()
		{
			if (AstarPath.active.isScanning)
			{
				this.InternalOnPostScan();
			}
			else
			{
				AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(bool force)
				{
					this.InternalOnPostScan();
					return true;
				}));
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00014DA8 File Offset: 0x000131A8
		public void InternalOnPostScan()
		{
			if (AstarPath.active.data.pointGraph == null)
			{
				AstarPath.active.data.AddGraph(typeof(PointGraph));
			}
			this.startNode = AstarPath.active.data.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.StartTransform.position);
			this.startNode.link = this;
			this.endNode = AstarPath.active.data.pointGraph.AddNode<NodeLink3Node>(new NodeLink3Node(AstarPath.active), (Int3)this.EndTransform.position);
			this.endNode.link = this;
			this.connectedNode1 = null;
			this.connectedNode2 = null;
			if (this.startNode == null || this.endNode == null)
			{
				this.startNode = null;
				this.endNode = null;
				return;
			}
			this.postScanCalled = true;
			NodeLink3.reference[this.startNode] = this;
			NodeLink3.reference[this.endNode] = this;
			this.Apply(true);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014EC8 File Offset: 0x000132C8
		public override void OnGraphsPostUpdate()
		{
			if (!AstarPath.active.isScanning)
			{
				if (this.connectedNode1 != null && this.connectedNode1.Destroyed)
				{
					this.connectedNode1 = null;
				}
				if (this.connectedNode2 != null && this.connectedNode2.Destroyed)
				{
					this.connectedNode2 = null;
				}
				if (!this.postScanCalled)
				{
					this.OnPostScan();
				}
				else
				{
					this.Apply(false);
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00014F48 File Offset: 0x00013348
		protected override void OnEnable()
		{
			base.OnEnable();
			if (Application.isPlaying && AstarPath.active != null && AstarPath.active.data != null && AstarPath.active.data.pointGraph != null)
			{
				this.OnGraphsPostUpdate();
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00014FA0 File Offset: 0x000133A0
		protected override void OnDisable()
		{
			base.OnDisable();
			this.postScanCalled = false;
			if (this.startNode != null)
			{
				NodeLink3.reference.Remove(this.startNode);
			}
			if (this.endNode != null)
			{
				NodeLink3.reference.Remove(this.endNode);
			}
			if (this.startNode != null && this.endNode != null)
			{
				this.startNode.RemoveConnection(this.endNode);
				this.endNode.RemoveConnection(this.startNode);
				if (this.connectedNode1 != null && this.connectedNode2 != null)
				{
					this.startNode.RemoveConnection(this.connectedNode1);
					this.connectedNode1.RemoveConnection(this.startNode);
					this.endNode.RemoveConnection(this.connectedNode2);
					this.connectedNode2.RemoveConnection(this.endNode);
				}
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00015084 File Offset: 0x00013484
		private void RemoveConnections(GraphNode node)
		{
			node.ClearConnections(true);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001508D File Offset: 0x0001348D
		[ContextMenu("Recalculate neighbours")]
		private void ContextApplyForce()
		{
			if (Application.isPlaying)
			{
				this.Apply(true);
				if (AstarPath.active != null)
				{
					AstarPath.active.FloodFill();
				}
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000150BC File Offset: 0x000134BC
		public void Apply(bool forceNewCheck)
		{
			NNConstraint none = NNConstraint.None;
			none.distanceXZ = true;
			int graphIndex = (int)this.startNode.GraphIndex;
			none.graphMask = ~(1 << graphIndex);
			bool flag = true;
			NNInfo nearest = AstarPath.active.GetNearest(this.StartTransform.position, none);
			flag &= (nearest.node == this.connectedNode1 && nearest.node != null);
			this.connectedNode1 = (nearest.node as MeshNode);
			this.clamped1 = nearest.position;
			if (this.connectedNode1 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode1.position, Vector3.up * 5f, Color.red);
			}
			NNInfo nearest2 = AstarPath.active.GetNearest(this.EndTransform.position, none);
			flag &= (nearest2.node == this.connectedNode2 && nearest2.node != null);
			this.connectedNode2 = (nearest2.node as MeshNode);
			this.clamped2 = nearest2.position;
			if (this.connectedNode2 != null)
			{
				Debug.DrawRay((Vector3)this.connectedNode2.position, Vector3.up * 5f, Color.cyan);
			}
			if (this.connectedNode2 == null || this.connectedNode1 == null)
			{
				return;
			}
			this.startNode.SetPosition((Int3)this.StartTransform.position);
			this.endNode.SetPosition((Int3)this.EndTransform.position);
			if (flag && !forceNewCheck)
			{
				return;
			}
			this.RemoveConnections(this.startNode);
			this.RemoveConnections(this.endNode);
			uint cost = (uint)Mathf.RoundToInt((float)((Int3)(this.StartTransform.position - this.EndTransform.position)).costMagnitude * this.costFactor);
			this.startNode.AddConnection(this.endNode, cost);
			this.endNode.AddConnection(this.startNode, cost);
			Int3 rhs = this.connectedNode2.position - this.connectedNode1.position;
			for (int i = 0; i < this.connectedNode1.GetVertexCount(); i++)
			{
				Int3 vertex = this.connectedNode1.GetVertex(i);
				Int3 vertex2 = this.connectedNode1.GetVertex((i + 1) % this.connectedNode1.GetVertexCount());
				if (Int3.DotLong((vertex2 - vertex).Normal2D(), rhs) <= 0L)
				{
					for (int j = 0; j < this.connectedNode2.GetVertexCount(); j++)
					{
						Int3 vertex3 = this.connectedNode2.GetVertex(j);
						Int3 vertex4 = this.connectedNode2.GetVertex((j + 1) % this.connectedNode2.GetVertexCount());
						if (Int3.DotLong((vertex4 - vertex3).Normal2D(), rhs) >= 0L)
						{
							if ((double)Int3.Angle(vertex4 - vertex3, vertex2 - vertex) > 2.9670598109563189)
							{
								float num = 0f;
								float num2 = 1f;
								num2 = Math.Min(num2, VectorMath.ClosestPointOnLineFactor(vertex, vertex2, vertex3));
								num = Math.Max(num, VectorMath.ClosestPointOnLineFactor(vertex, vertex2, vertex4));
								if (num2 >= num)
								{
									Vector3 vector = (Vector3)(vertex2 - vertex) * num + (Vector3)vertex;
									Vector3 vector2 = (Vector3)(vertex2 - vertex) * num2 + (Vector3)vertex;
									this.startNode.portalA = vector;
									this.startNode.portalB = vector2;
									this.endNode.portalA = vector2;
									this.endNode.portalB = vector;
									this.connectedNode1.AddConnection(this.startNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
									this.connectedNode2.AddConnection(this.endNode, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
									this.startNode.AddConnection(this.connectedNode1, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped1 - this.StartTransform.position)).costMagnitude * this.costFactor));
									this.endNode.AddConnection(this.connectedNode2, (uint)Mathf.RoundToInt((float)((Int3)(this.clamped2 - this.EndTransform.position)).costMagnitude * this.costFactor));
									return;
								}
								Debug.LogError(string.Concat(new object[]
								{
									"Something went wrong! ",
									num,
									" ",
									num2,
									" ",
									vertex,
									" ",
									vertex2,
									" ",
									vertex3,
									" ",
									vertex4,
									"\nTODO, how can this happen?"
								}));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00015655 File Offset: 0x00013A55
		public virtual void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001565E File Offset: 0x00013A5E
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00015668 File Offset: 0x00013A68
		public void OnDrawGizmos(bool selected)
		{
			Color color = (!selected) ? NodeLink3.GizmosColor : NodeLink3.GizmosColorSelected;
			if (this.StartTransform != null)
			{
				Draw.Gizmos.CircleXZ(this.StartTransform.position, 0.4f, color, 0f, 6.28318548f);
			}
			if (this.EndTransform != null)
			{
				Draw.Gizmos.CircleXZ(this.EndTransform.position, 0.4f, color, 0f, 6.28318548f);
			}
			if (this.StartTransform != null && this.EndTransform != null)
			{
				Draw.Gizmos.Bezier(this.StartTransform.position, this.EndTransform.position, color);
				if (selected)
				{
					Vector3 normalized = Vector3.Cross(Vector3.up, this.EndTransform.position - this.StartTransform.position).normalized;
					Draw.Gizmos.Bezier(this.StartTransform.position + normalized * 0.1f, this.EndTransform.position + normalized * 0.1f, color);
					Draw.Gizmos.Bezier(this.StartTransform.position - normalized * 0.1f, this.EndTransform.position - normalized * 0.1f, color);
				}
			}
		}

		// Token: 0x0400020D RID: 525
		protected static Dictionary<GraphNode, NodeLink3> reference = new Dictionary<GraphNode, NodeLink3>();

		// Token: 0x0400020E RID: 526
		public Transform end;

		// Token: 0x0400020F RID: 527
		public float costFactor = 1f;

		// Token: 0x04000210 RID: 528
		public bool oneWay;

		// Token: 0x04000211 RID: 529
		private NodeLink3Node startNode;

		// Token: 0x04000212 RID: 530
		private NodeLink3Node endNode;

		// Token: 0x04000213 RID: 531
		private MeshNode connectedNode1;

		// Token: 0x04000214 RID: 532
		private MeshNode connectedNode2;

		// Token: 0x04000215 RID: 533
		private Vector3 clamped1;

		// Token: 0x04000216 RID: 534
		private Vector3 clamped2;

		// Token: 0x04000217 RID: 535
		private bool postScanCalled;

		// Token: 0x04000218 RID: 536
		private static readonly Color GizmosColor = new Color(0.807843149f, 0.533333361f, 0.1882353f, 0.5f);

		// Token: 0x04000219 RID: 537
		private static readonly Color GizmosColorSelected = new Color(0.921568632f, 0.482352942f, 0.1254902f, 1f);
	}
}
