using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020003B2 RID: 946
public class EndOfDayScript : MonoBehaviour
{
	// Token: 0x06001931 RID: 6449 RVA: 0x000E86F0 File Offset: 0x000E6AF0
	public void Start()
	{
		if (GameGlobals.SenpaiMourning)
		{
			this.StopMourning = true;
		}
		this.Yandere.MainCamera.gameObject.SetActive(false);
		this.EndOfDayDarkness.color = new Color(this.EndOfDayDarkness.color.r, this.EndOfDayDarkness.color.g, this.EndOfDayDarkness.color.b, 1f);
		this.PreviouslyActivated = true;
		base.GetComponent<AudioSource>().volume = 0f;
		this.Clock.enabled = false;
		this.Clock.MainLight.color = new Color(1f, 1f, 1f, 1f);
		RenderSettings.ambientLight = new Color(0.75f, 0.75f, 0.75f, 1f);
		RenderSettings.skybox.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
		this.UpdateScene();
		this.CopAnimation[5]["idleShort_00"].speed = UnityEngine.Random.Range(0.9f, 1.1f);
		this.CopAnimation[6]["idleShort_00"].speed = UnityEngine.Random.Range(0.9f, 1.1f);
		this.CopAnimation[7]["idleShort_00"].speed = UnityEngine.Random.Range(0.9f, 1.1f);
		Time.timeScale = 1f;
		for (int i = 1; i < 6; i++)
		{
			this.Yandere.CharacterAnimation[this.Yandere.CreepyIdles[i]].weight = 0f;
			this.Yandere.CharacterAnimation[this.Yandere.CreepyWalks[i]].weight = 0f;
		}
		Debug.Log("BloodParent.childCount is: " + this.Police.BloodParent.childCount);
		IEnumerator enumerator = this.Police.BloodParent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				PickUpScript component = transform.gameObject.GetComponent<PickUpScript>();
				if (component != null && component.RedPaint)
				{
					this.ClothingWithRedPaint++;
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
		Debug.Log("Clothing with red paint is: " + this.ClothingWithRedPaint);
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x000E89B4 File Offset: 0x000E6DB4
	private void Update()
	{
		this.Yandere.UpdateSlouch();
		if (Input.GetKeyDown("space"))
		{
			this.EndOfDayDarkness.color = new Color(0f, 0f, 0f, 1f);
			this.Darken = true;
		}
		if (this.EndOfDayDarkness.color.a == 0f && Input.GetButtonDown("A"))
		{
			this.Darken = true;
		}
		if (this.Darken)
		{
			this.EndOfDayDarkness.color = new Color(this.EndOfDayDarkness.color.r, this.EndOfDayDarkness.color.g, this.EndOfDayDarkness.color.b, Mathf.MoveTowards(this.EndOfDayDarkness.color.a, 1f, Time.deltaTime * 2f));
			if (this.EndOfDayDarkness.color.a == 1f)
			{
				if (this.Senpai == null && this.StudentManager.Students[1] != null)
				{
					this.Senpai = this.StudentManager.Students[1];
					this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.Senpai.CharacterAnimation.enabled = true;
				}
				if (this.Senpai != null)
				{
					this.Senpai.gameObject.SetActive(false);
				}
				if (this.Rival == null && this.StudentManager.Students[this.StudentManager.RivalID] != null)
				{
					this.Rival = this.StudentManager.Students[this.StudentManager.RivalID];
					this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.Rival.CharacterAnimation.enabled = true;
				}
				if (this.Rival != null)
				{
					this.Rival.gameObject.SetActive(false);
				}
				this.Yandere.transform.parent = null;
				this.Yandere.transform.position = new Vector3(0f, 0f, -75f);
				this.EODCamera.localPosition = new Vector3(1f, 1.8f, -2.5f);
				this.EODCamera.localEulerAngles = new Vector3(22.5f, -22.5f, 0f);
				if (this.KidnappedVictim != null)
				{
					this.KidnappedVictim.gameObject.SetActive(false);
				}
				this.CardboardBox.parent = null;
				this.SearchingCop.SetActive(false);
				this.MurderScene.SetActive(false);
				this.Cops.SetActive(false);
				this.TabletCop.SetActive(false);
				this.ShruggingCops.SetActive(false);
				this.SecuritySystemScene.SetActive(false);
				this.OpenTranqCase.SetActive(false);
				this.ClosedTranqCase.SetActive(false);
				this.GaudyRing.SetActive(false);
				this.AnswerSheet.SetActive(false);
				this.Fence.SetActive(false);
				this.SCP.SetActive(false);
				this.ArrestingCops.SetActive(false);
				this.Mask.SetActive(false);
				this.EyeWitnessScene.SetActive(false);
				this.ScaredCops.SetActive(false);
				if (this.WitnessList[1] != null)
				{
					this.WitnessList[1].gameObject.SetActive(false);
				}
				if (this.WitnessList[2] != null)
				{
					this.WitnessList[2].gameObject.SetActive(false);
				}
				if (this.WitnessList[3] != null)
				{
					this.WitnessList[3].gameObject.SetActive(false);
				}
				if (this.WitnessList[4] != null)
				{
					this.WitnessList[4].gameObject.SetActive(false);
				}
				if (this.WitnessList[5] != null)
				{
					this.WitnessList[5].gameObject.SetActive(false);
				}
				if (this.Patsy != null)
				{
					this.Patsy.gameObject.SetActive(false);
				}
				if (!this.GameOver)
				{
					this.Darken = false;
					this.UpdateScene();
				}
				else
				{
					this.Heartbroken.transform.parent.transform.parent = null;
					this.Heartbroken.transform.parent.gameObject.SetActive(true);
					this.Heartbroken.Noticed = false;
					this.Heartbroken.Arrested = true;
					this.MainCamera.SetActive(false);
					base.gameObject.SetActive(false);
					Time.timeScale = 1f;
				}
			}
		}
		else
		{
			this.EndOfDayDarkness.color = new Color(this.EndOfDayDarkness.color.r, this.EndOfDayDarkness.color.g, this.EndOfDayDarkness.color.b, Mathf.MoveTowards(this.EndOfDayDarkness.color.a, 0f, Time.deltaTime * 2f));
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.volume = Mathf.MoveTowards(component.volume, 1f, Time.deltaTime * 2f);
		if (this.WitnessList[2] != null)
		{
			this.WitnessList[2].CharacterAnimation.Play(this.WitnessList[2].IdleAnim);
		}
		if (this.WitnessList[3] != null)
		{
			this.WitnessList[3].CharacterAnimation.Play(this.WitnessList[3].IdleAnim);
		}
		if (this.WitnessList[4] != null)
		{
			this.WitnessList[4].CharacterAnimation.Play(this.WitnessList[4].IdleAnim);
		}
		if (this.WitnessList[5] != null)
		{
			this.WitnessList[5].CharacterAnimation.Play(this.WitnessList[5].IdleAnim);
		}
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x000E9028 File Offset: 0x000E7428
	public void UpdateScene()
	{
		this.Label.color = new Color(0f, 0f, 0f, 1f);
		this.ID = 0;
		while (this.ID < this.WeaponManager.Weapons.Length)
		{
			if (this.WeaponManager.Weapons[this.ID] != null)
			{
				this.WeaponManager.Weapons[this.ID].gameObject.SetActive(false);
			}
			this.ID++;
		}
		if (this.PoliceArrived)
		{
			if (Input.GetKeyDown(KeyCode.Backspace))
			{
				this.Finish();
			}
			if (this.Phase == 1)
			{
				this.CopAnimation[1]["walk_00"].speed = UnityEngine.Random.Range(0.9f, 1.1f);
				this.CopAnimation[2]["walk_00"].speed = UnityEngine.Random.Range(0.9f, 1.1f);
				this.CopAnimation[3]["walk_00"].speed = UnityEngine.Random.Range(0.9f, 1.1f);
				this.Cops.SetActive(true);
				if (this.Yandere.Egg)
				{
					this.Label.text = "The police arrive at school.";
					this.Phase = 999;
				}
				else if (this.Police.PoisonScene)
				{
					this.Label.text = "The police and the paramedics arrive at school.";
					this.Phase = 103;
				}
				else if (this.Police.DrownVictims > 0)
				{
					this.Label.text = "The police arrive at school.";
					this.Phase = 104;
				}
				else if (this.Police.ElectroScene)
				{
					this.Label.text = "The police arrive at school.";
					this.Phase = 105;
				}
				else if (this.Police.MurderSuicideScene)
				{
					this.Label.text = "The police arrive at school, and discover what appears to be the scene of a murder-suicide.";
					this.Phase++;
				}
				else
				{
					this.Label.text = "The police arrive at school.";
					if (this.Police.SuicideScene)
					{
						this.Phase = 102;
					}
					else
					{
						this.Phase++;
					}
				}
			}
			else if (this.Phase == 2)
			{
				if (this.Police.Corpses == 0)
				{
					if (!this.Police.PoisonScene && !this.Police.SuicideScene)
					{
						if (this.Police.LimbParent.childCount > 0)
						{
							if (this.Police.LimbParent.childCount == 1)
							{
								this.Label.text = "The police find a severed body part at school.";
							}
							else
							{
								this.Label.text = "The police find multiple severed body parts at school.";
							}
							this.MurderScene.SetActive(true);
						}
						else
						{
							this.SearchingCop.SetActive(true);
							if (this.Police.BloodParent.childCount - this.ClothingWithRedPaint > 0)
							{
								this.Label.text = "The police find mysterious blood stains, but are unable to locate any corpses on school grounds.";
							}
							else if (this.ClothingWithRedPaint == 0)
							{
								this.Label.text = "The police are unable to locate any corpses on school grounds.";
							}
							else
							{
								this.Label.text = "The police find clothing that is stained with red paint, but are unable to locate any actual blood stains, and cannot locate any corpses, either.";
							}
						}
						this.Phase++;
					}
					else
					{
						this.SearchingCop.SetActive(true);
						this.Label.text = "The police are unable to locate any other corpses on school grounds.";
						this.Phase++;
					}
				}
				else
				{
					this.MurderScene.SetActive(true);
					List<string> list = new List<string>();
					foreach (RagdollScript ragdollScript in this.Police.CorpseList)
					{
						if (ragdollScript != null && !ragdollScript.Disposed)
						{
							if (ragdollScript.Student.StudentID == this.StudentManager.RivalID)
							{
								this.RivalEliminationMethod = RivalEliminationType.Dead;
							}
							this.VictimArray[this.Corpses] = ragdollScript.Student.StudentID;
							list.Add(ragdollScript.Student.Name);
							this.Corpses++;
						}
					}
					list.Sort();
					string text = "The police discover the corpse" + ((list.Count != 1) ? "s" : string.Empty) + " of ";
					if (list.Count == 1)
					{
						this.Label.text = text + list[0] + ".";
					}
					else if (list.Count == 2)
					{
						this.Label.text = string.Concat(new string[]
						{
							text,
							list[0],
							" and ",
							list[1],
							"."
						});
					}
					else if (list.Count < 6)
					{
						this.Label.text = "The police discover multiple corpses on school grounds.";
						StringBuilder stringBuilder = new StringBuilder();
						for (int j = 0; j < list.Count - 1; j++)
						{
							stringBuilder.Append(list[j] + ", ");
						}
						stringBuilder.Append("and " + list[list.Count - 1] + ".");
						this.Label.text = text + stringBuilder.ToString();
					}
					else
					{
						this.Label.text = "The police discover more than five corpses on school grounds.";
					}
					this.Phase++;
				}
			}
			else if (this.Phase == 3)
			{
				this.WeaponManager.CheckWeapons();
				if (this.WeaponManager.MurderWeapons == 0)
				{
					this.ShruggingCops.SetActive(true);
					if (this.Weapons == 0)
					{
						this.Label.text = "The police are unable to locate any murder weapons.";
						this.Phase += 2;
					}
					else
					{
						this.Phase += 2;
						this.UpdateScene();
					}
				}
				else
				{
					this.MurderWeapon = null;
					this.ID = 0;
					while (this.ID < this.WeaponManager.Weapons.Length)
					{
						if (this.MurderWeapon == null)
						{
							WeaponScript weaponScript = this.WeaponManager.Weapons[this.ID];
							if (weaponScript != null && weaponScript.Blood.enabled)
							{
								if (!weaponScript.AlreadyExamined)
								{
									weaponScript.gameObject.SetActive(true);
									weaponScript.AlreadyExamined = true;
									this.MurderWeapon = weaponScript;
									this.WeaponID = this.ID;
								}
								else
								{
									weaponScript.gameObject.SetActive(false);
								}
							}
						}
						this.ID++;
					}
					List<string> list2 = new List<string>();
					this.ID = 0;
					while (this.ID < this.MurderWeapon.Victims.Length)
					{
						if (this.MurderWeapon.Victims[this.ID])
						{
							list2.Add(this.JSON.Students[this.ID].Name);
						}
						this.ID++;
					}
					list2.Sort();
					this.Victims = list2.Count;
					string name = this.MurderWeapon.Name;
					string str = (name[name.Length - 1] == 's') ? (name.ToLower() + " that are") : ("a " + name.ToLower() + " that is");
					string text2 = "The police discover " + str + " stained with the blood of ";
					if (list2.Count == 1)
					{
						this.Label.text = text2 + list2[0] + ".";
					}
					else if (list2.Count == 2)
					{
						this.Label.text = string.Concat(new string[]
						{
							text2,
							list2[0],
							" and ",
							list2[1],
							"."
						});
					}
					else
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						for (int k = 0; k < list2.Count - 1; k++)
						{
							stringBuilder2.Append(list2[k] + ", ");
						}
						stringBuilder2.Append("and " + list2[list2.Count - 1] + ".");
						this.Label.text = text2 + stringBuilder2.ToString();
					}
					this.Weapons++;
					this.Phase++;
					this.MurderWeapon.transform.parent = base.transform;
					this.MurderWeapon.transform.localPosition = new Vector3(0.6f, 1.4f, -1.5f);
					this.MurderWeapon.transform.localEulerAngles = new Vector3(-45f, 90f, -90f);
					this.MurderWeapon.MyRigidbody.useGravity = false;
					this.MurderWeapon.Rotate = true;
				}
			}
			else if (this.Phase == 4)
			{
				if (this.MurderWeapon.FingerprintID == 0)
				{
					this.ShruggingCops.SetActive(true);
					this.Label.text = "The police find no fingerprints on the weapon.";
					this.Phase = 3;
				}
				else if (this.MurderWeapon.FingerprintID == 100)
				{
					this.TeleportYandere();
					this.Yandere.CharacterAnimation.Play("f02_disappointed_00");
					this.Label.text = "The police find Ayano's fingerprints on the weapon.";
					this.Phase = 100;
				}
				else
				{
					int fingerprintID = this.WeaponManager.Weapons[this.WeaponID].FingerprintID;
					this.TabletCop.SetActive(true);
					this.CopAnimation[4]["scienceTablet_00"].speed = 0f;
					this.TabletPortrait.material.mainTexture = this.VoidGoddess.Portraits[fingerprintID].mainTexture;
					this.Label.text = "The police find the fingerprints of " + this.JSON.Students[fingerprintID].Name + " on the weapon.";
					this.Phase = 101;
				}
			}
			else if (this.Phase == 5)
			{
				if (this.Police.PhotoEvidence > 0)
				{
					this.TeleportYandere();
					this.Yandere.CharacterAnimation.Play("f02_disappointed_00");
					this.Label.text = "The police find a smartphone with photographic evidence of Ayano committing a crime.";
					this.Phase = 100;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 6)
			{
				if (SchoolGlobals.HighSecurity)
				{
					this.SecuritySystemScene.SetActive(true);
					if (!this.SecuritySystem.Evidence)
					{
						this.Label.text = "The police investigate the security camera recordings, but cannot find anything incriminating in the footage.";
						this.Phase++;
					}
					else if (!this.SecuritySystem.Masked)
					{
						this.Label.text = "The police investigate the security camera recordings, and find incriminating footage of Ayano.";
						this.Phase = 100;
					}
					else
					{
						this.Label.text = "The police investigate the security camera recordings, and find footage of a suspicious masked person.";
						this.Police.MaskReported = true;
						this.Phase++;
					}
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 7)
			{
				this.ID = 1;
				while (this.ID < this.StudentManager.Students.Length)
				{
					if (this.StudentManager.Students[this.ID] != null && this.StudentManager.Students[this.ID].Alive && this.StudentManager.Students[this.ID].Persona != PersonaType.Coward && this.StudentManager.Students[this.ID].Persona != PersonaType.Spiteful && this.StudentManager.Students[this.ID].Club != ClubType.Delinquent && !this.StudentManager.Students[this.ID].SawMask && this.StudentManager.Students[this.ID].WitnessedMurder)
					{
						this.EyeWitnesses++;
						this.WitnessList[this.EyeWitnesses] = this.StudentManager.Students[this.ID];
					}
					this.ID++;
				}
				if (this.EyeWitnesses > 0)
				{
					this.DisableThings(this.WitnessList[1]);
					this.DisableThings(this.WitnessList[2]);
					this.DisableThings(this.WitnessList[3]);
					this.DisableThings(this.WitnessList[4]);
					this.DisableThings(this.WitnessList[5]);
					this.WitnessList[1].transform.localPosition = Vector3.zero;
					if (this.WitnessList[2] != null)
					{
						this.WitnessList[2].transform.localPosition = new Vector3(-1f, 0f, -0.5f);
					}
					if (this.WitnessList[3] != null)
					{
						this.WitnessList[3].transform.localPosition = new Vector3(1f, 0f, -0.5f);
					}
					if (this.WitnessList[4] != null)
					{
						this.WitnessList[4].transform.localPosition = new Vector3(-2f, 0f, -1f);
					}
					if (this.WitnessList[5] != null)
					{
						this.WitnessList[5].transform.localPosition = new Vector3(1.5f, 0f, -1f);
					}
					if (this.WitnessList[1].Male)
					{
						this.WitnessList[1].CharacterAnimation.Play("carefreeTalk_02");
					}
					else
					{
						this.WitnessList[1].CharacterAnimation.Play("f02_carefreeTalk_02");
					}
					this.EyeWitnessScene.SetActive(true);
					if (this.EyeWitnesses == 1)
					{
						this.Label.text = "One student accuses Ayano of murder, but nobody else can corroborate that students' claims, so the police are unable to develop reasonable justification to arrest Ayano.";
						this.Phase++;
					}
					else if (this.EyeWitnesses < 5)
					{
						this.Label.text = "Several students accuse Ayano of murder, but there are not enough witnesses to provide the police with reasonable justification to arrest her.";
						this.Phase++;
					}
					else
					{
						this.Label.text = "Numerous students accuse Ayano of murder, providing the police with enough justification to arrest her.";
						this.Phase = 100;
					}
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 8)
			{
				this.ShruggingCops.SetActive(false);
				if (this.Yandere.Sanity > 33.33333f)
				{
					if ((this.Yandere.Bloodiness > 0f && !this.Yandere.RedPaint) || (this.Yandere.Gloved && this.Yandere.Gloves.Blood.enabled))
					{
						if (this.Arrests == 0)
						{
							this.TeleportYandere();
							this.Yandere.CharacterAnimation.Play("f02_disappointed_00");
							this.Label.text = "The police notice that Ayano's clothing is bloody. They confirm that the blood is not hers. Ayano is unable to convince the police that she did not commit murder.";
							this.Phase = 100;
						}
						else
						{
							this.TeleportYandere();
							this.Yandere.CharacterAnimation["YandereConfessionRejected"].time = 4f;
							this.Yandere.CharacterAnimation.Play("YandereConfessionRejected");
							this.Label.text = "The police notice that Ayano's clothing is bloody. They confirm that the blood is not hers. Ayano is able to convince the police that she was splashed with blood while witnessing a murder.";
							if (!this.TranqCase.Occupied)
							{
								this.Phase += 2;
							}
							else
							{
								this.Phase++;
							}
						}
					}
					else if (this.Police.BloodyClothing - this.ClothingWithRedPaint > 0)
					{
						this.TeleportYandere();
						this.Yandere.CharacterAnimation.Play("f02_disappointed_00");
						this.Label.text = "The police find bloody clothing that has traces of Ayano's DNA. Ayano is unable to convince the police that she did not commit murder.";
						this.Phase = 100;
					}
					else
					{
						this.TeleportYandere();
						this.Yandere.CharacterAnimation["YandereConfessionRejected"].time = 4f;
						this.Yandere.CharacterAnimation.Play("YandereConfessionRejected");
						this.Label.text = "The police question all students in the school, including Ayano. The police are unable to link Ayano to any crimes.";
						if (!this.TranqCase.Occupied)
						{
							this.Phase += 2;
						}
						else if (this.TranqCase.VictimID == this.ArrestID)
						{
							this.Phase += 2;
						}
						else
						{
							this.Phase++;
						}
					}
				}
				else
				{
					this.TeleportYandere();
					this.Yandere.CharacterAnimation.Play("f02_disappointed_00");
					if (this.Yandere.Bloodiness == 0f)
					{
						this.Label.text = "The police question Ayano, who exhibits extremely unusual behavior. The police decide to investigate Ayano further, and eventually learn of her crimes.";
						this.Phase = 100;
					}
					else
					{
						this.Label.text = "The police notice that Ayano is covered in blood and exhibiting extremely unusual behavior. The police decide to investigate Ayano further, and eventually learn of her crimes.";
						this.Phase = 100;
					}
				}
			}
			else if (this.Phase == 9)
			{
				this.KidnappedVictim = this.StudentManager.Students[this.TranqCase.VictimID];
				this.KidnappedVictim.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
				this.KidnappedVictim.CharacterAnimation.enabled = true;
				this.KidnappedVictim.gameObject.SetActive(true);
				this.KidnappedVictim.Ragdoll.Zs.SetActive(false);
				this.KidnappedVictim.transform.parent = base.transform;
				this.KidnappedVictim.transform.localPosition = new Vector3(0f, 0.145f, 0f);
				this.KidnappedVictim.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
				this.KidnappedVictim.CharacterAnimation.Play("f02_sit_06");
				this.KidnappedVictim.WhiteQuestionMark.SetActive(true);
				this.OpenTranqCase.SetActive(true);
				this.Label.text = "The police discover " + this.JSON.Students[this.TranqCase.VictimID].Name + " inside of a musical instrument case. However, she is unable to recall how she got inside of the case. The police are unable to determine what happened.";
				StudentGlobals.SetStudentKidnapped(this.TranqCase.VictimID, false);
				StudentGlobals.SetStudentMissing(this.TranqCase.VictimID, false);
				this.TranqCase.VictimClubType = ClubType.None;
				this.TranqCase.VictimID = 0;
				this.TranqCase.Occupied = false;
				this.Phase++;
			}
			else if (this.Phase == 10)
			{
				if (this.Police.MaskReported)
				{
					this.Mask.SetActive(true);
					GameGlobals.MasksBanned = true;
					if (this.SecuritySystem.Masked)
					{
						this.Label.text = "In security camera footage, the killer was wearing a mask. As a result, the police are unable to gather meaningful information about the murderer. To prevent this from ever happening again, the Headmaster decides to ban all masks from the school from this day forward.";
					}
					else
					{
						this.Label.text = "Witnesses state that the killer was wearing a mask. As a result, the police are unable to gather meaningful information about the murderer. To prevent this from ever happening again, the Headmaster decides to ban all masks from the school from this day forward.";
					}
					this.Police.MaskReported = false;
					this.Phase++;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 11)
			{
				this.Cops.transform.eulerAngles = new Vector3(0f, 180f, 0f);
				this.Cops.SetActive(true);
				if (this.Arrests == 0)
				{
					if (this.DeadPerps == 0)
					{
						this.Label.text = "The police do not have enough evidence to perform an arrest. The police investigation ends, and students are free to leave.";
					}
					else
					{
						this.Label.text = "The police conclude that a murder-suicide took place, but are unable to take any further action. The police investigation ends, and students are free to leave.";
					}
				}
				else if (this.Arrests == 1)
				{
					this.Label.text = "The police believe that they have arrested the perpetrator of the crime. The police investigation ends, and students are free to leave.";
				}
				else
				{
					this.Label.text = "The police believe that they have arrested the perpetrators of the crimes. The police investigation ends, and students are free to leave.";
				}
				if (this.StudentManager.RivalEliminated)
				{
					this.Phase++;
				}
				else if (DateGlobals.Weekday == DayOfWeek.Friday)
				{
					this.Phase = 21;
				}
				else
				{
					this.Phase += 2;
				}
			}
			else if (this.Phase == 12)
			{
				this.Senpai.enabled = false;
				this.Senpai.transform.parent = base.transform;
				this.Senpai.gameObject.SetActive(true);
				this.Senpai.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.Senpai.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.Senpai.EmptyHands();
				Physics.SyncTransforms();
				string str2 = string.Empty;
				if (this.Yandere.Egg && this.StudentManager.Students[11] != null && !this.StudentManager.Students[11].Alive)
				{
					this.RivalEliminationMethod = RivalEliminationType.Dead;
				}
				if (this.RivalEliminationMethod == RivalEliminationType.Dead)
				{
					this.Senpai.CharacterAnimation.Play("kneelCry_00");
					if (DateGlobals.Weekday != DayOfWeek.Friday)
					{
						str2 = "\nSenpai will stay home from school for one day to mourn Osana's death.";
						GameGlobals.SenpaiMourning = true;
					}
					this.Label.text = "Senpai is absolutely devastated by the death of his childhood friend. His mental stability has been greatly affected." + str2;
				}
				else
				{
					this.Senpai.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					if (this.RivalEliminationMethod == RivalEliminationType.Vanished)
					{
						this.Senpai.CharacterAnimation.Play(this.Senpai.BulliedIdleAnim);
						this.Label.text = "Senpai is concerned about the sudden disappearance of his childhood friend. His mental stability has been slightly affected.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Arrested)
					{
						this.Senpai.CharacterAnimation["refuse_02"].speed = 0.5f;
						this.Senpai.CharacterAnimation.Play("refuse_02");
						this.Label.text = "Senpai is disgusted to learn that his childhood friend would actually commit murder. He is deeply disappointed in her.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Ruined)
					{
						this.Senpai.CharacterAnimation["refuse_02"].speed = 0.5f;
						this.Senpai.CharacterAnimation.Play("refuse_02");
						this.Label.text = "Senpai is disturbed by the rumors circulating about his childhood friend. He is deeply disappointed in her.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Expelled)
					{
						this.Senpai.CharacterAnimation.Play("surprisedPose_00");
						this.Label.text = "Senpai is shocked by the expulsion of his childhood friend. He is deeply disappointed in her.";
					}
					else if (this.RivalEliminationMethod == RivalEliminationType.Rejected)
					{
						this.Senpai.CharacterAnimation.Play(this.Senpai.BulliedIdleAnim);
						this.Label.text = "Senpai feels guilty for turning down Osana's feelings, but also he knows that he cannot take back what has been said.";
					}
				}
				this.Phase++;
			}
			else if (this.Phase == 13)
			{
				this.Senpai.enabled = false;
				this.Senpai.transform.parent = base.transform;
				this.Senpai.gameObject.SetActive(true);
				this.Senpai.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.Senpai.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
				this.Senpai.EmptyHands();
				if (this.StudentManager.RivalEliminated)
				{
					this.Senpai.CharacterAnimation.Play(this.Senpai.BulliedWalkAnim);
				}
				else
				{
					this.Senpai.CharacterAnimation.Play(this.Senpai.WalkAnim);
				}
				this.Yandere.LookAt.enabled = true;
				this.Yandere.MyController.enabled = false;
				this.Yandere.transform.parent = base.transform;
				this.Yandere.transform.localPosition = new Vector3(2.5f, 0f, 2.5f);
				this.Yandere.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
				this.Yandere.CharacterAnimation.Play(this.Yandere.WalkAnim);
				this.Label.text = "Ayano stalks Senpai until he has returned home, and then returns to her own home.";
				if (GameGlobals.SenpaiMourning)
				{
					this.Senpai.gameObject.SetActive(false);
					this.Yandere.LookAt.enabled = false;
					this.Yandere.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.Yandere.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
					this.Label.text = "Ayano returns home, thinking of Senpai every step of the way.";
				}
				Physics.SyncTransforms();
				this.Phase++;
			}
			else if (this.Phase == 14)
			{
				Debug.Log("We're currently in the End-of-Day sequence, checking to see if the Counselor has to lecture anyone.");
				if (!StudentGlobals.GetStudentDying(11) && !StudentGlobals.GetStudentDead(11) && !StudentGlobals.GetStudentArrested(11))
				{
					Debug.Log("Osana is not dying, dead, or arrested.");
					Debug.Log("Counselor.LectureID is: " + this.Counselor.LectureID);
					if (this.Counselor.LectureID > 0)
					{
						this.Yandere.gameObject.SetActive(false);
						for (int l = 1; l < 101; l++)
						{
							this.StudentManager.DisableStudent(l);
						}
						this.EODCamera.position = new Vector3(-18.5f, 1f, 6.5f);
						this.EODCamera.eulerAngles = new Vector3(0f, -45f, 0f);
						this.Counselor.Lecturing = true;
						base.enabled = false;
						Debug.Log("The counselor is going to lecture somebody! Exiting End-of-Day sequence.");
					}
					else
					{
						this.Phase++;
						this.UpdateScene();
					}
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 15)
			{
				Debug.Log("We've moved on, and now we're checking to see if any schemes are failing.");
				this.EODCamera.localPosition = new Vector3(1f, 1.8f, -2.5f);
				this.EODCamera.localEulerAngles = new Vector3(22.5f, -22.5f, 0f);
				this.TextWindow.SetActive(true);
				if (SchemeGlobals.GetSchemeStage(2) == 3)
				{
					if (!StudentGlobals.GetStudentDying(11) && !StudentGlobals.GetStudentDead(11) && !StudentGlobals.GetStudentArrested(11))
					{
						this.GaudyRing.SetActive(true);
						this.Label.text = "Osana discovers Sakyu's ring inside of her book bag. She returns the ring to Sakyu, who decides to stop bringing it to school.";
						SchemeGlobals.SetSchemeStage(2, 100);
					}
					else
					{
						this.GaudyRing.SetActive(true);
						this.Label.text = "Ayano decides to keep Sakyu Basu's ring.";
						SchemeGlobals.SetSchemeStage(2, 100);
					}
				}
				else if (this.RummageSpot.Phase == 2)
				{
					this.AnswerSheet.SetActive(true);
					this.Label.text = "A faculty member discovers that an answer sheet for an upcoming test is missing. She changes all of the questions for the test and keeps the new answer sheet with her at all times.";
					GameGlobals.AnswerSheetUnavailable = true;
					this.RummageSpot.Phase = 0;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 16)
			{
				this.ClubClosed = false;
				this.ClubKicked = false;
				float d = 1.2f;
				if (this.ClubID < this.ClubArray.Length)
				{
					if (!ClubGlobals.GetClubClosed(this.ClubArray[this.ClubID]))
					{
						this.ClubManager.CheckClub(this.ClubArray[this.ClubID]);
						if (this.ClubManager.ClubMembers < 5)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * d, Space.Self);
							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);
							this.Label.text = "The " + this.ClubNames[this.ClubID].ToString() + " no longer has enough members to remain operational. The school forces the club to disband.";
							this.ClubClosed = true;
							if (ClubGlobals.Club == this.ClubArray[this.ClubID])
							{
								ClubGlobals.Club = ClubType.None;
							}
						}
						if (this.ClubManager.LeaderMissing)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * d, Space.Self);
							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);
							this.Label.text = string.Concat(new string[]
							{
								"The leader of the ",
								this.ClubNames[this.ClubID].ToString(),
								" has gone missing. The ",
								this.ClubNames[this.ClubID].ToString(),
								" cannot operate without its leader. The club disbands."
							});
							this.ClubClosed = true;
							if (ClubGlobals.Club == this.ClubArray[this.ClubID])
							{
								ClubGlobals.Club = ClubType.None;
							}
						}
						else if (this.ClubManager.LeaderDead)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * d, Space.Self);
							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);
							this.Label.text = string.Concat(new string[]
							{
								"The leader of the ",
								this.ClubNames[this.ClubID].ToString(),
								" is gone. The ",
								this.ClubNames[this.ClubID].ToString(),
								" cannot operate without its leader. The club disbands."
							});
							this.ClubClosed = true;
							if (ClubGlobals.Club == this.ClubArray[this.ClubID])
							{
								ClubGlobals.Club = ClubType.None;
							}
						}
						else if (this.ClubManager.LeaderAshamed)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * d, Space.Self);
							ClubGlobals.SetClubClosed(this.ClubArray[this.ClubID], true);
							this.Label.text = "The leader of the " + this.ClubNames[this.ClubID].ToString() + " has unexpectedly disbanded the club without explanation.";
							this.ClubClosed = true;
							this.ClubManager.LeaderAshamed = false;
							if (ClubGlobals.Club == this.ClubArray[this.ClubID])
							{
								ClubGlobals.Club = ClubType.None;
							}
						}
					}
					if (!ClubGlobals.GetClubClosed(this.ClubArray[this.ClubID]) && !ClubGlobals.GetClubKicked(this.ClubArray[this.ClubID]) && ClubGlobals.Club == this.ClubArray[this.ClubID])
					{
						this.ClubManager.CheckGrudge(this.ClubArray[this.ClubID]);
						if (this.ClubManager.LeaderGrudge)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * d, Space.Self);
							this.Label.text = string.Concat(new string[]
							{
								"Ayano receives a text message from the president of the ",
								this.ClubNames[this.ClubID].ToString(),
								". Ayano is no longer a member of the ",
								this.ClubNames[this.ClubID].ToString(),
								", and is not welcome in the ",
								this.ClubNames[this.ClubID].ToString(),
								" room."
							});
							ClubGlobals.SetClubKicked(this.ClubArray[this.ClubID], true);
							ClubGlobals.Club = ClubType.None;
							this.ClubKicked = true;
						}
						else if (this.ClubManager.ClubGrudge)
						{
							this.EODCamera.position = this.ClubManager.ClubVantages[this.ClubID].position;
							this.EODCamera.eulerAngles = this.ClubManager.ClubVantages[this.ClubID].eulerAngles;
							this.EODCamera.Translate(Vector3.forward * d, Space.Self);
							this.Label.text = string.Concat(new string[]
							{
								"Ayano receives a text message from the president of the ",
								this.ClubNames[this.ClubID].ToString(),
								". There is someone in the ",
								this.ClubNames[this.ClubID].ToString(),
								" who hates and fears Ayano. Ayano is no longer a member of the ",
								this.ClubNames[this.ClubID].ToString(),
								", and is not welcome in the ",
								this.ClubNames[this.ClubID].ToString(),
								" room."
							});
							ClubGlobals.SetClubKicked(this.ClubArray[this.ClubID], true);
							ClubGlobals.Club = ClubType.None;
							this.ClubKicked = true;
						}
					}
					if (!this.ClubClosed && !this.ClubKicked)
					{
						this.ClubID++;
						this.UpdateScene();
					}
					this.ClubManager.LeaderAshamed = false;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 17)
			{
				if (this.TranqCase.Occupied)
				{
					this.ClosedTranqCase.SetActive(true);
					this.Label.color = new Color(this.Label.color.r, this.Label.color.g, this.Label.color.b, 1f);
					this.Label.text = "Ayano waits until midnight, sneaks into school, and returns to the musical instrument case that contains her unconscious victim. She pushes the case back to her house and ties the victim to a chair in her basement.";
					this.Phase++;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 18)
			{
				if (this.ErectFence)
				{
					this.Fence.SetActive(true);
					this.Label.text = "To prevent any other students from falling off of the school rooftop, the school erects a fence around the roof.";
					SchoolGlobals.RoofFence = true;
					this.ErectFence = false;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 19)
			{
				if (!SchoolGlobals.HighSecurity && this.Police.CouncilDeath)
				{
					this.SCP.SetActive(true);
					this.Label.text = "The student council president has ordered the implementation of heightened security precautions. Security cameras and metal detectors are now present at school.";
					this.Police.CouncilDeath = false;
				}
				else
				{
					this.Phase++;
					this.UpdateScene();
				}
			}
			else if (this.Phase == 20)
			{
				this.Finish();
			}
			else if (this.Phase == 21)
			{
				this.Senpai.enabled = false;
				this.Senpai.Pathfinding.enabled = false;
				this.Senpai.transform.parent = base.transform;
				this.Senpai.gameObject.SetActive(true);
				this.Senpai.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.Senpai.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.Senpai.EmptyHands();
				this.Senpai.MyController.enabled = false;
				this.Senpai.CharacterAnimation.enabled = true;
				this.Senpai.CharacterAnimation.CrossFade(this.Senpai.IdleAnim);
				this.Rival.enabled = false;
				this.Rival.Pathfinding.enabled = false;
				this.Rival.transform.parent = base.transform;
				this.Rival.gameObject.SetActive(true);
				this.Rival.transform.localPosition = new Vector3(0f, 0f, 1f);
				this.Rival.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
				this.Rival.EmptyHands();
				this.Rival.MyController.enabled = false;
				this.Rival.CharacterAnimation.enabled = true;
				this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
				this.Rival.CharacterAnimation["f02_shy_00"].weight = 1f;
				this.Rival.CharacterAnimation.Play("f02_shy_00");
				this.Label.text = "After the police investigation ends, Osana asks Senpai to speak with her under the cherry tree behind the school.";
				this.Phase++;
			}
			else if (this.Phase == 22)
			{
				for (int m = 1; m < 101; m++)
				{
					this.StudentManager.DisableStudent(m);
				}
				this.LoveManager.Suitor = this.Senpai;
				this.LoveManager.Rival = this.Rival;
				this.LoveManager.Rival.CharacterAnimation["f02_shy_00"].weight = 0f;
				this.LoveManager.Suitor.gameObject.SetActive(true);
				this.LoveManager.Rival.gameObject.SetActive(true);
				this.Yandere.gameObject.SetActive(true);
				this.LoveManager.Suitor.transform.parent = null;
				this.LoveManager.Rival.transform.parent = null;
				this.Yandere.gameObject.transform.parent = null;
				this.LoveManager.BeginConfession();
				this.Clock.NightLighting();
				this.Clock.enabled = false;
				base.gameObject.SetActive(false);
			}
			else if (this.Phase == 100)
			{
				this.Yandere.MyController.enabled = false;
				this.Yandere.transform.parent = base.transform;
				this.Yandere.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.Yandere.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.Yandere.CharacterAnimation.Play("f02_handcuffs_00");
				this.Yandere.Handcuffs.SetActive(true);
				this.ArrestingCops.SetActive(true);
				Physics.SyncTransforms();
				this.Label.text = "Ayano is arrested by the police. She will never have Senpai.";
				this.GameOver = true;
			}
			else if (this.Phase == 101)
			{
				int fingerprintID2 = this.WeaponManager.Weapons[this.WeaponID].FingerprintID;
				StudentScript studentScript = this.StudentManager.Students[fingerprintID2];
				if (studentScript.Alive)
				{
					this.Patsy = this.StudentManager.Students[fingerprintID2];
					this.Patsy.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
					this.Patsy.CharacterAnimation.enabled = true;
					if (this.Patsy.WeaponBag != null)
					{
						this.Patsy.WeaponBag.SetActive(false);
					}
					this.Patsy.EmptyHands();
					this.Patsy.SpeechLines.Stop();
					this.Patsy.Handcuffs.SetActive(true);
					this.Patsy.gameObject.SetActive(true);
					this.Patsy.Ragdoll.Zs.SetActive(false);
					this.Patsy.SmartPhone.SetActive(false);
					this.Patsy.MyController.enabled = false;
					this.Patsy.transform.parent = base.transform;
					this.Patsy.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.Patsy.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.Patsy.ShoeRemoval.enabled = false;
					if (this.StudentManager.Students[fingerprintID2].Male)
					{
						this.StudentManager.Students[fingerprintID2].CharacterAnimation.Play("handcuffs_00");
					}
					else
					{
						this.StudentManager.Students[fingerprintID2].CharacterAnimation.Play("f02_handcuffs_00");
					}
					this.ArrestingCops.SetActive(true);
					if (!studentScript.Tranquil)
					{
						this.Label.text = this.JSON.Students[fingerprintID2].Name + " is arrested by the police.";
						StudentGlobals.SetStudentArrested(fingerprintID2, true);
						this.Arrests++;
					}
					else
					{
						this.Label.text = this.JSON.Students[fingerprintID2].Name + " is found asleep inside of a musical instrument case. The police assume that she hid herself inside of the box after committing murder, and arrest her.";
						StudentGlobals.SetStudentArrested(fingerprintID2, true);
						this.ArrestID = fingerprintID2;
						this.TranqCase.Occupied = false;
						this.Arrests++;
					}
				}
				else
				{
					this.ShruggingCops.SetActive(true);
					bool flag = false;
					this.ID = 0;
					while (this.ID < this.VictimArray.Length)
					{
						if (this.VictimArray[this.ID] == fingerprintID2 && !studentScript.MurderSuicide)
						{
							flag = true;
						}
						this.ID++;
					}
					if (!flag)
					{
						this.Label.text = this.JSON.Students[fingerprintID2].Name + " is dead. The police cannot perform an arrest.";
						this.DeadPerps++;
					}
					else
					{
						this.Label.text = this.JSON.Students[fingerprintID2].Name + "'s fingerprints are on the same weapon that killed them. The police cannot solve this mystery.";
					}
				}
				this.Phase = 3;
			}
			else if (this.Phase == 102)
			{
				if (this.Police.SuicideStudent.activeInHierarchy)
				{
					this.MurderScene.SetActive(true);
					this.Label.text = "The police inspect the corpse of a student who appears to have fallen to their death from the school rooftop. The police treat the incident as a murder case, and search the school for any other victims.";
					this.ErectFence = true;
				}
				else
				{
					this.ShruggingCops.SetActive(true);
					this.Label.text = "The police attempt to determine whether or not a student fell to their death from the school rooftop. The police are unable to reach a conclusion.";
				}
				this.ID = 0;
				while (this.ID < this.Police.CorpseList.Length)
				{
					RagdollScript ragdollScript2 = this.Police.CorpseList[this.ID];
					if (ragdollScript2 != null && ragdollScript2.Suicide && this.Police.Corpses > 0)
					{
						this.Police.Corpses--;
					}
					this.ID++;
				}
				this.Phase = 2;
			}
			else if (this.Phase == 103)
			{
				this.MurderScene.SetActive(true);
				this.Label.text = "The paramedics attempt to resuscitate the poisoned student, but they are unable to revive her. The police treat the incident as a murder case, and search the school for any other victims.";
				this.ID = 0;
				while (this.ID < this.Police.CorpseList.Length)
				{
					RagdollScript ragdollScript3 = this.Police.CorpseList[this.ID];
					if (ragdollScript3 != null && ragdollScript3.Poisoned && this.Police.Corpses > 0)
					{
						this.Police.Corpses--;
					}
					this.ID++;
				}
				this.Phase = 2;
			}
			else if (this.Phase == 104)
			{
				this.MurderScene.SetActive(true);
				this.Label.text = "The police determine that " + this.Police.DrownedStudentName + " died from drowning. The police treat the death as a possible murder, and search the school for any other victims.";
				this.ID = 0;
				while (this.ID < this.Police.CorpseList.Length)
				{
					RagdollScript ragdollScript4 = this.Police.CorpseList[this.ID];
					if (ragdollScript4 != null && ragdollScript4.Drowned && this.Police.Corpses > 0)
					{
						this.Police.Corpses--;
					}
					this.ID++;
				}
				this.Phase = 2;
			}
			else if (this.Phase == 105)
			{
				this.MurderScene.SetActive(true);
				this.Label.text = "The police determine that " + this.Police.ElectrocutedStudentName + " died from being electrocuted. The police treat the death as a possible murder, and search the school for any other victims.";
				this.ID = 0;
				while (this.ID < this.Police.CorpseList.Length)
				{
					RagdollScript ragdollScript5 = this.Police.CorpseList[this.ID];
					if (ragdollScript5 != null && ragdollScript5.Electrocuted && this.Police.Corpses > 0)
					{
						this.Police.Corpses--;
					}
					this.ID++;
				}
				this.Phase = 2;
			}
			else if (this.Phase == 999)
			{
				this.ScaredCops.SetActive(true);
				this.Yandere.MyController.enabled = false;
				this.Yandere.transform.parent = base.transform;
				this.Yandere.transform.localPosition = new Vector3(0f, 0f, -1f);
				this.Yandere.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				Physics.SyncTransforms();
				this.Label.text = "The police witness actual evidence of the supernatural, are absolutely horrified, and run for their lives.";
				if (this.StudentManager.RivalEliminated)
				{
					this.Phase = 12;
				}
				else
				{
					this.Phase = 13;
				}
			}
		}
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x000EC0D8 File Offset: 0x000EA4D8
	private void TeleportYandere()
	{
		this.Yandere.MyController.enabled = false;
		this.Yandere.transform.parent = base.transform;
		this.Yandere.transform.localPosition = new Vector3(0.75f, 0.33333f, -1.9f);
		this.Yandere.transform.localEulerAngles = new Vector3(-22.5f, 157.5f, 0f);
		Physics.SyncTransforms();
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x000EC15C File Offset: 0x000EA55C
	private void Finish()
	{
		Debug.Log("We have reached the end of the End-of-Day sequence.");
		if (this.RivalEliminationMethod == RivalEliminationType.Expelled)
		{
			Debug.Log("Osana was expelled.");
			StudentGlobals.SetStudentExpelled(this.StudentManager.RivalID, true);
			GameGlobals.RivalEliminationID = 5;
		}
		PlayerGlobals.Reputation = this.Reputation.Reputation;
		StudentGlobals.MemorialStudents = 0;
		HomeGlobals.Night = true;
		this.Police.KillStudents();
		if (this.Police.Suspended)
		{
			DateGlobals.PassDays = this.Police.SuspensionLength;
		}
		if (this.StudentManager.Students[SchoolGlobals.KidnapVictim] != null && this.StudentManager.Students[SchoolGlobals.KidnapVictim].Ragdoll.enabled)
		{
			SchoolGlobals.KidnapVictim = 0;
		}
		if (!this.TranqCase.Occupied)
		{
			SceneManager.LoadScene("HomeScene");
		}
		else
		{
			SchoolGlobals.KidnapVictim = this.TranqCase.VictimID;
			StudentGlobals.SetStudentKidnapped(this.TranqCase.VictimID, true);
			StudentGlobals.SetStudentSanity(this.TranqCase.VictimID, 100f);
			SceneManager.LoadScene("CalendarScene");
		}
		if (this.Dumpster.StudentToGoMissing > 0)
		{
			this.Dumpster.SetVictimMissing();
		}
		this.ID = 0;
		while (this.ID < this.GardenHoles.Length)
		{
			this.GardenHoles[this.ID].EndOfDayCheck();
			this.ID++;
		}
		this.ID = 1;
		while (this.ID < this.Yandere.Inventory.ShrineCollectibles.Length)
		{
			if (this.Yandere.Inventory.ShrineCollectibles[this.ID])
			{
				PlayerGlobals.SetShrineCollectible(this.ID, true);
			}
			this.ID++;
		}
		this.Incinerator.SetVictimsMissing();
		this.WoodChipper.SetVictimsMissing();
		if (this.FragileTarget > 0)
		{
			Debug.Log("Setting target for Fragile student.");
			StudentGlobals.SetFragileTarget(this.FragileTarget);
			StudentGlobals.SetStudentFragileSlave(5);
		}
		if (this.StudentManager.ReactedToGameLeader)
		{
			SchoolGlobals.ReactedToGameLeader = true;
		}
		if (this.NewFriends > 0)
		{
			PlayerGlobals.Friends += this.NewFriends;
		}
		if (this.Yandere.Alerts > 0)
		{
			PlayerGlobals.Alerts += this.Yandere.Alerts;
		}
		SchoolGlobals.SchoolAtmosphere += (float)this.Arrests * 0.05f;
		if (this.Counselor.ExpelledDelinquents)
		{
			SchoolGlobals.SchoolAtmosphere += 0.25f;
		}
		if (this.Yandere.Inventory.FakeID)
		{
			PlayerGlobals.FakeID = true;
		}
		if (this.RaibaruLoner)
		{
			PlayerGlobals.RaibaruLoner = true;
		}
		if (this.StopMourning)
		{
			GameGlobals.SenpaiMourning = false;
		}
		CollectibleGlobals.MatchmakingGifts = this.MatchmakingGifts;
		CollectibleGlobals.SenpaiGifts = this.SenpaiGifts;
		PlayerGlobals.PantyShots = this.Yandere.Inventory.PantyShots;
		PlayerGlobals.Money = this.Yandere.Inventory.Money;
		this.WeaponManager.TrackDumpedWeapons();
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x000EC49C File Offset: 0x000EA89C
	private void DisableThings(StudentScript TargetStudent)
	{
		if (TargetStudent != null)
		{
			TargetStudent.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
			TargetStudent.CharacterAnimation.enabled = true;
			TargetStudent.CharacterAnimation.Play(TargetStudent.IdleAnim);
			TargetStudent.EmptyHands();
			TargetStudent.SpeechLines.Stop();
			TargetStudent.Ragdoll.Zs.SetActive(false);
			TargetStudent.SmartPhone.SetActive(false);
			TargetStudent.MyController.enabled = false;
			TargetStudent.ShoeRemoval.enabled = false;
			TargetStudent.enabled = false;
			TargetStudent.gameObject.SetActive(true);
			TargetStudent.transform.parent = base.transform;
			TargetStudent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x04001D51 RID: 7505
	public SecuritySystemScript SecuritySystem;

	// Token: 0x04001D52 RID: 7506
	public StudentManagerScript StudentManager;

	// Token: 0x04001D53 RID: 7507
	public WeaponManagerScript WeaponManager;

	// Token: 0x04001D54 RID: 7508
	public ClubManagerScript ClubManager;

	// Token: 0x04001D55 RID: 7509
	public HeartbrokenScript Heartbroken;

	// Token: 0x04001D56 RID: 7510
	public IncineratorScript Incinerator;

	// Token: 0x04001D57 RID: 7511
	public LoveManagerScript LoveManager;

	// Token: 0x04001D58 RID: 7512
	public RummageSpotScript RummageSpot;

	// Token: 0x04001D59 RID: 7513
	public VoidGoddessScript VoidGoddess;

	// Token: 0x04001D5A RID: 7514
	public WoodChipperScript WoodChipper;

	// Token: 0x04001D5B RID: 7515
	public ReputationScript Reputation;

	// Token: 0x04001D5C RID: 7516
	public DumpsterLidScript Dumpster;

	// Token: 0x04001D5D RID: 7517
	public CounselorScript Counselor;

	// Token: 0x04001D5E RID: 7518
	public WeaponScript MurderWeapon;

	// Token: 0x04001D5F RID: 7519
	public TranqCaseScript TranqCase;

	// Token: 0x04001D60 RID: 7520
	public YandereScript Yandere;

	// Token: 0x04001D61 RID: 7521
	public RagdollScript Corpse;

	// Token: 0x04001D62 RID: 7522
	public StudentScript Senpai;

	// Token: 0x04001D63 RID: 7523
	public StudentScript Patsy;

	// Token: 0x04001D64 RID: 7524
	public PoliceScript Police;

	// Token: 0x04001D65 RID: 7525
	public Transform EODCamera;

	// Token: 0x04001D66 RID: 7526
	public StudentScript Rival;

	// Token: 0x04001D67 RID: 7527
	public ClockScript Clock;

	// Token: 0x04001D68 RID: 7528
	public JsonScript JSON;

	// Token: 0x04001D69 RID: 7529
	public GardenHoleScript[] GardenHoles;

	// Token: 0x04001D6A RID: 7530
	public StudentScript[] WitnessList;

	// Token: 0x04001D6B RID: 7531
	public Animation[] CopAnimation;

	// Token: 0x04001D6C RID: 7532
	public GameObject MainCamera;

	// Token: 0x04001D6D RID: 7533
	public UISprite EndOfDayDarkness;

	// Token: 0x04001D6E RID: 7534
	public UILabel Label;

	// Token: 0x04001D6F RID: 7535
	public bool PreviouslyActivated;

	// Token: 0x04001D70 RID: 7536
	public bool PoliceArrived;

	// Token: 0x04001D71 RID: 7537
	public bool RaibaruLoner;

	// Token: 0x04001D72 RID: 7538
	public bool StopMourning;

	// Token: 0x04001D73 RID: 7539
	public bool ClubClosed;

	// Token: 0x04001D74 RID: 7540
	public bool ClubKicked;

	// Token: 0x04001D75 RID: 7541
	public bool ErectFence;

	// Token: 0x04001D76 RID: 7542
	public bool GameOver;

	// Token: 0x04001D77 RID: 7543
	public bool Darken;

	// Token: 0x04001D78 RID: 7544
	public int ClothingWithRedPaint;

	// Token: 0x04001D79 RID: 7545
	public int FragileTarget;

	// Token: 0x04001D7A RID: 7546
	public int EyeWitnesses;

	// Token: 0x04001D7B RID: 7547
	public int NewFriends;

	// Token: 0x04001D7C RID: 7548
	public int DeadPerps;

	// Token: 0x04001D7D RID: 7549
	public int Arrests;

	// Token: 0x04001D7E RID: 7550
	public int Corpses;

	// Token: 0x04001D7F RID: 7551
	public int Victims;

	// Token: 0x04001D80 RID: 7552
	public int Weapons;

	// Token: 0x04001D81 RID: 7553
	public int Phase = 1;

	// Token: 0x04001D82 RID: 7554
	public int MatchmakingGifts;

	// Token: 0x04001D83 RID: 7555
	public int SenpaiGifts;

	// Token: 0x04001D84 RID: 7556
	public int WeaponID;

	// Token: 0x04001D85 RID: 7557
	public int ArrestID;

	// Token: 0x04001D86 RID: 7558
	public int ClubID;

	// Token: 0x04001D87 RID: 7559
	public int ID;

	// Token: 0x04001D88 RID: 7560
	public string[] ClubNames;

	// Token: 0x04001D89 RID: 7561
	public int[] VictimArray;

	// Token: 0x04001D8A RID: 7562
	public ClubType[] ClubArray;

	// Token: 0x04001D8B RID: 7563
	private SaveFile saveFile;

	// Token: 0x04001D8C RID: 7564
	public GameObject TextWindow;

	// Token: 0x04001D8D RID: 7565
	public GameObject Cops;

	// Token: 0x04001D8E RID: 7566
	public GameObject SearchingCop;

	// Token: 0x04001D8F RID: 7567
	public GameObject MurderScene;

	// Token: 0x04001D90 RID: 7568
	public GameObject ShruggingCops;

	// Token: 0x04001D91 RID: 7569
	public GameObject TabletCop;

	// Token: 0x04001D92 RID: 7570
	public GameObject SecuritySystemScene;

	// Token: 0x04001D93 RID: 7571
	public GameObject OpenTranqCase;

	// Token: 0x04001D94 RID: 7572
	public GameObject ClosedTranqCase;

	// Token: 0x04001D95 RID: 7573
	public GameObject GaudyRing;

	// Token: 0x04001D96 RID: 7574
	public GameObject AnswerSheet;

	// Token: 0x04001D97 RID: 7575
	public GameObject Fence;

	// Token: 0x04001D98 RID: 7576
	public GameObject SCP;

	// Token: 0x04001D99 RID: 7577
	public GameObject ArrestingCops;

	// Token: 0x04001D9A RID: 7578
	public GameObject Mask;

	// Token: 0x04001D9B RID: 7579
	public GameObject EyeWitnessScene;

	// Token: 0x04001D9C RID: 7580
	public GameObject ScaredCops;

	// Token: 0x04001D9D RID: 7581
	public StudentScript KidnappedVictim;

	// Token: 0x04001D9E RID: 7582
	public Renderer TabletPortrait;

	// Token: 0x04001D9F RID: 7583
	public Transform CardboardBox;

	// Token: 0x04001DA0 RID: 7584
	public RivalEliminationType RivalEliminationMethod;
}
