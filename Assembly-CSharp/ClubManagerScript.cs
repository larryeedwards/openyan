using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class ClubManagerScript : MonoBehaviour
{
	// Token: 0x060017F6 RID: 6134 RVA: 0x000C1664 File Offset: 0x000BFA64
	private void Start()
	{
		this.ClubWindow.ActivityWindow.localScale = Vector3.zero;
		this.ClubWindow.ActivityWindow.gameObject.SetActive(false);
		this.ActivateClubBenefit();
		int num = 0;
		this.ID = 1;
		while (this.ID < this.ClubArray.Length)
		{
			if (ClubGlobals.GetClubClosed(this.ClubArray[this.ID]))
			{
				this.ClubPosters[this.ID].SetActive(false);
				if (this.ClubArray[this.ID] == ClubType.Gardening)
				{
					this.ClubPatrolPoints[this.ID].transform.position = new Vector3(-36f, this.ClubPatrolPoints[this.ID].transform.position.y, this.ClubPatrolPoints[this.ID].transform.position.z);
				}
				else if (this.ClubArray[this.ID] == ClubType.Gaming)
				{
					this.ClubPatrolPoints[this.ID].transform.position = new Vector3(20f, this.ClubPatrolPoints[this.ID].transform.position.y, this.ClubPatrolPoints[this.ID].transform.position.z);
				}
				else if (this.ClubArray[this.ID] != ClubType.Sports)
				{
					this.ClubPatrolPoints[this.ID].transform.position = new Vector3(this.ClubPatrolPoints[this.ID].transform.position.x, this.ClubPatrolPoints[this.ID].transform.position.y, 20f);
				}
				num++;
			}
			this.ID++;
		}
		if (num > 10)
		{
			this.StudentManager.NoClubMeeting = true;
		}
		if (ClubGlobals.GetClubClosed(this.ClubArray[2]))
		{
			this.StudentManager.HidingSpots.List[56] = this.StudentManager.Hangouts.List[56];
			this.StudentManager.HidingSpots.List[57] = this.StudentManager.Hangouts.List[57];
			this.StudentManager.HidingSpots.List[58] = this.StudentManager.Hangouts.List[58];
			this.StudentManager.HidingSpots.List[59] = this.StudentManager.Hangouts.List[59];
			this.StudentManager.HidingSpots.List[60] = this.StudentManager.Hangouts.List[60];
			this.StudentManager.SleuthPhase = 3;
		}
		this.ID = 0;
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x000C195C File Offset: 0x000BFD5C
	private void Update()
	{
		if (this.Club != ClubType.None)
		{
			if (this.Phase == 1)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			}
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.Darkness.color.a == 0f)
			{
				if (this.Phase == 1)
				{
					this.PromptBar.ClearButtons();
					this.PromptBar.Label[0].text = "Continue";
					this.PromptBar.UpdateButtons();
					this.PromptBar.Show = true;
					this.ClubWindow.PerformingActivity = true;
					this.ClubWindow.ActivityWindow.gameObject.SetActive(true);
					this.ClubWindow.ActivityLabel.text = this.ClubWindow.ActivityDescs[(int)this.Club];
					this.Phase++;
				}
				else if (this.Phase == 2)
				{
					if (this.ClubWindow.ActivityWindow.localScale.x > 0.9f)
					{
						if (this.Club == ClubType.MartialArts)
						{
							if (this.ClubPhase == 0)
							{
								component.clip = this.MotivationalQuotes[UnityEngine.Random.Range(0, this.MotivationalQuotes.Length)];
								component.Play();
								this.ClubEffect = true;
								this.ClubPhase++;
								this.TimeLimit = component.clip.length;
							}
							else if (this.ClubPhase == 1)
							{
								this.Timer += Time.deltaTime;
								if (this.Timer > this.TimeLimit)
								{
									this.ID = 0;
									while (this.ID < this.Club6Students.Length)
									{
										if (this.StudentManager.Students[this.ID] != null && !this.StudentManager.Students[this.ID].Tranquil)
										{
											this.StudentManager.Students[this.Club6Students[this.ID]].GetComponent<AudioSource>().volume = 1f;
										}
										this.ID++;
									}
									this.ClubPhase++;
								}
							}
						}
						if (Input.GetButtonDown("A"))
						{
							this.ClubWindow.PerformingActivity = false;
							this.PromptBar.Show = false;
							this.Phase++;
						}
					}
				}
				else if (this.ClubWindow.ActivityWindow.localScale.x < 0.1f)
				{
					this.Police.Darkness.enabled = true;
					this.Police.ClubActivity = false;
					this.Police.FadeOut = true;
				}
			}
			if (this.Club == ClubType.Occult)
			{
				component.volume = 1f - this.Darkness.color.a;
			}
		}
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x000C1CC0 File Offset: 0x000C00C0
	public void ClubActivity()
	{
		this.StudentManager.StopMoving();
		this.ShoulderCamera.enabled = false;
		this.MainCamera.enabled = false;
		this.MainCamera.transform.position = this.ClubVantages[(int)this.Club].position;
		this.MainCamera.transform.rotation = this.ClubVantages[(int)this.Club].rotation;
		if (this.Club == ClubType.Cooking)
		{
			this.Cake.SetActive(true);
			this.ID = 0;
			while (this.ID < this.Club1Students.Length)
			{
				StudentScript studentScript = this.StudentManager.Students[this.Club1Students[this.ID]];
				if (studentScript != null && !studentScript.Tranquil && studentScript.Alive)
				{
					studentScript.transform.position = this.Club1ActivitySpots[this.ID].position;
					studentScript.transform.rotation = this.Club1ActivitySpots[this.ID].rotation;
					studentScript.CharacterAnimation[studentScript.SocialSitAnim].layer = 99;
					studentScript.CharacterAnimation.Play(studentScript.SocialSitAnim);
					studentScript.CharacterAnimation[studentScript.SocialSitAnim].weight = 1f;
					studentScript.SmartPhone.SetActive(false);
					studentScript.SpeechLines.Play();
					studentScript.ClubActivity = true;
					studentScript.Talking = false;
					studentScript.Routine = false;
					studentScript.GetComponent<AudioSource>().volume = 0.1f;
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.CharacterAnimation.Play("f02_sit_00");
			this.Yandere.transform.position = this.Club1ActivitySpots[6].position;
			this.Yandere.transform.rotation = this.Club1ActivitySpots[6].rotation;
		}
		else if (this.Club == ClubType.Drama)
		{
			this.ID = 0;
			while (this.ID < this.Club2Students.Length)
			{
				this.StudentManager.DramaPhase = 1;
				this.StudentManager.UpdateDrama();
				StudentScript studentScript2 = this.StudentManager.Students[this.Club2Students[this.ID]];
				if (studentScript2 != null && !studentScript2.Tranquil && studentScript2.Alive)
				{
					if (!this.StudentManager.MemorialScene.gameObject.activeInHierarchy)
					{
						studentScript2.transform.position = studentScript2.CurrentDestination.position;
						studentScript2.transform.rotation = studentScript2.CurrentDestination.rotation;
					}
					else
					{
						studentScript2.transform.position = new Vector3(0f, 0f, 0f);
					}
					studentScript2.ClubActivity = true;
					studentScript2.Talking = false;
					studentScript2.Routine = true;
					studentScript2.GetComponent<AudioSource>().volume = 0.1f;
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			if (!this.StudentManager.MemorialScene.gameObject.activeInHierarchy)
			{
				this.Yandere.transform.position = new Vector3(42f, 1.3775f, 72f);
				this.Yandere.transform.eulerAngles = new Vector3(0f, -90f, 0f);
			}
		}
		else if (this.Club == ClubType.Occult)
		{
			this.ID = 0;
			while (this.ID < this.Club3Students.Length)
			{
				StudentScript studentScript3 = this.StudentManager.Students[this.Club3Students[this.ID]];
				if (studentScript3 != null && !studentScript3.Tranquil)
				{
					studentScript3.gameObject.SetActive(false);
				}
				this.ID++;
			}
			this.MainCamera.GetComponent<AudioListener>().enabled = true;
			AudioSource component = base.GetComponent<AudioSource>();
			component.clip = this.OccultAmbience;
			component.loop = true;
			component.volume = 0f;
			component.Play();
			this.Yandere.gameObject.SetActive(false);
			this.Ritual.SetActive(true);
			this.CheckClub(ClubType.Occult);
			foreach (GameObject gameObject in this.Cultists)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(false);
				}
			}
			while (this.ClubMembers > 0)
			{
				this.Cultists[this.ClubMembers].SetActive(true);
				this.ClubMembers--;
			}
			this.CheckClub(ClubType.Occult);
		}
		else if (this.Club == ClubType.Art)
		{
			this.ID = 0;
			while (this.ID < this.Club4Students.Length)
			{
				StudentScript studentScript4 = this.StudentManager.Students[this.Club4Students[this.ID]];
				if (studentScript4 != null && !studentScript4.Tranquil && studentScript4.Alive)
				{
					studentScript4.transform.position = this.Club4ActivitySpots[this.ID].position;
					studentScript4.transform.rotation = this.Club4ActivitySpots[this.ID].rotation;
					studentScript4.ClubActivity = true;
					studentScript4.Talking = false;
					studentScript4.Routine = true;
					if (!studentScript4.ClubAttire)
					{
						studentScript4.ChangeClubwear();
					}
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.Club4ActivitySpots[5].position;
			this.Yandere.transform.rotation = this.Club4ActivitySpots[5].rotation;
			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.LightMusic)
		{
			this.ID = 0;
			while (this.ID < this.Club5Students.Length)
			{
				StudentScript studentScript5 = this.StudentManager.Students[this.Club5Students[this.ID]];
				if (studentScript5 != null && !studentScript5.Tranquil && studentScript5.Alive)
				{
					studentScript5.transform.position = studentScript5.CurrentDestination.position;
					studentScript5.transform.rotation = studentScript5.CurrentDestination.rotation;
					studentScript5.ClubActivity = false;
					studentScript5.Talking = false;
					studentScript5.Routine = true;
					studentScript5.Stop = false;
				}
				this.ID++;
			}
		}
		else if (this.Club == ClubType.MartialArts)
		{
			this.ID = 0;
			while (this.ID < this.Club6Students.Length)
			{
				StudentScript studentScript6 = this.StudentManager.Students[this.Club6Students[this.ID]];
				if (studentScript6 != null && !studentScript6.Tranquil && studentScript6.Alive)
				{
					studentScript6.transform.position = this.Club6ActivitySpots[this.ID].position;
					studentScript6.transform.rotation = this.Club6ActivitySpots[this.ID].rotation;
					studentScript6.ClubActivity = true;
					studentScript6.GetComponent<AudioSource>().volume = 0.1f;
					if (!studentScript6.ClubAttire)
					{
						studentScript6.ChangeClubwear();
					}
				}
				this.ID++;
			}
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.Club6ActivitySpots[5].position;
			this.Yandere.transform.rotation = this.Club6ActivitySpots[5].rotation;
			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Photography)
		{
			this.ID = 0;
			while (this.ID < this.Club7Students.Length)
			{
				StudentScript studentScript7 = this.StudentManager.Students[this.Club7Students[this.ID]];
				if (studentScript7 != null && !studentScript7.Tranquil && studentScript7.Alive)
				{
					studentScript7.transform.position = this.StudentManager.Clubs.List[studentScript7.StudentID].position;
					studentScript7.transform.rotation = this.StudentManager.Clubs.List[studentScript7.StudentID].rotation;
					studentScript7.CharacterAnimation[studentScript7.SocialSitAnim].weight = 1f;
					studentScript7.SmartPhone.SetActive(false);
					studentScript7.ClubActivity = true;
					studentScript7.SpeechLines.Play();
					studentScript7.Talking = false;
					studentScript7.Routine = true;
					studentScript7.Hearts.Stop();
				}
				this.ID++;
			}
			this.Yandere.CanMove = false;
			this.Yandere.Talking = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.Club7ActivitySpot.position;
			this.Yandere.transform.rotation = this.Club7ActivitySpot.rotation;
			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Science)
		{
			this.ID = 0;
			while (this.ID < this.Club8Students.Length)
			{
				StudentScript studentScript8 = this.StudentManager.Students[this.Club8Students[this.ID]];
				if (studentScript8 != null && !studentScript8.Tranquil && studentScript8.Alive)
				{
					studentScript8.transform.position = this.Club8ActivitySpots[this.ID].position;
					studentScript8.transform.rotation = this.Club8ActivitySpots[this.ID].rotation;
					studentScript8.ClubActivity = true;
					studentScript8.Talking = false;
					studentScript8.Routine = true;
					if (!studentScript8.ClubAttire)
					{
						studentScript8.ChangeClubwear();
					}
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Sports)
		{
			this.ID = 0;
			while (this.ID < this.Club9Students.Length)
			{
				StudentScript studentScript9 = this.StudentManager.Students[this.Club9Students[this.ID]];
				if (studentScript9 != null && !studentScript9.Tranquil && studentScript9.Alive)
				{
					studentScript9.transform.position = studentScript9.CurrentDestination.position;
					studentScript9.transform.rotation = studentScript9.CurrentDestination.rotation;
					studentScript9.ClubActivity = true;
					studentScript9.Talking = false;
					studentScript9.Routine = true;
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.Schoolwear = 2;
			this.Yandere.ChangeSchoolwear();
		}
		else if (this.Club == ClubType.Gardening)
		{
			this.ID = 0;
			while (this.ID < this.Club10Students.Length)
			{
				StudentScript studentScript10 = this.StudentManager.Students[this.Club10Students[this.ID]];
				if (studentScript10 != null && !studentScript10.Tranquil && studentScript10.Alive)
				{
					studentScript10.transform.position = studentScript10.CurrentDestination.position;
					studentScript10.transform.rotation = studentScript10.CurrentDestination.rotation;
					studentScript10.ClubActivity = true;
					studentScript10.Talking = false;
					studentScript10.Routine = true;
					studentScript10.GetComponent<AudioSource>().volume = 0.1f;
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			if (!this.Yandere.ClubAttire)
			{
				this.Yandere.ChangeClubwear();
			}
		}
		else if (this.Club == ClubType.Gaming)
		{
			this.ID = 0;
			while (this.ID < this.Club11Students.Length)
			{
				StudentScript studentScript11 = this.StudentManager.Students[this.Club11Students[this.ID]];
				if (studentScript11 != null && !studentScript11.Tranquil && studentScript11.Alive)
				{
					studentScript11.transform.position = studentScript11.CurrentDestination.position;
					studentScript11.transform.rotation = studentScript11.CurrentDestination.rotation;
					studentScript11.ClubManager.GameScreens[this.ID].SetActive(true);
					studentScript11.SmartPhone.SetActive(false);
					studentScript11.ClubActivity = true;
					studentScript11.Talking = false;
					studentScript11.Routine = false;
					studentScript11.GetComponent<AudioSource>().volume = 0.1f;
				}
				this.ID++;
			}
			this.Yandere.Talking = false;
			this.Yandere.CanMove = false;
			this.Yandere.ClubActivity = true;
			this.Yandere.transform.position = this.StudentManager.ComputerGames.Chairs[1].transform.position;
			this.Yandere.transform.rotation = this.StudentManager.ComputerGames.Chairs[1].transform.rotation;
		}
		else if (this.Club == ClubType.Delinquent)
		{
			Debug.Log("Calling the Delinquent 'club activity'.");
			this.Yandere.gameObject.SetActive(false);
			this.ID = 0;
			while (this.ID < this.Club14Students.Length)
			{
				StudentScript studentScript12 = this.StudentManager.Students[this.Club14Students[this.ID]];
				if (studentScript12 != null && studentScript12.Alive)
				{
					Debug.Log("Telling a delinquent #" + studentScript12.StudentID + " to leave school.");
					studentScript12.Pathfinding.target = this.StudentManager.Exit;
					studentScript12.CurrentDestination = this.StudentManager.Exit;
					studentScript12.Pathfinding.canSearch = true;
					studentScript12.Pathfinding.canMove = true;
					studentScript12.Pathfinding.speed = 1f;
					studentScript12.DistanceToDestination = 100f;
					studentScript12.Talking = false;
					studentScript12.Stop = false;
				}
				this.ID++;
			}
		}
		this.Clock.SetActive(false);
		this.Reputation.SetActive(false);
		this.Heartrate.SetActive(false);
		this.Watermark.SetActive(false);
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000C2D00 File Offset: 0x000C1100
	public void CheckClub(ClubType Check)
	{
		if (Check == ClubType.Cooking)
		{
			this.ClubIDs = this.Club1IDs;
		}
		else if (Check == ClubType.Drama)
		{
			this.ClubIDs = this.Club2IDs;
		}
		else if (Check == ClubType.Occult)
		{
			this.ClubIDs = this.Club3IDs;
		}
		else if (Check == ClubType.Art)
		{
			this.ClubIDs = this.Club4IDs;
		}
		else if (Check == ClubType.LightMusic)
		{
			this.ClubIDs = this.Club5IDs;
		}
		else if (Check == ClubType.MartialArts)
		{
			this.ClubIDs = this.Club6IDs;
		}
		else if (Check == ClubType.Photography)
		{
			this.ClubIDs = this.Club7IDs;
		}
		else if (Check == ClubType.Science)
		{
			this.ClubIDs = this.Club8IDs;
		}
		else if (Check == ClubType.Sports)
		{
			this.ClubIDs = this.Club9IDs;
		}
		else if (Check == ClubType.Gardening)
		{
			this.ClubIDs = this.Club10IDs;
		}
		else if (Check == ClubType.Gaming)
		{
			this.ClubIDs = this.Club11IDs;
		}
		this.LeaderMissing = false;
		this.LeaderDead = false;
		this.ClubMembers = 0;
		this.ID = 1;
		while (this.ID < this.ClubIDs.Length)
		{
			if (!StudentGlobals.GetStudentDead(this.ClubIDs[this.ID]) && !StudentGlobals.GetStudentDying(this.ClubIDs[this.ID]) && !StudentGlobals.GetStudentKidnapped(this.ClubIDs[this.ID]) && !StudentGlobals.GetStudentArrested(this.ClubIDs[this.ID]) && !StudentGlobals.GetStudentExpelled(this.ClubIDs[this.ID]) && StudentGlobals.GetStudentReputation(this.ClubIDs[this.ID]) > -100)
			{
				this.ClubMembers++;
			}
			this.ID++;
		}
		if (this.TranqCase.VictimClubType == Check)
		{
			this.ClubMembers--;
		}
		if (Check == ClubType.LightMusic && this.ClubMembers < 5)
		{
			this.LeaderAshamed = true;
		}
		if (ClubGlobals.Club == Check)
		{
			this.ClubMembers++;
		}
		if (Check == ClubType.Cooking)
		{
			int num = 21;
			if (StudentGlobals.GetStudentDead(num) || StudentGlobals.GetStudentDying(num) || StudentGlobals.GetStudentArrested(num) || StudentGlobals.GetStudentReputation(num) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num) || StudentGlobals.GetStudentKidnapped(num) || this.TranqCase.VictimID == num)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Drama)
		{
			int num2 = 26;
			if (StudentGlobals.GetStudentDead(num2) || StudentGlobals.GetStudentDying(num2) || StudentGlobals.GetStudentArrested(num2) || StudentGlobals.GetStudentReputation(num2) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num2) || StudentGlobals.GetStudentKidnapped(num2) || this.TranqCase.VictimID == num2)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Occult)
		{
			int num3 = 31;
			if (StudentGlobals.GetStudentDead(num3) || StudentGlobals.GetStudentDying(num3) || StudentGlobals.GetStudentArrested(num3) || StudentGlobals.GetStudentReputation(num3) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num3) || StudentGlobals.GetStudentKidnapped(num3) || this.TranqCase.VictimID == num3)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Gaming)
		{
			int num4 = 36;
			if (StudentGlobals.GetStudentDead(num4) || StudentGlobals.GetStudentDying(num4) || StudentGlobals.GetStudentArrested(num4) || StudentGlobals.GetStudentReputation(num4) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num4) || StudentGlobals.GetStudentKidnapped(num4) || this.TranqCase.VictimID == num4)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Art)
		{
			int num5 = 41;
			if (StudentGlobals.GetStudentDead(num5) || StudentGlobals.GetStudentDying(num5) || StudentGlobals.GetStudentArrested(num5) || StudentGlobals.GetStudentReputation(num5) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num5) || StudentGlobals.GetStudentKidnapped(num5) || this.TranqCase.VictimID == num5)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.MartialArts)
		{
			int num6 = 46;
			if (StudentGlobals.GetStudentDead(num6) || StudentGlobals.GetStudentDying(num6) || StudentGlobals.GetStudentArrested(num6) || StudentGlobals.GetStudentReputation(num6) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num6) || StudentGlobals.GetStudentKidnapped(num6) || this.TranqCase.VictimID == num6)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.LightMusic)
		{
			int num7 = 51;
			if (StudentGlobals.GetStudentDead(num7) || StudentGlobals.GetStudentDying(num7) || StudentGlobals.GetStudentArrested(num7) || StudentGlobals.GetStudentReputation(num7) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num7) || StudentGlobals.GetStudentKidnapped(num7) || this.TranqCase.VictimID == num7)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Photography)
		{
			int num8 = 56;
			if (StudentGlobals.GetStudentDead(num8) || StudentGlobals.GetStudentDying(num8) || StudentGlobals.GetStudentArrested(num8) || StudentGlobals.GetStudentReputation(num8) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num8) || StudentGlobals.GetStudentKidnapped(num8) || this.TranqCase.VictimID == num8)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Science)
		{
			int num9 = 61;
			if (StudentGlobals.GetStudentDead(num9) || StudentGlobals.GetStudentDying(num9) || StudentGlobals.GetStudentArrested(num9) || StudentGlobals.GetStudentReputation(num9) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num9) || StudentGlobals.GetStudentKidnapped(num9) || this.TranqCase.VictimID == num9)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Sports)
		{
			int num10 = 66;
			if (StudentGlobals.GetStudentDead(num10) || StudentGlobals.GetStudentDying(num10) || StudentGlobals.GetStudentArrested(num10) || StudentGlobals.GetStudentReputation(num10) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num10) || StudentGlobals.GetStudentKidnapped(num10) || this.TranqCase.VictimID == num10)
			{
				this.LeaderMissing = true;
			}
		}
		else if (Check == ClubType.Gardening)
		{
			int num11 = 71;
			if (StudentGlobals.GetStudentDead(num11) || StudentGlobals.GetStudentDying(num11) || StudentGlobals.GetStudentArrested(num11) || StudentGlobals.GetStudentReputation(num11) <= -100)
			{
				this.LeaderDead = true;
			}
			if (StudentGlobals.GetStudentMissing(num11) || StudentGlobals.GetStudentKidnapped(num11) || this.TranqCase.VictimID == num11)
			{
				this.LeaderMissing = true;
			}
		}
		if (!this.LeaderDead && !this.LeaderMissing && Check == ClubType.LightMusic && (double)StudentGlobals.GetStudentReputation(51) < -33.33333)
		{
			this.LeaderAshamed = true;
		}
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000C3498 File Offset: 0x000C1898
	public void CheckGrudge(ClubType Check)
	{
		if (Check == ClubType.Cooking)
		{
			this.ClubIDs = this.Club1IDs;
		}
		else if (Check == ClubType.Drama)
		{
			this.ClubIDs = this.Club2IDs;
		}
		else if (Check == ClubType.Occult)
		{
			this.ClubIDs = this.Club3IDs;
		}
		else if (Check == ClubType.LightMusic)
		{
			this.ClubIDs = this.Club5IDs;
		}
		else if (Check == ClubType.MartialArts)
		{
			this.ClubIDs = this.Club6IDs;
		}
		else if (Check == ClubType.Photography)
		{
			this.ClubIDs = this.Club7IDs;
		}
		else if (Check == ClubType.Science)
		{
			this.ClubIDs = this.Club8IDs;
		}
		else if (Check == ClubType.Sports)
		{
			this.ClubIDs = this.Club9IDs;
		}
		else if (Check == ClubType.Gardening)
		{
			this.ClubIDs = this.Club10IDs;
		}
		else if (Check == ClubType.Gaming)
		{
			this.ClubIDs = this.Club11IDs;
		}
		this.LeaderGrudge = false;
		this.ClubGrudge = false;
		this.ID = 1;
		while (this.ID < this.ClubIDs.Length)
		{
			if (this.StudentManager.Students[this.ClubIDs[this.ID]] != null && StudentGlobals.GetStudentGrudge(this.ClubIDs[this.ID]))
			{
				this.ClubGrudge = true;
			}
			this.ID++;
		}
		if (Check == ClubType.Cooking)
		{
			if (this.StudentManager.Students[21].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Drama)
		{
			if (this.StudentManager.Students[26].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Occult)
		{
			if (this.StudentManager.Students[31].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Art)
		{
			if (this.StudentManager.Students[41].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.MartialArts)
		{
			if (this.StudentManager.Students[46].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.LightMusic)
		{
			if (this.StudentManager.Students[51].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Photography)
		{
			if (this.StudentManager.Students[56].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Science)
		{
			if (this.StudentManager.Students[61].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Sports)
		{
			if (this.StudentManager.Students[66].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Gardening)
		{
			if (this.StudentManager.Students[71].Grudge)
			{
				this.LeaderGrudge = true;
			}
		}
		else if (Check == ClubType.Gaming && this.StudentManager.Students[36].Grudge)
		{
			this.LeaderGrudge = true;
		}
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000C37E8 File Offset: 0x000C1BE8
	public void ActivateClubBenefit()
	{
		if (ClubGlobals.Club == ClubType.Cooking)
		{
			if (!this.Refrigerator.CookingEvent.EventActive)
			{
				this.Refrigerator.enabled = true;
				this.Refrigerator.Prompt.enabled = true;
			}
		}
		else if (ClubGlobals.Club == ClubType.Drama)
		{
			this.ID = 1;
			while (this.ID < this.Masks.Length)
			{
				this.Masks[this.ID].enabled = true;
				this.Masks[this.ID].Prompt.enabled = true;
				this.ID++;
			}
			this.Gloves.enabled = true;
			this.Gloves.Prompt.enabled = true;
		}
		else if (ClubGlobals.Club == ClubType.Occult)
		{
			this.StudentManager.UpdatePerception();
			this.Yandere.Numbness -= 0.5f;
		}
		else if (ClubGlobals.Club == ClubType.Art)
		{
			this.StudentManager.UpdateBooths();
		}
		else if (ClubGlobals.Club == ClubType.LightMusic)
		{
			this.Container.enabled = true;
			this.Container.Prompt.enabled = true;
		}
		else if (ClubGlobals.Club == ClubType.MartialArts)
		{
			this.StudentManager.UpdateBooths();
		}
		else if (ClubGlobals.Club != ClubType.Photography)
		{
			if (ClubGlobals.Club == ClubType.Science)
			{
				this.BloodCleaner.Prompt.enabled = true;
				this.StudentManager.UpdateBooths();
			}
			else if (ClubGlobals.Club == ClubType.Sports)
			{
				this.Yandere.RunSpeed += 1f;
				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.SuspicionCheck();
				}
			}
			else if (ClubGlobals.Club == ClubType.Gardening)
			{
				this.ShedDoor.Prompt.Label[0].text = "     Open";
				this.Padlock.SetActive(false);
				this.ShedDoor.Locked = false;
				if (this.Yandere.Armed)
				{
					this.Yandere.EquippedWeapon.SuspicionCheck();
				}
			}
			else if (ClubGlobals.Club == ClubType.Gaming)
			{
				this.ComputerGames.EnableGames();
			}
		}
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000C3A54 File Offset: 0x000C1E54
	public void DeactivateClubBenefit()
	{
		if (ClubGlobals.Club == ClubType.Cooking)
		{
			this.Refrigerator.enabled = false;
			this.Refrigerator.Prompt.Hide();
			this.Refrigerator.Prompt.enabled = false;
		}
		else if (ClubGlobals.Club == ClubType.Drama)
		{
			this.ID = 1;
			while (this.ID < this.Masks.Length)
			{
				if (this.Masks[this.ID] != null)
				{
					this.Masks[this.ID].enabled = false;
					this.Masks[this.ID].Prompt.Hide();
					this.Masks[this.ID].Prompt.enabled = false;
				}
				this.ID++;
			}
			this.Gloves.enabled = false;
			this.Gloves.Prompt.Hide();
			this.Gloves.Prompt.enabled = false;
		}
		else if (ClubGlobals.Club == ClubType.Occult)
		{
			ClubGlobals.Club = ClubType.None;
			this.StudentManager.UpdatePerception();
			this.Yandere.Numbness += 0.5f;
		}
		else if (ClubGlobals.Club != ClubType.Art)
		{
			if (ClubGlobals.Club == ClubType.LightMusic)
			{
				this.Container.enabled = false;
				this.Container.Prompt.Hide();
				this.Container.Prompt.enabled = false;
			}
			else if (ClubGlobals.Club != ClubType.MartialArts)
			{
				if (ClubGlobals.Club != ClubType.Photography)
				{
					if (ClubGlobals.Club == ClubType.Science)
					{
						this.BloodCleaner.enabled = false;
						this.BloodCleaner.Prompt.Hide();
						this.BloodCleaner.Prompt.enabled = false;
					}
					else if (ClubGlobals.Club == ClubType.Sports)
					{
						this.Yandere.RunSpeed -= 1f;
						if (this.Yandere.Armed)
						{
							ClubGlobals.Club = ClubType.None;
							this.Yandere.EquippedWeapon.SuspicionCheck();
						}
					}
					else if (ClubGlobals.Club == ClubType.Gardening)
					{
						if (!this.Yandere.Inventory.ShedKey)
						{
							this.ShedDoor.Prompt.Label[0].text = "     Locked";
							this.Padlock.SetActive(true);
							this.ShedDoor.Locked = true;
							this.ShedDoor.CloseDoor();
						}
						if (this.Yandere.Armed)
						{
							ClubGlobals.Club = ClubType.None;
							this.Yandere.EquippedWeapon.SuspicionCheck();
						}
					}
					else if (ClubGlobals.Club == ClubType.Gaming)
					{
						this.ComputerGames.DeactivateAllBenefits();
						this.ComputerGames.DisableGames();
					}
				}
			}
		}
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x000C3D40 File Offset: 0x000C2140
	public void UpdateMasks()
	{
		bool flag = this.Yandere.Mask != null;
		this.ID = 1;
		while (this.ID < this.Masks.Length)
		{
			this.Masks[this.ID].Prompt.HideButton[0] = flag;
			this.ID++;
		}
	}

	// Token: 0x04001862 RID: 6242
	public ShoulderCameraScript ShoulderCamera;

	// Token: 0x04001863 RID: 6243
	public StudentManagerScript StudentManager;

	// Token: 0x04001864 RID: 6244
	public ComputerGamesScript ComputerGames;

	// Token: 0x04001865 RID: 6245
	public BloodCleanerScript BloodCleaner;

	// Token: 0x04001866 RID: 6246
	public RefrigeratorScript Refrigerator;

	// Token: 0x04001867 RID: 6247
	public ClubWindowScript ClubWindow;

	// Token: 0x04001868 RID: 6248
	public ContainerScript Container;

	// Token: 0x04001869 RID: 6249
	public PromptBarScript PromptBar;

	// Token: 0x0400186A RID: 6250
	public TranqCaseScript TranqCase;

	// Token: 0x0400186B RID: 6251
	public YandereScript Yandere;

	// Token: 0x0400186C RID: 6252
	public RPG_Camera MainCamera;

	// Token: 0x0400186D RID: 6253
	public DoorScript ShedDoor;

	// Token: 0x0400186E RID: 6254
	public PoliceScript Police;

	// Token: 0x0400186F RID: 6255
	public GloveScript Gloves;

	// Token: 0x04001870 RID: 6256
	public UISprite Darkness;

	// Token: 0x04001871 RID: 6257
	public GameObject Reputation;

	// Token: 0x04001872 RID: 6258
	public GameObject Heartrate;

	// Token: 0x04001873 RID: 6259
	public GameObject Watermark;

	// Token: 0x04001874 RID: 6260
	public GameObject Padlock;

	// Token: 0x04001875 RID: 6261
	public GameObject Ritual;

	// Token: 0x04001876 RID: 6262
	public GameObject Clock;

	// Token: 0x04001877 RID: 6263
	public GameObject Cake;

	// Token: 0x04001878 RID: 6264
	public AudioClip[] MotivationalQuotes;

	// Token: 0x04001879 RID: 6265
	public Transform[] ClubPatrolPoints;

	// Token: 0x0400187A RID: 6266
	public GameObject[] ClubPosters;

	// Token: 0x0400187B RID: 6267
	public GameObject[] GameScreens;

	// Token: 0x0400187C RID: 6268
	public Transform[] ClubVantages;

	// Token: 0x0400187D RID: 6269
	public MaskScript[] Masks;

	// Token: 0x0400187E RID: 6270
	public GameObject[] Cultists;

	// Token: 0x0400187F RID: 6271
	public Transform[] Club1ActivitySpots;

	// Token: 0x04001880 RID: 6272
	public Transform[] Club4ActivitySpots;

	// Token: 0x04001881 RID: 6273
	public Transform[] Club6ActivitySpots;

	// Token: 0x04001882 RID: 6274
	public Transform Club7ActivitySpot;

	// Token: 0x04001883 RID: 6275
	public Transform[] Club8ActivitySpots;

	// Token: 0x04001884 RID: 6276
	public Transform[] Club10ActivitySpots;

	// Token: 0x04001885 RID: 6277
	public int[] Club1Students;

	// Token: 0x04001886 RID: 6278
	public int[] Club2Students;

	// Token: 0x04001887 RID: 6279
	public int[] Club3Students;

	// Token: 0x04001888 RID: 6280
	public int[] Club4Students;

	// Token: 0x04001889 RID: 6281
	public int[] Club5Students;

	// Token: 0x0400188A RID: 6282
	public int[] Club6Students;

	// Token: 0x0400188B RID: 6283
	public int[] Club7Students;

	// Token: 0x0400188C RID: 6284
	public int[] Club8Students;

	// Token: 0x0400188D RID: 6285
	public int[] Club9Students;

	// Token: 0x0400188E RID: 6286
	public int[] Club10Students;

	// Token: 0x0400188F RID: 6287
	public int[] Club11Students;

	// Token: 0x04001890 RID: 6288
	public int[] Club14Students;

	// Token: 0x04001891 RID: 6289
	public bool LeaderAshamed;

	// Token: 0x04001892 RID: 6290
	public bool ClubEffect;

	// Token: 0x04001893 RID: 6291
	public AudioClip OccultAmbience;

	// Token: 0x04001894 RID: 6292
	public int ClubPhase;

	// Token: 0x04001895 RID: 6293
	public int Phase = 1;

	// Token: 0x04001896 RID: 6294
	public ClubType Club;

	// Token: 0x04001897 RID: 6295
	public int ID;

	// Token: 0x04001898 RID: 6296
	public float TimeLimit;

	// Token: 0x04001899 RID: 6297
	public float Timer;

	// Token: 0x0400189A RID: 6298
	public ClubType[] ClubArray;

	// Token: 0x0400189B RID: 6299
	public bool LeaderMissing;

	// Token: 0x0400189C RID: 6300
	public bool LeaderDead;

	// Token: 0x0400189D RID: 6301
	public int ClubMembers;

	// Token: 0x0400189E RID: 6302
	public int[] Club1IDs;

	// Token: 0x0400189F RID: 6303
	public int[] Club2IDs;

	// Token: 0x040018A0 RID: 6304
	public int[] Club3IDs;

	// Token: 0x040018A1 RID: 6305
	public int[] Club4IDs;

	// Token: 0x040018A2 RID: 6306
	public int[] Club5IDs;

	// Token: 0x040018A3 RID: 6307
	public int[] Club6IDs;

	// Token: 0x040018A4 RID: 6308
	public int[] Club7IDs;

	// Token: 0x040018A5 RID: 6309
	public int[] Club8IDs;

	// Token: 0x040018A6 RID: 6310
	public int[] Club9IDs;

	// Token: 0x040018A7 RID: 6311
	public int[] Club10IDs;

	// Token: 0x040018A8 RID: 6312
	public int[] Club11IDs;

	// Token: 0x040018A9 RID: 6313
	public int[] Club14IDs;

	// Token: 0x040018AA RID: 6314
	public int[] ClubIDs;

	// Token: 0x040018AB RID: 6315
	public bool LeaderGrudge;

	// Token: 0x040018AC RID: 6316
	public bool ClubGrudge;
}
