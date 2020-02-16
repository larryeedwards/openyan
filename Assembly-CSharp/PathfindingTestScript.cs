using System;
using UnityEngine;

// Token: 0x020005C0 RID: 1472
public class PathfindingTestScript : MonoBehaviour
{
	// Token: 0x06002360 RID: 9056 RVA: 0x001BFA00 File Offset: 0x001BDE00
	private void Update()
	{
		if (Input.GetKeyDown("left"))
		{
			this.bytes = AstarPath.active.astarData.SerializeGraphs();
		}
		if (Input.GetKeyDown("right"))
		{
			AstarPath.active.astarData.DeserializeGraphs(this.bytes);
			AstarPath.active.Scan(null);
		}
	}

	// Token: 0x04003D2B RID: 15659
	private byte[] bytes;
}
