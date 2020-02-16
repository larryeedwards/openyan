using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000009 RID: 9
	public class RichPath
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00005420 File Offset: 0x00003820
		public RichPath()
		{
			this.Clear();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005439 File Offset: 0x00003839
		public void Clear()
		{
			this.parts.Clear();
			this.currentPart = 0;
			this.Endpoint = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005468 File Offset: 0x00003868
		public void Initialize(Seeker seeker, Path path, bool mergePartEndpoints, bool simplificationMode)
		{
			if (path.error)
			{
				throw new ArgumentException("Path has an error");
			}
			List<GraphNode> path2 = path.path;
			if (path2.Count == 0)
			{
				throw new ArgumentException("Path traverses no nodes");
			}
			this.seeker = seeker;
			for (int i = 0; i < this.parts.Count; i++)
			{
				RichFunnel richFunnel = this.parts[i] as RichFunnel;
				RichSpecial richSpecial = this.parts[i] as RichSpecial;
				if (richFunnel != null)
				{
					ObjectPool<RichFunnel>.Release(ref richFunnel);
				}
				else if (richSpecial != null)
				{
					ObjectPool<RichSpecial>.Release(ref richSpecial);
				}
			}
			this.Clear();
			this.Endpoint = path.vectorPath[path.vectorPath.Count - 1];
			for (int j = 0; j < path2.Count; j++)
			{
				if (path2[j] is TriangleMeshNode)
				{
					NavmeshBase navmeshBase = AstarData.GetGraph(path2[j]) as NavmeshBase;
					if (navmeshBase == null)
					{
						throw new Exception("Found a TriangleMeshNode that was not in a NavmeshBase graph");
					}
					RichFunnel richFunnel2 = ObjectPool<RichFunnel>.Claim().Initialize(this, navmeshBase);
					richFunnel2.funnelSimplification = simplificationMode;
					int num = j;
					uint graphIndex = path2[num].GraphIndex;
					while (j < path2.Count)
					{
						if (path2[j].GraphIndex != graphIndex && !(path2[j] is NodeLink3Node))
						{
							break;
						}
						j++;
					}
					j--;
					if (num == 0)
					{
						richFunnel2.exactStart = path.vectorPath[0];
					}
					else
					{
						richFunnel2.exactStart = (Vector3)path2[(!mergePartEndpoints) ? num : (num - 1)].position;
					}
					if (j == path2.Count - 1)
					{
						richFunnel2.exactEnd = path.vectorPath[path.vectorPath.Count - 1];
					}
					else
					{
						richFunnel2.exactEnd = (Vector3)path2[(!mergePartEndpoints) ? j : (j + 1)].position;
					}
					richFunnel2.BuildFunnelCorridor(path2, num, j);
					this.parts.Add(richFunnel2);
				}
				else if (NodeLink2.GetNodeLink(path2[j]) != null)
				{
					NodeLink2 nodeLink = NodeLink2.GetNodeLink(path2[j]);
					int num2 = j;
					uint graphIndex2 = path2[num2].GraphIndex;
					for (j++; j < path2.Count; j++)
					{
						if (path2[j].GraphIndex != graphIndex2)
						{
							break;
						}
					}
					j--;
					if (j - num2 > 1)
					{
						throw new Exception("NodeLink2 path length greater than two (2) nodes. " + (j - num2));
					}
					if (j - num2 != 0)
					{
						RichSpecial item = ObjectPool<RichSpecial>.Claim().Initialize(nodeLink, path2[num2]);
						this.parts.Add(item);
					}
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005787 File Offset: 0x00003B87
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000578F File Offset: 0x00003B8F
		public Vector3 Endpoint { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005798 File Offset: 0x00003B98
		public bool CompletedAllParts
		{
			get
			{
				return this.currentPart >= this.parts.Count;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000057B0 File Offset: 0x00003BB0
		public bool IsLastPart
		{
			get
			{
				return this.currentPart >= this.parts.Count - 1;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000057CA File Offset: 0x00003BCA
		public void NextPart()
		{
			this.currentPart = Mathf.Min(this.currentPart + 1, this.parts.Count);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000057EC File Offset: 0x00003BEC
		public RichPathPart GetCurrentPart()
		{
			if (this.parts.Count == 0)
			{
				return null;
			}
			return (this.currentPart >= this.parts.Count) ? this.parts[this.parts.Count - 1] : this.parts[this.currentPart];
		}

		// Token: 0x0400006A RID: 106
		private int currentPart;

		// Token: 0x0400006B RID: 107
		private readonly List<RichPathPart> parts = new List<RichPathPart>();

		// Token: 0x0400006C RID: 108
		public Seeker seeker;

		// Token: 0x0400006D RID: 109
		public ITransform transform;
	}
}
