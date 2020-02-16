using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200042D RID: 1069
public class HomeYandereScript : MonoBehaviour
{
	// Token: 0x06001CDE RID: 7390 RVA: 0x001096C8 File Offset: 0x00107AC8
	public void Start()
	{
		if (this.CutsceneYandere != null)
		{
			this.CutsceneYandere.GetComponent<Animation>()["f02_midoriTexting_00"].speed = 0.1f;
		}
		if (SceneManager.GetActiveScene().name == "HomeScene")
		{
			if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
			{
				base.transform.position = Vector3.zero;
				base.transform.eulerAngles = Vector3.zero;
				if (!HomeGlobals.Night)
				{
					this.ChangeSchoolwear();
					base.StartCoroutine(this.ApplyCustomCostume());
				}
				else
				{
					this.WearPajamas();
				}
				if (DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					this.Nude();
				}
			}
			else if (HomeGlobals.StartInBasement)
			{
				HomeGlobals.StartInBasement = false;
				base.transform.position = new Vector3(0f, -135f, 0f);
				base.transform.eulerAngles = Vector3.zero;
			}
			else if (HomeGlobals.MiyukiDefeated)
			{
				base.transform.position = new Vector3(1f, 0f, 0f);
				base.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.Character.GetComponent<Animation>().Play("f02_discScratch_00");
				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];
				this.Disc.SetActive(true);
				this.WearPajamas();
				this.MyAudio.clip = this.MiyukiReaction;
			}
			else
			{
				base.transform.position = new Vector3(1f, 0f, 0f);
				base.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.Character.GetComponent<Animation>().Play("f02_discScratch_00");
				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];
				this.Disc.SetActive(true);
				this.WearPajamas();
			}
			if (GameGlobals.BlondeHair)
			{
				this.PonytailRenderer.material.mainTexture = this.BlondePony;
			}
		}
		Time.timeScale = 1f;
		this.UpdateHair();
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x001099E4 File Offset: 0x00107DE4
	private void Update()
	{
		if (!this.Disc.activeInHierarchy)
		{
			Animation component = this.Character.GetComponent<Animation>();
			if (this.CanMove)
			{
				if (!OptionGlobals.ToggleRun)
				{
					this.Running = false;
					if (Input.GetButton("LB"))
					{
						this.Running = true;
					}
				}
				else if (Input.GetButtonDown("LB"))
				{
					this.Running = !this.Running;
				}
				this.MyController.Move(Physics.gravity * 0.01f);
				float axis = Input.GetAxis("Vertical");
				float axis2 = Input.GetAxis("Horizontal");
				Vector3 a = Camera.main.transform.TransformDirection(Vector3.forward);
				a.y = 0f;
				a = a.normalized;
				Vector3 a2 = new Vector3(a.z, 0f, -a.x);
				Vector3 vector = axis2 * a2 + axis * a;
				if (vector != Vector3.zero)
				{
					Quaternion b = Quaternion.LookRotation(vector);
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
				}
				if (axis != 0f || axis2 != 0f)
				{
					if (this.Running)
					{
						component.CrossFade("f02_run_00");
						this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
					}
					else
					{
						component.CrossFade("f02_newWalk_00");
						this.MyController.Move(base.transform.forward * this.WalkSpeed * Time.deltaTime);
					}
				}
				else
				{
					component.CrossFade("f02_idleShort_00");
				}
			}
			else
			{
				component.CrossFade("f02_idleShort_00");
			}
		}
		else if (this.HomeDarkness.color.a == 0f)
		{
			if (this.Timer == 0f)
			{
				this.MyAudio.Play();
			}
			else if (this.Timer > this.MyAudio.clip.length + 1f)
			{
				YanvaniaGlobals.DraculaDefeated = false;
				HomeGlobals.MiyukiDefeated = false;
				this.Disc.SetActive(false);
				this.HomeVideoGames.Quit();
			}
			this.Timer += Time.deltaTime;
		}
		Rigidbody component2 = base.GetComponent<Rigidbody>();
		if (component2 != null)
		{
			component2.velocity = Vector3.zero;
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			this.UpdateHair();
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			SchoolGlobals.KidnapVictim = this.VictimID;
			StudentGlobals.SetStudentSanity(this.VictimID, 100f);
			SchemeGlobals.SetSchemeStage(6, 5);
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.F1))
		{
			StudentGlobals.MaleUniform = 1;
			StudentGlobals.FemaleUniform = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			StudentGlobals.MaleUniform = 2;
			StudentGlobals.FemaleUniform = 2;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			StudentGlobals.MaleUniform = 3;
			StudentGlobals.FemaleUniform = 3;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F4))
		{
			StudentGlobals.MaleUniform = 4;
			StudentGlobals.FemaleUniform = 4;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F5))
		{
			StudentGlobals.MaleUniform = 5;
			StudentGlobals.FemaleUniform = 5;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKeyDown(KeyCode.F6))
		{
			StudentGlobals.MaleUniform = 6;
			StudentGlobals.FemaleUniform = 6;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (base.transform.position.y < -10f)
		{
			base.transform.position = new Vector3(base.transform.position.x, -10f, base.transform.position.z);
		}
	}

	// Token: 0x06001CE0 RID: 7392 RVA: 0x00109E78 File Offset: 0x00108278
	private void LateUpdate()
	{
		if (this.HidePony)
		{
			this.Ponytail.parent.transform.localScale = new Vector3(1f, 1f, 0.93f);
			this.Ponytail.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
			this.HairR.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
			this.HairL.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
		}
	}

	// Token: 0x06001CE1 RID: 7393 RVA: 0x00109F18 File Offset: 0x00108318
	private void UpdateHair()
	{
		this.PigtailR.transform.parent.transform.parent.transform.localScale = new Vector3(1f, 0.75f, 1f);
		this.PigtailL.transform.parent.transform.parent.transform.localScale = new Vector3(1f, 0.75f, 1f);
		this.PigtailR.gameObject.SetActive(false);
		this.PigtailL.gameObject.SetActive(false);
		this.Drills.gameObject.SetActive(false);
		this.HidePony = true;
		this.Hairstyle++;
		if (this.Hairstyle > 7)
		{
			this.Hairstyle = 1;
		}
		if (this.Hairstyle == 1)
		{
			this.HidePony = false;
			this.Ponytail.localScale = new Vector3(1f, 1f, 1f);
			this.HairR.localScale = new Vector3(1f, 1f, 1f);
			this.HairL.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (this.Hairstyle == 2)
		{
			this.PigtailR.gameObject.SetActive(true);
		}
		else if (this.Hairstyle == 3)
		{
			this.PigtailL.gameObject.SetActive(true);
		}
		else if (this.Hairstyle == 4)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
		}
		else if (this.Hairstyle == 5)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
			this.HidePony = false;
			this.Ponytail.localScale = new Vector3(1f, 1f, 1f);
			this.HairR.localScale = new Vector3(1f, 1f, 1f);
			this.HairL.localScale = new Vector3(1f, 1f, 1f);
		}
		else if (this.Hairstyle == 6)
		{
			this.PigtailR.gameObject.SetActive(true);
			this.PigtailL.gameObject.SetActive(true);
			this.PigtailR.transform.parent.transform.parent.transform.localScale = new Vector3(2f, 2f, 2f);
			this.PigtailL.transform.parent.transform.parent.transform.localScale = new Vector3(2f, 2f, 2f);
		}
		else if (this.Hairstyle == 7)
		{
			this.Drills.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x0010A234 File Offset: 0x00108634
	private void ChangeSchoolwear()
	{
		this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[0].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomCostume());
	}

	// Token: 0x06001CE3 RID: 7395 RVA: 0x0010A2BC File Offset: 0x001086BC
	private void WearPajamas()
	{
		this.MyRenderer.sharedMesh = this.PajamaMesh;
		this.MyRenderer.materials[0].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[1].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomFace());
	}

	// Token: 0x06001CE4 RID: 7396 RVA: 0x0010A330 File Offset: 0x00108730
	private void Nude()
	{
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
	}

	// Token: 0x06001CE5 RID: 7397 RVA: 0x0010A398 File Offset: 0x00108798
	private IEnumerator ApplyCustomCostume()
	{
		if (StudentGlobals.FemaleUniform == 1)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomUniform.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 2)
		{
			WWW CustomLong = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLong.png");
			yield return CustomLong;
			if (CustomLong.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomLong.texture;
				this.MyRenderer.materials[1].mainTexture = CustomLong.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 3)
		{
			WWW CustomSweater = new WWW("file:///" + Application.streamingAssetsPath + "/CustomSweater.png");
			yield return CustomSweater;
			if (CustomSweater.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomSweater.texture;
				this.MyRenderer.materials[1].mainTexture = CustomSweater.texture;
			}
		}
		else if (StudentGlobals.FemaleUniform == 4 || StudentGlobals.FemaleUniform == 5)
		{
			WWW CustomBlazer = new WWW("file:///" + Application.streamingAssetsPath + "/CustomBlazer.png");
			yield return CustomBlazer;
			if (CustomBlazer.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomBlazer.texture;
				this.MyRenderer.materials[1].mainTexture = CustomBlazer.texture;
			}
		}
		base.StartCoroutine(this.ApplyCustomFace());
		yield break;
	}

	// Token: 0x06001CE6 RID: 7398 RVA: 0x0010A3B4 File Offset: 0x001087B4
	private IEnumerator ApplyCustomFace()
	{
		WWW CustomFace = new WWW("file:///" + Application.streamingAssetsPath + "/CustomFace.png");
		yield return CustomFace;
		if (CustomFace.error == null)
		{
			this.MyRenderer.materials[2].mainTexture = CustomFace.texture;
			this.FaceTexture = CustomFace.texture;
		}
		WWW CustomHair = new WWW("file:///" + Application.streamingAssetsPath + "/CustomHair.png");
		yield return CustomHair;
		if (CustomHair.error == null)
		{
			this.PonytailRenderer.material.mainTexture = CustomHair.texture;
			this.PigtailR.material.mainTexture = CustomHair.texture;
			this.PigtailL.material.mainTexture = CustomHair.texture;
		}
		WWW CustomDrills = new WWW("file:///" + Application.streamingAssetsPath + "/CustomDrills.png");
		yield return CustomDrills;
		if (CustomDrills.error == null)
		{
			this.Drills.materials[0].mainTexture = CustomDrills.texture;
			this.Drills.materials[1].mainTexture = CustomDrills.texture;
			this.Drills.materials[2].mainTexture = CustomDrills.texture;
		}
		yield break;
	}

	// Token: 0x04002299 RID: 8857
	public CharacterController MyController;

	// Token: 0x0400229A RID: 8858
	public AudioSource MyAudio;

	// Token: 0x0400229B RID: 8859
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x0400229C RID: 8860
	public HomeCameraScript HomeCamera;

	// Token: 0x0400229D RID: 8861
	public UISprite HomeDarkness;

	// Token: 0x0400229E RID: 8862
	public GameObject CutsceneYandere;

	// Token: 0x0400229F RID: 8863
	public GameObject Controller;

	// Token: 0x040022A0 RID: 8864
	public GameObject Character;

	// Token: 0x040022A1 RID: 8865
	public GameObject Disc;

	// Token: 0x040022A2 RID: 8866
	public float WalkSpeed;

	// Token: 0x040022A3 RID: 8867
	public float RunSpeed;

	// Token: 0x040022A4 RID: 8868
	public bool CanMove;

	// Token: 0x040022A5 RID: 8869
	public bool Running;

	// Token: 0x040022A6 RID: 8870
	public AudioClip MiyukiReaction;

	// Token: 0x040022A7 RID: 8871
	public AudioClip DiscScratch;

	// Token: 0x040022A8 RID: 8872
	public Renderer PonytailRenderer;

	// Token: 0x040022A9 RID: 8873
	public Renderer PigtailR;

	// Token: 0x040022AA RID: 8874
	public Renderer PigtailL;

	// Token: 0x040022AB RID: 8875
	public Renderer Drills;

	// Token: 0x040022AC RID: 8876
	public Transform Ponytail;

	// Token: 0x040022AD RID: 8877
	public Transform HairR;

	// Token: 0x040022AE RID: 8878
	public Transform HairL;

	// Token: 0x040022AF RID: 8879
	public bool HidePony;

	// Token: 0x040022B0 RID: 8880
	public int Hairstyle;

	// Token: 0x040022B1 RID: 8881
	public int VictimID;

	// Token: 0x040022B2 RID: 8882
	public float Timer;

	// Token: 0x040022B3 RID: 8883
	public Texture BlondePony;

	// Token: 0x040022B4 RID: 8884
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040022B5 RID: 8885
	public Texture[] UniformTextures;

	// Token: 0x040022B6 RID: 8886
	public Texture FaceTexture;

	// Token: 0x040022B7 RID: 8887
	public Mesh[] Uniforms;

	// Token: 0x040022B8 RID: 8888
	public Texture PajamaTexture;

	// Token: 0x040022B9 RID: 8889
	public Mesh PajamaMesh;

	// Token: 0x040022BA RID: 8890
	public Texture NudeTexture;

	// Token: 0x040022BB RID: 8891
	public Mesh NudeMesh;
}
