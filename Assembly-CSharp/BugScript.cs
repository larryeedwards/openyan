using System;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class BugScript : MonoBehaviour
{
	// Token: 0x06001794 RID: 6036 RVA: 0x000BAC86 File Offset: 0x000B9086
	private void Start()
	{
		this.MyRenderer.enabled = false;
	}

	// Token: 0x06001795 RID: 6037 RVA: 0x000BAC94 File Offset: 0x000B9094
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.MyAudio.clip = this.Praise[UnityEngine.Random.Range(0, this.Praise.Length)];
			this.MyAudio.Play();
			this.MyRenderer.enabled = true;
			this.Prompt.Yandere.Inventory.PantyShots += 5;
			base.enabled = false;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
		}
	}

	// Token: 0x04001760 RID: 5984
	public PromptScript Prompt;

	// Token: 0x04001761 RID: 5985
	public Renderer MyRenderer;

	// Token: 0x04001762 RID: 5986
	public AudioSource MyAudio;

	// Token: 0x04001763 RID: 5987
	public AudioClip[] Praise;
}
