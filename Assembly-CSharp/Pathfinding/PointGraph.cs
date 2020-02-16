using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B1 RID: 177
	[JsonOptIn]
	public class PointGraph : NavGraph, IUpdatableGraph
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0003199C File Offset: 0x0002FD9C
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x000319A4 File Offset: 0x0002FDA4
		public int nodeCount { get; protected set; }

		// Token: 0x06000754 RID: 1876 RVA: 0x000319AD File Offset: 0x0002FDAD
		public override int CountNodes()
		{
			return this.nodeCount;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000319B8 File Offset: 0x0002FDB8
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			int nodeCount = this.nodeCount;
			for (int i = 0; i < nodeCount; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000319F8 File Offset: 0x0002FDF8
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			return this.GetNearestInternal(position, constraint, true);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00031A03 File Offset: 0x0002FE03
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearestInternal(position, constraint, false);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00031A10 File Offset: 0x0002FE10
		private NNInfoInternal GetNearestInternal(Vector3 position, NNConstraint constraint, bool fastCheck)
		{
			if (this.nodes == null)
			{
				return default(NNInfoInternal);
			}
			if (this.optimizeForSparseGraph)
			{
				return new NNInfoInternal(this.lookupTree.GetNearest((Int3)position, (!fastCheck) ? constraint : null));
			}
			float num = (constraint != null && !constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistanceSqr;
			NNInfoInternal result = new NNInfoInternal(null);
			float num2 = float.PositiveInfinity;
			float num3 = float.PositiveInfinity;
			for (int i = 0; i < this.nodeCount; i++)
			{
				PointNode pointNode = this.nodes[i];
				float sqrMagnitude = (position - (Vector3)pointNode.position).sqrMagnitude;
				if (sqrMagnitude < num2)
				{
					num2 = sqrMagnitude;
					result.node = pointNode;
				}
				if (sqrMagnitude < num3 && sqrMagnitude < num && (constraint == null || constraint.Suitable(pointNode)))
				{
					num3 = sqrMagnitude;
					result.constrainedNode = pointNode;
				}
			}
			if (!fastCheck)
			{
				result.node = result.constrainedNode;
			}
			result.UpdateInfo();
			return result;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00031B40 File Offset: 0x0002FF40
		public PointNode AddNode(Int3 position)
		{
			return this.AddNode<PointNode>(new PointNode(this.active), position);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00031B54 File Offset: 0x0002FF54
		public T AddNode<T>(T node, Int3 position) where T : PointNode
		{
			if (this.nodes == null || this.nodeCount == this.nodes.Length)
			{
				PointNode[] array = new PointNode[(this.nodes == null) ? 4 : Math.Max(this.nodes.Length + 4, this.nodes.Length * 2)];
				if (this.nodes != null)
				{
					this.nodes.CopyTo(array, 0);
				}
				this.nodes = array;
			}
			node.SetPosition(position);
			node.GraphIndex = this.graphIndex;
			node.Walkable = true;
			this.nodes[this.nodeCount] = node;
			this.nodeCount++;
			if (this.optimizeForSparseGraph)
			{
				this.AddToLookup(node);
			}
			return node;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00031C38 File Offset: 0x00030038
		protected static int CountChildren(Transform tr)
		{
			int num = 0;
			IEnumerator enumerator = tr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform tr2 = (Transform)obj;
					num++;
					num += PointGraph.CountChildren(tr2);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			return num;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00031CA4 File Offset: 0x000300A4
		protected void AddChildren(ref int c, Transform tr)
		{
			IEnumerator enumerator = tr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					this.nodes[c].position = (Int3)transform.position;
					this.nodes[c].Walkable = true;
					this.nodes[c].gameObject = transform.gameObject;
					c++;
					this.AddChildren(ref c, transform);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00031D48 File Offset: 0x00030148
		public void RebuildNodeLookup()
		{
			if (!this.optimizeForSparseGraph || this.nodes == null)
			{
				this.lookupTree = new PointKDTree();
			}
			else
			{
				this.lookupTree.Rebuild(this.nodes, 0, this.nodeCount);
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00031D88 File Offset: 0x00030188
		private void AddToLookup(PointNode node)
		{
			this.lookupTree.Add(node);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00031D98 File Offset: 0x00030198
		protected virtual PointNode[] CreateNodes(int count)
		{
			PointNode[] array = new PointNode[count];
			for (int i = 0; i < this.nodeCount; i++)
			{
				array[i] = new PointNode(this.active);
			}
			return array;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00031DD4 File Offset: 0x000301D4
		protected override IEnumerable<Progress> ScanInternal()
		{
			yield return new Progress(0f, "Searching for GameObjects");
			if (this.root == null)
			{
				GameObject[] gos = (this.searchTag == null) ? null : GameObject.FindGameObjectsWithTag(this.searchTag);
				if (gos == null)
				{
					this.nodes = new PointNode[0];
					this.nodeCount = 0;
					yield break;
				}
				yield return new Progress(0.1f, "Creating nodes");
				this.nodeCount = gos.Length;
				this.nodes = this.CreateNodes(this.nodeCount);
				for (int i = 0; i < gos.Length; i++)
				{
					this.nodes[i].position = (Int3)gos[i].transform.position;
					this.nodes[i].Walkable = true;
					this.nodes[i].gameObject = gos[i].gameObject;
				}
			}
			else if (!this.recursive)
			{
				this.nodeCount = this.root.childCount;
				this.nodes = this.CreateNodes(this.nodeCount);
				int num = 0;
				IEnumerator enumerator = this.root.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						this.nodes[num].position = (Int3)transform.position;
						this.nodes[num].Walkable = true;
						this.nodes[num].gameObject = transform.gameObject;
						num++;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			else
			{
				this.nodeCount = PointGraph.CountChildren(this.root);
				this.nodes = this.CreateNodes(this.nodeCount);
				int num2 = 0;
				this.AddChildren(ref num2, this.root);
			}
			if (this.optimizeForSparseGraph)
			{
				yield return new Progress(0.15f, "Building node lookup");
				this.RebuildNodeLookup();
			}
			foreach (Progress progress in this.ConnectNodesAsync())
			{
				yield return progress.MapTo(0.16f, 1f, null);
			}
			yield break;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00031DF8 File Offset: 0x000301F8
		public void ConnectNodes()
		{
			foreach (Progress progress in this.ConnectNodesAsync())
			{
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00031E4C File Offset: 0x0003024C
		private IEnumerable<Progress> ConnectNodesAsync()
		{
			if (this.maxDistance >= 0f)
			{
				List<Connection> connections = new List<Connection>();
				List<GraphNode> candidateConnections = new List<GraphNode>();
				long maxSquaredRange;
				if (this.maxDistance == 0f && (this.limits.x == 0f || this.limits.y == 0f || this.limits.z == 0f))
				{
					maxSquaredRange = long.MaxValue;
				}
				else
				{
					maxSquaredRange = (long)(Mathf.Max(this.limits.x, Mathf.Max(this.limits.y, Mathf.Max(this.limits.z, this.maxDistance))) * 1000f) + 1L;
					maxSquaredRange *= maxSquaredRange;
				}
				for (int i = 0; i < this.nodeCount; i++)
				{
					if (i % 512 == 0)
					{
						yield return new Progress((float)i / (float)this.nodes.Length, "Connecting nodes");
					}
					connections.Clear();
					PointNode node = this.nodes[i];
					if (this.optimizeForSparseGraph)
					{
						candidateConnections.Clear();
						this.lookupTree.GetInRange(node.position, maxSquaredRange, candidateConnections);
						for (int j = 0; j < candidateConnections.Count; j++)
						{
							PointNode pointNode = candidateConnections[j] as PointNode;
							float num;
							if (pointNode != node && this.IsValidConnection(node, pointNode, out num))
							{
								connections.Add(new Connection(pointNode, (uint)Mathf.RoundToInt(num * 1000f), byte.MaxValue));
							}
						}
					}
					else
					{
						for (int k = 0; k < this.nodeCount; k++)
						{
							if (i != k)
							{
								PointNode pointNode2 = this.nodes[k];
								float num2;
								if (this.IsValidConnection(node, pointNode2, out num2))
								{
									connections.Add(new Connection(pointNode2, (uint)Mathf.RoundToInt(num2 * 1000f), byte.MaxValue));
								}
							}
						}
					}
					node.connections = connections.ToArray();
				}
			}
			yield break;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00031E70 File Offset: 0x00030270
		public virtual bool IsValidConnection(GraphNode a, GraphNode b, out float dist)
		{
			dist = 0f;
			if (!a.Walkable || !b.Walkable)
			{
				return false;
			}
			Vector3 vector = (Vector3)(b.position - a.position);
			if ((!Mathf.Approximately(this.limits.x, 0f) && Mathf.Abs(vector.x) > this.limits.x) || (!Mathf.Approximately(this.limits.y, 0f) && Mathf.Abs(vector.y) > this.limits.y) || (!Mathf.Approximately(this.limits.z, 0f) && Mathf.Abs(vector.z) > this.limits.z))
			{
				return false;
			}
			dist = vector.magnitude;
			if (this.maxDistance != 0f && dist >= this.maxDistance)
			{
				return false;
			}
			if (!this.raycast)
			{
				return true;
			}
			Ray ray = new Ray((Vector3)a.position, vector);
			Ray ray2 = new Ray((Vector3)b.position, -vector);
			if (this.use2DPhysics)
			{
				if (this.thickRaycast)
				{
					return !Physics2D.CircleCast(ray.origin, this.thickRaycastRadius, ray.direction, dist, this.mask) && !Physics2D.CircleCast(ray2.origin, this.thickRaycastRadius, ray2.direction, dist, this.mask);
				}
				return !Physics2D.Linecast((Vector3)a.position, (Vector3)b.position, this.mask) && !Physics2D.Linecast((Vector3)b.position, (Vector3)a.position, this.mask);
			}
			else
			{
				if (this.thickRaycast)
				{
					return !Physics.SphereCast(ray, this.thickRaycastRadius, dist, this.mask) && !Physics.SphereCast(ray2, this.thickRaycastRadius, dist, this.mask);
				}
				return !Physics.Linecast((Vector3)a.position, (Vector3)b.position, this.mask) && !Physics.Linecast((Vector3)b.position, (Vector3)a.position, this.mask);
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00032159 File Offset: 0x00030559
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0003215C File Offset: 0x0003055C
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0003215E File Offset: 0x0003055E
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00032160 File Offset: 0x00030560
		void IUpdatableGraph.UpdateArea(GraphUpdateObject guo)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodeCount; i++)
			{
				PointNode pointNode = this.nodes[i];
				if (guo.bounds.Contains((Vector3)pointNode.position))
				{
					guo.WillUpdateNode(pointNode);
					guo.Apply(pointNode);
				}
			}
			if (guo.updatePhysics)
			{
				Bounds bounds = guo.bounds;
				if (this.thickRaycast)
				{
					bounds.Expand(this.thickRaycastRadius * 2f);
				}
				List<Connection> list = ListPool<Connection>.Claim();
				for (int j = 0; j < this.nodeCount; j++)
				{
					PointNode pointNode2 = this.nodes[j];
					Vector3 a = (Vector3)pointNode2.position;
					List<Connection> list2 = null;
					for (int k = 0; k < this.nodeCount; k++)
					{
						if (k != j)
						{
							Vector3 b = (Vector3)this.nodes[k].position;
							if (VectorMath.SegmentIntersectsBounds(bounds, a, b))
							{
								PointNode pointNode3 = this.nodes[k];
								bool flag = pointNode2.ContainsConnection(pointNode3);
								float num;
								bool flag2 = this.IsValidConnection(pointNode2, pointNode3, out num);
								if (list2 == null && flag != flag2)
								{
									list.Clear();
									list2 = list;
									list2.AddRange(pointNode2.connections);
								}
								if (!flag && flag2)
								{
									uint cost = (uint)Mathf.RoundToInt(num * 1000f);
									list2.Add(new Connection(pointNode3, cost, byte.MaxValue));
								}
								else if (flag && !flag2)
								{
									for (int l = 0; l < list2.Count; l++)
									{
										if (list2[l].node == pointNode3)
										{
											list2.RemoveAt(l);
											break;
										}
									}
								}
							}
						}
					}
					if (list2 != null)
					{
						pointNode2.connections = list2.ToArray();
					}
				}
				ListPool<Connection>.Release(ref list);
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0003236B File Offset: 0x0003076B
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.RebuildNodeLookup();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00032373 File Offset: 0x00030773
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			base.RelocateNodes(deltaMatrix);
			this.RebuildNodeLookup();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00032384 File Offset: 0x00030784
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.root = (ctx.DeserializeUnityObject() as Transform);
			this.searchTag = ctx.reader.ReadString();
			this.maxDistance = ctx.reader.ReadSingle();
			this.limits = ctx.DeserializeVector3();
			this.raycast = ctx.reader.ReadBoolean();
			this.use2DPhysics = ctx.reader.ReadBoolean();
			this.thickRaycast = ctx.reader.ReadBoolean();
			this.thickRaycastRadius = ctx.reader.ReadSingle();
			this.recursive = ctx.reader.ReadBoolean();
			ctx.reader.ReadBoolean();
			this.mask = ctx.reader.ReadInt32();
			this.optimizeForSparseGraph = ctx.reader.ReadBoolean();
			ctx.reader.ReadBoolean();
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0003246C File Offset: 0x0003086C
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
			}
			ctx.writer.Write(this.nodeCount);
			for (int i = 0; i < this.nodeCount; i++)
			{
				if (this.nodes[i] == null)
				{
					ctx.writer.Write(-1);
				}
				else
				{
					ctx.writer.Write(0);
					this.nodes[i].SerializeNode(ctx);
				}
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000324F0 File Offset: 0x000308F0
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new PointNode[num];
			this.nodeCount = num;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (ctx.reader.ReadInt32() != -1)
				{
					this.nodes[i] = new PointNode(this.active);
					this.nodes[i].DeserializeNode(ctx);
				}
			}
		}

		// Token: 0x040004AF RID: 1199
		[JsonMember]
		public Transform root;

		// Token: 0x040004B0 RID: 1200
		[JsonMember]
		public string searchTag;

		// Token: 0x040004B1 RID: 1201
		[JsonMember]
		public float maxDistance;

		// Token: 0x040004B2 RID: 1202
		[JsonMember]
		public Vector3 limits;

		// Token: 0x040004B3 RID: 1203
		[JsonMember]
		public bool raycast = true;

		// Token: 0x040004B4 RID: 1204
		[JsonMember]
		public bool use2DPhysics;

		// Token: 0x040004B5 RID: 1205
		[JsonMember]
		public bool thickRaycast;

		// Token: 0x040004B6 RID: 1206
		[JsonMember]
		public float thickRaycastRadius = 1f;

		// Token: 0x040004B7 RID: 1207
		[JsonMember]
		public bool recursive = true;

		// Token: 0x040004B8 RID: 1208
		[JsonMember]
		public LayerMask mask;

		// Token: 0x040004B9 RID: 1209
		[JsonMember]
		public bool optimizeForSparseGraph;

		// Token: 0x040004BA RID: 1210
		private PointKDTree lookupTree = new PointKDTree();

		// Token: 0x040004BB RID: 1211
		public PointNode[] nodes;
	}
}
