using System;
using UnityEngine;

// Token: 0x02000482 RID: 1154
public class PaintBucketScript : MonoBehaviour
{
	// Token: 0x06001E22 RID: 7714 RVA: 0x00123250 File Offset: 0x00121650
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Prompt.Yandere.Bloodiness == 0f)
			{
				this.Prompt.Yandere.Bloodiness += 100f;
				this.Prompt.Yandere.RedPaint = true;
			}
		}
	}

	// Token: 0x04002675 RID: 9845
	public PromptScript Prompt;
}
