using System;
using UnityEngine;

// Token: 0x02000547 RID: 1351
public class TeleportScript : MonoBehaviour
{
	// Token: 0x0600217B RID: 8571 RVA: 0x00194F68 File Offset: 0x00193368
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.transform.position = this.Destination.position;
			Physics.SyncTransforms();
		}
	}

	// Token: 0x04003604 RID: 13828
	public PromptScript Prompt;

	// Token: 0x04003605 RID: 13829
	public Transform Destination;
}
