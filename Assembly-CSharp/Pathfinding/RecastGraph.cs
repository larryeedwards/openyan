using System;
using System.Collections.Generic;
using Pathfinding.Recast;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Pathfinding.Voxels;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000B2 RID: 178
	[JsonOptIn]
	public class RecastGraph : NavmeshBase, IUpdatableGraph
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00032ECC File Offset: 0x000312CC
		protected override bool RecalculateNormals
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00032ECF File Offset: 0x000312CF
		public override float TileWorldSizeX
		{
			get
			{
				return (float)this.tileSizeX * this.cellSize;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00032EDF File Offset: 0x000312DF
		public override float TileWorldSizeZ
		{
			get
			{
				return (float)this.tileSizeZ * this.cellSize;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00032EEF File Offset: 0x000312EF
		protected override float MaxTileConnectionEdgeDistance
		{
			get
			{
				return this.walkableClimb;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00032EF7 File Offset: 0x000312F7
		[Obsolete("Obsolete since this is not accurate when the graph is rotated (rotation was not supported when this property was created)")]
		public Bounds forcedBounds
		{
			get
			{
				return new Bounds(this.forcedBoundsCenter, this.forcedBoundsSize);
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00032F0A File Offset: 0x0003130A
		[Obsolete("Use node.ClosestPointOnNode instead")]
		public Vector3 ClosestPointOnNode(TriangleMeshNode node, Vector3 pos)
		{
			return node.ClosestPointOnNode(pos);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00032F13 File Offset: 0x00031313
		[Obsolete("Use node.ContainsPoint instead")]
		public bool ContainsPoint(TriangleMeshNode node, Vector3 pos)
		{
			return node.ContainsPoint((Int3)pos);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00032F24 File Offset: 0x00031324
		public void SnapForceBoundsToScene()
		{
			List<RasterizationMesh> list = this.CollectMeshes(new Bounds(Vector3.zero, new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity)));
			if (list.Count == 0)
			{
				return;
			}
			Bounds bounds = list[0].bounds;
			for (int i = 1; i < list.Count; i++)
			{
				bounds.Encapsulate(list[i].bounds);
				list[i].Pool();
			}
			this.forcedBoundsCenter = bounds.center;
			this.forcedBoundsSize = bounds.size;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00032FBF File Offset: 0x000313BF
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return (!o.updatePhysics) ? GraphUpdateThreading.SeparateThread : ((GraphUpdateThreading)7);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00032FD4 File Offset: 0x000313D4
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
			if (!o.updatePhysics)
			{
				return;
			}
			RelevantGraphSurface.UpdateAllPositions();
			IntRect touchingTiles = base.GetTouchingTiles(o.bounds);
			Bounds tileBounds = base.GetTileBounds(touchingTiles);
			tileBounds.Expand(new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f);
			List<RasterizationMesh> inputMeshes = this.CollectMeshes(tileBounds);
			if (this.globalVox == null)
			{
				this.globalVox = new Voxelize(this.CellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope, this.maxEdgeLength);
			}
			this.globalVox.inputMeshes = inputMeshes;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00033088 File Offset: 0x00031488
		void IUpdatableGraph.UpdateArea(GraphUpdateObject guo)
		{
			IntRect touchingTiles = base.GetTouchingTiles(guo.bounds);
			if (!guo.updatePhysics)
			{
				for (int i = touchingTiles.ymin; i <= touchingTiles.ymax; i++)
				{
					for (int j = touchingTiles.xmin; j <= touchingTiles.xmax; j++)
					{
						NavmeshTile graph = this.tiles[i * this.tileXCount + j];
						NavMeshGraph.UpdateArea(guo, graph);
					}
				}
				return;
			}
			Voxelize voxelize = this.globalVox;
			if (voxelize == null)
			{
				throw new InvalidOperationException("No Voxelizer object. UpdateAreaInit should have been called before this function.");
			}
			for (int k = touchingTiles.xmin; k <= touchingTiles.xmax; k++)
			{
				for (int l = touchingTiles.ymin; l <= touchingTiles.ymax; l++)
				{
					this.stagingTiles.Add(this.BuildTileMesh(voxelize, k, l, 0));
				}
			}
			uint graphIndex = (uint)AstarPath.active.data.GetGraphIndex(this);
			for (int m = 0; m < this.stagingTiles.Count; m++)
			{
				NavmeshTile navmeshTile = this.stagingTiles[m];
				GraphNode[] nodes = navmeshTile.nodes;
				for (int n = 0; n < nodes.Length; n++)
				{
					nodes[n].GraphIndex = graphIndex;
				}
			}
			for (int num = 0; num < voxelize.inputMeshes.Count; num++)
			{
				voxelize.inputMeshes[num].Pool();
			}
			ListPool<RasterizationMesh>.Release(ref voxelize.inputMeshes);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00033228 File Offset: 0x00031628
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject guo)
		{
			for (int i = 0; i < this.stagingTiles.Count; i++)
			{
				NavmeshTile navmeshTile = this.stagingTiles[i];
				int num = navmeshTile.x + navmeshTile.z * this.tileXCount;
				NavmeshTile navmeshTile2 = this.tiles[num];
				for (int j = 0; j < navmeshTile2.nodes.Length; j++)
				{
					navmeshTile2.nodes[j].Destroy();
				}
				this.tiles[num] = navmeshTile;
			}
			for (int k = 0; k < this.stagingTiles.Count; k++)
			{
				NavmeshTile tile = this.stagingTiles[k];
				base.ConnectTileWithNeighbours(tile, false);
			}
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(this.stagingTiles.ToArray());
			}
			this.stagingTiles.Clear();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00033314 File Offset: 0x00031714
		protected override IEnumerable<Progress> ScanInternal()
		{
			TriangleMeshNode.SetNavmeshHolder(AstarPath.active.data.GetGraphIndex(this), this);
			if (!Application.isPlaying)
			{
				RelevantGraphSurface.FindAllGraphSurfaces();
			}
			RelevantGraphSurface.UpdateAllPositions();
			foreach (Progress progress in this.ScanAllTiles())
			{
				yield return progress;
			}
			yield break;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00033338 File Offset: 0x00031738
		public override GraphTransform CalculateTransform()
		{
			return new GraphTransform(Matrix4x4.TRS(this.forcedBoundsCenter, Quaternion.Euler(this.rotation), Vector3.one) * Matrix4x4.TRS(-this.forcedBoundsSize * 0.5f, Quaternion.identity, Vector3.one));
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00033390 File Offset: 0x00031790
		private void InitializeTileInfo()
		{
			int num = (int)(this.forcedBoundsSize.x / this.cellSize + 0.5f);
			int num2 = (int)(this.forcedBoundsSize.z / this.cellSize + 0.5f);
			if (!this.useTiles)
			{
				this.tileSizeX = num;
				this.tileSizeZ = num2;
			}
			else
			{
				this.tileSizeX = this.editorTileSize;
				this.tileSizeZ = this.editorTileSize;
			}
			this.tileXCount = (num + this.tileSizeX - 1) / this.tileSizeX;
			this.tileZCount = (num2 + this.tileSizeZ - 1) / this.tileSizeZ;
			if (this.tileXCount * this.tileZCount > 524288)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Too many tiles (",
					this.tileXCount * this.tileZCount,
					") maximum is ",
					524288,
					"\nTry disabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* inspector."
				}));
			}
			this.tiles = new NavmeshTile[this.tileXCount * this.tileZCount];
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000334B0 File Offset: 0x000318B0
		private List<RasterizationMesh>[] PutMeshesIntoTileBuckets(List<RasterizationMesh> meshes)
		{
			List<RasterizationMesh>[] array = new List<RasterizationMesh>[this.tiles.Length];
			Vector3 amount = new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ListPool<RasterizationMesh>.Claim();
			}
			for (int j = 0; j < meshes.Count; j++)
			{
				RasterizationMesh rasterizationMesh = meshes[j];
				Bounds bounds = rasterizationMesh.bounds;
				bounds.Expand(amount);
				IntRect touchingTiles = base.GetTouchingTiles(bounds);
				for (int k = touchingTiles.ymin; k <= touchingTiles.ymax; k++)
				{
					for (int l = touchingTiles.xmin; l <= touchingTiles.xmax; l++)
					{
						array[l + k * this.tileXCount].Add(rasterizationMesh);
					}
				}
			}
			return array;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000335A8 File Offset: 0x000319A8
		protected IEnumerable<Progress> ScanAllTiles()
		{
			this.transform = this.CalculateTransform();
			this.InitializeTileInfo();
			if (this.scanEmptyGraph)
			{
				base.FillWithEmptyTiles();
				yield break;
			}
			this.walkableClimb = Mathf.Min(this.walkableClimb, this.walkableHeight);
			yield return new Progress(0f, "Finding Meshes");
			Bounds bounds = this.transform.Transform(new Bounds(this.forcedBoundsSize * 0.5f, this.forcedBoundsSize));
			List<RasterizationMesh> meshes = this.CollectMeshes(bounds);
			List<RasterizationMesh>[] buckets = this.PutMeshesIntoTileBuckets(meshes);
			Queue<Int2> tileQueue = new Queue<Int2>();
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					tileQueue.Enqueue(new Int2(j, i));
				}
			}
			ParallelWorkQueue<Int2> workQueue = new ParallelWorkQueue<Int2>(tileQueue);
			Voxelize[] voxelizers = new Voxelize[workQueue.threadCount];
			for (int k = 0; k < voxelizers.Length; k++)
			{
				voxelizers[k] = new Voxelize(this.CellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope, this.maxEdgeLength);
			}
			workQueue.action = delegate(Int2 tile, int threadIndex)
			{
				voxelizers[threadIndex].inputMeshes = buckets[tile.x + tile.y * this.tileXCount];
				this.tiles[tile.x + tile.y * this.tileXCount] = this.BuildTileMesh(voxelizers[threadIndex], tile.x, tile.y, threadIndex);
			};
			int timeoutMillis = (!Application.isPlaying) ? 200 : 1;
			foreach (int done in workQueue.Run(timeoutMillis))
			{
				yield return new Progress(Mathf.Lerp(0.1f, 0.9f, (float)done / (float)this.tiles.Length), string.Concat(new object[]
				{
					"Calculated Tiles: ",
					done,
					"/",
					this.tiles.Length
				}));
			}
			yield return new Progress(0.9f, "Assigning Graph Indices");
			uint graphIndex = (uint)AstarPath.active.data.GetGraphIndex(this);
			this.GetNodes(delegate(GraphNode node)
			{
				node.GraphIndex = graphIndex;
			});
			for (int coordinateSum = 0; coordinateSum <= 1; coordinateSum++)
			{
				int direction;
				for (direction = 0; direction <= 1; direction++)
				{
					for (int l = 0; l < this.tiles.Length; l++)
					{
						if ((this.tiles[l].x + this.tiles[l].z) % 2 == coordinateSum)
						{
							tileQueue.Enqueue(new Int2(this.tiles[l].x, this.tiles[l].z));
						}
					}
					workQueue = new ParallelWorkQueue<Int2>(tileQueue);
					workQueue.action = delegate(Int2 tile, int threadIndex)
					{
						if (direction == 0 && tile.x < this.tileXCount - 1)
						{
							this.ConnectTiles(this.tiles[tile.x + tile.y * this.tileXCount], this.tiles[tile.x + 1 + tile.y * this.tileXCount]);
						}
						if (direction == 1 && tile.y < this.tileZCount - 1)
						{
							this.ConnectTiles(this.tiles[tile.x + tile.y * this.tileXCount], this.tiles[tile.x + (tile.y + 1) * this.tileXCount]);
						}
					};
					int numTilesInQueue = tileQueue.Count;
					foreach (int done2 in workQueue.Run(timeoutMillis))
					{
						yield return new Progress(0.95f, string.Concat(new object[]
						{
							"Connected Tiles ",
							numTilesInQueue - done2,
							"/",
							numTilesInQueue,
							" (Phase ",
							direction + 1 + 2 * coordinateSum,
							" of 4)"
						}));
					}
				}
			}
			for (int m = 0; m < meshes.Count; m++)
			{
				meshes[m].Pool();
			}
			ListPool<RasterizationMesh>.Release(ref meshes);
			if (this.OnRecalculatedTiles != null)
			{
				this.OnRecalculatedTiles(this.tiles.Clone() as NavmeshTile[]);
			}
			yield break;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000335CC File Offset: 0x000319CC
		private List<RasterizationMesh> CollectMeshes(Bounds bounds)
		{
			List<RasterizationMesh> list = ListPool<RasterizationMesh>.Claim();
			RecastMeshGatherer recastMeshGatherer = new RecastMeshGatherer(bounds, this.terrainSampleSize, this.mask, this.tagMask, this.colliderRasterizeDetail);
			if (this.rasterizeMeshes)
			{
				recastMeshGatherer.CollectSceneMeshes(list);
			}
			recastMeshGatherer.CollectRecastMeshObjs(list);
			if (this.rasterizeTerrain)
			{
				float desiredChunkSize = this.cellSize * (float)Math.Max(this.tileSizeX, this.tileSizeZ);
				recastMeshGatherer.CollectTerrainMeshes(this.rasterizeTrees, desiredChunkSize, list);
			}
			if (this.rasterizeColliders)
			{
				recastMeshGatherer.CollectColliderMeshes(list);
			}
			if (list.Count == 0)
			{
				Debug.LogWarning("No MeshFilters were found contained in the layers specified by the 'mask' variables");
			}
			return list;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00033672 File Offset: 0x00031A72
		private float CellHeight
		{
			get
			{
				return Mathf.Max(this.forcedBoundsSize.y / 64000f, 0.001f);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0003368F File Offset: 0x00031A8F
		private int CharacterRadiusInVoxels
		{
			get
			{
				return Mathf.CeilToInt(this.characterRadius / this.cellSize - 0.1f);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x000336A9 File Offset: 0x00031AA9
		private int TileBorderSizeInVoxels
		{
			get
			{
				return this.CharacterRadiusInVoxels + 3;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000336B3 File Offset: 0x00031AB3
		private float TileBorderSizeInWorldUnits
		{
			get
			{
				return (float)this.TileBorderSizeInVoxels * this.cellSize;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000336C4 File Offset: 0x00031AC4
		private Bounds CalculateTileBoundsWithBorder(int x, int z)
		{
			Bounds result = default(Bounds);
			result.SetMinMax(new Vector3((float)x * this.TileWorldSizeX, 0f, (float)z * this.TileWorldSizeZ), new Vector3((float)(x + 1) * this.TileWorldSizeX, this.forcedBoundsSize.y, (float)(z + 1) * this.TileWorldSizeZ));
			result.Expand(new Vector3(1f, 0f, 1f) * this.TileBorderSizeInWorldUnits * 2f);
			return result;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00033754 File Offset: 0x00031B54
		protected NavmeshTile BuildTileMesh(Voxelize vox, int x, int z, int threadIndex = 0)
		{
			vox.borderSize = this.TileBorderSizeInVoxels;
			vox.forcedBounds = this.CalculateTileBoundsWithBorder(x, z);
			vox.width = this.tileSizeX + vox.borderSize * 2;
			vox.depth = this.tileSizeZ + vox.borderSize * 2;
			if (!this.useTiles && this.relevantGraphSurfaceMode == RecastGraph.RelevantGraphSurfaceMode.OnlyForCompletelyInsideTile)
			{
				vox.relevantGraphSurfaceMode = RecastGraph.RelevantGraphSurfaceMode.RequireForAll;
			}
			else
			{
				vox.relevantGraphSurfaceMode = this.relevantGraphSurfaceMode;
			}
			vox.minRegionSize = Mathf.RoundToInt(this.minRegionSize / (this.cellSize * this.cellSize));
			vox.Init();
			vox.VoxelizeInput(this.transform, this.CalculateTileBoundsWithBorder(x, z));
			vox.FilterLedges(vox.voxelWalkableHeight, vox.voxelWalkableClimb, vox.cellSize, vox.cellHeight);
			vox.FilterLowHeightSpans(vox.voxelWalkableHeight, vox.cellSize, vox.cellHeight);
			vox.BuildCompactField();
			vox.BuildVoxelConnections();
			vox.ErodeWalkableArea(this.CharacterRadiusInVoxels);
			vox.BuildDistanceField();
			vox.BuildRegions();
			VoxelContourSet cset = new VoxelContourSet();
			vox.BuildContours(this.contourMaxError, 1, cset, 5);
			VoxelMesh mesh;
			vox.BuildPolyMesh(cset, 3, out mesh);
			for (int i = 0; i < mesh.verts.Length; i++)
			{
				mesh.verts[i] *= 1000;
			}
			vox.transformVoxel2Graph.Transform(mesh.verts);
			return this.CreateTile(vox, mesh, x, z, threadIndex);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000338E4 File Offset: 0x00031CE4
		private NavmeshTile CreateTile(Voxelize vox, VoxelMesh mesh, int x, int z, int threadIndex)
		{
			if (mesh.tris == null)
			{
				throw new ArgumentNullException("mesh.tris");
			}
			if (mesh.verts == null)
			{
				throw new ArgumentNullException("mesh.verts");
			}
			if (mesh.tris.Length % 3 != 0)
			{
				throw new ArgumentException("Indices array's length must be a multiple of 3 (mesh.tris)");
			}
			if (mesh.verts.Length >= 4095)
			{
				if (this.tileXCount * this.tileZCount == 1)
				{
					throw new ArgumentException("Too many vertices per tile (more than " + 4095 + ").\n<b>Try enabling tiling in the recast graph settings.</b>\n");
				}
				throw new ArgumentException("Too many vertices per tile (more than " + 4095 + ").\n<b>Try reducing tile size or enabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* Inspector</b>");
			}
			else
			{
				NavmeshTile navmeshTile = new NavmeshTile
				{
					x = x,
					z = z,
					w = 1,
					d = 1,
					tris = mesh.tris,
					bbTree = new BBTree(),
					graph = this
				};
				navmeshTile.vertsInGraphSpace = Utility.RemoveDuplicateVertices(mesh.verts, navmeshTile.tris);
				navmeshTile.verts = (Int3[])navmeshTile.vertsInGraphSpace.Clone();
				this.transform.Transform(navmeshTile.verts);
				uint num = (uint)(this.active.data.graphs.Length + threadIndex);
				if (num > 255u)
				{
					throw new Exception("Graph limit reached. Multithreaded recast calculations cannot be done because a few scratch graph indices are required.");
				}
				TriangleMeshNode.SetNavmeshHolder((int)num, navmeshTile);
				navmeshTile.nodes = new TriangleMeshNode[navmeshTile.tris.Length / 3];
				object active = this.active;
				lock (active)
				{
					base.CreateNodes(navmeshTile.nodes, navmeshTile.tris, x + z * this.tileXCount, num);
				}
				navmeshTile.bbTree.RebuildFrom(navmeshTile.nodes);
				NavmeshBase.CreateNodeConnections(navmeshTile.nodes);
				TriangleMeshNode.SetNavmeshHolder((int)num, null);
				return navmeshTile;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00033AD4 File Offset: 0x00031ED4
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.characterRadius = ctx.reader.ReadSingle();
			this.contourMaxError = ctx.reader.ReadSingle();
			this.cellSize = ctx.reader.ReadSingle();
			ctx.reader.ReadSingle();
			this.walkableHeight = ctx.reader.ReadSingle();
			this.maxSlope = ctx.reader.ReadSingle();
			this.maxEdgeLength = ctx.reader.ReadSingle();
			this.editorTileSize = ctx.reader.ReadInt32();
			this.tileSizeX = ctx.reader.ReadInt32();
			this.nearestSearchOnlyXZ = ctx.reader.ReadBoolean();
			this.useTiles = ctx.reader.ReadBoolean();
			this.relevantGraphSurfaceMode = (RecastGraph.RelevantGraphSurfaceMode)ctx.reader.ReadInt32();
			this.rasterizeColliders = ctx.reader.ReadBoolean();
			this.rasterizeMeshes = ctx.reader.ReadBoolean();
			this.rasterizeTerrain = ctx.reader.ReadBoolean();
			this.rasterizeTrees = ctx.reader.ReadBoolean();
			this.colliderRasterizeDetail = ctx.reader.ReadSingle();
			this.forcedBoundsCenter = ctx.DeserializeVector3();
			this.forcedBoundsSize = ctx.DeserializeVector3();
			this.mask = ctx.reader.ReadInt32();
			int num = ctx.reader.ReadInt32();
			this.tagMask = new List<string>(num);
			for (int i = 0; i < num; i++)
			{
				this.tagMask.Add(ctx.reader.ReadString());
			}
			this.showMeshOutline = ctx.reader.ReadBoolean();
			this.showNodeConnections = ctx.reader.ReadBoolean();
			this.terrainSampleSize = ctx.reader.ReadInt32();
			this.walkableClimb = ctx.DeserializeFloat(this.walkableClimb);
			this.minRegionSize = ctx.DeserializeFloat(this.minRegionSize);
			this.tileSizeZ = ctx.DeserializeInt(this.tileSizeX);
			this.showMeshSurface = ctx.reader.ReadBoolean();
		}

		// Token: 0x040004BD RID: 1213
		[JsonMember]
		public float characterRadius = 1.5f;

		// Token: 0x040004BE RID: 1214
		[JsonMember]
		public float contourMaxError = 2f;

		// Token: 0x040004BF RID: 1215
		[JsonMember]
		public float cellSize = 0.5f;

		// Token: 0x040004C0 RID: 1216
		[JsonMember]
		public float walkableHeight = 2f;

		// Token: 0x040004C1 RID: 1217
		[JsonMember]
		public float walkableClimb = 0.5f;

		// Token: 0x040004C2 RID: 1218
		[JsonMember]
		public float maxSlope = 30f;

		// Token: 0x040004C3 RID: 1219
		[JsonMember]
		public float maxEdgeLength = 20f;

		// Token: 0x040004C4 RID: 1220
		[JsonMember]
		public float minRegionSize = 3f;

		// Token: 0x040004C5 RID: 1221
		[JsonMember]
		public int editorTileSize = 128;

		// Token: 0x040004C6 RID: 1222
		[JsonMember]
		public int tileSizeX = 128;

		// Token: 0x040004C7 RID: 1223
		[JsonMember]
		public int tileSizeZ = 128;

		// Token: 0x040004C8 RID: 1224
		[JsonMember]
		public bool useTiles = true;

		// Token: 0x040004C9 RID: 1225
		public bool scanEmptyGraph;

		// Token: 0x040004CA RID: 1226
		[JsonMember]
		public RecastGraph.RelevantGraphSurfaceMode relevantGraphSurfaceMode;

		// Token: 0x040004CB RID: 1227
		[JsonMember]
		public bool rasterizeColliders;

		// Token: 0x040004CC RID: 1228
		[JsonMember]
		public bool rasterizeMeshes = true;

		// Token: 0x040004CD RID: 1229
		[JsonMember]
		public bool rasterizeTerrain = true;

		// Token: 0x040004CE RID: 1230
		[JsonMember]
		public bool rasterizeTrees = true;

		// Token: 0x040004CF RID: 1231
		[JsonMember]
		public float colliderRasterizeDetail = 10f;

		// Token: 0x040004D0 RID: 1232
		[JsonMember]
		public LayerMask mask = -1;

		// Token: 0x040004D1 RID: 1233
		[JsonMember]
		public List<string> tagMask = new List<string>();

		// Token: 0x040004D2 RID: 1234
		[JsonMember]
		public int terrainSampleSize = 3;

		// Token: 0x040004D3 RID: 1235
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x040004D4 RID: 1236
		[JsonMember]
		public Vector3 forcedBoundsCenter;

		// Token: 0x040004D5 RID: 1237
		private Voxelize globalVox;

		// Token: 0x040004D6 RID: 1238
		public const int BorderVertexMask = 1;

		// Token: 0x040004D7 RID: 1239
		public const int BorderVertexOffset = 31;

		// Token: 0x040004D8 RID: 1240
		private List<NavmeshTile> stagingTiles = new List<NavmeshTile>();

		// Token: 0x020000B3 RID: 179
		public enum RelevantGraphSurfaceMode
		{
			// Token: 0x040004DA RID: 1242
			DoNotRequire,
			// Token: 0x040004DB RID: 1243
			OnlyForCompletelyInsideTile,
			// Token: 0x040004DC RID: 1244
			RequireForAll
		}
	}
}
