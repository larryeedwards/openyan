using System;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class DumpScript : MonoBehaviour
{
	// Token: 0x060018FD RID: 6397 RVA: 0x000E6BA4 File Offset: 0x000E4FA4
	private void Update()
	{
		this.Timer += Time.deltaTime;
		if (this.Timer > 5f)
		{
			this.Incinerator.Corpses++;
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001CF3 RID: 7411
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04001CF4 RID: 7412
	public IncineratorScript Incinerator;

	// Token: 0x04001CF5 RID: 7413
	public float Timer;
}
