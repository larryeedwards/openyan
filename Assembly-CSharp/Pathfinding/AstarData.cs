using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Pathfinding.WindowsStore;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class AstarData
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007E9D File Offset: 0x0000629D
		public static AstarPath active
		{
			get
			{
				return AstarPath.active;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007EA4 File Offset: 0x000062A4
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00007EAC File Offset: 0x000062AC
		public NavMeshGraph navmesh { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007EB5 File Offset: 0x000062B5
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00007EBD File Offset: 0x000062BD
		public GridGraph gridGraph { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007EC6 File Offset: 0x000062C6
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00007ECE File Offset: 0x000062CE
		public LayerGridGraph layerGridGraph { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007ED7 File Offset: 0x000062D7
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00007EDF File Offset: 0x000062DF
		public PointGraph pointGraph { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00007EE8 File Offset: 0x000062E8
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00007EF0 File Offset: 0x000062F0
		public RecastGraph recastGraph { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007EF9 File Offset: 0x000062F9
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00007F01 File Offset: 0x00006301
		public Type[] graphTypes { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007F0C File Offset: 0x0000630C
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00007F61 File Offset: 0x00006361
		private byte[] data
		{
			get
			{
				if (this.upgradeData != null && this.upgradeData.Length > 0)
				{
					this.data = this.upgradeData;
					this.upgradeData = null;
				}
				return (this.dataString == null) ? null : Convert.FromBase64String(this.dataString);
			}
			set
			{
				this.dataString = ((value == null) ? null : Convert.ToBase64String(value));
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007F7B File Offset: 0x0000637B
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007F83 File Offset: 0x00006383
		public void SetData(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007F8C File Offset: 0x0000638C
		public void Awake()
		{
			this.graphs = new NavGraph[0];
			if (this.cacheStartup && this.file_cachedStartup != null)
			{
				this.LoadFromCache();
			}
			else
			{
				this.DeserializeGraphs();
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007FC7 File Offset: 0x000063C7
		internal void LockGraphStructure(bool allowAddingGraphs = false)
		{
			this.graphStructureLocked.Add(allowAddingGraphs);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007FD5 File Offset: 0x000063D5
		internal void UnlockGraphStructure()
		{
			if (this.graphStructureLocked.Count == 0)
			{
				throw new InvalidOperationException();
			}
			this.graphStructureLocked.RemoveAt(this.graphStructureLocked.Count - 1);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008008 File Offset: 0x00006408
		private PathProcessor.GraphUpdateLock AssertSafe(bool onlyAddingGraph = false)
		{
			if (this.graphStructureLocked.Count > 0)
			{
				bool flag = true;
				for (int i = 0; i < this.graphStructureLocked.Count; i++)
				{
					flag &= this.graphStructureLocked[i];
				}
				if (!onlyAddingGraph || !flag)
				{
					throw new InvalidOperationException("Graphs cannot be added, removed or serialized while the graph structure is locked. This is the case when a graph is currently being scanned and when executing graph updates and work items.\nHowever as a special case, graphs can be added inside work items.");
				}
			}
			PathProcessor.GraphUpdateLock result = AstarData.active.PausePathfinding();
			if (!AstarData.active.IsInsideWorkItem)
			{
				AstarData.active.FlushWorkItems();
				AstarData.active.pathReturnQueue.ReturnPaths(false);
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000080A0 File Offset: 0x000064A0
		public void UpdateShortcuts()
		{
			this.navmesh = (NavMeshGraph)this.FindGraphOfType(typeof(NavMeshGraph));
			this.gridGraph = (GridGraph)this.FindGraphOfType(typeof(GridGraph));
			this.layerGridGraph = (LayerGridGraph)this.FindGraphOfType(typeof(LayerGridGraph));
			this.pointGraph = (PointGraph)this.FindGraphOfType(typeof(PointGraph));
			this.recastGraph = (RecastGraph)this.FindGraphOfType(typeof(RecastGraph));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008134 File Offset: 0x00006534
		public void LoadFromCache()
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			if (this.file_cachedStartup != null)
			{
				byte[] bytes = this.file_cachedStartup.bytes;
				this.DeserializeGraphs(bytes);
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostCacheLoad);
			}
			else
			{
				Debug.LogError("Can't load from cache since the cache is empty");
			}
			graphUpdateLock.Release();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000818A File Offset: 0x0000658A
		public byte[] SerializeGraphs()
		{
			return this.SerializeGraphs(SerializeSettings.Settings);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008198 File Offset: 0x00006598
		public byte[] SerializeGraphs(SerializeSettings settings)
		{
			uint num;
			return this.SerializeGraphs(settings, out num);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000081B0 File Offset: 0x000065B0
		public byte[] SerializeGraphs(SerializeSettings settings, out uint checksum)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			AstarSerializer astarSerializer = new AstarSerializer(this, settings);
			astarSerializer.OpenSerialize();
			astarSerializer.SerializeGraphs(this.graphs);
			astarSerializer.SerializeExtraInfo();
			byte[] result = astarSerializer.CloseSerialize();
			checksum = astarSerializer.GetChecksum();
			graphUpdateLock.Release();
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000081FC File Offset: 0x000065FC
		public void DeserializeGraphs()
		{
			if (this.data != null)
			{
				this.DeserializeGraphs(this.data);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008218 File Offset: 0x00006618
		private void ClearGraphs()
		{
			if (this.graphs == null)
			{
				return;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					((IGraphInternals)this.graphs[i]).OnDestroy();
					this.graphs[i].active = null;
				}
			}
			this.graphs = null;
			this.UpdateShortcuts();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000827F File Offset: 0x0000667F
		public void OnDestroy()
		{
			this.ClearGraphs();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008288 File Offset: 0x00006688
		public void DeserializeGraphs(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			this.ClearGraphs();
			this.DeserializeGraphsAdditive(bytes);
			graphUpdateLock.Release();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000082B4 File Offset: 0x000066B4
		public void DeserializeGraphsAdditive(byte[] bytes)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("bytes");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPartAdditive(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).\nThe data is either corrupt or it was saved using a 3.0.x or earlier version of the system");
				}
				AstarData.active.VerifyIntegrity();
			}
			catch (Exception arg)
			{
				Debug.LogError("Caught exception while deserializing data.\n" + arg);
				this.graphs = new NavGraph[0];
			}
			this.UpdateShortcuts();
			graphUpdateLock.Release();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000835C File Offset: 0x0000675C
		private void DeserializeGraphsPartAdditive(AstarSerializer sr)
		{
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			sr.SetGraphIndexOffset(list.Count);
			list.AddRange(sr.DeserializeGraphs());
			this.graphs = list.ToArray();
			sr.DeserializeEditorSettingsCompatibility();
			sr.DeserializeExtraInfo();
			int i;
			for (i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
					});
				}
			}
			for (int k = 0; k < this.graphs.Length; k++)
			{
				for (int j = k + 1; j < this.graphs.Length; j++)
				{
					if (this.graphs[k] != null && this.graphs[j] != null && this.graphs[k].guid == this.graphs[j].guid)
					{
						Debug.LogWarning("Guid Conflict when importing graphs additively. Imported graph will get a new Guid.\nThis message is (relatively) harmless.");
						this.graphs[k].guid = Pathfinding.Util.Guid.NewGuid();
						break;
					}
				}
			}
			sr.PostDeserialization();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000084C0 File Offset: 0x000068C0
		public void FindGraphTypes()
		{
			Assembly assembly = WindowsStoreCompatibility.GetTypeInfo(typeof(AstarPath)).Assembly;
			Type[] types = assembly.GetTypes();
			List<Type> list = new List<Type>();
			foreach (Type type in types)
			{
				for (Type baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
				{
					if (object.Equals(baseType, typeof(NavGraph)))
					{
						list.Add(type);
						break;
					}
				}
			}
			this.graphTypes = list.ToArray();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000855C File Offset: 0x0000695C
		[Obsolete("If really necessary. Use System.Type.GetType instead.")]
		public Type GetGraphType(string type)
		{
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.graphTypes[i];
				}
			}
			return null;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000085A4 File Offset: 0x000069A4
		[Obsolete("Use CreateGraph(System.Type) instead")]
		public NavGraph CreateGraph(string type)
		{
			Debug.Log("Creating Graph of type '" + type + "'");
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.CreateGraph(this.graphTypes[i]);
				}
			}
			Debug.LogError("Graph type (" + type + ") wasn't found");
			return null;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000861C File Offset: 0x00006A1C
		internal NavGraph CreateGraph(Type type)
		{
			NavGraph navGraph = Activator.CreateInstance(type) as NavGraph;
			navGraph.active = AstarData.active;
			return navGraph;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008644 File Offset: 0x00006A44
		[Obsolete("Use AddGraph(System.Type) instead")]
		public NavGraph AddGraph(string type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError("No NavGraph of type '" + type + "' could be found");
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000086B8 File Offset: 0x00006AB8
		public NavGraph AddGraph(Type type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (object.Equals(this.graphTypes[i], type))
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"No NavGraph of type '",
					type,
					"' could be found, ",
					this.graphTypes.Length,
					" graph types are avaliable"
				}));
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008750 File Offset: 0x00006B50
		private void AddGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(true);
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					this.graphs[i] = graph;
					graph.graphIndex = (uint)i;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (this.graphs != null && (long)this.graphs.Length >= 255L)
				{
					throw new Exception("Graph Count Limit Reached. You cannot have more than " + 255u + " graphs.");
				}
				this.graphs = new List<NavGraph>(this.graphs ?? new NavGraph[0])
				{
					graph
				}.ToArray();
				graph.graphIndex = (uint)(this.graphs.Length - 1);
			}
			this.UpdateShortcuts();
			graph.active = AstarData.active;
			graphUpdateLock.Release();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000883C File Offset: 0x00006C3C
		public bool RemoveGraph(NavGraph graph)
		{
			PathProcessor.GraphUpdateLock graphUpdateLock = this.AssertSafe(false);
			((IGraphInternals)graph).OnDestroy();
			graph.active = null;
			int num = Array.IndexOf<NavGraph>(this.graphs, graph);
			if (num != -1)
			{
				this.graphs[num] = null;
			}
			this.UpdateShortcuts();
			graphUpdateLock.Release();
			return num != -1;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008890 File Offset: 0x00006C90
		public static NavGraph GetGraph(GraphNode node)
		{
			if (node == null)
			{
				return null;
			}
			AstarPath active = AstarPath.active;
			if (active == null)
			{
				return null;
			}
			AstarData data = active.data;
			if (data == null || data.graphs == null)
			{
				return null;
			}
			uint graphIndex = node.GraphIndex;
			if ((ulong)graphIndex >= (ulong)((long)data.graphs.Length))
			{
				return null;
			}
			return data.graphs[(int)graphIndex];
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000088F4 File Offset: 0x00006CF4
		public NavGraph FindGraph(Func<NavGraph, bool> predicate)
		{
			if (this.graphs != null)
			{
				for (int i = 0; i < this.graphs.Length; i++)
				{
					if (this.graphs[i] != null && predicate(this.graphs[i]))
					{
						return this.graphs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008950 File Offset: 0x00006D50
		public NavGraph FindGraphOfType(Type type)
		{
			return this.FindGraph((NavGraph graph) => object.Equals(graph.GetType(), type));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000897C File Offset: 0x00006D7C
		public NavGraph FindGraphWhichInheritsFrom(Type type)
		{
			return this.FindGraph((NavGraph graph) => WindowsStoreCompatibility.GetTypeInfo(type).IsAssignableFrom(WindowsStoreCompatibility.GetTypeInfo(graph.GetType())));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000089A8 File Offset: 0x00006DA8
		public IEnumerable FindGraphsOfType(Type type)
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && object.Equals(this.graphs[i].GetType(), type))
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000089D4 File Offset: 0x00006DD4
		public IEnumerable GetUpdateableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] is IUpdatableGraph)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000089F8 File Offset: 0x00006DF8
		[Obsolete("Obsolete because it is not used by the package internally and the use cases are few. Iterate through the graphs array instead.")]
		public IEnumerable GetRaycastableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] is IRaycastableGraph)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008A1C File Offset: 0x00006E1C
		public int GetGraphIndex(NavGraph graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			int num = -1;
			if (this.graphs != null)
			{
				num = Array.IndexOf<NavGraph>(this.graphs, graph);
				if (num == -1)
				{
					Debug.LogError("Graph doesn't exist");
				}
			}
			return num;
		}

		// Token: 0x04000118 RID: 280
		[NonSerialized]
		public NavGraph[] graphs = new NavGraph[0];

		// Token: 0x04000119 RID: 281
		[SerializeField]
		private string dataString;

		// Token: 0x0400011A RID: 282
		[SerializeField]
		[FormerlySerializedAs("data")]
		private byte[] upgradeData;

		// Token: 0x0400011B RID: 283
		public TextAsset file_cachedStartup;

		// Token: 0x0400011C RID: 284
		public byte[] data_cachedStartup;

		// Token: 0x0400011D RID: 285
		[SerializeField]
		public bool cacheStartup;

		// Token: 0x0400011E RID: 286
		private List<bool> graphStructureLocked = new List<bool>();
	}
}
