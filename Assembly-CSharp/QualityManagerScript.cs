using System;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

// Token: 0x020004B2 RID: 1202
public class QualityManagerScript : MonoBehaviour
{
	// Token: 0x06001EE5 RID: 7909 RVA: 0x00138528 File Offset: 0x00136928
	public void Start()
	{
		if (SceneManager.GetActiveScene().name != "SchoolScene")
		{
			this.DoNothing = true;
		}
		if (!this.DoNothing)
		{
			DepthOfField34[] components = Camera.main.GetComponents<DepthOfField34>();
			this.ExperimentalDepthOfField34 = components[1];
			this.ToggleExperiment();
			if (OptionGlobals.ParticleCount == 0)
			{
				OptionGlobals.ParticleCount = 3;
			}
			if (OptionGlobals.DrawDistance == 0)
			{
				OptionGlobals.DrawDistanceLimit = 350;
				OptionGlobals.DrawDistance = 350;
			}
			if (OptionGlobals.DisableFarAnimations == 0)
			{
				OptionGlobals.DisableFarAnimations = 5;
			}
			if (OptionGlobals.Sensitivity == 0)
			{
				OptionGlobals.Sensitivity = 3;
			}
			this.ToggleRun();
			this.UpdateFog();
			this.UpdateAnims();
			this.UpdateBloom();
			this.UpdateShadows();
			this.UpdateFPSIndex();
			this.UpdateParticles();
			this.UpdateObscurance();
			this.UpdatePostAliasing();
			this.UpdateDrawDistance();
			this.UpdateLowDetailStudents();
			this.Settings.ToggleBackground();
			if (!OptionGlobals.DepthOfField)
			{
				this.ToggleExperiment();
			}
		}
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x00138628 File Offset: 0x00136A28
	public void UpdateParticles()
	{
		if (OptionGlobals.ParticleCount > 3)
		{
			OptionGlobals.ParticleCount = 1;
		}
		else if (OptionGlobals.ParticleCount < 1)
		{
			OptionGlobals.ParticleCount = 3;
		}
		if (!this.DoNothing)
		{
			ParticleSystem.EmissionModule emission = this.EastRomanceBlossoms.emission;
			ParticleSystem.EmissionModule emission2 = this.WestRomanceBlossoms.emission;
			ParticleSystem.EmissionModule emission3 = this.CorridorBlossoms.emission;
			ParticleSystem.EmissionModule emission4 = this.PlazaBlossoms.emission;
			ParticleSystem.EmissionModule emission5 = this.MythBlossoms.emission;
			ParticleSystem.EmissionModule emission6 = this.Steam[1].emission;
			ParticleSystem.EmissionModule emission7 = this.Steam[2].emission;
			ParticleSystem.EmissionModule emission8 = this.Fountains[1].emission;
			ParticleSystem.EmissionModule emission9 = this.Fountains[2].emission;
			ParticleSystem.EmissionModule emission10 = this.Fountains[3].emission;
			emission.enabled = true;
			emission2.enabled = true;
			emission3.enabled = true;
			emission4.enabled = true;
			emission5.enabled = true;
			emission6.enabled = true;
			emission7.enabled = true;
			emission8.enabled = true;
			emission9.enabled = true;
			emission10.enabled = true;
			if (OptionGlobals.ParticleCount == 3)
			{
				emission.rateOverTime = 100f;
				emission2.rateOverTime = 100f;
				emission3.rateOverTime = 1000f;
				emission4.rateOverTime = 400f;
				emission5.rateOverTime = 100f;
				emission6.rateOverTime = 100f;
				emission7.rateOverTime = 100f;
				emission8.rateOverTime = 100f;
				emission9.rateOverTime = 100f;
				emission10.rateOverTime = 100f;
			}
			else if (OptionGlobals.ParticleCount == 2)
			{
				emission.rateOverTime = 10f;
				emission2.rateOverTime = 10f;
				emission3.rateOverTime = 100f;
				emission4.rateOverTime = 40f;
				emission5.rateOverTime = 10f;
				emission6.rateOverTime = 10f;
				emission7.rateOverTime = 10f;
				emission8.rateOverTime = 10f;
				emission9.rateOverTime = 10f;
				emission10.rateOverTime = 10f;
			}
			else if (OptionGlobals.ParticleCount == 1)
			{
				emission.enabled = false;
				emission2.enabled = false;
				emission3.enabled = false;
				emission4.enabled = false;
				emission5.enabled = false;
				emission6.enabled = false;
				emission7.enabled = false;
				emission8.enabled = false;
				emission9.enabled = false;
				emission10.enabled = false;
			}
		}
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x00138910 File Offset: 0x00136D10
	public void UpdateOutlines()
	{
		if (!this.DoNothing)
		{
			for (int i = 1; i < this.StudentManager.Students.Length; i++)
			{
				StudentScript studentScript = this.StudentManager.Students[i];
				if (studentScript != null && studentScript.gameObject.activeInHierarchy)
				{
					if (OptionGlobals.DisableOutlines)
					{
						this.NewHairShader = this.Toon;
						this.NewBodyShader = this.ToonOverlay;
					}
					else
					{
						this.NewHairShader = this.ToonOutline;
						this.NewBodyShader = this.ToonOutlineOverlay;
					}
					if (!studentScript.Male)
					{
						studentScript.MyRenderer.materials[0].shader = this.NewBodyShader;
						studentScript.MyRenderer.materials[1].shader = this.NewBodyShader;
						studentScript.MyRenderer.materials[2].shader = this.NewBodyShader;
						studentScript.Cosmetic.RightStockings[0].GetComponent<Renderer>().material.shader = this.NewBodyShader;
						studentScript.Cosmetic.LeftStockings[0].GetComponent<Renderer>().material.shader = this.NewBodyShader;
						if (studentScript.Club == ClubType.Bully)
						{
							studentScript.Cosmetic.Bookbag.GetComponent<Renderer>().material.shader = this.NewHairShader;
							studentScript.Cosmetic.LeftWristband.GetComponent<Renderer>().material.shader = this.NewHairShader;
							studentScript.Cosmetic.RightWristband.GetComponent<Renderer>().material.shader = this.NewHairShader;
							studentScript.Cosmetic.HoodieRenderer.GetComponent<Renderer>().material.shader = this.NewHairShader;
						}
						if (studentScript.StudentID == 87)
						{
							studentScript.Cosmetic.ScarfRenderer.material.shader = this.NewHairShader;
						}
						else if (studentScript.StudentID == 90)
						{
							if (studentScript.Cosmetic.TeacherAccessories[studentScript.Cosmetic.Accessory] != null)
							{
								studentScript.Cosmetic.TeacherAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>().material.shader = this.NewHairShader;
							}
							if (studentScript.MyRenderer.materials.Length == 4)
							{
								studentScript.MyRenderer.materials[3].shader = this.NewBodyShader;
							}
						}
					}
					else
					{
						studentScript.MyRenderer.materials[0].shader = this.NewHairShader;
						studentScript.MyRenderer.materials[1].shader = this.NewHairShader;
						studentScript.MyRenderer.materials[2].shader = this.NewBodyShader;
					}
					studentScript.Armband.GetComponent<Renderer>().material.shader = this.NewHairShader;
					if (!studentScript.Male)
					{
						if (!studentScript.Teacher)
						{
							if (studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle] != null)
							{
								if (studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials.Length == 1)
								{
									studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].material.shader = this.NewHairShader;
								}
								else
								{
									studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[0].shader = this.NewHairShader;
									studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[1].shader = this.NewHairShader;
								}
							}
							if (studentScript.Cosmetic.Accessory > 0 && studentScript.Cosmetic.FemaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>() != null)
							{
								studentScript.Cosmetic.FemaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>().material.shader = this.NewHairShader;
							}
						}
						else
						{
							if (studentScript.Cosmetic.TeacherHairRenderers[studentScript.Cosmetic.Hairstyle] != null)
							{
								studentScript.Cosmetic.TeacherHairRenderers[studentScript.Cosmetic.Hairstyle].material.shader = this.NewHairShader;
							}
							if (studentScript.Cosmetic.Accessory > 0)
							{
							}
						}
					}
					else
					{
						if (studentScript.Cosmetic.Hairstyle > 0)
						{
							if (studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials.Length == 1)
							{
								studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].material.shader = this.NewHairShader;
							}
							else
							{
								studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[0].shader = this.NewHairShader;
								studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[1].shader = this.NewHairShader;
							}
						}
						if (studentScript.Cosmetic.Accessory > 0)
						{
							Renderer component = studentScript.Cosmetic.MaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>();
							if (component != null)
							{
								component.material.shader = this.NewHairShader;
							}
						}
					}
					if (!studentScript.Teacher && studentScript.Cosmetic.Club > ClubType.None && studentScript.Cosmetic.Club != ClubType.Council && studentScript.Cosmetic.Club != ClubType.Bully && studentScript.Cosmetic.Club != ClubType.Delinquent && studentScript.Cosmetic.ClubAccessories[(int)studentScript.Cosmetic.Club] != null)
					{
						Renderer component2 = studentScript.Cosmetic.ClubAccessories[(int)studentScript.Cosmetic.Club].GetComponent<Renderer>();
						if (component2 != null)
						{
							component2.material.shader = this.NewHairShader;
						}
					}
				}
			}
			this.Yandere.MyRenderer.materials[0].shader = this.NewBodyShader;
			this.Yandere.MyRenderer.materials[1].shader = this.NewBodyShader;
			this.Yandere.MyRenderer.materials[2].shader = this.NewBodyShader;
			for (int j = 1; j < this.Yandere.Hairstyles.Length; j++)
			{
				Renderer component3 = this.Yandere.Hairstyles[j].GetComponent<Renderer>();
				if (component3 != null)
				{
					this.YandereHairRenderer.material.shader = this.NewHairShader;
					component3.material.shader = this.NewHairShader;
				}
			}
			this.Nemesis.Cosmetic.MyRenderer.materials[0].shader = this.NewBodyShader;
			this.Nemesis.Cosmetic.MyRenderer.materials[1].shader = this.NewBodyShader;
			this.Nemesis.Cosmetic.MyRenderer.materials[2].shader = this.NewBodyShader;
			this.Nemesis.NemesisHair.GetComponent<Renderer>().material.shader = this.NewHairShader;
		}
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x00139085 File Offset: 0x00137485
	public void UpdatePostAliasing()
	{
		if (!this.DoNothing)
		{
			this.PostAliasing.enabled = !OptionGlobals.DisablePostAliasing;
		}
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x001390A5 File Offset: 0x001374A5
	public void UpdateBloom()
	{
		if (!this.DoNothing)
		{
			this.BloomEffect.enabled = !OptionGlobals.DisableBloom;
		}
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x001390C8 File Offset: 0x001374C8
	public void UpdateLowDetailStudents()
	{
		if (OptionGlobals.LowDetailStudents > 10)
		{
			OptionGlobals.LowDetailStudents = 0;
		}
		else if (OptionGlobals.LowDetailStudents < 0)
		{
			OptionGlobals.LowDetailStudents = 10;
		}
		if (!this.DoNothing)
		{
			this.StudentManager.LowDetailThreshold = OptionGlobals.LowDetailStudents * 10;
			bool flag = (float)this.StudentManager.LowDetailThreshold > 0f;
			for (int i = 1; i < 101; i++)
			{
				if (this.StudentManager.Students[i] != null)
				{
					this.StudentManager.Students[i].LowPoly.enabled = flag;
					if (!flag && this.StudentManager.Students[i].LowPoly.MyMesh.enabled)
					{
						this.StudentManager.Students[i].LowPoly.MyMesh.enabled = false;
						this.StudentManager.Students[i].MyRenderer.enabled = true;
					}
				}
			}
		}
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x001391DC File Offset: 0x001375DC
	public void UpdateAnims()
	{
		if (OptionGlobals.DisableFarAnimations > 20)
		{
			OptionGlobals.DisableFarAnimations = 0;
		}
		else if (OptionGlobals.DisableFarAnimations < 0)
		{
			OptionGlobals.DisableFarAnimations = 20;
		}
		if (!this.DoNothing)
		{
			this.StudentManager.FarAnimThreshold = OptionGlobals.DisableFarAnimations * 5;
			if ((float)this.StudentManager.FarAnimThreshold > 0f)
			{
				this.StudentManager.DisableFarAnims = true;
			}
			else
			{
				this.StudentManager.DisableFarAnims = false;
			}
		}
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x00139264 File Offset: 0x00137664
	public void UpdateDrawDistance()
	{
		if (OptionGlobals.DrawDistance > OptionGlobals.DrawDistanceLimit)
		{
			OptionGlobals.DrawDistance = 10;
		}
		else if (OptionGlobals.DrawDistance < 1)
		{
			OptionGlobals.DrawDistance = OptionGlobals.DrawDistanceLimit;
		}
		if (!this.DoNothing)
		{
			Camera.main.farClipPlane = (float)OptionGlobals.DrawDistance;
			RenderSettings.fogEndDistance = (float)OptionGlobals.DrawDistance;
			this.Yandere.Smartphone.farClipPlane = (float)OptionGlobals.DrawDistance;
		}
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x001392E0 File Offset: 0x001376E0
	public void UpdateFog()
	{
		if (!this.DoNothing)
		{
			if (!OptionGlobals.Fog)
			{
				this.Yandere.MainCamera.clearFlags = CameraClearFlags.Skybox;
				RenderSettings.fogMode = FogMode.Exponential;
			}
			else
			{
				this.Yandere.MainCamera.clearFlags = CameraClearFlags.Color;
				RenderSettings.fogMode = FogMode.Linear;
				RenderSettings.fogEndDistance = (float)OptionGlobals.DrawDistance;
			}
		}
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x00139340 File Offset: 0x00137740
	public void UpdateShadows()
	{
		if (!this.DoNothing)
		{
			this.Sun.shadows = ((!OptionGlobals.EnableShadows) ? LightShadows.None : LightShadows.Soft);
		}
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x00139369 File Offset: 0x00137769
	public void ToggleRun()
	{
		if (!this.DoNothing)
		{
			this.Yandere.ToggleRun = OptionGlobals.ToggleRun;
		}
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x00139388 File Offset: 0x00137788
	public void UpdateFPSIndex()
	{
		if (OptionGlobals.FPSIndex < 0)
		{
			OptionGlobals.FPSIndex = QualityManagerScript.FPSValues.Length - 1;
		}
		else if (OptionGlobals.FPSIndex >= QualityManagerScript.FPSValues.Length)
		{
			OptionGlobals.FPSIndex = 0;
		}
		Application.targetFrameRate = QualityManagerScript.FPSValues[OptionGlobals.FPSIndex];
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x001393DC File Offset: 0x001377DC
	public void ToggleExperiment()
	{
		if (!this.DoNothing)
		{
			if (!this.ExperimentalBloomAndLensFlares.enabled)
			{
				this.ExperimentalBloomAndLensFlares.enabled = true;
				this.ExperimentalDepthOfField34.enabled = false;
				this.ExperimentalSSAOEffect.enabled = false;
				this.BloomEffect.enabled = true;
			}
			else
			{
				this.ExperimentalBloomAndLensFlares.enabled = false;
				this.ExperimentalDepthOfField34.enabled = false;
				this.ExperimentalSSAOEffect.enabled = false;
				this.BloomEffect.enabled = false;
			}
		}
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x0013946C File Offset: 0x0013786C
	public void RimLight()
	{
		if (!this.DoNothing)
		{
			if (!this.RimLightActive)
			{
				this.RimLightActive = true;
				for (int i = 1; i < this.StudentManager.Students.Length; i++)
				{
					StudentScript studentScript = this.StudentManager.Students[i];
					if (studentScript != null && studentScript.gameObject.activeInHierarchy)
					{
						this.NewHairShader = this.ToonOutlineRimLight;
						this.NewBodyShader = this.ToonOutlineRimLight;
						studentScript.MyRenderer.materials[0].shader = this.ToonOutlineRimLight;
						studentScript.MyRenderer.materials[1].shader = this.ToonOutlineRimLight;
						studentScript.MyRenderer.materials[2].shader = this.ToonOutlineRimLight;
						this.AdjustRimLight(studentScript.MyRenderer.materials[0]);
						this.AdjustRimLight(studentScript.MyRenderer.materials[1]);
						this.AdjustRimLight(studentScript.MyRenderer.materials[2]);
						if (!studentScript.Male)
						{
							if (!studentScript.Teacher)
							{
								if (studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle] != null)
								{
									if (studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials.Length == 1)
									{
										studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].material.shader = this.ToonOutlineRimLight;
										this.AdjustRimLight(studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].material);
									}
									else
									{
										studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[0].shader = this.ToonOutlineRimLight;
										studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[1].shader = this.ToonOutlineRimLight;
										this.AdjustRimLight(studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[0]);
										this.AdjustRimLight(studentScript.Cosmetic.FemaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[1]);
									}
								}
								if (studentScript.Cosmetic.Accessory > 0 && studentScript.Cosmetic.FemaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>() != null)
								{
									studentScript.Cosmetic.FemaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>().material.shader = this.ToonOutlineRimLight;
									this.AdjustRimLight(studentScript.Cosmetic.FemaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>().material);
								}
							}
							else
							{
								studentScript.Cosmetic.TeacherHairRenderers[studentScript.Cosmetic.Hairstyle].material.shader = this.ToonOutlineRimLight;
								this.AdjustRimLight(studentScript.Cosmetic.TeacherHairRenderers[studentScript.Cosmetic.Hairstyle].material);
							}
						}
						else
						{
							if (studentScript.Cosmetic.Hairstyle > 0)
							{
								if (studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials.Length == 1)
								{
									studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].material.shader = this.ToonOutlineRimLight;
									this.AdjustRimLight(studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].material);
								}
								else
								{
									studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[0].shader = this.ToonOutlineRimLight;
									studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[1].shader = this.ToonOutlineRimLight;
									this.AdjustRimLight(studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[0]);
									this.AdjustRimLight(studentScript.Cosmetic.MaleHairRenderers[studentScript.Cosmetic.Hairstyle].materials[1]);
								}
							}
							if (studentScript.Cosmetic.Accessory > 0)
							{
								Renderer component = studentScript.Cosmetic.MaleAccessories[studentScript.Cosmetic.Accessory].GetComponent<Renderer>();
								if (component != null)
								{
									component.material.shader = this.ToonOutlineRimLight;
									this.AdjustRimLight(component.material);
								}
							}
						}
						if (!studentScript.Teacher && studentScript.Cosmetic.Club > ClubType.None && studentScript.Cosmetic.Club != ClubType.Council && studentScript.Cosmetic.Club != ClubType.Bully && studentScript.Cosmetic.Club != ClubType.Delinquent && studentScript.Cosmetic.ClubAccessories[(int)studentScript.Cosmetic.Club] != null)
						{
							Renderer component2 = studentScript.Cosmetic.ClubAccessories[(int)studentScript.Cosmetic.Club].GetComponent<Renderer>();
							if (component2 != null)
							{
								component2.material.shader = this.ToonOutlineRimLight;
								this.AdjustRimLight(component2.material);
							}
						}
					}
				}
				this.Yandere.MyRenderer.materials[0].shader = this.ToonOutlineRimLight;
				this.Yandere.MyRenderer.materials[1].shader = this.ToonOutlineRimLight;
				this.Yandere.MyRenderer.materials[2].shader = this.ToonOutlineRimLight;
				this.AdjustRimLight(this.Yandere.MyRenderer.materials[0]);
				this.AdjustRimLight(this.Yandere.MyRenderer.materials[1]);
				this.AdjustRimLight(this.Yandere.MyRenderer.materials[2]);
				for (int j = 1; j < this.Yandere.Hairstyles.Length; j++)
				{
					Renderer component3 = this.Yandere.Hairstyles[j].GetComponent<Renderer>();
					if (component3 != null)
					{
						this.YandereHairRenderer.material.shader = this.ToonOutlineRimLight;
						component3.material.shader = this.ToonOutlineRimLight;
						this.AdjustRimLight(this.YandereHairRenderer.material);
						this.AdjustRimLight(component3.material);
					}
				}
				this.Nemesis.Cosmetic.MyRenderer.materials[0].shader = this.ToonOutlineRimLight;
				this.Nemesis.Cosmetic.MyRenderer.materials[1].shader = this.ToonOutlineRimLight;
				this.Nemesis.Cosmetic.MyRenderer.materials[2].shader = this.ToonOutlineRimLight;
				this.Nemesis.NemesisHair.GetComponent<Renderer>().material.shader = this.ToonOutlineRimLight;
				this.AdjustRimLight(this.Nemesis.Cosmetic.MyRenderer.materials[0]);
				this.AdjustRimLight(this.Nemesis.Cosmetic.MyRenderer.materials[1]);
				this.AdjustRimLight(this.Nemesis.Cosmetic.MyRenderer.materials[2]);
				this.AdjustRimLight(this.Nemesis.NemesisHair.GetComponent<Renderer>().material);
			}
			else
			{
				this.RimLightActive = false;
				this.UpdateOutlines();
			}
		}
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x00139BFA File Offset: 0x00137FFA
	public void UpdateObscurance()
	{
		if (!this.DoNothing)
		{
			this.Obscurance.enabled = !OptionGlobals.DisableObscurance;
		}
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00139C1A File Offset: 0x0013801A
	public void AdjustRimLight(Material mat)
	{
		if (!this.DoNothing)
		{
			mat.SetFloat("_RimLightIntensity", 5f);
			mat.SetFloat("_RimCrisp", 0.5f);
			mat.SetFloat("_RimAdditive", 0.5f);
		}
	}

	// Token: 0x040028F1 RID: 10481
	public AntialiasingAsPostEffect PostAliasing;

	// Token: 0x040028F2 RID: 10482
	public StudentManagerScript StudentManager;

	// Token: 0x040028F3 RID: 10483
	public PostProcessingBehaviour Obscurance;

	// Token: 0x040028F4 RID: 10484
	public SettingsScript Settings;

	// Token: 0x040028F5 RID: 10485
	public NemesisScript Nemesis;

	// Token: 0x040028F6 RID: 10486
	public YandereScript Yandere;

	// Token: 0x040028F7 RID: 10487
	public Bloom BloomEffect;

	// Token: 0x040028F8 RID: 10488
	public Light Sun;

	// Token: 0x040028F9 RID: 10489
	public ParticleSystem EastRomanceBlossoms;

	// Token: 0x040028FA RID: 10490
	public ParticleSystem WestRomanceBlossoms;

	// Token: 0x040028FB RID: 10491
	public ParticleSystem CorridorBlossoms;

	// Token: 0x040028FC RID: 10492
	public ParticleSystem PlazaBlossoms;

	// Token: 0x040028FD RID: 10493
	public ParticleSystem MythBlossoms;

	// Token: 0x040028FE RID: 10494
	public ParticleSystem[] Fountains;

	// Token: 0x040028FF RID: 10495
	public ParticleSystem[] Steam;

	// Token: 0x04002900 RID: 10496
	public Renderer YandereHairRenderer;

	// Token: 0x04002901 RID: 10497
	public Shader NewBodyShader;

	// Token: 0x04002902 RID: 10498
	public Shader NewHairShader;

	// Token: 0x04002903 RID: 10499
	public Shader Toon;

	// Token: 0x04002904 RID: 10500
	public Shader ToonOutline;

	// Token: 0x04002905 RID: 10501
	public Shader ToonOverlay;

	// Token: 0x04002906 RID: 10502
	public Shader ToonOutlineOverlay;

	// Token: 0x04002907 RID: 10503
	public Shader ToonOutlineRimLight;

	// Token: 0x04002908 RID: 10504
	public BloomAndLensFlares ExperimentalBloomAndLensFlares;

	// Token: 0x04002909 RID: 10505
	public DepthOfField34 ExperimentalDepthOfField34;

	// Token: 0x0400290A RID: 10506
	public SSAOEffect ExperimentalSSAOEffect;

	// Token: 0x0400290B RID: 10507
	public bool RimLightActive;

	// Token: 0x0400290C RID: 10508
	public bool DoNothing;

	// Token: 0x0400290D RID: 10509
	private static readonly int[] FPSValues = new int[]
	{
		int.MaxValue,
		30,
		60,
		120
	};

	// Token: 0x0400290E RID: 10510
	public static readonly string[] FPSStrings = new string[]
	{
		"Unlimited",
		"30",
		"60",
		"120"
	};
}
