using System;
using System.Collections.Generic;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009D RID: 157
	public abstract class NavGraph : IGraphInternals
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00025711 File Offset: 0x00023B11
		internal bool exists
		{
			get
			{
				return this.active != null;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00025720 File Offset: 0x00023B20
		public virtual int CountNodes()
		{
			int count = 0;
			this.GetNodes(delegate(GraphNode node)
			{
				count++;
			});
			return count;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00025754 File Offset: 0x00023B54
		public void GetNodes(Func<GraphNode, bool> action)
		{
			bool cont = true;
			this.GetNodes(delegate(GraphNode node)
			{
				if (cont)
				{
					cont &= action(node);
				}
			});
		}

		// Token: 0x060005FB RID: 1531
		public abstract void GetNodes(Action<GraphNode> action);

		// Token: 0x060005FC RID: 1532 RVA: 0x00025787 File Offset: 0x00023B87
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public void SetMatrix(Matrix4x4 m)
		{
			this.matrix = m;
			this.inverseMatrix = m.inverse;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002579D File Offset: 0x00023B9D
		[Obsolete("Use RelocateNodes(Matrix4x4) instead. To keep the same behavior you can call RelocateNodes(newMatrix * oldMatrix.inverse).")]
		public void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			this.RelocateNodes(newMatrix * oldMatrix.inverse);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000257B4 File Offset: 0x00023BB4
		public virtual void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.position = (Int3)deltaMatrix.MultiplyPoint((Vector3)node.position);
			});
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000257E0 File Offset: 0x00023BE0
		public NNInfoInternal GetNearest(Vector3 position)
		{
			return this.GetNearest(position, NNConstraint.None);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000257EE File Offset: 0x00023BEE
		public NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint, null);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000257FC File Offset: 0x00023BFC
		public virtual NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			float maxDistSqr = (constraint != null && !constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistanceSqr;
			float minDist = float.PositiveInfinity;
			GraphNode minNode = null;
			float minConstDist = float.PositiveInfinity;
			GraphNode minConstNode = null;
			this.GetNodes(delegate(GraphNode node)
			{
				float sqrMagnitude = (position - (Vector3)node.position).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					minNode = node;
				}
				if (sqrMagnitude < minConstDist && sqrMagnitude < maxDistSqr && (constraint == null || constraint.Suitable(node)))
				{
					minConstDist = sqrMagnitude;
					minConstNode = node;
				}
			});
			NNInfoInternal result = new NNInfoInternal(minNode);
			result.constrainedNode = minConstNode;
			if (minConstNode != null)
			{
				result.constClampedPosition = (Vector3)minConstNode.position;
			}
			else if (minNode != null)
			{
				result.constrainedNode = minNode;
				result.constClampedPosition = (Vector3)minNode.position;
			}
			return result;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000258F9 File Offset: 0x00023CF9
		public virtual NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00025903 File Offset: 0x00023D03
		protected virtual void OnDestroy()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002590B File Offset: 0x00023D0B
		protected virtual void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.Destroy();
			});
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00025930 File Offset: 0x00023D30
		[Obsolete("Use AstarPath.Scan instead")]
		public void ScanGraph()
		{
			this.Scan();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00025938 File Offset: 0x00023D38
		public void Scan()
		{
			this.active.Scan(this);
		}

		// Token: 0x06000607 RID: 1543
		protected abstract IEnumerable<Progress> ScanInternal();

		// Token: 0x06000608 RID: 1544 RVA: 0x00025946 File Offset: 0x00023D46
		protected virtual void SerializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00025948 File Offset: 0x00023D48
		protected virtual void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002594A File Offset: 0x00023D4A
		protected virtual void PostDeserialization(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002594C File Offset: 0x00023D4C
		protected virtual void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.guid = new Pathfinding.Util.Guid(ctx.reader.ReadBytes(16));
			this.initialPenalty = ctx.reader.ReadUInt32();
			this.open = ctx.reader.ReadBoolean();
			this.name = ctx.reader.ReadString();
			this.drawGizmos = ctx.reader.ReadBoolean();
			this.infoScreenOpen = ctx.reader.ReadBoolean();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000259C8 File Offset: 0x00023DC8
		public virtual void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
			this.GetNodes(delegate(GraphNode node)
			{
				hasher.HashNode(node);
			});
			if (!gizmos.Draw(hasher))
			{
				using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
				{
					this.GetNodes(new Action<GraphNode>(gizmoHelper.DrawConnections));
				}
			}
			if (this.active.showUnwalkableNodes)
			{
				this.DrawUnwalkableNodes(this.active.unwalkableNodeDebugSize);
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00025A80 File Offset: 0x00023E80
		protected void DrawUnwalkableNodes(float size)
		{
			Gizmos.color = AstarColor.UnwalkableNode;
			this.GetNodes(delegate(GraphNode node)
			{
				if (!node.Walkable)
				{
					Gizmos.DrawCube((Vector3)node.position, Vector3.one * size);
				}
			});
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00025AB6 File Offset: 0x00023EB6
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00025ABE File Offset: 0x00023EBE
		string IGraphInternals.SerializedEditorSettings
		{
			get
			{
				return this.serializedEditorSettings;
			}
			set
			{
				this.serializedEditorSettings = value;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00025AC7 File Offset: 0x00023EC7
		void IGraphInternals.OnDestroy()
		{
			this.OnDestroy();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00025ACF File Offset: 0x00023ECF
		void IGraphInternals.DestroyAllNodes()
		{
			this.DestroyAllNodes();
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00025AD7 File Offset: 0x00023ED7
		IEnumerable<Progress> IGraphInternals.ScanInternal()
		{
			return this.ScanInternal();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00025ADF File Offset: 0x00023EDF
		void IGraphInternals.SerializeExtraInfo(GraphSerializationContext ctx)
		{
			this.SerializeExtraInfo(ctx);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00025AE8 File Offset: 0x00023EE8
		void IGraphInternals.DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			this.DeserializeExtraInfo(ctx);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00025AF1 File Offset: 0x00023EF1
		void IGraphInternals.PostDeserialization(GraphSerializationContext ctx)
		{
			this.PostDeserialization(ctx);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00025AFA File Offset: 0x00023EFA
		void IGraphInternals.DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			this.DeserializeSettingsCompatibility(ctx);
		}

		// Token: 0x0400040E RID: 1038
		public AstarPath active;

		// Token: 0x0400040F RID: 1039
		[JsonMember]
		public Pathfinding.Util.Guid guid;

		// Token: 0x04000410 RID: 1040
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x04000411 RID: 1041
		[JsonMember]
		public bool open;

		// Token: 0x04000412 RID: 1042
		public uint graphIndex;

		// Token: 0x04000413 RID: 1043
		[JsonMember]
		public string name;

		// Token: 0x04000414 RID: 1044
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x04000415 RID: 1045
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x04000416 RID: 1046
		[JsonMember]
		private string serializedEditorSettings;

		// Token: 0x04000417 RID: 1047
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 matrix = Matrix4x4.identity;

		// Token: 0x04000418 RID: 1048
		[Obsolete("Use the transform field (only available on some graph types) instead", true)]
		public Matrix4x4 inverseMatrix = Matrix4x4.identity;
	}
}
