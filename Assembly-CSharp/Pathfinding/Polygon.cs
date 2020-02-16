using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200002E RID: 46
	public static class Polygon
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000AD15 File Offset: 0x00009115
		[Obsolete("Use VectorMath.SignedTriangleAreaTimes2XZ instead")]
		public static long TriangleArea2(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.SignedTriangleAreaTimes2XZ(a, b, c);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AD1F File Offset: 0x0000911F
		[Obsolete("Use VectorMath.SignedTriangleAreaTimes2XZ instead")]
		public static float TriangleArea2(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.SignedTriangleAreaTimes2XZ(a, b, c);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AD29 File Offset: 0x00009129
		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static long TriangleArea(Int3 a, Int3 b, Int3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AD33 File Offset: 0x00009133
		[Obsolete("Use TriangleArea2 instead to avoid confusion regarding the factor 2")]
		public static float TriangleArea(Vector3 a, Vector3 b, Vector3 c)
		{
			return Polygon.TriangleArea2(a, b, c);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AD3D File Offset: 0x0000913D
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return Polygon.ContainsPointXZ(a, b, c, p);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AD48 File Offset: 0x00009148
		public static bool ContainsPointXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, p) && VectorMath.IsClockwiseMarginXZ(b, c, p) && VectorMath.IsClockwiseMarginXZ(c, a, p);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000AD6F File Offset: 0x0000916F
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return Polygon.ContainsPointXZ(a, b, c, p);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000AD7A File Offset: 0x0000917A
		public static bool ContainsPointXZ(Int3 a, Int3 b, Int3 c, Int3 p)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, p) && VectorMath.IsClockwiseOrColinearXZ(b, c, p) && VectorMath.IsClockwiseOrColinearXZ(c, a, p);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000ADA1 File Offset: 0x000091A1
		public static bool ContainsPoint(Int2 a, Int2 b, Int2 c, Int2 p)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, p) && VectorMath.IsClockwiseOrColinear(b, c, p) && VectorMath.IsClockwiseOrColinear(c, a, p);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000ADC8 File Offset: 0x000091C8
		[Obsolete("Use ContainsPointXZ instead")]
		public static bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
		{
			return Polygon.ContainsPointXZ(polyPoints, p);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000ADD4 File Offset: 0x000091D4
		public static bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].y <= p.y && p.y < polyPoints[num].y) || (polyPoints[num].y <= p.y && p.y < polyPoints[i].y)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[num].y - polyPoints[i].y) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000AEC8 File Offset: 0x000092C8
		public static bool ContainsPointXZ(Vector3[] polyPoints, Vector3 p)
		{
			int num = polyPoints.Length - 1;
			bool flag = false;
			int i = 0;
			while (i < polyPoints.Length)
			{
				if (((polyPoints[i].z <= p.z && p.z < polyPoints[num].z) || (polyPoints[num].z <= p.z && p.z < polyPoints[i].z)) && p.x < (polyPoints[num].x - polyPoints[i].x) * (p.z - polyPoints[i].z) / (polyPoints[num].z - polyPoints[i].z) + polyPoints[i].x)
				{
					flag = !flag;
				}
				num = i++;
			}
			return flag;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AFBC File Offset: 0x000093BC
		public static int SampleYCoordinateInTriangle(Int3 p1, Int3 p2, Int3 p3, Int3 p)
		{
			double num = (double)(p2.z - p3.z) * (double)(p1.x - p3.x) + (double)(p3.x - p2.x) * (double)(p1.z - p3.z);
			double num2 = ((double)(p2.z - p3.z) * (double)(p.x - p3.x) + (double)(p3.x - p2.x) * (double)(p.z - p3.z)) / num;
			double num3 = ((double)(p3.z - p1.z) * (double)(p.x - p3.x) + (double)(p1.x - p3.x) * (double)(p.z - p3.z)) / num;
			return (int)Math.Round(num2 * (double)p1.y + num3 * (double)p2.y + (1.0 - num2 - num3) * (double)p3.y);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B0CB File Offset: 0x000094CB
		[Obsolete("Use VectorMath.RightXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool LeftNotColinear(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.RightXZ(a, b, p);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B0D5 File Offset: 0x000094D5
		[Obsolete("Use VectorMath.RightOrColinearXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Vector3 a, Vector3 b, Vector3 p)
		{
			return VectorMath.RightOrColinearXZ(a, b, p);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B0DF File Offset: 0x000094DF
		[Obsolete("Use VectorMath.RightOrColinear instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Vector2 a, Vector2 b, Vector2 p)
		{
			return VectorMath.RightOrColinear(a, b, p);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B0E9 File Offset: 0x000094E9
		[Obsolete("Use VectorMath.RightOrColinearXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.RightOrColinearXZ(a, b, p);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000B0F3 File Offset: 0x000094F3
		[Obsolete("Use VectorMath.RightXZ instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool LeftNotColinear(Int3 a, Int3 b, Int3 p)
		{
			return VectorMath.RightXZ(a, b, p);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B0FD File Offset: 0x000094FD
		[Obsolete("Use VectorMath.RightOrColinear instead. Note that it now uses a left handed coordinate system (same as Unity)")]
		public static bool Left(Int2 a, Int2 b, Int2 p)
		{
			return VectorMath.RightOrColinear(a, b, p);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B107 File Offset: 0x00009507
		[Obsolete("Use VectorMath.IsClockwiseMarginXZ instead")]
		public static bool IsClockwiseMargin(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsClockwiseMarginXZ(a, b, c);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B111 File Offset: 0x00009511
		[Obsolete("Use VectorMath.IsClockwiseXZ instead")]
		public static bool IsClockwise(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsClockwiseXZ(a, b, c);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B11B File Offset: 0x0000951B
		[Obsolete("Use VectorMath.IsClockwiseXZ instead")]
		public static bool IsClockwise(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsClockwiseXZ(a, b, c);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B125 File Offset: 0x00009525
		[Obsolete("Use VectorMath.IsClockwiseOrColinearXZ instead")]
		public static bool IsClockwiseMargin(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsClockwiseOrColinearXZ(a, b, c);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B12F File Offset: 0x0000952F
		[Obsolete("Use VectorMath.IsClockwiseOrColinear instead")]
		public static bool IsClockwiseMargin(Int2 a, Int2 b, Int2 c)
		{
			return VectorMath.IsClockwiseOrColinear(a, b, c);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B139 File Offset: 0x00009539
		[Obsolete("Use VectorMath.IsColinearXZ instead")]
		public static bool IsColinear(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsColinearXZ(a, b, c);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B143 File Offset: 0x00009543
		[Obsolete("Use VectorMath.IsColinearAlmostXZ instead")]
		public static bool IsColinearAlmost(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.IsColinearAlmostXZ(a, b, c);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B14D File Offset: 0x0000954D
		[Obsolete("Use VectorMath.IsColinearXZ instead")]
		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			return VectorMath.IsColinearXZ(a, b, c);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B157 File Offset: 0x00009557
		[Obsolete("Marked for removal since it is not used by any part of the A* Pathfinding Project")]
		public static bool IntersectsUnclamped(Vector3 a, Vector3 b, Vector3 a2, Vector3 b2)
		{
			return VectorMath.RightOrColinearXZ(a, b, a2) != VectorMath.RightOrColinearXZ(a, b, b2);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B16E File Offset: 0x0000956E
		[Obsolete("Use VectorMath.SegmentsIntersect instead")]
		public static bool Intersects(Int2 start1, Int2 end1, Int2 start2, Int2 end2)
		{
			return VectorMath.SegmentsIntersect(start1, end1, start2, end2);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B179 File Offset: 0x00009579
		[Obsolete("Use VectorMath.SegmentsIntersectXZ instead")]
		public static bool Intersects(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.SegmentsIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B184 File Offset: 0x00009584
		[Obsolete("Use VectorMath.SegmentsIntersectXZ instead")]
		public static bool Intersects(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.SegmentsIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B18F File Offset: 0x0000958F
		[Obsolete("Use VectorMath.LineDirIntersectionPointXZ instead")]
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
		{
			return VectorMath.LineDirIntersectionPointXZ(start1, dir1, start2, dir2);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B19A File Offset: 0x0000959A
		[Obsolete("Use VectorMath.LineDirIntersectionPointXZ instead")]
		public static Vector3 IntersectionPointOptimized(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2, out bool intersects)
		{
			return VectorMath.LineDirIntersectionPointXZ(start1, dir1, start2, dir2, out intersects);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B1A7 File Offset: 0x000095A7
		[Obsolete("Use VectorMath.RaySegmentIntersectXZ instead")]
		public static bool IntersectionFactorRaySegment(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.RaySegmentIntersectXZ(start1, end1, start2, end2);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B1B2 File Offset: 0x000095B2
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static bool IntersectionFactor(Int3 start1, Int3 end1, Int3 start2, Int3 end2, out float factor1, out float factor2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2, out factor1, out factor2);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B1C1 File Offset: 0x000095C1
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static bool IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out float factor1, out float factor2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2, out factor1, out factor2);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B1D0 File Offset: 0x000095D0
		[Obsolete("Use VectorMath.LineRayIntersectionFactorXZ instead")]
		public static float IntersectionFactorRay(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.LineRayIntersectionFactorXZ(start1, end1, start2, end2);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B1DB File Offset: 0x000095DB
		[Obsolete("Use VectorMath.LineIntersectionFactorXZ instead")]
		public static float IntersectionFactor(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.LineIntersectionFactorXZ(start1, end1, start2, end2);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B1E6 File Offset: 0x000095E6
		[Obsolete("Use VectorMath.LineIntersectionPointXZ instead")]
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B1F1 File Offset: 0x000095F1
		[Obsolete("Use VectorMath.LineIntersectionPointXZ instead")]
		public static Vector3 IntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B1FE File Offset: 0x000095FE
		[Obsolete("Use VectorMath.LineIntersectionPoint instead")]
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B209 File Offset: 0x00009609
		[Obsolete("Use VectorMath.LineIntersectionPoint instead")]
		public static Vector2 IntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out bool intersects)
		{
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B216 File Offset: 0x00009616
		[Obsolete("Use VectorMath.SegmentIntersectionPointXZ instead")]
		public static Vector3 SegmentIntersectionPoint(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			return VectorMath.SegmentIntersectionPointXZ(start1, end1, start2, end2, out intersects);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B223 File Offset: 0x00009623
		[Obsolete("Use ConvexHullXZ instead")]
		public static Vector3[] ConvexHull(Vector3[] points)
		{
			return Polygon.ConvexHullXZ(points);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B22C File Offset: 0x0000962C
		public static Vector3[] ConvexHullXZ(Vector3[] points)
		{
			if (points.Length == 0)
			{
				return new Vector3[0];
			}
			List<Vector3> list = ListPool<Vector3>.Claim();
			int num = 0;
			for (int i = 1; i < points.Length; i++)
			{
				if (points[i].x < points[num].x)
				{
					num = i;
				}
			}
			int num2 = num;
			int num3 = 0;
			for (;;)
			{
				list.Add(points[num]);
				int num4 = 0;
				for (int j = 0; j < points.Length; j++)
				{
					if (num4 == num || !VectorMath.RightOrColinearXZ(points[num], points[num4], points[j]))
					{
						num4 = j;
					}
				}
				num = num4;
				num3++;
				if (num3 > 10000)
				{
					break;
				}
				if (num == num2)
				{
					goto IL_E3;
				}
			}
			Debug.LogWarning("Infinite Loop in Convex Hull Calculation");
			IL_E3:
			Vector3[] result = list.ToArray();
			ListPool<Vector3>.Release(list);
			return result;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B32C File Offset: 0x0000972C
		[Obsolete("Use VectorMath.SegmentIntersectsBounds instead")]
		public static bool LineIntersectsBounds(Bounds bounds, Vector3 a, Vector3 b)
		{
			return VectorMath.SegmentIntersectsBounds(bounds, a, b);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B336 File Offset: 0x00009736
		[Obsolete("Scheduled for removal since it is not used by any part of the A* Pathfinding Project")]
		public static Vector3 ClosestPointOnTriangle(Vector3[] triangle, Vector3 point)
		{
			return Polygon.ClosestPointOnTriangle(triangle[0], triangle[1], triangle[2], point);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B364 File Offset: 0x00009764
		public static Vector2 ClosestPointOnTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 p)
		{
			Vector2 vector = b - a;
			Vector2 vector2 = c - a;
			Vector2 rhs = p - a;
			float num = Vector2.Dot(vector, rhs);
			float num2 = Vector2.Dot(vector2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 rhs2 = p - b;
			float num3 = Vector2.Dot(vector, rhs2);
			float num4 = Vector2.Dot(vector2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			if (num >= 0f && num3 <= 0f)
			{
				float num5 = num * num4 - num3 * num2;
				if (num5 <= 0f)
				{
					float d = num / (num - num3);
					return a + vector * d;
				}
			}
			Vector2 rhs3 = p - c;
			float num6 = Vector2.Dot(vector, rhs3);
			float num7 = Vector2.Dot(vector2, rhs3);
			if (num7 >= 0f && num6 <= num7)
			{
				return c;
			}
			if (num2 >= 0f && num7 <= 0f)
			{
				float num8 = num6 * num2 - num * num7;
				if (num8 <= 0f)
				{
					float d2 = num2 / (num2 - num7);
					return a + vector2 * d2;
				}
			}
			if (num4 - num3 >= 0f && num6 - num7 >= 0f)
			{
				float num9 = num3 * num7 - num6 * num4;
				if (num9 <= 0f)
				{
					float d3 = (num4 - num3) / (num4 - num3 + (num6 - num7));
					return b + (c - b) * d3;
				}
			}
			return p;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B50C File Offset: 0x0000990C
		public static Vector3 ClosestPointOnTriangleXZ(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector2 lhs = new Vector2(b.x - a.x, b.z - a.z);
			Vector2 lhs2 = new Vector2(c.x - a.x, c.z - a.z);
			Vector2 rhs = new Vector2(p.x - a.x, p.z - a.z);
			float num = Vector2.Dot(lhs, rhs);
			float num2 = Vector2.Dot(lhs2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector2 rhs2 = new Vector2(p.x - b.x, p.z - b.z);
			float num3 = Vector2.Dot(lhs, rhs2);
			float num4 = Vector2.Dot(lhs2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float num6 = num / (num - num3);
				return (1f - num6) * a + num6 * b;
			}
			Vector2 rhs3 = new Vector2(p.x - c.x, p.z - c.z);
			float num7 = Vector2.Dot(lhs, rhs3);
			float num8 = Vector2.Dot(lhs2, rhs3);
			if (num8 >= 0f && num7 <= num8)
			{
				return c;
			}
			float num9 = num7 * num2 - num * num8;
			if (num2 >= 0f && num8 <= 0f && num9 <= 0f)
			{
				float num10 = num2 / (num2 - num8);
				return (1f - num10) * a + num10 * c;
			}
			float num11 = num3 * num8 - num7 * num4;
			if (num4 - num3 >= 0f && num7 - num8 >= 0f && num11 <= 0f)
			{
				float d = (num4 - num3) / (num4 - num3 + (num7 - num8));
				return b + (c - b) * d;
			}
			float num12 = 1f / (num11 + num9 + num5);
			float num13 = num9 * num12;
			float num14 = num5 * num12;
			return new Vector3(p.x, (1f - num13 - num14) * a.y + num13 * b.y + num14 * c.y, p.z);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B7B4 File Offset: 0x00009BB4
		public static Vector3 ClosestPointOnTriangle(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 rhs = p - a;
			float num = Vector3.Dot(vector, rhs);
			float num2 = Vector3.Dot(vector2, rhs);
			if (num <= 0f && num2 <= 0f)
			{
				return a;
			}
			Vector3 rhs2 = p - b;
			float num3 = Vector3.Dot(vector, rhs2);
			float num4 = Vector3.Dot(vector2, rhs2);
			if (num3 >= 0f && num4 <= num3)
			{
				return b;
			}
			float num5 = num * num4 - num3 * num2;
			if (num >= 0f && num3 <= 0f && num5 <= 0f)
			{
				float d = num / (num - num3);
				return a + vector * d;
			}
			Vector3 rhs3 = p - c;
			float num6 = Vector3.Dot(vector, rhs3);
			float num7 = Vector3.Dot(vector2, rhs3);
			if (num7 >= 0f && num6 <= num7)
			{
				return c;
			}
			float num8 = num6 * num2 - num * num7;
			if (num2 >= 0f && num7 <= 0f && num8 <= 0f)
			{
				float d2 = num2 / (num2 - num7);
				return a + vector2 * d2;
			}
			float num9 = num3 * num7 - num6 * num4;
			if (num4 - num3 >= 0f && num6 - num7 >= 0f && num9 <= 0f)
			{
				float d3 = (num4 - num3) / (num4 - num3 + (num6 - num7));
				return b + (c - b) * d3;
			}
			float num10 = 1f / (num9 + num8 + num5);
			float d4 = num8 * num10;
			float d5 = num5 * num10;
			return a + vector * d4 + vector2 * d5;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B992 File Offset: 0x00009D92
		[Obsolete("Use VectorMath.SqrDistanceSegmentSegment instead")]
		public static float DistanceSegmentSegment3D(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
		{
			return VectorMath.SqrDistanceSegmentSegment(s1, e1, s2, e2);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B9A0 File Offset: 0x00009DA0
		public static void CompressMesh(List<Int3> vertices, List<int> triangles, out Int3[] outVertices, out int[] outTriangles)
		{
			Dictionary<Int3, int> dictionary = Polygon.cached_Int3_int_dict;
			dictionary.Clear();
			int[] array = ArrayPool<int>.Claim(vertices.Count);
			int num = 0;
			for (int i = 0; i < vertices.Count; i++)
			{
				int num2;
				if (!dictionary.TryGetValue(vertices[i], out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, 1, 0), out num2) && !dictionary.TryGetValue(vertices[i] + new Int3(0, -1, 0), out num2))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = num2;
				}
			}
			outTriangles = new int[triangles.Count];
			for (int j = 0; j < outTriangles.Length; j++)
			{
				outTriangles[j] = array[triangles[j]];
			}
			outVertices = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				outVertices[k] = vertices[k];
			}
			ArrayPool<int>.Release(ref array, false);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BAD4 File Offset: 0x00009ED4
		public static void TraceContours(Dictionary<int, int> outline, HashSet<int> hasInEdge, Action<List<int>, bool> results)
		{
			List<int> list = ListPool<int>.Claim();
			List<int> list2 = ListPool<int>.Claim();
			list2.AddRange(outline.Keys);
			for (int i = 0; i <= 1; i++)
			{
				bool flag = i == 1;
				for (int j = 0; j < list2.Count; j++)
				{
					int num = list2[j];
					if (flag || !hasInEdge.Contains(num))
					{
						int num2 = num;
						list.Clear();
						list.Add(num2);
						while (outline.ContainsKey(num2))
						{
							int num3 = outline[num2];
							outline.Remove(num2);
							list.Add(num3);
							if (num3 == num)
							{
								break;
							}
							num2 = num3;
						}
						if (list.Count > 1)
						{
							results(list, flag);
						}
					}
				}
			}
			ListPool<int>.Release(ref list2);
			ListPool<int>.Release(ref list);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BBC0 File Offset: 0x00009FC0
		public static void Subdivide(List<Vector3> points, List<Vector3> result, int subSegments)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				for (int j = 0; j < subSegments; j++)
				{
					result.Add(Vector3.Lerp(points[i], points[i + 1], (float)j / (float)subSegments));
				}
			}
			result.Add(points[points.Count - 1]);
		}

		// Token: 0x0400011F RID: 287
		private static readonly Dictionary<Int3, int> cached_Int3_int_dict = new Dictionary<Int3, int>();
	}
}
