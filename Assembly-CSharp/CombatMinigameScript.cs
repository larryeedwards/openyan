using System;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class CombatMinigameScript : MonoBehaviour
{
	// Token: 0x06001807 RID: 6151 RVA: 0x000C45D4 File Offset: 0x000C29D4
	private void Start()
	{
		this.RedVignette.color = new Color(1f, 1f, 1f, 0f);
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		this.Circle.enabled = false;
		this.BG.enabled = false;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x000C4658 File Offset: 0x000C2A58
	public void StartCombat()
	{
		this.StartPoint = this.MainCamera.transform.position;
		this.Midpoint.transform.position = this.MainCamera.transform.position + this.MainCamera.transform.forward;
		this.MainCamera.transform.parent = this.CombatTarget;
		this.Yandere.RPGCamera.enabled = false;
		this.Zoom = true;
		if (this.Delinquent.Male)
		{
			this.Prefix = string.Empty;
		}
		else
		{
			this.Prefix = "Female_";
		}
		if (!this.Practice)
		{
			this.Difficulty = 1f;
		}
		else
		{
			this.Delinquent.MyWeapon.GetComponent<Rigidbody>().isKinematic = true;
			this.Delinquent.MyWeapon.GetComponent<Rigidbody>().useGravity = false;
		}
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x000C4750 File Offset: 0x000C2B50
	private void Update()
	{
		if (this.Zoom)
		{
			this.MainCamera.transform.localPosition = Vector3.Lerp(this.MainCamera.transform.localPosition, new Vector3(1.5f, 0.25f, -0.5f), Time.deltaTime * 2f);
			this.RedVignette.color = Vector4.Lerp(this.RedVignette.color, new Color(1f, 1f, 1f, 1f - (float)this.Yandere.Health * 1f / 10f), Time.deltaTime);
			if (this.Timer < 1f)
			{
				this.Delinquent.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
			}
			this.Timer += Time.deltaTime;
			this.AdjustMidpoint();
			if (this.Timer > 1.5f)
			{
				Debug.Log(base.name + " is being instructed to perform the first combat animation of the combat minigame.");
				this.Delinquent.CharacterAnimation.CrossFade(this.Prefix + "Delinquent_CombatA");
				this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatA");
				this.CameraStart = this.MainCamera.localPosition;
				this.Label.text = "State: A";
				this.Zoom = false;
				this.Timer = 0f;
				this.Phase = 1;
				this.Path = 1;
				this.MyAudio.clip = this.CombatSFX[this.Phase];
				this.MyAudio.Play();
				this.MyVocals.clip = this.Vocals[this.Phase];
				this.MyVocals.Play();
			}
		}
		if (this.Phase > 0)
		{
			this.MainCamera.position += new Vector3(this.Shake * UnityEngine.Random.Range(-1f, 1f), this.Shake * UnityEngine.Random.Range(-1f, 1f), this.Shake * UnityEngine.Random.Range(-1f, 1f));
			this.Shake = Mathf.Lerp(this.Shake, 0f, Time.deltaTime * 10f);
			this.AdjustMidpoint();
		}
		if (this.ButtonID > 0)
		{
			this.Timer += Time.deltaTime;
			this.Circle.fillAmount = 1f - this.Timer / 0.33333f;
			if ((Input.GetButtonDown("A") && this.CurrentButton != "A") || (Input.GetButtonDown("B") && this.CurrentButton != "B") || (Input.GetButtonDown("X") && this.CurrentButton != "X") || (Input.GetButtonDown("Y") && this.CurrentButton != "Y"))
			{
				Time.timeScale = 1f;
				this.MyVocals.pitch = 1f;
				this.MyAudio.pitch = 1f;
				this.DisablePrompts();
				this.Phase++;
			}
			else if (Input.GetButtonDown(this.CurrentButton))
			{
				this.Success = true;
			}
		}
		if (this.Path == 1)
		{
			if (!this.Delinquent.CharacterAnimation.IsPlaying(this.Prefix + "Delinquent_CombatA"))
			{
				this.Delinquent.CharacterAnimation.CrossFade(this.Prefix + "Delinquent_CombatA");
				this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatA"].time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
			}
			this.MainCamera.localPosition = Vector3.Lerp(this.MainCamera.localPosition, this.CameraStart, Time.deltaTime);
			if (this.Phase == 1)
			{
				if (this.Yandere.CharacterAnimation["Yandere_CombatA"].time > 1f)
				{
					this.StartTime = this.Yandere.CharacterAnimation["Yandere_CombatA"].time - 1f;
					this.ChooseButton();
					this.Slowdown();
					this.Phase++;
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Yandere.CharacterAnimation["Yandere_CombatA"].time > 1.33333f)
				{
					Time.timeScale = 1f;
					this.MyVocals.pitch = 1f;
					this.MyAudio.pitch = 1f;
					this.DisablePrompts();
					this.Phase++;
				}
				else if (this.Success)
				{
					this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatB"].time = this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatA"].time;
					this.Yandere.CharacterAnimation["Yandere_CombatB"].time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
					this.Delinquent.CharacterAnimation.Play(this.Prefix + "Delinquent_CombatB");
					this.Yandere.CharacterAnimation.Play("Yandere_CombatB");
					this.Label.text = "State: B";
					Time.timeScale = 1f;
					this.MyAudio.pitch = 1f;
					this.DisablePrompts();
					this.Strike = 0;
					this.Path = 2;
					this.Phase++;
					this.MyAudio.clip = this.CombatSFX[this.Path];
					this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatB"].time;
					this.MyAudio.Play();
					this.MyVocals.clip = this.Vocals[this.Path];
					this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatB"].time + 0.5f;
					this.MyVocals.Play();
				}
			}
			else if (this.Phase == 3)
			{
				if (this.Strike < 1)
				{
					if (this.Yandere.CharacterAnimation["Yandere_CombatA"].time > 1.66666f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, this.Yandere.LeftArmRoll.position, Quaternion.identity);
						this.Shake += this.ShakeFactor;
						this.Strike++;
						this.Yandere.Health--;
						this.RedVignette.color = new Color(1f, 1f, 1f, 1f - (float)this.Yandere.Health * 1f / 10f);
					}
				}
				else if (this.Strike < 2 && this.Yandere.CharacterAnimation["Yandere_CombatA"].time > 2.5f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, this.Yandere.RightArmRoll.position, Quaternion.identity);
					this.Shake += this.ShakeFactor;
					this.Strike++;
					this.Yandere.Health--;
					if (this.Yandere.Health < 0)
					{
						this.Yandere.Health = 0;
					}
					this.RedVignette.color = new Color(1f, 1f, 1f, 1f - (float)this.Yandere.Health * 1f / 10f);
					if (!this.Practice)
					{
						this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 1f - (float)this.Yandere.Health * 1f / 10f);
					}
					if (this.Yandere.Health < 1)
					{
						if (!this.Delinquent.WitnessedMurder)
						{
							this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatF"].time = this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatA"].time;
							this.Yandere.CharacterAnimation["Yandere_CombatF"].time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
							this.Delinquent.CharacterAnimation.CrossFade(this.Prefix + "Delinquent_CombatF");
							this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatF");
							this.Shake += this.ShakeFactor;
							this.Label.text = "State: F";
							Time.timeScale = 1f;
							this.MyVocals.pitch = 1f;
							this.MyAudio.pitch = 1f;
							this.DisablePrompts();
							this.Timer = 0f;
							this.Path = 6;
							this.Phase++;
							this.MyAudio.clip = this.CombatSFX[this.Path];
							this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatF"].time;
							this.MyAudio.Play();
							this.MyVocals.clip = this.Vocals[this.Path];
							this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatF"].time;
							this.MyVocals.Play();
						}
						else
						{
							this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatE"].time = this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatA"].time;
							this.Yandere.CharacterAnimation["Yandere_CombatE"].time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
							this.Delinquent.CharacterAnimation.CrossFade(this.Prefix + "Delinquent_CombatE");
							this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatE");
							this.CameraTarget = this.MainCamera.position + new Vector3(0f, 1f, 0f);
							this.MainCamera.parent = null;
							this.Shake += this.ShakeFactor;
							this.KnockedOut = true;
							this.Label.text = "State: E";
							Time.timeScale = 1f;
							this.MyVocals.pitch = 1f;
							this.MyAudio.pitch = 1f;
							this.DisablePrompts();
							this.Timer = 0f;
							this.Path = 5;
							this.Phase++;
							this.MyAudio.clip = this.CombatSFX[this.Path];
							this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatE"].time;
							this.MyAudio.Play();
							this.MyVocals.clip = this.Vocals[this.Path];
							this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatE"].time;
							this.MyVocals.Play();
						}
					}
				}
				if (this.Yandere.CharacterAnimation["Yandere_CombatA"].time > this.Yandere.CharacterAnimation["Yandere_CombatA"].length)
				{
					this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatA"].time = 0f;
					this.Yandere.CharacterAnimation["Yandere_CombatA"].time = 0f;
					this.Label.text = "State: A";
					this.Strike = 0;
					this.Phase = 1;
					this.MyAudio.clip = this.CombatSFX[this.Path];
					this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
					this.MyAudio.Play();
					this.MyVocals.clip = this.Vocals[this.Path];
					this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
					this.MyVocals.Play();
				}
			}
		}
		else if (this.Path == 2)
		{
			if (this.Phase == 3)
			{
				if (this.Yandere.CharacterAnimation["Yandere_CombatB"].time > 1.833333f)
				{
					this.StartTime = this.Yandere.CharacterAnimation["Yandere_CombatB"].time - 1.833333f;
					this.ChooseButton();
					this.Slowdown();
					this.Phase++;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.Yandere.CharacterAnimation["Yandere_CombatB"].time > 2.166666f)
				{
					Time.timeScale = 1f;
					this.MyVocals.pitch = 1f;
					this.MyAudio.pitch = 1f;
					this.DisablePrompts();
					this.Phase++;
				}
				else if (this.Success)
				{
					this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatC"].time = this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatB"].time;
					this.Yandere.CharacterAnimation["Yandere_CombatC"].time = this.Yandere.CharacterAnimation["Yandere_CombatB"].time;
					this.Delinquent.CharacterAnimation.Play(this.Prefix + "Delinquent_CombatC");
					this.Yandere.CharacterAnimation.Play("Yandere_CombatC");
					this.Label.text = "State: C";
					Time.timeScale = 1f;
					this.MyVocals.pitch = 1f;
					this.MyAudio.pitch = 1f;
					this.DisablePrompts();
					this.Strike = 0;
					this.Path = 3;
					this.Phase++;
					this.MyAudio.clip = this.CombatSFX[this.Path];
					this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatC"].time;
					this.MyAudio.Play();
					this.MyVocals.clip = this.Vocals[this.Path];
					this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatC"].time;
					this.MyVocals.Play();
				}
			}
			else if (this.Phase == 5)
			{
				if (this.Strike < 1 && this.Yandere.CharacterAnimation["Yandere_CombatB"].time > 2.66666f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, this.Delinquent.LeftHand.position, Quaternion.identity);
					this.Shake += this.ShakeFactor;
					this.Strike++;
				}
				if (this.Yandere.CharacterAnimation["Yandere_CombatB"].time > this.Yandere.CharacterAnimation["Yandere_CombatB"].length)
				{
					this.Delinquent.CharacterAnimation.CrossFade(this.Prefix + "Delinquent_CombatA");
					this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatA");
					this.Label.text = "State: A";
					this.Strike = 0;
					this.Phase = 1;
					this.Path = 1;
					this.MyAudio.clip = this.CombatSFX[this.Path];
					this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
					this.MyAudio.Play();
					this.MyVocals.clip = this.Vocals[this.Path];
					this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
					this.MyVocals.Play();
				}
			}
		}
		else if (this.Path == 3)
		{
			if (this.Phase == 5)
			{
				if (this.Strike < 1 && this.Yandere.CharacterAnimation["Yandere_CombatC"].time > 2.5f)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, this.Yandere.RightHand.position, Quaternion.identity);
					this.Shake += this.ShakeFactor;
					this.Strike++;
				}
				if (this.Yandere.CharacterAnimation["Yandere_CombatC"].time > 3.166666f)
				{
					this.StartTime = this.Yandere.CharacterAnimation["Yandere_CombatC"].time - 3.166666f;
					this.ChooseButton();
					this.Slowdown();
					this.Phase++;
				}
			}
			else if (this.Phase == 6)
			{
				if (this.Yandere.CharacterAnimation["Yandere_CombatC"].time > 3.5f)
				{
					this.DisablePrompts();
					Time.timeScale = 1f;
					this.MyVocals.pitch = 1f;
					this.MyAudio.pitch = 1f;
					this.Phase++;
				}
				else if (this.Success)
				{
					this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatD"].time = this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatC"].time;
					this.Yandere.CharacterAnimation["Yandere_CombatD"].time = this.Yandere.CharacterAnimation["Yandere_CombatC"].time;
					this.Delinquent.CharacterAnimation.Play(this.Prefix + "Delinquent_CombatD");
					this.Yandere.CharacterAnimation.Play("Yandere_CombatD");
					this.Label.text = "State: D";
					Time.timeScale = 1f;
					this.MyVocals.pitch = 1f;
					this.MyAudio.pitch = 1f;
					this.DisablePrompts();
					this.Strike = 0;
					this.Path = 4;
					this.Phase++;
					this.MyAudio.clip = this.CombatSFX[this.Path];
					this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatD"].time;
					this.MyAudio.Play();
					this.MyVocals.clip = this.Vocals[this.Path];
					this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatD"].time;
					this.MyVocals.Play();
				}
			}
			else if (this.Phase == 7 && this.Yandere.CharacterAnimation["Yandere_CombatC"].time > this.Yandere.CharacterAnimation["Yandere_CombatC"].length)
			{
				this.Delinquent.CharacterAnimation.CrossFade(this.Prefix + "Delinquent_CombatA");
				this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatA");
				this.Label.text = "State: A";
				this.Strike = 0;
				this.Phase = 1;
				this.Path = 1;
				this.MyAudio.clip = this.CombatSFX[this.Path];
				this.MyAudio.time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
				this.MyAudio.Play();
				this.MyVocals.clip = this.Vocals[this.Path];
				this.MyVocals.time = this.Yandere.CharacterAnimation["Yandere_CombatA"].time;
				this.MyVocals.Play();
			}
		}
		else if (this.Path == 4)
		{
			if (this.Phase == 7)
			{
				if (this.Strike < 1)
				{
					if (this.Yandere.CharacterAnimation["Yandere_CombatD"].time > 4f)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.HitEffect, this.Yandere.RightKnee.position, Quaternion.identity);
						if (!this.Delinquent.WitnessedMurder)
						{
							this.Delinquent.MyWeapon.transform.parent = null;
							this.Delinquent.MyWeapon.MyCollider.enabled = true;
							this.Delinquent.MyWeapon.MyCollider.isTrigger = false;
							this.Delinquent.MyWeapon.Prompt.enabled = true;
							this.Delinquent.IgnoreBlood = true;
							Rigidbody component = this.Delinquent.MyWeapon.GetComponent<Rigidbody>();
							component.constraints = RigidbodyConstraints.None;
							component.isKinematic = false;
							component.useGravity = true;
							if (!this.Practice)
							{
								this.Delinquent.MyWeapon = null;
							}
						}
						this.Shake += this.ShakeFactor;
						this.Strike++;
					}
				}
				else if (this.Yandere.CharacterAnimation["Yandere_CombatD"].time > 5.5f)
				{
					this.MainCamera.transform.parent = null;
					this.Strength += Time.deltaTime;
					this.MainCamera.transform.position = Vector3.Lerp(this.MainCamera.transform.position, this.StartPoint, Time.deltaTime * this.Strength);
					this.RedVignette.color = Vector4.Lerp(this.RedVignette.color, new Vector4(1f, 0f, 0f, 0f), Time.deltaTime * this.Strength);
					this.Zoom = false;
				}
				if (this.Yandere.CharacterAnimation["Yandere_CombatD"].time > this.Yandere.CharacterAnimation["Yandere_CombatD"].length)
				{
					if (this.Delinquent.WitnessedMurder)
					{
						this.Yandere.Subtitle.UpdateLabel(SubtitleType.DelinquentNoSurrender, 0, 5f);
						if (!this.Delinquent.WillChase)
						{
							this.Delinquent.WillChase = true;
							this.Yandere.Chasers++;
						}
					}
					else if (!this.Practice)
					{
						this.Yandere.Subtitle.UpdateLabel(SubtitleType.DelinquentSurrender, 0, 5f);
						this.Delinquent.Persona = PersonaType.Loner;
					}
					if (!this.Practice)
					{
						ScheduleBlock scheduleBlock = this.Delinquent.ScheduleBlocks[2];
						scheduleBlock.destination = "Sulk";
						scheduleBlock.action = "Sulk";
						ScheduleBlock scheduleBlock2 = this.Delinquent.ScheduleBlocks[4];
						scheduleBlock2.destination = "Sulk";
						scheduleBlock2.action = "Sulk";
						ScheduleBlock scheduleBlock3 = this.Delinquent.ScheduleBlocks[6];
						scheduleBlock3.destination = "Sulk";
						scheduleBlock3.action = "Sulk";
						ScheduleBlock scheduleBlock4 = this.Delinquent.ScheduleBlocks[7];
						scheduleBlock4.destination = "Sulk";
						scheduleBlock4.action = "Sulk";
						this.Delinquent.GetDestinations();
						this.Delinquent.CurrentDestination = this.Delinquent.Destinations[this.Delinquent.Phase];
						this.Delinquent.Pathfinding.target = this.Delinquent.Destinations[this.Delinquent.Phase];
						this.Delinquent.IdleAnim = "idleInjured_00";
						this.Delinquent.WalkAnim = "walkInjured_00";
						this.Delinquent.OriginalIdleAnim = this.Delinquent.IdleAnim;
						this.Delinquent.OriginalWalkAnim = this.Delinquent.WalkAnim;
						this.Delinquent.LeanAnim = this.Delinquent.IdleAnim;
						this.Delinquent.CharacterAnimation.CrossFade(this.Delinquent.IdleAnim);
						this.Delinquent.Threatened = true;
						this.Delinquent.Alarmed = true;
						this.Delinquent.Injured = true;
						this.Delinquent.Strength = 0;
						this.Delinquent.Defeats++;
					}
					else
					{
						this.Delinquent.Threatened = false;
						this.Delinquent.Alarmed = false;
						this.PracticeWindow.Finish();
						this.Yandere.Health = 10;
						this.Practice = false;
					}
					this.Delinquent.Fighting = false;
					this.Delinquent.enabled = true;
					this.Delinquent.Distracted = false;
					this.Delinquent.Shoving = false;
					this.Delinquent.Paired = false;
					this.Delinquent = null;
					this.ReleaseYandere();
					this.ResetValues();
					this.Yandere.StudentManager.UpdateStudents(0);
				}
			}
		}
		else if (this.Path == 5)
		{
			if (this.Phase == 4)
			{
				this.MainCamera.position = Vector3.Lerp(this.MainCamera.position, this.CameraTarget, Time.deltaTime);
				if (this.Yandere.CharacterAnimation["Yandere_CombatE"].time > this.Yandere.CharacterAnimation["Yandere_CombatE"].length)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 1f)
					{
						this.Yandere.ShoulderCamera.HeartbrokenCamera.SetActive(true);
						this.Yandere.ShoulderCamera.enabled = false;
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.Jukebox.GameOver();
						this.Yandere.enabled = false;
						this.Yandere.EmptyHands();
						this.Yandere.Lost = true;
						this.Phase++;
					}
				}
			}
		}
		else if (this.Path == 6)
		{
			if (this.Phase == 4)
			{
				if (this.Yandere.CharacterAnimation["Yandere_CombatF"].time > 6.33333f)
				{
					this.MainCamera.transform.parent = null;
					this.Strength += Time.deltaTime;
					this.MainCamera.transform.position = Vector3.Lerp(this.MainCamera.transform.position, this.StartPoint, Time.deltaTime * this.Strength);
					this.RedVignette.color = Vector4.Lerp(this.RedVignette.color, new Vector4(1f, 0f, 0f, 0f), Time.deltaTime * this.Strength);
					this.Zoom = false;
				}
				if (this.Delinquent.CharacterAnimation[this.Prefix + "Delinquent_CombatF"].time > 7.83333f)
				{
					this.Delinquent.MyWeapon.transform.parent = this.Delinquent.WeaponBagParent;
					this.Delinquent.MyWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.Delinquent.MyWeapon.transform.localPosition = new Vector3(0f, 0f, 0f);
				}
				if (this.Yandere.CharacterAnimation["Yandere_CombatF"].time > this.Yandere.CharacterAnimation["Yandere_CombatF"].length)
				{
					if (!this.Practice)
					{
						this.Yandere.Subtitle.UpdateLabel(SubtitleType.DelinquentWin, 0, 5f);
						this.Yandere.IdleAnim = "f02_idleInjured_00";
						this.Yandere.WalkAnim = "f02_walkInjured_00";
						this.Yandere.OriginalIdleAnim = this.Yandere.IdleAnim;
						this.Yandere.OriginalWalkAnim = this.Yandere.WalkAnim;
						this.Yandere.StudentManager.Rest.Prompt.enabled = true;
					}
					else
					{
						this.PracticeWindow.Finish();
						this.Yandere.Health = 10;
						this.Practice = false;
					}
					this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
					this.Yandere.DelinquentFighting = false;
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.CannotRecover = false;
					this.Yandere.CanMove = true;
					this.Yandere.Chased = false;
					this.Delinquent.Threatened = false;
					this.Delinquent.Fighting = false;
					this.Delinquent.Injured = false;
					this.Delinquent.Alarmed = false;
					this.Delinquent.Routine = true;
					this.Delinquent.enabled = true;
					this.Delinquent.Distracted = false;
					this.Delinquent.Shoving = false;
					this.Delinquent.Paired = false;
					this.Delinquent.Patience = 5;
					this.ResetValues();
					this.Yandere.StudentManager.UpdateStudents(0);
				}
			}
		}
		else if (this.Path == 7)
		{
			if (this.Yandere.CharacterAnimation["f02_stopFighting_00"].time > 1f)
			{
				this.MainCamera.transform.parent = null;
				this.Strength += Time.deltaTime;
				this.MainCamera.transform.position = Vector3.Lerp(this.MainCamera.transform.position, this.StartPoint, Time.deltaTime * this.Strength);
				this.RedVignette.color = Vector4.Lerp(this.RedVignette.color, new Vector4(1f, 0f, 0f, 0f), Time.deltaTime * this.Strength);
				this.Zoom = false;
			}
			if (this.Delinquent.CharacterAnimation["stopFighting_00"].time > 3.83333f)
			{
				this.Delinquent.MyWeapon.transform.parent = this.Delinquent.WeaponBagParent;
				this.Delinquent.MyWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.Delinquent.MyWeapon.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
			if (this.Yandere.CharacterAnimation["f02_stopFighting_00"].time > this.Yandere.CharacterAnimation["f02_stopFighting_00"].length)
			{
				this.Delinquent.GetDestinations();
				this.Delinquent.CurrentDestination = this.Delinquent.Destinations[this.Delinquent.Phase];
				this.Delinquent.Pathfinding.target = this.Delinquent.Destinations[this.Delinquent.Phase];
				this.ReleaseYandere();
				this.Delinquent.Threatened = false;
				this.Delinquent.Fighting = false;
				this.Delinquent.Alarmed = false;
				this.Delinquent.enabled = true;
				this.Delinquent.Distracted = false;
				this.Delinquent.Shoving = false;
				this.Delinquent.Paired = false;
				this.Delinquent.Routine = true;
				this.Delinquent.Patience = 5;
				this.Delinquent = null;
				this.DisablePrompts();
				this.ResetValues();
				this.Yandere.StudentManager.UpdateStudents(0);
			}
		}
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000C6A04 File Offset: 0x000C4E04
	private void Slowdown()
	{
		Time.timeScale = this.SlowdownFactor * this.Difficulty;
		this.MyVocals.pitch = this.SlowdownFactor * this.Difficulty;
		this.MyAudio.pitch = this.SlowdownFactor * this.Difficulty;
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x000C6A54 File Offset: 0x000C4E54
	private void ChooseButton()
	{
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		int buttonID = this.ButtonID;
		while (this.ButtonID == buttonID)
		{
			this.ButtonID = UnityEngine.Random.Range(1, 5);
		}
		if (this.ButtonID == 1)
		{
			this.CurrentButton = "A";
		}
		else if (this.ButtonID == 2)
		{
			this.CurrentButton = "B";
		}
		else if (this.ButtonID == 3)
		{
			this.CurrentButton = "X";
		}
		else if (this.ButtonID == 4)
		{
			this.CurrentButton = "Y";
		}
		this.ButtonPrompts[this.ButtonID].enabled = true;
		this.Circle.enabled = true;
		this.BG.enabled = true;
		this.Timer = this.StartTime;
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000C6B60 File Offset: 0x000C4F60
	public void DisablePrompts()
	{
		this.ButtonPrompts[1].enabled = false;
		this.ButtonPrompts[2].enabled = false;
		this.ButtonPrompts[3].enabled = false;
		this.ButtonPrompts[4].enabled = false;
		this.Circle.fillAmount = 1f;
		this.Circle.enabled = false;
		this.BG.enabled = false;
		this.Success = false;
		this.ButtonID = 0;
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000C6BDC File Offset: 0x000C4FDC
	private void AdjustMidpoint()
	{
		if (this.Strength == 0f)
		{
			if (!this.KnockedOut)
			{
				this.Midpoint.position = (this.Delinquent.Hips.position - this.Yandere.Hips.position) * 0.5f + this.Yandere.Hips.position;
				this.Midpoint.position += new Vector3(0f, 0.25f, 0f);
			}
			else
			{
				this.Midpoint.position = Vector3.Lerp(this.Midpoint.position, this.Yandere.Hips.position + new Vector3(0f, 0.5f, 0f), Time.deltaTime);
			}
		}
		else
		{
			this.Midpoint.position = Vector3.Lerp(this.Midpoint.position, this.Yandere.RPGCamera.cameraPivot.position, Time.deltaTime * this.Strength);
		}
		this.MainCamera.LookAt(this.Midpoint.position);
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x000C6D22 File Offset: 0x000C5122
	public void Stop()
	{
		if (this.Delinquent != null)
		{
			this.Delinquent.CharacterAnimation.CrossFade("delinquentCombatIdle_00");
			this.ResetValues();
			base.enabled = false;
		}
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x000C6D58 File Offset: 0x000C5158
	public void ResetValues()
	{
		this.Label.text = "State: A";
		this.Strength = 0f;
		this.Strike = 0;
		this.Phase = 0;
		this.Path = 0;
		this.MyAudio.clip = this.CombatSFX[this.Path];
		this.MyAudio.time = 0f;
		this.MyAudio.Stop();
		this.MyVocals.clip = this.Vocals[this.Path];
		this.MyVocals.time = 0f;
		this.MyVocals.Stop();
		this.Delinquent = null;
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000C6E04 File Offset: 0x000C5204
	public void ReleaseYandere()
	{
		Debug.Log("Yandere-chan has been released from combat.");
		this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
		this.Yandere.DelinquentFighting = false;
		this.Yandere.RPGCamera.enabled = true;
		this.Yandere.CannotRecover = false;
		this.Yandere.CanMove = true;
		this.Yandere.Chased = false;
	}

	// Token: 0x040018CC RID: 6348
	public UISprite[] ButtonPrompts;

	// Token: 0x040018CD RID: 6349
	public UISprite Circle;

	// Token: 0x040018CE RID: 6350
	public UISprite BG;

	// Token: 0x040018CF RID: 6351
	public GameObject HitEffect;

	// Token: 0x040018D0 RID: 6352
	public PracticeWindowScript PracticeWindow;

	// Token: 0x040018D1 RID: 6353
	public StudentScript Delinquent;

	// Token: 0x040018D2 RID: 6354
	public YandereScript Yandere;

	// Token: 0x040018D3 RID: 6355
	public Transform CombatTarget;

	// Token: 0x040018D4 RID: 6356
	public Transform MainCamera;

	// Token: 0x040018D5 RID: 6357
	public Transform Midpoint;

	// Token: 0x040018D6 RID: 6358
	public Vector3 CameraTarget;

	// Token: 0x040018D7 RID: 6359
	public Vector3 CameraStart;

	// Token: 0x040018D8 RID: 6360
	public Vector3 StartPoint;

	// Token: 0x040018D9 RID: 6361
	public UITexture RedVignette;

	// Token: 0x040018DA RID: 6362
	public UILabel Label;

	// Token: 0x040018DB RID: 6363
	public string CurrentButton;

	// Token: 0x040018DC RID: 6364
	public float SlowdownFactor;

	// Token: 0x040018DD RID: 6365
	public float ShakeFactor;

	// Token: 0x040018DE RID: 6366
	public float Difficulty;

	// Token: 0x040018DF RID: 6367
	public float StartTime;

	// Token: 0x040018E0 RID: 6368
	public float Strength;

	// Token: 0x040018E1 RID: 6369
	public float Shake;

	// Token: 0x040018E2 RID: 6370
	public float Timer;

	// Token: 0x040018E3 RID: 6371
	public bool KnockedOut;

	// Token: 0x040018E4 RID: 6372
	public bool Practice;

	// Token: 0x040018E5 RID: 6373
	public bool Success;

	// Token: 0x040018E6 RID: 6374
	public bool Zoom;

	// Token: 0x040018E7 RID: 6375
	public string Prefix;

	// Token: 0x040018E8 RID: 6376
	public int ButtonID;

	// Token: 0x040018E9 RID: 6377
	public int Strike;

	// Token: 0x040018EA RID: 6378
	public int Phase;

	// Token: 0x040018EB RID: 6379
	public int Path;

	// Token: 0x040018EC RID: 6380
	public AudioSource MyVocals;

	// Token: 0x040018ED RID: 6381
	public AudioSource MyAudio;

	// Token: 0x040018EE RID: 6382
	public AudioClip[] CombatSFX;

	// Token: 0x040018EF RID: 6383
	public AudioClip[] Vocals;
}
