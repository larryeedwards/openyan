using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class PoliceScript : MonoBehaviour
{
	// Token: 0x06001E8D RID: 7821 RVA: 0x0012D06C File Offset: 0x0012B46C
	private void Start()
	{
		this.PartsIcon.gameObject.SetActive(false);
		if (SchoolGlobals.SchoolAtmosphere > 0.5f)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
			this.Darkness.enabled = false;
		}
		base.transform.localPosition = new Vector3(-260f, base.transform.localPosition.y, base.transform.localPosition.z);
		foreach (UILabel uilabel in this.ResultsLabels)
		{
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0f);
		}
		this.ContinueLabel.color = new Color(this.ContinueLabel.color.r, this.ContinueLabel.color.g, this.ContinueLabel.color.b, 0f);
		this.ContinueButton.color = new Color(this.ContinueButton.color.r, this.ContinueButton.color.g, this.ContinueButton.color.b, 0f);
		this.Icons.SetActive(false);
	}

	// Token: 0x06001E8E RID: 7822 RVA: 0x0012D248 File Offset: 0x0012B648
	private void Update()
	{
		if (this.Show)
		{
			this.StudentManager.TutorialWindow.ShowPoliceMessage = true;
			if (this.PoisonScene)
			{
			}
			if (!this.Icons.activeInHierarchy)
			{
				this.Icons.SetActive(true);
			}
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			if (this.BloodParent.childCount == 0)
			{
				if (!this.BloodDisposed)
				{
					this.BloodIcon.spriteName = "Yes";
					this.BloodDisposed = true;
				}
			}
			else if (this.BloodDisposed)
			{
				this.BloodIcon.spriteName = "No";
				this.BloodDisposed = false;
			}
			if (this.BloodyClothing == 0)
			{
				if (!this.UniformDisposed)
				{
					this.UniformIcon.spriteName = "Yes";
					this.UniformDisposed = true;
				}
			}
			else if (this.UniformDisposed)
			{
				this.UniformIcon.spriteName = "No";
				this.UniformDisposed = false;
			}
			if (this.IncineratedWeapons == this.MurderWeapons)
			{
				if (!this.WeaponDisposed)
				{
					this.WeaponIcon.spriteName = "Yes";
					this.WeaponDisposed = true;
				}
			}
			else if (this.WeaponDisposed)
			{
				this.WeaponIcon.spriteName = "No";
				this.WeaponDisposed = false;
			}
			if (this.Corpses == 0)
			{
				if (!this.CorpseDisposed)
				{
					this.CorpseIcon.spriteName = "Yes";
					this.CorpseDisposed = true;
				}
			}
			else if (this.CorpseDisposed)
			{
				this.CorpseIcon.spriteName = "No";
				this.CorpseDisposed = false;
			}
			if (this.BodyParts == 0)
			{
				if (!this.PartsDisposed)
				{
					this.PartsIcon.spriteName = "Yes";
					this.PartsDisposed = true;
				}
			}
			else if (this.PartsDisposed)
			{
				this.PartsIcon.spriteName = "No";
				this.PartsDisposed = false;
			}
			if (this.Yandere.Sanity == 100f)
			{
				if (!this.SanityRestored)
				{
					this.SanityIcon.spriteName = "Yes";
					this.SanityRestored = true;
				}
			}
			else if (this.SanityRestored)
			{
				this.SanityIcon.spriteName = "No";
				this.SanityRestored = false;
			}
			if (!this.Clock.StopTime)
			{
				this.Timer = Mathf.MoveTowards(this.Timer, 0f, Time.deltaTime);
			}
			if (this.Timer <= 0f)
			{
				this.Timer = 0f;
				if (!this.Yandere.Attacking && !this.Yandere.Struggling && !this.Yandere.Egg && !this.FadeOut)
				{
					this.BeginFadingOut();
				}
			}
			int num = Mathf.CeilToInt(this.Timer);
			this.Minutes = num / 60;
			this.Seconds = num % 60;
			this.TimeLabel.text = string.Format("{0:00}:{1:00}", this.Minutes, this.Seconds);
		}
		if (this.FadeOut)
		{
			if (this.Yandere.Laughing)
			{
				this.Yandere.StopLaughing();
			}
			if (this.Clock.TimeSkip || this.Yandere.CanMove)
			{
				if (this.Clock.TimeSkip)
				{
					this.Clock.EndTimeSkip();
				}
				this.Yandere.StopAiming();
				this.Yandere.CanMove = false;
				this.Yandere.YandereVision = false;
				this.Yandere.PauseScreen.enabled = false;
				this.Yandere.Character.GetComponent<Animation>().CrossFade("f02_idleShort_00");
				if (this.Yandere.Mask != null)
				{
					this.Yandere.Mask.Drop();
				}
				if (this.Yandere.PickUp != null)
				{
					this.Yandere.EmptyHands();
				}
				if (this.Yandere.Dragging || this.Yandere.Carrying)
				{
					this.Yandere.EmptyHands();
				}
			}
			this.PauseScreen.Panel.alpha = Mathf.MoveTowards(this.PauseScreen.Panel.alpha, 0f, Time.deltaTime);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a >= 1f && !this.ShowResults)
			{
				this.HeartbeatCamera.SetActive(false);
				this.DetectionCamera.SetActive(false);
				if (this.ClubActivity)
				{
					this.ClubManager.Club = ClubGlobals.Club;
					this.ClubManager.ClubActivity();
					this.FadeOut = false;
				}
				else
				{
					this.Yandere.MyController.enabled = false;
					this.Yandere.enabled = false;
					this.DetermineResults();
					this.ShowResults = true;
					Time.timeScale = 2f;
					this.Jukebox.Volume = 0f;
				}
			}
		}
		if (this.ShowResults)
		{
			this.ResultsTimer += Time.deltaTime;
			if (this.ResultsTimer > 1f)
			{
				UILabel uilabel = this.ResultsLabels[0];
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, uilabel.color.a + Time.deltaTime);
			}
			if (this.ResultsTimer > 2f)
			{
				UILabel uilabel2 = this.ResultsLabels[1];
				uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, uilabel2.color.a + Time.deltaTime);
			}
			if (this.ResultsTimer > 3f)
			{
				UILabel uilabel3 = this.ResultsLabels[2];
				uilabel3.color = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, uilabel3.color.a + Time.deltaTime);
			}
			if (this.ResultsTimer > 4f)
			{
				UILabel uilabel4 = this.ResultsLabels[3];
				uilabel4.color = new Color(uilabel4.color.r, uilabel4.color.g, uilabel4.color.b, uilabel4.color.a + Time.deltaTime);
			}
			if (this.ResultsTimer > 5f)
			{
				UILabel uilabel5 = this.ResultsLabels[4];
				uilabel5.color = new Color(uilabel5.color.r, uilabel5.color.g, uilabel5.color.b, uilabel5.color.a + Time.deltaTime);
			}
			if (this.ResultsTimer > 6f)
			{
				this.ContinueButton.color = new Color(this.ContinueButton.color.r, this.ContinueButton.color.g, this.ContinueButton.color.b, this.ContinueButton.color.a + Time.deltaTime);
				this.ContinueLabel.color = new Color(this.ContinueLabel.color.r, this.ContinueLabel.color.g, this.ContinueLabel.color.b, this.ContinueLabel.color.a + Time.deltaTime);
				if (this.ContinueButton.color.a > 1f)
				{
					this.ContinueButton.color = new Color(this.ContinueButton.color.r, this.ContinueButton.color.g, this.ContinueButton.color.b, 1f);
				}
				if (this.ContinueLabel.color.a > 1f)
				{
					this.ContinueLabel.color = new Color(this.ContinueLabel.color.r, this.ContinueLabel.color.g, this.ContinueLabel.color.b, 1f);
				}
			}
			if (Input.GetKeyDown("space"))
			{
				this.ShowResults = false;
				this.FadeResults = true;
				this.FadeOut = false;
				this.ResultsTimer = 0f;
			}
			if (this.ResultsTimer > 7f && Input.GetButtonDown("A"))
			{
				this.ShowResults = false;
				this.FadeResults = true;
				this.FadeOut = false;
				this.ResultsTimer = 0f;
			}
		}
		foreach (UILabel uilabel6 in this.ResultsLabels)
		{
			if (uilabel6.color.a > 1f)
			{
				uilabel6.color = new Color(uilabel6.color.r, uilabel6.color.g, uilabel6.color.b, 1f);
			}
		}
		if (this.FadeResults)
		{
			foreach (UILabel uilabel7 in this.ResultsLabels)
			{
				uilabel7.color = new Color(uilabel7.color.r, uilabel7.color.g, uilabel7.color.b, uilabel7.color.a - Time.deltaTime);
			}
			this.ContinueButton.color = new Color(this.ContinueButton.color.r, this.ContinueButton.color.g, this.ContinueButton.color.b, this.ContinueButton.color.a - Time.deltaTime);
			this.ContinueLabel.color = new Color(this.ContinueLabel.color.r, this.ContinueLabel.color.g, this.ContinueLabel.color.b, this.ContinueLabel.color.a - Time.deltaTime);
			if (this.ResultsLabels[0].color.a <= 0f)
			{
				if (this.BeginConfession)
				{
					this.LoveManager.Suitor = this.StudentManager.Students[1];
					this.LoveManager.Rival = this.StudentManager.Students[this.StudentManager.RivalID];
					this.LoveManager.Suitor.CharacterAnimation.enabled = true;
					this.LoveManager.Rival.CharacterAnimation.enabled = true;
					this.LoveManager.BeginConfession();
					Time.timeScale = 1f;
					base.enabled = false;
				}
				else if (this.GameOver)
				{
					this.Heartbroken.transform.parent.transform.parent = null;
					this.Heartbroken.transform.parent.gameObject.SetActive(true);
					this.Heartbroken.Noticed = false;
					base.transform.parent.transform.parent.gameObject.SetActive(false);
					if (!this.EndOfDay.gameObject.activeInHierarchy)
					{
						Time.timeScale = 1f;
					}
				}
				else if (this.LowRep)
				{
					this.Yandere.RPGCamera.enabled = false;
					this.Yandere.RPGCamera.transform.parent = this.LowRepGameOver.MyCamera;
					this.Yandere.RPGCamera.transform.localPosition = new Vector3(0f, 0f, 0f);
					this.Yandere.RPGCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					this.LowRepGameOver.gameObject.SetActive(true);
					this.UICamera.SetActive(false);
					this.FPS.SetActive(false);
					Time.timeScale = 1f;
					base.enabled = false;
				}
				else if (!this.TeacherReport)
				{
					if (this.EndOfDay.Phase == 1)
					{
						this.EndOfDay.gameObject.SetActive(true);
						this.EndOfDay.enabled = true;
						this.EndOfDay.Phase = 14;
						if (this.EndOfDay.PreviouslyActivated)
						{
							this.EndOfDay.Start();
						}
						for (int k = 0; k < 5; k++)
						{
							this.ResultsLabels[k].text = string.Empty;
						}
						base.enabled = false;
					}
				}
				else
				{
					this.DetermineResults();
					this.TeacherReport = false;
					this.FadeResults = false;
					this.ShowResults = true;
				}
			}
		}
	}

	// Token: 0x06001E8F RID: 7823 RVA: 0x0012E144 File Offset: 0x0012C544
	private void DetermineResults()
	{
		this.ResultsLabels[0].transform.parent.gameObject.SetActive(true);
		if (this.Show)
		{
			this.EndOfDay.gameObject.SetActive(true);
			base.enabled = false;
			for (int i = 0; i < 5; i++)
			{
				this.ResultsLabels[i].text = string.Empty;
			}
		}
		else if (this.Yandere.ShoulderCamera.GoingToCounselor)
		{
			this.ResultsLabels[0].text = "While Ayano was in the counselor's office,";
			this.ResultsLabels[1].text = "a corpse was discovered on school grounds.";
			this.ResultsLabels[2].text = "The school faculty was informed of the corpse,";
			this.ResultsLabels[3].text = "and the police were called to the school.";
			this.ResultsLabels[4].text = "No one is allowed to leave school until a police investigation has taken place.";
			this.TeacherReport = true;
			this.Show = true;
		}
		else if (this.Reputation.Reputation <= -100f)
		{
			this.ResultsLabels[0].text = "Ayano's bizarre conduct has been observed and discussed by many people.";
			this.ResultsLabels[1].text = "Word of Ayano's strange behavior has reached Senpai.";
			this.ResultsLabels[2].text = "Senpai is now aware that Ayano is a deranged person.";
			this.ResultsLabels[3].text = "From this day forward, Senpai will fear and avoid Ayano.";
			this.ResultsLabels[4].text = "Ayano will never have her Senpai's love.";
			this.LowRep = true;
		}
		else
		{
			bool flag = true;
			if (flag && DateGlobals.Weekday == DayOfWeek.Friday)
			{
				this.ResultsLabels[0].text = "This is the part where the game will determine whether or not the player has eliminated their rival.";
				this.ResultsLabels[1].text = "This game is still in development.";
				this.ResultsLabels[2].text = "The ''player eliminated rival'' state has not yet been implemented.";
				this.ResultsLabels[3].text = "Thank you for playtesting Yandere Simulator!";
				this.ResultsLabels[4].text = "Please check back soon for more updates!";
				this.GameOver = true;
			}
			else if (!this.Suicide && !this.PoisonScene)
			{
				if (this.Clock.HourTime < 18f)
				{
					if (this.Yandere.InClass)
					{
						this.ResultsLabels[0].text = "Ayano attempts to attend class without disposing of a corpse.";
					}
					else if (this.Yandere.Resting && this.Corpses > 0)
					{
						this.ResultsLabels[0].text = "Ayano rests without disposing of a corpse.";
					}
					else if (this.Yandere.Resting)
					{
						if (GameGlobals.SenpaiMourning)
						{
							this.ResultsLabels[0].text = "Ayano recovers from her injuries, and is ready to leave school.";
						}
						else
						{
							this.ResultsLabels[0].text = "Ayano recovers from her injuries, and is ready to leave school.";
						}
					}
					else if (GameGlobals.SenpaiMourning)
					{
						this.ResultsLabels[0].text = "Ayano is ready to leave school.";
					}
					else
					{
						this.ResultsLabels[0].text = "Ayano is ready to leave school.";
					}
				}
				else
				{
					this.ResultsLabels[0].text = "The school day has ended. Faculty members must walk through the school and tell any lingering students to leave.";
				}
				if (this.Suspended)
				{
					if (this.Clock.Weekday == 1)
					{
						this.RemainingDays = 5;
					}
					else if (this.Clock.Weekday == 2)
					{
						this.RemainingDays = 4;
					}
					else if (this.Clock.Weekday == 3)
					{
						this.RemainingDays = 3;
					}
					else if (this.Clock.Weekday == 4)
					{
						this.RemainingDays = 2;
					}
					else if (this.Clock.Weekday == 5)
					{
						this.RemainingDays = 1;
					}
					if (this.RemainingDays - this.SuspensionLength <= 0)
					{
						this.ResultsLabels[0].text = "Due to her suspension,";
						this.ResultsLabels[1].text = "Ayano will be unable";
						this.ResultsLabels[2].text = "to prevent her rival";
						this.ResultsLabels[3].text = "from confessing to Senpai.";
						this.ResultsLabels[4].text = "Ayano will never have Senpai.";
						this.GameOver = true;
					}
					else if (this.SuspensionLength == 1)
					{
						this.ResultsLabels[0].text = "Ayano has been sent home early.";
						this.ResultsLabels[1].text = string.Empty;
						this.ResultsLabels[2].text = "She won't be able to see Senpai again until tomorrow.";
						this.ResultsLabels[3].text = string.Empty;
						this.ResultsLabels[4].text = "Ayano's heart aches as she thinks of Senpai.";
					}
					else if (this.SuspensionLength == 2)
					{
						this.ResultsLabels[0].text = "Ayano has been sent home early.";
						this.ResultsLabels[1].text = string.Empty;
						this.ResultsLabels[2].text = "She will have to wait one day before returning to school.";
						this.ResultsLabels[3].text = string.Empty;
						this.ResultsLabels[4].text = "Ayano's heart aches as she thinks of Senpai.";
					}
					else
					{
						this.ResultsLabels[0].text = "Ayano has been sent home early.";
						this.ResultsLabels[1].text = string.Empty;
						this.ResultsLabels[2].text = "She will have to wait " + (this.SuspensionLength - 1) + " days before returning to school.";
						this.ResultsLabels[3].text = string.Empty;
						this.ResultsLabels[4].text = "Ayano's heart aches as she thinks of Senpai.";
					}
				}
				else
				{
					if (this.Yandere.RedPaint)
					{
						this.BloodyClothing--;
					}
					if (this.Corpses == 0 && this.LimbParent.childCount == 0 && this.BloodParent.childCount == 0 && this.BloodyWeapons == 0 && this.BloodyClothing == 0 && !this.SuicideScene)
					{
						if (this.Yandere.Sanity < 66.66666f || (this.Yandere.Bloodiness > 0f && !this.Yandere.RedPaint))
						{
							this.ResultsLabels[1].text = "Ayano is approached by a faculty member.";
							if (this.Yandere.Bloodiness > 0f)
							{
								this.ResultsLabels[2].text = "The faculty member immediately notices the blood staining her clothing.";
								this.ResultsLabels[3].text = "Ayano is not able to convince the faculty member that nothing is wrong.";
								this.ResultsLabels[4].text = "The faculty member calls the police.";
								this.TeacherReport = true;
								this.Show = true;
							}
							else
							{
								this.ResultsLabels[2].text = "Ayano exhibited extremely erratic behavior, frightening the faculty member.";
								this.ResultsLabels[3].text = "The faculty member becomes angry with Ayano, but Ayano leaves before the situation gets worse.";
								this.ResultsLabels[4].text = "Ayano returns home.";
							}
						}
						else if (this.Yandere.Inventory.RivalPhone && !this.StudentManager.RivalEliminated)
						{
							this.ResultsLabels[1].text = "Osana tells the faculty that her phone is missing.";
							this.ResultsLabels[2].text = "Suspecting theft, the faculty check all students' belongings before they are allowed to leave school.";
							this.ResultsLabels[3].text = "Osana's stolen phone is found on Ayano's person.";
							this.ResultsLabels[4].text = "Ayano is expelled from school for stealing from another student.";
							this.GameOver = true;
							this.Heartbroken.Counselor.Expelled = true;
						}
						else if (DateGlobals.Weekday == DayOfWeek.Friday)
						{
							if (!this.StudentManager.RivalEliminated)
							{
								this.ResultsLabels[0].text = "Ayano has failed to eliminate Osana before Friday evening.";
								this.ResultsLabels[1].text = "Osana asks Senpai to meet her under the cherry tree behind the school.";
								this.ResultsLabels[2].text = "As cherry blossoms fall around them...";
								this.ResultsLabels[3].text = "...Osana confesses her feelings for Senpai.";
								this.ResultsLabels[4].text = "Ayano watches from a short distance away...";
								this.BeginConfession = true;
							}
							else
							{
								this.ResultsLabels[0].text = "Ayano no longer has to worry about competing with Osana for Senpai's love.";
								this.ResultsLabels[1].text = "Ayano considers confessing her love to Senpai...";
								this.ResultsLabels[2].text = "...but she cannot build up the courage to speak to him.";
								this.ResultsLabels[3].text = "Ayano follows Senpai out of school and watches him from a distance until he has returned to his home.";
								this.ResultsLabels[4].text = "Then, Ayano returns to her own home, and considers what she should do next...";
							}
						}
						else
						{
							if (this.Clock.HourTime < 18f)
							{
								Debug.Log("Senpai's position is: " + this.Yandere.Senpai.position);
								if (this.Yandere.Senpai.position.z > -75f)
								{
									this.ResultsLabels[1].text = "However, she can't bring herself to leave before Senpai does.";
									this.ResultsLabels[2].text = "Ayano waits at the school's entrance until Senpai eventually appears.";
									this.ResultsLabels[3].text = "She follows him and watches him from a distance until he has returned to his home.";
									this.ResultsLabels[4].text = "Then, Ayano returns to her house.";
								}
								else
								{
									this.ResultsLabels[1].text = "Ayano quickly runs out of school, determined to catch a glimpse of Senpai as he walks home.";
									this.ResultsLabels[2].text = "Eventually, she catches up to him.";
									this.ResultsLabels[3].text = "Ayano follows Senpai and watches him from a distance until he has returned to his home.";
									this.ResultsLabels[4].text = "Then, Ayano returns to her house.";
								}
							}
							else
							{
								this.ResultsLabels[1].text = "Like all other students, Ayano is instructed to leave school.";
								this.ResultsLabels[2].text = "After exiting school, Ayano locates Senpai.";
								this.ResultsLabels[3].text = "Ayano follows Senpai and watches him from a distance until he has returned to his home.";
								this.ResultsLabels[4].text = "Then, Ayano returns to her house.";
							}
							if (GameGlobals.SenpaiMourning)
							{
								this.ResultsLabels[1].text = "Like all other students, Ayano is instructed to leave school.";
								this.ResultsLabels[2].text = "Ayano leaves school.";
								this.ResultsLabels[3].text = "Ayano returns to her home.";
								this.ResultsLabels[4].text = "Her heart aches as she thinks of Senpai.";
							}
						}
					}
					else if (this.Corpses > 0)
					{
						this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a corpse.";
						this.ResultsLabels[2].text = "The faculty member immediately calls the police.";
						this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
						this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";
						this.TeacherReport = true;
						this.Show = true;
					}
					else if (this.LimbParent.childCount > 0)
					{
						this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a dismembered body part.";
						this.ResultsLabels[2].text = "The faculty member decides to call the police.";
						this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
						this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";
						this.TeacherReport = true;
						this.Show = true;
					}
					else if (this.BloodParent.childCount > 0 || this.BloodyClothing > 0)
					{
						this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a mysterious blood stain.";
						this.ResultsLabels[2].text = "The faculty member decides to call the police.";
						this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
						this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";
						this.TeacherReport = true;
						this.Show = true;
					}
					else if (this.BloodyWeapons > 0)
					{
						this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a mysterious bloody weapon.";
						this.ResultsLabels[2].text = "The faculty member decides to call the police.";
						this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
						this.ResultsLabels[4].text = "The faculty do not allow any students to leave the school until a police investigation has taken place.";
						this.TeacherReport = true;
						this.Show = true;
					}
					else if (this.SuicideScene)
					{
						this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a pair of shoes on the rooftop.";
						this.ResultsLabels[2].text = "The faculty member fears that there has been a suicide, but cannot find a corpse anywhere. The faculty member does not take any action.";
						this.ResultsLabels[3].text = "Ayano leaves school and follows Senpai, watching him as he walks home.";
						this.ResultsLabels[4].text = "Once he is safely home, Ayano returns to her own home.";
						if (GameGlobals.SenpaiMourning)
						{
							this.ResultsLabels[3].text = "Ayano leaves school.";
							this.ResultsLabels[4].text = "Ayano returns home.";
						}
					}
				}
			}
			else if (this.Suicide)
			{
				if (!this.Yandere.InClass)
				{
					this.ResultsLabels[0].text = "The school day has ended. Faculty members must walk through the school and tell any lingering students to leave.";
				}
				else
				{
					this.ResultsLabels[0].text = "Ayano attempts to attend class without disposing of a corpse.";
				}
				this.ResultsLabels[1].text = "While walking around the school, a faculty member discovers a corpse.";
				this.ResultsLabels[2].text = "It appears as though a student has committed suicide.";
				this.ResultsLabels[3].text = "The faculty member informs the rest of the faculty about her discovery.";
				this.ResultsLabels[4].text = "The faculty members agree to call the police and report the student's death.";
				this.TeacherReport = true;
				this.Show = true;
			}
			else if (this.PoisonScene)
			{
				this.ResultsLabels[0].text = "A faculty member discovers the student who Ayano poisoned.";
				this.ResultsLabels[1].text = "The faculty member calls for an ambulance immediately.";
				this.ResultsLabels[2].text = "The faculty member suspects that the student's death was a murder.";
				this.ResultsLabels[3].text = "The faculty member also calls for the police.";
				this.ResultsLabels[4].text = "The school's students are not allowed to leave until a police investigation has taken place.";
				this.TeacherReport = true;
				this.Show = true;
			}
		}
	}

	// Token: 0x06001E90 RID: 7824 RVA: 0x0012EE3C File Offset: 0x0012D23C
	public void KillStudents()
	{
		if (this.Deaths > 0)
		{
			for (int i = 2; i < this.StudentManager.NPCsTotal + 1; i++)
			{
				if (StudentGlobals.GetStudentDying(i))
				{
					if (i < 90)
					{
						SchoolGlobals.SchoolAtmosphere -= 0.05f;
					}
					else
					{
						SchoolGlobals.SchoolAtmosphere -= 0.15f;
					}
					if (this.JSON.Students[i].Club == ClubType.Council)
					{
						SchoolGlobals.SchoolAtmosphere -= 1f;
						SchoolGlobals.HighSecurity = true;
					}
					StudentGlobals.SetStudentDead(i, true);
					PlayerGlobals.Kills++;
				}
			}
			SchoolGlobals.SchoolAtmosphere -= (float)this.Corpses * 0.05f;
			if (this.DrownVictims + this.Corpses > 0)
			{
				foreach (RagdollScript ragdollScript in this.CorpseList)
				{
					if (ragdollScript != null && StudentGlobals.MemorialStudents < 9)
					{
						StudentGlobals.MemorialStudents++;
						if (StudentGlobals.MemorialStudents == 1)
						{
							StudentGlobals.MemorialStudent1 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 2)
						{
							StudentGlobals.MemorialStudent2 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 3)
						{
							StudentGlobals.MemorialStudent3 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 4)
						{
							StudentGlobals.MemorialStudent4 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 5)
						{
							StudentGlobals.MemorialStudent5 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 6)
						{
							StudentGlobals.MemorialStudent6 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 7)
						{
							StudentGlobals.MemorialStudent7 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 8)
						{
							StudentGlobals.MemorialStudent8 = ragdollScript.Student.StudentID;
						}
						else if (StudentGlobals.MemorialStudents == 9)
						{
							StudentGlobals.MemorialStudent9 = ragdollScript.Student.StudentID;
						}
					}
				}
			}
		}
		else if (!SchoolGlobals.HighSecurity)
		{
			SchoolGlobals.SchoolAtmosphere += 0.2f;
		}
		SchoolGlobals.SchoolAtmosphere = Mathf.Clamp01(SchoolGlobals.SchoolAtmosphere);
		for (int k = 1; k < this.StudentManager.StudentsTotal; k++)
		{
			StudentScript studentScript = this.StudentManager.Students[k];
			if (studentScript != null && studentScript.Grudge && studentScript.Persona != PersonaType.Evil)
			{
				StudentGlobals.SetStudentGrudge(k, true);
				if (studentScript.OriginalPersona == PersonaType.Sleuth && !StudentGlobals.GetStudentDying(k))
				{
					StudentGlobals.SetStudentGrudge(56, true);
					StudentGlobals.SetStudentGrudge(57, true);
					StudentGlobals.SetStudentGrudge(58, true);
					StudentGlobals.SetStudentGrudge(59, true);
					StudentGlobals.SetStudentGrudge(60, true);
				}
			}
		}
	}

	// Token: 0x06001E91 RID: 7825 RVA: 0x0012F148 File Offset: 0x0012D548
	public void BeginFadingOut()
	{
		this.DayOver = true;
		this.StudentManager.StopMoving();
		this.Darkness.enabled = true;
		this.Yandere.StopLaughing();
		this.Clock.StopTime = true;
		this.FadeOut = true;
		if (!this.EndOfDay.gameObject.activeInHierarchy)
		{
			Time.timeScale = 1f;
		}
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x0012F1B0 File Offset: 0x0012D5B0
	public void UpdateCorpses()
	{
		foreach (RagdollScript ragdollScript in this.CorpseList)
		{
			if (ragdollScript != null)
			{
				ragdollScript.Prompt.HideButton[3] = true;
				if (ClassGlobals.PhysicalGrade + ClassGlobals.PhysicalBonus > 0 && !ragdollScript.Tranquil)
				{
					ragdollScript.Prompt.HideButton[3] = false;
				}
			}
		}
	}

	// Token: 0x040027B8 RID: 10168
	public LowRepGameOverScript LowRepGameOver;

	// Token: 0x040027B9 RID: 10169
	public StudentManagerScript StudentManager;

	// Token: 0x040027BA RID: 10170
	public ClubManagerScript ClubManager;

	// Token: 0x040027BB RID: 10171
	public HeartbrokenScript Heartbroken;

	// Token: 0x040027BC RID: 10172
	public LoveManagerScript LoveManager;

	// Token: 0x040027BD RID: 10173
	public PauseScreenScript PauseScreen;

	// Token: 0x040027BE RID: 10174
	public ReputationScript Reputation;

	// Token: 0x040027BF RID: 10175
	public TranqCaseScript TranqCase;

	// Token: 0x040027C0 RID: 10176
	public EndOfDayScript EndOfDay;

	// Token: 0x040027C1 RID: 10177
	public JukeboxScript Jukebox;

	// Token: 0x040027C2 RID: 10178
	public YandereScript Yandere;

	// Token: 0x040027C3 RID: 10179
	public ClockScript Clock;

	// Token: 0x040027C4 RID: 10180
	public JsonScript JSON;

	// Token: 0x040027C5 RID: 10181
	public UIPanel Panel;

	// Token: 0x040027C6 RID: 10182
	public GameObject HeartbeatCamera;

	// Token: 0x040027C7 RID: 10183
	public GameObject DetectionCamera;

	// Token: 0x040027C8 RID: 10184
	public GameObject SuicideStudent;

	// Token: 0x040027C9 RID: 10185
	public GameObject UICamera;

	// Token: 0x040027CA RID: 10186
	public GameObject Icons;

	// Token: 0x040027CB RID: 10187
	public GameObject FPS;

	// Token: 0x040027CC RID: 10188
	public Transform BloodParent;

	// Token: 0x040027CD RID: 10189
	public Transform LimbParent;

	// Token: 0x040027CE RID: 10190
	public RagdollScript[] CorpseList;

	// Token: 0x040027CF RID: 10191
	public UILabel[] ResultsLabels;

	// Token: 0x040027D0 RID: 10192
	public UILabel ContinueLabel;

	// Token: 0x040027D1 RID: 10193
	public UILabel TimeLabel;

	// Token: 0x040027D2 RID: 10194
	public UISprite ContinueButton;

	// Token: 0x040027D3 RID: 10195
	public UISprite Darkness;

	// Token: 0x040027D4 RID: 10196
	public UISprite BloodIcon;

	// Token: 0x040027D5 RID: 10197
	public UISprite UniformIcon;

	// Token: 0x040027D6 RID: 10198
	public UISprite WeaponIcon;

	// Token: 0x040027D7 RID: 10199
	public UISprite CorpseIcon;

	// Token: 0x040027D8 RID: 10200
	public UISprite PartsIcon;

	// Token: 0x040027D9 RID: 10201
	public UISprite SanityIcon;

	// Token: 0x040027DA RID: 10202
	public string ElectrocutedStudentName = string.Empty;

	// Token: 0x040027DB RID: 10203
	public string DrownedStudentName = string.Empty;

	// Token: 0x040027DC RID: 10204
	public bool BloodDisposed;

	// Token: 0x040027DD RID: 10205
	public bool UniformDisposed;

	// Token: 0x040027DE RID: 10206
	public bool WeaponDisposed;

	// Token: 0x040027DF RID: 10207
	public bool CorpseDisposed;

	// Token: 0x040027E0 RID: 10208
	public bool PartsDisposed;

	// Token: 0x040027E1 RID: 10209
	public bool SanityRestored;

	// Token: 0x040027E2 RID: 10210
	public bool MurderSuicideScene;

	// Token: 0x040027E3 RID: 10211
	public bool ElectroScene;

	// Token: 0x040027E4 RID: 10212
	public bool SuicideScene;

	// Token: 0x040027E5 RID: 10213
	public bool PoisonScene;

	// Token: 0x040027E6 RID: 10214
	public bool MurderScene;

	// Token: 0x040027E7 RID: 10215
	public bool BeginConfession;

	// Token: 0x040027E8 RID: 10216
	public bool TeacherReport;

	// Token: 0x040027E9 RID: 10217
	public bool ClubActivity;

	// Token: 0x040027EA RID: 10218
	public bool CouncilDeath;

	// Token: 0x040027EB RID: 10219
	public bool MaskReported;

	// Token: 0x040027EC RID: 10220
	public bool FadeResults;

	// Token: 0x040027ED RID: 10221
	public bool ShowResults;

	// Token: 0x040027EE RID: 10222
	public bool GameOver;

	// Token: 0x040027EF RID: 10223
	public bool DayOver;

	// Token: 0x040027F0 RID: 10224
	public bool Delayed;

	// Token: 0x040027F1 RID: 10225
	public bool FadeOut;

	// Token: 0x040027F2 RID: 10226
	public bool Suicide;

	// Token: 0x040027F3 RID: 10227
	public bool Called;

	// Token: 0x040027F4 RID: 10228
	public bool LowRep;

	// Token: 0x040027F5 RID: 10229
	public bool Show;

	// Token: 0x040027F6 RID: 10230
	public int IncineratedWeapons;

	// Token: 0x040027F7 RID: 10231
	public int BloodyClothing;

	// Token: 0x040027F8 RID: 10232
	public int BloodyWeapons;

	// Token: 0x040027F9 RID: 10233
	public int HiddenCorpses;

	// Token: 0x040027FA RID: 10234
	public int MurderWeapons;

	// Token: 0x040027FB RID: 10235
	public int PhotoEvidence;

	// Token: 0x040027FC RID: 10236
	public int DrownVictims;

	// Token: 0x040027FD RID: 10237
	public int BodyParts;

	// Token: 0x040027FE RID: 10238
	public int Witnesses;

	// Token: 0x040027FF RID: 10239
	public int Corpses;

	// Token: 0x04002800 RID: 10240
	public int Deaths;

	// Token: 0x04002801 RID: 10241
	public float ResultsTimer;

	// Token: 0x04002802 RID: 10242
	public float Timer;

	// Token: 0x04002803 RID: 10243
	public int Minutes;

	// Token: 0x04002804 RID: 10244
	public int Seconds;

	// Token: 0x04002805 RID: 10245
	public int SuspensionLength;

	// Token: 0x04002806 RID: 10246
	public int RemainingDays;

	// Token: 0x04002807 RID: 10247
	public bool Suspended;
}
