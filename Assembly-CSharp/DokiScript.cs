using System;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class DokiScript : MonoBehaviour
{
	// Token: 0x060018DD RID: 6365 RVA: 0x000E3FA8 File Offset: 0x000E23A8
	private void Update()
	{
		if (!this.Yandere.Egg)
		{
			if (this.OtherPrompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				base.enabled = false;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Yandere.PantyAttacher.newRenderer.enabled = false;
				this.Prompt.Circle[0].fillAmount = 1f;
				UnityEngine.Object.Instantiate<GameObject>(this.TransformEffect, this.Yandere.Hips.position, Quaternion.identity);
				this.Yandere.MyRenderer.sharedMesh = this.Yandere.Uniforms[4];
				this.Yandere.MyRenderer.materials[0].mainTexture = this.DokiTexture;
				this.Yandere.MyRenderer.materials[1].mainTexture = this.DokiTexture;
				this.ID++;
				if (this.ID > 4)
				{
					this.ID = 1;
				}
				this.Credits.SongLabel.text = this.DokiName[this.ID] + " from Doki Doki Literature Club";
				this.Credits.BandLabel.text = "by Team Salvato";
				this.Credits.Panel.enabled = true;
				this.Credits.Slide = true;
				this.Credits.Timer = 0f;
				if (this.ID == 1)
				{
					this.Yandere.MyRenderer.materials[0].SetTexture("_OverlayTex", this.DokiSocks[0]);
					this.Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.DokiSocks[0]);
				}
				else
				{
					this.Yandere.MyRenderer.materials[0].SetTexture("_OverlayTex", this.DokiSocks[1]);
					this.Yandere.MyRenderer.materials[1].SetTexture("_OverlayTex", this.DokiSocks[1]);
				}
				Debug.Log("Activating shadows on Yandere-chan.");
				this.Yandere.MyRenderer.materials[0].SetFloat("_BlendAmount", 1f);
				this.Yandere.MyRenderer.materials[1].SetFloat("_BlendAmount", 1f);
				this.Yandere.MyRenderer.materials[2].mainTexture = this.DokiHair[this.ID];
				this.Yandere.Hairstyle = 136 + this.ID;
				this.Yandere.UpdateHair();
			}
		}
		else
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x04001C98 RID: 7320
	public MusicCreditScript Credits;

	// Token: 0x04001C99 RID: 7321
	public YandereScript Yandere;

	// Token: 0x04001C9A RID: 7322
	public PromptScript OtherPrompt;

	// Token: 0x04001C9B RID: 7323
	public PromptScript Prompt;

	// Token: 0x04001C9C RID: 7324
	public GameObject TransformEffect;

	// Token: 0x04001C9D RID: 7325
	public Texture DokiTexture;

	// Token: 0x04001C9E RID: 7326
	public Texture[] DokiSocks;

	// Token: 0x04001C9F RID: 7327
	public Texture[] DokiHair;

	// Token: 0x04001CA0 RID: 7328
	public string[] DokiName;

	// Token: 0x04001CA1 RID: 7329
	public int ID;
}
