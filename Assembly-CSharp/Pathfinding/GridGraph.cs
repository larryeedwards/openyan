using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A1 RID: 161
	[JsonOptIn]
	public class GridGraph : NavGraph, IUpdatableGraph, ITransformedGraph, IRaycastableGraph
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x000263D0 File Offset: 0x000247D0
		public GridGraph()
		{
			this.unclampedSize = new Vector2(10f, 10f);
			this.nodeSize = 1f;
			this.collision = new GraphCollision();
			this.transform = new GraphTransform(Matrix4x4.identity);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000264C9 File Offset: 0x000248C9
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveGridGraphFromStatic();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000264D7 File Offset: 0x000248D7
		protected override void DestroyAllNodes()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				(node as GridNodeBase).ClearCustomConnections(true);
				node.ClearConnections(false);
				node.Destroy();
			});
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000264FC File Offset: 0x000248FC
		private void RemoveGridGraphFromStatic()
		{
			GridNode.SetGridGraph(AstarPath.active.data.GetGraphIndex(this), null);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00026514 File Offset: 0x00024914
		public virtual bool uniformWidthDepthGrid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00026517 File Offset: 0x00024917
		public virtual int LayerCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0002651A File Offset: 0x0002491A
		public override int CountNodes()
		{
			return this.nodes.Length;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00026524 File Offset: 0x00024924
		public override void GetNodes(Action<GraphNode> action)
		{
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				action(this.nodes[i]);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00026564 File Offset: 0x00024964
		protected bool useRaycastNormal
		{
			get
			{
				return Math.Abs(90f - this.maxSlope) > float.Epsilon;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0002657E File Offset: 0x0002497E
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00026586 File Offset: 0x00024986
		public Vector2 size { get; protected set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0002658F File Offset: 0x0002498F
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00026597 File Offset: 0x00024997
		public GraphTransform transform { get; private set; }

		// Token: 0x0600062D RID: 1581 RVA: 0x000265A0 File Offset: 0x000249A0
		public override void RelocateNodes(Matrix4x4 deltaMatrix)
		{
			throw new Exception("This method cannot be used for Grid Graphs. Please use the other overload of RelocateNodes instead");
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000265AC File Offset: 0x000249AC
		public void RelocateNodes(Vector3 center, Quaternion rotation, float nodeSize, float aspectRatio = 1f, float isometricAngle = 0f)
		{
			GraphTransform previousTransform = this.transform;
			this.center = center;
			this.rotation = rotation.eulerAngles;
			this.aspectRatio = aspectRatio;
			this.isometricAngle = isometricAngle;
			this.SetDimensions(this.width, this.depth, nodeSize);
			this.GetNodes(delegate(GraphNode node)
			{
				GridNodeBase gridNodeBase = node as GridNodeBase;
				float y = previousTransform.InverseTransform((Vector3)node.position).y;
				node.position = this.GraphPointToWorld(gridNodeBase.XCoordinateInGrid, gridNodeBase.ZCoordinateInGrid, y);
			});
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002661B File Offset: 0x00024A1B
		public Int3 GraphPointToWorld(int x, int z, float height)
		{
			return (Int3)this.transform.Transform(new Vector3((float)x + 0.5f, height, (float)z + 0.5f));
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00026643 File Offset: 0x00024A43
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0002664B File Offset: 0x00024A4B
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00026654 File Offset: 0x00024A54
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0002665C File Offset: 0x00024A5C
		public int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00026665 File Offset: 0x00024A65
		public uint GetConnectionCost(int dir)
		{
			return this.neighbourCosts[dir];
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00026670 File Offset: 0x00024A70
		public GridNode GetNodeConnection(GridNode node, int dir)
		{
			if (!node.HasConnectionInDirection(dir))
			{
				return null;
			}
			if (!node.EdgeNode)
			{
				return this.nodes[node.NodeInGridIndex + this.neighbourOffsets[dir]];
			}
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			return this.GetNodeConnection(nodeInGridIndex, x, num, dir);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000266D4 File Offset: 0x00024AD4
		public bool HasNodeConnection(GridNode node, int dir)
		{
			if (!node.HasConnectionInDirection(dir))
			{
				return false;
			}
			if (!node.EdgeNode)
			{
				return true;
			}
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			return this.HasNodeConnection(nodeInGridIndex, x, num, dir);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00026724 File Offset: 0x00024B24
		public void SetNodeConnection(GridNode node, int dir, bool value)
		{
			int nodeInGridIndex = node.NodeInGridIndex;
			int num = nodeInGridIndex / this.Width;
			int x = nodeInGridIndex - num * this.Width;
			this.SetNodeConnection(nodeInGridIndex, x, num, dir, value);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00026758 File Offset: 0x00024B58
		private GridNode GetNodeConnection(int index, int x, int z, int dir)
		{
			if (!this.nodes[index].HasConnectionInDirection(dir))
			{
				return null;
			}
			int num = x + this.neighbourXOffsets[dir];
			if (num < 0 || num >= this.Width)
			{
				return null;
			}
			int num2 = z + this.neighbourZOffsets[dir];
			if (num2 < 0 || num2 >= this.Depth)
			{
				return null;
			}
			int num3 = index + this.neighbourOffsets[dir];
			return this.nodes[num3];
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000267D1 File Offset: 0x00024BD1
		public void SetNodeConnection(int index, int x, int z, int dir, bool value)
		{
			this.nodes[index].SetConnectionInternal(dir, value);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000267E4 File Offset: 0x00024BE4
		public bool HasNodeConnection(int index, int x, int z, int dir)
		{
			if (!this.nodes[index].HasConnectionInDirection(dir))
			{
				return false;
			}
			int num = x + this.neighbourXOffsets[dir];
			if (num < 0 || num >= this.Width)
			{
				return false;
			}
			int num2 = z + this.neighbourZOffsets[dir];
			return num2 >= 0 && num2 < this.Depth;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002684A File Offset: 0x00024C4A
		public void SetDimensions(int width, int depth, float nodeSize)
		{
			this.unclampedSize = new Vector2((float)width, (float)depth) * nodeSize;
			this.nodeSize = nodeSize;
			this.UpdateTransform();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0002686E File Offset: 0x00024C6E
		[Obsolete("Use SetDimensions instead")]
		public void UpdateSizeFromWidthDepth()
		{
			this.SetDimensions(this.width, this.depth, this.nodeSize);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00026888 File Offset: 0x00024C88
		[Obsolete("This method has been renamed to UpdateTransform")]
		public void GenerateMatrix()
		{
			this.UpdateTransform();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00026890 File Offset: 0x00024C90
		public void UpdateTransform()
		{
			this.CalculateDimensions(out this.width, out this.depth, out this.nodeSize);
			this.transform = this.CalculateTransform();
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000268B8 File Offset: 0x00024CB8
		public GraphTransform CalculateTransform()
		{
			int num;
			int num2;
			float num3;
			this.CalculateDimensions(out num, out num2, out num3);
			Matrix4x4 rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 45f, 0f), Vector3.one);
			rhs = Matrix4x4.Scale(new Vector3(Mathf.Cos(0.0174532924f * this.isometricAngle), 1f, 1f)) * rhs;
			rhs = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, -45f, 0f), Vector3.one) * rhs;
			Matrix4x4 matrix = Matrix4x4.TRS((Matrix4x4.TRS(this.center, Quaternion.Euler(this.rotation), new Vector3(this.aspectRatio, 1f, 1f)) * rhs).MultiplyPoint3x4(-new Vector3((float)num * num3, 0f, (float)num2 * num3) * 0.5f), Quaternion.Euler(this.rotation), new Vector3(num3 * this.aspectRatio, 1f, num3)) * rhs;
			return new GraphTransform(matrix);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000269D8 File Offset: 0x00024DD8
		private void CalculateDimensions(out int width, out int depth, out float nodeSize)
		{
			Vector2 size = this.unclampedSize;
			size.x *= Mathf.Sign(size.x);
			size.y *= Mathf.Sign(size.y);
			nodeSize = Mathf.Max(this.nodeSize, size.x / 1024f);
			nodeSize = Mathf.Max(this.nodeSize, size.y / 1024f);
			size.x = ((size.x >= nodeSize) ? size.x : nodeSize);
			size.y = ((size.y >= nodeSize) ? size.y : nodeSize);
			this.size = size;
			width = Mathf.FloorToInt(this.size.x / nodeSize);
			depth = Mathf.FloorToInt(this.size.y / nodeSize);
			if (Mathf.Approximately(this.size.x / nodeSize, (float)Mathf.CeilToInt(this.size.x / nodeSize)))
			{
				width = Mathf.CeilToInt(this.size.x / nodeSize);
			}
			if (Mathf.Approximately(this.size.y / nodeSize, (float)Mathf.CeilToInt(this.size.y / nodeSize)))
			{
				depth = Mathf.CeilToInt(this.size.y / nodeSize);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00026B6C File Offset: 0x00024F6C
		public override NNInfoInternal GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			position = this.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			NNInfoInternal result = new NNInfoInternal(this.nodes[num2 * this.width + num]);
			float y = this.transform.InverseTransform((Vector3)this.nodes[num2 * this.width + num].position).y;
			result.clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00026C74 File Offset: 0x00025074
		public override NNInfoInternal GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfoInternal);
			}
			Vector3 b = position;
			position = this.transform.InverseTransform(position);
			float x = position.x;
			float z = position.z;
			int num = Mathf.Clamp((int)x, 0, this.width - 1);
			int num2 = Mathf.Clamp((int)z, 0, this.depth - 1);
			GridNode gridNode = this.nodes[num + num2 * this.width];
			GridNode gridNode2 = null;
			float num3 = float.PositiveInfinity;
			int num4 = 2;
			Vector3 clampedPosition = Vector3.zero;
			NNInfoInternal result = new NNInfoInternal(null);
			if (constraint == null || constraint.Suitable(gridNode))
			{
				gridNode2 = gridNode;
				num3 = ((Vector3)gridNode2.position - b).sqrMagnitude;
				float y = this.transform.InverseTransform((Vector3)gridNode.position).y;
				clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)num, (float)num + 1f), y, Mathf.Clamp(z, (float)num2, (float)num2 + 1f)));
			}
			if (gridNode2 != null)
			{
				result.node = gridNode2;
				result.clampedPosition = clampedPosition;
				if (num4 == 0)
				{
					return result;
				}
				num4--;
			}
			float num5 = (constraint != null && !constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistance;
			float num6 = num5 * num5;
			int num7 = 1;
			while (this.nodeSize * (float)num7 <= num5)
			{
				bool flag = false;
				int i = num2 + num7;
				int num8 = i * this.width;
				int j;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude < num3 && sqrMagnitude < num6)
							{
								num3 = sqrMagnitude;
								gridNode2 = this.nodes[j + num8];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				i = num2 - num7;
				num8 = i * this.width;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude2 = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude2 < num3 && sqrMagnitude2 < num6)
							{
								num3 = sqrMagnitude2;
								gridNode2 = this.nodes[j + num8];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				j = num - num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude3 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude3 < num3 && sqrMagnitude3 < num6)
							{
								num3 = sqrMagnitude3;
								gridNode2 = this.nodes[j + i * this.width];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				j = num + num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint == null || constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude4 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude4 < num3 && sqrMagnitude4 < num6)
							{
								num3 = sqrMagnitude4;
								gridNode2 = this.nodes[j + i * this.width];
								clampedPosition = this.transform.Transform(new Vector3(Mathf.Clamp(x, (float)j, (float)j + 1f), this.transform.InverseTransform((Vector3)gridNode2.position).y, Mathf.Clamp(z, (float)i, (float)i + 1f)));
							}
						}
					}
				}
				if (gridNode2 != null)
				{
					if (num4 == 0)
					{
						break;
					}
					num4--;
				}
				if (flag)
				{
					num7++;
					continue;
				}
				IL_676:
				result.node = gridNode2;
				result.clampedPosition = clampedPosition;
				return result;
			}
			goto IL_676;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002730C File Offset: 0x0002570C
		public virtual void SetUpOffsetsAndCosts()
		{
			this.neighbourOffsets[0] = -this.width;
			this.neighbourOffsets[1] = 1;
			this.neighbourOffsets[2] = this.width;
			this.neighbourOffsets[3] = -1;
			this.neighbourOffsets[4] = -this.width + 1;
			this.neighbourOffsets[5] = this.width + 1;
			this.neighbourOffsets[6] = this.width - 1;
			this.neighbourOffsets[7] = -this.width - 1;
			uint num = (uint)Mathf.RoundToInt(this.nodeSize * 1000f);
			uint num2 = (uint)((!this.uniformEdgeCosts) ? Mathf.RoundToInt(this.nodeSize * Mathf.Sqrt(2f) * 1000f) : ((int)num));
			this.neighbourCosts[0] = num;
			this.neighbourCosts[1] = num;
			this.neighbourCosts[2] = num;
			this.neighbourCosts[3] = num;
			this.neighbourCosts[4] = num2;
			this.neighbourCosts[5] = num2;
			this.neighbourCosts[6] = num2;
			this.neighbourCosts[7] = num2;
			this.neighbourXOffsets[0] = 0;
			this.neighbourXOffsets[1] = 1;
			this.neighbourXOffsets[2] = 0;
			this.neighbourXOffsets[3] = -1;
			this.neighbourXOffsets[4] = 1;
			this.neighbourXOffsets[5] = 1;
			this.neighbourXOffsets[6] = -1;
			this.neighbourXOffsets[7] = -1;
			this.neighbourZOffsets[0] = -1;
			this.neighbourZOffsets[1] = 0;
			this.neighbourZOffsets[2] = 1;
			this.neighbourZOffsets[3] = 0;
			this.neighbourZOffsets[4] = -1;
			this.neighbourZOffsets[5] = 1;
			this.neighbourZOffsets[6] = 1;
			this.neighbourZOffsets[7] = -1;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000274A4 File Offset: 0x000258A4
		protected override IEnumerable<Progress> ScanInternal()
		{
			if (this.nodeSize <= 0f)
			{
				yield break;
			}
			this.UpdateTransform();
			if (this.width > 1024 || this.depth > 1024)
			{
				Debug.LogError("One of the grid's sides is longer than 1024 nodes");
				yield break;
			}
			if (this.useJumpPointSearch)
			{
				Debug.LogError("Trying to use Jump Point Search, but support for it is not enabled. Please enable it in the inspector (Grid Graph settings).");
			}
			this.SetUpOffsetsAndCosts();
			GridNode.SetGridGraph((int)this.graphIndex, this);
			yield return new Progress(0.05f, "Creating nodes");
			this.nodes = new GridNode[this.width * this.depth];
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					int num = i * this.width + j;
					GridNode gridNode = this.nodes[num] = new GridNode(this.active);
					gridNode.GraphIndex = this.graphIndex;
					gridNode.NodeInGridIndex = num;
				}
			}
			if (this.collision == null)
			{
				this.collision = new GraphCollision();
			}
			this.collision.Initialize(this.transform, this.nodeSize);
			this.textureData.Initialize();
			int progressCounter = 0;
			for (int z = 0; z < this.depth; z++)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.1f, 0.7f, (float)z / (float)this.depth), "Calculating positions");
				}
				progressCounter += this.width;
				for (int k = 0; k < this.width; k++)
				{
					this.RecalculateCell(k, z, true, true);
					this.textureData.Apply(this.nodes[z * this.width + k], k, z);
				}
			}
			progressCounter = 0;
			for (int z2 = 0; z2 < this.depth; z2++)
			{
				if (progressCounter >= 1000)
				{
					progressCounter = 0;
					yield return new Progress(Mathf.Lerp(0.7f, 0.9f, (float)z2 / (float)this.depth), "Calculating connections");
				}
				progressCounter += this.width;
				for (int l = 0; l < this.width; l++)
				{
					this.CalculateConnections(l, z2);
				}
			}
			yield return new Progress(0.95f, "Calculating erosion");
			this.ErodeWalkableArea();
			yield break;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000274C7 File Offset: 0x000258C7
		[Obsolete("Use RecalculateCell instead which works both for grid graphs and layered grid graphs")]
		public virtual void UpdateNodePositionCollision(GridNode node, int x, int z, bool resetPenalty = true)
		{
			this.RecalculateCell(x, z, resetPenalty, false);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000274D4 File Offset: 0x000258D4
		public virtual void RecalculateCell(int x, int z, bool resetPenalties = true, bool resetTags = true)
		{
			GridNode gridNode = this.nodes[z * this.width + x];
			gridNode.position = this.GraphPointToWorld(x, z, 0f);
			RaycastHit raycastHit;
			bool flag;
			Vector3 ob = this.collision.CheckHeight((Vector3)gridNode.position, out raycastHit, out flag);
			gridNode.position = (Int3)ob;
			if (resetPenalties)
			{
				gridNode.Penalty = this.initialPenalty;
				if (this.penaltyPosition)
				{
					gridNode.Penalty += (uint)Mathf.RoundToInt(((float)gridNode.position.y - this.penaltyPositionOffset) * this.penaltyPositionFactor);
				}
			}
			if (resetTags)
			{
				gridNode.Tag = 0u;
			}
			if (flag && this.useRaycastNormal && this.collision.heightCheck && raycastHit.normal != Vector3.zero)
			{
				float num = Vector3.Dot(raycastHit.normal.normalized, this.collision.up);
				if (this.penaltyAngle && resetPenalties)
				{
					gridNode.Penalty += (uint)Mathf.RoundToInt((1f - Mathf.Pow(num, this.penaltyAnglePower)) * this.penaltyAngleFactor);
				}
				float num2 = Mathf.Cos(this.maxSlope * 0.0174532924f);
				if (num < num2)
				{
					flag = false;
				}
			}
			gridNode.Walkable = (flag && this.collision.Check((Vector3)gridNode.position));
			gridNode.WalkableErosion = gridNode.Walkable;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002766C File Offset: 0x00025A6C
		protected virtual bool ErosionAnyFalseConnections(GraphNode baseNode)
		{
			GridNode node = baseNode as GridNode;
			if (this.neighbours == NumNeighbours.Six)
			{
				for (int i = 0; i < 6; i++)
				{
					if (!this.HasNodeConnection(node, GridGraph.hexagonNeighbourIndices[i]))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < 4; j++)
				{
					if (!this.HasNodeConnection(node, j))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000276DA File Offset: 0x00025ADA
		private void ErodeNode(GraphNode node)
		{
			if (node.Walkable && this.ErosionAnyFalseConnections(node))
			{
				node.Walkable = false;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000276FA File Offset: 0x00025AFA
		private void ErodeNodeWithTagsInit(GraphNode node)
		{
			if (node.Walkable && this.ErosionAnyFalseConnections(node))
			{
				node.Tag = (uint)this.erosionFirstTag;
			}
			else
			{
				node.Tag = 0u;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002772C File Offset: 0x00025B2C
		private void ErodeNodeWithTags(GraphNode node, int iteration)
		{
			GridNodeBase gridNodeBase = node as GridNodeBase;
			if (gridNodeBase.Walkable && (ulong)gridNodeBase.Tag >= (ulong)((long)this.erosionFirstTag) && (ulong)gridNodeBase.Tag < (ulong)((long)(this.erosionFirstTag + iteration)))
			{
				if (this.neighbours == NumNeighbours.Six)
				{
					for (int i = 0; i < 6; i++)
					{
						GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(GridGraph.hexagonNeighbourIndices[i]);
						if (neighbourAlongDirection != null)
						{
							uint tag = neighbourAlongDirection.Tag;
							if ((ulong)tag > (ulong)((long)(this.erosionFirstTag + iteration)) || (ulong)tag < (ulong)((long)this.erosionFirstTag))
							{
								neighbourAlongDirection.Tag = (uint)(this.erosionFirstTag + iteration);
							}
						}
					}
				}
				else
				{
					for (int j = 0; j < 4; j++)
					{
						GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(j);
						if (neighbourAlongDirection2 != null)
						{
							uint tag2 = neighbourAlongDirection2.Tag;
							if ((ulong)tag2 > (ulong)((long)(this.erosionFirstTag + iteration)) || (ulong)tag2 < (ulong)((long)this.erosionFirstTag))
							{
								neighbourAlongDirection2.Tag = (uint)(this.erosionFirstTag + iteration);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002783C File Offset: 0x00025C3C
		public virtual void ErodeWalkableArea()
		{
			this.ErodeWalkableArea(0, 0, this.Width, this.Depth);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00027854 File Offset: 0x00025C54
		public void ErodeWalkableArea(int xmin, int zmin, int xmax, int zmax)
		{
			if (this.erosionUseTags)
			{
				if (this.erodeIterations + this.erosionFirstTag > 31)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Too few tags available for ",
						this.erodeIterations,
						" erode iterations and starting with tag ",
						this.erosionFirstTag,
						" (erodeIterations+erosionFirstTag > 31)"
					}), this.active);
					return;
				}
				if (this.erosionFirstTag <= 0)
				{
					Debug.LogError("First erosion tag must be greater or equal to 1", this.active);
					return;
				}
			}
			if (this.erodeIterations == 0)
			{
				return;
			}
			IntRect rect = new IntRect(xmin, zmin, xmax - 1, zmax - 1);
			List<GraphNode> nodesInRegion = this.GetNodesInRegion(rect);
			int count = nodesInRegion.Count;
			for (int i = 0; i < this.erodeIterations; i++)
			{
				if (this.erosionUseTags)
				{
					if (i == 0)
					{
						for (int j = 0; j < count; j++)
						{
							this.ErodeNodeWithTagsInit(nodesInRegion[j]);
						}
					}
					else
					{
						for (int k = 0; k < count; k++)
						{
							this.ErodeNodeWithTags(nodesInRegion[k], i);
						}
					}
				}
				else
				{
					for (int l = 0; l < count; l++)
					{
						this.ErodeNode(nodesInRegion[l]);
					}
					for (int m = 0; m < count; m++)
					{
						this.CalculateConnections(nodesInRegion[m] as GridNodeBase);
					}
				}
			}
			ListPool<GraphNode>.Release(ref nodesInRegion);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000279E4 File Offset: 0x00025DE4
		public virtual bool IsValidConnection(GridNodeBase node1, GridNodeBase node2)
		{
			if (!node1.Walkable || !node2.Walkable)
			{
				return false;
			}
			if (this.maxClimb <= 0f || this.collision.use2D)
			{
				return true;
			}
			if (this.transform.onlyTranslational)
			{
				return (float)Math.Abs(node1.position.y - node2.position.y) <= this.maxClimb * 1000f;
			}
			Vector3 vector = (Vector3)node1.position;
			Vector3 rhs = (Vector3)node2.position;
			Vector3 lhs = this.transform.WorldUpAtGraphPosition(vector);
			return Math.Abs(Vector3.Dot(lhs, vector) - Vector3.Dot(lhs, rhs)) <= this.maxClimb;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00027AB0 File Offset: 0x00025EB0
		public void CalculateConnectionsForCellAndNeighbours(int x, int z)
		{
			this.CalculateConnections(x, z);
			for (int i = 0; i < 8; i++)
			{
				int x2 = x + this.neighbourXOffsets[i];
				int z2 = z + this.neighbourZOffsets[i];
				this.CalculateConnections(x2, z2);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00027AF5 File Offset: 0x00025EF5
		[Obsolete("Use the instance function instead")]
		public static void CalculateConnections(GridNode node)
		{
			(AstarData.GetGraph(node) as GridGraph).CalculateConnections(node);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00027B08 File Offset: 0x00025F08
		public virtual void CalculateConnections(GridNodeBase node)
		{
			int nodeInGridIndex = node.NodeInGridIndex;
			int x = nodeInGridIndex % this.width;
			int z = nodeInGridIndex / this.width;
			this.CalculateConnections(x, z);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00027B36 File Offset: 0x00025F36
		[Obsolete("CalculateConnections no longer takes a node array, it just uses the one on the graph")]
		public virtual void CalculateConnections(GridNode[] nodes, int x, int z, GridNode node)
		{
			this.CalculateConnections(x, z);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00027B40 File Offset: 0x00025F40
		[Obsolete("Use CalculateConnections(x,z) or CalculateConnections(node) instead")]
		public virtual void CalculateConnections(int x, int z, GridNode node)
		{
			this.CalculateConnections(x, z);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00027B4C File Offset: 0x00025F4C
		public virtual void CalculateConnections(int x, int z)
		{
			GridNode gridNode = this.nodes[z * this.width + x];
			if (!gridNode.Walkable)
			{
				gridNode.ResetConnectionsInternal();
				return;
			}
			int nodeInGridIndex = gridNode.NodeInGridIndex;
			if (this.neighbours == NumNeighbours.Four || this.neighbours == NumNeighbours.Eight)
			{
				int num = 0;
				for (int i = 0; i < 4; i++)
				{
					int num2 = x + this.neighbourXOffsets[i];
					int num3 = z + this.neighbourZOffsets[i];
					if (num2 >= 0 & num3 >= 0 & num2 < this.width & num3 < this.depth)
					{
						GridNode node = this.nodes[nodeInGridIndex + this.neighbourOffsets[i]];
						if (this.IsValidConnection(gridNode, node))
						{
							num |= 1 << i;
						}
					}
				}
				int num4 = 0;
				if (this.neighbours == NumNeighbours.Eight)
				{
					if (this.cutCorners)
					{
						for (int j = 0; j < 4; j++)
						{
							if (((num >> j | num >> j + 1 | num >> j + 1 - 4) & 1) != 0)
							{
								int num5 = j + 4;
								int num6 = x + this.neighbourXOffsets[num5];
								int num7 = z + this.neighbourZOffsets[num5];
								if (num6 >= 0 & num7 >= 0 & num6 < this.width & num7 < this.depth)
								{
									GridNode node2 = this.nodes[nodeInGridIndex + this.neighbourOffsets[num5]];
									if (this.IsValidConnection(gridNode, node2))
									{
										num4 |= 1 << num5;
									}
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < 4; k++)
						{
							if ((num >> k & 1) != 0 && ((num >> k + 1 | num >> k + 1 - 4) & 1) != 0)
							{
								GridNode node3 = this.nodes[nodeInGridIndex + this.neighbourOffsets[k + 4]];
								if (this.IsValidConnection(gridNode, node3))
								{
									num4 |= 1 << k + 4;
								}
							}
						}
					}
				}
				gridNode.SetAllConnectionInternal(num | num4);
			}
			else
			{
				gridNode.ResetConnectionsInternal();
				for (int l = 0; l < GridGraph.hexagonNeighbourIndices.Length; l++)
				{
					int num8 = GridGraph.hexagonNeighbourIndices[l];
					int num9 = x + this.neighbourXOffsets[num8];
					int num10 = z + this.neighbourZOffsets[num8];
					if (num9 >= 0 & num10 >= 0 & num9 < this.width & num10 < this.depth)
					{
						GridNode node4 = this.nodes[nodeInGridIndex + this.neighbourOffsets[num8]];
						gridNode.SetConnectionInternal(num8, this.IsValidConnection(gridNode, node4));
					}
				}
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00027E08 File Offset: 0x00026208
		public override void OnDrawGizmos(RetainedGizmos gizmos, bool drawNodes)
		{
			using (GraphGizmoHelper singleFrameGizmoHelper = gizmos.GetSingleFrameGizmoHelper(this.active))
			{
				int num;
				int num2;
				float num3;
				this.CalculateDimensions(out num, out num2, out num3);
				Bounds bounds = default(Bounds);
				bounds.SetMinMax(Vector3.zero, new Vector3((float)num, 0f, (float)num2));
				GraphTransform graphTransform = this.CalculateTransform();
				singleFrameGizmoHelper.builder.DrawWireCube(graphTransform, bounds, Color.white);
				int num4 = (this.nodes == null) ? -1 : this.nodes.Length;
				if (this is LayerGridGraph)
				{
					num4 = (((this as LayerGridGraph).nodes == null) ? -1 : (this as LayerGridGraph).nodes.Length);
				}
				if (drawNodes && this.width * this.depth * this.LayerCount != num4)
				{
					Color color = new Color(1f, 1f, 1f, 0.2f);
					for (int i = 0; i < num2; i++)
					{
						singleFrameGizmoHelper.builder.DrawLine(graphTransform.Transform(new Vector3(0f, 0f, (float)i)), graphTransform.Transform(new Vector3((float)num, 0f, (float)i)), color);
					}
					for (int j = 0; j < num; j++)
					{
						singleFrameGizmoHelper.builder.DrawLine(graphTransform.Transform(new Vector3((float)j, 0f, 0f)), graphTransform.Transform(new Vector3((float)j, 0f, (float)num2)), color);
					}
				}
			}
			if (!drawNodes)
			{
				return;
			}
			GridNodeBase[] array = ArrayPool<GridNodeBase>.Claim(1024 * this.LayerCount);
			for (int k = this.width / 32; k >= 0; k--)
			{
				for (int l = this.depth / 32; l >= 0; l--)
				{
					int nodesInRegion = this.GetNodesInRegion(new IntRect(k * 32, l * 32, (k + 1) * 32 - 1, (l + 1) * 32 - 1), array);
					RetainedGizmos.Hasher hasher = new RetainedGizmos.Hasher(this.active);
					hasher.AddHash((!this.showMeshOutline) ? 0 : 1);
					hasher.AddHash((!this.showMeshSurface) ? 0 : 1);
					hasher.AddHash((!this.showNodeConnections) ? 0 : 1);
					for (int m = 0; m < nodesInRegion; m++)
					{
						hasher.HashNode(array[m]);
					}
					if (!gizmos.Draw(hasher))
					{
						using (GraphGizmoHelper gizmoHelper = gizmos.GetGizmoHelper(this.active, hasher))
						{
							if (this.showNodeConnections)
							{
								for (int n = 0; n < nodesInRegion; n++)
								{
									if (array[n].Walkable)
									{
										gizmoHelper.DrawConnections(array[n]);
									}
								}
							}
							if (this.showMeshSurface || this.showMeshOutline)
							{
								this.CreateNavmeshSurfaceVisualization(array, nodesInRegion, gizmoHelper);
							}
						}
					}
				}
			}
			ArrayPool<GridNodeBase>.Release(ref array, false);
			if (this.active.showUnwalkableNodes)
			{
				base.DrawUnwalkableNodes(this.nodeSize * 0.3f);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00028194 File Offset: 0x00026594
		private void CreateNavmeshSurfaceVisualization(GridNodeBase[] nodes, int nodeCount, GraphGizmoHelper helper)
		{
			int num = 0;
			for (int i = 0; i < nodeCount; i++)
			{
				if (nodes[i].Walkable)
				{
					num++;
				}
			}
			int[] array;
			if (this.neighbours == NumNeighbours.Six)
			{
				array = GridGraph.hexagonNeighbourIndices;
			}
			else
			{
				RuntimeHelpers.InitializeArray(array = new int[4], fieldof(<PrivateImplementationDetails>.$field-02E4414E7DFA0F3AA2387EE8EA7AB31431CB406A).FieldHandle);
			}
			int[] array2 = array;
			float num2 = (this.neighbours != NumNeighbours.Six) ? 0.5f : 0.333333f;
			int num3 = array2.Length - 2;
			int num4 = 3 * num3;
			Vector3[] array3 = ArrayPool<Vector3>.Claim(num * num4);
			Color[] array4 = ArrayPool<Color>.Claim(num * num4);
			int num5 = 0;
			for (int j = 0; j < nodeCount; j++)
			{
				GridNodeBase gridNodeBase = nodes[j];
				if (gridNodeBase.Walkable)
				{
					Color color = helper.NodeColor(gridNodeBase);
					if (color.a > 0.001f)
					{
						for (int k = 0; k < array2.Length; k++)
						{
							int num6 = array2[k];
							int num7 = array2[(k + 1) % array2.Length];
							GridNodeBase gridNodeBase2 = null;
							GridNodeBase neighbourAlongDirection = gridNodeBase.GetNeighbourAlongDirection(num6);
							if (neighbourAlongDirection != null && this.neighbours != NumNeighbours.Six)
							{
								gridNodeBase2 = neighbourAlongDirection.GetNeighbourAlongDirection(num7);
							}
							GridNodeBase neighbourAlongDirection2 = gridNodeBase.GetNeighbourAlongDirection(num7);
							if (neighbourAlongDirection2 != null && gridNodeBase2 == null && this.neighbours != NumNeighbours.Six)
							{
								gridNodeBase2 = neighbourAlongDirection2.GetNeighbourAlongDirection(num6);
							}
							Vector3 vector = new Vector3((float)gridNodeBase.XCoordinateInGrid + 0.5f, 0f, (float)gridNodeBase.ZCoordinateInGrid + 0.5f);
							vector.x += (float)(this.neighbourXOffsets[num6] + this.neighbourXOffsets[num7]) * num2;
							vector.z += (float)(this.neighbourZOffsets[num6] + this.neighbourZOffsets[num7]) * num2;
							vector.y += this.transform.InverseTransform((Vector3)gridNodeBase.position).y;
							if (neighbourAlongDirection != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)neighbourAlongDirection.position).y;
							}
							if (neighbourAlongDirection2 != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)neighbourAlongDirection2.position).y;
							}
							if (gridNodeBase2 != null)
							{
								vector.y += this.transform.InverseTransform((Vector3)gridNodeBase2.position).y;
							}
							vector.y /= 1f + ((neighbourAlongDirection == null) ? 0f : 1f) + ((neighbourAlongDirection2 == null) ? 0f : 1f) + ((gridNodeBase2 == null) ? 0f : 1f);
							vector = this.transform.Transform(vector);
							array3[num5 + k] = vector;
						}
						if (this.neighbours == NumNeighbours.Six)
						{
							array3[num5 + 6] = array3[num5];
							array3[num5 + 7] = array3[num5 + 2];
							array3[num5 + 8] = array3[num5 + 3];
							array3[num5 + 9] = array3[num5];
							array3[num5 + 10] = array3[num5 + 3];
							array3[num5 + 11] = array3[num5 + 5];
						}
						else
						{
							array3[num5 + 4] = array3[num5];
							array3[num5 + 5] = array3[num5 + 2];
						}
						for (int l = 0; l < num4; l++)
						{
							array4[num5 + l] = color;
						}
						for (int m = 0; m < array2.Length; m++)
						{
							GridNodeBase neighbourAlongDirection3 = gridNodeBase.GetNeighbourAlongDirection(array2[(m + 1) % array2.Length]);
							if (neighbourAlongDirection3 == null || (this.showMeshOutline && gridNodeBase.NodeInGridIndex < neighbourAlongDirection3.NodeInGridIndex))
							{
								helper.builder.DrawLine(array3[num5 + m], array3[num5 + (m + 1) % array2.Length], (neighbourAlongDirection3 != null) ? color : Color.black);
							}
						}
						num5 += num4;
					}
				}
			}
			if (this.showMeshSurface)
			{
				helper.DrawTriangles(array3, array4, num5 * num3 / num4);
			}
			ArrayPool<Vector3>.Release(ref array3, false);
			ArrayPool<Color>.Release(ref array4, false);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000286BC File Offset: 0x00026ABC
		protected IntRect GetRectFromBounds(Bounds bounds)
		{
			bounds = this.transform.InverseTransform(bounds);
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			int xmin = Mathf.RoundToInt(min.x - 0.5f);
			int xmax = Mathf.RoundToInt(max.x - 0.5f);
			int ymin = Mathf.RoundToInt(min.z - 0.5f);
			int ymax = Mathf.RoundToInt(max.z - 0.5f);
			IntRect a = new IntRect(xmin, ymin, xmax, ymax);
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			return IntRect.Intersection(a, b);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00028764 File Offset: 0x00026B64
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(Bounds bounds)
		{
			return this.GetNodesInRegion(bounds);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0002876D File Offset: 0x00026B6D
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(shape);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00028776 File Offset: 0x00026B76
		[Obsolete("This method has been renamed to GetNodesInRegion", true)]
		public List<GraphNode> GetNodesInArea(Bounds bounds, GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(bounds, shape);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00028780 File Offset: 0x00026B80
		public List<GraphNode> GetNodesInRegion(Bounds bounds)
		{
			return this.GetNodesInRegion(bounds, null);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002878A File Offset: 0x00026B8A
		public List<GraphNode> GetNodesInRegion(GraphUpdateShape shape)
		{
			return this.GetNodesInRegion(shape.GetBounds(), shape);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0002879C File Offset: 0x00026B9C
		protected virtual List<GraphNode> GetNodesInRegion(Bounds bounds, GraphUpdateShape shape)
		{
			IntRect rectFromBounds = this.GetRectFromBounds(bounds);
			if (this.nodes == null || !rectFromBounds.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return ListPool<GraphNode>.Claim();
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rectFromBounds.Width * rectFromBounds.Height);
			for (int i = rectFromBounds.xmin; i <= rectFromBounds.xmax; i++)
			{
				for (int j = rectFromBounds.ymin; j <= rectFromBounds.ymax; j++)
				{
					int num = j * this.width + i;
					GraphNode graphNode = this.nodes[num];
					if (bounds.Contains((Vector3)graphNode.position) && (shape == null || shape.Contains((Vector3)graphNode.position)))
					{
						list.Add(graphNode);
					}
				}
			}
			return list;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00028890 File Offset: 0x00026C90
		public virtual List<GraphNode> GetNodesInRegion(IntRect rect)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return ListPool<GraphNode>.Claim(0);
			}
			List<GraphNode> list = ListPool<GraphNode>.Claim(rect.Width * rect.Height);
			for (int i = rect.ymin; i <= rect.ymax; i++)
			{
				int num = i * this.Width;
				for (int j = rect.xmin; j <= rect.xmax; j++)
				{
					list.Add(this.nodes[num + j]);
				}
			}
			return list;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00028968 File Offset: 0x00026D68
		public virtual int GetNodesInRegion(IntRect rect, GridNodeBase[] buffer)
		{
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			rect = IntRect.Intersection(rect, b);
			if (this.nodes == null || !rect.IsValid() || this.nodes.Length != this.width * this.depth)
			{
				return 0;
			}
			if (buffer.Length < rect.Width * rect.Height)
			{
				throw new ArgumentException("Buffer is too small");
			}
			int num = 0;
			int i = rect.ymin;
			while (i <= rect.ymax)
			{
				Array.Copy(this.nodes, i * this.Width + rect.xmin, buffer, num, rect.Width);
				i++;
				num += rect.Width;
			}
			return num;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00028A3B File Offset: 0x00026E3B
		public virtual GridNodeBase GetNode(int x, int z)
		{
			if (x < 0 || z < 0 || x >= this.width || z >= this.depth)
			{
				return null;
			}
			return this.nodes[x + z * this.width];
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00028A76 File Offset: 0x00026E76
		GraphUpdateThreading IUpdatableGraph.CanUpdateAsync(GraphUpdateObject o)
		{
			return GraphUpdateThreading.UnityThread;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00028A79 File Offset: 0x00026E79
		void IUpdatableGraph.UpdateAreaInit(GraphUpdateObject o)
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00028A7B File Offset: 0x00026E7B
		void IUpdatableGraph.UpdateAreaPost(GraphUpdateObject o)
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00028A80 File Offset: 0x00026E80
		protected void CalculateAffectedRegions(GraphUpdateObject o, out IntRect originalRect, out IntRect affectRect, out IntRect physicsRect, out bool willChangeWalkability, out int erosion)
		{
			Bounds bounds = this.transform.InverseTransform(o.bounds);
			Vector3 a = bounds.min;
			Vector3 a2 = bounds.max;
			int xmin = Mathf.RoundToInt(a.x - 0.5f);
			int xmax = Mathf.RoundToInt(a2.x - 0.5f);
			int ymin = Mathf.RoundToInt(a.z - 0.5f);
			int ymax = Mathf.RoundToInt(a2.z - 0.5f);
			originalRect = new IntRect(xmin, ymin, xmax, ymax);
			affectRect = originalRect;
			physicsRect = originalRect;
			erosion = ((!o.updateErosion) ? 0 : this.erodeIterations);
			willChangeWalkability = (o.updatePhysics || o.modifyWalkability);
			if (o.updatePhysics && !o.modifyWalkability && this.collision.collisionCheck)
			{
				Vector3 a3 = new Vector3(this.collision.diameter, 0f, this.collision.diameter) * 0.5f;
				a -= a3 * 1.02f;
				a2 += a3 * 1.02f;
				physicsRect = new IntRect(Mathf.RoundToInt(a.x - 0.5f), Mathf.RoundToInt(a.z - 0.5f), Mathf.RoundToInt(a2.x - 0.5f), Mathf.RoundToInt(a2.z - 0.5f));
				affectRect = IntRect.Union(physicsRect, affectRect);
			}
			if (willChangeWalkability || erosion > 0)
			{
				affectRect = affectRect.Expand(erosion + 1);
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00028C58 File Offset: 0x00027058
		void IUpdatableGraph.UpdateArea(GraphUpdateObject o)
		{
			if (this.nodes == null || this.nodes.Length != this.width * this.depth)
			{
				Debug.LogWarning("The Grid Graph is not scanned, cannot update area");
				return;
			}
			IntRect a;
			IntRect a2;
			IntRect intRect;
			bool flag;
			int num;
			this.CalculateAffectedRegions(o, out a, out a2, out intRect, out flag, out num);
			IntRect b = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect2 = IntRect.Intersection(a2, b);
			for (int i = intRect2.xmin; i <= intRect2.xmax; i++)
			{
				for (int j = intRect2.ymin; j <= intRect2.ymax; j++)
				{
					o.WillUpdateNode(this.nodes[j * this.width + i]);
				}
			}
			if (o.updatePhysics && !o.modifyWalkability)
			{
				this.collision.Initialize(this.transform, this.nodeSize);
				intRect2 = IntRect.Intersection(intRect, b);
				for (int k = intRect2.xmin; k <= intRect2.xmax; k++)
				{
					for (int l = intRect2.ymin; l <= intRect2.ymax; l++)
					{
						this.RecalculateCell(k, l, o.resetPenaltyOnPhysics, false);
					}
				}
			}
			intRect2 = IntRect.Intersection(a, b);
			for (int m = intRect2.xmin; m <= intRect2.xmax; m++)
			{
				for (int n = intRect2.ymin; n <= intRect2.ymax; n++)
				{
					int num2 = n * this.width + m;
					GridNode gridNode = this.nodes[num2];
					if (flag)
					{
						gridNode.Walkable = gridNode.WalkableErosion;
						if (o.bounds.Contains((Vector3)gridNode.position))
						{
							o.Apply(gridNode);
						}
						gridNode.WalkableErosion = gridNode.Walkable;
					}
					else if (o.bounds.Contains((Vector3)gridNode.position))
					{
						o.Apply(gridNode);
					}
				}
			}
			if (flag && num == 0)
			{
				intRect2 = IntRect.Intersection(a2, b);
				for (int num3 = intRect2.xmin; num3 <= intRect2.xmax; num3++)
				{
					for (int num4 = intRect2.ymin; num4 <= intRect2.ymax; num4++)
					{
						this.CalculateConnections(num3, num4);
					}
				}
			}
			else if (flag && num > 0)
			{
				IntRect a3 = IntRect.Union(a, intRect).Expand(num);
				IntRect a4 = a3.Expand(num);
				a3 = IntRect.Intersection(a3, b);
				a4 = IntRect.Intersection(a4, b);
				for (int num5 = a4.xmin; num5 <= a4.xmax; num5++)
				{
					for (int num6 = a4.ymin; num6 <= a4.ymax; num6++)
					{
						int num7 = num6 * this.width + num5;
						GridNode gridNode2 = this.nodes[num7];
						bool walkable = gridNode2.Walkable;
						gridNode2.Walkable = gridNode2.WalkableErosion;
						if (!a3.Contains(num5, num6))
						{
							gridNode2.TmpWalkable = walkable;
						}
					}
				}
				for (int num8 = a4.xmin; num8 <= a4.xmax; num8++)
				{
					for (int num9 = a4.ymin; num9 <= a4.ymax; num9++)
					{
						this.CalculateConnections(num8, num9);
					}
				}
				this.ErodeWalkableArea(a4.xmin, a4.ymin, a4.xmax + 1, a4.ymax + 1);
				for (int num10 = a4.xmin; num10 <= a4.xmax; num10++)
				{
					for (int num11 = a4.ymin; num11 <= a4.ymax; num11++)
					{
						if (!a3.Contains(num10, num11))
						{
							int num12 = num11 * this.width + num10;
							GridNode gridNode3 = this.nodes[num12];
							gridNode3.Walkable = gridNode3.TmpWalkable;
						}
					}
				}
				for (int num13 = a4.xmin; num13 <= a4.xmax; num13++)
				{
					for (int num14 = a4.ymin; num14 <= a4.ymax; num14++)
					{
						this.CalculateConnections(num13, num14);
					}
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00029104 File Offset: 0x00027504
		public bool Linecast(Vector3 from, Vector3 to)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(from, to, null, out graphHitInfo);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0002911C File Offset: 0x0002751C
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(from, to, hint, out graphHitInfo);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00029134 File Offset: 0x00027534
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit)
		{
			return this.Linecast(from, to, hint, out hit, null);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00029142 File Offset: 0x00027542
		protected static float CrossMagnitude(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00029163 File Offset: 0x00027563
		protected static long CrossMagnitude(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.y - (long)b.x * (long)a.y;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00029188 File Offset: 0x00027588
		protected bool ClipLineSegmentToBounds(Vector3 a, Vector3 b, out Vector3 outA, out Vector3 outB)
		{
			if (a.x < 0f || a.z < 0f || a.x > (float)this.width || a.z > (float)this.depth || b.x < 0f || b.z < 0f || b.x > (float)this.width || b.z > (float)this.depth)
			{
				Vector3 vector = new Vector3(0f, 0f, 0f);
				Vector3 vector2 = new Vector3(0f, 0f, (float)this.depth);
				Vector3 vector3 = new Vector3((float)this.width, 0f, (float)this.depth);
				Vector3 vector4 = new Vector3((float)this.width, 0f, 0f);
				int num = 0;
				bool flag;
				Vector3 vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector, vector2, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector, vector2, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector2, vector3, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector2, vector3, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector3, vector4, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector3, vector4, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				vector5 = VectorMath.SegmentIntersectionPointXZ(a, b, vector4, vector, out flag);
				if (flag)
				{
					num++;
					if (!VectorMath.RightOrColinearXZ(vector4, vector, a))
					{
						a = vector5;
					}
					else
					{
						b = vector5;
					}
				}
				if (num == 0)
				{
					outA = Vector3.zero;
					outB = Vector3.zero;
					return false;
				}
			}
			outA = a;
			outB = b;
			return true;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00029388 File Offset: 0x00027788
		public bool Linecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			hit = default(GraphHitInfo);
			hit.origin = from;
			Vector3 vector = this.transform.InverseTransform(from);
			Vector3 vector2 = this.transform.InverseTransform(to);
			if (!this.ClipLineSegmentToBounds(vector, vector2, out vector, out vector2))
			{
				hit.point = to;
				return false;
			}
			GridNodeBase gridNodeBase = base.GetNearest(this.transform.Transform(vector), NNConstraint.None).node as GridNodeBase;
			GridNodeBase gridNodeBase2 = base.GetNearest(this.transform.Transform(vector2), NNConstraint.None).node as GridNodeBase;
			if (!gridNodeBase.Walkable)
			{
				hit.node = gridNodeBase;
				hit.point = this.transform.Transform(vector);
				hit.tangentOrigin = hit.point;
				return true;
			}
			Vector2 vector3 = new Vector2(vector.x - 0.5f, vector.z - 0.5f);
			Vector2 vector4 = new Vector2(vector2.x - 0.5f, vector2.z - 0.5f);
			if (gridNodeBase == null || gridNodeBase2 == null)
			{
				hit.node = null;
				hit.point = from;
				return true;
			}
			Vector2 a = vector4 - vector3;
			Vector2 b = new Vector2(Mathf.Sign(a.x), Mathf.Sign(a.y));
			float num = GridGraph.CrossMagnitude(a, b) * 0.5f;
			int num2 = ((a.y < 0f) ? 3 : 0) ^ ((a.x < 0f) ? 1 : 0);
			int num3 = num2 + 1 & 3;
			int num4 = num2 + 2 & 3;
			GridNodeBase gridNodeBase3 = gridNodeBase;
			while (gridNodeBase3.NodeInGridIndex != gridNodeBase2.NodeInGridIndex)
			{
				if (trace != null)
				{
					trace.Add(gridNodeBase3);
				}
				Vector2 a2 = new Vector2((float)gridNodeBase3.XCoordinateInGrid, (float)gridNodeBase3.ZCoordinateInGrid);
				float num5 = GridGraph.CrossMagnitude(a, a2 - vector3);
				float num6 = num5 + num;
				int num7 = (num6 >= 0f) ? num3 : num4;
				GridNodeBase neighbourAlongDirection = gridNodeBase3.GetNeighbourAlongDirection(num7);
				if (neighbourAlongDirection == null)
				{
					Vector2 a3 = new Vector2((float)this.neighbourXOffsets[num7], (float)this.neighbourZOffsets[num7]);
					Vector2 b2 = new Vector2((float)this.neighbourXOffsets[num7 - 1 + 4 & 3], (float)this.neighbourZOffsets[num7 - 1 + 4 & 3]);
					Vector2 b3 = new Vector2((float)this.neighbourXOffsets[num7 + 1 & 3], (float)this.neighbourZOffsets[num7 + 1 & 3]);
					Vector2 vector5 = a2 + (a3 + b2) * 0.5f;
					Vector2 vector6 = VectorMath.LineIntersectionPoint(vector5, vector5 + b3, vector3, vector4);
					Vector3 vector7 = this.transform.InverseTransform((Vector3)gridNodeBase3.position);
					Vector3 point = new Vector3(vector6.x + 0.5f, vector7.y, vector6.y + 0.5f);
					Vector3 point2 = new Vector3(vector5.x + 0.5f, vector7.y, vector5.y + 0.5f);
					hit.point = this.transform.Transform(point);
					hit.tangentOrigin = this.transform.Transform(point2);
					hit.tangent = this.transform.TransformVector(new Vector3(b3.x, 0f, b3.y));
					hit.node = gridNodeBase3;
					return true;
				}
				gridNodeBase3 = neighbourAlongDirection;
			}
			if (trace != null)
			{
				trace.Add(gridNodeBase3);
			}
			if (gridNodeBase3 == gridNodeBase2)
			{
				hit.point = to;
				hit.node = gridNodeBase3;
				return false;
			}
			hit.point = (Vector3)gridNodeBase3.position;
			hit.tangentOrigin = hit.point;
			return true;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00029774 File Offset: 0x00027B74
		public bool SnappedLinecast(Vector3 from, Vector3 to, GraphNode hint, out GraphHitInfo hit)
		{
			return this.Linecast((Vector3)base.GetNearest(from, NNConstraint.None).node.position, (Vector3)base.GetNearest(to, NNConstraint.None).node.position, hint, out hit);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000297C8 File Offset: 0x00027BC8
		public bool Linecast(GridNodeBase fromNode, GridNodeBase toNode)
		{
			Int2 a = new Int2(toNode.XCoordinateInGrid - fromNode.XCoordinateInGrid, toNode.ZCoordinateInGrid - fromNode.ZCoordinateInGrid);
			long num = GridGraph.CrossMagnitude(a, new Int2(Math.Sign(a.x), Math.Sign(a.y)));
			int num2 = 0;
			if (a.x <= 0 && a.y > 0)
			{
				num2 = 1;
			}
			else if (a.x < 0 && a.y <= 0)
			{
				num2 = 2;
			}
			else if (a.x >= 0 && a.y < 0)
			{
				num2 = 3;
			}
			int num3 = num2 + 1 & 3;
			int num4 = num2 + 2 & 3;
			int num5 = (a.x == 0 || a.y == 0) ? -1 : (4 + (num2 + 1 & 3));
			Int2 @int = new Int2(0, 0);
			while (fromNode != null && fromNode.NodeInGridIndex != toNode.NodeInGridIndex)
			{
				long num6 = GridGraph.CrossMagnitude(a, @int) * 2L;
				long num7 = num6 + num;
				int num8 = (num7 >= 0L) ? num3 : num4;
				if (num7 == 0L && num5 != -1)
				{
					num8 = num5;
				}
				fromNode = fromNode.GetNeighbourAlongDirection(num8);
				@int += new Int2(this.neighbourXOffsets[num8], this.neighbourZOffsets[num8]);
			}
			return fromNode != toNode;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00029940 File Offset: 0x00027D40
		public bool CheckConnection(GridNode node, int dir)
		{
			if (this.neighbours == NumNeighbours.Eight || this.neighbours == NumNeighbours.Six || dir < 4)
			{
				return this.HasNodeConnection(node, dir);
			}
			int num = dir - 4 - 1 & 3;
			int num2 = dir - 4 + 1 & 3;
			if (!this.HasNodeConnection(node, num) || !this.HasNodeConnection(node, num2))
			{
				return false;
			}
			GridNode gridNode = this.nodes[node.NodeInGridIndex + this.neighbourOffsets[num]];
			GridNode gridNode2 = this.nodes[node.NodeInGridIndex + this.neighbourOffsets[num2]];
			return gridNode.Walkable && gridNode2.Walkable && this.HasNodeConnection(gridNode2, num) && this.HasNodeConnection(gridNode, num2);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00029A04 File Offset: 0x00027E04
		protected override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			if (this.nodes == null)
			{
				ctx.writer.Write(-1);
				return;
			}
			ctx.writer.Write(this.nodes.Length);
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i].SerializeNode(ctx);
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00029A64 File Offset: 0x00027E64
		protected override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			int num = ctx.reader.ReadInt32();
			if (num == -1)
			{
				this.nodes = null;
				return;
			}
			this.nodes = new GridNode[num];
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i] = new GridNode(this.active);
				this.nodes[i].DeserializeNode(ctx);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00029AD4 File Offset: 0x00027ED4
		protected override void DeserializeSettingsCompatibility(GraphSerializationContext ctx)
		{
			base.DeserializeSettingsCompatibility(ctx);
			this.aspectRatio = ctx.reader.ReadSingle();
			this.rotation = ctx.DeserializeVector3();
			this.center = ctx.DeserializeVector3();
			this.unclampedSize = ctx.DeserializeVector3();
			this.nodeSize = ctx.reader.ReadSingle();
			this.collision.DeserializeSettingsCompatibility(ctx);
			this.maxClimb = ctx.reader.ReadSingle();
			ctx.reader.ReadInt32();
			this.maxSlope = ctx.reader.ReadSingle();
			this.erodeIterations = ctx.reader.ReadInt32();
			this.erosionUseTags = ctx.reader.ReadBoolean();
			this.erosionFirstTag = ctx.reader.ReadInt32();
			ctx.reader.ReadBoolean();
			this.neighbours = (NumNeighbours)ctx.reader.ReadInt32();
			this.cutCorners = ctx.reader.ReadBoolean();
			this.penaltyPosition = ctx.reader.ReadBoolean();
			this.penaltyPositionFactor = ctx.reader.ReadSingle();
			this.penaltyAngle = ctx.reader.ReadBoolean();
			this.penaltyAngleFactor = ctx.reader.ReadSingle();
			this.penaltyAnglePower = ctx.reader.ReadSingle();
			this.isometricAngle = ctx.reader.ReadSingle();
			this.uniformEdgeCosts = ctx.reader.ReadBoolean();
			this.useJumpPointSearch = ctx.reader.ReadBoolean();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00029C58 File Offset: 0x00028058
		protected override void PostDeserialization(GraphSerializationContext ctx)
		{
			this.UpdateTransform();
			this.SetUpOffsetsAndCosts();
			GridNode.SetGridGraph((int)this.graphIndex, this);
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			if (this.width * this.depth != this.nodes.Length)
			{
				Debug.LogError("Node data did not match with bounds data. Probably a change to the bounds/width/depth data was made after scanning the graph just prior to saving it. Nodes will be discarded");
				this.nodes = new GridNode[0];
				return;
			}
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = this.nodes[i * this.width + j];
					if (gridNode == null)
					{
						Debug.LogError("Deserialization Error : Couldn't cast the node to the appropriate type - GridGenerator");
						return;
					}
					gridNode.NodeInGridIndex = i * this.width + j;
				}
			}
		}

		// Token: 0x04000435 RID: 1077
		[JsonMember]
		public InspectorGridMode inspectorGridMode;

		// Token: 0x04000436 RID: 1078
		public int width;

		// Token: 0x04000437 RID: 1079
		public int depth;

		// Token: 0x04000438 RID: 1080
		[JsonMember]
		public float aspectRatio = 1f;

		// Token: 0x04000439 RID: 1081
		[JsonMember]
		public float isometricAngle;

		// Token: 0x0400043A RID: 1082
		[JsonMember]
		public bool uniformEdgeCosts;

		// Token: 0x0400043B RID: 1083
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x0400043C RID: 1084
		[JsonMember]
		public Vector3 center;

		// Token: 0x0400043D RID: 1085
		[JsonMember]
		public Vector2 unclampedSize;

		// Token: 0x0400043E RID: 1086
		[JsonMember]
		public float nodeSize = 1f;

		// Token: 0x0400043F RID: 1087
		[JsonMember]
		public GraphCollision collision;

		// Token: 0x04000440 RID: 1088
		[JsonMember]
		public float maxClimb = 0.4f;

		// Token: 0x04000441 RID: 1089
		[JsonMember]
		public float maxSlope = 90f;

		// Token: 0x04000442 RID: 1090
		[JsonMember]
		public int erodeIterations;

		// Token: 0x04000443 RID: 1091
		[JsonMember]
		public bool erosionUseTags;

		// Token: 0x04000444 RID: 1092
		[JsonMember]
		public int erosionFirstTag = 1;

		// Token: 0x04000445 RID: 1093
		[JsonMember]
		public NumNeighbours neighbours = NumNeighbours.Eight;

		// Token: 0x04000446 RID: 1094
		[JsonMember]
		public bool cutCorners = true;

		// Token: 0x04000447 RID: 1095
		[JsonMember]
		public float penaltyPositionOffset;

		// Token: 0x04000448 RID: 1096
		[JsonMember]
		public bool penaltyPosition;

		// Token: 0x04000449 RID: 1097
		[JsonMember]
		public float penaltyPositionFactor = 1f;

		// Token: 0x0400044A RID: 1098
		[JsonMember]
		public bool penaltyAngle;

		// Token: 0x0400044B RID: 1099
		[JsonMember]
		public float penaltyAngleFactor = 100f;

		// Token: 0x0400044C RID: 1100
		[JsonMember]
		public float penaltyAnglePower = 1f;

		// Token: 0x0400044D RID: 1101
		[JsonMember]
		public bool useJumpPointSearch;

		// Token: 0x0400044E RID: 1102
		[JsonMember]
		public bool showMeshOutline = true;

		// Token: 0x0400044F RID: 1103
		[JsonMember]
		public bool showNodeConnections;

		// Token: 0x04000450 RID: 1104
		[JsonMember]
		public bool showMeshSurface = true;

		// Token: 0x04000451 RID: 1105
		[JsonMember]
		public GridGraph.TextureData textureData = new GridGraph.TextureData();

		// Token: 0x04000453 RID: 1107
		[NonSerialized]
		public readonly int[] neighbourOffsets = new int[8];

		// Token: 0x04000454 RID: 1108
		[NonSerialized]
		public readonly uint[] neighbourCosts = new uint[8];

		// Token: 0x04000455 RID: 1109
		[NonSerialized]
		public readonly int[] neighbourXOffsets = new int[8];

		// Token: 0x04000456 RID: 1110
		[NonSerialized]
		public readonly int[] neighbourZOffsets = new int[8];

		// Token: 0x04000457 RID: 1111
		internal static readonly int[] hexagonNeighbourIndices = new int[]
		{
			0,
			1,
			5,
			2,
			3,
			7
		};

		// Token: 0x04000458 RID: 1112
		public const int getNearestForceOverlap = 2;

		// Token: 0x04000459 RID: 1113
		public GridNode[] nodes;

		// Token: 0x020000A2 RID: 162
		public class TextureData
		{
			// Token: 0x06000676 RID: 1654 RVA: 0x00029D7C File Offset: 0x0002817C
			public void Initialize()
			{
				if (this.enabled && this.source != null)
				{
					for (int i = 0; i < this.channels.Length; i++)
					{
						if (this.channels[i] != GridGraph.TextureData.ChannelUse.None)
						{
							try
							{
								this.data = this.source.GetPixels32();
							}
							catch (UnityException ex)
							{
								Debug.LogWarning(ex.ToString());
								this.data = null;
							}
							break;
						}
					}
				}
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x00029E10 File Offset: 0x00028210
			public void Apply(GridNode node, int x, int z)
			{
				if (this.enabled && this.data != null && x < this.source.width && z < this.source.height)
				{
					Color32 color = this.data[z * this.source.width + x];
					if (this.channels[0] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.r, this.channels[0], this.factors[0]);
					}
					if (this.channels[1] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.g, this.channels[1], this.factors[1]);
					}
					if (this.channels[2] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.b, this.channels[2], this.factors[2]);
					}
					node.WalkableErosion = node.Walkable;
				}
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x00029F08 File Offset: 0x00028308
			private void ApplyChannel(GridNode node, int x, int z, int value, GridGraph.TextureData.ChannelUse channelUse, float factor)
			{
				if (channelUse != GridGraph.TextureData.ChannelUse.Penalty)
				{
					if (channelUse != GridGraph.TextureData.ChannelUse.Position)
					{
						if (channelUse == GridGraph.TextureData.ChannelUse.WalkablePenalty)
						{
							if (value == 0)
							{
								node.Walkable = false;
							}
							else
							{
								node.Penalty += (uint)Mathf.RoundToInt((float)(value - 1) * factor);
							}
						}
					}
					else
					{
						node.position = GridNode.GetGridGraph(node.GraphIndex).GraphPointToWorld(x, z, (float)value);
					}
				}
				else
				{
					node.Penalty += (uint)Mathf.RoundToInt((float)value * factor);
				}
			}

			// Token: 0x0400045C RID: 1116
			public bool enabled;

			// Token: 0x0400045D RID: 1117
			public Texture2D source;

			// Token: 0x0400045E RID: 1118
			public float[] factors = new float[3];

			// Token: 0x0400045F RID: 1119
			public GridGraph.TextureData.ChannelUse[] channels = new GridGraph.TextureData.ChannelUse[3];

			// Token: 0x04000460 RID: 1120
			private Color32[] data;

			// Token: 0x020000A3 RID: 163
			public enum ChannelUse
			{
				// Token: 0x04000462 RID: 1122
				None,
				// Token: 0x04000463 RID: 1123
				Penalty,
				// Token: 0x04000464 RID: 1124
				Position,
				// Token: 0x04000465 RID: 1125
				WalkablePenalty
			}
		}
	}
}
