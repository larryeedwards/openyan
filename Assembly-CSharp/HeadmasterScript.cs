using System;
using UnityEngine;

// Token: 0x0200040F RID: 1039
public class HeadmasterScript : MonoBehaviour
{
	// Token: 0x06001C6B RID: 7275 RVA: 0x000FE405 File Offset: 0x000FC805
	private void Start()
	{
		this.MyAnimation["HeadmasterRaiseTazer"].speed = 2f;
		this.Tazer.SetActive(false);
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x000FE430 File Offset: 0x000FC830
	private void Update()
	{
		if (this.Yandere.transform.position.y > base.transform.position.y - 1f && this.Yandere.transform.position.y < base.transform.position.y + 1f && this.Yandere.transform.position.x < 6f && this.Yandere.transform.position.x > -6f)
		{
			this.Distance = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
			if (this.Shooting)
			{
				this.targetRotation = Quaternion.LookRotation(base.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.AimWeaponAtYandere();
				this.AimBodyAtYandere();
			}
			else if ((double)this.Distance < 1.2)
			{
				this.AimBodyAtYandere();
				if (this.Yandere.CanMove && !this.Yandere.Egg && !this.Shooting)
				{
					this.Shoot();
				}
			}
			else if ((double)this.Distance < 2.8)
			{
				this.PlayedSitSound = false;
				if (!this.StudentManager.Clock.StopTime)
				{
					this.PatienceTimer -= Time.deltaTime;
				}
				if (this.PatienceTimer < 0f && !this.Yandere.Egg)
				{
					this.LostPatience = true;
					this.PatienceTimer = 60f;
					this.Patience = 0;
					this.Shoot();
				}
				if (!this.LostPatience)
				{
					this.LostPatience = true;
					this.Patience--;
					if (this.Patience < 1 && !this.Yandere.Egg && !this.Shooting)
					{
						this.Shoot();
					}
				}
				this.AimBodyAtYandere();
				this.Threatened = true;
				this.AimWeaponAtYandere();
				this.ThreatTimer = Mathf.MoveTowards(this.ThreatTimer, 0f, Time.deltaTime);
				if (this.ThreatTimer == 0f)
				{
					this.ThreatID++;
					if (this.ThreatID < 5)
					{
						this.HeadmasterSubtitle.text = this.HeadmasterThreatText[this.ThreatID];
						this.MyAudio.clip = this.HeadmasterThreatClips[this.ThreatID];
						this.MyAudio.Play();
						this.ThreatTimer = this.HeadmasterThreatClips[this.ThreatID].length + 1f;
					}
				}
				this.CheckBehavior();
			}
			else if (this.Distance < 10f)
			{
				this.PlayedStandSound = false;
				this.LostPatience = false;
				this.targetRotation = Quaternion.LookRotation(new Vector3(0f, 8f, 0f) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition, new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -4.66666f), Time.deltaTime * 1f);
				this.LookAtPlayer = true;
				if (!this.Threatened)
				{
					this.MyAnimation.CrossFade("HeadmasterAttention", 1f);
					this.ScratchTimer = 0f;
					this.SpeechTimer = Mathf.MoveTowards(this.SpeechTimer, 0f, Time.deltaTime);
					if (this.SpeechTimer == 0f)
					{
						if (this.CardboardBox.parent == null && this.Yandere.Mask == null)
						{
							this.VoiceID++;
							if (this.VoiceID < 6)
							{
								this.HeadmasterSubtitle.text = this.HeadmasterSpeechText[this.VoiceID];
								this.MyAudio.clip = this.HeadmasterSpeechClips[this.VoiceID];
								this.MyAudio.Play();
								this.SpeechTimer = this.HeadmasterSpeechClips[this.VoiceID].length + 1f;
							}
						}
						else
						{
							this.BoxID++;
							if (this.BoxID < 6)
							{
								this.HeadmasterSubtitle.text = this.HeadmasterBoxText[this.BoxID];
								this.MyAudio.clip = this.HeadmasterBoxClips[this.BoxID];
								this.MyAudio.Play();
								this.SpeechTimer = this.HeadmasterBoxClips[this.BoxID].length + 1f;
							}
						}
					}
				}
				else if (!this.Relaxing)
				{
					this.HeadmasterSubtitle.text = this.HeadmasterRelaxText;
					this.MyAudio.clip = this.HeadmasterRelaxClip;
					this.MyAudio.Play();
					this.Relaxing = true;
				}
				else
				{
					if (!this.PlayedSitSound)
					{
						AudioSource.PlayClipAtPoint(this.SitDown, base.transform.position);
						this.PlayedSitSound = true;
					}
					this.MyAnimation.CrossFade("HeadmasterLowerTazer");
					this.Aiming = false;
					if ((double)this.MyAnimation["HeadmasterLowerTazer"].time > 1.33333)
					{
						this.Tazer.SetActive(false);
					}
					if (this.MyAnimation["HeadmasterLowerTazer"].time > this.MyAnimation["HeadmasterLowerTazer"].length)
					{
						this.Threatened = false;
						this.Relaxing = false;
					}
				}
				this.CheckBehavior();
			}
			else
			{
				if (this.LookAtPlayer)
				{
					this.MyAnimation.CrossFade("HeadmasterType");
					this.LookAtPlayer = false;
					this.Threatened = false;
					this.Relaxing = false;
					this.Aiming = false;
				}
				this.ScratchTimer += Time.deltaTime;
				if (this.ScratchTimer > 10f)
				{
					this.MyAnimation.CrossFade("HeadmasterScratch");
					if (this.MyAnimation["HeadmasterScratch"].time > this.MyAnimation["HeadmasterScratch"].length)
					{
						this.MyAnimation.CrossFade("HeadmasterType");
						this.ScratchTimer = 0f;
					}
				}
			}
			if (!this.MyAudio.isPlaying)
			{
				this.HeadmasterSubtitle.text = string.Empty;
				if (this.Shooting)
				{
					this.Taze();
				}
			}
			if (this.Yandere.Attacked && this.Yandere.Character.GetComponent<Animation>()["f02_swingB_00"].time >= this.Yandere.Character.GetComponent<Animation>()["f02_swingB_00"].length * 0.85f)
			{
				this.MyAudio.clip = this.Crumple;
				this.MyAudio.Play();
				base.enabled = false;
			}
		}
		else
		{
			this.HeadmasterSubtitle.text = string.Empty;
		}
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x000FEC28 File Offset: 0x000FD028
	private void LateUpdate()
	{
		this.LookAtTarget = Vector3.Lerp(this.LookAtTarget, (!this.LookAtPlayer) ? this.Default.position : this.Yandere.Head.position, Time.deltaTime * 10f);
		this.Head.LookAt(this.LookAtTarget);
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x000FEC90 File Offset: 0x000FD090
	private void AimBodyAtYandere()
	{
		this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 5f);
		this.Chair.localPosition = Vector3.Lerp(this.Chair.localPosition, new Vector3(this.Chair.localPosition.x, this.Chair.localPosition.y, -5.2f), Time.deltaTime * 1f);
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x000FED4C File Offset: 0x000FD14C
	private void AimWeaponAtYandere()
	{
		if (!this.Aiming)
		{
			this.MyAnimation.CrossFade("HeadmasterRaiseTazer");
			if (!this.PlayedStandSound)
			{
				AudioSource.PlayClipAtPoint(this.StandUp, base.transform.position);
				this.PlayedStandSound = true;
			}
			if ((double)this.MyAnimation["HeadmasterRaiseTazer"].time > 1.166666)
			{
				this.Tazer.SetActive(true);
				this.Aiming = true;
			}
		}
		else if (this.MyAnimation["HeadmasterRaiseTazer"].time > this.MyAnimation["HeadmasterRaiseTazer"].length)
		{
			this.MyAnimation.CrossFade("HeadmasterAimTazer");
		}
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x000FEE18 File Offset: 0x000FD218
	public void Shoot()
	{
		this.StudentManager.YandereDying = true;
		this.Yandere.StopAiming();
		this.Yandere.StopLaughing();
		this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
		if (this.Patience < 1)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterPatienceText;
			this.MyAudio.clip = this.HeadmasterPatienceClip;
		}
		else if (this.Yandere.Armed)
		{
			this.HeadmasterSubtitle.text = this.HeadmasterWeaponText;
			this.MyAudio.clip = this.HeadmasterWeaponClip;
		}
		else if (this.Yandere.Carrying || this.Yandere.Dragging || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart))
		{
			this.HeadmasterSubtitle.text = this.HeadmasterCorpseText;
			this.MyAudio.clip = this.HeadmasterCorpseClip;
		}
		else
		{
			this.HeadmasterSubtitle.text = this.HeadmasterAttackText;
			this.MyAudio.clip = this.HeadmasterAttackClip;
		}
		this.StudentManager.StopMoving();
		this.Yandere.EmptyHands();
		this.Yandere.CanMove = false;
		this.MyAudio.Play();
		this.Shooting = true;
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x000FEF94 File Offset: 0x000FD394
	private void CheckBehavior()
	{
		if (this.Yandere.CanMove && !this.Yandere.Egg)
		{
			if (this.Yandere.Chased || this.Yandere.Chasers > 0)
			{
				if (!this.Shooting)
				{
					this.Shoot();
				}
			}
			else if (this.Yandere.Armed)
			{
				if (!this.Shooting)
				{
					this.Shoot();
				}
			}
			else if ((this.Yandere.Carrying || this.Yandere.Dragging || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart)) && !this.Shooting)
			{
				this.Shoot();
			}
		}
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x000FF080 File Offset: 0x000FD480
	public void Taze()
	{
		if (this.Yandere.CanMove)
		{
			this.StudentManager.YandereDying = true;
			this.Yandere.StopAiming();
			this.Yandere.StopLaughing();
			this.StudentManager.StopMoving();
			this.Yandere.EmptyHands();
			this.Yandere.CanMove = false;
		}
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, this.TazerEffectTarget.position, Quaternion.identity);
		UnityEngine.Object.Instantiate<GameObject>(this.LightningEffect, this.Yandere.Spine[3].position, Quaternion.identity);
		this.MyAudio.clip = this.HeadmasterShockClip;
		this.MyAudio.Play();
		this.Yandere.CharacterAnimation.CrossFade("f02_swingB_00");
		this.Yandere.CharacterAnimation["f02_swingB_00"].time = 0.5f;
		this.Yandere.RPGCamera.enabled = false;
		this.Yandere.Attacked = true;
		this.Heartbroken.Headmaster = true;
		this.Jukebox.Volume = 0f;
		this.Shooting = false;
	}

	// Token: 0x040020B0 RID: 8368
	public StudentManagerScript StudentManager;

	// Token: 0x040020B1 RID: 8369
	public HeartbrokenScript Heartbroken;

	// Token: 0x040020B2 RID: 8370
	public YandereScript Yandere;

	// Token: 0x040020B3 RID: 8371
	public JukeboxScript Jukebox;

	// Token: 0x040020B4 RID: 8372
	public AudioClip[] HeadmasterSpeechClips;

	// Token: 0x040020B5 RID: 8373
	public AudioClip[] HeadmasterThreatClips;

	// Token: 0x040020B6 RID: 8374
	public AudioClip[] HeadmasterBoxClips;

	// Token: 0x040020B7 RID: 8375
	public AudioClip HeadmasterRelaxClip;

	// Token: 0x040020B8 RID: 8376
	public AudioClip HeadmasterAttackClip;

	// Token: 0x040020B9 RID: 8377
	public AudioClip HeadmasterCrypticClip;

	// Token: 0x040020BA RID: 8378
	public AudioClip HeadmasterShockClip;

	// Token: 0x040020BB RID: 8379
	public AudioClip HeadmasterPatienceClip;

	// Token: 0x040020BC RID: 8380
	public AudioClip HeadmasterCorpseClip;

	// Token: 0x040020BD RID: 8381
	public AudioClip HeadmasterWeaponClip;

	// Token: 0x040020BE RID: 8382
	public AudioClip Crumple;

	// Token: 0x040020BF RID: 8383
	public AudioClip StandUp;

	// Token: 0x040020C0 RID: 8384
	public AudioClip SitDown;

	// Token: 0x040020C1 RID: 8385
	public readonly string[] HeadmasterSpeechText = new string[]
	{
		string.Empty,
		"Ahh...! It's...it's you!",
		"No, that would be impossible...you must be...her daughter...",
		"I'll tolerate you in my school, but not in my office.",
		"Leave at once.",
		"There is nothing for you to achieve here. Just. Get. Out."
	};

	// Token: 0x040020C2 RID: 8386
	public readonly string[] HeadmasterThreatText = new string[]
	{
		string.Empty,
		"Not another step!",
		"You're up to no good! I know it!",
		"I'm not going to let you harm me!",
		"I'll use self-defense if I deem it necessary!",
		"This is your final warning. Get out of here...or else."
	};

	// Token: 0x040020C3 RID: 8387
	public readonly string[] HeadmasterBoxText = new string[]
	{
		string.Empty,
		"What...in...blazes are you doing?",
		"Are you trying to re-enact something you saw in a video game?",
		"Ugh, do you really think such a stupid ploy is going to work?",
		"I know who you are. It's obvious. You're not fooling anyone.",
		"I don't have time for this tomfoolery. Leave at once!"
	};

	// Token: 0x040020C4 RID: 8388
	public readonly string HeadmasterRelaxText = "Hmm...a wise decision.";

	// Token: 0x040020C5 RID: 8389
	public readonly string HeadmasterAttackText = "You asked for it!";

	// Token: 0x040020C6 RID: 8390
	public readonly string HeadmasterCrypticText = "Mr. Saikou...the deal is off.";

	// Token: 0x040020C7 RID: 8391
	public readonly string HeadmasterWeaponText = "How dare you raise a weapon in my office!";

	// Token: 0x040020C8 RID: 8392
	public readonly string HeadmasterPatienceText = "Enough of this nonsense!";

	// Token: 0x040020C9 RID: 8393
	public readonly string HeadmasterCorpseText = "You...you murderer!";

	// Token: 0x040020CA RID: 8394
	public UILabel HeadmasterSubtitle;

	// Token: 0x040020CB RID: 8395
	public Animation MyAnimation;

	// Token: 0x040020CC RID: 8396
	public AudioSource MyAudio;

	// Token: 0x040020CD RID: 8397
	public GameObject LightningEffect;

	// Token: 0x040020CE RID: 8398
	public GameObject Tazer;

	// Token: 0x040020CF RID: 8399
	public Transform TazerEffectTarget;

	// Token: 0x040020D0 RID: 8400
	public Transform CardboardBox;

	// Token: 0x040020D1 RID: 8401
	public Transform Chair;

	// Token: 0x040020D2 RID: 8402
	public Quaternion targetRotation;

	// Token: 0x040020D3 RID: 8403
	public float PatienceTimer;

	// Token: 0x040020D4 RID: 8404
	public float ScratchTimer;

	// Token: 0x040020D5 RID: 8405
	public float SpeechTimer;

	// Token: 0x040020D6 RID: 8406
	public float ThreatTimer;

	// Token: 0x040020D7 RID: 8407
	public float Distance;

	// Token: 0x040020D8 RID: 8408
	public int Patience = 10;

	// Token: 0x040020D9 RID: 8409
	public int ThreatID;

	// Token: 0x040020DA RID: 8410
	public int VoiceID;

	// Token: 0x040020DB RID: 8411
	public int BoxID;

	// Token: 0x040020DC RID: 8412
	public bool PlayedStandSound;

	// Token: 0x040020DD RID: 8413
	public bool PlayedSitSound;

	// Token: 0x040020DE RID: 8414
	public bool LostPatience;

	// Token: 0x040020DF RID: 8415
	public bool Threatened;

	// Token: 0x040020E0 RID: 8416
	public bool Relaxing;

	// Token: 0x040020E1 RID: 8417
	public bool Shooting;

	// Token: 0x040020E2 RID: 8418
	public bool Aiming;

	// Token: 0x040020E3 RID: 8419
	public Vector3 LookAtTarget;

	// Token: 0x040020E4 RID: 8420
	public bool LookAtPlayer;

	// Token: 0x040020E5 RID: 8421
	public Transform Default;

	// Token: 0x040020E6 RID: 8422
	public Transform Head;
}
