using System;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class CurtainScript : MonoBehaviour
{
	// Token: 0x0600185C RID: 6236 RVA: 0x000D5500 File Offset: 0x000D3900
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.MyAudio.Play();
			this.Animate = true;
			this.Open = !this.Open;
		}
		if (this.Animate)
		{
			if (!this.Open)
			{
				this.Weight = Mathf.Lerp(this.Weight, 0f, Time.deltaTime * 10f);
				if (this.Weight < 0.01f)
				{
					this.Animate = false;
					this.Weight = 0f;
				}
			}
			else
			{
				this.Weight = Mathf.Lerp(this.Weight, 100f, Time.deltaTime * 10f);
				if (this.Weight > 99.99f)
				{
					this.Animate = false;
					this.Weight = 100f;
				}
			}
			this.Curtains[0].SetBlendShapeWeight(0, this.Weight);
			this.Curtains[1].SetBlendShapeWeight(0, this.Weight);
		}
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x000D562C File Offset: 0x000D3A2C
	private void OnTriggerEnter(Collider other)
	{
		if ((other.gameObject.layer == 13 || other.gameObject.layer == 9) && !this.Open)
		{
			this.MyAudio.Play();
			this.Animate = true;
			this.Open = true;
		}
	}

	// Token: 0x04001ACC RID: 6860
	public SkinnedMeshRenderer[] Curtains;

	// Token: 0x04001ACD RID: 6861
	public PromptScript Prompt;

	// Token: 0x04001ACE RID: 6862
	public AudioSource MyAudio;

	// Token: 0x04001ACF RID: 6863
	public bool Animate;

	// Token: 0x04001AD0 RID: 6864
	public bool Open;

	// Token: 0x04001AD1 RID: 6865
	public float Weight;
}
