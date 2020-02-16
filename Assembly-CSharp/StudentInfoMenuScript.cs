using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200052E RID: 1326
public class StudentInfoMenuScript : MonoBehaviour
{
	// Token: 0x0600207B RID: 8315 RVA: 0x00154050 File Offset: 0x00152450
	private void Start()
	{
		for (int i = 1; i < 101; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StudentPortrait, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.PortraitGrid;
			gameObject.transform.localPosition = new Vector3(-300f + (float)this.Column * 150f, 80f - (float)this.Row * 160f, 0f);
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.StudentPortraits[i] = gameObject.GetComponent<StudentPortraitScript>();
			this.Column++;
			if (this.Column > 4)
			{
				this.Column = 0;
				this.Row++;
			}
		}
		this.Column = 0;
		this.Row = 0;
	}

	// Token: 0x0600207C RID: 8316 RVA: 0x00154154 File Offset: 0x00152554
	private void Update()
	{
		if (!this.GrabbedPortraits)
		{
			base.StartCoroutine(this.UpdatePortraits());
			this.GrabbedPortraits = true;
		}
		if (Input.GetButtonDown("A") && this.PromptBar.Label[0].text != string.Empty)
		{
			if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
			{
				if (this.UsingLifeNote)
				{
					this.PauseScreen.MainMenu.SetActive(true);
					this.PauseScreen.Sideways = false;
					this.PauseScreen.Show = false;
					base.gameObject.SetActive(false);
					this.NoteWindow.TargetStudent = this.StudentID;
					this.NoteWindow.gameObject.SetActive(true);
					this.NoteWindow.SlotLabels[1].text = this.StudentManager.Students[this.StudentID].Name;
					this.NoteWindow.SlotsFilled[1] = true;
					this.UsingLifeNote = false;
					this.PromptBar.Label[0].text = "Confirm";
					this.PromptBar.UpdateButtons();
					this.NoteWindow.CheckForCompletion();
				}
				else
				{
					this.StudentInfo.gameObject.SetActive(true);
					this.StudentInfo.UpdateInfo(this.StudentID);
					this.StudentInfo.Topics.SetActive(false);
					base.gameObject.SetActive(false);
					this.PromptBar.ClearButtons();
					if (this.Gossiping)
					{
						this.PromptBar.Label[0].text = "Gossip";
					}
					if (this.Distracting)
					{
						this.PromptBar.Label[0].text = "Distract";
					}
					if (this.CyberBullying || this.CyberStalking)
					{
						this.PromptBar.Label[0].text = "Accept";
					}
					if (this.FindingLocker)
					{
						this.PromptBar.Label[0].text = "Find Locker";
					}
					if (this.MatchMaking)
					{
						this.PromptBar.Label[0].text = "Match";
					}
					if (this.Targeting || this.UsingLifeNote)
					{
						this.PromptBar.Label[0].text = "Kill";
					}
					if (this.SendingHome)
					{
						this.PromptBar.Label[0].text = "Send Home";
					}
					if (this.StudentManager.Students[this.StudentID] != null)
					{
						if (this.StudentManager.Students[this.StudentID].gameObject.activeInHierarchy)
						{
							if (this.StudentManager.Tag.Target == this.StudentManager.Students[this.StudentID].Head)
							{
								this.PromptBar.Label[2].text = "Untag";
							}
							else
							{
								this.PromptBar.Label[2].text = "Tag";
							}
						}
						else
						{
							this.PromptBar.Label[2].text = string.Empty;
						}
					}
					else
					{
						this.PromptBar.Label[2].text = string.Empty;
					}
					this.PromptBar.Label[1].text = "Back";
					this.PromptBar.Label[3].text = "Interests";
					this.PromptBar.Label[6].text = "Reputation";
					this.PromptBar.UpdateButtons();
				}
			}
			else
			{
				StudentGlobals.SetStudentPhotographed(this.StudentID, true);
				this.PauseScreen.ServiceMenu.gameObject.SetActive(true);
				this.PauseScreen.ServiceMenu.UpdateList();
				this.PauseScreen.ServiceMenu.UpdateDesc();
				this.PauseScreen.ServiceMenu.Purchase();
				this.GettingInfo = false;
				base.gameObject.SetActive(false);
			}
		}
		if (Input.GetButtonDown("B"))
		{
			if (this.Gossiping || this.Distracting || this.MatchMaking || this.Targeting)
			{
				if (this.Targeting)
				{
					this.PauseScreen.Yandere.RPGCamera.enabled = true;
				}
				this.PauseScreen.Yandere.Interaction = YandereInteractionType.Bye;
				this.PauseScreen.Yandere.TalkTimer = 2f;
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.Distracting = false;
				this.MatchMaking = false;
				this.Gossiping = false;
				this.Targeting = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.CyberBullying || this.CyberStalking || this.FindingLocker)
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				if (this.FindingLocker)
				{
					this.PauseScreen.Yandere.RPGCamera.enabled = true;
				}
				this.FindingLocker = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.SendingHome || this.GettingInfo)
			{
				this.PauseScreen.ServiceMenu.gameObject.SetActive(true);
				this.PauseScreen.ServiceMenu.UpdateList();
				this.PauseScreen.ServiceMenu.UpdateDesc();
				base.gameObject.SetActive(false);
				this.SendingHome = false;
				this.GettingInfo = false;
			}
			else if (this.UsingLifeNote)
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				base.gameObject.SetActive(false);
				this.NoteWindow.gameObject.SetActive(true);
				this.UsingLifeNote = false;
			}
			else
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
		float t = Time.unscaledDeltaTime * 10f;
		float num = (float)((this.Row % 2 != 0) ? ((this.Row - 1) / 2) : (this.Row / 2));
		float b = 320f * num;
		this.PortraitGrid.localPosition = new Vector3(this.PortraitGrid.localPosition.x, Mathf.Lerp(this.PortraitGrid.localPosition.y, b, t), this.PortraitGrid.localPosition.z);
		this.Scrollbar.localPosition = new Vector3(this.Scrollbar.localPosition.x, Mathf.Lerp(this.Scrollbar.localPosition.y, 175f - 350f * (this.PortraitGrid.localPosition.y / 2880f), t), this.Scrollbar.localPosition.z);
		if (this.InputManager.TappedUp)
		{
			this.Row--;
			if (this.Row < 0)
			{
				this.Row = this.Rows - 1;
			}
			this.UpdateHighlight();
		}
		if (this.InputManager.TappedDown)
		{
			this.Row++;
			if (this.Row > this.Rows - 1)
			{
				this.Row = 0;
			}
			this.UpdateHighlight();
		}
		if (this.InputManager.TappedRight)
		{
			this.Column++;
			if (this.Column > this.Columns - 1)
			{
				this.Column = 0;
			}
			this.UpdateHighlight();
		}
		if (this.InputManager.TappedLeft)
		{
			this.Column--;
			if (this.Column < 0)
			{
				this.Column = this.Columns - 1;
			}
			this.UpdateHighlight();
		}
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x00154A80 File Offset: 0x00152E80
	public void UpdateHighlight()
	{
		this.StudentID = 1 + (this.Column + this.Row * this.Columns);
		if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
		{
			this.PromptBar.Label[0].text = "View Info";
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.Gossiping && (this.StudentID == 1 || this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID || this.JSON.Students[this.StudentID].Club == ClubType.Sports || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.CyberBullying && (this.JSON.Students[this.StudentID].Gender == 1 || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.CyberStalking && (StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.FindingLocker && (this.StudentID == 1 || this.StudentID > 85 || StudentGlobals.GetStudentDead(this.StudentID)))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.Distracting)
		{
			this.Dead = false;
			if (this.StudentManager.Students[this.StudentID] == null)
			{
				this.Dead = true;
			}
			if (this.Dead)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
			else if (this.StudentID == 1 || !this.StudentManager.Students[this.StudentID].Alive || this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID || StudentGlobals.GetStudentKidnapped(this.StudentID) || this.StudentManager.Students[this.StudentID].Tranquil || this.StudentManager.Students[this.StudentID].Teacher || this.StudentManager.Students[this.StudentID].Slave || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentManager.Students[this.StudentID].MyBento.Tampered || this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		if (this.MatchMaking && (this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.Targeting && (this.StudentID == 1 || this.StudentID > 97 || StudentGlobals.GetStudentDead(this.StudentID) || !this.StudentManager.Students[this.StudentID].gameObject.activeInHierarchy || this.StudentManager.Students[this.StudentID].InEvent || this.StudentManager.Students[this.StudentID].Tranquil))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.SendingHome)
		{
			Debug.Log("Highlighting student number " + this.StudentID);
			if (this.StudentManager.Students[this.StudentID] != null)
			{
				StudentScript studentScript = this.StudentManager.Students[this.StudentID];
				if (this.StudentID == 1 || StudentGlobals.GetStudentDead(this.StudentID) || (this.StudentID < 98 && studentScript.SentHome) || (this.StudentID > 97 || StudentGlobals.GetStudentSlave() == this.StudentID || (studentScript.Club == ClubType.MartialArts && studentScript.ClubAttire)) || (studentScript.Club == ClubType.Sports && studentScript.ClubAttire) || this.StudentManager.Students[this.StudentID].CameraReacting || !StudentGlobals.GetStudentPhotographed(this.StudentID))
				{
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.UpdateButtons();
				}
			}
		}
		if (this.GettingInfo)
		{
			if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.PromptBar.Label[0].text = "Get Info";
				this.PromptBar.UpdateButtons();
			}
		}
		if (this.UsingLifeNote)
		{
			if (this.StudentID == 1 || this.StudentID > 97 || (this.StudentID > 10 && this.StudentID < 21) || this.StudentPortraits[this.StudentID].DeathShadow.activeInHierarchy || (this.StudentManager.Students[this.StudentID] != null && !this.StudentManager.Students[this.StudentID].enabled))
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			else
			{
				this.PromptBar.Label[0].text = "Kill";
			}
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(-300f + (float)this.Column * 150f, 80f - (float)this.Row * 160f, this.Highlight.localPosition.z);
		this.UpdateNameLabel();
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x001551EC File Offset: 0x001535EC
	private void UpdateNameLabel()
	{
		if (this.StudentID > 97 || StudentGlobals.GetStudentPhotographed(this.StudentID) || this.GettingInfo)
		{
			this.NameLabel.text = this.JSON.Students[this.StudentID].Name;
		}
		else
		{
			this.NameLabel.text = "Unknown";
		}
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x00155258 File Offset: 0x00153658
	public IEnumerator UpdatePortraits()
	{
		if (this.Debugging)
		{
			Debug.Log("The Student Info Menu was instructed to get photos.");
		}
		for (int ID = 1; ID < 101; ID++)
		{
			if (this.Debugging)
			{
				Debug.Log("1 - We entered the loop.");
			}
			if (ID == 0)
			{
				this.StudentPortraits[ID].Portrait.mainTexture = this.InfoChan;
			}
			else
			{
				if (this.Debugging)
				{
					Debug.Log("2 - ID is not zero.");
				}
				if (!this.PortraitLoaded[ID])
				{
					if (this.Debugging)
					{
						Debug.Log("3 - PortraitLoaded is false.");
					}
					if (ID < 98)
					{
						if (this.Debugging)
						{
							Debug.Log("4 - ID is less than 98.");
						}
						if (StudentGlobals.GetStudentPhotographed(ID))
						{
							if (this.Debugging)
							{
								Debug.Log("5 - GetStudentPhotographed is true.");
							}
							string path = string.Concat(new string[]
							{
								"file:///",
								Application.streamingAssetsPath,
								"/Portraits/Student_",
								ID.ToString(),
								".png"
							});
							if (this.Debugging)
							{
								Debug.Log("Path is: " + path);
							}
							WWW www = new WWW(path);
							if (this.Debugging)
							{
								Debug.Log("Waiting for www to return.");
							}
							yield return www;
							if (this.Debugging)
							{
								Debug.Log("www has returned.");
							}
							if (www.error == null)
							{
								if (!StudentGlobals.GetStudentReplaced(ID))
								{
									this.StudentPortraits[ID].Portrait.mainTexture = www.texture;
								}
								else
								{
									this.StudentPortraits[ID].Portrait.mainTexture = this.BlankPortrait;
								}
							}
							else
							{
								this.StudentPortraits[ID].Portrait.mainTexture = this.UnknownPortrait;
							}
							this.PortraitLoaded[ID] = true;
						}
						else
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.UnknownPortrait;
						}
					}
					else if (ID == 98)
					{
						this.StudentPortraits[ID].Portrait.mainTexture = this.Counselor;
					}
					else if (ID == 99)
					{
						this.StudentPortraits[ID].Portrait.mainTexture = this.Headmaster;
					}
					else if (ID == 100)
					{
						this.StudentPortraits[ID].Portrait.mainTexture = this.InfoChan;
					}
				}
			}
			if (PlayerGlobals.GetStudentPantyShot(this.JSON.Students[ID].Name))
			{
				this.StudentPortraits[ID].Panties.SetActive(true);
			}
			this.StudentPortraits[ID].Friend.SetActive(PlayerGlobals.GetStudentFriend(ID));
			if (StudentGlobals.GetStudentDying(ID) || StudentGlobals.GetStudentDead(ID))
			{
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
			if (SceneManager.GetActiveScene().name == "SchoolScene" && this.StudentManager.Students[ID] != null && this.StudentManager.Students[ID].Tranquil)
			{
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
			if (StudentGlobals.GetStudentArrested(ID))
			{
				this.StudentPortraits[ID].PrisonBars.SetActive(true);
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
		}
		yield break;
	}

	// Token: 0x04002DDE RID: 11742
	public StudentManagerScript StudentManager;

	// Token: 0x04002DDF RID: 11743
	public InputManagerScript InputManager;

	// Token: 0x04002DE0 RID: 11744
	public PauseScreenScript PauseScreen;

	// Token: 0x04002DE1 RID: 11745
	public StudentInfoScript StudentInfo;

	// Token: 0x04002DE2 RID: 11746
	public NoteWindowScript NoteWindow;

	// Token: 0x04002DE3 RID: 11747
	public PromptBarScript PromptBar;

	// Token: 0x04002DE4 RID: 11748
	public JsonScript JSON;

	// Token: 0x04002DE5 RID: 11749
	public GameObject StudentPortrait;

	// Token: 0x04002DE6 RID: 11750
	public Texture UnknownPortrait;

	// Token: 0x04002DE7 RID: 11751
	public Texture BlankPortrait;

	// Token: 0x04002DE8 RID: 11752
	public Texture Headmaster;

	// Token: 0x04002DE9 RID: 11753
	public Texture Counselor;

	// Token: 0x04002DEA RID: 11754
	public Texture InfoChan;

	// Token: 0x04002DEB RID: 11755
	public Transform PortraitGrid;

	// Token: 0x04002DEC RID: 11756
	public Transform Highlight;

	// Token: 0x04002DED RID: 11757
	public Transform Scrollbar;

	// Token: 0x04002DEE RID: 11758
	public StudentPortraitScript[] StudentPortraits;

	// Token: 0x04002DEF RID: 11759
	public bool[] PortraitLoaded;

	// Token: 0x04002DF0 RID: 11760
	public UISprite[] DeathShadows;

	// Token: 0x04002DF1 RID: 11761
	public UISprite[] Friends;

	// Token: 0x04002DF2 RID: 11762
	public UISprite[] Panties;

	// Token: 0x04002DF3 RID: 11763
	public UITexture[] PrisonBars;

	// Token: 0x04002DF4 RID: 11764
	public UITexture[] Portraits;

	// Token: 0x04002DF5 RID: 11765
	public UILabel NameLabel;

	// Token: 0x04002DF6 RID: 11766
	public bool CyberBullying;

	// Token: 0x04002DF7 RID: 11767
	public bool CyberStalking;

	// Token: 0x04002DF8 RID: 11768
	public bool FindingLocker;

	// Token: 0x04002DF9 RID: 11769
	public bool UsingLifeNote;

	// Token: 0x04002DFA RID: 11770
	public bool GettingInfo;

	// Token: 0x04002DFB RID: 11771
	public bool MatchMaking;

	// Token: 0x04002DFC RID: 11772
	public bool Distracting;

	// Token: 0x04002DFD RID: 11773
	public bool SendingHome;

	// Token: 0x04002DFE RID: 11774
	public bool Gossiping;

	// Token: 0x04002DFF RID: 11775
	public bool Targeting;

	// Token: 0x04002E00 RID: 11776
	public bool Dead;

	// Token: 0x04002E01 RID: 11777
	public int[] SetSizes;

	// Token: 0x04002E02 RID: 11778
	public int StudentID;

	// Token: 0x04002E03 RID: 11779
	public int Column;

	// Token: 0x04002E04 RID: 11780
	public int Row;

	// Token: 0x04002E05 RID: 11781
	public int Set;

	// Token: 0x04002E06 RID: 11782
	public int Columns;

	// Token: 0x04002E07 RID: 11783
	public int Rows;

	// Token: 0x04002E08 RID: 11784
	public bool GrabbedPortraits;

	// Token: 0x04002E09 RID: 11785
	public bool Debugging;
}
