using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F9 RID: 249
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_tile_handler_helper.php")]
	public class TileHandlerHelper : VersionedMonoBehaviour
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x00048378 File Offset: 0x00046778
		public void UseSpecifiedHandler(TileHandler newHandler)
		{
			if (!base.enabled)
			{
				throw new InvalidOperationException("TileHandlerHelper is disabled");
			}
			if (this.handler != null)
			{
				NavmeshClipper.RemoveEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				NavmeshBase graph = this.handler.graph;
				graph.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Remove(graph.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
			this.handler = newHandler;
			if (this.handler != null)
			{
				NavmeshClipper.AddEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				NavmeshBase graph2 = this.handler.graph;
				graph2.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Combine(graph2.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0004844C File Offset: 0x0004684C
		private void OnEnable()
		{
			if (this.handler != null)
			{
				NavmeshClipper.AddEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				NavmeshBase graph = this.handler.graph;
				graph.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Combine(graph.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
			this.forcedReloadRects.Clear();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000484B8 File Offset: 0x000468B8
		private void OnDisable()
		{
			if (this.handler != null)
			{
				NavmeshClipper.RemoveEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
				this.forcedReloadRects.Clear();
				NavmeshBase graph = this.handler.graph;
				graph.OnRecalculatedTiles = (Action<NavmeshTile[]>)Delegate.Remove(graph.OnRecalculatedTiles, new Action<NavmeshTile[]>(this.OnRecalculatedTiles));
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00048524 File Offset: 0x00046924
		public void DiscardPending()
		{
			if (this.handler != null)
			{
				for (GridLookup<NavmeshClipper>.Root root = this.handler.cuts.AllItems; root != null; root = root.next)
				{
					if (root.obj.RequiresUpdate())
					{
						root.obj.NotifyUpdated();
					}
				}
			}
			this.forcedReloadRects.Clear();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00048585 File Offset: 0x00046985
		private void Start()
		{
			if (UnityEngine.Object.FindObjectsOfType(typeof(TileHandlerHelper)).Length > 1)
			{
				Debug.LogError("There should only be one TileHandlerHelper per scene. Destroying.");
				UnityEngine.Object.Destroy(this);
				return;
			}
			if (this.handler == null)
			{
				this.FindGraph();
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000485C0 File Offset: 0x000469C0
		private void FindGraph()
		{
			if (AstarPath.active != null)
			{
				NavmeshBase navmeshBase = AstarPath.active.data.FindGraphWhichInheritsFrom(typeof(NavmeshBase)) as NavmeshBase;
				if (navmeshBase != null)
				{
					this.UseSpecifiedHandler(new TileHandler(navmeshBase));
					this.handler.CreateTileTypesFromGraph();
				}
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00048619 File Offset: 0x00046A19
		private void OnRecalculatedTiles(NavmeshTile[] tiles)
		{
			if (!this.handler.isValid)
			{
				this.UseSpecifiedHandler(new TileHandler(this.handler.graph));
			}
			this.handler.OnRecalculatedTiles(tiles);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00048650 File Offset: 0x00046A50
		private void HandleOnEnableCallback(NavmeshClipper obj)
		{
			Rect bounds = obj.GetBounds(this.handler.graph.transform);
			IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
			this.handler.cuts.Add(obj, touchingTilesInGraphSpace);
			obj.ForceUpdate();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000486A0 File Offset: 0x00046AA0
		private void HandleOnDisableCallback(NavmeshClipper obj)
		{
			GridLookup<NavmeshClipper>.Root root = this.handler.cuts.GetRoot(obj);
			if (root != null)
			{
				this.forcedReloadRects.Add(root.previousBounds);
				this.handler.cuts.Remove(obj);
			}
			this.lastUpdateTime = float.NegativeInfinity;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000486F4 File Offset: 0x00046AF4
		private void Update()
		{
			if (this.handler == null)
			{
				this.FindGraph();
			}
			if (this.handler != null && !AstarPath.active.isScanning && ((this.updateInterval >= 0f && Time.realtimeSinceStartup - this.lastUpdateTime > this.updateInterval) || !this.handler.isValid))
			{
				this.ForceUpdate();
			}
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0004876C File Offset: 0x00046B6C
		public void ForceUpdate()
		{
			if (this.handler == null)
			{
				throw new Exception("Cannot update graphs. No TileHandler. Do not call the ForceUpdate method in Awake.");
			}
			this.lastUpdateTime = Time.realtimeSinceStartup;
			if (!this.handler.isValid)
			{
				if (!this.handler.graph.exists)
				{
					this.UseSpecifiedHandler(null);
					return;
				}
				Debug.Log("TileHandler no longer matched the underlaying graph (possibly because of a graph scan). Recreating TileHandler...");
				this.UseSpecifiedHandler(new TileHandler(this.handler.graph));
				this.handler.CreateTileTypesFromGraph();
				this.forcedReloadRects.Add(new IntRect(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue));
			}
			GridLookup<NavmeshClipper>.Root allItems = this.handler.cuts.AllItems;
			if (this.forcedReloadRects.Count == 0)
			{
				int num = 0;
				for (GridLookup<NavmeshClipper>.Root root = allItems; root != null; root = root.next)
				{
					if (root.obj.RequiresUpdate())
					{
						num++;
						break;
					}
				}
				if (num == 0)
				{
					return;
				}
			}
			bool flag = this.handler.StartBatchLoad();
			for (int i = 0; i < this.forcedReloadRects.Count; i++)
			{
				this.handler.ReloadInBounds(this.forcedReloadRects[i]);
			}
			this.forcedReloadRects.Clear();
			for (GridLookup<NavmeshClipper>.Root root2 = allItems; root2 != null; root2 = root2.next)
			{
				if (root2.obj.RequiresUpdate())
				{
					this.handler.ReloadInBounds(root2.previousBounds);
					Rect bounds = root2.obj.GetBounds(this.handler.graph.transform);
					IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
					this.handler.cuts.Move(root2.obj, touchingTilesInGraphSpace);
					this.handler.ReloadInBounds(touchingTilesInGraphSpace);
				}
			}
			for (GridLookup<NavmeshClipper>.Root root3 = allItems; root3 != null; root3 = root3.next)
			{
				if (root3.obj.RequiresUpdate())
				{
					root3.obj.NotifyUpdated();
				}
			}
			if (flag)
			{
				this.handler.EndBatchLoad();
			}
		}

		// Token: 0x04000676 RID: 1654
		private TileHandler handler;

		// Token: 0x04000677 RID: 1655
		public float updateInterval;

		// Token: 0x04000678 RID: 1656
		private float lastUpdateTime = float.NegativeInfinity;

		// Token: 0x04000679 RID: 1657
		private readonly List<IntRect> forcedReloadRects = new List<IntRect>();
	}
}
