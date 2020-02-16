using System;
using UnityEngine;

// Token: 0x020003D1 RID: 977
public class FanCoverScript : MonoBehaviour
{
	// Token: 0x0600199A RID: 6554 RVA: 0x000EEFC4 File Offset: 0x000ED3C4
	private void Start()
	{
		if (this.StudentManager.Students[this.RivalID] == null)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
		else
		{
			this.Rival = this.StudentManager.Students[this.RivalID];
		}
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x000EF02C File Offset: 0x000ED42C
	private void Update()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 2f)
		{
			if (this.Yandere.Armed)
			{
				this.Prompt.HideButton[0] = (this.Yandere.EquippedWeapon.WeaponID != 6 || !this.Rival.Meeting);
			}
			else
			{
				this.Prompt.HideButton[0] = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.CharacterAnimation.CrossFade("f02_fanMurderA_00");
			this.Rival.CharacterAnimation.CrossFade("f02_fanMurderB_00");
			this.Rival.OsanaHair.GetComponent<Animation>().CrossFade("fanMurderHair");
			this.Yandere.EmptyHands();
			this.Rival.OsanaHair.transform.parent = this.Rival.transform;
			this.Rival.OsanaHair.transform.localEulerAngles = Vector3.zero;
			this.Rival.OsanaHair.transform.localPosition = Vector3.zero;
			this.Rival.OsanaHair.transform.localScale = new Vector3(1f, 1f, 1f);
			this.Rival.OsanaHairL.enabled = false;
			this.Rival.OsanaHairR.enabled = false;
			this.Rival.Distracted = true;
			this.Yandere.CanMove = false;
			this.Rival.Meeting = false;
			this.FanSFX.enabled = false;
			base.GetComponent<AudioSource>().Play();
			base.transform.localPosition = new Vector3(-1.733f, 0.465f, 0.952f);
			base.transform.localEulerAngles = new Vector3(-90f, 165f, 0f);
			Physics.SyncTransforms();
			Rigidbody component = base.GetComponent<Rigidbody>();
			component.isKinematic = false;
			component.useGravity = true;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			this.Phase++;
		}
		if (this.Phase > 0)
		{
			if (this.Phase == 1)
			{
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.MurderSpot.rotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(this.MurderSpot.position);
				if (this.Yandere.CharacterAnimation["f02_fanMurderA_00"].time > 3.5f && !this.Reacted)
				{
					AudioSource.PlayClipAtPoint(this.RivalReaction, this.Rival.transform.position + new Vector3(0f, 1f, 0f));
					this.Yandere.MurderousActionTimer = this.Yandere.CharacterAnimation["f02_fanMurderA_00"].length - 3.5f;
					this.Reacted = true;
				}
				if (this.Yandere.CharacterAnimation["f02_fanMurderA_00"].time > 5f)
				{
					this.Rival.LiquidProjector.material.mainTexture = this.Rival.BloodTexture;
					this.Rival.LiquidProjector.enabled = true;
					this.Rival.EyeShrink = 1f;
					this.Yandere.BloodTextures = this.YandereBloodTextures;
					this.Yandere.Bloodiness += 20f;
					this.BloodProjector.gameObject.SetActive(true);
					this.BloodProjector.material.mainTexture = this.BloodTexture[1];
					this.BloodEffects.transform.parent = this.Rival.Head;
					this.BloodEffects.transform.localPosition = new Vector3(0f, 0.1f, 0f);
					this.BloodEffects.Play();
					this.Phase++;
				}
			}
			else if (this.Phase < 10)
			{
				if (this.Phase < 6)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 1f)
					{
						this.Phase++;
						if (this.Phase - 1 < 5)
						{
							this.BloodProjector.material.mainTexture = this.BloodTexture[this.Phase - 1];
							this.Yandere.Bloodiness += 20f;
							this.Timer = 0f;
						}
					}
				}
				if (this.Rival.CharacterAnimation["f02_fanMurderB_00"].time >= this.Rival.CharacterAnimation["f02_fanMurderB_00"].length)
				{
					this.BloodProjector.material.mainTexture = this.BloodTexture[5];
					this.Yandere.Bloodiness += 20f;
					this.Rival.Ragdoll.Decapitated = true;
					this.Rival.OsanaHair.SetActive(false);
					this.Rival.DeathType = DeathType.Weapon;
					this.Rival.BecomeRagdoll();
					this.BloodEffects.Stop();
					this.Explosion.SetActive(true);
					this.Smoke.SetActive(true);
					this.Fan.enabled = false;
					this.Phase = 10;
				}
			}
			else if (this.Yandere.CharacterAnimation["f02_fanMurderA_00"].time >= this.Yandere.CharacterAnimation["f02_fanMurderA_00"].length)
			{
				this.OfferHelp.SetActive(false);
				this.Yandere.CanMove = true;
				base.enabled = false;
			}
		}
	}

	// Token: 0x04001E86 RID: 7814
	public StudentManagerScript StudentManager;

	// Token: 0x04001E87 RID: 7815
	public YandereScript Yandere;

	// Token: 0x04001E88 RID: 7816
	public PromptScript Prompt;

	// Token: 0x04001E89 RID: 7817
	public StudentScript Rival;

	// Token: 0x04001E8A RID: 7818
	public SM_rotateThis Fan;

	// Token: 0x04001E8B RID: 7819
	public ParticleSystem BloodEffects;

	// Token: 0x04001E8C RID: 7820
	public Projector BloodProjector;

	// Token: 0x04001E8D RID: 7821
	public Rigidbody MyRigidbody;

	// Token: 0x04001E8E RID: 7822
	public Transform MurderSpot;

	// Token: 0x04001E8F RID: 7823
	public GameObject Explosion;

	// Token: 0x04001E90 RID: 7824
	public GameObject OfferHelp;

	// Token: 0x04001E91 RID: 7825
	public GameObject Smoke;

	// Token: 0x04001E92 RID: 7826
	public AudioClip RivalReaction;

	// Token: 0x04001E93 RID: 7827
	public AudioSource FanSFX;

	// Token: 0x04001E94 RID: 7828
	public Texture[] YandereBloodTextures;

	// Token: 0x04001E95 RID: 7829
	public Texture[] BloodTexture;

	// Token: 0x04001E96 RID: 7830
	public bool Reacted;

	// Token: 0x04001E97 RID: 7831
	public float Timer;

	// Token: 0x04001E98 RID: 7832
	public int RivalID = 11;

	// Token: 0x04001E99 RID: 7833
	public int Phase;
}
