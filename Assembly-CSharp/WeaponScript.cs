using System;
using UnityEngine;

// Token: 0x0200058F RID: 1423
public class WeaponScript : MonoBehaviour
{
	// Token: 0x0600226B RID: 8811 RVA: 0x0019FE48 File Offset: 0x0019E248
	private void Start()
	{
		this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
		this.StartingPosition = base.transform.position;
		this.StartingRotation = base.transform.eulerAngles;
		Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
		this.OriginalColor = this.Outline[0].color;
		if (this.StartLow)
		{
			this.OriginalOffset = this.Prompt.OffsetY[3];
			this.Prompt.OffsetY[3] = 0.2f;
		}
		if (this.DisableCollider)
		{
			this.MyCollider.enabled = false;
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (component != null)
		{
			this.OriginalClip = component.clip;
		}
		this.MyRigidbody = base.GetComponent<Rigidbody>();
		this.MyRigidbody.isKinematic = true;
		Transform transform = GameObject.Find("WeaponOriginParent").transform;
		this.Origin = UnityEngine.Object.Instantiate<GameObject>(this.Prompt.Yandere.StudentManager.EmptyObject, base.transform.position, Quaternion.identity).transform;
		this.Origin.parent = transform;
	}

	// Token: 0x0600226C RID: 8812 RVA: 0x0019FF84 File Offset: 0x0019E384
	public string GetTypePrefix()
	{
		if (this.Type == WeaponType.Knife)
		{
			return "knife";
		}
		if (this.Type == WeaponType.Katana)
		{
			return "katana";
		}
		if (this.Type == WeaponType.Bat)
		{
			return "bat";
		}
		if (this.Type == WeaponType.Saw)
		{
			return "saw";
		}
		if (this.Type == WeaponType.Syringe)
		{
			return "syringe";
		}
		if (this.Type == WeaponType.Weight)
		{
			return "weight";
		}
		Debug.LogError("Weapon type \"" + this.Type.ToString() + "\" not implemented.");
		return string.Empty;
	}

	// Token: 0x0600226D RID: 8813 RVA: 0x001A0028 File Offset: 0x0019E428
	public AudioClip GetClip(float sanity, bool stealth)
	{
		AudioClip[] array;
		if (this.Clips2.Length == 0)
		{
			array = this.Clips;
		}
		else
		{
			int num = UnityEngine.Random.Range(2, 4);
			array = ((num != 2) ? this.Clips3 : this.Clips2);
		}
		if (stealth)
		{
			return array[0];
		}
		if (sanity > 0.6666667f)
		{
			return array[1];
		}
		if (sanity > 0.333333343f)
		{
			return array[2];
		}
		return array[3];
	}

	// Token: 0x0600226E RID: 8814 RVA: 0x001A009C File Offset: 0x0019E49C
	private void Update()
	{
		if (this.WeaponID == 16 && this.Yandere.EquippedWeapon == this && Input.GetButtonDown("RB"))
		{
			this.ExtraBlade.SetActive(!this.ExtraBlade.activeInHierarchy);
		}
		if (this.Dismembering)
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.DismemberPhase < 4)
			{
				if (component.time > 0.75f)
				{
					if (this.Speed < 36f)
					{
						this.Speed += Time.deltaTime + 10f;
					}
					this.Rotation += this.Speed;
					this.Blade.localEulerAngles = new Vector3(this.Rotation, this.Blade.localEulerAngles.y, this.Blade.localEulerAngles.z);
				}
				if (component.time > this.SoundTime[this.DismemberPhase])
				{
					this.Yandere.Sanity -= 5f * this.Yandere.Numbness;
					this.Yandere.Bloodiness += 25f;
					this.ShortBloodSpray[0].Play();
					this.ShortBloodSpray[1].Play();
					this.Blood.enabled = true;
					this.MurderWeapon = true;
					this.DismemberPhase++;
				}
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 2f);
				this.Blade.localEulerAngles = new Vector3(this.Rotation, this.Blade.localEulerAngles.y, this.Blade.localEulerAngles.z);
				if (!component.isPlaying)
				{
					component.clip = this.OriginalClip;
					this.Yandere.StainWeapon();
					this.Dismembering = false;
					this.DismemberPhase = 0;
					this.Rotation = 0f;
					this.Speed = 0f;
				}
			}
		}
		else if (this.Yandere.EquippedWeapon == this)
		{
			if (this.Yandere.AttackManager.IsAttacking())
			{
				if (this.Type == WeaponType.Knife)
				{
					base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, Mathf.Lerp(base.transform.localEulerAngles.y, (!this.Flip) ? 0f : 180f, Time.deltaTime * 10f), base.transform.localEulerAngles.z);
				}
				else if (this.Type == WeaponType.Saw && this.Spin)
				{
					this.Blade.transform.localEulerAngles = new Vector3(this.Blade.transform.localEulerAngles.x + Time.deltaTime * 360f, this.Blade.transform.localEulerAngles.y, this.Blade.transform.localEulerAngles.z);
				}
			}
		}
		else if (!this.MyRigidbody.isKinematic)
		{
			this.KinematicTimer = Mathf.MoveTowards(this.KinematicTimer, 5f, Time.deltaTime);
			if (this.KinematicTimer == 5f)
			{
				this.MyRigidbody.isKinematic = true;
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -71f && base.transform.position.x < -61f && base.transform.position.z > -37.5f && base.transform.position.z < -27.5f)
			{
				base.transform.position = new Vector3(-63f, 1f, -26.5f);
				this.KinematicTimer = 0f;
			}
			if (base.transform.position.x > -46f && base.transform.position.x < -18f && base.transform.position.z > 66f && base.transform.position.z < 78f)
			{
				base.transform.position = new Vector3(-16f, 5f, 72f);
				this.KinematicTimer = 0f;
			}
		}
		if (this.Rotate)
		{
			base.transform.Rotate(Vector3.forward * Time.deltaTime * 100f);
		}
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x001A05D4 File Offset: 0x0019E9D4
	private void LateUpdate()
	{
		if (this.Prompt.Circle[3].fillAmount == 0f)
		{
			if (this.WeaponID == 6 && SchemeGlobals.GetSchemeStage(4) == 1)
			{
				SchemeGlobals.SetSchemeStage(4, 2);
				this.Yandere.PauseScreen.Schemes.UpdateInstructions();
			}
			this.Prompt.Circle[3].fillAmount = 1f;
			if (this.Prompt.Suspicious)
			{
				this.Yandere.TheftTimer = 0.1f;
			}
			if (this.Dangerous || this.Suspicious)
			{
				this.Yandere.WeaponTimer = 0.1f;
			}
			if (!this.Yandere.Gloved)
			{
				this.FingerprintID = 100;
			}
			this.ID = 0;
			while (this.ID < this.Outline.Length)
			{
				this.Outline[this.ID].color = new Color(0f, 0f, 0f, 1f);
				this.ID++;
			}
			base.transform.parent = this.Yandere.ItemParent;
			base.transform.localPosition = Vector3.zero;
			if (this.Type == WeaponType.Bat)
			{
				base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			}
			else
			{
				base.transform.localEulerAngles = Vector3.zero;
			}
			this.MyCollider.enabled = false;
			this.MyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			if (this.Yandere.Equipped == 3)
			{
				this.Yandere.Weapon[3].Drop();
			}
			if (this.Yandere.PickUp != null)
			{
				this.Yandere.PickUp.Drop();
			}
			if (this.Yandere.Dragging)
			{
				this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
			}
			if (this.Yandere.Carrying)
			{
				this.Yandere.StopCarrying();
			}
			if (this.Concealable)
			{
				if (this.Yandere.Weapon[1] == null)
				{
					if (this.Yandere.Weapon[2] != null)
					{
						this.Yandere.Weapon[2].gameObject.SetActive(false);
					}
					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
				}
				else if (this.Yandere.Weapon[2] == null)
				{
					if (this.Yandere.Weapon[1] != null)
					{
						this.Yandere.Weapon[1].gameObject.SetActive(false);
					}
					this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
				}
				else if (this.Yandere.Weapon[2].gameObject.activeInHierarchy)
				{
					this.Yandere.Weapon[2].Drop();
					this.Yandere.Equipped = 2;
					this.Yandere.EquippedWeapon = this;
				}
				else
				{
					this.Yandere.Weapon[1].Drop();
					this.Yandere.Equipped = 1;
					this.Yandere.EquippedWeapon = this;
				}
			}
			else
			{
				if (this.Yandere.Weapon[1] != null)
				{
					this.Yandere.Weapon[1].gameObject.SetActive(false);
				}
				if (this.Yandere.Weapon[2] != null)
				{
					this.Yandere.Weapon[2].gameObject.SetActive(false);
				}
				this.Yandere.Equipped = 3;
				this.Yandere.EquippedWeapon = this;
			}
			this.Yandere.StudentManager.UpdateStudents(0);
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Yandere.NearestPrompt = null;
			if (this.WeaponID == 9 || this.WeaponID == 10 || this.WeaponID == 12 || this.WeaponID == 25)
			{
				this.SuspicionCheck();
			}
			if (this.Yandere.EquippedWeapon.Suspicious)
			{
				if (!this.Yandere.WeaponWarning)
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Armed);
					this.Yandere.WeaponWarning = true;
				}
			}
			else
			{
				this.Yandere.WeaponWarning = false;
			}
			this.Yandere.WeaponMenu.UpdateSprites();
			this.Yandere.WeaponManager.UpdateLabels();
			if (this.Evidence)
			{
				this.Yandere.Police.BloodyWeapons--;
			}
			if (this.WeaponID == 11)
			{
				this.Yandere.IdleAnim = "CyborgNinja_Idle_Armed";
				this.Yandere.WalkAnim = "CyborgNinja_Walk_Armed";
				this.Yandere.RunAnim = "CyborgNinja_Run_Armed";
			}
			if (this.WeaponID == 26)
			{
				this.WeaponTrail.SetActive(true);
			}
			this.KinematicTimer = 0f;
			AudioSource.PlayClipAtPoint(this.EquipClip, Camera.main.transform.position);
		}
		if (this.Yandere.EquippedWeapon == this && this.Yandere.Armed)
		{
			base.transform.localScale = new Vector3(1f, 1f, 1f);
			if (!this.Yandere.Struggling)
			{
				if (this.Yandere.CanMove)
				{
					base.transform.localPosition = Vector3.zero;
					if (this.Type == WeaponType.Bat)
					{
						base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
					}
					else
					{
						base.transform.localEulerAngles = Vector3.zero;
					}
				}
			}
			else
			{
				base.transform.localPosition = new Vector3(-0.01f, 0.005f, -0.01f);
			}
		}
		if (this.Dumped)
		{
			this.DumpTimer += Time.deltaTime;
			if (this.DumpTimer > 1f)
			{
				this.Yandere.Incinerator.MurderWeapons++;
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
		if (base.transform.parent == this.Yandere.ItemParent && this.Concealable && this.Yandere.Weapon[1] != this && this.Yandere.Weapon[2] != this)
		{
			this.Drop();
		}
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x001A0CD8 File Offset: 0x0019F0D8
	public void Drop()
	{
		if (this.WeaponID == 6 && SchemeGlobals.GetSchemeStage(4) == 2)
		{
			SchemeGlobals.SetSchemeStage(4, 1);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		Debug.Log("A " + base.gameObject.name + " has been dropped.");
		if (this.WeaponID == 11)
		{
			this.Yandere.IdleAnim = "CyborgNinja_Idle_Unarmed";
			this.Yandere.WalkAnim = this.Yandere.OriginalWalkAnim;
			this.Yandere.RunAnim = "CyborgNinja_Run_Unarmed";
		}
		if (this.StartLow)
		{
			this.Prompt.OffsetY[3] = this.OriginalOffset;
		}
		if (this.Yandere.EquippedWeapon == this)
		{
			this.Yandere.EquippedWeapon = null;
			this.Yandere.Equipped = 0;
			this.Yandere.StudentManager.UpdateStudents(0);
		}
		base.gameObject.SetActive(true);
		base.transform.parent = null;
		this.MyRigidbody.constraints = RigidbodyConstraints.None;
		this.MyRigidbody.isKinematic = false;
		this.MyRigidbody.useGravity = true;
		this.MyCollider.isTrigger = false;
		if (this.Dumped)
		{
			base.transform.position = this.Incinerator.DumpPoint.position;
		}
		else
		{
			this.Prompt.enabled = true;
			this.MyCollider.enabled = true;
			if (this.Yandere.GetComponent<Collider>().enabled)
			{
				Physics.IgnoreCollision(this.Yandere.GetComponent<Collider>(), this.MyCollider);
			}
		}
		if (this.Evidence)
		{
			this.Yandere.Police.BloodyWeapons++;
		}
		if (Vector3.Distance(this.StartingPosition, base.transform.position) > 5f && Vector3.Distance(base.transform.position, this.Yandere.StudentManager.WeaponBoxSpot.parent.position) > 1f)
		{
			if (!this.Misplaced)
			{
				this.Prompt.Yandere.WeaponManager.MisplacedWeapons++;
				this.Misplaced = true;
			}
		}
		else if (this.Misplaced)
		{
			this.Prompt.Yandere.WeaponManager.MisplacedWeapons--;
			this.Misplaced = false;
		}
		this.ID = 0;
		while (this.ID < this.Outline.Length)
		{
			this.Outline[this.ID].color = ((!this.Evidence) ? this.OriginalColor : this.EvidenceColor);
			this.ID++;
		}
		if (base.transform.position.y > 1000f)
		{
			base.transform.position = new Vector3(12f, 0f, 28f);
		}
		if (this.WeaponID == 26)
		{
			base.transform.parent = this.Parent;
			base.transform.localEulerAngles = Vector3.zero;
			base.transform.localPosition = Vector3.zero;
			this.MyRigidbody.isKinematic = true;
			this.WeaponTrail.SetActive(false);
		}
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x001A1058 File Offset: 0x0019F458
	public void UpdateLabel()
	{
		if (this != null && base.gameObject.activeInHierarchy)
		{
			if (this.Yandere.Weapon[1] != null && this.Yandere.Weapon[2] != null && this.Concealable)
			{
				if (this.Prompt.Label[3] != null)
				{
					if (!this.Yandere.Armed || this.Yandere.Equipped == 3)
					{
						this.Prompt.Label[3].text = "     Swap " + this.Yandere.Weapon[1].Name + " for " + this.Name;
					}
					else
					{
						this.Prompt.Label[3].text = "     Swap " + this.Yandere.EquippedWeapon.Name + " for " + this.Name;
					}
				}
			}
			else if (this.Prompt.Label[3] != null)
			{
				this.Prompt.Label[3].text = "     " + this.Name;
			}
		}
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x001A11AC File Offset: 0x0019F5AC
	public void Effect()
	{
		if (this.WeaponID == 7)
		{
			this.BloodSpray[0].Play();
			this.BloodSpray[1].Play();
		}
		else if (this.WeaponID == 8)
		{
			base.gameObject.GetComponent<ParticleSystem>().Play();
			base.GetComponent<AudioSource>().clip = this.OriginalClip;
			base.GetComponent<AudioSource>().Play();
		}
		else if (this.WeaponID == 2 || this.WeaponID == 9 || this.WeaponID == 10 || this.WeaponID == 12 || this.WeaponID == 13)
		{
			base.GetComponent<AudioSource>().Play();
		}
		else if (this.WeaponID == 14)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.HeartBurst, this.Yandere.TargetStudent.Head.position, Quaternion.identity);
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x001A12B0 File Offset: 0x0019F6B0
	public void Dismember()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.DismemberClip;
		component.Play();
		this.Dismembering = true;
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x001A12E0 File Offset: 0x0019F6E0
	public void SuspicionCheck()
	{
		Debug.Log("Suspicion Check!");
		if ((this.WeaponID == 9 && ClubGlobals.Club == ClubType.Sports) || (this.WeaponID == 10 && ClubGlobals.Club == ClubType.Gardening) || (this.WeaponID == 12 && ClubGlobals.Club == ClubType.Sports) || (this.WeaponID == 25 && ClubGlobals.Club == ClubType.LightMusic))
		{
			this.Suspicious = false;
		}
		else
		{
			this.Suspicious = true;
		}
	}

	// Token: 0x04003805 RID: 14341
	public ParticleSystem[] ShortBloodSpray;

	// Token: 0x04003806 RID: 14342
	public ParticleSystem[] BloodSpray;

	// Token: 0x04003807 RID: 14343
	public OutlineScript[] Outline;

	// Token: 0x04003808 RID: 14344
	public float[] SoundTime;

	// Token: 0x04003809 RID: 14345
	public IncineratorScript Incinerator;

	// Token: 0x0400380A RID: 14346
	public StudentScript Returner;

	// Token: 0x0400380B RID: 14347
	public YandereScript Yandere;

	// Token: 0x0400380C RID: 14348
	public PromptScript Prompt;

	// Token: 0x0400380D RID: 14349
	public Transform Origin;

	// Token: 0x0400380E RID: 14350
	public Transform Parent;

	// Token: 0x0400380F RID: 14351
	public AudioClip[] Clips;

	// Token: 0x04003810 RID: 14352
	public AudioClip[] Clips2;

	// Token: 0x04003811 RID: 14353
	public AudioClip[] Clips3;

	// Token: 0x04003812 RID: 14354
	public AudioClip DismemberClip;

	// Token: 0x04003813 RID: 14355
	public AudioClip EquipClip;

	// Token: 0x04003814 RID: 14356
	public ParticleSystem FireEffect;

	// Token: 0x04003815 RID: 14357
	public GameObject WeaponTrail;

	// Token: 0x04003816 RID: 14358
	public GameObject ExtraBlade;

	// Token: 0x04003817 RID: 14359
	public AudioSource FireAudio;

	// Token: 0x04003818 RID: 14360
	public Rigidbody MyRigidbody;

	// Token: 0x04003819 RID: 14361
	public Collider MyCollider;

	// Token: 0x0400381A RID: 14362
	public Renderer MyRenderer;

	// Token: 0x0400381B RID: 14363
	public Transform Blade;

	// Token: 0x0400381C RID: 14364
	public Projector Blood;

	// Token: 0x0400381D RID: 14365
	public Vector3 StartingPosition;

	// Token: 0x0400381E RID: 14366
	public Vector3 StartingRotation;

	// Token: 0x0400381F RID: 14367
	public bool AlreadyExamined;

	// Token: 0x04003820 RID: 14368
	public bool DisableCollider;

	// Token: 0x04003821 RID: 14369
	public bool Dismembering;

	// Token: 0x04003822 RID: 14370
	public bool MurderWeapon;

	// Token: 0x04003823 RID: 14371
	public bool WeaponEffect;

	// Token: 0x04003824 RID: 14372
	public bool Concealable;

	// Token: 0x04003825 RID: 14373
	public bool Suspicious;

	// Token: 0x04003826 RID: 14374
	public bool Dangerous;

	// Token: 0x04003827 RID: 14375
	public bool Misplaced;

	// Token: 0x04003828 RID: 14376
	public bool Evidence;

	// Token: 0x04003829 RID: 14377
	public bool StartLow;

	// Token: 0x0400382A RID: 14378
	public bool Flaming;

	// Token: 0x0400382B RID: 14379
	public bool Bloody;

	// Token: 0x0400382C RID: 14380
	public bool Dumped;

	// Token: 0x0400382D RID: 14381
	public bool Heated;

	// Token: 0x0400382E RID: 14382
	public bool Rotate;

	// Token: 0x0400382F RID: 14383
	public bool Metal;

	// Token: 0x04003830 RID: 14384
	public bool Flip;

	// Token: 0x04003831 RID: 14385
	public bool Spin;

	// Token: 0x04003832 RID: 14386
	public Color EvidenceColor;

	// Token: 0x04003833 RID: 14387
	public Color OriginalColor;

	// Token: 0x04003834 RID: 14388
	public float OriginalOffset;

	// Token: 0x04003835 RID: 14389
	public float KinematicTimer;

	// Token: 0x04003836 RID: 14390
	public float DumpTimer;

	// Token: 0x04003837 RID: 14391
	public float Rotation;

	// Token: 0x04003838 RID: 14392
	public float Speed;

	// Token: 0x04003839 RID: 14393
	public string SpriteName;

	// Token: 0x0400383A RID: 14394
	public string Name;

	// Token: 0x0400383B RID: 14395
	public int DismemberPhase;

	// Token: 0x0400383C RID: 14396
	public int FingerprintID;

	// Token: 0x0400383D RID: 14397
	public int GlobalID;

	// Token: 0x0400383E RID: 14398
	public int WeaponID;

	// Token: 0x0400383F RID: 14399
	public int AnimID;

	// Token: 0x04003840 RID: 14400
	public WeaponType Type = WeaponType.Knife;

	// Token: 0x04003841 RID: 14401
	public bool[] Victims;

	// Token: 0x04003842 RID: 14402
	private AudioClip OriginalClip;

	// Token: 0x04003843 RID: 14403
	private int ID;

	// Token: 0x04003844 RID: 14404
	public GameObject HeartBurst;
}
