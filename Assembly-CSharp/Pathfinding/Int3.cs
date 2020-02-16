using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000044 RID: 68
	public struct Int3 : IEquatable<Int3>
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x00012F10 File Offset: 0x00011310
		public Int3(Vector3 position)
		{
			this.x = (int)Math.Round((double)(position.x * 1000f));
			this.y = (int)Math.Round((double)(position.y * 1000f));
			this.z = (int)Math.Round((double)(position.z * 1000f));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00012F6B File Offset: 0x0001136B
		public Int3(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00012F84 File Offset: 0x00011384
		public static Int3 zero
		{
			get
			{
				return default(Int3);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00012F9A File Offset: 0x0001139A
		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00012FD5 File Offset: 0x000113D5
		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00013014 File Offset: 0x00011414
		public static explicit operator Int3(Vector3 ob)
		{
			return new Int3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00013062 File Offset: 0x00011462
		public static explicit operator Vector3(Int3 ob)
		{
			return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00013094 File Offset: 0x00011494
		public static Int3 operator -(Int3 lhs, Int3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000130E1 File Offset: 0x000114E1
		public static Int3 operator -(Int3 lhs)
		{
			lhs.x = -lhs.x;
			lhs.y = -lhs.y;
			lhs.z = -lhs.z;
			return lhs;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00013114 File Offset: 0x00011514
		public static Int3 operator +(Int3 lhs, Int3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013161 File Offset: 0x00011561
		public static Int3 operator *(Int3 lhs, int rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00013194 File Offset: 0x00011594
		public static Int3 operator *(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
			return lhs;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000131EC File Offset: 0x000115EC
		public static Int3 operator *(Int3 lhs, double rhs)
		{
			lhs.x = (int)Math.Round((double)lhs.x * rhs);
			lhs.y = (int)Math.Round((double)lhs.y * rhs);
			lhs.z = (int)Math.Round((double)lhs.z * rhs);
			return lhs;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00013240 File Offset: 0x00011640
		public static Int3 operator /(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
			return lhs;
		}

		// Token: 0x1700007A RID: 122
		public int this[int i]
		{
			get
			{
				return (i != 0) ? ((i != 1) ? this.z : this.y) : this.x;
			}
			set
			{
				if (i == 0)
				{
					this.x = value;
				}
				else if (i == 1)
				{
					this.y = value;
				}
				else
				{
					this.z = value;
				}
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000132F0 File Offset: 0x000116F0
		public static float Angle(Int3 lhs, Int3 rhs)
		{
			double num = (double)Int3.Dot(lhs, rhs) / ((double)lhs.magnitude * (double)rhs.magnitude);
			num = ((num >= -1.0) ? ((num <= 1.0) ? num : 1.0) : -1.0);
			return (float)Math.Acos(num);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001335B File Offset: 0x0001175B
		public static int Dot(Int3 lhs, Int3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001338C File Offset: 0x0001178C
		public static long DotLong(Int3 lhs, Int3 rhs)
		{
			return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000133C3 File Offset: 0x000117C3
		public Int3 Normal2D()
		{
			return new Int3(this.z, this.y, -this.x);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000133E0 File Offset: 0x000117E0
		public float magnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00013416 File Offset: 0x00011816
		public int costMagnitude
		{
			get
			{
				return (int)Math.Round((double)this.magnitude);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00013428 File Offset: 0x00011828
		[Obsolete("This property is deprecated. Use magnitude or cast to a Vector3")]
		public float worldMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3) * 0.001f;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00013464 File Offset: 0x00011864
		public float sqrMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00013498 File Offset: 0x00011898
		public long sqrMagnitudeLong
		{
			get
			{
				long num = (long)this.x;
				long num2 = (long)this.y;
				long num3 = (long)this.z;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000134C8 File Offset: 0x000118C8
		public static implicit operator string(Int3 obj)
		{
			return obj.ToString();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000134D8 File Offset: 0x000118D8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( ",
				this.x,
				", ",
				this.y,
				", ",
				this.z,
				")"
			});
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001353C File Offset: 0x0001193C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Int3 @int = (Int3)obj;
			return this.x == @int.x && this.y == @int.y && this.z == @int.z;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001358E File Offset: 0x0001198E
		public bool Equals(Int3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000135C6 File Offset: 0x000119C6
		public override int GetHashCode()
		{
			return this.x * 73856093 ^ this.y * 19349663 ^ this.z * 83492791;
		}

		// Token: 0x040001EA RID: 490
		public int x;

		// Token: 0x040001EB RID: 491
		public int y;

		// Token: 0x040001EC RID: 492
		public int z;

		// Token: 0x040001ED RID: 493
		public const int Precision = 1000;

		// Token: 0x040001EE RID: 494
		public const float FloatPrecision = 1000f;

		// Token: 0x040001EF RID: 495
		public const float PrecisionFactor = 0.001f;
	}
}
