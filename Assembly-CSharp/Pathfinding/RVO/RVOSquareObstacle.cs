using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x02000111 RID: 273
	[AddComponentMenu("Pathfinding/Local Avoidance/Square Obstacle")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_r_v_o_1_1_r_v_o_square_obstacle.php")]
	public class RVOSquareObstacle : RVOObstacle
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0004C5B0 File Offset: 0x0004A9B0
		protected override bool StaticObstacle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0004C5B3 File Offset: 0x0004A9B3
		protected override bool ExecuteInEditor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0004C5B6 File Offset: 0x0004A9B6
		protected override bool LocalCoordinates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0004C5B9 File Offset: 0x0004A9B9
		protected override float Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0004C5C1 File Offset: 0x0004A9C1
		protected override bool AreGizmosDirty()
		{
			return false;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0004C5C4 File Offset: 0x0004A9C4
		protected override void CreateObstacles()
		{
			this.size.x = Mathf.Abs(this.size.x);
			this.size.y = Mathf.Abs(this.size.y);
			this.height = Mathf.Abs(this.height);
			Vector3[] array = new Vector3[]
			{
				new Vector3(1f, 0f, -1f),
				new Vector3(1f, 0f, 1f),
				new Vector3(-1f, 0f, 1f),
				new Vector3(-1f, 0f, -1f)
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Scale(new Vector3(this.size.x * 0.5f, 0f, this.size.y * 0.5f));
				array[i] += new Vector3(this.center.x, 0f, this.center.y);
			}
			base.AddObstacle(array, this.height);
		}

		// Token: 0x040006DF RID: 1759
		public float height = 1f;

		// Token: 0x040006E0 RID: 1760
		public Vector2 size = Vector3.one;

		// Token: 0x040006E1 RID: 1761
		public Vector2 center = Vector3.zero;
	}
}
