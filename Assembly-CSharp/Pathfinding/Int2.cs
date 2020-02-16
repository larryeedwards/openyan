using System;

namespace Pathfinding
{
	// Token: 0x02000045 RID: 69
	public struct Int2 : IEquatable<Int2>
	{
		// Token: 0x06000313 RID: 787 RVA: 0x000135EE File Offset: 0x000119EE
		public Int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000135FE File Offset: 0x000119FE
		public long sqrMagnitudeLong
		{
			get
			{
				return (long)this.x * (long)this.x + (long)this.y * (long)this.y;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001361F File Offset: 0x00011A1F
		public static Int2 operator +(Int2 a, Int2 b)
		{
			return new Int2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00013644 File Offset: 0x00011A44
		public static Int2 operator -(Int2 a, Int2 b)
		{
			return new Int2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00013669 File Offset: 0x00011A69
		public static bool operator ==(Int2 a, Int2 b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00013691 File Offset: 0x00011A91
		public static bool operator !=(Int2 a, Int2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000136BC File Offset: 0x00011ABC
		public static long DotLong(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000136E4 File Offset: 0x00011AE4
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			Int2 @int = (Int2)o;
			return this.x == @int.x && this.y == @int.y;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00013724 File Offset: 0x00011B24
		public bool Equals(Int2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001374A File Offset: 0x00011B4A
		public override int GetHashCode()
		{
			return this.x * 49157 + this.y * 98317;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00013768 File Offset: 0x00011B68
		[Obsolete("Deprecated becuase it is not used by any part of the A* Pathfinding Project")]
		public static Int2 Rotate(Int2 v, int r)
		{
			r %= 4;
			return new Int2(v.x * Int2.Rotations[r * 4] + v.y * Int2.Rotations[r * 4 + 1], v.x * Int2.Rotations[r * 4 + 2] + v.y * Int2.Rotations[r * 4 + 3]);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000137CB File Offset: 0x00011BCB
		public static Int2 Min(Int2 a, Int2 b)
		{
			return new Int2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000137F8 File Offset: 0x00011BF8
		public static Int2 Max(Int2 a, Int2 b)
		{
			return new Int2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00013825 File Offset: 0x00011C25
		public static Int2 FromInt3XZ(Int3 o)
		{
			return new Int2(o.x, o.z);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001383A File Offset: 0x00011C3A
		public static Int3 ToInt3XZ(Int2 o)
		{
			return new Int3(o.x, 0, o.y);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00013850 File Offset: 0x00011C50
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(",
				this.x,
				", ",
				this.y,
				")"
			});
		}

		// Token: 0x040001F0 RID: 496
		public int x;

		// Token: 0x040001F1 RID: 497
		public int y;

		// Token: 0x040001F2 RID: 498
		private static readonly int[] Rotations = new int[]
		{
			1,
			0,
			0,
			1,
			0,
			1,
			-1,
			0,
			-1,
			0,
			0,
			-1,
			0,
			-1,
			1,
			0
		};
	}
}
