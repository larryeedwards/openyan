using System;
using System.Linq;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009B RID: 155
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
	public class TargetMover : MonoBehaviour
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x0002556F File Offset: 0x0002396F
		public void Start()
		{
			this.cam = Camera.main;
			this.ais = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray<IAstarAI>();
			base.useGUILayout = false;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00025598 File Offset: 0x00023998
		public void OnGUI()
		{
			if (this.onlyOnDoubleClick && this.cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000255E6 File Offset: 0x000239E6
		private void Update()
		{
			if (!this.onlyOnDoubleClick && this.cam != null)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002560C File Offset: 0x00023A0C
		public void UpdateTargetPosition()
		{
			Vector3 vector = Vector3.zero;
			bool flag = false;
			RaycastHit raycastHit;
			if (this.use2D)
			{
				vector = this.cam.ScreenToWorldPoint(Input.mousePosition);
				vector.z = 0f;
				flag = true;
			}
			else if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity, this.mask))
			{
				vector = raycastHit.point;
				flag = true;
			}
			if (flag && vector != this.target.position)
			{
				this.target.position = vector;
				if (this.onlyOnDoubleClick)
				{
					for (int i = 0; i < this.ais.Length; i++)
					{
						if (this.ais[i] != null)
						{
							this.ais[i].SearchPath();
						}
					}
				}
			}
		}

		// Token: 0x04000408 RID: 1032
		public LayerMask mask;

		// Token: 0x04000409 RID: 1033
		public Transform target;

		// Token: 0x0400040A RID: 1034
		private IAstarAI[] ais;

		// Token: 0x0400040B RID: 1035
		public bool onlyOnDoubleClick;

		// Token: 0x0400040C RID: 1036
		public bool use2D;

		// Token: 0x0400040D RID: 1037
		private Camera cam;
	}
}
