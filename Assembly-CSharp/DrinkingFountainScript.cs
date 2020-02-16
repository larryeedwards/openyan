using System;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public class DrinkingFountainScript : MonoBehaviour
{
	// Token: 0x060018F5 RID: 6389 RVA: 0x000E6460 File Offset: 0x000E4860
	private void Update()
	{
		if (this.Prompt.Yandere.EquippedWeapon != null)
		{
			if (this.Prompt.Yandere.EquippedWeapon.Blood.enabled)
			{
				this.Prompt.HideButton[0] = false;
				this.Prompt.enabled = true;
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
			if (!this.Leak.activeInHierarchy)
			{
				if (this.Prompt.Yandere.EquippedWeapon.WeaponID == 24)
				{
					this.Prompt.HideButton[1] = false;
					this.Prompt.enabled = true;
				}
				else
				{
					this.Prompt.HideButton[1] = true;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Prompt.Yandere.CharacterAnimation.CrossFade("f02_cleaningWeapon_00");
				this.Prompt.Yandere.Target = this.DrinkPosition;
				this.Prompt.Yandere.CleaningWeapon = true;
				this.Prompt.Yandere.CanMove = false;
				this.WaterStream.Play();
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.HideButton[1] = true;
				this.Puddle.SetActive(true);
				this.Leak.SetActive(true);
				this.MyAudio.Play();
				this.PowerSwitch.CheckPuddle();
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
	}

	// Token: 0x04001CD5 RID: 7381
	public PowerSwitchScript PowerSwitch;

	// Token: 0x04001CD6 RID: 7382
	public ParticleSystem WaterStream;

	// Token: 0x04001CD7 RID: 7383
	public Transform DrinkPosition;

	// Token: 0x04001CD8 RID: 7384
	public GameObject Puddle;

	// Token: 0x04001CD9 RID: 7385
	public GameObject Leak;

	// Token: 0x04001CDA RID: 7386
	public PromptScript Prompt;

	// Token: 0x04001CDB RID: 7387
	public AudioSource MyAudio;

	// Token: 0x04001CDC RID: 7388
	public bool Occupied;
}
