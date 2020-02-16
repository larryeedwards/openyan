using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000417 RID: 1047
public class HomeCameraScript : MonoBehaviour
{
	// Token: 0x06001C89 RID: 7305 RVA: 0x00101EF0 File Offset: 0x001002F0
	public void Start()
	{
		this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, 0f);
		this.Focus.position = this.Target.position;
		base.transform.position = this.Destination.position;
		if (HomeGlobals.Night)
		{
			this.CeilingLight.SetActive(true);
			this.SenpaiLight.SetActive(true);
			this.NightLight.SetActive(true);
			this.DayLight.SetActive(false);
			this.Triggers[7].Disable();
			this.BasementJukebox.clip = this.NightBasement;
			this.RoomJukebox.clip = this.NightRoom;
			this.PlayMusic();
			this.PantiesMangaLabel.text = "Read Manga";
		}
		else
		{
			this.BasementJukebox.Play();
			this.RoomJukebox.Play();
			this.ComputerScreen.SetActive(false);
			this.Triggers[2].Disable();
			this.Triggers[3].Disable();
			this.Triggers[5].Disable();
			this.Triggers[9].Disable();
		}
		if (SchoolGlobals.KidnapVictim == 0)
		{
			this.RopeGroup.SetActive(false);
			this.Tripod.SetActive(false);
			this.Victim.SetActive(false);
			this.Triggers[10].Disable();
		}
		else
		{
			int kidnapVictim = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentArrested(kidnapVictim) || StudentGlobals.GetStudentDead(kidnapVictim))
			{
				this.RopeGroup.SetActive(false);
				this.Victim.SetActive(false);
				this.Triggers[10].Disable();
			}
		}
		if (GameGlobals.LoveSick)
		{
			this.LoveSickColorSwap();
		}
		Time.timeScale = 1f;
		this.HairLock.material.color = this.SenpaiCosmetic.ColorValue;
		if (SchoolGlobals.SchoolAtmosphere > 1f)
		{
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
	}

	// Token: 0x06001C8A RID: 7306 RVA: 0x00102124 File Offset: 0x00100524
	private void LateUpdate()
	{
		if (this.HomeYandere.transform.position.y > -5f)
		{
			Transform transform = this.Destinations[0];
			transform.position = new Vector3(-this.HomeYandere.transform.position.x, transform.position.y, transform.position.z);
		}
		this.Focus.position = Vector3.Lerp(this.Focus.position, this.Target.position, Time.deltaTime * 10f);
		base.transform.position = Vector3.Lerp(base.transform.position, this.Destination.position, Time.deltaTime * 10f);
		base.transform.LookAt(this.Focus.position);
		if (this.ID != 11 && Input.GetButtonDown("A") && this.HomeYandere.CanMove && this.ID != 0)
		{
			this.Destination = this.Destinations[this.ID];
			this.Target = this.Targets[this.ID];
			this.HomeWindows[this.ID].Show = true;
			this.HomeYandere.CanMove = false;
			if (this.ID == 1 || this.ID == 8)
			{
				this.HomeExit.enabled = true;
			}
			else if (this.ID == 2)
			{
				this.HomeSleep.enabled = true;
			}
			else if (this.ID == 3)
			{
				this.HomeInternet.enabled = true;
			}
			else if (this.ID == 4)
			{
				this.CorkboardLabel.SetActive(false);
				this.HomeCorkboard.enabled = true;
				this.LoadingScreen.SetActive(true);
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 5)
			{
				this.HomeYandere.enabled = false;
				this.Controller.transform.localPosition = new Vector3(0.1245f, 0.032f, 0f);
				this.HomeYandere.transform.position = new Vector3(1f, 0f, 0f);
				this.HomeYandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.HomeYandere.Character.GetComponent<Animation>().Play("f02_gaming_00");
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Play";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.Label[4].text = "Select";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
			else if (this.ID == 6)
			{
				this.HomeSenpaiShrine.enabled = true;
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 7)
			{
				this.HomePantyChanger.enabled = true;
			}
			else if (this.ID == 9)
			{
				this.HomeManga.enabled = true;
			}
			else if (this.ID == 10)
			{
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Back";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
				this.HomePrisoner.UpdateDesc();
				this.HomeYandere.gameObject.SetActive(false);
			}
			else if (this.ID == 12)
			{
				this.HomeAnime.enabled = true;
			}
		}
		if (this.Destination == this.Destinations[0])
		{
			this.Vignette.intensity = ((this.HomeYandere.transform.position.y <= -1f) ? Mathf.MoveTowards(this.Vignette.intensity, 5f, Time.deltaTime * 5f) : Mathf.MoveTowards(this.Vignette.intensity, 1f, Time.deltaTime));
			this.Vignette.chromaticAberration = Mathf.MoveTowards(this.Vignette.chromaticAberration, 1f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 1f, Time.deltaTime);
		}
		else
		{
			this.Vignette.intensity = ((this.HomeYandere.transform.position.y <= -1f) ? Mathf.MoveTowards(this.Vignette.intensity, 0f, Time.deltaTime * 5f) : Mathf.MoveTowards(this.Vignette.intensity, 0f, Time.deltaTime));
			this.Vignette.chromaticAberration = Mathf.MoveTowards(this.Vignette.chromaticAberration, 0f, Time.deltaTime);
			this.Vignette.blur = Mathf.MoveTowards(this.Vignette.blur, 0f, Time.deltaTime);
		}
		this.Button.color = new Color(this.Button.color.r, this.Button.color.g, this.Button.color.b, Mathf.MoveTowards(this.Button.color.a, (this.ID <= 0 || !this.HomeYandere.CanMove) ? 0f : 1f, Time.deltaTime * 10f));
		if (this.HomeDarkness.FadeOut)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0f, Time.deltaTime);
		}
		else if (this.HomeYandere.transform.position.y > -1f)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0.5f, Time.deltaTime);
		}
		else if (!this.Torturing)
		{
			this.BasementJukebox.volume = Mathf.MoveTowards(this.BasementJukebox.volume, 0.5f, Time.deltaTime);
			this.RoomJukebox.volume = Mathf.MoveTowards(this.RoomJukebox.volume, 0f, Time.deltaTime);
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			TaskGlobals.SetTaskStatus(38, 1);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			this.BasementJukebox.gameObject.SetActive(false);
			this.RoomJukebox.gameObject.SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			HomeGlobals.Night = !HomeGlobals.Night;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 1f;
		}
		if (Input.GetKeyDown(KeyCode.Minus) && Time.timeScale > 1f)
		{
			Time.timeScale -= 1f;
		}
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x00102948 File Offset: 0x00100D48
	public void PlayMusic()
	{
		if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
		{
			if (!this.BasementJukebox.isPlaying)
			{
				this.BasementJukebox.Play();
			}
			if (!this.RoomJukebox.isPlaying)
			{
				this.RoomJukebox.Play();
			}
		}
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x001029A0 File Offset: 0x00100DA0
	private void LoveSickColorSwap()
	{
		GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject gameObject in array)
		{
			if (gameObject.transform.parent != this.PauseScreen && gameObject.transform.parent != this.PromptBarPanel)
			{
				UISprite component = gameObject.GetComponent<UISprite>();
				if (component != null && component.color != Color.black)
				{
					component.color = new Color(1f, 0f, 0f, component.color.a);
				}
				UILabel component2 = gameObject.GetComponent<UILabel>();
				if (component2 != null && component2.color != Color.black)
				{
					component2.color = new Color(1f, 0f, 0f, component2.color.a);
				}
			}
		}
		this.DayLight.GetComponent<Light>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
		this.HomeDarkness.Sprite.color = Color.black;
		this.BasementJukebox.clip = this.HomeLoveSick;
		this.RoomJukebox.clip = this.HomeLoveSick;
		this.LoveSickCamera.SetActive(true);
		this.PlayMusic();
	}

	// Token: 0x04002146 RID: 8518
	public HomeWindowScript[] HomeWindows;

	// Token: 0x04002147 RID: 8519
	public HomeTriggerScript[] Triggers;

	// Token: 0x04002148 RID: 8520
	public HomePantyChangerScript HomePantyChanger;

	// Token: 0x04002149 RID: 8521
	public HomeSenpaiShrineScript HomeSenpaiShrine;

	// Token: 0x0400214A RID: 8522
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x0400214B RID: 8523
	public HomeCorkboardScript HomeCorkboard;

	// Token: 0x0400214C RID: 8524
	public HomeDarknessScript HomeDarkness;

	// Token: 0x0400214D RID: 8525
	public HomeInternetScript HomeInternet;

	// Token: 0x0400214E RID: 8526
	public HomePrisonerScript HomePrisoner;

	// Token: 0x0400214F RID: 8527
	public HomeYandereScript HomeYandere;

	// Token: 0x04002150 RID: 8528
	public HomeSleepScript HomeAnime;

	// Token: 0x04002151 RID: 8529
	public HomeMangaScript HomeManga;

	// Token: 0x04002152 RID: 8530
	public HomeSleepScript HomeSleep;

	// Token: 0x04002153 RID: 8531
	public HomeExitScript HomeExit;

	// Token: 0x04002154 RID: 8532
	public PromptBarScript PromptBar;

	// Token: 0x04002155 RID: 8533
	public Vignetting Vignette;

	// Token: 0x04002156 RID: 8534
	public UILabel PantiesMangaLabel;

	// Token: 0x04002157 RID: 8535
	public UISprite Button;

	// Token: 0x04002158 RID: 8536
	public GameObject CyberstalkWindow;

	// Token: 0x04002159 RID: 8537
	public GameObject ComputerScreen;

	// Token: 0x0400215A RID: 8538
	public GameObject CorkboardLabel;

	// Token: 0x0400215B RID: 8539
	public GameObject LoveSickCamera;

	// Token: 0x0400215C RID: 8540
	public GameObject LoadingScreen;

	// Token: 0x0400215D RID: 8541
	public GameObject CeilingLight;

	// Token: 0x0400215E RID: 8542
	public GameObject SenpaiLight;

	// Token: 0x0400215F RID: 8543
	public GameObject Controller;

	// Token: 0x04002160 RID: 8544
	public GameObject NightLight;

	// Token: 0x04002161 RID: 8545
	public GameObject RopeGroup;

	// Token: 0x04002162 RID: 8546
	public GameObject DayLight;

	// Token: 0x04002163 RID: 8547
	public GameObject Tripod;

	// Token: 0x04002164 RID: 8548
	public GameObject Victim;

	// Token: 0x04002165 RID: 8549
	public Transform Destination;

	// Token: 0x04002166 RID: 8550
	public Transform Target;

	// Token: 0x04002167 RID: 8551
	public Transform Focus;

	// Token: 0x04002168 RID: 8552
	public Transform[] Destinations;

	// Token: 0x04002169 RID: 8553
	public Transform[] Targets;

	// Token: 0x0400216A RID: 8554
	public int ID;

	// Token: 0x0400216B RID: 8555
	public AudioSource BasementJukebox;

	// Token: 0x0400216C RID: 8556
	public AudioSource RoomJukebox;

	// Token: 0x0400216D RID: 8557
	public AudioClip NightBasement;

	// Token: 0x0400216E RID: 8558
	public AudioClip NightRoom;

	// Token: 0x0400216F RID: 8559
	public AudioClip HomeLoveSick;

	// Token: 0x04002170 RID: 8560
	public bool Torturing;

	// Token: 0x04002171 RID: 8561
	public CosmeticScript SenpaiCosmetic;

	// Token: 0x04002172 RID: 8562
	public Renderer HairLock;

	// Token: 0x04002173 RID: 8563
	public Transform PromptBarPanel;

	// Token: 0x04002174 RID: 8564
	public Transform PauseScreen;
}
