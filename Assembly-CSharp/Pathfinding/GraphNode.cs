using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005E RID: 94
	public abstract class GraphNode
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x000141D4 File Offset: 0x000125D4
		protected GraphNode(AstarPath astar)
		{
			if (!object.ReferenceEquals(astar, null))
			{
				this.nodeIndex = astar.GetNewNodeIndex();
				astar.InitializeNode(this);
				return;
			}
			throw new Exception("No active AstarPath object to bind to");
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001420B File Offset: 0x0001260B
		public NavGraph Graph
		{
			get
			{
				return (!this.Destroyed) ? AstarData.GetGraph(this) : null;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00014224 File Offset: 0x00012624
		internal void Destroy()
		{
			if (this.Destroyed)
			{
				return;
			}
			this.ClearConnections(true);
			if (AstarPath.active != null)
			{
				AstarPath.active.DestroyNode(this);
			}
			this.NodeIndex = 268435454;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001425F File Offset: 0x0001265F
		public bool Destroyed
		{
			get
			{
				return this.NodeIndex == 268435454;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0001426E File Offset: 0x0001266E
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0001427C File Offset: 0x0001267C
		public int NodeIndex
		{
			get
			{
				return this.nodeIndex & 268435455;
			}
			private set
			{
				this.nodeIndex = ((this.nodeIndex & -268435456) | value);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00014292 File Offset: 0x00012692
		// (set) Token: 0x060003DE RID: 990 RVA: 0x000142A6 File Offset: 0x000126A6
		internal bool TemporaryFlag1
		{
			get
			{
				return (this.nodeIndex & 268435456) != 0;
			}
			set
			{
				this.nodeIndex = ((this.nodeIndex & -268435457) | ((!value) ? 0 : 268435456));
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003DF RID: 991 RVA: 0x000142CC File Offset: 0x000126CC
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x000142E0 File Offset: 0x000126E0
		internal bool TemporaryFlag2
		{
			get
			{
				return (this.nodeIndex & 536870912) != 0;
			}
			set
			{
				this.nodeIndex = ((this.nodeIndex & -536870913) | ((!value) ? 0 : 536870912));
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00014306 File Offset: 0x00012706
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001430E File Offset: 0x0001270E
		public uint Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00014317 File Offset: 0x00012717
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0001431F File Offset: 0x0001271F
		public uint Penalty
		{
			get
			{
				return this.penalty;
			}
			set
			{
				if (value > 16777215u)
				{
					Debug.LogWarning("Very high penalty applied. Are you sure negative values haven't underflowed?\nPenalty values this high could with long paths cause overflows and in some cases infinity loops because of that.\nPenalty value applied: " + value);
				}
				this.penalty = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00014348 File Offset: 0x00012748
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00014358 File Offset: 0x00012758
		public bool Walkable
		{
			get
			{
				return (this.flags & 1u) != 0u;
			}
			set
			{
				this.flags = ((this.flags & 4294967294u) | ((!value) ? 0u : 1u) << 0);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00014379 File Offset: 0x00012779
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00014389 File Offset: 0x00012789
		public uint Area
		{
			get
			{
				return (this.flags & 262142u) >> 1;
			}
			set
			{
				this.flags = ((this.flags & 4294705153u) | value << 1);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000143A1 File Offset: 0x000127A1
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000143B2 File Offset: 0x000127B2
		public uint GraphIndex
		{
			get
			{
				return (this.flags & 4278190080u) >> 24;
			}
			set
			{
				this.flags = ((this.flags & 16777215u) | value << 24);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000143CB File Offset: 0x000127CB
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x000143DC File Offset: 0x000127DC
		public uint Tag
		{
			get
			{
				return (this.flags & 16252928u) >> 19;
			}
			set
			{
				this.flags = ((this.flags & 4278714367u) | value << 19);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000143F8 File Offset: 0x000127F8
		public virtual void UpdateRecursiveG(Path path, PathNode pathNode, PathHandler handler)
		{
			pathNode.UpdateG(path);
			handler.heap.Add(pathNode);
			this.GetConnections(delegate(GraphNode other)
			{
				PathNode pathNode2 = handler.GetPathNode(other);
				if (pathNode2.parent == pathNode && pathNode2.pathID == handler.PathID)
				{
					other.UpdateRecursiveG(path, pathNode2, handler);
				}
			});
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001445C File Offset: 0x0001285C
		public virtual void FloodFill(Stack<GraphNode> stack, uint region)
		{
			this.GetConnections(delegate(GraphNode other)
			{
				if (other.Area != region)
				{
					other.Area = region;
					stack.Push(other);
				}
			});
		}

		// Token: 0x060003EF RID: 1007
		public abstract void GetConnections(Action<GraphNode> action);

		// Token: 0x060003F0 RID: 1008
		public abstract void AddConnection(GraphNode node, uint cost);

		// Token: 0x060003F1 RID: 1009
		public abstract void RemoveConnection(GraphNode node);

		// Token: 0x060003F2 RID: 1010
		public abstract void ClearConnections(bool alsoReverse);

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014490 File Offset: 0x00012890
		public virtual bool ContainsConnection(GraphNode node)
		{
			bool contains = false;
			this.GetConnections(delegate(GraphNode neighbour)
			{
				contains |= (neighbour == node);
			});
			return contains;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000144C9 File Offset: 0x000128C9
		public virtual void RecalculateConnectionCosts()
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000144CB File Offset: 0x000128CB
		public virtual bool GetPortal(GraphNode other, List<Vector3> left, List<Vector3> right, bool backwards)
		{
			return false;
		}

		// Token: 0x060003F6 RID: 1014
		public abstract void Open(Path path, PathNode pathNode, PathHandler handler);

		// Token: 0x060003F7 RID: 1015 RVA: 0x000144CE File Offset: 0x000128CE
		public virtual float SurfaceArea()
		{
			return 0f;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000144D5 File Offset: 0x000128D5
		public virtual Vector3 RandomPointOnSurface()
		{
			return (Vector3)this.position;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000144E2 File Offset: 0x000128E2
		public virtual int GetGizmoHashCode()
		{
			return this.position.GetHashCode() ^ (int)(19u * this.Penalty) ^ (int)(41u * this.flags);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014509 File Offset: 0x00012909
		public virtual void SerializeNode(GraphSerializationContext ctx)
		{
			ctx.writer.Write(this.Penalty);
			ctx.writer.Write(this.Flags);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001452D File Offset: 0x0001292D
		public virtual void DeserializeNode(GraphSerializationContext ctx)
		{
			this.Penalty = ctx.reader.ReadUInt32();
			this.Flags = ctx.reader.ReadUInt32();
			this.GraphIndex = ctx.graphIndex;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001455D File Offset: 0x0001295D
		public virtual void SerializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001455F File Offset: 0x0001295F
		public virtual void DeserializeReferences(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0400024E RID: 590
		private int nodeIndex;

		// Token: 0x0400024F RID: 591
		protected uint flags;

		// Token: 0x04000250 RID: 592
		private uint penalty;

		// Token: 0x04000251 RID: 593
		private const int NodeIndexMask = 268435455;

		// Token: 0x04000252 RID: 594
		private const int DestroyedNodeIndex = 268435454;

		// Token: 0x04000253 RID: 595
		private const int TemporaryFlag1Mask = 268435456;

		// Token: 0x04000254 RID: 596
		private const int TemporaryFlag2Mask = 536870912;

		// Token: 0x04000255 RID: 597
		public Int3 position;

		// Token: 0x04000256 RID: 598
		private const int FlagsWalkableOffset = 0;

		// Token: 0x04000257 RID: 599
		private const uint FlagsWalkableMask = 1u;

		// Token: 0x04000258 RID: 600
		private const int FlagsAreaOffset = 1;

		// Token: 0x04000259 RID: 601
		private const uint FlagsAreaMask = 262142u;

		// Token: 0x0400025A RID: 602
		private const int FlagsGraphOffset = 24;

		// Token: 0x0400025B RID: 603
		private const uint FlagsGraphMask = 4278190080u;

		// Token: 0x0400025C RID: 604
		public const uint MaxAreaIndex = 131071u;

		// Token: 0x0400025D RID: 605
		public const uint MaxGraphIndex = 255u;

		// Token: 0x0400025E RID: 606
		private const int FlagsTagOffset = 19;

		// Token: 0x0400025F RID: 607
		private const uint FlagsTagMask = 16252928u;
	}
}
