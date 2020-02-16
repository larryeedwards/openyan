using System;
using System.Text;

namespace Pathfinding.Util
{
	// Token: 0x0200011C RID: 284
	public struct Guid
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0004DB00 File Offset: 0x0004BF00
		public Guid(byte[] bytes)
		{
			ulong num = (ulong)bytes[0] << 0 | (ulong)bytes[1] << 8 | (ulong)bytes[2] << 16 | (ulong)bytes[3] << 24 | (ulong)bytes[4] << 32 | (ulong)bytes[5] << 40 | (ulong)bytes[6] << 48 | (ulong)bytes[7] << 56;
			ulong num2 = (ulong)bytes[8] << 0 | (ulong)bytes[9] << 8 | (ulong)bytes[10] << 16 | (ulong)bytes[11] << 24 | (ulong)bytes[12] << 32 | (ulong)bytes[13] << 40 | (ulong)bytes[14] << 48 | (ulong)bytes[15] << 56;
			this._a = ((!BitConverter.IsLittleEndian) ? Guid.SwapEndianness(num) : num);
			this._b = ((!BitConverter.IsLittleEndian) ? Guid.SwapEndianness(num2) : num2);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0004DBC8 File Offset: 0x0004BFC8
		public Guid(string str)
		{
			this._a = 0UL;
			this._b = 0UL;
			if (str.Length < 32)
			{
				throw new FormatException("Invalid Guid format");
			}
			int i = 0;
			int num = 0;
			int num2 = 60;
			while (i < 16)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c = str[num];
				if (c != '-')
				{
					int num3 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c));
					if (num3 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c + " is not a hexadecimal character");
					}
					this._a |= (ulong)((ulong)((long)num3) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
			num2 = 60;
			while (i < 32)
			{
				if (num >= str.Length)
				{
					throw new FormatException("Invalid Guid format. String too short");
				}
				char c2 = str[num];
				if (c2 != '-')
				{
					int num4 = "0123456789ABCDEF".IndexOf(char.ToUpperInvariant(c2));
					if (num4 == -1)
					{
						throw new FormatException("Invalid Guid format : " + c2 + " is not a hexadecimal character");
					}
					this._b |= (ulong)((ulong)((long)num4) << num2);
					num2 -= 4;
					i++;
				}
				num++;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0004DD29 File Offset: 0x0004C129
		public static Guid Parse(string input)
		{
			return new Guid(input);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0004DD34 File Offset: 0x0004C134
		private static ulong SwapEndianness(ulong value)
		{
			ulong num = value >> 0 & 255UL;
			ulong num2 = value >> 8 & 255UL;
			ulong num3 = value >> 16 & 255UL;
			ulong num4 = value >> 24 & 255UL;
			ulong num5 = value >> 32 & 255UL;
			ulong num6 = value >> 40 & 255UL;
			ulong num7 = value >> 48 & 255UL;
			ulong num8 = value >> 56 & 255UL;
			return num << 56 | num2 << 48 | num3 << 40 | num4 << 32 | num5 << 24 | num6 << 16 | num7 << 8 | num8 << 0;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0004DDCC File Offset: 0x0004C1CC
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			byte[] bytes = BitConverter.GetBytes(BitConverter.IsLittleEndian ? this._a : Guid.SwapEndianness(this._a));
			byte[] bytes2 = BitConverter.GetBytes(BitConverter.IsLittleEndian ? this._b : Guid.SwapEndianness(this._b));
			for (int i = 0; i < 8; i++)
			{
				array[i] = bytes[i];
				array[i + 8] = bytes2[i];
			}
			return array;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0004DE50 File Offset: 0x0004C250
		public static Guid NewGuid()
		{
			byte[] array = new byte[16];
			Guid.random.NextBytes(array);
			return new Guid(array);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0004DE76 File Offset: 0x0004C276
		public static bool operator ==(Guid lhs, Guid rhs)
		{
			return lhs._a == rhs._a && lhs._b == rhs._b;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0004DE9E File Offset: 0x0004C29E
		public static bool operator !=(Guid lhs, Guid rhs)
		{
			return lhs._a != rhs._a || lhs._b != rhs._b;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0004DECC File Offset: 0x0004C2CC
		public override bool Equals(object _rhs)
		{
			if (!(_rhs is Guid))
			{
				return false;
			}
			Guid guid = (Guid)_rhs;
			return this._a == guid._a && this._b == guid._b;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0004DF14 File Offset: 0x0004C314
		public override int GetHashCode()
		{
			ulong num = this._a ^ this._b;
			return (int)(num >> 32) ^ (int)num;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0004DF38 File Offset: 0x0004C338
		public override string ToString()
		{
			if (Guid.text == null)
			{
				Guid.text = new StringBuilder();
			}
			object obj = Guid.text;
			string result;
			lock (obj)
			{
				Guid.text.Length = 0;
				Guid.text.Append(this._a.ToString("x16")).Append('-').Append(this._b.ToString("x16"));
				result = Guid.text.ToString();
			}
			return result;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0004DFD8 File Offset: 0x0004C3D8
		// Note: this type is marked as 'beforefieldinit'.
		static Guid()
		{
			Guid guid = new Guid(new byte[16]);
			Guid.zeroString = guid.ToString();
			Guid.random = new Random();
		}

		// Token: 0x040006FC RID: 1788
		private const string hex = "0123456789ABCDEF";

		// Token: 0x040006FD RID: 1789
		public static readonly Guid zero = new Guid(new byte[16]);

		// Token: 0x040006FE RID: 1790
		public static readonly string zeroString;

		// Token: 0x040006FF RID: 1791
		private readonly ulong _a;

		// Token: 0x04000700 RID: 1792
		private readonly ulong _b;

		// Token: 0x04000701 RID: 1793
		private static Random random;

		// Token: 0x04000702 RID: 1794
		private static StringBuilder text;
	}
}
