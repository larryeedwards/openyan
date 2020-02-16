using System;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class YandereShowerScript : MonoBehaviour
{
	// Token: 0x060022F0 RID: 8944 RVA: 0x001B7408 File Offset: 0x001B5808
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (this.Yandere.Schoolwear > 0 || this.Yandere.Chased || this.Yandere.Chasers > 0 || this.Yandere.Bloodiness == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
			}
			else
			{
				AudioSource.PlayClipAtPoint(this.CurtainOpen, base.transform.position);
				this.CensorSteam.SetActive(true);
				this.MyAudio.Play();
				this.Yandere.EmptyHands();
				this.Yandere.YandereShower = this;
				this.Yandere.CanMove = false;
				this.Yandere.Bathing = true;
				this.UpdateCurtain = true;
				this.Open = true;
				this.Timer = 6f;
			}
		}
		if (this.UpdateCurtain)
		{
			this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			if (this.Timer < 1f)
			{
				if (this.Open)
				{
					AudioSource.PlayClipAtPoint(this.CurtainClose, base.transform.position);
				}
				this.Open = false;
				if (this.Timer == 0f)
				{
					this.CensorSteam.SetActive(false);
					this.UpdateCurtain = false;
				}
			}
			if (this.Open)
			{
				this.OpenValue = Mathf.Lerp(this.OpenValue, 0f, Time.deltaTime * 10f);
				this.Curtain.SetBlendShapeWeight(0, this.OpenValue);
			}
			else
			{
				this.OpenValue = Mathf.Lerp(this.OpenValue, 100f, Time.deltaTime * 10f);
				this.Curtain.SetBlendShapeWeight(0, this.OpenValue);
			}
		}
	}

	// Token: 0x04003BD7 RID: 15319
	public SkinnedMeshRenderer Curtain;

	// Token: 0x04003BD8 RID: 15320
	public GameObject CensorSteam;

	// Token: 0x04003BD9 RID: 15321
	public YandereScript Yandere;

	// Token: 0x04003BDA RID: 15322
	public PromptScript Prompt;

	// Token: 0x04003BDB RID: 15323
	public Transform BatheSpot;

	// Token: 0x04003BDC RID: 15324
	public float OpenValue;

	// Token: 0x04003BDD RID: 15325
	public float Timer;

	// Token: 0x04003BDE RID: 15326
	public bool UpdateCurtain;

	// Token: 0x04003BDF RID: 15327
	public bool Open;

	// Token: 0x04003BE0 RID: 15328
	public AudioSource MyAudio;

	// Token: 0x04003BE1 RID: 15329
	public AudioClip CurtainClose;

	// Token: 0x04003BE2 RID: 15330
	public AudioClip CurtainOpen;
}
