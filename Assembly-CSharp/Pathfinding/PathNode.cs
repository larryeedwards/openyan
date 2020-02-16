using System;

namespace Pathfinding
{
	// Token: 0x02000064 RID: 100
	public class PathNode
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00018F96 File Offset: 0x00017396
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00018FA4 File Offset: 0x000173A4
		public uint cost
		{
			get
			{
				return this.flags & 268435455u;
			}
			set
			{
				this.flags = ((this.flags & 4026531840u) | value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00018FBA File Offset: 0x000173BA
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00018FCE File Offset: 0x000173CE
		public bool flag1
		{
			get
			{
				return (this.flags & 268435456u) != 0u;
			}
			set
			{
				this.flags = ((this.flags & 4026531839u) | ((!value) ? 0u : 268435456u));
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00018FF4 File Offset: 0x000173F4
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00019008 File Offset: 0x00017408
		public bool flag2
		{
			get
			{
				return (this.flags & 536870912u) != 0u;
			}
			set
			{
				this.flags = ((this.flags & 3758096383u) | ((!value) ? 0u : 536870912u));
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001902E File Offset: 0x0001742E
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00019036 File Offset: 0x00017436
		public uint G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001903F File Offset: 0x0001743F
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x00019047 File Offset: 0x00017447
		public uint H
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00019050 File Offset: 0x00017450
		public uint F
		{
			get
			{
				return this.g + this.h;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001905F File Offset: 0x0001745F
		public void UpdateG(Path path)
		{
			this.g = this.parent.g + this.cost + path.GetTraversalCost(this.node);
		}

		// Token: 0x0400027D RID: 637
		public GraphNode node;

		// Token: 0x0400027E RID: 638
		public PathNode parent;

		// Token: 0x0400027F RID: 639
		public ushort pathID;

		// Token: 0x04000280 RID: 640
		public ushort heapIndex = ushort.MaxValue;

		// Token: 0x04000281 RID: 641
		private uint flags;

		// Token: 0x04000282 RID: 642
		private const uint CostMask = 268435455u;

		// Token: 0x04000283 RID: 643
		private const int Flag1Offset = 28;

		// Token: 0x04000284 RID: 644
		private const uint Flag1Mask = 268435456u;

		// Token: 0x04000285 RID: 645
		private const int Flag2Offset = 29;

		// Token: 0x04000286 RID: 646
		private const uint Flag2Mask = 536870912u;

		// Token: 0x04000287 RID: 647
		private uint g;

		// Token: 0x04000288 RID: 648
		private uint h;
	}
}
