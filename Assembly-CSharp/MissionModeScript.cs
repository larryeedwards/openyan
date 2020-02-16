using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200017E RID: 382
public class MissionModeScript : MonoBehaviour
{
	// Token: 0x06000C04 RID: 3076 RVA: 0x00062CEC File Offset: 0x000610EC
	private void Start()
	{
		if (!SchoolGlobals.HighSecurity)
		{
			this.SecurityCameraGroup.SetActive(false);
			this.MetalDetectorGroup.SetActive(false);
		}
		this.NewMissionWindow.gameObject.SetActive(false);
		this.MissionModeHUD.SetActive(false);
		this.SpottedWindow.SetActive(false);
		this.ExitPortal.SetActive(false);
		this.Safe.SetActive(false);
		this.MainCamera = Camera.main;
		if (GameGlobals.LoveSick)
		{
			this.MurderKit.SetActive(false);
			this.Yandere.HeartRate.MediumColour = new Color(1f, 1f, 1f, 1f);
			this.Yandere.HeartRate.NormalColour = new Color(1f, 1f, 1f, 1f);
			this.Clock.PeriodLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Clock.TimeLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Clock.DayLabel.enabled = false;
			this.MoneyLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Reputation.PendingRepMarker.GetComponent<UISprite>().color = new Color(1f, 0f, 0f, 1f);
			this.Reputation.CurrentRepMarker.gameObject.SetActive(false);
			this.Reputation.PendingRepLabel.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationFace1.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationFace2.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationBG.color = new Color(1f, 0f, 0f, 1f);
			this.ReputationLabel.color = new Color(1f, 0f, 0f, 1f);
			this.Watermark.color = new Color(1f, 0f, 0f, 1f);
			this.EventSubtitleLabel.color = new Color(1f, 0f, 0f, 1f);
			this.SubtitleLabel.color = new Color(1f, 0f, 0f, 1f);
			this.CautionSign.color = new Color(1f, 0f, 0f, 1f);
			this.FPS.color = new Color(1f, 0f, 0f, 1f);
			this.SanityLabel.color = new Color(1f, 0f, 0f, 1f);
			this.ID = 1;
			while (this.ID < this.PoliceLabel.Length)
			{
				this.PoliceLabel[this.ID].color = new Color(1f, 0f, 0f, 1f);
				this.ID++;
			}
			this.ID = 1;
			while (this.ID < this.PoliceIcon.Length)
			{
				this.PoliceIcon[this.ID].color = new Color(1f, 0f, 0f, 1f);
				this.ID++;
			}
		}
		if (MissionModeGlobals.MissionMode)
		{
			this.Headmaster.SetActive(false);
			this.Yandere.HeartRate.MediumColour = new Color(1f, 0.5f, 0.5f, 1f);
			this.Yandere.HeartRate.NormalColour = new Color(1f, 1f, 1f, 1f);
			this.Clock.PeriodLabel.color = new Color(1f, 1f, 1f, 1f);
			this.Clock.TimeLabel.color = new Color(1f, 1f, 1f, 1f);
			this.Clock.DayLabel.enabled = false;
			this.MoneyLabel.color = new Color(1f, 1f, 1f, 1f);
			this.MoneyLabel.fontStyle = FontStyle.Bold;
			this.MoneyLabel.trueTypeFont = this.Arial;
			this.Reputation.PendingRepMarker.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
			this.Reputation.CurrentRepMarker.gameObject.SetActive(false);
			this.Reputation.PendingRepLabel.color = new Color(1f, 1f, 1f, 1f);
			this.ReputationLabel.fontStyle = FontStyle.Bold;
			this.ReputationLabel.trueTypeFont = this.Arial;
			this.ReputationLabel.color = new Color(1f, 1f, 1f, 1f);
			this.ReputationLabel.text = "AWARENESS";
			this.ReputationIcons[0].SetActive(true);
			this.ReputationIcons[1].SetActive(false);
			this.ReputationIcons[2].SetActive(false);
			this.ReputationIcons[3].SetActive(false);
			this.ReputationIcons[4].SetActive(false);
			this.ReputationIcons[5].SetActive(false);
			this.Clock.TimeLabel.fontStyle = FontStyle.Bold;
			this.Clock.TimeLabel.trueTypeFont = this.Arial;
			this.Clock.PeriodLabel.fontStyle = FontStyle.Bold;
			this.Clock.PeriodLabel.trueTypeFont = this.Arial;
			this.Watermark.fontStyle = FontStyle.Bold;
			this.Watermark.color = new Color(1f, 1f, 1f, 1f);
			this.Watermark.trueTypeFont = this.Arial;
			this.SubtitleLabel.color = new Color(1f, 1f, 1f, 1f);
			this.CautionSign.color = new Color(1f, 1f, 1f, 1f);
			this.FPS.color = new Color(1f, 1f, 1f, 1f);
			this.SanityLabel.color = new Color(1f, 1f, 1f, 1f);
			this.ColorCorrections = this.MainCamera.GetComponents<ColorCorrectionCurves>();
			this.StudentManager.MissionMode = true;
			this.NemesisDifficulty = MissionModeGlobals.NemesisDifficulty;
			this.Difficulty = MissionModeGlobals.MissionDifficulty;
			this.TargetID = MissionModeGlobals.MissionTarget;
			ClassGlobals.BiologyGrade = 1;
			ClassGlobals.ChemistryGrade = 1;
			ClassGlobals.LanguageGrade = 1;
			ClassGlobals.PhysicalGrade = 1;
			ClassGlobals.PsychologyGrade = 1;
			this.Yandere.StudentManager.TutorialWindow.gameObject.SetActive(false);
			OptionGlobals.TutorialsOff = true;
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f - (float)this.Difficulty * 0.1f;
			PlayerGlobals.Money = 20f;
			this.StudentManager.Atmosphere = 1f - (float)this.Difficulty * 0.1f;
			this.StudentManager.SetAtmosphere();
			this.ID = 1;
			while (this.ID < this.PoliceLabel.Length)
			{
				this.PoliceLabel[this.ID].fontStyle = FontStyle.Bold;
				this.PoliceLabel[this.ID].color = new Color(1f, 1f, 1f, 1f);
				this.PoliceLabel[this.ID].trueTypeFont = this.Arial;
				this.ID++;
			}
			this.ID = 1;
			while (this.ID < this.PoliceIcon.Length)
			{
				this.PoliceIcon[this.ID].color = new Color(1f, 1f, 1f, 1f);
				this.ID++;
			}
			if (this.Difficulty > 1)
			{
				this.ID = 2;
				while (this.ID < this.Difficulty + 1)
				{
					int missionCondition = MissionModeGlobals.GetMissionCondition(this.ID);
					if (missionCondition == 1)
					{
						this.RequiredWeaponID = MissionModeGlobals.MissionRequiredWeapon;
					}
					else if (missionCondition == 2)
					{
						this.RequiredClothingID = MissionModeGlobals.MissionRequiredClothing;
					}
					else if (missionCondition == 3)
					{
						this.RequiredDisposalID = MissionModeGlobals.MissionRequiredDisposal;
					}
					else if (missionCondition == 4)
					{
						this.NoCollateral = true;
					}
					else if (missionCondition == 5)
					{
						this.NoWitnesses = true;
					}
					else if (missionCondition == 6)
					{
						this.NoCorpses = true;
					}
					else if (missionCondition == 7)
					{
						this.NoWeapon = true;
					}
					else if (missionCondition == 8)
					{
						this.NoBlood = true;
					}
					else if (missionCondition == 9)
					{
						this.TimeLimit = true;
					}
					else if (missionCondition == 10)
					{
						this.NoSuspicion = true;
					}
					else if (missionCondition == 11)
					{
						this.SecurityCameras = true;
					}
					else if (missionCondition == 12)
					{
						this.MetalDetectors = true;
					}
					else if (missionCondition == 13)
					{
						this.NoSpeech = true;
					}
					else if (missionCondition == 14)
					{
						this.StealDocuments = true;
					}
					this.Conditions[this.ID] = missionCondition;
					this.ID++;
				}
			}
			if (!this.StealDocuments)
			{
				this.DocumentsStolen = true;
			}
			else
			{
				this.Safe.SetActive(true);
			}
			if (this.SecurityCameras)
			{
				this.SecurityCameraGroup.SetActive(true);
			}
			if (this.MetalDetectors)
			{
				this.MetalDetectorGroup.SetActive(true);
			}
			if (this.TimeLimit)
			{
				this.TimeLabel.gameObject.SetActive(true);
			}
			if (this.NoSpeech)
			{
				this.StudentManager.NoSpeech = true;
			}
			if (this.RequiredDisposalID == 0)
			{
				this.CorpseDisposed = true;
			}
			if (this.NemesisDifficulty > 0)
			{
				this.Nemesis.SetActive(true);
			}
			if (!this.NoWeapon)
			{
				this.WeaponDisposed = true;
			}
			if (!this.NoBlood)
			{
				this.BloodCleaned = true;
			}
			this.Jukebox.Egg = true;
			this.Jukebox.KillVolume();
			this.Jukebox.MissionMode.enabled = true;
			this.Jukebox.MissionMode.volume = 0f;
			this.Yandere.FixCamera();
			this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, 6.51505f, -76.9222f);
			this.MainCamera.transform.eulerAngles = new Vector3(15f, this.MainCamera.transform.eulerAngles.y, this.MainCamera.transform.eulerAngles.z);
			this.Yandere.RPGCamera.enabled = false;
			this.Yandere.SanityBased = true;
			this.Yandere.CanMove = false;
			this.VoidGoddess.GetComponent<VoidGoddessScript>().Window.gameObject.SetActive(false);
			this.HeartbeatCamera.SetActive(false);
			this.TranqDetector.SetActive(false);
			this.VoidGoddess.SetActive(false);
			this.MurderKit.SetActive(false);
			this.TargetHeight = 1.51505f;
			this.Yandere.HUD.alpha = 0f;
			this.MusicIcon.color = new Color(this.MusicIcon.color.r, this.MusicIcon.color.g, this.MusicIcon.color.b, 1f);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
			this.MissionModeMenu.UpdateGraphics();
			this.MissionModeMenu.gameObject.SetActive(true);
			if (MissionModeGlobals.MultiMission)
			{
				this.NewMissionWindow.gameObject.SetActive(true);
				this.MissionModeMenu.gameObject.SetActive(false);
				this.NewMissionWindow.FillOutInfo();
				this.NewMissionWindow.HideButtons();
				this.MultiMission = true;
				for (int i = 1; i < 11; i++)
				{
					this.Target[i] = PlayerPrefs.GetInt("MissionModeTarget" + i);
					this.Method[i] = PlayerPrefs.GetInt("MissionModeMethod" + i);
				}
			}
			this.Enabled = true;
		}
		else
		{
			this.MissionModeMenu.gameObject.SetActive(false);
			this.TimeLabel.gameObject.SetActive(false);
			base.enabled = false;
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x00063AEC File Offset: 0x00061EEC
	private void Update()
	{
		if (this.Phase == 1)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime / 3f));
			if (this.Darkness.color.a == 0f)
			{
				this.Speed += Time.deltaTime / 3f;
				this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, Mathf.Lerp(this.MainCamera.transform.position.y, this.TargetHeight, Time.deltaTime * this.Speed), this.MainCamera.transform.position.z);
				if (this.MainCamera.transform.position.y < this.TargetHeight + 0.1f)
				{
					this.Yandere.HUD.alpha = Mathf.MoveTowards(this.Yandere.HUD.alpha, 1f, Time.deltaTime / 3f);
					if (this.Yandere.HUD.alpha == 1f)
					{
						Debug.Log("Incrementing phase.");
						this.Yandere.RPGCamera.enabled = true;
						this.HeartbeatCamera.SetActive(true);
						this.Yandere.CanMove = true;
						this.Phase++;
					}
				}
			}
			if (Input.GetButtonDown("A"))
			{
				this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, this.TargetHeight, this.MainCamera.transform.position.z);
				this.Yandere.RPGCamera.enabled = true;
				this.HeartbeatCamera.SetActive(true);
				this.Yandere.CanMove = true;
				this.Yandere.HUD.alpha = 1f;
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
				this.Phase++;
			}
		}
		else if (this.Phase == 2)
		{
			if (this.MultiMission)
			{
				for (int i = 1; i < this.Target.Length; i++)
				{
					if (this.Target[i] == 0)
					{
						this.Checking[i] = false;
					}
					else if (this.Checking[i])
					{
						if (this.StudentManager.Students[this.Target[i]].transform.position.y < -11f)
						{
							this.GameOverID = 1;
							this.GameOver();
							this.Phase = 4;
						}
						else if (!this.StudentManager.Students[this.Target[i]].Alive)
						{
							if (this.Method[i] == 0)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Weapon)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 1)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Drowning)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 2)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Poison)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 3)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Electrocution)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 4)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Burning)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 5)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Falling)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
							else if (this.Method[i] == 6)
							{
								if (this.StudentManager.Students[this.Target[i]].DeathType == DeathType.Weight)
								{
									this.NewMissionWindow.DeathSkulls[i].SetActive(true);
									this.Checking[i] = false;
									this.CheckForCompletion();
								}
								else
								{
									this.GameOverID = 18;
									this.GameOver();
									this.Phase = 4;
								}
							}
						}
					}
				}
			}
			if (!this.TargetDead && this.StudentManager.Students[this.TargetID] != null)
			{
				if (!this.StudentManager.Students[this.TargetID].Alive)
				{
					if (this.Yandere.Equipped > 0)
					{
						if (this.StudentManager.Students[this.TargetID].DeathType == DeathType.Weapon)
						{
							this.MurderWeaponID = this.Yandere.EquippedWeapon.WeaponID;
						}
						else
						{
							this.WeaponDisposed = true;
						}
					}
					else
					{
						this.WeaponDisposed = true;
					}
					this.TargetDead = true;
				}
				if (this.StudentManager.Students[this.TargetID].transform.position.y < -11f)
				{
					this.GameOverID = 1;
					this.GameOver();
					this.Phase = 4;
				}
			}
			if (this.RequiredWeaponID > 0 && this.StudentManager.Students[this.TargetID] != null && !this.StudentManager.Students[this.TargetID].Alive && this.StudentManager.Students[this.TargetID].DeathCause != this.RequiredWeaponID)
			{
				this.Chastise = true;
				this.GameOverID = 2;
				this.GameOver();
				this.Phase = 4;
			}
			if (!this.CorrectClothingConfirmed && this.RequiredClothingID > 0 && this.StudentManager.Students[this.TargetID] != null && !this.StudentManager.Students[this.TargetID].Alive)
			{
				if (this.Yandere.Schoolwear != this.RequiredClothingID)
				{
					this.Chastise = true;
					this.GameOverID = 3;
					this.GameOver();
					this.Phase = 4;
				}
				else
				{
					this.CorrectClothingConfirmed = true;
				}
			}
			if (this.RequiredDisposalID > 0 && this.DisposalMethod == 0 && this.TargetDead)
			{
				this.ID = 1;
				while (this.ID < this.Incinerator.Victims + 1)
				{
					if (this.Incinerator.VictimList[this.ID] == this.TargetID && this.Incinerator.Smoke.isPlaying)
					{
						this.DisposalMethod = 1;
					}
					this.ID++;
				}
				int num = 0;
				this.ID = 1;
				while (this.ID < this.Incinerator.Limbs + 1)
				{
					if (this.Incinerator.LimbList[this.ID] == this.TargetID)
					{
						num++;
					}
					if (num == 6 && this.Incinerator.Smoke.isPlaying)
					{
						this.DisposalMethod = 1;
					}
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.WoodChipper.Victims + 1)
				{
					if (this.WoodChipper.VictimList[this.ID] == this.TargetID && this.WoodChipper.Shredding)
					{
						this.DisposalMethod = 2;
					}
					this.ID++;
				}
				this.ID = 1;
				while (this.ID < this.GardenHoles.Length)
				{
					if (this.GardenHoles[this.ID].VictimID == this.TargetID && !this.GardenHoles[this.ID].enabled)
					{
						this.DisposalMethod = 3;
					}
					this.ID++;
				}
				if (this.DisposalMethod > 0)
				{
					if (this.DisposalMethod != this.RequiredDisposalID)
					{
						this.Chastise = true;
						this.GameOverID = 4;
						this.GameOver();
						this.Phase = 4;
					}
					else
					{
						this.CorpseDisposed = true;
					}
				}
			}
			if (this.NoCollateral)
			{
				if (this.Police.Corpses == 1)
				{
					if (this.Police.CorpseList[0].StudentID != this.TargetID)
					{
						this.Chastise = true;
						this.GameOverID = 5;
						this.GameOver();
						this.Phase = 4;
					}
				}
				else if (this.Police.Corpses > 1)
				{
					this.GameOverID = 5;
					this.GameOver();
					this.Phase = 4;
				}
			}
			if (this.NoWitnesses)
			{
				this.ID = 1;
				while (this.ID < this.StudentManager.Students.Length)
				{
					if (this.StudentManager.Students[this.ID] != null && this.StudentManager.Students[this.ID].WitnessedMurder)
					{
						this.SpottedLabel.text = this.StudentManager.Students[this.ID].Name;
						this.SpottedWindow.SetActive(true);
						this.Chastise = true;
						this.GameOverID = 6;
						if (this.Yandere.Mopping)
						{
							this.GameOverID = 20;
						}
						this.GameOver();
						this.Phase = 4;
					}
					this.ID++;
				}
			}
			if (this.NoCorpses)
			{
				this.ID = 1;
				while (this.ID < this.StudentManager.Students.Length)
				{
					if (this.StudentManager.Students[this.ID] != null && (this.StudentManager.Students[this.ID].WitnessedCorpse || this.StudentManager.Students[this.ID].WitnessedMurder))
					{
						this.SpottedLabel.text = this.StudentManager.Students[this.ID].Name;
						this.SpottedWindow.SetActive(true);
						this.Chastise = true;
						if (this.Yandere.DelinquentFighting)
						{
							this.GameOverID = 19;
						}
						else
						{
							this.GameOverID = 7;
						}
						this.GameOver();
						this.Phase = 4;
					}
					this.ID++;
				}
			}
			if (this.NoBlood)
			{
				if (this.Police.Deaths > 0)
				{
					this.CheckForBlood = true;
				}
				if (this.CheckForBlood)
				{
					if (this.Police.BloodParent.childCount == 0)
					{
						this.BloodTimer += Time.deltaTime;
						if (this.BloodTimer > 2f)
						{
							this.BloodCleaned = true;
						}
					}
					else
					{
						this.BloodTimer = 0f;
					}
				}
			}
			if (this.NoWeapon && !this.WeaponDisposed && this.Incinerator.Timer > 0f)
			{
				this.ID = 1;
				while (this.ID < this.Incinerator.DestroyedEvidence + 1)
				{
					if (this.Incinerator.EvidenceList[this.ID] == this.MurderWeaponID)
					{
						this.WeaponDisposed = true;
					}
					this.ID++;
				}
			}
			if (this.TimeLimit)
			{
				if (!this.Yandere.PauseScreen.Show)
				{
					this.TimeRemaining = Mathf.MoveTowards(this.TimeRemaining, 0f, Time.deltaTime);
				}
				int num2 = Mathf.CeilToInt(this.TimeRemaining);
				int num3 = num2 / 60;
				int num4 = num2 % 60;
				this.TimeLabel.text = string.Format("{0:00}:{1:00}", num3, num4);
				if (this.TimeRemaining == 0f)
				{
					this.Chastise = true;
					this.GameOverID = 10;
					this.GameOver();
					this.Phase = 4;
				}
			}
			if (this.Reputation.Reputation + this.Reputation.PendingRep <= -100f)
			{
				this.GameOverID = 14;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.NoSuspicion && this.Reputation.Reputation + this.Reputation.PendingRep < 0f)
			{
				this.GameOverID = 14;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.HeartbrokenCamera.activeInHierarchy)
			{
				this.HeartbrokenCamera.SetActive(false);
				this.GameOverID = 0;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.Clock.PresentTime > 1080f)
			{
				this.GameOverID = 11;
				this.GameOver();
				this.Phase = 4;
			}
			else if (this.Police.FadeOut)
			{
				this.GameOverID = 12;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.Yandere.ShoulderCamera.Noticed)
			{
				this.GameOverID = 17;
				this.GameOver();
				this.Phase = 4;
			}
			if (this.ExitPortal.activeInHierarchy)
			{
				if (this.Yandere.Chased || this.Yandere.Chasers > 0)
				{
					this.ExitPortalPrompt.Label[0].text = "     Cannot Exfiltrate!";
					this.ExitPortalPrompt.Circle[0].fillAmount = 1f;
				}
				else
				{
					this.ExitPortalPrompt.Label[0].text = "     Exfiltrate";
					if (this.ExitPortalPrompt.Circle[0].fillAmount == 0f)
					{
						this.StudentManager.DisableChaseCameras();
						this.MainCamera.transform.position = new Vector3(0.5f, 2.25f, -100.5f);
						this.MainCamera.transform.eulerAngles = Vector3.zero;
						this.Yandere.transform.eulerAngles = new Vector3(0f, 180f, 0f);
						this.Yandere.transform.position = new Vector3(0f, 0f, -94.5f);
						this.Yandere.Character.GetComponent<Animation>().Play(this.Yandere.WalkAnim);
						this.Yandere.RPGCamera.enabled = false;
						this.Yandere.HUD.gameObject.SetActive(false);
						this.Yandere.CanMove = false;
						AudioSource component = this.Jukebox.MissionMode.GetComponent<AudioSource>();
						component.clip = this.StealthMusic[10];
						component.loop = false;
						component.Play();
						base.GetComponent<AudioSource>().PlayOneShot(this.InfoAccomplished);
						this.HeartbeatCamera.SetActive(false);
						this.Boundary.enabled = false;
						this.Phase++;
					}
				}
			}
			if (this.TargetDead && this.CorpseDisposed && this.BloodCleaned && this.WeaponDisposed && this.DocumentsStolen && this.GameOverID == 0 && !this.ExitPortal.activeInHierarchy)
			{
				this.NotificationManager.DisplayNotification(NotificationType.Complete);
				this.NotificationManager.DisplayNotification(NotificationType.Exfiltrate);
				base.GetComponent<AudioSource>().PlayOneShot(this.InfoExfiltrate);
				this.ExitPortal.SetActive(true);
			}
			if (this.NoBlood && this.BloodCleaned && this.Police.BloodParent.childCount > 0)
			{
				this.ExitPortal.SetActive(false);
				this.BloodCleaned = false;
				this.BloodTimer = 0f;
			}
			if (!this.InfoRemark && this.GameOverID == 0 && this.TargetDead && (!this.CorpseDisposed || !this.BloodCleaned || !this.WeaponDisposed))
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.InfoObjective);
				this.InfoRemark = true;
			}
		}
		else if (this.Phase == 3)
		{
			this.Timer += Time.deltaTime;
			this.MainCamera.transform.position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y - Time.deltaTime * 0.2f, this.MainCamera.transform.position.z);
			this.Yandere.transform.position = new Vector3(this.Yandere.transform.position.x, this.Yandere.transform.position.y, this.Yandere.transform.position.z - Time.deltaTime);
			if (this.Timer > 5f)
			{
				this.Success();
				this.Timer = 0f;
				this.Phase++;
			}
		}
		else if (this.Phase == 4)
		{
			this.Timer += 0.0166666675f;
			if (this.Timer > 1f)
			{
				if (!this.FadeOut)
				{
					if (!this.PromptBar.Show)
					{
						this.PromptBar.Show = true;
					}
					else if (Input.GetButtonDown("A"))
					{
						this.PromptBar.Show = false;
						this.Destination = 1;
						this.FadeOut = true;
					}
					else if (Input.GetButtonDown("B"))
					{
						this.PromptBar.Show = false;
						this.Destination = 2;
						this.FadeOut = true;
					}
					else if (Input.GetButtonDown("X"))
					{
						this.PromptBar.Show = false;
						this.Destination = 3;
						this.FadeOut = true;
					}
				}
				else
				{
					this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, 0.0166666675f));
					this.Jukebox.Dip = Mathf.MoveTowards(this.Jukebox.Dip, 0f, 0.0166666675f);
					if (this.Darkness.color.a > 0.9921875f)
					{
						if (this.Destination == 1)
						{
							this.LoadingLabel.enabled = true;
							this.ResetGlobals();
							SceneManager.LoadScene(SceneManager.GetActiveScene().name);
						}
						else if (this.Destination == 2)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene("MissionModeScene");
						}
						else if (this.Destination == 3)
						{
							Globals.DeleteAll();
							SceneManager.LoadScene("TitleScene");
						}
					}
				}
			}
			if (this.GameOverPhase == 1)
			{
				if (this.Timer > 2.5f)
				{
					if (this.Chastise)
					{
						base.GetComponent<AudioSource>().PlayOneShot(this.InfoFailure);
						this.GameOverPhase++;
					}
					else
					{
						this.GameOverPhase++;
						this.Timer += 5f;
					}
				}
			}
			else if (this.GameOverPhase == 2 && this.Timer > 7.5f)
			{
				this.Jukebox.MissionMode.GetComponent<AudioSource>().clip = this.StealthMusic[0];
				this.Jukebox.MissionMode.GetComponent<AudioSource>().Play();
				this.Jukebox.Volume = 0.5f;
				this.GameOverPhase++;
			}
		}
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00065200 File Offset: 0x00063600
	public void GameOver()
	{
		if (this.Yandere.Aiming)
		{
			this.Yandere.StopAiming();
		}
		if (this.Yandere.YandereVision)
		{
			this.Yandere.YandereVision = false;
			this.Yandere.ResetYandereEffects();
		}
		this.Yandere.enabled = false;
		this.GameOverReason.text = this.GameOverReasons[this.GameOverID];
		if (this.ColorCorrections.Length > 0)
		{
			this.ColorCorrections[2].enabled = true;
		}
		base.GetComponent<AudioSource>().PlayOneShot(this.GameOverSound);
		this.DetectionCamera.SetActive(false);
		this.HeartbeatCamera.SetActive(false);
		this.WitnessCamera.SetActive(false);
		this.GameOverText.SetActive(true);
		this.Yandere.HUD.gameObject.SetActive(false);
		this.Subtitle.SetActive(false);
		Time.timeScale = 0.0001f;
		this.GameOverPhase = 1;
		this.Jukebox.MissionMode.GetComponent<AudioSource>().Stop();
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00065318 File Offset: 0x00063718
	private void Success()
	{
		this.GameOverHeader.transform.localPosition = new Vector3(this.GameOverHeader.transform.localPosition.x, 0f, this.GameOverHeader.transform.localPosition.z);
		this.GameOverHeader.text = "MISSION ACCOMPLISHED";
		this.GameOverReason.gameObject.SetActive(false);
		this.ColorCorrections[2].enabled = true;
		this.DetectionCamera.SetActive(false);
		this.WitnessCamera.SetActive(false);
		this.GameOverText.SetActive(true);
		this.GameOverReason.text = string.Empty;
		this.Subtitle.SetActive(false);
		this.Jukebox.Volume = 1f;
		Time.timeScale = 0.0001f;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x000653F8 File Offset: 0x000637F8
	public void ChangeMusic()
	{
		this.MusicID++;
		if (this.MusicID > 9)
		{
			this.MusicID = 1;
		}
		this.Jukebox.MissionMode.GetComponent<AudioSource>().clip = this.StealthMusic[this.MusicID];
		this.Jukebox.MissionMode.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00065460 File Offset: 0x00063860
	private void ResetGlobals()
	{
		Debug.Log("Mission Difficulty was: " + MissionModeGlobals.MissionDifficulty);
		int disableFarAnimations = OptionGlobals.DisableFarAnimations;
		bool disablePostAliasing = OptionGlobals.DisablePostAliasing;
		bool disableOutlines = OptionGlobals.DisableOutlines;
		int lowDetailStudents = OptionGlobals.LowDetailStudents;
		int particleCount = OptionGlobals.ParticleCount;
		bool enableShadows = OptionGlobals.EnableShadows;
		int drawDistance = OptionGlobals.DrawDistance;
		int drawDistanceLimit = OptionGlobals.DrawDistanceLimit;
		bool disableBloom = OptionGlobals.DisableBloom;
		bool fog = OptionGlobals.Fog;
		string missionTargetName = MissionModeGlobals.MissionTargetName;
		bool highPopulation = OptionGlobals.HighPopulation;
		Globals.DeleteAll();
		OptionGlobals.TutorialsOff = true;
		for (int i = 1; i < 11; i++)
		{
			PlayerPrefs.SetInt("MissionModeTarget" + i, this.Target[i]);
			PlayerPrefs.SetInt("MissionModeMethod" + i, this.Method[i]);
		}
		SchoolGlobals.SchoolAtmosphere = 1f - (float)this.Difficulty * 0.1f;
		MissionModeGlobals.MissionTargetName = missionTargetName;
		MissionModeGlobals.MissionDifficulty = this.Difficulty;
		OptionGlobals.HighPopulation = highPopulation;
		MissionModeGlobals.MissionTarget = this.TargetID;
		SchoolGlobals.SchoolAtmosphereSet = true;
		MissionModeGlobals.MissionMode = true;
		MissionModeGlobals.MultiMission = this.MultiMission;
		MissionModeGlobals.MissionRequiredWeapon = this.RequiredWeaponID;
		MissionModeGlobals.MissionRequiredClothing = this.RequiredClothingID;
		MissionModeGlobals.MissionRequiredDisposal = this.RequiredDisposalID;
		ClassGlobals.BiologyGrade = 1;
		ClassGlobals.ChemistryGrade = 1;
		ClassGlobals.LanguageGrade = 1;
		ClassGlobals.PhysicalGrade = 1;
		ClassGlobals.PsychologyGrade = 1;
		this.ID = 2;
		while (this.ID < 11)
		{
			MissionModeGlobals.SetMissionCondition(this.ID, this.Conditions[this.ID]);
			this.ID++;
		}
		MissionModeGlobals.NemesisDifficulty = this.NemesisDifficulty;
		OptionGlobals.DisableFarAnimations = disableFarAnimations;
		OptionGlobals.DisablePostAliasing = disablePostAliasing;
		OptionGlobals.DisableOutlines = disableOutlines;
		OptionGlobals.LowDetailStudents = lowDetailStudents;
		OptionGlobals.ParticleCount = particleCount;
		OptionGlobals.EnableShadows = enableShadows;
		OptionGlobals.DrawDistance = drawDistance;
		OptionGlobals.DrawDistanceLimit = drawDistanceLimit;
		OptionGlobals.DisableBloom = disableBloom;
		OptionGlobals.Fog = fog;
		Debug.Log("Mission Difficulty is now: " + MissionModeGlobals.MissionDifficulty);
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00065670 File Offset: 0x00063A70
	private void ChangeAllText()
	{
		UILabel[] array = UnityEngine.Object.FindObjectsOfType<UILabel>();
		foreach (UILabel uilabel in array)
		{
			float a = uilabel.color.a;
			uilabel.color = new Color(1f, 1f, 1f, a);
			uilabel.trueTypeFont = this.Arial;
		}
		UISprite[] array3 = UnityEngine.Object.FindObjectsOfType<UISprite>();
		foreach (UISprite uisprite in array3)
		{
			float a2 = uisprite.color.a;
			if (uisprite.color != new Color(0f, 0f, 0f, a2))
			{
				uisprite.color = new Color(1f, 1f, 1f, a2);
			}
		}
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00065758 File Offset: 0x00063B58
	private void CheckForCompletion()
	{
		if (!this.Checking[1] && !this.Checking[2] && !this.Checking[3] && !this.Checking[4] && !this.Checking[5] && !this.Checking[6] && !this.Checking[7] && !this.Checking[8] && !this.Checking[9] && !this.Checking[10])
		{
			this.TargetDead = true;
		}
	}

	// Token: 0x04000A10 RID: 2576
	public NotificationManagerScript NotificationManager;

	// Token: 0x04000A11 RID: 2577
	public NewMissionWindowScript NewMissionWindow;

	// Token: 0x04000A12 RID: 2578
	public MissionModeMenuScript MissionModeMenu;

	// Token: 0x04000A13 RID: 2579
	public StudentManagerScript StudentManager;

	// Token: 0x04000A14 RID: 2580
	public WeaponManagerScript WeaponManager;

	// Token: 0x04000A15 RID: 2581
	public PromptScript ExitPortalPrompt;

	// Token: 0x04000A16 RID: 2582
	public IncineratorScript Incinerator;

	// Token: 0x04000A17 RID: 2583
	public WoodChipperScript WoodChipper;

	// Token: 0x04000A18 RID: 2584
	public ReputationScript Reputation;

	// Token: 0x04000A19 RID: 2585
	public GrayscaleEffect Grayscale;

	// Token: 0x04000A1A RID: 2586
	public PromptBarScript PromptBar;

	// Token: 0x04000A1B RID: 2587
	public BoundaryScript Boundary;

	// Token: 0x04000A1C RID: 2588
	public JukeboxScript Jukebox;

	// Token: 0x04000A1D RID: 2589
	public YandereScript Yandere;

	// Token: 0x04000A1E RID: 2590
	public PoliceScript Police;

	// Token: 0x04000A1F RID: 2591
	public ClockScript Clock;

	// Token: 0x04000A20 RID: 2592
	public UILabel EventSubtitleLabel;

	// Token: 0x04000A21 RID: 2593
	public UILabel ReputationLabel;

	// Token: 0x04000A22 RID: 2594
	public UILabel GameOverHeader;

	// Token: 0x04000A23 RID: 2595
	public UILabel GameOverReason;

	// Token: 0x04000A24 RID: 2596
	public UILabel SubtitleLabel;

	// Token: 0x04000A25 RID: 2597
	public UILabel LoadingLabel;

	// Token: 0x04000A26 RID: 2598
	public UILabel SpottedLabel;

	// Token: 0x04000A27 RID: 2599
	public UILabel SanityLabel;

	// Token: 0x04000A28 RID: 2600
	public UILabel MoneyLabel;

	// Token: 0x04000A29 RID: 2601
	public UILabel TimeLabel;

	// Token: 0x04000A2A RID: 2602
	public UISprite ReputationFace1;

	// Token: 0x04000A2B RID: 2603
	public UISprite ReputationFace2;

	// Token: 0x04000A2C RID: 2604
	public UISprite ReputationBG;

	// Token: 0x04000A2D RID: 2605
	public UISprite CautionSign;

	// Token: 0x04000A2E RID: 2606
	public UISprite MusicIcon;

	// Token: 0x04000A2F RID: 2607
	public UISprite Darkness;

	// Token: 0x04000A30 RID: 2608
	public GUIText FPS;

	// Token: 0x04000A31 RID: 2609
	public GardenHoleScript[] GardenHoles;

	// Token: 0x04000A32 RID: 2610
	public GameObject[] ReputationIcons;

	// Token: 0x04000A33 RID: 2611
	public string[] GameOverReasons;

	// Token: 0x04000A34 RID: 2612
	public AudioClip[] StealthMusic;

	// Token: 0x04000A35 RID: 2613
	public Transform[] SpawnPoints;

	// Token: 0x04000A36 RID: 2614
	public UISprite[] PoliceIcon;

	// Token: 0x04000A37 RID: 2615
	public UILabel[] PoliceLabel;

	// Token: 0x04000A38 RID: 2616
	public int[] Conditions;

	// Token: 0x04000A39 RID: 2617
	public GameObject SecurityCameraGroup;

	// Token: 0x04000A3A RID: 2618
	public GameObject MetalDetectorGroup;

	// Token: 0x04000A3B RID: 2619
	public GameObject HeartbrokenCamera;

	// Token: 0x04000A3C RID: 2620
	public GameObject DetectionCamera;

	// Token: 0x04000A3D RID: 2621
	public GameObject HeartbeatCamera;

	// Token: 0x04000A3E RID: 2622
	public GameObject MissionModeHUD;

	// Token: 0x04000A3F RID: 2623
	public GameObject SpottedWindow;

	// Token: 0x04000A40 RID: 2624
	public GameObject TranqDetector;

	// Token: 0x04000A41 RID: 2625
	public GameObject WitnessCamera;

	// Token: 0x04000A42 RID: 2626
	public GameObject GameOverText;

	// Token: 0x04000A43 RID: 2627
	public GameObject VoidGoddess;

	// Token: 0x04000A44 RID: 2628
	public GameObject Headmaster;

	// Token: 0x04000A45 RID: 2629
	public GameObject ExitPortal;

	// Token: 0x04000A46 RID: 2630
	public GameObject MurderKit;

	// Token: 0x04000A47 RID: 2631
	public GameObject Subtitle;

	// Token: 0x04000A48 RID: 2632
	public GameObject Nemesis;

	// Token: 0x04000A49 RID: 2633
	public GameObject Safe;

	// Token: 0x04000A4A RID: 2634
	public Transform LastKnownPosition;

	// Token: 0x04000A4B RID: 2635
	public int RequiredClothingID;

	// Token: 0x04000A4C RID: 2636
	public int RequiredDisposalID;

	// Token: 0x04000A4D RID: 2637
	public int RequiredWeaponID;

	// Token: 0x04000A4E RID: 2638
	public int NemesisDifficulty;

	// Token: 0x04000A4F RID: 2639
	public int DisposalMethod;

	// Token: 0x04000A50 RID: 2640
	public int MurderWeaponID;

	// Token: 0x04000A51 RID: 2641
	public int GameOverPhase;

	// Token: 0x04000A52 RID: 2642
	public int Destination;

	// Token: 0x04000A53 RID: 2643
	public int Difficulty;

	// Token: 0x04000A54 RID: 2644
	public int GameOverID;

	// Token: 0x04000A55 RID: 2645
	public int TargetID;

	// Token: 0x04000A56 RID: 2646
	public int MusicID = 1;

	// Token: 0x04000A57 RID: 2647
	public int Phase = 1;

	// Token: 0x04000A58 RID: 2648
	public int ID;

	// Token: 0x04000A59 RID: 2649
	public int[] Target;

	// Token: 0x04000A5A RID: 2650
	public int[] Method;

	// Token: 0x04000A5B RID: 2651
	public bool SecurityCameras;

	// Token: 0x04000A5C RID: 2652
	public bool MetalDetectors;

	// Token: 0x04000A5D RID: 2653
	public bool StealDocuments;

	// Token: 0x04000A5E RID: 2654
	public bool NoCollateral;

	// Token: 0x04000A5F RID: 2655
	public bool NoSuspicion;

	// Token: 0x04000A60 RID: 2656
	public bool NoWitnesses;

	// Token: 0x04000A61 RID: 2657
	public bool NoCorpses;

	// Token: 0x04000A62 RID: 2658
	public bool NoSpeech;

	// Token: 0x04000A63 RID: 2659
	public bool NoWeapon;

	// Token: 0x04000A64 RID: 2660
	public bool NoBlood;

	// Token: 0x04000A65 RID: 2661
	public bool TimeLimit;

	// Token: 0x04000A66 RID: 2662
	public bool CorrectClothingConfirmed;

	// Token: 0x04000A67 RID: 2663
	public bool DocumentsStolen;

	// Token: 0x04000A68 RID: 2664
	public bool CorpseDisposed;

	// Token: 0x04000A69 RID: 2665
	public bool WeaponDisposed;

	// Token: 0x04000A6A RID: 2666
	public bool CheckForBlood;

	// Token: 0x04000A6B RID: 2667
	public bool BloodCleaned;

	// Token: 0x04000A6C RID: 2668
	public bool MultiMission;

	// Token: 0x04000A6D RID: 2669
	public bool InfoRemark;

	// Token: 0x04000A6E RID: 2670
	public bool TargetDead;

	// Token: 0x04000A6F RID: 2671
	public bool Chastise;

	// Token: 0x04000A70 RID: 2672
	public bool FadeOut;

	// Token: 0x04000A71 RID: 2673
	public bool Enabled;

	// Token: 0x04000A72 RID: 2674
	public bool[] Checking;

	// Token: 0x04000A73 RID: 2675
	public string CauseOfFailure = string.Empty;

	// Token: 0x04000A74 RID: 2676
	public float TimeRemaining = 300f;

	// Token: 0x04000A75 RID: 2677
	public float TargetHeight;

	// Token: 0x04000A76 RID: 2678
	public float BloodTimer;

	// Token: 0x04000A77 RID: 2679
	public float Speed;

	// Token: 0x04000A78 RID: 2680
	public float Timer;

	// Token: 0x04000A79 RID: 2681
	public AudioClip InfoAccomplished;

	// Token: 0x04000A7A RID: 2682
	public AudioClip InfoExfiltrate;

	// Token: 0x04000A7B RID: 2683
	public AudioClip InfoObjective;

	// Token: 0x04000A7C RID: 2684
	public AudioClip InfoFailure;

	// Token: 0x04000A7D RID: 2685
	public AudioClip GameOverSound;

	// Token: 0x04000A7E RID: 2686
	public ColorCorrectionCurves[] ColorCorrections;

	// Token: 0x04000A7F RID: 2687
	public Camera MainCamera;

	// Token: 0x04000A80 RID: 2688
	public UILabel Watermark;

	// Token: 0x04000A81 RID: 2689
	public Font Arial;

	// Token: 0x04000A82 RID: 2690
	public int Frame;
}
