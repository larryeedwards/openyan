using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public struct IntRect
	{
		// Token: 0x06000133 RID: 307 RVA: 0x000078C9 File Offset: 0x00005CC9
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000078E8 File Offset: 0x00005CE8
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000791D File Offset: 0x00005D1D
		public int Width
		{
			get
			{
				return this.xmax - this.xmin + 1;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000792E File Offset: 0x00005D2E
		public int Height
		{
			get
			{
				return this.ymax - this.ymin + 1;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000793F File Offset: 0x00005D3F
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007968 File Offset: 0x00005D68
		public static bool operator ==(IntRect a, IntRect b)
		{
			return a.xmin == b.xmin && a.xmax == b.xmax && a.ymin == b.ymin && a.ymax == b.ymax;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000079C4 File Offset: 0x00005DC4
		public static bool operator !=(IntRect a, IntRect b)
		{
			return a.xmin != b.xmin || a.xmax != b.xmax || a.ymin != b.ymin || a.ymax != b.ymax;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007A20 File Offset: 0x00005E20
		public override bool Equals(object obj)
		{
			IntRect intRect = (IntRect)obj;
			return this.xmin == intRect.xmin && this.xmax == intRect.xmax && this.ymin == intRect.ymin && this.ymax == intRect.ymax;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007A7C File Offset: 0x00005E7C
		public override int GetHashCode()
		{
			return this.xmin * 131071 ^ this.xmax * 3571 ^ this.ymin * 3109 ^ this.ymax * 7;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007AB0 File Offset: 0x00005EB0
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			return new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007B10 File Offset: 0x00005F10
		public static bool Intersects(IntRect a, IntRect b)
		{
			return a.xmin <= b.xmax && a.ymin <= b.ymax && a.xmax >= b.xmin && a.ymax >= b.ymin;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007B6C File Offset: 0x00005F6C
		public static IntRect Union(IntRect a, IntRect b)
		{
			return new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007BCA File Offset: 0x00005FCA
		public IntRect ExpandToContain(int x, int y)
		{
			return new IntRect(Math.Min(this.xmin, x), Math.Min(this.ymin, y), Math.Max(this.xmax, x), Math.Max(this.ymax, y));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007C01 File Offset: 0x00006001
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007C28 File Offset: 0x00006028
		public IntRect Rotate(int r)
		{
			int num = IntRect.Rotations[r * 4];
			int num2 = IntRect.Rotations[r * 4 + 1];
			int num3 = IntRect.Rotations[r * 4 + 2];
			int num4 = IntRect.Rotations[r * 4 + 3];
			int val = num * this.xmin + num2 * this.ymin;
			int val2 = num3 * this.xmin + num4 * this.ymin;
			int val3 = num * this.xmax + num2 * this.ymax;
			int val4 = num3 * this.xmax + num4 * this.ymax;
			return new IntRect(Math.Min(val, val3), Math.Min(val2, val4), Math.Max(val, val3), Math.Max(val2, val4));
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007CD8 File Offset: 0x000060D8
		public IntRect Offset(Int2 offset)
		{
			return new IntRect(this.xmin + offset.x, this.ymin + offset.y, this.xmax + offset.x, this.ymax + offset.y);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007D17 File Offset: 0x00006117
		public IntRect Offset(int x, int y)
		{
			return new IntRect(this.xmin + x, this.ymin + y, this.xmax + x, this.ymax + y);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007D40 File Offset: 0x00006140
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"[x: ",
				this.xmin,
				"...",
				this.xmax,
				", y: ",
				this.ymin,
				"...",
				this.ymax,
				"]"
			});
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007DBC File Offset: 0x000061BC
		public void DebugDraw(GraphTransform transform, Color color)
		{
			Vector3 vector = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymin));
			Vector3 vector2 = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymax));
			Vector3 vector3 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymax));
			Vector3 vector4 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymin));
			Debug.DrawLine(vector, vector2, color);
			Debug.DrawLine(vector2, vector3, color);
			Debug.DrawLine(vector3, vector4, color);
			Debug.DrawLine(vector4, vector, color);
		}

		// Token: 0x040000D1 RID: 209
		public int xmin;

		// Token: 0x040000D2 RID: 210
		public int ymin;

		// Token: 0x040000D3 RID: 211
		public int xmax;

		// Token: 0x040000D4 RID: 212
		public int ymax;

		// Token: 0x040000D5 RID: 213
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
