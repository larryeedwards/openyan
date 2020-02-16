using System;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class PromptManagerScript : MonoBehaviour
{
	// Token: 0x06001ED5 RID: 7893 RVA: 0x001369D0 File Offset: 0x00134DD0
	private void Update()
	{
		if (this.Yandere.transform.position.z < -38f)
		{
			if (!this.Outside)
			{
				this.Outside = true;
				foreach (PromptScript promptScript in this.Prompts)
				{
					if (promptScript != null)
					{
						promptScript.enabled = false;
					}
				}
			}
		}
		else if (this.Outside)
		{
			this.Outside = false;
			foreach (PromptScript promptScript2 in this.Prompts)
			{
				if (promptScript2 != null)
				{
					promptScript2.enabled = true;
				}
			}
		}
	}

	// Token: 0x040028AD RID: 10413
	public PromptScript[] Prompts;

	// Token: 0x040028AE RID: 10414
	public int ID;

	// Token: 0x040028AF RID: 10415
	public Transform Yandere;

	// Token: 0x040028B0 RID: 10416
	public bool Outside;
}
