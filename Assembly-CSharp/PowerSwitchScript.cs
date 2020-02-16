using System;
using UnityEngine;

// Token: 0x020004A8 RID: 1192
public class PowerSwitchScript : MonoBehaviour
{
	// Token: 0x06001EBB RID: 7867 RVA: 0x001343D4 File Offset: 0x001327D4
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.On = !this.On;
			if (this.On)
			{
				this.Prompt.Label[0].text = "     Turn Off";
				this.MyAudio.clip = this.Flick[1];
			}
			else
			{
				this.Prompt.Label[0].text = "     Turn On";
				this.MyAudio.clip = this.Flick[0];
			}
			if (this.BathroomLight != null)
			{
				this.BathroomLight.enabled = !this.BathroomLight.enabled;
			}
			this.CheckPuddle();
			this.MyAudio.Play();
		}
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x001344C4 File Offset: 0x001328C4
	public void CheckPuddle()
	{
		if (this.On)
		{
			if (this.DrinkingFountain.Puddle != null && this.DrinkingFountain.Puddle.gameObject.activeInHierarchy && this.PowerOutlet.SabotagedOutlet.activeInHierarchy)
			{
				this.Electricity.SetActive(true);
			}
		}
		else
		{
			this.Electricity.SetActive(false);
		}
	}

	// Token: 0x04002862 RID: 10338
	public DrinkingFountainScript DrinkingFountain;

	// Token: 0x04002863 RID: 10339
	public PowerOutletScript PowerOutlet;

	// Token: 0x04002864 RID: 10340
	public GameObject Electricity;

	// Token: 0x04002865 RID: 10341
	public Light BathroomLight;

	// Token: 0x04002866 RID: 10342
	public PromptScript Prompt;

	// Token: 0x04002867 RID: 10343
	public AudioSource MyAudio;

	// Token: 0x04002868 RID: 10344
	public AudioClip[] Flick;

	// Token: 0x04002869 RID: 10345
	public bool On;
}
