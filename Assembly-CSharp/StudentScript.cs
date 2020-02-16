using System;
using System.Collections.Generic;
using FIMSpace.FLook;
using Pathfinding;
using UnityEngine;

// Token: 0x02000536 RID: 1334
public class StudentScript : MonoBehaviour
{
	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x060020CD RID: 8397 RVA: 0x0015E096 File Offset: 0x0015C496
	public bool Alive
	{
		get
		{
			return this.DeathType == DeathType.None;
		}
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x0015E0A4 File Offset: 0x0015C4A4
	public void Start()
	{
		this.CounterAnim = "f02_teacherCounterB_00";
		if (!this.Started)
		{
			this.CharacterAnimation = this.Character.GetComponent<Animation>();
			this.MyBento = this.Bento.GetComponent<GenericBentoScript>();
			this.Pathfinding.repathRate += (float)this.StudentID * 0.01f;
			this.OriginalIdleAnim = this.IdleAnim;
			this.OriginalLeanAnim = this.LeanAnim;
			if (!this.StudentManager.LoveSick && SchoolAtmosphere.Type == SchoolAtmosphereType.Low && this.Club <= ClubType.Gaming)
			{
				this.IdleAnim = this.ParanoidAnim;
			}
			if (ClubGlobals.Club == ClubType.Occult)
			{
				this.Perception = 0.5f;
			}
			this.Hearts.emission.enabled = false;
			this.Prompt.OwnerType = PromptOwnerType.Person;
			this.Paranoia = 2f - SchoolGlobals.SchoolAtmosphere;
			this.VisionDistance = ((PlayerGlobals.PantiesEquipped != 4) ? 10f : 5f) * this.Paranoia;
			if (GameObject.Find("DetectionCamera") != null)
			{
				this.SpawnDetectionMarker();
			}
			StudentJson studentJson = this.JSON.Students[this.StudentID];
			this.ScheduleBlocks = studentJson.ScheduleBlocks;
			this.Persona = studentJson.Persona;
			this.Class = studentJson.Class;
			this.Crush = studentJson.Crush;
			this.Club = studentJson.Club;
			this.BreastSize = studentJson.BreastSize;
			this.Strength = studentJson.Strength;
			this.Hairstyle = studentJson.Hairstyle;
			this.Accessory = studentJson.Accessory;
			this.Name = studentJson.Name;
			this.OriginalClub = this.Club;
			if (StudentGlobals.GetStudentBroken(this.StudentID))
			{
				this.Cosmetic.RightEyeRenderer.gameObject.SetActive(false);
				this.Cosmetic.LeftEyeRenderer.gameObject.SetActive(false);
				this.Cosmetic.RightIrisLight.SetActive(false);
				this.Cosmetic.LeftIrisLight.SetActive(false);
				this.RightEmptyEye.SetActive(true);
				this.LeftEmptyEye.SetActive(true);
				this.Shy = true;
				this.Persona = PersonaType.Coward;
			}
			if (this.Name == "Random")
			{
				this.Persona = (PersonaType)UnityEngine.Random.Range(1, 8);
				if (this.Persona == PersonaType.Lovestruck)
				{
					this.Persona = PersonaType.PhoneAddict;
				}
				studentJson.Persona = this.Persona;
				if (this.Persona == PersonaType.Heroic)
				{
					this.Strength = UnityEngine.Random.Range(1, 5);
					studentJson.Strength = this.Strength;
				}
			}
			this.Seat = this.StudentManager.Seats[this.Class].List[studentJson.Seat];
			base.gameObject.name = string.Concat(new string[]
			{
				"Student_",
				this.StudentID.ToString(),
				" (",
				this.Name,
				")"
			});
			this.OriginalPersona = this.Persona;
			if (this.StudentID == 81 && StudentGlobals.GetStudentBroken(81))
			{
				this.Persona = PersonaType.Coward;
			}
			if (this.Persona == PersonaType.Loner || this.Persona == PersonaType.Coward || this.Persona == PersonaType.Fragile)
			{
				this.CameraAnims = this.CowardAnims;
			}
			else if (this.Persona == PersonaType.TeachersPet || this.Persona == PersonaType.Heroic || this.Persona == PersonaType.Strict)
			{
				this.CameraAnims = this.HeroAnims;
			}
			else if (this.Persona == PersonaType.Evil || this.Persona == PersonaType.Spiteful || this.Persona == PersonaType.Violent)
			{
				this.CameraAnims = this.EvilAnims;
			}
			else if (this.Persona == PersonaType.SocialButterfly || this.Persona == PersonaType.Lovestruck || this.Persona == PersonaType.PhoneAddict || this.Persona == PersonaType.Sleuth)
			{
				this.CameraAnims = this.SocialAnims;
			}
			if (ClubGlobals.GetClubClosed(this.Club))
			{
				this.Club = ClubType.None;
			}
			this.DialogueWheel = GameObject.Find("DialogueWheel").GetComponent<DialogueWheelScript>();
			this.ClubManager = GameObject.Find("ClubManager").GetComponent<ClubManagerScript>();
			this.Reputation = GameObject.Find("Reputation").GetComponent<ReputationScript>();
			this.Yandere = GameObject.Find("YandereChan").GetComponent<YandereScript>();
			this.Police = GameObject.Find("Police").GetComponent<PoliceScript>();
			this.Clock = GameObject.Find("Clock").GetComponent<ClockScript>();
			this.Subtitle = this.Yandere.Subtitle;
			this.CameraEffects = this.Yandere.MainCamera.GetComponent<CameraEffectsScript>();
			this.RightEyeOrigin = this.RightEye.localPosition;
			this.LeftEyeOrigin = this.LeftEye.localPosition;
			this.Bento.GetComponent<GenericBentoScript>().StudentID = this.StudentID;
			this.HealthBar.transform.parent.gameObject.SetActive(false);
			this.VomitEmitter.gameObject.SetActive(false);
			this.ChaseCamera.gameObject.SetActive(false);
			this.Countdown.gameObject.SetActive(false);
			this.MiyukiGameScreen.SetActive(false);
			this.Chopsticks[0].SetActive(false);
			this.Chopsticks[1].SetActive(false);
			this.Sketchbook.SetActive(false);
			this.SmartPhone.SetActive(false);
			this.OccultBook.SetActive(false);
			this.Paintbrush.SetActive(false);
			this.EventBook.SetActive(false);
			this.Handcuffs.SetActive(false);
			this.Scrubber.SetActive(false);
			this.Octodog.SetActive(false);
			this.Palette.SetActive(false);
			this.Eraser.SetActive(false);
			this.Pencil.SetActive(false);
			this.Bento.SetActive(false);
			this.Pen.SetActive(false);
			this.SpeechLines.Stop();
			foreach (GameObject gameObject in this.ScienceProps)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			foreach (GameObject gameObject2 in this.Fingerfood)
			{
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			this.OriginalOriginalWalkAnim = this.WalkAnim;
			this.OriginalSprintAnim = this.SprintAnim;
			this.OriginalWalkAnim = this.WalkAnim;
			if (this.Persona == PersonaType.Evil)
			{
				this.ScaredAnim = this.EvilWitnessAnim;
			}
			if (this.Persona == PersonaType.PhoneAddict)
			{
				this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
				this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 165f);
				this.Countdown.Speed = 0.1f;
				this.SprintAnim = this.PhoneAnims[2];
				this.PatrolAnim = this.PhoneAnims[3];
			}
			if (this.Club == ClubType.Bully)
			{
				if (!StudentGlobals.GetStudentBroken(this.StudentID))
				{
					this.IdleAnim = this.PhoneAnims[0];
					this.BullyID = this.StudentID - 80;
				}
				if (TaskGlobals.GetTaskStatus(36) == 3 && !SchoolGlobals.ReactedToGameLeader)
				{
					this.StudentManager.ReactedToGameLeader = true;
					ScheduleBlock scheduleBlock = this.ScheduleBlocks[4];
					scheduleBlock.destination = "Shock";
					scheduleBlock.action = "Shock";
				}
			}
			if (!this.Male)
			{
				this.SkirtOrigins[0] = this.Skirt[0].transform.localPosition;
				this.SkirtOrigins[1] = this.Skirt[1].transform.localPosition;
				this.SkirtOrigins[2] = this.Skirt[2].transform.localPosition;
				this.SkirtOrigins[3] = this.Skirt[3].transform.localPosition;
				this.InstrumentBag[1].SetActive(false);
				this.InstrumentBag[2].SetActive(false);
				this.InstrumentBag[3].SetActive(false);
				this.InstrumentBag[4].SetActive(false);
				this.InstrumentBag[5].SetActive(false);
				this.PickRandomGossipAnim();
				this.DramaticCamera.gameObject.SetActive(false);
				this.AnimatedBook.SetActive(false);
				this.Handkerchief.SetActive(false);
				this.PepperSpray.SetActive(false);
				this.WateringCan.SetActive(false);
				this.Sketchbook.SetActive(false);
				this.Cigarette.SetActive(false);
				this.CandyBar.SetActive(false);
				this.Lighter.SetActive(false);
				foreach (GameObject gameObject3 in this.Instruments)
				{
					if (gameObject3 != null)
					{
						gameObject3.SetActive(false);
					}
				}
				this.Drumsticks[0].SetActive(false);
				this.Drumsticks[1].SetActive(false);
				if (this.Club >= ClubType.Teacher)
				{
					this.BecomeTeacher();
				}
				if (this.StudentManager.Censor && !this.Teacher)
				{
					this.Cosmetic.CensorPanties();
				}
				this.DisableEffects();
			}
			else
			{
				this.RandomCheerAnim = this.CheerAnims[UnityEngine.Random.Range(0, this.CheerAnims.Length)];
				this.MapMarker.gameObject.SetActive(false);
				this.DelinquentSpeechLines.Stop();
				this.PinkSeifuku.SetActive(false);
				this.WeaponBag.SetActive(false);
				this.Earpiece.SetActive(false);
				this.SetSplashes(false);
				foreach (ParticleSystem particleSystem in this.LiquidEmitters)
				{
					particleSystem.gameObject.SetActive(false);
				}
			}
			if (this.StudentID == 1)
			{
				this.MapMarker.gameObject.SetActive(true);
			}
			else if (this.StudentID == 2)
			{
				if (StudentGlobals.GetStudentDead(3) || StudentGlobals.GetStudentKidnapped(3) || this.StudentManager.Students[3].Slave)
				{
					ScheduleBlock scheduleBlock2 = this.ScheduleBlocks[2];
					scheduleBlock2.destination = "Mourn";
					scheduleBlock2.action = "Mourn";
					this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
				}
			}
			else if (this.StudentID == 3)
			{
				if (StudentGlobals.GetStudentDead(2) || StudentGlobals.GetStudentKidnapped(2) || this.StudentManager.Students[2] == null || (this.StudentManager.Students[2] != null && this.StudentManager.Students[2].Slave))
				{
					ScheduleBlock scheduleBlock3 = this.ScheduleBlocks[2];
					scheduleBlock3.destination = "Mourn";
					scheduleBlock3.action = "Mourn";
					this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
				}
			}
			else if (this.StudentID == 4)
			{
				this.IdleAnim = "f02_idleShort_00";
				this.WalkAnim = "f02_newWalk_00";
			}
			else if (this.StudentID == 5)
			{
				this.LongSkirt = true;
				this.Shy = true;
			}
			else if (this.StudentID == 6)
			{
				this.RelaxAnim = "crossarms_00";
				this.CameraAnims = this.HeroAnims;
			}
			else if (this.StudentID == 7)
			{
				this.RunAnim = "runFeminine_00";
				this.SprintAnim = "runFeminine_00";
				this.RelaxAnim = "infirmaryRest_00";
				this.OriginalSprintAnim = this.SprintAnim;
				this.Cosmetic.Start();
				base.gameObject.SetActive(false);
			}
			else if (this.StudentID == 8)
			{
				this.IdleAnim = this.BulliedIdleAnim;
				this.WalkAnim = this.BulliedWalkAnim;
			}
			else if (this.StudentID == 9)
			{
				this.IdleAnim = "idleScholarly_00";
				this.WalkAnim = "walkScholarly_00";
				this.CameraAnims = this.HeroAnims;
			}
			else if (this.StudentID == 10)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (this.StudentID == 11)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else if (this.StudentID == 24 && this.StudentID == 25)
			{
				this.IdleAnim = "f02_idleGirly_00";
				this.WalkAnim = "f02_walkGirly_00";
			}
			else if (this.StudentID == 26)
			{
				this.IdleAnim = "idleHaughty_00";
				this.WalkAnim = "walkHaughty_00";
			}
			else if (this.StudentID == 28 || this.StudentID == 30)
			{
				if (this.StudentID == 30)
				{
					this.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.KokonaPhoneTexture;
				}
			}
			else if (this.StudentID == 31)
			{
				this.IdleAnim = this.BulliedIdleAnim;
				this.WalkAnim = this.BulliedWalkAnim;
			}
			else if (this.StudentID == 34 || this.StudentID == 35)
			{
				this.IdleAnim = "f02_idleShort_00";
				this.WalkAnim = "f02_newWalk_00";
				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			else if (this.StudentID == 36)
			{
				if (TaskGlobals.GetTaskStatus(36) < 3)
				{
					this.IdleAnim = "slouchIdle_00";
					this.WalkAnim = "slouchWalk_00";
					this.ClubAnim = "slouchGaming_00";
				}
				else
				{
					this.IdleAnim = "properIdle_00";
					this.WalkAnim = "properWalk_00";
					this.ClubAnim = "properGaming_00";
				}
				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			else if (this.StudentID == 39)
			{
				this.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.MidoriPhoneTexture;
				this.PatrolAnim = "f02_midoriTexting_00";
			}
			else if (this.StudentID == 51)
			{
				this.IdleAnim = "f02_idleConfident_01";
				this.WalkAnim = "f02_walkConfident_01";
				if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
				{
					this.IdleAnim = this.BulliedIdleAnim;
					this.WalkAnim = this.BulliedWalkAnim;
					this.CameraAnims = this.CowardAnims;
					this.Persona = PersonaType.Loner;
					if (!SchoolGlobals.RoofFence)
					{
						ScheduleBlock scheduleBlock4 = this.ScheduleBlocks[2];
						scheduleBlock4.destination = "Sulk";
						scheduleBlock4.action = "Sulk";
						ScheduleBlock scheduleBlock5 = this.ScheduleBlocks[4];
						scheduleBlock5.destination = "Sulk";
						scheduleBlock5.action = "Sulk";
						ScheduleBlock scheduleBlock6 = this.ScheduleBlocks[7];
						scheduleBlock6.destination = "Sulk";
						scheduleBlock6.action = "Sulk";
						ScheduleBlock scheduleBlock7 = this.ScheduleBlocks[8];
						scheduleBlock7.destination = "Sulk";
						scheduleBlock7.action = "Sulk";
					}
					else
					{
						ScheduleBlock scheduleBlock8 = this.ScheduleBlocks[2];
						scheduleBlock8.destination = "Seat";
						scheduleBlock8.action = "Sit";
						ScheduleBlock scheduleBlock9 = this.ScheduleBlocks[4];
						scheduleBlock9.destination = "Seat";
						scheduleBlock9.action = "Sit";
						ScheduleBlock scheduleBlock10 = this.ScheduleBlocks[7];
						scheduleBlock10.destination = "Seat";
						scheduleBlock10.action = "Sit";
						ScheduleBlock scheduleBlock11 = this.ScheduleBlocks[8];
						scheduleBlock11.destination = "Seat";
						scheduleBlock11.action = "Sit";
					}
				}
			}
			else if (this.StudentID == 56)
			{
				this.IdleAnim = "idleConfident_00";
				this.WalkAnim = "walkConfident_00";
				this.SleuthID = 0;
			}
			else if (this.StudentID == 57)
			{
				this.IdleAnim = "idleChill_01";
				this.WalkAnim = "walkChill_01";
				this.SleuthID = 20;
			}
			else if (this.StudentID == 58)
			{
				this.IdleAnim = "idleChill_00";
				this.WalkAnim = "walkChill_00";
				this.SleuthID = 40;
			}
			else if (this.StudentID == 59)
			{
				this.IdleAnim = "f02_idleGraceful_00";
				this.WalkAnim = "f02_walkGraceful_00";
				this.SleuthID = 60;
			}
			else if (this.StudentID == 60)
			{
				this.IdleAnim = "f02_idleScholarly_00";
				this.WalkAnim = "f02_walkScholarly_00";
				this.CameraAnims = this.HeroAnims;
				this.SleuthID = 80;
			}
			else if (this.StudentID == 61)
			{
				this.IdleAnim = "scienceIdle_00";
				this.WalkAnim = "scienceWalk_00";
				this.OriginalWalkAnim = "scienceWalk_00";
			}
			else if (this.StudentID == 62)
			{
				this.IdleAnim = "idleFrown_00";
				this.WalkAnim = "walkFrown_00";
				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			else if (this.StudentID == 64 || this.StudentID == 65)
			{
				this.IdleAnim = "f02_idleShort_00";
				this.WalkAnim = "f02_newWalk_00";
				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			else if (this.StudentID == 66)
			{
				this.IdleAnim = "pose_03";
				this.OriginalWalkAnim = "walkConfident_00";
				this.WalkAnim = "walkConfident_00";
				this.ClubThreshold = 100f;
			}
			else if (this.StudentID == 69)
			{
				this.IdleAnim = "idleFrown_00";
				this.WalkAnim = "walkFrown_00";
				if (this.Paranoia > 1.66666f)
				{
					this.IdleAnim = this.ParanoidAnim;
					this.WalkAnim = this.ParanoidWalkAnim;
				}
			}
			else if (this.StudentID == 71)
			{
				this.IdleAnim = "f02_idleGirly_00";
				this.WalkAnim = "f02_walkGirly_00";
			}
			this.OriginalWalkAnim = this.WalkAnim;
			if (StudentGlobals.GetStudentGrudge(this.StudentID))
			{
				if (this.Persona != PersonaType.Coward && this.Persona != PersonaType.Evil)
				{
					this.CameraAnims = this.EvilAnims;
					this.Reputation.PendingRep -= 10f;
					this.PendingRep -= 10f;
					this.ID = 0;
					while (this.ID < this.Outlines.Length)
					{
						this.Outlines[this.ID].color = new Color(1f, 1f, 0f, 1f);
						this.ID++;
					}
				}
				this.Grudge = true;
				this.CameraAnims = this.EvilAnims;
			}
			if (this.Persona == PersonaType.Sleuth)
			{
				if (SchoolGlobals.SchoolAtmosphere <= 0.8f || this.Grudge)
				{
					this.Indoors = true;
					this.Spawned = true;
					if (this.ShoeRemoval.Locker == null)
					{
						this.ShoeRemoval.Start();
					}
					this.ShoeRemoval.PutOnShoes();
					this.SprintAnim = this.SleuthSprintAnim;
					this.IdleAnim = this.SleuthIdleAnim;
					this.WalkAnim = this.SleuthWalkAnim;
					this.CameraAnims = this.HeroAnims;
					this.SmartPhone.SetActive(true);
					this.Countdown.Speed = 0.075f;
					this.Sleuthing = true;
					if (this.Male)
					{
						this.SmartPhone.transform.localPosition = new Vector3(0.06f, -0.02f, -0.02f);
					}
					else
					{
						this.SmartPhone.transform.localPosition = new Vector3(0.033333f, -0.015f, -0.015f);
					}
					this.SmartPhone.transform.localEulerAngles = new Vector3(12.5f, 120f, 180f);
					if (this.Club == ClubType.None)
					{
						this.StudentManager.SleuthPhase = 3;
						this.GetSleuthTarget();
					}
					else
					{
						this.SleuthTarget = this.StudentManager.Clubs.List[this.StudentID];
					}
					if (!this.Grudge)
					{
						ScheduleBlock scheduleBlock12 = this.ScheduleBlocks[2];
						scheduleBlock12.destination = "Sleuth";
						scheduleBlock12.action = "Sleuth";
						ScheduleBlock scheduleBlock13 = this.ScheduleBlocks[4];
						scheduleBlock13.destination = "Sleuth";
						scheduleBlock13.action = "Sleuth";
						ScheduleBlock scheduleBlock14 = this.ScheduleBlocks[7];
						scheduleBlock14.destination = "Sleuth";
						scheduleBlock14.action = "Sleuth";
					}
					else
					{
						this.StalkTarget = this.Yandere.transform;
						this.SleuthTarget = this.Yandere.transform;
						ScheduleBlock scheduleBlock15 = this.ScheduleBlocks[2];
						scheduleBlock15.destination = "Stalk";
						scheduleBlock15.action = "Stalk";
						ScheduleBlock scheduleBlock16 = this.ScheduleBlocks[4];
						scheduleBlock16.destination = "Stalk";
						scheduleBlock16.action = "Stalk";
						ScheduleBlock scheduleBlock17 = this.ScheduleBlocks[7];
						scheduleBlock17.destination = "Stalk";
						scheduleBlock17.action = "Stalk";
					}
				}
				else if (SchoolGlobals.SchoolAtmosphere <= 0.9f)
				{
					this.WalkAnim = this.ParanoidWalkAnim;
					this.CameraAnims = this.HeroAnims;
				}
			}
			if (!this.Slave)
			{
				if (this.StudentManager.Bullies > 1)
				{
					if (this.StudentID == 81 || this.StudentID == 83 || this.StudentID == 85)
					{
						if (this.Persona != PersonaType.Coward)
						{
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							this.Paired = true;
							this.CharacterAnimation["f02_walkTalk_00"].time += (float)(this.StudentID - 81);
							this.WalkAnim = "f02_walkTalk_00";
							this.SpeechLines.Play();
						}
					}
					else if (this.StudentID == 82 || this.StudentID == 84)
					{
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Paired = true;
						this.CharacterAnimation["f02_walkTalk_01"].time += (float)(this.StudentID - 81);
						this.WalkAnim = "f02_walkTalk_01";
						this.SpeechLines.Play();
					}
				}
				if (this.Club == ClubType.Delinquent)
				{
					if (PlayerGlobals.GetStudentFriend(this.StudentID))
					{
						this.RespectEarned = true;
					}
					if (CounselorGlobals.WeaponsBanned == 0)
					{
						this.MyWeapon = this.Yandere.WeaponManager.DelinquentWeapons[this.StudentID - 75];
						this.MyWeapon.transform.parent = this.WeaponBagParent;
						this.MyWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
						this.MyWeapon.transform.localPosition = new Vector3(0f, 0f, 0f);
						this.MyWeapon.FingerprintID = this.StudentID;
						this.MyWeapon.MyCollider.enabled = false;
						this.WeaponBag.SetActive(true);
					}
					else
					{
						this.OriginalPersona = PersonaType.Heroic;
						this.Persona = PersonaType.Heroic;
					}
					this.ScaredAnim = "delinquentCombatIdle_00";
					this.LeanAnim = "delinquentConcern_00";
					this.ShoveAnim = "pushTough_00";
					this.WalkAnim = "walkTough_00";
					this.IdleAnim = "idleTough_00";
					this.SpeechLines = this.DelinquentSpeechLines;
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Paired = true;
					this.TaskAnims[0] = "delinquentTalk_04";
					this.TaskAnims[1] = "delinquentTalk_01";
					this.TaskAnims[2] = "delinquentTalk_02";
					this.TaskAnims[3] = "delinquentTalk_03";
					this.TaskAnims[4] = "delinquentTalk_00";
					this.TaskAnims[5] = "delinquentTalk_03";
				}
			}
			if (this.StudentID == this.StudentManager.RivalID)
			{
				this.RivalPrefix = "Rival ";
				if (DateGlobals.Weekday == DayOfWeek.Friday)
				{
					Debug.Log("It's Friday, and " + this.Name + " should leave a note in Senpai's locker ar 5:00 PM.");
					ScheduleBlock scheduleBlock18 = this.ScheduleBlocks[7];
					scheduleBlock18.time = 17f;
				}
			}
			if (!this.Teacher && this.Name != "Random")
			{
				this.StudentManager.CleaningManager.GetRole(this.StudentID);
				this.CleaningSpot = this.StudentManager.CleaningManager.Spot;
				this.CleaningRole = this.StudentManager.CleaningManager.Role;
			}
			if (this.Club == ClubType.Cooking)
			{
				this.SleuthID = (this.StudentID - 21) * 20;
				this.ClubAnim = this.PrepareFoodAnim;
				this.ClubMemberID = this.StudentID - 20;
				this.MyPlate = this.StudentManager.Plates[this.ClubMemberID];
				this.OriginalPlatePosition = this.MyPlate.position;
				this.OriginalPlateRotation = this.MyPlate.rotation;
				if (!GameGlobals.EmptyDemon)
				{
					this.ApronAttacher.enabled = true;
				}
			}
			else if (this.Club == ClubType.Drama)
			{
				if (this.StudentID == 26)
				{
					this.ClubAnim = "teaching_00";
				}
				else if (this.StudentID == 27 || this.StudentID == 28)
				{
					this.ClubAnim = "sit_00";
				}
				else if (this.StudentID == 29 || this.StudentID == 30)
				{
					this.ClubAnim = "f02_sit_00";
				}
				this.OriginalClubAnim = this.ClubAnim;
			}
			else if (this.Club == ClubType.Occult)
			{
				this.PatrolAnim = this.ThinkAnim;
				this.CharacterAnimation[this.PatrolAnim].speed = 0.2f;
			}
			else if (this.Club == ClubType.Gaming)
			{
				this.MiyukiGameScreen.SetActive(true);
				this.ClubMemberID = this.StudentID - 35;
				if (this.StudentID > 36)
				{
					this.ClubAnim = this.GameAnim;
				}
				this.ActivityAnim = this.GameAnim;
			}
			else if (this.Club == ClubType.Art)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[4];
				this.ActivityAnim = this.PaintAnim;
				this.Attacher.ArtClub = true;
				this.ClubAnim = this.PaintAnim;
				this.DressCode = true;
				if (DateGlobals.Weekday == DayOfWeek.Friday)
				{
					Debug.Log("It's Friday, so the art club is changing their club activity to painting the cherry tree.");
					ScheduleBlock scheduleBlock19 = this.ScheduleBlocks[7];
					scheduleBlock19.destination = "Paint";
					scheduleBlock19.action = "Paint";
				}
				this.ClubMemberID = this.StudentID - 40;
				this.Painting = this.StudentManager.FridayPaintings[this.ClubMemberID];
				this.GetDestinations();
			}
			else if (this.Club == ClubType.LightMusic)
			{
				this.ClubMemberID = this.StudentID - 50;
				this.InstrumentBag[this.ClubMemberID].SetActive(true);
				if (this.StudentID == 51)
				{
					this.ClubAnim = "f02_practiceGuitar_01";
				}
				else if (this.StudentID == 52 || this.StudentID == 53)
				{
					this.ClubAnim = "f02_practiceGuitar_00";
				}
				else if (this.StudentID == 54)
				{
					this.ClubAnim = "f02_practiceDrums_00";
					this.Instruments[4] = this.StudentManager.DrumSet;
				}
				else if (this.StudentID == 55)
				{
					this.ClubAnim = "f02_practiceKeytar_00";
				}
			}
			else if (this.Club == ClubType.MartialArts)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[6];
				this.DressCode = true;
				if (this.StudentID == 46)
				{
					this.IdleAnim = "pose_03";
					this.ClubAnim = "pose_03";
				}
				else if (this.StudentID == 47)
				{
					this.GetNewAnimation = true;
					this.ClubAnim = "idle_20";
					this.ActivityAnim = "kick_24";
				}
				else if (this.StudentID == 48)
				{
					this.ClubAnim = "sit_04";
					this.ActivityAnim = "kick_24";
				}
				else if (this.StudentID == 49)
				{
					this.GetNewAnimation = true;
					this.ClubAnim = "f02_idle_20";
					this.ActivityAnim = "f02_kick_23";
				}
				else if (this.StudentID == 50)
				{
					this.ClubAnim = "f02_sit_05";
					this.ActivityAnim = "f02_kick_23";
				}
				this.ClubMemberID = this.StudentID - 45;
			}
			else if (this.Club == ClubType.Science)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[8];
				this.Attacher.ScienceClub = true;
				this.DressCode = true;
				if (this.StudentID == 61)
				{
					this.ClubAnim = "scienceMad_00";
				}
				else if (this.StudentID == 62)
				{
					this.ClubAnim = "scienceTablet_00";
				}
				else if (this.StudentID == 63)
				{
					this.ClubAnim = "scienceChemistry_00";
				}
				else if (this.StudentID == 64)
				{
					this.ClubAnim = "f02_scienceLeg_00";
				}
				else if (this.StudentID == 65)
				{
					this.ClubAnim = "f02_scienceConsole_00";
				}
				this.ClubMemberID = this.StudentID - 60;
			}
			else if (this.Club == ClubType.Sports)
			{
				this.ChangingBooth = this.StudentManager.ChangingBooths[9];
				this.DressCode = true;
				this.ClubAnim = "stretch_00";
				this.ClubMemberID = this.StudentID - 65;
			}
			else if (this.Club == ClubType.Gardening)
			{
				if (this.StudentID == 71)
				{
					this.PatrolAnim = "f02_thinking_00";
					this.ClubAnim = "f02_thinking_00";
					this.CharacterAnimation[this.PatrolAnim].speed = 0.9f;
				}
				else
				{
					this.ClubAnim = "f02_waterPlant_00";
					this.WateringCan.SetActive(true);
				}
			}
			if (this.OriginalClub != ClubType.Gaming)
			{
				this.Miyuki.gameObject.SetActive(false);
			}
			if (this.Cosmetic.Hairstyle == 20)
			{
				this.IdleAnim = "f02_tsunIdle_00";
			}
			this.GetDestinations();
			this.OriginalActions = new StudentActionType[this.Actions.Length];
			Array.Copy(this.Actions, this.OriginalActions, this.Actions.Length);
			if (this.AoT)
			{
				this.AttackOnTitan();
			}
			if (this.Slave)
			{
				this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
				this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
				this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
				this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
				this.IdleAnim = this.BrokenAnim;
				this.WalkAnim = this.BrokenWalkAnim;
				this.RightEmptyEye.SetActive(true);
				this.LeftEmptyEye.SetActive(true);
				this.SmartPhone.SetActive(false);
				this.Distracted = true;
				this.Indoors = true;
				this.Safe = false;
				this.Shy = false;
				this.ID = 0;
				while (this.ID < this.ScheduleBlocks.Length)
				{
					this.ScheduleBlocks[this.ID].time = 0f;
					this.ID++;
				}
				if (this.FragileSlave)
				{
					this.HuntTarget = this.StudentManager.Students[StudentGlobals.GetFragileTarget()];
				}
				else
				{
					SchoolGlobals.KidnapVictim = 0;
				}
			}
			if (this.Spooky)
			{
				this.Spook();
			}
			this.Prompt.HideButton[0] = true;
			this.Prompt.HideButton[2] = true;
			this.ID = 0;
			while (this.ID < this.Ragdoll.AllRigidbodies.Length)
			{
				this.Ragdoll.AllRigidbodies[this.ID].isKinematic = true;
				this.Ragdoll.AllColliders[this.ID].enabled = false;
				this.ID++;
			}
			this.Ragdoll.AllColliders[10].enabled = true;
			if (this.StudentID == 1)
			{
				this.DetectionMarker.GetComponent<DetectionMarkerScript>().Tex.color = new Color(1f, 0f, 0f, 0f);
				this.Yandere.Senpai = base.transform;
				this.Yandere.LookAt.target = this.Head;
				this.ID = 0;
				while (this.ID < this.Outlines.Length)
				{
					if (this.Outlines[this.ID] != null)
					{
						this.Outlines[this.ID].enabled = true;
						this.Outlines[this.ID].color = new Color(1f, 0f, 1f);
					}
					this.ID++;
				}
				this.Prompt.ButtonActive[0] = false;
				this.Prompt.ButtonActive[1] = false;
				this.Prompt.ButtonActive[2] = false;
				this.Prompt.ButtonActive[3] = false;
				if (this.StudentManager.MissionMode || GameGlobals.SenpaiMourning)
				{
					base.transform.position = Vector3.zero;
					base.gameObject.SetActive(false);
				}
			}
			else if (!StudentGlobals.GetStudentPhotographed(this.StudentID))
			{
				this.ID = 0;
				while (this.ID < this.Outlines.Length)
				{
					if (this.Outlines[this.ID] != null)
					{
						this.Outlines[this.ID].enabled = false;
						this.Outlines[this.ID].color = new Color(0f, 1f, 0f);
					}
					this.ID++;
				}
			}
			this.PickRandomAnim();
			this.PickRandomSleuthAnim();
			Renderer component = this.Armband.GetComponent<Renderer>();
			this.Armband.SetActive(false);
			if (this.Club != ClubType.None && (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 || this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 || this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 || this.StudentID == 66 || this.StudentID == 71))
			{
				this.Armband.SetActive(true);
				if (GameGlobals.EmptyDemon)
				{
					this.IdleAnim = this.OriginalIdleAnim;
					this.WalkAnim = this.OriginalOriginalWalkAnim;
					this.OriginalPersona = PersonaType.Evil;
					this.Persona = PersonaType.Evil;
					this.ScaredAnim = this.EvilWitnessAnim;
				}
			}
			if (!this.Teacher)
			{
				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.Destinations[this.Phase];
			}
			else
			{
				this.Indoors = true;
			}
			if (this.StudentID == 1 || this.StudentID == 4 || this.StudentID == 5 || this.StudentID == 11)
			{
				this.BookRenderer.material.mainTexture = this.RedBookTexture;
			}
			this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			if (this.StudentManager.MissionMode && this.StudentID == MissionModeGlobals.MissionTarget)
			{
				this.ID = 0;
				while (this.ID < this.Outlines.Length)
				{
					this.Outlines[this.ID].enabled = true;
					this.Outlines[this.ID].color = new Color(1f, 0f, 0f);
					this.ID++;
				}
			}
			UnityEngine.Object.Destroy(this.MyRigidbody);
			this.Started = true;
			if (this.Club == ClubType.Council)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.64375f, 0f));
				this.Armband.SetActive(true);
				this.Indoors = true;
				this.Spawned = true;
				if (this.ShoeRemoval.Locker == null)
				{
					this.ShoeRemoval.Start();
				}
				this.ShoeRemoval.PutOnShoes();
				if (this.StudentID == 86)
				{
					this.Suffix = "Strict";
				}
				else if (this.StudentID == 87)
				{
					this.Suffix = "Casual";
				}
				else if (this.StudentID == 88)
				{
					this.Suffix = "Grace";
				}
				else if (this.StudentID == 89)
				{
					this.Suffix = "Edgy";
				}
				this.IdleAnim = "f02_idleCouncil" + this.Suffix + "_00";
				this.WalkAnim = "f02_walkCouncil" + this.Suffix + "_00";
				this.ShoveAnim = "f02_pushCouncil" + this.Suffix + "_00";
				this.PatrolAnim = "f02_scanCouncil" + this.Suffix + "_00";
				this.RelaxAnim = "f02_relaxCouncil" + this.Suffix + "_00";
				this.SprayAnim = "f02_sprayCouncil" + this.Suffix + "_00";
				this.BreakUpAnim = "f02_stopCouncil" + this.Suffix + "_00";
				this.PickUpAnim = "f02_teacherPickUp_00";
				this.ScaredAnim = this.ReadyToFightAnim;
				this.ParanoidAnim = this.GuardAnim;
				this.CameraAnims[1] = this.IdleAnim;
				this.CameraAnims[2] = this.IdleAnim;
				this.CameraAnims[3] = this.IdleAnim;
				this.ClubMemberID = this.StudentID - 85;
				this.VisionDistance *= 2f;
			}
			if (!this.StudentManager.NoClubMeeting && this.Armband.activeInHierarchy && this.Clock.Weekday == 5)
			{
				if (this.StudentID < 86)
				{
					ScheduleBlock scheduleBlock20 = this.ScheduleBlocks[6];
					scheduleBlock20.destination = "Meeting";
					scheduleBlock20.action = "Meeting";
				}
				else
				{
					ScheduleBlock scheduleBlock21 = this.ScheduleBlocks[5];
					scheduleBlock21.destination = "Meeting";
					scheduleBlock21.action = "Meeting";
				}
				this.GetDestinations();
			}
			if (this.StudentID == 81 && StudentGlobals.GetStudentBroken(81))
			{
				this.Destinations[2] = this.StudentManager.BrokenSpot;
				this.Destinations[4] = this.StudentManager.BrokenSpot;
				this.Actions[2] = StudentActionType.Shamed;
				this.Actions[4] = StudentActionType.Shamed;
			}
		}
		this.UpdateAnimLayers();
		if (!this.Male)
		{
			base.GetComponent<FLookAnimatorWEyes>().ObjectToFollow = this.Yandere.Head;
			base.GetComponent<FLookAnimatorWEyes>().EyesTarget = this.Yandere.Head;
			if (this.StudentID != 55)
			{
				this.LongHair[0] = null;
			}
		}
		if (this.StudentID > 9 && this.StudentID < 21)
		{
			Debug.Log("Destroying a character who should not exist.");
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x00160A14 File Offset: 0x0015EE14
	private float GetPerceptionPercent(float distance)
	{
		float num = Mathf.Clamp01(distance / this.VisionDistance);
		return 1f - num * num;
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00160A38 File Offset: 0x0015EE38
	private SubtitleType LostPhoneSubtitleType
	{
		get
		{
			if (this.RivalPrefix == string.Empty)
			{
				return SubtitleType.LostPhone;
			}
			if (this.RivalPrefix == "Rival ")
			{
				return SubtitleType.RivalLostPhone;
			}
			throw new NotImplementedException("\"" + this.RivalPrefix + "\" case not implemented.");
		}
	}

	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00160A94 File Offset: 0x0015EE94
	private SubtitleType PickpocketSubtitleType
	{
		get
		{
			if (this.RivalPrefix == string.Empty)
			{
				return SubtitleType.PickpocketReaction;
			}
			if (this.RivalPrefix == "Rival ")
			{
				return SubtitleType.RivalPickpocketReaction;
			}
			throw new NotImplementedException("\"" + this.RivalPrefix + "\" case not implemented.");
		}
	}

	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00160AF4 File Offset: 0x0015EEF4
	private SubtitleType SplashSubtitleType
	{
		get
		{
			if (this.RivalPrefix == string.Empty)
			{
				if (!this.Male)
				{
					return SubtitleType.SplashReaction;
				}
				return SubtitleType.SplashReactionMale;
			}
			else
			{
				if (this.RivalPrefix == "Rival ")
				{
					return SubtitleType.RivalSplashReaction;
				}
				throw new NotImplementedException("\"" + this.RivalPrefix + "\" case not implemented.");
			}
		}
	}

	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x060020D3 RID: 8403 RVA: 0x00160B64 File Offset: 0x0015EF64
	public SubtitleType TaskLineResponseType
	{
		get
		{
			Debug.Log("Student#" + this.StudentID + " has been asked to display a subtitle about their task.");
			if (this.StudentID == 6)
			{
				if (!false)
				{
					return SubtitleType.TaskGenericLine;
				}
				return SubtitleType.Task6Line;
			}
			else
			{
				if (this.StudentID == 8)
				{
					return SubtitleType.Task8Line;
				}
				if (this.StudentID == 11)
				{
					return SubtitleType.Task11Line;
				}
				if (this.StudentID == 13)
				{
					return SubtitleType.Task13Line;
				}
				if (this.StudentID == 14)
				{
					return SubtitleType.Task14Line;
				}
				if (this.StudentID == 15)
				{
					return SubtitleType.Task15Line;
				}
				if (this.StudentID == 25)
				{
					return SubtitleType.Task25Line;
				}
				if (this.StudentID == 28)
				{
					return SubtitleType.Task28Line;
				}
				if (this.StudentID == 30)
				{
					return SubtitleType.Task30Line;
				}
				if (this.StudentID == 36)
				{
					return SubtitleType.Task36Line;
				}
				if (this.StudentID == 37)
				{
					return SubtitleType.Task37Line;
				}
				if (this.StudentID == 38)
				{
					return SubtitleType.Task38Line;
				}
				if (this.StudentID == 52)
				{
					return SubtitleType.Task52Line;
				}
				if (this.StudentID == 76)
				{
					return SubtitleType.Task76Line;
				}
				if (this.StudentID == 77)
				{
					return SubtitleType.Task77Line;
				}
				if (this.StudentID == 78)
				{
					return SubtitleType.Task78Line;
				}
				if (this.StudentID == 79)
				{
					return SubtitleType.Task79Line;
				}
				if (this.StudentID == 80)
				{
					return SubtitleType.Task80Line;
				}
				if (this.StudentID == 81)
				{
					return SubtitleType.Task81Line;
				}
				return SubtitleType.TaskGenericLine;
			}
		}
	}

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00160D0C File Offset: 0x0015F10C
	public SubtitleType ClubInfoResponseType
	{
		get
		{
			if (GameGlobals.EmptyDemon)
			{
				return SubtitleType.ClubPlaceholderInfo;
			}
			if (this.Club == ClubType.Cooking)
			{
				return SubtitleType.ClubCookingInfo;
			}
			if (this.Club == ClubType.Drama)
			{
				return SubtitleType.ClubDramaInfo;
			}
			if (this.Club == ClubType.Occult)
			{
				return SubtitleType.ClubOccultInfo;
			}
			if (this.Club == ClubType.Art)
			{
				return SubtitleType.ClubArtInfo;
			}
			if (this.Club == ClubType.LightMusic)
			{
				return SubtitleType.ClubLightMusicInfo;
			}
			if (this.Club == ClubType.MartialArts)
			{
				return SubtitleType.ClubMartialArtsInfo;
			}
			if (this.Club == ClubType.Photography)
			{
				if (this.Sleuthing)
				{
					return SubtitleType.ClubPhotoInfoDark;
				}
				return SubtitleType.ClubPhotoInfoLight;
			}
			else
			{
				if (this.Club == ClubType.Science)
				{
					return SubtitleType.ClubScienceInfo;
				}
				if (this.Club == ClubType.Sports)
				{
					return SubtitleType.ClubSportsInfo;
				}
				if (this.Club == ClubType.Gardening)
				{
					return SubtitleType.ClubGardenInfo;
				}
				if (this.Club == ClubType.Gaming)
				{
					return SubtitleType.ClubGamingInfo;
				}
				if (this.Club == ClubType.Delinquent)
				{
					return SubtitleType.ClubDelinquentInfo;
				}
				return SubtitleType.ClubPlaceholderInfo;
			}
		}
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x00160DF0 File Offset: 0x0015F1F0
	private bool PointIsInFOV(Vector3 point)
	{
		Vector3 position = this.Eyes.transform.position;
		Vector3 to = point - position;
		float num = 90f;
		return Vector3.Angle(this.Head.transform.forward, to) <= num;
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x00160E38 File Offset: 0x0015F238
	public bool CanSeeObject(GameObject obj, Vector3 targetPoint, int[] layers, int mask)
	{
		Vector3 position = this.Eyes.transform.position;
		Vector3 vector = targetPoint - position;
		bool flag = this.PointIsInFOV(targetPoint);
		if (flag)
		{
			float num = this.VisionDistance * this.VisionDistance;
			bool flag2 = vector.sqrMagnitude <= num;
			if (flag2)
			{
				Debug.DrawLine(position, targetPoint, Color.green);
				RaycastHit raycastHit;
				bool flag3 = Physics.Linecast(position, targetPoint, out raycastHit, mask);
				if (flag3)
				{
					foreach (int num2 in layers)
					{
						bool flag4 = raycastHit.collider.gameObject.layer == num2;
						if (flag4)
						{
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060020D7 RID: 8407 RVA: 0x00160EF4 File Offset: 0x0015F2F4
	public bool CanSeeObject(GameObject obj, Vector3 targetPoint)
	{
		if (!this.Blind)
		{
			Debug.DrawLine(this.Eyes.position, targetPoint, Color.green);
			Vector3 position = this.Eyes.transform.position;
			Vector3 vector = targetPoint - position;
			float num = this.VisionDistance * this.VisionDistance;
			bool flag = this.PointIsInFOV(targetPoint);
			bool flag2 = vector.sqrMagnitude <= num;
			if (flag && flag2)
			{
				RaycastHit raycastHit;
				bool flag3 = Physics.Linecast(position, targetPoint, out raycastHit, this.Mask);
				if (flag3)
				{
					bool flag4 = raycastHit.collider.gameObject == obj;
					if (flag4)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060020D8 RID: 8408 RVA: 0x00160FA8 File Offset: 0x0015F3A8
	public bool CanSeeObject(GameObject obj)
	{
		return this.CanSeeObject(obj, obj.transform.position);
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x00160FBC File Offset: 0x0015F3BC
	private void Update()
	{
		if (!this.Stop)
		{
			this.DistanceToPlayer = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
			if (this.Yandere.Egg && this.StudentID > 1)
			{
				if (this.DistanceToPlayer < 1f && this.Yandere.EbolaHair.activeInHierarchy)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Yandere.EbolaEffect, base.transform.position + Vector3.up, Quaternion.identity);
					this.SpawnAlarmDisc();
					this.BecomeRagdoll();
					this.DeathType = DeathType.EasterEgg;
				}
				if (this.DistanceToPlayer < 5f && this.Yandere.Hunger >= 5)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.Yandere.DarkHelix, base.transform.position + Vector3.up, Quaternion.identity);
					this.SpawnAlarmDisc();
					this.BecomeRagdoll();
					this.DeathType = DeathType.EasterEgg;
				}
			}
			this.UpdateRoutine();
			this.UpdateDetectionMarker();
			if (this.Dying)
			{
				this.UpdateDying();
				if (this.Burning)
				{
					this.UpdateBurning();
				}
			}
			else
			{
				if (this.DistanceToPlayer <= 2f)
				{
					this.UpdateTalkInput();
				}
				this.UpdateVision();
				if (this.Pushed)
				{
					this.UpdatePushed();
				}
				else if (this.Drowned)
				{
					this.UpdateDrowned();
				}
				else if (this.WitnessedMurder)
				{
					this.UpdateWitnessedMurder();
				}
				else if (this.Alarmed)
				{
					this.UpdateAlarmed();
				}
				else if (this.TurnOffRadio)
				{
					this.UpdateTurningOffRadio();
				}
				else if (this.Confessing)
				{
					this.UpdateConfessing();
				}
				else if (this.Vomiting)
				{
					this.UpdateVomiting();
				}
				else if (this.Splashed)
				{
					this.UpdateSplashed();
				}
			}
			this.UpdateMisc();
		}
		else
		{
			if (this.StudentManager.Pose)
			{
				this.DistanceToPlayer = Vector3.Distance(base.transform.position, this.Yandere.transform.position);
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.Pose();
				}
			}
			else if (!this.ClubActivity)
			{
				if (!this.Yandere.Talking)
				{
					if (this.Yandere.Sprayed)
					{
						this.CharacterAnimation.CrossFade(this.ScaredAnim);
					}
					if (this.Yandere.Noticed || this.StudentManager.YandereDying)
					{
						this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					}
				}
			}
			else if (this.Police.Darkness.color.a < 1f)
			{
				if (this.Club == ClubType.Cooking)
				{
					this.CharacterAnimation[this.SocialSitAnim].layer = 99;
					this.CharacterAnimation.Play(this.SocialSitAnim);
					this.CharacterAnimation[this.SocialSitAnim].weight = 1f;
					this.SmartPhone.SetActive(false);
					this.SpeechLines.Play();
					this.CharacterAnimation.CrossFade(this.RandomAnim);
					if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
					{
						this.PickRandomAnim();
					}
				}
				else if (this.Club == ClubType.MartialArts)
				{
					this.CharacterAnimation.Play(this.ActivityAnim);
					AudioSource component = base.GetComponent<AudioSource>();
					if (!this.Male)
					{
						if (this.CharacterAnimation["f02_kick_23"].time > 1f)
						{
							if (!this.PlayingAudio)
							{
								component.clip = this.FemaleAttacks[UnityEngine.Random.Range(0, this.FemaleAttacks.Length)];
								component.Play();
								this.PlayingAudio = true;
							}
							if (this.CharacterAnimation["f02_kick_23"].time >= this.CharacterAnimation["f02_kick_23"].length)
							{
								this.CharacterAnimation["f02_kick_23"].time = 0f;
								this.PlayingAudio = false;
							}
						}
					}
					else if (this.CharacterAnimation["kick_24"].time > 1f)
					{
						if (!this.PlayingAudio)
						{
							component.clip = this.MaleAttacks[UnityEngine.Random.Range(0, this.MaleAttacks.Length)];
							component.Play();
							this.PlayingAudio = true;
						}
						if (this.CharacterAnimation["kick_24"].time >= this.CharacterAnimation["kick_24"].length)
						{
							this.CharacterAnimation["kick_24"].time = 0f;
							this.PlayingAudio = false;
						}
					}
				}
				else if (this.Club == ClubType.Drama)
				{
					this.Stop = false;
				}
				else if (this.Club == ClubType.Photography)
				{
					this.CharacterAnimation.CrossFade(this.RandomSleuthAnim);
					if (this.CharacterAnimation[this.RandomSleuthAnim].time >= this.CharacterAnimation[this.RandomSleuthAnim].length)
					{
						this.PickRandomSleuthAnim();
					}
				}
				else if (this.Club == ClubType.Art)
				{
					this.CharacterAnimation.Play(this.ActivityAnim);
					this.Paintbrush.SetActive(true);
					this.Palette.SetActive(true);
				}
				else if (this.Club == ClubType.Science)
				{
					this.CharacterAnimation.Play(this.ClubAnim);
					if (this.StudentID == 62)
					{
						this.ScienceProps[0].SetActive(true);
					}
					else if (this.StudentID == 63)
					{
						this.ScienceProps[1].SetActive(true);
						this.ScienceProps[2].SetActive(true);
					}
					else if (this.StudentID == 64)
					{
						this.ScienceProps[0].SetActive(true);
					}
				}
				else if (this.Club == ClubType.Sports)
				{
					this.Stop = false;
				}
				else if (this.Club == ClubType.Gardening)
				{
					this.CharacterAnimation.Play(this.ClubAnim);
					this.Stop = false;
				}
				else if (this.Club == ClubType.Gaming)
				{
					this.CharacterAnimation.CrossFade(this.ClubAnim);
				}
			}
			this.Alarm = Mathf.MoveTowards(this.Alarm, 0f, Time.deltaTime);
			this.UpdateDetectionMarker();
		}
		if (this.AoT)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(10f, 10f, 10f), Time.deltaTime);
		}
		if (this.Prompt.Label[0] != null)
		{
			if (this.StudentManager.Pose)
			{
				this.Prompt.Label[0].text = "     Pose";
			}
			else if (this.BadTime)
			{
				this.Prompt.Label[0].text = "     Psychokinesis";
			}
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x001617F0 File Offset: 0x0015FBF0
	private void UpdateRoutine()
	{
		if (this.Routine)
		{
			if (this.CurrentDestination != null)
			{
				this.DistanceToDestination = Vector3.Distance(base.transform.position, this.CurrentDestination.position);
			}
			if (this.StalkerFleeing)
			{
				if (this.Actions[this.Phase] == StudentActionType.Stalk && this.DistanceToPlayer > 10f)
				{
					this.Pathfinding.target = this.Yandere.transform;
					this.CurrentDestination = this.Yandere.transform;
					this.StalkerFleeing = false;
					this.Pathfinding.speed = 1f;
				}
			}
			else if (this.Actions[this.Phase] == StudentActionType.Stalk)
			{
				this.TargetDistance = 10f;
				if (this.DistanceToPlayer > 20f)
				{
					Debug.Log("Sprinting 1");
					this.Pathfinding.speed = 4f;
				}
				else if (this.DistanceToPlayer < 10f)
				{
					this.Pathfinding.speed = 1f;
				}
			}
			if (!this.Indoors)
			{
				if (this.Hurry && !this.Tripped && base.transform.position.z > -50.5f && base.transform.position.x < 6f)
				{
					this.CharacterAnimation.CrossFade("trip_00");
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Tripping = true;
					this.Routine = false;
				}
				if (this.Paired)
				{
					if (base.transform.position.z < -50f)
					{
						if (base.transform.localEulerAngles != Vector3.zero)
						{
							base.transform.localEulerAngles = Vector3.zero;
						}
						this.MyController.Move(base.transform.forward * Time.deltaTime);
						if (this.StudentID == 81)
						{
							this.MusumeTimer += Time.deltaTime;
							if (this.MusumeTimer > 5f)
							{
								this.MusumeTimer = 0f;
								this.MusumeRight = !this.MusumeRight;
								this.WalkAnim = ((!this.MusumeRight) ? "f02_walkTalk_01" : "f02_walkTalk_00");
							}
						}
					}
					else
					{
						if (this.Persona == PersonaType.PhoneAddict)
						{
							this.SpeechLines.Stop();
							this.SmartPhone.SetActive(true);
						}
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.StopPairing();
					}
				}
				if (this.Phase == 0)
				{
					if (this.DistanceToDestination < 1f)
					{
						this.CurrentDestination = this.MyLocker;
						this.Pathfinding.target = this.MyLocker;
						this.Phase++;
					}
				}
				else if (this.DistanceToDestination < 0.5f && !this.ShoeRemoval.enabled)
				{
					if (this.Shy)
					{
						this.CharacterAnimation[this.ShyAnim].weight = 0.5f;
					}
					this.SmartPhone.SetActive(false);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.ShoeRemoval.StartChangingShoes();
					this.ShoeRemoval.enabled = true;
					this.CanTalk = false;
					this.Routine = false;
				}
			}
			else if (this.Phase < this.ScheduleBlocks.Length - 1)
			{
				if (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time && !this.InEvent && !this.Meeting && this.ClubActivityPhase < 16)
				{
					this.MyRenderer.updateWhenOffscreen = false;
					this.SprintAnim = this.OriginalSprintAnim;
					if (this.Headache)
					{
						this.SprintAnim = this.OriginalSprintAnim;
						this.WalkAnim = this.OriginalWalkAnim;
					}
					this.Headache = false;
					this.Sedated = false;
					this.Hurry = false;
					this.Phase++;
					if (this.Drownable)
					{
						this.Drownable = false;
						this.StudentManager.UpdateMe(this.StudentID);
					}
					if (this.StudentID == 1)
					{
					}
					if (this.StudentID == 11)
					{
					}
					if (this.Schoolwear > 1 && this.Destinations[this.Phase] == this.MyLocker)
					{
						this.Phase++;
					}
					if (this.Actions[this.Phase] == StudentActionType.Graffiti && !this.StudentManager.Bully)
					{
						ScheduleBlock scheduleBlock = this.ScheduleBlocks[2];
						scheduleBlock.destination = "Patrol";
						scheduleBlock.action = "Patrol";
						this.GetDestinations();
					}
					if (!this.StudentManager.ReactedToGameLeader && this.Actions[this.Phase] == StudentActionType.Bully && !this.StudentManager.Bully)
					{
						ScheduleBlock scheduleBlock2 = this.ScheduleBlocks[4];
						scheduleBlock2.destination = "Sunbathe";
						scheduleBlock2.action = "Sunbathe";
						this.GetDestinations();
					}
					if (this.Sedated)
					{
						this.SprintAnim = this.OriginalSprintAnim;
						this.Sedated = false;
					}
					this.CurrentAction = this.Actions[this.Phase];
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					if ((this.StudentID == 30 && this.StudentManager.DatingMinigame.Affection == 100f) || (this.StudentID == this.StudentManager.RivalID && DateGlobals.Weekday == DayOfWeek.Friday && !this.InCouple))
					{
						if (this.StudentID == this.StudentManager.RivalID && DateGlobals.Weekday == DayOfWeek.Friday)
						{
							Debug.Log("This is the part where Osana should go put a note in Senpai's locker.");
						}
						if (this.Actions[this.Phase] == StudentActionType.ChangeShoes)
						{
							if (this.StudentID == 30)
							{
								this.CurrentDestination = this.StudentManager.SuitorLocker;
								this.Pathfinding.target = this.StudentManager.SuitorLocker;
							}
							else
							{
								this.CurrentDestination = this.StudentManager.SenpaiLocker;
								this.Pathfinding.target = this.StudentManager.SenpaiLocker;
							}
							this.Confessing = true;
							this.Routine = false;
							this.CanTalk = false;
						}
					}
					if (this.CurrentDestination != null)
					{
						this.DistanceToDestination = Vector3.Distance(base.transform.position, this.CurrentDestination.position);
					}
					if (this.Bento != null && this.Bento.activeInHierarchy)
					{
						this.Bento.SetActive(false);
						this.Chopsticks[0].SetActive(false);
						this.Chopsticks[1].SetActive(false);
					}
					if (this.Male)
					{
						this.Cosmetic.MyRenderer.materials[this.Cosmetic.FaceID].SetFloat("_BlendAmount", 0f);
						this.PinkSeifuku.SetActive(false);
					}
					else
					{
						this.HorudaCollider.gameObject.SetActive(false);
					}
					if (this.StudentID == 81)
					{
						this.Cigarette.SetActive(false);
						this.Lighter.SetActive(false);
					}
					if (!this.Paired)
					{
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
					}
					if (this.Persona != PersonaType.PhoneAddict && !this.Sleuthing)
					{
						this.SmartPhone.SetActive(false);
					}
					else if (!this.Slave)
					{
						this.IdleAnim = this.PhoneAnims[0];
						this.SmartPhone.SetActive(true);
					}
					this.Paintbrush.SetActive(false);
					this.Sketchbook.SetActive(false);
					this.Palette.SetActive(false);
					this.Pencil.SetActive(false);
					this.OccultBook.SetActive(false);
					this.Scrubber.SetActive(false);
					this.Eraser.SetActive(false);
					this.Pen.SetActive(false);
					foreach (GameObject gameObject in this.ScienceProps)
					{
						if (gameObject != null)
						{
							gameObject.SetActive(false);
						}
					}
					foreach (GameObject gameObject2 in this.Fingerfood)
					{
						if (gameObject2 != null)
						{
							gameObject2.SetActive(false);
						}
					}
					this.SpeechLines.Stop();
					this.GoAway = false;
					this.ReadPhase = 0;
					this.PatrolID = 0;
					if (this.Actions[this.Phase] == StudentActionType.Clean)
					{
						this.EquipCleaningItems();
					}
					else if (!this.Slave)
					{
						if (this.Persona == PersonaType.PhoneAddict)
						{
							this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
							this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 165f);
							this.WalkAnim = this.PhoneAnims[1];
						}
						else if (this.Sleuthing)
						{
							this.WalkAnim = this.SleuthWalkAnim;
						}
					}
					if (this.Actions[this.Phase] == StudentActionType.Sleuth && this.StudentManager.SleuthPhase == 3)
					{
						this.GetSleuthTarget();
					}
					if (this.Actions[this.Phase] == StudentActionType.Stalk)
					{
						this.TargetDistance = 10f;
					}
					else if (this.Actions[this.Phase] == StudentActionType.Follow)
					{
						this.TargetDistance = 1f;
					}
					else if (this.Actions[this.Phase] != StudentActionType.SitAndEatBento)
					{
						this.TargetDistance = 0.5f;
					}
					if (this.Club == ClubType.Gardening && this.StudentID != 71)
					{
						this.WateringCan.transform.parent = this.Hips;
						this.WateringCan.transform.localPosition = new Vector3(0f, 0.0135f, -0.184f);
						this.WateringCan.transform.localEulerAngles = new Vector3(0f, 90f, 30f);
						if (this.Clock.Period == 6)
						{
							this.StudentManager.Patrols.List[this.StudentID] = this.StudentManager.GardeningPatrols[this.StudentID - 71];
							this.ClubAnim = "f02_waterPlantLow_00";
							this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
							this.Pathfinding.target = this.CurrentDestination;
						}
					}
					if (this.Club == ClubType.LightMusic)
					{
						if (this.StudentID == 51)
						{
							if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
							{
								this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
								this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
								this.Instruments[this.ClubMemberID].transform.parent = null;
								this.Instruments[this.ClubMemberID].transform.position = new Vector3(-0.5f, 4.5f, 22.45666f);
								this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, 0f, 0f);
							}
							else
							{
								this.Instruments[this.ClubMemberID].SetActive(false);
							}
						}
						else
						{
							this.Instruments[this.ClubMemberID].SetActive(false);
						}
						this.Drumsticks[0].SetActive(false);
						this.Drumsticks[1].SetActive(false);
						this.AirGuitar.Stop();
					}
					if (this.Phase == 8 && this.StudentID == 36)
					{
						this.StudentManager.Clubs.List[this.StudentID].position = this.StudentManager.Clubs.List[71].position;
						this.StudentManager.Clubs.List[this.StudentID].rotation = this.StudentManager.Clubs.List[71].rotation;
						this.ClubAnim = this.GameAnim;
					}
					if (this.MyPlate != null && this.MyPlate.parent == this.RightHand)
					{
						this.CurrentDestination = this.StudentManager.Clubs.List[this.StudentID];
						this.Pathfinding.target = this.StudentManager.Clubs.List[this.StudentID];
					}
					if (this.Persona == PersonaType.Sleuth)
					{
						if (this.Male)
						{
							this.SmartPhone.transform.localPosition = new Vector3(0.06f, -0.02f, -0.02f);
						}
						else
						{
							this.SmartPhone.transform.localPosition = new Vector3(0.033333f, -0.015f, -0.015f);
						}
						this.SmartPhone.transform.localEulerAngles = new Vector3(12.5f, 120f, 180f);
					}
				}
				if (!this.Teacher && this.Club != ClubType.Delinquent && (this.Clock.Period == 2 || this.Clock.Period == 4) && this.ClubActivityPhase < 16)
				{
					this.Pathfinding.speed = 4f;
				}
			}
			if (this.MeetTime > 0f && this.Clock.HourTime > this.MeetTime)
			{
				Debug.Log("Sprinting 3");
				this.CurrentDestination = this.MeetSpot;
				this.Pathfinding.target = this.MeetSpot;
				this.DistanceToDestination = Vector3.Distance(base.transform.position, this.CurrentDestination.position);
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Pathfinding.speed = 4f;
				this.SpeechLines.Stop();
				this.Meeting = true;
				this.MeetTime = 0f;
			}
			if (this.DistanceToDestination > this.TargetDistance)
			{
				if (this.CurrentDestination == null && this.Actions[this.Phase] == StudentActionType.Sleuth)
				{
					this.GetSleuthTarget();
				}
				if (this.Actions[this.Phase] == StudentActionType.Follow)
				{
					this.Obstacle.enabled = false;
					this.MyController.radius = 0f;
					if (this.FollowTarget.Wet && this.FollowTarget.DistanceToDestination < 5f)
					{
						this.TargetDistance = 4f;
					}
					else
					{
						this.TargetDistance = 1f;
					}
					if (this.DistanceToDestination > 2f && !this.Calm)
					{
						this.Pathfinding.speed = 5f;
						this.SpeechLines.Stop();
					}
					else
					{
						this.Pathfinding.speed = 1f;
						this.SpeechLines.Stop();
					}
				}
				if (this.CuriosityPhase == 1 && this.Actions[this.Phase] == StudentActionType.Relax && this.StudentManager.Students[this.Crush].Private)
				{
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.CurrentDestination = this.Destinations[this.Phase];
					this.TargetDistance = 0.5f;
					this.CuriosityTimer = 0f;
					this.CuriosityPhase--;
				}
				if (this.Actions[this.Phase] != StudentActionType.Follow || (this.Actions[this.Phase] == StudentActionType.Follow && this.DistanceToDestination > this.TargetDistance + 0.1f))
				{
					if (((this.Clock.Period == 1 && this.Clock.HourTime > 8.483334f) || (this.Clock.Period == 3 && this.Clock.HourTime > 13.4833336f)) && !this.Teacher)
					{
						this.Pathfinding.speed = 4f;
					}
					if (!this.InEvent && !this.Meeting && !this.GoAway)
					{
						if (this.DressCode)
						{
							if (this.Actions[this.Phase] == StudentActionType.ClubAction)
							{
								if (!this.ClubAttire)
								{
									if (!this.ChangingBooth.Occupied)
									{
										this.CurrentDestination = this.ChangingBooth.transform;
										this.Pathfinding.target = this.ChangingBooth.transform;
									}
									else
									{
										this.CurrentDestination = this.ChangingBooth.WaitSpots[this.ClubMemberID];
										this.Pathfinding.target = this.ChangingBooth.WaitSpots[this.ClubMemberID];
									}
								}
								else if (this.Indoors && !this.GoAway)
								{
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.DistanceToDestination = 100f;
								}
							}
							else if (this.ClubAttire)
							{
								if (!this.ChangingBooth.Occupied)
								{
									this.CurrentDestination = this.ChangingBooth.transform;
									this.Pathfinding.target = this.ChangingBooth.transform;
								}
								else
								{
									this.CurrentDestination = this.ChangingBooth.WaitSpots[this.ClubMemberID];
									this.Pathfinding.target = this.ChangingBooth.WaitSpots[this.ClubMemberID];
								}
							}
							else if (this.Indoors && this.Actions[this.Phase] != StudentActionType.Clean && this.Actions[this.Phase] != StudentActionType.Sketch)
							{
								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];
							}
						}
						else if (this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes && this.Schoolwear > 1 && !this.SchoolwearUnavailable)
						{
							this.CurrentDestination = this.StudentManager.FemaleStripSpot;
							this.Pathfinding.target = this.StudentManager.FemaleStripSpot;
						}
					}
					if (!this.Pathfinding.canMove)
					{
						if (!this.Spawned)
						{
							base.transform.position = this.StudentManager.SpawnPositions[this.StudentID].position;
							this.Spawned = true;
							if (this.StudentID == 10 && this.StudentManager.Students[11] == null)
							{
								base.transform.position = new Vector3(-4f, 0f, -96f);
								Physics.SyncTransforms();
							}
						}
						if (!this.Paired && !this.Alarmed)
						{
							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;
							this.Obstacle.enabled = false;
						}
					}
					if (this.Pathfinding.speed > 0f)
					{
						if (this.Pathfinding.speed == 1f)
						{
							if (!this.CharacterAnimation.IsPlaying(this.WalkAnim))
							{
								if (this.Persona == PersonaType.PhoneAddict && this.Actions[this.Phase] == StudentActionType.Clean)
								{
									this.CharacterAnimation.CrossFade(this.OriginalWalkAnim);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.WalkAnim);
								}
							}
						}
						else if (!this.Dying)
						{
							this.CharacterAnimation.CrossFade(this.SprintAnim);
						}
					}
					if (this.Club == ClubType.Occult && this.Actions[this.Phase] != StudentActionType.ClubAction)
					{
						this.OccultBook.SetActive(false);
					}
					if (!this.Meeting && !this.GoAway && !this.InEvent)
					{
						if (this.Actions[this.Phase] == StudentActionType.Clean)
						{
							if (this.SmartPhone.activeInHierarchy)
							{
								this.SmartPhone.SetActive(false);
							}
							if (this.CurrentDestination != this.CleaningSpot.GetChild(this.CleanID))
							{
								this.CurrentDestination = this.CleaningSpot.GetChild(this.CleanID);
								this.Pathfinding.target = this.CurrentDestination;
							}
						}
						if (this.Actions[this.Phase] == StudentActionType.Patrol && this.CurrentDestination != this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID))
						{
							this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
							this.Pathfinding.target = this.CurrentDestination;
						}
					}
				}
			}
			else
			{
				if (this.CurrentDestination != null)
				{
					bool flag = false;
					if ((this.Actions[this.Phase] == StudentActionType.Sleuth && this.StudentManager.SleuthPhase == 3) || this.Actions[this.Phase] == StudentActionType.Stalk || (this.Actions[this.Phase] == StudentActionType.Relax && this.CuriosityPhase == 1))
					{
						flag = true;
					}
					if (this.Actions[this.Phase] == StudentActionType.Follow)
					{
						this.FollowTargetDistance = Vector3.Distance(this.FollowTarget.transform.position, this.StudentManager.Hangouts.List[this.StudentID].transform.position);
						if (this.FollowTargetDistance < 1.1f && !this.FollowTarget.Alone)
						{
							this.MoveTowardsTarget(this.StudentManager.Hangouts.List[this.StudentID].position);
							float num = Quaternion.Angle(base.transform.rotation, this.StudentManager.Hangouts.List[this.StudentID].rotation);
							if (num > 1f && !this.StopRotating)
							{
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.StudentManager.Hangouts.List[this.StudentID].rotation, 10f * Time.deltaTime);
							}
						}
						else if (!this.ManualRotation)
						{
							this.targetRotation = Quaternion.LookRotation(this.FollowTarget.transform.position - base.transform.position);
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
						}
					}
					else if (!flag)
					{
						this.MoveTowardsTarget(this.CurrentDestination.position);
						float num2 = Quaternion.Angle(base.transform.rotation, this.CurrentDestination.rotation);
						if (num2 > 1f && !this.StopRotating)
						{
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, 10f * Time.deltaTime);
						}
					}
					else
					{
						if (this.Actions[this.Phase] == StudentActionType.Sleuth || this.Actions[this.Phase] == StudentActionType.Stalk)
						{
							this.targetRotation = Quaternion.LookRotation(this.SleuthTarget.position - base.transform.position);
						}
						else if (this.Crush > 0)
						{
							this.targetRotation = Quaternion.LookRotation(new Vector3(this.StudentManager.Students[this.Crush].transform.position.x, base.transform.position.y, this.StudentManager.Students[this.Crush].transform.position.z) - base.transform.position);
						}
						float num3 = Quaternion.Angle(base.transform.rotation, this.targetRotation);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
						if (num3 > 1f)
						{
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
						}
					}
					if (!this.Hurry)
					{
						this.Pathfinding.speed = 1f;
					}
					else
					{
						this.Pathfinding.speed = 4f;
					}
				}
				if (this.Pathfinding.canMove)
				{
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					if (this.Actions[this.Phase] != StudentActionType.Clean)
					{
						this.Obstacle.enabled = true;
					}
				}
				if (!this.InEvent && !this.Meeting && this.DressCode)
				{
					if (this.Actions[this.Phase] == StudentActionType.ClubAction)
					{
						if (!this.ClubAttire)
						{
							if (!this.ChangingBooth.Occupied)
							{
								if (this.CurrentDestination == this.ChangingBooth.transform)
								{
									this.ChangingBooth.Occupied = true;
									this.ChangingBooth.Student = this;
									this.ChangingBooth.CheckYandereClub();
									this.Obstacle.enabled = false;
								}
								this.CurrentDestination = this.ChangingBooth.transform;
								this.Pathfinding.target = this.ChangingBooth.transform;
							}
							else
							{
								this.CharacterAnimation.CrossFade(this.IdleAnim);
							}
						}
						else if (!this.GoAway)
						{
							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];
						}
					}
					else if (this.ClubAttire)
					{
						if (!this.ChangingBooth.Occupied)
						{
							if (this.CurrentDestination == this.ChangingBooth.transform)
							{
								this.ChangingBooth.Occupied = true;
								this.ChangingBooth.Student = this;
								this.ChangingBooth.CheckYandereClub();
							}
							this.CurrentDestination = this.ChangingBooth.transform;
							this.Pathfinding.target = this.ChangingBooth.transform;
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
					}
					else if (this.Actions[this.Phase] != StudentActionType.Clean)
					{
						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
					}
				}
				if (!this.InEvent)
				{
					if (!this.Meeting)
					{
						if (!this.GoAway)
						{
							if (this.Actions[this.Phase] == StudentActionType.AtLocker)
							{
								this.CharacterAnimation.CrossFade(this.IdleAnim);
								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
							}
							else if (this.Actions[this.Phase] == StudentActionType.Socializing || (this.Actions[this.Phase] == StudentActionType.Follow && this.FollowTargetDistance < 1f && !this.FollowTarget.Alone && !this.FollowTarget.InEvent && !this.FollowTarget.Talking))
							{
								if (this.MyPlate != null && this.MyPlate.parent == this.RightHand)
								{
									this.MyPlate.parent = null;
									this.MyPlate.position = this.OriginalPlatePosition;
									this.MyPlate.rotation = this.OriginalPlateRotation;
									this.IdleAnim = this.OriginalIdleAnim;
									this.WalkAnim = this.OriginalWalkAnim;
									this.LeanAnim = this.OriginalLeanAnim;
									this.ResumeDistracting = false;
									this.Distracting = false;
									this.Distracted = false;
									this.CanTalk = true;
								}
								if (this.Paranoia > 1.66666f && !this.StudentManager.LoveSick && this.Club != ClubType.Delinquent)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									this.StudentManager.ConvoManager.CheckMe(this.StudentID);
									if (this.Club == ClubType.Delinquent)
									{
										if (this.Alone)
										{
											if (this.TrueAlone)
											{
												if (!this.SmartPhone.activeInHierarchy)
												{
													this.CharacterAnimation.CrossFade("delinquentTexting_00");
													this.SmartPhone.SetActive(true);
													this.SpeechLines.Stop();
												}
											}
											else
											{
												this.CharacterAnimation.CrossFade(this.IdleAnim);
												this.SpeechLines.Stop();
											}
										}
										else
										{
											if (!this.InEvent)
											{
												if (!this.Grudge)
												{
													if (!this.SpeechLines.isPlaying)
													{
														this.SmartPhone.SetActive(false);
														this.SpeechLines.Play();
													}
												}
												else
												{
													this.SmartPhone.SetActive(false);
												}
											}
											this.CharacterAnimation.CrossFade(this.RandomAnim);
											if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
											{
												this.PickRandomAnim();
											}
										}
									}
									else if (this.Alone)
									{
										if (!this.Male)
										{
											this.CharacterAnimation.CrossFade("f02_standTexting_00");
										}
										else if (this.StudentID == 36)
										{
											this.CharacterAnimation.CrossFade(this.ClubAnim);
										}
										else if (this.StudentID == 66)
										{
											this.CharacterAnimation.CrossFade("delinquentTexting_00");
										}
										else
										{
											this.CharacterAnimation.CrossFade("standTexting_00");
										}
										if (!this.SmartPhone.activeInHierarchy && !this.Shy)
										{
											if (this.StudentID == 36)
											{
												this.SmartPhone.transform.localPosition = new Vector3(0.0566666f, -0.02f, 0f);
												this.SmartPhone.transform.localEulerAngles = new Vector3(10f, 115f, 180f);
											}
											else
											{
												this.SmartPhone.transform.localPosition = new Vector3(0.015f, 0.01f, 0.025f);
												this.SmartPhone.transform.localEulerAngles = new Vector3(10f, -160f, 165f);
											}
											this.SmartPhone.SetActive(true);
											this.SpeechLines.Stop();
										}
									}
									else
									{
										if (!this.InEvent)
										{
											if (!this.Grudge)
											{
												if (!this.SpeechLines.isPlaying)
												{
													this.SmartPhone.SetActive(false);
													this.SpeechLines.Play();
												}
											}
											else
											{
												this.SmartPhone.SetActive(false);
											}
										}
										if (this.Club != ClubType.Photography)
										{
											this.CharacterAnimation.CrossFade(this.RandomAnim);
											if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
											{
												this.PickRandomAnim();
											}
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.RandomSleuthAnim);
											if (this.CharacterAnimation[this.RandomSleuthAnim].time >= this.CharacterAnimation[this.RandomSleuthAnim].length)
											{
												this.PickRandomSleuthAnim();
											}
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Gossip)
							{
								if (this.Paranoia > 1.66666f && !this.StudentManager.LoveSick)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									this.StudentManager.ConvoManager.CheckMe(this.StudentID);
									if (this.Alone)
									{
										if (!this.Shy)
										{
											this.CharacterAnimation.CrossFade("f02_standTexting_00");
											if (!this.SmartPhone.activeInHierarchy)
											{
												this.SmartPhone.SetActive(true);
												this.SpeechLines.Stop();
											}
										}
									}
									else
									{
										if (!this.SpeechLines.isPlaying)
										{
											this.SmartPhone.SetActive(false);
											this.SpeechLines.Play();
										}
										this.CharacterAnimation.CrossFade(this.RandomGossipAnim);
										if (this.CharacterAnimation[this.RandomGossipAnim].time >= this.CharacterAnimation[this.RandomGossipAnim].length)
										{
											this.PickRandomGossipAnim();
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Gaming)
							{
								this.CharacterAnimation.CrossFade(this.GameAnim);
							}
							else if (this.Actions[this.Phase] == StudentActionType.Shamed)
							{
								this.CharacterAnimation.CrossFade(this.SadSitAnim);
							}
							else if (this.Actions[this.Phase] == StudentActionType.Slave)
							{
								this.CharacterAnimation.CrossFade(this.BrokenSitAnim);
								if (this.FragileSlave)
								{
									if (this.HuntTarget == null)
									{
										this.HuntTarget = this;
										this.GoCommitMurder();
									}
									else if (this.HuntTarget.Indoors)
									{
										this.GoCommitMurder();
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Relax)
							{
								if (this.CuriosityPhase == 0)
								{
									this.CharacterAnimation.CrossFade(this.RelaxAnim);
									if (this.Curious)
									{
										this.CuriosityTimer += Time.deltaTime;
										if (this.CuriosityTimer > 30f)
										{
											if (!this.StudentManager.Students[this.Crush].Private && !this.StudentManager.Students[this.Crush].Wet)
											{
												this.Pathfinding.target = this.StudentManager.Students[this.Crush].transform;
												this.CurrentDestination = this.StudentManager.Students[this.Crush].transform;
												this.TargetDistance = 5f;
												this.CuriosityTimer = 0f;
												this.CuriosityPhase++;
											}
											else
											{
												this.CuriosityTimer = 0f;
											}
										}
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.LeanAnim);
									this.CuriosityTimer += Time.deltaTime;
									if (this.CuriosityTimer > 10f || !this.StudentManager.Students[this.Crush].Private || !this.StudentManager.Students[this.Crush].Wet)
									{
										this.Pathfinding.target = this.Destinations[this.Phase];
										this.CurrentDestination = this.Destinations[this.Phase];
										this.TargetDistance = 0.5f;
										this.CuriosityTimer = 0f;
										this.CuriosityPhase--;
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes)
							{
								if (this.MyPlate != null && this.MyPlate.parent == this.RightHand)
								{
									this.MyPlate.parent = null;
									this.MyPlate.position = this.OriginalPlatePosition;
									this.MyPlate.rotation = this.OriginalPlateRotation;
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.IdleAnim = this.OriginalIdleAnim;
									this.WalkAnim = this.OriginalWalkAnim;
									this.LeanAnim = this.OriginalLeanAnim;
									this.ResumeDistracting = false;
									this.Distracting = false;
									this.Distracted = false;
									this.CanTalk = true;
								}
								if (this.MustChangeClothing)
								{
									if (this.ChangeClothingPhase == 0)
									{
										if (this.StudentManager.CommunalLocker.Student == null)
										{
											this.StudentManager.CommunalLocker.Open = true;
											this.StudentManager.CommunalLocker.Student = this;
											this.StudentManager.CommunalLocker.SpawnSteam();
											this.Schoolwear = 1;
											this.ChangeClothingPhase++;
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);
										}
									}
									else if (this.ChangeClothingPhase == 1 && !this.StudentManager.CommunalLocker.SteamCountdown)
									{
										this.Pathfinding.target = this.Seat;
										this.CurrentDestination = this.Seat;
										this.StudentManager.CommunalLocker.Student = null;
										this.ChangeClothingPhase++;
										this.MustChangeClothing = false;
									}
								}
								else if (this.Bullied)
								{
									if (this.SmartPhone.activeInHierarchy)
									{
										this.SmartPhone.SetActive(false);
									}
									this.CharacterAnimation.CrossFade(this.SadDeskSitAnim, 1f);
								}
								else
								{
									if (this.Rival && this.Phoneless && this.StudentManager.CommunalLocker.RivalPhone.gameObject.activeInHierarchy && !this.EndSearch && this.Yandere.CanMove)
									{
										if (SchemeGlobals.GetSchemeStage(1) == 7)
										{
											SchemeGlobals.SetSchemeStage(1, 8);
											this.Yandere.PauseScreen.Schemes.UpdateInstructions();
										}
										this.CharacterAnimation.CrossFade(this.DiscoverPhoneAnim);
										this.Subtitle.UpdateLabel(this.LostPhoneSubtitleType, 2, 5f);
										this.EndSearch = true;
										this.Routine = false;
									}
									if (!this.EndSearch)
									{
										if (this.Clock.Period != 2 && this.Clock.Period != 4)
										{
											if (this.DressCode && this.ClubAttire)
											{
												this.CharacterAnimation.CrossFade(this.IdleAnim);
											}
											else if ((double)Vector3.Distance(base.transform.position, this.Seat.position) < 0.5)
											{
												if (!this.Phoneless)
												{
													if (this.StudentID == 1 && this.StudentManager.Gift.activeInHierarchy)
													{
														this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
														this.CharacterAnimation.CrossFade(this.InspectBloodAnim);
														if (this.CharacterAnimation[this.InspectBloodAnim].time >= this.CharacterAnimation[this.InspectBloodAnim].length)
														{
															this.StudentManager.Gift.SetActive(false);
														}
													}
													else if (this.Club != ClubType.Delinquent)
													{
														if (!this.SmartPhone.activeInHierarchy)
														{
															if (this.Male)
															{
																this.SmartPhone.transform.localPosition = new Vector3(0.025f, 0.0025f, 0.025f);
																this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 180f);
															}
															else
															{
																this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.01f, 0.01f);
																this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 165f);
															}
															this.SmartPhone.SetActive(true);
														}
														this.CharacterAnimation.CrossFade(this.DeskTextAnim);
													}
													else
													{
														this.CharacterAnimation.CrossFade("delinquentSit_00");
													}
												}
												else
												{
													this.CharacterAnimation.CrossFade(this.SadDeskSitAnim);
												}
											}
										}
										else if (this.StudentManager.Teachers[this.Class].SpeechLines.isPlaying && !this.StudentManager.Teachers[this.Class].Alarmed)
										{
											if (this.DressCode && this.ClubAttire)
											{
												this.CharacterAnimation.CrossFade(this.IdleAnim);
											}
											else
											{
												if (!this.Depressed && !this.Pen.activeInHierarchy)
												{
													this.Pen.SetActive(true);
												}
												if (this.SmartPhone.activeInHierarchy)
												{
													this.SmartPhone.SetActive(false);
												}
												if (this.MyPaper == null)
												{
													if (base.transform.position.x < 0f)
													{
														this.MyPaper = UnityEngine.Object.Instantiate<GameObject>(this.Paper, this.Seat.position + new Vector3(-0.4f, 0.772f, 0f), Quaternion.identity);
													}
													else
													{
														this.MyPaper = UnityEngine.Object.Instantiate<GameObject>(this.Paper, this.Seat.position + new Vector3(0.4f, 0.772f, 0f), Quaternion.identity);
													}
													this.MyPaper.transform.eulerAngles = new Vector3(0f, 0f, -90f);
													this.MyPaper.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
													this.MyPaper.transform.parent = this.StudentManager.Papers;
												}
												this.CharacterAnimation.CrossFade(this.SitAnim);
											}
										}
										else if (this.Club != ClubType.Delinquent)
										{
											this.CharacterAnimation.CrossFade(this.ConfusedSitAnim);
										}
										else
										{
											this.CharacterAnimation.CrossFade("delinquentSit_00");
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Peek)
							{
								this.CharacterAnimation.CrossFade(this.PeekAnim);
								if (this.Male)
								{
									this.Cosmetic.MyRenderer.materials[this.Cosmetic.FaceID].SetFloat("_BlendAmount", 1f);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.ClubAction)
							{
								if (this.DressCode && !this.ClubAttire)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									if (this.StudentID == 47 || this.StudentID == 49)
									{
										if (this.GetNewAnimation)
										{
											this.StudentManager.ConvoManager.MartialArtsCheck();
										}
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.GetNewAnimation = true;
										}
									}
									if (this.Club != ClubType.Occult)
									{
										this.CharacterAnimation.CrossFade(this.ClubAnim);
									}
								}
								if (this.Club == ClubType.Cooking)
								{
									if (this.ClubActivityPhase == 0)
									{
										if (this.ClubTimer == 0f)
										{
											this.MyPlate.parent = null;
											this.MyPlate.gameObject.SetActive(true);
											this.MyPlate.position = this.OriginalPlatePosition;
											this.MyPlate.rotation = this.OriginalPlateRotation;
										}
										this.ClubTimer += Time.deltaTime;
										if (this.ClubTimer > 60f)
										{
											this.MyPlate.parent = this.RightHand;
											this.MyPlate.localPosition = new Vector3(0.02f, -0.02f, -0.15f);
											this.MyPlate.localEulerAngles = new Vector3(-5f, -90f, 172.5f);
											this.IdleAnim = this.PlateIdleAnim;
											this.WalkAnim = this.PlateWalkAnim;
											this.LeanAnim = this.PlateIdleAnim;
											this.GetFoodTarget();
											this.ClubTimer = 0f;
											this.ClubActivityPhase++;
										}
									}
									else
									{
										this.GetFoodTarget();
									}
								}
								else if (this.Club == ClubType.Drama)
								{
									this.StudentManager.DramaTimer += Time.deltaTime;
									if (this.StudentManager.DramaPhase == 1)
									{
										this.StudentManager.ConvoManager.CheckMe(this.StudentID);
										if (this.Alone)
										{
											if (this.Phoneless)
											{
												this.CharacterAnimation.CrossFade("f02_sit_01");
											}
											else
											{
												if (this.Male)
												{
													this.CharacterAnimation.CrossFade("standTexting_00");
												}
												else
												{
													this.CharacterAnimation.CrossFade("f02_standTexting_00");
												}
												if (!this.SmartPhone.activeInHierarchy)
												{
													this.SmartPhone.transform.localPosition = new Vector3(0.02f, 0.01f, 0.03f);
													this.SmartPhone.transform.localEulerAngles = new Vector3(5f, -160f, 180f);
													this.SmartPhone.SetActive(true);
													this.SpeechLines.Stop();
												}
											}
										}
										else if (this.StudentID == 26 && !this.SpeechLines.isPlaying)
										{
											this.SmartPhone.SetActive(false);
											this.SpeechLines.Play();
										}
										if (this.StudentManager.DramaTimer > 100f)
										{
											this.StudentManager.DramaTimer = 0f;
											this.StudentManager.UpdateDrama();
										}
									}
									else if (this.StudentManager.DramaPhase == 2)
									{
										if (this.StudentManager.DramaTimer > 300f)
										{
											this.StudentManager.DramaTimer = 0f;
											this.StudentManager.UpdateDrama();
										}
									}
									else if (this.StudentManager.DramaPhase == 3)
									{
										this.SpeechLines.Play();
										this.CharacterAnimation.CrossFade(this.RandomAnim);
										if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
										{
											this.PickRandomAnim();
										}
										if (this.StudentManager.DramaTimer > 100f)
										{
											this.StudentManager.DramaTimer = 0f;
											this.StudentManager.UpdateDrama();
										}
									}
								}
								else if (this.Club == ClubType.Occult)
								{
									if (this.ReadPhase == 0)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
										this.CharacterAnimation.CrossFade(this.BookSitAnim);
										if (this.CharacterAnimation[this.BookSitAnim].time > this.CharacterAnimation[this.BookSitAnim].length)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											this.CharacterAnimation.CrossFade(this.BookReadAnim);
											this.ReadPhase++;
										}
										else if (this.CharacterAnimation[this.BookSitAnim].time > 1f)
										{
											this.OccultBook.SetActive(true);
										}
									}
								}
								else if (this.Club == ClubType.Art)
								{
									if (this.ClubAttire && !this.Paintbrush.activeInHierarchy && (double)Vector3.Distance(base.transform.position, this.CurrentDestination.position) < 0.5)
									{
										this.Paintbrush.SetActive(true);
										this.Palette.SetActive(true);
									}
								}
								else if (this.Club == ClubType.LightMusic)
								{
									if ((double)this.Clock.HourTime < 16.9)
									{
										this.Instruments[this.ClubMemberID].SetActive(true);
										this.CharacterAnimation.CrossFade(this.ClubAnim);
										if (this.StudentID == 51)
										{
											if (this.InstrumentBag[this.ClubMemberID].transform.parent != null)
											{
												this.InstrumentBag[this.ClubMemberID].transform.parent = null;
												this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(0.5f, 4.5f, 22.45666f);
												this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, 0f, 0f);
											}
											if (this.Instruments[this.ClubMemberID].transform.parent == null)
											{
												this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Play();
												this.Instruments[this.ClubMemberID].transform.parent = base.transform;
												this.Instruments[this.ClubMemberID].transform.localPosition = new Vector3(0.340493f, 0.653502f, -0.286104f);
												this.Instruments[this.ClubMemberID].transform.localEulerAngles = new Vector3(-13.6139f, 16.16775f, 72.5293f);
											}
										}
										else if (this.StudentID == 54 && !this.Drumsticks[0].activeInHierarchy)
										{
											this.Drumsticks[0].SetActive(true);
											this.Drumsticks[1].SetActive(true);
										}
									}
									else if (this.StudentID == 51)
									{
										this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(0.5f, 4.5f, 22.45666f);
										this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, 0f, 0f);
										this.InstrumentBag[this.ClubMemberID].transform.parent = null;
										if (!this.StudentManager.PracticeMusic.isPlaying)
										{
											this.CharacterAnimation.CrossFade("f02_vocalIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 114.5f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalCelebrate_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 104f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalWait_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 32f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalSingB_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 24f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalSingB_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 17f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalSingB_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 14f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalWait_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 8f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalSingA_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 0f)
										{
											this.CharacterAnimation.CrossFade("f02_vocalWait_00");
										}
									}
									else if (this.StudentID == 52)
									{
										if (!this.Instruments[this.ClubMemberID].activeInHierarchy)
										{
											this.Instruments[this.ClubMemberID].SetActive(true);
											this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
											this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
											this.Instruments[this.ClubMemberID].transform.parent = this.Spine;
											this.Instruments[this.ClubMemberID].transform.localPosition = new Vector3(0.275f, -0.16f, 0.095f);
											this.Instruments[this.ClubMemberID].transform.localEulerAngles = new Vector3(-22.5f, 30f, 60f);
											this.InstrumentBag[this.ClubMemberID].transform.parent = null;
											this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 25f);
											this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, -90f, 0f);
										}
										if (!this.StudentManager.PracticeMusic.isPlaying)
										{
											this.CharacterAnimation.CrossFade("f02_guitarIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 114.5f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarCelebrate_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 112f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarWait_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 64f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarPlayA_01");
										}
										else if (this.StudentManager.PracticeMusic.time > 8f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarPlayA_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 0f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarWait_00");
										}
									}
									else if (this.StudentID == 53)
									{
										if (!this.Instruments[this.ClubMemberID].activeInHierarchy)
										{
											this.Instruments[this.ClubMemberID].SetActive(true);
											this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
											this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
											this.Instruments[this.ClubMemberID].transform.parent = this.Spine;
											this.Instruments[this.ClubMemberID].transform.localPosition = new Vector3(0.275f, -0.16f, 0.095f);
											this.Instruments[this.ClubMemberID].transform.localEulerAngles = new Vector3(-22.5f, 30f, 60f);
											this.InstrumentBag[this.ClubMemberID].transform.parent = null;
											this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 26f);
											this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, -90f, 0f);
										}
										if (!this.StudentManager.PracticeMusic.isPlaying)
										{
											this.CharacterAnimation.CrossFade("f02_guitarIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 114.5f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarCelebrate_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 112f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarWait_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 88f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarPlayA_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 80f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarWait_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 64f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarPlayB_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 0f)
										{
											this.CharacterAnimation.CrossFade("f02_guitarPlaySlowA_01");
										}
									}
									else if (this.StudentID == 54)
									{
										if (this.InstrumentBag[this.ClubMemberID].transform.parent != null)
										{
											this.InstrumentBag[this.ClubMemberID].transform.parent = null;
											this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 23f);
											this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, -90f, 0f);
										}
										this.Drumsticks[0].SetActive(true);
										this.Drumsticks[1].SetActive(true);
										if (!this.StudentManager.PracticeMusic.isPlaying)
										{
											this.CharacterAnimation.CrossFade("f02_drumsIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 114.5f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsCelebrate_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 108f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 96f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsPlaySlow_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 80f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 38f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsPlay_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 46f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 16f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsPlay_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 0f)
										{
											this.CharacterAnimation.CrossFade("f02_drumsIdle_00");
										}
									}
									else if (this.StudentID == 55)
									{
										if (this.InstrumentBag[this.ClubMemberID].transform.parent != null)
										{
											this.InstrumentBag[this.ClubMemberID].transform.parent = null;
											this.InstrumentBag[this.ClubMemberID].transform.position = new Vector3(5.5825f, 4.01f, 24f);
											this.InstrumentBag[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, -90f, 0f);
										}
										if (!this.StudentManager.PracticeMusic.isPlaying)
										{
											this.CharacterAnimation.CrossFade("f02_keysIdle_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 114.5f)
										{
											this.CharacterAnimation.CrossFade("f02_keysCelebrate_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 80f)
										{
											this.CharacterAnimation.CrossFade("f02_keysWait_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 24f)
										{
											this.CharacterAnimation.CrossFade("f02_keysPlay_00");
										}
										else if (this.StudentManager.PracticeMusic.time > 0f)
										{
											this.CharacterAnimation.CrossFade("f02_keysWait_00");
										}
									}
								}
								else if (this.Club == ClubType.Science)
								{
									if (this.ClubAttire && !this.GoAway)
									{
										if (this.SciencePhase == 0)
										{
											this.CharacterAnimation.CrossFade(this.ClubAnim);
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.RummageAnim);
										}
										if ((double)Vector3.Distance(base.transform.position, this.CurrentDestination.position) < 0.5)
										{
											if (this.SciencePhase == 0)
											{
												if (this.StudentID == 62)
												{
													this.ScienceProps[0].SetActive(true);
												}
												else if (this.StudentID == 63)
												{
													this.ScienceProps[1].SetActive(true);
													this.ScienceProps[2].SetActive(true);
												}
												else if (this.StudentID == 64)
												{
													this.ScienceProps[0].SetActive(true);
												}
											}
											if (this.StudentID > 61)
											{
												this.ClubTimer += Time.deltaTime;
												if (this.ClubTimer > 60f)
												{
													this.ClubTimer = 0f;
													this.SciencePhase++;
													if (this.SciencePhase == 1)
													{
														this.ClubTimer = 50f;
														this.OriginalPosition = this.CurrentDestination.transform.position;
														this.OriginalRotation = this.CurrentDestination.transform.rotation;
														this.CurrentDestination.transform.position = this.StudentManager.SupplySpots[this.StudentID - 61].position;
														this.Pathfinding.target.position = this.StudentManager.SupplySpots[this.StudentID - 61].position;
														this.CurrentDestination.transform.rotation = this.StudentManager.SupplySpots[this.StudentID - 61].rotation;
														this.Pathfinding.target.rotation = this.StudentManager.SupplySpots[this.StudentID - 61].rotation;
														foreach (GameObject gameObject3 in this.ScienceProps)
														{
															if (gameObject3 != null)
															{
																gameObject3.SetActive(false);
															}
														}
													}
													else
													{
														this.SciencePhase = 0;
														this.ClubTimer = 0f;
														this.CurrentDestination.transform.position = this.OriginalPosition;
														this.Pathfinding.target.position = this.OriginalPosition;
														this.CurrentDestination.transform.rotation = this.OriginalRotation;
														this.Pathfinding.target.rotation = this.OriginalRotation;
													}
												}
											}
										}
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
									}
								}
								else if (this.Club == ClubType.Sports)
								{
									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
									if (this.ClubActivityPhase == 0)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase++;
											this.ClubAnim = "stretch_01";
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									else if (this.ClubActivityPhase == 1)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase++;
											this.ClubAnim = "stretch_02";
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									else if (this.ClubActivityPhase == 2)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.WalkAnim = "trackJog_00";
											this.Hurry = true;
											this.ClubActivityPhase++;
											this.CharacterAnimation[this.ClubAnim].time = 0f;
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									else if (this.ClubActivityPhase < 14)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase++;
											this.CharacterAnimation[this.ClubAnim].time = 0f;
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									else if (this.ClubActivityPhase == 14)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.WalkAnim = this.OriginalWalkAnim;
											this.Hurry = false;
											this.ClubActivityPhase = 0;
											this.ClubAnim = "stretch_00";
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
										}
									}
									else if (this.ClubActivityPhase == 15)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= 1f && this.MyController.radius > 0f)
										{
											this.MyRenderer.updateWhenOffscreen = true;
											this.Obstacle.enabled = false;
											this.MyController.radius = 0f;
											this.Distracted = true;
										}
										if (this.CharacterAnimation[this.ClubAnim].time >= 2f)
										{
											float value = this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0) + Time.deltaTime * 200f;
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, value);
										}
										if (this.CharacterAnimation[this.ClubAnim].time >= 5f)
										{
											this.ClubActivityPhase++;
										}
									}
									else if (this.ClubActivityPhase == 16)
									{
										if ((double)this.CharacterAnimation[this.ClubAnim].time >= 6.1)
										{
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100f);
											this.Cosmetic.MaleHair[this.Cosmetic.Hairstyle].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100f);
											GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.BigWaterSplash, this.RightHand.transform.position, Quaternion.identity);
											gameObject4.transform.eulerAngles = new Vector3(-90f, gameObject4.transform.eulerAngles.y, gameObject4.transform.eulerAngles.z);
											this.SetSplashes(true);
											this.ClubActivityPhase++;
										}
									}
									else if (this.ClubActivityPhase == 17)
									{
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.WalkAnim = "poolSwim_00";
											this.ClubAnim = "poolSwim_00";
											this.ClubActivityPhase++;
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase - 2);
											base.transform.position = this.Hips.transform.position;
											this.Character.transform.localPosition = new Vector3(0f, -0.25f, 0f);
											Physics.SyncTransforms();
											this.CharacterAnimation.Play(this.WalkAnim);
										}
									}
									else if (this.ClubActivityPhase == 18)
									{
										this.ClubActivityPhase++;
										this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase - 2);
										this.DistanceToDestination = 100f;
									}
									else if (this.ClubActivityPhase == 19)
									{
										this.ClubAnim = "poolExit_00";
										if (this.CharacterAnimation[this.ClubAnim].time >= 0.1f)
										{
											this.SetSplashes(false);
										}
										if (this.CharacterAnimation[this.ClubAnim].time >= 4.66666f)
										{
											float value2 = this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0) - Time.deltaTime * 200f;
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, value2);
										}
										if (this.CharacterAnimation[this.ClubAnim].time >= this.CharacterAnimation[this.ClubAnim].length)
										{
											this.ClubActivityPhase = 15;
											this.ClubAnim = "poolDive_00";
											this.MyController.radius = 0.1f;
											this.WalkAnim = this.OriginalWalkAnim;
											this.MyRenderer.updateWhenOffscreen = false;
											this.Character.transform.localPosition = new Vector3(0f, 0f, 0f);
											this.Cosmetic.Goggles[this.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
											this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
											base.transform.position = new Vector3(this.Hips.position.x, 4f, this.Hips.position.z);
											Physics.SyncTransforms();
											this.CharacterAnimation.Play(this.IdleAnim);
											this.Distracted = false;
											if (this.Clock.Period == 2 || this.Clock.Period == 4)
											{
												Debug.Log("Sprinting 6");
												this.Pathfinding.speed = 4f;
											}
										}
									}
								}
								else if (this.Club == ClubType.Gardening)
								{
									if (this.WateringCan.transform.parent != this.RightHand)
									{
										this.WateringCan.transform.parent = this.RightHand;
										this.WateringCan.transform.localPosition = new Vector3(0.14f, -0.15f, -0.05f);
										this.WateringCan.transform.localEulerAngles = new Vector3(10f, 15f, 45f);
									}
									this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;
									if (this.PatrolTimer >= this.CharacterAnimation[this.ClubAnim].length)
									{
										this.PatrolID++;
										if (this.PatrolID == this.StudentManager.Patrols.List[this.StudentID].childCount)
										{
											this.PatrolID = 0;
										}
										this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
										this.Pathfinding.target = this.CurrentDestination;
										this.PatrolTimer = 0f;
										this.WateringCan.transform.parent = this.Hips;
										this.WateringCan.transform.localPosition = new Vector3(0f, 0.0135f, -0.184f);
										this.WateringCan.transform.localEulerAngles = new Vector3(0f, 90f, 30f);
									}
								}
								else if (this.Club == ClubType.Gaming)
								{
									if (this.Phase < 8)
									{
										if (this.StudentID == 36 && !this.SmartPhone.activeInHierarchy)
										{
											this.SmartPhone.SetActive(true);
											this.SmartPhone.transform.localPosition = new Vector3(0.0566666f, -0.02f, 0f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(10f, 115f, 180f);
										}
									}
									else
									{
										if (!this.ClubManager.GameScreens[this.ClubMemberID].activeInHierarchy)
										{
											this.ClubManager.GameScreens[this.ClubMemberID].SetActive(true);
											this.MyController.radius = 0.2f;
										}
										if (this.SmartPhone.activeInHierarchy)
										{
											this.SmartPhone.SetActive(false);
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.SitAndSocialize)
							{
								if (this.Paranoia > 1.66666f)
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else
								{
									this.StudentManager.ConvoManager.CheckMe(this.StudentID);
									if (this.Alone)
									{
										if (!this.Male)
										{
											this.CharacterAnimation.CrossFade("f02_standTexting_00");
										}
										else
										{
											this.CharacterAnimation.CrossFade("standTexting_00");
										}
										if (!this.SmartPhone.activeInHierarchy)
										{
											this.SmartPhone.SetActive(true);
											this.SpeechLines.Stop();
										}
									}
									else
									{
										if (!this.InEvent && !this.SpeechLines.isPlaying)
										{
											this.SmartPhone.SetActive(false);
											this.SpeechLines.Play();
										}
										if (this.Club != ClubType.Photography)
										{
											this.CharacterAnimation.CrossFade(this.RandomAnim);
											if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
											{
												this.PickRandomAnim();
											}
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.RandomSleuthAnim);
											if (this.CharacterAnimation[this.RandomSleuthAnim].time >= this.CharacterAnimation[this.RandomSleuthAnim].length)
											{
												this.PickRandomSleuthAnim();
											}
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.SitAndEatBento)
							{
								if (!this.DiscCheck && this.Alarm < 100f)
								{
									if (!this.Ragdoll.Poisoned && (!this.Bento.activeInHierarchy || this.Bento.transform.parent == null))
									{
										this.SmartPhone.SetActive(false);
										if (!this.Male)
										{
											this.Bento.transform.parent = this.LeftItemParent;
											this.Bento.transform.localPosition = new Vector3(-0.025f, -0.105f, 0f);
											this.Bento.transform.localEulerAngles = new Vector3(0f, 165f, 82.5f);
										}
										else
										{
											this.Bento.transform.parent = this.LeftItemParent;
											this.Bento.transform.localPosition = new Vector3(-0.05f, -0.085f, 0f);
											this.Bento.transform.localEulerAngles = new Vector3(-3.2f, -24.4f, -94f);
										}
										this.Chopsticks[0].SetActive(true);
										this.Chopsticks[1].SetActive(true);
										this.Bento.SetActive(true);
										this.Lid.SetActive(false);
										this.MyBento.Prompt.Hide();
										this.MyBento.Prompt.enabled = false;
										if (this.MyBento.Tampered)
										{
											if (this.MyBento.Emetic)
											{
												this.Emetic = true;
											}
											else if (this.MyBento.Lethal)
											{
												this.Lethal = true;
											}
											else if (this.MyBento.Tranquil)
											{
												this.Sedated = true;
											}
											else if (this.MyBento.Headache)
											{
												this.Headache = true;
											}
											this.Distracted = true;
										}
									}
									if (!this.Emetic && !this.Lethal && !this.Sedated && !this.Headache)
									{
										this.CharacterAnimation.CrossFade(this.EatAnim);
										if (this.FollowTarget != null && this.FollowTarget.CurrentAction != StudentActionType.SitAndEatBento && !this.FollowTarget.Dying)
										{
											Debug.Log("Osana is no longer eating, so Raibaru is now following Osana.");
											this.EmptyHands();
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											ScheduleBlock scheduleBlock3 = this.ScheduleBlocks[4];
											scheduleBlock3.destination = "Follow";
											scheduleBlock3.action = "Follow";
											this.GetDestinations();
											this.Pathfinding.target = this.FollowTarget.transform;
											this.CurrentDestination = this.FollowTarget.transform;
										}
									}
									else if (this.Emetic)
									{
										if (!this.Private)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
											this.CharacterAnimation.CrossFade(this.EmeticAnim);
											this.Private = true;
											this.CanTalk = false;
										}
										if (this.CharacterAnimation[this.EmeticAnim].time >= 16f && this.StudentID == 10 && this.VomitPhase < 0)
										{
											this.Subtitle.UpdateLabel(SubtitleType.ObstaclePoisonReaction, 0, 9f);
											this.VomitPhase++;
										}
										if (this.CharacterAnimation[this.EmeticAnim].time >= this.CharacterAnimation[this.EmeticAnim].length)
										{
											Debug.Log(this.Name + " has ingested emetic poison, and should be going to a toilet.");
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											if (this.Male)
											{
												this.WalkAnim = "stomachPainWalk_00";
												this.StudentManager.GetMaleVomitSpot(this);
												this.Pathfinding.target = this.StudentManager.MaleVomitSpot;
												this.CurrentDestination = this.StudentManager.MaleVomitSpot;
											}
											else
											{
												this.WalkAnim = "f02_stomachPainWalk_00";
												this.StudentManager.GetFemaleVomitSpot(this);
												this.Pathfinding.target = this.StudentManager.FemaleVomitSpot;
												this.CurrentDestination = this.StudentManager.FemaleVomitSpot;
											}
											if (this.StudentID == 10)
											{
												this.Pathfinding.target = this.StudentManager.AltFemaleVomitSpot;
												this.CurrentDestination = this.StudentManager.AltFemaleVomitSpot;
												this.VomitDoor = this.StudentManager.AltFemaleVomitDoor;
											}
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											this.CharacterAnimation.CrossFade(this.WalkAnim);
											this.DistanceToDestination = 100f;
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.Pathfinding.speed = 2f;
											this.MyBento.Tampered = false;
											this.Vomiting = true;
											this.Routine = false;
											this.Chopsticks[0].SetActive(false);
											this.Chopsticks[1].SetActive(false);
											this.Bento.SetActive(false);
										}
									}
									else if (this.Lethal)
									{
										Debug.Log(this.Name + " is currently eating a poisoned bento.");
										if (!this.Private)
										{
											AudioSource component = base.GetComponent<AudioSource>();
											component.clip = this.PoisonDeathClip;
											component.Play();
											if (this.Male)
											{
												this.CharacterAnimation.CrossFade("poisonDeath_00");
												this.PoisonDeathAnim = "poisonDeath_00";
											}
											else
											{
												this.CharacterAnimation.CrossFade("f02_poisonDeath_00");
												this.PoisonDeathAnim = "f02_poisonDeath_00";
												this.Distracted = true;
											}
											this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
											this.MyRenderer.updateWhenOffscreen = true;
											this.Ragdoll.Poisoned = true;
											this.Private = true;
											this.Prompt.Hide();
											this.Prompt.enabled = false;
										}
										else if (this.StudentID == 11 && this.StudentManager.Students[1] != null && !this.StudentManager.Students[1].SenpaiWitnessingRivalDie && Vector3.Distance(base.transform.position, this.StudentManager.Students[1].transform.position) < 2f)
										{
											Debug.Log("Setting ''SenpaiWitnessingRivalDie'' to true.");
											this.StudentManager.Students[1].CharacterAnimation.CrossFade("witnessPoisoning_00");
											this.StudentManager.Students[1].CurrentDestination = this.StudentManager.LunchSpots.List[1];
											this.StudentManager.Students[1].Pathfinding.target = this.StudentManager.LunchSpots.List[1];
											this.StudentManager.Students[1].MyRenderer.updateWhenOffscreen = true;
											this.StudentManager.Students[1].SenpaiWitnessingRivalDie = true;
											this.StudentManager.Students[1].Distracted = true;
											this.StudentManager.Students[1].Routine = false;
										}
										if (!this.Distracted && this.CharacterAnimation[this.PoisonDeathAnim].time >= 2.5f)
										{
											this.Distracted = true;
										}
										if (this.CharacterAnimation[this.PoisonDeathAnim].time >= 17.5f && this.Bento.activeInHierarchy)
										{
											this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
											this.Police.Corpses++;
											GameObjectUtils.SetLayerRecursively(base.gameObject, 11);
											base.tag = "Blood";
											this.Ragdoll.ChokingAnimation = true;
											this.Ragdoll.Disturbing = true;
											this.Ragdoll.Choking = true;
											this.Dying = true;
											this.MurderSuicidePhase = 100;
											this.SpawnAlarmDisc();
											this.Chopsticks[0].SetActive(false);
											this.Chopsticks[1].SetActive(false);
											this.Bento.SetActive(false);
										}
										if (this.CharacterAnimation[this.PoisonDeathAnim].time >= this.CharacterAnimation[this.PoisonDeathAnim].length)
										{
											this.BecomeRagdoll();
											this.DeathType = DeathType.Poison;
											this.Ragdoll.Choking = false;
											if (this.StudentManager.Students[1].SenpaiWitnessingRivalDie)
											{
												this.Ragdoll.Prompt.Hide();
												this.Ragdoll.Prompt.enabled = false;
											}
										}
									}
									else if (this.Sedated)
									{
										if (!this.Private)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
											this.CharacterAnimation.CrossFade(this.HeadacheAnim);
											this.CanTalk = false;
											this.Private = true;
										}
										if (this.CharacterAnimation[this.HeadacheAnim].time >= this.CharacterAnimation[this.HeadacheAnim].length)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											if (this.Male)
											{
												this.SprintAnim = "headacheWalk_00";
												this.RelaxAnim = "infirmaryRest_00";
											}
											else
											{
												this.SprintAnim = "f02_headacheWalk_00";
												this.RelaxAnim = "f02_infirmaryRest_00";
											}
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											this.CharacterAnimation.CrossFade(this.SprintAnim);
											this.DistanceToDestination = 100f;
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.Pathfinding.speed = 2f;
											this.MyBento.Tampered = false;
											this.Distracted = true;
											this.Private = true;
											ScheduleBlock scheduleBlock4 = this.ScheduleBlocks[4];
											scheduleBlock4.destination = "InfirmaryBed";
											scheduleBlock4.action = "Relax";
											this.GetDestinations();
											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];
											this.Chopsticks[0].SetActive(false);
											this.Chopsticks[1].SetActive(false);
											this.Bento.SetActive(false);
										}
									}
									else if (this.Headache)
									{
										if (!this.Private)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
											this.CharacterAnimation.CrossFade(this.HeadacheAnim);
											this.CanTalk = false;
											this.Private = true;
										}
										if (this.CharacterAnimation[this.HeadacheAnim].time >= this.CharacterAnimation[this.HeadacheAnim].length)
										{
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											if (this.Male)
											{
												this.SprintAnim = "headacheWalk_00";
												this.IdleAnim = "headacheIdle_00";
											}
											else
											{
												this.SprintAnim = "f02_headacheWalk_00";
												this.IdleAnim = "f02_headacheIdle_00";
											}
											this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
											this.CharacterAnimation.CrossFade(this.SprintAnim);
											this.DistanceToDestination = 100f;
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.Pathfinding.speed = 2f;
											this.MyBento.Tampered = false;
											this.SeekingMedicine = true;
											this.Routine = false;
											this.Private = true;
											this.Chopsticks[0].SetActive(false);
											this.Chopsticks[1].SetActive(false);
											this.Bento.SetActive(false);
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.ChangeShoes)
							{
								if (this.MeetTime == 0f)
								{
									if (this.StudentManager.LoveManager.Suitor == this && this.StudentManager.LoveManager.LeftNote)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
										this.CharacterAnimation.CrossFade("keepNote_00");
										this.ShoeRemoval.Locker.GetComponent<Animation>().CrossFade("lockerKeepNote");
										this.Pathfinding.canSearch = false;
										this.Pathfinding.canMove = false;
										this.Confessing = true;
										this.CanTalk = false;
										this.Routine = false;
									}
									else
									{
										this.SmartPhone.SetActive(false);
										this.Pathfinding.canSearch = false;
										this.Pathfinding.canMove = false;
										this.ShoeRemoval.enabled = true;
										this.CanTalk = false;
										this.Routine = false;
										this.ShoeRemoval.LeavingSchool();
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.GradePapers)
							{
								this.CharacterAnimation.CrossFade("f02_deskWrite");
								this.GradingPaper.Writing = true;
								this.Obstacle.enabled = true;
								this.Pen.SetActive(true);
							}
							else if (this.Actions[this.Phase] == StudentActionType.Patrol)
							{
								this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;
								this.CharacterAnimation.CrossFade(this.PatrolAnim);
								if (this.PatrolTimer >= this.CharacterAnimation[this.PatrolAnim].length)
								{
									this.PatrolID++;
									if (this.PatrolID == this.StudentManager.Patrols.List[this.StudentID].childCount)
									{
										this.PatrolID = 0;
									}
									this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
									this.Pathfinding.target = this.CurrentDestination;
									if (this.StudentID == 39)
									{
										this.CharacterAnimation["f02_topHalfTexting_00"].weight = 1f;
									}
									this.PatrolTimer = 0f;
								}
								if (this.Restless)
								{
									this.SewTimer += Time.deltaTime;
									if (this.SewTimer > 20f)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
										ScheduleBlock scheduleBlock5 = this.ScheduleBlocks[this.Phase];
										scheduleBlock5.destination = "Sketch";
										scheduleBlock5.action = "Sketch";
										this.GetDestinations();
										this.CurrentDestination = this.SketchPosition;
										this.Pathfinding.target = this.SketchPosition;
										this.SewTimer = 0f;
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Read)
							{
								if (this.ReadPhase == 0)
								{
									if (this.StudentID == 5)
									{
										this.HorudaCollider.gameObject.SetActive(true);
									}
									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
									this.CharacterAnimation.CrossFade(this.BookSitAnim);
									if (this.CharacterAnimation[this.BookSitAnim].time > this.CharacterAnimation[this.BookSitAnim].length)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
										this.CharacterAnimation.CrossFade(this.BookReadAnim);
										this.ReadPhase++;
									}
									else if (this.CharacterAnimation[this.BookSitAnim].time > 1f)
									{
										this.OccultBook.SetActive(true);
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Texting)
							{
								this.CharacterAnimation.CrossFade("f02_midoriTexting_00");
								if (this.SmartPhone.transform.localPosition.x != 0.02f)
								{
									this.SmartPhone.transform.localPosition = new Vector3(0.02f, -0.0075f, 0f);
									this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, -164f);
								}
								if (!this.SmartPhone.activeInHierarchy && base.transform.position.y > 11f)
								{
									this.SmartPhone.SetActive(true);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Mourn)
							{
								this.CharacterAnimation.CrossFade("f02_brokenSit_00");
							}
							else if (this.Actions[this.Phase] == StudentActionType.Cuddle)
							{
								if (Vector3.Distance(base.transform.position, this.Partner.transform.position) < 1f)
								{
									ParticleSystem.EmissionModule emission = this.Hearts.emission;
									if (!emission.enabled)
									{
										emission.enabled = true;
										if (!this.Male)
										{
											this.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
										}
										else
										{
											this.Cosmetic.MyRenderer.materials[this.Cosmetic.FaceID].SetFloat("_BlendAmount", 1f);
										}
									}
									this.CharacterAnimation.CrossFade(this.CuddleAnim);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Teaching)
							{
								if (this.Clock.Period != 2 && this.Clock.Period != 4)
								{
									this.CharacterAnimation.CrossFade("f02_teacherPodium_00");
								}
								else
								{
									if (!this.SpeechLines.isPlaying)
									{
										this.SpeechLines.Play();
									}
									this.CharacterAnimation.CrossFade(this.TeachAnim);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.SearchPatrol)
							{
								if (this.PatrolID == 0 && this.StudentManager.CommunalLocker.RivalPhone.gameObject.activeInHierarchy && !this.EndSearch)
								{
									if (SchemeGlobals.GetSchemeStage(1) == 7)
									{
										SchemeGlobals.SetSchemeStage(1, 8);
										this.Yandere.PauseScreen.Schemes.UpdateInstructions();
									}
									this.CharacterAnimation.CrossFade(this.DiscoverPhoneAnim);
									this.Subtitle.UpdateLabel(this.LostPhoneSubtitleType, 2, 5f);
									this.EndSearch = true;
									this.Routine = false;
								}
								if (!this.EndSearch)
								{
									this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;
									this.CharacterAnimation.CrossFade(this.SearchPatrolAnim);
									if (this.PatrolTimer >= this.CharacterAnimation[this.SearchPatrolAnim].length)
									{
										this.PatrolID++;
										if (this.PatrolID == this.StudentManager.SearchPatrols.List[this.StudentID].childCount)
										{
											this.PatrolID = 0;
										}
										this.CurrentDestination = this.StudentManager.SearchPatrols.List[this.StudentID].GetChild(this.PatrolID);
										this.Pathfinding.target = this.CurrentDestination;
										this.DistanceToDestination = 100f;
										if (this.StudentID == 39)
										{
											this.CharacterAnimation["f02_topHalfTexting_00"].weight = 1f;
										}
										this.PatrolTimer = 0f;
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Wait)
							{
								if (!this.Cigarette.active && TaskGlobals.GetTaskStatus(81) == 3)
								{
									this.WaitAnim = "f02_smokeAttempt_00";
									this.SmartPhone.SetActive(false);
									this.Cigarette.SetActive(true);
									this.Lighter.SetActive(true);
								}
								this.CharacterAnimation.CrossFade(this.WaitAnim);
							}
							else if (this.Actions[this.Phase] == StudentActionType.Clean)
							{
								this.CleanTimer += Time.deltaTime;
								if (this.CleaningRole == 5)
								{
									if (this.CleanID == 0)
									{
										this.CharacterAnimation.CrossFade(this.CleanAnims[1]);
									}
									else
									{
										if (!this.StudentManager.RoofFenceUp)
										{
											this.Prompt.Label[0].text = "     Push";
											this.Prompt.HideButton[0] = false;
											this.Pushable = true;
										}
										this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);
										if ((double)this.CleanTimer >= 1.166666 && (double)this.CleanTimer <= 6.166666 && !this.ChalkDust.isPlaying)
										{
											this.ChalkDust.Play();
										}
									}
								}
								else if (this.CleaningRole == 4)
								{
									this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);
									if (!this.Drownable)
									{
										this.Prompt.Label[0].text = "     Drown";
										this.Prompt.HideButton[0] = false;
										this.Drownable = true;
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);
								}
								if (this.CleanTimer >= this.CharacterAnimation[this.CleanAnims[this.CleaningRole]].length)
								{
									this.CleanID++;
									if (this.CleanID == this.CleaningSpot.childCount)
									{
										this.CleanID = 0;
									}
									this.CurrentDestination = this.CleaningSpot.GetChild(this.CleanID);
									this.Pathfinding.target = this.CurrentDestination;
									this.DistanceToDestination = 100f;
									this.CleanTimer = 0f;
									if (this.Pushable)
									{
										this.Prompt.Label[0].text = "     Talk";
										this.Pushable = false;
									}
									if (this.Drownable)
									{
										this.Drownable = false;
										this.StudentManager.UpdateMe(this.StudentID);
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Graffiti)
							{
								if (this.KilledMood)
								{
									this.Subtitle.UpdateLabel(SubtitleType.KilledMood, 0, 5f);
									this.GraffitiPhase = 4;
									this.KilledMood = false;
								}
								if (this.GraffitiPhase == 0)
								{
									AudioSource.PlayClipAtPoint(this.BullyGiggles[UnityEngine.Random.Range(0, this.BullyGiggles.Length)], this.Head.position);
									this.CharacterAnimation.CrossFade("f02_bullyDesk_00");
									this.SmartPhone.SetActive(false);
									this.GraffitiPhase++;
								}
								else if (this.GraffitiPhase == 1)
								{
									if (this.CharacterAnimation["f02_bullyDesk_00"].time >= 8f)
									{
										this.StudentManager.Graffiti[this.BullyID].SetActive(true);
										this.GraffitiPhase++;
									}
								}
								else if (this.GraffitiPhase == 2)
								{
									if (this.CharacterAnimation["f02_bullyDesk_00"].time >= 9.66666f)
									{
										AudioSource.PlayClipAtPoint(this.BullyGiggles[UnityEngine.Random.Range(0, this.BullyGiggles.Length)], this.Head.position);
										this.GraffitiPhase++;
									}
								}
								else if (this.GraffitiPhase == 3)
								{
									if (this.CharacterAnimation["f02_bullyDesk_00"].time >= this.CharacterAnimation["f02_bullyDesk_00"].length)
									{
										this.GraffitiPhase++;
									}
								}
								else if (this.GraffitiPhase == 4)
								{
									this.DistanceToDestination = 100f;
									ScheduleBlock scheduleBlock6 = this.ScheduleBlocks[2];
									scheduleBlock6.destination = "Patrol";
									scheduleBlock6.action = "Patrol";
									this.GetDestinations();
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.SmartPhone.SetActive(true);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Bully)
							{
								if (this.StudentManager.Students[this.StudentManager.VictimID] != null)
								{
									if (this.StudentManager.Students[this.StudentManager.VictimID].Distracted || this.StudentManager.Students[this.StudentManager.VictimID].Tranquil)
									{
										this.StudentManager.NoBully[this.BullyID] = true;
										this.KilledMood = true;
									}
									if (this.KilledMood)
									{
										this.Subtitle.UpdateLabel(SubtitleType.KilledMood, 0, 5f);
										this.BullyPhase = 4;
										this.KilledMood = false;
										this.BullyDust.Stop();
									}
									if (this.StudentManager.Students[81] == null)
									{
										ScheduleBlock scheduleBlock7 = this.ScheduleBlocks[4];
										scheduleBlock7.destination = "Patrol";
										scheduleBlock7.action = "Patrol";
										this.GetDestinations();
										this.CurrentDestination = this.Destinations[this.Phase];
										this.Pathfinding.target = this.Destinations[this.Phase];
									}
									else
									{
										this.SmartPhone.SetActive(false);
										if (this.BullyID == 1)
										{
											if (this.BullyPhase == 0)
											{
												this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
												this.Scrubber.SetActive(true);
												this.Eraser.SetActive(true);
												this.StudentManager.Students[this.StudentManager.VictimID].CharacterAnimation.CrossFade(this.StudentManager.Students[this.StudentManager.VictimID].BullyVictimAnim);
												this.StudentManager.Students[this.StudentManager.VictimID].Routine = false;
												this.CharacterAnimation.CrossFade("f02_bullyEraser_00");
												this.BullyPhase++;
											}
											else if (this.BullyPhase == 1)
											{
												if (this.CharacterAnimation["f02_bullyEraser_00"].time >= 0.833333f)
												{
													this.BullyDust.Play();
													this.BullyPhase++;
												}
											}
											else if (this.BullyPhase == 2)
											{
												if (this.CharacterAnimation["f02_bullyEraser_00"].time >= this.CharacterAnimation["f02_bullyEraser_00"].length)
												{
													AudioSource.PlayClipAtPoint(this.BullyLaughs[this.BullyID], this.Head.position);
													this.CharacterAnimation.CrossFade("f02_bullyLaugh_00");
													this.Scrubber.SetActive(false);
													this.Eraser.SetActive(false);
													this.BullyPhase++;
												}
											}
											else if (this.BullyPhase == 3)
											{
												if (this.CharacterAnimation["f02_bullyLaugh_00"].time >= this.CharacterAnimation["f02_bullyLaugh_00"].length)
												{
													this.BullyPhase++;
												}
											}
											else if (this.BullyPhase == 4)
											{
												this.StudentManager.Students[this.StudentManager.VictimID].Routine = true;
												ScheduleBlock scheduleBlock8 = this.ScheduleBlocks[4];
												scheduleBlock8.destination = "LunchSpot";
												scheduleBlock8.action = "Wait";
												this.GetDestinations();
												this.CurrentDestination = this.Destinations[this.Phase];
												this.Pathfinding.target = this.Destinations[this.Phase];
												this.SmartPhone.SetActive(true);
												this.Scrubber.SetActive(false);
												this.Eraser.SetActive(false);
											}
										}
										else
										{
											if (this.StudentManager.Students[81].BullyPhase < 2)
											{
												if (this.GiggleTimer == 0f)
												{
													AudioSource.PlayClipAtPoint(this.BullyGiggles[UnityEngine.Random.Range(0, this.BullyGiggles.Length)], this.Head.position);
													this.GiggleTimer = 5f;
												}
												this.GiggleTimer = Mathf.MoveTowards(this.GiggleTimer, 0f, Time.deltaTime);
												this.CharacterAnimation.CrossFade("f02_bullyGiggle_00");
											}
											else if (this.StudentManager.Students[81].BullyPhase < 4)
											{
												if (this.LaughTimer == 0f)
												{
													AudioSource.PlayClipAtPoint(this.BullyLaughs[this.BullyID], this.Head.position);
													this.LaughTimer = 5f;
												}
												this.LaughTimer = Mathf.MoveTowards(this.LaughTimer, 0f, Time.deltaTime);
												this.CharacterAnimation.CrossFade("f02_bullyLaugh_00");
											}
											if (this.CharacterAnimation["f02_bullyLaugh_00"].time >= this.CharacterAnimation["f02_bullyLaugh_00"].length || this.StudentManager.Students[81].BullyPhase == 4 || this.BullyPhase == 4)
											{
												this.DistanceToDestination = 100f;
												ScheduleBlock scheduleBlock9 = this.ScheduleBlocks[4];
												scheduleBlock9.destination = "Patrol";
												scheduleBlock9.action = "Patrol";
												this.GetDestinations();
												this.CurrentDestination = this.Destinations[this.Phase];
												this.Pathfinding.target = this.Destinations[this.Phase];
												this.SmartPhone.SetActive(true);
											}
										}
									}
								}
								else
								{
									this.DistanceToDestination = 100f;
									ScheduleBlock scheduleBlock10 = this.ScheduleBlocks[4];
									scheduleBlock10.destination = "Patrol";
									scheduleBlock10.action = "Patrol";
									this.GetDestinations();
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.SmartPhone.SetActive(true);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Follow)
							{
								if (this.FollowTarget.Routine && !this.FollowTarget.InEvent && this.FollowTarget.CurrentAction == StudentActionType.Clean && this.FollowTarget.DistanceToDestination < 1f)
								{
									this.CharacterAnimation.CrossFade(this.CleanAnims[this.CleaningRole]);
									this.Scrubber.SetActive(true);
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent && this.FollowTarget.CurrentAction == StudentActionType.Socializing && this.FollowTarget.DistanceToDestination < 1f)
								{
									if (this.FollowTarget.Alone || this.FollowTarget.Meeting)
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
										this.SpeechLines.Stop();
									}
									else
									{
										this.Scrubber.SetActive(false);
										this.SpeechLines.Play();
										this.CharacterAnimation.CrossFade(this.RandomAnim);
										if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
										{
											this.PickRandomAnim();
										}
									}
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent && this.FollowTarget.CurrentAction == StudentActionType.SitAndTakeNotes && this.FollowTarget.DistanceToDestination < 1f)
								{
									Debug.Log("Raibaru just changed her destination to class.");
									ScheduleBlock scheduleBlock11 = this.ScheduleBlocks[this.Phase];
									scheduleBlock11.destination = "Seat";
									scheduleBlock11.action = "SitAndTakeNotes";
									this.Actions[this.Phase] = StudentActionType.SitAndTakeNotes;
									this.CurrentAction = StudentActionType.SitAndTakeNotes;
									this.GetDestinations();
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent && this.FollowTarget.CurrentAction == StudentActionType.SitAndEatBento && this.FollowTarget.DistanceToDestination < 1f)
								{
									Debug.Log("Raibaru just changed her destination to lunch.");
									ScheduleBlock scheduleBlock12 = this.ScheduleBlocks[this.Phase];
									scheduleBlock12.destination = "LunchSpot";
									scheduleBlock12.action = "SitAndEatBento";
									this.Actions[this.Phase] = StudentActionType.SitAndEatBento;
									this.CurrentAction = StudentActionType.SitAndEatBento;
									this.GetDestinations();
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
								else if (this.FollowTarget.Routine && !this.FollowTarget.InEvent && this.FollowTarget.Phase == 8 && this.FollowTarget.DistanceToDestination < 1f)
								{
									Debug.Log("Raibaru just changed her destination to the lockers.");
									ScheduleBlock scheduleBlock13 = this.ScheduleBlocks[this.Phase];
									scheduleBlock13.destination = "Locker";
									scheduleBlock13.action = "Shoes";
									this.Actions[this.Phase] = StudentActionType.ChangeShoes;
									this.CurrentAction = StudentActionType.ChangeShoes;
									this.GetDestinations();
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
								}
								else if (this.StudentManager.LoveManager.RivalWaiting && this.FollowTarget.DistanceToDestination < 1f)
								{
									Debug.Log("Raibaru just changed her destination to the bush near the matchmaking spot.");
									this.CurrentDestination = this.StudentManager.LoveManager.FriendWaitSpot;
									this.Pathfinding.target = this.StudentManager.LoveManager.FriendWaitSpot;
									this.CharacterAnimation.CrossFade(this.IdleAnim);
								}
								else if (this.FollowTarget.ConfessPhase == 5)
								{
									Debug.Log("Raibaru just changed her action to Sketch and her destination to Paint.");
									ScheduleBlock scheduleBlock14 = this.ScheduleBlocks[this.Phase];
									scheduleBlock14.destination = "Paint";
									scheduleBlock14.action = "Sketch";
									scheduleBlock14.time = 99f;
									this.GetDestinations();
									this.CurrentDestination = this.Destinations[this.Phase];
									this.Pathfinding.target = this.Destinations[this.Phase];
									this.MyController.radius = 0.1f;
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
									this.SpeechLines.Stop();
									if (this.SlideIn)
									{
										this.MoveTowardsTarget(this.CurrentDestination.position);
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Sulk)
							{
								if (this.Male)
								{
									this.CharacterAnimation.CrossFade("delinquentSulk_00");
								}
								else
								{
									this.CharacterAnimation.CrossFade("f02_railingSulk_0" + this.SulkPhase, 1f);
									this.SulkTimer += Time.deltaTime;
									if (this.SulkTimer > 7.66666f)
									{
										this.SulkTimer = 0f;
										this.SulkPhase++;
										if (this.SulkPhase == 3)
										{
											this.SulkPhase = 0;
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Sleuth)
							{
								if (this.StudentManager.SleuthPhase != 3)
								{
									this.StudentManager.ConvoManager.CheckMe(this.StudentID);
									if (this.Alone)
									{
										if (this.Male)
										{
											this.CharacterAnimation.CrossFade("standTexting_00");
										}
										else
										{
											this.CharacterAnimation.CrossFade("f02_standTexting_00");
										}
										if (this.Male)
										{
											this.SmartPhone.transform.localPosition = new Vector3(0.025f, 0.0025f, 0.025f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 180f);
										}
										else
										{
											this.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.01f, 0.01f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 165f);
										}
										this.SmartPhone.SetActive(true);
										this.SpeechLines.Stop();
									}
									else
									{
										if (!this.SpeechLines.isPlaying)
										{
											this.SmartPhone.SetActive(false);
											this.SpeechLines.Play();
										}
										this.CharacterAnimation.CrossFade(this.RandomSleuthAnim, 1f);
										if (this.CharacterAnimation[this.RandomSleuthAnim].time >= this.CharacterAnimation[this.RandomSleuthAnim].length)
										{
											this.PickRandomSleuthAnim();
										}
										this.StudentManager.SleuthTimer += Time.deltaTime;
										if (this.StudentManager.SleuthTimer > 100f)
										{
											this.StudentManager.SleuthTimer = 0f;
											this.StudentManager.UpdateSleuths();
										}
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.SleuthScanAnim);
									if (this.CharacterAnimation[this.SleuthScanAnim].time >= this.CharacterAnimation[this.SleuthScanAnim].length)
									{
										this.GetSleuthTarget();
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Stalk)
							{
								this.CharacterAnimation.CrossFade(this.SleuthIdleAnim);
								if (this.DistanceToPlayer < 5f)
								{
									if (Vector3.Distance(base.transform.position, this.StudentManager.FleeSpots[0].position) > 10f)
									{
										this.Pathfinding.target = this.StudentManager.FleeSpots[0];
										this.CurrentDestination = this.StudentManager.FleeSpots[0];
									}
									else
									{
										this.Pathfinding.target = this.StudentManager.FleeSpots[1];
										this.CurrentDestination = this.StudentManager.FleeSpots[1];
									}
									Debug.Log("Sprinting 7");
									this.Pathfinding.speed = 4f;
									this.StalkerFleeing = true;
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Sketch)
							{
								this.CharacterAnimation.CrossFade(this.SketchAnim);
								this.Sketchbook.SetActive(true);
								this.Pencil.SetActive(true);
								if (this.Restless)
								{
									this.SewTimer += Time.deltaTime;
									if (this.SewTimer > 20f)
									{
										this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
										ScheduleBlock scheduleBlock15 = this.ScheduleBlocks[this.Phase];
										scheduleBlock15.destination = "Patrol";
										scheduleBlock15.action = "Patrol";
										this.GetDestinations();
										this.EmptyHands();
										this.PatrolID = 1;
										this.PatrolTimer = 0f;
										this.CharacterAnimation[this.PatrolAnim].time = 0f;
										this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
										this.Pathfinding.target = this.CurrentDestination;
										this.SewTimer = 0f;
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Sunbathe)
							{
								if (this.SunbathePhase == 0)
								{
									this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
									this.StudentManager.CommunalLocker.Open = true;
									this.StudentManager.CommunalLocker.SpawnSteamNoSideEffects(this);
									this.MustChangeClothing = true;
									this.ChangeClothingPhase++;
									this.SunbathePhase++;
								}
								else if (this.SunbathePhase == 1)
								{
									this.CharacterAnimation.CrossFade(this.StripAnim);
									this.Pathfinding.canSearch = false;
									this.Pathfinding.canMove = false;
									if (this.CharacterAnimation[this.StripAnim].time >= 1.5f)
									{
										if (this.Schoolwear != 2)
										{
											this.Schoolwear = 2;
											this.ChangeSchoolwear();
										}
										if (this.CharacterAnimation[this.StripAnim].time > this.CharacterAnimation[this.StripAnim].length)
										{
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.Stripping = false;
											if (!this.StudentManager.CommunalLocker.SteamCountdown)
											{
												this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
												this.Destinations[this.Phase] = this.StudentManager.SunbatheSpots[this.StudentID];
												this.Pathfinding.target = this.StudentManager.SunbatheSpots[this.StudentID];
												this.CurrentDestination = this.StudentManager.SunbatheSpots[this.StudentID];
												this.StudentManager.CommunalLocker.Student = null;
												this.SunbathePhase++;
											}
										}
									}
								}
								else if (this.SunbathePhase == 2)
								{
									this.MyRenderer.updateWhenOffscreen = true;
									this.CharacterAnimation.CrossFade("f02_sunbatheStart_00");
									this.SmartPhone.SetActive(false);
									if (this.CharacterAnimation["f02_sunbatheStart_00"].time >= this.CharacterAnimation["f02_sunbatheStart_00"].length)
									{
										this.MyController.radius = 0f;
										this.SunbathePhase++;
									}
								}
								else if (this.SunbathePhase == 3)
								{
									this.CharacterAnimation.CrossFade("f02_sunbatheLoop_00");
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Shock)
							{
								if (this.StudentManager.Students[36] == null)
								{
									this.Phase++;
								}
								else if (this.StudentManager.Students[36].Routine && this.StudentManager.Students[36].DistanceToDestination < 1f)
								{
									if (!this.StudentManager.GamingDoor.Open)
									{
										this.StudentManager.GamingDoor.OpenDoor();
									}
									ParticleSystem.EmissionModule emission2 = this.Hearts.emission;
									if (this.SmartPhone.activeInHierarchy)
									{
										this.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
										this.SmartPhone.SetActive(false);
										this.MyController.radius = 0f;
										emission2.rateOverTime = 5f;
										emission2.enabled = true;
										this.Hearts.Play();
									}
									this.CharacterAnimation.CrossFade("f02_peeking_0" + (this.StudentID - 80));
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.PatrolAnim);
									if (!this.SmartPhone.activeInHierarchy)
									{
										this.SmartPhone.SetActive(true);
										this.MyController.radius = 0.1f;
										if (this.BullyID == 2)
										{
											this.MyController.Move(base.transform.right * 1f * Time.timeScale * 0.2f);
										}
										else if (this.BullyID == 3)
										{
											this.MyController.Move(base.transform.right * -1f * Time.timeScale * 0.2f);
										}
										else if (this.BullyID == 4)
										{
											this.MyController.Move(base.transform.right * 1f * Time.timeScale * 0.2f);
										}
										else if (this.BullyID == 5)
										{
											this.MyController.Move(base.transform.right * -1f * Time.timeScale * 0.2f);
										}
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Miyuki)
							{
								if (this.StudentManager.MiyukiEnemy.Enemy.activeInHierarchy)
								{
									this.CharacterAnimation.CrossFade(this.MiyukiAnim);
									this.MiyukiTimer += Time.deltaTime;
									if (this.MiyukiTimer > 1f)
									{
										this.MiyukiTimer = 0f;
										this.Miyuki.Shoot();
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.VictoryAnim);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Meeting)
							{
								if (this.StudentID != 36)
								{
									this.StudentManager.Meeting = true;
									if (this.StudentManager.Speaker == this.StudentID)
									{
										if (!this.SpeechLines.isPlaying)
										{
											this.CharacterAnimation.CrossFade(this.RandomAnim);
											this.SpeechLines.Play();
										}
									}
									else
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
										if (this.SpeechLines.isPlaying)
										{
											this.SpeechLines.Stop();
										}
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.PeekAnim);
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Lyrics)
							{
								this.LyricsTimer += Time.deltaTime;
								if (this.LyricsPhase == 0)
								{
									this.CharacterAnimation.CrossFade("f02_writingLyrics_00");
									if (!this.Pencil.activeInHierarchy)
									{
										this.Pencil.SetActive(true);
									}
									if (this.LyricsTimer > 18f)
									{
										this.StudentManager.LyricsSpot.position = this.StudentManager.AirGuitarSpot.position;
										this.StudentManager.LyricsSpot.eulerAngles = this.StudentManager.AirGuitarSpot.eulerAngles;
										this.Pencil.SetActive(false);
										this.LyricsPhase = 1;
										this.LyricsTimer = 0f;
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade("f02_airGuitar_00");
									if (!this.AirGuitar.isPlaying)
									{
										this.AirGuitar.Play();
									}
									if (this.LyricsTimer > 18f)
									{
										this.StudentManager.LyricsSpot.position = this.StudentManager.OriginalLyricsSpot.position;
										this.StudentManager.LyricsSpot.eulerAngles = this.StudentManager.OriginalLyricsSpot.eulerAngles;
										this.AirGuitar.Stop();
										this.LyricsPhase = 0;
										this.LyricsTimer = 0f;
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Sew)
							{
								this.CharacterAnimation.CrossFade("sewing_00");
								this.PinkSeifuku.SetActive(true);
								if (this.SewTimer < 10f && TaskGlobals.GetTaskStatus(8) == 3)
								{
									this.SewTimer += Time.deltaTime;
									if (this.SewTimer > 10f)
									{
										UnityEngine.Object.Instantiate<GameObject>(this.Yandere.PauseScreen.DropsMenu.GetComponent<DropsScript>().InfoChanWindow.Drops[1], new Vector3(28.289f, 0.7718928f, 5.196f), Quaternion.identity);
									}
								}
							}
							else if (this.Actions[this.Phase] == StudentActionType.Paint)
							{
								this.Painting.material.color += new Color(0f, 0f, 0f, Time.deltaTime * 0.00066666f);
								this.CharacterAnimation.CrossFade(this.PaintAnim);
								this.Paintbrush.SetActive(true);
								this.Palette.SetActive(true);
							}
						}
						else
						{
							this.CurrentDestination = this.StudentManager.GoAwaySpots.List[this.StudentID];
							this.Pathfinding.target = this.StudentManager.GoAwaySpots.List[this.StudentID];
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							this.GoAwayTimer += Time.deltaTime;
							if (this.GoAwayTimer > 10f)
							{
								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];
								this.GoAwayTimer = 0f;
								this.GoAway = false;
							}
						}
					}
					else
					{
						if (this.MeetTimer == 0f)
						{
							if (this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood == 0f && (double)this.Yandere.Sanity >= 66.66666 && (this.CurrentDestination == this.StudentManager.MeetSpots.List[8] || this.CurrentDestination == this.StudentManager.MeetSpots.List[9] || this.CurrentDestination == this.StudentManager.MeetSpots.List[10]))
							{
								if (this.StudentID == 30)
								{
									this.StudentManager.OfferHelp.UpdateLocation();
									this.StudentManager.OfferHelp.enabled = true;
								}
								else if (this.StudentID == 5)
								{
									this.Yandere.BullyPhotoCheck();
									if (this.Yandere.BullyPhoto)
									{
										this.StudentManager.FragileOfferHelp.UpdateLocation();
										this.StudentManager.FragileOfferHelp.enabled = true;
									}
								}
							}
							if (!SchoolGlobals.RoofFence && base.transform.position.y > 11f)
							{
								this.Prompt.Label[0].text = "     Push";
								this.Prompt.HideButton[0] = false;
								this.Pushable = true;
							}
							if (this.CurrentDestination == this.StudentManager.FountainSpot)
							{
								this.Prompt.Label[0].text = "     Drown";
								this.Prompt.HideButton[0] = false;
								this.Drownable = true;
							}
						}
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.MeetTimer += Time.deltaTime;
						if (this.MeetTimer > 60f)
						{
							if (!this.Male)
							{
								this.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 4, 3f);
							}
							else if (this.StudentID == 28)
							{
								this.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 6, 3f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 4, 3f);
							}
							while (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
							{
								this.Phase++;
							}
							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];
							this.StopMeeting();
						}
					}
				}
			}
		}
		else
		{
			if (this.CurrentDestination != null)
			{
				this.DistanceToDestination = Vector3.Distance(base.transform.position, this.CurrentDestination.position);
			}
			if (this.Fleeing && !this.Dying)
			{
				if (!this.PinningDown)
				{
					if (this.Persona == PersonaType.Dangerous)
					{
						this.Yandere.Pursuer = this;
						Debug.Log("This student council member is running to intercept Yandere-chan.");
						if (this.StudentManager.CombatMinigame.Path > 3 && this.StudentManager.CombatMinigame.Path < 7)
						{
							this.ReturnToRoutine();
						}
					}
					if (this.Pathfinding.target != null)
					{
						this.DistanceToDestination = Vector3.Distance(base.transform.position, this.Pathfinding.target.position);
					}
					if (this.AlarmTimer > 0f)
					{
						this.AlarmTimer = Mathf.MoveTowards(this.AlarmTimer, 0f, Time.deltaTime);
						if (this.StudentID == 1)
						{
							Debug.Log("Senpai entered his scared animation.");
						}
						this.CharacterAnimation.CrossFade(this.ScaredAnim);
						if (this.AlarmTimer == 0f)
						{
							this.WalkBack = false;
							this.Alarmed = false;
						}
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						if (this.WitnessedMurder)
						{
							this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
						}
						else if (this.WitnessedCorpse)
						{
							this.targetRotation = Quaternion.LookRotation(new Vector3(this.Corpse.AllColliders[0].transform.position.x, base.transform.position.y, this.Corpse.AllColliders[0].transform.position.z) - base.transform.position);
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
						}
					}
					else
					{
						if (this.Persona == PersonaType.TeachersPet && this.WitnessedMurder && this.ReportPhase == 0 && this.StudentManager.Reporter == null && !this.Police.Called)
						{
							Debug.Log("Setting teacher as destination at beginning of Flee protocol.");
							this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
							this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;
							this.StudentManager.Reporter = this;
							this.ReportingMurder = true;
							this.DetermineCorpseLocation();
						}
						if (base.transform.position.y < -11f)
						{
							if (this.Persona != PersonaType.Coward && this.Persona != PersonaType.Evil && this.Persona != PersonaType.Fragile && this.OriginalPersona != PersonaType.Evil)
							{
								this.Police.Witnesses--;
								this.Police.Show = true;
								if (this.Countdown.gameObject.activeInHierarchy)
								{
									this.PhoneAddictGameOver();
								}
							}
							base.transform.position = new Vector3(base.transform.position.x, -100f, base.transform.position.z);
							base.gameObject.SetActive(false);
						}
						if (base.transform.position.z < -99f)
						{
							this.Prompt.Hide();
							this.Prompt.enabled = false;
							this.Safe = true;
						}
						if (this.DistanceToDestination > this.TargetDistance)
						{
							this.CharacterAnimation.CrossFade(this.SprintAnim);
							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;
							if (this.Yandere.Chased)
							{
								Debug.Log(this.Name + " is chasing Yandere-chan.");
								this.Pathfinding.repathRate = 0f;
								this.Pathfinding.speed = 5f;
								this.ChaseTimer += Time.deltaTime;
								if (this.ChaseTimer > 10f)
								{
									base.transform.position = this.Yandere.transform.position + this.Yandere.transform.forward * 1f;
									base.transform.LookAt(this.Yandere.transform.position);
									Physics.SyncTransforms();
								}
							}
							else
							{
								this.Pathfinding.speed = 4f;
							}
							if (this.Persona == PersonaType.PhoneAddict && !this.CrimeReported)
							{
								if (this.Countdown.Sprite.fillAmount == 0f)
								{
									this.Countdown.Sprite.fillAmount = 1f;
									this.CrimeReported = true;
									if (this.WitnessedMurder && !this.Countdown.MaskedPhoto)
									{
										this.PhoneAddictGameOver();
									}
									else
									{
										if (this.StudentManager.ChaseCamera == this.ChaseCamera)
										{
											this.StudentManager.ChaseCamera = null;
										}
										this.SprintAnim = this.OriginalSprintAnim;
										this.Countdown.gameObject.SetActive(false);
										this.ChaseCamera.SetActive(false);
										this.Police.Called = true;
										this.Police.Show = true;
									}
								}
								else
								{
									this.SprintAnim = this.PhoneAnims[2];
									if (this.StudentManager.ChaseCamera == null)
									{
										this.StudentManager.ChaseCamera = this.ChaseCamera;
										this.ChaseCamera.SetActive(true);
									}
								}
							}
						}
						else
						{
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							if (!this.Halt)
							{
								if (this.StudentID > 1)
								{
									this.MoveTowardsTarget(this.Pathfinding.target.position);
									if (!this.Teacher)
									{
										base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Pathfinding.target.rotation, 10f * Time.deltaTime);
									}
								}
							}
							else if (this.Persona == PersonaType.TeachersPet)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(this.StudentManager.Teachers[this.Class].transform.position.x, base.transform.position.y, this.StudentManager.Teachers[this.Class].transform.position.z) - base.transform.position);
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
							}
							else if (this.Persona == PersonaType.Dangerous && !this.BreakingUpFight)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
							}
							if (this.Persona == PersonaType.TeachersPet)
							{
								if (this.ReportingMurder || this.ReportingBlood)
								{
									if (this.StudentManager.Teachers[this.Class].Alarmed && this.ReportPhase < 100)
									{
										if (this.Club == ClubType.Council)
										{
											this.Pathfinding.target = this.StudentManager.CorpseLocation;
											this.CurrentDestination = this.StudentManager.CorpseLocation;
										}
										else
										{
											if (this.PetDestination == null)
											{
												this.PetDestination = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, this.Seat.position + this.Seat.forward * -0.5f, Quaternion.identity).transform;
											}
											this.Pathfinding.target = this.PetDestination;
											this.CurrentDestination = this.PetDestination;
										}
										this.ReportPhase = 3;
									}
									if (this.ReportPhase == 0)
									{
										if (this.WitnessedMurder)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.PetMurderReport, 2, 3f);
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
										else if (this.WitnessedCorpse)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.PetCorpseReport, 2, 3f);
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
										else if (this.WitnessedLimb)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.PetLimbReport, 2, 3f);
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
										else if (this.WitnessedBloodyWeapon)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.PetBloodyWeaponReport, 2, 3f);
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
										else if (this.WitnessedBloodPool)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.PetBloodReport, 2, 3f);
											this.CharacterAnimation.CrossFade(this.IdleAnim);
										}
										else if (this.WitnessedWeapon)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReport, 2, 3f);
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
										this.StudentManager.Teachers[this.Class].CharacterAnimation.CrossFade(this.StudentManager.Teachers[this.Class].IdleAnim);
										this.StudentManager.Teachers[this.Class].Routine = false;
										if (this.StudentManager.Teachers[this.Class].Investigating)
										{
											this.StudentManager.Teachers[this.Class].StopInvestigating();
										}
										this.Halt = true;
										this.ReportPhase++;
									}
									else if (this.ReportPhase == 1)
									{
										this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
										this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;
										if (this.WitnessedBloodPool || (this.WitnessedWeapon && !this.WitnessedBloodyWeapon))
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);
										}
										else if (this.WitnessedMurder || this.WitnessedCorpse || this.WitnessedLimb || this.WitnessedBloodyWeapon)
										{
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
										this.StudentManager.Teachers[this.Class].targetRotation = Quaternion.LookRotation(base.transform.position - this.StudentManager.Teachers[this.Class].transform.position);
										this.StudentManager.Teachers[this.Class].transform.rotation = Quaternion.Slerp(this.StudentManager.Teachers[this.Class].transform.rotation, this.StudentManager.Teachers[this.Class].targetRotation, 10f * Time.deltaTime);
										this.ReportTimer += Time.deltaTime;
										if (this.ReportTimer >= 3f)
										{
											base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.1f, base.transform.position.z);
											this.StudentManager.Teachers[this.Class].MyReporter = this;
											this.StudentManager.Teachers[this.Class].Routine = false;
											this.StudentManager.Teachers[this.Class].Fleeing = true;
											this.ReportTimer = 0f;
											this.ReportPhase++;
										}
									}
									else if (this.ReportPhase == 2)
									{
										this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
										this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;
										if (this.WitnessedBloodPool || (this.WitnessedWeapon && !this.WitnessedBloodyWeapon))
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);
										}
										else if (this.WitnessedMurder || this.WitnessedCorpse || this.WitnessedLimb || this.WitnessedBloodyWeapon)
										{
											this.CharacterAnimation.CrossFade(this.ScaredAnim);
										}
									}
									else if (this.ReportPhase == 3)
									{
										Debug.Log(this.Name + " just set their destination to themself.");
										this.Pathfinding.target = base.transform;
										this.CurrentDestination = base.transform;
										if (this.WitnessedBloodPool || (this.WitnessedWeapon && !this.WitnessedBloodyWeapon))
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);
										}
										else if (this.WitnessedMurder || this.WitnessedCorpse || this.WitnessedLimb || this.WitnessedBloodyWeapon)
										{
											this.CharacterAnimation.CrossFade(this.ParanoidAnim);
										}
									}
									else if (this.ReportPhase < 100)
									{
										this.CharacterAnimation.CrossFade(this.ParanoidAnim);
									}
									else
									{
										Debug.Log("This character just set their destination to themself.");
										this.Pathfinding.target = base.transform;
										this.CurrentDestination = base.transform;
										this.CharacterAnimation.CrossFade(this.ScaredAnim);
										this.ReportTimer += Time.deltaTime;
										if (this.ReportTimer >= 5f)
										{
											if (this.StudentManager.Reporter == this)
											{
												this.StudentManager.CorpseLocation.position = Vector3.zero;
												this.StudentManager.Reporter = null;
											}
											else if (this.StudentManager.BloodReporter == this)
											{
												this.StudentManager.BloodLocation.position = Vector3.zero;
												this.StudentManager.BloodReporter = null;
											}
											this.StudentManager.UpdateStudents(0);
											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];
											this.Pathfinding.speed = 1f;
											this.TargetDistance = 1f;
											this.ReportPhase = 0;
											this.ReportTimer = 0f;
											this.AlarmTimer = 0f;
											this.RandomAnim = this.BulliedIdleAnim;
											this.IdleAnim = this.BulliedIdleAnim;
											this.WalkAnim = this.BulliedWalkAnim;
											if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
											{
												this.Persona = this.OriginalPersona;
											}
											Debug.Log("WitnessedMurder is being set to false.");
											this.BloodPool = null;
											this.WitnessedBloodyWeapon = false;
											this.WitnessedBloodPool = false;
											this.WitnessedSomething = false;
											this.WitnessedCorpse = false;
											this.WitnessedMurder = false;
											this.WitnessedWeapon = false;
											this.WitnessedLimb = false;
											this.SmartPhone.SetActive(false);
											this.LostTeacherTrust = true;
											this.ReportingMurder = false;
											this.ReportingBlood = false;
											this.Distracted = false;
											this.Reacted = false;
											this.Alarmed = false;
											this.Fleeing = false;
											this.Routine = true;
											this.Halt = false;
											if (this.Club == ClubType.Council)
											{
												this.Persona = PersonaType.Dangerous;
											}
											this.ID = 0;
											while (this.ID < this.Outlines.Length)
											{
												this.Outlines[this.ID].color = new Color(1f, 1f, 0f, 1f);
												this.ID++;
											}
										}
									}
								}
								else if (this.Club == ClubType.Council)
								{
									this.CharacterAnimation.CrossFade(this.GuardAnim);
									this.Persona = PersonaType.Dangerous;
									this.Guarding = true;
									this.Fleeing = false;
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.ParanoidAnim);
									this.ReportPhase = 100;
								}
							}
							else if (this.Persona == PersonaType.Heroic)
							{
								Debug.Log(this.Name + " has the ''Heroic'' Persona and is using the ''Fleeing'' protocol.");
								if (this.Yandere.Attacking || (this.Yandere.Struggling && this.Yandere.StruggleBar.Student != this))
								{
									Debug.Log(this.Name + " is waiting his turn to fight Yandere-chan.");
									this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
									this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
									base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
									this.Pathfinding.canSearch = false;
									this.Pathfinding.canMove = false;
								}
								else if (!this.Yandere.Attacking && !this.StudentManager.PinningDown && !this.Yandere.Shoved)
								{
									if (this.StudentID > 1)
									{
										if (!this.Yandere.Struggling && this.Yandere.ShoulderCamera.Timer == 0f)
										{
											this.BeginStruggle();
										}
										Debug.Log(this.Name + " is currently engaged in a stuggle.");
										if (!this.Teacher)
										{
											this.CharacterAnimation[this.StruggleAnim].time = this.Yandere.CharacterAnimation["f02_struggleA_00"].time;
										}
										else
										{
											this.CharacterAnimation[this.StruggleAnim].time = this.Yandere.CharacterAnimation["f02_teacherStruggleA_00"].time;
										}
										base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Yandere.transform.rotation, 10f * Time.deltaTime);
										this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * 0.425f);
										if (!this.Yandere.Armed || !this.Yandere.EquippedWeapon.Concealable)
										{
											this.Yandere.StruggleBar.HeroWins();
										}
										if (this.Lost)
										{
											this.CharacterAnimation.CrossFade(this.StruggleWonAnim);
											if (this.CharacterAnimation[this.StruggleWonAnim].time > 1f)
											{
												this.EyeShrink = 1f;
											}
											if (this.CharacterAnimation[this.StruggleWonAnim].time >= this.CharacterAnimation[this.StruggleWonAnim].length)
											{
											}
										}
										else if (this.Won)
										{
											this.CharacterAnimation.CrossFade(this.StruggleLostAnim);
										}
									}
									else
									{
										this.Yandere.EmptyHands();
										this.Pathfinding.canSearch = false;
										this.Pathfinding.canMove = false;
										this.TargetDistance = 1f;
										this.Yandere.CharacterAnimation.CrossFade("f02_unmasking_00");
										this.CharacterAnimation.CrossFade("unmasking_00");
										this.Yandere.CanMove = false;
										this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
										base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
										this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * 1f);
										if (this.CharacterAnimation["unmasking_00"].time == 0f)
										{
											this.Yandere.ShoulderCamera.YandereNo();
											this.Yandere.Jukebox.GameOver();
										}
										if (this.CharacterAnimation["unmasking_00"].time >= 0.66666f && this.Yandere.Mask.transform.parent != this.LeftHand)
										{
											this.Yandere.Mask.transform.parent = this.LeftHand;
											this.Yandere.Mask.transform.localPosition = new Vector3(-0.1f, -0.05f, 0f);
											this.Yandere.Mask.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
											this.Yandere.Mask.transform.localScale = new Vector3(1f, 1f, 1f);
										}
										if (this.CharacterAnimation["unmasking_00"].time >= this.CharacterAnimation["unmasking_00"].length)
										{
											this.Yandere.Unmasked = true;
											this.Yandere.ShoulderCamera.GameOver();
										}
									}
								}
							}
							else if (this.Persona == PersonaType.Coward || this.Persona == PersonaType.Fragile)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
								this.CharacterAnimation.CrossFade(this.CowardAnim);
								this.ReactionTimer += Time.deltaTime;
								if (this.ReactionTimer > 5f)
								{
									this.CurrentDestination = this.StudentManager.Exit;
									this.Pathfinding.target = this.StudentManager.Exit;
								}
							}
							else if (this.Persona == PersonaType.Evil)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
								this.CharacterAnimation.CrossFade(this.EvilAnim);
								this.ReactionTimer += Time.deltaTime;
								if (this.ReactionTimer > 5f)
								{
									this.CurrentDestination = this.StudentManager.Exit;
									this.Pathfinding.target = this.StudentManager.Exit;
								}
							}
							else if (this.Persona == PersonaType.SocialButterfly)
							{
								if (this.ReportPhase < 4)
								{
									base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Pathfinding.target.rotation, 10f * Time.deltaTime);
								}
								if (this.ReportPhase == 1)
								{
									if (!this.SmartPhone.activeInHierarchy)
									{
										if (this.StudentManager.Reporter == null && !this.Police.Called)
										{
											this.CharacterAnimation.CrossFade(this.SocialReportAnim);
											this.Subtitle.UpdateLabel(SubtitleType.SocialReport, 1, 5f);
											this.StudentManager.Reporter = this;
											this.SmartPhone.SetActive(true);
											this.SmartPhone.transform.localPosition = new Vector3(-0.015f, -0.01f, 0f);
											this.SmartPhone.transform.localEulerAngles = new Vector3(0f, -170f, 165f);
										}
										else
										{
											this.ReportTimer = 5f;
										}
									}
									this.ReportTimer += Time.deltaTime;
									if (this.ReportTimer > 5f)
									{
										if (this.StudentManager.Reporter == this)
										{
											this.Police.Called = true;
											this.Police.Show = true;
										}
										this.CharacterAnimation.CrossFade(this.ParanoidAnim);
										this.SmartPhone.SetActive(false);
										this.ReportPhase++;
										this.Halt = false;
									}
								}
								else if (this.ReportPhase == 2)
								{
									if (this.WitnessedMurder && (!this.SawMask || (this.SawMask && this.Yandere.Mask != null)))
									{
										this.LookForYandere();
									}
								}
								else if (this.ReportPhase == 3)
								{
									this.CharacterAnimation.CrossFade(this.SocialFearAnim);
									this.Subtitle.UpdateLabel(SubtitleType.SocialFear, 1, 5f);
									this.SpawnAlarmDisc();
									this.ReportPhase++;
									this.Halt = true;
								}
								else if (this.ReportPhase == 4)
								{
									this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
									base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
									if (this.Yandere.Attacking)
									{
										this.LookForYandere();
									}
								}
								else if (this.ReportPhase == 5)
								{
									this.CharacterAnimation.CrossFade(this.SocialTerrorAnim);
									this.Subtitle.UpdateLabel(SubtitleType.SocialTerror, 1, 5f);
									this.VisionDistance = 0f;
									this.SpawnAlarmDisc();
									this.ReportPhase++;
								}
							}
							else if (this.Persona == PersonaType.Dangerous)
							{
								if (!this.Yandere.Attacking && !this.StudentManager.PinningDown && !this.Yandere.Struggling)
								{
									this.Spray();
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
								}
							}
							else if (this.Persona == PersonaType.Violent)
							{
								if (!this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.Dumping && !this.StudentManager.PinningDown && !this.RespectEarned)
								{
									if (!this.Yandere.DelinquentFighting)
									{
										Debug.Log(this.Name + " is supposed to begin the combat minigame now.");
										this.SmartPhone.SetActive(false);
										this.Threatened = true;
										this.Fleeing = false;
										this.Alarmed = true;
										this.NoTalk = false;
										this.Patience = 0;
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
								}
							}
							else if (this.Persona == PersonaType.Strict)
							{
								if (!this.WitnessedMurder)
								{
									if (this.ReportPhase == 0)
									{
										if (this.MyReporter.WitnessedMurder || this.MyReporter.WitnessedCorpse)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 0, 3f);
											this.InvestigatingPossibleDeath = true;
										}
										else if (this.MyReporter.WitnessedLimb)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 2, 3f);
										}
										else if (this.MyReporter.WitnessedBloodyWeapon)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 3, 3f);
										}
										else if (this.MyReporter.WitnessedBloodPool)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 1, 3f);
										}
										else if (this.MyReporter.WitnessedWeapon)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherReportReaction, 4, 3f);
										}
										this.ReportPhase++;
									}
									else if (this.ReportPhase == 1)
									{
										this.CharacterAnimation.CrossFade(this.IdleAnim);
										this.ReportTimer += Time.deltaTime;
										if (this.ReportTimer >= 3f)
										{
											base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.1f, base.transform.position.z);
											StudentScript studentScript = null;
											if (this.MyReporter.WitnessedMurder || this.MyReporter.WitnessedCorpse)
											{
												studentScript = this.StudentManager.Reporter;
											}
											else if (this.MyReporter.WitnessedBloodPool || this.MyReporter.WitnessedLimb || this.MyReporter.WitnessedWeapon)
											{
												studentScript = this.StudentManager.BloodReporter;
											}
											if (this.MyReporter.WitnessedLimb)
											{
												this.InvestigatingPossibleLimb = true;
											}
											if (!studentScript.Teacher)
											{
												if (this.MyReporter.WitnessedMurder || this.MyReporter.WitnessedCorpse)
												{
													this.StudentManager.Reporter.CurrentDestination = this.StudentManager.CorpseLocation;
													this.StudentManager.Reporter.Pathfinding.target = this.StudentManager.CorpseLocation;
													this.CurrentDestination = this.StudentManager.CorpseLocation;
													this.Pathfinding.target = this.StudentManager.CorpseLocation;
													this.StudentManager.Reporter.TargetDistance = 2f;
												}
												else if (this.MyReporter.WitnessedBloodPool || this.MyReporter.WitnessedLimb || this.MyReporter.WitnessedWeapon)
												{
													this.StudentManager.BloodReporter.CurrentDestination = this.StudentManager.BloodLocation;
													this.StudentManager.BloodReporter.Pathfinding.target = this.StudentManager.BloodLocation;
													this.CurrentDestination = this.StudentManager.BloodLocation;
													this.Pathfinding.target = this.StudentManager.BloodLocation;
													this.StudentManager.BloodReporter.TargetDistance = 2f;
												}
											}
											this.TargetDistance = 1f;
											this.ReportTimer = 0f;
											this.ReportPhase++;
										}
									}
									else if (this.ReportPhase == 2)
									{
										if (this.WitnessedCorpse)
										{
											Debug.Log("A teacher has just witnessed a corpse while on their way to investigate a student's report of a corpse.");
											this.DetermineCorpseLocation();
											if (!this.Corpse.Poisoned)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 1, 5f);
											}
											else
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 2, 2f);
											}
											this.ReportPhase++;
										}
										else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
										{
											Debug.Log("A teacher has just witnessed an alarming object while on their way to investigate a student's report.");
											this.DetermineBloodLocation();
											if (this.WitnessedLimb)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 4, 5f);
											}
											else if (this.WitnessedBloodPool || this.WitnessedBloodyWeapon)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 3, 5f);
											}
											else if (this.WitnessedWeapon)
											{
												this.Subtitle.Speaker = this;
												this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 5, 5f);
											}
											PromptScript component2 = this.BloodPool.GetComponent<PromptScript>();
											if (component2 != null)
											{
												Debug.Log("Disabling an object's prompt.");
												component2.Hide();
												component2.enabled = false;
											}
											this.ReportPhase++;
										}
										else
										{
											this.CharacterAnimation.CrossFade(this.GuardAnim);
											this.ReportTimer += Time.deltaTime;
											if (this.ReportTimer > 5f)
											{
												this.Subtitle.UpdateLabel(SubtitleType.TeacherPrankReaction, 1, 7f);
												this.ReportPhase = 98;
												this.ReportTimer = 0f;
											}
										}
									}
									else if (this.ReportPhase == 3)
									{
										if (this.WitnessedCorpse)
										{
											this.targetRotation = Quaternion.LookRotation(new Vector3(this.Corpse.AllColliders[0].transform.position.x, base.transform.position.y, this.Corpse.AllColliders[0].transform.position.z) - base.transform.position);
											base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
											this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
											this.CharacterAnimation.CrossFade(this.InspectAnim);
										}
										else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
										{
											this.targetRotation = Quaternion.LookRotation(new Vector3(this.BloodPool.transform.position.x, base.transform.position.y, this.BloodPool.transform.position.z) - base.transform.position);
											base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
											this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
											this.CharacterAnimation[this.InspectBloodAnim].speed = 0.66666f;
											this.CharacterAnimation.CrossFade(this.InspectBloodAnim);
										}
										this.ReportTimer += Time.deltaTime;
										if (this.ReportTimer >= 6f)
										{
											this.ReportTimer = 0f;
											if (this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
											{
												this.ReportPhase = 7;
											}
											else
											{
												this.ReportPhase++;
											}
										}
									}
									else if (this.ReportPhase == 4)
									{
										if (this.WitnessedCorpse)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherPoliceReport, 0, 5f);
										}
										else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
										{
											this.Subtitle.Speaker = this;
											this.Subtitle.UpdateLabel(SubtitleType.TeacherPoliceReport, 1, 5f);
										}
										this.SmartPhone.transform.localPosition = new Vector3(-0.01f, -0.005f, -0.02f);
										this.SmartPhone.transform.localEulerAngles = new Vector3(-10f, -145f, 170f);
										this.SmartPhone.SetActive(true);
										this.ReportPhase++;
									}
									else if (this.ReportPhase == 5)
									{
										this.CharacterAnimation.CrossFade(this.CallAnim);
										this.ReportTimer += Time.deltaTime;
										if (this.ReportTimer >= 5f)
										{
											this.CharacterAnimation.CrossFade(this.GuardAnim);
											this.SmartPhone.SetActive(false);
											this.WitnessedBloodyWeapon = false;
											this.WitnessedBloodPool = false;
											this.WitnessedSomething = false;
											this.WitnessedWeapon = false;
											this.WitnessedLimb = false;
											this.IgnoringPettyActions = true;
											this.Police.Called = true;
											this.Police.Show = true;
											this.ReportTimer = 0f;
											this.Guarding = true;
											this.Alarmed = false;
											this.Fleeing = false;
											this.Reacted = false;
											this.ReportPhase++;
											if (this.MyReporter != null && this.MyReporter.ReportingBlood)
											{
												Debug.Log("The blood reporter has just been instructed to stop following the teacher.");
												this.MyReporter.ReportPhase++;
											}
										}
									}
									else if (this.ReportPhase != 6)
									{
										if (this.ReportPhase == 7)
										{
											Debug.Log("Telling reporter to go back to their normal routine.");
											if (this.StudentManager.BloodReporter != this)
											{
												this.StudentManager.BloodReporter = null;
											}
											this.StudentManager.UpdateStudents(0);
											if (this.MyReporter != null)
											{
												this.MyReporter.CurrentDestination = this.MyReporter.Destinations[this.MyReporter.Phase];
												this.MyReporter.Pathfinding.target = this.MyReporter.Destinations[this.MyReporter.Phase];
												this.MyReporter.Pathfinding.speed = 1f;
												this.MyReporter.ReportTimer = 0f;
												this.MyReporter.AlarmTimer = 0f;
												this.MyReporter.TargetDistance = 1f;
												this.MyReporter.ReportPhase = 0;
												this.MyReporter.WitnessedSomething = false;
												this.MyReporter.WitnessedWeapon = false;
												this.MyReporter.Reacted = false;
												this.MyReporter.Alarmed = false;
												this.MyReporter.Fleeing = false;
												this.MyReporter.Routine = true;
												this.MyReporter.Halt = false;
												this.MyReporter.Persona = this.OriginalPersona;
												if (this.MyReporter.Club == ClubType.Council)
												{
													this.MyReporter.Persona = PersonaType.Dangerous;
												}
												this.ID = 0;
												while (this.ID < this.MyReporter.Outlines.Length)
												{
													this.MyReporter.Outlines[this.ID].color = new Color(1f, 1f, 0f, 1f);
													this.ID++;
												}
											}
											this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = false;
											this.BloodPool.GetComponent<WeaponScript>().Prompt.Hide();
											this.BloodPool.GetComponent<WeaponScript>().enabled = false;
											this.ReportPhase++;
										}
										else if (this.ReportPhase == 8)
										{
											this.CharacterAnimation.CrossFade("f02_teacherPickUp_00");
											if (this.CharacterAnimation["f02_teacherPickUp_00"].time >= 0.33333f)
											{
												this.Handkerchief.SetActive(true);
											}
											if (this.CharacterAnimation["f02_teacherPickUp_00"].time >= 2f)
											{
												this.BloodPool.parent = this.RightHand;
												this.BloodPool.localPosition = new Vector3(0f, 0f, 0f);
												this.BloodPool.localEulerAngles = new Vector3(0f, 0f, 0f);
												this.BloodPool.GetComponent<WeaponScript>().Returner = this;
											}
											if (this.CharacterAnimation["f02_teacherPickUp_00"].time >= this.CharacterAnimation["f02_teacherPickUp_00"].length)
											{
												this.CurrentDestination = this.StudentManager.WeaponBoxSpot;
												this.Pathfinding.target = this.StudentManager.WeaponBoxSpot;
												this.Pathfinding.speed = 1f;
												this.Hurry = false;
												this.ReportPhase++;
											}
										}
										else if (this.ReportPhase == 9)
										{
											this.StudentManager.BloodLocation.position = Vector3.zero;
											this.BloodPool.parent = null;
											this.BloodPool.position = this.StudentManager.WeaponBoxSpot.parent.position + new Vector3(0f, 1f, 0f);
											this.BloodPool.eulerAngles = new Vector3(0f, 90f, 0f);
											this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = true;
											this.BloodPool.GetComponent<WeaponScript>().Returner = null;
											this.BloodPool.GetComponent<WeaponScript>().enabled = true;
											this.BloodPool.GetComponent<WeaponScript>().Drop();
											this.BloodPool = null;
											this.CharacterAnimation.CrossFade(this.RunAnim);
											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];
											this.Handkerchief.SetActive(false);
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.Pathfinding.speed = 1f;
											this.WitnessedSomething = false;
											this.WitnessedWeapon = false;
											this.ReportingBlood = false;
											this.Distracted = false;
											this.Alarmed = false;
											this.Fleeing = false;
											this.Routine = true;
											this.ReportTimer = 0f;
											this.ReportPhase = 0;
										}
										else if (this.ReportPhase == 98)
										{
											this.CharacterAnimation.CrossFade(this.IdleAnim);
											this.targetRotation = Quaternion.LookRotation(this.MyReporter.transform.position - base.transform.position);
											base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
											this.ReportTimer += Time.deltaTime;
											if (this.ReportTimer > 7f)
											{
												this.ReportPhase++;
											}
										}
										else if (this.ReportPhase == 99)
										{
											this.Subtitle.UpdateLabel(SubtitleType.PrankReaction, 1, 5f);
											this.CharacterAnimation.CrossFade(this.RunAnim);
											this.CurrentDestination = this.Destinations[this.Phase];
											this.Pathfinding.target = this.Destinations[this.Phase];
											this.Pathfinding.canSearch = true;
											this.Pathfinding.canMove = true;
											this.MyReporter.Persona = PersonaType.TeachersPet;
											this.MyReporter.ReportPhase = 100;
											this.MyReporter.Fleeing = true;
											this.ReportTimer = 0f;
											this.ReportPhase = 0;
											this.InvestigatingPossibleDeath = false;
											this.InvestigatingPossibleLimb = false;
											this.Alarmed = false;
											this.Fleeing = false;
											this.Routine = true;
										}
									}
								}
								else if (!this.Yandere.Dumping && !this.Yandere.Attacking)
								{
									if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus == 0)
									{
										Debug.Log("A teacher is taking down Yandere-chan.");
										if (this.Yandere.Aiming)
										{
											this.Yandere.StopAiming();
										}
										this.Yandere.Mopping = false;
										this.Yandere.EmptyHands();
										this.AttackReaction();
										this.CharacterAnimation[this.CounterAnim].time = 5f;
										this.Yandere.CharacterAnimation["f02_teacherCounterA_00"].time = 5f;
										this.Yandere.ShoulderCamera.Timer = 5f;
										this.Yandere.ShoulderCamera.Phase = 3;
										this.Police.Show = false;
										this.Yandere.CameraEffects.MurderWitnessed();
										this.Yandere.Jukebox.GameOver();
									}
									else
									{
										this.Persona = PersonaType.Heroic;
									}
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
								}
							}
						}
						if (this.Persona == PersonaType.Strict && this.BloodPool != null && this.BloodPool.parent == this.Yandere.RightHand)
						{
							Debug.Log("Yandere-chan picked up the weapon that the teacher was investigating!");
							this.WitnessedBloodyWeapon = false;
							this.WitnessedBloodPool = false;
							this.WitnessedSomething = false;
							this.WitnessedCorpse = false;
							this.WitnessedMurder = false;
							this.WitnessedWeapon = false;
							this.WitnessedLimb = false;
							this.YandereVisible = true;
							this.ReportTimer = 0f;
							this.BloodPool = null;
							this.ReportPhase = 0;
							this.Alarmed = false;
							this.Fleeing = false;
							this.Routine = true;
							this.Reacted = false;
							this.AlarmTimer = 0f;
							this.Alarm = 200f;
							this.BecomeAlarmed();
						}
					}
				}
				else if (this.PinPhase == 0)
				{
					if (this.DistanceToDestination < 1f)
					{
						if (this.Pathfinding.canSearch)
						{
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
						}
						this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
						this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
						this.MoveTowardsTarget(this.CurrentDestination.position);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.SprintAnim);
						if (!this.Pathfinding.canSearch)
						{
							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;
						}
					}
				}
				else
				{
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
					this.MoveTowardsTarget(this.CurrentDestination.position);
				}
			}
			if (this.Following && !this.Waiting)
			{
				this.DistanceToDestination = Vector3.Distance(base.transform.position, this.Pathfinding.target.position);
				if (this.DistanceToDestination > 2f)
				{
					Debug.Log("Sprinting 10");
					this.CharacterAnimation.CrossFade(this.RunAnim);
					this.Pathfinding.speed = 4f;
					this.Obstacle.enabled = false;
				}
				else if (this.DistanceToDestination > 1f)
				{
					this.CharacterAnimation.CrossFade(this.OriginalWalkAnim);
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 1f;
					this.Obstacle.enabled = false;
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Pathfinding.canMove = false;
					this.Obstacle.enabled = true;
				}
				if (this.Phase < this.ScheduleBlocks.Length - 1 && (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time || this.StudentManager.LockerRoomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.IncineratorArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.HeadmasterArea.bounds.Contains(this.Yandere.transform.position)))
				{
					if (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
					{
						this.Phase++;
					}
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.Hearts.emission.enabled = false;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 1f;
					this.Yandere.Followers--;
					this.Following = false;
					this.Routine = true;
					if (this.StudentManager.LockerRoomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.WestBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.EastBathroomArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.IncineratorArea.bounds.Contains(this.Yandere.transform.position) || this.StudentManager.HeadmasterArea.bounds.Contains(this.Yandere.transform.position))
					{
						this.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 1, 3f);
					}
					else
					{
						this.Subtitle.UpdateLabel(SubtitleType.StopFollowApology, 0, 3f);
					}
					this.Prompt.Label[0].text = "     Talk";
				}
			}
			if (this.Wet)
			{
				if (this.DistanceToDestination < this.TargetDistance)
				{
					if (!this.Splashed)
					{
						if (!this.InDarkness)
						{
							if (this.BathePhase == 1)
							{
								this.StudentManager.CommunalLocker.Open = true;
								this.StudentManager.CommunalLocker.Student = this;
								this.StudentManager.CommunalLocker.SpawnSteam();
								this.Pathfinding.speed = 1f;
								this.Schoolwear = 0;
								this.BathePhase++;
							}
							else if (this.BathePhase == 2)
							{
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
								this.MoveTowardsTarget(this.CurrentDestination.position);
							}
							else if (this.BathePhase == 3)
							{
								this.StudentManager.CommunalLocker.Open = false;
								this.CharacterAnimation.CrossFade(this.WalkAnim);
								if (!this.BatheFast)
								{
									if (!this.Male)
									{
										this.CurrentDestination = this.StudentManager.FemaleBatheSpot;
										this.Pathfinding.target = this.StudentManager.FemaleBatheSpot;
									}
									else
									{
										this.CurrentDestination = this.StudentManager.MaleBatheSpot;
										this.Pathfinding.target = this.StudentManager.MaleBatheSpot;
									}
								}
								else if (!this.Male)
								{
									this.CurrentDestination = this.StudentManager.FastBatheSpot;
									this.Pathfinding.target = this.StudentManager.FastBatheSpot;
								}
								else
								{
									this.CurrentDestination = this.StudentManager.MaleBatheSpot;
									this.Pathfinding.target = this.StudentManager.MaleBatheSpot;
								}
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.BathePhase++;
							}
							else if (this.BathePhase == 4)
							{
								this.StudentManager.OpenValue = Mathf.Lerp(this.StudentManager.OpenValue, 0f, Time.deltaTime * 10f);
								this.StudentManager.FemaleShowerCurtain.SetBlendShapeWeight(0, this.StudentManager.OpenValue);
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
								this.MoveTowardsTarget(this.CurrentDestination.position);
								this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
								this.CharacterAnimation.CrossFade(this.BathingAnim);
								if (this.CharacterAnimation[this.BathingAnim].time >= this.CharacterAnimation[this.BathingAnim].length)
								{
									this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
									this.StudentManager.OpenCurtain = true;
									this.LiquidProjector.enabled = false;
									this.Bloody = false;
									this.BathePhase++;
									this.Gas = false;
									this.GoChange();
									this.UnWet();
								}
							}
							else if (this.BathePhase == 5)
							{
								this.StudentManager.CommunalLocker.Open = true;
								this.StudentManager.CommunalLocker.Student = this;
								this.StudentManager.CommunalLocker.SpawnSteam();
								this.Schoolwear = ((!this.InEvent) ? 3 : 1);
								this.BathePhase++;
							}
							else if (this.BathePhase == 6)
							{
								base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
								this.MoveTowardsTarget(this.CurrentDestination.position);
							}
							else if (this.BathePhase == 7)
							{
								if (this.StudentID == 30 || this.StudentID == this.StudentManager.RivalID)
								{
									if (this.StudentManager.CommunalLocker.RivalPhone.Stolen)
									{
										this.CharacterAnimation.CrossFade("f02_losingPhone_00");
										this.Subtitle.UpdateLabel(this.LostPhoneSubtitleType, 1, 5f);
										if (this.StudentID == this.StudentManager.RivalID)
										{
											this.RealizePhoneIsMissing();
										}
										this.Phoneless = true;
										this.BatheTimer = 6f;
										this.BathePhase++;
									}
									else
									{
										this.StudentManager.CommunalLocker.RivalPhone.gameObject.SetActive(false);
										this.BathePhase++;
									}
								}
								else
								{
									this.BathePhase += 2;
								}
							}
							else if (this.BathePhase == 8)
							{
								if (this.BatheTimer == 0f)
								{
									this.BathePhase++;
								}
								else
								{
									this.BatheTimer = Mathf.MoveTowards(this.BatheTimer, 0f, Time.deltaTime);
								}
							}
							else if (this.BathePhase == 9)
							{
								if (this.Persona == PersonaType.PhoneAddict)
								{
									this.SmartPhone.SetActive(true);
								}
								this.StudentManager.CommunalLocker.Student = null;
								this.StudentManager.CommunalLocker.Open = false;
								if (this.Phase == 1)
								{
									this.Phase++;
								}
								Debug.Log(this.Name + " has finished bathing. Returning to normal routine.");
								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.DistanceToDestination = 100f;
								this.Routine = true;
								this.Wet = false;
								if (this.FleeWhenClean)
								{
									this.CurrentDestination = this.StudentManager.Exit;
									this.Pathfinding.target = this.StudentManager.Exit;
									this.TargetDistance = 0f;
									this.Routine = false;
									this.Fleeing = true;
								}
							}
						}
						else if (this.BathePhase == -1)
						{
							this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							this.Subtitle.UpdateLabel(SubtitleType.LightSwitchReaction, 2, 5f);
							this.CharacterAnimation.CrossFade("f02_electrocution_00");
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							this.Distracted = true;
							this.BathePhase++;
						}
						else if (this.BathePhase == 0)
						{
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
							this.MoveTowardsTarget(this.CurrentDestination.position);
							if (this.CharacterAnimation["f02_electrocution_00"].time > 2f && this.CharacterAnimation["f02_electrocution_00"].time < 5.95000029f)
							{
								if (!this.LightSwitch.Panel.useGravity)
								{
									if (!this.Bloody)
									{
										this.Subtitle.Speaker = this;
										this.Subtitle.UpdateLabel(this.SplashSubtitleType, 2, 5f);
									}
									else
									{
										this.Subtitle.Speaker = this;
										this.Subtitle.UpdateLabel(this.SplashSubtitleType, 4, 5f);
									}
									this.CurrentDestination = this.StudentManager.FemaleStripSpot;
									this.Pathfinding.target = this.StudentManager.FemaleStripSpot;
									Debug.Log("Sprinting 11");
									this.Pathfinding.canSearch = true;
									this.Pathfinding.canMove = true;
									this.Pathfinding.speed = 4f;
									this.CharacterAnimation.CrossFade(this.WalkAnim);
									this.BathePhase++;
									this.LightSwitch.Prompt.Label[0].text = "     Turn Off";
									this.LightSwitch.BathroomLight.SetActive(true);
									this.LightSwitch.GetComponent<AudioSource>().clip = this.LightSwitch.Flick[0];
									this.LightSwitch.GetComponent<AudioSource>().Play();
									this.InDarkness = false;
								}
								else
								{
									if (!this.LightSwitch.Flicker)
									{
										this.CharacterAnimation["f02_electrocution_00"].speed = 0.85f;
										GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.LightSwitch.Electricity, base.transform.position, Quaternion.identity);
										gameObject5.transform.parent = this.Bones[1].transform;
										gameObject5.transform.localPosition = Vector3.zero;
										this.Subtitle.UpdateLabel(SubtitleType.LightSwitchReaction, 3, 0f);
										this.LightSwitch.GetComponent<AudioSource>().clip = this.LightSwitch.Flick[2];
										this.LightSwitch.Flicker = true;
										this.LightSwitch.GetComponent<AudioSource>().Play();
										this.EyeShrink = 1f;
										this.ElectroSteam[0].SetActive(true);
										this.ElectroSteam[1].SetActive(true);
										this.ElectroSteam[2].SetActive(true);
										this.ElectroSteam[3].SetActive(true);
									}
									this.RightDrill.eulerAngles = new Vector3(UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f));
									this.LeftDrill.eulerAngles = new Vector3(UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f), UnityEngine.Random.Range(-360f, 360f));
									this.ElectroTimer += Time.deltaTime;
									if (this.ElectroTimer > 0.1f)
									{
										this.ElectroTimer = 0f;
										if (this.MyRenderer.enabled)
										{
											this.Spook();
										}
										else
										{
											this.Unspook();
										}
									}
								}
							}
							else if (this.CharacterAnimation["f02_electrocution_00"].time > 5.95000029f && this.CharacterAnimation["f02_electrocution_00"].time < this.CharacterAnimation["f02_electrocution_00"].length)
							{
								if (this.LightSwitch.Flicker)
								{
									this.CharacterAnimation["f02_electrocution_00"].speed = 1f;
									this.Prompt.Label[0].text = "     Turn Off";
									this.LightSwitch.BathroomLight.SetActive(true);
									this.RightDrill.localEulerAngles = new Vector3(0f, 0f, 68.30447f);
									this.LeftDrill.localEulerAngles = new Vector3(0f, -180f, -159.417f);
									this.LightSwitch.Flicker = false;
									this.Unspook();
									this.UnWet();
								}
							}
							else if (this.CharacterAnimation["f02_electrocution_00"].time >= this.CharacterAnimation["f02_electrocution_00"].length)
							{
								this.Police.ElectrocutedStudentName = this.Name;
								this.Police.ElectroScene = true;
								this.Electrocuted = true;
								this.BecomeRagdoll();
								this.DeathType = DeathType.Electrocution;
							}
						}
					}
				}
				else if (this.Pathfinding.canMove)
				{
					if (this.BathePhase == 1 || this.FleeWhenClean)
					{
						if (this.Persona == PersonaType.PhoneAddict)
						{
							this.CharacterAnimation.CrossFade(this.OriginalSprintAnim);
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.SprintAnim);
						}
						this.Pathfinding.speed = 4f;
					}
					else
					{
						if (this.Persona == PersonaType.PhoneAddict)
						{
							this.CharacterAnimation.CrossFade(this.OriginalWalkAnim);
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
						}
						this.Pathfinding.speed = 1f;
					}
				}
			}
			if (this.Distracting)
			{
				if (this.DistractionTarget == null)
				{
					this.Distracting = false;
				}
				else if (this.DistractionTarget.Dying)
				{
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.DistractionTarget.TargetedForDistraction = false;
					this.DistractionTarget.Distracted = false;
					this.DistractionTarget.EmptyHands();
					this.Pathfinding.speed = 1f;
					this.Distracting = false;
					this.Distracted = false;
					this.CanTalk = true;
					this.Routine = true;
				}
				else
				{
					if (this.Actions[this.Phase] == StudentActionType.ClubAction && this.Club == ClubType.Cooking && this.ClubActivityPhase > 0 && this.DistractionTarget.InEvent)
					{
						this.GetFoodTarget();
					}
					if (this.DistanceToDestination < 5f || this.DistractionTarget.Leaving)
					{
						if (this.DistractionTarget.InEvent || this.DistractionTarget.Talking || this.DistractionTarget.Following || this.DistractionTarget.TurnOffRadio || this.DistractionTarget.Splashed || this.DistractionTarget.Shoving || this.DistractionTarget.Spraying || this.DistractionTarget.FocusOnYandere || this.DistractionTarget.ShoeRemoval.enabled || this.DistractionTarget.Posing || this.DistractionTarget.ClubActivityPhase >= 16 || !this.DistractionTarget.enabled || this.DistractionTarget.Alarmed || this.DistractionTarget.Fleeing || this.DistractionTarget.Wet || this.DistractionTarget.EatingSnack || this.DistractionTarget.MyBento.Tampered || this.DistractionTarget.Meeting || this.DistractionTarget.InvestigatingBloodPool || this.DistractionTarget.ReturningMisplacedWeapon || this.StudentManager.LockerRoomArea.bounds.Contains(this.DistractionTarget.transform.position) || this.StudentManager.WestBathroomArea.bounds.Contains(this.DistractionTarget.transform.position) || this.StudentManager.EastBathroomArea.bounds.Contains(this.DistractionTarget.transform.position) || this.StudentManager.HeadmasterArea.bounds.Contains(this.DistractionTarget.transform.position) || (this.DistractionTarget.Actions[this.DistractionTarget.Phase] == StudentActionType.Bully && this.DistractionTarget.DistanceToDestination < 1f) || this.DistractionTarget.Leaving)
						{
							this.CurrentDestination = this.Destinations[this.Phase];
							this.Pathfinding.target = this.Destinations[this.Phase];
							this.DistractionTarget.TargetedForDistraction = false;
							this.Pathfinding.speed = 1f;
							this.Distracting = false;
							this.Distracted = false;
							this.SpeechLines.Stop();
							this.CanTalk = true;
							this.Routine = true;
							if (this.Actions[this.Phase] == StudentActionType.ClubAction && this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
							{
								this.GetFoodTarget();
							}
						}
						else if (this.DistanceToDestination < this.TargetDistance)
						{
							if (!this.DistractionTarget.Distracted)
							{
								if (this.StudentID > 1 && this.DistractionTarget.StudentID > 1 && this.Persona != PersonaType.Fragile && this.DistractionTarget.Persona != PersonaType.Fragile && ((this.Club != ClubType.Bully && this.DistractionTarget.Club == ClubType.Bully) || (this.Club == ClubType.Bully && this.DistractionTarget.Club != ClubType.Bully)))
								{
									this.BullyPhotoCollider.SetActive(true);
								}
								if (this.DistractionTarget.Investigating)
								{
									this.DistractionTarget.StopInvestigating();
								}
								this.StudentManager.UpdateStudents(this.DistractionTarget.StudentID);
								this.DistractionTarget.Pathfinding.canSearch = false;
								this.DistractionTarget.Pathfinding.canMove = false;
								this.DistractionTarget.OccultBook.SetActive(false);
								this.DistractionTarget.SmartPhone.SetActive(false);
								this.DistractionTarget.Distraction = base.transform;
								this.DistractionTarget.CameraReacting = false;
								this.DistractionTarget.Pathfinding.speed = 0f;
								this.DistractionTarget.Pen.SetActive(false);
								this.DistractionTarget.Drownable = false;
								this.DistractionTarget.Distracted = true;
								this.DistractionTarget.Pushable = false;
								this.DistractionTarget.Routine = false;
								this.DistractionTarget.CanTalk = false;
								this.DistractionTarget.ReadPhase = 0;
								this.DistractionTarget.SpeechLines.Stop();
								this.DistractionTarget.ChalkDust.Stop();
								this.DistractionTarget.CleanTimer = 0f;
								this.DistractionTarget.EmptyHands();
								this.DistractionTarget.Distractor = this;
								this.Pathfinding.speed = 0f;
								this.Distracted = true;
							}
							this.targetRotation = Quaternion.LookRotation(new Vector3(this.DistractionTarget.transform.position.x, base.transform.position.y, this.DistractionTarget.transform.position.z) - base.transform.position);
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
							if (this.Actions[this.Phase] == StudentActionType.ClubAction && this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
							{
								this.CharacterAnimation.CrossFade(this.IdleAnim);
							}
							else
							{
								this.DistractionTarget.SpeechLines.Play();
								this.SpeechLines.Play();
								this.CharacterAnimation.CrossFade(this.RandomAnim);
								if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
								{
									this.PickRandomAnim();
								}
							}
							this.DistractTimer -= Time.deltaTime;
							if (this.DistractTimer <= 0f)
							{
								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];
								this.DistractionTarget.TargetedForDistraction = false;
								this.DistractionTarget.Pathfinding.canSearch = true;
								this.DistractionTarget.Pathfinding.canMove = true;
								this.DistractionTarget.Pathfinding.speed = 1f;
								this.DistractionTarget.Octodog.SetActive(false);
								this.DistractionTarget.Distraction = null;
								this.DistractionTarget.Distracted = false;
								this.DistractionTarget.CanTalk = true;
								this.DistractionTarget.Routine = true;
								this.DistractionTarget.EquipCleaningItems();
								this.DistractionTarget.EatingSnack = false;
								this.Private = false;
								this.DistractionTarget.CurrentDestination = this.DistractionTarget.Destinations[this.Phase];
								this.DistractionTarget.Pathfinding.target = this.DistractionTarget.Destinations[this.Phase];
								if (this.DistractionTarget.Persona == PersonaType.PhoneAddict)
								{
									this.DistractionTarget.SmartPhone.SetActive(true);
								}
								this.DistractionTarget.Distractor = null;
								this.DistractionTarget.SpeechLines.Stop();
								this.SpeechLines.Stop();
								this.BullyPhotoCollider.SetActive(false);
								this.Pathfinding.speed = 1f;
								this.Distracting = false;
								this.Distracted = false;
								this.CanTalk = true;
								this.Routine = true;
								if (this.Actions[this.Phase] == StudentActionType.ClubAction && this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
								{
									this.GetFoodTarget();
								}
							}
						}
						else if (this.Actions[this.Phase] == StudentActionType.ClubAction && this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;
						}
						else if (this.Pathfinding.speed == 1f)
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.SprintAnim);
						}
					}
					else if (this.Actions[this.Phase] == StudentActionType.ClubAction && this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
					{
						this.CharacterAnimation.CrossFade(this.WalkAnim);
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						if (this.Phase < this.ScheduleBlocks.Length - 1 && this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
						{
							this.Routine = true;
						}
					}
					else if (this.Pathfinding.speed == 1f)
					{
						this.CharacterAnimation.CrossFade(this.WalkAnim);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.SprintAnim);
					}
				}
			}
			if (this.Hunting)
			{
				if (this.HuntTarget != null)
				{
					if (this.HuntTarget.Prompt.enabled && !this.HuntTarget.FightingSlave)
					{
						this.HuntTarget.Prompt.Hide();
						this.HuntTarget.Prompt.enabled = false;
					}
					this.Pathfinding.target = this.HuntTarget.transform;
					this.CurrentDestination = this.HuntTarget.transform;
					if (this.HuntTarget.Alive && !this.HuntTarget.Tranquil && !this.HuntTarget.PinningDown)
					{
						if (this.DistanceToDestination > this.TargetDistance)
						{
							if (this.MurderSuicidePhase == 0)
							{
								if (this.CharacterAnimation["f02_brokenStandUp_00"].time >= this.CharacterAnimation["f02_brokenStandUp_00"].length)
								{
									this.MurderSuicidePhase++;
									this.Pathfinding.canSearch = true;
									this.Pathfinding.canMove = true;
									this.CharacterAnimation.CrossFade(this.WalkAnim);
									this.Pathfinding.speed = 1.15f;
								}
							}
							else if (this.MurderSuicidePhase > 1)
							{
								this.CharacterAnimation.CrossFade(this.WalkAnim);
								this.HuntTarget.MoveTowardsTarget(base.transform.position + base.transform.forward * 0.01f);
							}
							if (this.HuntTarget.Dying || this.HuntTarget.Struggling || this.HuntTarget.Ragdoll.enabled)
							{
								this.Hunting = false;
								this.Suicide = true;
							}
						}
						else if (this.HuntTarget.ClubActivityPhase >= 16)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
						}
						else if (!this.NEStairs.bounds.Contains(base.transform.position) && !this.NWStairs.bounds.Contains(base.transform.position) && !this.SEStairs.bounds.Contains(base.transform.position) && !this.SWStairs.bounds.Contains(base.transform.position))
						{
							if (!this.NEStairs.bounds.Contains(this.HuntTarget.transform.position) && !this.NWStairs.bounds.Contains(this.HuntTarget.transform.position) && !this.SEStairs.bounds.Contains(this.HuntTarget.transform.position) && !this.SWStairs.bounds.Contains(this.HuntTarget.transform.position))
							{
								if (this.Pathfinding.canMove)
								{
									Debug.Log("Slave is attacking target!");
									if (this.HuntTarget.Strength == 9)
									{
										this.AttackWillFail = true;
									}
									if (!this.AttackWillFail)
									{
										this.CharacterAnimation.CrossFade("f02_murderSuicide_00");
									}
									else
									{
										this.CharacterAnimation.CrossFade("f02_brokenAttackFailA_00");
										this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
										this.Police.Corpses++;
										GameObjectUtils.SetLayerRecursively(base.gameObject, 11);
										base.tag = "Blood";
										this.Ragdoll.MurderSuicideAnimation = true;
										this.Ragdoll.Disturbing = true;
										this.Dying = true;
										this.HipCollider.enabled = true;
										this.HipCollider.radius = 1f;
										this.MurderSuicidePhase = 9;
									}
									this.Pathfinding.canSearch = false;
									this.Pathfinding.canMove = false;
									this.Broken.Subtitle.text = string.Empty;
									this.MyController.radius = 0f;
									this.Broken.Done = true;
									if (!this.AttackWillFail)
									{
										AudioSource.PlayClipAtPoint(this.MurderSuicideSounds, base.transform.position + new Vector3(0f, 1f, 0f));
										AudioSource component3 = base.GetComponent<AudioSource>();
										component3.clip = this.MurderSuicideKiller;
										component3.Play();
									}
									if (this.HuntTarget.Shoving)
									{
										this.Yandere.CannotRecover = false;
									}
									if (this.StudentManager.CombatMinigame.Delinquent == this.HuntTarget)
									{
										this.StudentManager.CombatMinigame.Stop();
										this.StudentManager.CombatMinigame.ReleaseYandere();
									}
									if (!this.AttackWillFail)
									{
										this.HuntTarget.HipCollider.enabled = true;
										this.HuntTarget.HipCollider.radius = 1f;
										this.HuntTarget.DetectionMarker.Tex.enabled = false;
									}
									this.HuntTarget.TargetedForDistraction = false;
									this.HuntTarget.Pathfinding.canSearch = false;
									this.HuntTarget.Pathfinding.canMove = false;
									this.HuntTarget.WitnessCamera.Show = false;
									this.HuntTarget.CameraReacting = false;
									this.HuntTarget.Investigating = false;
									this.HuntTarget.Distracting = false;
									this.HuntTarget.Splashed = false;
									this.HuntTarget.Alarmed = false;
									this.HuntTarget.Burning = false;
									this.HuntTarget.Fleeing = false;
									this.HuntTarget.Routine = false;
									this.HuntTarget.Shoving = false;
									this.HuntTarget.Tripped = false;
									this.HuntTarget.Wet = false;
									this.HuntTarget.Hunter = this;
									this.HuntTarget.Prompt.Hide();
									this.HuntTarget.Prompt.enabled = false;
									if (this.Yandere.Pursuer == this.HuntTarget)
									{
										this.Yandere.Chased = false;
										this.Yandere.Pursuer = null;
									}
									if (!this.AttackWillFail)
									{
										if (!this.HuntTarget.Male)
										{
											this.HuntTarget.CharacterAnimation.CrossFade("f02_murderSuicide_01");
										}
										else
										{
											this.HuntTarget.CharacterAnimation.CrossFade("murderSuicide_01");
										}
										this.HuntTarget.Subtitle.UpdateLabel(SubtitleType.Dying, 0, 1f);
										this.HuntTarget.GetComponent<AudioSource>().clip = this.HuntTarget.MurderSuicideVictim;
										this.HuntTarget.GetComponent<AudioSource>().Play();
										this.Police.CorpseList[this.Police.Corpses] = this.HuntTarget.Ragdoll;
										this.Police.Corpses++;
										GameObjectUtils.SetLayerRecursively(this.HuntTarget.gameObject, 11);
										this.HuntTarget.tag = "Blood";
										this.HuntTarget.Ragdoll.MurderSuicideAnimation = true;
										this.HuntTarget.Ragdoll.Disturbing = true;
										this.HuntTarget.Dying = true;
										this.HuntTarget.MurderSuicidePhase = 100;
									}
									else
									{
										this.HuntTarget.CharacterAnimation.CrossFade("f02_brokenAttackFailB_00");
										this.HuntTarget.FightingSlave = true;
										this.HuntTarget.Distracted = true;
										this.StudentManager.UpdateMe(this.HuntTarget.StudentID);
									}
									this.HuntTarget.MyController.radius = 0f;
									this.HuntTarget.SpeechLines.Stop();
									this.HuntTarget.EyeShrink = 1f;
									this.HuntTarget.SpawnAlarmDisc();
									if (this.HuntTarget.Following)
									{
										this.Yandere.Followers--;
										this.Hearts.emission.enabled = false;
										this.HuntTarget.Following = false;
									}
									this.OriginalYPosition = this.HuntTarget.transform.position.y;
									if (this.MurderSuicidePhase == 0)
									{
										this.MurderSuicidePhase++;
									}
								}
								else
								{
									if (this.MurderSuicidePhase == 0 && this.CharacterAnimation["f02_brokenStandUp_00"].time >= this.CharacterAnimation["f02_brokenStandUp_00"].length)
									{
										this.Pathfinding.canSearch = true;
										this.Pathfinding.canMove = true;
									}
									if (this.MurderSuicidePhase > 0)
									{
										if (!this.AttackWillFail)
										{
											this.HuntTarget.targetRotation = Quaternion.LookRotation(this.HuntTarget.transform.position - base.transform.position);
											this.HuntTarget.MoveTowardsTarget(base.transform.position + base.transform.forward * 0.01f);
										}
										else
										{
											this.HuntTarget.targetRotation = Quaternion.LookRotation(base.transform.position - this.HuntTarget.transform.position);
											this.HuntTarget.MoveTowardsTarget(base.transform.position + base.transform.forward * 1f);
										}
										this.HuntTarget.transform.rotation = Quaternion.Slerp(this.HuntTarget.transform.rotation, this.HuntTarget.targetRotation, Time.deltaTime * 10f);
										this.HuntTarget.transform.position = new Vector3(this.HuntTarget.transform.position.x, this.OriginalYPosition, this.HuntTarget.transform.position.z);
										this.targetRotation = Quaternion.LookRotation(this.HuntTarget.transform.position - base.transform.position);
										base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
									}
									if (this.MurderSuicidePhase == 1)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 2.4f)
										{
											this.MyWeapon.transform.parent = this.ItemParent;
											this.MyWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
											this.MyWeapon.transform.localPosition = Vector3.zero;
											this.MyWeapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 2)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 3.3f)
										{
											GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.Ragdoll.BloodPoolSpawner.BloodPool, base.transform.position + base.transform.up * 0.012f + base.transform.forward, Quaternion.identity);
											gameObject6.transform.localEulerAngles = new Vector3(90f, UnityEngine.Random.Range(0f, 360f), 0f);
											gameObject6.transform.parent = this.Police.BloodParent;
											this.MyWeapon.Victims[this.HuntTarget.StudentID] = true;
											this.MyWeapon.Blood.enabled = true;
											if (!this.MyWeapon.Evidence)
											{
												this.MyWeapon.MurderWeapon = true;
												this.MyWeapon.Evidence = true;
												this.Police.MurderWeapons++;
											}
											UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);
											this.KnifeDown = true;
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 3)
									{
										if (!this.KnifeDown)
										{
											if (this.MyWeapon.transform.position.y < base.transform.position.y + 0.333333343f)
											{
												UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);
												this.KnifeDown = true;
												Debug.Log("Stab!");
											}
										}
										else if (this.MyWeapon.transform.position.y > base.transform.position.y + 0.333333343f)
										{
											this.KnifeDown = false;
										}
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 16.666666f)
										{
											Debug.Log("Released knife!");
											this.MyWeapon.transform.parent = null;
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 4)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 18.9f)
										{
											Debug.Log("Yanked out knife!");
											UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);
											this.MyWeapon.transform.parent = this.ItemParent;
											this.MyWeapon.transform.localPosition = Vector3.zero;
											this.MyWeapon.transform.localEulerAngles = Vector3.zero;
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 5)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 26.166666f)
										{
											Debug.Log("Stabbed neck!");
											UnityEngine.Object.Instantiate<GameObject>(this.BloodEffect, this.MyWeapon.transform.position, Quaternion.identity);
											this.MyWeapon.Victims[this.StudentID] = true;
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 6)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 30.5f)
										{
											Debug.Log("BLOOD FOUNTAIN!");
											this.BloodFountain.Play();
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 7)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= 31.5f)
										{
											this.BloodSprayCollider.SetActive(true);
											this.MurderSuicidePhase++;
										}
									}
									else if (this.MurderSuicidePhase == 8)
									{
										if (this.CharacterAnimation["f02_murderSuicide_00"].time >= this.CharacterAnimation["f02_murderSuicide_00"].length)
										{
											this.MyWeapon.transform.parent = null;
											this.MyWeapon.Drop();
											this.MyWeapon = null;
											this.StudentManager.StopHesitating();
											this.HuntTarget.HipCollider.radius = 0.5f;
											this.HuntTarget.BecomeRagdoll();
											this.HuntTarget.Ragdoll.Disturbing = false;
											this.HuntTarget.Ragdoll.MurderSuicide = true;
											this.HuntTarget.DeathType = DeathType.Weapon;
											if (this.BloodSprayCollider != null)
											{
												this.BloodSprayCollider.SetActive(false);
											}
											this.BecomeRagdoll();
											this.DeathType = DeathType.Weapon;
											this.StudentManager.MurderTakingPlace = false;
											this.Police.MurderSuicideScene = true;
											this.Ragdoll.MurderSuicide = true;
											this.MurderSuicide = true;
											this.Broken.HairPhysics[0].enabled = true;
											this.Broken.HairPhysics[1].enabled = true;
											this.Broken.enabled = false;
										}
									}
									else if (this.MurderSuicidePhase == 9)
									{
										this.CharacterAnimation.CrossFade("f02_brokenAttackFailA_00");
										if (this.CharacterAnimation["f02_brokenAttackFailA_00"].time >= this.CharacterAnimation["f02_brokenAttackFailA_00"].length)
										{
											this.MurderSuicidePhase = 1;
											this.Hunting = false;
											this.Suicide = true;
											this.HuntTarget.MyController.radius = 0.1f;
											this.HuntTarget.Distracted = false;
											this.HuntTarget.Routine = true;
										}
									}
								}
							}
						}
					}
					else
					{
						this.Hunting = false;
						this.Suicide = true;
					}
				}
				else
				{
					this.Hunting = false;
					this.Suicide = true;
				}
			}
			if (this.Suicide)
			{
				if (this.MurderSuicidePhase == 0)
				{
					if (this.CharacterAnimation["f02_brokenStandUp_00"].time >= this.CharacterAnimation["f02_brokenStandUp_00"].length)
					{
						this.MurderSuicidePhase++;
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Pathfinding.speed = 0f;
						this.CharacterAnimation.CrossFade("f02_suicide_00");
					}
				}
				else if (this.MurderSuicidePhase == 1)
				{
					if (this.Pathfinding.speed > 0f)
					{
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Pathfinding.speed = 0f;
						this.CharacterAnimation.CrossFade("f02_suicide_00");
					}
					if (this.CharacterAnimation["f02_suicide_00"].time >= 0.733333349f)
					{
						this.MyWeapon.transform.parent = this.ItemParent;
						this.MyWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
						this.MyWeapon.transform.localPosition = Vector3.zero;
						this.MyWeapon.transform.localEulerAngles = Vector3.zero;
						this.Broken.Subtitle.text = string.Empty;
						this.Broken.Done = true;
						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 2)
				{
					if (this.CharacterAnimation["f02_suicide_00"].time >= 4.16666651f)
					{
						Debug.Log("Stabbed neck!");
						UnityEngine.Object.Instantiate<GameObject>(this.StabBloodEffect, this.MyWeapon.transform.position, Quaternion.identity);
						this.MyWeapon.Victims[this.StudentID] = true;
						this.MyWeapon.Blood.enabled = true;
						if (!this.MyWeapon.Evidence)
						{
							this.MyWeapon.Evidence = true;
							this.Police.MurderWeapons++;
						}
						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 3)
				{
					if (this.CharacterAnimation["f02_suicide_00"].time >= 6.16666651f)
					{
						Debug.Log("BLOOD FOUNTAIN!");
						this.BloodFountain.gameObject.GetComponent<AudioSource>().Play();
						this.BloodFountain.Play();
						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 4)
				{
					if (this.CharacterAnimation["f02_suicide_00"].time >= 7f)
					{
						this.Ragdoll.BloodPoolSpawner.SpawnPool(base.transform);
						this.BloodSprayCollider.SetActive(true);
						this.MurderSuicidePhase++;
					}
				}
				else if (this.MurderSuicidePhase == 5 && this.CharacterAnimation["f02_suicide_00"].time >= this.CharacterAnimation["f02_suicide_00"].length)
				{
					this.MyWeapon.transform.parent = null;
					this.MyWeapon.Drop();
					this.MyWeapon = null;
					this.StudentManager.StopHesitating();
					if (this.BloodSprayCollider != null)
					{
						this.BloodSprayCollider.SetActive(false);
					}
					this.BecomeRagdoll();
					this.DeathType = DeathType.Weapon;
					this.Broken.HairPhysics[0].enabled = true;
					this.Broken.HairPhysics[1].enabled = true;
					this.Broken.enabled = false;
				}
			}
			if (this.CameraReacting)
			{
				this.PhotoPatience = Mathf.MoveTowards(this.PhotoPatience, 0f, Time.deltaTime);
				this.Prompt.Circle[0].fillAmount = 1f;
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				if (!this.Yandere.ClubAccessories[7].activeInHierarchy || this.Club == ClubType.Delinquent)
				{
					if (this.CameraReactPhase == 1)
					{
						if (this.CharacterAnimation[this.CameraAnims[1]].time >= this.CharacterAnimation[this.CameraAnims[1]].length)
						{
							this.CharacterAnimation.CrossFade(this.CameraAnims[2]);
							this.CameraReactPhase = 2;
							this.CameraPoseTimer = 1f;
						}
					}
					else if (this.CameraReactPhase == 2)
					{
						this.CameraPoseTimer -= Time.deltaTime;
						if (this.CameraPoseTimer <= 0f)
						{
							this.CharacterAnimation.CrossFade(this.CameraAnims[3]);
							this.CameraReactPhase = 3;
						}
					}
					else if (this.CameraReactPhase == 3)
					{
						if (this.CameraPoseTimer == 1f)
						{
							this.CharacterAnimation.CrossFade(this.CameraAnims[2]);
							this.CameraReactPhase = 2;
						}
						if (this.CharacterAnimation[this.CameraAnims[3]].time >= this.CharacterAnimation[this.CameraAnims[3]].length)
						{
							if (this.Persona == PersonaType.PhoneAddict || this.Sleuthing)
							{
								this.SmartPhone.SetActive(true);
							}
							if (!this.Male && this.Cigarette.activeInHierarchy)
							{
								this.SmartPhone.SetActive(false);
							}
							this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
							this.Obstacle.enabled = false;
							this.CameraReacting = false;
							this.Routine = true;
							this.ReadPhase = 0;
							if (this.Actions[this.Phase] == StudentActionType.Clean)
							{
								this.Scrubber.SetActive(true);
								if (this.CleaningRole == 5)
								{
									this.Eraser.SetActive(true);
								}
							}
						}
					}
				}
				else if (this.Yandere.Shutter.TargetStudent != this.StudentID)
				{
					this.CameraPoseTimer = Mathf.MoveTowards(this.CameraPoseTimer, 0f, Time.deltaTime);
					if (this.CameraPoseTimer == 0f)
					{
						if (this.Persona == PersonaType.PhoneAddict || this.Sleuthing)
						{
							this.SmartPhone.SetActive(true);
						}
						this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
						this.Obstacle.enabled = false;
						this.CameraReacting = false;
						this.Routine = true;
						this.ReadPhase = 0;
						if (this.Actions[this.Phase] == StudentActionType.Clean)
						{
							this.Scrubber.SetActive(true);
							if (this.CleaningRole == 5)
							{
								this.Eraser.SetActive(true);
							}
						}
					}
				}
				else
				{
					this.CameraPoseTimer = 1f;
				}
				if (this.InEvent)
				{
					this.CameraReacting = false;
					this.ReadPhase = 0;
				}
			}
			if (this.Investigating)
			{
				if (!this.YandereInnocent && this.InvestigationPhase < 100 && this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
				{
					if (Vector3.Distance(this.Yandere.transform.position, this.Giggle.transform.position) > 2.5f)
					{
						this.YandereInnocent = true;
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.InvestigationPhase = 100;
						this.InvestigationTimer = 0f;
					}
				}
				if (this.InvestigationPhase == 0)
				{
					if (this.InvestigationTimer < 5f)
					{
						if (this.Persona == PersonaType.Heroic)
						{
							this.InvestigationTimer += Time.deltaTime * 5f;
						}
						else if (this.Persona == PersonaType.Protective)
						{
							this.InvestigationTimer += Time.deltaTime * 50f;
						}
						else
						{
							this.InvestigationTimer += Time.deltaTime;
						}
						this.targetRotation = Quaternion.LookRotation(new Vector3(this.Giggle.transform.position.x, base.transform.position.y, this.Giggle.transform.position.z) - base.transform.position);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Pathfinding.target = this.Giggle.transform;
						this.CurrentDestination = this.Giggle.transform;
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						if ((this.Persona == PersonaType.Heroic && this.HeardScream) || (this.Persona == PersonaType.Protective && this.HeardScream))
						{
							Debug.Log("Sprinting 13");
							this.Pathfinding.speed = 4f;
						}
						else
						{
							this.Pathfinding.speed = 1f;
						}
						this.InvestigationPhase++;
					}
				}
				else if (this.InvestigationPhase == 1)
				{
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					if (this.DistanceToDestination > 1f)
					{
						if ((this.Persona == PersonaType.Heroic && this.HeardScream) || (this.Persona == PersonaType.Protective && this.HeardScream))
						{
							this.CharacterAnimation.CrossFade(this.SprintAnim);
						}
						else
						{
							this.CharacterAnimation.CrossFade(this.WalkAnim);
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.InvestigationPhase++;
					}
				}
				else if (this.InvestigationPhase == 2)
				{
					this.InvestigationTimer += Time.deltaTime;
					if (this.InvestigationTimer > 10f)
					{
						this.StopInvestigating();
					}
				}
				else if (this.InvestigationPhase == 100)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					this.InvestigationTimer += Time.deltaTime;
					if (this.InvestigationTimer > 2f)
					{
						this.StopInvestigating();
					}
				}
			}
			if (this.EndSearch)
			{
				this.MoveTowardsTarget(this.Pathfinding.target.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.Pathfinding.target.rotation, 10f * Time.deltaTime);
				this.PatrolTimer += Time.deltaTime * this.CharacterAnimation[this.PatrolAnim].speed;
				if (this.PatrolTimer > 5f)
				{
					this.StudentManager.CommunalLocker.RivalPhone.gameObject.SetActive(false);
					ScheduleBlock scheduleBlock16 = this.ScheduleBlocks[2];
					scheduleBlock16.destination = "Hangout";
					scheduleBlock16.action = "Hangout";
					ScheduleBlock scheduleBlock17 = this.ScheduleBlocks[4];
					scheduleBlock17.destination = "LunchSpot";
					scheduleBlock17.action = "Eat";
					ScheduleBlock scheduleBlock18 = this.ScheduleBlocks[7];
					scheduleBlock18.destination = "Hangout";
					scheduleBlock18.action = "Hangout";
					this.GetDestinations();
					Array.Copy(this.OriginalActions, this.Actions, this.OriginalActions.Length);
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.DistanceToDestination = 100f;
					this.LewdPhotos = this.StudentManager.CommunalLocker.RivalPhone.LewdPhotos;
					this.EndSearch = false;
					this.Phoneless = false;
					this.Routine = true;
				}
			}
			if (this.Shoving)
			{
				this.CharacterAnimation.CrossFade(this.ShoveAnim);
				if (this.CharacterAnimation[this.ShoveAnim].time > this.CharacterAnimation[this.ShoveAnim].length)
				{
					if ((this.Club != ClubType.Council && this.Persona != PersonaType.Violent) || this.RespectEarned)
					{
						this.Patience = 999;
					}
					if (this.Patience > 0)
					{
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Distracted = false;
						this.Shoving = false;
						this.Routine = true;
						this.Paired = false;
					}
					else if (this.Club == ClubType.Council)
					{
						this.Shoving = false;
						this.Spray();
					}
					else
					{
						this.SpawnAlarmDisc();
						this.SmartPhone.SetActive(false);
						this.Distracted = true;
						this.Threatened = true;
						this.Shoving = false;
						this.Alarmed = true;
					}
				}
			}
			if (this.Spraying && (double)this.CharacterAnimation[this.SprayAnim].time > 0.66666)
			{
				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.Drop();
				}
				this.Yandere.EmptyHands();
				this.PepperSprayEffect.Play();
				this.Spraying = false;
			}
			if (this.SentHome)
			{
				if (this.SentHomePhase == 0)
				{
					if (this.Shy)
					{
						this.CharacterAnimation[this.ShyAnim].weight = 0f;
					}
					this.CharacterAnimation.CrossFade(this.SentHomeAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.SentHomePhase++;
					if (this.SmartPhone.activeInHierarchy)
					{
						this.CharacterAnimation[this.SentHomeAnim].time = 1.5f;
						this.SentHomePhase++;
					}
				}
				else if (this.SentHomePhase == 1)
				{
					if (this.CharacterAnimation[this.SentHomeAnim].time > 0.66666f)
					{
						this.SmartPhone.SetActive(true);
						this.SentHomePhase++;
					}
				}
				else if (this.SentHomePhase == 2 && this.CharacterAnimation[this.SentHomeAnim].time > this.CharacterAnimation[this.SentHomeAnim].length)
				{
					Debug.Log("Sprinting 14");
					this.SprintAnim = this.OriginalSprintAnim;
					this.CharacterAnimation.CrossFade(this.SprintAnim);
					this.CurrentDestination = this.StudentManager.Exit;
					this.Pathfinding.target = this.StudentManager.Exit;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.SmartPhone.SetActive(false);
					this.Pathfinding.speed = 4f;
					this.SentHomePhase++;
				}
			}
			if (this.DramaticReaction)
			{
				this.DramaticCamera.transform.Translate(Vector3.forward * Time.deltaTime * 0.01f);
				if (this.DramaticPhase == 0)
				{
					this.DramaticCamera.rect = new Rect(0f, Mathf.Lerp(this.DramaticCamera.rect.y, 0.25f, Time.deltaTime * 10f), 1f, Mathf.Lerp(this.DramaticCamera.rect.height, 0.5f, Time.deltaTime * 10f));
					this.DramaticTimer += Time.deltaTime;
					if (this.DramaticTimer > 1f)
					{
						this.DramaticTimer = 0f;
						this.DramaticPhase++;
					}
				}
				else if (this.DramaticPhase == 1)
				{
					this.DramaticCamera.rect = new Rect(0f, Mathf.Lerp(this.DramaticCamera.rect.y, 0.5f, Time.deltaTime * 10f), 1f, Mathf.Lerp(this.DramaticCamera.rect.height, 0f, Time.deltaTime * 10f));
					this.DramaticTimer += Time.deltaTime;
					if (this.DramaticCamera.rect.height < 0.01f || this.DramaticTimer > 0.5f)
					{
						Debug.Log("Disabling Dramatic Camera.");
						this.DramaticCamera.gameObject.SetActive(false);
						this.AttackReaction();
						this.DramaticPhase++;
					}
				}
			}
			if (this.HitReacting && this.CharacterAnimation[this.SithReactAnim].time >= this.CharacterAnimation[this.SithReactAnim].length)
			{
				this.Persona = PersonaType.SocialButterfly;
				this.PersonaReaction();
				this.HitReacting = false;
			}
			if (this.Eaten)
			{
				if (this.Yandere.Eating)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				}
				if (this.CharacterAnimation[this.EatVictimAnim].time >= this.CharacterAnimation[this.EatVictimAnim].length)
				{
					this.BecomeRagdoll();
				}
			}
			if (this.Electrified && this.CharacterAnimation[this.ElectroAnim].time >= this.CharacterAnimation[this.ElectroAnim].length)
			{
				this.Electrocuted = true;
				this.BecomeRagdoll();
				this.DeathType = DeathType.Electrocution;
			}
			if (this.BreakingUpFight)
			{
				this.targetRotation = this.Yandere.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * 0.5f);
				if (this.StudentID == 87)
				{
					if (this.CharacterAnimation[this.BreakUpAnim].time >= 4f)
					{
						this.CandyBar.SetActive(false);
					}
					else if ((double)this.CharacterAnimation[this.BreakUpAnim].time >= 0.16666666666)
					{
						this.CandyBar.SetActive(true);
					}
				}
				if (this.CharacterAnimation[this.BreakUpAnim].time >= this.CharacterAnimation[this.BreakUpAnim].length)
				{
					this.ReturnToRoutine();
				}
			}
			if (this.Tripping)
			{
				this.MoveTowardsTarget(new Vector3(0f, 0f, base.transform.position.z));
				this.CharacterAnimation.CrossFade("trip_00");
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				if (this.CharacterAnimation["trip_00"].time >= 0.5f && this.CharacterAnimation["trip_00"].time <= 5.5f && this.StudentManager.Gate.Crushing)
				{
					this.BecomeRagdoll();
					this.Ragdoll.Decapitated = true;
					UnityEngine.Object.Instantiate<GameObject>(this.SquishyBloodEffect, this.Head.position, Quaternion.identity);
				}
				if (this.CharacterAnimation["trip_00"].time >= this.CharacterAnimation["trip_00"].length)
				{
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Distracted = true;
					this.Tripping = false;
					this.Routine = true;
					this.Tripped = true;
				}
			}
			if (this.SeekingMedicine)
			{
				if (this.StudentManager.Students[90] == null)
				{
					if (this.SeekMedicinePhase < 5)
					{
						this.SeekMedicinePhase = 5;
					}
				}
				else if ((!this.StudentManager.Students[90].gameObject.activeInHierarchy || this.StudentManager.Students[90].Dying) && this.SeekMedicinePhase < 5)
				{
					this.SeekMedicinePhase = 5;
				}
				if (this.SeekMedicinePhase == 0)
				{
					this.CurrentDestination = this.StudentManager.Students[90].transform;
					this.Pathfinding.target = this.StudentManager.Students[90].transform;
					this.SeekMedicinePhase++;
				}
				else if (this.SeekMedicinePhase == 1)
				{
					this.CharacterAnimation.CrossFade(this.SprintAnim);
					if (this.DistanceToDestination < 1f)
					{
						StudentScript studentScript2 = this.StudentManager.Students[90];
						if (studentScript2.Investigating)
						{
							studentScript2.StopInvestigating();
						}
						this.StudentManager.UpdateStudents(studentScript2.StudentID);
						studentScript2.Pathfinding.canSearch = false;
						studentScript2.Pathfinding.canMove = false;
						studentScript2.RetreivingMedicine = true;
						studentScript2.Pathfinding.speed = 0f;
						studentScript2.CameraReacting = false;
						studentScript2.TargetStudent = this;
						studentScript2.Routine = false;
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Pathfinding.speed = 0f;
						this.Subtitle.UpdateLabel(SubtitleType.RequestMedicine, 0, 5f);
						this.SeekMedicinePhase++;
					}
				}
				else if (this.SeekMedicinePhase == 2)
				{
					StudentScript studentScript3 = this.StudentManager.Students[90];
					this.targetRotation = Quaternion.LookRotation(new Vector3(studentScript3.transform.position.x, base.transform.position.y, studentScript3.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					this.MedicineTimer += Time.deltaTime;
					if (this.MedicineTimer > 5f)
					{
						this.SeekMedicinePhase++;
						this.MedicineTimer = 0f;
						studentScript3.Pathfinding.canSearch = true;
						studentScript3.Pathfinding.canMove = true;
						studentScript3.RetrieveMedicinePhase++;
					}
				}
				else if (this.SeekMedicinePhase != 3)
				{
					if (this.SeekMedicinePhase == 4)
					{
						StudentScript studentScript4 = this.StudentManager.Students[90];
						this.targetRotation = Quaternion.LookRotation(new Vector3(studentScript4.transform.position.x, base.transform.position.y, studentScript4.transform.position.z) - base.transform.position);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					}
					else if (this.SeekMedicinePhase == 5)
					{
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						ScheduleBlock scheduleBlock19 = this.ScheduleBlocks[this.Phase];
						scheduleBlock19.destination = "InfirmarySeat";
						scheduleBlock19.action = "Relax";
						this.GetDestinations();
						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
						this.Pathfinding.speed = 2f;
						this.RelaxAnim = this.HeadacheSitAnim;
						this.SeekingMedicine = false;
						this.Routine = true;
					}
				}
			}
			if (this.RetreivingMedicine)
			{
				if (this.RetrieveMedicinePhase == 0)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				}
				else if (this.RetrieveMedicinePhase == 1)
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);
					this.CurrentDestination = this.StudentManager.MedicineCabinet;
					this.Pathfinding.target = this.StudentManager.MedicineCabinet;
					this.Pathfinding.speed = 1f;
					this.RetrieveMedicinePhase++;
				}
				else if (this.RetrieveMedicinePhase == 2)
				{
					if (this.DistanceToDestination < 1f)
					{
						this.StudentManager.CabinetDoor.Locked = false;
						this.StudentManager.CabinetDoor.Open = true;
						this.StudentManager.CabinetDoor.Timer = 0f;
						this.CurrentDestination = this.TargetStudent.transform;
						this.Pathfinding.target = this.TargetStudent.transform;
						this.RetrieveMedicinePhase++;
					}
				}
				else if (this.RetrieveMedicinePhase == 3)
				{
					if (this.DistanceToDestination < 1f)
					{
						this.CurrentDestination = this.TargetStudent.transform;
						this.Pathfinding.target = this.TargetStudent.transform;
						this.RetrieveMedicinePhase++;
					}
				}
				else if (this.RetrieveMedicinePhase == 4)
				{
					if (this.DistanceToDestination < 1f)
					{
						this.CharacterAnimation.CrossFade("f02_giveItem_00");
						if (this.TargetStudent.Male)
						{
							this.TargetStudent.CharacterAnimation.CrossFade("eatItem_00");
						}
						else
						{
							this.TargetStudent.CharacterAnimation.CrossFade("f02_eatItem_00");
						}
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.TargetStudent.SeekMedicinePhase++;
						this.RetrieveMedicinePhase++;
					}
				}
				else if (this.RetrieveMedicinePhase == 5)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.TargetStudent.transform.position.x, base.transform.position.y, this.TargetStudent.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
					this.MedicineTimer += Time.deltaTime;
					if (this.MedicineTimer > 3f)
					{
						this.CharacterAnimation.CrossFade(this.WalkAnim);
						this.CurrentDestination = this.StudentManager.MedicineCabinet;
						this.Pathfinding.target = this.StudentManager.MedicineCabinet;
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.TargetStudent.SeekMedicinePhase++;
						this.RetrieveMedicinePhase++;
					}
				}
				else if (this.RetrieveMedicinePhase == 6 && this.DistanceToDestination < 1f)
				{
					this.StudentManager.CabinetDoor.Locked = true;
					this.StudentManager.CabinetDoor.Open = false;
					this.StudentManager.CabinetDoor.Timer = 0f;
					this.RetreivingMedicine = false;
					this.Routine = true;
				}
			}
			if (this.EatingSnack)
			{
				if (this.SnackPhase == 0)
				{
					this.CharacterAnimation.CrossFade(this.EatChipsAnim);
					this.SmartPhone.SetActive(false);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.SnackTimer += Time.deltaTime;
					if (this.SnackTimer > 10f)
					{
						UnityEngine.Object.Destroy(this.BagOfChips);
						if (this.StudentID != this.StudentManager.RivalID)
						{
							this.StudentManager.GetNearestFountain(this);
							this.Pathfinding.target = this.DrinkingFountain.DrinkPosition;
							this.CurrentDestination = this.DrinkingFountain.DrinkPosition;
							this.Pathfinding.canSearch = true;
							this.Pathfinding.canMove = true;
							this.SnackTimer = 0f;
						}
						this.SnackPhase++;
					}
				}
				else if (this.SnackPhase == 1)
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);
					if (this.Persona == PersonaType.PhoneAddict)
					{
						this.SmartPhone.SetActive(true);
					}
					if (this.DistanceToDestination < 1f)
					{
						this.SmartPhone.SetActive(false);
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.SnackPhase++;
					}
				}
				else if (this.SnackPhase == 2)
				{
					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.CharacterAnimation.CrossFade(this.DrinkFountainAnim);
					this.MoveTowardsTarget(this.DrinkingFountain.DrinkPosition.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.DrinkingFountain.DrinkPosition.rotation, 10f * Time.deltaTime);
					if (this.CharacterAnimation[this.DrinkFountainAnim].time >= this.CharacterAnimation[this.DrinkFountainAnim].length)
					{
						this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
						this.DrinkingFountain.Occupied = false;
						this.EquipCleaningItems();
						this.EatingSnack = false;
						this.Private = false;
						this.Routine = true;
						this.StudentManager.UpdateMe(this.StudentID);
						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
					}
					else if (this.CharacterAnimation[this.DrinkFountainAnim].time > 0.5f && this.CharacterAnimation[this.DrinkFountainAnim].time < 1.5f)
					{
						this.DrinkingFountain.WaterStream.Play();
					}
				}
			}
			if (this.Dodging)
			{
				if (this.CharacterAnimation[this.DodgeAnim].time >= this.CharacterAnimation[this.DodgeAnim].length)
				{
					this.Routine = true;
					this.Dodging = false;
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
				}
				else if (this.CharacterAnimation[this.DodgeAnim].time < 0.66666f)
				{
					this.MyController.Move(base.transform.forward * -1f * this.DodgeSpeed * Time.deltaTime);
					this.MyController.Move(Physics.gravity * 0.1f);
					if (this.DodgeSpeed > 0f)
					{
						this.DodgeSpeed = Mathf.MoveTowards(this.DodgeSpeed, 0f, Time.deltaTime * 3f);
					}
				}
			}
			if (!this.Guarding && this.InvestigatingBloodPool)
			{
				if (this.DistanceToDestination < 1f)
				{
					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.CharacterAnimation[this.InspectBloodAnim].speed = 1f;
					this.CharacterAnimation.CrossFade(this.InspectBloodAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Distracted = true;
					if (this.CharacterAnimation[this.InspectBloodAnim].time >= this.CharacterAnimation[this.InspectBloodAnim].length || this.Persona == PersonaType.Strict)
					{
						bool flag2 = false;
						if (this.Club == ClubType.Cooking && this.CurrentAction == StudentActionType.ClubAction)
						{
							flag2 = true;
						}
						if (this.WitnessedWeapon)
						{
							bool flag3 = false;
							if (!this.Teacher && this.BloodPool.GetComponent<WeaponScript>().Metal && this.StudentManager.MetalDetectors)
							{
								flag3 = true;
							}
							if (!this.WitnessedBloodyWeapon && this.StudentID > 1 && !flag3 && this.CurrentAction != StudentActionType.SitAndTakeNotes && this.Indoors && !flag2 && this.Club != ClubType.Delinquent && !this.BloodPool.GetComponent<WeaponScript>().Dangerous && this.BloodPool.GetComponent<WeaponScript>().Returner == null)
							{
								Debug.Log(this.Name + " has picked up a weapon with intent to return it to its original location.");
								this.CharacterAnimation[this.PickUpAnim].time = 0f;
								this.BloodPool.GetComponent<WeaponScript>().Prompt.Hide();
								this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = false;
								this.BloodPool.GetComponent<WeaponScript>().enabled = false;
								this.BloodPool.GetComponent<WeaponScript>().Returner = this;
								if (this.StudentID == 41)
								{
									this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5f);
								}
								else
								{
									this.Subtitle.UpdateLabel(SubtitleType.ReturningWeapon, 0, 5f);
								}
								this.ReturningMisplacedWeapon = true;
								this.ReportingBlood = false;
								this.Distracted = false;
								this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
							}
						}
						this.InvestigatingBloodPool = false;
						this.WitnessCooldownTimer = 5f;
						if (this.WitnessedLimb)
						{
							this.SpawnAlarmDisc();
						}
						if (!this.ReturningMisplacedWeapon)
						{
							if (this.StudentManager.BloodReporter == this && this.WitnessedWeapon && !this.BloodPool.GetComponent<WeaponScript>().Dangerous)
							{
								this.StudentManager.BloodReporter = null;
							}
							if (this.StudentManager.BloodReporter == this && this.StudentID > 1)
							{
								if (this.Persona != PersonaType.Strict && this.Persona != PersonaType.Violent)
								{
									Debug.Log(this.Name + " has changed from their original Persona into a Teacher's Pet.");
									this.Persona = PersonaType.TeachersPet;
								}
								this.PersonaReaction();
							}
							else if (this.WitnessedWeapon && !this.WitnessedBloodyWeapon)
							{
								this.CurrentDestination = this.Destinations[this.Phase];
								this.Pathfinding.target = this.Destinations[this.Phase];
								this.LastSuspiciousObject2 = this.LastSuspiciousObject;
								this.LastSuspiciousObject = this.BloodPool;
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.Pathfinding.speed = 1f;
								if (this.StudentID == 41)
								{
									this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5f);
								}
								else if (this.Club == ClubType.Delinquent)
								{
									this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 2, 3f);
								}
								else if (this.BloodPool.GetComponent<WeaponScript>().Dangerous)
								{
									this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 0, 3f);
								}
								else
								{
									this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 1, 3f);
								}
								this.WitnessedSomething = false;
								this.WitnessedWeapon = false;
								this.Distracted = false;
								this.Routine = true;
								this.BloodPool = null;
								if (this.StudentManager.BloodReporter == this)
								{
									if (this.Persona != PersonaType.Strict && this.Persona != PersonaType.Violent)
									{
										Debug.Log(this.Name + " has changed from their original Persona into a Teacher's Pet.");
										this.Persona = PersonaType.TeachersPet;
									}
									this.PersonaReaction();
								}
							}
							else
							{
								if (this.Persona != PersonaType.Strict && this.Persona != PersonaType.Violent)
								{
									Debug.Log(this.Name + " has changed from their original Persona into a Teacher's Pet.");
									this.Persona = PersonaType.TeachersPet;
								}
								this.PersonaReaction();
							}
							this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
						}
					}
				}
				else if (this.Persona == PersonaType.Strict)
				{
					if (this.WitnessedWeapon && this.BloodPool.GetComponent<WeaponScript>().Returner)
					{
						this.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 3f);
						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
						this.InvestigatingBloodPool = false;
						this.WitnessedBloodyWeapon = false;
						this.WitnessedBloodPool = false;
						this.WitnessedSomething = false;
						this.WitnessedWeapon = false;
						this.Distracted = false;
						this.Routine = true;
						this.BloodPool = null;
						this.WitnessCooldownTimer = 5f;
					}
					else if (this.BloodPool.parent == this.Yandere.RightHand || !this.BloodPool.gameObject.activeInHierarchy)
					{
						this.InvestigatingBloodPool = false;
						this.WitnessedBloodyWeapon = false;
						this.WitnessedBloodPool = false;
						this.WitnessedSomething = false;
						this.WitnessedWeapon = false;
						this.Distracted = false;
						this.Routine = true;
						this.BloodPool = null;
						this.WitnessCooldownTimer = 5f;
						this.AlarmTimer = 0f;
						this.Alarm = 200f;
					}
				}
				else if (this.BloodPool == null || (this.WitnessedWeapon && this.BloodPool.parent != null) || (this.WitnessedBloodPool && this.BloodPool.parent == this.Yandere.RightHand) || (this.WitnessedWeapon && this.BloodPool.GetComponent<WeaponScript>().Returner))
				{
					this.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 3f);
					if (this.Club == ClubType.Cooking && this.CurrentAction == StudentActionType.ClubAction)
					{
						this.GetFoodTarget();
					}
					else
					{
						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
					}
					this.InvestigatingBloodPool = false;
					this.WitnessedBloodyWeapon = false;
					this.WitnessedBloodPool = false;
					this.WitnessedSomething = false;
					this.WitnessedWeapon = false;
					this.Distracted = false;
					this.Routine = true;
					this.BloodPool = null;
					this.WitnessCooldownTimer = 5f;
				}
			}
			if (this.ReturningMisplacedWeapon)
			{
				if (this.ReturningMisplacedWeaponPhase == 0)
				{
					this.CharacterAnimation.CrossFade(this.PickUpAnim);
					if ((this.Club == ClubType.Council || this.Teacher) && this.CharacterAnimation[this.PickUpAnim].time >= 0.33333f)
					{
						this.Handkerchief.SetActive(true);
					}
					if (this.CharacterAnimation[this.PickUpAnim].time >= 2f)
					{
						this.BloodPool.parent = this.RightHand;
						this.BloodPool.localPosition = new Vector3(0f, 0f, 0f);
						this.BloodPool.localEulerAngles = new Vector3(0f, 0f, 0f);
						if (this.Club != ClubType.Council && !this.Teacher)
						{
							this.BloodPool.GetComponent<WeaponScript>().FingerprintID = this.StudentID;
						}
					}
					if (this.CharacterAnimation[this.PickUpAnim].time >= this.CharacterAnimation[this.PickUpAnim].length)
					{
						this.CurrentDestination = this.BloodPool.GetComponent<WeaponScript>().Origin;
						this.Pathfinding.target = this.BloodPool.GetComponent<WeaponScript>().Origin;
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Pathfinding.speed = 1f;
						this.Hurry = false;
						this.ReturningMisplacedWeaponPhase++;
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);
					if (this.DistanceToDestination < 1.1f)
					{
						this.ReturnMisplacedWeapon();
					}
				}
			}
			if (this.SentToLocker && !this.CheckingNote)
			{
				this.CharacterAnimation.CrossFade(this.RunAnim);
			}
			if (this.Stripping)
			{
				this.CharacterAnimation.CrossFade(this.StripAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				if (this.CharacterAnimation[this.StripAnim].time >= 1.5f)
				{
					if (this.Schoolwear != 1)
					{
						this.Schoolwear = 1;
						this.ChangeSchoolwear();
					}
					if (this.CharacterAnimation[this.StripAnim].time > this.CharacterAnimation[this.StripAnim].length)
					{
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Stripping = false;
						this.Routine = true;
					}
				}
			}
		}
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x00172970 File Offset: 0x00170D70
	private void UpdateVisibleCorpses()
	{
		this.VisibleCorpses.Clear();
		this.ID = 0;
		while (this.ID < this.Police.Corpses)
		{
			RagdollScript ragdollScript = this.Police.CorpseList[this.ID];
			if (ragdollScript != null && !ragdollScript.Hidden)
			{
				Collider collider = ragdollScript.AllColliders[0];
				bool flag = false;
				if (collider.transform.position.y < base.transform.position.y + 4f)
				{
					flag = true;
				}
				if (flag && this.CanSeeObject(collider.gameObject, collider.transform.position, this.CorpseLayers, this.Mask))
				{
					this.VisibleCorpses.Add(ragdollScript.StudentID);
					this.Corpse = ragdollScript;
					if (this.Club == ClubType.Delinquent && this.Corpse.Student.Club == ClubType.Bully)
					{
						this.ScaredAnim = this.EvilWitnessAnim;
						this.Persona = PersonaType.Evil;
					}
					if (this.Persona == PersonaType.TeachersPet && this.StudentManager.Reporter == null && !this.Police.Called)
					{
						this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;
						this.StudentManager.CorpseLocation.LookAt(base.transform.position);
						this.StudentManager.CorpseLocation.Translate(this.StudentManager.CorpseLocation.forward * -1f);
						this.StudentManager.LowerCorpsePosition();
						this.StudentManager.Reporter = this;
						this.ReportingMurder = true;
						this.DetermineCorpseLocation();
					}
				}
			}
			this.ID++;
		}
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x00172B68 File Offset: 0x00170F68
	private void UpdateVisibleBlood()
	{
		this.ID = 0;
		while (this.ID < this.StudentManager.Blood.Length)
		{
			if (!(this.BloodPool == null))
			{
				break;
			}
			Collider collider = this.StudentManager.Blood[this.ID];
			if (collider != null && Vector3.Distance(base.transform.position, collider.transform.position) < this.VisionDistance && this.CanSeeObject(collider.gameObject, collider.transform.position))
			{
				this.BloodPool = collider.transform;
				this.WitnessedBloodPool = true;
				if (this.Club != ClubType.Delinquent && this.StudentManager.BloodReporter == null && !this.Police.Called && !this.LostTeacherTrust)
				{
					this.StudentManager.BloodLocation.position = this.BloodPool.position;
					this.StudentManager.BloodLocation.LookAt(new Vector3(base.transform.position.x, this.StudentManager.BloodLocation.position.y, base.transform.position.z));
					this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward * -1f);
					this.StudentManager.LowerBloodPosition();
					this.StudentManager.BloodReporter = this;
					this.ReportingBlood = true;
					this.DetermineBloodLocation();
				}
			}
			this.ID++;
		}
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x00172D30 File Offset: 0x00171130
	private void UpdateVisibleLimbs()
	{
		this.ID = 0;
		while (this.ID < this.StudentManager.Limbs.Length)
		{
			if (!(this.BloodPool == null))
			{
				break;
			}
			Collider collider = this.StudentManager.Limbs[this.ID];
			if (collider != null && this.CanSeeObject(collider.gameObject, collider.transform.position))
			{
				this.BloodPool = collider.transform;
				this.WitnessedLimb = true;
				if (this.Club != ClubType.Delinquent && this.StudentManager.BloodReporter == null && !this.Police.Called && !this.LostTeacherTrust)
				{
					this.StudentManager.BloodLocation.position = this.BloodPool.position;
					this.StudentManager.BloodLocation.LookAt(new Vector3(base.transform.position.x, this.StudentManager.BloodLocation.position.y, base.transform.position.z));
					this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward * -1f);
					this.StudentManager.LowerBloodPosition();
					this.StudentManager.BloodReporter = this;
					this.ReportingBlood = true;
					this.DetermineBloodLocation();
				}
			}
			this.ID++;
		}
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x00172ED4 File Offset: 0x001712D4
	private void UpdateVisibleWeapons()
	{
		this.ID = 0;
		while (this.ID < this.Yandere.WeaponManager.Weapons.Length)
		{
			if (this.Yandere.WeaponManager.Weapons[this.ID] != null && (this.Yandere.WeaponManager.Weapons[this.ID].Blood.enabled || (this.Yandere.WeaponManager.Weapons[this.ID].Misplaced && this.Yandere.WeaponManager.Weapons[this.ID].transform.parent == null)))
			{
				if (!(this.BloodPool == null))
				{
					break;
				}
				if (this.Yandere.WeaponManager.Weapons[this.ID].transform != this.LastSuspiciousObject && this.Yandere.WeaponManager.Weapons[this.ID].transform != this.LastSuspiciousObject2 && this.Yandere.WeaponManager.Weapons[this.ID].enabled)
				{
					Collider myCollider = this.Yandere.WeaponManager.Weapons[this.ID].MyCollider;
					if (myCollider != null && this.CanSeeObject(myCollider.gameObject, myCollider.transform.position))
					{
						Debug.Log(this.Name + " has seen a dropped weapon on the ground.");
						this.BloodPool = myCollider.transform;
						if (this.Yandere.WeaponManager.Weapons[this.ID].Blood.enabled)
						{
							this.WitnessedBloodyWeapon = true;
						}
						this.WitnessedWeapon = true;
						if (this.Club != ClubType.Delinquent && this.StudentManager.BloodReporter == null && !this.Police.Called && !this.LostTeacherTrust)
						{
							this.StudentManager.BloodLocation.position = this.BloodPool.position;
							this.StudentManager.BloodLocation.LookAt(new Vector3(base.transform.position.x, this.StudentManager.BloodLocation.position.y, base.transform.position.z));
							this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward * -1f);
							this.StudentManager.LowerBloodPosition();
							this.StudentManager.BloodReporter = this;
							this.ReportingBlood = true;
							this.DetermineBloodLocation();
						}
					}
				}
			}
			this.ID++;
		}
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x001731D8 File Offset: 0x001715D8
	private void UpdateVision()
	{
		bool flag = false;
		if (this.Distracted)
		{
			flag = true;
			if (!this.Hunting && this.ClubActivityPhase < 15 && (this.Yandere.Attacking || this.Yandere.Dragging || this.Yandere.Carrying))
			{
				flag = false;
			}
		}
		if (!flag)
		{
			bool flag2 = true;
			if (this.Yandere.Pursuer == null && this.Yandere.Pursuer == this)
			{
				flag2 = false;
			}
			if (!this.WitnessedMurder && !this.CheckingNote && !this.Shoving && !this.Struggling && flag2 && !this.Drownable)
			{
				if (this.Police.Corpses > 0)
				{
					this.UpdateVisibleCorpses();
					if (this.VisibleCorpses.Count > 0)
					{
						if (!this.WitnessedCorpse)
						{
							Debug.Log(this.Name + " discovered a corpse.");
							this.SawCorpseThisFrame = true;
							if (this.Club == ClubType.Delinquent && this.Corpse.Student.Club == ClubType.Bully)
							{
								this.FoundEnemyCorpse = true;
							}
							if (this.Persona == PersonaType.Sleuth)
							{
								if (this.Sleuthing)
								{
									this.Persona = PersonaType.PhoneAddict;
									this.SmartPhone.SetActive(true);
								}
								else
								{
									this.Persona = PersonaType.SocialButterfly;
								}
							}
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							if (!this.Male)
							{
								this.CharacterAnimation["f02_smile_00"].weight = 0f;
							}
							this.AlarmTimer = 0f;
							this.Alarm = 200f;
							this.LastKnownCorpse = this.Corpse.AllColliders[0].transform.position;
							this.WitnessedBloodyWeapon = false;
							this.WitnessedBloodPool = false;
							this.WitnessedSomething = false;
							this.WitnessedWeapon = false;
							this.WitnessedCorpse = true;
							this.WitnessedLimb = false;
							if (this.ReturningMisplacedWeapon && this.ReturningMisplacedWeapon)
							{
								this.DropMisplacedWeapon();
							}
							this.InvestigatingBloodPool = false;
							this.Investigating = false;
							this.EatingSnack = false;
							this.Threatened = false;
							this.SentHome = false;
							this.Routine = false;
							if (this.Persona == PersonaType.Spiteful && ((this.Bullied && this.Corpse.Student.Club == ClubType.Bully) || this.Corpse.Student.Bullied))
							{
								this.ScaredAnim = this.EvilWitnessAnim;
								this.Persona = PersonaType.Evil;
							}
							this.ForgetRadio();
							if (this.Wet)
							{
								this.Persona = PersonaType.Loner;
							}
							if (this.Corpse.Disturbing)
							{
								if (this.Corpse.BurningAnimation)
								{
									this.WalkBackTimer = 5f;
									this.WalkBack = true;
									this.Hesitation = 0.5f;
								}
								if (this.Corpse.MurderSuicideAnimation)
								{
									this.WitnessedMindBrokenMurder = true;
									this.WalkBackTimer = 5f;
									this.WalkBack = true;
									this.Hesitation = 1f;
								}
								if (this.Corpse.ChokingAnimation)
								{
									this.WalkBackTimer = 0f;
									this.WalkBack = true;
									this.Hesitation = 0.6f;
								}
								if (this.Corpse.ElectrocutionAnimation)
								{
									this.WalkBackTimer = 0f;
									this.WalkBack = true;
									this.Hesitation = 0.5f;
								}
							}
							this.StudentManager.UpdateMe(this.StudentID);
							if (!this.Teacher)
							{
								this.SpawnAlarmDisc();
							}
							else
							{
								this.AlarmTimer = 3f;
							}
							ParticleSystem.EmissionModule emission = this.Hearts.emission;
							if (this.Talking)
							{
								this.DialogueWheel.End();
								emission.enabled = false;
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.Obstacle.enabled = false;
								this.Talk.enabled = false;
								this.Talking = false;
								this.Waiting = false;
								this.StudentManager.EnablePrompts();
							}
							if (this.Following)
							{
								emission.enabled = false;
								this.Yandere.Followers--;
								this.Following = false;
							}
						}
						if (this.Corpse.Dragged || this.Corpse.Dumped)
						{
							if (this.Teacher)
							{
								this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, UnityEngine.Random.Range(1, 3), 3f);
								this.StudentManager.Portal.SetActive(false);
							}
							if (!this.Yandere.Egg)
							{
								this.WitnessMurder();
							}
						}
					}
				}
				if (this.VisibleCorpses.Count == 0 && !this.WitnessedCorpse)
				{
					if (this.WitnessCooldownTimer > 0f)
					{
						this.WitnessCooldownTimer = Mathf.MoveTowards(this.WitnessCooldownTimer, 0f, Time.deltaTime);
					}
					else if ((this.StudentID == this.StudentManager.CurrentID || (this.Persona == PersonaType.Strict && this.Fleeing)) && !this.Wet && !this.Guarding && !this.IgnoreBlood && !this.InvestigatingPossibleDeath && !this.Emetic && !this.Sedated && !this.Headache && !this.SentHome && !this.Slave && !this.Talking)
					{
						if (this.BloodPool == null && this.StudentManager.Police.LimbParent.childCount > 0)
						{
							this.UpdateVisibleLimbs();
						}
						if (this.BloodPool == null && (this.Police.BloodyWeapons > 0 || this.Yandere.WeaponManager.MisplacedWeapons > 0) && !this.InvestigatingPossibleLimb && !this.Alarmed)
						{
							this.UpdateVisibleWeapons();
						}
						if (this.BloodPool == null && this.StudentManager.Police.BloodParent.childCount > 0 && !this.InvestigatingPossibleLimb)
						{
							this.UpdateVisibleBlood();
						}
						if (this.BloodPool != null && !this.WitnessedSomething)
						{
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							if (!this.Male)
							{
								this.CharacterAnimation["f02_smile_00"].weight = 0f;
							}
							this.AlarmTimer = 0f;
							this.Alarm = 200f;
							this.LastKnownBlood = this.BloodPool.transform.position;
							this.NotAlarmedByYandereChan = true;
							this.WitnessedSomething = true;
							this.Investigating = false;
							this.EatingSnack = false;
							this.Threatened = false;
							this.SentHome = false;
							this.Routine = false;
							this.ForgetRadio();
							this.StudentManager.UpdateMe(this.StudentID);
							if (this.Teacher)
							{
								this.AlarmTimer = 3f;
							}
							ParticleSystem.EmissionModule emission2 = this.Hearts.emission;
							if (this.Talking)
							{
								this.DialogueWheel.End();
								emission2.enabled = false;
								this.Pathfinding.canSearch = true;
								this.Pathfinding.canMove = true;
								this.Obstacle.enabled = false;
								this.Talk.enabled = false;
								this.Talking = false;
								this.Waiting = false;
								this.StudentManager.EnablePrompts();
							}
							if (this.Following)
							{
								emission2.enabled = false;
								this.Yandere.Followers--;
								this.Following = false;
							}
						}
					}
				}
				this.PreviousAlarm = this.Alarm;
				if (this.DistanceToPlayer < this.VisionDistance - this.ChameleonBonus)
				{
					if (!this.Talking && !this.Spraying && !this.SentHome && !this.Slave)
					{
						if (!this.Yandere.Noticed)
						{
							bool flag3 = false;
							if (this.Guarding || this.Fleeing || this.InvestigatingBloodPool)
							{
								flag3 = true;
							}
							if ((this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious) || (!this.Teacher && this.StudentID > 1 && !this.Teacher && this.Yandere.PickUp != null && this.Yandere.PickUp.Suspicious) || (this.Teacher && this.Yandere.PickUp != null && this.Yandere.PickUp.Suspicious && !this.Yandere.PickUp.CleaningProduct) || (this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint) || (this.Yandere.Sanity < 33.333f || this.Yandere.Pickpocketing || this.Yandere.Attacking || this.Yandere.Struggling || this.Yandere.Dragging || (!this.IgnoringPettyActions && this.Yandere.Lewd)) || (this.Yandere.Carrying || this.Yandere.Medusa || this.Yandere.Poisoning || this.Yandere.Pickpocketing || this.Yandere.WeaponTimer > 0f || this.Yandere.MurderousActionTimer > 0f || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null)) || (!this.IgnoringPettyActions && this.Yandere.Laughing && this.Yandere.LaughIntensity > 15f) || (!this.IgnoringPettyActions && this.Yandere.Stance.Current == StanceType.Crouching) || (!this.IgnoringPettyActions && this.Yandere.Stance.Current == StanceType.Crawling) || (this.Private && this.Yandere.Trespassing) || (this.Private && this.Yandere.Eavesdropping) || (this.Teacher && !this.WitnessedCorpse && this.Yandere.Trespassing) || (this.Teacher && !this.IgnoringPettyActions && this.Yandere.Rummaging) || (!this.IgnoringPettyActions && this.Yandere.TheftTimer > 0f) || (this.StudentID == 1 && this.Yandere.NearSenpai && !this.Yandere.Talking) || (this.Yandere.Eavesdropping && this.Private) || (!this.StudentManager.CombatMinigame.Practice && this.Yandere.DelinquentFighting && this.StudentManager.CombatMinigame.Path < 4) || (flag3 && this.Yandere.PickUp != null && this.Yandere.PickUp.Mop != null && this.Yandere.PickUp.Mop.Bloodiness > 0f) || (flag3 && this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null) || (this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence))
							{
								bool flag4 = false;
								if (this.Yandere.transform.position.y < base.transform.position.y + 4f)
								{
									flag4 = true;
								}
								if (flag4 && this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
								{
									this.YandereVisible = true;
									if (this.Yandere.Attacking || this.Yandere.Struggling || (this.WitnessedCorpse && this.Yandere.NearBodies > 0 && this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint) || (this.WitnessedCorpse && this.Yandere.NearBodies > 0 && this.Yandere.Armed) || (this.WitnessedCorpse && this.Yandere.NearBodies > 0 && this.Yandere.Sanity < 66.66666f) || (this.Yandere.Carrying || this.Yandere.Dragging || this.Yandere.MurderousActionTimer > 0f || (this.Guarding && this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint)) || (this.Guarding && this.Yandere.Armed) || (this.Guarding && this.Yandere.Sanity < 66.66666f) || (!this.StudentManager.CombatMinigame.Practice && this.Club == ClubType.Council && this.Yandere.DelinquentFighting && this.StudentManager.CombatMinigame.Path < 4 && !this.StudentManager.CombatMinigame.Practice) || (!this.StudentManager.CombatMinigame.Practice && this.Teacher && this.Yandere.DelinquentFighting && this.StudentManager.CombatMinigame.Path < 4 && !this.StudentManager.CombatMinigame.Practice) || (flag3 && this.Yandere.PickUp != null && this.Yandere.PickUp.Mop != null && this.Yandere.PickUp.Mop.Bloodiness > 0f) || (flag3 && this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null) || (this.Yandere.PickUp != null && this.Teacher && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence) || (this.StudentManager.Atmosphere < 0.33333f && this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && this.Yandere.Armed))
									{
										Debug.Log(this.Name + " is aware that Yandere-chan is misbehaving.");
										if (this.Yandere.Hungry || !this.Yandere.Egg)
										{
											Debug.Log(this.Name + " has just witnessed a murder!");
											if (this.Yandere.PickUp != null)
											{
												if (flag3)
												{
													if (this.Yandere.PickUp.Mop != null)
													{
														if (this.Yandere.PickUp.Mop.Bloodiness > 0f)
														{
															Debug.Log("This character witnessed Yandere-chan trying to cover up a crime.");
															this.WitnessedCoverUp = true;
														}
													}
													else if (this.Yandere.PickUp.BodyPart != null)
													{
														Debug.Log("This character witnessed Yandere-chan trying to cover up a crime.");
														this.WitnessedCoverUp = true;
													}
												}
												if (this.Teacher && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence)
												{
													Debug.Log("This character witnessed Yandere-chan trying to cover up a crime.");
													this.WitnessedCoverUp = true;
												}
											}
											if (this.Persona == PersonaType.PhoneAddict)
											{
												Debug.Log(this.Name + ", a Phone Addict, is deciding what to do.");
												this.Countdown.gameObject.SetActive(false);
												this.Countdown.Sprite.fillAmount = 1f;
												this.WitnessedMurder = false;
												this.Fleeing = false;
												if (this.CrimeReported)
												{
													Debug.Log(this.Name + "'s ''CrimeReported'' was ''true'', but we're seeing it to ''false''.");
													this.CrimeReported = false;
												}
											}
											if (!this.Yandere.DelinquentFighting)
											{
												this.NoBreakUp = true;
											}
											this.WitnessMurder();
										}
									}
									else if (!this.Fleeing && (!this.Alarmed || this.CanStillNotice))
									{
										if (this.Yandere.Rummaging || this.Yandere.TheftTimer > 0f)
										{
											this.Alarm = 200f;
										}
										if (this.Yandere.WeaponTimer > 0f)
										{
											this.Alarm = 200f;
										}
										if (this.IgnoreTimer == 0f || this.CanStillNotice)
										{
											if (this.Teacher)
											{
												this.StudentManager.TutorialWindow.ShowTeacherMessage = true;
											}
											this.Alarm += Time.deltaTime * (100f / this.DistanceToPlayer) * this.Paranoia * this.Perception;
											if (this.StudentID == 1 && this.Yandere.TimeSkipping)
											{
												this.Clock.EndTimeSkip();
											}
										}
									}
								}
								else
								{
									this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
								}
							}
							else
							{
								this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
							}
						}
						else
						{
							this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
						}
					}
					else
					{
						this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
					}
				}
				else
				{
					this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
				}
				if (this.PreviousAlarm > this.Alarm && this.Alarm < 100f)
				{
					this.YandereVisible = false;
				}
				if (this.Teacher && !this.Yandere.Medusa && this.Yandere.Egg)
				{
					this.Alarm = 0f;
				}
				if (this.Alarm > 100f)
				{
					this.BecomeAlarmed();
				}
			}
			else
			{
				this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
			}
		}
		else if (this.Distraction != null)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.Distraction.position.x, base.transform.position.y, this.Distraction.position.z) - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
			if (this.Distractor != null)
			{
				if (this.Distractor.Club == ClubType.Cooking && this.Distractor.ClubActivityPhase > 0 && this.Distractor.Actions[this.Distractor.Phase] == StudentActionType.ClubAction)
				{
					this.CharacterAnimation.CrossFade(this.PlateEatAnim);
					if ((double)this.CharacterAnimation[this.PlateEatAnim].time > 6.83333)
					{
						this.Fingerfood[this.Distractor.StudentID - 20].SetActive(false);
					}
					else if ((double)this.CharacterAnimation[this.PlateEatAnim].time > 2.66666)
					{
						this.Fingerfood[this.Distractor.StudentID - 20].SetActive(true);
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.RandomAnim);
					if (this.CharacterAnimation[this.RandomAnim].time >= this.CharacterAnimation[this.RandomAnim].length)
					{
						this.PickRandomAnim();
					}
				}
			}
		}
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x00174894 File Offset: 0x00172C94
	private void BecomeAlarmed()
	{
		if (this.Yandere.Medusa && this.YandereVisible)
		{
			this.TurnToStone();
			return;
		}
		if (!this.Alarmed || this.DiscCheck)
		{
			Debug.Log(this.Name + " has become alarmed.");
			if (this.Persona == PersonaType.PhoneAddict)
			{
				this.SmartPhone.SetActive(true);
			}
			this.Yandere.Alerts++;
			if (this.ReturningMisplacedWeapon)
			{
				this.DropMisplacedWeapon();
				this.ReturnToRoutineAfter = true;
			}
			if (this.StudentID > 1)
			{
				this.ID = 0;
				while (this.ID < this.Outlines.Length)
				{
					if (this.Outlines[this.ID] != null)
					{
						this.Outlines[this.ID].color = new Color(1f, 1f, 0f, 1f);
					}
					this.ID++;
				}
			}
			if (this.DistractionTarget != null)
			{
				this.DistractionTarget.TargetedForDistraction = false;
			}
			this.CharacterAnimation.CrossFade(this.IdleAnim);
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.CameraReacting = false;
			this.CanStillNotice = false;
			this.Distracting = false;
			this.Distracted = false;
			this.DiscCheck = false;
			this.Reacted = false;
			this.Routine = false;
			this.Alarmed = true;
			this.ReadPhase = 0;
			if (!this.Male)
			{
				this.HorudaCollider.gameObject.SetActive(false);
			}
			if (this.YandereVisible && this.Yandere.Mask == null)
			{
				this.Witness = true;
			}
			this.EmptyHands();
			if (this.Club == ClubType.Cooking && this.Actions[this.Phase] == StudentActionType.ClubAction && this.ClubActivityPhase == 1 && !this.WitnessedCorpse)
			{
				this.ResumeDistracting = true;
				this.MyPlate.gameObject.SetActive(true);
			}
			this.SpeechLines.Stop();
			this.StopPairing();
			if (this.Witnessed != StudentWitnessType.Weapon && this.Witnessed != StudentWitnessType.Eavesdropping)
			{
				this.PreviouslyWitnessed = this.Witnessed;
			}
			if (this.DistanceToDestination < 5f && (this.Actions[this.Phase] == StudentActionType.Graffiti || this.Actions[this.Phase] == StudentActionType.Bully))
			{
				this.StudentManager.NoBully[this.BullyID] = true;
				this.KilledMood = true;
			}
			if (this.WitnessedCorpse && !this.WitnessedMurder)
			{
				this.Witnessed = StudentWitnessType.Corpse;
				this.EyeShrink = 0.9f;
			}
			else if (this.WitnessedLimb)
			{
				this.Witnessed = StudentWitnessType.SeveredLimb;
			}
			else if (this.WitnessedBloodyWeapon)
			{
				this.Witnessed = StudentWitnessType.BloodyWeapon;
			}
			else if (this.WitnessedBloodPool)
			{
				this.Witnessed = StudentWitnessType.BloodPool;
			}
			else if (this.WitnessedWeapon)
			{
				this.Witnessed = StudentWitnessType.DroppedWeapon;
			}
			if (this.SawCorpseThisFrame)
			{
				this.YandereVisible = false;
			}
			if (this.YandereVisible && !this.NotAlarmedByYandereChan)
			{
				if ((!this.Injured && this.Persona == PersonaType.Violent && this.Yandere.Armed && !this.WitnessedCorpse && !this.RespectEarned) || (this.Persona == PersonaType.Violent && this.Yandere.DelinquentFighting))
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentWeaponReaction, 0, 3f);
					this.ThreatDistance = this.DistanceToPlayer;
					this.CheerTimer = UnityEngine.Random.Range(1f, 2f);
					this.SmartPhone.SetActive(false);
					this.Threatened = true;
					this.ThreatPhase = 1;
					this.ForgetGiggle();
				}
				Debug.Log(this.Name + " saw Yandere-chan doing something bad.");
				if (this.Yandere.Attacking || this.Yandere.Struggling || this.Yandere.Carrying || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart))
				{
					if (!this.Yandere.Egg)
					{
						this.WitnessMurder();
					}
				}
				else if (this.Witnessed != StudentWitnessType.Corpse)
				{
					this.DetermineWhatWasWitnessed();
				}
				if (this.Teacher && this.WitnessedCorpse)
				{
					this.Concern = 1;
				}
				if (this.StudentID == 1 && this.Yandere.Mask == null && !this.Yandere.Egg)
				{
					if (this.Concern == 5)
					{
						Debug.Log("Senpai noticed stalking or lewdness.");
						this.SenpaiNoticed();
						if (this.Witnessed == StudentWitnessType.Stalking || this.Witnessed == StudentWitnessType.Lewd)
						{
							this.CharacterAnimation.CrossFade(this.IdleAnim);
							this.CharacterAnimation[this.AngryFaceAnim].weight = 1f;
						}
						else
						{
							Debug.Log("Senpai entered his scared animation.");
							this.CharacterAnimation.CrossFade(this.ScaredAnim);
							this.CharacterAnimation["scaredFace_00"].weight = 1f;
						}
						this.CameraEffects.MurderWitnessed();
					}
					else
					{
						this.CharacterAnimation.CrossFade("suspicious_00");
						this.CameraEffects.Alarm();
					}
				}
				else if (!this.Teacher)
				{
					this.CameraEffects.Alarm();
				}
				else
				{
					Debug.Log("A teacher has just witnessed Yandere-chan doing something bad.");
					if (!this.Fleeing)
					{
						if (this.Concern < 5)
						{
							this.CameraEffects.Alarm();
						}
						else if (!this.Yandere.Struggling && !this.StudentManager.PinningDown)
						{
							this.SenpaiNoticed();
							this.CameraEffects.MurderWitnessed();
						}
					}
					else
					{
						this.PersonaReaction();
						this.AlarmTimer = 0f;
						if (this.Concern < 5)
						{
							this.CameraEffects.Alarm();
						}
						else
						{
							this.CameraEffects.MurderWitnessed();
						}
					}
				}
				if (!this.Teacher && this.Club != ClubType.Delinquent && this.Witnessed == this.PreviouslyWitnessed)
				{
					this.RepeatReaction = true;
				}
				if (this.Yandere.Mask == null)
				{
					this.RepDeduction = 0f;
					this.CalculateReputationPenalty();
					if (this.RepDeduction >= 0f)
					{
						this.RepLoss -= this.RepDeduction;
					}
					this.Reputation.PendingRep -= this.RepLoss * this.Paranoia;
					this.PendingRep -= this.RepLoss * this.Paranoia;
				}
				if (this.ToiletEvent != null && this.ToiletEvent.EventDay == DayOfWeek.Monday)
				{
					this.ToiletEvent.EndEvent();
				}
			}
			else if (!this.WitnessedCorpse)
			{
				if (this.Yandere.Caught)
				{
					if (this.Yandere.Mask == null)
					{
						if (this.Yandere.Pickpocketing)
						{
							this.Witnessed = StudentWitnessType.Pickpocketing;
							this.RepLoss += 10f;
						}
						else
						{
							this.Witnessed = StudentWitnessType.Theft;
						}
						this.RepDeduction = 0f;
						this.CalculateReputationPenalty();
						if (this.RepDeduction >= 0f)
						{
							this.RepLoss -= this.RepDeduction;
						}
						this.Reputation.PendingRep -= this.RepLoss * this.Paranoia;
						this.PendingRep -= this.RepLoss * this.Paranoia;
					}
				}
				else if (this.WitnessedLimb)
				{
					this.Witnessed = StudentWitnessType.SeveredLimb;
				}
				else if (this.WitnessedBloodyWeapon)
				{
					this.Witnessed = StudentWitnessType.BloodyWeapon;
				}
				else if (this.WitnessedBloodPool)
				{
					this.Witnessed = StudentWitnessType.BloodPool;
				}
				else if (this.WitnessedWeapon)
				{
					this.Witnessed = StudentWitnessType.DroppedWeapon;
				}
				else
				{
					Debug.Log(this.Name + " was alarmed by something, but didn't see what it was.");
					this.Witnessed = StudentWitnessType.None;
					this.DiscCheck = true;
					this.Witness = false;
				}
			}
			else
			{
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
			}
		}
		this.NotAlarmedByYandereChan = false;
		this.SawCorpseThisFrame = false;
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x001751A8 File Offset: 0x001735A8
	private void UpdateDetectionMarker()
	{
		if (this.Alarm < 0f)
		{
			this.Alarm = 0f;
			if (this.Club == ClubType.Council && !this.Yandere.Noticed)
			{
				this.CanStillNotice = true;
			}
		}
		if (this.DetectionMarker != null)
		{
			if (this.Alarm > 0f)
			{
				if (!this.DetectionMarker.Tex.enabled)
				{
					this.DetectionMarker.Tex.enabled = true;
				}
				this.DetectionMarker.Tex.transform.localScale = new Vector3(this.DetectionMarker.Tex.transform.localScale.x, (this.Alarm > 100f) ? 1f : (this.Alarm / 100f), this.DetectionMarker.Tex.transform.localScale.z);
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, this.Alarm / 100f);
			}
			else if (this.DetectionMarker.Tex.color.a != 0f)
			{
				this.DetectionMarker.Tex.enabled = false;
				this.DetectionMarker.Tex.color = new Color(this.DetectionMarker.Tex.color.r, this.DetectionMarker.Tex.color.g, this.DetectionMarker.Tex.color.b, 0f);
			}
		}
		else
		{
			this.SpawnDetectionMarker();
		}
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x001753C8 File Offset: 0x001737C8
	private void UpdateTalkInput()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			if (!GameGlobals.EmptyDemon && (this.Alarm > 0f || this.AlarmTimer > 0f || this.Yandere.Armed || this.Yandere.Shoved || this.Waiting || this.InEvent || this.SentHome || this.Threatened || (this.Distracted && !this.Drownable) || this.StudentID == 1) && !this.Slave && !this.BadTime && !this.Yandere.Gazing && !this.FightingSlave)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				bool flag = false;
				if (this.StudentID < 86 && this.Armband.activeInHierarchy && (this.Actions[this.Phase] == StudentActionType.ClubAction || this.Actions[this.Phase] == StudentActionType.SitAndSocialize || this.Actions[this.Phase] == StudentActionType.Socializing || this.Actions[this.Phase] == StudentActionType.Sleuth || this.Actions[this.Phase] == StudentActionType.Lyrics || this.Actions[this.Phase] == StudentActionType.Patrol || this.Actions[this.Phase] == StudentActionType.SitAndEatBento) && (this.DistanceToDestination < 1f || (base.transform.position.y > this.StudentManager.ClubZones[(int)this.Club].position.y - 1f && base.transform.position.y < this.StudentManager.ClubZones[(int)this.Club].position.y + 1f && Vector3.Distance(base.transform.position, this.StudentManager.ClubZones[(int)this.Club].position) < this.ClubThreshold) || Vector3.Distance(base.transform.position, this.StudentManager.DramaSpots[1].position) < 12f))
				{
					flag = true;
					this.Warned = false;
				}
				if (this.StudentID == 76 && GameGlobals.BlondeHair && this.Reputation.Reputation < -33.33333f && this.Yandere.Persona == YanderePersonaType.Tough && PlayerGlobals.GetStudentFriend(76) && PlayerGlobals.GetStudentFriend(77) && PlayerGlobals.GetStudentFriend(78) && PlayerGlobals.GetStudentFriend(79) && PlayerGlobals.GetStudentFriend(80))
				{
					flag = true;
					this.Warned = false;
				}
				bool flag2 = false;
				if (this.Yandere.PickUp != null && this.Yandere.PickUp.Salty && !this.Indoors)
				{
					flag2 = true;
				}
				if (this.StudentManager.Pose)
				{
					this.MyController.enabled = false;
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Stop = true;
					this.Pose();
				}
				else if (this.BadTime)
				{
					this.Yandere.EmptyHands();
					this.BecomeRagdoll();
					this.Yandere.RagdollPK.connectedBody = this.Ragdoll.AllRigidbodies[5];
					this.Yandere.RagdollPK.connectedAnchor = this.Ragdoll.LimbAnchor[4];
					this.DialogueWheel.PromptBar.ClearButtons();
					this.DialogueWheel.PromptBar.Label[1].text = "Back";
					this.DialogueWheel.PromptBar.UpdateButtons();
					this.DialogueWheel.PromptBar.Show = true;
					this.Yandere.Ragdoll = this.Ragdoll.gameObject;
					this.Yandere.SansEyes[0].SetActive(true);
					this.Yandere.SansEyes[1].SetActive(true);
					this.Yandere.GlowEffect.Play();
					this.Yandere.CanMove = false;
					this.Yandere.PK = true;
					this.DeathType = DeathType.EasterEgg;
				}
				else if (this.StudentManager.Six)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
					gameObject.GetComponent<AlarmDiscScript>().Originator = this;
					AudioSource.PlayClipAtPoint(this.Yandere.SixTakedown, base.transform.position);
					AudioSource.PlayClipAtPoint(this.Yandere.Snarls[UnityEngine.Random.Range(0, this.Yandere.Snarls.Length)], base.transform.position);
					this.Yandere.CharacterAnimation.CrossFade("f02_sixEat_00");
					this.Yandere.TargetStudent = this;
					this.Yandere.FollowHips = true;
					this.Yandere.Attacking = true;
					this.Yandere.CanMove = false;
					this.Yandere.Eating = true;
					this.CharacterAnimation.CrossFade(this.EatVictimAnim);
					this.Pathfinding.enabled = false;
					this.Routine = false;
					this.Dying = true;
					this.Eaten = true;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
					this.Yandere.SixTarget = gameObject2.transform;
					this.Yandere.SixTarget.LookAt(this.Yandere.transform.position);
					this.Yandere.SixTarget.Translate(this.Yandere.SixTarget.forward);
				}
				else if (this.Yandere.SpiderGrow)
				{
					if (!this.Eaten && !this.Cosmetic.Empty)
					{
						AudioSource.PlayClipAtPoint(this.Yandere.SixTakedown, base.transform.position);
						AudioSource.PlayClipAtPoint(this.Yandere.Snarls[UnityEngine.Random.Range(0, this.Yandere.Snarls.Length)], base.transform.position);
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.Yandere.EmptyHusk, base.transform.position + base.transform.forward * 0.5f, Quaternion.identity);
						gameObject3.GetComponent<EmptyHuskScript>().TargetStudent = this;
						gameObject3.transform.LookAt(base.transform.position);
						this.CharacterAnimation.CrossFade(this.EatVictimAnim);
						this.Pathfinding.enabled = false;
						this.Distracted = false;
						this.Routine = false;
						this.Dying = true;
						this.Eaten = true;
						if (this.Investigating)
						{
							this.StopInvestigating();
						}
						if (this.Following)
						{
							this.Yandere.Followers--;
							this.Following = false;
						}
						GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, base.transform.position, Quaternion.identity);
					}
				}
				else if (this.StudentManager.Gaze)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_gazerPoint_00");
					this.Yandere.GazerEyes.Attacking = true;
					this.Yandere.TargetStudent = this;
					this.Yandere.GazeAttacking = true;
					this.Yandere.CanMove = false;
					this.Routine = false;
				}
				else if (this.Slave)
				{
					this.Yandere.TargetStudent = this;
					this.Yandere.PauseScreen.StudentInfoMenu.Targeting = true;
					this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
					this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
					this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
					this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
					base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
					this.Yandere.PauseScreen.MainMenu.SetActive(false);
					this.Yandere.PauseScreen.Panel.enabled = true;
					this.Yandere.PauseScreen.Sideways = true;
					this.Yandere.PauseScreen.Show = true;
					Time.timeScale = 0.0001f;
					this.Yandere.PromptBar.ClearButtons();
					this.Yandere.PromptBar.Label[1].text = "Cancel";
					this.Yandere.PromptBar.UpdateButtons();
					this.Yandere.PromptBar.Show = true;
				}
				else if (this.FightingSlave)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_subtleStab_00");
					this.Yandere.SubtleStabbing = true;
					this.Yandere.TargetStudent = this;
					this.Yandere.CanMove = false;
				}
				else if (this.Following)
				{
					this.Subtitle.UpdateLabel(SubtitleType.StudentFarewell, 0, 3f);
					this.Prompt.Label[0].text = "     Talk";
					this.Prompt.Circle[0].fillAmount = 1f;
					this.Hearts.emission.enabled = false;
					this.Yandere.Followers--;
					this.Following = false;
					this.Routine = true;
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 1f;
				}
				else if (this.Pushable)
				{
					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					if (!this.Male)
					{
						this.Subtitle.UpdateLabel(SubtitleType.NoteReaction, 5, 3f);
					}
					else
					{
						this.Subtitle.UpdateLabel(SubtitleType.NoteReactionMale, 5, 3f);
					}
					this.Prompt.Label[0].text = "     Talk";
					this.Prompt.Circle[0].fillAmount = 1f;
					this.Yandere.TargetStudent = this;
					this.Yandere.Attacking = true;
					this.Yandere.RoofPush = true;
					this.Yandere.CanMove = false;
					this.Yandere.EmptyHands();
					this.EmptyHands();
					this.Distracted = true;
					this.Routine = false;
					this.Pushed = true;
					this.CharacterAnimation.CrossFade(this.PushedAnim);
				}
				else if (this.Drownable)
				{
					Debug.Log("Just began to drown someone.");
					if (this.VomitDoor != null)
					{
						this.VomitDoor.Prompt.enabled = true;
						this.VomitDoor.enabled = true;
					}
					this.Yandere.EmptyHands();
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Prompt.Circle[0].fillAmount = 1f;
					this.VomitEmitter.gameObject.SetActive(false);
					this.Police.DrownedStudentName = this.Name;
					this.MyController.enabled = false;
					this.VomitEmitter.gameObject.SetActive(false);
					this.SmartPhone.SetActive(false);
					this.Police.DrownVictims++;
					this.Distracted = true;
					this.Routine = false;
					this.Drowned = true;
					this.Subtitle.UpdateLabel(SubtitleType.DrownReaction, 0, 3f);
					this.Yandere.TargetStudent = this;
					this.Yandere.Attacking = true;
					this.Yandere.CanMove = false;
					this.Yandere.Drown = true;
					this.Yandere.DrownAnim = "f02_fountainDrownA_00";
					if (this.Male)
					{
						if (Vector3.Distance(base.transform.position, this.StudentManager.transform.position) < 5f)
						{
							this.DrownAnim = "fountainDrown_00_B";
						}
						else
						{
							this.DrownAnim = "toiletDrown_00_B";
						}
					}
					else if (Vector3.Distance(base.transform.position, this.StudentManager.transform.position) < 5f)
					{
						this.DrownAnim = "f02_fountainDrownB_00";
					}
					else
					{
						this.DrownAnim = "f02_toiletDrownB_00";
					}
					this.CharacterAnimation.CrossFade(this.DrownAnim);
				}
				else if (this.Clock.Period == 2 || this.Clock.Period == 4 || this.CurrentDestination == this.Seat)
				{
					this.Subtitle.UpdateLabel(SubtitleType.ClassApology, 0, 3f);
					this.Prompt.Circle[0].fillAmount = 1f;
				}
				else if (this.InEvent || !this.CanTalk || this.GoAway || this.Fleeing || (this.Meeting && !this.Drownable) || (this.Wet || this.TurnOffRadio || this.InvestigatingBloodPool || (this.MyPlate != null && this.MyPlate.parent == this.RightHand)) || flag2 || this.ReturningMisplacedWeapon || this.FollowTarget != null || this.Actions[this.Phase] == StudentActionType.Bully || this.Actions[this.Phase] == StudentActionType.Graffiti)
				{
					this.Subtitle.UpdateLabel(SubtitleType.EventApology, 1, 3f);
					this.Prompt.Circle[0].fillAmount = 1f;
				}
				else if (this.Clock.Period == 3 && this.BusyAtLunch)
				{
					this.Subtitle.UpdateLabel(SubtitleType.SadApology, 1, 3f);
					this.Prompt.Circle[0].fillAmount = 1f;
				}
				else if (this.Warned)
				{
					Debug.Log("This character refuses to speak to Yandere-chan because of a grudge.");
					this.Subtitle.UpdateLabel(SubtitleType.GrudgeRefusal, 0, 3f);
					this.Prompt.Circle[0].fillAmount = 1f;
				}
				else if (this.Ignoring)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PhotoAnnoyance, 0, 3f);
					this.Prompt.Circle[0].fillAmount = 1f;
				}
				else
				{
					bool flag3 = false;
					if (this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint)
					{
						flag3 = true;
					}
					if (!this.Witness && flag3)
					{
						this.Prompt.Circle[0].fillAmount = 1f;
						this.YandereVisible = true;
						this.Alarm = 200f;
					}
					else
					{
						this.SpeechLines.Stop();
						this.Yandere.TargetStudent = this;
						if (!this.Grudge)
						{
							this.ClubManager.CheckGrudge(this.Club);
							if (ClubGlobals.GetClubKicked(this.Club) && flag)
							{
								Debug.Log("Here, specifically.");
								this.Interaction = StudentInteractionType.ClubGrudge;
								this.TalkTimer = 5f;
								this.Warned = true;
							}
							else if (ClubGlobals.Club == this.Club && flag && this.ClubManager.ClubGrudge)
							{
								this.Interaction = StudentInteractionType.ClubKick;
								ClubGlobals.SetClubKicked(this.Club, true);
								this.TalkTimer = 5f;
								this.Warned = true;
							}
							else if (this.Prompt.Label[0].text == "     Feed")
							{
								this.Interaction = StudentInteractionType.Feeding;
								this.TalkTimer = 10f;
							}
							else if (this.Prompt.Label[0].text == "     Give Snack")
							{
								this.Yandere.Interaction = YandereInteractionType.GivingSnack;
								this.Yandere.TalkTimer = 3f;
								this.Interaction = StudentInteractionType.Idle;
							}
							else if (this.Prompt.Label[0].text == "     Ask For Help")
							{
								this.Yandere.Interaction = YandereInteractionType.AskingForHelp;
								this.Yandere.TalkTimer = 5f;
								this.Interaction = StudentInteractionType.Idle;
							}
							else
							{
								this.DistanceToDestination = Vector3.Distance(base.transform.position, this.Destinations[this.Phase].position);
								if (this.Sleuthing)
								{
									this.DistanceToDestination = Vector3.Distance(base.transform.position, this.SleuthTarget.position);
								}
								if (flag)
								{
									int num;
									if (this.Sleuthing)
									{
										num = 5;
									}
									else
									{
										num = 0;
									}
									if (GameGlobals.EmptyDemon)
									{
										num = (int)(this.Club * (ClubType)(-1));
									}
									this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)(this.Club + num), 4f);
									this.DialogueWheel.ClubLeader = true;
								}
								else
								{
									this.Subtitle.UpdateLabel(SubtitleType.Greeting, 0, 3f);
								}
								if (this.Club != ClubType.Council && this.Club != ClubType.Delinquent && ((this.Male && PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 0) || PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 4))
								{
									ParticleSystem.EmissionModule emission = this.Hearts.emission;
									emission.rateOverTime = (float)(PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus);
									emission.enabled = true;
									this.Hearts.Play();
								}
								this.StudentManager.DisablePrompts();
								this.StudentManager.VolumeDown();
								this.DialogueWheel.HideShadows();
								this.DialogueWheel.Show = true;
								this.DialogueWheel.Panel.enabled = true;
								this.TalkTimer = 0f;
								if (!ConversationGlobals.GetTopicDiscovered(20))
								{
									this.Yandere.NotificationManager.TopicName = "Socializing";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
									ConversationGlobals.SetTopicDiscovered(20, true);
								}
								if (!ConversationGlobals.GetTopicLearnedByStudent(20, this.StudentID))
								{
									this.Yandere.NotificationManager.TopicName = "Socializing";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
									ConversationGlobals.SetTopicLearnedByStudent(20, this.StudentID, true);
								}
								if (!ConversationGlobals.GetTopicDiscovered(21))
								{
									this.Yandere.NotificationManager.TopicName = "Solitude";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
									ConversationGlobals.SetTopicDiscovered(21, true);
								}
								if (!ConversationGlobals.GetTopicLearnedByStudent(21, this.StudentID))
								{
									this.Yandere.NotificationManager.TopicName = "Solitude";
									this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
									ConversationGlobals.SetTopicLearnedByStudent(21, this.StudentID, true);
								}
							}
						}
						else if (flag)
						{
							this.Interaction = StudentInteractionType.ClubUnwelcome;
							this.TalkTimer = 5f;
							this.Warned = true;
						}
						else
						{
							this.Interaction = StudentInteractionType.PersonalGrudge;
							this.TalkTimer = 5f;
							this.Warned = true;
						}
						this.Yandere.ShoulderCamera.OverShoulder = true;
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.Obstacle.enabled = true;
						this.Giggle = null;
						this.Yandere.WeaponMenu.KeyboardShow = false;
						this.Yandere.Obscurance.enabled = false;
						this.Yandere.WeaponMenu.Show = false;
						this.Yandere.YandereVision = false;
						this.Yandere.CanMove = false;
						this.Yandere.Talking = true;
						this.Investigating = false;
						this.Talk.enabled = true;
						this.Reacted = false;
						this.Routine = false;
						this.Talking = true;
						this.ReadPhase = 0;
						this.EmptyHands();
						bool flag4 = false;
						if (this.CurrentAction == StudentActionType.Sunbathe && this.SunbathePhase > 2)
						{
							this.SunbathePhase = 2;
							flag4 = true;
						}
						if (this.Sleuthing)
						{
							if (!this.Scrubber.activeInHierarchy)
							{
								this.SmartPhone.SetActive(true);
							}
							else
							{
								this.SmartPhone.SetActive(false);
							}
						}
						else if (this.Persona != PersonaType.PhoneAddict)
						{
							this.SmartPhone.SetActive(false);
						}
						else if (!this.Scrubber.activeInHierarchy && !flag4)
						{
							this.SmartPhone.SetActive(true);
						}
						this.ChalkDust.Stop();
						this.StopPairing();
					}
				}
			}
		}
		if (this.Prompt.Circle[2].fillAmount == 0f || (this.Yandere.Sanity < 33.33333f && this.Yandere.CanMove && !this.Prompt.HideButton[2] && this.Prompt.InSight && this.Club != ClubType.Council && !this.Struggling && !this.Chasing))
		{
			Debug.Log(this.Name + " was attacked because the player pressed the X button, or because Yandere-chan had low sanity.");
			float f = Vector3.Angle(-base.transform.forward, this.Yandere.transform.position - base.transform.position);
			this.Yandere.AttackManager.Stealth = (Mathf.Abs(f) <= 45f);
			bool flag5 = false;
			if (this.Yandere.AttackManager.Stealth && (this.Yandere.EquippedWeapon.Type == WeaponType.Bat || this.Yandere.EquippedWeapon.Type == WeaponType.Weight))
			{
				flag5 = true;
			}
			if (flag5 || this.StudentManager.OriginalUniforms + this.StudentManager.NewUniforms > 1)
			{
				if (this.ClubActivityPhase < 16)
				{
					bool flag6 = false;
					if (this.Club == ClubType.Delinquent && !this.Injured && !this.Yandere.AttackManager.Stealth && !this.RespectEarned)
					{
						Debug.Log(this.Name + " knows that Yandere-chan is tyring to attack him.");
						flag6 = true;
						this.Fleeing = false;
						this.Patience = 1;
						this.Shove();
						this.SpawnAlarmDisc();
					}
					if (this.Yandere.AttackManager.Stealth)
					{
						this.SpawnSmallAlarmDisc();
					}
					if (!flag6 && !this.Yandere.NearSenpai && !this.Yandere.Attacking && this.Yandere.Stance.Current != StanceType.Crouching)
					{
						if (this.Yandere.EquippedWeapon.Flaming || this.Yandere.CyborgParts[1].activeInHierarchy)
						{
							this.Yandere.SanityBased = false;
						}
						if (this.Strength == 9)
						{
							if (!this.Yandere.AttackManager.Stealth)
							{
								this.CharacterAnimation.CrossFade("f02_dramaticFrontal_00");
							}
							else
							{
								this.CharacterAnimation.CrossFade("f02_dramaticStealth_00");
							}
							this.Yandere.CharacterAnimation.CrossFade("f02_readyToFight_00");
							this.Yandere.CanMove = false;
							this.DramaticCamera.enabled = true;
							this.DramaticCamera.rect = new Rect(0f, 0.5f, 1f, 0f);
							this.DramaticCamera.gameObject.SetActive(true);
							this.DramaticCamera.gameObject.GetComponent<AudioSource>().Play();
							this.DramaticReaction = true;
							this.Pathfinding.canSearch = false;
							this.Pathfinding.canMove = false;
							this.Routine = false;
						}
						else
						{
							this.AttackReaction();
						}
					}
				}
			}
			else if (!this.Yandere.ClothingWarning)
			{
				this.Yandere.NotificationManager.DisplayNotification(NotificationType.Clothing);
				this.StudentManager.TutorialWindow.ShowClothingMessage = true;
				this.Yandere.ClothingWarning = true;
			}
		}
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x00176D90 File Offset: 0x00175190
	private void UpdateDying()
	{
		this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
		this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
		if (this.Attacked)
		{
			if (!this.Teacher)
			{
				if (this.Strength == 9)
				{
					if (!this.StudentManager.Stop)
					{
						this.StudentManager.StopMoving();
						this.Yandere.RPGCamera.enabled = false;
						this.SmartPhone.SetActive(false);
						this.Police.Show = false;
					}
					Debug.Log("The mysterious obstacle is counter-attacking!");
					this.CharacterAnimation.CrossFade("f02_moCounterB_00");
					if (!this.WitnessedMurder && this.CharacterAnimation["f02_moLipSync_00"].weight == 0f)
					{
						this.CharacterAnimation["f02_moLipSync_00"].weight = 1f;
						this.CharacterAnimation["f02_moLipSync_00"].time = 0f;
						this.CharacterAnimation.Play("f02_moLipSync_00");
					}
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
					this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
				}
				else
				{
					this.EyeShrink = Mathf.Lerp(this.EyeShrink, 1f, Time.deltaTime * 10f);
					if (this.Alive && !this.Tranquil)
					{
						if (!this.Yandere.SanityBased)
						{
							this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
							if (this.Yandere.EquippedWeapon.WeaponID == 11)
							{
								this.CharacterAnimation.CrossFade(this.CyborgDeathAnim);
								this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
								if (this.CharacterAnimation[this.CyborgDeathAnim].time >= this.CharacterAnimation[this.CyborgDeathAnim].length - 0.25f && this.Yandere.EquippedWeapon.WeaponID == 11)
								{
									UnityEngine.Object.Instantiate<GameObject>(this.BloodyScream, base.transform.position + Vector3.up, Quaternion.identity);
									this.DeathType = DeathType.EasterEgg;
									this.BecomeRagdoll();
									this.Ragdoll.Dismember();
								}
							}
							else if (this.Yandere.EquippedWeapon.WeaponID == 7)
							{
								this.CharacterAnimation.CrossFade(this.BuzzSawDeathAnim);
								this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
							}
							else if (!this.Yandere.EquippedWeapon.Concealable)
							{
								this.CharacterAnimation.CrossFade(this.SwingDeathAnim);
								this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
							}
							else
							{
								this.CharacterAnimation.CrossFade(this.DefendAnim);
								this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * 0.1f);
							}
						}
						else
						{
							this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward * this.Yandere.AttackManager.Distance);
							if (!this.Yandere.AttackManager.Stealth)
							{
								this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
							}
							else
							{
								this.targetRotation = Quaternion.LookRotation(base.transform.position - new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z));
							}
							base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
						}
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.DeathAnim);
						if (this.CharacterAnimation[this.DeathAnim].time < 1f)
						{
							base.transform.Translate(Vector3.back * Time.deltaTime);
						}
						else
						{
							Debug.Log("Reloaded from save, calling BecomeRagdoll()");
							this.BecomeRagdoll();
						}
					}
				}
			}
			else
			{
				if (!this.StudentManager.Stop)
				{
					this.StudentManager.StopMoving();
					this.Yandere.Laughing = false;
					this.Yandere.Dipping = false;
					this.Yandere.RPGCamera.enabled = false;
					this.SmartPhone.SetActive(false);
					this.Police.Show = false;
				}
				this.CharacterAnimation.CrossFade(this.CounterAnim);
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.Yandere.transform.position + this.Yandere.transform.forward);
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
			}
		}
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x00177554 File Offset: 0x00175954
	private void UpdatePushed()
	{
		this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
		this.EyeShrink = Mathf.Lerp(this.EyeShrink, 1f, Time.deltaTime * 10f);
		if (this.CharacterAnimation[this.PushedAnim].time >= this.CharacterAnimation[this.PushedAnim].length)
		{
			this.BecomeRagdoll();
		}
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x001775E0 File Offset: 0x001759E0
	private void UpdateDrowned()
	{
		this.SplashTimer += Time.deltaTime;
		if (this.SplashTimer > 3f && this.SplashTimer < 100f)
		{
			this.DrowningSplashes.Play();
			this.SplashTimer += 100f;
		}
		this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
		this.EyeShrink = Mathf.Lerp(this.EyeShrink, 1f, Time.deltaTime * 10f);
		if (this.CharacterAnimation[this.DrownAnim].time >= this.CharacterAnimation[this.DrownAnim].length)
		{
			this.BecomeRagdoll();
		}
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x001776BC File Offset: 0x00175ABC
	private void UpdateWitnessedMurder()
	{
		if (this.Threatened)
		{
			this.UpdateAlarmed();
		}
		else if (!this.Fleeing && !this.Shoving)
		{
			if (this.StudentID > 1 && this.Persona != PersonaType.Evil)
			{
				this.EyeShrink += Time.deltaTime * 0.2f;
			}
			if (this.Yandere.TargetStudent != null && this.LovedOneIsTargeted(this.Yandere.TargetStudent.StudentID))
			{
				this.Strength = 5;
				this.Persona = PersonaType.Heroic;
				this.SmartPhone.SetActive(false);
				this.SprintAnim = this.OriginalSprintAnim;
			}
			if ((this.Club != ClubType.Delinquent || (this.Club == ClubType.Delinquent && this.Injured)) && this.Yandere.TargetStudent == null && this.LovedOneIsTargeted(this.Yandere.NearestCorpseID))
			{
				this.Strength = 5;
				if (this.Injured)
				{
					this.Strength = 1;
				}
				this.Persona = PersonaType.Heroic;
			}
			if (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null && this.Yandere.PickUp.BodyPart.Type == 1 && this.LovedOneIsTargeted(this.Yandere.PickUp.BodyPart.StudentID))
			{
				this.Strength = 5;
				this.Persona = PersonaType.Heroic;
				this.SmartPhone.SetActive(false);
				this.SprintAnim = this.OriginalSprintAnim;
			}
			if (this.Persona != PersonaType.PhoneAddict)
			{
				this.CharacterAnimation.CrossFade(this.ScaredAnim);
			}
			else if (!this.Attacked)
			{
				this.PhoneAddictCameraUpdate();
			}
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.position.x, base.transform.position.y, this.Yandere.Hips.position.z) - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
			if (!this.Yandere.Struggling)
			{
				if (this.Persona != PersonaType.Heroic && this.Persona != PersonaType.Dangerous && this.Persona != PersonaType.Violent)
				{
					this.AlarmTimer += Time.deltaTime * (float)this.MurdersWitnessed;
				}
				else
				{
					this.AlarmTimer += Time.deltaTime * ((float)this.MurdersWitnessed * 5f);
				}
			}
			if (this.AlarmTimer > 5f)
			{
				this.PersonaReaction();
				this.AlarmTimer = 0f;
			}
			else if (this.AlarmTimer > 1f && !this.Reacted)
			{
				if (this.StudentID > 1 || this.Yandere.Mask != null)
				{
					if (this.StudentID == 1)
					{
						Debug.Log("Senpai saw a mask.");
						this.Persona = PersonaType.Heroic;
						this.PersonaReaction();
					}
					if (!this.Teacher)
					{
						if (this.Persona != PersonaType.Evil)
						{
							if (this.Club == ClubType.Delinquent)
							{
								this.SmartPhone.SetActive(false);
							}
							else if (this.StudentID == 10)
							{
								this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 1, 3f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.MurderReaction, 1, 3f);
							}
						}
					}
					else
					{
						if (this.WitnessedCoverUp)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherCoverUpHostile, 1, 5f);
						}
						else
						{
							this.DetermineWhatWasWitnessed();
							this.DetermineTeacherSubtitle();
						}
						this.StudentManager.Portal.SetActive(false);
					}
				}
				else
				{
					Debug.Log("Senpai witnessed murder, and entered a specific murder reaction animation.");
					this.MurderReaction = UnityEngine.Random.Range(1, 6);
					this.CharacterAnimation.CrossFade("senpaiMurderReaction_0" + this.MurderReaction);
					this.GameOverCause = GameOverType.Murder;
					this.SenpaiNoticed();
					this.CharacterAnimation["scaredFace_00"].weight = 0f;
					this.CharacterAnimation[this.AngryFaceAnim].weight = 0f;
					this.Yandere.ShoulderCamera.enabled = true;
					this.Yandere.ShoulderCamera.Noticed = true;
					this.Yandere.RPGCamera.enabled = false;
					this.Stop = true;
				}
				this.Reacted = true;
			}
		}
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x00177BB0 File Offset: 0x00175FB0
	private void UpdateAlarmed()
	{
		Debug.Log(this.Name + " is calling UpdateAlarmed()");
		if (!this.Threatened)
		{
			if (this.Yandere.Medusa && this.YandereVisible)
			{
				this.TurnToStone();
				return;
			}
			if (this.Persona != PersonaType.PhoneAddict && !this.Sleuthing)
			{
				this.SmartPhone.SetActive(false);
			}
			this.OccultBook.SetActive(false);
			this.Pen.SetActive(false);
			this.SpeechLines.Stop();
			this.ReadPhase = 0;
			if (this.WitnessedCorpse)
			{
				if (!this.WalkBack)
				{
					if (this.StudentID == 1)
					{
					}
					if (this.Persona != PersonaType.PhoneAddict)
					{
						this.CharacterAnimation.CrossFade(this.ScaredAnim);
					}
					else if (!this.Attacked)
					{
						this.PhoneAddictCameraUpdate();
					}
				}
				else
				{
					Debug.Log(this.Name + " is walking backwards");
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.MyController.Move(base.transform.forward * (-0.5f * Time.deltaTime));
					this.CharacterAnimation.CrossFade(this.WalkBackAnim);
					this.WalkBackTimer -= Time.deltaTime;
					if (this.WalkBackTimer <= 0f)
					{
						this.WalkBack = false;
					}
				}
			}
			else if (!this.WitnessedLimb)
			{
				if (!this.WitnessedBloodyWeapon)
				{
					if (!this.WitnessedBloodPool)
					{
						if (!this.WitnessedWeapon)
						{
							if (this.StudentID > 1)
							{
								if (this.Witness)
								{
									this.CharacterAnimation.CrossFade(this.LeanAnim);
								}
								else
								{
									this.CharacterAnimation.CrossFade(this.IdleAnim);
									if (this.FocusOnYandere)
									{
										if (this.DistanceToPlayer < 1f && !this.Injured)
										{
											this.AlarmTimer = 0f;
											if (this.Club == ClubType.Council || (this.Club == ClubType.Delinquent && !this.Injured))
											{
												this.ThreatTimer += Time.deltaTime;
												if (this.ThreatTimer > 5f && !this.Yandere.Struggling && !this.Yandere.DelinquentFighting && this.Prompt.InSight)
												{
													this.ThreatTimer = 0f;
													this.Shove();
												}
											}
										}
										this.DistractionSpot = new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z);
									}
								}
							}
							else
							{
								this.CharacterAnimation.CrossFade(this.LeanAnim);
							}
						}
					}
				}
			}
			if (this.WitnessedMurder)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
			}
			else if (this.WitnessedCorpse)
			{
				if (this.Corpse != null && this.Corpse.AllColliders[0] != null)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.Corpse.AllColliders[0].transform.position.x, base.transform.position.y, this.Corpse.AllColliders[0].transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				}
			}
			else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
			{
				if (this.BloodPool != null)
				{
					this.targetRotation = Quaternion.LookRotation(new Vector3(this.BloodPool.transform.position.x, base.transform.position.y, this.BloodPool.transform.position.z) - base.transform.position);
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				}
			}
			else if (!this.DiscCheck)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.transform.position.x, base.transform.position.y, this.Yandere.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
			}
			else
			{
				if (!this.FocusOnYandere)
				{
					this.targetRotation = Quaternion.LookRotation(this.DistractionSpot - base.transform.position);
				}
				else
				{
					this.targetRotation = Quaternion.LookRotation(this.Yandere.transform.position - base.transform.position);
				}
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
			}
			if (!this.Fleeing && !this.Yandere.DelinquentFighting)
			{
				this.AlarmTimer += Time.deltaTime * (1f - this.Hesitation);
			}
			if (!this.CanStillNotice)
			{
				this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia) * 5f;
			}
			if (this.AlarmTimer > 5f)
			{
				this.EndAlarm();
			}
			else if (this.AlarmTimer > 1f && !this.Reacted)
			{
				if (this.Teacher)
				{
					if (!this.WitnessedCorpse)
					{
						Debug.Log("A teacher's subtitle is now being determined.");
						this.CharacterAnimation.CrossFade(this.IdleAnim);
						if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponReaction, 1, 6f);
							this.GameOverCause = GameOverType.Weapon;
						}
						else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.Weapon)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponReaction, 1, 6f);
							this.GameOverCause = GameOverType.Weapon;
						}
						else if (this.Witnessed == StudentWitnessType.Blood)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherBloodReaction, 1, 6f);
							this.GameOverCause = GameOverType.Blood;
						}
						else if (this.Witnessed == StudentWitnessType.Insanity || this.Witnessed == StudentWitnessType.Poisoning)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.Lewd)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherLewdReaction, 1, 6f);
							this.GameOverCause = GameOverType.Lewd;
						}
						else if (this.Witnessed == StudentWitnessType.Violence)
						{
							Debug.Log("A teacher witnessed violence.");
							this.Subtitle.UpdateLabel(SubtitleType.TeacherTrespassingReaction, 5, 5f);
							this.GameOverCause = GameOverType.Violence;
							this.Concern = 5;
						}
						else if (this.Witnessed == StudentWitnessType.Trespassing)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherTrespassingReaction, this.Concern, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.Theft || this.Witnessed == StudentWitnessType.Pickpocketing)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherTheftReaction, 1, 6f);
						}
						else if (this.Witnessed == StudentWitnessType.CleaningItem)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
							this.GameOverCause = GameOverType.Insanity;
						}
						else if (this.Witnessed == StudentWitnessType.CleaningItem)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
							this.GameOverCause = GameOverType.Insanity;
						}
						if (this.Club == ClubType.Council)
						{
							UnityEngine.Object.Destroy(this.Subtitle.CurrentClip);
							this.Subtitle.UpdateLabel(SubtitleType.CouncilToCounselor, this.ClubMemberID, 6f);
						}
						if (this.BloodPool != null)
						{
							Debug.Log("The teacher was alarmed because she saw something weird on the ground.");
							UnityEngine.Object.Destroy(this.Subtitle.CurrentClip);
							this.Subtitle.UpdateLabel(SubtitleType.BloodPoolReaction, 2, 5f);
							PromptScript component = this.BloodPool.GetComponent<PromptScript>();
							if (component != null)
							{
								Debug.Log("Disabling a bloody object's prompt because a teacher is heading for it.");
								component.Hide();
								component.enabled = false;
							}
						}
					}
					else
					{
						Debug.Log("A teacher found a corpse.");
						this.Concern = 1;
						this.DetermineWhatWasWitnessed();
						this.DetermineTeacherSubtitle();
						if (this.WitnessedMurder)
						{
							this.MurdersWitnessed++;
							if (!this.Yandere.Chased)
							{
								Debug.Log("A teacher has reached ChaseYandere() through UpdateAlarm().");
								this.ChaseYandere();
							}
						}
					}
					if (!this.Guarding && !this.Chasing && ((this.YandereVisible && this.Concern == 5) || this.Yandere.Noticed))
					{
						Debug.Log("Yandere-chan is getting sent to the guidance counselor.");
						if (this.Witnessed == StudentWitnessType.Theft && this.Yandere.StolenObject != null)
						{
							this.Yandere.StolenObject.SetActive(true);
							this.Yandere.StolenObject = null;
							this.Yandere.Inventory.IDCard = false;
						}
						this.StudentManager.CombatMinigame.Stop();
						this.CharacterAnimation[this.AngryFaceAnim].weight = 1f;
						this.Yandere.ShoulderCamera.enabled = true;
						this.Yandere.ShoulderCamera.Noticed = true;
						this.Yandere.RPGCamera.enabled = false;
						this.Stop = true;
					}
				}
				else if (this.StudentID > 1 || this.Yandere.Mask != null)
				{
					if (this.StudentID == 41)
					{
						this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5f);
					}
					else if (this.RepeatReaction)
					{
						this.Subtitle.UpdateLabel(SubtitleType.RepeatReaction, 1, 3f);
						this.RepeatReaction = false;
					}
					else if (this.Club != ClubType.Delinquent)
					{
						if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.WeaponAndBloodAndInsanityReaction, 1, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
						{
							this.Subtitle.UpdateLabel(SubtitleType.WeaponAndBloodReaction, 1, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.WeaponAndInsanityReaction, 1, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.BloodAndInsanityReaction, 1, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.Weapon)
						{
							this.Subtitle.StudentID = this.StudentID;
							this.Subtitle.UpdateLabel(SubtitleType.WeaponReaction, this.WeaponWitnessed, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.Blood)
						{
							if (!this.Bloody)
							{
								this.Subtitle.UpdateLabel(SubtitleType.BloodReaction, 1, 3f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.WetBloodReaction, 1, 3f);
								this.Witnessed = StudentWitnessType.None;
								this.Witness = false;
							}
						}
						else if (this.Witnessed == StudentWitnessType.Insanity)
						{
							this.Subtitle.UpdateLabel(SubtitleType.InsanityReaction, 1, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.Lewd)
						{
							this.Subtitle.UpdateLabel(SubtitleType.LewdReaction, 1, 3f);
						}
						else if (this.Witnessed == StudentWitnessType.CleaningItem)
						{
							this.Subtitle.UpdateLabel(SubtitleType.SuspiciousReaction, 0, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.Suspicious)
						{
							this.Subtitle.UpdateLabel(SubtitleType.SuspiciousReaction, 1, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.Corpse)
						{
							Debug.Log(this.Name + " is currently reacting to a corpse and deciding what subtitle to use.");
							if (this.StudentID == 10 && this.Corpse.StudentID == this.StudentManager.RivalID)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.RaibaruRivalDeathReaction, 1, 5f);
								Debug.Log("Raibaru is reacting to Osana's corpse with a unique subtitle.");
							}
							else if (this.Club == ClubType.Council)
							{
								if (this.StudentID == 86)
								{
									this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 1, 5f);
								}
								else if (this.StudentID == 87)
								{
									this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 2, 5f);
								}
								else if (this.StudentID == 88)
								{
									this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 3, 5f);
								}
								else if (this.StudentID == 89)
								{
									this.Subtitle.UpdateLabel(SubtitleType.CouncilCorpseReaction, 4, 5f);
								}
							}
							else if (this.Persona == PersonaType.Evil)
							{
								this.Subtitle.UpdateLabel(SubtitleType.EvilCorpseReaction, 1, 5f);
							}
							else if (!this.Corpse.Choking)
							{
								this.Subtitle.UpdateLabel(SubtitleType.CorpseReaction, 0, 5f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.CorpseReaction, 1, 5f);
							}
						}
						else if (this.Witnessed == StudentWitnessType.Interruption)
						{
							if (this.StudentID == 11)
							{
								this.Subtitle.UpdateLabel(SubtitleType.InterruptionReaction, 1, 5f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.InterruptionReaction, 2, 5f);
							}
						}
						else if (this.Witnessed == StudentWitnessType.Eavesdropping)
						{
							if (this.StudentID == this.StudentManager.RivalID)
							{
								this.Subtitle.UpdateLabel(SubtitleType.RivalEavesdropReaction, 0, 9f);
								this.Hesitation = 0.6f;
							}
							else if (this.StudentID == 10)
							{
								this.Subtitle.UpdateLabel(SubtitleType.RivalEavesdropReaction, 1, 9f);
								this.Hesitation = 0.6f;
							}
							else if (this.EventInterrupted)
							{
								this.Subtitle.UpdateLabel(SubtitleType.EventEavesdropReaction, 1, 5f);
								this.EventInterrupted = false;
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.EavesdropReaction, 1, 5f);
							}
						}
						else if (this.Witnessed == StudentWitnessType.Pickpocketing)
						{
							this.Subtitle.UpdateLabel(this.PickpocketSubtitleType, 1, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.Violence)
						{
							this.Subtitle.UpdateLabel(SubtitleType.ViolenceReaction, 5, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.Poisoning)
						{
							if (this.Yandere.TargetBento.StudentID != this.StudentID)
							{
								this.Subtitle.UpdateLabel(SubtitleType.PoisonReaction, 1, 5f);
							}
							else
							{
								this.Subtitle.UpdateLabel(SubtitleType.PoisonReaction, 2, 5f);
								this.Phase++;
								this.Pathfinding.target = this.Destinations[this.Phase];
								this.CurrentDestination = this.Destinations[this.Phase];
							}
						}
						else if (this.Witnessed == StudentWitnessType.SeveredLimb)
						{
							this.Subtitle.UpdateLabel(SubtitleType.LimbReaction, 0, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.BloodyWeapon)
						{
							this.Subtitle.UpdateLabel(SubtitleType.BloodyWeaponReaction, 0, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.DroppedWeapon)
						{
							this.Subtitle.UpdateLabel(SubtitleType.BloodyWeaponReaction, 0, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.BloodPool)
						{
							this.Subtitle.UpdateLabel(SubtitleType.BloodPoolReaction, 0, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.HoldingBloodyClothing)
						{
							this.Subtitle.UpdateLabel(SubtitleType.HoldingBloodyClothingReaction, 0, 5f);
						}
						else if (this.Witnessed == StudentWitnessType.Theft)
						{
							this.Subtitle.UpdateLabel(SubtitleType.TheftReaction, 0, 5f);
						}
						else
						{
							this.Subtitle.UpdateLabel(SubtitleType.HmmReaction, 1, 3f);
						}
					}
					else if (this.Witnessed == StudentWitnessType.None)
					{
						this.Subtitle.Speaker = this;
						this.Subtitle.UpdateLabel(SubtitleType.DelinquentHmm, 0, 5f);
					}
					else if (this.Witnessed == StudentWitnessType.Corpse)
					{
						if (this.FoundEnemyCorpse)
						{
							this.Subtitle.UpdateLabel(SubtitleType.EvilDelinquentCorpseReaction, 1, 5f);
						}
						else if (this.Corpse.Student.Club == ClubType.Delinquent)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentFriendCorpseReaction, 1, 5f);
							this.FoundFriendCorpse = true;
						}
						else
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentCorpseReaction, 1, 5f);
						}
					}
					else if (this.Witnessed == StudentWitnessType.Weapon && !this.Injured)
					{
						this.Subtitle.Speaker = this;
						this.Subtitle.UpdateLabel(SubtitleType.DelinquentWeaponReaction, 0, 3f);
					}
					else
					{
						this.Subtitle.Speaker = this;
						if (this.WitnessedLimb || this.WitnessedWeapon || this.WitnessedBloodPool || this.WitnessedBloodyWeapon)
						{
							this.Subtitle.UpdateLabel(SubtitleType.LimbReaction, 0, 5f);
						}
						else
						{
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentReaction, 0, 5f);
							Debug.Log("A delinquent is reacting to Yandere-chan's behavior.");
						}
					}
				}
				else
				{
					Debug.Log("We are now determining what Senpai saw...");
					if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
					{
						this.CharacterAnimation.CrossFade("senpaiInsanityReaction_00");
						this.GameOverCause = GameOverType.Insanity;
					}
					else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
					{
						this.CharacterAnimation.CrossFade("senpaiWeaponReaction_00");
						this.GameOverCause = GameOverType.Weapon;
					}
					else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
					{
						this.CharacterAnimation.CrossFade("senpaiInsanityReaction_00");
						this.GameOverCause = GameOverType.Insanity;
					}
					else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
					{
						this.CharacterAnimation.CrossFade("senpaiInsanityReaction_00");
						this.GameOverCause = GameOverType.Insanity;
					}
					else if (this.Witnessed == StudentWitnessType.Weapon)
					{
						this.CharacterAnimation.CrossFade("senpaiWeaponReaction_00");
						this.GameOverCause = GameOverType.Weapon;
					}
					else if (this.Witnessed == StudentWitnessType.Blood)
					{
						this.CharacterAnimation.CrossFade("senpaiBloodReaction_00");
						this.GameOverCause = GameOverType.Blood;
					}
					else if (this.Witnessed == StudentWitnessType.Insanity)
					{
						this.CharacterAnimation.CrossFade("senpaiInsanityReaction_00");
						this.GameOverCause = GameOverType.Insanity;
					}
					else if (this.Witnessed == StudentWitnessType.Lewd)
					{
						this.CharacterAnimation.CrossFade("senpaiLewdReaction_00");
						this.GameOverCause = GameOverType.Lewd;
					}
					else if (this.Witnessed == StudentWitnessType.Stalking)
					{
						if (this.Concern < 5)
						{
							this.Subtitle.UpdateLabel(SubtitleType.SenpaiStalkingReaction, this.Concern, 4.5f);
						}
						else
						{
							this.CharacterAnimation.CrossFade("senpaiCreepyReaction_00");
							this.GameOverCause = GameOverType.Stalking;
						}
						this.Witnessed = StudentWitnessType.None;
					}
					else if (this.Witnessed == StudentWitnessType.Corpse)
					{
						if (this.Corpse.StudentID == this.StudentManager.RivalID)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.SenpaiRivalDeathReaction, 1, 5f);
							Debug.Log("Senpai is reacting to Osana's corpse with a unique subtitle.");
						}
						else
						{
							this.Subtitle.UpdateLabel(SubtitleType.SenpaiCorpseReaction, 1, 5f);
						}
					}
					else if (this.Witnessed == StudentWitnessType.Violence)
					{
						this.CharacterAnimation.CrossFade("senpaiFightReaction_00");
						this.GameOverCause = GameOverType.Violence;
						this.Concern = 5;
					}
					if (this.Concern == 5)
					{
						this.CharacterAnimation["scaredFace_00"].weight = 0f;
						this.CharacterAnimation[this.AngryFaceAnim].weight = 0f;
						this.Yandere.ShoulderCamera.enabled = true;
						this.Yandere.ShoulderCamera.Noticed = true;
						this.Yandere.RPGCamera.enabled = false;
						this.Stop = true;
					}
				}
				this.Reacted = true;
			}
			if (this.Club == ClubType.Council && this.DistanceToPlayer < 1.1f && (this.Yandere.Armed || this.Yandere.Carrying || this.Yandere.Dragging) && this.Prompt.InSight)
			{
				this.Spray();
			}
		}
		else
		{
			this.Alarm -= Time.deltaTime * 100f * (1f / this.Paranoia);
			if (this.StudentManager.CombatMinigame.Delinquent == null || this.StudentManager.CombatMinigame.Delinquent == this)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
			}
			else
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.StudentManager.CombatMinigame.Midpoint.position.x, base.transform.position.y, this.StudentManager.CombatMinigame.Midpoint.position.z) - base.transform.position);
			}
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
			if (this.Yandere.DelinquentFighting && this.StudentManager.CombatMinigame.Delinquent != this)
			{
				if (this.StudentManager.CombatMinigame.Path < 4)
				{
					if (this.DistanceToPlayer < 1f)
					{
						this.MyController.Move(base.transform.forward * Time.deltaTime * -1f);
					}
					if (Vector3.Distance(base.transform.position, this.StudentManager.CombatMinigame.Delinquent.transform.position) < 1f)
					{
						this.MyController.Move(base.transform.forward * Time.deltaTime * -1f);
					}
					if (this.Yandere.enabled)
					{
						this.CheerTimer = Mathf.MoveTowards(this.CheerTimer, 0f, Time.deltaTime);
						if (this.CheerTimer == 0f)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentCheer, 0, 5f);
							this.CheerTimer = UnityEngine.Random.Range(2f, 3f);
						}
					}
					this.CharacterAnimation.CrossFade(this.RandomCheerAnim);
					if (this.CharacterAnimation[this.RandomCheerAnim].time >= this.CharacterAnimation[this.RandomCheerAnim].length)
					{
						this.RandomCheerAnim = this.CheerAnims[UnityEngine.Random.Range(0, this.CheerAnims.Length)];
					}
					this.ThreatPhase = 3;
					this.ThreatTimer = 0f;
					if (this.WitnessedMurder)
					{
						this.Injured = true;
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim, 5f);
					this.NoTalk = true;
				}
			}
			else if (!this.Injured)
			{
				if (this.DistanceToPlayer > 5f + this.ThreatDistance && this.ThreatPhase < 4)
				{
					this.ThreatPhase = 3;
					this.ThreatTimer = 0f;
				}
				if (!this.Yandere.Shoved && !this.Yandere.Dumping)
				{
					if (this.DistanceToPlayer > 1f && this.Patience > 0)
					{
						if (this.ThreatPhase == 1)
						{
							this.CharacterAnimation.CrossFade("delinquentShock_00");
							if (this.CharacterAnimation["delinquentShock_00"].time >= this.CharacterAnimation["delinquentShock_00"].length)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentThreatened, 0, 3f);
								this.CharacterAnimation.CrossFade("delinquentCombatIdle_00");
								this.ThreatTimer = 5f;
								this.ThreatPhase++;
							}
						}
						else if (this.ThreatPhase == 2)
						{
							this.ThreatTimer -= Time.deltaTime;
							if (this.ThreatTimer < 0f)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentTaunt, 0, 5f);
								this.ThreatTimer = 5f;
								this.ThreatPhase++;
							}
						}
						else if (this.ThreatPhase == 3)
						{
							this.ThreatTimer -= Time.deltaTime;
							if (this.ThreatTimer < 0f)
							{
								if (!this.NoTalk)
								{
									this.Subtitle.Speaker = this;
									this.Subtitle.UpdateLabel(SubtitleType.DelinquentCalm, 0, 5f);
								}
								this.CharacterAnimation.CrossFade(this.IdleAnim, 5f);
								this.ThreatTimer = 5f;
								this.ThreatPhase++;
							}
						}
						else if (this.ThreatPhase == 4)
						{
							this.ThreatTimer -= Time.deltaTime;
							if (this.ThreatTimer < 0f)
							{
								if (this.CurrentDestination != this.Destinations[this.Phase])
								{
									this.StopInvestigating();
								}
								this.Distracted = false;
								this.Threatened = false;
								this.Alarmed = false;
								this.Routine = true;
								this.NoTalk = false;
								this.IgnoreTimer = 5f;
								this.AlarmTimer = 0f;
							}
						}
					}
					else if (!this.NoTalk)
					{
						string str = string.Empty;
						if (!this.Male)
						{
							str = "Female_";
						}
						if (this.StudentID == 46)
						{
							this.CharacterAnimation.CrossFade("delinquentDrawWeapon_00");
						}
						if (this.StudentManager.CombatMinigame.Delinquent == null)
						{
							this.Yandere.CharacterAnimation.CrossFade("Yandere_CombatIdle");
							if (this.MyWeapon.transform.parent != this.ItemParent)
							{
								Debug.Log("This character should be drawing a weapon.");
								this.CharacterAnimation.CrossFade(str + "delinquentDrawWeapon_00");
							}
							else
							{
								this.CharacterAnimation.CrossFade("delinquentCombatIdle_00");
							}
							if (this.Yandere.Carrying || this.Yandere.Dragging)
							{
								this.Yandere.EmptyHands();
							}
							if (this.Yandere.Pickpocketing)
							{
								this.Yandere.Caught = true;
								this.PickPocket.PickpocketMinigame.End();
							}
							this.Yandere.StopLaughing();
							this.Yandere.StopAiming();
							this.Yandere.Unequip();
							if (this.Yandere.PickUp != null)
							{
								this.Yandere.EmptyHands();
							}
							this.Yandere.DelinquentFighting = true;
							this.Yandere.NearSenpai = false;
							this.Yandere.Degloving = false;
							this.Yandere.CanMove = false;
							this.Yandere.GloveTimer = 0f;
							this.Distracted = true;
							this.Fighting = true;
							this.ThreatTimer = 0f;
							this.StudentManager.CombatMinigame.Delinquent = this;
							this.StudentManager.CombatMinigame.enabled = true;
							this.StudentManager.CombatMinigame.StartCombat();
							this.SpeechLines.Stop();
							this.SpawnAlarmDisc();
							if (this.WitnessedMurder)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentAvenge, 0, 5f);
							}
							else if (!this.StudentManager.CombatMinigame.Practice)
							{
								this.Subtitle.Speaker = this;
								this.Subtitle.UpdateLabel(SubtitleType.DelinquentFight, 0, 5f);
							}
						}
						this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(this.Hips.transform.position.x, this.Yandere.transform.position.y, this.Hips.transform.position.z) - this.Yandere.transform.position);
						if (this.CharacterAnimation[str + "delinquentDrawWeapon_00"].time >= 0.5f)
						{
							this.MyWeapon.transform.parent = this.ItemParent;
							this.MyWeapon.transform.localEulerAngles = new Vector3(0f, 15f, 0f);
							this.MyWeapon.transform.localPosition = new Vector3(0.01f, 0f, 0f);
						}
						if (this.CharacterAnimation[str + "delinquentDrawWeapon_00"].time >= this.CharacterAnimation[str + "delinquentDrawWeapon_00"].length)
						{
							this.Threatened = false;
							this.Alarmed = false;
							base.enabled = false;
						}
					}
					else
					{
						this.ThreatTimer -= Time.deltaTime;
						if (this.ThreatTimer < 0f)
						{
							if (this.CurrentDestination != this.Destinations[this.Phase])
							{
								this.StopInvestigating();
							}
							this.Distracted = false;
							this.Threatened = false;
							this.Alarmed = false;
							this.Routine = true;
							this.NoTalk = false;
							this.IgnoreTimer = 5f;
							this.AlarmTimer = 0f;
						}
					}
				}
			}
			else
			{
				this.ThreatTimer += Time.deltaTime;
				if (this.ThreatTimer > 5f)
				{
					this.DistanceToDestination = 100f;
					if (!this.WitnessedMurder)
					{
						this.Distracted = false;
						this.Threatened = false;
						this.EndAlarm();
					}
					else
					{
						this.Threatened = false;
						this.Alarmed = false;
						this.Injured = false;
						this.PersonaReaction();
					}
				}
			}
		}
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x00179D94 File Offset: 0x00178194
	private void UpdateBurning()
	{
		if (this.DistanceToPlayer < 1f && !this.Yandere.Shoved && !this.Yandere.Egg)
		{
			this.PushYandereAway();
		}
		if (this.BurnTarget != Vector3.zero)
		{
			this.MoveTowardsTarget(this.BurnTarget);
		}
		if (this.CharacterAnimation[this.BurningAnim].time > this.CharacterAnimation[this.BurningAnim].length)
		{
			this.DeathType = DeathType.Burning;
			this.BecomeRagdoll();
		}
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x00179E38 File Offset: 0x00178238
	private void UpdateSplashed()
	{
		this.CharacterAnimation.CrossFade(this.SplashedAnim);
		if (this.Yandere.Tripping)
		{
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
		}
		this.SplashTimer += Time.deltaTime;
		if (this.SplashTimer > 2f && this.SplashPhase == 1)
		{
			if (this.Gas)
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 5, 5f);
			}
			else if (this.Bloody)
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 3, 5f);
			}
			else if (this.Yandere.Tripping)
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 7, 5f);
			}
			else
			{
				this.Subtitle.Speaker = this;
				this.Subtitle.UpdateLabel(this.SplashSubtitleType, 1, 5f);
			}
			this.CharacterAnimation[this.SplashedAnim].speed = 0.5f;
			this.SplashPhase++;
		}
		if (this.SplashTimer > 12f && this.SplashPhase == 2)
		{
			if (this.LightSwitch == null)
			{
				if (this.Gas)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 6, 5f);
				}
				else if (this.Bloody)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 4, 5f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 2, 5f);
				}
				this.SplashPhase++;
				if (!this.Male)
				{
					this.CurrentDestination = this.StudentManager.FemaleStripSpot;
					this.Pathfinding.target = this.StudentManager.FemaleStripSpot;
				}
				else
				{
					this.CurrentDestination = this.StudentManager.MaleStripSpot;
					this.Pathfinding.target = this.StudentManager.MaleStripSpot;
				}
			}
			else if (!this.LightSwitch.BathroomLight.activeInHierarchy)
			{
				if (this.LightSwitch.Panel.useGravity)
				{
					this.LightSwitch.Prompt.Hide();
					this.LightSwitch.Prompt.enabled = false;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
				this.Subtitle.UpdateLabel(SubtitleType.LightSwitchReaction, 1, 5f);
				this.CurrentDestination = this.LightSwitch.ElectrocutionSpot;
				this.Pathfinding.target = this.LightSwitch.ElectrocutionSpot;
				this.Pathfinding.speed = 1f;
				this.BathePhase = -1;
				this.InDarkness = true;
			}
			else
			{
				if (!this.Bloody)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 2, 5f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(this.SplashSubtitleType, 4, 5f);
				}
				this.SplashPhase++;
				this.CurrentDestination = this.StudentManager.FemaleStripSpot;
				this.Pathfinding.target = this.StudentManager.FemaleStripSpot;
			}
			this.Pathfinding.canSearch = true;
			this.Pathfinding.canMove = true;
			this.Splashed = false;
		}
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x0017A2A8 File Offset: 0x001786A8
	private void UpdateTurningOffRadio()
	{
		if (this.Radio.On || (this.RadioPhase == 3 && this.Radio.transform.parent == null))
		{
			if (this.RadioPhase == 1)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Radio.transform.position.x, base.transform.position.y, this.Radio.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.RadioTimer += Time.deltaTime;
				if (this.RadioTimer > 3f)
				{
					if (this.Persona == PersonaType.PhoneAddict)
					{
						this.SmartPhone.SetActive(true);
					}
					this.CharacterAnimation.CrossFade(this.WalkAnim);
					this.CurrentDestination = this.Radio.transform;
					this.Pathfinding.target = this.Radio.transform;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.RadioTimer = 0f;
					this.RadioPhase++;
				}
			}
			else if (this.RadioPhase == 2)
			{
				if (this.DistanceToDestination < 0.5f)
				{
					this.CharacterAnimation.CrossFade(this.RadioAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.SmartPhone.SetActive(false);
					this.RadioPhase++;
				}
			}
			else if (this.RadioPhase == 3)
			{
				this.targetRotation = Quaternion.LookRotation(new Vector3(this.Radio.transform.position.x, base.transform.position.y, this.Radio.transform.position.z) - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.RadioTimer += Time.deltaTime;
				if (this.RadioTimer > 4f)
				{
					if (this.Persona == PersonaType.PhoneAddict)
					{
						this.SmartPhone.SetActive(true);
					}
					this.CurrentDestination = this.Destinations[this.Phase];
					this.Pathfinding.target = this.Destinations[this.Phase];
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.ForgetRadio();
				}
				else if (this.RadioTimer > 2f)
				{
					this.Radio.Victim = null;
					this.Radio.TurnOff();
				}
			}
		}
		else
		{
			if (this.RadioPhase < 100)
			{
				this.CharacterAnimation.CrossFade(this.IdleAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				this.RadioPhase = 100;
				this.RadioTimer = 0f;
			}
			this.targetRotation = Quaternion.LookRotation(new Vector3(this.Radio.transform.position.x, base.transform.position.y, this.Radio.transform.position.z) - base.transform.position);
			this.RadioTimer += Time.deltaTime;
			if (this.RadioTimer > 1f || this.Radio.transform.parent != null)
			{
				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.Destinations[this.Phase];
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.ForgetRadio();
			}
		}
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x0017A71C File Offset: 0x00178B1C
	private void UpdateVomiting()
	{
		if (this.VomitPhase != 0 && this.VomitPhase != 4)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
			this.MoveTowardsTarget(this.CurrentDestination.position);
		}
		if (this.VomitPhase == 0)
		{
			if (this.DistanceToDestination < 0.5f)
			{
				Debug.Log("Character is now drownable.");
				this.Prompt.Label[0].text = "     Drown";
				this.Prompt.HideButton[0] = false;
				this.Prompt.enabled = true;
				this.Drownable = true;
				if (this.VomitDoor != null)
				{
					this.VomitDoor.Prompt.enabled = false;
					this.VomitDoor.Prompt.Hide();
					this.VomitDoor.enabled = false;
				}
				this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.CharacterAnimation.CrossFade(this.VomitAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 1)
		{
			if (this.CharacterAnimation[this.VomitAnim].time > 1f)
			{
				this.VomitEmitter.gameObject.SetActive(true);
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 2)
		{
			if (this.CharacterAnimation[this.VomitAnim].time > 13f)
			{
				this.VomitEmitter.gameObject.SetActive(false);
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 3)
		{
			if (this.CharacterAnimation[this.VomitAnim].time >= this.CharacterAnimation[this.VomitAnim].length)
			{
				this.Prompt.Label[0].text = "     Talk";
				this.Drownable = false;
				this.WalkAnim = this.OriginalWalkAnim;
				this.CharacterAnimation.CrossFade(this.WalkAnim);
				if (this.Male)
				{
					this.StudentManager.GetMaleWashSpot(this);
					this.Pathfinding.target = this.StudentManager.MaleWashSpot;
					this.CurrentDestination = this.StudentManager.MaleWashSpot;
				}
				else
				{
					this.StudentManager.GetFemaleWashSpot(this);
					this.Pathfinding.target = this.StudentManager.FemaleWashSpot;
					this.CurrentDestination = this.StudentManager.FemaleWashSpot;
				}
				if (this.VomitDoor != null)
				{
					this.VomitDoor.Prompt.enabled = true;
					this.VomitDoor.enabled = true;
				}
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Pathfinding.speed = 1f;
				this.DistanceToDestination = 100f;
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 4)
		{
			if (this.DistanceToDestination < 0.5f)
			{
				this.CharacterAnimation.CrossFade(this.WashFaceAnim);
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				this.VomitPhase++;
			}
		}
		else if (this.VomitPhase == 5 && this.CharacterAnimation[this.WashFaceAnim].time > this.CharacterAnimation[this.WashFaceAnim].length)
		{
			this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Prompt.Label[0].text = "     Talk";
			this.Pathfinding.canSearch = true;
			this.Pathfinding.canMove = true;
			this.Distracted = false;
			this.Drownable = false;
			this.Vomiting = false;
			this.Private = false;
			this.CanTalk = true;
			this.Routine = true;
			this.Emetic = false;
			this.VomitPhase = 0;
			this.WalkAnim = this.OriginalWalkAnim;
			this.Phase++;
			this.Pathfinding.target = this.Destinations[this.Phase];
			this.CurrentDestination = this.Destinations[this.Phase];
			this.DistanceToDestination = 100f;
		}
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x0017ABC0 File Offset: 0x00178FC0
	private void UpdateConfessing()
	{
		if (!this.Male)
		{
			if (this.ConfessPhase == 1)
			{
				if (this.DistanceToDestination < 0.5f)
				{
					this.Cosmetic.MyRenderer.materials[2].SetFloat("_BlendAmount", 1f);
					this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.CharacterAnimation.CrossFade("f02_insertNote_00");
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.Note.SetActive(true);
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 2)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
				this.MoveTowardsTarget(this.CurrentDestination.position);
				if (this.CharacterAnimation["f02_insertNote_00"].time >= 9f)
				{
					this.Note.SetActive(false);
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 3)
			{
				if (this.CharacterAnimation["f02_insertNote_00"].time >= this.CharacterAnimation["f02_insertNote_00"].length)
				{
					Debug.Log("Sprinting 15");
					this.CurrentDestination = this.StudentManager.RivalConfessionSpot;
					this.Pathfinding.target = this.StudentManager.RivalConfessionSpot;
					this.Pathfinding.canSearch = true;
					this.Pathfinding.canMove = true;
					this.Pathfinding.speed = 4f;
					this.StudentManager.LoveManager.LeftNote = true;
					Debug.Log("Sprinting 8");
					this.CharacterAnimation.CrossFade(this.SprintAnim);
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 4)
			{
				if (this.DistanceToDestination < 0.5f)
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
					this.Pathfinding.canSearch = false;
					this.Pathfinding.canMove = false;
					this.ConfessPhase++;
				}
			}
			else if (this.ConfessPhase == 5)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
				this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.ShyAnim].weight, 1f, Time.deltaTime);
				this.MoveTowardsTarget(this.CurrentDestination.position);
			}
		}
		else if (this.ConfessPhase == 1)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
			this.MoveTowardsTarget(this.CurrentDestination.position);
			if (this.CharacterAnimation["keepNote_00"].time > 14f)
			{
				this.Note.SetActive(false);
			}
			else if ((double)this.CharacterAnimation["keepNote_00"].time > 4.5)
			{
				this.Note.SetActive(true);
			}
			if (this.CharacterAnimation["keepNote_00"].time >= this.CharacterAnimation["keepNote_00"].length)
			{
				Debug.Log("Sprinting 16");
				this.CurrentDestination = this.StudentManager.SuitorConfessionSpot;
				this.Pathfinding.target = this.StudentManager.SuitorConfessionSpot;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Pathfinding.speed = 4f;
				Debug.Log("Sprinting 9");
				this.CharacterAnimation.CrossFade(this.SprintAnim);
				this.ConfessPhase++;
			}
		}
		else if (this.ConfessPhase == 2)
		{
			if (this.DistanceToDestination < 0.5f)
			{
				this.CharacterAnimation.CrossFade("exhausted_00");
				this.Pathfinding.canSearch = false;
				this.Pathfinding.canMove = false;
				this.ConfessPhase++;
			}
		}
		else if (this.ConfessPhase == 3)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.CurrentDestination.rotation, Time.deltaTime * 10f);
			this.MoveTowardsTarget(this.CurrentDestination.position);
		}
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x0017B0B8 File Offset: 0x001794B8
	private void UpdateMisc()
	{
		if (this.IgnoreTimer > 0f)
		{
			this.IgnoreTimer = Mathf.MoveTowards(this.IgnoreTimer, 0f, Time.deltaTime);
		}
		if (!this.Fleeing)
		{
			if (base.transform.position.z < -100f)
			{
				if (base.transform.position.y < -11f && this.StudentID > 1)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			else
			{
				if (base.transform.position.y < -0f)
				{
					base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
				}
				if (!this.Dying && !this.Distracted && !this.WalkBack && !this.Waiting && !this.Guarding && !this.WitnessedMurder && !this.WitnessedCorpse && !this.Blind && !this.SentHome && !this.TurnOffRadio && !this.Wet && !this.InvestigatingBloodPool && !this.ReturningMisplacedWeapon && !this.Yandere.Egg && !this.StudentManager.Pose && !this.ShoeRemoval.enabled && !this.Drownable)
				{
					if (this.StudentManager.MissionMode && (double)this.DistanceToPlayer < 0.5)
					{
						Debug.Log("This student cannot be interacted with right now.");
						this.Yandere.Shutter.FaceStudent = this;
						this.Yandere.Shutter.Penalize();
					}
					if (this.Club == ClubType.Council)
					{
						if (this.DistanceToPlayer < 5f)
						{
							if (this.DistanceToPlayer < 2f)
							{
								this.StudentManager.TutorialWindow.ShowCouncilMessage = true;
							}
							float f = Vector3.Angle(-base.transform.forward, this.Yandere.transform.position - base.transform.position);
							if (Mathf.Abs(f) <= 45f && this.Yandere.Stance.Current != StanceType.Crouching && this.Yandere.Stance.Current != StanceType.Crawling && (this.Yandere.h != 0f || this.Yandere.v != 0f) && (this.Yandere.Running || this.DistanceToPlayer < 2f))
							{
								this.DistractionSpot = this.Yandere.transform.position;
								this.Alarm = 100f + Time.deltaTime * 100f * (1f / this.Paranoia);
								this.FocusOnYandere = true;
								this.Pathfinding.canSearch = false;
								this.Pathfinding.canMove = false;
								this.StopInvestigating();
							}
						}
						if (this.DistanceToPlayer < 1.1f)
						{
							float f2 = Vector3.Angle(-base.transform.forward, this.Yandere.transform.position - base.transform.position);
							if (Mathf.Abs(f2) > 45f && (this.Yandere.Armed || this.Yandere.Carrying || this.Yandere.Dragging) && this.Prompt.InSight)
							{
								this.Spray();
							}
						}
					}
					if (((this.Club == ClubType.Council && !this.Spraying) || (this.Club == ClubType.Delinquent && !this.Injured && !this.RespectEarned && !this.Vomiting && !this.Emetic && !this.Headache && !this.Sedated && !this.Lethal)) && (double)this.DistanceToPlayer < 0.5 && this.Yandere.CanMove && (this.Yandere.h != 0f || this.Yandere.v != 0f))
					{
						if (this.Club == ClubType.Delinquent)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentShove, 0, 3f);
						}
						this.Shove();
					}
				}
			}
		}
		if (this.Wet && !this.Splashed && this.BathePhase == 1 && !this.Burning && !this.Dying)
		{
			if (this.CharacterAnimation[this.WetAnim].weight < 1f)
			{
				this.CharacterAnimation[this.WetAnim].weight = 1f;
			}
		}
		else if (this.CharacterAnimation[this.WetAnim].weight > 0f)
		{
			this.CharacterAnimation[this.WetAnim].weight = 0f;
		}
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x0017B664 File Offset: 0x00179A64
	private void LateUpdate()
	{
		if (this.StudentManager.DisableFarAnims && this.DistanceToPlayer >= (float)this.StudentManager.FarAnimThreshold && this.CharacterAnimation.cullingType != AnimationCullingType.AlwaysAnimate && !this.WitnessCamera.Show)
		{
			this.CharacterAnimation.enabled = false;
		}
		else
		{
			this.CharacterAnimation.enabled = true;
		}
		if (this.EyeShrink > 0f)
		{
			if (this.EyeShrink > 1f)
			{
				this.EyeShrink = 1f;
			}
			this.LeftEye.localPosition = new Vector3(this.LeftEye.localPosition.x, this.LeftEye.localPosition.y, this.LeftEyeOrigin.z - this.EyeShrink * 0.01f);
			this.RightEye.localPosition = new Vector3(this.RightEye.localPosition.x, this.RightEye.localPosition.y, this.RightEyeOrigin.z + this.EyeShrink * 0.01f);
			this.LeftEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.LeftEye.localScale.z);
			this.RightEye.localScale = new Vector3(1f - this.EyeShrink * 0.5f, 1f - this.EyeShrink * 0.5f, this.RightEye.localScale.z);
			this.PreviousEyeShrink = this.EyeShrink;
		}
		if (!this.Male)
		{
			if (this.Shy)
			{
				if (this.Routine)
				{
					if ((this.Phase == 2 && this.DistanceToDestination < 1f) || (this.Phase == 4 && this.DistanceToDestination < 1f) || (this.Actions[this.Phase] == StudentActionType.SitAndTakeNotes && this.DistanceToDestination < 1f) || (this.Actions[this.Phase] == StudentActionType.Clean && this.DistanceToDestination < 1f) || (this.Actions[this.Phase] == StudentActionType.Read && this.DistanceToDestination < 1f))
					{
						this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.ShyAnim].weight, 0f, Time.deltaTime);
					}
					else
					{
						this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.ShyAnim].weight, 1f, Time.deltaTime);
					}
				}
				else if (!this.Headache)
				{
					this.CharacterAnimation[this.ShyAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.ShyAnim].weight, 0f, Time.deltaTime);
				}
			}
			if (!this.BoobsResized)
			{
				this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
				if (!this.Cosmetic.CustomEyes)
				{
					this.RightBreast.gameObject.name = "RightBreastRENAMED";
					this.LeftBreast.gameObject.name = "LeftBreastRENAMED";
				}
				this.BoobsResized = true;
			}
			if (this.Following)
			{
				if (this.Gush)
				{
					this.Neck.LookAt(this.GushTarget);
				}
				else
				{
					this.Neck.LookAt(this.DefaultTarget);
				}
			}
			if (this.StudentManager.Egg && this.SecurityCamera.activeInHierarchy)
			{
				this.Head.localScale = new Vector3(0f, 0f, 0f);
			}
			if (this.Club == ClubType.Bully)
			{
				for (int i = 0; i < 4; i++)
				{
					if (this.Skirt[i] != null)
					{
						Transform transform = this.Skirt[i].transform;
						transform.localScale = new Vector3(transform.localScale.x, 0.6666667f, transform.localScale.z);
					}
				}
			}
			if (this.LongHair[0] != null)
			{
				this.LongHair[0].eulerAngles = new Vector3(this.Spine.eulerAngles.x, this.Spine.eulerAngles.y, this.Spine.eulerAngles.z);
				this.LongHair[0].RotateAround(this.LongHair[0].position, base.transform.right, 180f);
				this.LongHair[1].eulerAngles = new Vector3(this.Spine.eulerAngles.x, this.Spine.eulerAngles.y, this.Spine.eulerAngles.z);
				this.LongHair[1].RotateAround(this.LongHair[1].position, base.transform.right, 180f);
			}
		}
		if (this.Routine && !this.InEvent && !this.Meeting && !this.GoAway)
		{
			if ((this.DistanceToDestination < this.TargetDistance && this.Actions[this.Phase] == StudentActionType.SitAndSocialize) || (this.DistanceToDestination < this.TargetDistance && this.StudentID != 36 && this.Actions[this.Phase] == StudentActionType.Meeting) || (this.DistanceToDestination < this.TargetDistance && this.Actions[this.Phase] == StudentActionType.Sleuth && this.StudentManager.SleuthPhase != 2 && this.StudentManager.SleuthPhase != 3) || (this.Club == ClubType.Photography && this.ClubActivity))
			{
				if (this.CharacterAnimation[this.SocialSitAnim].weight != 1f)
				{
					this.CharacterAnimation[this.SocialSitAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.SocialSitAnim].weight, 1f, Time.deltaTime * 10f);
					if ((double)this.CharacterAnimation[this.SocialSitAnim].weight > 0.99)
					{
						this.CharacterAnimation[this.SocialSitAnim].weight = 1f;
					}
				}
			}
			else if (this.CharacterAnimation[this.SocialSitAnim].weight != 0f)
			{
				this.CharacterAnimation[this.SocialSitAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.SocialSitAnim].weight, 0f, Time.deltaTime * 10f);
				if ((double)this.CharacterAnimation[this.SocialSitAnim].weight < 0.01)
				{
					this.CharacterAnimation[this.SocialSitAnim].weight = 0f;
				}
			}
		}
		else if (this.CharacterAnimation[this.SocialSitAnim].weight != 0f)
		{
			this.CharacterAnimation[this.SocialSitAnim].weight = Mathf.Lerp(this.CharacterAnimation[this.SocialSitAnim].weight, 0f, Time.deltaTime * 10f);
			if ((double)this.CharacterAnimation[this.SocialSitAnim].weight < 0.01)
			{
				this.CharacterAnimation[this.SocialSitAnim].weight = 0f;
			}
		}
		if (this.DK)
		{
			this.Arm[0].localScale = new Vector3(2f, 2f, 2f);
			this.Arm[1].localScale = new Vector3(2f, 2f, 2f);
			this.Head.localScale = new Vector3(2f, 2f, 2f);
		}
		if (this.Fate > 0 && this.Clock.HourTime > this.TimeOfDeath)
		{
			this.Yandere.TargetStudent = this;
			this.StudentManager.Shinigami.Effect = this.Fate - 1;
			this.StudentManager.Shinigami.Attack();
			this.Yandere.TargetStudent = null;
			this.Fate = 0;
		}
		if (this.Yandere.BlackHole && this.DistanceToPlayer < 2.5f)
		{
			if (this.DeathScream != null)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.DeathScream, base.transform.position + Vector3.up, Quaternion.identity);
			}
			this.BlackHoleEffect[0].enabled = true;
			this.BlackHoleEffect[1].enabled = true;
			this.BlackHoleEffect[2].enabled = true;
			this.DeathType = DeathType.EasterEgg;
			this.CharacterAnimation.Stop();
			this.Suck.enabled = true;
			this.BecomeRagdoll();
			this.Dying = true;
		}
		if (this.CameraReacting && (this.StudentManager.NEStairs.bounds.Contains(base.transform.position) || this.StudentManager.NWStairs.bounds.Contains(base.transform.position) || this.StudentManager.SEStairs.bounds.Contains(base.transform.position) || this.StudentManager.SWStairs.bounds.Contains(base.transform.position)))
		{
			this.Spine.LookAt(this.Yandere.Spine[0]);
			this.Head.LookAt(this.Yandere.Head);
		}
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x0017C178 File Offset: 0x0017A578
	public void CalculateReputationPenalty()
	{
		if ((this.Male && PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 2) || PlayerGlobals.Seduction + PlayerGlobals.SeductionBonus > 4)
		{
			this.RepDeduction += this.RepLoss * 0.2f;
		}
		if (PlayerGlobals.Reputation < -33.33333f)
		{
			this.RepDeduction += this.RepLoss * 0.2f;
		}
		if (PlayerGlobals.Reputation > 33.33333f)
		{
			this.RepDeduction -= this.RepLoss * 0.2f;
		}
		if (PlayerGlobals.GetStudentFriend(this.StudentID))
		{
			this.RepDeduction += this.RepLoss * 0.2f;
		}
		if (PlayerGlobals.PantiesEquipped == 1)
		{
			this.RepDeduction += this.RepLoss * 0.2f;
		}
		if (PlayerGlobals.SocialBonus > 0)
		{
			this.RepDeduction += this.RepLoss * 0.2f;
		}
		this.ChameleonCheck();
		if (this.Chameleon)
		{
			Debug.Log("Chopping reputation loss in half!");
			this.RepLoss *= 0.5f;
		}
		if (this.Yandere.Persona == YanderePersonaType.Aggressive)
		{
			this.RepLoss *= 2f;
		}
		if (this.Club == ClubType.Bully)
		{
			this.RepLoss *= 2f;
		}
		if (this.Club == ClubType.Delinquent)
		{
			this.RepDeduction = 0f;
			this.RepLoss = 0f;
		}
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x0017C320 File Offset: 0x0017A720
	public void MoveTowardsTarget(Vector3 target)
	{
		if (Time.timeScale > 0.0001f && this.MyController.enabled)
		{
			Vector3 a = target - base.transform.position;
			float sqrMagnitude = a.sqrMagnitude;
			if (sqrMagnitude > 1E-06f)
			{
				this.MyController.Move(a * (Time.deltaTime * 5f / Time.timeScale));
			}
		}
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x0017C394 File Offset: 0x0017A794
	private void LookTowardsTarget(Vector3 target)
	{
		if (Time.timeScale > 0.0001f)
		{
		}
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x0017C3A8 File Offset: 0x0017A7A8
	public void AttackReaction()
	{
		Debug.Log(this.Name + " is being attacked.");
		if (this.HorudaCollider != null)
		{
			this.HorudaCollider.gameObject.SetActive(false);
		}
		if (this.PhotoEvidence)
		{
			this.SmartPhone.GetComponent<SmartphoneScript>().enabled = true;
			this.SmartPhone.GetComponent<PromptScript>().enabled = true;
			this.SmartPhone.GetComponent<Rigidbody>().useGravity = true;
			this.SmartPhone.GetComponent<Collider>().enabled = true;
			this.SmartPhone.transform.parent = null;
			this.SmartPhone.layer = 15;
		}
		else
		{
			this.SmartPhone.SetActive(false);
		}
		if (!this.WitnessedMurder)
		{
			float f = Vector3.Angle(-base.transform.forward, this.Yandere.transform.position - base.transform.position);
			this.Yandere.AttackManager.Stealth = (Mathf.Abs(f) <= 45f);
		}
		if (this.ReturningMisplacedWeapon)
		{
			Debug.Log(this.Name + " was in the process of returning a weapon when they were attacked.");
			this.DropMisplacedWeapon();
		}
		if (this.BloodPool != null)
		{
			Debug.Log(this.Name + "'s BloodPool was not null.");
			if (this.BloodPool.GetComponent<WeaponScript>() != null && this.BloodPool.GetComponent<WeaponScript>().Returner == this)
			{
				this.BloodPool.GetComponent<WeaponScript>().Returner = null;
				this.BloodPool.GetComponent<WeaponScript>().Drop();
				this.BloodPool.GetComponent<WeaponScript>().enabled = true;
				this.BloodPool = null;
			}
		}
		if (!this.Male)
		{
			if (this.Club != ClubType.Council)
			{
				this.StudentManager.TranqDetector.TranqCheck();
			}
			this.CharacterAnimation["f02_smile_00"].weight = 0f;
			this.SmartPhone.SetActive(false);
			this.CharacterAnimation[this.ShyAnim].weight = 0f;
			this.Shy = false;
		}
		this.WitnessCamera.Show = false;
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		this.Yandere.CharacterAnimation["f02_idleShort_00"].time = 0f;
		this.Yandere.CharacterAnimation["f02_swingA_00"].time = 0f;
		this.Yandere.HipCollider.enabled = true;
		this.Yandere.TargetStudent = this;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.YandereVision = false;
		this.Yandere.Attacking = true;
		this.Yandere.CanMove = false;
		if (this.Yandere.Equipped > 0 && this.Yandere.EquippedWeapon.AnimID == 2)
		{
			this.Yandere.CharacterAnimation[this.Yandere.ArmedAnims[2]].weight = 0f;
		}
		if (this.DetectionMarker != null)
		{
			this.DetectionMarker.Tex.enabled = false;
		}
		this.EmptyHands();
		this.DropPlate();
		this.MyController.radius = 0f;
		if (this.Persona == PersonaType.PhoneAddict)
		{
			this.Countdown.gameObject.SetActive(false);
			this.ChaseCamera.SetActive(false);
			if (this.StudentManager.ChaseCamera == this.ChaseCamera)
			{
				this.StudentManager.ChaseCamera = null;
			}
		}
		this.VomitEmitter.gameObject.SetActive(false);
		this.InvestigatingBloodPool = false;
		this.Investigating = false;
		this.Pen.SetActive(false);
		this.EatingSnack = false;
		this.SpeechLines.Stop();
		this.Attacked = true;
		this.Alarmed = false;
		this.Fleeing = false;
		this.Routine = false;
		this.ReadPhase = 0;
		this.Dying = true;
		this.Wet = false;
		this.Prompt.Hide();
		this.Prompt.enabled = false;
		if (this.Following)
		{
			Debug.Log("Yandere-chan's follower is being set to ''null''.");
			this.Hearts.emission.enabled = false;
			this.Yandere.Follower = null;
			this.Yandere.Followers--;
			this.Following = false;
		}
		if (this.Distracting)
		{
			this.DistractionTarget.TargetedForDistraction = false;
			this.DistractionTarget.Octodog.SetActive(false);
			this.DistractionTarget.Distracted = false;
			this.Distracting = false;
		}
		if (this.Teacher)
		{
			if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && this.Yandere.EquippedWeapon.Type == WeaponType.Knife)
			{
				Debug.Log(this.Name + " has called the ''BeginStruggle'' function.");
				this.Pathfinding.target = this.Yandere.transform;
				this.CurrentDestination = this.Yandere.transform;
				this.Yandere.Attacking = false;
				this.Attacked = false;
				this.Fleeing = true;
				this.Dying = false;
				this.Persona = PersonaType.Heroic;
				this.BeginStruggle();
			}
			else
			{
				this.Yandere.HeartRate.gameObject.SetActive(false);
				this.Yandere.ShoulderCamera.Counter = true;
				this.Yandere.ShoulderCamera.OverShoulder = false;
				this.Yandere.RPGCamera.enabled = false;
				this.Yandere.Senpai = base.transform;
				this.Yandere.Attacking = true;
				this.Yandere.CanMove = false;
				this.Yandere.Talking = false;
				this.Yandere.Noticed = true;
				this.Yandere.HUD.alpha = 0f;
			}
		}
		else if (this.Strength == 9)
		{
			if (!this.WitnessedMurder)
			{
				this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 3, 11f);
			}
			this.Yandere.CharacterAnimation.CrossFade("f02_moCounterA_00");
			this.Yandere.HeartRate.gameObject.SetActive(false);
			this.Yandere.ShoulderCamera.ObstacleCounter = true;
			this.Yandere.ShoulderCamera.OverShoulder = false;
			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.Senpai = base.transform;
			this.Yandere.Attacking = true;
			this.Yandere.CanMove = false;
			this.Yandere.Talking = false;
			this.Yandere.Noticed = true;
			this.Yandere.HUD.alpha = 0f;
		}
		else
		{
			if (!this.Yandere.AttackManager.Stealth)
			{
				this.Subtitle.UpdateLabel(SubtitleType.Dying, 0, 1f);
				this.SpawnAlarmDisc();
			}
			if (this.Yandere.SanityBased)
			{
				this.Yandere.AttackManager.Attack(this.Character, this.Yandere.EquippedWeapon);
			}
		}
		if (this.StudentManager.Reporter == this)
		{
			this.StudentManager.Reporter = null;
			if (this.ReportPhase == 0)
			{
				Debug.Log("A reporter died before being able to report a corpse. Corpse position reset.");
				this.StudentManager.CorpseLocation.position = Vector3.zero;
			}
		}
		if (this.Club == ClubType.Delinquent && this.MyWeapon != null && this.MyWeapon.transform.parent == this.ItemParent)
		{
			this.MyWeapon.transform.parent = null;
			this.MyWeapon.MyCollider.enabled = true;
			this.MyWeapon.Prompt.enabled = true;
			Rigidbody component = this.MyWeapon.GetComponent<Rigidbody>();
			component.constraints = RigidbodyConstraints.None;
			component.isKinematic = false;
			component.useGravity = true;
		}
		if (this.PhotoEvidence)
		{
			this.CameraFlash.SetActive(false);
			this.SmartPhone.SetActive(true);
		}
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x0017CC1C File Offset: 0x0017B01C
	public void DropPlate()
	{
		if (this.MyPlate != null)
		{
			if (this.MyPlate.parent == this.RightHand)
			{
				this.MyPlate.GetComponent<Rigidbody>().isKinematic = false;
				this.MyPlate.GetComponent<Rigidbody>().useGravity = true;
				this.MyPlate.GetComponent<Collider>().enabled = true;
				this.MyPlate.parent = null;
				this.MyPlate.gameObject.SetActive(true);
			}
			if (this.Distracting)
			{
				this.DistractionTarget.TargetedForDistraction = false;
				this.Distracting = false;
				this.IdleAnim = this.OriginalIdleAnim;
				this.WalkAnim = this.OriginalWalkAnim;
			}
		}
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x0017CCDC File Offset: 0x0017B0DC
	public void SenpaiNoticed()
	{
		Debug.Log("The ''SenpaiNoticed'' function has been called.");
		if (this.Yandere.Shutter.Snapping)
		{
			this.Yandere.Shutter.ResumeGameplay();
			this.Yandere.StopAiming();
		}
		this.Yandere.Noticed = true;
		if (this.WeaponToTakeAway != null && this.Teacher && !this.Yandere.Attacking)
		{
			Debug.Log("Taking away Yandere-chan's weapon.");
			this.Yandere.EquippedWeapon.Drop();
			this.WeaponToTakeAway.transform.position = this.StudentManager.WeaponBoxSpot.parent.position + new Vector3(0f, 1f, 0f);
			this.WeaponToTakeAway.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		}
		this.WeaponToTakeAway = null;
		if (!this.Yandere.Attacking)
		{
			this.Yandere.EmptyHands();
		}
		this.Yandere.Senpai = base.transform;
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}
		this.Yandere.DetectionPanel.alpha = 0f;
		this.Yandere.RPGCamera.mouseSpeed = 0f;
		this.Yandere.LaughIntensity = 0f;
		this.Yandere.HUD.alpha = 0f;
		this.Yandere.EyeShrink = 0f;
		this.Yandere.Sanity = 100f;
		this.Yandere.ProgressBar.transform.parent.gameObject.SetActive(false);
		this.Yandere.HeartRate.gameObject.SetActive(false);
		this.Yandere.Stance.Current = StanceType.Standing;
		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.DelinquentFighting = false;
		this.Yandere.YandereVision = false;
		this.Yandere.CannotRecover = true;
		this.Yandere.Police.Show = false;
		this.Yandere.Poisoning = false;
		this.Yandere.Rummaging = false;
		this.Yandere.Laughing = false;
		this.Yandere.CanMove = false;
		this.Yandere.Dipping = false;
		this.Yandere.Mopping = false;
		this.Yandere.Talking = false;
		this.Yandere.Jukebox.GameOver();
		this.StudentManager.StopMoving();
		if (this.Teacher || this.StudentID == 1)
		{
			if (this.Club != ClubType.Council)
			{
				this.IdleAnim = this.OriginalIdleAnim;
			}
			this.AlarmTimer = 0f;
			base.enabled = true;
			this.Stop = false;
		}
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x0017CFEC File Offset: 0x0017B3EC
	private void WitnessMurder()
	{
		Debug.Log(this.Name + " just realized that Yandere-chan is responsible for a murder!");
		this.RespectEarned = false;
		if ((this.Fleeing && this.WitnessedBloodPool) || this.ReportPhase == 2)
		{
			this.WitnessedBloodyWeapon = false;
			this.WitnessedBloodPool = false;
			this.WitnessedSomething = false;
			this.WitnessedWeapon = false;
			this.WitnessedLimb = false;
			this.Fleeing = false;
			this.ReportPhase = 0;
		}
		this.CharacterAnimation[this.ScaredAnim].time = 0f;
		this.CameraFlash.SetActive(false);
		if (!this.Male)
		{
			this.CharacterAnimation["f02_smile_00"].weight = 0f;
			this.WateringCan.SetActive(false);
		}
		if (this.MyPlate != null && this.MyPlate.parent == this.RightHand)
		{
			this.MyPlate.GetComponent<Rigidbody>().isKinematic = false;
			this.MyPlate.GetComponent<Rigidbody>().useGravity = true;
			this.MyPlate.GetComponent<Collider>().enabled = true;
			this.MyPlate.parent = null;
		}
		this.EmptyHands();
		this.MurdersWitnessed++;
		this.SpeechLines.Stop();
		this.WitnessedBloodyWeapon = false;
		this.WitnessedBloodPool = false;
		this.WitnessedSomething = false;
		this.WitnessedWeapon = false;
		this.WitnessedLimb = false;
		if (this.ReturningMisplacedWeapon)
		{
			this.DropMisplacedWeapon();
		}
		this.ReturningMisplacedWeapon = false;
		this.InvestigatingBloodPool = false;
		this.CameraReacting = false;
		this.WitnessedMurder = true;
		this.Investigating = false;
		this.Distracting = false;
		this.EatingSnack = false;
		this.Threatened = false;
		this.Distracted = false;
		this.Reacted = false;
		this.Routine = false;
		this.Alarmed = true;
		this.NoTalk = false;
		this.Wet = false;
		if (this.OriginalPersona != PersonaType.Violent && this.Persona != this.OriginalPersona)
		{
			Debug.Log(this.Name + " is reverting back into their original Persona.");
			this.Persona = this.OriginalPersona;
			this.SwitchBack = false;
			if (this.Persona == PersonaType.Heroic || this.Persona == PersonaType.Dangerous)
			{
				this.PersonaReaction();
			}
		}
		if (this.Persona == PersonaType.Spiteful && this.Yandere.TargetStudent != null)
		{
			Debug.Log("A Spiteful student witnessed a murder.");
			if ((this.Bullied && this.Yandere.TargetStudent.Club == ClubType.Bully) || this.Yandere.TargetStudent.Bullied)
			{
				this.ScaredAnim = this.EvilWitnessAnim;
				this.Persona = PersonaType.Evil;
			}
		}
		if (this.Club == ClubType.Delinquent)
		{
			Debug.Log("A Delinquent witnessed a murder.");
			if (this.Yandere.TargetStudent != null && this.Yandere.TargetStudent.Club == ClubType.Bully)
			{
				this.ScaredAnim = this.EvilWitnessAnim;
				this.Persona = PersonaType.Evil;
			}
			if ((this.Yandere.Lifting || this.Yandere.Carrying || this.Yandere.Dragging) && this.Yandere.CurrentRagdoll.Student.Club == ClubType.Bully)
			{
				this.ScaredAnim = this.EvilWitnessAnim;
				this.Persona = PersonaType.Evil;
			}
		}
		if (this.Persona == PersonaType.Sleuth)
		{
			Debug.Log("A Sleuth is witnessing a murder.");
			if (this.Yandere.Attacking || this.Yandere.Struggling || this.Yandere.Carrying || this.Yandere.Lifting || (this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart))
			{
				if (!this.Sleuthing)
				{
					Debug.Log("A Sleuth is changing their Persona.");
					if (this.StudentID == 56)
					{
						this.Persona = PersonaType.Heroic;
					}
					else
					{
						this.Persona = PersonaType.SocialButterfly;
					}
				}
				else
				{
					this.Persona = PersonaType.SocialButterfly;
				}
			}
		}
		if (this.StudentID > 1 || this.Yandere.Mask != null)
		{
			this.ID = 0;
			while (this.ID < this.Outlines.Length)
			{
				this.Outlines[this.ID].color = new Color(1f, 0f, 0f, 1f);
				this.Outlines[this.ID].enabled = true;
				this.ID++;
			}
			this.WitnessCamera.transform.parent = this.WitnessPOV;
			this.WitnessCamera.transform.localPosition = Vector3.zero;
			this.WitnessCamera.transform.localEulerAngles = Vector3.zero;
			this.WitnessCamera.MyCamera.enabled = true;
			this.WitnessCamera.Show = true;
			this.CameraEffects.MurderWitnessed();
			this.Witnessed = StudentWitnessType.Murder;
			if (this.Persona != PersonaType.Evil)
			{
				this.Police.Witnesses++;
			}
			if (this.Teacher)
			{
				this.StudentManager.Reporter = this;
			}
			if (this.Talking)
			{
				this.DialogueWheel.End();
				this.Hearts.emission.enabled = false;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.Obstacle.enabled = false;
				this.Talk.enabled = false;
				this.Talking = false;
				this.Waiting = false;
				this.StudentManager.EnablePrompts();
			}
			if (this.Prompt.Label[0] != null && !GameGlobals.EmptyDemon)
			{
				this.Prompt.Label[0].text = "     Talk";
				this.Prompt.HideButton[0] = true;
			}
		}
		else
		{
			if (!this.Yandere.Attacking)
			{
				this.SenpaiNoticed();
			}
			this.Fleeing = false;
			this.EyeShrink = 0f;
			this.Yandere.Noticed = true;
			this.Yandere.Talking = false;
			this.CameraEffects.MurderWitnessed();
			this.Yandere.ShoulderCamera.OverShoulder = false;
			this.CharacterAnimation.CrossFade(this.ScaredAnim);
			this.CharacterAnimation["scaredFace_00"].weight = 1f;
			if (this.StudentID == 1)
			{
				Debug.Log("Senpai entered his scared animation.");
			}
		}
		if (this.Persona == PersonaType.TeachersPet && this.StudentManager.Reporter == null && !this.Police.Called)
		{
			this.StudentManager.CorpseLocation.position = this.Yandere.transform.position;
			this.StudentManager.LowerCorpsePosition();
			this.StudentManager.Reporter = this;
			this.ReportingMurder = true;
		}
		if (this.Following)
		{
			this.Hearts.emission.enabled = false;
			this.Yandere.Followers--;
			this.Following = false;
		}
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		if (this.Persona == PersonaType.PhoneAddict || this.Sleuthing)
		{
			this.SmartPhone.SetActive(true);
		}
		if (this.SmartPhone.activeInHierarchy)
		{
			if (this.Persona != PersonaType.Heroic && this.Persona != PersonaType.Dangerous && this.Persona != PersonaType.Evil && this.Persona != PersonaType.Violent && this.Persona != PersonaType.Coward && !this.Teacher)
			{
				this.Persona = PersonaType.PhoneAddict;
				if (!this.Sleuthing)
				{
					this.SprintAnim = this.PhoneAnims[2];
				}
				else
				{
					this.SprintAnim = this.SleuthReportAnim;
				}
			}
			else
			{
				this.SmartPhone.SetActive(false);
			}
		}
		this.StopPairing();
		if (this.Persona != PersonaType.Heroic)
		{
			this.AlarmTimer = 0f;
			this.Alarm = 0f;
		}
		if (this.Teacher)
		{
			if (!this.Yandere.Chased)
			{
				Debug.Log("A teacher has reached ChaseYandere through WitnessMurder.");
				this.ChaseYandere();
			}
		}
		else
		{
			this.SpawnAlarmDisc();
		}
		if (!this.PinDownWitness && this.Persona != PersonaType.Evil)
		{
			this.StudentManager.Witnesses++;
			this.StudentManager.WitnessList[this.StudentManager.Witnesses] = this;
			this.StudentManager.PinDownCheck();
			this.PinDownWitness = true;
		}
		if (this.Persona == PersonaType.Violent)
		{
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
		}
		if (this.Yandere.Mask == null)
		{
			this.SawMask = false;
			if (this.Persona != PersonaType.Evil)
			{
				this.Grudge = true;
			}
		}
		else
		{
			this.SawMask = true;
		}
		this.StudentManager.UpdateMe(this.StudentID);
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x0017D96C File Offset: 0x0017BD6C
	public void DropMisplacedWeapon()
	{
		this.WitnessedWeapon = false;
		this.InvestigatingBloodPool = false;
		this.ReturningMisplacedWeaponPhase = 0;
		this.ReturningMisplacedWeapon = false;
		this.BloodPool.GetComponent<WeaponScript>().Returner = null;
		this.BloodPool.GetComponent<WeaponScript>().Drop();
		this.BloodPool.GetComponent<WeaponScript>().enabled = true;
		this.BloodPool = null;
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x0017D9D0 File Offset: 0x0017BDD0
	private void ChaseYandere()
	{
		Debug.Log("A character has begun to chase Yandere-chan.");
		this.CurrentDestination = this.Yandere.transform;
		this.Pathfinding.target = this.Yandere.transform;
		this.Pathfinding.speed = 5f;
		this.StudentManager.Portal.SetActive(false);
		if (this.Yandere.Pursuer == null)
		{
			this.Yandere.Pursuer = this;
		}
		this.TargetDistance = 1f;
		this.AlarmTimer = 0f;
		this.Chasing = false;
		this.Fleeing = false;
		this.StudentManager.UpdateStudents(0);
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x0017DA84 File Offset: 0x0017BE84
	private void PersonaReaction()
	{
		Debug.Log(string.Concat(new object[]
		{
			this.Name,
			" has started calling PersonaReaction(). As of now, they are a: ",
			this.Persona,
			"."
		}));
		if (this.Persona == PersonaType.Sleuth)
		{
			if (this.Sleuthing)
			{
				this.Persona = PersonaType.PhoneAddict;
				this.SmartPhone.SetActive(true);
			}
			else
			{
				this.Persona = PersonaType.SocialButterfly;
			}
		}
		if (!this.Indoors && this.WitnessedMurder && this.StudentID != this.StudentManager.RivalID)
		{
			Debug.Log(this.Name + "'s current Persona is: " + this.Persona);
			if ((this.Persona != PersonaType.Evil && this.Persona != PersonaType.Heroic && this.Persona != PersonaType.Coward && this.Persona != PersonaType.PhoneAddict && this.Persona != PersonaType.Protective && this.Persona != PersonaType.Violent) || this.Injured)
			{
				Debug.Log(this.Name + " is switching to the Loner Persona.");
				this.Persona = PersonaType.Loner;
			}
		}
		if (!this.WitnessedMurder)
		{
			if (this.Persona == PersonaType.Heroic)
			{
				this.SwitchBack = true;
				this.Persona = ((!(this.Corpse != null)) ? PersonaType.Loner : PersonaType.TeachersPet);
			}
			else if (this.Persona == PersonaType.Coward || this.Persona == PersonaType.Evil || this.Persona == PersonaType.Fragile)
			{
				this.Persona = PersonaType.Loner;
			}
		}
		if (this.Persona == PersonaType.Loner || this.Persona == PersonaType.Spiteful)
		{
			Debug.Log(this.Name + " is looking in the Loner/Spiteful section of PersonaReaction() to decide what to do next.");
			if (this.Club == ClubType.Delinquent)
			{
				Debug.Log("A delinquent turned into a loner, and now he is fleeing.");
				if (this.Injured)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentInjuredFlee, 1, 3f);
				}
				else if (this.FoundFriendCorpse)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFriendFlee, 1, 3f);
				}
				else if (this.FoundEnemyCorpse)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentEnemyFlee, 1, 3f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFlee, 1, 3f);
				}
			}
			else if (this.WitnessedMurder)
			{
				this.Subtitle.UpdateLabel(SubtitleType.LonerMurderReaction, 1, 3f);
			}
			else
			{
				this.Subtitle.UpdateLabel(SubtitleType.LonerCorpseReaction, 1, 3f);
			}
			if (this.Schoolwear > 0)
			{
				if (!this.Bloody)
				{
					this.Pathfinding.target = this.StudentManager.Exit;
					this.TargetDistance = 0f;
					this.Routine = false;
					this.Fleeing = true;
				}
				else
				{
					this.FleeWhenClean = true;
					this.TargetDistance = 1f;
					this.BatheFast = true;
				}
			}
			else
			{
				this.FleeWhenClean = true;
				if (!this.Bloody)
				{
					this.BathePhase = 5;
					this.GoChange();
				}
				else
				{
					this.CurrentDestination = this.StudentManager.FastBatheSpot;
					this.Pathfinding.target = this.StudentManager.FastBatheSpot;
					this.TargetDistance = 1f;
					this.BatheFast = true;
				}
			}
			if (this.Corpse.StudentID == this.StudentManager.RivalID)
			{
				this.CurrentDestination = this.Corpse.Student.Hips;
				this.Pathfinding.target = this.Corpse.Student.Hips;
				this.SenpaiWitnessingRivalDie = true;
				this.DistanceToDestination = 1f;
				this.WitnessRivalDiePhase = 3;
				this.Routine = false;
				this.TargetDistance = 0.5f;
			}
		}
		else if (this.Persona == PersonaType.TeachersPet)
		{
			if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
			{
				if (this.StudentManager.BloodReporter == null)
				{
					if (this.Club != ClubType.Delinquent && !this.Police.Called && !this.LostTeacherTrust)
					{
						this.StudentManager.BloodLocation.position = this.BloodPool.transform.position;
						this.StudentManager.LowerBloodPosition();
						Debug.Log(this.Name + " has become a ''blood reporter''.");
						this.StudentManager.BloodReporter = this;
						this.ReportingBlood = true;
						this.DetermineBloodLocation();
					}
					else
					{
						this.ReportingBlood = false;
					}
				}
			}
			else if (this.StudentManager.Reporter == null && !this.Police.Called)
			{
				this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;
				this.StudentManager.LowerCorpsePosition();
				Debug.Log("A student has become a ''reporter''.");
				this.StudentManager.Reporter = this;
				this.ReportingMurder = true;
				this.DetermineCorpseLocation();
			}
			if (this.StudentManager.Reporter == this)
			{
				Debug.Log(this.Name + " is running to a teacher to report murder.");
				this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
				this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;
				this.TargetDistance = 2f;
				if (this.WitnessedMurder)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetMurderReport, 1, 3f);
				}
				else if (this.WitnessedCorpse)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetCorpseReport, 1, 3f);
				}
			}
			else if (this.StudentManager.BloodReporter == this)
			{
				Debug.Log(this.Name + " is running to a teacher to report something.");
				this.DropPlate();
				this.Pathfinding.target = this.StudentManager.Teachers[this.Class].transform;
				this.CurrentDestination = this.StudentManager.Teachers[this.Class].transform;
				this.TargetDistance = 2f;
				if (this.WitnessedLimb)
				{
					this.Subtitle.UpdateLabel(SubtitleType.LimbReaction, 1, 3f);
				}
				else if (this.WitnessedBloodyWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.BloodyWeaponReaction, 1, 3f);
				}
				else if (this.WitnessedBloodPool)
				{
					this.Subtitle.UpdateLabel(SubtitleType.BloodPoolReaction, 1, 3f);
				}
				else if (this.WitnessedWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReport, 1, 3f);
				}
			}
			else
			{
				if (this.Club == ClubType.Council)
				{
					if (this.WitnessedCorpse)
					{
						if (this.StudentManager.CorpseLocation.position == Vector3.zero)
						{
							this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;
							this.AssignCorpseGuardLocations();
						}
						if (this.StudentID == 86)
						{
							this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[1];
						}
						else if (this.StudentID == 87)
						{
							this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[2];
						}
						else if (this.StudentID == 88)
						{
							this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[3];
						}
						else if (this.StudentID == 89)
						{
							this.Pathfinding.target = this.StudentManager.CorpseGuardLocation[4];
						}
						this.CurrentDestination = this.Pathfinding.target;
					}
					else
					{
						Debug.Log("A student council member is being told to travel to ''BloodGuardLocation''.");
						if (this.StudentManager.BloodLocation.position == Vector3.zero)
						{
							this.StudentManager.BloodLocation.position = this.BloodPool.transform.position;
							this.AssignBloodGuardLocations();
						}
						if (this.StudentManager.BloodGuardLocation[1].position == Vector3.zero)
						{
							this.AssignBloodGuardLocations();
						}
						if (this.StudentID == 86)
						{
							this.Pathfinding.target = this.StudentManager.BloodGuardLocation[1];
						}
						else if (this.StudentID == 87)
						{
							this.Pathfinding.target = this.StudentManager.BloodGuardLocation[2];
						}
						else if (this.StudentID == 88)
						{
							this.Pathfinding.target = this.StudentManager.BloodGuardLocation[3];
						}
						else if (this.StudentID == 89)
						{
							this.Pathfinding.target = this.StudentManager.BloodGuardLocation[4];
						}
						this.CurrentDestination = this.Pathfinding.target;
						this.Guarding = true;
					}
				}
				else
				{
					this.PetDestination = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, this.Seat.position + this.Seat.forward * -0.5f, Quaternion.identity).transform;
					this.Pathfinding.target = this.PetDestination;
					this.CurrentDestination = this.PetDestination;
					this.Distracting = false;
					this.DropPlate();
				}
				if (this.WitnessedMurder)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetMurderReaction, 1, 3f);
				}
				else if (this.WitnessedCorpse)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetCorpseReaction, 1, 3f);
				}
				else if (this.WitnessedLimb)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetLimbReaction, 1, 3f);
				}
				else if (this.WitnessedBloodyWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetBloodyWeaponReaction, 1, 3f);
				}
				else if (this.WitnessedBloodPool)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetBloodReaction, 1, 3f);
				}
				else if (this.WitnessedWeapon)
				{
					this.Subtitle.UpdateLabel(SubtitleType.PetWeaponReaction, 1, 3f);
				}
				this.TargetDistance = 1f;
				this.ReportingMurder = false;
				this.ReportingBlood = false;
			}
			this.Routine = false;
			this.Fleeing = true;
		}
		else if (this.Persona == PersonaType.Heroic || this.Persona == PersonaType.Protective)
		{
			if (!this.Yandere.Chased)
			{
				this.StudentManager.PinDownCheck();
				if (!this.StudentManager.PinningDown)
				{
					Debug.Log(this.Name + "'s ''Flee'' was set to ''true'' because Hero persona reaction was called.");
					if (this.Persona == PersonaType.Protective)
					{
						this.Subtitle.UpdateLabel(SubtitleType.ObstacleMurderReaction, 2, 3f);
					}
					else if (this.Persona != PersonaType.Violent)
					{
						this.Subtitle.UpdateLabel(SubtitleType.HeroMurderReaction, 3, 3f);
					}
					else if (this.Defeats > 0)
					{
						this.Subtitle.Speaker = this;
						this.Subtitle.UpdateLabel(SubtitleType.DelinquentResume, 3, 3f);
					}
					else
					{
						this.Subtitle.Speaker = this;
						this.Subtitle.UpdateLabel(SubtitleType.DelinquentMurderReaction, 3, 3f);
					}
					this.Pathfinding.target = this.Yandere.transform;
					this.Pathfinding.speed = 5f;
					this.StudentManager.Portal.SetActive(false);
					this.Yandere.Pursuer = this;
					this.Yandere.Chased = true;
					this.TargetDistance = 1f;
					this.StudentManager.UpdateStudents(0);
					this.Routine = false;
					this.Fleeing = true;
				}
			}
		}
		else if (this.Persona == PersonaType.Coward || this.Persona == PersonaType.Fragile)
		{
			Debug.Log("This character just set their destination to themself.");
			this.CurrentDestination = base.transform;
			this.Pathfinding.target = base.transform;
			this.Subtitle.UpdateLabel(SubtitleType.CowardMurderReaction, 1, 5f);
			this.Routine = false;
			this.Fleeing = true;
		}
		else if (this.Persona == PersonaType.Evil)
		{
			Debug.Log("This character just set their destination to themself.");
			this.CurrentDestination = base.transform;
			this.Pathfinding.target = base.transform;
			this.Subtitle.UpdateLabel(SubtitleType.EvilMurderReaction, 1, 5f);
			this.Routine = false;
			this.Fleeing = true;
		}
		else if (this.Persona == PersonaType.SocialButterfly)
		{
			this.StudentManager.HidingSpots.List[this.StudentID].position = this.StudentManager.PopulationManager.GetCrowdedLocation();
			this.CurrentDestination = this.StudentManager.HidingSpots.List[this.StudentID];
			this.Pathfinding.target = this.StudentManager.HidingSpots.List[this.StudentID];
			this.Subtitle.UpdateLabel(SubtitleType.SocialDeathReaction, 1, 5f);
			this.TargetDistance = 2f;
			this.ReportPhase = 1;
			this.Routine = false;
			this.Fleeing = true;
			this.Halt = true;
		}
		else if (this.Persona == PersonaType.Dangerous)
		{
			if (this.WitnessedMurder)
			{
				if (this.StudentID == 86)
				{
					this.Subtitle.UpdateLabel(SubtitleType.Chasing, 1, 5f);
				}
				else if (this.StudentID == 87)
				{
					this.Subtitle.UpdateLabel(SubtitleType.Chasing, 2, 5f);
				}
				else if (this.StudentID == 88)
				{
					this.Subtitle.UpdateLabel(SubtitleType.Chasing, 3, 5f);
				}
				else if (this.StudentID == 89)
				{
					this.Subtitle.UpdateLabel(SubtitleType.Chasing, 4, 5f);
				}
				this.Pathfinding.target = this.Yandere.transform;
				this.Pathfinding.speed = 5f;
				this.StudentManager.Portal.SetActive(false);
				this.Yandere.Chased = true;
				this.TargetDistance = 1f;
				this.StudentManager.UpdateStudents(0);
				Debug.Log("Sprinting 10");
				this.Routine = false;
				this.Fleeing = true;
				this.Halt = true;
			}
			else
			{
				Debug.Log("A student council member has transformed into a Teacher's Pet.");
				this.Persona = PersonaType.TeachersPet;
				this.PersonaReaction();
			}
		}
		else if (this.Persona == PersonaType.PhoneAddict)
		{
			Debug.Log(this.Name + " is executing the Phone Addict Persona code.");
			this.CurrentDestination = this.StudentManager.Exit;
			this.Pathfinding.target = this.StudentManager.Exit;
			this.Countdown.gameObject.SetActive(true);
			this.Routine = false;
			this.Fleeing = true;
			if (this.StudentManager.ChaseCamera == null)
			{
				this.StudentManager.ChaseCamera = this.ChaseCamera;
				this.ChaseCamera.SetActive(true);
			}
		}
		else if (this.Persona == PersonaType.Violent)
		{
			Debug.Log(this.Name + ", a delinquent, is currently in the ''Violent'' part of PersonaReaction()");
			if (this.WitnessedMurder)
			{
				if (!this.Yandere.Chased)
				{
					this.StudentManager.PinDownCheck();
					if (!this.StudentManager.PinningDown)
					{
						Debug.Log(this.Name + " began fleeing because Violent persona reaction was called.");
						if (this.Defeats > 0)
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentResume, 3, 3f);
						}
						else
						{
							this.Subtitle.Speaker = this;
							this.Subtitle.UpdateLabel(SubtitleType.DelinquentMurderReaction, 3, 3f);
						}
						this.Pathfinding.target = this.Yandere.transform;
						this.Pathfinding.canSearch = true;
						this.Pathfinding.canMove = true;
						this.Pathfinding.speed = 5f;
						this.StudentManager.Portal.SetActive(false);
						this.Yandere.Pursuer = this;
						this.Yandere.Chased = true;
						this.TargetDistance = 1f;
						this.StudentManager.UpdateStudents(0);
						this.Routine = false;
						this.Fleeing = true;
					}
				}
			}
			else
			{
				Debug.Log("A delinquent has reached the ''Flee'' protocol.");
				if (this.FoundFriendCorpse)
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFriendFlee, 1, 3f);
				}
				else
				{
					this.Subtitle.Speaker = this;
					this.Subtitle.UpdateLabel(SubtitleType.DelinquentFlee, 1, 3f);
				}
				this.Pathfinding.target = this.StudentManager.Exit;
				this.Pathfinding.canSearch = true;
				this.Pathfinding.canMove = true;
				this.TargetDistance = 0f;
				this.Routine = false;
				this.Fleeing = true;
			}
		}
		else if (this.Persona == PersonaType.Strict)
		{
			if (this.Yandere.Pursuer == this)
			{
				Debug.Log("This teacher is now pursuing Yandere-chan.");
			}
			if (this.WitnessedMurder)
			{
				if (this.Yandere.Pursuer == this)
				{
					Debug.Log("A teacher is now reacting to the sight of murder.");
					this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, 3, 3f);
					this.Pathfinding.target = this.Yandere.transform;
					this.Pathfinding.speed = 5f;
					this.StudentManager.Portal.SetActive(false);
					this.Yandere.Chased = true;
					this.TargetDistance = 1f;
					this.StudentManager.UpdateStudents(0);
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.1f, base.transform.position.z);
					this.Routine = false;
					this.Fleeing = true;
				}
				else if (!this.Yandere.Chased)
				{
					if (this.Yandere.FightHasBrokenUp)
					{
						Debug.Log("This teacher is returning to normal after witnessing a SC member break up a fight.");
						this.WitnessedMurder = false;
						this.PinDownWitness = false;
						this.Alarmed = false;
						this.Reacted = false;
						this.Routine = true;
						this.Grudge = false;
						this.AlarmTimer = 0f;
						this.PreviousEyeShrink = 0f;
						this.EyeShrink = 0f;
						this.PreviousAlarm = 0f;
						this.MurdersWitnessed = 0;
						this.Concern = 0;
						this.Witnessed = StudentWitnessType.None;
						this.GameOverCause = GameOverType.None;
						this.CurrentDestination = this.Destinations[this.Phase];
						this.Pathfinding.target = this.Destinations[this.Phase];
					}
					else
					{
						Debug.Log("A teacher has reached ChaseYandere through PersonaReaction.");
						this.ChaseYandere();
					}
				}
			}
			else if (this.WitnessedCorpse)
			{
				Debug.Log("A teacher is now reacting to the sight of a corpse.");
				if (this.ReportPhase == 0)
				{
					this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseReaction, 1, 3f);
				}
				this.Pathfinding.target = UnityEngine.Object.Instantiate<GameObject>(this.EmptyGameObject, new Vector3(this.Corpse.AllColliders[0].transform.position.x, base.transform.position.y, this.Corpse.AllColliders[0].transform.position.z), Quaternion.identity).transform;
				this.Pathfinding.target.position = Vector3.MoveTowards(this.Pathfinding.target.position, base.transform.position, 1.5f);
				this.TargetDistance = 1f;
				this.ReportPhase = 2;
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.1f, base.transform.position.z);
				this.IgnoringPettyActions = true;
				this.Routine = false;
				this.Fleeing = true;
			}
			else
			{
				Debug.Log("A teacher is now reacting to the sight of a severed limb, blood pool, or weapon.");
				if (this.ReportPhase == 0)
				{
					if (this.WitnessedBloodPool || this.WitnessedBloodyWeapon)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 3, 3f);
					}
					else if (this.WitnessedLimb)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 4, 3f);
					}
					else if (this.WitnessedWeapon)
					{
						this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseInspection, 5, 3f);
					}
				}
				this.TargetDistance = 1f;
				this.ReportPhase = 2;
				this.Routine = false;
				this.Fleeing = true;
				this.Halt = true;
			}
		}
		if (this.StudentID == 41)
		{
			this.Subtitle.UpdateLabel(SubtitleType.Impatience, 6, 5f);
		}
		Debug.Log(string.Concat(new object[]
		{
			this.Name,
			" has finished calling PersonaReaction(). As of now, they are a: ",
			this.Persona,
			"."
		}));
		this.Alarm = 0f;
		this.UpdateDetectionMarker();
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x0017F0B0 File Offset: 0x0017D4B0
	private void BeginStruggle()
	{
		Debug.Log(this.Name + " has begun a struggle with Yandere-chan.");
		if (this.Yandere.Dragging)
		{
			this.Yandere.Ragdoll.GetComponent<RagdollScript>().StopDragging();
		}
		if (this.Yandere.Armed)
		{
			this.Yandere.EquippedWeapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		}
		this.Yandere.StruggleBar.Strength = (float)this.Strength;
		this.Yandere.StruggleBar.Struggling = true;
		this.Yandere.StruggleBar.Student = this;
		this.Yandere.StruggleBar.gameObject.SetActive(true);
		this.CharacterAnimation.CrossFade(this.StruggleAnim);
		this.Yandere.ShoulderCamera.LastPosition = this.Yandere.ShoulderCamera.transform.position;
		this.Yandere.ShoulderCamera.Struggle = true;
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		this.Obstacle.enabled = true;
		this.Struggling = true;
		this.DiscCheck = false;
		this.Alarmed = false;
		this.Halt = true;
		if (!this.Teacher)
		{
			this.Yandere.CharacterAnimation["f02_struggleA_00"].time = 0f;
		}
		else
		{
			this.Yandere.CharacterAnimation["f02_teacherStruggleA_00"].time = 0f;
			base.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}
		this.Yandere.StopLaughing();
		this.Yandere.TargetStudent = this;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.YandereVision = false;
		this.Yandere.NearSenpai = false;
		this.Yandere.Struggling = true;
		this.Yandere.CanMove = false;
		if (this.Yandere.DelinquentFighting)
		{
			this.StudentManager.CombatMinigame.Stop();
		}
		this.Yandere.EmptyHands();
		this.Yandere.MyController.enabled = false;
		this.Yandere.RPGCamera.enabled = false;
		this.MyController.radius = 0f;
		this.TargetDistance = 100f;
		this.AlarmTimer = 0f;
		this.SpawnAlarmDisc();
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x0017F360 File Offset: 0x0017D760
	public void GetDestinations()
	{
		if (this.StudentID == 10)
		{
		}
		if (!this.Teacher)
		{
			this.MyLocker = this.StudentManager.LockerPositions[this.StudentID];
		}
		if (this.Slave)
		{
			foreach (ScheduleBlock scheduleBlock in this.ScheduleBlocks)
			{
				scheduleBlock.destination = "Slave";
				scheduleBlock.action = "Slave";
			}
		}
		this.ID = 1;
		while (this.ID < this.JSON.Students[this.StudentID].ScheduleBlocks.Length)
		{
			ScheduleBlock scheduleBlock2 = this.ScheduleBlocks[this.ID];
			if (scheduleBlock2.destination == "Locker")
			{
				this.Destinations[this.ID] = this.MyLocker;
			}
			else if (scheduleBlock2.destination == "Seat")
			{
				this.Destinations[this.ID] = this.Seat;
			}
			else if (scheduleBlock2.destination == "SocialSeat")
			{
				this.Destinations[this.ID] = this.StudentManager.SocialSeats[this.StudentID];
			}
			else if (scheduleBlock2.destination == "Podium")
			{
				this.Destinations[this.ID] = this.StudentManager.Podiums.List[this.Class];
			}
			else if (scheduleBlock2.destination == "Exit")
			{
				this.Destinations[this.ID] = this.StudentManager.Hangouts.List[0];
			}
			else if (scheduleBlock2.destination == "Hangout")
			{
				this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
			}
			else if (scheduleBlock2.destination == "LunchSpot")
			{
				this.Destinations[this.ID] = this.StudentManager.LunchSpots.List[this.StudentID];
			}
			else if (scheduleBlock2.destination == "Slave")
			{
				if (!this.FragileSlave)
				{
					this.Destinations[this.ID] = this.StudentManager.SlaveSpot;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.FragileSlaveSpot;
				}
			}
			else if (scheduleBlock2.destination == "Patrol")
			{
				this.Destinations[this.ID] = this.StudentManager.Patrols.List[this.StudentID].GetChild(0);
				if ((this.OriginalClub == ClubType.Gardening || this.OriginalClub == ClubType.Occult) && this.Club == ClubType.None)
				{
					Debug.Log("This student's club disbanded, so their destination has been set to ''Hangout''.");
					this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
				}
			}
			else if (scheduleBlock2.destination == "Search Patrol")
			{
				this.Destinations[this.ID] = this.StudentManager.SearchPatrols.List[this.StudentID].GetChild(0);
			}
			else if (scheduleBlock2.destination == "Graffiti")
			{
				this.Destinations[this.ID] = this.StudentManager.GraffitiSpots[this.BullyID];
			}
			else if (scheduleBlock2.destination == "Bully")
			{
				this.Destinations[this.ID] = this.StudentManager.BullySpots[this.BullyID];
			}
			else if (scheduleBlock2.destination == "Mourn")
			{
				this.Destinations[this.ID] = this.StudentManager.MournSpots[this.StudentID];
			}
			else if (scheduleBlock2.destination == "Clean")
			{
				this.Destinations[this.ID] = this.CleaningSpot.GetChild(0);
			}
			else if (scheduleBlock2.destination == "ShameSpot")
			{
				this.Destinations[this.ID] = this.StudentManager.ShameSpot;
			}
			else if (scheduleBlock2.destination == "Follow")
			{
				this.Destinations[this.ID] = this.StudentManager.Students[11].transform;
			}
			else if (scheduleBlock2.destination == "Cuddle")
			{
				if (!this.Male)
				{
					this.Destinations[this.ID] = this.StudentManager.FemaleCoupleSpot;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.MaleCoupleSpot;
				}
			}
			else if (scheduleBlock2.destination == "Peek")
			{
				if (!this.Male)
				{
					this.Destinations[this.ID] = this.StudentManager.FemaleStalkSpot;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.MaleStalkSpot;
				}
			}
			else if (scheduleBlock2.destination == "Club")
			{
				if (this.Club > ClubType.None)
				{
					if (this.Club == ClubType.Sports)
					{
						this.Destinations[this.ID] = this.StudentManager.Clubs.List[this.StudentID].GetChild(0);
					}
					else
					{
						this.Destinations[this.ID] = this.StudentManager.Clubs.List[this.StudentID];
					}
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
				}
			}
			else if (scheduleBlock2.destination == "Sulk")
			{
				this.Destinations[this.ID] = this.StudentManager.SulkSpots[this.StudentID];
			}
			else if (scheduleBlock2.destination == "Sleuth")
			{
				this.Destinations[this.ID] = this.SleuthTarget;
			}
			else if (scheduleBlock2.destination == "Stalk")
			{
				this.Destinations[this.ID] = this.StalkTarget;
			}
			else if (scheduleBlock2.destination == "Sunbathe")
			{
				this.Destinations[this.ID] = this.StudentManager.FemaleStripSpot;
			}
			else if (scheduleBlock2.destination == "Shock")
			{
				this.Destinations[this.ID] = this.StudentManager.ShockedSpots[this.StudentID - 80];
			}
			else if (scheduleBlock2.destination == "Miyuki")
			{
				this.ClubMemberID = this.StudentID - 35;
				this.Destinations[this.ID] = this.StudentManager.MiyukiSpots[this.ClubMemberID].transform;
			}
			else if (scheduleBlock2.destination == "Practice")
			{
				if (this.Club > ClubType.None)
				{
					this.Destinations[this.ID] = this.StudentManager.PracticeSpots[this.ClubMemberID];
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.Hangouts.List[this.StudentID];
				}
			}
			else if (scheduleBlock2.destination == "Lyrics")
			{
				this.Destinations[this.ID] = this.StudentManager.LyricsSpot;
			}
			else if (scheduleBlock2.destination == "Meeting")
			{
				this.Destinations[this.ID] = this.StudentManager.MeetingSpots[this.StudentID].transform;
			}
			else if (scheduleBlock2.destination == "InfirmaryBed")
			{
				if (this.StudentManager.SedatedStudents == 0)
				{
					this.Destinations[this.ID] = this.StudentManager.MaleRestSpot;
					this.StudentManager.SedatedStudents++;
				}
				else
				{
					this.Destinations[this.ID] = this.StudentManager.FemaleRestSpot;
				}
			}
			else if (scheduleBlock2.destination == "InfirmarySeat")
			{
				this.Destinations[this.ID] = this.StudentManager.InfirmarySeat;
			}
			else if (scheduleBlock2.destination == "Paint")
			{
				this.Destinations[this.ID] = this.StudentManager.FridaySpots[this.StudentID];
			}
			if (scheduleBlock2.action == "Stand")
			{
				this.Actions[this.ID] = StudentActionType.AtLocker;
			}
			else if (scheduleBlock2.action == "Socialize")
			{
				this.Actions[this.ID] = StudentActionType.Socializing;
			}
			else if (scheduleBlock2.action == "Game")
			{
				this.Actions[this.ID] = StudentActionType.Gaming;
			}
			else if (scheduleBlock2.action == "Slave")
			{
				this.Actions[this.ID] = StudentActionType.Slave;
			}
			else if (scheduleBlock2.action == "Relax")
			{
				this.Actions[this.ID] = StudentActionType.Relax;
			}
			else if (scheduleBlock2.action == "Sit")
			{
				this.Actions[this.ID] = StudentActionType.SitAndTakeNotes;
			}
			else if (scheduleBlock2.action == "Peek")
			{
				this.Actions[this.ID] = StudentActionType.Peek;
			}
			else if (scheduleBlock2.action == "SocialSit")
			{
				this.Actions[this.ID] = StudentActionType.SitAndSocialize;
				if (this.Persona == PersonaType.Sleuth && this.Club == ClubType.None)
				{
					this.Actions[this.ID] = StudentActionType.Socializing;
				}
			}
			else if (scheduleBlock2.action == "Eat")
			{
				this.Actions[this.ID] = StudentActionType.SitAndEatBento;
			}
			else if (scheduleBlock2.action == "Shoes")
			{
				this.Actions[this.ID] = StudentActionType.ChangeShoes;
			}
			else if (scheduleBlock2.action == "Grade")
			{
				this.Actions[this.ID] = StudentActionType.GradePapers;
			}
			else if (scheduleBlock2.action == "Patrol")
			{
				this.Actions[this.ID] = StudentActionType.Patrol;
				if (this.OriginalClub == ClubType.Occult && this.Club == ClubType.None)
				{
					Debug.Log("This student's club disbanded, so their action has been set to ''Socialize''.");
					this.Actions[this.ID] = StudentActionType.Socializing;
				}
			}
			else if (scheduleBlock2.action == "Search Patrol")
			{
				this.Actions[this.ID] = StudentActionType.SearchPatrol;
			}
			else if (scheduleBlock2.action == "Gossip")
			{
				this.Actions[this.ID] = StudentActionType.Gossip;
			}
			else if (scheduleBlock2.action == "Graffiti")
			{
				this.Actions[this.ID] = StudentActionType.Graffiti;
			}
			else if (scheduleBlock2.action == "Bully")
			{
				this.Actions[this.ID] = StudentActionType.Bully;
			}
			else if (scheduleBlock2.action == "Read")
			{
				this.Actions[this.ID] = StudentActionType.Read;
			}
			else if (scheduleBlock2.action == "Text")
			{
				this.Actions[this.ID] = StudentActionType.Texting;
			}
			else if (scheduleBlock2.action == "Mourn")
			{
				this.Actions[this.ID] = StudentActionType.Mourn;
			}
			else if (scheduleBlock2.action == "Cuddle")
			{
				this.Actions[this.ID] = StudentActionType.Cuddle;
			}
			else if (scheduleBlock2.action == "Teach")
			{
				this.Actions[this.ID] = StudentActionType.Teaching;
			}
			else if (scheduleBlock2.action == "Wait")
			{
				this.Actions[this.ID] = StudentActionType.Wait;
			}
			else if (scheduleBlock2.action == "Clean")
			{
				this.Actions[this.ID] = StudentActionType.Clean;
			}
			else if (scheduleBlock2.action == "Shamed")
			{
				this.Actions[this.ID] = StudentActionType.Shamed;
			}
			else if (scheduleBlock2.action == "Follow")
			{
				this.Actions[this.ID] = StudentActionType.Follow;
			}
			else if (scheduleBlock2.action == "Sulk")
			{
				this.Actions[this.ID] = StudentActionType.Sulk;
			}
			else if (scheduleBlock2.action == "Sleuth")
			{
				this.Actions[this.ID] = StudentActionType.Sleuth;
			}
			else if (scheduleBlock2.action == "Stalk")
			{
				this.Actions[this.ID] = StudentActionType.Stalk;
			}
			else if (scheduleBlock2.action == "Sketch")
			{
				this.Actions[this.ID] = StudentActionType.Sketch;
			}
			else if (scheduleBlock2.action == "Sunbathe")
			{
				this.Actions[this.ID] = StudentActionType.Sunbathe;
			}
			else if (scheduleBlock2.action == "Shock")
			{
				this.Actions[this.ID] = StudentActionType.Shock;
			}
			else if (scheduleBlock2.action == "Miyuki")
			{
				this.Actions[this.ID] = StudentActionType.Miyuki;
			}
			else if (scheduleBlock2.action == "Meeting")
			{
				this.Actions[this.ID] = StudentActionType.Meeting;
			}
			else if (scheduleBlock2.action == "Lyrics")
			{
				this.Actions[this.ID] = StudentActionType.Lyrics;
			}
			else if (scheduleBlock2.action == "Practice")
			{
				this.Actions[this.ID] = StudentActionType.Practice;
			}
			else if (scheduleBlock2.action == "Sew")
			{
				this.Actions[this.ID] = StudentActionType.Sew;
			}
			else if (scheduleBlock2.action == "Paint")
			{
				this.Actions[this.ID] = StudentActionType.Paint;
			}
			else if (scheduleBlock2.action == "Club")
			{
				if (this.Club > ClubType.None)
				{
					this.Actions[this.ID] = StudentActionType.ClubAction;
				}
				else if (this.OriginalClub == ClubType.Art)
				{
					this.Actions[this.ID] = StudentActionType.Sketch;
				}
				else
				{
					this.Actions[this.ID] = StudentActionType.Socializing;
				}
			}
			this.ID++;
		}
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x00180330 File Offset: 0x0017E730
	private void UpdateOutlines()
	{
		this.ID = 0;
		while (this.ID < this.Outlines.Length)
		{
			if (this.Outlines[this.ID] != null)
			{
				this.Outlines[this.ID].color = new Color(1f, 0.5f, 0f, 1f);
				this.Outlines[this.ID].enabled = true;
			}
			this.ID++;
		}
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x001803C0 File Offset: 0x0017E7C0
	public void PickRandomAnim()
	{
		if (this.Grudge)
		{
			this.RandomAnim = this.BulliedIdleAnim;
		}
		else if (this.Club != ClubType.Delinquent)
		{
			this.RandomAnim = this.AnimationNames[UnityEngine.Random.Range(0, this.AnimationNames.Length)];
		}
		else
		{
			this.RandomAnim = this.DelinquentAnims[UnityEngine.Random.Range(0, this.DelinquentAnims.Length)];
		}
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x00180434 File Offset: 0x0017E834
	private void PickRandomGossipAnim()
	{
		if (this.Grudge)
		{
			this.RandomAnim = this.BulliedIdleAnim;
		}
		else
		{
			this.RandomGossipAnim = this.GossipAnims[UnityEngine.Random.Range(0, this.GossipAnims.Length)];
			if (this.Actions[this.Phase] == StudentActionType.Gossip && this.DistanceToPlayer < 3f)
			{
				if (!ConversationGlobals.GetTopicDiscovered(19))
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Topic);
					ConversationGlobals.SetTopicDiscovered(19, true);
				}
				if (!ConversationGlobals.GetTopicLearnedByStudent(19, this.StudentID))
				{
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Opinion);
					ConversationGlobals.SetTopicLearnedByStudent(19, this.StudentID, true);
				}
			}
		}
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x001804F3 File Offset: 0x0017E8F3
	private void PickRandomSleuthAnim()
	{
		if (!this.Sleuthing)
		{
			this.RandomSleuthAnim = this.SleuthAnims[UnityEngine.Random.Range(0, 3)];
		}
		else
		{
			this.RandomSleuthAnim = this.SleuthAnims[UnityEngine.Random.Range(3, 6)];
		}
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x00180530 File Offset: 0x0017E930
	private void BecomeTeacher()
	{
		base.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		this.StudentManager.Teachers[this.Class] = this;
		if (this.Class != 1)
		{
			this.GradingPaper = this.StudentManager.FacultyDesks[this.Class];
			this.GradingPaper.LeftHand = this.LeftHand.parent;
			this.GradingPaper.Character = this.Character;
			this.GradingPaper.Teacher = this;
			this.SkirtCollider.gameObject.SetActive(false);
			this.LowPoly.MyMesh = this.LowPoly.TeacherMesh;
			this.PantyCollider.enabled = false;
		}
		if (this.Class > 1)
		{
			this.VisionDistance = 12f * this.Paranoia;
			base.name = "Teacher_" + this.Class.ToString();
			this.OriginalIdleAnim = "f02_idleShort_00";
			this.IdleAnim = "f02_idleShort_00";
		}
		else if (this.Class == 1)
		{
			this.VisionDistance = 12f * this.Paranoia;
			this.PatrolAnim = "f02_idle_00";
			base.name = "Nurse";
		}
		else
		{
			this.VisionDistance = 16f * this.Paranoia;
			this.PatrolAnim = "f02_stretch_00";
			base.name = "Coach";
			this.OriginalIdleAnim = "f02_tsunIdle_00";
			this.IdleAnim = "f02_tsunIdle_00";
		}
		this.StruggleAnim = "f02_teacherStruggleB_00";
		this.StruggleWonAnim = "f02_teacherStruggleWinB_00";
		this.StruggleLostAnim = "f02_teacherStruggleLoseB_00";
		this.OriginallyTeacher = true;
		this.Spawned = true;
		this.Teacher = true;
		base.gameObject.tag = "Untagged";
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x00180710 File Offset: 0x0017EB10
	public void RemoveShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.Cosmetic.SocksTexture;
			this.MyRenderer.materials[1].mainTexture = this.Cosmetic.SocksTexture;
		}
		else
		{
			this.MyRenderer.materials[this.Cosmetic.UniformID].mainTexture = this.Cosmetic.SocksTexture;
		}
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x00180790 File Offset: 0x0017EB90
	public void BecomeRagdoll()
	{
		if (this.StudentID == this.StudentManager.RivalID)
		{
			this.StudentManager.RivalEliminated = true;
		}
		if (this.LabcoatAttacher.newRenderer != null)
		{
			this.LabcoatAttacher.newRenderer.updateWhenOffscreen = true;
		}
		if (this.ApronAttacher.newRenderer != null)
		{
			this.ApronAttacher.newRenderer.updateWhenOffscreen = true;
		}
		if (this.Attacher.newRenderer != null)
		{
			this.Attacher.newRenderer.updateWhenOffscreen = true;
		}
		if (this.DrinkingFountain != null)
		{
			this.DrinkingFountain.Occupied = false;
		}
		if (!this.Ragdoll.enabled)
		{
			this.EmptyHands();
			if (this.Broken != null)
			{
				this.Broken.enabled = false;
				this.Broken.MyAudio.Stop();
			}
			if (this.Club == ClubType.Delinquent && this.MyWeapon != null)
			{
				this.MyWeapon.transform.parent = null;
				this.MyWeapon.MyCollider.enabled = true;
				this.MyWeapon.Prompt.enabled = true;
				Rigidbody component = this.MyWeapon.GetComponent<Rigidbody>();
				component.constraints = RigidbodyConstraints.None;
				component.isKinematic = false;
				component.useGravity = true;
				this.MyWeapon = null;
			}
			if (this.StudentManager.ChaseCamera == this.ChaseCamera)
			{
				this.StudentManager.ChaseCamera = null;
			}
			this.Countdown.gameObject.SetActive(false);
			this.ChaseCamera.SetActive(false);
			if (this.Club == ClubType.Council)
			{
				this.Police.CouncilDeath = true;
			}
			if (this.WillChase)
			{
				this.Yandere.Chasers--;
			}
			ParticleSystem.EmissionModule emission = this.Hearts.emission;
			if (this.Following)
			{
				emission.enabled = false;
				this.Yandere.Followers--;
				this.Following = false;
			}
			if (this == this.StudentManager.Reporter)
			{
				this.StudentManager.Reporter = null;
			}
			if (this.Pushed)
			{
				this.Police.SuicideStudent = base.gameObject;
				this.Police.SuicideScene = true;
				this.Ragdoll.Suicide = true;
				this.Police.Suicide = true;
			}
			if (!this.Tranquil)
			{
				StudentGlobals.SetStudentDying(this.StudentID, true);
				if (!this.Ragdoll.Burning && !this.Ragdoll.Disturbing)
				{
					if (this.Police.Corpses < this.Police.CorpseList.Length)
					{
						this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
					}
					this.Police.Corpses++;
				}
			}
			if (!this.Male)
			{
				this.LiquidProjector.ignoreLayers = -2049;
				this.RightHandCollider.enabled = false;
				this.LeftHandCollider.enabled = false;
				this.PantyCollider.enabled = false;
				this.SkirtCollider.gameObject.SetActive(false);
			}
			this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			this.Ragdoll.AllColliders[10].isTrigger = false;
			this.NotFaceCollider.enabled = false;
			this.FaceCollider.enabled = false;
			this.MyController.enabled = false;
			emission.enabled = false;
			this.SpeechLines.Stop();
			if (this.MyRenderer.enabled)
			{
				this.MyRenderer.updateWhenOffscreen = true;
			}
			this.Pathfinding.enabled = false;
			this.HipCollider.enabled = true;
			base.enabled = false;
			this.UnWet();
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			this.Ragdoll.CharacterAnimation = this.CharacterAnimation;
			this.Ragdoll.DetectionMarker = this.DetectionMarker;
			this.Ragdoll.RightEyeOrigin = this.RightEyeOrigin;
			this.Ragdoll.LeftEyeOrigin = this.LeftEyeOrigin;
			this.Ragdoll.Electrocuted = this.Electrocuted;
			this.Ragdoll.BreastSize = this.BreastSize;
			this.Ragdoll.EyeShrink = this.EyeShrink;
			this.Ragdoll.StudentID = this.StudentID;
			this.Ragdoll.Tranquil = this.Tranquil;
			this.Ragdoll.Burning = this.Burning;
			this.Ragdoll.Drowned = this.Drowned;
			this.Ragdoll.Yandere = this.Yandere;
			this.Ragdoll.Police = this.Police;
			this.Ragdoll.Pushed = this.Pushed;
			this.Ragdoll.Male = this.Male;
			this.Police.Deaths++;
			this.Ragdoll.enabled = true;
			this.Reputation.PendingRep -= this.PendingRep;
			if (this.WitnessedMurder && this.Persona != PersonaType.Evil)
			{
				this.Police.Witnesses--;
			}
			this.UpdateOutlines();
			if (this.DetectionMarker != null)
			{
				this.DetectionMarker.Tex.enabled = false;
			}
			GameObjectUtils.SetLayerRecursively(base.gameObject, 11);
			base.tag = "Blood";
			this.LowPoly.transform.parent = this.Hips;
			this.LowPoly.transform.localPosition = new Vector3(0f, -1f, 0f);
			this.LowPoly.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		}
		if (this.SmartPhone.transform.parent == this.ItemParent)
		{
			this.SmartPhone.SetActive(false);
		}
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x00180DDC File Offset: 0x0017F1DC
	public void GetWet()
	{
		if (SchemeGlobals.GetSchemeStage(1) == 3 && this.StudentID == this.StudentManager.RivalID)
		{
			SchemeGlobals.SetSchemeStage(1, 4);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
		this.TargetDistance = 1f;
		this.BeenSplashed = true;
		this.BatheFast = true;
		this.LiquidProjector.enabled = true;
		this.Emetic = false;
		this.Sedated = false;
		this.Headache = false;
		if (this.Gas)
		{
			this.LiquidProjector.material.mainTexture = this.GasTexture;
		}
		else if (this.Bloody)
		{
			this.LiquidProjector.material.mainTexture = this.BloodTexture;
		}
		else
		{
			this.LiquidProjector.material.mainTexture = this.WaterTexture;
		}
		this.ID = 0;
		while (this.ID < this.LiquidEmitters.Length)
		{
			ParticleSystem particleSystem = this.LiquidEmitters[this.ID];
			particleSystem.gameObject.SetActive(true);
			ParticleSystem.MainModule main = particleSystem.main;
			if (this.Gas)
			{
				main.startColor = new Color(1f, 1f, 0f, 1f);
			}
			else if (this.Bloody)
			{
				main.startColor = new Color(1f, 0f, 0f, 1f);
			}
			else
			{
				main.startColor = new Color(0f, 1f, 1f, 1f);
			}
			this.ID++;
		}
		if (!this.Slave)
		{
			this.CharacterAnimation[this.SplashedAnim].speed = 1f;
			this.CharacterAnimation.CrossFade(this.SplashedAnim);
			this.Subtitle.UpdateLabel(this.SplashSubtitleType, 0, 1f);
			this.SpeechLines.Stop();
			this.Hearts.Stop();
			this.StopMeeting();
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.SplashTimer = 0f;
			this.SplashPhase = 1;
			this.BathePhase = 1;
			this.ForgetRadio();
			if (this.Distracting)
			{
				this.DistractionTarget.TargetedForDistraction = false;
				this.DistractionTarget.Octodog.SetActive(false);
				this.DistractionTarget.Distracted = false;
				this.Distracting = false;
				this.CanTalk = true;
			}
			if (this.Investigating)
			{
				this.Investigating = false;
			}
			this.SchoolwearUnavailable = true;
			this.Distracted = true;
			this.Splashed = true;
			this.Routine = false;
			this.Wet = true;
			if (this.Following)
			{
				this.Yandere.Followers--;
				this.Following = false;
			}
			this.SpawnAlarmDisc();
			if (this.Club == ClubType.Cooking)
			{
				this.IdleAnim = this.OriginalIdleAnim;
				this.WalkAnim = this.OriginalWalkAnim;
				this.LeanAnim = this.OriginalLeanAnim;
				this.ClubActivityPhase = 0;
				this.ClubTimer = 0f;
			}
			this.EmptyHands();
		}
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x0018112C File Offset: 0x0017F52C
	public void UnWet()
	{
		this.ID = 0;
		while (this.ID < this.LiquidEmitters.Length)
		{
			this.LiquidEmitters[this.ID].gameObject.SetActive(false);
			this.ID++;
		}
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x00181180 File Offset: 0x0017F580
	public void SetSplashes(bool Bool)
	{
		this.ID = 0;
		while (this.ID < this.SplashEmitters.Length)
		{
			this.SplashEmitters[this.ID].gameObject.SetActive(Bool);
			this.ID++;
		}
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x001811D4 File Offset: 0x0017F5D4
	private void StopMeeting()
	{
		this.Prompt.Label[0].text = "     Talk";
		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;
		this.Drownable = false;
		this.Pushable = false;
		this.Meeting = false;
		this.MeetTimer = 0f;
		if (this.StudentID == 30)
		{
			this.StudentManager.OfferHelp.gameObject.SetActive(false);
			this.StudentManager.LoveManager.RivalWaiting = false;
		}
		else if (this.StudentID == 5)
		{
			this.StudentManager.FragileOfferHelp.gameObject.SetActive(false);
		}
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x0018128C File Offset: 0x0017F68C
	public void Combust()
	{
		this.Police.CorpseList[this.Police.Corpses] = this.Ragdoll;
		this.Police.Corpses++;
		GameObjectUtils.SetLayerRecursively(base.gameObject, 11);
		base.tag = "Blood";
		this.Dying = true;
		this.SpawnAlarmDisc();
		this.Character.GetComponent<Animation>().CrossFade(this.BurningAnim);
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		this.Ragdoll.BurningAnimation = true;
		this.Ragdoll.Disturbing = true;
		this.Ragdoll.Burning = true;
		this.WitnessedCorpse = false;
		this.Investigating = false;
		this.EatingSnack = false;
		this.DiscCheck = false;
		this.WalkBack = false;
		this.Alarmed = false;
		this.CanTalk = false;
		this.Fleeing = false;
		this.Routine = false;
		this.Reacted = false;
		this.Burning = true;
		this.Wet = false;
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.BurningClip;
		component.Play();
		this.LiquidProjector.enabled = false;
		this.UnWet();
		if (this.Following)
		{
			this.Yandere.Followers--;
			this.Following = false;
		}
		this.ID = 0;
		while (this.ID < this.FireEmitters.Length)
		{
			this.FireEmitters[this.ID].gameObject.SetActive(true);
			this.ID++;
		}
		if (this.Attacked)
		{
			this.BurnTarget = this.Yandere.transform.position + this.Yandere.transform.forward;
			this.Attacked = false;
		}
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x00181468 File Offset: 0x0017F868
	public void JojoReact()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.JojoHitEffect, base.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
		if (!this.Dying)
		{
			this.Dying = true;
			this.SpawnAlarmDisc();
			this.Character.GetComponent<Animation>().CrossFade(this.JojoReactAnim);
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
			this.WitnessedCorpse = false;
			this.Investigating = false;
			this.EatingSnack = false;
			this.DiscCheck = false;
			this.WalkBack = false;
			this.Alarmed = false;
			this.CanTalk = false;
			this.Fleeing = false;
			this.Routine = false;
			this.Reacted = false;
			this.Wet = false;
			AudioSource component = base.GetComponent<AudioSource>();
			component.Play();
			if (this.Following)
			{
				this.Yandere.Followers--;
				this.Following = false;
			}
		}
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x00181570 File Offset: 0x0017F970
	private void Nude()
	{
		if (!this.Male)
		{
			this.PantyCollider.enabled = false;
			this.SkirtCollider.gameObject.SetActive(false);
		}
		if (!this.Male)
		{
			this.MyRenderer.sharedMesh = this.TowelMesh;
			if (this.Club == ClubType.Bully)
			{
				this.Cosmetic.DeactivateBullyAccessories();
			}
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[0].mainTexture = this.TowelTexture;
			this.MyRenderer.materials[1].mainTexture = this.TowelTexture;
			this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
			this.Cosmetic.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		}
		else
		{
			this.MyRenderer.sharedMesh = this.BaldNudeMesh;
			this.MyRenderer.materials[0].mainTexture = this.NudeTexture;
			this.MyRenderer.materials[1].mainTexture = null;
			this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTextures[this.SkinColor];
		}
		this.Cosmetic.RemoveCensor();
		if (!this.AoT)
		{
			if (this.Male)
			{
				this.ID = 0;
				while (this.ID < this.CensorSteam.Length)
				{
					this.CensorSteam[this.ID].SetActive(true);
					this.ID++;
				}
			}
		}
		else if (!this.Male)
		{
			this.MyRenderer.sharedMesh = this.BaldNudeMesh;
			this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTexture;
			this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
			this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
		}
		else
		{
			this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTextures[this.SkinColor];
		}
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x001817C0 File Offset: 0x0017FBC0
	public void ChangeSchoolwear()
	{
		this.ID = 0;
		while (this.ID < this.CensorSteam.Length)
		{
			this.CensorSteam[this.ID].SetActive(false);
			this.ID++;
		}
		if (this.Attacher.gameObject.activeInHierarchy)
		{
			this.Attacher.RemoveAccessory();
		}
		if (this.LabcoatAttacher.enabled)
		{
			UnityEngine.Object.Destroy(this.LabcoatAttacher.newRenderer);
			this.LabcoatAttacher.enabled = false;
		}
		if (this.Schoolwear == 0)
		{
			this.Nude();
		}
		else if (this.Schoolwear == 1)
		{
			if (!this.Male)
			{
				this.Cosmetic.SetFemaleUniform();
				this.SkirtCollider.gameObject.SetActive(true);
				if (this.PantyCollider != null)
				{
					this.PantyCollider.enabled = true;
				}
				if (this.Club == ClubType.Bully)
				{
					this.Cosmetic.RightWristband.SetActive(true);
					this.Cosmetic.LeftWristband.SetActive(true);
					this.Cosmetic.Bookbag.SetActive(true);
					this.Cosmetic.Hoodie.SetActive(true);
				}
			}
			else
			{
				this.Cosmetic.SetMaleUniform();
			}
		}
		else if (this.Schoolwear == 2)
		{
			if (this.Club == ClubType.Sports)
			{
				this.MyRenderer.sharedMesh = this.SwimmingTrunks;
				this.MyRenderer.SetBlendShapeWeight(0, (float)(20 * (6 - this.ClubMemberID)));
				this.MyRenderer.SetBlendShapeWeight(1, (float)(20 * (6 - this.ClubMemberID)));
				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.Trunks[this.StudentID];
				this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
				this.MyRenderer.materials[2].mainTexture = this.Cosmetic.Trunks[this.StudentID];
			}
			else
			{
				this.MyRenderer.sharedMesh = this.SchoolSwimsuit;
				if (!this.Male)
				{
					if (this.Club == ClubType.Bully)
					{
						this.MyRenderer.materials[0].mainTexture = this.Cosmetic.GanguroSwimsuitTextures[this.BullyID];
						this.MyRenderer.materials[1].mainTexture = this.Cosmetic.GanguroSwimsuitTextures[this.BullyID];
						this.Cosmetic.RightWristband.SetActive(false);
						this.Cosmetic.LeftWristband.SetActive(false);
						this.Cosmetic.Bookbag.SetActive(false);
						this.Cosmetic.Hoodie.SetActive(false);
					}
					else
					{
						this.MyRenderer.materials[0].mainTexture = this.SwimsuitTexture;
						this.MyRenderer.materials[1].mainTexture = this.SwimsuitTexture;
					}
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.SwimsuitTexture;
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
					this.MyRenderer.materials[2].mainTexture = this.SwimsuitTexture;
				}
			}
		}
		else if (this.Schoolwear == 3)
		{
			this.MyRenderer.sharedMesh = this.GymUniform;
			if (!this.Male)
			{
				this.MyRenderer.materials[0].mainTexture = this.GymTexture;
				this.MyRenderer.materials[1].mainTexture = this.GymTexture;
				this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
				if (this.Club == ClubType.Bully)
				{
					this.Cosmetic.ActivateBullyAccessories();
				}
			}
			else
			{
				Debug.Log("A male is putting on a gym uniform.");
				this.MyRenderer.materials[0].mainTexture = this.GymTexture;
				this.MyRenderer.materials[1].mainTexture = this.Cosmetic.SkinTextures[this.Cosmetic.SkinColor];
				this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
			}
		}
		if (!this.Male)
		{
			this.Cosmetic.Stockings = ((this.Schoolwear != 1) ? string.Empty : this.Cosmetic.OriginalStockings);
			base.StartCoroutine(this.Cosmetic.PutOnStockings());
			if (this.StudentManager.Censor)
			{
				this.Cosmetic.CensorPanties();
			}
		}
		while (this.ID < this.Outlines.Length)
		{
			if (this.Outlines[this.ID] != null && this.Outlines[this.ID].h != null)
			{
				this.Outlines[this.ID].h.ReinitMaterials();
			}
			this.ID++;
		}
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x00181D18 File Offset: 0x00180118
	public void AttackOnTitan()
	{
		this.CharacterAnimation.CrossFade(this.WalkAnim);
		this.AoT = true;
		this.MyController.center = new Vector3(this.MyController.center.x, 0.0825f, this.MyController.center.z);
		this.MyController.radius = 0.015f;
		this.MyController.height = 0.15f;
		if (!this.Male)
		{
			this.Cosmetic.FaceTexture = this.TitanFaceTexture;
		}
		else
		{
			this.Cosmetic.FaceTextures[this.SkinColor] = this.TitanFaceTexture;
		}
		this.NudeTexture = this.TitanBodyTexture;
		this.Nude();
		this.ID = 0;
		while (this.ID < this.Outlines.Length)
		{
			OutlineScript outlineScript = this.Outlines[this.ID];
			if (outlineScript.h == null)
			{
				outlineScript.Awake();
			}
			outlineScript.h.ReinitMaterials();
			this.ID++;
		}
		if (!this.Male && !this.Teacher)
		{
			this.PantyCollider.enabled = false;
			this.SkirtCollider.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x00181E74 File Offset: 0x00180274
	public void Spook()
	{
		if (!this.Male)
		{
			this.RightEye.gameObject.SetActive(false);
			this.LeftEye.gameObject.SetActive(false);
			this.MyRenderer.enabled = false;
			this.ID = 0;
			while (this.ID < this.Bones.Length)
			{
				this.Bones[this.ID].SetActive(true);
				this.ID++;
			}
		}
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x00181EFC File Offset: 0x001802FC
	private void Unspook()
	{
		this.MyRenderer.enabled = true;
		this.ID = 0;
		while (this.ID < this.Bones.Length)
		{
			this.Bones[this.ID].SetActive(false);
			this.ID++;
		}
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x00181F58 File Offset: 0x00180358
	private void GoChange()
	{
		if (!this.Male)
		{
			this.CurrentDestination = this.StudentManager.FemaleStripSpot;
			this.Pathfinding.target = this.StudentManager.FemaleStripSpot;
		}
		else
		{
			this.CurrentDestination = this.StudentManager.MaleStripSpot;
			this.Pathfinding.target = this.StudentManager.MaleStripSpot;
		}
		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;
		this.Distracted = false;
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x00181FE4 File Offset: 0x001803E4
	public void SpawnAlarmDisc()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity);
		gameObject.GetComponent<AlarmDiscScript>().Male = this.Male;
		gameObject.GetComponent<AlarmDiscScript>().Originator = this;
		if (this.Splashed)
		{
			gameObject.GetComponent<AlarmDiscScript>().Shocking = true;
			gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
		}
		if (this.Struggling || this.Shoving || this.MurderSuicidePhase == 100 || this.StudentManager.CombatMinigame.Delinquent == this)
		{
			gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
		}
		if (this.Club == ClubType.Delinquent)
		{
			gameObject.GetComponent<AlarmDiscScript>().Delinquent = true;
		}
		if (this.Dying && this.Yandere.Equipped > 0 && this.Yandere.EquippedWeapon.WeaponID == 7)
		{
			gameObject.GetComponent<AlarmDiscScript>().Long = true;
		}
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x001820F8 File Offset: 0x001804F8
	public void SpawnSmallAlarmDisc()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, base.transform.position + Vector3.up, Quaternion.identity);
		gameObject.transform.localScale = new Vector3(100f, 1f, 100f);
		gameObject.GetComponent<AlarmDiscScript>().NoScream = true;
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x00182158 File Offset: 0x00180558
	public void ChangeClubwear()
	{
		if (!this.ClubAttire)
		{
			this.Cosmetic.RemoveCensor();
			this.DistanceToDestination = 100f;
			this.ClubAttire = true;
			if (this.Club == ClubType.Art)
			{
				if (!this.Attacher.gameObject.activeInHierarchy)
				{
					this.Attacher.gameObject.SetActive(true);
				}
				else
				{
					this.Attacher.Start();
				}
			}
			else if (this.Club == ClubType.MartialArts)
			{
				this.MyRenderer.sharedMesh = this.JudoGiMesh;
				if (!this.Male)
				{
					this.MyRenderer.materials[0].mainTexture = this.JudoGiTexture;
					this.MyRenderer.materials[1].mainTexture = this.JudoGiTexture;
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
					this.SkirtCollider.gameObject.SetActive(false);
					this.PantyCollider.enabled = false;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.JudoGiTexture;
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
					this.MyRenderer.materials[2].mainTexture = this.JudoGiTexture;
				}
			}
			else if (this.Club == ClubType.Science)
			{
				this.WearLabCoat();
			}
			else if (this.Club == ClubType.Sports)
			{
				if (this.Clock.Period < 3)
				{
					this.MyRenderer.sharedMesh = this.GymUniform;
					this.MyRenderer.materials[0].mainTexture = this.GymTexture;
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.SkinTextures[this.Cosmetic.SkinID];
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.FaceTexture;
				}
				else
				{
					this.MyRenderer.sharedMesh = this.SwimmingTrunks;
					this.MyRenderer.SetBlendShapeWeight(0, (float)(20 * (6 - this.ClubMemberID)));
					this.MyRenderer.SetBlendShapeWeight(1, (float)(20 * (6 - this.ClubMemberID)));
					this.MyRenderer.materials[0].mainTexture = this.Cosmetic.Trunks[this.StudentID];
					this.MyRenderer.materials[1].mainTexture = this.Cosmetic.FaceTexture;
					this.MyRenderer.materials[2].mainTexture = this.Cosmetic.Trunks[this.StudentID];
					this.ClubAnim = "poolDive_00";
					this.ClubActivityPhase = 15;
					this.Destinations[this.Phase] = this.StudentManager.Clubs.List[this.StudentID].GetChild(this.ClubActivityPhase);
				}
			}
			if (this.StudentID == 46)
			{
				this.Armband.transform.localPosition = new Vector3(this.Armband.transform.localPosition.x, this.Armband.transform.localPosition.y, 0.01f);
				this.Armband.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
			}
		}
		else
		{
			this.ClubAttire = false;
			if (this.Club == ClubType.Art)
			{
				this.Attacher.RemoveAccessory();
			}
			else if (this.Club == ClubType.Science)
			{
				this.WearLabCoat();
			}
			else
			{
				this.ChangeSchoolwear();
				if (this.StudentID == 46)
				{
					this.Armband.transform.localPosition = new Vector3(this.Armband.transform.localPosition.x, this.Armband.transform.localPosition.y, 0.012f);
					this.Armband.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
				}
				else if (this.StudentID == 47)
				{
					this.StudentManager.ConvoManager.Confirmed = false;
					this.ClubAnim = "idle_20";
				}
				else if (this.StudentID == 49)
				{
					this.StudentManager.ConvoManager.Confirmed = false;
					this.ClubAnim = "f02_idle_20";
				}
			}
		}
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x001825E8 File Offset: 0x001809E8
	private void WearLabCoat()
	{
		if (!this.LabcoatAttacher.enabled)
		{
			this.MyRenderer.sharedMesh = this.HeadAndHands;
			this.LabcoatAttacher.enabled = true;
			if (!this.Male)
			{
				this.RightBreast.gameObject.name = "RightBreastRENAMED";
				this.LeftBreast.gameObject.name = "LeftBreastRENAMED";
			}
			if (this.LabcoatAttacher.Initialized)
			{
				this.LabcoatAttacher.AttachAccessory();
			}
			if (!this.Male)
			{
				this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTexture;
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = null;
				this.Cosmetic.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
				this.SkirtCollider.gameObject.SetActive(false);
				this.PantyCollider.enabled = false;
			}
			else
			{
				this.MyRenderer.materials[0].mainTexture = this.Cosmetic.FaceTextures[this.SkinColor];
				this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
				this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
			}
		}
		else
		{
			if (!this.Male)
			{
				this.RightBreast.gameObject.name = "RightBreastRENAMED";
				this.LeftBreast.gameObject.name = "LeftBreastRENAMED";
				this.SkirtCollider.gameObject.SetActive(true);
				this.PantyCollider.enabled = true;
			}
			UnityEngine.Object.Destroy(this.LabcoatAttacher.newRenderer);
			this.LabcoatAttacher.enabled = false;
			this.ChangeSchoolwear();
		}
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x001827F4 File Offset: 0x00180BF4
	public void AttachRiggedAccessory()
	{
		this.RiggedAccessory.GetComponent<RiggedAccessoryAttacher>().ID = this.StudentID;
		if (this.Cosmetic.Accessory > 0)
		{
			this.Cosmetic.FemaleAccessories[this.Cosmetic.Accessory].SetActive(false);
		}
		if (this.StudentID == 26)
		{
			this.MyRenderer.sharedMesh = this.NoArmsNoTorso;
		}
		this.RiggedAccessory.SetActive(true);
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x00182870 File Offset: 0x00180C70
	public void CameraReact()
	{
		this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
		this.Pathfinding.canSearch = false;
		this.Pathfinding.canMove = false;
		this.Obstacle.enabled = true;
		this.CameraReacting = true;
		this.CameraReactPhase = 1;
		this.SpeechLines.Stop();
		this.Routine = false;
		this.StopPairing();
		if (!this.Sleuthing)
		{
			this.SmartPhone.SetActive(false);
		}
		this.OccultBook.SetActive(false);
		this.Scrubber.SetActive(false);
		this.Eraser.SetActive(false);
		this.Pen.SetActive(false);
		this.Pencil.SetActive(false);
		this.Sketchbook.SetActive(false);
		if (this.Club == ClubType.Gardening)
		{
			this.WateringCan.transform.parent = this.Hips;
			this.WateringCan.transform.localPosition = new Vector3(0f, 0.0135f, -0.184f);
			this.WateringCan.transform.localEulerAngles = new Vector3(0f, 90f, 30f);
		}
		else if (this.Club == ClubType.LightMusic)
		{
			if (this.StudentID == 51)
			{
				if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
				{
					this.Instruments[this.ClubMemberID].transform.parent = null;
					this.Instruments[this.ClubMemberID].transform.position = new Vector3(-0.5f, 4.5f, 22.45666f);
					this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, 0f, 0f);
					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
				}
				else
				{
					this.Instruments[this.ClubMemberID].SetActive(false);
				}
			}
			else
			{
				this.Instruments[this.ClubMemberID].SetActive(false);
			}
			this.Drumsticks[0].SetActive(false);
			this.Drumsticks[1].SetActive(false);
		}
		foreach (GameObject gameObject in this.ScienceProps)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject2 in this.Fingerfood)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
		if (!this.Yandere.ClubAccessories[7].activeInHierarchy || this.Club == ClubType.Delinquent)
		{
			this.CharacterAnimation.CrossFade(this.CameraAnims[1]);
		}
		else
		{
			if (this.Club == ClubType.Bully)
			{
				this.SmartPhone.SetActive(true);
			}
			this.CharacterAnimation.CrossFade(this.IdleAnim);
		}
		this.EmptyHands();
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x00182BA0 File Offset: 0x00180FA0
	private void LookForYandere()
	{
		if (!this.Yandere.Chased && this.CanSeeObject(this.Yandere.gameObject, this.Yandere.HeadPosition))
		{
			this.ReportPhase++;
		}
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x00182BEC File Offset: 0x00180FEC
	public void UpdatePerception()
	{
		if (ClubGlobals.Club == ClubType.Occult || PlayerGlobals.StealthBonus > 0)
		{
			this.Perception = 0.5f;
		}
		else
		{
			this.Perception = 1f;
		}
		this.ChameleonCheck();
		if (this.Chameleon)
		{
			this.Perception *= 0.5f;
		}
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x00182C50 File Offset: 0x00181050
	public void StopInvestigating()
	{
		Debug.Log(this.Name + " was invesigating a giggle, but has stopped.");
		this.Giggle = null;
		if (!this.Sleuthing)
		{
			this.CurrentDestination = this.Destinations[this.Phase];
			this.Pathfinding.target = this.Destinations[this.Phase];
			if (this.Actions[this.Phase] == StudentActionType.Sunbathe && this.SunbathePhase > 1)
			{
				this.CurrentDestination = this.StudentManager.SunbatheSpots[this.StudentID];
				this.Pathfinding.target = this.StudentManager.SunbatheSpots[this.StudentID];
			}
		}
		else
		{
			this.CurrentDestination = this.SleuthTarget;
			this.Pathfinding.target = this.SleuthTarget;
		}
		this.InvestigationTimer = 0f;
		this.InvestigationPhase = 0;
		if (!this.Hurry)
		{
			this.Pathfinding.speed = 1f;
		}
		else
		{
			Debug.Log("Sprinting 17");
			this.Pathfinding.speed = 4f;
		}
		if (this.CurrentAction == StudentActionType.Clean)
		{
			this.SmartPhone.SetActive(false);
			this.Scrubber.SetActive(true);
			if (this.CleaningRole == 5)
			{
				this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
				this.Eraser.SetActive(true);
			}
		}
		this.YandereInnocent = false;
		this.Investigating = false;
		this.EatingSnack = false;
		this.HeardScream = false;
		this.DiscCheck = false;
		this.Routine = true;
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x00182E03 File Offset: 0x00181203
	public void ForgetGiggle()
	{
		this.Giggle = null;
		this.InvestigationTimer = 0f;
		this.InvestigationPhase = 0;
		this.YandereInnocent = false;
		this.Investigating = false;
		this.DiscCheck = false;
	}

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x06002118 RID: 8472 RVA: 0x00182E33 File Offset: 0x00181233
	public bool InCouple
	{
		get
		{
			return this.CoupleID > 0;
		}
	}

	// Token: 0x06002119 RID: 8473 RVA: 0x00182E40 File Offset: 0x00181240
	private bool LovedOneIsTargeted(int yandereTargetID)
	{
		bool flag = this.InCouple && this.CoupleID == yandereTargetID;
		bool flag2 = this.StudentID == 3 && yandereTargetID == 2;
		bool flag3 = this.StudentID == 2 && yandereTargetID == 3;
		bool flag4 = this.StudentID == 38 && yandereTargetID == 37;
		bool flag5 = this.StudentID == 37 && yandereTargetID == 38;
		bool flag6 = this.StudentID == 30 && yandereTargetID == 25;
		bool flag7 = this.StudentID == 25 && yandereTargetID == 30;
		bool flag8 = this.StudentID == 28 && yandereTargetID == 30;
		bool flag9 = this.StudentID == 6 && yandereTargetID == 11;
		bool flag10 = false;
		bool flag11 = this.StudentID > 55 && this.StudentID < 61 && yandereTargetID > 55 && yandereTargetID < 61;
		if (this.Injured)
		{
			flag10 = (this.Club == ClubType.Delinquent && this.StudentManager.Students[yandereTargetID].Club == ClubType.Delinquent);
		}
		return flag || flag2 || flag3 || flag4 || flag5 || flag6 || flag7 || flag8 || flag9 || flag10 || flag11;
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x00182FC4 File Offset: 0x001813C4
	private void Pose()
	{
		this.StudentManager.PoseMode.ChoosingAction = true;
		this.StudentManager.PoseMode.Panel.enabled = true;
		this.StudentManager.PoseMode.Student = this;
		this.StudentManager.PoseMode.UpdateLabels();
		this.StudentManager.PoseMode.Show = true;
		this.DialogueWheel.PromptBar.ClearButtons();
		this.DialogueWheel.PromptBar.Label[0].text = "Confirm";
		this.DialogueWheel.PromptBar.Label[1].text = "Back";
		this.DialogueWheel.PromptBar.Label[4].text = "Change";
		this.DialogueWheel.PromptBar.Label[5].text = "Increase/Decrease";
		this.DialogueWheel.PromptBar.UpdateButtons();
		this.DialogueWheel.PromptBar.Show = true;
		this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
		this.Yandere.CanMove = false;
		this.Posing = true;
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x00183100 File Offset: 0x00181500
	public void DisableEffects()
	{
		this.LiquidProjector.enabled = false;
		this.ElectroSteam[0].SetActive(false);
		this.ElectroSteam[1].SetActive(false);
		this.ElectroSteam[2].SetActive(false);
		this.ElectroSteam[3].SetActive(false);
		this.CensorSteam[0].SetActive(false);
		this.CensorSteam[1].SetActive(false);
		this.CensorSteam[2].SetActive(false);
		this.CensorSteam[3].SetActive(false);
		foreach (ParticleSystem particleSystem in this.LiquidEmitters)
		{
			particleSystem.gameObject.SetActive(false);
		}
		foreach (ParticleSystem particleSystem2 in this.FireEmitters)
		{
			particleSystem2.gameObject.SetActive(false);
		}
		this.ID = 0;
		while (this.ID < this.Bones.Length)
		{
			if (this.Bones[this.ID] != null)
			{
				this.Bones[this.ID].SetActive(false);
			}
			this.ID++;
		}
		if (this.Persona != PersonaType.PhoneAddict)
		{
			this.SmartPhone.SetActive(false);
		}
		this.Note.SetActive(false);
		if (!this.Slave)
		{
			UnityEngine.Object.Destroy(this.Broken);
		}
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x0018327C File Offset: 0x0018167C
	public void DetermineSenpaiReaction()
	{
		Debug.Log("We are now determining Senpai's reaction to Yandere-chan's behavior.");
		if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiWeaponReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.Weapon)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiWeaponReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.Blood)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiBloodReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.Insanity)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiInsanityReaction, 1, 4.5f);
		}
		else if (this.Witnessed == StudentWitnessType.Lewd)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiLewdReaction, 1, 4.5f);
		}
		else if (this.GameOverCause == GameOverType.Stalking)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiStalkingReaction, this.Concern, 4.5f);
		}
		else if (this.GameOverCause == GameOverType.Murder)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiMurderReaction, this.MurderReaction, 4.5f);
		}
		else if (this.GameOverCause == GameOverType.Violence)
		{
			this.Subtitle.UpdateLabel(SubtitleType.SenpaiViolenceReaction, 1, 4.5f);
		}
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x00183449 File Offset: 0x00181849
	public void ForgetRadio()
	{
		this.TurnOffRadio = false;
		this.RadioTimer = 0f;
		this.RadioPhase = 1;
		this.Routine = true;
		this.Radio = null;
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x00183474 File Offset: 0x00181874
	public void RealizePhoneIsMissing()
	{
		ScheduleBlock scheduleBlock = this.ScheduleBlocks[2];
		scheduleBlock.destination = "Search Patrol";
		scheduleBlock.action = "Search Patrol";
		this.GetDestinations();
		ScheduleBlock scheduleBlock2 = this.ScheduleBlocks[4];
		scheduleBlock2.destination = "Search Patrol";
		scheduleBlock2.action = "Search Patrol";
		this.GetDestinations();
		ScheduleBlock scheduleBlock3 = this.ScheduleBlocks[7];
		scheduleBlock3.destination = "Search Patrol";
		scheduleBlock3.action = "Search Patrol";
		this.GetDestinations();
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x001834F0 File Offset: 0x001818F0
	public void TeleportToDestination()
	{
		if (this.Clock.HourTime >= this.ScheduleBlocks[this.Phase].time)
		{
			this.Phase++;
			if (this.Actions[this.Phase] == StudentActionType.Patrol)
			{
				this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
				this.Pathfinding.target = this.CurrentDestination;
			}
			else
			{
				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.Destinations[this.Phase];
			}
			base.transform.position = this.CurrentDestination.position;
		}
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x001835C0 File Offset: 0x001819C0
	public void GoCommitMurder()
	{
		this.StudentManager.MurderTakingPlace = true;
		if (!this.FragileSlave)
		{
			this.Yandere.EquippedWeapon.transform.parent = this.HipCollider.transform;
			this.Yandere.EquippedWeapon.transform.localPosition = Vector3.zero;
			this.Yandere.EquippedWeapon.transform.localScale = Vector3.zero;
			this.MyWeapon = this.Yandere.EquippedWeapon;
			this.MyWeapon.FingerprintID = this.StudentID;
			this.Yandere.EquippedWeapon = null;
			this.Yandere.Equipped = 0;
			this.StudentManager.UpdateStudents(0);
			this.Yandere.WeaponManager.UpdateLabels();
			this.Yandere.WeaponMenu.UpdateSprites();
			this.Yandere.WeaponWarning = false;
		}
		else
		{
			this.StudentManager.FragileWeapon.transform.parent = this.HipCollider.transform;
			this.StudentManager.FragileWeapon.transform.localPosition = Vector3.zero;
			this.StudentManager.FragileWeapon.transform.localScale = Vector3.zero;
			this.MyWeapon = this.StudentManager.FragileWeapon;
			this.MyWeapon.FingerprintID = this.StudentID;
			this.MyWeapon.MyCollider.enabled = false;
		}
		this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
		this.CharacterAnimation.CrossFade("f02_brokenStandUp_00");
		if (this.HuntTarget != this)
		{
			this.DistanceToDestination = 100f;
			this.Broken.Hunting = true;
			this.TargetDistance = 1f;
			this.Routine = false;
			this.Hunting = true;
		}
		else
		{
			this.Broken.Done = true;
			this.Routine = false;
			this.Suicide = true;
		}
		this.Prompt.Hide();
		this.Prompt.enabled = false;
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x001837CC File Offset: 0x00181BCC
	public void Shove()
	{
		if (!this.Yandere.Shoved && !this.Dying && !this.Yandere.Egg && !this.Yandere.Lifting && !this.ShoeRemoval.enabled && !this.Yandere.Talking && !this.SentToLocker)
		{
			this.ForgetRadio();
			Debug.Log(this.Name + " is shoving Yandere-chan.");
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.StudentID == 86)
			{
				this.Subtitle.UpdateLabel(SubtitleType.Shoving, 1, 5f);
			}
			else if (this.StudentID == 87)
			{
				this.Subtitle.UpdateLabel(SubtitleType.Shoving, 2, 5f);
			}
			else if (this.StudentID == 88)
			{
				this.Subtitle.UpdateLabel(SubtitleType.Shoving, 3, 5f);
			}
			else if (this.StudentID == 89)
			{
				this.Subtitle.UpdateLabel(SubtitleType.Shoving, 4, 5f);
			}
			if (this.Yandere.Aiming)
			{
				this.Yandere.StopAiming();
			}
			if (this.Yandere.Laughing)
			{
				this.Yandere.StopLaughing();
			}
			base.transform.rotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
			this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(this.Hips.transform.position.x, this.Yandere.transform.position.y, this.Hips.transform.position.z) - this.Yandere.transform.position);
			this.CharacterAnimation[this.ShoveAnim].time = 0f;
			this.CharacterAnimation.CrossFade(this.ShoveAnim);
			this.FocusOnYandere = false;
			this.Investigating = false;
			this.Distracted = true;
			this.Alarmed = false;
			this.Routine = false;
			this.Shoving = true;
			this.NoTalk = false;
			this.Patience--;
			if (this.Club != ClubType.Council && this.Persona != PersonaType.Violent)
			{
				this.Patience = 999;
			}
			if (this.Patience < 1)
			{
				this.Yandere.CannotRecover = true;
			}
			if (this.ReturningMisplacedWeapon)
			{
				this.DropMisplacedWeapon();
			}
			this.Yandere.CharacterAnimation["f02_shoveA_01"].time = 0f;
			this.Yandere.CharacterAnimation.CrossFade("f02_shoveA_01");
			this.Yandere.YandereVision = false;
			this.Yandere.NearSenpai = false;
			this.Yandere.Degloving = false;
			this.Yandere.Flicking = false;
			this.Yandere.Punching = false;
			this.Yandere.CanMove = false;
			this.Yandere.Shoved = true;
			this.Yandere.EmptyHands();
			this.Yandere.GloveTimer = 0f;
			this.Yandere.h = 0f;
			this.Yandere.v = 0f;
			this.Yandere.ShoveSpeed = 2f;
			if (this.Distraction != null)
			{
				this.TargetedForDistraction = false;
				this.Pathfinding.speed = 1f;
				this.SpeechLines.Stop();
				this.Distraction = null;
				this.CanTalk = true;
			}
			if (this.Actions[this.Phase] != StudentActionType.Patrol)
			{
				this.CurrentDestination = this.Destinations[this.Phase];
				this.Pathfinding.target = this.CurrentDestination;
			}
			this.Pathfinding.canSearch = false;
			this.Pathfinding.canMove = false;
		}
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x00183C3C File Offset: 0x0018203C
	public void PushYandereAway()
	{
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}
		if (this.Yandere.Laughing)
		{
			this.Yandere.StopLaughing();
		}
		this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(this.Hips.transform.position.x, this.Yandere.transform.position.y, this.Hips.transform.position.z) - this.Yandere.transform.position);
		this.Yandere.CharacterAnimation["f02_shoveA_01"].time = 0f;
		this.Yandere.CharacterAnimation.CrossFade("f02_shoveA_01");
		this.Yandere.YandereVision = false;
		this.Yandere.NearSenpai = false;
		this.Yandere.Degloving = false;
		this.Yandere.Flicking = false;
		this.Yandere.Punching = false;
		this.Yandere.CanMove = false;
		this.Yandere.Shoved = true;
		this.Yandere.EmptyHands();
		this.Yandere.GloveTimer = 0f;
		this.Yandere.h = 0f;
		this.Yandere.v = 0f;
		this.Yandere.ShoveSpeed = 2f;
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x00183DCC File Offset: 0x001821CC
	public void Spray()
	{
		Debug.Log(this.Name + " is trying to Spray Yandere-chan!");
		if (this.Yandere.Attacking)
		{
			this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
		}
		else
		{
			bool flag = false;
			if (this.Yandere.DelinquentFighting && !this.NoBreakUp && !this.StudentManager.CombatMinigame.Delinquent.WitnessedMurder)
			{
				flag = true;
			}
			if (!flag)
			{
				if (!this.Yandere.Sprayed && !this.Dying && !this.Yandere.Egg && !this.Yandere.Dumping && !this.Yandere.Bathing)
				{
					if (this.SprayTimer > 0f)
					{
						this.SprayTimer = Mathf.MoveTowards(this.SprayTimer, 0f, Time.deltaTime);
					}
					else
					{
						AudioSource.PlayClipAtPoint(this.PepperSpraySFX, base.transform.position);
						if (this.StudentID == 86)
						{
							this.Subtitle.UpdateLabel(SubtitleType.Spraying, 1, 5f);
						}
						else if (this.StudentID == 87)
						{
							this.Subtitle.UpdateLabel(SubtitleType.Spraying, 2, 5f);
						}
						else if (this.StudentID == 88)
						{
							this.Subtitle.UpdateLabel(SubtitleType.Spraying, 3, 5f);
						}
						else if (this.StudentID == 89)
						{
							this.Subtitle.UpdateLabel(SubtitleType.Spraying, 4, 5f);
						}
						if (this.Yandere.Aiming)
						{
							this.Yandere.StopAiming();
						}
						if (this.Yandere.Laughing)
						{
							this.Yandere.StopLaughing();
						}
						base.transform.rotation = Quaternion.LookRotation(new Vector3(this.Yandere.Hips.transform.position.x, base.transform.position.y, this.Yandere.Hips.transform.position.z) - base.transform.position);
						this.Yandere.transform.rotation = Quaternion.LookRotation(new Vector3(this.Hips.transform.position.x, this.Yandere.transform.position.y, this.Hips.transform.position.z) - this.Yandere.transform.position);
						this.CharacterAnimation.CrossFade(this.SprayAnim);
						this.PepperSpray.SetActive(true);
						this.Distracted = true;
						this.Spraying = true;
						this.Alarmed = false;
						this.Routine = false;
						this.Yandere.CharacterAnimation.CrossFade("f02_sprayed_00");
						this.Yandere.YandereVision = false;
						this.Yandere.NearSenpai = false;
						this.Yandere.FollowHips = true;
						this.Yandere.Punching = false;
						this.Yandere.CanMove = false;
						this.Yandere.Sprayed = true;
						this.Pathfinding.canSearch = false;
						this.Pathfinding.canMove = false;
						this.StudentManager.YandereDying = true;
						this.StudentManager.StopMoving();
						this.Yandere.Blur.blurIterations = 1;
						this.Yandere.Jukebox.Volume = 0f;
						if (this.Yandere.DelinquentFighting)
						{
							this.StudentManager.CombatMinigame.Stop();
						}
					}
				}
				else if (!this.Yandere.Sprayed)
				{
					this.CharacterAnimation.CrossFade(this.ReadyToFightAnim);
				}
			}
			else
			{
				Debug.Log("A student council member is breaking up the fight.");
				this.StudentManager.CombatMinigame.Delinquent.CharacterAnimation.Play("stopFighting_00");
				this.Yandere.CharacterAnimation.Play("f02_stopFighting_00");
				this.Yandere.FightHasBrokenUp = true;
				this.Yandere.BreakUpTimer = 10f;
				this.StudentManager.CombatMinigame.Path = 7;
				this.CharacterAnimation.Play(this.BreakUpAnim);
				this.BreakingUpFight = true;
				this.SprayTimer = 1f;
			}
			this.StudentManager.CombatMinigame.DisablePrompts();
			this.StudentManager.CombatMinigame.MyVocals.Stop();
			this.StudentManager.CombatMinigame.MyAudio.Stop();
			Time.timeScale = 1f;
		}
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x001842AC File Offset: 0x001826AC
	private void DetermineCorpseLocation()
	{
		Debug.Log(this.Name + " has called the DetermineCorpseLocation() function.");
		if (this.StudentManager.Reporter == null)
		{
			this.StudentManager.Reporter = this;
		}
		if (this.Teacher)
		{
			this.StudentManager.CorpseLocation.position = this.Corpse.AllColliders[0].transform.position;
			this.StudentManager.CorpseLocation.LookAt(new Vector3(base.transform.position.x, this.StudentManager.CorpseLocation.position.y, base.transform.position.z));
			this.StudentManager.CorpseLocation.Translate(this.StudentManager.CorpseLocation.forward);
			this.StudentManager.LowerCorpsePosition();
		}
		this.Pathfinding.target = this.StudentManager.CorpseLocation;
		this.CurrentDestination = this.StudentManager.CorpseLocation;
		this.AssignCorpseGuardLocations();
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x001843D0 File Offset: 0x001827D0
	private void DetermineBloodLocation()
	{
		if (this.StudentManager.BloodReporter == null)
		{
			this.StudentManager.BloodReporter = this;
		}
		if (this.Teacher)
		{
			this.StudentManager.BloodLocation.position = this.BloodPool.transform.position;
			this.StudentManager.BloodLocation.LookAt(new Vector3(base.transform.position.x, this.StudentManager.BloodLocation.position.y, base.transform.position.z));
			this.StudentManager.BloodLocation.Translate(this.StudentManager.BloodLocation.forward);
			this.StudentManager.LowerBloodPosition();
		}
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x001844A8 File Offset: 0x001828A8
	private void AssignCorpseGuardLocations()
	{
		this.StudentManager.CorpseGuardLocation[1].position = this.StudentManager.CorpseLocation.position + new Vector3(0f, 0f, 1f);
		this.LookAway(this.StudentManager.CorpseGuardLocation[1], this.StudentManager.CorpseLocation);
		this.StudentManager.CorpseGuardLocation[2].position = this.StudentManager.CorpseLocation.position + new Vector3(1f, 0f, 0f);
		this.LookAway(this.StudentManager.CorpseGuardLocation[2], this.StudentManager.CorpseLocation);
		this.StudentManager.CorpseGuardLocation[3].position = this.StudentManager.CorpseLocation.position + new Vector3(0f, 0f, -1f);
		this.LookAway(this.StudentManager.CorpseGuardLocation[3], this.StudentManager.CorpseLocation);
		this.StudentManager.CorpseGuardLocation[4].position = this.StudentManager.CorpseLocation.position + new Vector3(-1f, 0f, 0f);
		this.LookAway(this.StudentManager.CorpseGuardLocation[4], this.StudentManager.CorpseLocation);
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x0018461C File Offset: 0x00182A1C
	private void AssignBloodGuardLocations()
	{
		this.StudentManager.BloodGuardLocation[1].position = this.StudentManager.BloodLocation.position + new Vector3(0f, 0f, 1f);
		this.LookAway(this.StudentManager.BloodGuardLocation[1], this.StudentManager.BloodLocation);
		this.StudentManager.BloodGuardLocation[2].position = this.StudentManager.BloodLocation.position + new Vector3(1f, 0f, 0f);
		this.LookAway(this.StudentManager.BloodGuardLocation[2], this.StudentManager.BloodLocation);
		this.StudentManager.BloodGuardLocation[3].position = this.StudentManager.BloodLocation.position + new Vector3(0f, 0f, -1f);
		this.LookAway(this.StudentManager.BloodGuardLocation[3], this.StudentManager.BloodLocation);
		this.StudentManager.BloodGuardLocation[4].position = this.StudentManager.BloodLocation.position + new Vector3(-1f, 0f, 0f);
		this.LookAway(this.StudentManager.BloodGuardLocation[4], this.StudentManager.BloodLocation);
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x00184790 File Offset: 0x00182B90
	private void AssignTeacherGuardLocations()
	{
		this.StudentManager.TeacherGuardLocation[1].position = this.StudentManager.CorpseLocation.position + new Vector3(0.75f, 0f, 0.75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[1], this.StudentManager.CorpseLocation);
		this.StudentManager.TeacherGuardLocation[2].position = this.StudentManager.CorpseLocation.position + new Vector3(0.75f, 0f, -0.75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[2], this.StudentManager.CorpseLocation);
		this.StudentManager.TeacherGuardLocation[3].position = this.StudentManager.CorpseLocation.position + new Vector3(-0.75f, 0f, -0.75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[3], this.StudentManager.CorpseLocation);
		this.StudentManager.TeacherGuardLocation[4].position = this.StudentManager.CorpseLocation.position + new Vector3(-0.75f, 0f, 0.75f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[4], this.StudentManager.CorpseLocation);
		this.StudentManager.TeacherGuardLocation[5].position = this.StudentManager.CorpseLocation.position + new Vector3(0f, 0f, 0.5f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[5], this.StudentManager.CorpseLocation);
		this.StudentManager.TeacherGuardLocation[6].position = this.StudentManager.CorpseLocation.position + new Vector3(0f, 0f, -0.5f);
		this.LookAway(this.StudentManager.TeacherGuardLocation[6], this.StudentManager.CorpseLocation);
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x001849B4 File Offset: 0x00182DB4
	private void LookAway(Transform T1, Transform T2)
	{
		T1.LookAt(T2);
		float y = T1.eulerAngles.y + 180f;
		T1.eulerAngles = new Vector3(T1.eulerAngles.x, y, T1.eulerAngles.z);
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x00184A08 File Offset: 0x00182E08
	public void TurnToStone()
	{
		this.Cosmetic.RightEyeRenderer.material.mainTexture = this.Yandere.Stone;
		this.Cosmetic.LeftEyeRenderer.material.mainTexture = this.Yandere.Stone;
		this.Cosmetic.HairRenderer.material.mainTexture = this.Yandere.Stone;
		if (this.Cosmetic.HairRenderer.materials.Length > 1)
		{
			this.Cosmetic.HairRenderer.materials[1].mainTexture = this.Yandere.Stone;
		}
		this.Cosmetic.RightEyeRenderer.material.color = new Color(1f, 1f, 1f, 1f);
		this.Cosmetic.LeftEyeRenderer.material.color = new Color(1f, 1f, 1f, 1f);
		this.Cosmetic.HairRenderer.material.color = new Color(1f, 1f, 1f, 1f);
		this.MyRenderer.materials[0].mainTexture = this.Yandere.Stone;
		this.MyRenderer.materials[1].mainTexture = this.Yandere.Stone;
		this.MyRenderer.materials[2].mainTexture = this.Yandere.Stone;
		if (this.Teacher && this.Cosmetic.TeacherAccessories[8].activeInHierarchy)
		{
			this.MyRenderer.materials[3].mainTexture = this.Yandere.Stone;
		}
		if (this.PickPocket != null)
		{
			this.PickPocket.enabled = false;
			this.PickPocket.Prompt.Hide();
			this.PickPocket.Prompt.enabled = false;
		}
		this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		UnityEngine.Object.Destroy(this.DetectionMarker.gameObject);
		AudioSource.PlayClipAtPoint(this.Yandere.Petrify, base.transform.position + new Vector3(0f, 1f, 0f));
		UnityEngine.Object.Instantiate<GameObject>(this.Yandere.Pebbles, this.Hips.position, Quaternion.identity);
		this.Pathfinding.enabled = false;
		this.ShoeRemoval.enabled = false;
		this.CharacterAnimation.Stop();
		this.Prompt.enabled = false;
		this.SpeechLines.Stop();
		this.Prompt.Hide();
		base.enabled = false;
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x00184CF8 File Offset: 0x001830F8
	public void StopPairing()
	{
		if (this.Actions[this.Phase] != StudentActionType.Clean && this.Persona == PersonaType.PhoneAddict && !this.LostTeacherTrust)
		{
			this.WalkAnim = this.PhoneAnims[1];
		}
		this.Paired = false;
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x00184D48 File Offset: 0x00183148
	public void ChameleonCheck()
	{
		this.ChameleonBonus = 0f;
		this.Chameleon = false;
		if (this.Yandere != null && ((this.Yandere.Persona == YanderePersonaType.Scholarly && this.Persona == PersonaType.TeachersPet) || (this.Yandere.Persona == YanderePersonaType.Scholarly && this.Club == ClubType.Science) || (this.Yandere.Persona == YanderePersonaType.Scholarly && this.Club == ClubType.Art) || (this.Yandere.Persona == YanderePersonaType.Chill && this.Persona == PersonaType.SocialButterfly) || (this.Yandere.Persona == YanderePersonaType.Chill && this.Club == ClubType.Photography) || (this.Yandere.Persona == YanderePersonaType.Chill && this.Club == ClubType.Gaming) || (this.Yandere.Persona == YanderePersonaType.Confident && this.Persona == PersonaType.Heroic) || (this.Yandere.Persona == YanderePersonaType.Confident && this.Club == ClubType.MartialArts) || (this.Yandere.Persona == YanderePersonaType.Elegant && this.Club == ClubType.Drama) || (this.Yandere.Persona == YanderePersonaType.Girly && this.Persona == PersonaType.SocialButterfly) || (this.Yandere.Persona == YanderePersonaType.Girly && this.Club == ClubType.Cooking) || (this.Yandere.Persona == YanderePersonaType.Graceful && this.Club == ClubType.Gardening) || (this.Yandere.Persona == YanderePersonaType.Haughty && this.Club == ClubType.Bully) || (this.Yandere.Persona == YanderePersonaType.Lively && this.Persona == PersonaType.SocialButterfly) || (this.Yandere.Persona == YanderePersonaType.Lively && this.Club == ClubType.LightMusic) || (this.Yandere.Persona == YanderePersonaType.Lively && this.Club == ClubType.Sports) || (this.Yandere.Persona == YanderePersonaType.Shy && this.Persona == PersonaType.Loner) || (this.Yandere.Persona == YanderePersonaType.Shy && this.Club == ClubType.Occult) || (this.Yandere.Persona == YanderePersonaType.Tough && this.Persona == PersonaType.Spiteful) || (this.Yandere.Persona == YanderePersonaType.Tough && this.Club == ClubType.Delinquent)))
		{
			Debug.Log("Chameleon is true!");
			this.ChameleonBonus = this.VisionDistance * 0.5f;
			this.Chameleon = true;
		}
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x00184FEC File Offset: 0x001833EC
	private void PhoneAddictGameOver()
	{
		if (!this.Yandere.Lost)
		{
			this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_down_22");
			this.Yandere.ShoulderCamera.HeartbrokenCamera.SetActive(true);
			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.Jukebox.GameOver();
			this.Yandere.enabled = false;
			this.Yandere.EmptyHands();
			this.Countdown.gameObject.SetActive(false);
			this.ChaseCamera.SetActive(false);
			this.Police.Heartbroken.Exposed = true;
			this.StudentManager.StopMoving();
			this.Fleeing = false;
		}
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x001850B4 File Offset: 0x001834B4
	private void EndAlarm()
	{
		Debug.Log(this.Name + " has stopped being alarmed.");
		if (this.ReturnToRoutineAfter)
		{
			this.CurrentDestination = this.Destinations[this.Phase];
			this.Pathfinding.target = this.Destinations[this.Phase];
			this.ReturnToRoutineAfter = false;
		}
		this.Pathfinding.canSearch = true;
		this.Pathfinding.canMove = true;
		if (this.StudentID == 1 || this.Teacher)
		{
			this.IgnoreTimer = 0.0001f;
		}
		else
		{
			this.IgnoreTimer = 5f;
		}
		if (this.Persona == PersonaType.PhoneAddict)
		{
			this.SmartPhone.SetActive(true);
		}
		this.FocusOnYandere = false;
		this.DiscCheck = false;
		this.Alarmed = false;
		this.Reacted = false;
		this.Hesitation = 0f;
		this.AlarmTimer = 0f;
		if (this.WitnessedCorpse)
		{
			this.PersonaReaction();
		}
		else if (this.WitnessedBloodPool || this.WitnessedLimb || this.WitnessedWeapon)
		{
			if (this.Following)
			{
				this.Hearts.emission.enabled = false;
				this.Yandere.Followers--;
				this.Following = false;
			}
			this.CharacterAnimation.CrossFade(this.WalkAnim);
			this.CurrentDestination = this.BloodPool;
			this.Pathfinding.target = this.BloodPool;
			this.Pathfinding.canSearch = true;
			this.Pathfinding.canMove = true;
			this.Pathfinding.speed = 1f;
			this.InvestigatingBloodPool = true;
			this.Routine = false;
			this.IgnoreTimer = 0.0001f;
		}
		else if (!this.Following && !this.Wet && !this.Investigating)
		{
			this.Routine = true;
		}
		if (this.ResumeDistracting)
		{
			this.CharacterAnimation.CrossFade(this.WalkAnim);
			this.Distracting = true;
			this.Routine = false;
		}
		if (this.CurrentAction == StudentActionType.Clean)
		{
			this.SmartPhone.SetActive(false);
			this.Scrubber.SetActive(true);
			if (this.CleaningRole == 5)
			{
				this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
				this.Eraser.SetActive(true);
			}
		}
		if (this.TurnOffRadio)
		{
			this.Routine = false;
		}
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x00185358 File Offset: 0x00183758
	public void GetSleuthTarget()
	{
		this.TargetDistance = 2f;
		this.SleuthID++;
		if (this.SleuthID < 98)
		{
			if (this.StudentManager.Students[this.SleuthID] == null)
			{
				this.GetSleuthTarget();
			}
			else if (!this.StudentManager.Students[this.SleuthID].gameObject.activeInHierarchy)
			{
				this.GetSleuthTarget();
			}
			else
			{
				this.SleuthTarget = this.StudentManager.Students[this.SleuthID].transform;
				this.Pathfinding.target = this.SleuthTarget;
				this.CurrentDestination = this.SleuthTarget;
			}
		}
		else if (this.SleuthID == 98)
		{
			if (ClubGlobals.Club == ClubType.Photography)
			{
				this.SleuthID = 0;
				this.GetSleuthTarget();
			}
			else
			{
				this.SleuthTarget = this.Yandere.transform;
				this.Pathfinding.target = this.SleuthTarget;
				this.CurrentDestination = this.SleuthTarget;
			}
		}
		else
		{
			this.SleuthID = 0;
			this.GetSleuthTarget();
		}
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x00185488 File Offset: 0x00183888
	public void GetFoodTarget()
	{
		this.Attempts++;
		if (this.Attempts >= 100)
		{
			this.Phase++;
		}
		else
		{
			this.SleuthID++;
			if (this.SleuthID < 90)
			{
				if (this.SleuthID == this.StudentID)
				{
					this.GetFoodTarget();
				}
				else if (this.StudentManager.Students[this.SleuthID] == null)
				{
					this.GetFoodTarget();
				}
				else if (!this.StudentManager.Students[this.SleuthID].gameObject.activeInHierarchy)
				{
					this.GetFoodTarget();
				}
				else if (this.StudentManager.Students[this.SleuthID].CurrentAction == StudentActionType.SitAndEatBento || this.StudentManager.Students[this.SleuthID].Club == ClubType.Cooking || this.StudentManager.Students[this.SleuthID].Club == ClubType.Delinquent || this.StudentManager.Students[this.SleuthID].Club == ClubType.Sports || this.StudentManager.Students[this.SleuthID].TargetedForDistraction || this.StudentManager.Students[this.SleuthID].ClubActivityPhase >= 16 || this.StudentManager.Students[this.SleuthID].InEvent || !this.StudentManager.Students[this.SleuthID].Routine || this.StudentManager.Students[this.SleuthID].Posing || this.StudentManager.Students[this.SleuthID].Slave || this.StudentManager.Students[this.SleuthID].Wet || (this.StudentManager.Students[this.SleuthID].Club == ClubType.LightMusic && this.StudentManager.PracticeMusic.isPlaying))
				{
					this.GetFoodTarget();
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.WalkAnim);
					this.DistractionTarget = this.StudentManager.Students[this.SleuthID];
					this.DistractionTarget.TargetedForDistraction = true;
					this.SleuthTarget = this.StudentManager.Students[this.SleuthID].transform;
					this.Pathfinding.target = this.SleuthTarget;
					this.CurrentDestination = this.SleuthTarget;
					this.TargetDistance = 0.75f;
					this.DistractTimer = 8f;
					this.Distracting = true;
					this.CanTalk = false;
					this.Routine = false;
					this.Attempts = 0;
				}
			}
			else
			{
				this.SleuthID = 0;
				this.GetFoodTarget();
			}
		}
	}

	// Token: 0x06002131 RID: 8497 RVA: 0x0018577C File Offset: 0x00183B7C
	private void PhoneAddictCameraUpdate()
	{
		if (this.SmartPhone.transform.parent != null)
		{
			this.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			this.SmartPhone.transform.localPosition = new Vector3(0f, 0.005f, -0.01f);
			this.SmartPhone.transform.localEulerAngles = new Vector3(7.33333f, -154f, 173.666656f);
			this.SmartPhone.SetActive(true);
			if (this.Sleuthing)
			{
				if (this.AlarmTimer < 2f)
				{
					this.AlarmTimer = 2f;
					this.ScaredAnim = this.SleuthReactAnim;
					this.SprintAnim = this.SleuthReportAnim;
					this.CharacterAnimation.CrossFade(this.ScaredAnim);
				}
				if (!this.CameraFlash.activeInHierarchy && this.CharacterAnimation[this.ScaredAnim].time > 2f)
				{
					this.CameraFlash.SetActive(true);
					if (this.Yandere.Mask != null)
					{
						this.Countdown.MaskedPhoto = true;
					}
				}
			}
			else
			{
				this.ScaredAnim = this.PhoneAnims[4];
				this.CharacterAnimation.CrossFade(this.ScaredAnim);
				if (!this.CameraFlash.activeInHierarchy && (double)this.CharacterAnimation[this.ScaredAnim].time > 3.66666)
				{
					this.CameraFlash.SetActive(true);
					if (this.Yandere.Mask != null)
					{
						this.Countdown.MaskedPhoto = true;
					}
					else if (this.Grudge)
					{
						this.Police.PhotoEvidence++;
						this.PhotoEvidence = true;
					}
				}
			}
		}
	}

	// Token: 0x06002132 RID: 8498 RVA: 0x00185964 File Offset: 0x00183D64
	private void ReturnToRoutine()
	{
		if (this.Actions[this.Phase] == StudentActionType.Patrol)
		{
			this.CurrentDestination = this.StudentManager.Patrols.List[this.StudentID].GetChild(this.PatrolID);
			this.Pathfinding.target = this.CurrentDestination;
		}
		else
		{
			this.CurrentDestination = this.Destinations[this.Phase];
			this.Pathfinding.target = this.Destinations[this.Phase];
		}
		this.BreakingUpFight = false;
		this.WitnessedMurder = false;
		this.Pathfinding.speed = 1f;
		this.Prompt.enabled = true;
		this.Alarmed = false;
		this.Fleeing = false;
		this.Routine = true;
		this.Grudge = false;
	}

	// Token: 0x06002133 RID: 8499 RVA: 0x00185A34 File Offset: 0x00183E34
	public void EmptyHands()
	{
		bool flag = false;
		if ((this.SentHome && this.SmartPhone.activeInHierarchy) || this.PhotoEvidence || (this.Persona == PersonaType.PhoneAddict && !this.Dying && !this.Wet))
		{
			flag = true;
		}
		if (this.MyPlate != null && this.MyPlate.parent != null)
		{
			if (this.WitnessedMurder || this.WitnessedCorpse)
			{
				this.DropPlate();
			}
			else
			{
				this.MyPlate.gameObject.SetActive(false);
			}
		}
		if (this.Club == ClubType.Gardening)
		{
			this.WateringCan.transform.parent = this.Hips;
			this.WateringCan.transform.localPosition = new Vector3(0f, 0.0135f, -0.184f);
			this.WateringCan.transform.localEulerAngles = new Vector3(0f, 90f, 30f);
		}
		if (this.Club == ClubType.LightMusic)
		{
			if (this.StudentID == 51)
			{
				if (this.InstrumentBag[this.ClubMemberID].transform.parent == null)
				{
					this.Instruments[this.ClubMemberID].transform.parent = null;
					this.Instruments[this.ClubMemberID].transform.position = new Vector3(-0.5f, 4.5f, 22.45666f);
					this.Instruments[this.ClubMemberID].transform.eulerAngles = new Vector3(-15f, 0f, 0f);
					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().playOnAwake = false;
					this.Instruments[this.ClubMemberID].GetComponent<AudioSource>().Stop();
				}
				else
				{
					this.Instruments[this.ClubMemberID].SetActive(false);
				}
			}
			else
			{
				this.Instruments[this.ClubMemberID].SetActive(false);
			}
			this.Drumsticks[0].SetActive(false);
			this.Drumsticks[1].SetActive(false);
			this.AirGuitar.Stop();
		}
		if (!this.Male)
		{
			this.Handkerchief.SetActive(false);
			this.Cigarette.SetActive(false);
			this.Lighter.SetActive(false);
		}
		else
		{
			this.PinkSeifuku.SetActive(false);
		}
		if (!flag)
		{
			this.SmartPhone.SetActive(false);
		}
		if (this.BagOfChips != null)
		{
			this.BagOfChips.SetActive(false);
		}
		this.Chopsticks[0].SetActive(false);
		this.Chopsticks[1].SetActive(false);
		this.Sketchbook.SetActive(false);
		this.OccultBook.SetActive(false);
		this.Paintbrush.SetActive(false);
		this.EventBook.SetActive(false);
		this.Scrubber.SetActive(false);
		this.Octodog.SetActive(false);
		this.Palette.SetActive(false);
		this.Eraser.SetActive(false);
		this.Pencil.SetActive(false);
		this.Bento.SetActive(false);
		this.Pen.SetActive(false);
		foreach (GameObject gameObject in this.ScienceProps)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
		foreach (GameObject gameObject2 in this.Fingerfood)
		{
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x00185E00 File Offset: 0x00184200
	public void UpdateAnimLayers()
	{
		this.CharacterAnimation[this.LeanAnim].speed += (float)this.StudentID * 0.01f;
		this.CharacterAnimation[this.ConfusedSitAnim].speed += (float)this.StudentID * 0.01f;
		this.CharacterAnimation[this.WalkAnim].time = UnityEngine.Random.Range(0f, this.CharacterAnimation[this.WalkAnim].length);
		this.CharacterAnimation[this.WetAnim].layer = 9;
		this.CharacterAnimation.Play(this.WetAnim);
		this.CharacterAnimation[this.WetAnim].weight = 0f;
		if (!this.Male)
		{
			this.CharacterAnimation[this.StripAnim].speed = 1.5f;
			this.CharacterAnimation[this.GameAnim].speed = 2f;
			this.CharacterAnimation["f02_moLipSync_00"].layer = 9;
			this.CharacterAnimation.Play("f02_moLipSync_00");
			this.CharacterAnimation["f02_moLipSync_00"].weight = 0f;
			this.CharacterAnimation["f02_topHalfTexting_00"].layer = 8;
			this.CharacterAnimation.Play("f02_topHalfTexting_00");
			this.CharacterAnimation["f02_topHalfTexting_00"].weight = 0f;
			this.CharacterAnimation[this.CarryAnim].layer = 7;
			this.CharacterAnimation.Play(this.CarryAnim);
			this.CharacterAnimation[this.CarryAnim].weight = 0f;
			this.CharacterAnimation[this.SocialSitAnim].layer = 6;
			this.CharacterAnimation.Play(this.SocialSitAnim);
			this.CharacterAnimation[this.SocialSitAnim].weight = 0f;
			this.CharacterAnimation[this.ShyAnim].layer = 5;
			this.CharacterAnimation.Play(this.ShyAnim);
			this.CharacterAnimation[this.ShyAnim].weight = 0f;
			this.CharacterAnimation[this.FistAnim].layer = 4;
			this.CharacterAnimation[this.FistAnim].weight = 0f;
			this.CharacterAnimation[this.BentoAnim].layer = 3;
			this.CharacterAnimation.Play(this.BentoAnim);
			this.CharacterAnimation[this.BentoAnim].weight = 0f;
			this.CharacterAnimation[this.AngryFaceAnim].layer = 2;
			this.CharacterAnimation.Play(this.AngryFaceAnim);
			this.CharacterAnimation[this.AngryFaceAnim].weight = 0f;
			this.CharacterAnimation["f02_wetIdle_00"].speed = 1.25f;
			this.CharacterAnimation["f02_sleuthScan_00"].speed = 1.4f;
		}
		else
		{
			this.CharacterAnimation[this.ConfusedSitAnim].speed *= -1f;
			this.CharacterAnimation[this.ToughFaceAnim].layer = 7;
			this.CharacterAnimation.Play(this.ToughFaceAnim);
			this.CharacterAnimation[this.ToughFaceAnim].weight = 0f;
			this.CharacterAnimation[this.SocialSitAnim].layer = 6;
			this.CharacterAnimation.Play(this.SocialSitAnim);
			this.CharacterAnimation[this.SocialSitAnim].weight = 0f;
			this.CharacterAnimation[this.CarryShoulderAnim].layer = 5;
			this.CharacterAnimation.Play(this.CarryShoulderAnim);
			this.CharacterAnimation[this.CarryShoulderAnim].weight = 0f;
			this.CharacterAnimation["scaredFace_00"].layer = 4;
			this.CharacterAnimation.Play("scaredFace_00");
			this.CharacterAnimation["scaredFace_00"].weight = 0f;
			this.CharacterAnimation[this.SadFaceAnim].layer = 3;
			this.CharacterAnimation.Play(this.SadFaceAnim);
			this.CharacterAnimation[this.SadFaceAnim].weight = 0f;
			this.CharacterAnimation[this.AngryFaceAnim].layer = 2;
			this.CharacterAnimation.Play(this.AngryFaceAnim);
			this.CharacterAnimation[this.AngryFaceAnim].weight = 0f;
			this.CharacterAnimation["sleuthScan_00"].speed = 1.4f;
		}
		if (this.Persona == PersonaType.Sleuth)
		{
			this.CharacterAnimation[this.WalkAnim].time = UnityEngine.Random.Range(0f, this.CharacterAnimation[this.WalkAnim].length);
		}
		if (this.Club == ClubType.Bully)
		{
			if (!StudentGlobals.GetStudentBroken(this.StudentID) && this.BullyID > 1)
			{
				this.CharacterAnimation["f02_bullyLaugh_00"].speed = 0.9f + (float)this.BullyID * 0.1f;
			}
		}
		else if (this.Club == ClubType.Delinquent)
		{
			this.CharacterAnimation[this.WalkAnim].time = UnityEngine.Random.Range(0f, this.CharacterAnimation[this.WalkAnim].length);
			this.CharacterAnimation[this.LeanAnim].speed = 0.5f;
		}
		else if (this.Club == ClubType.Council)
		{
			this.CharacterAnimation["f02_faceCouncil" + this.Suffix + "_00"].layer = 10;
			this.CharacterAnimation.Play("f02_faceCouncil" + this.Suffix + "_00");
		}
		else if (this.Club == ClubType.Gaming)
		{
			this.CharacterAnimation[this.VictoryAnim].speed -= 0.1f * (float)(this.StudentID - 36);
			this.CharacterAnimation[this.VictoryAnim].speed = 0.866666f;
		}
		else if (this.Club == ClubType.Cooking && this.ClubActivityPhase > 0)
		{
			Debug.Log("This is a cooking club member, and they should be performing the ''PlateWalkAnim''.");
			this.WalkAnim = this.PlateWalkAnim;
		}
		if (this.StudentID == 36)
		{
			this.CharacterAnimation[this.ToughFaceAnim].weight = 1f;
		}
		else if (this.StudentID == 66)
		{
			this.CharacterAnimation[this.ToughFaceAnim].weight = 1f;
		}
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x00186564 File Offset: 0x00184964
	private void SpawnDetectionMarker()
	{
		this.DetectionMarker = UnityEngine.Object.Instantiate<GameObject>(this.Marker, GameObject.Find("DetectionPanel").transform.position, Quaternion.identity).GetComponent<DetectionMarkerScript>();
		this.DetectionMarker.transform.parent = GameObject.Find("DetectionPanel").transform;
		this.DetectionMarker.Target = base.transform;
	}

	// Token: 0x06002136 RID: 8502 RVA: 0x001865D0 File Offset: 0x001849D0
	public void EquipCleaningItems()
	{
		if (this.CurrentAction == StudentActionType.Clean)
		{
			if (this.Persona == PersonaType.PhoneAddict || this.Persona == PersonaType.Sleuth)
			{
				this.WalkAnim = this.OriginalWalkAnim;
			}
			this.SmartPhone.SetActive(false);
			this.Scrubber.SetActive(true);
			if (this.CleaningRole == 5)
			{
				this.Scrubber.GetComponent<Renderer>().material.mainTexture = this.Eraser.GetComponent<Renderer>().material.mainTexture;
				this.Eraser.SetActive(true);
			}
			if (this.StudentID == 9 || this.StudentID == 60)
			{
				this.WalkAnim = this.OriginalOriginalWalkAnim;
			}
		}
	}

	// Token: 0x06002137 RID: 8503 RVA: 0x00186690 File Offset: 0x00184A90
	public void DetermineWhatWasWitnessed()
	{
		Debug.Log("We are now determining what " + this.Name + " witnessed.");
		if (this.Witnessed == StudentWitnessType.Murder)
		{
			Debug.Log("No need to go through the entire chain. We already know that this character witnessed murder.");
			this.Concern = 5;
		}
		else if (this.YandereVisible)
		{
			bool flag = false;
			if (this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint)
			{
				flag = true;
			}
			bool flag2 = this.Yandere.Armed && this.Yandere.EquippedWeapon.Suspicious;
			bool flag3 = this.Yandere.PickUp != null && this.Yandere.PickUp.Suspicious;
			bool flag4 = this.Yandere.PickUp != null && this.Yandere.PickUp.BodyPart != null;
			bool flag5 = this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence;
			int concern = this.Concern;
			if (flag2)
			{
				this.WeaponToTakeAway = this.Yandere.EquippedWeapon;
			}
			if (this.Yandere.Rummaging || this.Yandere.TheftTimer > 0f)
			{
				Debug.Log("Saw Yandere-chan stealing.");
				this.Witnessed = StudentWitnessType.Theft;
				this.Concern = 5;
			}
			else if (this.Yandere.Pickpocketing || this.Yandere.Caught)
			{
				Debug.Log("Saw Yandere-chan pickpocketing.");
				this.Witnessed = StudentWitnessType.Pickpocketing;
				this.Concern = 5;
				this.Yandere.StudentManager.PickpocketMinigame.Failure = true;
				this.Yandere.StudentManager.PickpocketMinigame.End();
				this.Yandere.Caught = true;
				if (this.Teacher)
				{
					this.Witnessed = StudentWitnessType.Theft;
				}
			}
			else if (flag2 && flag && this.Yandere.Sanity < 33.333f)
			{
				this.Witnessed = StudentWitnessType.WeaponAndBloodAndInsanity;
				this.RepLoss = 30f;
				this.Concern = 5;
			}
			else if (flag2 && this.Yandere.Sanity < 33.333f)
			{
				this.Witnessed = StudentWitnessType.WeaponAndInsanity;
				this.RepLoss = 20f;
				this.Concern = 5;
			}
			else if (flag && this.Yandere.Sanity < 33.333f)
			{
				this.Witnessed = StudentWitnessType.BloodAndInsanity;
				this.RepLoss = 20f;
				this.Concern = 5;
			}
			else if (flag2 && flag)
			{
				this.Witnessed = StudentWitnessType.WeaponAndBlood;
				this.RepLoss = 20f;
				this.Concern = 5;
			}
			else if (flag2)
			{
				Debug.Log("Saw Yandere-chan with a weapon.");
				this.WeaponWitnessed = this.Yandere.EquippedWeapon.WeaponID;
				this.Witnessed = StudentWitnessType.Weapon;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (flag3)
			{
				if (this.Yandere.PickUp.CleaningProduct)
				{
					this.Witnessed = StudentWitnessType.CleaningItem;
				}
				else if (this.Teacher)
				{
					this.Witnessed = StudentWitnessType.Suspicious;
				}
				else
				{
					this.Witnessed = StudentWitnessType.Weapon;
				}
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (flag)
			{
				this.Witnessed = StudentWitnessType.Blood;
				if (!this.Bloody)
				{
					this.RepLoss = 10f;
					this.Concern = 5;
				}
				else
				{
					this.RepLoss = 0f;
					this.Concern = 0;
				}
			}
			else if (this.Yandere.Sanity < 33.333f)
			{
				this.Witnessed = StudentWitnessType.Insanity;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (this.Yandere.Lewd)
			{
				this.Witnessed = StudentWitnessType.Lewd;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if ((this.Yandere.Laughing && this.Yandere.LaughIntensity > 15f) || this.Yandere.Stance.Current == StanceType.Crouching || this.Yandere.Stance.Current == StanceType.Crawling)
			{
				this.Witnessed = StudentWitnessType.Insanity;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (this.Yandere.Poisoning)
			{
				this.Witnessed = StudentWitnessType.Poisoning;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (this.Yandere.Trespassing && this.StudentID > 1)
			{
				this.Witnessed = ((!this.Private) ? StudentWitnessType.Trespassing : StudentWitnessType.Interruption);
				this.Witness = false;
				this.Concern++;
			}
			else if (this.Yandere.NearSenpai)
			{
				this.Witnessed = StudentWitnessType.Stalking;
				this.Concern++;
			}
			else if (this.Yandere.Eavesdropping)
			{
				if (this.StudentID == 1)
				{
					this.Witnessed = StudentWitnessType.Stalking;
					this.Concern++;
				}
				else
				{
					if (this.InEvent)
					{
						this.EventInterrupted = true;
					}
					this.Witnessed = StudentWitnessType.Eavesdropping;
					this.RepLoss = 10f;
					this.Concern = 5;
				}
			}
			else if (this.Yandere.Aiming)
			{
				this.Witnessed = StudentWitnessType.Stalking;
				this.Concern++;
			}
			else if (this.Yandere.DelinquentFighting)
			{
				this.Witnessed = StudentWitnessType.Violence;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (this.Yandere.PickUp != null && this.Yandere.PickUp.Clothing && this.Yandere.PickUp.Evidence)
			{
				Debug.Log("Saw Yandere-chan with bloody clothing.");
				this.Witnessed = StudentWitnessType.HoldingBloodyClothing;
				this.RepLoss = 10f;
				this.Concern = 5;
			}
			else if (flag4 || flag5)
			{
				this.Witnessed = StudentWitnessType.CoverUp;
			}
			if (this.StudentID == 1 && this.Witnessed == StudentWitnessType.Insanity && (this.Yandere.Stance.Current == StanceType.Crouching || this.Yandere.Stance.Current == StanceType.Crawling))
			{
				this.Witnessed = StudentWitnessType.Stalking;
				this.Concern = concern;
				this.Concern++;
			}
		}
		else
		{
			Debug.Log(this.Name + " is reacting to something other than Yandere-chan.");
			if (this.WitnessedLimb)
			{
				this.Witnessed = StudentWitnessType.SeveredLimb;
			}
			else if (this.WitnessedBloodyWeapon)
			{
				this.Witnessed = StudentWitnessType.BloodyWeapon;
			}
			else if (this.WitnessedBloodPool)
			{
				this.Witnessed = StudentWitnessType.BloodPool;
			}
			else if (this.WitnessedWeapon)
			{
				this.Witnessed = StudentWitnessType.DroppedWeapon;
			}
			else if (this.WitnessedCorpse)
			{
				this.Witnessed = StudentWitnessType.Corpse;
			}
			else
			{
				Debug.Log("Apparently, we didn't even see anything! 1");
				this.Witnessed = StudentWitnessType.None;
				this.DiscCheck = true;
				this.Witness = false;
			}
		}
		if (this.Concern == 5 && this.Club == ClubType.Council)
		{
			Debug.Log("A member of the student council is being transformed into a teacher.");
			this.Teacher = true;
		}
	}

	// Token: 0x06002138 RID: 8504 RVA: 0x00186E80 File Offset: 0x00185280
	public void DetermineTeacherSubtitle()
	{
		Debug.Log("We are now determining what line of dialogue the teacher should say.");
		if (this.Club == ClubType.Council)
		{
			this.Subtitle.UpdateLabel(SubtitleType.CouncilToCounselor, this.ClubMemberID, 5f);
		}
		else
		{
			if (this.Guarding)
			{
				if (this.Yandere.Bloodiness + (float)this.Yandere.GloveBlood > 0f && !this.Yandere.Paint)
				{
					this.Witnessed = StudentWitnessType.Blood;
				}
				else if (this.Yandere.Armed)
				{
					this.Witnessed = StudentWitnessType.Weapon;
				}
				else if (this.Yandere.Sanity < 66.66666f)
				{
					this.Witnessed = StudentWitnessType.Insanity;
				}
			}
			if (this.Witnessed == StudentWitnessType.Murder)
			{
				if (this.WitnessedMindBrokenMurder)
				{
					this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, 4, 6f);
				}
				else
				{
					this.Subtitle.UpdateLabel(SubtitleType.TeacherMurderReaction, 1, 6f);
				}
				this.GameOverCause = GameOverType.Murder;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.WeaponAndBloodAndInsanity)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.WeaponAndBlood)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponHostile, 1, 6f);
				this.GameOverCause = GameOverType.Weapon;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.WeaponAndInsanity)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.BloodAndInsanity)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Weapon)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherWeaponHostile, 1, 6f);
				this.GameOverCause = GameOverType.Weapon;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Blood)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherBloodHostile, 1, 6f);
				this.GameOverCause = GameOverType.Blood;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Insanity || this.Witnessed == StudentWitnessType.Poisoning)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityHostile, 1, 6f);
				this.GameOverCause = GameOverType.Insanity;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.Lewd)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherLewdReaction, 1, 6f);
				this.GameOverCause = GameOverType.Lewd;
			}
			else if (this.Witnessed == StudentWitnessType.Trespassing)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherTrespassingReaction, this.Concern, 5f);
			}
			else if (this.Witnessed == StudentWitnessType.Corpse)
			{
				Debug.Log("A teacher just discovered a corpse.");
				this.DetermineCorpseLocation();
				this.Subtitle.UpdateLabel(SubtitleType.TeacherCorpseReaction, 1, 3f);
				this.Police.Called = true;
			}
			else if (this.Witnessed == StudentWitnessType.CoverUp)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherCoverUpHostile, 1, 6f);
				this.GameOverCause = GameOverType.Blood;
				this.WitnessedMurder = true;
			}
			else if (this.Witnessed == StudentWitnessType.CleaningItem)
			{
				this.Subtitle.UpdateLabel(SubtitleType.TeacherInsanityReaction, 1, 6f);
				this.GameOverCause = GameOverType.Insanity;
			}
		}
	}

	// Token: 0x06002139 RID: 8505 RVA: 0x00187224 File Offset: 0x00185624
	public void ReturnMisplacedWeapon()
	{
		Debug.Log(this.Name + " has returned a misplaced weapon.");
		if (this.StudentManager.BloodReporter == this)
		{
			this.StudentManager.BloodReporter = null;
		}
		this.BloodPool.parent = null;
		this.BloodPool.position = this.BloodPool.GetComponent<WeaponScript>().StartingPosition;
		this.BloodPool.eulerAngles = this.BloodPool.GetComponent<WeaponScript>().StartingRotation;
		this.BloodPool.GetComponent<WeaponScript>().Prompt.enabled = true;
		this.BloodPool.GetComponent<WeaponScript>().enabled = true;
		this.BloodPool.GetComponent<WeaponScript>().Drop();
		this.BloodPool.GetComponent<WeaponScript>().MyRigidbody.useGravity = false;
		this.BloodPool.GetComponent<WeaponScript>().MyRigidbody.isKinematic = true;
		this.BloodPool.GetComponent<WeaponScript>().Returner = null;
		this.BloodPool = null;
		this.CurrentDestination = this.Destinations[this.Phase];
		this.Pathfinding.target = this.Destinations[this.Phase];
		if (this.Club == ClubType.Council || this.Teacher)
		{
			this.Handkerchief.SetActive(false);
		}
		this.Pathfinding.speed = 1f;
		this.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.ReturningMisplacedWeapon = false;
		this.WitnessedSomething = false;
		this.WitnessedWeapon = false;
		this.ReportingBlood = false;
		this.Distracted = false;
		this.Routine = true;
		this.ReturningMisplacedWeaponPhase = 0;
		this.WitnessCooldownTimer = 0f;
	}

	// Token: 0x04002FCD RID: 12237
	public Quaternion targetRotation;

	// Token: 0x04002FCE RID: 12238
	public Quaternion OriginalRotation;

	// Token: 0x04002FCF RID: 12239
	public Quaternion OriginalPlateRotation;

	// Token: 0x04002FD0 RID: 12240
	public SelectiveGrayscale ChaseSelectiveGrayscale;

	// Token: 0x04002FD1 RID: 12241
	public DrinkingFountainScript DrinkingFountain;

	// Token: 0x04002FD2 RID: 12242
	public DetectionMarkerScript DetectionMarker;

	// Token: 0x04002FD3 RID: 12243
	public ChemistScannerScript ChemistScanner;

	// Token: 0x04002FD4 RID: 12244
	public StudentManagerScript StudentManager;

	// Token: 0x04002FD5 RID: 12245
	public CameraEffectsScript CameraEffects;

	// Token: 0x04002FD6 RID: 12246
	public ChangingBoothScript ChangingBooth;

	// Token: 0x04002FD7 RID: 12247
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04002FD8 RID: 12248
	public WitnessCameraScript WitnessCamera;

	// Token: 0x04002FD9 RID: 12249
	public StudentScript DistractionTarget;

	// Token: 0x04002FDA RID: 12250
	public CookingEventScript CookingEvent;

	// Token: 0x04002FDB RID: 12251
	public EventManagerScript EventManager;

	// Token: 0x04002FDC RID: 12252
	public GradingPaperScript GradingPaper;

	// Token: 0x04002FDD RID: 12253
	public ClubManagerScript ClubManager;

	// Token: 0x04002FDE RID: 12254
	public LightSwitchScript LightSwitch;

	// Token: 0x04002FDF RID: 12255
	public MovingEventScript MovingEvent;

	// Token: 0x04002FE0 RID: 12256
	public ShoeRemovalScript ShoeRemoval;

	// Token: 0x04002FE1 RID: 12257
	public StruggleBarScript StruggleBar;

	// Token: 0x04002FE2 RID: 12258
	public ToiletEventScript ToiletEvent;

	// Token: 0x04002FE3 RID: 12259
	public WeaponScript WeaponToTakeAway;

	// Token: 0x04002FE4 RID: 12260
	public DynamicGridObstacle Obstacle;

	// Token: 0x04002FE5 RID: 12261
	public PhoneEventScript PhoneEvent;

	// Token: 0x04002FE6 RID: 12262
	public PickpocketScript PickPocket;

	// Token: 0x04002FE7 RID: 12263
	public ReputationScript Reputation;

	// Token: 0x04002FE8 RID: 12264
	public StudentScript TargetStudent;

	// Token: 0x04002FE9 RID: 12265
	public GenericBentoScript MyBento;

	// Token: 0x04002FEA RID: 12266
	public StudentScript FollowTarget;

	// Token: 0x04002FEB RID: 12267
	public CountdownScript Countdown;

	// Token: 0x04002FEC RID: 12268
	public Renderer SmartPhoneScreen;

	// Token: 0x04002FED RID: 12269
	public StudentScript Distractor;

	// Token: 0x04002FEE RID: 12270
	public StudentScript HuntTarget;

	// Token: 0x04002FEF RID: 12271
	public StudentScript MyReporter;

	// Token: 0x04002FF0 RID: 12272
	public StudentScript MyTeacher;

	// Token: 0x04002FF1 RID: 12273
	public BoneSetsScript BoneSets;

	// Token: 0x04002FF2 RID: 12274
	public CosmeticScript Cosmetic;

	// Token: 0x04002FF3 RID: 12275
	public SaveLoadScript SaveLoad;

	// Token: 0x04002FF4 RID: 12276
	public SubtitleScript Subtitle;

	// Token: 0x04002FF5 RID: 12277
	public StudentScript Follower;

	// Token: 0x04002FF6 RID: 12278
	public DynamicBone OsanaHairL;

	// Token: 0x04002FF7 RID: 12279
	public DynamicBone OsanaHairR;

	// Token: 0x04002FF8 RID: 12280
	public ARMiyukiScript Miyuki;

	// Token: 0x04002FF9 RID: 12281
	public WeaponScript MyWeapon;

	// Token: 0x04002FFA RID: 12282
	public StudentScript Partner;

	// Token: 0x04002FFB RID: 12283
	public RagdollScript Ragdoll;

	// Token: 0x04002FFC RID: 12284
	public YandereScript Yandere;

	// Token: 0x04002FFD RID: 12285
	public Camera DramaticCamera;

	// Token: 0x04002FFE RID: 12286
	public RagdollScript Corpse;

	// Token: 0x04002FFF RID: 12287
	public StudentScript Hunter;

	// Token: 0x04003000 RID: 12288
	public DoorScript VomitDoor;

	// Token: 0x04003001 RID: 12289
	public BrokenScript Broken;

	// Token: 0x04003002 RID: 12290
	public PoliceScript Police;

	// Token: 0x04003003 RID: 12291
	public PromptScript Prompt;

	// Token: 0x04003004 RID: 12292
	public AIPath Pathfinding;

	// Token: 0x04003005 RID: 12293
	public TalkingScript Talk;

	// Token: 0x04003006 RID: 12294
	public CheerScript Cheer;

	// Token: 0x04003007 RID: 12295
	public ClockScript Clock;

	// Token: 0x04003008 RID: 12296
	public RadioScript Radio;

	// Token: 0x04003009 RID: 12297
	public Renderer Painting;

	// Token: 0x0400300A RID: 12298
	public JsonScript JSON;

	// Token: 0x0400300B RID: 12299
	public SuckScript Suck;

	// Token: 0x0400300C RID: 12300
	public Renderer Tears;

	// Token: 0x0400300D RID: 12301
	public Rigidbody MyRigidbody;

	// Token: 0x0400300E RID: 12302
	public Collider HorudaCollider;

	// Token: 0x0400300F RID: 12303
	public Collider MyCollider;

	// Token: 0x04003010 RID: 12304
	public CharacterController MyController;

	// Token: 0x04003011 RID: 12305
	public Animation CharacterAnimation;

	// Token: 0x04003012 RID: 12306
	public Projector LiquidProjector;

	// Token: 0x04003013 RID: 12307
	public float VisionFOV;

	// Token: 0x04003014 RID: 12308
	public float VisionDistance;

	// Token: 0x04003015 RID: 12309
	public ParticleSystem DelinquentSpeechLines;

	// Token: 0x04003016 RID: 12310
	public ParticleSystem PepperSprayEffect;

	// Token: 0x04003017 RID: 12311
	public ParticleSystem DrowningSplashes;

	// Token: 0x04003018 RID: 12312
	public ParticleSystem BloodFountain;

	// Token: 0x04003019 RID: 12313
	public ParticleSystem VomitEmitter;

	// Token: 0x0400301A RID: 12314
	public ParticleSystem SpeechLines;

	// Token: 0x0400301B RID: 12315
	public ParticleSystem BullyDust;

	// Token: 0x0400301C RID: 12316
	public ParticleSystem ChalkDust;

	// Token: 0x0400301D RID: 12317
	public ParticleSystem Hearts;

	// Token: 0x0400301E RID: 12318
	public Texture KokonaPhoneTexture;

	// Token: 0x0400301F RID: 12319
	public Texture MidoriPhoneTexture;

	// Token: 0x04003020 RID: 12320
	public Texture OsanaPhoneTexture;

	// Token: 0x04003021 RID: 12321
	public Texture RedBookTexture;

	// Token: 0x04003022 RID: 12322
	public Texture BloodTexture;

	// Token: 0x04003023 RID: 12323
	public Texture WaterTexture;

	// Token: 0x04003024 RID: 12324
	public Texture GasTexture;

	// Token: 0x04003025 RID: 12325
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x04003026 RID: 12326
	public Renderer BookRenderer;

	// Token: 0x04003027 RID: 12327
	public Transform LastSuspiciousObject2;

	// Token: 0x04003028 RID: 12328
	public Transform LastSuspiciousObject;

	// Token: 0x04003029 RID: 12329
	public Transform CurrentDestination;

	// Token: 0x0400302A RID: 12330
	public Transform LeftMiddleFinger;

	// Token: 0x0400302B RID: 12331
	public Transform WeaponBagParent;

	// Token: 0x0400302C RID: 12332
	public Transform LeftItemParent;

	// Token: 0x0400302D RID: 12333
	public Transform PetDestination;

	// Token: 0x0400302E RID: 12334
	public Transform SketchPosition;

	// Token: 0x0400302F RID: 12335
	public Transform CleaningSpot;

	// Token: 0x04003030 RID: 12336
	public Transform SleuthTarget;

	// Token: 0x04003031 RID: 12337
	public Transform Distraction;

	// Token: 0x04003032 RID: 12338
	public Transform StalkTarget;

	// Token: 0x04003033 RID: 12339
	public Transform ItemParent;

	// Token: 0x04003034 RID: 12340
	public Transform WitnessPOV;

	// Token: 0x04003035 RID: 12341
	public Transform RightDrill;

	// Token: 0x04003036 RID: 12342
	public Transform BloodPool;

	// Token: 0x04003037 RID: 12343
	public Transform LeftDrill;

	// Token: 0x04003038 RID: 12344
	public Transform LeftPinky;

	// Token: 0x04003039 RID: 12345
	public Transform MapMarker;

	// Token: 0x0400303A RID: 12346
	public Transform RightHand;

	// Token: 0x0400303B RID: 12347
	public Transform LeftHand;

	// Token: 0x0400303C RID: 12348
	public Transform MeetSpot;

	// Token: 0x0400303D RID: 12349
	public Transform MyLocker;

	// Token: 0x0400303E RID: 12350
	public Transform MyPlate;

	// Token: 0x0400303F RID: 12351
	public Transform Spine;

	// Token: 0x04003040 RID: 12352
	public Transform Eyes;

	// Token: 0x04003041 RID: 12353
	public Transform Head;

	// Token: 0x04003042 RID: 12354
	public Transform Hips;

	// Token: 0x04003043 RID: 12355
	public Transform Neck;

	// Token: 0x04003044 RID: 12356
	public Transform Seat;

	// Token: 0x04003045 RID: 12357
	public ParticleSystem[] LiquidEmitters;

	// Token: 0x04003046 RID: 12358
	public ParticleSystem[] SplashEmitters;

	// Token: 0x04003047 RID: 12359
	public ParticleSystem[] FireEmitters;

	// Token: 0x04003048 RID: 12360
	public ScheduleBlock[] ScheduleBlocks;

	// Token: 0x04003049 RID: 12361
	public Transform[] Destinations;

	// Token: 0x0400304A RID: 12362
	public Transform[] LongHair;

	// Token: 0x0400304B RID: 12363
	public Transform[] Skirt;

	// Token: 0x0400304C RID: 12364
	public Transform[] Arm;

	// Token: 0x0400304D RID: 12365
	public DynamicBone[] BlackHoleEffect;

	// Token: 0x0400304E RID: 12366
	public OutlineScript[] Outlines;

	// Token: 0x0400304F RID: 12367
	public GameObject[] InstrumentBag;

	// Token: 0x04003050 RID: 12368
	public GameObject[] ScienceProps;

	// Token: 0x04003051 RID: 12369
	public GameObject[] Instruments;

	// Token: 0x04003052 RID: 12370
	public GameObject[] Chopsticks;

	// Token: 0x04003053 RID: 12371
	public GameObject[] Drumsticks;

	// Token: 0x04003054 RID: 12372
	public GameObject[] Fingerfood;

	// Token: 0x04003055 RID: 12373
	public GameObject[] Bones;

	// Token: 0x04003056 RID: 12374
	public string[] DelinquentAnims;

	// Token: 0x04003057 RID: 12375
	public string[] AnimationNames;

	// Token: 0x04003058 RID: 12376
	public string[] GossipAnims;

	// Token: 0x04003059 RID: 12377
	public string[] SleuthAnims;

	// Token: 0x0400305A RID: 12378
	public string[] CheerAnims;

	// Token: 0x0400305B RID: 12379
	[SerializeField]
	private List<int> VisibleCorpses = new List<int>();

	// Token: 0x0400305C RID: 12380
	[SerializeField]
	private int[] CorpseLayers = new int[]
	{
		11,
		14
	};

	// Token: 0x0400305D RID: 12381
	[SerializeField]
	private LayerMask Mask;

	// Token: 0x0400305E RID: 12382
	public StudentActionType CurrentAction;

	// Token: 0x0400305F RID: 12383
	public StudentActionType[] Actions;

	// Token: 0x04003060 RID: 12384
	public StudentActionType[] OriginalActions;

	// Token: 0x04003061 RID: 12385
	public AudioClip MurderSuicideKiller;

	// Token: 0x04003062 RID: 12386
	public AudioClip MurderSuicideVictim;

	// Token: 0x04003063 RID: 12387
	public AudioClip MurderSuicideSounds;

	// Token: 0x04003064 RID: 12388
	public AudioClip PoisonDeathClip;

	// Token: 0x04003065 RID: 12389
	public AudioClip PepperSpraySFX;

	// Token: 0x04003066 RID: 12390
	public AudioClip BurningClip;

	// Token: 0x04003067 RID: 12391
	public AudioSource AirGuitar;

	// Token: 0x04003068 RID: 12392
	public AudioClip[] FemaleAttacks;

	// Token: 0x04003069 RID: 12393
	public AudioClip[] BullyGiggles;

	// Token: 0x0400306A RID: 12394
	public AudioClip[] BullyLaughs;

	// Token: 0x0400306B RID: 12395
	public AudioClip[] MaleAttacks;

	// Token: 0x0400306C RID: 12396
	public SphereCollider HipCollider;

	// Token: 0x0400306D RID: 12397
	public Collider RightHandCollider;

	// Token: 0x0400306E RID: 12398
	public Collider LeftHandCollider;

	// Token: 0x0400306F RID: 12399
	public Collider NotFaceCollider;

	// Token: 0x04003070 RID: 12400
	public Collider PantyCollider;

	// Token: 0x04003071 RID: 12401
	public Collider SkirtCollider;

	// Token: 0x04003072 RID: 12402
	public Collider FaceCollider;

	// Token: 0x04003073 RID: 12403
	public Collider NEStairs;

	// Token: 0x04003074 RID: 12404
	public Collider NWStairs;

	// Token: 0x04003075 RID: 12405
	public Collider SEStairs;

	// Token: 0x04003076 RID: 12406
	public Collider SWStairs;

	// Token: 0x04003077 RID: 12407
	public GameObject BloodSprayCollider;

	// Token: 0x04003078 RID: 12408
	public GameObject BullyPhotoCollider;

	// Token: 0x04003079 RID: 12409
	public GameObject SquishyBloodEffect;

	// Token: 0x0400307A RID: 12410
	public GameObject WhiteQuestionMark;

	// Token: 0x0400307B RID: 12411
	public GameObject MiyukiGameScreen;

	// Token: 0x0400307C RID: 12412
	public GameObject EmptyGameObject;

	// Token: 0x0400307D RID: 12413
	public GameObject StabBloodEffect;

	// Token: 0x0400307E RID: 12414
	public GameObject BigWaterSplash;

	// Token: 0x0400307F RID: 12415
	public GameObject SecurityCamera;

	// Token: 0x04003080 RID: 12416
	public GameObject RightEmptyEye;

	// Token: 0x04003081 RID: 12417
	public GameObject Handkerchief;

	// Token: 0x04003082 RID: 12418
	public GameObject LeftEmptyEye;

	// Token: 0x04003083 RID: 12419
	public GameObject AnimatedBook;

	// Token: 0x04003084 RID: 12420
	public GameObject BloodyScream;

	// Token: 0x04003085 RID: 12421
	public GameObject BloodEffect;

	// Token: 0x04003086 RID: 12422
	public GameObject CameraFlash;

	// Token: 0x04003087 RID: 12423
	public GameObject ChaseCamera;

	// Token: 0x04003088 RID: 12424
	public GameObject DeathScream;

	// Token: 0x04003089 RID: 12425
	public GameObject PepperSpray;

	// Token: 0x0400308A RID: 12426
	public GameObject PinkSeifuku;

	// Token: 0x0400308B RID: 12427
	public GameObject WateringCan;

	// Token: 0x0400308C RID: 12428
	public GameObject BagOfChips;

	// Token: 0x0400308D RID: 12429
	public GameObject BloodSpray;

	// Token: 0x0400308E RID: 12430
	public GameObject Sketchbook;

	// Token: 0x0400308F RID: 12431
	public GameObject SmartPhone;

	// Token: 0x04003090 RID: 12432
	public GameObject OccultBook;

	// Token: 0x04003091 RID: 12433
	public GameObject Paintbrush;

	// Token: 0x04003092 RID: 12434
	public GameObject AlarmDisc;

	// Token: 0x04003093 RID: 12435
	public GameObject Character;

	// Token: 0x04003094 RID: 12436
	public GameObject Cigarette;

	// Token: 0x04003095 RID: 12437
	public GameObject EventBook;

	// Token: 0x04003096 RID: 12438
	public GameObject Handcuffs;

	// Token: 0x04003097 RID: 12439
	public GameObject HealthBar;

	// Token: 0x04003098 RID: 12440
	public GameObject OsanaHair;

	// Token: 0x04003099 RID: 12441
	public GameObject WeaponBag;

	// Token: 0x0400309A RID: 12442
	public GameObject CandyBar;

	// Token: 0x0400309B RID: 12443
	public GameObject Earpiece;

	// Token: 0x0400309C RID: 12444
	public GameObject Scrubber;

	// Token: 0x0400309D RID: 12445
	public GameObject Armband;

	// Token: 0x0400309E RID: 12446
	public GameObject BookBag;

	// Token: 0x0400309F RID: 12447
	public GameObject Lighter;

	// Token: 0x040030A0 RID: 12448
	public GameObject MyPaper;

	// Token: 0x040030A1 RID: 12449
	public GameObject Octodog;

	// Token: 0x040030A2 RID: 12450
	public GameObject Palette;

	// Token: 0x040030A3 RID: 12451
	public GameObject Eraser;

	// Token: 0x040030A4 RID: 12452
	public GameObject Giggle;

	// Token: 0x040030A5 RID: 12453
	public GameObject Marker;

	// Token: 0x040030A6 RID: 12454
	public GameObject Pencil;

	// Token: 0x040030A7 RID: 12455
	public GameObject Weapon;

	// Token: 0x040030A8 RID: 12456
	public GameObject Bento;

	// Token: 0x040030A9 RID: 12457
	public GameObject Paper;

	// Token: 0x040030AA RID: 12458
	public GameObject Note;

	// Token: 0x040030AB RID: 12459
	public GameObject Pen;

	// Token: 0x040030AC RID: 12460
	public GameObject Lid;

	// Token: 0x040030AD RID: 12461
	public bool InvestigatingPossibleDeath;

	// Token: 0x040030AE RID: 12462
	public bool InvestigatingPossibleLimb;

	// Token: 0x040030AF RID: 12463
	public bool SpecialRivalDeathReaction;

	// Token: 0x040030B0 RID: 12464
	public bool WitnessedMindBrokenMurder;

	// Token: 0x040030B1 RID: 12465
	public bool ReturningMisplacedWeapon;

	// Token: 0x040030B2 RID: 12466
	public bool SenpaiWitnessingRivalDie;

	// Token: 0x040030B3 RID: 12467
	public bool TargetedForDistraction;

	// Token: 0x040030B4 RID: 12468
	public bool SchoolwearUnavailable;

	// Token: 0x040030B5 RID: 12469
	public bool WitnessedBloodyWeapon;

	// Token: 0x040030B6 RID: 12470
	public bool IgnoringPettyActions;

	// Token: 0x040030B7 RID: 12471
	public bool ReturnToRoutineAfter;

	// Token: 0x040030B8 RID: 12472
	public bool MustChangeClothing;

	// Token: 0x040030B9 RID: 12473
	public bool SawCorpseThisFrame;

	// Token: 0x040030BA RID: 12474
	public bool WitnessedBloodPool;

	// Token: 0x040030BB RID: 12475
	public bool WitnessedSomething;

	// Token: 0x040030BC RID: 12476
	public bool FoundFriendCorpse;

	// Token: 0x040030BD RID: 12477
	public bool OriginallyTeacher;

	// Token: 0x040030BE RID: 12478
	public bool DramaticReaction;

	// Token: 0x040030BF RID: 12479
	public bool EventInterrupted;

	// Token: 0x040030C0 RID: 12480
	public bool FoundEnemyCorpse;

	// Token: 0x040030C1 RID: 12481
	public bool LostTeacherTrust;

	// Token: 0x040030C2 RID: 12482
	public bool WitnessedCoverUp;

	// Token: 0x040030C3 RID: 12483
	public bool WitnessedCorpse;

	// Token: 0x040030C4 RID: 12484
	public bool WitnessedMurder;

	// Token: 0x040030C5 RID: 12485
	public bool WitnessedWeapon;

	// Token: 0x040030C6 RID: 12486
	public bool YandereInnocent;

	// Token: 0x040030C7 RID: 12487
	public bool GetNewAnimation = true;

	// Token: 0x040030C8 RID: 12488
	public bool AttackWillFail;

	// Token: 0x040030C9 RID: 12489
	public bool CanStillNotice;

	// Token: 0x040030CA RID: 12490
	public bool FocusOnYandere;

	// Token: 0x040030CB RID: 12491
	public bool ManualRotation;

	// Token: 0x040030CC RID: 12492
	public bool PinDownWitness;

	// Token: 0x040030CD RID: 12493
	public bool RepeatReaction;

	// Token: 0x040030CE RID: 12494
	public bool StalkerFleeing;

	// Token: 0x040030CF RID: 12495
	public bool YandereVisible;

	// Token: 0x040030D0 RID: 12496
	public bool CrimeReported;

	// Token: 0x040030D1 RID: 12497
	public bool FleeWhenClean;

	// Token: 0x040030D2 RID: 12498
	public bool MurderSuicide;

	// Token: 0x040030D3 RID: 12499
	public bool PhotoEvidence;

	// Token: 0x040030D4 RID: 12500
	public bool RespectEarned;

	// Token: 0x040030D5 RID: 12501
	public bool WitnessedLimb;

	// Token: 0x040030D6 RID: 12502
	public bool BeenSplashed;

	// Token: 0x040030D7 RID: 12503
	public bool BoobsResized;

	// Token: 0x040030D8 RID: 12504
	public bool CheckingNote;

	// Token: 0x040030D9 RID: 12505
	public bool ClubActivity;

	// Token: 0x040030DA RID: 12506
	public bool Complimented;

	// Token: 0x040030DB RID: 12507
	public bool Electrocuted;

	// Token: 0x040030DC RID: 12508
	public bool FragileSlave;

	// Token: 0x040030DD RID: 12509
	public bool HoldingHands;

	// Token: 0x040030DE RID: 12510
	public bool PlayingAudio;

	// Token: 0x040030DF RID: 12511
	public bool StopRotating;

	// Token: 0x040030E0 RID: 12512
	public bool SawFriendDie;

	// Token: 0x040030E1 RID: 12513
	public bool SentToLocker;

	// Token: 0x040030E2 RID: 12514
	public bool TurnOffRadio;

	// Token: 0x040030E3 RID: 12515
	public bool BusyAtLunch;

	// Token: 0x040030E4 RID: 12516
	public bool Electrified;

	// Token: 0x040030E5 RID: 12517
	public bool HeardScream;

	// Token: 0x040030E6 RID: 12518
	public bool IgnoreBlood;

	// Token: 0x040030E7 RID: 12519
	public bool MusumeRight;

	// Token: 0x040030E8 RID: 12520
	public bool UpdateSkirt;

	// Token: 0x040030E9 RID: 12521
	public bool ClubAttire;

	// Token: 0x040030EA RID: 12522
	public bool Confessing;

	// Token: 0x040030EB RID: 12523
	public bool Distracted;

	// Token: 0x040030EC RID: 12524
	public bool KilledMood;

	// Token: 0x040030ED RID: 12525
	public bool LewdPhotos;

	// Token: 0x040030EE RID: 12526
	public bool InDarkness;

	// Token: 0x040030EF RID: 12527
	public bool SwitchBack;

	// Token: 0x040030F0 RID: 12528
	public bool Threatened;

	// Token: 0x040030F1 RID: 12529
	public bool BatheFast;

	// Token: 0x040030F2 RID: 12530
	public bool Counselor;

	// Token: 0x040030F3 RID: 12531
	public bool Depressed;

	// Token: 0x040030F4 RID: 12532
	public bool DiscCheck;

	// Token: 0x040030F5 RID: 12533
	public bool DressCode;

	// Token: 0x040030F6 RID: 12534
	public bool Drownable;

	// Token: 0x040030F7 RID: 12535
	public bool EndSearch;

	// Token: 0x040030F8 RID: 12536
	public bool KnifeDown;

	// Token: 0x040030F9 RID: 12537
	public bool LongSkirt;

	// Token: 0x040030FA RID: 12538
	public bool NoBreakUp;

	// Token: 0x040030FB RID: 12539
	public bool Phoneless;

	// Token: 0x040030FC RID: 12540
	public bool TrueAlone;

	// Token: 0x040030FD RID: 12541
	public bool WillChase;

	// Token: 0x040030FE RID: 12542
	public bool Attacked;

	// Token: 0x040030FF RID: 12543
	public bool Headache;

	// Token: 0x04003100 RID: 12544
	public bool Gossiped;

	// Token: 0x04003101 RID: 12545
	public bool Pushable;

	// Token: 0x04003102 RID: 12546
	public bool Replaced;

	// Token: 0x04003103 RID: 12547
	public bool Restless;

	// Token: 0x04003104 RID: 12548
	public bool SentHome;

	// Token: 0x04003105 RID: 12549
	public bool Splashed;

	// Token: 0x04003106 RID: 12550
	public bool Tranquil;

	// Token: 0x04003107 RID: 12551
	public bool WalkBack;

	// Token: 0x04003108 RID: 12552
	public bool Alarmed;

	// Token: 0x04003109 RID: 12553
	public bool BadTime;

	// Token: 0x0400310A RID: 12554
	public bool Bullied;

	// Token: 0x0400310B RID: 12555
	public bool Drowned;

	// Token: 0x0400310C RID: 12556
	public bool Forgave;

	// Token: 0x0400310D RID: 12557
	public bool Indoors;

	// Token: 0x0400310E RID: 12558
	public bool InEvent;

	// Token: 0x0400310F RID: 12559
	public bool Injured;

	// Token: 0x04003110 RID: 12560
	public bool Nemesis;

	// Token: 0x04003111 RID: 12561
	public bool Private;

	// Token: 0x04003112 RID: 12562
	public bool Reacted;

	// Token: 0x04003113 RID: 12563
	public bool SawMask;

	// Token: 0x04003114 RID: 12564
	public bool Sedated;

	// Token: 0x04003115 RID: 12565
	public bool SlideIn;

	// Token: 0x04003116 RID: 12566
	public bool Spawned;

	// Token: 0x04003117 RID: 12567
	public bool Started;

	// Token: 0x04003118 RID: 12568
	public bool Suicide;

	// Token: 0x04003119 RID: 12569
	public bool Teacher;

	// Token: 0x0400311A RID: 12570
	public bool Tripped;

	// Token: 0x0400311B RID: 12571
	public bool Witness;

	// Token: 0x0400311C RID: 12572
	public bool Bloody;

	// Token: 0x0400311D RID: 12573
	public bool CanTalk = true;

	// Token: 0x0400311E RID: 12574
	public bool Emetic;

	// Token: 0x0400311F RID: 12575
	public bool Lethal;

	// Token: 0x04003120 RID: 12576
	public bool Routine = true;

	// Token: 0x04003121 RID: 12577
	public bool GoAway;

	// Token: 0x04003122 RID: 12578
	public bool Grudge;

	// Token: 0x04003123 RID: 12579
	public bool Hungry;

	// Token: 0x04003124 RID: 12580
	public bool Hunted;

	// Token: 0x04003125 RID: 12581
	public bool NoTalk;

	// Token: 0x04003126 RID: 12582
	public bool Paired;

	// Token: 0x04003127 RID: 12583
	public bool Pushed;

	// Token: 0x04003128 RID: 12584
	public bool Warned;

	// Token: 0x04003129 RID: 12585
	public bool Alone;

	// Token: 0x0400312A RID: 12586
	public bool Blind;

	// Token: 0x0400312B RID: 12587
	public bool Eaten;

	// Token: 0x0400312C RID: 12588
	public bool Hurry;

	// Token: 0x0400312D RID: 12589
	public bool Rival;

	// Token: 0x0400312E RID: 12590
	public bool Slave;

	// Token: 0x0400312F RID: 12591
	public bool Calm;

	// Token: 0x04003130 RID: 12592
	public bool Halt;

	// Token: 0x04003131 RID: 12593
	public bool Lost;

	// Token: 0x04003132 RID: 12594
	public bool Male;

	// Token: 0x04003133 RID: 12595
	public bool Rose;

	// Token: 0x04003134 RID: 12596
	public bool Safe;

	// Token: 0x04003135 RID: 12597
	public bool Stop;

	// Token: 0x04003136 RID: 12598
	public bool AoT;

	// Token: 0x04003137 RID: 12599
	public bool Fed;

	// Token: 0x04003138 RID: 12600
	public bool Gas;

	// Token: 0x04003139 RID: 12601
	public bool Shy;

	// Token: 0x0400313A RID: 12602
	public bool Wet;

	// Token: 0x0400313B RID: 12603
	public bool Won;

	// Token: 0x0400313C RID: 12604
	public bool DK;

	// Token: 0x0400313D RID: 12605
	public bool NotAlarmedByYandereChan;

	// Token: 0x0400313E RID: 12606
	public bool InvestigatingBloodPool;

	// Token: 0x0400313F RID: 12607
	public bool RetreivingMedicine;

	// Token: 0x04003140 RID: 12608
	public bool ResumeDistracting;

	// Token: 0x04003141 RID: 12609
	public bool BreakingUpFight;

	// Token: 0x04003142 RID: 12610
	public bool SeekingMedicine;

	// Token: 0x04003143 RID: 12611
	public bool ReportingMurder;

	// Token: 0x04003144 RID: 12612
	public bool CameraReacting;

	// Token: 0x04003145 RID: 12613
	public bool UsingRigidbody;

	// Token: 0x04003146 RID: 12614
	public bool ReportingBlood;

	// Token: 0x04003147 RID: 12615
	public bool FightingSlave;

	// Token: 0x04003148 RID: 12616
	public bool Investigating;

	// Token: 0x04003149 RID: 12617
	public bool Distracting;

	// Token: 0x0400314A RID: 12618
	public bool EatingSnack;

	// Token: 0x0400314B RID: 12619
	public bool HitReacting;

	// Token: 0x0400314C RID: 12620
	public bool PinningDown;

	// Token: 0x0400314D RID: 12621
	public bool Struggling;

	// Token: 0x0400314E RID: 12622
	public bool Following;

	// Token: 0x0400314F RID: 12623
	public bool Sleuthing;

	// Token: 0x04003150 RID: 12624
	public bool Stripping;

	// Token: 0x04003151 RID: 12625
	public bool Fighting;

	// Token: 0x04003152 RID: 12626
	public bool Guarding;

	// Token: 0x04003153 RID: 12627
	public bool Ignoring;

	// Token: 0x04003154 RID: 12628
	public bool Spraying;

	// Token: 0x04003155 RID: 12629
	public bool Tripping;

	// Token: 0x04003156 RID: 12630
	public bool Vomiting;

	// Token: 0x04003157 RID: 12631
	public bool Burning;

	// Token: 0x04003158 RID: 12632
	public bool Chasing;

	// Token: 0x04003159 RID: 12633
	public bool Curious;

	// Token: 0x0400315A RID: 12634
	public bool Fleeing;

	// Token: 0x0400315B RID: 12635
	public bool Hunting;

	// Token: 0x0400315C RID: 12636
	public bool Leaving;

	// Token: 0x0400315D RID: 12637
	public bool Meeting;

	// Token: 0x0400315E RID: 12638
	public bool Shoving;

	// Token: 0x0400315F RID: 12639
	public bool Talking;

	// Token: 0x04003160 RID: 12640
	public bool Waiting;

	// Token: 0x04003161 RID: 12641
	public bool Dodging;

	// Token: 0x04003162 RID: 12642
	public bool Posing;

	// Token: 0x04003163 RID: 12643
	public bool Dying;

	// Token: 0x04003164 RID: 12644
	public float DistanceToDestination;

	// Token: 0x04003165 RID: 12645
	public float FollowTargetDistance;

	// Token: 0x04003166 RID: 12646
	public float DistanceToPlayer;

	// Token: 0x04003167 RID: 12647
	public float TargetDistance;

	// Token: 0x04003168 RID: 12648
	public float ThreatDistance;

	// Token: 0x04003169 RID: 12649
	public float WitnessCooldownTimer;

	// Token: 0x0400316A RID: 12650
	public float InvestigationTimer;

	// Token: 0x0400316B RID: 12651
	public float CameraPoseTimer;

	// Token: 0x0400316C RID: 12652
	public float RivalDeathTimer;

	// Token: 0x0400316D RID: 12653
	public float CuriosityTimer;

	// Token: 0x0400316E RID: 12654
	public float DistractTimer;

	// Token: 0x0400316F RID: 12655
	public float DramaticTimer;

	// Token: 0x04003170 RID: 12656
	public float MedicineTimer;

	// Token: 0x04003171 RID: 12657
	public float ReactionTimer;

	// Token: 0x04003172 RID: 12658
	public float WalkBackTimer;

	// Token: 0x04003173 RID: 12659
	public float ElectroTimer;

	// Token: 0x04003174 RID: 12660
	public float ThreatTimer;

	// Token: 0x04003175 RID: 12661
	public float GiggleTimer;

	// Token: 0x04003176 RID: 12662
	public float GoAwayTimer;

	// Token: 0x04003177 RID: 12663
	public float IgnoreTimer;

	// Token: 0x04003178 RID: 12664
	public float LyricsTimer;

	// Token: 0x04003179 RID: 12665
	public float MiyukiTimer;

	// Token: 0x0400317A RID: 12666
	public float MusumeTimer;

	// Token: 0x0400317B RID: 12667
	public float PatrolTimer;

	// Token: 0x0400317C RID: 12668
	public float ReportTimer;

	// Token: 0x0400317D RID: 12669
	public float SplashTimer;

	// Token: 0x0400317E RID: 12670
	public float UpdateTimer;

	// Token: 0x0400317F RID: 12671
	public float AlarmTimer;

	// Token: 0x04003180 RID: 12672
	public float BatheTimer;

	// Token: 0x04003181 RID: 12673
	public float ChaseTimer;

	// Token: 0x04003182 RID: 12674
	public float CheerTimer;

	// Token: 0x04003183 RID: 12675
	public float CleanTimer;

	// Token: 0x04003184 RID: 12676
	public float LaughTimer;

	// Token: 0x04003185 RID: 12677
	public float RadioTimer;

	// Token: 0x04003186 RID: 12678
	public float SnackTimer;

	// Token: 0x04003187 RID: 12679
	public float SprayTimer;

	// Token: 0x04003188 RID: 12680
	public float StuckTimer;

	// Token: 0x04003189 RID: 12681
	public float ClubTimer;

	// Token: 0x0400318A RID: 12682
	public float MeetTimer;

	// Token: 0x0400318B RID: 12683
	public float SulkTimer;

	// Token: 0x0400318C RID: 12684
	public float TalkTimer;

	// Token: 0x0400318D RID: 12685
	public float WaitTimer;

	// Token: 0x0400318E RID: 12686
	public float SewTimer;

	// Token: 0x0400318F RID: 12687
	public float OriginalYPosition;

	// Token: 0x04003190 RID: 12688
	public float PreviousEyeShrink;

	// Token: 0x04003191 RID: 12689
	public float PhotoPatience;

	// Token: 0x04003192 RID: 12690
	public float PreviousAlarm;

	// Token: 0x04003193 RID: 12691
	public float ClubThreshold = 6f;

	// Token: 0x04003194 RID: 12692
	public float RepDeduction;

	// Token: 0x04003195 RID: 12693
	public float RepRecovery;

	// Token: 0x04003196 RID: 12694
	public float BreastSize;

	// Token: 0x04003197 RID: 12695
	public float DodgeSpeed = 2f;

	// Token: 0x04003198 RID: 12696
	public float Hesitation;

	// Token: 0x04003199 RID: 12697
	public float PendingRep;

	// Token: 0x0400319A RID: 12698
	public float Perception = 1f;

	// Token: 0x0400319B RID: 12699
	public float EyeShrink;

	// Token: 0x0400319C RID: 12700
	public float MeetTime;

	// Token: 0x0400319D RID: 12701
	public float Paranoia;

	// Token: 0x0400319E RID: 12702
	public float RepLoss;

	// Token: 0x0400319F RID: 12703
	public float Health = 100f;

	// Token: 0x040031A0 RID: 12704
	public float Alarm;

	// Token: 0x040031A1 RID: 12705
	public int ReturningMisplacedWeaponPhase;

	// Token: 0x040031A2 RID: 12706
	public int RetrieveMedicinePhase;

	// Token: 0x040031A3 RID: 12707
	public int WitnessRivalDiePhase;

	// Token: 0x040031A4 RID: 12708
	public int ChangeClothingPhase;

	// Token: 0x040031A5 RID: 12709
	public int InvestigationPhase;

	// Token: 0x040031A6 RID: 12710
	public int MurderSuicidePhase;

	// Token: 0x040031A7 RID: 12711
	public int ClubActivityPhase;

	// Token: 0x040031A8 RID: 12712
	public int SeekMedicinePhase;

	// Token: 0x040031A9 RID: 12713
	public int CameraReactPhase;

	// Token: 0x040031AA RID: 12714
	public int CuriosityPhase;

	// Token: 0x040031AB RID: 12715
	public int DramaticPhase;

	// Token: 0x040031AC RID: 12716
	public int GraffitiPhase;

	// Token: 0x040031AD RID: 12717
	public int SentHomePhase;

	// Token: 0x040031AE RID: 12718
	public int SunbathePhase;

	// Token: 0x040031AF RID: 12719
	public int ConfessPhase = 1;

	// Token: 0x040031B0 RID: 12720
	public int SciencePhase;

	// Token: 0x040031B1 RID: 12721
	public int LyricsPhase;

	// Token: 0x040031B2 RID: 12722
	public int ReportPhase;

	// Token: 0x040031B3 RID: 12723
	public int SplashPhase;

	// Token: 0x040031B4 RID: 12724
	public int ThreatPhase = 1;

	// Token: 0x040031B5 RID: 12725
	public int BathePhase;

	// Token: 0x040031B6 RID: 12726
	public int BullyPhase;

	// Token: 0x040031B7 RID: 12727
	public int RadioPhase = 1;

	// Token: 0x040031B8 RID: 12728
	public int SnackPhase;

	// Token: 0x040031B9 RID: 12729
	public int VomitPhase;

	// Token: 0x040031BA RID: 12730
	public int ClubPhase;

	// Token: 0x040031BB RID: 12731
	public int SulkPhase;

	// Token: 0x040031BC RID: 12732
	public int TaskPhase;

	// Token: 0x040031BD RID: 12733
	public int ReadPhase;

	// Token: 0x040031BE RID: 12734
	public int PinPhase;

	// Token: 0x040031BF RID: 12735
	public int Phase;

	// Token: 0x040031C0 RID: 12736
	public PersonaType OriginalPersona;

	// Token: 0x040031C1 RID: 12737
	public StudentInteractionType Interaction;

	// Token: 0x040031C2 RID: 12738
	public int LovestruckTarget;

	// Token: 0x040031C3 RID: 12739
	public int MurdersWitnessed;

	// Token: 0x040031C4 RID: 12740
	public int WeaponWitnessed;

	// Token: 0x040031C5 RID: 12741
	public int MurderReaction;

	// Token: 0x040031C6 RID: 12742
	public int CleaningRole;

	// Token: 0x040031C7 RID: 12743
	public int StruggleWait;

	// Token: 0x040031C8 RID: 12744
	public int TimesAnnoyed;

	// Token: 0x040031C9 RID: 12745
	public int GossipBonus;

	// Token: 0x040031CA RID: 12746
	public int DeathCause;

	// Token: 0x040031CB RID: 12747
	public int Schoolwear;

	// Token: 0x040031CC RID: 12748
	public int SkinColor = 3;

	// Token: 0x040031CD RID: 12749
	public int Attempts;

	// Token: 0x040031CE RID: 12750
	public int Patience = 5;

	// Token: 0x040031CF RID: 12751
	public int Pestered;

	// Token: 0x040031D0 RID: 12752
	public int RepBonus;

	// Token: 0x040031D1 RID: 12753
	public int Strength;

	// Token: 0x040031D2 RID: 12754
	public int Concern;

	// Token: 0x040031D3 RID: 12755
	public int Defeats;

	// Token: 0x040031D4 RID: 12756
	public int Crush;

	// Token: 0x040031D5 RID: 12757
	public StudentWitnessType PreviouslyWitnessed;

	// Token: 0x040031D6 RID: 12758
	public StudentWitnessType Witnessed;

	// Token: 0x040031D7 RID: 12759
	public GameOverType GameOverCause;

	// Token: 0x040031D8 RID: 12760
	public DeathType DeathType;

	// Token: 0x040031D9 RID: 12761
	public string CurrentAnim = string.Empty;

	// Token: 0x040031DA RID: 12762
	public string RivalPrefix = string.Empty;

	// Token: 0x040031DB RID: 12763
	public string RandomAnim = string.Empty;

	// Token: 0x040031DC RID: 12764
	public string Accessory = string.Empty;

	// Token: 0x040031DD RID: 12765
	public string Hairstyle = string.Empty;

	// Token: 0x040031DE RID: 12766
	public string Suffix = string.Empty;

	// Token: 0x040031DF RID: 12767
	public string Name = string.Empty;

	// Token: 0x040031E0 RID: 12768
	public string OriginalOriginalWalkAnim = string.Empty;

	// Token: 0x040031E1 RID: 12769
	public string OriginalIdleAnim = string.Empty;

	// Token: 0x040031E2 RID: 12770
	public string OriginalWalkAnim = string.Empty;

	// Token: 0x040031E3 RID: 12771
	public string OriginalSprintAnim = string.Empty;

	// Token: 0x040031E4 RID: 12772
	public string OriginalLeanAnim = string.Empty;

	// Token: 0x040031E5 RID: 12773
	public string WalkAnim = string.Empty;

	// Token: 0x040031E6 RID: 12774
	public string RunAnim = string.Empty;

	// Token: 0x040031E7 RID: 12775
	public string SprintAnim = string.Empty;

	// Token: 0x040031E8 RID: 12776
	public string IdleAnim = string.Empty;

	// Token: 0x040031E9 RID: 12777
	public string Nod1Anim = string.Empty;

	// Token: 0x040031EA RID: 12778
	public string Nod2Anim = string.Empty;

	// Token: 0x040031EB RID: 12779
	public string DefendAnim = string.Empty;

	// Token: 0x040031EC RID: 12780
	public string DeathAnim = string.Empty;

	// Token: 0x040031ED RID: 12781
	public string ScaredAnim = string.Empty;

	// Token: 0x040031EE RID: 12782
	public string EvilWitnessAnim = string.Empty;

	// Token: 0x040031EF RID: 12783
	public string LookDownAnim = string.Empty;

	// Token: 0x040031F0 RID: 12784
	public string PhoneAnim = string.Empty;

	// Token: 0x040031F1 RID: 12785
	public string AngryFaceAnim = string.Empty;

	// Token: 0x040031F2 RID: 12786
	public string ToughFaceAnim = string.Empty;

	// Token: 0x040031F3 RID: 12787
	public string InspectAnim = string.Empty;

	// Token: 0x040031F4 RID: 12788
	public string GuardAnim = string.Empty;

	// Token: 0x040031F5 RID: 12789
	public string CallAnim = string.Empty;

	// Token: 0x040031F6 RID: 12790
	public string CounterAnim = string.Empty;

	// Token: 0x040031F7 RID: 12791
	public string PushedAnim = string.Empty;

	// Token: 0x040031F8 RID: 12792
	public string GameAnim = string.Empty;

	// Token: 0x040031F9 RID: 12793
	public string BentoAnim = string.Empty;

	// Token: 0x040031FA RID: 12794
	public string EatAnim = string.Empty;

	// Token: 0x040031FB RID: 12795
	public string DrownAnim = string.Empty;

	// Token: 0x040031FC RID: 12796
	public string WetAnim = string.Empty;

	// Token: 0x040031FD RID: 12797
	public string SplashedAnim = string.Empty;

	// Token: 0x040031FE RID: 12798
	public string StripAnim = string.Empty;

	// Token: 0x040031FF RID: 12799
	public string ParanoidAnim = string.Empty;

	// Token: 0x04003200 RID: 12800
	public string GossipAnim = string.Empty;

	// Token: 0x04003201 RID: 12801
	public string SadSitAnim = string.Empty;

	// Token: 0x04003202 RID: 12802
	public string BrokenAnim = string.Empty;

	// Token: 0x04003203 RID: 12803
	public string BrokenSitAnim = string.Empty;

	// Token: 0x04003204 RID: 12804
	public string BrokenWalkAnim = string.Empty;

	// Token: 0x04003205 RID: 12805
	public string FistAnim = string.Empty;

	// Token: 0x04003206 RID: 12806
	public string AttackAnim = string.Empty;

	// Token: 0x04003207 RID: 12807
	public string SuicideAnim = string.Empty;

	// Token: 0x04003208 RID: 12808
	public string RelaxAnim = string.Empty;

	// Token: 0x04003209 RID: 12809
	public string SitAnim = string.Empty;

	// Token: 0x0400320A RID: 12810
	public string ShyAnim = string.Empty;

	// Token: 0x0400320B RID: 12811
	public string PeekAnim = string.Empty;

	// Token: 0x0400320C RID: 12812
	public string ClubAnim = string.Empty;

	// Token: 0x0400320D RID: 12813
	public string StruggleAnim = string.Empty;

	// Token: 0x0400320E RID: 12814
	public string StruggleWonAnim = string.Empty;

	// Token: 0x0400320F RID: 12815
	public string StruggleLostAnim = string.Empty;

	// Token: 0x04003210 RID: 12816
	public string SocialSitAnim = string.Empty;

	// Token: 0x04003211 RID: 12817
	public string CarryAnim = string.Empty;

	// Token: 0x04003212 RID: 12818
	public string ActivityAnim = string.Empty;

	// Token: 0x04003213 RID: 12819
	public string GrudgeAnim = string.Empty;

	// Token: 0x04003214 RID: 12820
	public string SadFaceAnim = string.Empty;

	// Token: 0x04003215 RID: 12821
	public string CowardAnim = string.Empty;

	// Token: 0x04003216 RID: 12822
	public string EvilAnim = string.Empty;

	// Token: 0x04003217 RID: 12823
	public string SocialReportAnim = string.Empty;

	// Token: 0x04003218 RID: 12824
	public string SocialFearAnim = string.Empty;

	// Token: 0x04003219 RID: 12825
	public string SocialTerrorAnim = string.Empty;

	// Token: 0x0400321A RID: 12826
	public string BuzzSawDeathAnim = string.Empty;

	// Token: 0x0400321B RID: 12827
	public string SwingDeathAnim = string.Empty;

	// Token: 0x0400321C RID: 12828
	public string CyborgDeathAnim = string.Empty;

	// Token: 0x0400321D RID: 12829
	public string WalkBackAnim = string.Empty;

	// Token: 0x0400321E RID: 12830
	public string PatrolAnim = string.Empty;

	// Token: 0x0400321F RID: 12831
	public string RadioAnim = string.Empty;

	// Token: 0x04003220 RID: 12832
	public string BookSitAnim = string.Empty;

	// Token: 0x04003221 RID: 12833
	public string BookReadAnim = string.Empty;

	// Token: 0x04003222 RID: 12834
	public string LovedOneAnim = string.Empty;

	// Token: 0x04003223 RID: 12835
	public string CuddleAnim = string.Empty;

	// Token: 0x04003224 RID: 12836
	public string VomitAnim = string.Empty;

	// Token: 0x04003225 RID: 12837
	public string WashFaceAnim = string.Empty;

	// Token: 0x04003226 RID: 12838
	public string EmeticAnim = string.Empty;

	// Token: 0x04003227 RID: 12839
	public string BurningAnim = string.Empty;

	// Token: 0x04003228 RID: 12840
	public string JojoReactAnim = string.Empty;

	// Token: 0x04003229 RID: 12841
	public string TeachAnim = string.Empty;

	// Token: 0x0400322A RID: 12842
	public string LeanAnim = string.Empty;

	// Token: 0x0400322B RID: 12843
	public string DeskTextAnim = string.Empty;

	// Token: 0x0400322C RID: 12844
	public string CarryShoulderAnim = string.Empty;

	// Token: 0x0400322D RID: 12845
	public string ReadyToFightAnim = string.Empty;

	// Token: 0x0400322E RID: 12846
	public string SearchPatrolAnim = string.Empty;

	// Token: 0x0400322F RID: 12847
	public string DiscoverPhoneAnim = string.Empty;

	// Token: 0x04003230 RID: 12848
	public string WaitAnim = string.Empty;

	// Token: 0x04003231 RID: 12849
	public string ShoveAnim = string.Empty;

	// Token: 0x04003232 RID: 12850
	public string SprayAnim = string.Empty;

	// Token: 0x04003233 RID: 12851
	public string SithReactAnim = string.Empty;

	// Token: 0x04003234 RID: 12852
	public string EatVictimAnim = string.Empty;

	// Token: 0x04003235 RID: 12853
	public string RandomGossipAnim = string.Empty;

	// Token: 0x04003236 RID: 12854
	public string CuteAnim = string.Empty;

	// Token: 0x04003237 RID: 12855
	public string BulliedIdleAnim = string.Empty;

	// Token: 0x04003238 RID: 12856
	public string BulliedWalkAnim = string.Empty;

	// Token: 0x04003239 RID: 12857
	public string BullyVictimAnim = string.Empty;

	// Token: 0x0400323A RID: 12858
	public string SadDeskSitAnim = string.Empty;

	// Token: 0x0400323B RID: 12859
	public string ConfusedSitAnim = string.Empty;

	// Token: 0x0400323C RID: 12860
	public string SentHomeAnim = string.Empty;

	// Token: 0x0400323D RID: 12861
	public string RandomCheerAnim = string.Empty;

	// Token: 0x0400323E RID: 12862
	public string ParanoidWalkAnim = string.Empty;

	// Token: 0x0400323F RID: 12863
	public string SleuthIdleAnim = string.Empty;

	// Token: 0x04003240 RID: 12864
	public string SleuthWalkAnim = string.Empty;

	// Token: 0x04003241 RID: 12865
	public string SleuthCalmAnim = string.Empty;

	// Token: 0x04003242 RID: 12866
	public string SleuthScanAnim = string.Empty;

	// Token: 0x04003243 RID: 12867
	public string SleuthReactAnim = string.Empty;

	// Token: 0x04003244 RID: 12868
	public string SleuthSprintAnim = string.Empty;

	// Token: 0x04003245 RID: 12869
	public string SleuthReportAnim = string.Empty;

	// Token: 0x04003246 RID: 12870
	public string RandomSleuthAnim = string.Empty;

	// Token: 0x04003247 RID: 12871
	public string BreakUpAnim = string.Empty;

	// Token: 0x04003248 RID: 12872
	public string PaintAnim = string.Empty;

	// Token: 0x04003249 RID: 12873
	public string SketchAnim = string.Empty;

	// Token: 0x0400324A RID: 12874
	public string RummageAnim = string.Empty;

	// Token: 0x0400324B RID: 12875
	public string ThinkAnim = string.Empty;

	// Token: 0x0400324C RID: 12876
	public string ActAnim = string.Empty;

	// Token: 0x0400324D RID: 12877
	public string OriginalClubAnim = string.Empty;

	// Token: 0x0400324E RID: 12878
	public string MiyukiAnim = string.Empty;

	// Token: 0x0400324F RID: 12879
	public string VictoryAnim = string.Empty;

	// Token: 0x04003250 RID: 12880
	public string PlateIdleAnim = string.Empty;

	// Token: 0x04003251 RID: 12881
	public string PlateWalkAnim = string.Empty;

	// Token: 0x04003252 RID: 12882
	public string PlateEatAnim = string.Empty;

	// Token: 0x04003253 RID: 12883
	public string PrepareFoodAnim = string.Empty;

	// Token: 0x04003254 RID: 12884
	public string PoisonDeathAnim = string.Empty;

	// Token: 0x04003255 RID: 12885
	public string HeadacheAnim = string.Empty;

	// Token: 0x04003256 RID: 12886
	public string HeadacheSitAnim = string.Empty;

	// Token: 0x04003257 RID: 12887
	public string ElectroAnim = string.Empty;

	// Token: 0x04003258 RID: 12888
	public string EatChipsAnim = string.Empty;

	// Token: 0x04003259 RID: 12889
	public string DrinkFountainAnim = string.Empty;

	// Token: 0x0400325A RID: 12890
	public string PullBoxCutterAnim = string.Empty;

	// Token: 0x0400325B RID: 12891
	public string TossNoteAnim = string.Empty;

	// Token: 0x0400325C RID: 12892
	public string KeepNoteAnim = string.Empty;

	// Token: 0x0400325D RID: 12893
	public string BathingAnim = string.Empty;

	// Token: 0x0400325E RID: 12894
	public string DodgeAnim = string.Empty;

	// Token: 0x0400325F RID: 12895
	public string InspectBloodAnim = string.Empty;

	// Token: 0x04003260 RID: 12896
	public string PickUpAnim = string.Empty;

	// Token: 0x04003261 RID: 12897
	public string[] CleanAnims;

	// Token: 0x04003262 RID: 12898
	public string[] CameraAnims;

	// Token: 0x04003263 RID: 12899
	public string[] SocialAnims;

	// Token: 0x04003264 RID: 12900
	public string[] CowardAnims;

	// Token: 0x04003265 RID: 12901
	public string[] EvilAnims;

	// Token: 0x04003266 RID: 12902
	public string[] HeroAnims;

	// Token: 0x04003267 RID: 12903
	public string[] TaskAnims;

	// Token: 0x04003268 RID: 12904
	public string[] PhoneAnims;

	// Token: 0x04003269 RID: 12905
	public int ClubMemberID;

	// Token: 0x0400326A RID: 12906
	public int StudentID;

	// Token: 0x0400326B RID: 12907
	public int PatrolID;

	// Token: 0x0400326C RID: 12908
	public int SleuthID;

	// Token: 0x0400326D RID: 12909
	public int BullyID;

	// Token: 0x0400326E RID: 12910
	public int CleanID;

	// Token: 0x0400326F RID: 12911
	public int Class;

	// Token: 0x04003270 RID: 12912
	public int ID;

	// Token: 0x04003271 RID: 12913
	public PersonaType Persona;

	// Token: 0x04003272 RID: 12914
	public ClubType OriginalClub;

	// Token: 0x04003273 RID: 12915
	public ClubType Club;

	// Token: 0x04003274 RID: 12916
	public Vector3 OriginalPlatePosition;

	// Token: 0x04003275 RID: 12917
	public Vector3 OriginalPosition;

	// Token: 0x04003276 RID: 12918
	public Vector3 LastKnownCorpse;

	// Token: 0x04003277 RID: 12919
	public Vector3 DistractionSpot;

	// Token: 0x04003278 RID: 12920
	public Vector3 LastKnownBlood;

	// Token: 0x04003279 RID: 12921
	public Vector3 RightEyeOrigin;

	// Token: 0x0400327A RID: 12922
	public Vector3 LeftEyeOrigin;

	// Token: 0x0400327B RID: 12923
	public Vector3 PreviousSkirt;

	// Token: 0x0400327C RID: 12924
	public Vector3 LastPosition;

	// Token: 0x0400327D RID: 12925
	public Vector3 BurnTarget;

	// Token: 0x0400327E RID: 12926
	public Transform RightBreast;

	// Token: 0x0400327F RID: 12927
	public Transform LeftBreast;

	// Token: 0x04003280 RID: 12928
	public Transform RightEye;

	// Token: 0x04003281 RID: 12929
	public Transform LeftEye;

	// Token: 0x04003282 RID: 12930
	public int Frame;

	// Token: 0x04003283 RID: 12931
	private float MaxSpeed = 10f;

	// Token: 0x04003284 RID: 12932
	private const string RIVAL_PREFIX = "Rival ";

	// Token: 0x04003285 RID: 12933
	public Vector3[] SkirtPositions;

	// Token: 0x04003286 RID: 12934
	public Vector3[] SkirtRotations;

	// Token: 0x04003287 RID: 12935
	public Vector3[] SkirtOrigins;

	// Token: 0x04003288 RID: 12936
	public Transform DefaultTarget;

	// Token: 0x04003289 RID: 12937
	public Transform GushTarget;

	// Token: 0x0400328A RID: 12938
	public bool Gush;

	// Token: 0x0400328B RID: 12939
	public float LookSpeed = 2f;

	// Token: 0x0400328C RID: 12940
	public float TimeOfDeath;

	// Token: 0x0400328D RID: 12941
	public int Fate;

	// Token: 0x0400328E RID: 12942
	public LowPolyStudentScript LowPoly;

	// Token: 0x0400328F RID: 12943
	public GameObject JojoHitEffect;

	// Token: 0x04003290 RID: 12944
	public GameObject[] ElectroSteam;

	// Token: 0x04003291 RID: 12945
	public GameObject[] CensorSteam;

	// Token: 0x04003292 RID: 12946
	public Texture NudeTexture;

	// Token: 0x04003293 RID: 12947
	public Mesh BaldNudeMesh;

	// Token: 0x04003294 RID: 12948
	public Mesh NudeMesh;

	// Token: 0x04003295 RID: 12949
	public Texture TowelTexture;

	// Token: 0x04003296 RID: 12950
	public Mesh TowelMesh;

	// Token: 0x04003297 RID: 12951
	public Mesh SwimmingTrunks;

	// Token: 0x04003298 RID: 12952
	public Mesh SchoolSwimsuit;

	// Token: 0x04003299 RID: 12953
	public Mesh GymUniform;

	// Token: 0x0400329A RID: 12954
	public Texture UniformTexture;

	// Token: 0x0400329B RID: 12955
	public Texture SwimsuitTexture;

	// Token: 0x0400329C RID: 12956
	public Texture GyaruSwimsuitTexture;

	// Token: 0x0400329D RID: 12957
	public Texture GymTexture;

	// Token: 0x0400329E RID: 12958
	public Texture TitanBodyTexture;

	// Token: 0x0400329F RID: 12959
	public Texture TitanFaceTexture;

	// Token: 0x040032A0 RID: 12960
	public bool Spooky;

	// Token: 0x040032A1 RID: 12961
	public Mesh JudoGiMesh;

	// Token: 0x040032A2 RID: 12962
	public Texture JudoGiTexture;

	// Token: 0x040032A3 RID: 12963
	public RiggedAccessoryAttacher Attacher;

	// Token: 0x040032A4 RID: 12964
	public Mesh NoArmsNoTorso;

	// Token: 0x040032A5 RID: 12965
	public GameObject RiggedAccessory;

	// Token: 0x040032A6 RID: 12966
	public int CoupleID;

	// Token: 0x040032A7 RID: 12967
	public float ChameleonBonus;

	// Token: 0x040032A8 RID: 12968
	public bool Chameleon;

	// Token: 0x040032A9 RID: 12969
	public RiggedAccessoryAttacher LabcoatAttacher;

	// Token: 0x040032AA RID: 12970
	public RiggedAccessoryAttacher ApronAttacher;

	// Token: 0x040032AB RID: 12971
	public Mesh HeadAndHands;
}
