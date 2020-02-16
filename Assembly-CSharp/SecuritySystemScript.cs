using System;
using UnityEngine;

// Token: 0x02000500 RID: 1280
public class SecuritySystemScript : MonoBehaviour
{
	// Token: 0x06001FD7 RID: 8151 RVA: 0x001465FD File Offset: 0x001449FD
	private void Start()
	{
		if (!SchoolGlobals.HighSecurity)
		{
			base.enabled = false;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x00146628 File Offset: 0x00144A28
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			for (int i = 0; i < this.Cameras.Length; i++)
			{
				this.Cameras[i].transform.parent.transform.parent.gameObject.GetComponent<AudioSource>().Stop();
				this.Cameras[i].gameObject.SetActive(false);
			}
			for (int i = 0; i < this.Detectors.Length; i++)
			{
				this.Detectors[i].MyCollider.enabled = false;
				this.Detectors[i].enabled = false;
			}
			base.GetComponent<AudioSource>().Play();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Evidence = false;
			base.enabled = false;
		}
	}

	// Token: 0x04002BC3 RID: 11203
	public PromptScript Prompt;

	// Token: 0x04002BC4 RID: 11204
	public bool Evidence;

	// Token: 0x04002BC5 RID: 11205
	public bool Masked;

	// Token: 0x04002BC6 RID: 11206
	public SecurityCameraScript[] Cameras;

	// Token: 0x04002BC7 RID: 11207
	public MetalDetectorScript[] Detectors;
}
