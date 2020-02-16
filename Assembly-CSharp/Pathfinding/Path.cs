using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000062 RID: 98
	public abstract class Path : IPathInternals
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00018320 File Offset: 0x00016720
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x00018328 File Offset: 0x00016728
		internal PathState PipelineState { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00018331 File Offset: 0x00016731
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0001833C File Offset: 0x0001673C
		public PathCompleteState CompleteState
		{
			get
			{
				return this.completeState;
			}
			protected set
			{
				object obj = this.stateLock;
				lock (obj)
				{
					if (this.completeState != PathCompleteState.Error)
					{
						this.completeState = value;
					}
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00018388 File Offset: 0x00016788
		public bool error
		{
			get
			{
				return this.CompleteState == PathCompleteState.Error;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00018393 File Offset: 0x00016793
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0001839B File Offset: 0x0001679B
		public string errorLog { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000183A4 File Offset: 0x000167A4
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x000183AC File Offset: 0x000167AC
		bool IPathInternals.Pooled { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000183B5 File Offset: 0x000167B5
		[Obsolete("Has been renamed to 'Pooled' to use more widely underestood terminology", true)]
		internal bool recycled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000183B8 File Offset: 0x000167B8
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x000183C0 File Offset: 0x000167C0
		internal ushort pathID { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000183C9 File Offset: 0x000167C9
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x000183D1 File Offset: 0x000167D1
		public int[] tagPenalties
		{
			get
			{
				return this.manualTagPenalties;
			}
			set
			{
				if (value == null || value.Length != 32)
				{
					this.manualTagPenalties = null;
					this.internalTagPenalties = Path.ZeroTagPenalties;
				}
				else
				{
					this.manualTagPenalties = value;
					this.internalTagPenalties = value;
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00018408 File Offset: 0x00016808
		internal virtual bool FloodingPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001840C File Offset: 0x0001680C
		public float GetTotalLength()
		{
			if (this.vectorPath == null)
			{
				return float.PositiveInfinity;
			}
			float num = 0f;
			for (int i = 0; i < this.vectorPath.Count - 1; i++)
			{
				num += Vector3.Distance(this.vectorPath[i], this.vectorPath[i + 1]);
			}
			return num;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00018474 File Offset: 0x00016874
		public IEnumerator WaitForPath()
		{
			if (this.PipelineState == PathState.Created)
			{
				throw new InvalidOperationException("This path has not been started yet");
			}
			while (this.PipelineState != PathState.Returned)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001848F File Offset: 0x0001688F
		public void BlockUntilCalculated()
		{
			AstarPath.BlockUntilCalculated(this);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00018498 File Offset: 0x00016898
		internal uint CalculateHScore(GraphNode node)
		{
			Heuristic heuristic = this.heuristic;
			uint num;
			if (heuristic == Heuristic.Euclidean)
			{
				num = (uint)((float)(this.GetHTarget() - node.position).costMagnitude * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			if (heuristic == Heuristic.Manhattan)
			{
				Int3 position = node.position;
				num = (uint)((float)(Math.Abs(this.hTarget.x - position.x) + Math.Abs(this.hTarget.y - position.y) + Math.Abs(this.hTarget.z - position.z)) * this.heuristicScale);
				if (this.hTargetNode != null)
				{
					num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
				}
				return num;
			}
			if (heuristic != Heuristic.DiagonalManhattan)
			{
				return 0u;
			}
			Int3 @int = this.GetHTarget() - node.position;
			@int.x = Math.Abs(@int.x);
			@int.y = Math.Abs(@int.y);
			@int.z = Math.Abs(@int.z);
			int num2 = Math.Min(@int.x, @int.z);
			int num3 = Math.Max(@int.x, @int.z);
			num = (uint)((float)(14 * num2 / 10 + (num3 - num2) + @int.y) * this.heuristicScale);
			if (this.hTargetNode != null)
			{
				num = Math.Max(num, AstarPath.active.euclideanEmbedding.GetHeuristic(node.NodeIndex, this.hTargetNode.NodeIndex));
			}
			return num;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00018676 File Offset: 0x00016A76
		internal uint GetTagPenalty(int tag)
		{
			return (uint)this.internalTagPenalties[tag];
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00018680 File Offset: 0x00016A80
		internal Int3 GetHTarget()
		{
			return this.hTarget;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00018688 File Offset: 0x00016A88
		internal bool CanTraverse(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.CanTraverse(this, node);
			}
			return node.Walkable && (this.enabledTags >> (int)node.Tag & 1) != 0;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000186D4 File Offset: 0x00016AD4
		internal uint GetTraversalCost(GraphNode node)
		{
			if (this.traversalProvider != null)
			{
				return this.traversalProvider.GetTraversalCost(this, node);
			}
			return this.GetTagPenalty((int)node.Tag) + node.Penalty;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00018702 File Offset: 0x00016B02
		internal virtual uint GetConnectionSpecialCost(GraphNode a, GraphNode b, uint currentCost)
		{
			return currentCost;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00018705 File Offset: 0x00016B05
		public bool IsDone()
		{
			return this.CompleteState != PathCompleteState.NotCalculated;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00018714 File Offset: 0x00016B14
		void IPathInternals.AdvanceState(PathState s)
		{
			object obj = this.stateLock;
			lock (obj)
			{
				this.PipelineState = (PathState)Math.Max((int)this.PipelineState, (int)s);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001875C File Offset: 0x00016B5C
		[Obsolete("Use the 'PipelineState' property instead")]
		public PathState GetState()
		{
			return this.PipelineState;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00018764 File Offset: 0x00016B64
		internal void FailWithError(string msg)
		{
			this.Error();
			if (this.errorLog != string.Empty)
			{
				this.errorLog = this.errorLog + "\n" + msg;
			}
			else
			{
				this.errorLog = msg;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000187A4 File Offset: 0x00016BA4
		[Obsolete("Use FailWithError instead")]
		internal void LogError(string msg)
		{
			this.Log(msg);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000187AD File Offset: 0x00016BAD
		[Obsolete("Use FailWithError instead")]
		internal void Log(string msg)
		{
			this.errorLog += msg;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000187C1 File Offset: 0x00016BC1
		public void Error()
		{
			this.CompleteState = PathCompleteState.Error;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000187CC File Offset: 0x00016BCC
		private void ErrorCheck()
		{
			if (!this.hasBeenReset)
			{
				this.FailWithError("Please use the static Construct function for creating paths, do not use the normal constructors.");
			}
			if (((IPathInternals)this).Pooled)
			{
				this.FailWithError("The path is currently in a path pool. Are you sending the path for calculation twice?");
			}
			if (this.pathHandler == null)
			{
				this.FailWithError("Field pathHandler is not set. Please report this bug.");
			}
			if (this.PipelineState > PathState.Processing)
			{
				this.FailWithError("This path has already been processed. Do not request a path with the same path object twice.");
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00018834 File Offset: 0x00016C34
		protected virtual void OnEnterPool()
		{
			if (this.vectorPath != null)
			{
				ListPool<Vector3>.Release(ref this.vectorPath);
			}
			if (this.path != null)
			{
				ListPool<GraphNode>.Release(ref this.path);
			}
			this.callback = null;
			this.immediateCallback = null;
			this.traversalProvider = null;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00018884 File Offset: 0x00016C84
		protected virtual void Reset()
		{
			if (object.ReferenceEquals(AstarPath.active, null))
			{
				throw new NullReferenceException("No AstarPath object found in the scene. Make sure there is one or do not create paths in Awake");
			}
			this.hasBeenReset = true;
			this.PipelineState = PathState.Created;
			this.releasedNotSilent = false;
			this.pathHandler = null;
			this.callback = null;
			this.immediateCallback = null;
			this.errorLog = string.Empty;
			this.completeState = PathCompleteState.NotCalculated;
			this.path = ListPool<GraphNode>.Claim();
			this.vectorPath = ListPool<Vector3>.Claim();
			this.currentR = null;
			this.duration = 0f;
			this.searchedNodes = 0;
			this.nnConstraint = PathNNConstraint.Default;
			this.next = null;
			this.heuristic = AstarPath.active.heuristic;
			this.heuristicScale = AstarPath.active.heuristicScale;
			this.enabledTags = -1;
			this.tagPenalties = null;
			this.pathID = AstarPath.active.GetNextPathID();
			this.hTarget = Int3.zero;
			this.hTargetNode = null;
			this.traversalProvider = null;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00018980 File Offset: 0x00016D80
		public void Claim(object o)
		{
			if (object.ReferenceEquals(o, null))
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (object.ReferenceEquals(this.claimed[i], o))
				{
					throw new ArgumentException("You have already claimed the path with that object (" + o + "). Are you claiming the path with the same object twice?");
				}
			}
			this.claimed.Add(o);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000189F9 File Offset: 0x00016DF9
		[Obsolete("Use Release(o, true) instead")]
		internal void ReleaseSilent(object o)
		{
			this.Release(o, true);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00018A04 File Offset: 0x00016E04
		public void Release(object o, bool silent = false)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			for (int i = 0; i < this.claimed.Count; i++)
			{
				if (object.ReferenceEquals(this.claimed[i], o))
				{
					this.claimed.RemoveAt(i);
					if (!silent)
					{
						this.releasedNotSilent = true;
					}
					if (this.claimed.Count == 0 && this.releasedNotSilent)
					{
						PathPool.Pool(this);
					}
					return;
				}
			}
			if (this.claimed.Count == 0)
			{
				throw new ArgumentException("You are releasing a path which is not claimed at all (most likely it has been pooled already). Are you releasing the path with the same object (" + o + ") twice?\nCheck out the documentation on path pooling for help.");
			}
			throw new ArgumentException("You are releasing a path which has not been claimed with this object (" + o + "). Are you releasing the path with the same object twice?\nCheck out the documentation on path pooling for help.");
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00018ACC File Offset: 0x00016ECC
		protected virtual void Trace(PathNode from)
		{
			PathNode pathNode = from;
			int num = 0;
			while (pathNode != null)
			{
				pathNode = pathNode.parent;
				num++;
				if (num > 2048)
				{
					Debug.LogWarning("Infinite loop? >2048 node path. Remove this message if you really have that long paths (Path.cs, Trace method)");
					break;
				}
			}
			if (this.path.Capacity < num)
			{
				this.path.Capacity = num;
			}
			if (this.vectorPath.Capacity < num)
			{
				this.vectorPath.Capacity = num;
			}
			pathNode = from;
			for (int i = 0; i < num; i++)
			{
				this.path.Add(pathNode.node);
				pathNode = pathNode.parent;
			}
			int num2 = num / 2;
			for (int j = 0; j < num2; j++)
			{
				GraphNode value = this.path[j];
				this.path[j] = this.path[num - j - 1];
				this.path[num - j - 1] = value;
			}
			for (int k = 0; k < num; k++)
			{
				this.vectorPath.Add((Vector3)this.path[k].position);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00018C08 File Offset: 0x00017008
		protected void DebugStringPrefix(PathLog logMode, StringBuilder text)
		{
			text.Append((!this.error) ? "Path Completed : " : "Path Failed : ");
			text.Append("Computation Time ");
			text.Append(this.duration.ToString((logMode != PathLog.Heavy) ? "0.00 ms " : "0.000 ms "));
			text.Append("Searched Nodes ").Append(this.searchedNodes);
			if (!this.error)
			{
				text.Append(" Path Length ");
				text.Append((this.path != null) ? this.path.Count.ToString() : "Null");
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00018CD0 File Offset: 0x000170D0
		protected void DebugStringSuffix(PathLog logMode, StringBuilder text)
		{
			if (this.error)
			{
				text.Append("\nError: ").Append(this.errorLog);
			}
			if (logMode == PathLog.Heavy && !AstarPath.active.IsUsingMultithreading)
			{
				text.Append("\nCallback references ");
				if (this.callback != null)
				{
					text.Append(this.callback.Target.GetType().FullName).AppendLine();
				}
				else
				{
					text.AppendLine("NULL");
				}
			}
			text.Append("\nPath Number ").Append(this.pathID).Append(" (unique id)");
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00018D80 File Offset: 0x00017180
		internal virtual string DebugString(PathLog logMode)
		{
			if (logMode == PathLog.None || (!this.error && logMode == PathLog.OnlyErrors))
			{
				return string.Empty;
			}
			StringBuilder debugStringBuilder = this.pathHandler.DebugStringBuilder;
			debugStringBuilder.Length = 0;
			this.DebugStringPrefix(logMode, debugStringBuilder);
			this.DebugStringSuffix(logMode, debugStringBuilder);
			return debugStringBuilder.ToString();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00018DD4 File Offset: 0x000171D4
		protected virtual void ReturnPath()
		{
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00018DF0 File Offset: 0x000171F0
		protected void PrepareBase(PathHandler pathHandler)
		{
			if (pathHandler.PathID > this.pathID)
			{
				pathHandler.ClearPathIDs();
			}
			this.pathHandler = pathHandler;
			pathHandler.InitializeForPath(this);
			if (this.internalTagPenalties == null || this.internalTagPenalties.Length != 32)
			{
				this.internalTagPenalties = Path.ZeroTagPenalties;
			}
			try
			{
				this.ErrorCheck();
			}
			catch (Exception ex)
			{
				this.FailWithError(ex.Message);
			}
		}

		// Token: 0x06000441 RID: 1089
		protected abstract void Prepare();

		// Token: 0x06000442 RID: 1090 RVA: 0x00018E74 File Offset: 0x00017274
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06000443 RID: 1091
		protected abstract void Initialize();

		// Token: 0x06000444 RID: 1092
		protected abstract void CalculateStep(long targetTick);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00018E76 File Offset: 0x00017276
		PathHandler IPathInternals.PathHandler
		{
			get
			{
				return this.pathHandler;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00018E7E File Offset: 0x0001727E
		void IPathInternals.OnEnterPool()
		{
			this.OnEnterPool();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00018E86 File Offset: 0x00017286
		void IPathInternals.Reset()
		{
			this.Reset();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00018E8E File Offset: 0x0001728E
		void IPathInternals.ReturnPath()
		{
			this.ReturnPath();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00018E96 File Offset: 0x00017296
		void IPathInternals.PrepareBase(PathHandler handler)
		{
			this.PrepareBase(handler);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00018E9F File Offset: 0x0001729F
		void IPathInternals.Prepare()
		{
			this.Prepare();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00018EA7 File Offset: 0x000172A7
		void IPathInternals.Cleanup()
		{
			this.Cleanup();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00018EAF File Offset: 0x000172AF
		void IPathInternals.Initialize()
		{
			this.Initialize();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00018EB7 File Offset: 0x000172B7
		void IPathInternals.CalculateStep(long targetTick)
		{
			this.CalculateStep(targetTick);
		}

		// Token: 0x04000262 RID: 610
		protected PathHandler pathHandler;

		// Token: 0x04000263 RID: 611
		public OnPathDelegate callback;

		// Token: 0x04000264 RID: 612
		public OnPathDelegate immediateCallback;

		// Token: 0x04000266 RID: 614
		private object stateLock = new object();

		// Token: 0x04000267 RID: 615
		public ITraversalProvider traversalProvider;

		// Token: 0x04000268 RID: 616
		protected PathCompleteState completeState;

		// Token: 0x0400026A RID: 618
		public List<GraphNode> path;

		// Token: 0x0400026B RID: 619
		public List<Vector3> vectorPath;

		// Token: 0x0400026C RID: 620
		protected PathNode currentR;

		// Token: 0x0400026D RID: 621
		internal float duration;

		// Token: 0x0400026E RID: 622
		internal int searchedNodes;

		// Token: 0x0400026F RID: 623
		protected bool hasBeenReset;

		// Token: 0x04000270 RID: 624
		public NNConstraint nnConstraint = PathNNConstraint.Default;

		// Token: 0x04000271 RID: 625
		internal Path next;

		// Token: 0x04000272 RID: 626
		public Heuristic heuristic;

		// Token: 0x04000273 RID: 627
		public float heuristicScale = 1f;

		// Token: 0x04000275 RID: 629
		protected GraphNode hTargetNode;

		// Token: 0x04000276 RID: 630
		protected Int3 hTarget;

		// Token: 0x04000277 RID: 631
		public int enabledTags = -1;

		// Token: 0x04000278 RID: 632
		private static readonly int[] ZeroTagPenalties = new int[32];

		// Token: 0x04000279 RID: 633
		protected int[] internalTagPenalties;

		// Token: 0x0400027A RID: 634
		protected int[] manualTagPenalties;

		// Token: 0x0400027B RID: 635
		private List<object> claimed = new List<object>();

		// Token: 0x0400027C RID: 636
		private bool releasedNotSilent;
	}
}
