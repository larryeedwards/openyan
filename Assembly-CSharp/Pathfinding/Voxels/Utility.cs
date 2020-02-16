using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Voxels
{
	// Token: 0x020000DD RID: 221
	public class Utility
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x00042043 File Offset: 0x00040443
		public static float Min(float a, float b, float c)
		{
			a = ((a >= b) ? b : a);
			return (a >= c) ? c : a;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00042063 File Offset: 0x00040463
		public static float Max(float a, float b, float c)
		{
			a = ((a <= b) ? b : a);
			return (a <= c) ? c : a;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00042083 File Offset: 0x00040483
		public static int Max(int a, int b, int c, int d)
		{
			a = ((a <= b) ? b : a);
			a = ((a <= c) ? c : a);
			return (a <= d) ? d : a;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000420B3 File Offset: 0x000404B3
		public static int Min(int a, int b, int c, int d)
		{
			a = ((a >= b) ? b : a);
			a = ((a >= c) ? c : a);
			return (a >= d) ? d : a;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000420E3 File Offset: 0x000404E3
		public static float Max(float a, float b, float c, float d)
		{
			a = ((a <= b) ? b : a);
			a = ((a <= c) ? c : a);
			return (a <= d) ? d : a;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00042113 File Offset: 0x00040513
		public static float Min(float a, float b, float c, float d)
		{
			a = ((a >= b) ? b : a);
			a = ((a >= c) ? c : a);
			return (a >= d) ? d : a;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00042143 File Offset: 0x00040543
		public static void CopyVector(float[] a, int i, Vector3 v)
		{
			a[i] = v.x;
			a[i + 1] = v.y;
			a[i + 2] = v.z;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00042168 File Offset: 0x00040568
		public static Int3[] RemoveDuplicateVertices(Int3[] vertices, int[] triangles)
		{
			Dictionary<Int3, int> dictionary = ObjectPoolSimple<Dictionary<Int3, int>>.Claim();
			dictionary.Clear();
			int[] array = new int[vertices.Length];
			int num = 0;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (!dictionary.ContainsKey(vertices[i]))
				{
					dictionary.Add(vertices[i], num);
					array[i] = num;
					vertices[num] = vertices[i];
					num++;
				}
				else
				{
					array[i] = dictionary[vertices[i]];
				}
			}
			dictionary.Clear();
			ObjectPoolSimple<Dictionary<Int3, int>>.Release(ref dictionary);
			for (int j = 0; j < triangles.Length; j++)
			{
				triangles[j] = array[triangles[j]];
			}
			Int3[] array2 = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				array2[k] = vertices[k];
			}
			return array2;
		}
	}
}
