using System;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000071 RID: 113
	public class RVOQuadtree
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001B9B8 File Offset: 0x00019DB8
		public void Clear()
		{
			this.nodes[0] = default(RVOQuadtree.Node);
			this.filledNodes = 1;
			this.maxRadius = 0f;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001B9F1 File Offset: 0x00019DF1
		public void SetBounds(Rect r)
		{
			this.bounds = r;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001B9FC File Offset: 0x00019DFC
		private int GetNodeIndex()
		{
			if (this.filledNodes == this.nodes.Length)
			{
				RVOQuadtree.Node[] array = new RVOQuadtree.Node[this.nodes.Length * 2];
				for (int i = 0; i < this.nodes.Length; i++)
				{
					array[i] = this.nodes[i];
				}
				this.nodes = array;
			}
			this.nodes[this.filledNodes] = default(RVOQuadtree.Node);
			this.nodes[this.filledNodes].child00 = this.filledNodes;
			this.filledNodes++;
			return this.filledNodes - 1;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001BABC File Offset: 0x00019EBC
		public void Insert(Agent agent)
		{
			int num = 0;
			Rect r = this.bounds;
			Vector2 vector = new Vector2(agent.position.x, agent.position.y);
			agent.next = null;
			this.maxRadius = Math.Max(agent.radius, this.maxRadius);
			int num2 = 0;
			for (;;)
			{
				num2++;
				if (this.nodes[num].child00 == num)
				{
					if (this.nodes[num].count < 15 || num2 > 10)
					{
						break;
					}
					RVOQuadtree.Node node = this.nodes[num];
					node.child00 = this.GetNodeIndex();
					node.child01 = this.GetNodeIndex();
					node.child10 = this.GetNodeIndex();
					node.child11 = this.GetNodeIndex();
					this.nodes[num] = node;
					this.nodes[num].Distribute(this.nodes, r);
				}
				if (this.nodes[num].child00 != num)
				{
					Vector2 center = r.center;
					if (vector.x > center.x)
					{
						if (vector.y > center.y)
						{
							num = this.nodes[num].child11;
							r = Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax);
						}
						else
						{
							num = this.nodes[num].child10;
							r = Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y);
						}
					}
					else if (vector.y > center.y)
					{
						num = this.nodes[num].child01;
						r = Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax);
					}
					else
					{
						num = this.nodes[num].child00;
						r = Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y);
					}
				}
			}
			this.nodes[num].Add(agent);
			RVOQuadtree.Node[] array = this.nodes;
			int num3 = num;
			array[num3].count = array[num3].count + 1;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001BD2A File Offset: 0x0001A12A
		public void CalculateSpeeds()
		{
			this.nodes[0].CalculateMaxSpeed(this.nodes, 0);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001BD48 File Offset: 0x0001A148
		public void Query(Vector2 p, float speed, float timeHorizon, float agentRadius, Agent agent)
		{
			RVOQuadtree.QuadtreeQuery quadtreeQuery = default(RVOQuadtree.QuadtreeQuery);
			quadtreeQuery.p = p;
			quadtreeQuery.speed = speed;
			quadtreeQuery.timeHorizon = timeHorizon;
			quadtreeQuery.maxRadius = float.PositiveInfinity;
			quadtreeQuery.agentRadius = agentRadius;
			quadtreeQuery.agent = agent;
			quadtreeQuery.nodes = this.nodes;
			quadtreeQuery.QueryRec(0, this.bounds);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001BDAE File Offset: 0x0001A1AE
		public void DebugDraw()
		{
			this.DebugDrawRec(0, this.bounds);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001BDC0 File Offset: 0x0001A1C0
		private void DebugDrawRec(int i, Rect r)
		{
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMin), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMin), new Vector3(r.xMax, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMax, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMax), Color.white);
			Debug.DrawLine(new Vector3(r.xMin, 0f, r.yMax), new Vector3(r.xMin, 0f, r.yMin), Color.white);
			if (this.nodes[i].child00 != i)
			{
				Vector2 center = r.center;
				this.DebugDrawRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
				this.DebugDrawRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
				this.DebugDrawRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
				this.DebugDrawRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
			}
			for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
			{
				Vector2 position = this.nodes[i].linkedList.position;
				Debug.DrawLine(new Vector3(position.x, 0f, position.y) + Vector3.up, new Vector3(agent.position.x, 0f, agent.position.y) + Vector3.up, new Color(1f, 1f, 0f, 0.5f));
			}
		}

		// Token: 0x04000315 RID: 789
		private const int LeafSize = 15;

		// Token: 0x04000316 RID: 790
		private float maxRadius;

		// Token: 0x04000317 RID: 791
		private RVOQuadtree.Node[] nodes = new RVOQuadtree.Node[42];

		// Token: 0x04000318 RID: 792
		private int filledNodes = 1;

		// Token: 0x04000319 RID: 793
		private Rect bounds;

		// Token: 0x02000072 RID: 114
		private struct Node
		{
			// Token: 0x060004FD RID: 1277 RVA: 0x0001C059 File Offset: 0x0001A459
			public void Add(Agent agent)
			{
				agent.next = this.linkedList;
				this.linkedList = agent;
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x0001C070 File Offset: 0x0001A470
			public void Distribute(RVOQuadtree.Node[] nodes, Rect r)
			{
				Vector2 center = r.center;
				while (this.linkedList != null)
				{
					Agent next = this.linkedList.next;
					if (this.linkedList.position.x > center.x)
					{
						if (this.linkedList.position.y > center.y)
						{
							nodes[this.child11].Add(this.linkedList);
						}
						else
						{
							nodes[this.child10].Add(this.linkedList);
						}
					}
					else if (this.linkedList.position.y > center.y)
					{
						nodes[this.child01].Add(this.linkedList);
					}
					else
					{
						nodes[this.child00].Add(this.linkedList);
					}
					this.linkedList = next;
				}
				this.count = 0;
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x0001C170 File Offset: 0x0001A570
			public float CalculateMaxSpeed(RVOQuadtree.Node[] nodes, int index)
			{
				if (this.child00 == index)
				{
					for (Agent next = this.linkedList; next != null; next = next.next)
					{
						this.maxSpeed = Math.Max(this.maxSpeed, next.CalculatedSpeed);
					}
				}
				else
				{
					this.maxSpeed = Math.Max(nodes[this.child00].CalculateMaxSpeed(nodes, this.child00), nodes[this.child01].CalculateMaxSpeed(nodes, this.child01));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child10].CalculateMaxSpeed(nodes, this.child10));
					this.maxSpeed = Math.Max(this.maxSpeed, nodes[this.child11].CalculateMaxSpeed(nodes, this.child11));
				}
				return this.maxSpeed;
			}

			// Token: 0x0400031A RID: 794
			public int child00;

			// Token: 0x0400031B RID: 795
			public int child01;

			// Token: 0x0400031C RID: 796
			public int child10;

			// Token: 0x0400031D RID: 797
			public int child11;

			// Token: 0x0400031E RID: 798
			public Agent linkedList;

			// Token: 0x0400031F RID: 799
			public byte count;

			// Token: 0x04000320 RID: 800
			public float maxSpeed;
		}

		// Token: 0x02000073 RID: 115
		private struct QuadtreeQuery
		{
			// Token: 0x06000500 RID: 1280 RVA: 0x0001C254 File Offset: 0x0001A654
			public void QueryRec(int i, Rect r)
			{
				float num = Math.Min(Math.Max((this.nodes[i].maxSpeed + this.speed) * this.timeHorizon, this.agentRadius) + this.agentRadius, this.maxRadius);
				if (this.nodes[i].child00 == i)
				{
					for (Agent agent = this.nodes[i].linkedList; agent != null; agent = agent.next)
					{
						float num2 = this.agent.InsertAgentNeighbour(agent, num * num);
						if (num2 < this.maxRadius * this.maxRadius)
						{
							this.maxRadius = Mathf.Sqrt(num2);
						}
					}
				}
				else
				{
					Vector2 center = r.center;
					if (this.p.x - num < center.x)
					{
						if (this.p.y - num < center.y)
						{
							this.QueryRec(this.nodes[i].child00, Rect.MinMaxRect(r.xMin, r.yMin, center.x, center.y));
							num = Math.Min(num, this.maxRadius);
						}
						if (this.p.y + num > center.y)
						{
							this.QueryRec(this.nodes[i].child01, Rect.MinMaxRect(r.xMin, center.y, center.x, r.yMax));
							num = Math.Min(num, this.maxRadius);
						}
					}
					if (this.p.x + num > center.x)
					{
						if (this.p.y - num < center.y)
						{
							this.QueryRec(this.nodes[i].child10, Rect.MinMaxRect(center.x, r.yMin, r.xMax, center.y));
							num = Math.Min(num, this.maxRadius);
						}
						if (this.p.y + num > center.y)
						{
							this.QueryRec(this.nodes[i].child11, Rect.MinMaxRect(center.x, center.y, r.xMax, r.yMax));
						}
					}
				}
			}

			// Token: 0x04000321 RID: 801
			public Vector2 p;

			// Token: 0x04000322 RID: 802
			public float speed;

			// Token: 0x04000323 RID: 803
			public float timeHorizon;

			// Token: 0x04000324 RID: 804
			public float agentRadius;

			// Token: 0x04000325 RID: 805
			public float maxRadius;

			// Token: 0x04000326 RID: 806
			public Agent agent;

			// Token: 0x04000327 RID: 807
			public RVOQuadtree.Node[] nodes;
		}
	}
}
