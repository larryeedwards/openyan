using System;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x02000098 RID: 152
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_object_placer.php")]
	public class ObjectPlacer : MonoBehaviour
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00024FF9 File Offset: 0x000233F9
		private void Update()
		{
			if (Input.GetKeyDown("p"))
			{
				this.PlaceObject();
			}
			if (Input.GetKeyDown("r"))
			{
				this.RemoveObject();
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00025028 File Offset: 0x00023428
		public void PlaceObject()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity))
			{
				Vector3 point = raycastHit.point;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.go, point, Quaternion.identity);
				if (this.issueGUOs)
				{
					Bounds bounds = gameObject.GetComponent<Collider>().bounds;
					GraphUpdateObject ob = new GraphUpdateObject(bounds);
					AstarPath.active.UpdateGraphs(ob);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000250B4 File Offset: 0x000234B4
		public void RemoveObject()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity))
			{
				if (raycastHit.collider.isTrigger || raycastHit.transform.gameObject.name == "Ground")
				{
					return;
				}
				Bounds bounds = raycastHit.collider.bounds;
				UnityEngine.Object.Destroy(raycastHit.collider);
				UnityEngine.Object.Destroy(raycastHit.collider.gameObject);
				if (this.issueGUOs)
				{
					GraphUpdateObject ob = new GraphUpdateObject(bounds);
					AstarPath.active.UpdateGraphs(ob);
					if (this.direct)
					{
						AstarPath.active.FlushGraphUpdates();
					}
				}
			}
		}

		// Token: 0x040003FF RID: 1023
		public GameObject go;

		// Token: 0x04000400 RID: 1024
		public bool direct;

		// Token: 0x04000401 RID: 1025
		public bool issueGUOs = true;
	}
}
