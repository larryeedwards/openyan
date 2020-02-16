using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200002D RID: 45
	public static class AstarMath
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000A91B File Offset: 0x00008D1B
		[Obsolete("Use VectorMath.ClosestPointOnLine instead")]
		public static Vector3 NearestPoint(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnLine(lineStart, lineEnd, point);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000A925 File Offset: 0x00008D25
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A92F File Offset: 0x00008D2F
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Int3 lineStart, Int3 lineEnd, Int3 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A939 File Offset: 0x00008D39
		[Obsolete("Use VectorMath.ClosestPointOnLineFactor instead")]
		public static float NearestPointFactor(Int2 lineStart, Int2 lineEnd, Int2 point)
		{
			return VectorMath.ClosestPointOnLineFactor(lineStart, lineEnd, point);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A943 File Offset: 0x00008D43
		[Obsolete("Use VectorMath.ClosestPointOnSegment instead")]
		public static Vector3 NearestPointStrict(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnSegment(lineStart, lineEnd, point);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A94D File Offset: 0x00008D4D
		[Obsolete("Use VectorMath.ClosestPointOnSegmentXZ instead")]
		public static Vector3 NearestPointStrictXZ(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			return VectorMath.ClosestPointOnSegmentXZ(lineStart, lineEnd, point);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A957 File Offset: 0x00008D57
		[Obsolete("Use VectorMath.SqrDistancePointSegmentApproximate instead")]
		public static float DistancePointSegment(int x, int z, int px, int pz, int qx, int qz)
		{
			return VectorMath.SqrDistancePointSegmentApproximate(x, z, px, pz, qx, qz);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A966 File Offset: 0x00008D66
		[Obsolete("Use VectorMath.SqrDistancePointSegmentApproximate instead")]
		public static float DistancePointSegment(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.SqrDistancePointSegmentApproximate(a, b, p);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A970 File Offset: 0x00008D70
		[Obsolete("Use VectorMath.SqrDistancePointSegment instead")]
		public static float DistancePointSegmentStrict(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.SqrDistancePointSegment(a, b, p);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A97A File Offset: 0x00008D7A
		[Obsolete("Use AstarSplines.CubicBezier instead")]
		public static Vector3 CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			return AstarSplines.CubicBezier(p0, p1, p2, p3, t);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A987 File Offset: 0x00008D87
		[Obsolete("Use Mathf.InverseLerp instead")]
		public static float MapTo(float startMin, float startMax, float value)
		{
			return Mathf.InverseLerp(startMin, startMax, value);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A991 File Offset: 0x00008D91
		public static float MapTo(float startMin, float startMax, float targetMin, float targetMax, float value)
		{
			return Mathf.Lerp(targetMin, targetMax, Mathf.InverseLerp(startMin, startMax, value));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A9A4 File Offset: 0x00008DA4
		public static string FormatBytesBinary(int bytes)
		{
			double num = (bytes < 0) ? -1.0 : 1.0;
			bytes = Mathf.Abs(bytes);
			if (bytes < 1024)
			{
				return (double)bytes * num + " bytes";
			}
			if (bytes < 1048576)
			{
				return ((double)bytes / 1024.0 * num).ToString("0.0") + " KiB";
			}
			if (bytes < 1073741824)
			{
				return ((double)bytes / 1048576.0 * num).ToString("0.0") + " MiB";
			}
			return ((double)bytes / 1073741824.0 * num).ToString("0.0") + " GiB";
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000AA7E File Offset: 0x00008E7E
		private static int Bit(int a, int b)
		{
			return a >> b & 1;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000AA88 File Offset: 0x00008E88
		public static Color IntToColor(int i, float a)
		{
			int num = AstarMath.Bit(i, 2) + AstarMath.Bit(i, 3) * 2 + 1;
			int num2 = AstarMath.Bit(i, 1) + AstarMath.Bit(i, 4) * 2 + 1;
			int num3 = AstarMath.Bit(i, 0) + AstarMath.Bit(i, 5) * 2 + 1;
			return new Color((float)num * 0.25f, (float)num2 * 0.25f, (float)num3 * 0.25f, a);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000AAF0 File Offset: 0x00008EF0
		public static Color HSVToRGB(float h, float s, float v)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = s * v;
			float num5 = h / 60f;
			float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
			if (num5 < 1f)
			{
				num = num4;
				num2 = num6;
			}
			else if (num5 < 2f)
			{
				num = num6;
				num2 = num4;
			}
			else if (num5 < 3f)
			{
				num2 = num4;
				num3 = num6;
			}
			else if (num5 < 4f)
			{
				num2 = num6;
				num3 = num4;
			}
			else if (num5 < 5f)
			{
				num = num6;
				num3 = num4;
			}
			else if (num5 < 6f)
			{
				num = num4;
				num3 = num6;
			}
			float num7 = v - num4;
			num += num7;
			num2 += num7;
			num3 += num7;
			return new Color(num, num2, num3);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000ABD4 File Offset: 0x00008FD4
		[Obsolete("Use VectorMath.SqrDistanceXZ instead")]
		public static float SqrMagnitudeXZ(Vector3 a, Vector3 b)
		{
			return VectorMath.SqrDistanceXZ(a, b);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000ABDD File Offset: 0x00008FDD
		[Obsolete("Obsolete", true)]
		public static float DistancePointSegment2(int x, int z, int px, int pz, int qx, int qz)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000ABE9 File Offset: 0x00008FE9
		[Obsolete("Obsolete", true)]
		public static float DistancePointSegment2(Vector3 a, Vector3 b, Vector3 p)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000ABF5 File Offset: 0x00008FF5
		[Obsolete("Use Int3.GetHashCode instead", true)]
		public static int ComputeVertexHash(int x, int y, int z)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AC01 File Offset: 0x00009001
		[Obsolete("Obsolete", true)]
		public static float Hermite(float start, float end, float value)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000AC0D File Offset: 0x0000900D
		[Obsolete("Obsolete", true)]
		public static float MapToRange(float targetMin, float targetMax, float value)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AC19 File Offset: 0x00009019
		[Obsolete("Obsolete", true)]
		public static string FormatBytes(int bytes)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000AC25 File Offset: 0x00009025
		[Obsolete("Obsolete", true)]
		public static float MagnitudeXZ(Vector3 a, Vector3 b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000AC31 File Offset: 0x00009031
		[Obsolete("Obsolete", true)]
		public static int Repeat(int i, int n)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000AC3D File Offset: 0x0000903D
		[Obsolete("Use Mathf.Abs instead", true)]
		public static float Abs(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AC49 File Offset: 0x00009049
		[Obsolete("Use Mathf.Abs instead", true)]
		public static int Abs(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000AC55 File Offset: 0x00009055
		[Obsolete("Use Mathf.Min instead", true)]
		public static float Min(float a, float b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000AC61 File Offset: 0x00009061
		[Obsolete("Use Mathf.Min instead", true)]
		public static int Min(int a, int b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000AC6D File Offset: 0x0000906D
		[Obsolete("Use Mathf.Min instead", true)]
		public static uint Min(uint a, uint b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AC79 File Offset: 0x00009079
		[Obsolete("Use Mathf.Max instead", true)]
		public static float Max(float a, float b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AC85 File Offset: 0x00009085
		[Obsolete("Use Mathf.Max instead", true)]
		public static int Max(int a, int b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000AC91 File Offset: 0x00009091
		[Obsolete("Use Mathf.Max instead", true)]
		public static uint Max(uint a, uint b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AC9D File Offset: 0x0000909D
		[Obsolete("Use Mathf.Max instead", true)]
		public static ushort Max(ushort a, ushort b)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000ACA9 File Offset: 0x000090A9
		[Obsolete("Use Mathf.Sign instead", true)]
		public static float Sign(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000ACB5 File Offset: 0x000090B5
		[Obsolete("Use Mathf.Sign instead", true)]
		public static int Sign(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000ACC1 File Offset: 0x000090C1
		[Obsolete("Use Mathf.Clamp instead", true)]
		public static float Clamp(float a, float b, float c)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000ACCD File Offset: 0x000090CD
		[Obsolete("Use Mathf.Clamp instead", true)]
		public static int Clamp(int a, int b, int c)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000ACD9 File Offset: 0x000090D9
		[Obsolete("Use Mathf.Clamp01 instead", true)]
		public static float Clamp01(float a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000ACE5 File Offset: 0x000090E5
		[Obsolete("Use Mathf.Clamp01 instead", true)]
		public static int Clamp01(int a)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000ACF1 File Offset: 0x000090F1
		[Obsolete("Use Mathf.Lerp instead", true)]
		public static float Lerp(float a, float b, float t)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000ACFD File Offset: 0x000090FD
		[Obsolete("Use Mathf.RoundToInt instead", true)]
		public static int RoundToInt(float v)
		{
			throw new NotImplementedException("Obsolete");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000AD09 File Offset: 0x00009109
		[Obsolete("Use Mathf.RoundToInt instead", true)]
		public static int RoundToInt(double v)
		{
			throw new NotImplementedException("Obsolete");
		}
	}
}
