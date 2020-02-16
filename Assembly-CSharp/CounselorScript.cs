using System;
using UnityEngine;
using XInputDotNetPure;

// Token: 0x02000379 RID: 889
public class CounselorScript : MonoBehaviour
{
	// Token: 0x06001844 RID: 6212 RVA: 0x000D1038 File Offset: 0x000CF438
	private void Start()
	{
		this.CounselorWindow.localScale = Vector3.zero;
		this.CounselorWindow.gameObject.SetActive(false);
		this.CounselorOptions.SetActive(false);
		this.CounselorBar.SetActive(false);
		this.Reticle.SetActive(false);
		this.ExpelProgress.color = new Color(this.ExpelProgress.color.r, this.ExpelProgress.color.g, this.ExpelProgress.color.b, 0f);
		this.Chibi.localPosition = new Vector3(this.Chibi.localPosition.x, 250f + (float)StudentGlobals.ExpelProgress * -90f, this.Chibi.localPosition.z);
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x000D1124 File Offset: 0x000CF524
	private void Update()
	{
		if (this.LookAtPlayer)
		{
			if (this.TalkTimer < 1f)
			{
				this.TalkTimer = Mathf.MoveTowards(this.TalkTimer, 1f, Time.deltaTime);
				if (this.TalkTimer == 1f)
				{
					int num = UnityEngine.Random.Range(1, 3);
					this.CounselorSubtitle.text = this.CounselorGreetingText[num];
					this.MyAudio.clip = this.CounselorGreetingClips[num];
					this.MyAudio.Play();
				}
			}
			if (this.InputManager.TappedUp)
			{
				this.Selected--;
				if (this.Selected == 6)
				{
					this.Selected = 5;
				}
				this.UpdateHighlight();
			}
			if (this.InputManager.TappedDown)
			{
				this.Selected++;
				if (this.Selected == 6)
				{
					this.Selected = 7;
				}
				this.UpdateHighlight();
			}
			if (this.ShowWindow)
			{
				if (this.CounselorDoor.Darkness.color.a == 0f && Input.GetButtonDown("A"))
				{
					if (this.Selected == 7)
					{
						if (!this.CounselorDoor.Exit)
						{
							this.CounselorSubtitle.text = this.CounselorFarewellText;
							this.MyAudio.clip = this.CounselorFarewellClip;
							this.MyAudio.Play();
							this.CounselorDoor.FadeOut = true;
							this.CounselorDoor.FadeIn = false;
							this.CounselorDoor.Exit = true;
						}
					}
					else if (this.Labels[this.Selected].color.a == 1f)
					{
						if (this.Selected == 1)
						{
							SchemeGlobals.SetSchemeStage(1, 9);
							this.Schemes.UpdateInstructions();
						}
						else if (this.Selected == 2)
						{
							SchemeGlobals.SetSchemeStage(2, 4);
							this.Schemes.UpdateInstructions();
						}
						else if (this.Selected == 3)
						{
							SchemeGlobals.SetSchemeStage(3, 5);
							this.Schemes.UpdateInstructions();
						}
						else if (this.Selected == 4)
						{
							SchemeGlobals.SetSchemeStage(4, 8);
							this.Schemes.UpdateInstructions();
						}
						else if (this.Selected == 5)
						{
							SchemeGlobals.SetSchemeStage(5, 10);
							this.Schemes.UpdateInstructions();
						}
						this.CounselorSubtitle.text = this.CounselorReportText[this.Selected];
						this.MyAudio.clip = this.CounselorReportClips[this.Selected];
						this.MyAudio.Play();
						this.ShowWindow = false;
						this.Angry = true;
						this.CutsceneManager.Scheme = this.Selected;
						this.LectureID = this.Selected;
						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
						this.Busy = true;
					}
				}
			}
			else if (!this.Interrogating)
			{
				if (Input.GetButtonDown("A"))
				{
					this.MyAudio.Stop();
				}
				if (!this.MyAudio.isPlaying)
				{
					this.Timer += Time.deltaTime;
					if (this.Timer > 0.5f)
					{
						this.CounselorDoor.FadeOut = true;
						this.CounselorDoor.Exit = true;
						this.LookAtPlayer = false;
						this.UpdateList();
					}
				}
			}
		}
		else if (!this.Interrogating)
		{
		}
		if (this.ShowWindow)
		{
			this.CounselorWindow.localScale = Vector3.Lerp(this.CounselorWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
		}
		else if (this.CounselorWindow.localScale.x > 0.1f)
		{
			this.CounselorWindow.localScale = Vector3.Lerp(this.CounselorWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
		}
		else if (this.CounselorWindow.gameObject.activeInHierarchy)
		{
			this.CounselorWindow.localScale = Vector3.zero;
			this.CounselorWindow.gameObject.SetActive(false);
		}
		if (this.Lecturing)
		{
			Debug.Log("The guidance counselor is lecturing!");
			this.Chibi.localPosition = new Vector3(this.Chibi.localPosition.x, Mathf.Lerp(this.Chibi.localPosition.y, 250f + (float)StudentGlobals.ExpelProgress * -90f, Time.deltaTime * 3f), this.Chibi.localPosition.z);
			if (this.LecturePhase == 1)
			{
				Debug.Log("Lecture Phase 1.");
				this.LectureLabel.text = this.LectureIntro[this.LectureID];
				this.EndOfDayDarkness.color = new Color(this.EndOfDayDarkness.color.r, this.EndOfDayDarkness.color.g, this.EndOfDayDarkness.color.b, Mathf.MoveTowards(this.EndOfDayDarkness.color.a, 0f, Time.deltaTime));
				if (this.EndOfDayDarkness.color.a == 0f)
				{
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Continue";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					if (Input.GetButtonDown("A"))
					{
						this.LecturePhase++;
						this.PromptBar.ClearButtons();
						this.PromptBar.Show = false;
					}
				}
			}
			else if (this.LecturePhase == 2)
			{
				Debug.Log("Lecture Phase 2.");
				this.LectureLabel.color = new Color(this.LectureLabel.color.r, this.LectureLabel.color.g, this.LectureLabel.color.b, Mathf.MoveTowards(this.LectureLabel.color.a, 0f, Time.deltaTime));
				if (this.LectureLabel.color.a == 0f)
				{
					this.EndOfDay.TextWindow.SetActive(false);
					this.EndOfDay.EODCamera.GetComponent<AudioListener>().enabled = true;
					this.LectureSubtitle.text = this.CounselorLectureText[this.LectureID];
					this.MyAudio.clip = this.CounselorLectureClips[this.LectureID];
					this.MyAudio.Play();
					this.LecturePhase++;
				}
			}
			else if (this.LecturePhase == 3)
			{
				Debug.Log("Lecture Phase 3.");
				if (!this.MyAudio.isPlaying || Input.GetButtonDown("A"))
				{
					this.LectureSubtitle.text = this.RivalText[this.LectureID];
					this.MyAudio.clip = this.RivalClips[this.LectureID];
					this.MyAudio.Play();
					this.LecturePhase++;
				}
			}
			else if (this.LecturePhase == 4)
			{
				Debug.Log("Lecture Phase 4.");
				if (!this.MyAudio.isPlaying || Input.GetButtonDown("A"))
				{
					this.LectureSubtitle.text = string.Empty;
					if (StudentGlobals.ExpelProgress < 5)
					{
						this.LecturePhase++;
					}
					else
					{
						this.LecturePhase = 7;
						this.ExpelTimer = 0f;
					}
				}
			}
			else if (this.LecturePhase == 5)
			{
				Debug.Log("Lecture Phase 5.");
				this.ExpelProgress.color = new Color(this.ExpelProgress.color.r, this.ExpelProgress.color.g, this.ExpelProgress.color.b, Mathf.MoveTowards(this.ExpelProgress.color.a, 1f, Time.deltaTime));
				this.ExpelTimer += Time.deltaTime;
				if (this.ExpelTimer > 2f)
				{
					StudentGlobals.ExpelProgress++;
					this.LecturePhase++;
					Debug.Log("StudentGlobals.ExpelProgress is now: " + StudentGlobals.ExpelProgress);
				}
			}
			else if (this.LecturePhase == 6)
			{
				Debug.Log("Lecture Phase 6.");
				this.ExpelTimer += Time.deltaTime;
				if (this.ExpelTimer > 4f)
				{
					this.LecturePhase += 2;
				}
			}
			else if (this.LecturePhase == 7)
			{
				Debug.Log("Lecture Phase 7.");
				this.ExpelTimer += Time.deltaTime;
				if (this.ExpelTimer > 1f)
				{
					this.RIVAL.gameObject.SetActive(true);
				}
				if (this.ExpelTimer > 3f)
				{
					this.EXPELLED.gameObject.SetActive(true);
				}
				if (this.ExpelTimer > 5f)
				{
					this.RIVAL.color = new Color(this.RIVAL.color.r, this.RIVAL.color.g, this.RIVAL.color.b, this.RIVAL.color.a - Time.deltaTime);
					this.EXPELLED.color = new Color(this.EXPELLED.color.r, this.EXPELLED.color.g, this.EXPELLED.color.b, this.EXPELLED.color.a - Time.deltaTime);
				}
				if (this.ExpelTimer > 7f)
				{
					this.RIVAL.gameObject.SetActive(false);
					this.EXPELLED.gameObject.SetActive(false);
					this.LecturePhase++;
				}
			}
			else if (this.LecturePhase == 8)
			{
				Debug.Log("Lecture Phase 8.");
				this.ExpelProgress.color = new Color(this.ExpelProgress.color.r, this.ExpelProgress.color.g, this.ExpelProgress.color.b, Mathf.MoveTowards(this.ExpelProgress.color.a, 0f, Time.deltaTime));
				this.ExpelTimer += Time.deltaTime;
				if (this.ExpelTimer > 6f)
				{
					if ((StudentGlobals.ExpelProgress == 5 && !StudentGlobals.GetStudentExpelled(11) && this.EndOfDay.RivalEliminationMethod != RivalEliminationType.Expelled && this.StudentManager.Police.TranqCase.VictimID != 11) || this.StudentManager.Students[11].SentHome)
					{
						Debug.Log("Osana has now been expelled.");
						this.EndOfDay.RivalEliminationMethod = RivalEliminationType.Expelled;
						this.StudentManager.RivalEliminated = true;
						this.EndOfDayDarkness.color = new Color(this.EndOfDayDarkness.color.r, this.EndOfDayDarkness.color.g, this.EndOfDayDarkness.color.b, 0f);
						this.LectureLabel.color = new Color(this.LectureLabel.color.r, this.LectureLabel.color.g, this.LectureLabel.color.b, 0f);
						this.LecturePhase = 2;
						this.ExpelTimer = 0f;
						this.LectureID = 6;
					}
					else
					{
						Debug.Log("We are leaving the lecture and returning to gameplay.");
						this.EndOfDay.gameObject.SetActive(false);
						this.EndOfDay.Phase = 1;
						this.CutsceneManager.Phase++;
						this.Lecturing = false;
						this.Yandere.PauseScreen.Schemes.SchemeManager.enabled = false;
						this.Yandere.MainCamera.gameObject.SetActive(true);
						this.Yandere.gameObject.SetActive(true);
						this.StudentManager.ComeBack();
						this.StudentManager.Students[this.StudentManager.RivalID].IdleAnim = this.StudentManager.Students[this.StudentManager.RivalID].BulliedIdleAnim;
						this.StudentManager.Students[this.StudentManager.RivalID].WalkAnim = this.StudentManager.Students[this.StudentManager.RivalID].BulliedWalkAnim;
						if (this.LectureID == 6 && this.StudentManager.Students[10] != null)
						{
							StudentScript studentScript = this.StudentManager.Students[10];
							Debug.Log("Osana is gone, so Raibaru's routine has to change.");
							ScheduleBlock scheduleBlock = studentScript.ScheduleBlocks[4];
							scheduleBlock.destination = "Mourn";
							scheduleBlock.action = "Mourn";
							ScheduleBlock scheduleBlock2 = studentScript.ScheduleBlocks[5];
							scheduleBlock2.destination = "Seat";
							scheduleBlock2.action = "Sit";
							ScheduleBlock scheduleBlock3 = studentScript.ScheduleBlocks[6];
							scheduleBlock3.destination = "Locker";
							scheduleBlock3.action = "Shoes";
							ScheduleBlock scheduleBlock4 = studentScript.ScheduleBlocks[7];
							scheduleBlock4.destination = "Exit";
							scheduleBlock4.action = "Exit";
							ScheduleBlock scheduleBlock5 = studentScript.ScheduleBlocks[8];
							scheduleBlock5.destination = "Exit";
							scheduleBlock5.action = "Exit";
							ScheduleBlock scheduleBlock6 = studentScript.ScheduleBlocks[9];
							scheduleBlock6.destination = "Exit";
							scheduleBlock6.action = "Exit";
							studentScript.TargetDistance = 0.5f;
							studentScript.IdleAnim = studentScript.BulliedIdleAnim;
							studentScript.WalkAnim = studentScript.BulliedWalkAnim;
							studentScript.OriginalIdleAnim = studentScript.IdleAnim;
							studentScript.Pathfinding.speed = 1f;
							studentScript.GetDestinations();
						}
						this.LectureID = 0;
					}
				}
			}
		}
		if (!this.MyAudio.isPlaying)
		{
			this.CounselorSubtitle.text = string.Empty;
		}
		if (this.Interrogating)
		{
			this.UpdateInterrogation();
		}
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x000D2074 File Offset: 0x000D0474
	public void Talk()
	{
		this.MyAnimation.CrossFade("CounselorComputerAttention", 1f);
		this.ChinTimer = 0f;
		this.Yandere.TargetStudent = this.Student;
		this.TalkTimer = 0f;
		this.StudentManager.DisablePrompts();
		this.CounselorWindow.gameObject.SetActive(true);
		this.LookAtPlayer = true;
		this.ShowWindow = true;
		this.Yandere.ShoulderCamera.OverShoulder = true;
		this.Yandere.WeaponMenu.KeyboardShow = false;
		this.Yandere.Obscurance.enabled = false;
		this.Yandere.WeaponMenu.Show = false;
		this.Yandere.YandereVision = false;
		this.Yandere.CanMove = false;
		this.Yandere.Talking = true;
		this.PromptBar.ClearButtons();
		this.PromptBar.Label[0].text = "Accept";
		this.PromptBar.Label[4].text = "Choose";
		this.PromptBar.UpdateButtons();
		this.PromptBar.Show = true;
		this.UpdateList();
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000D21A8 File Offset: 0x000D05A8
	private void UpdateList()
	{
		for (int i = 1; i < this.Labels.Length; i++)
		{
			UILabel uilabel = this.Labels[i];
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		}
		if (this.StudentManager.Students[11] != null)
		{
			if (SchemeGlobals.GetSchemeStage(1) == 8)
			{
				UILabel uilabel2 = this.Labels[1];
				uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 1f);
			}
			if (SchemeGlobals.GetSchemeStage(2) == 3)
			{
				UILabel uilabel3 = this.Labels[2];
				uilabel3.color = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, 1f);
			}
			if (SchemeGlobals.GetSchemeStage(3) == 4)
			{
				UILabel uilabel4 = this.Labels[3];
				uilabel4.color = new Color(uilabel4.color.r, uilabel4.color.g, uilabel4.color.b, 1f);
			}
			if (SchemeGlobals.GetSchemeStage(4) == 7)
			{
				UILabel uilabel5 = this.Labels[4];
				uilabel5.color = new Color(uilabel5.color.r, uilabel5.color.g, uilabel5.color.b, 1f);
			}
			if (SchemeGlobals.GetSchemeStage(5) == 9)
			{
				UILabel uilabel6 = this.Labels[5];
				uilabel6.color = new Color(uilabel6.color.r, uilabel6.color.g, uilabel6.color.b, 1f);
			}
		}
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x000D23E0 File Offset: 0x000D07E0
	private void UpdateHighlight()
	{
		if (this.Selected < 1)
		{
			this.Selected = 7;
		}
		else if (this.Selected > 7)
		{
			this.Selected = 1;
		}
		this.Highlight.transform.localPosition = new Vector3(this.Highlight.transform.localPosition.x, 200f - 50f * (float)this.Selected, this.Highlight.transform.localPosition.z);
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000D2470 File Offset: 0x000D0870
	private void LateUpdate()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			if (this.Angry)
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 100f, Time.deltaTime * 10f);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 100f, Time.deltaTime * 10f);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 0f, Time.deltaTime * 10f);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 100f, Time.deltaTime * 10f);
			}
			else if (this.Stern)
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 0f, Time.deltaTime * 10f);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 100f, Time.deltaTime * 10f);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 0f, Time.deltaTime * 10f);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 0f, Time.deltaTime * 10f);
			}
			else if (this.Sad)
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 100f, Time.deltaTime * 10f);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 0f, Time.deltaTime * 10f);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 100f, Time.deltaTime * 10f);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 0f, Time.deltaTime * 10f);
			}
			else
			{
				this.BS_SadMouth = Mathf.Lerp(this.BS_SadMouth, 0f, Time.deltaTime * 10f);
				this.BS_MadBrow = Mathf.Lerp(this.BS_MadBrow, 0f, Time.deltaTime * 10f);
				this.BS_SadBrow = Mathf.Lerp(this.BS_SadBrow, 0f, Time.deltaTime * 10f);
				this.BS_AngryEyes = Mathf.Lerp(this.BS_AngryEyes, 0f, Time.deltaTime * 10f);
			}
			this.Face.SetBlendShapeWeight(1, this.BS_SadMouth);
			this.Face.SetBlendShapeWeight(5, this.BS_MadBrow);
			this.Face.SetBlendShapeWeight(6, this.BS_SadBrow);
			this.Face.SetBlendShapeWeight(9, this.BS_AngryEyes);
			if (this.MyAudio.isPlaying)
			{
				if (this.InterrogationPhase != 6)
				{
					this.MouthTimer += Time.deltaTime;
					if (this.MouthTimer > this.TimerLimit)
					{
						this.MouthTarget = UnityEngine.Random.Range(0f, 100f);
						this.MouthTimer = 0f;
					}
					this.MouthOpen = Mathf.Lerp(this.MouthOpen, this.MouthTarget, Time.deltaTime * this.TalkSpeed);
				}
				else
				{
					this.MouthOpen = Mathf.Lerp(this.MouthOpen, 0f, Time.deltaTime * this.TalkSpeed);
				}
			}
			else
			{
				this.MouthOpen = Mathf.Lerp(this.MouthOpen, 0f, Time.deltaTime * this.TalkSpeed);
			}
			this.Face.SetBlendShapeWeight(2, this.MouthOpen);
			this.LookAtTarget = Vector3.Lerp(this.LookAtTarget, (!this.LookAtPlayer) ? this.Default.position : this.Yandere.Head.position, Time.deltaTime * 2f);
			this.Head.LookAt(this.LookAtTarget);
		}
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000D286C File Offset: 0x000D0C6C
	public void Quit()
	{
		this.Yandere.Senpai = this.StudentManager.Students[1].transform;
		this.Yandere.DetectionPanel.alpha = 1f;
		this.Yandere.RPGCamera.mouseSpeed = 8f;
		this.Yandere.HUD.alpha = 1f;
		this.Yandere.HeartRate.gameObject.SetActive(true);
		this.Yandere.CannotRecover = false;
		this.Yandere.Noticed = false;
		this.Yandere.Talking = true;
		this.Yandere.ShoulderCamera.GoingToCounselor = false;
		this.Yandere.ShoulderCamera.HUD.SetActive(true);
		this.Yandere.ShoulderCamera.Noticed = false;
		this.Yandere.ShoulderCamera.enabled = true;
		this.Yandere.TargetStudent = this.Student;
		if (!this.Yandere.Jukebox.FullSanity.isPlaying)
		{
			this.Yandere.Jukebox.FullSanity.volume = 0f;
			this.Yandere.Jukebox.HalfSanity.volume = 0f;
			this.Yandere.Jukebox.NoSanity.volume = 0f;
			this.Yandere.Jukebox.FullSanity.Play();
			this.Yandere.Jukebox.HalfSanity.Play();
			this.Yandere.Jukebox.NoSanity.Play();
		}
		this.Yandere.transform.position = new Vector3(-21.5f, 0f, 8f);
		this.Yandere.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		this.Yandere.ShoulderCamera.OverShoulder = false;
		this.CounselorBar.SetActive(false);
		this.StudentManager.EnablePrompts();
		this.Laptop.SetActive(true);
		this.LookAtPlayer = false;
		this.ShowWindow = false;
		this.TalkTimer = 1f;
		this.Patience = 0;
		this.Stern = false;
		this.Angry = false;
		this.Sad = false;
		this.PromptBar.ClearButtons();
		this.PromptBar.Show = false;
		this.StudentManager.ComeBack();
		this.StudentManager.Reputation.UpdateRep();
		Physics.SyncTransforms();
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x000D2AF8 File Offset: 0x000D0EF8
	private void UpdateInterrogation()
	{
		if (this.VibrationCheck)
		{
			this.VibrationTimer = Mathf.MoveTowards(this.VibrationTimer, 0f, Time.deltaTime);
			if (this.VibrationTimer == 0f)
			{
				GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
				this.VibrationCheck = false;
			}
		}
		this.Timer += Time.deltaTime;
		if (Input.GetButtonDown("A") && this.InterrogationPhase != 4)
		{
			this.Timer += 20f;
		}
		if (this.InterrogationPhase == 0)
		{
			if (this.Timer > 1f || Input.GetButtonDown("A"))
			{
				Debug.Log("Previous Punishments: " + CounselorGlobals.CounselorPunishments);
				this.Patience -= CounselorGlobals.CounselorPunishments;
				if (this.Patience < -6)
				{
					this.Patience = -6;
				}
				this.GenkaChibi.transform.localPosition = new Vector3(0f, (float)(90 * this.Patience), 0f);
				this.Yandere.MainCamera.transform.eulerAngles = this.CameraTarget.eulerAngles;
				this.Yandere.MainCamera.transform.position = this.CameraTarget.position;
				this.Yandere.MainCamera.transform.Translate(Vector3.forward * -1f);
				if (CounselorGlobals.CounselorVisits < 3)
				{
					CounselorGlobals.CounselorVisits++;
				}
				if (CounselorGlobals.CounselorTape == 0)
				{
					this.CounselorOption[4].Label.color = new Color(0f, 0f, 0f, 0.5f);
				}
				else
				{
					this.CounselorOption[4].Label.color = new Color(0f, 0f, 0f, 1f);
					this.CounselorOption[4].Label.text = "Blame Delinquents";
				}
				if (this.Yandere.Subtitle.CurrentClip != null)
				{
					UnityEngine.Object.Destroy(this.Yandere.Subtitle.CurrentClip);
				}
				this.GenkaChibi.mainTexture = this.AnnoyedChibi;
				this.CounselorBar.SetActive(true);
				this.Subtitle.Label.text = string.Empty;
				this.InterrogationPhase++;
				Time.timeScale = 1f;
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 1)
		{
			this.Yandere.Police.Darkness.color -= new Color(0f, 0f, 0f, Time.deltaTime);
			this.Yandere.MainCamera.transform.position = Vector3.Lerp(this.Yandere.MainCamera.transform.position, this.CameraTarget.position, this.Timer * Time.deltaTime * 0.5f);
			if (this.Timer > 5f || Input.GetButtonDown("A"))
			{
				this.Yandere.MainCamera.transform.position = this.CameraTarget.position;
				this.MyAudio.clip = this.GreetingClips[CounselorGlobals.CounselorVisits];
				this.CounselorSubtitle.text = this.Greetings[CounselorGlobals.CounselorVisits];
				this.Yandere.Police.Darkness.color = new Color(0f, 0f, 0f, 0f);
				this.InterrogationPhase++;
				this.MyAudio.Play();
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 2)
		{
			if (Input.GetButtonDown("A"))
			{
				this.MyAudio.Stop();
			}
			if (this.Timer > this.MyAudio.clip.length + 0.5f)
			{
				if (this.Crime == StudentWitnessType.Blood || this.Crime == StudentWitnessType.BloodAndInsanity)
				{
					this.MyAudio.clip = this.BloodLectureClips[CounselorGlobals.BloodVisits];
					this.CounselorSubtitle.text = this.BloodLectures[CounselorGlobals.BloodVisits];
					if (CounselorGlobals.BloodVisits < 2)
					{
						CounselorGlobals.BloodVisits++;
					}
					this.CrimeID = 1;
				}
				else if (this.Crime == StudentWitnessType.Insanity || this.Crime == StudentWitnessType.CleaningItem || this.Crime == StudentWitnessType.HoldingBloodyClothing || this.Crime == StudentWitnessType.Poisoning)
				{
					this.MyAudio.clip = this.InsanityLectureClips[CounselorGlobals.InsanityVisits];
					this.CounselorSubtitle.text = this.InsanityLectures[CounselorGlobals.InsanityVisits];
					if (CounselorGlobals.InsanityVisits < 2)
					{
						CounselorGlobals.InsanityVisits++;
					}
					this.CrimeID = 2;
				}
				else if (this.Crime == StudentWitnessType.Lewd)
				{
					this.MyAudio.clip = this.LewdLectureClips[CounselorGlobals.LewdVisits];
					this.CounselorSubtitle.text = this.LewdLectures[CounselorGlobals.LewdVisits];
					if (CounselorGlobals.LewdVisits < 2)
					{
						CounselorGlobals.LewdVisits++;
					}
					this.CrimeID = 3;
				}
				else if (this.Crime == StudentWitnessType.Theft || this.Crime == StudentWitnessType.Pickpocketing)
				{
					this.MyAudio.clip = this.TheftLectureClips[CounselorGlobals.TheftVisits];
					this.CounselorSubtitle.text = this.TheftLectures[CounselorGlobals.TheftVisits];
					if (CounselorGlobals.TheftVisits < 2)
					{
						CounselorGlobals.TheftVisits++;
					}
					this.CrimeID = 4;
				}
				else if (this.Crime == StudentWitnessType.Trespassing)
				{
					this.MyAudio.clip = this.TrespassLectureClips[CounselorGlobals.TrespassVisits];
					this.CounselorSubtitle.text = this.TrespassLectures[CounselorGlobals.TrespassVisits];
					if (CounselorGlobals.TrespassVisits < 2)
					{
						CounselorGlobals.TrespassVisits++;
					}
					this.CrimeID = 5;
				}
				else if (this.Crime == StudentWitnessType.Weapon || this.Crime == StudentWitnessType.WeaponAndBlood || this.Crime == StudentWitnessType.WeaponAndInsanity || this.Crime == StudentWitnessType.WeaponAndBloodAndInsanity)
				{
					this.MyAudio.clip = this.WeaponLectureClips[CounselorGlobals.WeaponVisits];
					this.CounselorSubtitle.text = this.WeaponLectures[CounselorGlobals.WeaponVisits];
					if (CounselorGlobals.WeaponVisits < 2)
					{
						CounselorGlobals.WeaponVisits++;
					}
					this.CrimeID = 6;
				}
				this.InterrogationPhase++;
				this.MyAudio.Play();
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 3)
		{
			if (Input.GetButtonDown("A"))
			{
				this.MyAudio.Stop();
			}
			if (this.Timer > this.MyAudio.clip.length + 0.5f)
			{
				for (int i = 1; i < 7; i++)
				{
					this.CounselorOption[i].transform.localPosition = this.CounselorOption[i].OriginalPosition;
					this.CounselorOption[i].Sprite.color = this.CounselorOption[i].OriginalColor;
					this.CounselorOption[i].transform.localScale = new Vector3(0.9f, 0.9f, 1f);
					this.CounselorOption[i].gameObject.SetActive(true);
					this.CounselorOption[i].Clicked = false;
				}
				this.Yandere.CharacterAnimation["f02_countdown_00"].speed = 1f;
				this.Yandere.CharacterAnimation.Play("f02_countdown_00");
				this.Yandere.transform.position = new Vector3(-27.818f, 0f, 12f);
				this.Yandere.transform.eulerAngles = new Vector3(0f, -90f, 0f);
				this.Yandere.MainCamera.transform.position = new Vector3(-28f, 1.1f, 12f);
				this.Yandere.MainCamera.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.Reticle.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.CounselorOptions.SetActive(true);
				this.CounselorBar.SetActive(false);
				this.CounselorSubtitle.text = string.Empty;
				this.MyAudio.clip = this.Countdown;
				this.MyAudio.Play();
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				this.InterrogationPhase++;
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 4)
		{
			this.Yandere.MainCamera.transform.Translate(Vector3.forward * Time.deltaTime * 0.01f);
			this.CounselorOptions.transform.localEulerAngles += new Vector3(0f, 0f, Time.deltaTime * -36f);
			if (this.InputDevice.Type == InputDeviceType.Gamepad)
			{
				this.Reticle.SetActive(true);
				Cursor.visible = false;
				this.Reticle.transform.localPosition += new Vector3(Input.GetAxis("Horizontal") * 20f, Input.GetAxis("Vertical") * 20f, 0f);
				if (this.Reticle.transform.localPosition.x > 975f)
				{
					this.Reticle.transform.localPosition = new Vector3(975f, this.Reticle.transform.localPosition.y, this.Reticle.transform.localPosition.z);
				}
				if (this.Reticle.transform.localPosition.x < -975f)
				{
					this.Reticle.transform.localPosition = new Vector3(-975f, this.Reticle.transform.localPosition.y, this.Reticle.transform.localPosition.z);
				}
				if (this.Reticle.transform.localPosition.y > 525f)
				{
					this.Reticle.transform.localPosition = new Vector3(this.Reticle.transform.localPosition.x, 525f, this.Reticle.transform.localPosition.z);
				}
				if (this.Reticle.transform.localPosition.y < -525f)
				{
					this.Reticle.transform.localPosition = new Vector3(this.Reticle.transform.localPosition.x, -525f, this.Reticle.transform.localPosition.z);
				}
			}
			else
			{
				this.Reticle.SetActive(false);
				Cursor.visible = true;
			}
			for (int j = 1; j < 7; j++)
			{
				this.CounselorOption[j].transform.eulerAngles = new Vector3(this.CounselorOption[j].transform.eulerAngles.x, this.CounselorOption[j].transform.eulerAngles.y, 0f);
				if (this.CounselorOption[j].Clicked || (this.CounselorOption[j].Sprite.color != this.CounselorOption[j].OriginalColor && Input.GetButtonDown("A")))
				{
					for (int k = 1; k < 7; k++)
					{
						if (k != j)
						{
							this.CounselorOption[k].gameObject.SetActive(false);
						}
					}
					this.Yandere.CharacterAnimation["f02_countdown_00"].time = 10f;
					this.MyAudio.clip = this.Choice;
					this.MyAudio.pitch = 1f;
					this.MyAudio.Play();
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
					this.Reticle.SetActive(false);
					this.InterrogationPhase++;
					this.Answer = j;
					this.Timer = 0f;
				}
			}
			if (this.Timer > 10f)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				this.Reticle.SetActive(false);
				this.SilentTreatment = true;
				this.InterrogationPhase++;
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 5)
		{
			int l = 1;
			if (this.SilentTreatment)
			{
				this.CounselorOptions.transform.localScale += new Vector3(Time.deltaTime * 2f, Time.deltaTime * 2f, Time.deltaTime * 2f);
				while (l < 7)
				{
					this.CounselorOption[l].transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
					l++;
				}
			}
			if (this.Timer > 3f || Input.GetButtonDown("A"))
			{
				this.CounselorOptions.transform.localScale = new Vector3(1f, 1f, 1f);
				this.CounselorOptions.SetActive(false);
				this.CounselorBar.SetActive(true);
				this.Yandere.transform.position = new Vector3(-27.51f, 0f, 12f);
				this.Yandere.MainCamera.transform.position = this.CameraTarget.position;
				this.Yandere.MainCamera.transform.eulerAngles = this.CameraTarget.eulerAngles;
				if (this.SilentTreatment)
				{
					this.MyAudio.clip = this.Silence;
					this.CounselorSubtitle.text = "...";
				}
				else if (this.Answer == 1)
				{
					this.MyAudio.clip = this.ExcuseClips[this.CrimeID];
					this.CounselorSubtitle.text = this.Excuses[this.CrimeID];
					if (this.CrimeID == 1)
					{
						CounselorGlobals.BloodExcuseUsed++;
					}
					else if (this.CrimeID == 2)
					{
						CounselorGlobals.InsanityExcuseUsed++;
					}
					else if (this.CrimeID == 3)
					{
						CounselorGlobals.LewdExcuseUsed++;
					}
					else if (this.CrimeID == 4)
					{
						CounselorGlobals.TheftExcuseUsed++;
					}
					else if (this.CrimeID == 5)
					{
						CounselorGlobals.TrespassExcuseUsed++;
					}
					else if (this.CrimeID == 6)
					{
						CounselorGlobals.WeaponExcuseUsed++;
					}
				}
				else if (this.Answer == 2)
				{
					this.MyAudio.clip = this.ApologyClip;
					this.CounselorSubtitle.text = this.Apology;
					CounselorGlobals.ApologiesUsed++;
				}
				else if (this.Answer == 3)
				{
					this.MyAudio.clip = this.LieClips[this.CrimeID];
					this.CounselorSubtitle.text = this.Lies[this.CrimeID];
				}
				else if (this.Answer == 4)
				{
					this.MyAudio.clip = this.DelinquentClips[this.CrimeID];
					this.CounselorSubtitle.text = this.Delinquents[this.CrimeID];
				}
				else if (this.Answer == 5)
				{
					this.MyAudio.clip = this.FlirtClip;
					this.CounselorSubtitle.text = this.Flirt;
				}
				else if (this.Answer == 6)
				{
					this.MyAudio.clip = this.ThreatenClip;
					this.CounselorSubtitle.text = this.Threaten;
				}
				this.Yandere.CharacterAnimation.Play("f02_sit_00");
				this.InterrogationPhase++;
				this.MyAudio.Play();
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 6)
		{
			if (this.Answer == 6)
			{
				this.Yandere.Sanity = Mathf.MoveTowards(this.Yandere.Sanity, 0f, Time.deltaTime * 7.5f);
				this.Rumble.volume += Time.deltaTime * 0.075f;
			}
			if (this.Timer > this.MyAudio.clip.length + 0.5f || Input.GetButtonDown("A"))
			{
				if (this.SilentTreatment)
				{
					int num = UnityEngine.Random.Range(0, 3);
					this.MyAudio.clip = this.SilentClips[num];
					this.CounselorSubtitle.text = this.Silents[num];
					this.Patience--;
				}
				else if (this.Answer == 1)
				{
					if (this.CrimeID == 1)
					{
						Debug.Log("The player's crime is Bloodiness.");
					}
					else if (this.CrimeID == 2)
					{
						Debug.Log("The player's crime is Insanity.");
					}
					else if (this.CrimeID == 3)
					{
						Debug.Log("The player's crime is Lewdness.");
					}
					else if (this.CrimeID == 4)
					{
						Debug.Log("The player's crime is Theft.");
					}
					else if (this.CrimeID == 5)
					{
						Debug.Log("The player's crime is Trespassing.");
					}
					else if (this.CrimeID == 6)
					{
						Debug.Log("The player's crime is Weaponry.");
					}
					Debug.Log("The player has chosen to use an exuse.");
					bool flag = false;
					if ((this.CrimeID == 1 && CounselorGlobals.BloodExcuseUsed > 1) || (this.CrimeID == 2 && CounselorGlobals.InsanityExcuseUsed > 1) || (this.CrimeID == 3 && CounselorGlobals.LewdExcuseUsed > 1) || (this.CrimeID == 4 && CounselorGlobals.TheftExcuseUsed > 1) || (this.CrimeID == 5 && CounselorGlobals.TrespassExcuseUsed > 1) || (this.CrimeID == 6 && CounselorGlobals.WeaponExcuseUsed > 1))
					{
						Debug.Log("Yandere-chan has already used this excuse before.");
						flag = true;
					}
					if (!flag)
					{
						Debug.Log("Yandere-chan's excuse is not invalid!");
						this.MyAudio.clip = this.AcceptExcuseClips[this.CrimeID];
						this.CounselorSubtitle.text = this.AcceptExcuses[this.CrimeID];
						this.MyAnimation.CrossFade("CounselorRelief", 1f);
						this.Stern = false;
						this.Patience = 1;
					}
					else
					{
						Debug.Log("Yandere-chan's excuse has been deemed invalid.");
						int num2 = UnityEngine.Random.Range(0, 3);
						this.MyAudio.clip = this.RejectExcuseClips[num2];
						this.CounselorSubtitle.text = this.RejectExcuses[num2];
						this.MyAnimation.CrossFade("CounselorAnnoyed");
						this.Angry = true;
						this.Patience--;
					}
				}
				else if (this.Answer == 2)
				{
					if (CounselorGlobals.ApologiesUsed == 1)
					{
						this.MyAudio.clip = this.AcceptApologyClip;
						this.CounselorSubtitle.text = this.AcceptApology;
						this.MyAnimation.CrossFade("CounselorRelief", 1f);
						this.Stern = false;
						this.Patience = 1;
					}
					else
					{
						int num3 = UnityEngine.Random.Range(0, 3);
						this.MyAudio.clip = this.RejectApologyClips[num3];
						this.CounselorSubtitle.text = this.RejectApologies[num3];
						this.MyAnimation.CrossFade("CounselorAnnoyed");
						this.Patience--;
					}
				}
				else if (this.Answer == 3)
				{
					int num4 = UnityEngine.Random.Range(0, 5);
					this.MyAudio.clip = this.RejectLieClips[num4];
					this.CounselorSubtitle.text = this.RejectLies[num4];
					this.MyAnimation.CrossFade("CounselorAnnoyed");
					this.Angry = true;
					this.Patience--;
				}
				else if (this.Answer == 4)
				{
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num5 = 5;
					if (StudentGlobals.GetStudentDead(76) && StudentGlobals.GetStudentDead(77) && StudentGlobals.GetStudentDead(78) && StudentGlobals.GetStudentDead(79) && StudentGlobals.GetStudentDead(80))
					{
						flag4 = true;
					}
					else if (StudentGlobals.GetStudentExpelled(76) && StudentGlobals.GetStudentExpelled(77) && StudentGlobals.GetStudentExpelled(78) && StudentGlobals.GetStudentExpelled(79) && StudentGlobals.GetStudentExpelled(80))
					{
						flag3 = true;
					}
					else
					{
						if (this.StudentManager.Students[76] == null)
						{
							num5--;
						}
						else if (!this.StudentManager.Students[76].gameObject.activeInHierarchy)
						{
							num5--;
						}
						if (this.StudentManager.Students[77] == null)
						{
							num5--;
						}
						else if (!this.StudentManager.Students[77].gameObject.activeInHierarchy)
						{
							num5--;
						}
						if (this.StudentManager.Students[78] == null)
						{
							num5--;
						}
						else if (!this.StudentManager.Students[78].gameObject.activeInHierarchy)
						{
							num5--;
						}
						if (this.StudentManager.Students[79] == null)
						{
							num5--;
						}
						else if (!this.StudentManager.Students[79].gameObject.activeInHierarchy)
						{
							num5--;
						}
						if (this.StudentManager.Students[80] == null)
						{
							num5--;
						}
						else if (!this.StudentManager.Students[80].gameObject.activeInHierarchy)
						{
							num5--;
						}
						if (num5 == 0)
						{
							flag2 = true;
						}
					}
					bool flag5 = false;
					if ((this.CrimeID == 1 && CounselorGlobals.BloodBlameUsed > 1) || (this.CrimeID == 2 && CounselorGlobals.InsanityBlameUsed > 1) || (this.CrimeID == 3 && CounselorGlobals.LewdBlameUsed > 1) || (this.CrimeID == 4 && CounselorGlobals.TheftBlameUsed > 1) || (this.CrimeID == 5 && CounselorGlobals.TrespassBlameUsed > 1) || (this.CrimeID == 6 && CounselorGlobals.WeaponBlameUsed > 1))
					{
						flag5 = true;
					}
					if (flag4)
					{
						this.MyAudio.clip = this.DelinquentsDeadClip;
						this.CounselorSubtitle.text = this.DelinquentsDead;
						this.MyAnimation.CrossFade("CounselorAnnoyed");
						this.Angry = true;
						this.Patience--;
					}
					else if (flag3)
					{
						this.MyAudio.clip = this.DelinquentsExpelledClip;
						this.CounselorSubtitle.text = this.DelinquentsExpelled;
						this.MyAnimation.CrossFade("CounselorAnnoyed");
						this.Patience--;
					}
					else if (flag2)
					{
						this.MyAudio.clip = this.DelinquentsGoneClip;
						this.CounselorSubtitle.text = this.DelinquentsGone;
						this.MyAnimation.CrossFade("CounselorAnnoyed");
						this.Patience--;
					}
					else if (!flag5)
					{
						if (this.CrimeID == 1)
						{
							Debug.Log("Banning weapons.");
							CounselorGlobals.WeaponsBanned++;
						}
						this.MyAudio.clip = this.AcceptBlameClips[this.CrimeID];
						this.CounselorSubtitle.text = this.AcceptBlames[this.CrimeID];
						this.MyAnimation.CrossFade("CounselorSad", 1f);
						this.Stern = false;
						this.Sad = true;
						this.Patience = 1;
						CounselorGlobals.DelinquentPunishments++;
						if (this.CrimeID == 1)
						{
							CounselorGlobals.BloodBlameUsed++;
						}
						else if (this.CrimeID == 2)
						{
							CounselorGlobals.InsanityBlameUsed++;
						}
						else if (this.CrimeID == 3)
						{
							CounselorGlobals.LewdBlameUsed++;
						}
						else if (this.CrimeID == 4)
						{
							CounselorGlobals.TheftBlameUsed++;
						}
						else if (this.CrimeID == 5)
						{
							CounselorGlobals.TrespassBlameUsed++;
						}
						else if (this.CrimeID == 6)
						{
							CounselorGlobals.WeaponBlameUsed++;
						}
						if (CounselorGlobals.DelinquentPunishments > 5)
						{
							this.MustExpelDelinquents = true;
						}
					}
					else
					{
						int num6 = UnityEngine.Random.Range(0, 3);
						this.MyAudio.clip = this.RejectBlameClips[num6];
						this.CounselorSubtitle.text = this.RejectBlames[num6];
						this.MyAnimation.CrossFade("CounselorAnnoyed");
						this.Patience--;
					}
				}
				else if (this.Answer == 5)
				{
					int num7 = UnityEngine.Random.Range(0, 3);
					this.MyAudio.clip = this.RejectFlirtClips[num7];
					this.CounselorSubtitle.text = this.RejectFlirts[num7];
					this.MyAnimation.CrossFade("CounselorAnnoyed");
					this.Angry = true;
					this.Patience--;
				}
				else if (this.Answer == 6)
				{
					this.MyAudio.clip = this.RejectThreatClip;
					this.CounselorSubtitle.text = this.RejectThreat;
					this.MyAnimation.CrossFade("CounselorAnnoyed");
					this.InterrogationPhase += 2;
					this.Patience = -6;
					this.Angry = true;
				}
				if (this.Patience < -6)
				{
					this.Patience = -6;
				}
				if (this.Patience == 1)
				{
					this.GenkaChibi.mainTexture = this.HappyChibi;
				}
				else if (this.Patience == -6)
				{
					this.GenkaChibi.mainTexture = this.MadChibi;
				}
				else
				{
					this.GenkaChibi.mainTexture = this.AnnoyedChibi;
				}
				this.InterrogationPhase++;
				this.MyAudio.Play();
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 7)
		{
			if (this.Timer > this.MyAudio.clip.length + 0.5f || Input.GetButtonDown("A"))
			{
				if (this.Patience < 0)
				{
					int num8 = UnityEngine.Random.Range(0, 3);
					this.MyAudio.clip = this.BadClosingClips[num8];
					this.CounselorSubtitle.text = this.BadClosings[num8];
					this.MyAnimation.CrossFade("CounselorArmsCrossed", 1f);
					this.InterrogationPhase += 2;
				}
				else
				{
					if (this.MustExpelDelinquents)
					{
						this.MyAudio.clip = this.ExpelDelinquentsClip;
						this.CounselorSubtitle.text = this.ExpelDelinquents;
						this.MustExpelDelinquents = false;
						StudentGlobals.SetStudentExpelled(76, true);
						StudentGlobals.SetStudentExpelled(77, true);
						StudentGlobals.SetStudentExpelled(78, true);
						StudentGlobals.SetStudentExpelled(79, true);
						StudentGlobals.SetStudentExpelled(80, true);
						this.ExpelledDelinquents = true;
					}
					else if (this.Answer == 4)
					{
						this.MyAudio.clip = this.BlameClosingClips[this.CrimeID];
						this.CounselorSubtitle.text = this.BlameClosings[this.CrimeID];
					}
					else
					{
						int num9 = UnityEngine.Random.Range(0, 3);
						this.MyAudio.clip = this.FreeToLeaveClips[num9];
						this.CounselorSubtitle.text = this.FreeToLeaves[num9];
						this.MyAnimation.CrossFade("CounselorArmsCrossed", 1f);
						this.Stern = true;
					}
					this.InterrogationPhase++;
				}
				this.MyAudio.Play();
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 8)
		{
			if (this.Timer > this.MyAudio.clip.length + 0.5f || Input.GetButtonDown("A"))
			{
				this.CounselorDoor.FadeOut = true;
				this.CounselorDoor.Exit = true;
				this.Interrogating = false;
				this.InterrogationPhase = 0;
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 9)
		{
			if (this.Timer > this.MyAudio.clip.length + 0.5f || Input.GetButtonDown("A"))
			{
				this.MyAnimation.Play("CounselorSlamDesk");
				this.InterrogationPhase++;
				this.MyAudio.Stop();
				this.Stern = false;
				this.Angry = true;
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 10)
		{
			if (this.Timer > 0.5f)
			{
				if (!this.Slammed)
				{
					GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
					this.VibrationCheck = true;
					this.VibrationTimer = 0.2f;
					AudioSource.PlayClipAtPoint(this.Slam, base.transform.position);
					this.Shake.shakeAmount = 0.1f;
					this.Shake.enabled = true;
					this.Shake.shake = 0.5f;
					this.Slammed = true;
				}
				this.Shake.shakeAmount = Mathf.Lerp(this.Shake.shakeAmount, 0f, Time.deltaTime);
			}
			this.Shake.shakeAmount = Mathf.Lerp(this.Shake.shakeAmount, 0f, Time.deltaTime * 10f);
			if (this.Timer > 1.5f || Input.GetButtonDown("A"))
			{
				this.MyAudio.clip = this.SuspensionClips[Mathf.Abs(this.Patience)];
				this.CounselorSubtitle.text = this.Suspensions[Mathf.Abs(this.Patience)];
				this.MyAnimation.Play("CounselorSlamIdle");
				this.Shake.enabled = false;
				this.InterrogationPhase++;
				this.MyAudio.Play();
				this.Timer = 0f;
			}
		}
		else if (this.InterrogationPhase == 11 && (this.Timer > this.MyAudio.clip.length + 0.5f || Input.GetButtonDown("A")) && !this.Yandere.Police.FadeOut)
		{
			CounselorGlobals.CounselorPunishments++;
			this.Yandere.Police.Darkness.color = new Color(0f, 0f, 0f, 0f);
			this.Yandere.Police.SuspensionLength = Mathf.Abs(this.Patience);
			this.Yandere.Police.Darkness.enabled = true;
			this.Yandere.Police.ClubActivity = false;
			this.Yandere.Police.Suspended = true;
			this.Yandere.Police.FadeOut = true;
			this.Yandere.ShoulderCamera.HUD.SetActive(true);
			this.InterrogationPhase++;
			this.Expelled = true;
			this.Timer = 0f;
			this.Yandere.Senpai = this.StudentManager.Students[1].transform;
			this.StudentManager.Reputation.PendingRep -= 10f;
			this.StudentManager.Reputation.UpdateRep();
		}
		if (this.InterrogationPhase > 6)
		{
			this.Yandere.Sanity = Mathf.Lerp(this.Yandere.Sanity, 100f, Time.deltaTime);
			this.Rumble.volume = Mathf.Lerp(this.Rumble.volume, 0f, Time.deltaTime);
			this.GenkaChibi.transform.localPosition = Vector3.Lerp(this.GenkaChibi.transform.localPosition, new Vector3(0f, (float)(90 * this.Patience), 0f), Time.deltaTime * 10f);
		}
	}

	// Token: 0x04001A18 RID: 6680
	public CutsceneManagerScript CutsceneManager;

	// Token: 0x04001A19 RID: 6681
	public StudentManagerScript StudentManager;

	// Token: 0x04001A1A RID: 6682
	public CounselorDoorScript CounselorDoor;

	// Token: 0x04001A1B RID: 6683
	public InputManagerScript InputManager;

	// Token: 0x04001A1C RID: 6684
	public PromptBarScript PromptBar;

	// Token: 0x04001A1D RID: 6685
	public EndOfDayScript EndOfDay;

	// Token: 0x04001A1E RID: 6686
	public SubtitleScript Subtitle;

	// Token: 0x04001A1F RID: 6687
	public SchemesScript Schemes;

	// Token: 0x04001A20 RID: 6688
	public StudentScript Student;

	// Token: 0x04001A21 RID: 6689
	public YandereScript Yandere;

	// Token: 0x04001A22 RID: 6690
	public Animation MyAnimation;

	// Token: 0x04001A23 RID: 6691
	public AudioSource MyAudio;

	// Token: 0x04001A24 RID: 6692
	public PromptScript Prompt;

	// Token: 0x04001A25 RID: 6693
	public AudioClip[] CounselorGreetingClips;

	// Token: 0x04001A26 RID: 6694
	public AudioClip[] CounselorLectureClips;

	// Token: 0x04001A27 RID: 6695
	public AudioClip[] CounselorReportClips;

	// Token: 0x04001A28 RID: 6696
	public AudioClip[] RivalClips;

	// Token: 0x04001A29 RID: 6697
	public AudioClip CounselorFarewellClip;

	// Token: 0x04001A2A RID: 6698
	public readonly string CounselorFarewellText = "Don't misbehave.";

	// Token: 0x04001A2B RID: 6699
	public AudioClip CounselorBusyClip;

	// Token: 0x04001A2C RID: 6700
	public readonly string CounselorBusyText = "I'm sorry, I've got my hands full for the rest of today. I won't be available until tomorrow.";

	// Token: 0x04001A2D RID: 6701
	public readonly string[] CounselorGreetingText = new string[]
	{
		string.Empty,
		"What can I help you with?",
		"Can I help you?"
	};

	// Token: 0x04001A2E RID: 6702
	public readonly string[] CounselorLectureText = new string[]
	{
		string.Empty,
		"May I see your phone for a moment? ...what is THIS?! Would you care to explain why something like this is on your phone?",
		"May I take a look inside your bag? ...this doesn't belong to you, does it?! What are you doing with someone else's property?",
		"I need to take a look in your bag. ...cigarettes?! You have absolutely no excuse to be carrying something like this around!",
		"It has come to my attention that you've been vandalizing the school's property. What, exactly, do you have to say for yourself?",
		"Obviously, we need to have a long talk about the kind of behavior that will not tolerated at this school!",
		"That's it! I've given you enough second chances. You have repeatedly broken school rules and ignored every warning that I have given you. You have left me with no choice but to permanently expel you!"
	};

	// Token: 0x04001A2F RID: 6703
	public readonly string[] CounselorReportText = new string[]
	{
		string.Empty,
		"That's a very serious accusation. I hope you're not lying to me. Hopefully, it's just a misunderstanding. I'll investigate the matter.",
		"Is that true? I'd hate to think we have a thief here at school. Don't worry - I'll get to the bottom of this.",
		"That's a clear violation of school rules, not to mention completely illegal. If what you're saying is true, she will face serious consequences. I'll confront her about this.",
		"It's appalling to learn that there is a student at this school who thinks they can get away with this kind of misbehavior. I'll be sure to speak with her about this later today.",
		"That's a bold claim. Are you certain? I'll investigate the matter. If she is cheating, I'll catch her in the act."
	};

	// Token: 0x04001A30 RID: 6704
	public readonly string[] LectureIntro = new string[]
	{
		string.Empty,
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana...",
		"During class, the guidance counselor enters the classroom and says that she needs to speak with Osana..."
	};

	// Token: 0x04001A31 RID: 6705
	public readonly string[] RivalText = new string[]
	{
		string.Empty,
		"What?! I've never taken and pictures like that! How did this get on my phone?!",
		"No! I'm not the one who did this! I would never steal from anyone!",
		"Huh? I don't smoke! I don't know why something like this was in my desk!",
		"W-wait, I can explain! It's not what you think!",
		"I'm telling the truth! I didn't steal the answer sheet! I don't know why it was in my desk!",
		"No...! P-please! Don't do this!"
	};

	// Token: 0x04001A32 RID: 6706
	public UILabel[] Labels;

	// Token: 0x04001A33 RID: 6707
	public Transform CounselorWindow;

	// Token: 0x04001A34 RID: 6708
	public Transform Highlight;

	// Token: 0x04001A35 RID: 6709
	public Transform Chibi;

	// Token: 0x04001A36 RID: 6710
	public SkinnedMeshRenderer Face;

	// Token: 0x04001A37 RID: 6711
	public UILabel CounselorSubtitle;

	// Token: 0x04001A38 RID: 6712
	public UISprite EndOfDayDarkness;

	// Token: 0x04001A39 RID: 6713
	public UILabel LectureSubtitle;

	// Token: 0x04001A3A RID: 6714
	public UISprite ExpelProgress;

	// Token: 0x04001A3B RID: 6715
	public UILabel LectureLabel;

	// Token: 0x04001A3C RID: 6716
	public bool ShowWindow;

	// Token: 0x04001A3D RID: 6717
	public bool Lecturing;

	// Token: 0x04001A3E RID: 6718
	public bool Busy;

	// Token: 0x04001A3F RID: 6719
	public int Selected = 1;

	// Token: 0x04001A40 RID: 6720
	public int LecturePhase = 1;

	// Token: 0x04001A41 RID: 6721
	public int LectureID = 5;

	// Token: 0x04001A42 RID: 6722
	public float ExpelTimer;

	// Token: 0x04001A43 RID: 6723
	public float ChinTimer;

	// Token: 0x04001A44 RID: 6724
	public float TalkTimer = 1f;

	// Token: 0x04001A45 RID: 6725
	public float Timer;

	// Token: 0x04001A46 RID: 6726
	public Vector3 LookAtTarget;

	// Token: 0x04001A47 RID: 6727
	public bool LookAtPlayer;

	// Token: 0x04001A48 RID: 6728
	public Transform Default;

	// Token: 0x04001A49 RID: 6729
	public Transform Head;

	// Token: 0x04001A4A RID: 6730
	public bool Angry;

	// Token: 0x04001A4B RID: 6731
	public bool Stern;

	// Token: 0x04001A4C RID: 6732
	public bool Sad;

	// Token: 0x04001A4D RID: 6733
	public float MouthTarget;

	// Token: 0x04001A4E RID: 6734
	public float MouthTimer;

	// Token: 0x04001A4F RID: 6735
	public float TimerLimit;

	// Token: 0x04001A50 RID: 6736
	public float MouthOpen;

	// Token: 0x04001A51 RID: 6737
	public float TalkSpeed;

	// Token: 0x04001A52 RID: 6738
	public float BS_SadMouth;

	// Token: 0x04001A53 RID: 6739
	public float BS_MadBrow;

	// Token: 0x04001A54 RID: 6740
	public float BS_SadBrow;

	// Token: 0x04001A55 RID: 6741
	public float BS_AngryEyes;

	// Token: 0x04001A56 RID: 6742
	public DetectClickScript[] CounselorOption;

	// Token: 0x04001A57 RID: 6743
	public InputDeviceScript InputDevice;

	// Token: 0x04001A58 RID: 6744
	public StudentWitnessType Crime;

	// Token: 0x04001A59 RID: 6745
	public UITexture GenkaChibi;

	// Token: 0x04001A5A RID: 6746
	public CameraShake Shake;

	// Token: 0x04001A5B RID: 6747
	public Texture HappyChibi;

	// Token: 0x04001A5C RID: 6748
	public Texture AnnoyedChibi;

	// Token: 0x04001A5D RID: 6749
	public Texture MadChibi;

	// Token: 0x04001A5E RID: 6750
	public GameObject CounselorOptions;

	// Token: 0x04001A5F RID: 6751
	public GameObject CounselorBar;

	// Token: 0x04001A60 RID: 6752
	public GameObject Reticle;

	// Token: 0x04001A61 RID: 6753
	public GameObject Laptop;

	// Token: 0x04001A62 RID: 6754
	public Transform CameraTarget;

	// Token: 0x04001A63 RID: 6755
	public int InterrogationPhase;

	// Token: 0x04001A64 RID: 6756
	public int Patience;

	// Token: 0x04001A65 RID: 6757
	public int CrimeID;

	// Token: 0x04001A66 RID: 6758
	public int Answer;

	// Token: 0x04001A67 RID: 6759
	public bool MustExpelDelinquents;

	// Token: 0x04001A68 RID: 6760
	public bool ExpelledDelinquents;

	// Token: 0x04001A69 RID: 6761
	public bool SilentTreatment;

	// Token: 0x04001A6A RID: 6762
	public bool Interrogating;

	// Token: 0x04001A6B RID: 6763
	public bool Expelled;

	// Token: 0x04001A6C RID: 6764
	public bool Slammed;

	// Token: 0x04001A6D RID: 6765
	public AudioSource Rumble;

	// Token: 0x04001A6E RID: 6766
	public AudioClip Countdown;

	// Token: 0x04001A6F RID: 6767
	public AudioClip Choice;

	// Token: 0x04001A70 RID: 6768
	public AudioClip Slam;

	// Token: 0x04001A71 RID: 6769
	public AudioClip[] GreetingClips;

	// Token: 0x04001A72 RID: 6770
	public string[] Greetings;

	// Token: 0x04001A73 RID: 6771
	public AudioClip[] BloodLectureClips;

	// Token: 0x04001A74 RID: 6772
	public string[] BloodLectures;

	// Token: 0x04001A75 RID: 6773
	public AudioClip[] InsanityLectureClips;

	// Token: 0x04001A76 RID: 6774
	public string[] InsanityLectures;

	// Token: 0x04001A77 RID: 6775
	public AudioClip[] LewdLectureClips;

	// Token: 0x04001A78 RID: 6776
	public string[] LewdLectures;

	// Token: 0x04001A79 RID: 6777
	public AudioClip[] TheftLectureClips;

	// Token: 0x04001A7A RID: 6778
	public string[] TheftLectures;

	// Token: 0x04001A7B RID: 6779
	public AudioClip[] TrespassLectureClips;

	// Token: 0x04001A7C RID: 6780
	public string[] TrespassLectures;

	// Token: 0x04001A7D RID: 6781
	public AudioClip[] WeaponLectureClips;

	// Token: 0x04001A7E RID: 6782
	public string[] WeaponLectures;

	// Token: 0x04001A7F RID: 6783
	public AudioClip[] SilentClips;

	// Token: 0x04001A80 RID: 6784
	public string[] Silents;

	// Token: 0x04001A81 RID: 6785
	public AudioClip[] SuspensionClips;

	// Token: 0x04001A82 RID: 6786
	public string[] Suspensions;

	// Token: 0x04001A83 RID: 6787
	public AudioClip[] AcceptExcuseClips;

	// Token: 0x04001A84 RID: 6788
	public string[] AcceptExcuses;

	// Token: 0x04001A85 RID: 6789
	public AudioClip[] RejectExcuseClips;

	// Token: 0x04001A86 RID: 6790
	public string[] RejectExcuses;

	// Token: 0x04001A87 RID: 6791
	public AudioClip[] RejectLieClips;

	// Token: 0x04001A88 RID: 6792
	public string[] RejectLies;

	// Token: 0x04001A89 RID: 6793
	public AudioClip[] AcceptBlameClips;

	// Token: 0x04001A8A RID: 6794
	public string[] AcceptBlames;

	// Token: 0x04001A8B RID: 6795
	public AudioClip[] RejectApologyClips;

	// Token: 0x04001A8C RID: 6796
	public string[] RejectApologies;

	// Token: 0x04001A8D RID: 6797
	public AudioClip[] RejectBlameClips;

	// Token: 0x04001A8E RID: 6798
	public string[] RejectBlames;

	// Token: 0x04001A8F RID: 6799
	public AudioClip[] RejectFlirtClips;

	// Token: 0x04001A90 RID: 6800
	public string[] RejectFlirts;

	// Token: 0x04001A91 RID: 6801
	public AudioClip[] BadClosingClips;

	// Token: 0x04001A92 RID: 6802
	public string[] BadClosings;

	// Token: 0x04001A93 RID: 6803
	public AudioClip[] BlameClosingClips;

	// Token: 0x04001A94 RID: 6804
	public string[] BlameClosings;

	// Token: 0x04001A95 RID: 6805
	public AudioClip[] FreeToLeaveClips;

	// Token: 0x04001A96 RID: 6806
	public string[] FreeToLeaves;

	// Token: 0x04001A97 RID: 6807
	public AudioClip AcceptApologyClip;

	// Token: 0x04001A98 RID: 6808
	public string AcceptApology;

	// Token: 0x04001A99 RID: 6809
	public AudioClip RejectThreatClip;

	// Token: 0x04001A9A RID: 6810
	public string RejectThreat;

	// Token: 0x04001A9B RID: 6811
	public AudioClip ExpelDelinquentsClip;

	// Token: 0x04001A9C RID: 6812
	public string ExpelDelinquents;

	// Token: 0x04001A9D RID: 6813
	public AudioClip DelinquentsDeadClip;

	// Token: 0x04001A9E RID: 6814
	public string DelinquentsDead;

	// Token: 0x04001A9F RID: 6815
	public AudioClip DelinquentsExpelledClip;

	// Token: 0x04001AA0 RID: 6816
	public string DelinquentsExpelled;

	// Token: 0x04001AA1 RID: 6817
	public AudioClip DelinquentsGoneClip;

	// Token: 0x04001AA2 RID: 6818
	public string DelinquentsGone;

	// Token: 0x04001AA3 RID: 6819
	public AudioClip[] ExcuseClips;

	// Token: 0x04001AA4 RID: 6820
	public string[] Excuses;

	// Token: 0x04001AA5 RID: 6821
	public AudioClip[] LieClips;

	// Token: 0x04001AA6 RID: 6822
	public string[] Lies;

	// Token: 0x04001AA7 RID: 6823
	public AudioClip[] DelinquentClips;

	// Token: 0x04001AA8 RID: 6824
	public string[] Delinquents;

	// Token: 0x04001AA9 RID: 6825
	public AudioClip ApologyClip;

	// Token: 0x04001AAA RID: 6826
	public string Apology;

	// Token: 0x04001AAB RID: 6827
	public AudioClip FlirtClip;

	// Token: 0x04001AAC RID: 6828
	public string Flirt;

	// Token: 0x04001AAD RID: 6829
	public AudioClip ThreatenClip;

	// Token: 0x04001AAE RID: 6830
	public string Threaten;

	// Token: 0x04001AAF RID: 6831
	public AudioClip Silence;

	// Token: 0x04001AB0 RID: 6832
	public float VibrationTimer;

	// Token: 0x04001AB1 RID: 6833
	public bool VibrationCheck;

	// Token: 0x04001AB2 RID: 6834
	public UILabel RIVAL;

	// Token: 0x04001AB3 RID: 6835
	public UILabel EXPELLED;
}
