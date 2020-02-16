using System;
using System.Collections;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000081 RID: 129
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_procedural_grid_mover.php")]
	public class ProceduralGridMover : VersionedMonoBehaviour
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00020965 File Offset: 0x0001ED65
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0002096D File Offset: 0x0001ED6D
		public bool updatingGraph { get; private set; }

		// Token: 0x06000570 RID: 1392 RVA: 0x00020978 File Offset: 0x0001ED78
		private void Start()
		{
			if (AstarPath.active == null)
			{
				throw new Exception("There is no AstarPath object in the scene");
			}
			this.graph = (AstarPath.active.data.FindGraphWhichInheritsFrom(typeof(GridGraph)) as GridGraph);
			if (this.graph == null)
			{
				throw new Exception("The AstarPath object has no GridGraph or LayeredGridGraph");
			}
			this.UpdateGraph();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000209E0 File Offset: 0x0001EDE0
		private void Update()
		{
			if (this.graph == null)
			{
				return;
			}
			Vector3 a = this.PointToGraphSpace(this.graph.center);
			Vector3 b = this.PointToGraphSpace(this.target.position);
			if (VectorMath.SqrDistanceXZ(a, b) > this.updateDistance * this.updateDistance)
			{
				this.UpdateGraph();
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00020A3C File Offset: 0x0001EE3C
		private Vector3 PointToGraphSpace(Vector3 p)
		{
			return this.graph.transform.InverseTransform(p);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00020A50 File Offset: 0x0001EE50
		public void UpdateGraph()
		{
			if (this.updatingGraph)
			{
				return;
			}
			this.updatingGraph = true;
			IEnumerator ie = this.UpdateGraphCoroutine();
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext context, bool force)
			{
				if (this.floodFill)
				{
					context.QueueFloodFill();
				}
				if (force)
				{
					while (ie.MoveNext())
					{
					}
				}
				bool flag;
				try
				{
					flag = !ie.MoveNext();
				}
				catch (Exception exception)
				{
					Debug.LogException(exception, this);
					flag = true;
				}
				if (flag)
				{
					this.updatingGraph = false;
				}
				return flag;
			}));
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00020AA4 File Offset: 0x0001EEA4
		private IEnumerator UpdateGraphCoroutine()
		{
			Vector3 dir = this.PointToGraphSpace(this.target.position) - this.PointToGraphSpace(this.graph.center);
			dir.x = Mathf.Round(dir.x);
			dir.z = Mathf.Round(dir.z);
			dir.y = 0f;
			if (dir == Vector3.zero)
			{
				yield break;
			}
			Int2 offset = new Int2(-Mathf.RoundToInt(dir.x), -Mathf.RoundToInt(dir.z));
			this.graph.center += this.graph.transform.TransformVector(dir);
			this.graph.UpdateTransform();
			int width = this.graph.width;
			int depth = this.graph.depth;
			int layers = this.graph.LayerCount;
			LayerGridGraph layeredGraph = this.graph as LayerGridGraph;
			GridNodeBase[] nodes;
			if (layeredGraph != null)
			{
				nodes = layeredGraph.nodes;
			}
			else
			{
				nodes = this.graph.nodes;
			}
			if (this.buffer == null || this.buffer.Length != width * depth)
			{
				this.buffer = new GridNodeBase[width * depth];
			}
			if (Mathf.Abs(offset.x) <= width && Mathf.Abs(offset.y) <= depth)
			{
				IntRect recalculateRect = new IntRect(0, 0, offset.x, offset.y);
				if (recalculateRect.xmin > recalculateRect.xmax)
				{
					int xmax3 = recalculateRect.xmax;
					recalculateRect.xmax = width + recalculateRect.xmin;
					recalculateRect.xmin = width + xmax3;
				}
				if (recalculateRect.ymin > recalculateRect.ymax)
				{
					int ymax = recalculateRect.ymax;
					recalculateRect.ymax = depth + recalculateRect.ymin;
					recalculateRect.ymin = depth + ymax;
				}
				IntRect connectionRect = recalculateRect.Expand(1);
				connectionRect = IntRect.Intersection(connectionRect, new IntRect(0, 0, width, depth));
				for (int i = 0; i < layers; i++)
				{
					int layerOffset = i * width * depth;
					for (int j = 0; j < depth; j++)
					{
						int num = j * width;
						int num2 = (j + offset.y + depth) % depth * width;
						for (int k = 0; k < width; k++)
						{
							this.buffer[num2 + (k + offset.x + width) % width] = nodes[layerOffset + num + k];
						}
					}
					yield return null;
					for (int l = 0; l < depth; l++)
					{
						int num3 = l * width;
						for (int m = 0; m < width; m++)
						{
							int num4 = num3 + m;
							GridNodeBase gridNodeBase = this.buffer[num4];
							if (gridNodeBase != null)
							{
								gridNodeBase.NodeInGridIndex = num4;
							}
							nodes[layerOffset + num4] = gridNodeBase;
						}
						int num5;
						int num6;
						if (l >= recalculateRect.ymin && l < recalculateRect.ymax)
						{
							num5 = 0;
							num6 = depth;
						}
						else
						{
							num5 = recalculateRect.xmin;
							num6 = recalculateRect.xmax;
						}
						for (int n = num5; n < num6; n++)
						{
							GridNodeBase gridNodeBase2 = this.buffer[num3 + n];
							if (gridNodeBase2 != null)
							{
								gridNodeBase2.ClearConnections(false);
							}
						}
					}
					yield return null;
				}
				int yieldEvery = 1000;
				int approxNumNodesToUpdate = Mathf.Max(Mathf.Abs(offset.x), Mathf.Abs(offset.y)) * Mathf.Max(width, depth);
				yieldEvery = Mathf.Max(yieldEvery, approxNumNodesToUpdate / 10);
				int counter = 0;
				for (int z = 0; z < depth; z++)
				{
					int xmin;
					int xmax;
					if (z >= recalculateRect.ymin && z < recalculateRect.ymax)
					{
						xmin = 0;
						xmax = width;
					}
					else
					{
						xmin = recalculateRect.xmin;
						xmax = recalculateRect.xmax;
					}
					for (int num7 = xmin; num7 < xmax; num7++)
					{
						this.graph.RecalculateCell(num7, z, false, false);
					}
					counter += xmax - xmin;
					if (counter > yieldEvery)
					{
						counter = 0;
						yield return null;
					}
				}
				for (int z2 = 0; z2 < depth; z2++)
				{
					int xmin2;
					int xmax2;
					if (z2 >= connectionRect.ymin && z2 < connectionRect.ymax)
					{
						xmin2 = 0;
						xmax2 = width;
					}
					else
					{
						xmin2 = connectionRect.xmin;
						xmax2 = connectionRect.xmax;
					}
					for (int num8 = xmin2; num8 < xmax2; num8++)
					{
						this.graph.CalculateConnections(num8, z2);
					}
					counter += xmax2 - xmin2;
					if (counter > yieldEvery)
					{
						counter = 0;
						yield return null;
					}
				}
				yield return null;
				for (int num9 = 0; num9 < depth; num9++)
				{
					for (int num10 = 0; num10 < width; num10++)
					{
						if (num10 == 0 || num9 == 0 || num10 == width - 1 || num9 == depth - 1)
						{
							this.graph.CalculateConnections(num10, num9);
						}
					}
				}
				if (!this.floodFill)
				{
					this.graph.GetNodes(delegate(GraphNode node)
					{
						node.Area = 1u;
					});
				}
			}
			else
			{
				int yieldEvery2 = Mathf.Max(depth * width / 20, 1000);
				int counter2 = 0;
				for (int z3 = 0; z3 < depth; z3++)
				{
					for (int num11 = 0; num11 < width; num11++)
					{
						this.graph.RecalculateCell(num11, z3, true, true);
					}
					counter2 += width;
					if (counter2 > yieldEvery2)
					{
						counter2 = 0;
						yield return null;
					}
				}
				for (int z4 = 0; z4 < depth; z4++)
				{
					for (int num12 = 0; num12 < width; num12++)
					{
						this.graph.CalculateConnections(num12, z4);
					}
					counter2 += width;
					if (counter2 > yieldEvery2)
					{
						counter2 = 0;
						yield return null;
					}
				}
			}
			yield break;
		}

		// Token: 0x04000383 RID: 899
		public float updateDistance = 10f;

		// Token: 0x04000384 RID: 900
		public Transform target;

		// Token: 0x04000385 RID: 901
		public bool floodFill = true;

		// Token: 0x04000386 RID: 902
		private GridGraph graph;

		// Token: 0x04000387 RID: 903
		private GridNodeBase[] buffer;
	}
}
