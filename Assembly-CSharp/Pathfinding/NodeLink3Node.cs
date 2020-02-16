using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200004A RID: 74
	public class NodeLink3Node : PointNode
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00014BB6 File Offset: 0x00012FB6
		public NodeLink3Node(AstarPath active) : base(active)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00014BC0 File Offset: 0x00012FC0
		public override bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			if (this.connections.Length < 2)
			{
				return false;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			if (left != null)
			{
				left.Add(this.portalA);
				right.Add(this.portalB);
			}
			return true;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00014C28 File Offset: 0x00013028
		public GraphNode GetOther(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			if (this.connections.Length != 2)
			{
				throw new Exception("Invalid NodeLink3Node. Expected 2 connections, found " + this.connections.Length);
			}
			return (a != this.connections[0].node) ? (this.connections[0].node as NodeLink3Node).GetOtherInternal(this) : (this.connections[1].node as NodeLink3Node).GetOtherInternal(this);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00014CC4 File Offset: 0x000130C4
		private GraphNode GetOtherInternal(GraphNode a)
		{
			if (this.connections.Length < 2)
			{
				return null;
			}
			return (a != this.connections[0].node) ? this.connections[0].node : this.connections[1].node;
		}

		// Token: 0x0400020A RID: 522
		public NodeLink3 link;

		// Token: 0x0400020B RID: 523
		public Vector3 portalA;

		// Token: 0x0400020C RID: 524
		public Vector3 portalB;
	}
}
