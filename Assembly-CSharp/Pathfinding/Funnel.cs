using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200011D RID: 285
	public class Funnel
	{
		// Token: 0x06000A32 RID: 2610 RVA: 0x0004E028 File Offset: 0x0004C428
		public static List<Funnel.PathPart> SplitIntoParts(Path path)
		{
			List<GraphNode> path2 = path.path;
			List<Funnel.PathPart> list = ListPool<Funnel.PathPart>.Claim();
			if (path2 == null || path2.Count == 0)
			{
				return list;
			}
			for (int i = 0; i < path2.Count; i++)
			{
				if (path2[i] is TriangleMeshNode || path2[i] is GridNodeBase)
				{
					Funnel.PathPart item = default(Funnel.PathPart);
					item.startIndex = i;
					uint graphIndex = path2[i].GraphIndex;
					while (i < path2.Count)
					{
						if (path2[i].GraphIndex != graphIndex && !(path2[i] is NodeLink3Node))
						{
							break;
						}
						i++;
					}
					i--;
					item.endIndex = i;
					if (item.startIndex == 0)
					{
						item.startPoint = path.vectorPath[0];
					}
					else
					{
						item.startPoint = (Vector3)path2[item.startIndex - 1].position;
					}
					if (item.endIndex == path2.Count - 1)
					{
						item.endPoint = path.vectorPath[path.vectorPath.Count - 1];
					}
					else
					{
						item.endPoint = (Vector3)path2[item.endIndex + 1].position;
					}
					list.Add(item);
				}
				else
				{
					if (!(NodeLink2.GetNodeLink(path2[i]) != null))
					{
						throw new Exception("Unsupported node type or null node");
					}
					Funnel.PathPart item2 = default(Funnel.PathPart);
					item2.startIndex = i;
					uint graphIndex2 = path2[i].GraphIndex;
					for (i++; i < path2.Count; i++)
					{
						if (path2[i].GraphIndex != graphIndex2)
						{
							break;
						}
					}
					i--;
					if (i - item2.startIndex != 0)
					{
						if (i - item2.startIndex != 1)
						{
							throw new Exception("NodeLink2 link length greater than two (2) nodes. " + (i - item2.startIndex + 1));
						}
						item2.endIndex = i;
						item2.isLink = true;
						item2.startPoint = (Vector3)path2[item2.startIndex].position;
						item2.endPoint = (Vector3)path2[item2.endIndex].position;
						list.Add(item2);
					}
				}
			}
			return list;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0004E2B0 File Offset: 0x0004C6B0
		public static Funnel.FunnelPortals ConstructFunnelPortals(List<GraphNode> nodes, Funnel.PathPart part)
		{
			if (nodes == null || nodes.Count == 0)
			{
				return new Funnel.FunnelPortals
				{
					left = ListPool<Vector3>.Claim(0),
					right = ListPool<Vector3>.Claim(0)
				};
			}
			if (part.endIndex < part.startIndex || part.startIndex < 0 || part.endIndex > nodes.Count)
			{
				throw new ArgumentOutOfRangeException();
			}
			List<Vector3> list = ListPool<Vector3>.Claim(nodes.Count + 1);
			List<Vector3> list2 = ListPool<Vector3>.Claim(nodes.Count + 1);
			list.Add(part.startPoint);
			list2.Add(part.startPoint);
			for (int i = part.startIndex; i < part.endIndex; i++)
			{
				if (!nodes[i].GetPortal(nodes[i + 1], list, list2, false))
				{
					list.Add((Vector3)nodes[i].position);
					list2.Add((Vector3)nodes[i].position);
					list.Add((Vector3)nodes[i + 1].position);
					list2.Add((Vector3)nodes[i + 1].position);
				}
			}
			list.Add(part.endPoint);
			list2.Add(part.endPoint);
			return new Funnel.FunnelPortals
			{
				left = list,
				right = list2
			};
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0004E434 File Offset: 0x0004C834
		public static void ShrinkPortals(Funnel.FunnelPortals portals, float shrink)
		{
			if (shrink <= 1E-05f)
			{
				return;
			}
			for (int i = 0; i < portals.left.Count; i++)
			{
				Vector3 a = portals.left[i];
				Vector3 b = portals.right[i];
				float magnitude = (a - b).magnitude;
				if (magnitude > 0f)
				{
					float num = Mathf.Min(shrink / magnitude, 0.4f);
					portals.left[i] = Vector3.Lerp(a, b, num);
					portals.right[i] = Vector3.Lerp(a, b, 1f - num);
				}
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0004E4E4 File Offset: 0x0004C8E4
		private static bool UnwrapHelper(Vector3 portalStart, Vector3 portalEnd, Vector3 prevPoint, Vector3 nextPoint, ref Quaternion mRot, ref Vector3 mOffset)
		{
			if (VectorMath.IsColinear(portalStart, portalEnd, nextPoint))
			{
				return false;
			}
			Vector3 vector = portalEnd - portalStart;
			float sqrMagnitude = vector.sqrMagnitude;
			prevPoint -= Vector3.Dot(prevPoint - portalStart, vector) / sqrMagnitude * vector;
			nextPoint -= Vector3.Dot(nextPoint - portalStart, vector) / sqrMagnitude * vector;
			Quaternion quaternion = Quaternion.FromToRotation(nextPoint - portalStart, portalStart - prevPoint);
			mOffset += mRot * (portalStart - quaternion * portalStart);
			mRot *= quaternion;
			return true;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0004E5A0 File Offset: 0x0004C9A0
		public static void Unwrap(Funnel.FunnelPortals funnel, Vector2[] left, Vector2[] right)
		{
			int num = 1;
			Vector3 fromDirection = Vector3.Cross(funnel.right[1] - funnel.left[0], funnel.left[1] - funnel.left[0]);
			while (fromDirection.sqrMagnitude <= 1E-08f && num + 1 < funnel.left.Count)
			{
				num++;
				fromDirection = Vector3.Cross(funnel.right[num] - funnel.left[0], funnel.left[num] - funnel.left[0]);
			}
			left[0] = (right[0] = Vector2.zero);
			Vector3 vector = funnel.left[1];
			Vector3 vector2 = funnel.right[1];
			Vector3 prevPoint = funnel.left[0];
			Quaternion rotation = Quaternion.FromToRotation(fromDirection, Vector3.forward);
			Vector3 b = rotation * -funnel.right[0];
			for (int i = 1; i < funnel.left.Count; i++)
			{
				if (Funnel.UnwrapHelper(vector, vector2, prevPoint, funnel.left[i], ref rotation, ref b))
				{
					prevPoint = vector;
					vector = funnel.left[i];
				}
				left[i] = rotation * funnel.left[i] + b;
				if (Funnel.UnwrapHelper(vector, vector2, prevPoint, funnel.right[i], ref rotation, ref b))
				{
					prevPoint = vector2;
					vector2 = funnel.right[i];
				}
				right[i] = rotation * funnel.right[i] + b;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0004E7C0 File Offset: 0x0004CBC0
		private static int FixFunnel(Vector2[] left, Vector2[] right, int numPortals)
		{
			if (numPortals > left.Length || numPortals > right.Length)
			{
				throw new ArgumentException("Arrays do not have as many elements as specified");
			}
			if (numPortals < 3)
			{
				return -1;
			}
			int num = 0;
			while (left[num + 1] == left[num + 2] && right[num + 1] == right[num + 2])
			{
				left[num + 1] = left[num];
				right[num + 1] = right[num];
				num++;
				if (numPortals - num < 3)
				{
					return -1;
				}
			}
			return num;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0004E88A File Offset: 0x0004CC8A
		protected static Vector2 ToXZ(Vector3 p)
		{
			return new Vector2(p.x, p.z);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0004E89F File Offset: 0x0004CC9F
		protected static Vector3 FromXZ(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0004E8B9 File Offset: 0x0004CCB9
		protected static bool RightOrColinear(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y <= 0f;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0004E8E4 File Offset: 0x0004CCE4
		protected static bool LeftOrColinear(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y >= 0f;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0004E910 File Offset: 0x0004CD10
		public static List<Vector3> Calculate(Funnel.FunnelPortals funnel, bool unwrap, bool splitAtEveryPortal)
		{
			if (funnel.left.Count != funnel.right.Count)
			{
				throw new ArgumentException("funnel.left.Count != funnel.right.Count");
			}
			Vector2[] array = ArrayPool<Vector2>.Claim(funnel.left.Count);
			Vector2[] array2 = ArrayPool<Vector2>.Claim(funnel.left.Count);
			if (unwrap)
			{
				Funnel.Unwrap(funnel, array, array2);
			}
			else
			{
				for (int i = 0; i < funnel.left.Count; i++)
				{
					array[i] = Funnel.ToXZ(funnel.left[i]);
					array2[i] = Funnel.ToXZ(funnel.right[i]);
				}
			}
			int num = Funnel.FixFunnel(array, array2, funnel.left.Count);
			List<int> list = ListPool<int>.Claim();
			if (num == -1)
			{
				list.Add(0);
				list.Add(funnel.left.Count - 1);
			}
			else
			{
				bool flag;
				Funnel.Calculate(array, array2, funnel.left.Count, num, list, int.MaxValue, out flag);
			}
			List<Vector3> list2 = ListPool<Vector3>.Claim(list.Count);
			Vector2 p = array[0];
			int num2 = 0;
			for (int j = 0; j < list.Count; j++)
			{
				int num3 = list[j];
				if (splitAtEveryPortal)
				{
					Vector2 vector = (num3 < 0) ? array2[-num3] : array[num3];
					for (int k = num2 + 1; k < Math.Abs(num3); k++)
					{
						float t = VectorMath.LineIntersectionFactorXZ(Funnel.FromXZ(array[k]), Funnel.FromXZ(array2[k]), Funnel.FromXZ(p), Funnel.FromXZ(vector));
						list2.Add(Vector3.Lerp(funnel.left[k], funnel.right[k], t));
					}
					num2 = Mathf.Abs(num3);
					p = vector;
				}
				if (num3 >= 0)
				{
					list2.Add(funnel.left[num3]);
				}
				else
				{
					list2.Add(funnel.right[-num3]);
				}
			}
			ListPool<int>.Release(ref list);
			ArrayPool<Vector2>.Release(ref array, false);
			ArrayPool<Vector2>.Release(ref array2, false);
			return list2;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0004EB8C File Offset: 0x0004CF8C
		private static void Calculate(Vector2[] left, Vector2[] right, int numPortals, int startIndex, List<int> funnelPath, int maxCorners, out bool lastCorner)
		{
			if (left.Length != right.Length)
			{
				throw new ArgumentException();
			}
			lastCorner = false;
			int num = startIndex + 1;
			int num2 = startIndex + 1;
			Vector2 vector = left[startIndex];
			Vector2 vector2 = left[num2];
			Vector2 vector3 = right[num];
			funnelPath.Add(startIndex);
			int i = startIndex + 2;
			while (i < numPortals)
			{
				if (funnelPath.Count >= maxCorners)
				{
					return;
				}
				if (funnelPath.Count > 2000)
				{
					Debug.LogWarning("Avoiding infinite loop. Remove this check if you have this long paths.");
					break;
				}
				Vector2 vector4 = left[i];
				Vector2 vector5 = right[i];
				if (!Funnel.LeftOrColinear(vector3 - vector, vector5 - vector))
				{
					goto IL_10E;
				}
				if (vector == vector3 || Funnel.RightOrColinear(vector2 - vector, vector5 - vector))
				{
					vector3 = vector5;
					num = i;
					goto IL_10E;
				}
				vector3 = (vector = vector2);
				funnelPath.Add(i = (num = num2));
				IL_176:
				i++;
				continue;
				IL_10E:
				if (!Funnel.RightOrColinear(vector2 - vector, vector4 - vector))
				{
					goto IL_176;
				}
				if (vector == vector2 || Funnel.LeftOrColinear(vector3 - vector, vector4 - vector))
				{
					vector2 = vector4;
					num2 = i;
					goto IL_176;
				}
				vector2 = (vector = vector3);
				funnelPath.Add(-(i = (num2 = num)));
				goto IL_176;
			}
			lastCorner = true;
			funnelPath.Add(numPortals - 1);
		}

		// Token: 0x0200011E RID: 286
		public struct FunnelPortals
		{
			// Token: 0x04000703 RID: 1795
			public List<Vector3> left;

			// Token: 0x04000704 RID: 1796
			public List<Vector3> right;
		}

		// Token: 0x0200011F RID: 287
		public struct PathPart
		{
			// Token: 0x04000705 RID: 1797
			public int startIndex;

			// Token: 0x04000706 RID: 1798
			public int endIndex;

			// Token: 0x04000707 RID: 1799
			public Vector3 startPoint;

			// Token: 0x04000708 RID: 1800
			public Vector3 endPoint;

			// Token: 0x04000709 RID: 1801
			public bool isLink;
		}
	}
}
