using System;
using UnityEngine;

// Token: 0x020004B5 RID: 1205
public class RagdollScript : MonoBehaviour
{
	// Token: 0x06001EFA RID: 7930 RVA: 0x0013A16C File Offset: 0x0013856C
	private void Start()
	{
		this.ElectrocutionAnimation = false;
		this.MurderSuicideAnimation = false;
		this.BurningAnimation = false;
		this.ChokingAnimation = false;
		this.Disturbing = false;
		Physics.IgnoreLayerCollision(11, 13, true);
		this.Zs.SetActive(this.Tranquil);
		if (!this.Tranquil && !this.Poisoned && !this.Drowned && !this.Electrocuted && !this.Burning && !this.NeckSnapped)
		{
			this.Student.StudentManager.TutorialWindow.ShowPoolMessage = true;
			this.BloodPoolSpawner.gameObject.SetActive(true);
			if (this.Pushed)
			{
				this.BloodPoolSpawner.Timer = 5f;
			}
		}
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			this.AllRigidbodies[i].isKinematic = false;
			this.AllColliders[i].enabled = true;
			if (this.Yandere.StudentManager.NoGravity)
			{
				this.AllRigidbodies[i].useGravity = false;
			}
		}
		this.Prompt.enabled = true;
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		if (this.Student.Yandere.BlackHole)
		{
			this.DisableRigidbodies();
		}
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x0013A2E4 File Offset: 0x001386E4
	private void Update()
	{
		if (!this.Dragged && !this.Carried && !this.Settled && !this.Yandere.PK && !this.Yandere.StudentManager.NoGravity)
		{
			this.SettleTimer += Time.deltaTime;
			if (this.SettleTimer > 5f)
			{
				this.Settled = true;
				for (int i = 0; i < this.AllRigidbodies.Length; i++)
				{
					this.AllRigidbodies[i].isKinematic = true;
					this.AllColliders[i].enabled = false;
				}
			}
		}
		if (this.DetectionMarker != null)
		{
			if (this.DetectionMarker.Tex.color.a > 0.1f)
			{
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, Mathf.MoveTowards(this.DetectionMarker.Tex.color.a, 0f, Time.deltaTime * 10f));
			}
			else
			{
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, 0f);
				this.DetectionMarker = null;
			}
		}
		if (!this.Dumped)
		{
			if (this.StopAnimation && this.Student.CharacterAnimation.isPlaying)
			{
				this.Student.CharacterAnimation.Stop();
			}
			if (this.BloodPoolSpawner != null && this.BloodPoolSpawner.gameObject.activeInHierarchy && !this.Cauterized)
			{
				if (this.Yandere.PickUp != null)
				{
					if (this.Yandere.PickUp.Blowtorch)
					{
						if (!this.Cauterizable)
						{
							this.Prompt.Label[0].text = "     Cauterize";
							this.Cauterizable = true;
						}
					}
					else if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     Dismember";
						this.Cauterizable = false;
					}
				}
				else if (this.Cauterizable)
				{
					this.Prompt.Label[0].text = "     Dismember";
					this.Cauterizable = false;
				}
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					if (this.Cauterizable)
					{
						this.Prompt.Label[0].text = "     Dismember";
						this.BloodPoolSpawner.enabled = false;
						this.Cauterizable = false;
						this.Cauterized = true;
						this.Yandere.CharacterAnimation.CrossFade("f02_cauterize_00");
						this.Yandere.Cauterizing = true;
						this.Yandere.CanMove = false;
						this.Yandere.PickUp.GetComponent<BlowtorchScript>().enabled = true;
						this.Yandere.PickUp.GetComponent<AudioSource>().Play();
					}
					else if (this.Yandere.StudentManager.OriginalUniforms + this.Yandere.StudentManager.NewUniforms > 1)
					{
						this.Yandere.CharacterAnimation.CrossFade("f02_dismember_00");
						this.Yandere.transform.LookAt(base.transform);
						this.Yandere.RPGCamera.transform.position = this.Yandere.DismemberSpot.position;
						this.Yandere.RPGCamera.transform.eulerAngles = this.Yandere.DismemberSpot.eulerAngles;
						this.Yandere.EquippedWeapon.Dismember();
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.TargetStudent = this.Student;
						this.Yandere.Ragdoll = base.gameObject;
						this.Yandere.Dismembering = true;
						this.Yandere.CanMove = false;
					}
					else if (!this.Yandere.ClothingWarning)
					{
						this.Yandere.NotificationManager.DisplayNotification(NotificationType.Clothing);
						this.Yandere.StudentManager.TutorialWindow.ShowClothingMessage = true;
						this.Yandere.ClothingWarning = true;
					}
				}
			}
			if (this.Prompt.Circle[1].fillAmount == 0f)
			{
				this.Prompt.Circle[1].fillAmount = 1f;
				if (!this.Student.FireEmitters[1].isPlaying)
				{
					if (!this.Dragged)
					{
						this.Yandere.EmptyHands();
						this.Prompt.AcceptingInput[1] = false;
						this.Prompt.Label[1].text = "     Drop";
						this.PickNearestLimb();
						this.Yandere.RagdollDragger.connectedBody = this.Rigidbodies[this.LimbID];
						this.Yandere.RagdollDragger.connectedAnchor = this.LimbAnchor[this.LimbID];
						this.Yandere.Dragging = true;
						this.Yandere.Running = false;
						this.Yandere.DragState = 0;
						this.Yandere.Ragdoll = base.gameObject;
						this.Dragged = true;
						this.Yandere.StudentManager.UpdateStudents(0);
						if (this.MurderSuicide)
						{
							this.Police.MurderSuicideScene = false;
							this.MurderSuicide = false;
						}
						if (this.Suicide)
						{
							this.Police.Suicide = false;
							this.Suicide = false;
						}
						for (int j = 0; j < this.Student.Ragdoll.AllRigidbodies.Length; j++)
						{
							this.Student.Ragdoll.AllRigidbodies[j].drag = 2f;
						}
						for (int k = 0; k < this.AllRigidbodies.Length; k++)
						{
							this.AllRigidbodies[k].isKinematic = false;
							this.AllColliders[k].enabled = true;
							if (this.Yandere.StudentManager.NoGravity)
							{
								this.AllRigidbodies[k].useGravity = false;
							}
						}
					}
					else
					{
						this.StopDragging();
					}
				}
			}
			if (this.Prompt.Circle[3].fillAmount == 0f)
			{
				this.Prompt.Circle[3].fillAmount = 1f;
				if (!this.Student.FireEmitters[1].isPlaying)
				{
					this.Yandere.EmptyHands();
					this.Prompt.Label[1].text = "     Drop";
					this.Prompt.HideButton[1] = true;
					this.Prompt.HideButton[3] = true;
					this.Prompt.enabled = false;
					this.Prompt.Hide();
					for (int l = 0; l < this.AllRigidbodies.Length; l++)
					{
						this.AllRigidbodies[l].isKinematic = true;
						this.AllColliders[l].enabled = false;
					}
					if (this.Male)
					{
						Rigidbody rigidbody = this.AllRigidbodies[0];
						rigidbody.transform.parent.transform.localPosition = new Vector3(rigidbody.transform.parent.transform.localPosition.x, 0.2f, rigidbody.transform.parent.transform.localPosition.z);
					}
					this.Yandere.CharacterAnimation.Play("f02_carryLiftA_00");
					this.Student.CharacterAnimation.Play(this.LiftAnim);
					this.BloodSpawnerCollider.enabled = false;
					this.PelvisRoot.localEulerAngles = new Vector3(this.PelvisRoot.localEulerAngles.x, 0f, this.PelvisRoot.localEulerAngles.z);
					this.Prompt.MyCollider.enabled = false;
					this.PelvisRoot.localPosition = new Vector3(this.PelvisRoot.localPosition.x, this.PelvisRoot.localPosition.y, 0f);
					this.Yandere.Ragdoll = base.gameObject;
					this.Yandere.CurrentRagdoll = this;
					this.Yandere.CanMove = false;
					this.Yandere.Lifting = true;
					this.StopAnimation = false;
					this.Carried = true;
					this.Falling = false;
					this.FallTimer = 0f;
				}
			}
			if (this.Yandere.Running && this.Yandere.CanMove && this.Dragged)
			{
				this.StopDragging();
			}
			if (Vector3.Distance(this.Yandere.transform.position, this.Prompt.transform.position) < 2f)
			{
				if (!this.Suicide && !this.AddingToCount)
				{
					this.Yandere.NearestCorpseID = this.StudentID;
					this.Yandere.NearBodies++;
					this.AddingToCount = true;
				}
			}
			else if (this.AddingToCount)
			{
				this.Yandere.NearBodies--;
				this.AddingToCount = false;
			}
			if (!this.Prompt.AcceptingInput[1] && Input.GetButtonUp("B"))
			{
				this.Prompt.AcceptingInput[1] = true;
			}
			bool flag = false;
			if (this.Yandere.Armed && this.Yandere.EquippedWeapon.WeaponID == 7 && !this.Student.Nemesis)
			{
				flag = true;
			}
			if (!this.Cauterized && this.Yandere.PickUp != null && this.Yandere.PickUp.Blowtorch && this.BloodPoolSpawner.gameObject.activeInHierarchy)
			{
				flag = true;
			}
			this.Prompt.HideButton[0] = (this.Dragged || this.Carried || this.Tranquil || !flag);
		}
		else if (this.DumpType == RagdollDumpType.Incinerator)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.Incinerator.Corpses++;
				this.Incinerator.CorpseList[this.Incinerator.Corpses] = this.StudentID;
				this.Remove();
				base.enabled = false;
			}
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.TranqCase.Open = false;
				if (this.AddingToCount)
				{
					this.Yandere.NearBodies--;
				}
				base.enabled = false;
			}
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			if (this.DumpTimer == 0f && this.Yandere.Carrying)
			{
				this.Student.CharacterAnimation[this.DumpedAnim].time = 2.5f;
			}
			this.Student.CharacterAnimation.CrossFade(this.DumpedAnim);
			this.DumpTimer += Time.deltaTime;
			if (this.Student.CharacterAnimation[this.DumpedAnim].time >= this.Student.CharacterAnimation[this.DumpedAnim].length)
			{
				this.WoodChipper.VictimID = this.StudentID;
				this.Remove();
				base.enabled = false;
			}
		}
		if (this.Hidden && this.HideCollider == null)
		{
			this.Police.HiddenCorpses--;
			this.Hidden = false;
		}
		if (this.Falling)
		{
			this.FallTimer += Time.deltaTime;
			if (this.FallTimer > 2f)
			{
				this.BloodSpawnerCollider.enabled = true;
				this.FallTimer = 0f;
				this.Falling = false;
			}
		}
		if (this.Burning)
		{
			for (int m = 0; m < 3; m++)
			{
				Material material = this.MyRenderer.materials[m];
				material.color = Vector4.MoveTowards(material.color, new Vector4(0.1f, 0.1f, 0.1f, 1f), Time.deltaTime * 0.1f);
			}
			this.Student.Cosmetic.HairRenderer.material.color = Vector4.MoveTowards(this.Student.Cosmetic.HairRenderer.material.color, new Vector4(0.1f, 0.1f, 0.1f, 1f), Time.deltaTime * 0.1f);
			if (this.MyRenderer.materials[0].color == new Color(0.1f, 0.1f, 0.1f, 1f))
			{
				this.Burning = false;
				this.Burned = true;
			}
		}
		if (this.Burned)
		{
			this.Sacrifice = (Vector3.Distance(this.Prompt.transform.position, this.Yandere.StudentManager.SacrificeSpot.position) < 1.5f);
		}
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x0013B2BC File Offset: 0x001396BC
	private void LateUpdate()
	{
		if (!this.Male)
		{
			if (this.LeftEye != null)
			{
				this.LeftEye.localPosition = new Vector3(this.LeftEye.localPosition.x, this.LeftEye.localPosition.y, this.LeftEyeOrigin.z - this.EyeShrink * 0.01f);
				this.RightEye.localPosition = new Vector3(this.RightEye.localPosition.x, this.RightEye.localPosition.y, this.RightEyeOrigin.z + this.EyeShrink * 0.01f);
				this.LeftEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.LeftEye.localScale.z);
				this.RightEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.RightEye.localScale.z);
			}
			if (this.StudentID == 81)
			{
				for (int i = 0; i < 4; i++)
				{
					Transform transform = this.Student.Skirt[i];
					transform.transform.localScale = new Vector3(transform.transform.localScale.x, 0.6666667f, transform.transform.localScale.z);
				}
			}
		}
		if (this.Decapitated)
		{
			this.Head.localScale = Vector3.zero;
		}
		if (this.Yandere.Ragdoll == base.gameObject)
		{
			if (this.Yandere.DumpTimer < 1f)
			{
				if (this.Yandere.Lifting)
				{
					base.transform.position = this.Yandere.transform.position;
					base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
				}
				else if (this.Carried)
				{
					base.transform.position = this.Yandere.transform.position;
					base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
					float axis = Input.GetAxis("Vertical");
					float axis2 = Input.GetAxis("Horizontal");
					if (axis != 0f || axis2 != 0f)
					{
						this.Student.CharacterAnimation.CrossFade((!this.Yandere.Running) ? this.WalkAnim : this.RunAnim);
					}
					else
					{
						this.Student.CharacterAnimation.CrossFade(this.IdleAnim);
					}
					this.Student.CharacterAnimation[this.IdleAnim].time = this.Yandere.CharacterAnimation["f02_carryIdleA_00"].time;
					this.Student.CharacterAnimation[this.WalkAnim].time = this.Yandere.CharacterAnimation["f02_carryWalkA_00"].time;
					this.Student.CharacterAnimation[this.RunAnim].time = this.Yandere.CharacterAnimation["f02_carryRunA_00"].time;
				}
			}
			if (this.Carried)
			{
				if (this.Male)
				{
					Rigidbody rigidbody = this.AllRigidbodies[0];
					rigidbody.transform.parent.transform.localPosition = new Vector3(rigidbody.transform.parent.transform.localPosition.x, 0.2f, rigidbody.transform.parent.transform.localPosition.z);
				}
				if (this.Yandere.Struggling || this.Yandere.DelinquentFighting || this.Yandere.Sprayed)
				{
					this.Fall();
				}
			}
		}
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x0013B72C File Offset: 0x00139B2C
	public void StopDragging()
	{
		foreach (Rigidbody rigidbody in this.Student.Ragdoll.AllRigidbodies)
		{
			rigidbody.drag = 0f;
		}
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		this.Prompt.AcceptingInput[1] = true;
		this.Prompt.Circle[1].fillAmount = 1f;
		this.Prompt.Label[1].text = "     Drag";
		this.Yandere.RagdollDragger.connectedBody = null;
		this.Yandere.RagdollPK.connectedBody = null;
		this.Yandere.Dragging = false;
		this.Yandere.Ragdoll = null;
		this.Yandere.StudentManager.UpdateStudents(0);
		this.SettleTimer = 0f;
		this.Settled = false;
		this.Dragged = false;
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x0013B838 File Offset: 0x00139C38
	private void PickNearestLimb()
	{
		this.NearestLimb = this.Limb[0];
		this.LimbID = 0;
		for (int i = 1; i < 4; i++)
		{
			Transform transform = this.Limb[i];
			if (Vector3.Distance(transform.position, this.Yandere.transform.position) < Vector3.Distance(this.NearestLimb.position, this.Yandere.transform.position))
			{
				this.NearestLimb = transform;
				this.LimbID = i;
			}
		}
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x0013B8C4 File Offset: 0x00139CC4
	public void Dump()
	{
		if (this.DumpType == RagdollDumpType.Incinerator)
		{
			base.transform.eulerAngles = this.Yandere.transform.eulerAngles;
			base.transform.position = this.Yandere.transform.position;
			this.Incinerator = this.Yandere.Incinerator;
			this.BloodPoolSpawner.enabled = false;
		}
		else if (this.DumpType == RagdollDumpType.TranqCase)
		{
			this.TranqCase = this.Yandere.TranqCase;
		}
		else if (this.DumpType == RagdollDumpType.WoodChipper)
		{
			this.WoodChipper = this.Yandere.WoodChipper;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		this.Dumped = true;
		foreach (Rigidbody rigidbody in this.AllRigidbodies)
		{
			rigidbody.isKinematic = true;
		}
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x0013B9B8 File Offset: 0x00139DB8
	public void Fall()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.0001f, base.transform.position.z);
		this.Prompt.Label[1].text = "     Drag";
		this.Prompt.HideButton[1] = false;
		this.Prompt.enabled = true;
		if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !this.Tranquil)
		{
			this.Prompt.HideButton[3] = false;
		}
		if (this.Dragged)
		{
			this.Yandere.RagdollDragger.connectedBody = null;
			this.Yandere.RagdollPK.connectedBody = null;
			this.Yandere.Dragging = false;
			this.Dragged = false;
		}
		this.Yandere.Ragdoll = null;
		this.Prompt.MyCollider.enabled = true;
		this.BloodPoolSpawner.NearbyBlood = 0;
		this.StopAnimation = true;
		this.SettleTimer = 0f;
		this.Carried = false;
		this.Settled = false;
		this.Falling = true;
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			this.AllRigidbodies[i].isKinematic = false;
			this.AllColliders[i].enabled = true;
		}
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x0013BB34 File Offset: 0x00139F34
	public void QuickDismember()
	{
		for (int i = 0; i < this.BodyParts.Length; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BodyParts[i], this.SpawnPoints[i].position, Quaternion.identity);
			gameObject.transform.eulerAngles = this.SpawnPoints[i].eulerAngles;
			gameObject.GetComponent<PromptScript>().enabled = false;
			gameObject.GetComponent<PickUpScript>().enabled = false;
			gameObject.GetComponent<OutlineScript>().enabled = false;
		}
		if (this.BloodPoolSpawner.BloodParent == null)
		{
			this.BloodPoolSpawner.Start();
		}
		Debug.Log("BloodPoolSpawner.transform.position is: " + this.BloodPoolSpawner.transform.position);
		Debug.Log("Student.StudentManager.SEStairs.bounds is: " + this.Student.StudentManager.SEStairs.bounds);
		if (!this.Student.StudentManager.NEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.NWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position))
		{
			this.BloodPoolSpawner.SpawnBigPool();
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x0013BD08 File Offset: 0x0013A108
	public void Dismember()
	{
		if (!this.Dismembered)
		{
			this.Student.LiquidProjector.material.mainTexture = this.Student.BloodTexture;
			for (int i = 0; i < this.BodyParts.Length; i++)
			{
				if (this.Decapitated)
				{
					i++;
					this.Decapitated = false;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BodyParts[i], this.SpawnPoints[i].position, Quaternion.identity);
				gameObject.transform.parent = this.Yandere.LimbParent;
				gameObject.transform.eulerAngles = this.SpawnPoints[i].eulerAngles;
				BodyPartScript component = gameObject.GetComponent<BodyPartScript>();
				component.StudentID = this.StudentID;
				component.Sacrifice = this.Sacrifice;
				if (this.Yandere.StudentManager.NoGravity)
				{
					gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
				if (i == 0)
				{
					if (!this.Student.OriginallyTeacher)
					{
						if (!this.Male)
						{
							this.Student.Cosmetic.FemaleHair[this.Student.Cosmetic.Hairstyle].transform.parent = gameObject.transform;
							if (this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory] != null && this.Student.Cosmetic.Accessory != 3 && this.Student.Cosmetic.Accessory != 6)
							{
								this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
							}
						}
						else
						{
							Transform transform = this.Student.Cosmetic.MaleHair[this.Student.Cosmetic.Hairstyle].transform;
							transform.parent = gameObject.transform;
							transform.localScale *= 1.06382978f;
							if (transform.transform.localPosition.y < -1f)
							{
								transform.transform.localPosition = new Vector3(transform.transform.localPosition.x, transform.transform.localPosition.y - 0.095f, transform.transform.localPosition.z);
							}
							if (this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory] != null)
							{
								this.Student.Cosmetic.MaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
							}
						}
					}
					else
					{
						this.Student.Cosmetic.TeacherHair[this.Student.Cosmetic.Hairstyle].transform.parent = gameObject.transform;
						if (this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory] != null)
						{
							this.Student.Cosmetic.TeacherAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
						}
					}
					if (this.Student.Club != ClubType.Photography && this.Student.Club < ClubType.Gaming && this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
					{
						this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent = gameObject.transform;
						if (this.Student.Club == ClubType.Occult)
						{
							if (!this.Male)
							{
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localPosition = new Vector3(0f, -1.5f, 0.01f);
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localEulerAngles = Vector3.zero;
							}
							else
							{
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localPosition = new Vector3(0f, -1.42f, 0.005f);
								this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.localEulerAngles = Vector3.zero;
							}
						}
					}
					gameObject.GetComponent<Renderer>().materials[0].mainTexture = this.Student.Cosmetic.FaceTexture;
					if (i == 0)
					{
						gameObject.transform.position += new Vector3(0f, 1f, 0f);
					}
				}
				else if (i == 1)
				{
					if (this.Student.Club == ClubType.Photography && this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club] != null)
					{
						this.Student.Cosmetic.ClubAccessories[(int)this.Student.Club].transform.parent = gameObject.transform;
					}
				}
				else if (i == 2 && !this.Student.Male && this.Student.Cosmetic.Accessory == 6)
				{
					this.Student.Cosmetic.FemaleAccessories[this.Student.Cosmetic.Accessory].transform.parent = gameObject.transform;
				}
			}
			if (this.BloodPoolSpawner.BloodParent == null)
			{
				this.BloodPoolSpawner.Start();
			}
			Debug.Log("BloodPoolSpawner.transform.position is: " + this.BloodPoolSpawner.transform.position);
			Debug.Log("Student.StudentManager.SEStairs.bounds is: " + this.Student.StudentManager.SEStairs.bounds);
			Debug.Log("Student.StudentManager.SEStairs.bounds.Contains(BloodPoolSpawner.transform.position) is: " + this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position));
			if (!this.Student.StudentManager.NEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.NWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SEStairs.bounds.Contains(this.BloodPoolSpawner.transform.position) && !this.Student.StudentManager.SWStairs.bounds.Contains(this.BloodPoolSpawner.transform.position))
			{
				this.BloodPoolSpawner.SpawnBigPool();
			}
			this.Police.PartsIcon.gameObject.SetActive(true);
			this.Police.BodyParts += 6;
			this.Yandere.NearBodies--;
			this.Police.Corpses--;
			base.gameObject.SetActive(false);
			this.Dismembered = true;
		}
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0013C4F8 File Offset: 0x0013A8F8
	public void Remove()
	{
		this.BloodPoolSpawner.enabled = false;
		if (this.AddingToCount)
		{
			this.Yandere.NearBodies--;
		}
		if (this.Poisoned)
		{
			this.Police.PoisonScene = false;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0013C554 File Offset: 0x0013A954
	public void DisableRigidbodies()
	{
		this.BloodPoolSpawner.gameObject.SetActive(false);
		for (int i = 0; i < this.AllRigidbodies.Length; i++)
		{
			if (this.AllRigidbodies[i].gameObject.GetComponent<CharacterJoint>() != null)
			{
				UnityEngine.Object.Destroy(this.AllRigidbodies[i].gameObject.GetComponent<CharacterJoint>());
			}
			UnityEngine.Object.Destroy(this.AllRigidbodies[i]);
			this.AllColliders[i].enabled = false;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		base.enabled = false;
	}

	// Token: 0x04002923 RID: 10531
	public BloodPoolSpawnerScript BloodPoolSpawner;

	// Token: 0x04002924 RID: 10532
	public DetectionMarkerScript DetectionMarker;

	// Token: 0x04002925 RID: 10533
	public IncineratorScript Incinerator;

	// Token: 0x04002926 RID: 10534
	public WoodChipperScript WoodChipper;

	// Token: 0x04002927 RID: 10535
	public TranqCaseScript TranqCase;

	// Token: 0x04002928 RID: 10536
	public StudentScript Student;

	// Token: 0x04002929 RID: 10537
	public YandereScript Yandere;

	// Token: 0x0400292A RID: 10538
	public PoliceScript Police;

	// Token: 0x0400292B RID: 10539
	public PromptScript Prompt;

	// Token: 0x0400292C RID: 10540
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400292D RID: 10541
	public Collider BloodSpawnerCollider;

	// Token: 0x0400292E RID: 10542
	public Animation CharacterAnimation;

	// Token: 0x0400292F RID: 10543
	public Collider HideCollider;

	// Token: 0x04002930 RID: 10544
	public Rigidbody[] AllRigidbodies;

	// Token: 0x04002931 RID: 10545
	public Collider[] AllColliders;

	// Token: 0x04002932 RID: 10546
	public Rigidbody[] Rigidbodies;

	// Token: 0x04002933 RID: 10547
	public Transform[] SpawnPoints;

	// Token: 0x04002934 RID: 10548
	public GameObject[] BodyParts;

	// Token: 0x04002935 RID: 10549
	public Transform NearestLimb;

	// Token: 0x04002936 RID: 10550
	public Transform RightBreast;

	// Token: 0x04002937 RID: 10551
	public Transform LeftBreast;

	// Token: 0x04002938 RID: 10552
	public Transform PelvisRoot;

	// Token: 0x04002939 RID: 10553
	public Transform Ponytail;

	// Token: 0x0400293A RID: 10554
	public Transform RightEye;

	// Token: 0x0400293B RID: 10555
	public Transform LeftEye;

	// Token: 0x0400293C RID: 10556
	public Transform HairR;

	// Token: 0x0400293D RID: 10557
	public Transform HairL;

	// Token: 0x0400293E RID: 10558
	public Transform[] Limb;

	// Token: 0x0400293F RID: 10559
	public Transform Head;

	// Token: 0x04002940 RID: 10560
	public Vector3 RightEyeOrigin;

	// Token: 0x04002941 RID: 10561
	public Vector3 LeftEyeOrigin;

	// Token: 0x04002942 RID: 10562
	public Vector3[] LimbAnchor;

	// Token: 0x04002943 RID: 10563
	public GameObject Character;

	// Token: 0x04002944 RID: 10564
	public GameObject Zs;

	// Token: 0x04002945 RID: 10565
	public bool ElectrocutionAnimation;

	// Token: 0x04002946 RID: 10566
	public bool MurderSuicideAnimation;

	// Token: 0x04002947 RID: 10567
	public bool BurningAnimation;

	// Token: 0x04002948 RID: 10568
	public bool ChokingAnimation;

	// Token: 0x04002949 RID: 10569
	public bool AddingToCount;

	// Token: 0x0400294A RID: 10570
	public bool MurderSuicide;

	// Token: 0x0400294B RID: 10571
	public bool Cauterizable;

	// Token: 0x0400294C RID: 10572
	public bool Electrocuted;

	// Token: 0x0400294D RID: 10573
	public bool StopAnimation = true;

	// Token: 0x0400294E RID: 10574
	public bool Decapitated;

	// Token: 0x0400294F RID: 10575
	public bool Dismembered;

	// Token: 0x04002950 RID: 10576
	public bool NeckSnapped;

	// Token: 0x04002951 RID: 10577
	public bool Cauterized;

	// Token: 0x04002952 RID: 10578
	public bool Disturbing;

	// Token: 0x04002953 RID: 10579
	public bool Sacrifice;

	// Token: 0x04002954 RID: 10580
	public bool Disposed;

	// Token: 0x04002955 RID: 10581
	public bool Poisoned;

	// Token: 0x04002956 RID: 10582
	public bool Tranquil;

	// Token: 0x04002957 RID: 10583
	public bool Burning;

	// Token: 0x04002958 RID: 10584
	public bool Carried;

	// Token: 0x04002959 RID: 10585
	public bool Choking;

	// Token: 0x0400295A RID: 10586
	public bool Dragged;

	// Token: 0x0400295B RID: 10587
	public bool Drowned;

	// Token: 0x0400295C RID: 10588
	public bool Falling;

	// Token: 0x0400295D RID: 10589
	public bool Nemesis;

	// Token: 0x0400295E RID: 10590
	public bool Settled;

	// Token: 0x0400295F RID: 10591
	public bool Suicide;

	// Token: 0x04002960 RID: 10592
	public bool Burned;

	// Token: 0x04002961 RID: 10593
	public bool Dumped;

	// Token: 0x04002962 RID: 10594
	public bool Hidden;

	// Token: 0x04002963 RID: 10595
	public bool Pushed;

	// Token: 0x04002964 RID: 10596
	public bool Male;

	// Token: 0x04002965 RID: 10597
	public float AnimStartTime;

	// Token: 0x04002966 RID: 10598
	public float SettleTimer;

	// Token: 0x04002967 RID: 10599
	public float BreastSize;

	// Token: 0x04002968 RID: 10600
	public float DumpTimer;

	// Token: 0x04002969 RID: 10601
	public float EyeShrink;

	// Token: 0x0400296A RID: 10602
	public float FallTimer;

	// Token: 0x0400296B RID: 10603
	public int StudentID;

	// Token: 0x0400296C RID: 10604
	public RagdollDumpType DumpType;

	// Token: 0x0400296D RID: 10605
	public int LimbID;

	// Token: 0x0400296E RID: 10606
	public int Frame;

	// Token: 0x0400296F RID: 10607
	public string DumpedAnim = string.Empty;

	// Token: 0x04002970 RID: 10608
	public string LiftAnim = string.Empty;

	// Token: 0x04002971 RID: 10609
	public string IdleAnim = string.Empty;

	// Token: 0x04002972 RID: 10610
	public string WalkAnim = string.Empty;

	// Token: 0x04002973 RID: 10611
	public string RunAnim = string.Empty;
}
