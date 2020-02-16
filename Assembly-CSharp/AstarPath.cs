using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x0200002F RID: 47
[ExecuteInEditMode]
[AddComponentMenu("Pathfinding/Pathfinder")]
[HelpURL("http://arongranberg.com/astar/docs/class_astar_path.php")]
public class AstarPath : VersionedMonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x0000BC38 File Offset: 0x0000A038
	private AstarPath()
	{
		this.pathReturnQueue = new PathReturnQueue(this);
		this.pathProcessor = new PathProcessor(this, this.pathReturnQueue, 1, false);
		this.workItems = new WorkItemProcessor(this);
		this.graphUpdates = new GraphUpdateProcessor(this);
		this.graphUpdates.OnGraphsUpdated += delegate()
		{
			if (AstarPath.OnGraphsUpdated != null)
			{
				AstarPath.OnGraphsUpdated(this);
			}
		};
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000227 RID: 551 RVA: 0x0000BD39 File Offset: 0x0000A139
	[Obsolete]
	public Type[] graphTypes
	{
		get
		{
			return this.data.graphTypes;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000228 RID: 552 RVA: 0x0000BD46 File Offset: 0x0000A146
	[Obsolete("The 'astarData' field has been renamed to 'data'")]
	public AstarData astarData
	{
		get
		{
			return this.data;
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000229 RID: 553 RVA: 0x0000BD4E File Offset: 0x0000A14E
	public NavGraph[] graphs
	{
		get
		{
			if (this.data == null)
			{
				this.data = new AstarData();
			}
			return this.data.graphs;
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x0600022A RID: 554 RVA: 0x0000BD71 File Offset: 0x0000A171
	public float maxNearestNodeDistanceSqr
	{
		get
		{
			return this.maxNearestNodeDistance * this.maxNearestNodeDistance;
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600022B RID: 555 RVA: 0x0000BD80 File Offset: 0x0000A180
	// (set) Token: 0x0600022C RID: 556 RVA: 0x0000BD88 File Offset: 0x0000A188
	[Obsolete("This field has been renamed to 'batchGraphUpdates'")]
	public bool limitGraphUpdates
	{
		get
		{
			return this.batchGraphUpdates;
		}
		set
		{
			this.batchGraphUpdates = value;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x0600022D RID: 557 RVA: 0x0000BD91 File Offset: 0x0000A191
	// (set) Token: 0x0600022E RID: 558 RVA: 0x0000BD99 File Offset: 0x0000A199
	[Obsolete("This field has been renamed to 'graphUpdateBatchingInterval'")]
	public float maxGraphUpdateFreq
	{
		get
		{
			return this.graphUpdateBatchingInterval;
		}
		set
		{
			this.graphUpdateBatchingInterval = value;
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x0600022F RID: 559 RVA: 0x0000BDA2 File Offset: 0x0000A1A2
	// (set) Token: 0x06000230 RID: 560 RVA: 0x0000BDAA File Offset: 0x0000A1AA
	public float lastScanTime { get; private set; }

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000231 RID: 561 RVA: 0x0000BDB3 File Offset: 0x0000A1B3
	// (set) Token: 0x06000232 RID: 562 RVA: 0x0000BDBB File Offset: 0x0000A1BB
	public bool isScanning
	{
		get
		{
			return this.isScanningBacking;
		}
		private set
		{
			this.isScanningBacking = value;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000233 RID: 563 RVA: 0x0000BDC4 File Offset: 0x0000A1C4
	public int NumParallelThreads
	{
		get
		{
			return this.pathProcessor.NumThreads;
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000234 RID: 564 RVA: 0x0000BDD1 File Offset: 0x0000A1D1
	public bool IsUsingMultithreading
	{
		get
		{
			return this.pathProcessor.IsUsingMultithreading;
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000235 RID: 565 RVA: 0x0000BDDE File Offset: 0x0000A1DE
	[Obsolete("Fixed grammar, use IsAnyGraphUpdateQueued instead")]
	public bool IsAnyGraphUpdatesQueued
	{
		get
		{
			return this.IsAnyGraphUpdateQueued;
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000236 RID: 566 RVA: 0x0000BDE6 File Offset: 0x0000A1E6
	public bool IsAnyGraphUpdateQueued
	{
		get
		{
			return this.graphUpdates.IsAnyGraphUpdateQueued;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000237 RID: 567 RVA: 0x0000BDF3 File Offset: 0x0000A1F3
	public bool IsAnyGraphUpdateInProgress
	{
		get
		{
			return this.graphUpdates.IsAnyGraphUpdateInProgress;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000238 RID: 568 RVA: 0x0000BE00 File Offset: 0x0000A200
	public bool IsAnyWorkItemInProgress
	{
		get
		{
			return this.workItems.workItemsInProgress;
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000239 RID: 569 RVA: 0x0000BE0D File Offset: 0x0000A20D
	internal bool IsInsideWorkItem
	{
		get
		{
			return this.workItems.workItemsInProgressRightNow;
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000BE1C File Offset: 0x0000A21C
	public string[] GetTagNames()
	{
		if (this.tagNames == null || this.tagNames.Length != 32)
		{
			this.tagNames = new string[32];
			for (int i = 0; i < this.tagNames.Length; i++)
			{
				this.tagNames[i] = string.Empty + i;
			}
			this.tagNames[0] = "Basic Ground";
		}
		return this.tagNames;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000BE94 File Offset: 0x0000A294
	public static void FindAstarPath()
	{
		if (Application.isPlaying)
		{
			return;
		}
		if (AstarPath.active == null)
		{
			AstarPath.active = UnityEngine.Object.FindObjectOfType<AstarPath>();
		}
		if (AstarPath.active != null && (AstarPath.active.data.graphs == null || AstarPath.active.data.graphs.Length == 0))
		{
			AstarPath.active.data.DeserializeGraphs();
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000BF0F File Offset: 0x0000A30F
	public static string[] FindTagNames()
	{
		AstarPath.FindAstarPath();
		string[] result;
		if (AstarPath.active != null)
		{
			result = AstarPath.active.GetTagNames();
		}
		else
		{
			(result = new string[1])[0] = "There is no AstarPath component in the scene";
		}
		return result;
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000BF44 File Offset: 0x0000A344
	internal ushort GetNextPathID()
	{
		if (this.nextFreePathID == 0)
		{
			this.nextFreePathID += 1;
			if (AstarPath.On65KOverflow != null)
			{
				Action on65KOverflow = AstarPath.On65KOverflow;
				AstarPath.On65KOverflow = null;
				on65KOverflow();
			}
		}
		ushort result;
		this.nextFreePathID = (result = this.nextFreePathID) + 1;
		return result;
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000BF9C File Offset: 0x0000A39C
	private void RecalculateDebugLimits()
	{
		this.debugFloor = float.PositiveInfinity;
		this.debugRoof = float.NegativeInfinity;
		bool ignoreSearchTree = !this.showSearchTree || this.debugPathData == null;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			if (this.graphs[i] != null && this.graphs[i].drawGizmos)
			{
				this.graphs[i].GetNodes(delegate(GraphNode node)
				{
					if (ignoreSearchTree || GraphGizmoHelper.InSearchTree(node, this.debugPathData, this.debugPathID))
					{
						if (this.debugMode == GraphDebugMode.Penalty)
						{
							this.debugFloor = Mathf.Min(this.debugFloor, node.Penalty);
							this.debugRoof = Mathf.Max(this.debugRoof, node.Penalty);
						}
						else if (this.debugPathData != null)
						{
							PathNode pathNode = this.debugPathData.GetPathNode(node);
							GraphDebugMode graphDebugMode = this.debugMode;
							if (graphDebugMode != GraphDebugMode.F)
							{
								if (graphDebugMode != GraphDebugMode.G)
								{
									if (graphDebugMode == GraphDebugMode.H)
									{
										this.debugFloor = Mathf.Min(this.debugFloor, pathNode.H);
										this.debugRoof = Mathf.Max(this.debugRoof, pathNode.H);
									}
								}
								else
								{
									this.debugFloor = Mathf.Min(this.debugFloor, pathNode.G);
									this.debugRoof = Mathf.Max(this.debugRoof, pathNode.G);
								}
							}
							else
							{
								this.debugFloor = Mathf.Min(this.debugFloor, pathNode.F);
								this.debugRoof = Mathf.Max(this.debugRoof, pathNode.F);
							}
						}
					}
				});
			}
		}
		if (float.IsInfinity(this.debugFloor))
		{
			this.debugFloor = 0f;
			this.debugRoof = 1f;
		}
		if (this.debugRoof - this.debugFloor < 1f)
		{
			this.debugRoof += 1f;
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000C08C File Offset: 0x0000A48C
	private void OnDrawGizmos()
	{
		if (AstarPath.active == null)
		{
			AstarPath.active = this;
		}
		if (AstarPath.active != this || this.graphs == null)
		{
			return;
		}
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		if (this.workItems.workItemsInProgress || this.isScanning)
		{
			this.gizmos.DrawExisting();
		}
		else
		{
			if (this.showNavGraphs && !this.manualDebugFloorRoof)
			{
				this.RecalculateDebugLimits();
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && this.graphs[i].drawGizmos)
				{
					this.graphs[i].OnDrawGizmos(this.gizmos, this.showNavGraphs);
				}
			}
			if (this.showNavGraphs)
			{
				this.euclideanEmbedding.OnDrawGizmos();
			}
		}
		this.gizmos.FinalizeDraw();
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000C198 File Offset: 0x0000A598
	private void OnGUI()
	{
		if (this.logPathResults == PathLog.InGame && this.inGameDebugPath != string.Empty)
		{
			GUI.Label(new Rect(5f, 5f, 400f, 600f), this.inGameDebugPath);
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000C1EC File Offset: 0x0000A5EC
	private void LogPathResults(Path path)
	{
		if (this.logPathResults != PathLog.None && (path.error || this.logPathResults != PathLog.OnlyErrors))
		{
			string message = path.DebugString(this.logPathResults);
			if (this.logPathResults == PathLog.InGame)
			{
				this.inGameDebugPath = message;
			}
			else if (path.error)
			{
				UnityEngine.Debug.LogWarning(message);
			}
			else
			{
				UnityEngine.Debug.Log(message);
			}
		}
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000C25C File Offset: 0x0000A65C
	private void Update()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (!this.isScanning)
		{
			this.PerformBlockingActions(false);
		}
		this.pathProcessor.TickNonMultithreaded();
		this.pathReturnQueue.ReturnPaths(true);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000C294 File Offset: 0x0000A694
	private void PerformBlockingActions(bool force = false)
	{
		if (this.workItemLock.Held && this.pathProcessor.queue.AllReceiversBlocked)
		{
			this.pathReturnQueue.ReturnPaths(false);
			if (this.workItems.ProcessWorkItems(force))
			{
				this.workItemLock.Release();
			}
		}
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000C2EE File Offset: 0x0000A6EE
	[Obsolete("This method has been moved. Use the method on the context object that can be sent with work item delegates instead")]
	public void QueueWorkItemFloodFill()
	{
		throw new Exception("This method has been moved. Use the method on the context object that can be sent with work item delegates instead");
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000C2FA File Offset: 0x0000A6FA
	[Obsolete("This method has been moved. Use the method on the context object that can be sent with work item delegates instead")]
	public void EnsureValidFloodFill()
	{
		throw new Exception("This method has been moved. Use the method on the context object that can be sent with work item delegates instead");
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000C306 File Offset: 0x0000A706
	public void AddWorkItem(Action callback)
	{
		this.AddWorkItem(new AstarWorkItem(callback, null));
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000C315 File Offset: 0x0000A715
	public void AddWorkItem(Action<IWorkItemContext> callback)
	{
		this.AddWorkItem(new AstarWorkItem(callback, null));
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000C324 File Offset: 0x0000A724
	public void AddWorkItem(AstarWorkItem item)
	{
		this.workItems.AddWorkItem(item);
		if (!this.workItemLock.Held)
		{
			this.workItemLock = this.PausePathfindingSoon();
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000C350 File Offset: 0x0000A750
	public void QueueGraphUpdates()
	{
		if (!this.graphUpdatesWorkItemAdded)
		{
			this.graphUpdatesWorkItemAdded = true;
			AstarWorkItem workItem = this.graphUpdates.GetWorkItem();
			this.AddWorkItem(new AstarWorkItem(delegate()
			{
				this.graphUpdatesWorkItemAdded = false;
				this.lastGraphUpdate = Time.realtimeSinceStartup;
				workItem.init();
			}, workItem.update));
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000C3B0 File Offset: 0x0000A7B0
	private IEnumerator DelayedGraphUpdate()
	{
		this.graphUpdateRoutineRunning = true;
		yield return new WaitForSeconds(this.graphUpdateBatchingInterval - (Time.realtimeSinceStartup - this.lastGraphUpdate));
		this.QueueGraphUpdates();
		this.graphUpdateRoutineRunning = false;
		yield break;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000C3CB File Offset: 0x0000A7CB
	public void UpdateGraphs(Bounds bounds, float delay)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds), delay);
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000C3DA File Offset: 0x0000A7DA
	public void UpdateGraphs(GraphUpdateObject ob, float delay)
	{
		base.StartCoroutine(this.UpdateGraphsInternal(ob, delay));
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000C3EC File Offset: 0x0000A7EC
	private IEnumerator UpdateGraphsInternal(GraphUpdateObject ob, float delay)
	{
		yield return new WaitForSeconds(delay);
		this.UpdateGraphs(ob);
		yield break;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000C415 File Offset: 0x0000A815
	public void UpdateGraphs(Bounds bounds)
	{
		this.UpdateGraphs(new GraphUpdateObject(bounds));
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000C424 File Offset: 0x0000A824
	public void UpdateGraphs(GraphUpdateObject ob)
	{
		this.graphUpdates.AddToQueue(ob);
		if (this.batchGraphUpdates && Time.realtimeSinceStartup - this.lastGraphUpdate < this.graphUpdateBatchingInterval)
		{
			if (!this.graphUpdateRoutineRunning)
			{
				base.StartCoroutine(this.DelayedGraphUpdate());
			}
		}
		else
		{
			this.QueueGraphUpdates();
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000C482 File Offset: 0x0000A882
	public void FlushGraphUpdates()
	{
		if (this.IsAnyGraphUpdateQueued)
		{
			this.QueueGraphUpdates();
			this.FlushWorkItems();
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000C49C File Offset: 0x0000A89C
	public void FlushWorkItems()
	{
		PathProcessor.GraphUpdateLock graphUpdateLock = this.PausePathfinding();
		this.PerformBlockingActions(true);
		graphUpdateLock.Release();
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000C4C0 File Offset: 0x0000A8C0
	[Obsolete("Use FlushWorkItems() instead")]
	public void FlushWorkItems(bool unblockOnComplete, bool block)
	{
		PathProcessor.GraphUpdateLock graphUpdateLock = this.PausePathfinding();
		this.PerformBlockingActions(block);
		graphUpdateLock.Release();
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000C4E2 File Offset: 0x0000A8E2
	[Obsolete("Use FlushWorkItems instead")]
	public void FlushThreadSafeCallbacks()
	{
		this.FlushWorkItems();
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000C4EC File Offset: 0x0000A8EC
	public static int CalculateThreadCount(ThreadCount count)
	{
		if (count != ThreadCount.AutomaticLowLoad && count != ThreadCount.AutomaticHighLoad)
		{
			return (int)count;
		}
		int num = Mathf.Max(1, SystemInfo.processorCount);
		int num2 = SystemInfo.systemMemorySize;
		if (num2 <= 0)
		{
			UnityEngine.Debug.LogError("Machine reporting that is has <= 0 bytes of RAM. This is definitely not true, assuming 1 GiB");
			num2 = 1024;
		}
		if (num <= 1)
		{
			return 0;
		}
		if (num2 <= 512)
		{
			return 0;
		}
		if (count == ThreadCount.AutomaticHighLoad)
		{
			if (num2 <= 1024)
			{
				num = Math.Min(num, 2);
			}
		}
		else
		{
			num /= 2;
			num = Mathf.Max(1, num);
			if (num2 <= 1024)
			{
				num = Math.Min(num, 2);
			}
			num = Math.Min(num, 6);
		}
		return num;
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000C594 File Offset: 0x0000A994
	protected override void Awake()
	{
		base.Awake();
		AstarPath.active = this;
		if (UnityEngine.Object.FindObjectsOfType(typeof(AstarPath)).Length > 1)
		{
			UnityEngine.Debug.LogError("You should NOT have more than one AstarPath component in the scene at any time.\nThis can cause serious errors since the AstarPath component builds around a singleton pattern.");
		}
		base.useGUILayout = false;
		if (!Application.isPlaying)
		{
			return;
		}
		if (AstarPath.OnAwakeSettings != null)
		{
			AstarPath.OnAwakeSettings();
		}
		GraphModifier.FindAllModifiers();
		RelevantGraphSurface.FindAllGraphSurfaces();
		this.InitializePathProcessor();
		this.InitializeProfiler();
		this.ConfigureReferencesInternal();
		this.InitializeAstarData();
		this.FlushWorkItems();
		this.euclideanEmbedding.dirty = true;
		if (this.scanOnStartup && (!this.data.cacheStartup || this.data.file_cachedStartup == null))
		{
			this.Scan(null);
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000C660 File Offset: 0x0000AA60
	private void InitializePathProcessor()
	{
		int num = AstarPath.CalculateThreadCount(this.threadCount);
		if (!Application.isPlaying)
		{
			num = 0;
		}
		int processors = Mathf.Max(num, 1);
		bool flag = num > 0;
		this.pathProcessor = new PathProcessor(this, this.pathReturnQueue, processors, flag);
		this.pathProcessor.OnPathPreSearch += delegate(Path path)
		{
			OnPathDelegate onPathPreSearch = AstarPath.OnPathPreSearch;
			if (onPathPreSearch != null)
			{
				onPathPreSearch(path);
			}
		};
		this.pathProcessor.OnPathPostSearch += delegate(Path path)
		{
			this.LogPathResults(path);
			OnPathDelegate onPathPostSearch = AstarPath.OnPathPostSearch;
			if (onPathPostSearch != null)
			{
				onPathPostSearch(path);
			}
		};
		this.pathProcessor.OnQueueUnblocked += delegate()
		{
			if (this.euclideanEmbedding.dirty)
			{
				this.euclideanEmbedding.RecalculateCosts();
			}
		};
		if (flag)
		{
			this.graphUpdates.EnableMultithreading();
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000C710 File Offset: 0x0000AB10
	internal void VerifyIntegrity()
	{
		if (AstarPath.active != this)
		{
			throw new Exception("Singleton pattern broken. Make sure you only have one AstarPath object in the scene");
		}
		if (this.data == null)
		{
			throw new NullReferenceException("data is null... A* not set up correctly?");
		}
		if (this.data.graphs == null)
		{
			this.data.graphs = new NavGraph[0];
			this.data.UpdateShortcuts();
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x0000C77C File Offset: 0x0000AB7C
	public void ConfigureReferencesInternal()
	{
		AstarPath.active = this;
		this.data = (this.data ?? new AstarData());
		this.colorSettings = (this.colorSettings ?? new AstarColor());
		this.colorSettings.OnEnable();
	}

	// Token: 0x06000259 RID: 601 RVA: 0x0000C7CA File Offset: 0x0000ABCA
	private void InitializeProfiler()
	{
	}

	// Token: 0x0600025A RID: 602 RVA: 0x0000C7CC File Offset: 0x0000ABCC
	private void InitializeAstarData()
	{
		this.data.FindGraphTypes();
		this.data.Awake();
		this.data.UpdateShortcuts();
	}

	// Token: 0x0600025B RID: 603 RVA: 0x0000C7EF File Offset: 0x0000ABEF
	private void OnDisable()
	{
		this.gizmos.ClearCache();
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0000C7FC File Offset: 0x0000ABFC
	private void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("+++ AstarPath Component Destroyed - Cleaning Up Pathfinding Data +++");
		}
		if (AstarPath.active != this)
		{
			return;
		}
		this.PausePathfinding();
		this.euclideanEmbedding.dirty = false;
		this.FlushWorkItems();
		this.pathProcessor.queue.TerminateReceivers();
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Processing Possible Work Items");
		}
		this.graphUpdates.DisableMultithreading();
		this.pathProcessor.JoinThreads();
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Returning Paths");
		}
		this.pathReturnQueue.ReturnPaths(false);
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Destroying Graphs");
		}
		this.data.OnDestroy();
		if (this.logPathResults == PathLog.Heavy)
		{
			UnityEngine.Debug.Log("Cleaning up variables");
		}
		AstarPath.OnAwakeSettings = null;
		AstarPath.OnGraphPreScan = null;
		AstarPath.OnGraphPostScan = null;
		AstarPath.OnPathPreSearch = null;
		AstarPath.OnPathPostSearch = null;
		AstarPath.OnPreScan = null;
		AstarPath.OnPostScan = null;
		AstarPath.OnLatePostScan = null;
		AstarPath.On65KOverflow = null;
		AstarPath.OnGraphsUpdated = null;
		AstarPath.active = null;
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0000C92B File Offset: 0x0000AD2B
	public void FloodFill(GraphNode seed)
	{
		this.graphUpdates.FloodFill(seed);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0000C939 File Offset: 0x0000AD39
	public void FloodFill(GraphNode seed, uint area)
	{
		this.graphUpdates.FloodFill(seed, area);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000C948 File Offset: 0x0000AD48
	[ContextMenu("Flood Fill Graphs")]
	public void FloodFill()
	{
		this.graphUpdates.FloodFill();
		this.workItems.OnFloodFill();
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0000C960 File Offset: 0x0000AD60
	internal int GetNewNodeIndex()
	{
		return this.pathProcessor.GetNewNodeIndex();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0000C96D File Offset: 0x0000AD6D
	internal void InitializeNode(GraphNode node)
	{
		this.pathProcessor.InitializeNode(node);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000C97B File Offset: 0x0000AD7B
	internal void DestroyNode(GraphNode node)
	{
		this.pathProcessor.DestroyNode(node);
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0000C989 File Offset: 0x0000AD89
	[Obsolete("Use PausePathfinding instead. Make sure to call Release on the returned lock.", true)]
	public void BlockUntilPathQueueBlocked()
	{
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0000C98B File Offset: 0x0000AD8B
	public PathProcessor.GraphUpdateLock PausePathfinding()
	{
		return this.pathProcessor.PausePathfinding(true);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000C999 File Offset: 0x0000AD99
	private PathProcessor.GraphUpdateLock PausePathfindingSoon()
	{
		return this.pathProcessor.PausePathfinding(false);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000C9A7 File Offset: 0x0000ADA7
	public void Scan(NavGraph graphToScan)
	{
		if (graphToScan == null)
		{
			throw new ArgumentNullException();
		}
		this.Scan(new NavGraph[]
		{
			graphToScan
		});
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000C9C8 File Offset: 0x0000ADC8
	public void Scan(NavGraph[] graphsToScan = null)
	{
		Progress progress = default(Progress);
		foreach (Progress progress2 in this.ScanAsync(graphsToScan))
		{
			if (progress.description != progress2.description)
			{
			}
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000CA3C File Offset: 0x0000AE3C
	[Obsolete("ScanLoop is now named ScanAsync and is an IEnumerable<Progress>. Use foreach to iterate over the progress insead")]
	public void ScanLoop(OnScanStatus statusCallback)
	{
		foreach (Progress progress in this.ScanAsync(null))
		{
			statusCallback(progress);
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000CA98 File Offset: 0x0000AE98
	public IEnumerable<Progress> ScanAsync(NavGraph graphToScan)
	{
		if (graphToScan == null)
		{
			throw new ArgumentNullException();
		}
		return this.ScanAsync(new NavGraph[]
		{
			graphToScan
		});
	}

	// Token: 0x0600026A RID: 618 RVA: 0x0000CAB8 File Offset: 0x0000AEB8
	public IEnumerable<Progress> ScanAsync(NavGraph[] graphsToScan = null)
	{
		if (graphsToScan == null)
		{
			graphsToScan = this.graphs;
		}
		if (graphsToScan == null)
		{
			yield break;
		}
		if (this.isScanning)
		{
			throw new InvalidOperationException("Another async scan is already running");
		}
		this.isScanning = true;
		this.VerifyIntegrity();
		PathProcessor.GraphUpdateLock graphUpdateLock = this.PausePathfinding();
		this.pathReturnQueue.ReturnPaths(false);
		if (!Application.isPlaying)
		{
			this.data.FindGraphTypes();
			GraphModifier.FindAllModifiers();
		}
		yield return new Progress(0.05f, "Pre processing graphs");
		if (AstarPath.OnPreScan != null)
		{
			AstarPath.OnPreScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PreScan);
		this.data.LockGraphStructure(false);
		Stopwatch watch = Stopwatch.StartNew();
		for (int j = 0; j < graphsToScan.Length; j++)
		{
			if (graphsToScan[j] != null)
			{
				((IGraphInternals)graphsToScan[j]).DestroyAllNodes();
			}
		}
		for (int i = 0; i < graphsToScan.Length; i++)
		{
			if (graphsToScan[i] != null)
			{
				float minp = Mathf.Lerp(0.1f, 0.8f, (float)i / (float)graphsToScan.Length);
				float maxp = Mathf.Lerp(0.1f, 0.8f, ((float)i + 0.95f) / (float)graphsToScan.Length);
				string progressDescriptionPrefix = string.Concat(new object[]
				{
					"Scanning graph ",
					i + 1,
					" of ",
					graphsToScan.Length,
					" - "
				});
				IEnumerator<Progress> coroutine = this.ScanGraph(graphsToScan[i]).GetEnumerator();
				for (;;)
				{
					try
					{
						if (!coroutine.MoveNext())
						{
							break;
						}
					}
					catch
					{
						this.isScanning = false;
						this.data.UnlockGraphStructure();
						graphUpdateLock.Release();
						throw;
					}
					Progress progress = coroutine.Current;
					yield return progress.MapTo(minp, maxp, progressDescriptionPrefix);
				}
			}
		}
		this.data.UnlockGraphStructure();
		yield return new Progress(0.8f, "Post processing graphs");
		if (AstarPath.OnPostScan != null)
		{
			AstarPath.OnPostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.PostScan);
		this.FlushWorkItems();
		yield return new Progress(0.9f, "Computing areas");
		this.FloodFill();
		yield return new Progress(0.95f, "Late post processing");
		this.isScanning = false;
		if (AstarPath.OnLatePostScan != null)
		{
			AstarPath.OnLatePostScan(this);
		}
		GraphModifier.TriggerEvent(GraphModifier.EventType.LatePostScan);
		this.euclideanEmbedding.dirty = true;
		this.euclideanEmbedding.RecalculatePivots();
		this.FlushWorkItems();
		graphUpdateLock.Release();
		watch.Stop();
		this.lastScanTime = (float)watch.Elapsed.TotalSeconds;
		GC.Collect();
		if (this.logPathResults != PathLog.None && this.logPathResults != PathLog.OnlyErrors)
		{
			UnityEngine.Debug.Log("Scanning - Process took " + (this.lastScanTime * 1000f).ToString("0") + " ms to complete");
		}
		yield break;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000CAEC File Offset: 0x0000AEEC
	private IEnumerable<Progress> ScanGraph(NavGraph graph)
	{
		if (AstarPath.OnGraphPreScan != null)
		{
			yield return new Progress(0f, "Pre processing");
			AstarPath.OnGraphPreScan(graph);
		}
		yield return new Progress(0f, string.Empty);
		foreach (Progress p in ((IGraphInternals)graph).ScanInternal())
		{
			yield return p.MapTo(0f, 0.95f, null);
		}
		yield return new Progress(0.95f, "Assigning graph indices");
		graph.GetNodes(delegate(GraphNode node)
		{
			node.GraphIndex = graph.graphIndex;
		});
		if (AstarPath.OnGraphPostScan != null)
		{
			yield return new Progress(0.99f, "Post processing");
			AstarPath.OnGraphPostScan(graph);
		}
		yield break;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000CB0F File Offset: 0x0000AF0F
	[Obsolete("This method has been renamed to BlockUntilCalculated")]
	public static void WaitForPath(Path path)
	{
		AstarPath.BlockUntilCalculated(path);
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000CB18 File Offset: 0x0000AF18
	public static void BlockUntilCalculated(Path path)
	{
		if (AstarPath.active == null)
		{
			throw new Exception("Pathfinding is not correctly initialized in this scene (yet?). AstarPath.active is null.\nDo not call this function in Awake");
		}
		if (path == null)
		{
			throw new ArgumentNullException("Path must not be null");
		}
		if (AstarPath.active.pathProcessor.queue.IsTerminating)
		{
			return;
		}
		if (path.PipelineState == PathState.Created)
		{
			throw new Exception("The specified path has not been started yet.");
		}
		AstarPath.waitForPathDepth++;
		if (AstarPath.waitForPathDepth == 5)
		{
			UnityEngine.Debug.LogError("You are calling the BlockUntilCalculated function recursively (maybe from a path callback). Please don't do this.");
		}
		if (path.PipelineState < PathState.ReturnQueue)
		{
			if (AstarPath.active.IsUsingMultithreading)
			{
				while (path.PipelineState < PathState.ReturnQueue)
				{
					if (AstarPath.active.pathProcessor.queue.IsTerminating)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Pathfinding Threads seem to have crashed.");
					}
					Thread.Sleep(1);
					AstarPath.active.PerformBlockingActions(true);
				}
			}
			else
			{
				while (path.PipelineState < PathState.ReturnQueue)
				{
					if (AstarPath.active.pathProcessor.queue.IsEmpty && path.PipelineState != PathState.Processing)
					{
						AstarPath.waitForPathDepth--;
						throw new Exception("Critical error. Path Queue is empty but the path state is '" + path.PipelineState + "'");
					}
					AstarPath.active.pathProcessor.TickNonMultithreaded();
					AstarPath.active.PerformBlockingActions(true);
				}
			}
		}
		AstarPath.active.pathReturnQueue.ReturnPaths(false);
		AstarPath.waitForPathDepth--;
	}

	// Token: 0x0600026E RID: 622 RVA: 0x0000CCAC File Offset: 0x0000B0AC
	[Obsolete("The threadSafe parameter has been deprecated")]
	public static void RegisterSafeUpdate(Action callback, bool threadSafe)
	{
		AstarPath.RegisterSafeUpdate(callback);
	}

	// Token: 0x0600026F RID: 623 RVA: 0x0000CCB4 File Offset: 0x0000B0B4
	[Obsolete("Use AddWorkItem(System.Action) instead. Note the slight change in behavior (mentioned in the documentation).")]
	public static void RegisterSafeUpdate(Action callback)
	{
		AstarPath.active.AddWorkItem(new AstarWorkItem(callback, null));
	}

	// Token: 0x06000270 RID: 624 RVA: 0x0000CCC8 File Offset: 0x0000B0C8
	public static void StartPath(Path path, bool pushToFront = false)
	{
		AstarPath astarPath = AstarPath.active;
		if (object.ReferenceEquals(astarPath, null))
		{
			UnityEngine.Debug.LogError("There is no AstarPath object in the scene or it has not been initialized yet");
			return;
		}
		if (path.PipelineState != PathState.Created)
		{
			throw new Exception(string.Concat(new object[]
			{
				"The path has an invalid state. Expected ",
				PathState.Created,
				" found ",
				path.PipelineState,
				"\nMake sure you are not requesting the same path twice"
			}));
		}
		if (astarPath.pathProcessor.queue.IsTerminating)
		{
			path.FailWithError("No new paths are accepted");
			return;
		}
		if (astarPath.graphs == null || astarPath.graphs.Length == 0)
		{
			UnityEngine.Debug.LogError("There are no graphs in the scene");
			path.FailWithError("There are no graphs in the scene");
			UnityEngine.Debug.LogError(path.errorLog);
			return;
		}
		path.Claim(astarPath);
		((IPathInternals)path).AdvanceState(PathState.PathQueue);
		if (pushToFront)
		{
			astarPath.pathProcessor.queue.PushFront(path);
		}
		else
		{
			astarPath.pathProcessor.queue.Push(path);
		}
		if (!Application.isPlaying)
		{
			AstarPath.BlockUntilCalculated(path);
		}
	}

	// Token: 0x06000271 RID: 625 RVA: 0x0000CDE2 File Offset: 0x0000B1E2
	private void OnApplicationQuit()
	{
		this.OnDestroy();
		this.pathProcessor.AbortThreads();
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000CDF5 File Offset: 0x0000B1F5
	public NNInfo GetNearest(Vector3 position)
	{
		return this.GetNearest(position, NNConstraint.None);
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000CE03 File Offset: 0x0000B203
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint)
	{
		return this.GetNearest(position, constraint, null);
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000CE10 File Offset: 0x0000B210
	public NNInfo GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
	{
		NavGraph[] graphs = this.graphs;
		float num = float.PositiveInfinity;
		NNInfoInternal internalInfo = default(NNInfoInternal);
		int num2 = -1;
		if (graphs != null)
		{
			for (int i = 0; i < graphs.Length; i++)
			{
				NavGraph navGraph = graphs[i];
				if (navGraph != null && constraint.SuitableGraph(i, navGraph))
				{
					NNInfoInternal nninfoInternal;
					if (this.fullGetNearestSearch)
					{
						nninfoInternal = navGraph.GetNearestForce(position, constraint);
					}
					else
					{
						nninfoInternal = navGraph.GetNearest(position, constraint);
					}
					if (nninfoInternal.node != null)
					{
						float magnitude = (nninfoInternal.clampedPosition - position).magnitude;
						if (this.prioritizeGraphs && magnitude < this.prioritizeGraphsLimit)
						{
							internalInfo = nninfoInternal;
							num2 = i;
							break;
						}
						if (magnitude < num)
						{
							num = magnitude;
							internalInfo = nninfoInternal;
							num2 = i;
						}
					}
				}
			}
		}
		if (num2 == -1)
		{
			return default(NNInfo);
		}
		if (internalInfo.constrainedNode != null)
		{
			internalInfo.node = internalInfo.constrainedNode;
			internalInfo.clampedPosition = internalInfo.constClampedPosition;
		}
		if (!this.fullGetNearestSearch && internalInfo.node != null && !constraint.Suitable(internalInfo.node))
		{
			NNInfoInternal nearestForce = graphs[num2].GetNearestForce(position, constraint);
			if (nearestForce.node != null)
			{
				internalInfo = nearestForce;
			}
		}
		if (!constraint.Suitable(internalInfo.node) || (constraint.constrainDistance && (internalInfo.clampedPosition - position).sqrMagnitude > this.maxNearestNodeDistanceSqr))
		{
			return default(NNInfo);
		}
		return new NNInfo(internalInfo);
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000CFCC File Offset: 0x0000B3CC
	public GraphNode GetNearest(Ray ray)
	{
		if (this.graphs == null)
		{
			return null;
		}
		float minDist = float.PositiveInfinity;
		GraphNode nearestNode = null;
		Vector3 lineDirection = ray.direction;
		Vector3 lineOrigin = ray.origin;
		for (int i = 0; i < this.graphs.Length; i++)
		{
			NavGraph navGraph = this.graphs[i];
			navGraph.GetNodes(delegate(GraphNode node)
			{
				Vector3 vector = (Vector3)node.position;
				Vector3 a = lineOrigin + Vector3.Dot(vector - lineOrigin, lineDirection) * lineDirection;
				float num = Mathf.Abs(a.x - vector.x);
				num *= num;
				if (num > minDist)
				{
					return;
				}
				num = Mathf.Abs(a.z - vector.z);
				num *= num;
				if (num > minDist)
				{
					return;
				}
				float sqrMagnitude = (a - vector).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					nearestNode = node;
				}
			});
		}
		return nearestNode;
	}

	// Token: 0x04000120 RID: 288
	public static readonly Version Version = new Version(4, 1, 16);

	// Token: 0x04000121 RID: 289
	public static readonly AstarPath.AstarDistribution Distribution = AstarPath.AstarDistribution.WebsiteDownload;

	// Token: 0x04000122 RID: 290
	public static readonly string Branch = "master_Pro";

	// Token: 0x04000123 RID: 291
	[FormerlySerializedAs("astarData")]
	public AstarData data;

	// Token: 0x04000124 RID: 292
	public static AstarPath active;

	// Token: 0x04000125 RID: 293
	public bool showNavGraphs = true;

	// Token: 0x04000126 RID: 294
	public bool showUnwalkableNodes = true;

	// Token: 0x04000127 RID: 295
	public GraphDebugMode debugMode;

	// Token: 0x04000128 RID: 296
	public float debugFloor;

	// Token: 0x04000129 RID: 297
	public float debugRoof = 20000f;

	// Token: 0x0400012A RID: 298
	public bool manualDebugFloorRoof;

	// Token: 0x0400012B RID: 299
	public bool showSearchTree;

	// Token: 0x0400012C RID: 300
	public float unwalkableNodeDebugSize = 0.3f;

	// Token: 0x0400012D RID: 301
	public PathLog logPathResults = PathLog.Normal;

	// Token: 0x0400012E RID: 302
	public float maxNearestNodeDistance = 100f;

	// Token: 0x0400012F RID: 303
	public bool scanOnStartup = true;

	// Token: 0x04000130 RID: 304
	public bool fullGetNearestSearch;

	// Token: 0x04000131 RID: 305
	public bool prioritizeGraphs;

	// Token: 0x04000132 RID: 306
	public float prioritizeGraphsLimit = 1f;

	// Token: 0x04000133 RID: 307
	public AstarColor colorSettings;

	// Token: 0x04000134 RID: 308
	[SerializeField]
	protected string[] tagNames;

	// Token: 0x04000135 RID: 309
	public Heuristic heuristic = Heuristic.Euclidean;

	// Token: 0x04000136 RID: 310
	public float heuristicScale = 1f;

	// Token: 0x04000137 RID: 311
	public ThreadCount threadCount = ThreadCount.One;

	// Token: 0x04000138 RID: 312
	public float maxFrameTime = 1f;

	// Token: 0x04000139 RID: 313
	[Obsolete("Minimum area size is mostly obsolete since the limit has been raised significantly, and the edge cases are handled automatically")]
	public int minAreaSize;

	// Token: 0x0400013A RID: 314
	public bool batchGraphUpdates;

	// Token: 0x0400013B RID: 315
	public float graphUpdateBatchingInterval = 0.2f;

	// Token: 0x0400013D RID: 317
	[NonSerialized]
	public PathHandler debugPathData;

	// Token: 0x0400013E RID: 318
	[NonSerialized]
	public ushort debugPathID;

	// Token: 0x0400013F RID: 319
	private string inGameDebugPath;

	// Token: 0x04000140 RID: 320
	[NonSerialized]
	private bool isScanningBacking;

	// Token: 0x04000141 RID: 321
	public static Action OnAwakeSettings;

	// Token: 0x04000142 RID: 322
	public static OnGraphDelegate OnGraphPreScan;

	// Token: 0x04000143 RID: 323
	public static OnGraphDelegate OnGraphPostScan;

	// Token: 0x04000144 RID: 324
	public static OnPathDelegate OnPathPreSearch;

	// Token: 0x04000145 RID: 325
	public static OnPathDelegate OnPathPostSearch;

	// Token: 0x04000146 RID: 326
	public static OnScanDelegate OnPreScan;

	// Token: 0x04000147 RID: 327
	public static OnScanDelegate OnPostScan;

	// Token: 0x04000148 RID: 328
	public static OnScanDelegate OnLatePostScan;

	// Token: 0x04000149 RID: 329
	public static OnScanDelegate OnGraphsUpdated;

	// Token: 0x0400014A RID: 330
	public static Action On65KOverflow;

	// Token: 0x0400014B RID: 331
	[Obsolete]
	public Action OnGraphsWillBeUpdated;

	// Token: 0x0400014C RID: 332
	[Obsolete]
	public Action OnGraphsWillBeUpdated2;

	// Token: 0x0400014D RID: 333
	private readonly GraphUpdateProcessor graphUpdates;

	// Token: 0x0400014E RID: 334
	private readonly WorkItemProcessor workItems;

	// Token: 0x0400014F RID: 335
	private PathProcessor pathProcessor;

	// Token: 0x04000150 RID: 336
	private bool graphUpdateRoutineRunning;

	// Token: 0x04000151 RID: 337
	private bool graphUpdatesWorkItemAdded;

	// Token: 0x04000152 RID: 338
	private float lastGraphUpdate = -9999f;

	// Token: 0x04000153 RID: 339
	private PathProcessor.GraphUpdateLock workItemLock;

	// Token: 0x04000154 RID: 340
	internal readonly PathReturnQueue pathReturnQueue;

	// Token: 0x04000155 RID: 341
	public EuclideanEmbedding euclideanEmbedding = new EuclideanEmbedding();

	// Token: 0x04000156 RID: 342
	public bool showGraphs;

	// Token: 0x04000157 RID: 343
	private ushort nextFreePathID = 1;

	// Token: 0x04000158 RID: 344
	private RetainedGizmos gizmos = new RetainedGizmos();

	// Token: 0x04000159 RID: 345
	private static int waitForPathDepth = 0;

	// Token: 0x02000030 RID: 48
	public enum AstarDistribution
	{
		// Token: 0x0400015C RID: 348
		WebsiteDownload,
		// Token: 0x0400015D RID: 349
		AssetStore
	}
}
