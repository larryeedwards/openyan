using System;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class MetalDetectorScript : MonoBehaviour
{
	// Token: 0x06001DA6 RID: 7590 RVA: 0x001193E5 File Offset: 0x001177E5
	private void Start()
	{
		this.MyAudio = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x001193F4 File Offset: 0x001177F4
	private void Update()
	{
		if (this.Yandere.Armed)
		{
			if (this.Yandere.EquippedWeapon.WeaponID == 6)
			{
				this.Prompt.enabled = true;
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.MyAudio.Play();
					this.MyCollider.enabled = false;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					base.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Spraying)
		{
			this.SprayTimer += Time.deltaTime;
			if ((double)this.SprayTimer > 0.66666)
			{
				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.Drop();
				}
				this.Yandere.EmptyHands();
				this.PepperSprayEffect.Play();
				this.Spraying = false;
			}
		}
		this.MyAudio.volume -= Time.deltaTime * 0.01f;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00119568 File Offset: 0x00117968
	private void OnTriggerStay(Collider other)
	{
		bool flag = false;
		if (this.MissionMode.GameOverID == 0 && other.gameObject.layer == 13)
		{
			for (int i = 1; i < 4; i++)
			{
				WeaponScript weaponScript = this.Yandere.Weapon[i];
				flag |= (weaponScript != null && weaponScript.Metal);
				if (!flag)
				{
					if (this.Yandere.Container != null && this.Yandere.Container.Weapon != null)
					{
						weaponScript = this.Yandere.Container.Weapon;
						flag = weaponScript.Metal;
					}
					if (this.Yandere.PickUp != null)
					{
						if (this.Yandere.PickUp.TrashCan != null && this.Yandere.PickUp.TrashCan.Weapon)
						{
							weaponScript = this.Yandere.PickUp.TrashCan.Item.GetComponent<WeaponScript>();
							flag = weaponScript.Metal;
						}
						if (this.Yandere.PickUp.StuckBoxCutter != null)
						{
							weaponScript = this.Yandere.PickUp.StuckBoxCutter;
							flag = true;
						}
					}
				}
			}
			if (flag && !this.Yandere.Inventory.IDCard)
			{
				if (this.MissionMode.enabled)
				{
					this.MissionMode.GameOverID = 16;
					this.MissionMode.GameOver();
					this.MissionMode.Phase = 4;
					base.enabled = false;
				}
				else if (!this.Yandere.Sprayed)
				{
					this.MyAudio.clip = this.Alarm;
					this.MyAudio.loop = true;
					this.MyAudio.Play();
					this.MyAudio.volume = 0.1f;
					AudioSource.PlayClipAtPoint(this.PepperSpraySFX, base.transform.position);
					if (this.Yandere.Aiming)
					{
						this.Yandere.StopAiming();
					}
					this.PepperSprayEffect.transform.position = new Vector3(base.transform.position.x, this.Yandere.transform.position.y + 1.8f, this.Yandere.transform.position.z);
					this.Spraying = true;
					this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
					this.Yandere.FollowHips = true;
					this.Yandere.Punching = false;
					this.Yandere.CanMove = false;
					this.Yandere.Sprayed = true;
					this.Yandere.StudentManager.YandereDying = true;
					this.Yandere.StudentManager.StopMoving();
					this.Yandere.Blur.blurIterations = 1;
					this.Yandere.Jukebox.Volume = 0f;
					Time.timeScale = 1f;
				}
			}
		}
	}

	// Token: 0x04002539 RID: 9529
	public MissionModeScript MissionMode;

	// Token: 0x0400253A RID: 9530
	public YandereScript Yandere;

	// Token: 0x0400253B RID: 9531
	public PromptScript Prompt;

	// Token: 0x0400253C RID: 9532
	public ParticleSystem PepperSprayEffect;

	// Token: 0x0400253D RID: 9533
	public AudioSource MyAudio;

	// Token: 0x0400253E RID: 9534
	public AudioClip PepperSpraySFX;

	// Token: 0x0400253F RID: 9535
	public AudioClip Alarm;

	// Token: 0x04002540 RID: 9536
	public Collider MyCollider;

	// Token: 0x04002541 RID: 9537
	public float SprayTimer;

	// Token: 0x04002542 RID: 9538
	public bool Spraying;
}
