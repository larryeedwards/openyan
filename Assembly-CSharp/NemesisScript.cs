using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200046F RID: 1135
public class NemesisScript : MonoBehaviour
{
	// Token: 0x06001DD8 RID: 7640 RVA: 0x0011C7A8 File Offset: 0x0011ABA8
	private void Start()
	{
		foreach (GameObject gameObject in this.Cosmetic.FemaleHair)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject2 in this.Cosmetic.TeacherHair)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		foreach (GameObject gameObject3 in this.Cosmetic.FemaleAccessories)
		{
			if (gameObject3 != null)
			{
				gameObject3.SetActive(false);
			}
		}
		foreach (GameObject gameObject4 in this.Cosmetic.TeacherAccessories)
		{
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
		foreach (GameObject gameObject5 in this.Cosmetic.ClubAccessories)
		{
			if (gameObject5 != null)
			{
				gameObject5.SetActive(false);
			}
		}
		foreach (GameObject gameObject6 in this.Cosmetic.Kerchiefs)
		{
			if (gameObject6 != null)
			{
				gameObject6.SetActive(false);
			}
		}
		this.Difficulty = MissionModeGlobals.NemesisDifficulty;
		this.Student.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
		this.Student.WitnessCamera = GameObject.Find("WitnessCamera").GetComponent<WitnessCameraScript>();
		this.Student.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
		this.Student.JSON = GameObject.Find("JSON").GetComponent<JsonScript>();
		this.Student.CharacterAnimation = this.Student.Character.GetComponent<Animation>();
		this.Student.Ragdoll.Nemesis = true;
		this.Student.Yandere = this.Yandere;
		this.Student.IdleAnim = "f02_newIdle_00";
		this.Student.WalkAnim = "f02_newWalk_00";
		this.Student.ShoeRemoval.RightCasualShoe.gameObject.SetActive(false);
		this.Student.ShoeRemoval.LeftCasualShoe.gameObject.SetActive(false);
		if (this.Difficulty < 3)
		{
			this.Student.Character.GetComponent<Animation>()["f02_nemesisEyes_00"].layer = 2;
			this.Student.Character.GetComponent<Animation>().Play("f02_nemesisEyes_00");
			this.Cosmetic.MyRenderer.sharedMesh = this.Cosmetic.FemaleUniforms[5];
			this.Cosmetic.MyRenderer.materials[0].mainTexture = this.NemesisUniform;
			this.Cosmetic.MyRenderer.materials[1].mainTexture = this.NemesisUniform;
			this.Cosmetic.MyRenderer.materials[2].mainTexture = this.NemesisFace;
			this.Cosmetic.RightEyeRenderer.material.mainTexture = this.NemesisEyes;
			this.Cosmetic.LeftEyeRenderer.material.mainTexture = this.NemesisEyes;
			this.Student.FaceCollider.tag = "Nemesis";
			this.NemesisHair.SetActive(true);
		}
		else
		{
			this.NemesisHair.SetActive(false);
			this.PutOnDisguise = true;
		}
		this.Student.LowPoly.enabled = false;
		this.Student.DisableEffects();
		this.HideObjects();
		this.ID = 0;
		while (this.ID < this.Student.Ragdoll.AllRigidbodies.Length)
		{
			this.Student.Ragdoll.AllRigidbodies[this.ID].isKinematic = true;
			this.Student.Ragdoll.AllColliders[this.ID].enabled = false;
			this.ID++;
		}
		this.Student.Ragdoll.AllColliders[10].enabled = true;
		this.Student.Prompt.HideButton[0] = true;
		this.Student.Prompt.HideButton[2] = true;
		UnityEngine.Object.Destroy(this.Student.MyRigidbody);
		base.transform.position = this.MissionMode.SpawnPoints[UnityEngine.Random.Range(0, 4)].position;
		this.MissionMode.LastKnownPosition.position = new Vector3(0f, 0f, -36f);
		this.UpdateLKP();
		base.transform.parent = null;
		this.Student.Name = "Nemesis";
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x0011CCB0 File Offset: 0x0011B0B0
	private void Update()
	{
		if (this.PutOnDisguise)
		{
			int num = 1;
			while ((this.Student.StudentManager.Students[num] != null && this.Student.StudentManager.Students[num].Male) || (num > 5 && num < 21) || num == 21 || num == 26 || num == 31 || num == 36 || num == 41 || num == 46 || num == 51 || num == 56 || num == 61 || num == 66 || num == 71 || num == this.MissionMode.TargetID)
			{
				num = UnityEngine.Random.Range(2, 90);
			}
			this.Student.StudentManager.Students[num].gameObject.SetActive(false);
			this.Student.StudentManager.Students[num].Replaced = true;
			this.Cosmetic.StudentID = num;
			this.Cosmetic.Start();
			OutlineScript component = this.Cosmetic.FemaleHair[this.Cosmetic.Hairstyle].GetComponent<OutlineScript>();
			if (component != null)
			{
				component.enabled = false;
			}
			else
			{
				component = this.Cosmetic.FemaleHairRenderers[this.Cosmetic.Hairstyle].GetComponent<OutlineScript>();
				if (component != null)
				{
					component.enabled = false;
				}
			}
			this.Student.FaceCollider.tag = "Disguise";
			Debug.Log("Nemesis has disguised herself as " + this.Student.StudentManager.Students[num].Name);
			this.PutOnDisguise = false;
		}
		if (!this.Dying)
		{
			if (!this.Attacking)
			{
				if (this.Yandere.Laughing && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 10f)
				{
					this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
					this.UpdateLKP();
				}
				if (!this.Yandere.CanMove && !this.Yandere.Laughing)
				{
					if (this.Student.Pathfinding.canSearch)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade("f02_idleShort_00");
						this.Student.Pathfinding.canSearch = false;
						this.Student.Pathfinding.canMove = false;
						this.Student.Pathfinding.speed = 0f;
					}
				}
				else
				{
					if (this.Yandere.Stance.Current != StanceType.Crouching && this.Yandere.Stance.Current != StanceType.Crawling && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 10f && this.Yandere.Running)
					{
						this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
						this.UpdateLKP();
					}
					if (!this.Student.Pathfinding.canSearch)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.WalkAnim);
						this.Student.Pathfinding.canSearch = true;
						this.Student.Pathfinding.canMove = true;
						this.Student.Pathfinding.speed = 1f;
					}
					this.InView = false;
					this.LookForYandere();
					this.Student.Pathfinding.speed = Mathf.MoveTowards(this.Student.Pathfinding.speed, (!this.InView) ? 1f : 2f, Time.deltaTime * 0.1f);
					this.Student.Character.GetComponent<Animation>()[this.Student.WalkAnim].speed = this.Student.Pathfinding.speed;
					if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
					{
						if (this.InView)
						{
							this.Student.CharacterAnimation.CrossFade("f02_knifeLowSanityA_00");
							this.Yandere.CharacterAnimation.CrossFade("f02_knifeLowSanityB_00");
							AudioSource.PlayClipAtPoint(this.YandereDeath, base.transform.position);
							this.Student.Pathfinding.canSearch = false;
							this.Student.Pathfinding.canMove = false;
							this.Knife.SetActive(true);
							this.Attacking = true;
							this.OriginalYPosition = this.Yandere.transform.position.y;
							this.Yandere.StudentManager.YandereDying = true;
							this.Yandere.StudentManager.StopMoving();
							AudioSource component2 = base.GetComponent<AudioSource>();
							component2.Play();
							this.Yandere.YandereVision = false;
							this.Yandere.FollowHips = true;
							this.Yandere.Laughing = false;
							this.Yandere.CanMove = false;
							this.Yandere.EyeShrink = 1f;
							this.Yandere.StopAiming();
							this.Yandere.EmptyHands();
						}
					}
					else if (Vector3.Distance(base.transform.position, this.MissionMode.LastKnownPosition.position) < 1f)
					{
						this.Student.Character.GetComponent<Animation>().CrossFade("f02_nemesisScan_00");
						this.Student.Pathfinding.speed = 0f;
						this.ScanTimer += Time.deltaTime;
						if (this.ScanTimer > 6f)
						{
							Vector3 vector = new Vector3(0f, 0f, -2.5f);
							this.MissionMode.LastKnownPosition.position = ((!(this.MissionMode.LastKnownPosition.position == vector)) ? vector : this.Yandere.transform.position);
							this.UpdateLKP();
						}
					}
				}
				if (this.Difficulty == 1 || this.Difficulty == 3)
				{
					if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
					{
						float f = Vector3.Angle(-base.transform.forward, this.Yandere.transform.position - base.transform.position);
						if (Mathf.Abs(f) > 45f)
						{
							this.Student.Prompt.HideButton[2] = true;
						}
						else if (this.Yandere.Armed)
						{
							this.Student.Prompt.HideButton[2] = false;
						}
						if (!this.Yandere.Armed)
						{
							this.Student.Prompt.HideButton[2] = true;
						}
						if (this.Student.Prompt.Circle[2].fillAmount < 1f)
						{
							this.Yandere.TargetStudent = this.Student;
							this.Yandere.AttackManager.Stealth = true;
							this.Student.AttackReaction();
							this.Student.Pathfinding.canSearch = false;
							this.Student.Pathfinding.canMove = false;
							this.Student.Prompt.HideButton[2] = true;
							this.Dying = true;
						}
					}
					else
					{
						this.Student.Prompt.HideButton[2] = true;
					}
				}
			}
			else
			{
				this.SpecialEffect();
				this.Yandere.targetRotation = Quaternion.LookRotation(base.transform.position - this.Yandere.transform.position);
				this.Yandere.transform.rotation = Quaternion.Slerp(this.Yandere.transform.rotation, this.Yandere.targetRotation, Time.deltaTime * 10f);
				this.Yandere.MoveTowardsTarget(base.transform.position + base.transform.forward * 0.5f);
				this.Yandere.EyeShrink = 1f;
				this.Yandere.transform.position = new Vector3(this.Yandere.transform.position.x, this.OriginalYPosition, this.Yandere.transform.position.z);
				Quaternion b = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * 10f);
				Animation component3 = this.Student.Character.GetComponent<Animation>();
				if (component3["f02_knifeLowSanityA_00"].time >= component3["f02_knifeLowSanityA_00"].length)
				{
					if (this.MissionMode.enabled)
					{
						this.MissionMode.GameOverID = 13;
						this.MissionMode.GameOver();
						this.MissionMode.Phase = 4;
						base.enabled = false;
					}
					else
					{
						SceneManager.LoadScene("LoadingScene");
					}
				}
			}
		}
		else if (this.Student.Alive)
		{
			this.Student.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * this.Yandere.AttackManager.Distance);
			Quaternion b2 = Quaternion.LookRotation(base.transform.position - new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z));
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * 10f);
		}
		else
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x0011D77C File Offset: 0x0011BB7C
	private void LookForYandere()
	{
		if (this.Student.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
		{
			this.MissionMode.LastKnownPosition.position = this.Yandere.transform.position;
			this.InView = true;
			this.UpdateLKP();
		}
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x0011D7E4 File Offset: 0x0011BBE4
	private void UpdateLKP()
	{
		this.Student.Character.GetComponent<Animation>().CrossFade(this.Student.WalkAnim);
		if (this.Student.Pathfinding.speed == 0f)
		{
			this.Student.Pathfinding.speed = 1f;
		}
		this.ScanTimer = 0f;
		this.InView = true;
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x0011D854 File Offset: 0x0011BC54
	private void SpecialEffect()
	{
		Animation component = this.Student.Character.GetComponent<Animation>();
		if (this.EffectPhase == 0)
		{
			if (component["f02_knifeLowSanityA_00"].time > 2.76666665f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position + this.Knife.transform.forward * 0.1f, Quaternion.identity);
				this.EffectPhase++;
			}
		}
		else if (this.EffectPhase == 1)
		{
			if (component["f02_knifeLowSanityA_00"].time > 3.5333333f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position + this.Knife.transform.forward * 0.1f, Quaternion.identity);
				this.EffectPhase++;
			}
		}
		else if (this.EffectPhase == 2 && component["f02_knifeLowSanityA_00"].time > 4.16666651f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.Knife.transform.position + this.Knife.transform.forward * 0.1f, Quaternion.identity);
			this.EffectPhase++;
		}
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x0011D9D8 File Offset: 0x0011BDD8
	private void HideObjects()
	{
		this.Student.Cosmetic.RightStockings[0].SetActive(false);
		this.Student.Cosmetic.LeftStockings[0].SetActive(false);
		this.Student.Cosmetic.RightWristband.SetActive(false);
		this.Student.Cosmetic.LeftWristband.SetActive(false);
		this.Student.DramaticCamera.gameObject.SetActive(false);
		this.Student.VomitEmitter.gameObject.SetActive(false);
		this.Student.Countdown.gameObject.SetActive(false);
		this.Student.ScienceProps[0].SetActive(false);
		this.Student.Chopsticks[0].SetActive(false);
		this.Student.Chopsticks[1].SetActive(false);
		this.Student.Handkerchief.SetActive(false);
		this.Student.ChaseCamera.SetActive(false);
		this.Student.PepperSpray.SetActive(false);
		this.Student.WateringCan.SetActive(false);
		this.Student.OccultBook.SetActive(false);
		this.Student.Cigarette.SetActive(false);
		this.Student.EventBook.SetActive(false);
		this.Student.Handcuffs.SetActive(false);
		this.Student.CandyBar.SetActive(false);
		this.Student.Scrubber.SetActive(false);
		this.Student.Lighter.SetActive(false);
		this.Student.Octodog.SetActive(false);
		this.Student.Eraser.SetActive(false);
		this.Student.Bento.SetActive(false);
		this.Student.Pen.SetActive(false);
		this.Student.SpeechLines.Stop();
		this.Student.InstrumentBag[1].SetActive(false);
		this.Student.InstrumentBag[2].SetActive(false);
		this.Student.InstrumentBag[3].SetActive(false);
		this.Student.InstrumentBag[4].SetActive(false);
		this.Student.InstrumentBag[5].SetActive(false);
		this.Student.Instruments[1].SetActive(false);
		this.Student.Instruments[2].SetActive(false);
		this.Student.Instruments[3].SetActive(false);
		this.Student.Instruments[4].SetActive(false);
		this.Student.Instruments[5].SetActive(false);
		this.Student.Drumsticks[0].SetActive(false);
		this.Student.Drumsticks[1].SetActive(false);
		this.Student.Cosmetic.ThickBrows.SetActive(false);
		foreach (GameObject gameObject in this.Student.Cosmetic.PunkAccessories)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject2 in this.Student.Fingerfood)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	// Token: 0x0400259F RID: 9631
	public MissionModeScript MissionMode;

	// Token: 0x040025A0 RID: 9632
	public CosmeticScript Cosmetic;

	// Token: 0x040025A1 RID: 9633
	public StudentScript Student;

	// Token: 0x040025A2 RID: 9634
	public YandereScript Yandere;

	// Token: 0x040025A3 RID: 9635
	public AudioClip YandereDeath;

	// Token: 0x040025A4 RID: 9636
	public Texture NemesisUniform;

	// Token: 0x040025A5 RID: 9637
	public Texture NemesisFace;

	// Token: 0x040025A6 RID: 9638
	public Texture NemesisEyes;

	// Token: 0x040025A7 RID: 9639
	public GameObject BloodEffect;

	// Token: 0x040025A8 RID: 9640
	public GameObject NemesisHair;

	// Token: 0x040025A9 RID: 9641
	public GameObject Knife;

	// Token: 0x040025AA RID: 9642
	public bool PutOnDisguise;

	// Token: 0x040025AB RID: 9643
	public bool Attacking;

	// Token: 0x040025AC RID: 9644
	public bool InView;

	// Token: 0x040025AD RID: 9645
	public bool Dying;

	// Token: 0x040025AE RID: 9646
	public int EffectPhase;

	// Token: 0x040025AF RID: 9647
	public int Difficulty;

	// Token: 0x040025B0 RID: 9648
	public int ID;

	// Token: 0x040025B1 RID: 9649
	public float OriginalYPosition;

	// Token: 0x040025B2 RID: 9650
	public float ScanTimer = 6f;
}
