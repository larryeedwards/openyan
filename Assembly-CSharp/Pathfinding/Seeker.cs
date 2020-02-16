using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000D RID: 13
	[AddComponentMenu("Pathfinding/Seeker")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_seeker.php")]
	public class Seeker : VersionedMonoBehaviour
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00006A7C File Offset: 0x00004E7C
		public Seeker()
		{
			this.onPathDelegate = new OnPathDelegate(this.OnPathComplete);
			this.onPartialPathDelegate = new OnPathDelegate(this.OnPartialPathComplete);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006AEB File Offset: 0x00004EEB
		protected override void Awake()
		{
			base.Awake();
			this.startEndModifier.Awake(this);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006AFF File Offset: 0x00004EFF
		public Path GetCurrentPath()
		{
			return this.path;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006B08 File Offset: 0x00004F08
		public void CancelCurrentPathRequest(bool pool = true)
		{
			if (!this.IsDone())
			{
				this.path.FailWithError("Canceled by script (Seeker.CancelCurrentPathRequest)");
				if (pool)
				{
					this.path.Claim(this.path);
					this.path.Release(this.path, false);
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006B59 File Offset: 0x00004F59
		public void OnDestroy()
		{
			this.ReleaseClaimedPath();
			this.startEndModifier.OnDestroy(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006B6D File Offset: 0x00004F6D
		public void ReleaseClaimedPath()
		{
			if (this.prevPath != null)
			{
				this.prevPath.Release(this, true);
				this.prevPath = null;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006B8E File Offset: 0x00004F8E
		public void RegisterModifier(IPathModifier modifier)
		{
			this.modifiers.Add(modifier);
			this.modifiers.Sort((IPathModifier a, IPathModifier b) => a.Order.CompareTo(b.Order));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006BC4 File Offset: 0x00004FC4
		public void DeregisterModifier(IPathModifier modifier)
		{
			this.modifiers.Remove(modifier);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006BD3 File Offset: 0x00004FD3
		public void PostProcess(Path path)
		{
			this.RunModifiers(Seeker.ModifierPass.PostProcess, path);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006BE0 File Offset: 0x00004FE0
		public void RunModifiers(Seeker.ModifierPass pass, Path path)
		{
			if (pass == Seeker.ModifierPass.PreProcess)
			{
				if (this.preProcessPath != null)
				{
					this.preProcessPath(path);
				}
				for (int i = 0; i < this.modifiers.Count; i++)
				{
					this.modifiers[i].PreProcess(path);
				}
			}
			else if (pass == Seeker.ModifierPass.PostProcess)
			{
				if (this.postProcessPath != null)
				{
					this.postProcessPath(path);
				}
				for (int j = 0; j < this.modifiers.Count; j++)
				{
					this.modifiers[j].Apply(path);
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006C89 File Offset: 0x00005089
		public bool IsDone()
		{
			return this.path == null || this.path.PipelineState >= PathState.Returned;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006CAA File Offset: 0x000050AA
		private void OnPathComplete(Path path)
		{
			this.OnPathComplete(path, true, true);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006CB8 File Offset: 0x000050B8
		private void OnPathComplete(Path p, bool runModifiers, bool sendCallbacks)
		{
			if (p != null && p != this.path && sendCallbacks)
			{
				return;
			}
			if (this == null || p == null || p != this.path)
			{
				return;
			}
			if (!this.path.error && runModifiers)
			{
				this.RunModifiers(Seeker.ModifierPass.PostProcess, this.path);
			}
			if (sendCallbacks)
			{
				p.Claim(this);
				this.lastCompletedNodePath = p.path;
				this.lastCompletedVectorPath = p.vectorPath;
				if (this.tmpPathCallback != null)
				{
					this.tmpPathCallback(p);
				}
				if (this.pathCallback != null)
				{
					this.pathCallback(p);
				}
				if (this.prevPath != null)
				{
					this.prevPath.Release(this, true);
				}
				this.prevPath = p;
				if (!this.drawGizmos)
				{
					this.ReleaseClaimedPath();
				}
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006DA3 File Offset: 0x000051A3
		private void OnPartialPathComplete(Path p)
		{
			this.OnPathComplete(p, true, false);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006DAE File Offset: 0x000051AE
		private void OnMultiPathComplete(Path p)
		{
			this.OnPathComplete(p, false, true);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006DB9 File Offset: 0x000051B9
		[Obsolete("Use ABPath.Construct(start, end, null) instead")]
		public ABPath GetNewPath(Vector3 start, Vector3 end)
		{
			return ABPath.Construct(start, end, null);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006DC3 File Offset: 0x000051C3
		public Path StartPath(Vector3 start, Vector3 end)
		{
			return this.StartPath(start, end, null);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006DCE File Offset: 0x000051CE
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006DDF File Offset: 0x000051DF
		public Path StartPath(Vector3 start, Vector3 end, OnPathDelegate callback, int graphMask)
		{
			return this.StartPath(ABPath.Construct(start, end, null), callback, graphMask);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006DF2 File Offset: 0x000051F2
		public Path StartPath(Path p, OnPathDelegate callback = null)
		{
			if (p.nnConstraint.graphMask == -1)
			{
				p.nnConstraint.graphMask = this.graphMask;
			}
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006E1F File Offset: 0x0000521F
		public Path StartPath(Path p, OnPathDelegate callback, int graphMask)
		{
			p.nnConstraint.graphMask = graphMask;
			this.StartPathInternal(p, callback);
			return p;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006E38 File Offset: 0x00005238
		private void StartPathInternal(Path p, OnPathDelegate callback)
		{
			MultiTargetPath multiTargetPath = p as MultiTargetPath;
			if (multiTargetPath != null)
			{
				OnPathDelegate[] array = new OnPathDelegate[multiTargetPath.targetPoints.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.onPartialPathDelegate;
				}
				multiTargetPath.callbacks = array;
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, new OnPathDelegate(this.OnMultiPathComplete));
			}
			else
			{
				p.callback = (OnPathDelegate)Delegate.Combine(p.callback, this.onPathDelegate);
			}
			p.enabledTags = this.traversableTags;
			p.tagPenalties = this.tagPenalties;
			if (this.path != null && this.path.PipelineState <= PathState.Processing && this.path.CompleteState != PathCompleteState.Error && this.lastPathID == (uint)this.path.pathID)
			{
				this.path.FailWithError("Canceled path because a new one was requested.\nThis happens when a new path is requested from the seeker when one was already being calculated.\nFor example if a unit got a new order, you might request a new path directly instead of waiting for the now invalid path to be calculated. Which is probably what you want.\nIf you are getting this a lot, you might want to consider how you are scheduling path requests.");
			}
			this.path = p;
			this.tmpPathCallback = callback;
			this.lastPathID = (uint)this.path.pathID;
			this.RunModifiers(Seeker.ModifierPass.PreProcess, this.path);
			AstarPath.StartPath(this.path, false);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006F6C File Offset: 0x0000536C
		public MultiTargetPath StartMultiTargetPath(Vector3 start, Vector3[] endPoints, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(start, endPoints, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006F98 File Offset: 0x00005398
		public MultiTargetPath StartMultiTargetPath(Vector3[] startPoints, Vector3 end, bool pathsForAll, OnPathDelegate callback = null, int graphMask = -1)
		{
			MultiTargetPath multiTargetPath = MultiTargetPath.Construct(startPoints, end, null, null);
			multiTargetPath.pathsForAll = pathsForAll;
			this.StartPath(multiTargetPath, callback, graphMask);
			return multiTargetPath;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006FC3 File Offset: 0x000053C3
		[Obsolete("You can use StartPath instead of this method now. It will behave identically.")]
		public MultiTargetPath StartMultiTargetPath(MultiTargetPath p, OnPathDelegate callback = null, int graphMask = -1)
		{
			this.StartPath(p, callback, graphMask);
			return p;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006FD0 File Offset: 0x000053D0
		public void OnDrawGizmos()
		{
			if (this.lastCompletedNodePath == null || !this.drawGizmos)
			{
				return;
			}
			if (this.detailedGizmos)
			{
				Gizmos.color = new Color(0.7f, 0.5f, 0.1f, 0.5f);
				if (this.lastCompletedNodePath != null)
				{
					for (int i = 0; i < this.lastCompletedNodePath.Count - 1; i++)
					{
						Gizmos.DrawLine((Vector3)this.lastCompletedNodePath[i].position, (Vector3)this.lastCompletedNodePath[i + 1].position);
					}
				}
			}
			Gizmos.color = new Color(0f, 1f, 0f, 1f);
			if (this.lastCompletedVectorPath != null)
			{
				for (int j = 0; j < this.lastCompletedVectorPath.Count - 1; j++)
				{
					Gizmos.DrawLine(this.lastCompletedVectorPath[j], this.lastCompletedVectorPath[j + 1]);
				}
			}
		}

		// Token: 0x04000082 RID: 130
		public bool drawGizmos = true;

		// Token: 0x04000083 RID: 131
		public bool detailedGizmos;

		// Token: 0x04000084 RID: 132
		[HideInInspector]
		public StartEndModifier startEndModifier = new StartEndModifier();

		// Token: 0x04000085 RID: 133
		[HideInInspector]
		public int traversableTags = -1;

		// Token: 0x04000086 RID: 134
		[HideInInspector]
		public int[] tagPenalties = new int[32];

		// Token: 0x04000087 RID: 135
		[HideInInspector]
		public int graphMask = -1;

		// Token: 0x04000088 RID: 136
		public OnPathDelegate pathCallback;

		// Token: 0x04000089 RID: 137
		public OnPathDelegate preProcessPath;

		// Token: 0x0400008A RID: 138
		public OnPathDelegate postProcessPath;

		// Token: 0x0400008B RID: 139
		[NonSerialized]
		private List<Vector3> lastCompletedVectorPath;

		// Token: 0x0400008C RID: 140
		[NonSerialized]
		private List<GraphNode> lastCompletedNodePath;

		// Token: 0x0400008D RID: 141
		[NonSerialized]
		protected Path path;

		// Token: 0x0400008E RID: 142
		[NonSerialized]
		private Path prevPath;

		// Token: 0x0400008F RID: 143
		private readonly OnPathDelegate onPathDelegate;

		// Token: 0x04000090 RID: 144
		private readonly OnPathDelegate onPartialPathDelegate;

		// Token: 0x04000091 RID: 145
		private OnPathDelegate tmpPathCallback;

		// Token: 0x04000092 RID: 146
		protected uint lastPathID;

		// Token: 0x04000093 RID: 147
		private readonly List<IPathModifier> modifiers = new List<IPathModifier>();

		// Token: 0x0200000E RID: 14
		public enum ModifierPass
		{
			// Token: 0x04000096 RID: 150
			PreProcess,
			// Token: 0x04000097 RID: 151
			PostProcess = 2
		}
	}
}
