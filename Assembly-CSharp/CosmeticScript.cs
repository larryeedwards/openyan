using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000377 RID: 887
public class CosmeticScript : MonoBehaviour
{
	// Token: 0x06001830 RID: 6192 RVA: 0x000CB29C File Offset: 0x000C969C
	public void Start()
	{
		if (this.Kidnapped)
		{
		}
		if (this.RightShoe != null)
		{
			this.RightShoe.SetActive(false);
			this.LeftShoe.SetActive(false);
		}
		this.ColorValue = new Color(1f, 1f, 1f, 1f);
		if (this.JSON == null)
		{
			this.JSON = this.Student.JSON;
		}
		string name = string.Empty;
		if (!this.Initialized)
		{
			this.Accessory = int.Parse(this.JSON.Students[this.StudentID].Accessory);
			this.Hairstyle = int.Parse(this.JSON.Students[this.StudentID].Hairstyle);
			this.Stockings = this.JSON.Students[this.StudentID].Stockings;
			this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
			this.EyeType = this.JSON.Students[this.StudentID].EyeType;
			this.HairColor = this.JSON.Students[this.StudentID].Color;
			this.EyeColor = this.JSON.Students[this.StudentID].Eyes;
			this.Club = this.JSON.Students[this.StudentID].Club;
			this.Name = this.JSON.Students[this.StudentID].Name;
			if (this.Yandere)
			{
				this.Accessory = 0;
				this.Hairstyle = 1;
				this.Stockings = "Black";
				this.BreastSize = 1f;
				this.HairColor = "White";
				this.EyeColor = "Black";
				this.Club = ClubType.None;
			}
			this.OriginalStockings = this.Stockings;
			this.Initialized = true;
		}
		if (this.StudentID == 36)
		{
			if (TaskGlobals.GetTaskStatus(36) < 3)
			{
				this.FacialHairstyle = 12;
				this.EyewearID = 8;
			}
			else
			{
				this.FacialHairstyle = 0;
				this.EyewearID = 9;
				this.Hairstyle = 49;
				this.Accessory = 0;
			}
		}
		if (this.StudentID == 51 && ClubGlobals.GetClubClosed(ClubType.LightMusic))
		{
			this.Hairstyle = 51;
		}
		if (GameGlobals.EmptyDemon && (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 || this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 || this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 || this.StudentID == 66 || this.StudentID == 71))
		{
			if (!this.Male)
			{
				this.Hairstyle = 52;
			}
			else
			{
				this.Hairstyle = 53;
			}
			this.FacialHairstyle = 0;
			this.EyewearID = 0;
			this.Accessory = 0;
			this.Stockings = string.Empty;
			this.BreastSize = 1f;
			this.Empty = true;
		}
		if (this.Name == "Random")
		{
			this.Randomize = true;
			if (!this.Male)
			{
				name = this.StudentManager.FirstNames[UnityEngine.Random.Range(0, this.StudentManager.FirstNames.Length)] + " " + this.StudentManager.LastNames[UnityEngine.Random.Range(0, this.StudentManager.LastNames.Length)];
				this.JSON.Students[this.StudentID].Name = name;
				this.Student.Name = name;
			}
			else
			{
				name = this.StudentManager.MaleNames[UnityEngine.Random.Range(0, this.StudentManager.MaleNames.Length)] + " " + this.StudentManager.LastNames[UnityEngine.Random.Range(0, this.StudentManager.LastNames.Length)];
				this.JSON.Students[this.StudentID].Name = name;
				this.Student.Name = name;
			}
			if (MissionModeGlobals.MissionMode && MissionModeGlobals.MissionTarget == this.StudentID)
			{
				this.JSON.Students[this.StudentID].Name = MissionModeGlobals.MissionTargetName;
				this.Student.Name = MissionModeGlobals.MissionTargetName;
				name = MissionModeGlobals.MissionTargetName;
			}
		}
		if (this.Randomize)
		{
			this.Teacher = false;
			this.BreastSize = UnityEngine.Random.Range(0.5f, 2f);
			this.Accessory = 0;
			this.Club = ClubType.None;
			if (!this.Male)
			{
				this.Hairstyle = 1;
				while (this.Hairstyle == 1 || this.Hairstyle == 20 || this.Hairstyle == 21)
				{
					this.Hairstyle = UnityEngine.Random.Range(1, this.FemaleHair.Length);
				}
			}
			else
			{
				this.SkinColor = UnityEngine.Random.Range(0, this.SkinTextures.Length);
				this.Hairstyle = UnityEngine.Random.Range(1, this.MaleHair.Length);
			}
		}
		if (!this.Male)
		{
			if (this.Hairstyle == 20 || this.Hairstyle == 21)
			{
				if (this.Direction == 1)
				{
					this.Hairstyle = 22;
				}
				else
				{
					this.Hairstyle = 19;
				}
			}
			this.ThickBrows.SetActive(false);
			if (!this.TakingPortrait)
			{
				this.Tongue.SetActive(false);
			}
			foreach (GameObject gameObject in this.PhoneCharms)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			this.RightBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			this.LeftBreast.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			this.RightWristband.SetActive(false);
			this.LeftWristband.SetActive(false);
			if (this.StudentID == 51)
			{
				this.RightTemple.name = "RENAMED";
				this.LeftTemple.name = "RENAMED";
				this.RightTemple.localScale = new Vector3(0f, 1f, 1f);
				this.LeftTemple.localScale = new Vector3(0f, 1f, 1f);
				if (ClubGlobals.GetClubClosed(ClubType.LightMusic))
				{
					this.SadBrows.SetActive(true);
				}
				else
				{
					this.ThickBrows.SetActive(true);
				}
			}
			if (this.Club == ClubType.Bully)
			{
				if (!this.Kidnapped)
				{
					this.Student.SmartPhone.GetComponent<Renderer>().material.mainTexture = this.SmartphoneTextures[this.StudentID];
					this.Student.SmartPhone.transform.localPosition = new Vector3(0.01f, 0.005f, 0.01f);
					this.Student.SmartPhone.transform.localEulerAngles = new Vector3(0f, -160f, 165f);
				}
				this.RightWristband.GetComponent<Renderer>().material.mainTexture = this.WristwearTextures[this.StudentID];
				this.LeftWristband.GetComponent<Renderer>().material.mainTexture = this.WristwearTextures[this.StudentID];
				this.Bookbag.GetComponent<Renderer>().material.mainTexture = this.BookbagTextures[this.StudentID];
				this.HoodieRenderer.material.mainTexture = this.HoodieTextures[this.StudentID];
				if (this.PhoneCharms.Length > 0)
				{
					this.PhoneCharms[this.StudentID].SetActive(true);
				}
				if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
				{
					this.RightWristband.SetActive(true);
					this.LeftWristband.SetActive(true);
				}
				this.Bookbag.SetActive(true);
				this.Hoodie.SetActive(true);
				for (int j = 0; j < 10; j++)
				{
					this.Fingernails[j].material.color = this.BullyColor[this.StudentID];
				}
				this.Student.GymTexture = this.TanGymTexture;
				this.Student.TowelTexture = this.TanTowelTexture;
			}
			else
			{
				for (int k = 0; k < 10; k++)
				{
					this.Fingernails[k].gameObject.SetActive(false);
				}
				if (this.Club == ClubType.Gardening && !this.TakingPortrait && !this.Kidnapped)
				{
					this.CanRenderer.material.mainTexture = this.CanTextures[this.StudentID];
				}
			}
			if (!this.Kidnapped && SceneManager.GetActiveScene().name == "PortraitScene")
			{
				if (this.StudentID == 2)
				{
					this.CharacterAnimation.Play("succubus_a_idle_twins_01");
					base.transform.position = new Vector3(0.094f, 0f, 0f);
					this.LookCamera = true;
					this.CharacterAnimation["f02_smile_00"].layer = 1;
					this.CharacterAnimation.Play("f02_smile_00");
					this.CharacterAnimation["f02_smile_00"].weight = 1f;
				}
				else if (this.StudentID == 3)
				{
					this.CharacterAnimation.Play("succubus_b_idle_twins_01");
					base.transform.position = new Vector3(-0.332f, 0f, 0f);
					this.LookCamera = true;
					this.CharacterAnimation["f02_smile_00"].layer = 1;
					this.CharacterAnimation.Play("f02_smile_00");
					this.CharacterAnimation["f02_smile_00"].weight = 1f;
				}
				else if (this.StudentID == 4)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 5)
				{
					this.CharacterAnimation.Play("f02_shy_00");
					this.CharacterAnimation.Play("f02_shy_00");
					this.CharacterAnimation["f02_shy_00"].time = 1f;
				}
				else if (this.StudentID == 10)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
				}
				else if (this.StudentID == 24)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1f;
				}
				else if (this.StudentID == 25)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 30)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 34)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 35)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 38)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 39)
				{
					this.CharacterAnimation.Play("f02_socialCameraPose_00");
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.05f, base.transform.position.z);
				}
				else if (this.StudentID == 40)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1f;
				}
				else if (this.StudentID == 51)
				{
					this.CharacterAnimation.Play("f02_musicPose_00");
					this.Tongue.SetActive(true);
				}
				else if (this.StudentID == 59)
				{
					this.CharacterAnimation.Play("f02_sleuthPortrait_00");
				}
				else if (this.StudentID == 60)
				{
					this.CharacterAnimation.Play("f02_sleuthPortrait_01");
				}
				else if (this.StudentID == 64)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 65)
				{
					this.CharacterAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
				else if (this.StudentID == 71)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0f;
				}
				else if (this.StudentID == 72)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 0.66666f;
				}
				else if (this.StudentID == 73)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1.33332f;
				}
				else if (this.StudentID == 74)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 1.99998f;
				}
				else if (this.StudentID == 75)
				{
					this.CharacterAnimation.Play("f02_idleGirly_00");
					this.CharacterAnimation["f02_idleGirly_00"].time = 2.66664f;
				}
				else if (this.StudentID == 81)
				{
					this.CharacterAnimation.Play("f02_socialCameraPose_00");
					base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.05f, base.transform.position.z);
				}
				else if (this.StudentID == 82 || this.StudentID == 52)
				{
					this.CharacterAnimation.Play("f02_galPose_01");
				}
				else if (this.StudentID == 83 || this.StudentID == 53)
				{
					this.CharacterAnimation.Play("f02_galPose_02");
				}
				else if (this.StudentID == 84 || this.StudentID == 54)
				{
					this.CharacterAnimation.Play("f02_galPose_03");
				}
				else if (this.StudentID == 85 || this.StudentID == 55)
				{
					this.CharacterAnimation.Play("f02_galPose_04");
				}
				else if (this.Club != ClubType.Council)
				{
					this.CharacterAnimation.Play("f02_idleShort_01");
					base.transform.position = new Vector3(0.015f, 0f, 0f);
					this.LookCamera = true;
				}
			}
		}
		else
		{
			this.ThickBrows.SetActive(false);
			foreach (GameObject gameObject2 in this.GaloAccessories)
			{
				gameObject2.SetActive(false);
			}
			if (this.Club == ClubType.Occult)
			{
				this.CharacterAnimation["sadFace_00"].layer = 1;
				this.CharacterAnimation.Play("sadFace_00");
				this.CharacterAnimation["sadFace_00"].weight = 1f;
			}
			bool flag = false;
			if ((this.StudentID == 28 || flag) && StudentGlobals.CustomSuitor)
			{
				if (StudentGlobals.CustomSuitorHair > 0)
				{
					this.Hairstyle = StudentGlobals.CustomSuitorHair;
					this.HairColor = "Purple";
					this.EyeColor = "Purple";
				}
				if (StudentGlobals.CustomSuitorAccessory > 0)
				{
					this.Accessory = StudentGlobals.CustomSuitorAccessory;
					if (this.Accessory == 1)
					{
						Transform transform = this.MaleAccessories[1].transform;
						transform.localScale = new Vector3(1.02f, transform.localScale.y, 1.062f);
					}
				}
				if (StudentGlobals.CustomSuitorBlonde > 0)
				{
					this.HairColor = "Yellow";
				}
				if (StudentGlobals.CustomSuitorJewelry > 0)
				{
					foreach (GameObject gameObject3 in this.GaloAccessories)
					{
						gameObject3.SetActive(true);
					}
				}
			}
			if (this.StudentID == 36 || this.StudentID == 66)
			{
				this.CharacterAnimation["toughFace_00"].layer = 1;
				this.CharacterAnimation.Play("toughFace_00");
				this.CharacterAnimation["toughFace_00"].weight = 1f;
				if (this.StudentID == 66)
				{
					this.ThickBrows.SetActive(true);
				}
			}
			if (SceneManager.GetActiveScene().name == "PortraitScene")
			{
				if (this.StudentID == 26)
				{
					this.CharacterAnimation.Play("idleHaughty_00");
				}
				else if (this.StudentID == 36)
				{
					this.CharacterAnimation.Play("slouchIdle_00");
				}
				else if (this.StudentID == 56)
				{
					this.CharacterAnimation.Play("idleConfident_00");
				}
				else if (this.StudentID == 57)
				{
					this.CharacterAnimation.Play("sleuthPortrait_00");
				}
				else if (this.StudentID == 58)
				{
					this.CharacterAnimation.Play("sleuthPortrait_01");
				}
				else if (this.StudentID == 61)
				{
					this.CharacterAnimation.Play("scienceMad_00");
					base.transform.position = new Vector3(0f, 0.1f, 0f);
				}
				else if (this.StudentID == 62)
				{
					this.CharacterAnimation.Play("idleFrown_00");
				}
				else if (this.StudentID == 69)
				{
					this.CharacterAnimation.Play("idleFrown_00");
				}
				else if (this.StudentID == 76)
				{
					this.CharacterAnimation.Play("delinquentPoseB");
				}
				else if (this.StudentID == 77)
				{
					this.CharacterAnimation.Play("delinquentPoseA");
				}
				else if (this.StudentID == 78)
				{
					this.CharacterAnimation.Play("delinquentPoseC");
				}
				else if (this.StudentID == 79)
				{
					this.CharacterAnimation.Play("delinquentPoseD");
				}
				else if (this.StudentID == 80)
				{
					this.CharacterAnimation.Play("delinquentPoseE");
				}
			}
		}
		if (this.Club == ClubType.Teacher)
		{
			this.MyRenderer.sharedMesh = this.TeacherMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.GymTeacher)
		{
			if (!StudentGlobals.GetStudentReplaced(this.StudentID))
			{
				this.CharacterAnimation["f02_smile_00"].layer = 1;
				this.CharacterAnimation.Play("f02_smile_00");
				this.CharacterAnimation["f02_smile_00"].weight = 1f;
				this.RightEyeRenderer.gameObject.SetActive(false);
				this.LeftEyeRenderer.gameObject.SetActive(false);
			}
			this.MyRenderer.sharedMesh = this.CoachMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.Nurse)
		{
			this.MyRenderer.sharedMesh = this.NurseMesh;
			this.Teacher = true;
		}
		else if (this.Club == ClubType.Council)
		{
			this.Armband.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(-0.64375f, 0f));
			this.Armband.SetActive(true);
			string str = string.Empty;
			if (this.StudentID == 86)
			{
				str = "Strict";
			}
			if (this.StudentID == 87)
			{
				str = "Casual";
			}
			if (this.StudentID == 88)
			{
				str = "Grace";
			}
			if (this.StudentID == 89)
			{
				str = "Edgy";
			}
			this.CharacterAnimation["f02_faceCouncil" + str + "_00"].layer = 1;
			this.CharacterAnimation.Play("f02_faceCouncil" + str + "_00");
			this.CharacterAnimation["f02_idleCouncil" + str + "_00"].time = 1f;
			this.CharacterAnimation.Play("f02_idleCouncil" + str + "_00");
		}
		if (!ClubGlobals.GetClubClosed(this.Club) && (this.StudentID == 21 || this.StudentID == 26 || this.StudentID == 31 || this.StudentID == 36 || this.StudentID == 41 || this.StudentID == 46 || this.StudentID == 51 || this.StudentID == 56 || this.StudentID == 61 || this.StudentID == 66 || this.StudentID == 71))
		{
			this.Armband.SetActive(true);
			Renderer component = this.Armband.GetComponent<Renderer>();
			if (this.StudentID == 21)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.63f, -0.22f));
			}
			else if (this.StudentID == 26)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, -0.22f));
			}
			else if (this.StudentID == 31)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.01f));
			}
			else if (this.StudentID == 36)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.633333f, -0.44f));
			}
			else if (this.StudentID == 41)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(-0.62f, -0.66666f));
			}
			else if (this.StudentID == 46)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, -0.66666f));
			}
			else if (this.StudentID == 51)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.5566666f));
			}
			else if (this.StudentID == 56)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, 0.5533333f));
			}
			else if (this.StudentID == 61)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
			}
			else if (this.StudentID == 66)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, -0.22f));
			}
			else if (this.StudentID == 71)
			{
				component.material.SetTextureOffset("_MainTex", new Vector2(0.69f, 0.335f));
			}
		}
		foreach (GameObject gameObject4 in this.FemaleAccessories)
		{
			if (gameObject4 != null)
			{
				gameObject4.SetActive(false);
			}
		}
		foreach (GameObject gameObject5 in this.MaleAccessories)
		{
			if (gameObject5 != null)
			{
				gameObject5.SetActive(false);
			}
		}
		foreach (GameObject gameObject6 in this.ClubAccessories)
		{
			if (gameObject6 != null)
			{
				gameObject6.SetActive(false);
			}
		}
		foreach (GameObject gameObject7 in this.TeacherAccessories)
		{
			if (gameObject7 != null)
			{
				gameObject7.SetActive(false);
			}
		}
		foreach (GameObject gameObject8 in this.TeacherHair)
		{
			if (gameObject8 != null)
			{
				gameObject8.SetActive(false);
			}
		}
		foreach (GameObject gameObject9 in this.FemaleHair)
		{
			if (gameObject9 != null)
			{
				gameObject9.SetActive(false);
			}
		}
		foreach (GameObject gameObject10 in this.MaleHair)
		{
			if (gameObject10 != null)
			{
				gameObject10.SetActive(false);
			}
		}
		foreach (GameObject gameObject11 in this.FacialHair)
		{
			if (gameObject11 != null)
			{
				gameObject11.SetActive(false);
			}
		}
		foreach (GameObject gameObject12 in this.Eyewear)
		{
			if (gameObject12 != null)
			{
				gameObject12.SetActive(false);
			}
		}
		foreach (GameObject gameObject13 in this.RightStockings)
		{
			if (gameObject13 != null)
			{
				gameObject13.SetActive(false);
			}
		}
		foreach (GameObject gameObject14 in this.LeftStockings)
		{
			if (gameObject14 != null)
			{
				gameObject14.SetActive(false);
			}
		}
		foreach (GameObject gameObject15 in this.Scanners)
		{
			if (gameObject15 != null)
			{
				gameObject15.SetActive(false);
			}
		}
		foreach (GameObject gameObject16 in this.Flowers)
		{
			if (gameObject16 != null)
			{
				gameObject16.SetActive(false);
			}
		}
		foreach (GameObject gameObject17 in this.Roses)
		{
			if (gameObject17 != null)
			{
				gameObject17.SetActive(false);
			}
		}
		foreach (GameObject gameObject18 in this.Goggles)
		{
			if (gameObject18 != null)
			{
				gameObject18.SetActive(false);
			}
		}
		foreach (GameObject gameObject19 in this.RedCloth)
		{
			if (gameObject19 != null)
			{
				gameObject19.SetActive(false);
			}
		}
		foreach (GameObject gameObject20 in this.Kerchiefs)
		{
			if (gameObject20 != null)
			{
				gameObject20.SetActive(false);
			}
		}
		foreach (GameObject gameObject21 in this.PunkAccessories)
		{
			if (gameObject21 != null)
			{
				gameObject21.SetActive(false);
			}
		}
		foreach (GameObject gameObject22 in this.MusicNotes)
		{
			if (gameObject22 != null)
			{
				gameObject22.SetActive(false);
			}
		}
		bool flag2 = false;
		if ((this.StudentID == 28 || flag2) && StudentGlobals.CustomSuitor && StudentGlobals.CustomSuitorEyewear > 0)
		{
			this.Eyewear[StudentGlobals.CustomSuitorEyewear].SetActive(true);
		}
		if (this.StudentID == 1 && SenpaiGlobals.CustomSenpai)
		{
			if (SenpaiGlobals.SenpaiEyeWear > 0)
			{
				this.Eyewear[SenpaiGlobals.SenpaiEyeWear].SetActive(true);
			}
			this.FacialHairstyle = SenpaiGlobals.SenpaiFacialHair;
			this.HairColor = SenpaiGlobals.SenpaiHairColor;
			this.EyeColor = SenpaiGlobals.SenpaiEyeColor;
			this.Hairstyle = SenpaiGlobals.SenpaiHairStyle;
		}
		if (!this.Male)
		{
			if (!this.Teacher)
			{
				this.FemaleHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.FemaleHairRenderers[this.Hairstyle];
				this.SetFemaleUniform();
			}
			else
			{
				this.TeacherHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.TeacherHairRenderers[this.Hairstyle];
				if (this.Club == ClubType.Teacher)
				{
					this.MyRenderer.materials[1].mainTexture = this.TeacherBodyTexture;
					this.MyRenderer.materials[2].mainTexture = this.DefaultFaceTexture;
					this.MyRenderer.materials[0].mainTexture = this.TeacherBodyTexture;
				}
				else if (this.Club == ClubType.GymTeacher)
				{
					if (StudentGlobals.GetStudentReplaced(this.StudentID))
					{
						this.MyRenderer.materials[2].mainTexture = this.DefaultFaceTexture;
						this.MyRenderer.materials[0].mainTexture = this.CoachPaleBodyTexture;
						this.MyRenderer.materials[1].mainTexture = this.CoachPaleBodyTexture;
					}
					else
					{
						this.MyRenderer.materials[2].mainTexture = this.CoachFaceTexture;
						this.MyRenderer.materials[0].mainTexture = this.CoachBodyTexture;
						this.MyRenderer.materials[1].mainTexture = this.CoachBodyTexture;
					}
				}
				else if (this.Club == ClubType.Nurse)
				{
					this.MyRenderer.materials = this.NurseMaterials;
				}
			}
		}
		else
		{
			if (this.Hairstyle > 0)
			{
				this.MaleHair[this.Hairstyle].SetActive(true);
				this.HairRenderer = this.MaleHairRenderers[this.Hairstyle];
			}
			if (this.FacialHairstyle > 0)
			{
				this.FacialHair[this.FacialHairstyle].SetActive(true);
				this.FacialHairRenderer = this.FacialHairRenderers[this.FacialHairstyle];
			}
			if (this.EyewearID > 0)
			{
				this.Eyewear[this.EyewearID].SetActive(true);
			}
			this.SetMaleUniform();
		}
		if (!this.Male)
		{
			if (!this.Teacher)
			{
				if (this.FemaleAccessories[this.Accessory] != null)
				{
					this.FemaleAccessories[this.Accessory].SetActive(true);
				}
			}
			else if (this.TeacherAccessories[this.Accessory] != null)
			{
				this.TeacherAccessories[this.Accessory].SetActive(true);
			}
		}
		else if (this.MaleAccessories[this.Accessory] != null)
		{
			this.MaleAccessories[this.Accessory].SetActive(true);
		}
		if (!this.Empty)
		{
			if (this.Club < ClubType.Gaming && this.ClubAccessories[(int)this.Club] != null && !ClubGlobals.GetClubClosed(this.Club) && this.StudentID != 26)
			{
				this.ClubAccessories[(int)this.Club].SetActive(true);
			}
			if (this.StudentID == 36)
			{
				this.ClubAccessories[(int)this.Club].SetActive(true);
			}
			if (this.Club == ClubType.Cooking)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Kerchiefs[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Drama)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Roses[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Art)
			{
				this.ClubAccessories[(int)this.Club].GetComponent<MeshFilter>().sharedMesh = this.Berets[this.StudentID];
			}
			else if (this.Club == ClubType.Science)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Scanners[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.LightMusic)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.MusicNotes[this.StudentID - 50];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Sports)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Goggles[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Gardening)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.Flowers[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club))
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
			else if (this.Club == ClubType.Gaming)
			{
				this.ClubAccessories[(int)this.Club].SetActive(false);
				this.ClubAccessories[(int)this.Club] = this.RedCloth[this.StudentID];
				if (!ClubGlobals.GetClubClosed(this.Club) && this.ClubAccessories[(int)this.Club] != null)
				{
					this.ClubAccessories[(int)this.Club].SetActive(true);
				}
			}
		}
		if (this.StudentID == 36 && TaskGlobals.GetTaskStatus(36) == 3)
		{
			this.ClubAccessories[(int)this.Club].SetActive(false);
		}
		if (!this.Male)
		{
			base.StartCoroutine(this.PutOnStockings());
		}
		if (!this.Randomize)
		{
			if (this.EyeColor != string.Empty)
			{
				if (this.EyeColor == "White")
				{
					this.CorrectColor = new Color(1f, 1f, 1f);
				}
				else if (this.EyeColor == "Black")
				{
					this.CorrectColor = new Color(0.5f, 0.5f, 0.5f);
				}
				else if (this.EyeColor == "Red")
				{
					this.CorrectColor = new Color(1f, 0f, 0f);
				}
				else if (this.EyeColor == "Yellow")
				{
					this.CorrectColor = new Color(1f, 1f, 0f);
				}
				else if (this.EyeColor == "Green")
				{
					this.CorrectColor = new Color(0f, 1f, 0f);
				}
				else if (this.EyeColor == "Cyan")
				{
					this.CorrectColor = new Color(0f, 1f, 1f);
				}
				else if (this.EyeColor == "Blue")
				{
					this.CorrectColor = new Color(0f, 0f, 1f);
				}
				else if (this.EyeColor == "Purple")
				{
					this.CorrectColor = new Color(1f, 0f, 1f);
				}
				else if (this.EyeColor == "Orange")
				{
					this.CorrectColor = new Color(1f, 0.5f, 0f);
				}
				else if (this.EyeColor == "Brown")
				{
					this.CorrectColor = new Color(0.5f, 0.25f, 0f);
				}
				else
				{
					this.CorrectColor = new Color(0f, 0f, 0f);
				}
				if (this.StudentID > 90 && this.StudentID < 97)
				{
					this.CorrectColor.r = this.CorrectColor.r * 0.5f;
					this.CorrectColor.g = this.CorrectColor.g * 0.5f;
					this.CorrectColor.b = this.CorrectColor.b * 0.5f;
				}
				if (this.CorrectColor != new Color(0f, 0f, 0f))
				{
					this.RightEyeRenderer.material.color = this.CorrectColor;
					this.LeftEyeRenderer.material.color = this.CorrectColor;
				}
			}
		}
		else
		{
			float r = UnityEngine.Random.Range(0f, 1f);
			float g = UnityEngine.Random.Range(0f, 1f);
			float b = UnityEngine.Random.Range(0f, 1f);
			this.RightEyeRenderer.material.color = new Color(r, g, b);
			this.LeftEyeRenderer.material.color = new Color(r, g, b);
		}
		if (!this.Randomize)
		{
			if (this.HairColor == "White")
			{
				this.ColorValue = new Color(1f, 1f, 1f);
			}
			else if (this.HairColor == "Black")
			{
				this.ColorValue = new Color(0.5f, 0.5f, 0.5f);
			}
			else if (this.HairColor == "Red")
			{
				this.ColorValue = new Color(1f, 0f, 0f);
			}
			else if (this.HairColor == "Yellow")
			{
				this.ColorValue = new Color(1f, 1f, 0f);
			}
			else if (this.HairColor == "Green")
			{
				this.ColorValue = new Color(0f, 1f, 0f);
			}
			else if (this.HairColor == "Cyan")
			{
				this.ColorValue = new Color(0f, 1f, 1f);
			}
			else if (this.HairColor == "Blue")
			{
				this.ColorValue = new Color(0f, 0f, 1f);
			}
			else if (this.HairColor == "Purple")
			{
				this.ColorValue = new Color(1f, 0f, 1f);
			}
			else if (this.HairColor == "Orange")
			{
				this.ColorValue = new Color(1f, 0.5f, 0f);
			}
			else if (this.HairColor == "Brown")
			{
				this.ColorValue = new Color(0.5f, 0.25f, 0f);
			}
			else
			{
				this.ColorValue = new Color(0f, 0f, 0f);
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			if (this.StudentID > 90 && this.StudentID < 97)
			{
				this.ColorValue.r = this.ColorValue.r * 0.5f;
				this.ColorValue.g = this.ColorValue.g * 0.5f;
				this.ColorValue.b = this.ColorValue.b * 0.5f;
			}
			if (this.ColorValue == new Color(0f, 0f, 0f))
			{
				this.RightEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
				this.LeftEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
				this.FaceTexture = this.HairRenderer.material.mainTexture;
				if (this.Empty)
				{
					this.FaceTexture = this.GrayFace;
				}
				this.CustomHair = true;
			}
			if (!this.CustomHair)
			{
				if (this.Hairstyle > 0)
				{
					if (GameGlobals.LoveSick)
					{
						this.HairRenderer.material.color = new Color(0.1f, 0.1f, 0.1f);
						if (this.HairRenderer.materials.Length > 1)
						{
							this.HairRenderer.materials[1].color = new Color(0.1f, 0.1f, 0.1f);
						}
					}
					else
					{
						this.HairRenderer.material.color = this.ColorValue;
					}
				}
			}
			else if (GameGlobals.LoveSick)
			{
				this.HairRenderer.material.color = new Color(0.1f, 0.1f, 0.1f);
				if (this.HairRenderer.materials.Length > 1)
				{
					this.HairRenderer.materials[1].color = new Color(0.1f, 0.1f, 0.1f);
				}
			}
			if (!this.Male)
			{
				if (this.StudentID == 25)
				{
					this.FemaleAccessories[6].GetComponent<Renderer>().material.color = new Color(0f, 1f, 1f);
				}
				else if (this.StudentID == 30)
				{
					this.FemaleAccessories[6].GetComponent<Renderer>().material.color = new Color(1f, 0f, 1f);
				}
			}
		}
		else
		{
			this.HairRenderer.material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
		if (!this.Teacher)
		{
			if (this.CustomHair)
			{
				if (!this.Male)
				{
					this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				}
				else if (StudentGlobals.MaleUniform == 1)
				{
					this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
				}
				else if (StudentGlobals.MaleUniform < 4)
				{
					this.MyRenderer.materials[1].mainTexture = this.FaceTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.FaceTexture;
				}
			}
		}
		else if (this.Teacher && StudentGlobals.GetStudentReplaced(this.StudentID))
		{
			Color studentColor = StudentGlobals.GetStudentColor(this.StudentID);
			Color studentEyeColor = StudentGlobals.GetStudentEyeColor(this.StudentID);
			this.HairRenderer.material.color = studentColor;
			this.RightEyeRenderer.material.color = studentEyeColor;
			this.LeftEyeRenderer.material.color = studentEyeColor;
		}
		if (this.Male)
		{
			if (this.Accessory == 2)
			{
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			if (SceneManager.GetActiveScene().name == "PortraitScene")
			{
				this.Character.transform.localScale = new Vector3(0.93f, 0.93f, 0.93f);
			}
			if (this.FacialHairRenderer != null)
			{
				this.FacialHairRenderer.material.color = this.ColorValue;
				if (this.FacialHairRenderer.materials.Length > 1)
				{
					this.FacialHairRenderer.materials[1].color = this.ColorValue;
				}
			}
		}
		if (this.StudentID == 10)
		{
		}
		if (this.StudentID == 25 || this.StudentID == 30)
		{
			this.FemaleAccessories[6].SetActive(true);
			if ((float)StudentGlobals.GetStudentReputation(this.StudentID) < -33.33333f)
			{
				this.FemaleAccessories[6].SetActive(false);
			}
		}
		if (this.StudentID == 2)
		{
			if (SchemeGlobals.GetSchemeStage(2) == 2 || SchemeGlobals.GetSchemeStage(2) == 100)
			{
				this.FemaleAccessories[3].SetActive(false);
			}
		}
		else if (this.StudentID == 40)
		{
			if (base.transform.position != Vector3.zero)
			{
				this.RightEyeRenderer.material.mainTexture = this.DefaultFaceTexture;
				this.LeftEyeRenderer.material.mainTexture = this.DefaultFaceTexture;
				this.RightEyeRenderer.gameObject.GetComponent<RainbowScript>().enabled = true;
				this.LeftEyeRenderer.gameObject.GetComponent<RainbowScript>().enabled = true;
			}
		}
		else if (this.StudentID == 41)
		{
			this.CharacterAnimation["moodyEyes_00"].layer = 1;
			this.CharacterAnimation.Play("moodyEyes_00");
			this.CharacterAnimation["moodyEyes_00"].weight = 1f;
			this.CharacterAnimation.Play("moodyEyes_00");
		}
		else if (this.StudentID == 51)
		{
			if (!ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.PunkAccessories[1].SetActive(true);
				this.PunkAccessories[2].SetActive(true);
				this.PunkAccessories[3].SetActive(true);
			}
		}
		else if (this.StudentID == 59)
		{
			this.ClubAccessories[7].transform.localPosition = new Vector3(0f, -1.04f, 0.5f);
			this.ClubAccessories[7].transform.localEulerAngles = new Vector3(-22.5f, 0f, 0f);
		}
		else if (this.StudentID == 60)
		{
			this.FemaleAccessories[13].SetActive(true);
		}
		if (this.Student != null && this.Student.AoT)
		{
			this.Student.AttackOnTitan();
		}
		if (this.HomeScene)
		{
			this.Student.CharacterAnimation["idle_00"].time = 9f;
			this.Student.CharacterAnimation["idle_00"].speed = 0f;
		}
		this.TaskCheck();
		this.TurnOnCheck();
		if (!this.Male)
		{
			this.EyeTypeCheck();
		}
		if (this.Kidnapped)
		{
			this.WearIndoorShoes();
		}
		if (!this.Male && (this.Hairstyle == 20 || this.Hairstyle == 21))
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000CE5A0 File Offset: 0x000CC9A0
	public void SetMaleUniform()
	{
		if (this.StudentID == 1)
		{
			this.SkinColor = SenpaiGlobals.SenpaiSkinColor;
			this.FaceTexture = this.FaceTextures[this.SkinColor];
		}
		else
		{
			this.FaceTexture = ((!this.CustomHair) ? this.FaceTextures[this.SkinColor] : this.HairRenderer.material.mainTexture);
			bool flag = false;
			if ((this.StudentID == 28 || flag) && StudentGlobals.CustomSuitor && StudentGlobals.CustomSuitorTan)
			{
				this.SkinColor = 6;
				this.FaceTexture = this.FaceTextures[6];
			}
		}
		this.MyRenderer.sharedMesh = this.MaleUniforms[StudentGlobals.MaleUniform];
		this.SchoolUniform = this.MaleUniforms[StudentGlobals.MaleUniform];
		this.UniformTexture = this.MaleUniformTextures[StudentGlobals.MaleUniform];
		this.CasualTexture = this.MaleCasualTextures[StudentGlobals.MaleUniform];
		this.SocksTexture = this.MaleSocksTextures[StudentGlobals.MaleUniform];
		if (StudentGlobals.MaleUniform == 1)
		{
			this.SkinID = 0;
			this.UniformID = 1;
			this.FaceID = 2;
		}
		else if (StudentGlobals.MaleUniform == 2)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 3)
		{
			this.UniformID = 0;
			this.FaceID = 1;
			this.SkinID = 2;
		}
		else if (StudentGlobals.MaleUniform == 4)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (StudentGlobals.MaleUniform == 5)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		else if (StudentGlobals.MaleUniform == 6)
		{
			this.FaceID = 0;
			this.SkinID = 1;
			this.UniformID = 2;
		}
		if (StudentGlobals.MaleUniform < 2 && this.Club == ClubType.Delinquent)
		{
			this.MyRenderer.sharedMesh = this.DelinquentMesh;
			if (this.StudentID == 76)
			{
				this.UniformTexture = this.EyeTextures[0];
				this.CasualTexture = this.EyeTextures[1];
				this.SocksTexture = this.EyeTextures[2];
			}
			else if (this.StudentID == 77)
			{
				this.UniformTexture = this.CheekTextures[0];
				this.CasualTexture = this.CheekTextures[1];
				this.SocksTexture = this.CheekTextures[2];
			}
			else if (this.StudentID == 78)
			{
				this.UniformTexture = this.ForeheadTextures[0];
				this.CasualTexture = this.ForeheadTextures[1];
				this.SocksTexture = this.ForeheadTextures[2];
			}
			else if (this.StudentID == 79)
			{
				this.UniformTexture = this.MouthTextures[0];
				this.CasualTexture = this.MouthTextures[1];
				this.SocksTexture = this.MouthTextures[2];
			}
			else if (this.StudentID == 80)
			{
				this.UniformTexture = this.NoseTextures[0];
				this.CasualTexture = this.NoseTextures[1];
				this.SocksTexture = this.NoseTextures[2];
			}
		}
		if (this.StudentID == 10)
		{
			this.Student.GymTexture = this.ObstacleGymTexture;
			this.Student.TowelTexture = this.ObstacleTowelTexture;
			this.Student.SwimsuitTexture = this.ObstacleSwimsuitTexture;
		}
		if (this.StudentID == 11)
		{
			this.Student.SwimsuitTexture = this.OsanaSwimsuitTexture;
		}
		if (this.StudentID == 58)
		{
			this.SkinColor = 8;
			this.Student.TowelTexture = this.TanTowelTexture;
			this.Student.SwimsuitTexture = this.TanSwimsuitTexture;
		}
		if (this.Empty)
		{
			this.UniformTexture = this.MaleUniformTextures[7];
			this.CasualTexture = this.MaleCasualTextures[7];
			this.SocksTexture = this.MaleSocksTextures[7];
			this.FaceTexture = this.GrayFace;
			this.SkinColor = 7;
		}
		if (!this.Student.Indoors)
		{
			this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[this.SkinID].mainTexture = this.SkinTextures[this.SkinColor];
			this.MyRenderer.materials[this.UniformID].mainTexture = this.CasualTexture;
		}
		else
		{
			this.MyRenderer.materials[this.FaceID].mainTexture = this.FaceTexture;
			this.MyRenderer.materials[this.SkinID].mainTexture = this.SkinTextures[this.SkinColor];
			this.MyRenderer.materials[this.UniformID].mainTexture = this.UniformTexture;
		}
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000CEA84 File Offset: 0x000CCE84
	public void SetFemaleUniform()
	{
		if (this.Club != ClubType.Council)
		{
			this.MyRenderer.sharedMesh = this.FemaleUniforms[StudentGlobals.FemaleUniform];
			this.SchoolUniform = this.FemaleUniforms[StudentGlobals.FemaleUniform];
			if (this.Club == ClubType.Bully)
			{
				this.UniformTexture = this.GanguroUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.GanguroCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.GanguroSocksTextures[StudentGlobals.FemaleUniform];
			}
			else if (this.StudentID == 10)
			{
				this.UniformTexture = this.ObstacleUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.ObstacleCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.ObstacleSocksTextures[StudentGlobals.FemaleUniform];
			}
			else if (this.StudentID > 11 && this.StudentID < 21)
			{
				this.MysteriousObstacle = true;
				this.UniformTexture = this.BlackBody;
				this.CasualTexture = this.BlackBody;
				this.SocksTexture = this.BlackBody;
				this.HairRenderer.enabled = false;
				this.RightEyeRenderer.enabled = false;
				this.LeftEyeRenderer.enabled = false;
				this.RightIrisLight.SetActive(false);
				this.LeftIrisLight.SetActive(false);
			}
			else
			{
				this.UniformTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
				this.CasualTexture = this.FemaleCasualTextures[StudentGlobals.FemaleUniform];
				this.SocksTexture = this.FemaleSocksTextures[StudentGlobals.FemaleUniform];
			}
		}
		else
		{
			this.RightIrisLight.SetActive(false);
			this.LeftIrisLight.SetActive(false);
			this.MyRenderer.sharedMesh = this.FemaleUniforms[4];
			this.SchoolUniform = this.FemaleUniforms[4];
			this.UniformTexture = this.FemaleUniformTextures[7];
			this.CasualTexture = this.FemaleCasualTextures[7];
			this.SocksTexture = this.FemaleSocksTextures[7];
		}
		if (this.Empty)
		{
			this.UniformTexture = this.FemaleUniformTextures[8];
			this.CasualTexture = this.FemaleCasualTextures[8];
			this.SocksTexture = this.FemaleSocksTextures[8];
		}
		if (!this.Cutscene)
		{
			if (!this.Kidnapped)
			{
				if (!this.Student.Indoors)
				{
					this.MyRenderer.materials[0].mainTexture = this.CasualTexture;
					this.MyRenderer.materials[1].mainTexture = this.CasualTexture;
				}
				else
				{
					this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
					this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
				}
			}
			else
			{
				this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
				this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
			}
		}
		else
		{
			this.UniformTexture = this.FemaleUniformTextures[StudentGlobals.FemaleUniform];
			this.FaceTexture = this.DefaultFaceTexture;
			this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
			this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
		}
		if (this.Club == ClubType.Bully)
		{
		}
		if (this.MysteriousObstacle)
		{
			this.FaceTexture = this.BlackBody;
		}
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		if (!this.TakingPortrait && this.Student != null && this.Student.StudentManager != null && this.Student.StudentManager.Censor)
		{
			this.CensorPanties();
		}
		if (this.MyStockings != null)
		{
			base.StartCoroutine(this.PutOnStockings());
		}
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000CEE6C File Offset: 0x000CD26C
	public void CensorPanties()
	{
		if (!this.Student.ClubAttire && this.Student.Schoolwear == 1)
		{
			this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 1f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 1f);
		}
		else
		{
			this.RemoveCensor();
		}
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000CEEDD File Offset: 0x000CD2DD
	public void RemoveCensor()
	{
		this.MyRenderer.materials[0].SetFloat("_BlendAmount1", 0f);
		this.MyRenderer.materials[1].SetFloat("_BlendAmount1", 0f);
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x000CEF18 File Offset: 0x000CD318
	private void TaskCheck()
	{
		if (this.StudentID == 37)
		{
			if (TaskGlobals.GetTaskStatus(37) < 3)
			{
				if (!this.TakingPortrait)
				{
					this.MaleAccessories[1].SetActive(false);
				}
				else
				{
					this.MaleAccessories[1].SetActive(true);
				}
			}
		}
		else if (this.StudentID == 11 && this.PhoneCharms.Length > 0)
		{
			if (TaskGlobals.GetTaskStatus(11) < 3)
			{
				this.PhoneCharms[11].SetActive(false);
			}
			else
			{
				this.PhoneCharms[11].SetActive(true);
			}
		}
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x000CEFBC File Offset: 0x000CD3BC
	private void TurnOnCheck()
	{
		if (!this.TurnedOn && !this.TakingPortrait && this.Male)
		{
			if (this.HairColor == "Purple")
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if (this.Hairstyle == 30)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if ((this.Accessory > 1 && this.Accessory < 5) || this.Accessory == 13)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if (this.Student.Persona == PersonaType.TeachersPet)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
			else if (this.EyewearID > 0)
			{
				this.LoveManager.Targets[this.LoveManager.TotalTargets] = this.Student.Head;
				this.LoveManager.TotalTargets++;
			}
		}
		this.TurnedOn = true;
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000CF174 File Offset: 0x000CD574
	private void DestroyUnneccessaryObjects()
	{
		foreach (GameObject gameObject in this.FemaleAccessories)
		{
			if (gameObject != null && !gameObject.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		foreach (GameObject gameObject2 in this.MaleAccessories)
		{
			if (gameObject2 != null && !gameObject2.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject2);
			}
		}
		foreach (GameObject gameObject3 in this.ClubAccessories)
		{
			if (gameObject3 != null && !gameObject3.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject3);
			}
		}
		foreach (GameObject gameObject4 in this.TeacherAccessories)
		{
			if (gameObject4 != null && !gameObject4.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject4);
			}
		}
		foreach (GameObject gameObject5 in this.TeacherHair)
		{
			if (gameObject5 != null && !gameObject5.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject5);
			}
		}
		foreach (GameObject gameObject6 in this.FemaleHair)
		{
			if (gameObject6 != null && !gameObject6.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject6);
			}
		}
		foreach (GameObject gameObject7 in this.MaleHair)
		{
			if (gameObject7 != null && !gameObject7.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject7);
			}
		}
		foreach (GameObject gameObject8 in this.FacialHair)
		{
			if (gameObject8 != null && !gameObject8.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject8);
			}
		}
		foreach (GameObject gameObject9 in this.Eyewear)
		{
			if (gameObject9 != null && !gameObject9.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject9);
			}
		}
		foreach (GameObject gameObject10 in this.RightStockings)
		{
			if (gameObject10 != null && !gameObject10.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject10);
			}
		}
		foreach (GameObject gameObject11 in this.LeftStockings)
		{
			if (gameObject11 != null && !gameObject11.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(gameObject11);
			}
		}
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000CF48C File Offset: 0x000CD88C
	public IEnumerator PutOnStockings()
	{
		this.RightStockings[0].SetActive(false);
		this.LeftStockings[0].SetActive(false);
		if (this.Stockings == string.Empty)
		{
			this.MyStockings = null;
		}
		else if (this.Stockings == "Red")
		{
			this.MyStockings = this.RedStockings;
		}
		else if (this.Stockings == "Yellow")
		{
			this.MyStockings = this.YellowStockings;
		}
		else if (this.Stockings == "Green")
		{
			this.MyStockings = this.GreenStockings;
		}
		else if (this.Stockings == "Cyan")
		{
			this.MyStockings = this.CyanStockings;
		}
		else if (this.Stockings == "Blue")
		{
			this.MyStockings = this.BlueStockings;
		}
		else if (this.Stockings == "Purple")
		{
			this.MyStockings = this.PurpleStockings;
		}
		else if (this.Stockings == "ShortGreen")
		{
			this.MyStockings = this.GreenSocks;
		}
		else if (this.Stockings == "ShortBlack")
		{
			this.MyStockings = this.BlackKneeSocks;
		}
		else if (this.Stockings == "Black")
		{
			this.MyStockings = this.BlackStockings;
		}
		else if (this.Stockings == "Osana")
		{
			this.MyStockings = this.OsanaStockings;
		}
		else if (this.Stockings == "Kizana")
		{
			this.MyStockings = this.KizanaStockings;
		}
		else if (this.Stockings == "Council1")
		{
			this.MyStockings = this.TurtleStockings;
		}
		else if (this.Stockings == "Council2")
		{
			this.MyStockings = this.TigerStockings;
		}
		else if (this.Stockings == "Council3")
		{
			this.MyStockings = this.BirdStockings;
		}
		else if (this.Stockings == "Council4")
		{
			this.MyStockings = this.DragonStockings;
		}
		else if (this.Stockings == "Music1")
		{
			if (!ClubGlobals.GetClubClosed(ClubType.LightMusic))
			{
				this.MyStockings = this.MusicStockings[1];
			}
		}
		else if (this.Stockings == "Music2")
		{
			this.MyStockings = this.MusicStockings[2];
		}
		else if (this.Stockings == "Music3")
		{
			this.MyStockings = this.MusicStockings[3];
		}
		else if (this.Stockings == "Music4")
		{
			this.MyStockings = this.MusicStockings[4];
		}
		else if (this.Stockings == "Music5")
		{
			this.MyStockings = this.MusicStockings[5];
		}
		else if (this.Stockings == "Custom1")
		{
			WWW NewCustomStockings = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings1.png");
			yield return NewCustomStockings;
			if (NewCustomStockings.error == null)
			{
				this.CustomStockings[1] = NewCustomStockings.texture;
			}
			this.MyStockings = this.CustomStockings[1];
		}
		else if (this.Stockings == "Custom2")
		{
			WWW NewCustomStockings2 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings2.png");
			yield return NewCustomStockings2;
			if (NewCustomStockings2.error == null)
			{
				this.CustomStockings[2] = NewCustomStockings2.texture;
			}
			this.MyStockings = this.CustomStockings[2];
		}
		else if (this.Stockings == "Custom3")
		{
			WWW NewCustomStockings3 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings3.png");
			yield return NewCustomStockings3;
			if (NewCustomStockings3.error == null)
			{
				this.CustomStockings[3] = NewCustomStockings3.texture;
			}
			this.MyStockings = this.CustomStockings[3];
		}
		else if (this.Stockings == "Custom4")
		{
			WWW NewCustomStockings4 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings4.png");
			yield return NewCustomStockings4;
			if (NewCustomStockings4.error == null)
			{
				this.CustomStockings[4] = NewCustomStockings4.texture;
			}
			this.MyStockings = this.CustomStockings[4];
		}
		else if (this.Stockings == "Custom5")
		{
			WWW NewCustomStockings5 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings5.png");
			yield return NewCustomStockings5;
			if (NewCustomStockings5.error == null)
			{
				this.CustomStockings[5] = NewCustomStockings5.texture;
			}
			this.MyStockings = this.CustomStockings[5];
		}
		else if (this.Stockings == "Custom6")
		{
			WWW NewCustomStockings6 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings6.png");
			yield return NewCustomStockings6;
			if (NewCustomStockings6.error == null)
			{
				this.CustomStockings[6] = NewCustomStockings6.texture;
			}
			this.MyStockings = this.CustomStockings[6];
		}
		else if (this.Stockings == "Custom7")
		{
			WWW NewCustomStockings7 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings7.png");
			yield return NewCustomStockings7;
			if (NewCustomStockings7.error == null)
			{
				this.CustomStockings[7] = NewCustomStockings7.texture;
			}
			this.MyStockings = this.CustomStockings[7];
		}
		else if (this.Stockings == "Custom8")
		{
			WWW NewCustomStockings8 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings8.png");
			yield return NewCustomStockings8;
			if (NewCustomStockings8.error == null)
			{
				this.CustomStockings[8] = NewCustomStockings8.texture;
			}
			this.MyStockings = this.CustomStockings[8];
		}
		else if (this.Stockings == "Custom9")
		{
			WWW NewCustomStockings9 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings9.png");
			yield return NewCustomStockings9;
			if (NewCustomStockings9.error == null)
			{
				this.CustomStockings[9] = NewCustomStockings9.texture;
			}
			this.MyStockings = this.CustomStockings[9];
		}
		else if (this.Stockings == "Custom10")
		{
			WWW NewCustomStockings10 = new WWW("file:///" + Application.streamingAssetsPath + "/CustomStockings10.png");
			yield return NewCustomStockings10;
			if (NewCustomStockings10.error == null)
			{
				this.CustomStockings[10] = NewCustomStockings10.texture;
			}
			this.MyStockings = this.CustomStockings[10];
		}
		else if (this.Stockings == "Loose")
		{
			this.MyStockings = null;
			this.RightStockings[0].SetActive(true);
			this.LeftStockings[0].SetActive(true);
		}
		if (this.MyStockings != null)
		{
			this.MyRenderer.materials[0].SetTexture("_OverlayTex", this.MyStockings);
			this.MyRenderer.materials[1].SetTexture("_OverlayTex", this.MyStockings);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 1f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 1f);
		}
		else
		{
			this.MyRenderer.materials[0].SetTexture("_OverlayTex", null);
			this.MyRenderer.materials[1].SetTexture("_OverlayTex", null);
			this.MyRenderer.materials[0].SetFloat("_BlendAmount", 0f);
			this.MyRenderer.materials[1].SetFloat("_BlendAmount", 0f);
		}
		yield break;
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000CF4A8 File Offset: 0x000CD8A8
	public void WearIndoorShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.CasualTexture;
			this.MyRenderer.materials[1].mainTexture = this.CasualTexture;
		}
		else
		{
			this.MyRenderer.materials[this.UniformID].mainTexture = this.CasualTexture;
		}
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000CF514 File Offset: 0x000CD914
	public void WearOutdoorShoes()
	{
		if (!this.Male)
		{
			this.MyRenderer.materials[0].mainTexture = this.UniformTexture;
			this.MyRenderer.materials[1].mainTexture = this.UniformTexture;
		}
		else
		{
			this.MyRenderer.materials[this.UniformID].mainTexture = this.UniformTexture;
		}
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x000CF580 File Offset: 0x000CD980
	public void EyeTypeCheck()
	{
		int num = 0;
		if (this.EyeType == "Thin")
		{
			this.MyRenderer.SetBlendShapeWeight(8, 100f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Thins++;
			num = this.StudentManager.Thins;
		}
		else if (this.EyeType == "Serious")
		{
			this.MyRenderer.SetBlendShapeWeight(5, 50f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Seriouses++;
			num = this.StudentManager.Seriouses;
		}
		else if (this.EyeType == "Round")
		{
			this.MyRenderer.SetBlendShapeWeight(5, 15f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Rounds++;
			num = this.StudentManager.Rounds;
		}
		else if (this.EyeType == "Sad")
		{
			this.MyRenderer.SetBlendShapeWeight(0, 50f);
			this.MyRenderer.SetBlendShapeWeight(5, 15f);
			this.MyRenderer.SetBlendShapeWeight(6, 50f);
			this.MyRenderer.SetBlendShapeWeight(8, 50f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.StudentManager.Sads++;
			num = this.StudentManager.Sads;
		}
		else if (this.EyeType == "Mean")
		{
			this.MyRenderer.SetBlendShapeWeight(10, 100f);
			this.StudentManager.Means++;
			num = this.StudentManager.Means;
		}
		else if (this.EyeType == "Smug")
		{
			this.MyRenderer.SetBlendShapeWeight(0, 50f);
			this.MyRenderer.SetBlendShapeWeight(5, 25f);
			this.StudentManager.Smugs++;
			num = this.StudentManager.Smugs;
		}
		else if (this.EyeType == "Gentle")
		{
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.MyRenderer.SetBlendShapeWeight(12, 100f);
			this.StudentManager.Gentles++;
			num = this.StudentManager.Gentles;
		}
		else if (this.EyeType == "MO")
		{
			this.MyRenderer.SetBlendShapeWeight(8, 50f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.MyRenderer.SetBlendShapeWeight(12, 100f);
			this.StudentManager.Gentles++;
			num = this.StudentManager.Gentles;
		}
		else if (this.EyeType == "Rival1")
		{
			this.MyRenderer.SetBlendShapeWeight(8, 5f);
			this.MyRenderer.SetBlendShapeWeight(9, 20f);
			this.MyRenderer.SetBlendShapeWeight(10, 50f);
			this.MyRenderer.SetBlendShapeWeight(11, 50f);
			this.MyRenderer.SetBlendShapeWeight(12, 10f);
			this.StudentManager.Rival1s++;
			num = this.StudentManager.Rival1s;
		}
		if (!this.Modified)
		{
			if ((this.EyeType == "Thin" && this.StudentManager.Thins > 1) || (this.EyeType == "Serious" && this.StudentManager.Seriouses > 1) || (this.EyeType == "Round" && this.StudentManager.Rounds > 1) || (this.EyeType == "Sad" && this.StudentManager.Sads > 1) || (this.EyeType == "Mean" && this.StudentManager.Means > 1) || (this.EyeType == "Smug" && this.StudentManager.Smugs > 1) || (this.EyeType == "Gentle" && this.StudentManager.Gentles > 1))
			{
				this.MyRenderer.SetBlendShapeWeight(8, this.MyRenderer.GetBlendShapeWeight(8) + (float)num);
				this.MyRenderer.SetBlendShapeWeight(9, this.MyRenderer.GetBlendShapeWeight(9) + (float)num);
				this.MyRenderer.SetBlendShapeWeight(10, this.MyRenderer.GetBlendShapeWeight(10) + (float)num);
				this.MyRenderer.SetBlendShapeWeight(12, this.MyRenderer.GetBlendShapeWeight(12) + (float)num);
			}
			this.Modified = true;
		}
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000CFAC0 File Offset: 0x000CDEC0
	public void DeactivateBullyAccessories()
	{
		if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
		{
			this.RightWristband.SetActive(false);
			this.LeftWristband.SetActive(false);
		}
		this.Bookbag.SetActive(false);
		this.Hoodie.SetActive(false);
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x000CFB14 File Offset: 0x000CDF14
	public void ActivateBullyAccessories()
	{
		if (StudentGlobals.FemaleUniform < 2 || StudentGlobals.FemaleUniform == 3)
		{
			this.RightWristband.SetActive(true);
			this.LeftWristband.SetActive(true);
		}
		this.Bookbag.SetActive(true);
		this.Hoodie.SetActive(true);
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000CFB68 File Offset: 0x000CDF68
	public void LoadCosmeticSheet(StudentCosmeticSheet mySheet)
	{
		if (this.Male != mySheet.Male)
		{
			return;
		}
		this.Accessory = mySheet.Accessory;
		this.Hairstyle = mySheet.Hairstyle;
		this.Stockings = mySheet.Stockings;
		this.BreastSize = mySheet.BreastSize;
		this.Start();
		this.ColorValue = mySheet.HairColor;
		this.HairRenderer.material.color = this.ColorValue;
		if (mySheet.CustomHair)
		{
			this.RightEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
			this.LeftEyeRenderer.material.mainTexture = this.HairRenderer.material.mainTexture;
			this.FaceTexture = this.HairRenderer.material.mainTexture;
			this.LeftIrisLight.SetActive(false);
			this.RightIrisLight.SetActive(false);
			this.CustomHair = true;
		}
		this.CorrectColor = mySheet.EyeColor;
		this.RightEyeRenderer.material.color = this.CorrectColor;
		this.LeftEyeRenderer.material.color = this.CorrectColor;
		this.Student.Schoolwear = mySheet.Schoolwear;
		this.Student.ChangeSchoolwear();
		if (mySheet.Bloody)
		{
			this.Student.LiquidProjector.material.mainTexture = this.Student.BloodTexture;
			this.Student.LiquidProjector.enabled = true;
		}
		if (!this.Male)
		{
			this.Stockings = mySheet.Stockings;
			base.StartCoroutine(this.Student.Cosmetic.PutOnStockings());
			for (int i = 0; i < this.MyRenderer.sharedMesh.blendShapeCount; i++)
			{
				this.MyRenderer.SetBlendShapeWeight(i, mySheet.Blendshapes[i]);
			}
		}
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x000CFD64 File Offset: 0x000CE164
	public StudentCosmeticSheet CosmeticSheet()
	{
		StudentCosmeticSheet result = default(StudentCosmeticSheet);
		result.Blendshapes = new List<float>();
		result.Male = this.Male;
		result.CustomHair = this.CustomHair;
		result.Accessory = this.Accessory;
		result.Hairstyle = this.Hairstyle;
		result.Stockings = this.Stockings;
		result.BreastSize = this.BreastSize;
		result.CustomHair = this.CustomHair;
		result.Schoolwear = this.Student.Schoolwear;
		result.Bloody = (this.Student.LiquidProjector.enabled && this.Student.LiquidProjector.material.mainTexture == this.Student.BloodTexture);
		result.HairColor = this.HairRenderer.material.color;
		result.EyeColor = this.RightEyeRenderer.material.color;
		if (!this.Male)
		{
			for (int i = 0; i < this.MyRenderer.sharedMesh.blendShapeCount; i++)
			{
				result.Blendshapes.Add(this.MyRenderer.GetBlendShapeWeight(i));
			}
		}
		return result;
	}

	// Token: 0x0400195C RID: 6492
	public StudentManagerScript StudentManager;

	// Token: 0x0400195D RID: 6493
	public TextureManagerScript TextureManager;

	// Token: 0x0400195E RID: 6494
	public SkinnedMeshUpdater SkinUpdater;

	// Token: 0x0400195F RID: 6495
	public LoveManagerScript LoveManager;

	// Token: 0x04001960 RID: 6496
	public Animation CharacterAnimation;

	// Token: 0x04001961 RID: 6497
	public ModelSwapScript ModelSwap;

	// Token: 0x04001962 RID: 6498
	public StudentScript Student;

	// Token: 0x04001963 RID: 6499
	public JsonScript JSON;

	// Token: 0x04001964 RID: 6500
	public GameObject[] TeacherAccessories;

	// Token: 0x04001965 RID: 6501
	public GameObject[] FemaleAccessories;

	// Token: 0x04001966 RID: 6502
	public GameObject[] MaleAccessories;

	// Token: 0x04001967 RID: 6503
	public GameObject[] ClubAccessories;

	// Token: 0x04001968 RID: 6504
	public GameObject[] PunkAccessories;

	// Token: 0x04001969 RID: 6505
	public GameObject[] RightStockings;

	// Token: 0x0400196A RID: 6506
	public GameObject[] LeftStockings;

	// Token: 0x0400196B RID: 6507
	public GameObject[] PhoneCharms;

	// Token: 0x0400196C RID: 6508
	public GameObject[] TeacherHair;

	// Token: 0x0400196D RID: 6509
	public GameObject[] FacialHair;

	// Token: 0x0400196E RID: 6510
	public GameObject[] FemaleHair;

	// Token: 0x0400196F RID: 6511
	public GameObject[] MusicNotes;

	// Token: 0x04001970 RID: 6512
	public GameObject[] Kerchiefs;

	// Token: 0x04001971 RID: 6513
	public GameObject[] MaleHair;

	// Token: 0x04001972 RID: 6514
	public GameObject[] RedCloth;

	// Token: 0x04001973 RID: 6515
	public GameObject[] Scanners;

	// Token: 0x04001974 RID: 6516
	public GameObject[] Eyewear;

	// Token: 0x04001975 RID: 6517
	public GameObject[] Goggles;

	// Token: 0x04001976 RID: 6518
	public GameObject[] Flowers;

	// Token: 0x04001977 RID: 6519
	public GameObject[] Roses;

	// Token: 0x04001978 RID: 6520
	public Renderer[] TeacherHairRenderers;

	// Token: 0x04001979 RID: 6521
	public Renderer[] FacialHairRenderers;

	// Token: 0x0400197A RID: 6522
	public Renderer[] FemaleHairRenderers;

	// Token: 0x0400197B RID: 6523
	public Renderer[] MaleHairRenderers;

	// Token: 0x0400197C RID: 6524
	public Renderer[] Fingernails;

	// Token: 0x0400197D RID: 6525
	public Texture[] GanguroSwimsuitTextures;

	// Token: 0x0400197E RID: 6526
	public Texture[] GanguroUniformTextures;

	// Token: 0x0400197F RID: 6527
	public Texture[] GanguroCasualTextures;

	// Token: 0x04001980 RID: 6528
	public Texture[] GanguroSocksTextures;

	// Token: 0x04001981 RID: 6529
	public Texture[] ObstacleUniformTextures;

	// Token: 0x04001982 RID: 6530
	public Texture[] ObstacleCasualTextures;

	// Token: 0x04001983 RID: 6531
	public Texture[] ObstacleSocksTextures;

	// Token: 0x04001984 RID: 6532
	public Texture[] OccultUniformTextures;

	// Token: 0x04001985 RID: 6533
	public Texture[] OccultCasualTextures;

	// Token: 0x04001986 RID: 6534
	public Texture[] OccultSocksTextures;

	// Token: 0x04001987 RID: 6535
	public Texture[] FemaleUniformTextures;

	// Token: 0x04001988 RID: 6536
	public Texture[] FemaleCasualTextures;

	// Token: 0x04001989 RID: 6537
	public Texture[] FemaleSocksTextures;

	// Token: 0x0400198A RID: 6538
	public Texture[] MaleUniformTextures;

	// Token: 0x0400198B RID: 6539
	public Texture[] MaleCasualTextures;

	// Token: 0x0400198C RID: 6540
	public Texture[] MaleSocksTextures;

	// Token: 0x0400198D RID: 6541
	public Texture[] SmartphoneTextures;

	// Token: 0x0400198E RID: 6542
	public Texture[] HoodieTextures;

	// Token: 0x0400198F RID: 6543
	public Texture[] FaceTextures;

	// Token: 0x04001990 RID: 6544
	public Texture[] SkinTextures;

	// Token: 0x04001991 RID: 6545
	public Texture[] WristwearTextures;

	// Token: 0x04001992 RID: 6546
	public Texture[] CardiganTextures;

	// Token: 0x04001993 RID: 6547
	public Texture[] BookbagTextures;

	// Token: 0x04001994 RID: 6548
	public Texture[] EyeTextures;

	// Token: 0x04001995 RID: 6549
	public Texture[] CheekTextures;

	// Token: 0x04001996 RID: 6550
	public Texture[] ForeheadTextures;

	// Token: 0x04001997 RID: 6551
	public Texture[] MouthTextures;

	// Token: 0x04001998 RID: 6552
	public Texture[] NoseTextures;

	// Token: 0x04001999 RID: 6553
	public Texture[] ApronTextures;

	// Token: 0x0400199A RID: 6554
	public Texture[] CanTextures;

	// Token: 0x0400199B RID: 6555
	public Texture[] Trunks;

	// Token: 0x0400199C RID: 6556
	public Texture[] MusicStockings;

	// Token: 0x0400199D RID: 6557
	public Mesh[] FemaleUniforms;

	// Token: 0x0400199E RID: 6558
	public Mesh[] MaleUniforms;

	// Token: 0x0400199F RID: 6559
	public Mesh[] Berets;

	// Token: 0x040019A0 RID: 6560
	public Color[] BullyColor;

	// Token: 0x040019A1 RID: 6561
	public SkinnedMeshRenderer CardiganRenderer;

	// Token: 0x040019A2 RID: 6562
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040019A3 RID: 6563
	public Renderer FacialHairRenderer;

	// Token: 0x040019A4 RID: 6564
	public Renderer RightEyeRenderer;

	// Token: 0x040019A5 RID: 6565
	public Renderer LeftEyeRenderer;

	// Token: 0x040019A6 RID: 6566
	public Renderer HoodieRenderer;

	// Token: 0x040019A7 RID: 6567
	public Renderer ScarfRenderer;

	// Token: 0x040019A8 RID: 6568
	public Renderer HairRenderer;

	// Token: 0x040019A9 RID: 6569
	public Renderer CanRenderer;

	// Token: 0x040019AA RID: 6570
	public Mesh DelinquentMesh;

	// Token: 0x040019AB RID: 6571
	public Mesh SchoolUniform;

	// Token: 0x040019AC RID: 6572
	public Texture DefaultFaceTexture;

	// Token: 0x040019AD RID: 6573
	public Texture TeacherBodyTexture;

	// Token: 0x040019AE RID: 6574
	public Texture CoachPaleBodyTexture;

	// Token: 0x040019AF RID: 6575
	public Texture CoachBodyTexture;

	// Token: 0x040019B0 RID: 6576
	public Texture CoachFaceTexture;

	// Token: 0x040019B1 RID: 6577
	public Texture UniformTexture;

	// Token: 0x040019B2 RID: 6578
	public Texture CasualTexture;

	// Token: 0x040019B3 RID: 6579
	public Texture SocksTexture;

	// Token: 0x040019B4 RID: 6580
	public Texture FaceTexture;

	// Token: 0x040019B5 RID: 6581
	public Texture PurpleStockings;

	// Token: 0x040019B6 RID: 6582
	public Texture YellowStockings;

	// Token: 0x040019B7 RID: 6583
	public Texture BlackStockings;

	// Token: 0x040019B8 RID: 6584
	public Texture GreenStockings;

	// Token: 0x040019B9 RID: 6585
	public Texture BlueStockings;

	// Token: 0x040019BA RID: 6586
	public Texture CyanStockings;

	// Token: 0x040019BB RID: 6587
	public Texture RedStockings;

	// Token: 0x040019BC RID: 6588
	public Texture BlackKneeSocks;

	// Token: 0x040019BD RID: 6589
	public Texture GreenSocks;

	// Token: 0x040019BE RID: 6590
	public Texture KizanaStockings;

	// Token: 0x040019BF RID: 6591
	public Texture OsanaStockings;

	// Token: 0x040019C0 RID: 6592
	public Texture TurtleStockings;

	// Token: 0x040019C1 RID: 6593
	public Texture TigerStockings;

	// Token: 0x040019C2 RID: 6594
	public Texture BirdStockings;

	// Token: 0x040019C3 RID: 6595
	public Texture DragonStockings;

	// Token: 0x040019C4 RID: 6596
	public Texture[] CustomStockings;

	// Token: 0x040019C5 RID: 6597
	public Texture MyStockings;

	// Token: 0x040019C6 RID: 6598
	public Texture BlackBody;

	// Token: 0x040019C7 RID: 6599
	public Texture BlackFace;

	// Token: 0x040019C8 RID: 6600
	public Texture GrayFace;

	// Token: 0x040019C9 RID: 6601
	public Texture DelinquentUniformTexture;

	// Token: 0x040019CA RID: 6602
	public Texture DelinquentCasualTexture;

	// Token: 0x040019CB RID: 6603
	public Texture DelinquentSocksTexture;

	// Token: 0x040019CC RID: 6604
	public Texture OsanaSwimsuitTexture;

	// Token: 0x040019CD RID: 6605
	public Texture ObstacleSwimsuitTexture;

	// Token: 0x040019CE RID: 6606
	public Texture ObstacleTowelTexture;

	// Token: 0x040019CF RID: 6607
	public Texture ObstacleGymTexture;

	// Token: 0x040019D0 RID: 6608
	public Texture TanSwimsuitTexture;

	// Token: 0x040019D1 RID: 6609
	public Texture TanTowelTexture;

	// Token: 0x040019D2 RID: 6610
	public Texture TanGymTexture;

	// Token: 0x040019D3 RID: 6611
	public GameObject RightIrisLight;

	// Token: 0x040019D4 RID: 6612
	public GameObject LeftIrisLight;

	// Token: 0x040019D5 RID: 6613
	public GameObject RightWristband;

	// Token: 0x040019D6 RID: 6614
	public GameObject LeftWristband;

	// Token: 0x040019D7 RID: 6615
	public GameObject Cardigan;

	// Token: 0x040019D8 RID: 6616
	public GameObject Bookbag;

	// Token: 0x040019D9 RID: 6617
	public GameObject ThickBrows;

	// Token: 0x040019DA RID: 6618
	public GameObject Character;

	// Token: 0x040019DB RID: 6619
	public GameObject RightShoe;

	// Token: 0x040019DC RID: 6620
	public GameObject LeftShoe;

	// Token: 0x040019DD RID: 6621
	public GameObject SadBrows;

	// Token: 0x040019DE RID: 6622
	public GameObject Armband;

	// Token: 0x040019DF RID: 6623
	public GameObject Hoodie;

	// Token: 0x040019E0 RID: 6624
	public GameObject Tongue;

	// Token: 0x040019E1 RID: 6625
	public Transform RightBreast;

	// Token: 0x040019E2 RID: 6626
	public Transform LeftBreast;

	// Token: 0x040019E3 RID: 6627
	public Transform RightTemple;

	// Token: 0x040019E4 RID: 6628
	public Transform LeftTemple;

	// Token: 0x040019E5 RID: 6629
	public Transform Head;

	// Token: 0x040019E6 RID: 6630
	public Transform Neck;

	// Token: 0x040019E7 RID: 6631
	public Color CorrectColor;

	// Token: 0x040019E8 RID: 6632
	public Color ColorValue;

	// Token: 0x040019E9 RID: 6633
	public Mesh TeacherMesh;

	// Token: 0x040019EA RID: 6634
	public Mesh CoachMesh;

	// Token: 0x040019EB RID: 6635
	public Mesh NurseMesh;

	// Token: 0x040019EC RID: 6636
	public bool MysteriousObstacle;

	// Token: 0x040019ED RID: 6637
	public bool TakingPortrait;

	// Token: 0x040019EE RID: 6638
	public bool Initialized;

	// Token: 0x040019EF RID: 6639
	public bool CustomEyes;

	// Token: 0x040019F0 RID: 6640
	public bool CustomHair;

	// Token: 0x040019F1 RID: 6641
	public bool LookCamera;

	// Token: 0x040019F2 RID: 6642
	public bool HomeScene;

	// Token: 0x040019F3 RID: 6643
	public bool Kidnapped;

	// Token: 0x040019F4 RID: 6644
	public bool Randomize;

	// Token: 0x040019F5 RID: 6645
	public bool Cutscene;

	// Token: 0x040019F6 RID: 6646
	public bool Modified;

	// Token: 0x040019F7 RID: 6647
	public bool TurnedOn;

	// Token: 0x040019F8 RID: 6648
	public bool Teacher;

	// Token: 0x040019F9 RID: 6649
	public bool Yandere;

	// Token: 0x040019FA RID: 6650
	public bool Empty;

	// Token: 0x040019FB RID: 6651
	public bool Male;

	// Token: 0x040019FC RID: 6652
	public float BreastSize;

	// Token: 0x040019FD RID: 6653
	public string OriginalStockings = string.Empty;

	// Token: 0x040019FE RID: 6654
	public string HairColor = string.Empty;

	// Token: 0x040019FF RID: 6655
	public string Stockings = string.Empty;

	// Token: 0x04001A00 RID: 6656
	public string EyeColor = string.Empty;

	// Token: 0x04001A01 RID: 6657
	public string EyeType = string.Empty;

	// Token: 0x04001A02 RID: 6658
	public string Name = string.Empty;

	// Token: 0x04001A03 RID: 6659
	public int FacialHairstyle;

	// Token: 0x04001A04 RID: 6660
	public int Accessory;

	// Token: 0x04001A05 RID: 6661
	public int Direction;

	// Token: 0x04001A06 RID: 6662
	public int Hairstyle;

	// Token: 0x04001A07 RID: 6663
	public int SkinColor;

	// Token: 0x04001A08 RID: 6664
	public int StudentID;

	// Token: 0x04001A09 RID: 6665
	public int EyewearID;

	// Token: 0x04001A0A RID: 6666
	public ClubType Club;

	// Token: 0x04001A0B RID: 6667
	public int ID;

	// Token: 0x04001A0C RID: 6668
	public GameObject[] GaloAccessories;

	// Token: 0x04001A0D RID: 6669
	public Material[] NurseMaterials;

	// Token: 0x04001A0E RID: 6670
	public GameObject CardiganPrefab;

	// Token: 0x04001A0F RID: 6671
	public int FaceID;

	// Token: 0x04001A10 RID: 6672
	public int SkinID;

	// Token: 0x04001A11 RID: 6673
	public int UniformID;
}
