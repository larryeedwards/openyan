using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000530 RID: 1328
public class StudentManagerScript : MonoBehaviour
{
	// Token: 0x06002089 RID: 8329 RVA: 0x00156E78 File Offset: 0x00155278
	private void Start()
	{
		this.LoveSick = GameGlobals.LoveSick;
		this.MetalDetectors = SchoolGlobals.HighSecurity;
		this.RoofFenceUp = SchoolGlobals.RoofFence;
		SchemeGlobals.DeleteAll();
		if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
		{
			this.SpawnPositions[51].position = new Vector3(3f, 0f, -95f);
		}
		if (HomeGlobals.LateForSchool)
		{
			HomeGlobals.LateForSchool = false;
			this.YandereLate = true;
			Debug.Log("Yandere-chan is late for school!");
		}
		if (!this.YandereLate && StudentGlobals.MemorialStudents > 0)
		{
			this.Yandere.HUD.alpha = 0f;
			this.Yandere.HeartCamera.enabled = false;
		}
		if (GameGlobals.Profile == 0)
		{
			GameGlobals.Profile = 1;
			PlayerGlobals.Money = 10f;
		}
		if (!GameGlobals.ReputationsInitialized)
		{
			GameGlobals.ReputationsInitialized = true;
			this.InitializeReputations();
		}
		this.ID = 76;
		while (this.ID < 81)
		{
			if (StudentGlobals.GetStudentReputation(this.ID) > -67)
			{
				StudentGlobals.SetStudentReputation(this.ID, -67);
			}
			this.ID++;
		}
		if (ClubGlobals.GetClubClosed(ClubType.Gardening))
		{
			this.GardenBlockade.SetActive(true);
			this.Flowers.SetActive(false);
		}
		this.ID = 0;
		this.ID = 1;
		while (this.ID < this.JSON.Students.Length)
		{
			if (!this.JSON.Students[this.ID].Success)
			{
				this.ProblemID = this.ID;
				break;
			}
			this.ID++;
		}
		if (this.FridayPaintings.Length > 0)
		{
			this.ID = 1;
			while (this.ID < this.FridayPaintings.Length)
			{
				Renderer renderer = this.FridayPaintings[this.ID];
				renderer.material.color = new Color(1f, 1f, 1f, 0f);
				this.ID++;
			}
		}
		if (DateGlobals.Weekday != DayOfWeek.Friday)
		{
			if (this.Canvases != null)
			{
				this.Canvases.SetActive(false);
			}
		}
		else if (ClubGlobals.GetClubClosed(ClubType.Art))
		{
			this.Canvases.SetActive(false);
		}
		bool flag = this.ProblemID != -1;
		if (flag)
		{
			if (this.ErrorLabel != null)
			{
				this.ErrorLabel.text = string.Empty;
				this.ErrorLabel.enabled = false;
			}
			if (MissionModeGlobals.MissionMode)
			{
				StudentGlobals.FemaleUniform = 5;
				StudentGlobals.MaleUniform = 5;
				this.RedString.gameObject.SetActive(false);
			}
			this.SetAtmosphere();
			GameGlobals.Paranormal = false;
			if (StudentGlobals.GetStudentSlave() > 0 && !StudentGlobals.GetStudentDead(StudentGlobals.GetStudentSlave()))
			{
				int studentSlave = StudentGlobals.GetStudentSlave();
				this.ForceSpawn = true;
				this.SpawnPositions[studentSlave] = this.SlaveSpot;
				this.SpawnID = studentSlave;
				StudentGlobals.SetStudentDead(studentSlave, false);
				this.SpawnStudent(this.SpawnID);
				this.Students[studentSlave].Slave = true;
				this.SpawnID = 0;
			}
			if (StudentGlobals.GetStudentFragileSlave() > 0 && !StudentGlobals.GetStudentDead(StudentGlobals.GetStudentFragileSlave()))
			{
				int studentFragileSlave = StudentGlobals.GetStudentFragileSlave();
				this.ForceSpawn = true;
				this.SpawnPositions[studentFragileSlave] = this.FragileSlaveSpot;
				this.SpawnID = studentFragileSlave;
				StudentGlobals.SetStudentDead(studentFragileSlave, false);
				this.SpawnStudent(this.SpawnID);
				this.Students[studentFragileSlave].FragileSlave = true;
				this.Students[studentFragileSlave].Slave = true;
				this.SpawnID = 0;
			}
			this.NPCsTotal = this.StudentsTotal + this.TeachersTotal;
			this.SpawnID = 1;
			if (StudentGlobals.MaleUniform == 0)
			{
				StudentGlobals.MaleUniform = 1;
			}
			this.ID = 1;
			while (this.ID < this.NPCsTotal + 1)
			{
				if (!StudentGlobals.GetStudentDead(this.ID))
				{
					StudentGlobals.SetStudentDying(this.ID, false);
				}
				this.ID++;
			}
			if (!this.TakingPortraits)
			{
				this.ID = 1;
				while (this.ID < this.Lockers.List.Length)
				{
					this.LockerPositions[this.ID].transform.position = this.Lockers.List[this.ID].position + this.Lockers.List[this.ID].forward * 0.5f;
					this.LockerPositions[this.ID].LookAt(this.Lockers.List[this.ID].position);
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.HidingSpots.List.Length)
				{
					if (this.HidingSpots.List[this.ID] == null)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyObject, new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f)), Quaternion.identity);
						while (gameObject.transform.position.x < 2.5f && gameObject.transform.position.x > -2.5f && gameObject.transform.position.z > -2.5f && gameObject.transform.position.z < 2.5f)
						{
							gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f));
						}
						gameObject.transform.parent = this.HidingSpots.transform;
						this.HidingSpots.List[this.ID] = gameObject.transform;
					}
					this.ID++;
				}
			}
			if (this.YandereLate)
			{
				this.Clock.PresentTime = 480f;
				this.Clock.HourTime = 8f;
				this.SkipTo8();
			}
			if (!this.TakingPortraits)
			{
				while (this.SpawnID < this.NPCsTotal + 1)
				{
					this.SpawnStudent(this.SpawnID);
					this.SpawnID++;
				}
				this.Graffiti[1].SetActive(false);
				this.Graffiti[2].SetActive(false);
				this.Graffiti[3].SetActive(false);
				this.Graffiti[4].SetActive(false);
				this.Graffiti[5].SetActive(false);
			}
		}
		else
		{
			string str = string.Empty;
			if (this.ProblemID > 1)
			{
				str = "The problem may be caused by Student " + this.ProblemID.ToString() + ".";
			}
			if (this.ErrorLabel != null)
			{
				this.ErrorLabel.text = "The game cannot compile Students.JSON! There is a typo somewhere in the JSON file. The problem might be a missing quotation mark, a missing colon, a missing comma, or something else like that. Please find your typo and fix it, or revert to a backup of the JSON file. " + str;
				this.ErrorLabel.enabled = true;
			}
		}
		if (!this.TakingPortraits)
		{
			this.NEStairs = GameObject.Find("NEStairs").GetComponent<Collider>();
			this.NWStairs = GameObject.Find("NWStairs").GetComponent<Collider>();
			this.SEStairs = GameObject.Find("SEStairs").GetComponent<Collider>();
			this.SWStairs = GameObject.Find("SWStairs").GetComponent<Collider>();
		}
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x0015764C File Offset: 0x00155A4C
	public void SetAtmosphere()
	{
		if (GameGlobals.LoveSick)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 0f;
		}
		if (!MissionModeGlobals.MissionMode)
		{
			if (!SchoolGlobals.SchoolAtmosphereSet)
			{
				SchoolGlobals.SchoolAtmosphereSet = true;
				SchoolGlobals.SchoolAtmosphere = 1f;
			}
			this.Atmosphere = SchoolGlobals.SchoolAtmosphere;
		}
		Vignetting[] components = Camera.main.GetComponents<Vignetting>();
		float num = 1f - this.Atmosphere;
		if (!this.TakingPortraits)
		{
			this.SelectiveGreyscale.desaturation = num;
			if (this.HandSelectiveGreyscale != null)
			{
				this.HandSelectiveGreyscale.desaturation = num;
				this.SmartphoneSelectiveGreyscale.desaturation = num;
			}
			components[2].intensity = num * 5f;
			components[2].blur = num;
			components[2].chromaticAberration = num * 5f;
			float num2 = 1f - num;
			RenderSettings.fogColor = new Color(num2, num2, num2, 1f);
			Camera.main.backgroundColor = new Color(num2, num2, num2, 1f);
			RenderSettings.fogDensity = num * 0.1f;
		}
		if (this.Yandere != null)
		{
			this.Yandere.GreyTarget = num;
		}
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x0015777C File Offset: 0x00155B7C
	private void Update()
	{
		if (!this.TakingPortraits)
		{
			if (!this.Yandere.ShoulderCamera.Counselor.Interrogating)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			this.Frame++;
			if (!this.FirstUpdate)
			{
				this.QualityManager.UpdateOutlines();
				this.FirstUpdate = true;
				this.AssignTeachers();
			}
			if (this.Frame == 3)
			{
				this.LoveManager.CoupleCheck();
				if (this.Bullies > 0)
				{
					this.DetermineVictim();
				}
				this.UpdateStudents(0);
				if (!OptionGlobals.RimLight)
				{
					this.QualityManager.RimLight();
				}
				this.ID = 26;
				while (this.ID < 31)
				{
					if (this.Students[this.ID] != null)
					{
						this.OriginalClubPositions[this.ID - 25] = this.Clubs.List[this.ID].position;
						this.OriginalClubRotations[this.ID - 25] = this.Clubs.List[this.ID].rotation;
					}
					this.ID++;
				}
				if (!this.TakingPortraits)
				{
					this.TaskManager.UpdateTaskStatus();
				}
				this.Yandere.GloveAttacher.newRenderer.enabled = false;
				this.UpdateAprons();
				if (PlayerPrefs.GetInt("LoadingSave") == 1)
				{
					this.Load();
					PlayerPrefs.SetInt("LoadingSave", 0);
				}
				if (!this.YandereLate && StudentGlobals.MemorialStudents > 0)
				{
					this.Yandere.HUD.alpha = 0f;
					this.Yandere.RPGCamera.transform.position = new Vector3(38f, 4.125f, 68.825f);
					this.Yandere.RPGCamera.transform.eulerAngles = new Vector3(22.5f, 67.5f, 0f);
					this.Yandere.RPGCamera.transform.Translate(Vector3.forward, Space.Self);
					this.Yandere.RPGCamera.enabled = false;
					this.Yandere.HeartCamera.enabled = false;
					this.Yandere.CanMove = false;
					this.Clock.StopTime = true;
					this.StopMoving();
					this.MemorialScene.gameObject.SetActive(true);
					this.MemorialScene.enabled = true;
				}
				this.ID = 1;
				while (this.ID < 90)
				{
					if (this.Students[this.ID] != null)
					{
						this.Students[this.ID].ShoeRemoval.Start();
					}
					this.ID++;
				}
			}
			if ((double)this.Clock.HourTime > 16.9)
			{
				this.CheckMusic();
			}
		}
		else if (this.NPCsSpawned < this.StudentsTotal + this.TeachersTotal)
		{
			this.Frame++;
			if (this.Frame == 1)
			{
				if (this.NewStudent != null)
				{
					UnityEngine.Object.Destroy(this.NewStudent);
				}
				if (this.Randomize)
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>((UnityEngine.Random.Range(0, 2) != 0) ? this.PortraitKun : this.PortraitChan, Vector3.zero, Quaternion.identity);
				}
				else
				{
					this.NewStudent = UnityEngine.Object.Instantiate<GameObject>((this.JSON.Students[this.NPCsSpawned + 1].Gender != 0) ? this.PortraitKun : this.PortraitChan, Vector3.zero, Quaternion.identity);
				}
				CosmeticScript component = this.NewStudent.GetComponent<CosmeticScript>();
				component.StudentID = this.NPCsSpawned + 1;
				component.StudentManager = this;
				component.TakingPortrait = true;
				component.Randomize = this.Randomize;
				component.JSON = this.JSON;
				if (!this.Randomize)
				{
					this.NPCsSpawned++;
				}
			}
			if (this.Frame == 2)
			{
				ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/Portraits/Student_" + this.NPCsSpawned.ToString() + ".png");
				this.Frame = 0;
			}
		}
		else
		{
			ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/Portraits/Student_" + this.NPCsSpawned.ToString() + ".png");
			base.gameObject.SetActive(false);
		}
		if (this.Witnesses > 0)
		{
			this.ID = 1;
			while (this.ID < this.WitnessList.Length)
			{
				StudentScript studentScript = this.WitnessList[this.ID];
				if (studentScript != null && (!studentScript.Alive || studentScript.Attacked || studentScript.Dying || (studentScript.Fleeing && !studentScript.PinningDown)))
				{
					studentScript.PinDownWitness = false;
					if (this.ID != this.WitnessList.Length - 1)
					{
						this.Shuffle(this.ID);
					}
					this.Witnesses--;
				}
				this.ID++;
			}
			if (this.PinningDown && this.Witnesses < 4)
			{
				Debug.Log("Students were going to pin Yandere-chan down, but now there are less than 4 witnesses, so it's not going to happen.");
				if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
				{
					this.Yandere.CanMove = true;
				}
				this.PinningDown = false;
				this.PinDownTimer = 0f;
				this.PinPhase = 0;
			}
		}
		if (this.PinningDown)
		{
			if (!this.Yandere.Attacking && this.Yandere.CanMove)
			{
				this.Yandere.CharacterAnimation.CrossFade("f02_pinDownPanic_00");
				this.Yandere.EmptyHands();
				this.Yandere.CanMove = false;
			}
			if (this.PinPhase == 1)
			{
				if (!this.Yandere.Attacking && !this.Yandere.Struggling)
				{
					this.PinTimer += Time.deltaTime;
				}
				if (this.PinTimer > 1f)
				{
					this.ID = 1;
					while (this.ID < 5)
					{
						StudentScript studentScript2 = this.WitnessList[this.ID];
						if (studentScript2 != null)
						{
							studentScript2.transform.position = new Vector3(studentScript2.transform.position.x, studentScript2.transform.position.y + 0.1f, studentScript2.transform.position.z);
							studentScript2.CurrentDestination = this.PinDownSpots[this.ID];
							studentScript2.Pathfinding.target = this.PinDownSpots[this.ID];
							studentScript2.SprintAnim = studentScript2.OriginalSprintAnim;
							studentScript2.DistanceToDestination = 100f;
							studentScript2.Pathfinding.speed = 5f;
							studentScript2.MyController.radius = 0f;
							studentScript2.PinningDown = true;
							studentScript2.Alarmed = false;
							studentScript2.Routine = false;
							studentScript2.Fleeing = true;
							studentScript2.AlarmTimer = 0f;
							studentScript2.SmartPhone.SetActive(false);
							studentScript2.Safe = true;
							studentScript2.Prompt.Hide();
							studentScript2.Prompt.enabled = false;
							Debug.Log(studentScript2 + "'s current destination is " + studentScript2.CurrentDestination);
						}
						this.ID++;
					}
					this.PinPhase++;
				}
			}
			else if (this.WitnessList[1].PinPhase == 0)
			{
				if (!this.Yandere.ShoulderCamera.Noticed && !this.Yandere.ShoulderCamera.HeartbrokenCamera.activeInHierarchy)
				{
					this.PinDownTimer += Time.deltaTime;
					if (this.PinDownTimer > 10f || (this.WitnessList[1].DistanceToDestination < 1f && this.WitnessList[2].DistanceToDestination < 1f && this.WitnessList[3].DistanceToDestination < 1f && this.WitnessList[4].DistanceToDestination < 1f))
					{
						this.Clock.StopTime = true;
						if (this.Yandere.Aiming)
						{
							this.Yandere.StopAiming();
							this.Yandere.enabled = false;
						}
						this.Yandere.Mopping = false;
						this.Yandere.EmptyHands();
						AudioSource component2 = base.GetComponent<AudioSource>();
						component2.PlayOneShot(this.PinDownSFX);
						component2.PlayOneShot(this.YanderePinDown);
						this.Yandere.CharacterAnimation.CrossFade("f02_pinDown_00");
						this.Yandere.CanMove = false;
						this.Yandere.ShoulderCamera.LookDown = true;
						this.Yandere.RPGCamera.enabled = false;
						this.StopMoving();
						this.Yandere.ShoulderCamera.HeartbrokenCamera.GetComponent<Camera>().cullingMask |= 512;
						this.ID = 1;
						while (this.ID < 5)
						{
							StudentScript studentScript3 = this.WitnessList[this.ID];
							if (studentScript3.MyWeapon != null)
							{
								GameObjectUtils.SetLayerRecursively(studentScript3.MyWeapon.gameObject, 13);
							}
							studentScript3.CharacterAnimation.CrossFade((((!studentScript3.Male) ? "f02_pinDown_0" : "pinDown_0") + this.ID).ToString());
							studentScript3.PinPhase++;
							this.ID++;
						}
					}
				}
			}
			else
			{
				bool flag = false;
				if (!this.WitnessList[1].Male)
				{
					if (this.WitnessList[1].CharacterAnimation["f02_pinDown_01"].time >= this.WitnessList[1].CharacterAnimation["f02_pinDown_01"].length)
					{
						flag = true;
					}
				}
				else if (this.WitnessList[1].CharacterAnimation["pinDown_01"].time >= this.WitnessList[1].CharacterAnimation["pinDown_01"].length)
				{
					flag = true;
				}
				if (flag)
				{
					this.Yandere.CharacterAnimation.CrossFade("f02_pinDownLoop_00");
					this.ID = 1;
					while (this.ID < 5)
					{
						StudentScript studentScript4 = this.WitnessList[this.ID];
						studentScript4.CharacterAnimation.CrossFade((((!studentScript4.Male) ? "f02_pinDownLoop_0" : "pinDownLoop_0") + this.ID).ToString());
						this.ID++;
					}
					this.PinningDown = false;
				}
			}
		}
		if (this.Meeting)
		{
			this.UpdateMeeting();
		}
		if (Input.GetKeyDown("space"))
		{
			this.DetermineVictim();
		}
		if (this.Police != null && (this.Police.BloodParent.childCount > 0 || this.Police.LimbParent.childCount > 0 || this.Yandere.WeaponManager.MisplacedWeapons > 0))
		{
			this.CurrentID++;
			if (this.CurrentID > 97)
			{
				this.UpdateBlood();
				this.CurrentID = 1;
			}
			if (this.Students[this.CurrentID] == null)
			{
				this.CurrentID++;
			}
			else if (!this.Students[this.CurrentID].gameObject.activeInHierarchy)
			{
				this.CurrentID++;
			}
		}
		if (this.OpenCurtain)
		{
			this.OpenValue = Mathf.Lerp(this.OpenValue, 100f, Time.deltaTime * 10f);
			if (this.OpenValue > 99f)
			{
				this.OpenCurtain = false;
			}
			this.FemaleShowerCurtain.SetBlendShapeWeight(0, this.OpenValue);
		}
		this.YandereVisible = false;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x0015843C File Offset: 0x0015683C
	public void SpawnStudent(int spawnID)
	{
		bool flag = false;
		if (this.JSON.Students[spawnID].Club != ClubType.Delinquent && StudentGlobals.GetStudentReputation(spawnID) < -100)
		{
			flag = true;
		}
		if (spawnID > 9 && spawnID < 21)
		{
			flag = true;
		}
		if (!flag && this.Students[spawnID] == null && !StudentGlobals.GetStudentDead(spawnID) && !StudentGlobals.GetStudentKidnapped(spawnID) && !StudentGlobals.GetStudentArrested(spawnID) && !StudentGlobals.GetStudentExpelled(spawnID))
		{
			int num;
			if (this.JSON.Students[spawnID].Name == "Random")
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyObject, new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f)), Quaternion.identity);
				while (gameObject.transform.position.x < 2.5f && gameObject.transform.position.x > -2.5f && gameObject.transform.position.z > -2.5f && gameObject.transform.position.z < 2.5f)
				{
					gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-17f, 17f), 0f, UnityEngine.Random.Range(-17f, 17f));
				}
				gameObject.transform.parent = this.HidingSpots.transform;
				this.HidingSpots.List[spawnID] = gameObject.transform;
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.RandomPatrol, Vector3.zero, Quaternion.identity);
				gameObject2.transform.parent = this.Patrols.transform;
				this.Patrols.List[spawnID] = gameObject2.transform;
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.RandomPatrol, Vector3.zero, Quaternion.identity);
				gameObject3.transform.parent = this.CleaningSpots.transform;
				this.CleaningSpots.List[spawnID] = gameObject3.transform;
				num = ((!MissionModeGlobals.MissionMode || MissionModeGlobals.MissionTarget != spawnID) ? UnityEngine.Random.Range(0, 2) : 0);
				this.FindUnoccupiedSeat();
			}
			else
			{
				num = this.JSON.Students[spawnID].Gender;
			}
			this.NewStudent = UnityEngine.Object.Instantiate<GameObject>((num != 0) ? this.StudentKun : this.StudentChan, this.SpawnPositions[spawnID].position, Quaternion.identity);
			CosmeticScript component = this.NewStudent.GetComponent<CosmeticScript>();
			component.LoveManager = this.LoveManager;
			component.StudentManager = this;
			component.Randomize = this.Randomize;
			component.StudentID = spawnID;
			component.JSON = this.JSON;
			if (this.JSON.Students[spawnID].Name == "Random")
			{
				this.NewStudent.GetComponent<StudentScript>().CleaningSpot = this.CleaningSpots.List[spawnID];
				this.NewStudent.GetComponent<StudentScript>().CleaningRole = 3;
			}
			if (this.JSON.Students[spawnID].Club == ClubType.Bully)
			{
				this.Bullies++;
			}
			this.Students[spawnID] = this.NewStudent.GetComponent<StudentScript>();
			StudentScript studentScript = this.Students[spawnID];
			studentScript.ChaseSelectiveGrayscale.desaturation = 1f - SchoolGlobals.SchoolAtmosphere;
			studentScript.Cosmetic.TextureManager = this.TextureManager;
			studentScript.WitnessCamera = this.WitnessCamera;
			studentScript.StudentManager = this;
			studentScript.StudentID = spawnID;
			studentScript.JSON = this.JSON;
			if (studentScript.Miyuki != null)
			{
				studentScript.Miyuki.Enemy = this.MiyukiCat;
			}
			if (this.AoT)
			{
				studentScript.AoT = true;
			}
			if (this.DK)
			{
				studentScript.DK = true;
			}
			if (this.Spooky)
			{
				studentScript.Spooky = true;
			}
			if (this.Sans)
			{
				studentScript.BadTime = true;
			}
			if (spawnID == this.RivalID)
			{
				studentScript.Rival = true;
				this.RedString.transform.parent = studentScript.LeftPinky;
				this.RedString.transform.localPosition = new Vector3(0f, 0f, 0f);
			}
			if (spawnID == 1)
			{
				this.RedString.Target = studentScript.LeftPinky;
			}
			if (this.JSON.Students[spawnID].Persona == PersonaType.Protective || this.JSON.Students[spawnID].Hairstyle == "20" || this.JSON.Students[spawnID].Hairstyle == "21")
			{
				UnityEngine.Object.Destroy(studentScript);
			}
			this.OccupySeat();
		}
		this.NPCsSpawned++;
		this.ForceSpawn = false;
		if (this.Students[10] != null || this.Students[11] != null)
		{
			UnityEngine.Object.Destroy(this.Students[10].gameObject);
			UnityEngine.Object.Destroy(this.Students[11].gameObject);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x001589D0 File Offset: 0x00156DD0
	public void UpdateStudents(int SpecificStudent = 0)
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			bool flag = false;
			if (SpecificStudent != 0)
			{
				this.ID = SpecificStudent;
				flag = true;
			}
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (studentScript.gameObject.activeInHierarchy)
				{
					if (!studentScript.Safe)
					{
						if (!studentScript.Slave)
						{
							if (studentScript.Pushable)
							{
								studentScript.Prompt.Label[0].text = "     Push";
							}
							else if (this.Yandere.SpiderGrow)
							{
								if (!studentScript.Cosmetic.Empty)
								{
									studentScript.Prompt.Label[0].text = "     Send Husk";
								}
								else
								{
									studentScript.Prompt.Label[0].text = "     Talk";
								}
							}
							else if (!studentScript.Following)
							{
								studentScript.Prompt.Label[0].text = "     Talk";
							}
							else
							{
								studentScript.Prompt.Label[0].text = "     Stop";
							}
							studentScript.Prompt.HideButton[0] = false;
							studentScript.Prompt.HideButton[2] = false;
							studentScript.Prompt.Attack = false;
							if (this.Yandere.Mask != null || studentScript.Ragdoll.Zs.activeInHierarchy)
							{
								studentScript.Prompt.HideButton[0] = true;
							}
							if (this.Yandere.Dragging || this.Yandere.PickUp != null || this.Yandere.Chased)
							{
								studentScript.Prompt.HideButton[0] = true;
								studentScript.Prompt.HideButton[2] = true;
								if (this.Yandere.PickUp != null && !studentScript.Following)
								{
									if (this.Yandere.PickUp.Food > 0)
									{
										studentScript.Prompt.Label[0].text = "     Feed";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
									else if (this.Yandere.PickUp.Salty)
									{
										studentScript.Prompt.Label[0].text = "     Give Snack";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
									else if (this.Yandere.PickUp.StuckBoxCutter != null)
									{
										studentScript.Prompt.Label[0].text = "     Ask For Help";
										studentScript.Prompt.HideButton[0] = false;
										studentScript.Prompt.HideButton[2] = true;
									}
								}
							}
							if (this.Yandere.Armed)
							{
								studentScript.Prompt.HideButton[0] = true;
								studentScript.Prompt.Attack = true;
								studentScript.Prompt.MinimumDistanceSqr = 1f;
								studentScript.Prompt.MinimumDistance = 1f;
							}
							else
							{
								studentScript.Prompt.HideButton[2] = true;
								studentScript.Prompt.MinimumDistanceSqr = 2f;
								studentScript.Prompt.MinimumDistance = 2f;
								if (studentScript.WitnessedMurder || studentScript.WitnessedCorpse || studentScript.Private)
								{
									studentScript.Prompt.HideButton[0] = true;
								}
							}
							if (this.Yandere.NearBodies > 0 || this.Yandere.Sanity < 33.33333f)
							{
								studentScript.Prompt.HideButton[0] = true;
							}
							if (studentScript.Teacher)
							{
								studentScript.Prompt.HideButton[0] = true;
							}
						}
						else if (!studentScript.FragileSlave)
						{
							if (this.Yandere.Armed)
							{
								if (this.Yandere.EquippedWeapon.Concealable)
								{
									studentScript.Prompt.HideButton[0] = false;
									studentScript.Prompt.Label[0].text = "     Give Weapon";
								}
								else
								{
									studentScript.Prompt.HideButton[0] = true;
									studentScript.Prompt.Label[0].text = string.Empty;
								}
							}
							else
							{
								studentScript.Prompt.HideButton[0] = true;
								studentScript.Prompt.Label[0].text = string.Empty;
							}
						}
					}
					if (studentScript.FightingSlave && this.Yandere.Armed)
					{
						Debug.Log("Fighting with a slave!");
						studentScript.Prompt.Label[0].text = "     Stab";
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.HideButton[2] = true;
						studentScript.Prompt.enabled = true;
					}
					if (this.NoSpeech && !studentScript.Armband.activeInHierarchy)
					{
						studentScript.Prompt.HideButton[0] = true;
					}
				}
				if (studentScript.Prompt.Label[0] != null)
				{
					if (this.Sans)
					{
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Psychokinesis";
					}
					if (this.Pose)
					{
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Pose";
						studentScript.Prompt.BloodMask = 1;
						studentScript.Prompt.BloodMask |= 2;
						studentScript.Prompt.BloodMask |= 512;
						studentScript.Prompt.BloodMask |= 8192;
						studentScript.Prompt.BloodMask |= 16384;
						studentScript.Prompt.BloodMask |= 65536;
						studentScript.Prompt.BloodMask |= 2097152;
						studentScript.Prompt.BloodMask = ~studentScript.Prompt.BloodMask;
					}
					if (!studentScript.Teacher && this.Six)
					{
						studentScript.Prompt.MinimumDistance = 0.75f;
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Eat";
					}
					if (this.Gaze)
					{
						studentScript.Prompt.MinimumDistance = 5f;
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.Label[0].text = "     Gaze";
					}
				}
				if (GameGlobals.EmptyDemon)
				{
					studentScript.Prompt.HideButton[0] = false;
				}
			}
			this.ID++;
			if (flag)
			{
				this.ID = this.Students.Length;
			}
		}
		this.Container.UpdatePrompts();
		this.TrashCan.UpdatePrompt();
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x001590F8 File Offset: 0x001574F8
	public void UpdateMe(int ID)
	{
		if (ID > 1)
		{
			StudentScript studentScript = this.Students[ID];
			if (!studentScript.Safe)
			{
				studentScript.Prompt.Label[0].text = "     Talk";
				studentScript.Prompt.HideButton[0] = false;
				studentScript.Prompt.HideButton[2] = false;
				studentScript.Prompt.Attack = false;
				if (studentScript.FightingSlave)
				{
					if (this.Yandere.Armed)
					{
						Debug.Log("Fighting with a slave!");
						studentScript.Prompt.Label[0].text = "     Stab";
						studentScript.Prompt.HideButton[0] = false;
						studentScript.Prompt.HideButton[2] = true;
						studentScript.Prompt.enabled = true;
					}
				}
				else
				{
					if (this.Yandere.Armed && this.OriginalUniforms + this.NewUniforms > 0)
					{
						studentScript.Prompt.HideButton[0] = true;
						studentScript.Prompt.MinimumDistance = 1f;
						studentScript.Prompt.Attack = true;
					}
					else
					{
						studentScript.Prompt.HideButton[2] = true;
						studentScript.Prompt.MinimumDistance = 2f;
						if (studentScript.WitnessedMurder || studentScript.WitnessedCorpse || studentScript.Private)
						{
							studentScript.Prompt.HideButton[0] = true;
						}
					}
					if (this.Yandere.Dragging || this.Yandere.PickUp != null || this.Yandere.Chased || this.Yandere.Chasers > 0)
					{
						studentScript.Prompt.HideButton[0] = true;
						studentScript.Prompt.HideButton[2] = true;
					}
					if (this.Yandere.NearBodies > 0 || this.Yandere.Sanity < 33.33333f)
					{
						studentScript.Prompt.HideButton[0] = true;
					}
					if (studentScript.Teacher)
					{
						studentScript.Prompt.HideButton[0] = true;
					}
				}
			}
			if (this.Sans)
			{
				studentScript.Prompt.HideButton[0] = false;
				studentScript.Prompt.Label[0].text = "     Psychokinesis";
			}
			if (this.Pose)
			{
				studentScript.Prompt.HideButton[0] = false;
				studentScript.Prompt.Label[0].text = "     Pose";
			}
			if (this.NoSpeech || studentScript.Ragdoll.Zs.activeInHierarchy)
			{
				studentScript.Prompt.HideButton[0] = true;
			}
		}
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x001593A8 File Offset: 0x001577A8
	public void AttendClass()
	{
		this.ConvoManager.Confirmed = false;
		this.SleuthPhase = 3;
		if (this.RingEvent.EventActive)
		{
			this.RingEvent.ReturnRing();
		}
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}
		if (this.Clock.LateStudent)
		{
			this.Clock.ActivateLateStudent();
		}
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (studentScript.WitnessedBloodPool && !studentScript.WitnessedMurder && !studentScript.WitnessedCorpse)
				{
					studentScript.Fleeing = false;
					studentScript.Alarmed = false;
					studentScript.AlarmTimer = 0f;
					studentScript.ReportPhase = 0;
					studentScript.WitnessedBloodPool = false;
				}
				if (studentScript.HoldingHands)
				{
					studentScript.HoldingHands = false;
					studentScript.Paired = false;
					studentScript.enabled = true;
				}
				if (studentScript.Alive && !studentScript.Slave && !studentScript.Tranquil && !studentScript.Fleeing && studentScript.enabled && studentScript.gameObject.activeInHierarchy)
				{
					if (!studentScript.Started)
					{
						studentScript.Start();
					}
					if (!studentScript.Teacher)
					{
						if (!studentScript.Indoors)
						{
							if (studentScript.ShoeRemoval.Locker == null)
							{
								studentScript.ShoeRemoval.Start();
							}
							studentScript.ShoeRemoval.PutOnShoes();
						}
						studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
						studentScript.transform.rotation = studentScript.Seat.rotation;
						studentScript.Character.GetComponent<Animation>().Play(studentScript.SitAnim);
						studentScript.Pathfinding.canSearch = false;
						studentScript.Pathfinding.canMove = false;
						studentScript.Pathfinding.speed = 0f;
						studentScript.ClubActivityPhase = 0;
						studentScript.ClubTimer = 0f;
						studentScript.Pestered = 0;
						studentScript.Distracting = false;
						studentScript.Distracted = false;
						studentScript.Tripping = false;
						studentScript.Ignoring = false;
						studentScript.Pushable = false;
						studentScript.Vomiting = false;
						studentScript.Private = false;
						studentScript.Sedated = false;
						studentScript.Emetic = false;
						studentScript.Hurry = false;
						studentScript.Safe = false;
						studentScript.CanTalk = true;
						studentScript.Routine = true;
						if (studentScript.Wet)
						{
							this.CommunalLocker.Student = null;
							studentScript.Schoolwear = 3;
							studentScript.ChangeSchoolwear();
							studentScript.LiquidProjector.enabled = false;
							studentScript.Splashed = false;
							studentScript.Bloody = false;
							studentScript.BathePhase = 1;
							studentScript.Wet = false;
							studentScript.UnWet();
							if (studentScript.Rival && this.CommunalLocker.RivalPhone.Stolen)
							{
								studentScript.RealizePhoneIsMissing();
							}
						}
						if (studentScript.ClubAttire)
						{
							studentScript.ChangeSchoolwear();
							studentScript.ClubAttire = false;
						}
						if (studentScript.Schoolwear != 1 && !studentScript.BeenSplashed)
						{
							studentScript.Schoolwear = 1;
							studentScript.ChangeSchoolwear();
						}
						if (studentScript.Meeting && this.Clock.HourTime > studentScript.MeetTime)
						{
							studentScript.Meeting = false;
						}
						if (studentScript.Club == ClubType.Sports)
						{
							studentScript.SetSplashes(false);
							studentScript.WalkAnim = studentScript.OriginalWalkAnim;
							studentScript.Character.transform.localPosition = new Vector3(0f, 0f, 0f);
							studentScript.Cosmetic.Goggles[studentScript.StudentID].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
							if (!studentScript.Cosmetic.Empty)
							{
								studentScript.Cosmetic.MaleHair[studentScript.Cosmetic.Hairstyle].GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0f);
							}
						}
						if (studentScript.MyPlate != null && studentScript.MyPlate.transform.parent == studentScript.RightHand)
						{
							studentScript.MyPlate.transform.parent = null;
							studentScript.MyPlate.transform.position = studentScript.OriginalPlatePosition;
							studentScript.MyPlate.transform.rotation = studentScript.OriginalPlateRotation;
							studentScript.IdleAnim = studentScript.OriginalIdleAnim;
							studentScript.WalkAnim = studentScript.OriginalWalkAnim;
						}
						if (studentScript.ReturningMisplacedWeapon)
						{
							studentScript.ReturnMisplacedWeapon();
						}
					}
					else if (this.ID != this.GymTeacherID && this.ID != this.NurseID)
					{
						studentScript.transform.position = this.Podiums.List[studentScript.Class].position + Vector3.up * 0.01f;
						studentScript.transform.rotation = this.Podiums.List[studentScript.Class].rotation;
					}
					else
					{
						studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
						studentScript.transform.rotation = studentScript.Seat.rotation;
					}
				}
			}
			this.ID++;
		}
		this.UpdateStudents(0);
		Physics.SyncTransforms();
		if (GameGlobals.SenpaiMourning)
		{
			this.Students[1].gameObject.SetActive(false);
		}
		for (int i = 1; i < 10; i++)
		{
			if (this.ShrineCollectibles[i] != null)
			{
				this.ShrineCollectibles[i].SetActive(true);
			}
		}
		this.Gift.SetActive(false);
	}

	// Token: 0x06002090 RID: 8336 RVA: 0x001599A8 File Offset: 0x00157DA8
	public void SkipTo8()
	{
		while (this.NPCsSpawned < this.NPCsTotal)
		{
			this.SpawnStudent(this.SpawnID);
			this.SpawnID++;
		}
		int num = 0;
		int num2 = 0;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive && !studentScript.Slave && !studentScript.Tranquil)
			{
				if (!studentScript.Started)
				{
					studentScript.Start();
				}
				bool flag = false;
				if (this.MemorialScene.enabled && studentScript.Teacher)
				{
					flag = true;
					studentScript.Teacher = false;
				}
				if (!studentScript.Teacher)
				{
					if (!studentScript.Indoors)
					{
						if (studentScript.ShoeRemoval.Locker == null)
						{
							studentScript.ShoeRemoval.Start();
						}
						studentScript.ShoeRemoval.PutOnShoes();
					}
					studentScript.transform.position = studentScript.Seat.position + Vector3.up * 0.01f;
					studentScript.transform.rotation = studentScript.Seat.rotation;
					studentScript.Pathfinding.canSearch = true;
					studentScript.Pathfinding.canMove = true;
					studentScript.Pathfinding.speed = 1f;
					studentScript.ClubActivityPhase = 0;
					studentScript.Distracted = false;
					studentScript.Spawned = true;
					studentScript.Routine = true;
					studentScript.Safe = false;
					studentScript.SprintAnim = studentScript.OriginalSprintAnim;
					if (studentScript.ClubAttire)
					{
						studentScript.ChangeSchoolwear();
						studentScript.ClubAttire = true;
					}
					studentScript.TeleportToDestination();
					studentScript.TeleportToDestination();
				}
				else
				{
					studentScript.TeleportToDestination();
					studentScript.TeleportToDestination();
				}
				if (this.MemorialScene.enabled)
				{
					if (flag)
					{
						studentScript.Teacher = true;
					}
					if (studentScript.Persona == PersonaType.PhoneAddict)
					{
						studentScript.SmartPhone.SetActive(true);
					}
					if (studentScript.Actions[studentScript.Phase] == StudentActionType.Graffiti && !this.Bully)
					{
						ScheduleBlock scheduleBlock = studentScript.ScheduleBlocks[2];
						scheduleBlock.destination = "Patrol";
						scheduleBlock.action = "Patrol";
						studentScript.GetDestinations();
					}
					studentScript.SpeechLines.Stop();
					studentScript.transform.position = new Vector3(20f + (float)num * 1.1f, 0f, (float)(82 - num2 * 5));
					num2++;
					if (num2 > 4)
					{
						num++;
						num2 = 0;
					}
				}
			}
			this.ID++;
		}
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x00159C5C File Offset: 0x0015805C
	public void ResumeMovement()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Fleeing)
			{
				studentScript.Pathfinding.canSearch = true;
				studentScript.Pathfinding.canMove = true;
				studentScript.Pathfinding.speed = 1f;
				studentScript.Routine = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x00159CEC File Offset: 0x001580EC
	public void StopMoving()
	{
		this.CombatMinigame.enabled = false;
		this.Stop = true;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (!studentScript.Dying && !studentScript.PinningDown && !studentScript.Spraying)
				{
					if (this.YandereDying && studentScript.Club != ClubType.Council)
					{
						studentScript.IdleAnim = studentScript.ScaredAnim;
					}
					if (this.Yandere.Attacking)
					{
						if (studentScript.MurderReaction == 0)
						{
							studentScript.Character.GetComponent<Animation>().CrossFade(studentScript.ScaredAnim);
						}
					}
					else if (this.ID > 1 && studentScript.CharacterAnimation != null)
					{
						studentScript.CharacterAnimation.CrossFade(studentScript.IdleAnim);
					}
					studentScript.Pathfinding.canSearch = false;
					studentScript.Pathfinding.canMove = false;
					studentScript.Pathfinding.speed = 0f;
					studentScript.Stop = true;
					if (studentScript.EventManager != null)
					{
						studentScript.EventManager.EndEvent();
					}
				}
				if (studentScript.Alive && studentScript.SawMask)
				{
					this.Police.MaskReported = true;
				}
				if (studentScript.Slave && this.Police.DayOver)
				{
					Debug.Log("A mind-broken slave committed suicide.");
					studentScript.Broken.Subtitle.text = string.Empty;
					studentScript.Broken.Done = true;
					UnityEngine.Object.Destroy(studentScript.Broken);
					studentScript.BecomeRagdoll();
					studentScript.Slave = false;
					studentScript.Suicide = true;
					studentScript.DeathType = DeathType.Mystery;
					StudentGlobals.SetStudentSlave(studentScript.StudentID);
				}
			}
			this.ID++;
		}
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x00159EE4 File Offset: 0x001582E4
	public void TimeFreeze()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive)
			{
				studentScript.enabled = false;
				studentScript.CharacterAnimation.Stop();
				studentScript.Pathfinding.canSearch = false;
				studentScript.Pathfinding.canMove = false;
				studentScript.Prompt.Hide();
				studentScript.Prompt.enabled = false;
			}
			this.ID++;
		}
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x00159F84 File Offset: 0x00158384
	public void TimeUnfreeze()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.Alive)
			{
				studentScript.enabled = true;
				studentScript.Prompt.enabled = true;
				studentScript.Pathfinding.canSearch = true;
				studentScript.Pathfinding.canMove = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06002095 RID: 8341 RVA: 0x0015A010 File Offset: 0x00158410
	public void ComeBack()
	{
		this.Stop = false;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (!studentScript.Dying && !studentScript.Replaced && studentScript.Spawned && !StudentGlobals.GetStudentExpelled(this.ID) && !studentScript.Ragdoll.Disposed)
				{
					studentScript.gameObject.SetActive(true);
					studentScript.Pathfinding.canSearch = true;
					studentScript.Pathfinding.canMove = true;
					studentScript.Pathfinding.speed = 1f;
					studentScript.Stop = false;
				}
				if (studentScript.Teacher)
				{
					studentScript.CurrentDestination = studentScript.Destinations[studentScript.Phase];
					studentScript.Pathfinding.target = studentScript.Destinations[studentScript.Phase];
					studentScript.Alarmed = false;
					studentScript.Reacted = false;
					studentScript.Witness = false;
					studentScript.Routine = true;
					studentScript.AlarmTimer = 0f;
					studentScript.Concern = 0;
				}
				if (studentScript.Club == ClubType.Council)
				{
					studentScript.Teacher = false;
				}
				if (studentScript.Slave)
				{
					studentScript.Stop = false;
				}
			}
			this.ID++;
		}
		this.UpdateAllAnimLayers();
		if (this.Police.EndOfDay.RivalEliminationMethod == RivalEliminationType.Expelled)
		{
			this.Students[this.RivalID].gameObject.SetActive(false);
		}
		if (GameGlobals.SenpaiMourning)
		{
			this.Students[1].gameObject.SetActive(false);
		}
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x0015A1C4 File Offset: 0x001585C4
	public void StopFleeing()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Teacher)
			{
				studentScript.Pathfinding.target = studentScript.Destinations[studentScript.Phase];
				studentScript.Pathfinding.speed = 1f;
				studentScript.WitnessedCorpse = false;
				studentScript.WitnessedMurder = false;
				studentScript.Alarmed = false;
				studentScript.Fleeing = false;
				studentScript.Reacted = false;
				studentScript.Witness = false;
				studentScript.Routine = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x0015A27C File Offset: 0x0015867C
	public void EnablePrompts()
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Prompt.enabled = true;
			}
			this.ID++;
		}
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x0015A2DC File Offset: 0x001586DC
	public void DisablePrompts()
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Prompt.Hide();
				studentScript.Prompt.enabled = false;
			}
			this.ID++;
		}
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x0015A348 File Offset: 0x00158748
	public void WipePendingRep()
	{
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.PendingRep = 0f;
			}
			this.ID++;
		}
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x0015A3A8 File Offset: 0x001587A8
	public void AttackOnTitan()
	{
		this.AoT = true;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Teacher)
			{
				studentScript.AttackOnTitan();
			}
			this.ID++;
		}
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x0015A414 File Offset: 0x00158814
	public void Kong()
	{
		this.DK = true;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.DK = true;
			}
			this.ID++;
		}
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x0015A478 File Offset: 0x00158878
	public void Spook()
	{
		this.Spooky = true;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Male)
			{
				studentScript.Spook();
			}
			this.ID++;
		}
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x0015A4E4 File Offset: 0x001588E4
	public void BadTime()
	{
		this.Sans = true;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Prompt.HideButton[0] = false;
				studentScript.BadTime = true;
			}
			this.ID++;
		}
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x0015A554 File Offset: 0x00158954
	public void UpdateBooths()
	{
		this.ID = 0;
		while (this.ID < this.ChangingBooths.Length)
		{
			ChangingBoothScript changingBoothScript = this.ChangingBooths[this.ID];
			if (changingBoothScript != null)
			{
				changingBoothScript.CheckYandereClub();
			}
			this.ID++;
		}
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x0015A5B0 File Offset: 0x001589B0
	public void UpdatePerception()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.UpdatePerception();
			}
			this.ID++;
		}
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x0015A60C File Offset: 0x00158A0C
	public void StopHesitating()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				if (studentScript.AlarmTimer > 0f)
				{
					studentScript.AlarmTimer = 1f;
				}
				studentScript.Hesitation = 0f;
			}
			this.ID++;
		}
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x0015A688 File Offset: 0x00158A88
	public void Unstop()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Stop = false;
			}
			this.ID++;
		}
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x0015A6E4 File Offset: 0x00158AE4
	public void LowerCorpsePosition()
	{
		int num;
		if (this.CorpseLocation.position.y < 2f)
		{
			num = 0;
		}
		else if (this.CorpseLocation.position.y < 4f)
		{
			num = 2;
		}
		else if (this.CorpseLocation.position.y < 6f)
		{
			num = 4;
		}
		else if (this.CorpseLocation.position.y < 8f)
		{
			num = 6;
		}
		else if (this.CorpseLocation.position.y < 10f)
		{
			num = 8;
		}
		else if (this.CorpseLocation.position.y < 12f)
		{
			num = 10;
		}
		else
		{
			num = 12;
		}
		this.CorpseLocation.position = new Vector3(this.CorpseLocation.position.x, (float)num, this.CorpseLocation.position.z);
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x0015A80C File Offset: 0x00158C0C
	public void LowerBloodPosition()
	{
		int num;
		if (this.BloodLocation.position.y < 2f)
		{
			num = 0;
		}
		else if (this.BloodLocation.position.y < 4f)
		{
			num = 2;
		}
		else if (this.BloodLocation.position.y < 6f)
		{
			num = 4;
		}
		else if (this.BloodLocation.position.y < 8f)
		{
			num = 6;
		}
		else if (this.BloodLocation.position.y < 10f)
		{
			num = 8;
		}
		else if (this.BloodLocation.position.y < 12f)
		{
			num = 10;
		}
		else
		{
			num = 12;
		}
		this.BloodLocation.position = new Vector3(this.BloodLocation.position.x, (float)num, this.BloodLocation.position.z);
	}

	// Token: 0x060020A4 RID: 8356 RVA: 0x0015A934 File Offset: 0x00158D34
	public void CensorStudents()
	{
		this.ID = 0;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Male && studentScript.Club != ClubType.Teacher && studentScript.Club != ClubType.GymTeacher && studentScript.Club != ClubType.Nurse)
			{
				if (this.Censor)
				{
					studentScript.Cosmetic.CensorPanties();
				}
				else
				{
					studentScript.Cosmetic.RemoveCensor();
				}
			}
			this.ID++;
		}
	}

	// Token: 0x060020A5 RID: 8357 RVA: 0x0015A9E0 File Offset: 0x00158DE0
	private void OccupySeat()
	{
		int @class = this.JSON.Students[this.SpawnID].Class;
		int seat = this.JSON.Students[this.SpawnID].Seat;
		if (@class == 11)
		{
			this.SeatsTaken11[seat] = true;
		}
		else if (@class == 12)
		{
			this.SeatsTaken12[seat] = true;
		}
		else if (@class == 21)
		{
			this.SeatsTaken21[seat] = true;
		}
		else if (@class == 22)
		{
			this.SeatsTaken22[seat] = true;
		}
		else if (@class == 31)
		{
			this.SeatsTaken31[seat] = true;
		}
		else if (@class == 32)
		{
			this.SeatsTaken32[seat] = true;
		}
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x0015AA9C File Offset: 0x00158E9C
	private void FindUnoccupiedSeat()
	{
		this.SeatOccupied = false;
		if (this.Class == 1)
		{
			this.JSON.Students[this.SpawnID].Class = 11;
			this.ID = 1;
			while (this.ID < this.SeatsTaken11.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken11[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken11[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 2)
		{
			this.JSON.Students[this.SpawnID].Class = 12;
			this.ID = 1;
			while (this.ID < this.SeatsTaken12.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken12[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken12[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 3)
		{
			this.JSON.Students[this.SpawnID].Class = 21;
			this.ID = 1;
			while (this.ID < this.SeatsTaken21.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken21[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken21[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 4)
		{
			this.JSON.Students[this.SpawnID].Class = 22;
			this.ID = 1;
			while (this.ID < this.SeatsTaken22.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken22[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken22[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 5)
		{
			this.JSON.Students[this.SpawnID].Class = 31;
			this.ID = 1;
			while (this.ID < this.SeatsTaken31.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken31[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken31[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		else if (this.Class == 6)
		{
			this.JSON.Students[this.SpawnID].Class = 32;
			this.ID = 1;
			while (this.ID < this.SeatsTaken32.Length && !this.SeatOccupied)
			{
				if (!this.SeatsTaken32[this.ID])
				{
					this.JSON.Students[this.SpawnID].Seat = this.ID;
					this.SeatsTaken32[this.ID] = true;
					this.SeatOccupied = true;
				}
				this.ID++;
				if (this.ID > 15)
				{
					this.Class++;
				}
			}
		}
		if (!this.SeatOccupied)
		{
			this.FindUnoccupiedSeat();
		}
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x0015AF44 File Offset: 0x00159344
	public void PinDownCheck()
	{
		if (!this.PinningDown && this.Witnesses > 3)
		{
			this.ID = 1;
			while (this.ID < this.WitnessList.Length)
			{
				StudentScript studentScript = this.WitnessList[this.ID];
				if (studentScript != null && (!studentScript.Alive || studentScript.Attacked || studentScript.Fleeing || studentScript.Dying))
				{
					if (this.ID != this.WitnessList.Length - 1)
					{
						this.Shuffle(this.ID);
					}
					this.Witnesses--;
				}
				this.ID++;
			}
			if (this.Witnesses > 3)
			{
				this.PinningDown = true;
				this.PinPhase = 1;
			}
		}
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x0015B028 File Offset: 0x00159428
	private void Shuffle(int Start)
	{
		for (int i = Start; i < this.WitnessList.Length - 1; i++)
		{
			this.WitnessList[i] = this.WitnessList[i + 1];
		}
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x0015B064 File Offset: 0x00159464
	public void RemovePapersFromDesks()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.MyPaper != null)
			{
				studentScript.MyPaper.SetActive(false);
			}
			this.ID++;
		}
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x0015B0D8 File Offset: 0x001594D8
	public void SetStudentsActive(bool active)
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.gameObject.SetActive(active);
			}
			this.ID++;
		}
	}

	// Token: 0x060020AB RID: 8363 RVA: 0x0015B138 File Offset: 0x00159538
	public void AssignTeachers()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.MyTeacher = this.Teachers[this.JSON.Students[studentScript.StudentID].Class];
			}
			this.ID++;
		}
	}

	// Token: 0x060020AC RID: 8364 RVA: 0x0015B1B0 File Offset: 0x001595B0
	public void ToggleBookBags()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.BookBag.SetActive(!studentScript.BookBag.activeInHierarchy);
			}
			this.ID++;
		}
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x0015B220 File Offset: 0x00159620
	public void DetermineVictim()
	{
		this.Bully = false;
		this.ID = 2;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && (float)StudentGlobals.GetStudentReputation(this.ID) < 33.33333f)
			{
				if (this.ID != 36 || TaskGlobals.GetTaskStatus(36) != 3)
				{
					if (!studentScript.Teacher && !studentScript.Slave && studentScript.Club != ClubType.Bully && studentScript.Club != ClubType.Council && studentScript.Club != ClubType.Photography && studentScript.Club != ClubType.Delinquent && (float)StudentGlobals.GetStudentReputation(this.ID) < this.LowestRep)
					{
						this.LowestRep = (float)StudentGlobals.GetStudentReputation(this.ID);
						this.VictimID = this.ID;
						this.Bully = true;
					}
				}
			}
			this.ID++;
		}
		if (this.Bully)
		{
			Debug.Log("A student has been chosen to be bullied. It's Student #" + this.VictimID + ".");
			if (this.Students[this.VictimID].Seat.position.x > 0f)
			{
				this.BullyGroup.position = this.Students[this.VictimID].Seat.position + new Vector3(0.33333f, 0f, 0f);
			}
			else
			{
				this.BullyGroup.position = this.Students[this.VictimID].Seat.position - new Vector3(0.33333f, 0f, 0f);
				this.BullyGroup.eulerAngles = new Vector3(0f, 90f, 0f);
			}
			StudentScript studentScript2 = this.Students[this.VictimID];
			ScheduleBlock scheduleBlock = studentScript2.ScheduleBlocks[2];
			scheduleBlock.destination = "ShameSpot";
			scheduleBlock.action = "Shamed";
			scheduleBlock.time = 8f;
			ScheduleBlock scheduleBlock2 = studentScript2.ScheduleBlocks[4];
			scheduleBlock2.destination = "Seat";
			scheduleBlock2.action = "Sit";
			if (studentScript2.Male)
			{
				studentScript2.ChemistScanner.MyRenderer.materials[1].mainTexture = studentScript2.ChemistScanner.SadEyes;
				studentScript2.ChemistScanner.enabled = false;
			}
			studentScript2.IdleAnim = studentScript2.BulliedIdleAnim;
			studentScript2.WalkAnim = studentScript2.BulliedWalkAnim;
			studentScript2.Bullied = true;
			studentScript2.GetDestinations();
			studentScript2.CameraAnims = studentScript2.CowardAnims;
			studentScript2.BusyAtLunch = true;
			studentScript2.Shy = false;
		}
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x0015B4F4 File Offset: 0x001598F4
	public void SecurityCameras()
	{
		this.Egg = true;
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && studentScript.SecurityCamera != null && studentScript.Alive)
			{
				Debug.Log("Enabling security camera on this character's head.");
				studentScript.SecurityCamera.SetActive(true);
			}
			this.ID++;
		}
	}

	// Token: 0x060020AF RID: 8367 RVA: 0x0015B584 File Offset: 0x00159984
	public void DisableEveryone()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null && !studentScript.Ragdoll.enabled)
			{
				studentScript.gameObject.SetActive(false);
			}
			this.ID++;
		}
	}

	// Token: 0x060020B0 RID: 8368 RVA: 0x0015B5F4 File Offset: 0x001599F4
	public void DisableStudent(int DisableID)
	{
		StudentScript studentScript = this.Students[DisableID];
		if (studentScript != null)
		{
			if (studentScript.gameObject.activeInHierarchy)
			{
				studentScript.gameObject.SetActive(false);
			}
			else
			{
				studentScript.gameObject.SetActive(true);
				this.UpdateOneAnimLayer(DisableID);
				this.Students[DisableID].ReadPhase = 0;
			}
		}
	}

	// Token: 0x060020B1 RID: 8369 RVA: 0x0015B658 File Offset: 0x00159A58
	public void UpdateOneAnimLayer(int DisableID)
	{
		this.Students[DisableID].UpdateAnimLayers();
		this.Students[DisableID].ReadPhase = 0;
	}

	// Token: 0x060020B2 RID: 8370 RVA: 0x0015B678 File Offset: 0x00159A78
	public void UpdateAllAnimLayers()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.UpdateAnimLayers();
				studentScript.ReadPhase = 0;
			}
			this.ID++;
		}
	}

	// Token: 0x060020B3 RID: 8371 RVA: 0x0015B6DC File Offset: 0x00159ADC
	public void UpdateGrafitti()
	{
		this.ID = 1;
		while (this.ID < 6)
		{
			if (!this.NoBully[this.ID])
			{
				this.Graffiti[this.ID].SetActive(true);
			}
			this.ID++;
		}
	}

	// Token: 0x060020B4 RID: 8372 RVA: 0x0015B734 File Offset: 0x00159B34
	public void UpdateAllBentos()
	{
		this.ID = 1;
		while (this.ID < this.Students.Length)
		{
			StudentScript studentScript = this.Students[this.ID];
			if (studentScript != null)
			{
				studentScript.Bento.GetComponent<GenericBentoScript>().Prompt.Yandere = this.Yandere;
				studentScript.Bento.GetComponent<GenericBentoScript>().UpdatePrompts();
			}
			this.ID++;
		}
	}

	// Token: 0x060020B5 RID: 8373 RVA: 0x0015B7B4 File Offset: 0x00159BB4
	public void UpdateSleuths()
	{
		this.SleuthPhase++;
		this.ID = 56;
		while (this.ID < 61)
		{
			if (this.Students[this.ID] != null && !this.Students[this.ID].Slave && !this.Students[this.ID].Following)
			{
				if (this.SleuthPhase < 3)
				{
					this.Students[this.ID].SleuthTarget = this.SleuthDestinations[this.ID - 55];
					this.Students[this.ID].Pathfinding.target = this.Students[this.ID].SleuthTarget;
					this.Students[this.ID].CurrentDestination = this.Students[this.ID].SleuthTarget;
				}
				else if (this.SleuthPhase == 3)
				{
					this.Students[this.ID].GetSleuthTarget();
				}
				else if (this.SleuthPhase == 4)
				{
					this.Students[this.ID].SleuthTarget = this.Clubs.List[this.ID];
					this.Students[this.ID].Pathfinding.target = this.Students[this.ID].SleuthTarget;
					this.Students[this.ID].CurrentDestination = this.Students[this.ID].SleuthTarget;
				}
				this.Students[this.ID].SmartPhone.SetActive(true);
				this.Students[this.ID].SpeechLines.Stop();
			}
			this.ID++;
		}
	}

	// Token: 0x060020B6 RID: 8374 RVA: 0x0015B98C File Offset: 0x00159D8C
	public void UpdateDrama()
	{
		if (!this.MemorialScene.gameObject.activeInHierarchy)
		{
			this.DramaPhase++;
			this.ID = 26;
			while (this.ID < 31)
			{
				if (this.Students[this.ID] != null)
				{
					if (this.DramaPhase == 1)
					{
						this.Clubs.List[this.ID].position = this.OriginalClubPositions[this.ID - 25];
						this.Clubs.List[this.ID].rotation = this.OriginalClubRotations[this.ID - 25];
						this.Students[this.ID].ClubAnim = this.Students[this.ID].OriginalClubAnim;
					}
					else if (this.DramaPhase == 2)
					{
						this.Clubs.List[this.ID].position = this.DramaSpots[this.ID - 25].position;
						this.Clubs.List[this.ID].rotation = this.DramaSpots[this.ID - 25].rotation;
						if (this.ID == 26)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ActAnim;
						}
						else if (this.ID == 27)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ThinkAnim;
						}
						else if (this.ID == 28)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ThinkAnim;
						}
						else if (this.ID == 29)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ActAnim;
						}
						else if (this.ID == 30)
						{
							this.Students[this.ID].ClubAnim = this.Students[this.ID].ThinkAnim;
						}
					}
					else if (this.DramaPhase == 3)
					{
						this.Clubs.List[this.ID].position = this.BackstageSpots[this.ID - 25].position;
						this.Clubs.List[this.ID].rotation = this.BackstageSpots[this.ID - 25].rotation;
					}
					else if (this.DramaPhase == 4)
					{
						this.DramaPhase = 1;
						this.UpdateDrama();
					}
					this.Students[this.ID].DistanceToDestination = 100f;
					this.Students[this.ID].SmartPhone.SetActive(false);
					this.Students[this.ID].SpeechLines.Stop();
				}
				this.ID++;
			}
		}
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x0015BCCC File Offset: 0x0015A0CC
	public void UpdateMartialArts()
	{
		this.ConvoManager.Confirmed = false;
		this.MartialArtsPhase++;
		this.ID = 46;
		while (this.ID < 51)
		{
			if (this.Students[this.ID] != null)
			{
				if (this.MartialArtsPhase == 1)
				{
					this.Clubs.List[this.ID].position = this.MartialArtsSpots[this.ID - 45].position;
					this.Clubs.List[this.ID].rotation = this.MartialArtsSpots[this.ID - 45].rotation;
				}
				else if (this.MartialArtsPhase == 2)
				{
					this.Clubs.List[this.ID].position = this.MartialArtsSpots[this.ID - 40].position;
					this.Clubs.List[this.ID].rotation = this.MartialArtsSpots[this.ID - 40].rotation;
				}
				else if (this.MartialArtsPhase == 3)
				{
					this.Clubs.List[this.ID].position = this.MartialArtsSpots[this.ID - 35].position;
					this.Clubs.List[this.ID].rotation = this.MartialArtsSpots[this.ID - 35].rotation;
				}
				else if (this.MartialArtsPhase == 4)
				{
					this.MartialArtsPhase = 0;
					this.UpdateMartialArts();
				}
				this.Students[this.ID].DistanceToDestination = 100f;
				this.Students[this.ID].SmartPhone.SetActive(false);
				this.Students[this.ID].SpeechLines.Stop();
			}
			this.ID++;
		}
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x0015BED0 File Offset: 0x0015A2D0
	public void UpdateMeeting()
	{
		this.MeetingTimer += Time.deltaTime;
		if (this.MeetingTimer > 5f)
		{
			this.Speaker += 5;
			if (this.Speaker == 91)
			{
				this.Speaker = 21;
			}
			else if (this.Speaker == 76)
			{
				this.Speaker = 86;
			}
			else if (this.Speaker == 36)
			{
				this.Speaker = 41;
			}
			this.MeetingTimer = 0f;
		}
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x0015BF64 File Offset: 0x0015A364
	public void CheckMusic()
	{
		int num = 0;
		this.ID = 51;
		while (this.ID < 56)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].Routine && this.Students[this.ID].DistanceToDestination < 0.1f)
			{
				num++;
			}
			this.ID++;
		}
		if (num == 5)
		{
			this.PracticeVocals.pitch = Time.timeScale;
			this.PracticeMusic.pitch = Time.timeScale;
			if (!this.PracticeMusic.isPlaying)
			{
				this.PracticeVocals.Play();
				this.PracticeMusic.Play();
			}
		}
		else
		{
			this.PracticeVocals.Stop();
			this.PracticeMusic.Stop();
		}
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x0015C054 File Offset: 0x0015A454
	public void UpdateAprons()
	{
		this.ID = 21;
		while (this.ID < 26)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].ClubMemberID > 0 && this.Students[this.ID].ApronAttacher != null && this.Students[this.ID].ApronAttacher.newRenderer != null)
			{
				this.Students[this.ID].ApronAttacher.newRenderer.material.mainTexture = this.Students[this.ID].Cosmetic.ApronTextures[this.Students[this.ID].ClubMemberID];
			}
			this.ID++;
		}
	}

	// Token: 0x060020BB RID: 8379 RVA: 0x0015C144 File Offset: 0x0015A544
	public void PreventAlarm()
	{
		this.ID = 1;
		while (this.ID < 101)
		{
			if (this.Students[this.ID] != null)
			{
				this.Students[this.ID].Alarm = 0f;
			}
			this.ID++;
		}
	}

	// Token: 0x060020BC RID: 8380 RVA: 0x0015C1A8 File Offset: 0x0015A5A8
	public void VolumeDown()
	{
		this.ID = 51;
		while (this.ID < 56)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID] != null)
			{
				this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID].GetComponent<AudioSource>().volume = 0.2f;
			}
			this.ID++;
		}
	}

	// Token: 0x060020BD RID: 8381 RVA: 0x0015C25C File Offset: 0x0015A65C
	public void VolumeUp()
	{
		this.ID = 51;
		while (this.ID < 56)
		{
			if (this.Students[this.ID] != null && this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID] != null)
			{
				this.Students[this.ID].Instruments[this.Students[this.ID].ClubMemberID].GetComponent<AudioSource>().volume = 1f;
			}
			this.ID++;
		}
	}

	// Token: 0x060020BE RID: 8382 RVA: 0x0015C310 File Offset: 0x0015A710
	public void GetMaleVomitSpot(StudentScript VomitStudent)
	{
		this.MaleVomitSpot = this.MaleVomitSpots[1];
		VomitStudent.VomitDoor = this.MaleToiletDoors[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.MaleVomitSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, this.MaleVomitSpot.position))
			{
				this.MaleVomitSpot = this.MaleVomitSpots[this.ID];
				VomitStudent.VomitDoor = this.MaleToiletDoors[this.ID];
			}
			this.ID++;
		}
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x0015C3C8 File Offset: 0x0015A7C8
	public void GetFemaleVomitSpot(StudentScript VomitStudent)
	{
		this.FemaleVomitSpot = this.FemaleVomitSpots[1];
		VomitStudent.VomitDoor = this.FemaleToiletDoors[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.FemaleVomitSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, this.FemaleVomitSpot.position))
			{
				this.FemaleVomitSpot = this.FemaleVomitSpots[this.ID];
				VomitStudent.VomitDoor = this.FemaleToiletDoors[this.ID];
			}
			this.ID++;
		}
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x0015C480 File Offset: 0x0015A880
	public void GetMaleWashSpot(StudentScript VomitStudent)
	{
		Transform transform = this.MaleWashSpots[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.MaleWashSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, transform.position))
			{
				transform = this.MaleWashSpots[this.ID];
			}
			this.ID++;
		}
		this.MaleWashSpot = transform;
	}

	// Token: 0x060020C1 RID: 8385 RVA: 0x0015C510 File Offset: 0x0015A910
	public void GetFemaleWashSpot(StudentScript VomitStudent)
	{
		Transform transform = this.FemaleWashSpots[1];
		this.ID = 2;
		while (this.ID < 7)
		{
			if (Vector3.Distance(VomitStudent.transform.position, this.FemaleWashSpots[this.ID].position) < Vector3.Distance(VomitStudent.transform.position, transform.position))
			{
				transform = this.FemaleWashSpots[this.ID];
			}
			this.ID++;
		}
		this.FemaleWashSpot = transform;
	}

	// Token: 0x060020C2 RID: 8386 RVA: 0x0015C5A0 File Offset: 0x0015A9A0
	public void GetNearestFountain(StudentScript Student)
	{
		DrinkingFountainScript drinkingFountainScript = this.DrinkingFountains[1];
		bool flag = false;
		this.ID = 1;
		while (drinkingFountainScript.Occupied)
		{
			drinkingFountainScript = this.DrinkingFountains[1 + this.ID];
			this.ID++;
			if (1 + this.ID == this.DrinkingFountains.Length)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Student.EquipCleaningItems();
			Student.EatingSnack = false;
			Student.Private = false;
			Student.Routine = true;
			Student.StudentManager.UpdateMe(Student.StudentID);
			Student.CurrentDestination = Student.Destinations[Student.Phase];
			Student.Pathfinding.target = Student.Destinations[Student.Phase];
		}
		else
		{
			this.ID = 2;
			while (this.ID < 8)
			{
				if (Vector3.Distance(Student.transform.position, this.DrinkingFountains[this.ID].transform.position) < Vector3.Distance(Student.transform.position, drinkingFountainScript.transform.position) && !this.DrinkingFountains[this.ID].Occupied)
				{
					drinkingFountainScript = this.DrinkingFountains[this.ID];
				}
				this.ID++;
			}
			Student.DrinkingFountain = drinkingFountainScript;
			Student.DrinkingFountain.Occupied = true;
		}
	}

	// Token: 0x060020C3 RID: 8387 RVA: 0x0015C710 File Offset: 0x0015AB10
	public void Save()
	{
		this.ID = 1;
		while (this.ID < 101)
		{
			if (this.Students[this.ID] != null)
			{
				this.Students[this.ID].SaveLoad.SaveData();
			}
			this.ID++;
		}
		int profile = GameGlobals.Profile;
		int @int = PlayerPrefs.GetInt("SaveSlot");
		foreach (DoorScript doorScript in this.Doors)
		{
			if (doorScript != null)
			{
				if (doorScript.Open)
				{
					PlayerPrefs.SetInt(string.Concat(new object[]
					{
						"Profile_",
						profile,
						"_Slot_",
						@int,
						"_Door",
						doorScript.DoorID,
						"_Open"
					}), 1);
				}
				else
				{
					PlayerPrefs.SetInt(string.Concat(new object[]
					{
						"Profile_",
						profile,
						"_Slot_",
						@int,
						"_Door",
						doorScript.DoorID,
						"_Open"
					}), 0);
				}
			}
		}
	}

	// Token: 0x060020C4 RID: 8388 RVA: 0x0015C868 File Offset: 0x0015AC68
	public void Load()
	{
		this.ID = 1;
		while (this.ID < 101)
		{
			if (this.Students[this.ID] != null)
			{
				this.Students[this.ID].SaveLoad.LoadData();
			}
			this.ID++;
		}
		int profile = GameGlobals.Profile;
		int @int = PlayerPrefs.GetInt("SaveSlot");
		this.Yandere.transform.position = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int,
			"_YanderePosX"
		})), PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int,
			"_YanderePosY"
		})), PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int,
			"_YanderePosZ"
		})));
		this.Yandere.transform.eulerAngles = new Vector3(PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int,
			"_YandereRotX"
		})), PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int,
			"_YandereRotY"
		})), PlayerPrefs.GetFloat(string.Concat(new object[]
		{
			"Profile_",
			profile,
			"_Slot_",
			@int,
			"_YandereRotZ"
		})));
		this.Yandere.FixCamera();
		Physics.SyncTransforms();
		foreach (DoorScript doorScript in this.Doors)
		{
			if (doorScript != null)
			{
				if (PlayerPrefs.GetInt(string.Concat(new object[]
				{
					"Profile_",
					profile,
					"_Slot_",
					@int,
					"_Door",
					doorScript.DoorID,
					"_Open"
				})) == 1)
				{
					doorScript.Open = true;
					doorScript.OpenDoor();
				}
				else
				{
					doorScript.Open = false;
				}
			}
		}
	}

	// Token: 0x060020C5 RID: 8389 RVA: 0x0015CB14 File Offset: 0x0015AF14
	public void UpdateBlood()
	{
		if (this.Police.BloodParent.childCount > 0)
		{
			this.ID = 0;
			IEnumerator enumerator = this.Police.BloodParent.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (this.ID < 100)
					{
						this.Blood[this.ID] = transform.gameObject.GetComponent<Collider>();
						this.ID++;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (this.Police.BloodParent.childCount > 0 || this.Police.LimbParent.childCount > 0)
		{
			this.ID = 0;
			IEnumerator enumerator2 = this.Police.LimbParent.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Transform transform2 = (Transform)obj2;
					if (this.ID < 100)
					{
						this.Limbs[this.ID] = transform2.gameObject.GetComponent<Collider>();
						this.ID++;
					}
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x0015CC88 File Offset: 0x0015B088
	public void CanAnyoneSeeYandere()
	{
		this.YandereVisible = false;
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null && studentScript.CanSeeObject(studentScript.Yandere.gameObject, studentScript.Yandere.HeadPosition))
			{
				this.YandereVisible = true;
				break;
			}
		}
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x0015CCF4 File Offset: 0x0015B0F4
	public void SetFaces(float alpha)
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null && studentScript.StudentID > 1)
			{
				studentScript.MyRenderer.materials[0].color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.MyRenderer.materials[1].color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.MyRenderer.materials[2].color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.Cosmetic.LeftEyeRenderer.material.color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.Cosmetic.RightEyeRenderer.material.color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
				studentScript.Cosmetic.HairRenderer.material.color = new Color(1f - alpha, 1f - alpha, 1f - alpha, 1f);
			}
		}
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x0015CE68 File Offset: 0x0015B268
	public void DisableChaseCameras()
	{
		foreach (StudentScript studentScript in this.Students)
		{
			if (studentScript != null)
			{
				studentScript.ChaseCamera.SetActive(false);
			}
		}
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x0015CEAC File Offset: 0x0015B2AC
	public void InitializeReputations()
	{
		StudentGlobals.SetReputationTriangle(1, new Vector3(0f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(2, new Vector3(70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(3, new Vector3(50f, -10f, 30f));
		StudentGlobals.SetReputationTriangle(4, new Vector3(0f, 10f, 0f));
		StudentGlobals.SetReputationTriangle(5, new Vector3(-50f, -30f, 10f));
		StudentGlobals.SetReputationTriangle(6, new Vector3(30f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(7, new Vector3(-10f, -10f, -10f));
		StudentGlobals.SetReputationTriangle(8, new Vector3(0f, 10f, -30f));
		StudentGlobals.SetReputationTriangle(9, new Vector3(0f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(10, new Vector3(100f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(11, new Vector3(100f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(12, new Vector3(100f, 100f, -10f));
		StudentGlobals.SetReputationTriangle(13, new Vector3(-10f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(14, new Vector3(0f, 100f, -10f));
		StudentGlobals.SetReputationTriangle(15, new Vector3(100f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(16, new Vector3(0f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(17, new Vector3(-10f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(18, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(19, new Vector3(10f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(20, new Vector3(100f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(21, new Vector3(50f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(22, new Vector3(30f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(23, new Vector3(50f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(24, new Vector3(30f, 50f, 10f));
		StudentGlobals.SetReputationTriangle(25, new Vector3(70f, 50f, -30f));
		StudentGlobals.SetReputationTriangle(26, new Vector3(-10f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(27, new Vector3(0f, 70f, 0f));
		StudentGlobals.SetReputationTriangle(28, new Vector3(0f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(29, new Vector3(-10f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(30, new Vector3(30f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(31, new Vector3(-70f, 100f, 10f));
		StudentGlobals.SetReputationTriangle(32, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(33, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(34, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(35, new Vector3(-70f, -10f, 10f));
		StudentGlobals.SetReputationTriangle(36, new Vector3(-70f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(37, new Vector3(0f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(38, new Vector3(50f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(39, new Vector3(-50f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(40, new Vector3(70f, -30f, 10f));
		StudentGlobals.SetReputationTriangle(41, new Vector3(0f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(42, new Vector3(-50f, -30f, 30f));
		StudentGlobals.SetReputationTriangle(43, new Vector3(-10f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(44, new Vector3(-10f, 0f, 0f));
		StudentGlobals.SetReputationTriangle(45, new Vector3(0f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(46, new Vector3(100f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(47, new Vector3(10f, 30f, 10f));
		StudentGlobals.SetReputationTriangle(48, new Vector3(30f, 10f, 10f));
		StudentGlobals.SetReputationTriangle(49, new Vector3(30f, 30f, 10f));
		StudentGlobals.SetReputationTriangle(50, new Vector3(30f, 10f, 10f));
		StudentGlobals.SetReputationTriangle(51, new Vector3(10f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(52, new Vector3(30f, 70f, 0f));
		StudentGlobals.SetReputationTriangle(53, new Vector3(50f, 10f, 0f));
		StudentGlobals.SetReputationTriangle(54, new Vector3(50f, 50f, -10f));
		StudentGlobals.SetReputationTriangle(55, new Vector3(30f, 30f, 0f));
		StudentGlobals.SetReputationTriangle(56, new Vector3(70f, 100f, 0f));
		StudentGlobals.SetReputationTriangle(57, new Vector3(70f, -30f, 0f));
		StudentGlobals.SetReputationTriangle(58, new Vector3(70f, -30f, 0f));
		StudentGlobals.SetReputationTriangle(59, new Vector3(50f, -10f, 0f));
		StudentGlobals.SetReputationTriangle(60, new Vector3(-10f, -50f, 0f));
		StudentGlobals.SetReputationTriangle(61, new Vector3(-50f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(62, new Vector3(0f, 70f, 10f));
		StudentGlobals.SetReputationTriangle(63, new Vector3(0f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(64, new Vector3(-10f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(65, new Vector3(-10f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(66, new Vector3(-50f, 100f, 50f));
		StudentGlobals.SetReputationTriangle(67, new Vector3(30f, 70f, 0f));
		StudentGlobals.SetReputationTriangle(68, new Vector3(0f, 0f, 50f));
		StudentGlobals.SetReputationTriangle(69, new Vector3(30f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(70, new Vector3(50f, 30f, 0f));
		StudentGlobals.SetReputationTriangle(71, new Vector3(100f, 100f, -100f));
		StudentGlobals.SetReputationTriangle(72, new Vector3(50f, 30f, 0f));
		StudentGlobals.SetReputationTriangle(73, new Vector3(100f, 100f, -100f));
		StudentGlobals.SetReputationTriangle(74, new Vector3(70f, 50f, -50f));
		StudentGlobals.SetReputationTriangle(75, new Vector3(10f, 50f, 0f));
		StudentGlobals.SetReputationTriangle(76, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(77, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(78, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(79, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(80, new Vector3(-100f, -100f, 100f));
		StudentGlobals.SetReputationTriangle(81, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(82, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(83, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(84, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(85, new Vector3(50f, -10f, 50f));
		StudentGlobals.SetReputationTriangle(86, new Vector3(30f, 100f, 70f));
		StudentGlobals.SetReputationTriangle(87, new Vector3(30f, -10f, 100f));
		StudentGlobals.SetReputationTriangle(88, new Vector3(100f, 30f, 50f));
		StudentGlobals.SetReputationTriangle(89, new Vector3(-10f, 30f, 100f));
		StudentGlobals.SetReputationTriangle(90, new Vector3(10f, 100f, 10f));
		StudentGlobals.SetReputationTriangle(91, new Vector3(0f, 50f, 100f));
		StudentGlobals.SetReputationTriangle(92, new Vector3(0f, 70f, 50f));
		StudentGlobals.SetReputationTriangle(93, new Vector3(0f, 100f, 50f));
		StudentGlobals.SetReputationTriangle(94, new Vector3(0f, 70f, 100f));
		StudentGlobals.SetReputationTriangle(95, new Vector3(0f, 50f, 70f));
		StudentGlobals.SetReputationTriangle(96, new Vector3(0f, 100f, 50f));
		StudentGlobals.SetReputationTriangle(97, new Vector3(50f, 100f, 30f));
		StudentGlobals.SetReputationTriangle(98, new Vector3(0f, 100f, 100f));
		StudentGlobals.SetReputationTriangle(99, new Vector3(-50f, 50f, 100f));
		StudentGlobals.SetReputationTriangle(99, new Vector3(-100f, -100f, 100f));
		this.ID = 2;
		while (this.ID < 101)
		{
			Vector3 reputationTriangle = StudentGlobals.GetReputationTriangle(this.ID);
			reputationTriangle.x *= 0.33333f;
			reputationTriangle.y *= 0.33333f;
			reputationTriangle.z *= 0.33333f;
			StudentGlobals.SetStudentReputation(this.ID, Mathf.RoundToInt(reputationTriangle.x + reputationTriangle.y + reputationTriangle.z));
			this.ID++;
		}
	}

	// Token: 0x04002E2F RID: 11823
	private PortraitChanScript NewPortraitChan;

	// Token: 0x04002E30 RID: 11824
	private GameObject NewStudent;

	// Token: 0x04002E31 RID: 11825
	public StudentScript[] Students;

	// Token: 0x04002E32 RID: 11826
	public SelectiveGrayscale SmartphoneSelectiveGreyscale;

	// Token: 0x04002E33 RID: 11827
	public PickpocketMinigameScript PickpocketMinigame;

	// Token: 0x04002E34 RID: 11828
	public PopulationManagerScript PopulationManager;

	// Token: 0x04002E35 RID: 11829
	public SelectiveGrayscale HandSelectiveGreyscale;

	// Token: 0x04002E36 RID: 11830
	public SkinnedMeshRenderer FemaleShowerCurtain;

	// Token: 0x04002E37 RID: 11831
	public CleaningManagerScript CleaningManager;

	// Token: 0x04002E38 RID: 11832
	public StolenPhoneSpotScript StolenPhoneSpot;

	// Token: 0x04002E39 RID: 11833
	public SelectiveGrayscale SelectiveGreyscale;

	// Token: 0x04002E3A RID: 11834
	public CombatMinigameScript CombatMinigame;

	// Token: 0x04002E3B RID: 11835
	public DatingMinigameScript DatingMinigame;

	// Token: 0x04002E3C RID: 11836
	public TextureManagerScript TextureManager;

	// Token: 0x04002E3D RID: 11837
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04002E3E RID: 11838
	public QualityManagerScript QualityManager;

	// Token: 0x04002E3F RID: 11839
	public ComputerGamesScript ComputerGames;

	// Token: 0x04002E40 RID: 11840
	public EmergencyExitScript EmergencyExit;

	// Token: 0x04002E41 RID: 11841
	public MemorialSceneScript MemorialScene;

	// Token: 0x04002E42 RID: 11842
	public TranqDetectorScript TranqDetector;

	// Token: 0x04002E43 RID: 11843
	public WitnessCameraScript WitnessCamera;

	// Token: 0x04002E44 RID: 11844
	public ConvoManagerScript ConvoManager;

	// Token: 0x04002E45 RID: 11845
	public TallLockerScript CommunalLocker;

	// Token: 0x04002E46 RID: 11846
	public CabinetDoorScript CabinetDoor;

	// Token: 0x04002E47 RID: 11847
	public LightSwitchScript LightSwitch;

	// Token: 0x04002E48 RID: 11848
	public LoveManagerScript LoveManager;

	// Token: 0x04002E49 RID: 11849
	public MiyukiEnemyScript MiyukiEnemy;

	// Token: 0x04002E4A RID: 11850
	public TaskManagerScript TaskManager;

	// Token: 0x04002E4B RID: 11851
	public StudentScript BloodReporter;

	// Token: 0x04002E4C RID: 11852
	public HeadmasterScript Headmaster;

	// Token: 0x04002E4D RID: 11853
	public NoteWindowScript NoteWindow;

	// Token: 0x04002E4E RID: 11854
	public ReputationScript Reputation;

	// Token: 0x04002E4F RID: 11855
	public WeaponScript FragileWeapon;

	// Token: 0x04002E50 RID: 11856
	public AudioSource PracticeVocals;

	// Token: 0x04002E51 RID: 11857
	public AudioSource PracticeMusic;

	// Token: 0x04002E52 RID: 11858
	public ContainerScript Container;

	// Token: 0x04002E53 RID: 11859
	public RedStringScript RedString;

	// Token: 0x04002E54 RID: 11860
	public RingEventScript RingEvent;

	// Token: 0x04002E55 RID: 11861
	public RivalPoseScript RivalPose;

	// Token: 0x04002E56 RID: 11862
	public GazerEyesScript Shinigami;

	// Token: 0x04002E57 RID: 11863
	public HologramScript Holograms;

	// Token: 0x04002E58 RID: 11864
	public RobotArmScript RobotArms;

	// Token: 0x04002E59 RID: 11865
	public PickUpScript Flashlight;

	// Token: 0x04002E5A RID: 11866
	public FountainScript Fountain;

	// Token: 0x04002E5B RID: 11867
	public PoseModeScript PoseMode;

	// Token: 0x04002E5C RID: 11868
	public TrashCanScript TrashCan;

	// Token: 0x04002E5D RID: 11869
	public Collider LockerRoomArea;

	// Token: 0x04002E5E RID: 11870
	public StudentScript Reporter;

	// Token: 0x04002E5F RID: 11871
	public DoorScript GamingDoor;

	// Token: 0x04002E60 RID: 11872
	public GhostScript GhostChan;

	// Token: 0x04002E61 RID: 11873
	public YandereScript Yandere;

	// Token: 0x04002E62 RID: 11874
	public ListScript MeetSpots;

	// Token: 0x04002E63 RID: 11875
	public PoliceScript Police;

	// Token: 0x04002E64 RID: 11876
	public DoorScript ShedDoor;

	// Token: 0x04002E65 RID: 11877
	public UILabel ErrorLabel;

	// Token: 0x04002E66 RID: 11878
	public RestScript Rest;

	// Token: 0x04002E67 RID: 11879
	public TagScript Tag;

	// Token: 0x04002E68 RID: 11880
	public Collider EastBathroomArea;

	// Token: 0x04002E69 RID: 11881
	public Collider WestBathroomArea;

	// Token: 0x04002E6A RID: 11882
	public Collider IncineratorArea;

	// Token: 0x04002E6B RID: 11883
	public Collider HeadmasterArea;

	// Token: 0x04002E6C RID: 11884
	public Collider NEStairs;

	// Token: 0x04002E6D RID: 11885
	public Collider NWStairs;

	// Token: 0x04002E6E RID: 11886
	public Collider SEStairs;

	// Token: 0x04002E6F RID: 11887
	public Collider SWStairs;

	// Token: 0x04002E70 RID: 11888
	public DoorScript AltFemaleVomitDoor;

	// Token: 0x04002E71 RID: 11889
	public DoorScript FemaleVomitDoor;

	// Token: 0x04002E72 RID: 11890
	public CounselorDoorScript[] CounselorDoor;

	// Token: 0x04002E73 RID: 11891
	public ParticleSystem AltFemaleDrownSplashes;

	// Token: 0x04002E74 RID: 11892
	public ParticleSystem FemaleDrownSplashes;

	// Token: 0x04002E75 RID: 11893
	public OfferHelpScript FragileOfferHelp;

	// Token: 0x04002E76 RID: 11894
	public OfferHelpScript OfferHelp;

	// Token: 0x04002E77 RID: 11895
	public Transform AltFemaleVomitSpot;

	// Token: 0x04002E78 RID: 11896
	public ListScript SearchPatrols;

	// Token: 0x04002E79 RID: 11897
	public ListScript CleaningSpots;

	// Token: 0x04002E7A RID: 11898
	public ListScript Patrols;

	// Token: 0x04002E7B RID: 11899
	public ClockScript Clock;

	// Token: 0x04002E7C RID: 11900
	public JsonScript JSON;

	// Token: 0x04002E7D RID: 11901
	public GateScript Gate;

	// Token: 0x04002E7E RID: 11902
	public ListScript EntranceVectors;

	// Token: 0x04002E7F RID: 11903
	public ListScript GoAwaySpots;

	// Token: 0x04002E80 RID: 11904
	public ListScript HidingSpots;

	// Token: 0x04002E81 RID: 11905
	public ListScript LunchSpots;

	// Token: 0x04002E82 RID: 11906
	public ListScript Hangouts;

	// Token: 0x04002E83 RID: 11907
	public ListScript Lockers;

	// Token: 0x04002E84 RID: 11908
	public ListScript Podiums;

	// Token: 0x04002E85 RID: 11909
	public ListScript Clubs;

	// Token: 0x04002E86 RID: 11910
	public ChangingBoothScript[] ChangingBooths;

	// Token: 0x04002E87 RID: 11911
	public GradingPaperScript[] FacultyDesks;

	// Token: 0x04002E88 RID: 11912
	public GameObject[] ShrineCollectibles;

	// Token: 0x04002E89 RID: 11913
	public StudentScript[] WitnessList;

	// Token: 0x04002E8A RID: 11914
	public StudentScript[] Teachers;

	// Token: 0x04002E8B RID: 11915
	public GameObject[] Graffiti;

	// Token: 0x04002E8C RID: 11916
	public GameObject[] Canvas;

	// Token: 0x04002E8D RID: 11917
	public ListScript[] Seats;

	// Token: 0x04002E8E RID: 11918
	public Collider[] Blood;

	// Token: 0x04002E8F RID: 11919
	public Collider[] Limbs;

	// Token: 0x04002E90 RID: 11920
	public Transform[] TeacherGuardLocation;

	// Token: 0x04002E91 RID: 11921
	public Transform[] CorpseGuardLocation;

	// Token: 0x04002E92 RID: 11922
	public Transform[] BloodGuardLocation;

	// Token: 0x04002E93 RID: 11923
	public Transform[] SleuthDestinations;

	// Token: 0x04002E94 RID: 11924
	public Transform[] GardeningPatrols;

	// Token: 0x04002E95 RID: 11925
	public Transform[] MartialArtsSpots;

	// Token: 0x04002E96 RID: 11926
	public Transform[] LockerPositions;

	// Token: 0x04002E97 RID: 11927
	public Transform[] BackstageSpots;

	// Token: 0x04002E98 RID: 11928
	public Transform[] SpawnPositions;

	// Token: 0x04002E99 RID: 11929
	public Transform[] GraffitiSpots;

	// Token: 0x04002E9A RID: 11930
	public Transform[] PracticeSpots;

	// Token: 0x04002E9B RID: 11931
	public Transform[] SunbatheSpots;

	// Token: 0x04002E9C RID: 11932
	public Transform[] MeetingSpots;

	// Token: 0x04002E9D RID: 11933
	public Transform[] PinDownSpots;

	// Token: 0x04002E9E RID: 11934
	public Transform[] ShockedSpots;

	// Token: 0x04002E9F RID: 11935
	public Transform[] FridaySpots;

	// Token: 0x04002EA0 RID: 11936
	public Transform[] MiyukiSpots;

	// Token: 0x04002EA1 RID: 11937
	public Transform[] SocialSeats;

	// Token: 0x04002EA2 RID: 11938
	public Transform[] SocialSpots;

	// Token: 0x04002EA3 RID: 11939
	public Transform[] SupplySpots;

	// Token: 0x04002EA4 RID: 11940
	public Transform[] BullySpots;

	// Token: 0x04002EA5 RID: 11941
	public Transform[] DramaSpots;

	// Token: 0x04002EA6 RID: 11942
	public Transform[] MournSpots;

	// Token: 0x04002EA7 RID: 11943
	public Transform[] ClubZones;

	// Token: 0x04002EA8 RID: 11944
	public Transform[] SulkSpots;

	// Token: 0x04002EA9 RID: 11945
	public Transform[] FleeSpots;

	// Token: 0x04002EAA RID: 11946
	public Transform[] Uniforms;

	// Token: 0x04002EAB RID: 11947
	public Transform[] Plates;

	// Token: 0x04002EAC RID: 11948
	public Transform[] FemaleVomitSpots;

	// Token: 0x04002EAD RID: 11949
	public Transform[] MaleVomitSpots;

	// Token: 0x04002EAE RID: 11950
	public Transform[] FemaleWashSpots;

	// Token: 0x04002EAF RID: 11951
	public Transform[] MaleWashSpots;

	// Token: 0x04002EB0 RID: 11952
	public DoorScript[] FemaleToiletDoors;

	// Token: 0x04002EB1 RID: 11953
	public DoorScript[] MaleToiletDoors;

	// Token: 0x04002EB2 RID: 11954
	public DrinkingFountainScript[] DrinkingFountains;

	// Token: 0x04002EB3 RID: 11955
	public Renderer[] FridayPaintings;

	// Token: 0x04002EB4 RID: 11956
	public bool[] SeatsTaken11;

	// Token: 0x04002EB5 RID: 11957
	public bool[] SeatsTaken12;

	// Token: 0x04002EB6 RID: 11958
	public bool[] SeatsTaken21;

	// Token: 0x04002EB7 RID: 11959
	public bool[] SeatsTaken22;

	// Token: 0x04002EB8 RID: 11960
	public bool[] SeatsTaken31;

	// Token: 0x04002EB9 RID: 11961
	public bool[] SeatsTaken32;

	// Token: 0x04002EBA RID: 11962
	public bool[] NoBully;

	// Token: 0x04002EBB RID: 11963
	public Quaternion[] OriginalClubRotations;

	// Token: 0x04002EBC RID: 11964
	public Vector3[] OriginalClubPositions;

	// Token: 0x04002EBD RID: 11965
	public Collider RivalDeskCollider;

	// Token: 0x04002EBE RID: 11966
	public Transform FollowerLookAtTarget;

	// Token: 0x04002EBF RID: 11967
	public Transform SuitorConfessionSpot;

	// Token: 0x04002EC0 RID: 11968
	public Transform RivalConfessionSpot;

	// Token: 0x04002EC1 RID: 11969
	public Transform OriginalLyricsSpot;

	// Token: 0x04002EC2 RID: 11970
	public Transform FragileSlaveSpot;

	// Token: 0x04002EC3 RID: 11971
	public Transform FemaleCoupleSpot;

	// Token: 0x04002EC4 RID: 11972
	public Transform YandereStripSpot;

	// Token: 0x04002EC5 RID: 11973
	public Transform FemaleBatheSpot;

	// Token: 0x04002EC6 RID: 11974
	public Transform FemaleStalkSpot;

	// Token: 0x04002EC7 RID: 11975
	public Transform FemaleStripSpot;

	// Token: 0x04002EC8 RID: 11976
	public Transform FemaleVomitSpot;

	// Token: 0x04002EC9 RID: 11977
	public Transform MedicineCabinet;

	// Token: 0x04002ECA RID: 11978
	public Transform ConfessionSpot;

	// Token: 0x04002ECB RID: 11979
	public Transform CorpseLocation;

	// Token: 0x04002ECC RID: 11980
	public Transform FemaleRestSpot;

	// Token: 0x04002ECD RID: 11981
	public Transform FemaleWashSpot;

	// Token: 0x04002ECE RID: 11982
	public Transform MaleCoupleSpot;

	// Token: 0x04002ECF RID: 11983
	public Transform AirGuitarSpot;

	// Token: 0x04002ED0 RID: 11984
	public Transform BloodLocation;

	// Token: 0x04002ED1 RID: 11985
	public Transform FastBatheSpot;

	// Token: 0x04002ED2 RID: 11986
	public Transform InfirmarySeat;

	// Token: 0x04002ED3 RID: 11987
	public Transform MaleBatheSpot;

	// Token: 0x04002ED4 RID: 11988
	public Transform MaleStalkSpot;

	// Token: 0x04002ED5 RID: 11989
	public Transform MaleStripSpot;

	// Token: 0x04002ED6 RID: 11990
	public Transform MaleVomitSpot;

	// Token: 0x04002ED7 RID: 11991
	public Transform SacrificeSpot;

	// Token: 0x04002ED8 RID: 11992
	public Transform WeaponBoxSpot;

	// Token: 0x04002ED9 RID: 11993
	public Transform FountainSpot;

	// Token: 0x04002EDA RID: 11994
	public Transform MaleWashSpot;

	// Token: 0x04002EDB RID: 11995
	public Transform SenpaiLocker;

	// Token: 0x04002EDC RID: 11996
	public Transform SuitorLocker;

	// Token: 0x04002EDD RID: 11997
	public Transform MaleRestSpot;

	// Token: 0x04002EDE RID: 11998
	public Transform RomanceSpot;

	// Token: 0x04002EDF RID: 11999
	public Transform BrokenSpot;

	// Token: 0x04002EE0 RID: 12000
	public Transform BullyGroup;

	// Token: 0x04002EE1 RID: 12001
	public Transform EdgeOfGrid;

	// Token: 0x04002EE2 RID: 12002
	public Transform GoAwaySpot;

	// Token: 0x04002EE3 RID: 12003
	public Transform LyricsSpot;

	// Token: 0x04002EE4 RID: 12004
	public Transform MainCamera;

	// Token: 0x04002EE5 RID: 12005
	public Transform SuitorSpot;

	// Token: 0x04002EE6 RID: 12006
	public Transform ToolTarget;

	// Token: 0x04002EE7 RID: 12007
	public Transform MiyukiCat;

	// Token: 0x04002EE8 RID: 12008
	public Transform ShameSpot;

	// Token: 0x04002EE9 RID: 12009
	public Transform SlaveSpot;

	// Token: 0x04002EEA RID: 12010
	public Transform Papers;

	// Token: 0x04002EEB RID: 12011
	public Transform Exit;

	// Token: 0x04002EEC RID: 12012
	public GameObject LovestruckCamera;

	// Token: 0x04002EED RID: 12013
	public GameObject DelinquentRadio;

	// Token: 0x04002EEE RID: 12014
	public GameObject GardenBlockade;

	// Token: 0x04002EEF RID: 12015
	public GameObject PortraitChan;

	// Token: 0x04002EF0 RID: 12016
	public GameObject RandomPatrol;

	// Token: 0x04002EF1 RID: 12017
	public GameObject ChaseCamera;

	// Token: 0x04002EF2 RID: 12018
	public GameObject EmptyObject;

	// Token: 0x04002EF3 RID: 12019
	public GameObject PortraitKun;

	// Token: 0x04002EF4 RID: 12020
	public GameObject StudentChan;

	// Token: 0x04002EF5 RID: 12021
	public GameObject StudentKun;

	// Token: 0x04002EF6 RID: 12022
	public GameObject RivalChan;

	// Token: 0x04002EF7 RID: 12023
	public GameObject Canvases;

	// Token: 0x04002EF8 RID: 12024
	public GameObject Medicine;

	// Token: 0x04002EF9 RID: 12025
	public GameObject DrumSet;

	// Token: 0x04002EFA RID: 12026
	public GameObject Flowers;

	// Token: 0x04002EFB RID: 12027
	public GameObject Portal;

	// Token: 0x04002EFC RID: 12028
	public GameObject Gift;

	// Token: 0x04002EFD RID: 12029
	public float[] SpawnTimes;

	// Token: 0x04002EFE RID: 12030
	public int LowDetailThreshold;

	// Token: 0x04002EFF RID: 12031
	public int FarAnimThreshold;

	// Token: 0x04002F00 RID: 12032
	public int MartialArtsPhase;

	// Token: 0x04002F01 RID: 12033
	public int OriginalUniforms = 2;

	// Token: 0x04002F02 RID: 12034
	public int StudentsSpawned;

	// Token: 0x04002F03 RID: 12035
	public int SedatedStudents;

	// Token: 0x04002F04 RID: 12036
	public int StudentsTotal = 13;

	// Token: 0x04002F05 RID: 12037
	public int TeachersTotal = 6;

	// Token: 0x04002F06 RID: 12038
	public int NewUniforms;

	// Token: 0x04002F07 RID: 12039
	public int NPCsSpawned;

	// Token: 0x04002F08 RID: 12040
	public int SleuthPhase = 1;

	// Token: 0x04002F09 RID: 12041
	public int DramaPhase = 1;

	// Token: 0x04002F0A RID: 12042
	public int NPCsTotal;

	// Token: 0x04002F0B RID: 12043
	public int Witnesses;

	// Token: 0x04002F0C RID: 12044
	public int PinPhase;

	// Token: 0x04002F0D RID: 12045
	public int Bullies;

	// Token: 0x04002F0E RID: 12046
	public int Speaker = 21;

	// Token: 0x04002F0F RID: 12047
	public int Frame;

	// Token: 0x04002F10 RID: 12048
	public int GymTeacherID = 100;

	// Token: 0x04002F11 RID: 12049
	public int ObstacleID = 6;

	// Token: 0x04002F12 RID: 12050
	public int CurrentID;

	// Token: 0x04002F13 RID: 12051
	public int SuitorID = 13;

	// Token: 0x04002F14 RID: 12052
	public int VictimID;

	// Token: 0x04002F15 RID: 12053
	public int NurseID = 93;

	// Token: 0x04002F16 RID: 12054
	public int RivalID = 7;

	// Token: 0x04002F17 RID: 12055
	public int SpawnID;

	// Token: 0x04002F18 RID: 12056
	public int ID;

	// Token: 0x04002F19 RID: 12057
	public bool ReactedToGameLeader;

	// Token: 0x04002F1A RID: 12058
	public bool MurderTakingPlace;

	// Token: 0x04002F1B RID: 12059
	public bool ControllerShrink;

	// Token: 0x04002F1C RID: 12060
	public bool DisableFarAnims;

	// Token: 0x04002F1D RID: 12061
	public bool RivalEliminated;

	// Token: 0x04002F1E RID: 12062
	public bool TakingPortraits;

	// Token: 0x04002F1F RID: 12063
	public bool TeachersSpawned;

	// Token: 0x04002F20 RID: 12064
	public bool MetalDetectors;

	// Token: 0x04002F21 RID: 12065
	public bool YandereVisible;

	// Token: 0x04002F22 RID: 12066
	public bool NoClubMeeting;

	// Token: 0x04002F23 RID: 12067
	public bool UpdatedBlood;

	// Token: 0x04002F24 RID: 12068
	public bool YandereDying;

	// Token: 0x04002F25 RID: 12069
	public bool FirstUpdate;

	// Token: 0x04002F26 RID: 12070
	public bool MissionMode;

	// Token: 0x04002F27 RID: 12071
	public bool OpenCurtain;

	// Token: 0x04002F28 RID: 12072
	public bool PinningDown;

	// Token: 0x04002F29 RID: 12073
	public bool RoofFenceUp;

	// Token: 0x04002F2A RID: 12074
	public bool YandereLate;

	// Token: 0x04002F2B RID: 12075
	public bool ForceSpawn;

	// Token: 0x04002F2C RID: 12076
	public bool NoGravity;

	// Token: 0x04002F2D RID: 12077
	public bool Randomize;

	// Token: 0x04002F2E RID: 12078
	public bool LoveSick;

	// Token: 0x04002F2F RID: 12079
	public bool NoSpeech;

	// Token: 0x04002F30 RID: 12080
	public bool Meeting;

	// Token: 0x04002F31 RID: 12081
	public bool Censor;

	// Token: 0x04002F32 RID: 12082
	public bool Spooky;

	// Token: 0x04002F33 RID: 12083
	public bool Bully;

	// Token: 0x04002F34 RID: 12084
	public bool Gaze;

	// Token: 0x04002F35 RID: 12085
	public bool Pose;

	// Token: 0x04002F36 RID: 12086
	public bool Sans;

	// Token: 0x04002F37 RID: 12087
	public bool Stop;

	// Token: 0x04002F38 RID: 12088
	public bool Egg;

	// Token: 0x04002F39 RID: 12089
	public bool Six;

	// Token: 0x04002F3A RID: 12090
	public bool AoT;

	// Token: 0x04002F3B RID: 12091
	public bool DK;

	// Token: 0x04002F3C RID: 12092
	public float Atmosphere;

	// Token: 0x04002F3D RID: 12093
	public float OpenValue = 100f;

	// Token: 0x04002F3E RID: 12094
	public float YandereHeight = 999f;

	// Token: 0x04002F3F RID: 12095
	public float MeetingTimer;

	// Token: 0x04002F40 RID: 12096
	public float PinDownTimer;

	// Token: 0x04002F41 RID: 12097
	public float ChangeTimer;

	// Token: 0x04002F42 RID: 12098
	public float SleuthTimer;

	// Token: 0x04002F43 RID: 12099
	public float DramaTimer;

	// Token: 0x04002F44 RID: 12100
	public float LowestRep;

	// Token: 0x04002F45 RID: 12101
	public float PinTimer;

	// Token: 0x04002F46 RID: 12102
	public float Timer;

	// Token: 0x04002F47 RID: 12103
	public string[] ColorNames;

	// Token: 0x04002F48 RID: 12104
	public string[] MaleNames;

	// Token: 0x04002F49 RID: 12105
	public string[] FirstNames;

	// Token: 0x04002F4A RID: 12106
	public string[] LastNames;

	// Token: 0x04002F4B RID: 12107
	public AudioClip YanderePinDown;

	// Token: 0x04002F4C RID: 12108
	public AudioClip PinDownSFX;

	// Token: 0x04002F4D RID: 12109
	[SerializeField]
	private int ProblemID = -1;

	// Token: 0x04002F4E RID: 12110
	public GameObject Cardigan;

	// Token: 0x04002F4F RID: 12111
	public SkinnedMeshRenderer CardiganRenderer;

	// Token: 0x04002F50 RID: 12112
	public Mesh OpenChipBag;

	// Token: 0x04002F51 RID: 12113
	public Renderer[] Trees;

	// Token: 0x04002F52 RID: 12114
	public bool SeatOccupied;

	// Token: 0x04002F53 RID: 12115
	public int Class = 1;

	// Token: 0x04002F54 RID: 12116
	public int Thins;

	// Token: 0x04002F55 RID: 12117
	public int Seriouses;

	// Token: 0x04002F56 RID: 12118
	public int Rounds;

	// Token: 0x04002F57 RID: 12119
	public int Sads;

	// Token: 0x04002F58 RID: 12120
	public int Means;

	// Token: 0x04002F59 RID: 12121
	public int Smugs;

	// Token: 0x04002F5A RID: 12122
	public int Gentles;

	// Token: 0x04002F5B RID: 12123
	public int Rival1s;

	// Token: 0x04002F5C RID: 12124
	public DoorScript[] Doors;

	// Token: 0x04002F5D RID: 12125
	public int DoorID;
}
