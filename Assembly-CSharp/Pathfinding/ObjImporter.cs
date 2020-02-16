using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000BF RID: 191
	public class ObjImporter
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x0003762C File Offset: 0x00035A2C
		public static Mesh ImportFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Debug.LogError("No file was found at '" + filePath + "'");
				return null;
			}
			ObjImporter.meshStruct meshStruct = ObjImporter.createMeshStruct(filePath);
			ObjImporter.populateMeshStruct(ref meshStruct);
			Vector3[] array = new Vector3[meshStruct.faceData.Length];
			Vector2[] array2 = new Vector2[meshStruct.faceData.Length];
			Vector3[] array3 = new Vector3[meshStruct.faceData.Length];
			int num = 0;
			foreach (Vector3 vector in meshStruct.faceData)
			{
				array[num] = meshStruct.vertices[(int)vector.x - 1];
				if (vector.y >= 1f)
				{
					array2[num] = meshStruct.uv[(int)vector.y - 1];
				}
				if (vector.z >= 1f)
				{
					array3[num] = meshStruct.normals[(int)vector.z - 1];
				}
				num++;
			}
			Mesh mesh = new Mesh();
			mesh.vertices = array;
			mesh.uv = array2;
			mesh.normals = array3;
			mesh.triangles = meshStruct.triangles;
			mesh.RecalculateBounds();
			return mesh;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000377A4 File Offset: 0x00035BA4
		private static ObjImporter.meshStruct createMeshStruct(string filename)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			ObjImporter.meshStruct result = default(ObjImporter.meshStruct);
			result.fileName = filename;
			StreamReader streamReader = File.OpenText(filename);
			string s = streamReader.ReadToEnd();
			streamReader.Dispose();
			using (StringReader stringReader = new StringReader(s))
			{
				string text = stringReader.ReadLine();
				char[] separator = new char[]
				{
					' '
				};
				while (text != null)
				{
					if (!text.StartsWith("f ") && !text.StartsWith("v ") && !text.StartsWith("vt ") && !text.StartsWith("vn "))
					{
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
					else
					{
						text = text.Trim();
						string[] array = text.Split(separator, 50);
						string text2 = array[0];
						if (text2 != null)
						{
							if (!(text2 == "v"))
							{
								if (!(text2 == "vt"))
								{
									if (!(text2 == "vn"))
									{
										if (text2 == "f")
										{
											num5 = num5 + array.Length - 1;
											num += 3 * (array.Length - 2);
										}
									}
									else
									{
										num4++;
									}
								}
								else
								{
									num3++;
								}
							}
							else
							{
								num2++;
							}
						}
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
				}
			}
			result.triangles = new int[num];
			result.vertices = new Vector3[num2];
			result.uv = new Vector2[num3];
			result.normals = new Vector3[num4];
			result.faceData = new Vector3[num5];
			return result;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000379B4 File Offset: 0x00035DB4
		private static void populateMeshStruct(ref ObjImporter.meshStruct mesh)
		{
			StreamReader streamReader = File.OpenText(mesh.fileName);
			string s = streamReader.ReadToEnd();
			streamReader.Close();
			using (StringReader stringReader = new StringReader(s))
			{
				string text = stringReader.ReadLine();
				char[] separator = new char[]
				{
					' '
				};
				char[] separator2 = new char[]
				{
					'/'
				};
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				while (text != null)
				{
					if (!text.StartsWith("f ") && !text.StartsWith("v ") && !text.StartsWith("vt ") && !text.StartsWith("vn ") && !text.StartsWith("g ") && !text.StartsWith("usemtl ") && !text.StartsWith("mtllib ") && !text.StartsWith("vt1 ") && !text.StartsWith("vt2 ") && !text.StartsWith("vc ") && !text.StartsWith("usemap "))
					{
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
					else
					{
						text = text.Trim();
						string[] array = text.Split(separator, 50);
						string text2 = array[0];
						switch (text2)
						{
						case "v":
							mesh.vertices[num3] = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
							num3++;
							break;
						case "vt":
							mesh.uv[num5] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
							num5++;
							break;
						case "vt1":
							mesh.uv[num6] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
							num6++;
							break;
						case "vt2":
							mesh.uv[num7] = new Vector2(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
							num7++;
							break;
						case "vn":
							mesh.normals[num4] = new Vector3(Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
							num4++;
							break;
						case "f":
						{
							int num9 = 1;
							List<int> list = new List<int>();
							while (num9 < array.Length && (string.Empty + array[num9]).Length > 0)
							{
								Vector3 vector = default(Vector3);
								string[] array2 = array[num9].Split(separator2, 3);
								vector.x = (float)Convert.ToInt32(array2[0]);
								if (array2.Length > 1)
								{
									if (array2[1] != string.Empty)
									{
										vector.y = (float)Convert.ToInt32(array2[1]);
									}
									vector.z = (float)Convert.ToInt32(array2[2]);
								}
								num9++;
								mesh.faceData[num2] = vector;
								list.Add(num2);
								num2++;
							}
							num9 = 1;
							while (num9 + 2 < array.Length)
							{
								mesh.triangles[num] = list[0];
								num++;
								mesh.triangles[num] = list[num9];
								num++;
								mesh.triangles[num] = list[num9 + 1];
								num++;
								num9++;
							}
							break;
						}
						}
						text = stringReader.ReadLine();
						if (text != null)
						{
							text = text.Replace("  ", " ");
						}
					}
				}
			}
		}

		// Token: 0x020000C0 RID: 192
		private struct meshStruct
		{
			// Token: 0x0400051E RID: 1310
			public Vector3[] vertices;

			// Token: 0x0400051F RID: 1311
			public Vector3[] normals;

			// Token: 0x04000520 RID: 1312
			public Vector2[] uv;

			// Token: 0x04000521 RID: 1313
			public Vector2[] uv1;

			// Token: 0x04000522 RID: 1314
			public Vector2[] uv2;

			// Token: 0x04000523 RID: 1315
			public int[] triangles;

			// Token: 0x04000524 RID: 1316
			public int[] faceVerts;

			// Token: 0x04000525 RID: 1317
			public int[] faceUVs;

			// Token: 0x04000526 RID: 1318
			public Vector3[] faceData;

			// Token: 0x04000527 RID: 1319
			public string name;

			// Token: 0x04000528 RID: 1320
			public string fileName;
		}
	}
}
