using System;
using UnityEngine;

// Token: 0x02000493 RID: 1171
public class PianoScript : MonoBehaviour
{
	// Token: 0x06001E70 RID: 7792 RVA: 0x0012AD10 File Offset: 0x00129110
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount < 1f && this.Prompt.Circle[0].fillAmount > 0f)
		{
			this.Prompt.Circle[0].fillAmount = 0f;
			this.Notes[this.ID].Play();
			this.ID++;
			if (this.ID == this.Notes.Length)
			{
				this.ID = 0;
			}
		}
	}

	// Token: 0x0400274F RID: 10063
	public PromptScript Prompt;

	// Token: 0x04002750 RID: 10064
	public AudioSource[] Notes;

	// Token: 0x04002751 RID: 10065
	public int ID;
}
