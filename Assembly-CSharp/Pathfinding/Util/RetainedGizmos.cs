using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x02000124 RID: 292
	public class RetainedGizmos
	{
		// Token: 0x06000A66 RID: 2662 RVA: 0x0004FF18 File Offset: 0x0004E318
		public GraphGizmoHelper GetSingleFrameGizmoHelper(AstarPath active)
		{
			RetainedGizmos.Hasher hasher = default(RetainedGizmos.Hasher);
			hasher.AddHash(Time.realtimeSinceStartup.GetHashCode());
			this.Draw(hasher);
			return this.GetGizmoHelper(active, hasher);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0004FF58 File Offset: 0x0004E358
		public GraphGizmoHelper GetGizmoHelper(AstarPath active, RetainedGizmos.Hasher hasher)
		{
			GraphGizmoHelper graphGizmoHelper = ObjectPool<GraphGizmoHelper>.Claim();
			graphGizmoHelper.Init(active, hasher, this);
			return graphGizmoHelper;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0004FF75 File Offset: 0x0004E375
		private void PoolMesh(Mesh mesh)
		{
			mesh.Clear();
			this.cachedMeshes.Push(mesh);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0004FF8C File Offset: 0x0004E38C
		private Mesh GetMesh()
		{
			if (this.cachedMeshes.Count > 0)
			{
				return this.cachedMeshes.Pop();
			}
			return new Mesh
			{
				hideFlags = HideFlags.DontSave
			};
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0004FFC5 File Offset: 0x0004E3C5
		public bool HasCachedMesh(RetainedGizmos.Hasher hasher)
		{
			return this.existingHashes.Contains(hasher.Hash);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0004FFD9 File Offset: 0x0004E3D9
		public bool Draw(RetainedGizmos.Hasher hasher)
		{
			this.usedHashes.Add(hasher.Hash);
			return this.HasCachedMesh(hasher);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0004FFF8 File Offset: 0x0004E3F8
		public void DrawExisting()
		{
			for (int i = 0; i < this.meshes.Count; i++)
			{
				this.usedHashes.Add(this.meshes[i].hash);
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00050044 File Offset: 0x0004E444
		public void FinalizeDraw()
		{
			this.RemoveUnusedMeshes(this.meshes);
			Camera current = Camera.current;
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(current);
			if (this.surfaceMaterial == null || this.lineMaterial == null)
			{
				return;
			}
			for (int i = 0; i <= 1; i++)
			{
				Material material = (i != 0) ? this.lineMaterial : this.surfaceMaterial;
				for (int j = 0; j < material.passCount; j++)
				{
					material.SetPass(j);
					for (int k = 0; k < this.meshes.Count; k++)
					{
						if (this.meshes[k].lines == (material == this.lineMaterial) && GeometryUtility.TestPlanesAABB(planes, this.meshes[k].mesh.bounds))
						{
							Graphics.DrawMeshNow(this.meshes[k].mesh, Matrix4x4.identity);
						}
					}
				}
			}
			this.usedHashes.Clear();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00050173 File Offset: 0x0004E573
		public void ClearCache()
		{
			this.usedHashes.Clear();
			this.RemoveUnusedMeshes(this.meshes);
			while (this.cachedMeshes.Count > 0)
			{
				UnityEngine.Object.DestroyImmediate(this.cachedMeshes.Pop());
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000501B4 File Offset: 0x0004E5B4
		private void RemoveUnusedMeshes(List<RetainedGizmos.MeshWithHash> meshList)
		{
			int i = 0;
			int num = 0;
			while (i < meshList.Count)
			{
				if (num == meshList.Count)
				{
					num--;
					meshList.RemoveAt(num);
				}
				else if (this.usedHashes.Contains(meshList[num].hash))
				{
					meshList[i] = meshList[num];
					i++;
					num++;
				}
				else
				{
					this.PoolMesh(meshList[num].mesh);
					this.existingHashes.Remove(meshList[num].hash);
					num++;
				}
			}
		}

		// Token: 0x04000720 RID: 1824
		private List<RetainedGizmos.MeshWithHash> meshes = new List<RetainedGizmos.MeshWithHash>();

		// Token: 0x04000721 RID: 1825
		private HashSet<ulong> usedHashes = new HashSet<ulong>();

		// Token: 0x04000722 RID: 1826
		private HashSet<ulong> existingHashes = new HashSet<ulong>();

		// Token: 0x04000723 RID: 1827
		private Stack<Mesh> cachedMeshes = new Stack<Mesh>();

		// Token: 0x04000724 RID: 1828
		public Material surfaceMaterial;

		// Token: 0x04000725 RID: 1829
		public Material lineMaterial;

		// Token: 0x02000125 RID: 293
		public struct Hasher
		{
			// Token: 0x06000A70 RID: 2672 RVA: 0x00050264 File Offset: 0x0004E664
			public Hasher(AstarPath active)
			{
				this.hash = 0UL;
				this.debugData = active.debugPathData;
				this.includePathSearchInfo = (this.debugData != null && (active.debugMode == GraphDebugMode.F || active.debugMode == GraphDebugMode.G || active.debugMode == GraphDebugMode.H || active.showSearchTree));
				this.AddHash((int)active.debugMode);
				this.AddHash(active.debugFloor.GetHashCode());
				this.AddHash(active.debugRoof.GetHashCode());
			}

			// Token: 0x06000A71 RID: 2673 RVA: 0x00050300 File Offset: 0x0004E700
			public void AddHash(int hash)
			{
				this.hash = (1572869UL * this.hash ^ (ulong)((long)hash));
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x00050318 File Offset: 0x0004E718
			public void HashNode(GraphNode node)
			{
				this.AddHash(node.GetGizmoHashCode());
				if (this.includePathSearchInfo)
				{
					PathNode pathNode = this.debugData.GetPathNode(node.NodeIndex);
					this.AddHash((int)pathNode.pathID);
					this.AddHash((pathNode.pathID != this.debugData.PathID) ? 0 : 1);
					this.AddHash((int)pathNode.F);
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00050389 File Offset: 0x0004E789
			public ulong Hash
			{
				get
				{
					return this.hash;
				}
			}

			// Token: 0x04000726 RID: 1830
			private ulong hash;

			// Token: 0x04000727 RID: 1831
			private bool includePathSearchInfo;

			// Token: 0x04000728 RID: 1832
			private PathHandler debugData;
		}

		// Token: 0x02000126 RID: 294
		public class Builder : IAstarPooledObject
		{
			// Token: 0x06000A75 RID: 2677 RVA: 0x000503BC File Offset: 0x0004E7BC
			public void DrawMesh(RetainedGizmos gizmos, Vector3[] vertices, List<int> triangles, Color[] colors)
			{
				Mesh mesh = gizmos.GetMesh();
				mesh.vertices = vertices;
				mesh.SetTriangles(triangles, 0);
				mesh.colors = colors;
				mesh.UploadMeshData(true);
				this.meshes.Add(mesh);
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x000503FC File Offset: 0x0004E7FC
			public void DrawWireCube(GraphTransform tr, Bounds bounds, Color color)
			{
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, min.z)), tr.Transform(new Vector3(max.x, min.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, min.z)), tr.Transform(new Vector3(max.x, min.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, max.z)), tr.Transform(new Vector3(min.x, min.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, max.z)), tr.Transform(new Vector3(min.x, min.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, max.y, min.z)), tr.Transform(new Vector3(max.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, max.y, min.z)), tr.Transform(new Vector3(max.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, max.y, max.z)), tr.Transform(new Vector3(min.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, max.y, max.z)), tr.Transform(new Vector3(min.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, min.z)), tr.Transform(new Vector3(min.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, min.z)), tr.Transform(new Vector3(max.x, max.y, min.z)), color);
				this.DrawLine(tr.Transform(new Vector3(max.x, min.y, max.z)), tr.Transform(new Vector3(max.x, max.y, max.z)), color);
				this.DrawLine(tr.Transform(new Vector3(min.x, min.y, max.z)), tr.Transform(new Vector3(min.x, max.y, max.z)), color);
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x00050770 File Offset: 0x0004EB70
			public void DrawLine(Vector3 start, Vector3 end, Color color)
			{
				this.lines.Add(start);
				this.lines.Add(end);
				Color32 item = color;
				this.lineColors.Add(item);
				this.lineColors.Add(item);
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x000507B4 File Offset: 0x0004EBB4
			public void Submit(RetainedGizmos gizmos, RetainedGizmos.Hasher hasher)
			{
				this.SubmitLines(gizmos, hasher.Hash);
				this.SubmitMeshes(gizmos, hasher.Hash);
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x000507D4 File Offset: 0x0004EBD4
			private void SubmitMeshes(RetainedGizmos gizmos, ulong hash)
			{
				for (int i = 0; i < this.meshes.Count; i++)
				{
					gizmos.meshes.Add(new RetainedGizmos.MeshWithHash
					{
						hash = hash,
						mesh = this.meshes[i],
						lines = false
					});
					gizmos.existingHashes.Add(hash);
				}
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x00050844 File Offset: 0x0004EC44
			private void SubmitLines(RetainedGizmos gizmos, ulong hash)
			{
				int num = (this.lines.Count + 32766 - 1) / 32766;
				for (int i = 0; i < num; i++)
				{
					int num2 = 32766 * i;
					int num3 = Mathf.Min(num2 + 32766, this.lines.Count);
					int num4 = num3 - num2;
					List<Vector3> list = ListPool<Vector3>.Claim(num4 * 2);
					List<Color32> list2 = ListPool<Color32>.Claim(num4 * 2);
					List<Vector3> list3 = ListPool<Vector3>.Claim(num4 * 2);
					List<Vector2> list4 = ListPool<Vector2>.Claim(num4 * 2);
					List<int> list5 = ListPool<int>.Claim(num4 * 3);
					for (int j = num2; j < num3; j++)
					{
						Vector3 item = this.lines[j];
						list.Add(item);
						list.Add(item);
						Color32 item2 = this.lineColors[j];
						list2.Add(item2);
						list2.Add(item2);
						list4.Add(new Vector2(0f, 0f));
						list4.Add(new Vector2(1f, 0f));
					}
					for (int k = num2; k < num3; k += 2)
					{
						Vector3 item3 = this.lines[k + 1] - this.lines[k];
						list3.Add(item3);
						list3.Add(item3);
						list3.Add(item3);
						list3.Add(item3);
					}
					int l = 0;
					int num5 = 0;
					while (l < num4 * 3)
					{
						list5.Add(num5);
						list5.Add(num5 + 1);
						list5.Add(num5 + 2);
						list5.Add(num5 + 1);
						list5.Add(num5 + 3);
						list5.Add(num5 + 2);
						l += 6;
						num5 += 4;
					}
					Mesh mesh = gizmos.GetMesh();
					mesh.SetVertices(list);
					mesh.SetTriangles(list5, 0);
					mesh.SetColors(list2);
					mesh.SetNormals(list3);
					mesh.SetUVs(0, list4);
					mesh.UploadMeshData(true);
					ListPool<Vector3>.Release(ref list);
					ListPool<Color32>.Release(ref list2);
					ListPool<Vector3>.Release(ref list3);
					ListPool<Vector2>.Release(ref list4);
					ListPool<int>.Release(ref list5);
					gizmos.meshes.Add(new RetainedGizmos.MeshWithHash
					{
						hash = hash,
						mesh = mesh,
						lines = true
					});
					gizmos.existingHashes.Add(hash);
				}
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x00050AB9 File Offset: 0x0004EEB9
			void IAstarPooledObject.OnEnterPool()
			{
				this.lines.Clear();
				this.lineColors.Clear();
				this.meshes.Clear();
			}

			// Token: 0x04000729 RID: 1833
			private List<Vector3> lines = new List<Vector3>();

			// Token: 0x0400072A RID: 1834
			private List<Color32> lineColors = new List<Color32>();

			// Token: 0x0400072B RID: 1835
			private List<Mesh> meshes = new List<Mesh>();
		}

		// Token: 0x02000127 RID: 295
		private struct MeshWithHash
		{
			// Token: 0x0400072C RID: 1836
			public ulong hash;

			// Token: 0x0400072D RID: 1837
			public Mesh mesh;

			// Token: 0x0400072E RID: 1838
			public bool lines;
		}
	}
}
