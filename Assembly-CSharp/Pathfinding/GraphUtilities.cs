using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000043 RID: 67
	public static class GraphUtilities
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0001284C File Offset: 0x00010C4C
		public static List<Vector3> GetContours(NavGraph graph)
		{
			List<Vector3> result = ListPool<Vector3>.Claim();
			if (graph is INavmesh)
			{
				GraphUtilities.GetContours(graph as INavmesh, delegate(List<Int3> vertices, bool cycle)
				{
					int index = (!cycle) ? 0 : (vertices.Count - 1);
					for (int i = 0; i < vertices.Count; i++)
					{
						result.Add((Vector3)vertices[index]);
						result.Add((Vector3)vertices[i]);
						index = i;
					}
				});
			}
			else if (graph is GridGraph)
			{
				GraphUtilities.GetContours(graph as GridGraph, delegate(Vector3[] vertices)
				{
					int num = vertices.Length - 1;
					for (int i = 0; i < vertices.Length; i++)
					{
						result.Add(vertices[num]);
						result.Add(vertices[i]);
						num = i;
					}
				}, 0f, null);
			}
			return result;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000128C0 File Offset: 0x00010CC0
		public static void GetContours(INavmesh navmesh, Action<List<Int3>, bool> results)
		{
			bool[] uses = new bool[3];
			Dictionary<int, int> outline = new Dictionary<int, int>();
			Dictionary<int, Int3> vertexPositions = new Dictionary<int, Int3>();
			HashSet<int> hasInEdge = new HashSet<int>();
			navmesh.GetNodes(delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				uses[0] = (uses[1] = (uses[2] = false));
				if (triangleMeshNode != null)
				{
					for (int i = 0; i < triangleMeshNode.connections.Length; i++)
					{
						TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[i].node as TriangleMeshNode;
						if (triangleMeshNode2 != null)
						{
							int num = triangleMeshNode.SharedEdge(triangleMeshNode2);
							if (num != -1)
							{
								uses[num] = true;
							}
						}
					}
					for (int j = 0; j < 3; j++)
					{
						if (!uses[j])
						{
							int i2 = j;
							int i3 = (j + 1) % triangleMeshNode.GetVertexCount();
							outline[triangleMeshNode.GetVertexIndex(i2)] = triangleMeshNode.GetVertexIndex(i3);
							hasInEdge.Add(triangleMeshNode.GetVertexIndex(i3));
							vertexPositions[triangleMeshNode.GetVertexIndex(i2)] = triangleMeshNode.GetVertex(i2);
							vertexPositions[triangleMeshNode.GetVertexIndex(i3)] = triangleMeshNode.GetVertex(i3);
						}
					}
				}
			});
			Polygon.TraceContours(outline, hasInEdge, delegate(List<int> chain, bool cycle)
			{
				List<Int3> list = ListPool<Int3>.Claim();
				for (int i = 0; i < chain.Count; i++)
				{
					list.Add(vertexPositions[chain[i]]);
				}
				results(list, cycle);
			});
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00012938 File Offset: 0x00010D38
		public static void GetContours(GridGraph grid, Action<Vector3[]> callback, float yMergeThreshold, GridNodeBase[] nodes = null)
		{
			HashSet<GridNodeBase> hashSet = (nodes == null) ? null : new HashSet<GridNodeBase>(nodes);
			if (grid is LayerGridGraph)
			{
				nodes = (nodes ?? (grid as LayerGridGraph).nodes);
			}
			nodes = (nodes ?? grid.nodes);
			int[] neighbourXOffsets = grid.neighbourXOffsets;
			int[] neighbourZOffsets = grid.neighbourZOffsets;
			int[] array;
			if (grid.neighbours == NumNeighbours.Six)
			{
				array = GridGraph.hexagonNeighbourIndices;
			}
			else
			{
				RuntimeHelpers.InitializeArray(array = new int[4], fieldof(<PrivateImplementationDetails>.$field-02E4414E7DFA0F3AA2387EE8EA7AB31431CB406A).FieldHandle);
			}
			int[] array2 = array;
			float num = (grid.neighbours != NumNeighbours.Six) ? 0.5f : 0.333333343f;
			if (nodes != null)
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				HashSet<int> hashSet2 = new HashSet<int>();
				foreach (GridNodeBase gridNodeBase in nodes)
				{
					if (gridNodeBase != null && gridNodeBase.Walkable && (!gridNodeBase.HasConnectionsToAllEightNeighbours || hashSet != null))
					{
						for (int j = 0; j < array2.Length; j++)
						{
							int num2 = gridNodeBase.NodeIndex << 4 | j;
							GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(array2[j]);
							if ((neighbourAlongDirection == null || (hashSet != null && !hashSet.Contains(neighbourAlongDirection))) && !hashSet2.Contains(num2))
							{
								list.ClearFast<Vector3>();
								int num3 = j;
								GridNodeBase gridNodeBase2 = gridNodeBase;
								for (;;)
								{
									int num4 = gridNodeBase2.NodeIndex << 4 | num3;
									if (num4 == num2 && list.Count > 0)
									{
										break;
									}
									hashSet2.Add(num4);
									GridNodeBase neighbourAlongDirection2 = gridNodeBase2.GetNeighbourAlongDirection(array2[num3]);
									if (neighbourAlongDirection2 == null || (hashSet != null && !hashSet.Contains(neighbourAlongDirection2)))
									{
										int num5 = array2[num3];
										num3 = (num3 + 1) % array2.Length;
										int num6 = array2[num3];
										Vector3 vector = new Vector3((float)gridNodeBase2.XCoordinateInGrid + 0.5f, 0f, (float)gridNodeBase2.ZCoordinateInGrid + 0.5f);
										vector.x += (float)(neighbourXOffsets[num5] + neighbourXOffsets[num6]) * num;
										vector.z += (float)(neighbourZOffsets[num5] + neighbourZOffsets[num6]) * num;
										vector.y = grid.transform.InverseTransform((Vector3)gridNodeBase2.position).y;
										if (list.Count >= 2)
										{
											Vector3 b = list[list.Count - 2];
											Vector3 a = list[list.Count - 1];
											Vector3 vector2 = a - b;
											Vector3 vector3 = vector - b;
											if (((Mathf.Abs(vector2.x) > 0.01f || Mathf.Abs(vector3.x) > 0.01f) && (Mathf.Abs(vector2.z) > 0.01f || Mathf.Abs(vector3.z) > 0.01f)) || Mathf.Abs(vector2.y) > yMergeThreshold || Mathf.Abs(vector3.y) > yMergeThreshold)
											{
												list.Add(vector);
											}
											else
											{
												list[list.Count - 1] = vector;
											}
										}
										else
										{
											list.Add(vector);
										}
									}
									else
									{
										gridNodeBase2 = neighbourAlongDirection2;
										num3 = (num3 + array2.Length / 2 + 1) % array2.Length;
									}
								}
								Vector3[] array3 = list.ToArray();
								grid.transform.Transform(array3);
								callback(array3);
							}
						}
					}
				}
				ListPool<Vector3>.Release(ref list);
			}
		}
	}
}
