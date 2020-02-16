using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000092 RID: 146
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_door_controller.php")]
	public class DoorController : MonoBehaviour
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0002462F File Offset: 0x00022A2F
		public void Start()
		{
			this.bounds = base.GetComponent<Collider>().bounds;
			this.SetState(this.open);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002464E File Offset: 0x00022A4E
		private void OnGUI()
		{
			if (GUI.Button(new Rect(5f, this.yOffset, 100f, 22f), "Toggle Door"))
			{
				this.SetState(!this.open);
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00024688 File Offset: 0x00022A88
		public void SetState(bool open)
		{
			this.open = open;
			if (this.updateGraphsWithGUO)
			{
				GraphUpdateObject graphUpdateObject = new GraphUpdateObject(this.bounds);
				int num = (!open) ? this.closedtag : this.opentag;
				if (num > 31)
				{
					Debug.LogError("tag > 31");
					return;
				}
				graphUpdateObject.modifyTag = true;
				graphUpdateObject.setTag = num;
				graphUpdateObject.updatePhysics = false;
				AstarPath.active.UpdateGraphs(graphUpdateObject);
			}
			if (open)
			{
				base.GetComponent<Animation>().Play("Open");
			}
			else
			{
				base.GetComponent<Animation>().Play("Close");
			}
		}

		// Token: 0x040003E0 RID: 992
		private bool open;

		// Token: 0x040003E1 RID: 993
		public int opentag = 1;

		// Token: 0x040003E2 RID: 994
		public int closedtag = 1;

		// Token: 0x040003E3 RID: 995
		public bool updateGraphsWithGUO = true;

		// Token: 0x040003E4 RID: 996
		public float yOffset = 5f;

		// Token: 0x040003E5 RID: 997
		private Bounds bounds;
	}
}
