using System;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class GraphUpdaterScript : MonoBehaviour
{
	// Token: 0x06001C58 RID: 7256 RVA: 0x000FD560 File Offset: 0x000FB960
	private void Update()
	{
		if (this.Frames > 0)
		{
			this.Graph.Scan(null);
			UnityEngine.Object.Destroy(this);
		}
		this.Frames++;
	}

	// Token: 0x04002084 RID: 8324
	public AstarPath Graph;

	// Token: 0x04002085 RID: 8325
	public int Frames;
}
