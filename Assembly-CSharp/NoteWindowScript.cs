﻿using System;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class NoteWindowScript : MonoBehaviour
{
	// Token: 0x06001DF0 RID: 7664 RVA: 0x00120034 File Offset: 0x0011E434
	private void Start()
	{
		this.SubMenu.transform.localScale = Vector3.zero;
		base.transform.localPosition = new Vector3(455f, -965f, 0f);
		base.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
		this.OriginalText[1] = this.SlotLabels[1].text;
		this.OriginalText[2] = this.SlotLabels[2].text;
		this.OriginalText[3] = this.SlotLabels[3].text;
		this.UpdateHighlights();
		this.UpdateSubLabels();
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x001200E0 File Offset: 0x0011E4E0
	public void BecomeLifeNote()
	{
		this.Stationery.mainTexture = this.LifeNoteTexture;
		this.Stationery.color = new Color(1f, 1f, 1f, 1f);
		this.Background2.color = new Color(0f, 0f, 0f, 1f);
		foreach (UILabel uilabel in this.Labels)
		{
			if (uilabel != null)
			{
				uilabel.color = new Color(1f, 1f, 1f, 1f);
			}
		}
		this.Labels[1].color = new Color(1f, 1f, 1f, 0f);
		this.Labels[2].color = new Color(1f, 1f, 1f, 0f);
		this.Labels[3].transform.localPosition = new Vector3(-365f, 265f, 0f);
		this.Labels[3].text = "______________";
		this.Labels[4].text = "will die from";
		this.Labels[8].color = new Color(1f, 1f, 1f, 0f);
		this.SlotHighlights[1].transform.localPosition = new Vector3(-100f, 280f, 0f);
		foreach (UILabel uilabel2 in this.SubLabels)
		{
			if (uilabel2 != null)
			{
				uilabel2.color = new Color(1f, 1f, 1f, 1f);
			}
		}
		this.LifeNote = true;
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x001202CC File Offset: 0x0011E6CC
	private void Update()
	{
		float t = Time.unscaledDeltaTime * 10f;
		if (!this.Show)
		{
			if (this.Rotation > -90f)
			{
				base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, new Vector3(455f, -965f, 0f), t);
				this.Rotation = Mathf.Lerp(this.Rotation, -91f, t);
				base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, this.Rotation);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, Vector3.zero, t);
			this.Rotation = Mathf.Lerp(this.Rotation, 0f, t);
			base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, base.transform.localEulerAngles.y, this.Rotation);
			if (!this.Selecting)
			{
				if (this.SubMenu.transform.localScale.x > 0.1f)
				{
					this.SubMenu.transform.localScale = Vector3.Lerp(this.SubMenu.transform.localScale, Vector3.zero, t);
				}
				else
				{
					this.SubMenu.transform.localScale = Vector3.zero;
				}
				if (this.InputManager.TappedDown)
				{
					this.Slot++;
					if (this.Slot > 3)
					{
						this.Slot = 1;
					}
					this.UpdateHighlights();
				}
				if (this.InputManager.TappedUp)
				{
					this.Slot--;
					if (this.Slot < 1)
					{
						this.Slot = 3;
					}
					this.UpdateHighlights();
				}
				if (Input.GetButtonDown("A"))
				{
					if (this.LifeNote && this.Slot == 1)
					{
						this.Yandere.PauseScreen.transform.parent.GetComponent<UIPanel>().alpha = 1f;
						this.Yandere.PauseScreen.StudentInfoMenu.UsingLifeNote = true;
						this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
						this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
						this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
						this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
						this.Yandere.PauseScreen.StudentInfoMenu.GrabbedPortraits = false;
						this.Yandere.PauseScreen.MainMenu.SetActive(false);
						this.Yandere.PauseScreen.Panel.enabled = true;
						this.Yandere.PauseScreen.Sideways = true;
						this.Yandere.PauseScreen.Show = true;
						Time.timeScale = 0.0001f;
						this.Yandere.PromptBar.ClearButtons();
						this.Yandere.PromptBar.Label[1].text = "Cancel";
						this.Yandere.PromptBar.UpdateButtons();
						this.Yandere.PromptBar.Show = true;
						base.gameObject.SetActive(false);
					}
					else
					{
						this.PromptBar.Label[2].text = string.Empty;
						this.PromptBar.UpdateButtons();
						this.Selecting = true;
						this.UpdateSubLabels();
					}
				}
				if (Input.GetButtonDown("B"))
				{
					this.Exit();
				}
				if (Input.GetButtonDown("X") && this.SlotsFilled[1] && this.SlotsFilled[2] && this.SlotsFilled[3])
				{
					if (this.LifeNote)
					{
						AudioSource.PlayClipAtPoint(this.Yandere.DramaticWriting, this.Yandere.transform.position);
						this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
						this.Yandere.CharacterAnimation["f02_dramaticWriting_00"].speed = 2f;
						this.Yandere.CharacterAnimation["f02_dramaticWriting_00"].time = 0f;
						this.Yandere.CharacterAnimation["f02_dramaticWriting_00"].weight = 0.75f;
						this.Yandere.CharacterAnimation.CrossFade("f02_dramaticWriting_00");
						this.Yandere.WritingName = true;
						this.Exit();
					}
					else
					{
						this.NoteLocker.MeetID = this.MeetID;
						this.NoteLocker.MeetTime = this.TimeID;
						this.NoteLocker.Prompt.enabled = false;
						this.NoteLocker.CanLeaveNote = false;
						this.NoteLocker.NoteLeft = true;
						if (this.NoteLocker.Student.StudentID == 30)
						{
							if (this.SlotLabels[1].text == this.Subjects[10])
							{
								this.NoteLocker.Success = true;
							}
						}
						else if (this.NoteLocker.Student.StudentID == 5 && this.NoteLocker.Student.Bullied && this.SlotLabels[1].text == this.Subjects[7] && this.MeetID > 7)
						{
							this.NoteLocker.Success = true;
						}
						if (this.NoteLocker.Student.Persona == PersonaType.Loner && this.SlotLabels[1].text == this.Subjects[1])
						{
							this.NoteLocker.Success = true;
						}
						else if (this.NoteLocker.Student.Persona == PersonaType.TeachersPet && this.SlotLabels[1].text == this.Subjects[2])
						{
							this.NoteLocker.Success = true;
						}
						else if (this.NoteLocker.Student.Persona == PersonaType.Heroic || this.NoteLocker.Student.Persona == PersonaType.Sleuth)
						{
							if (this.SlotLabels[1].text == this.Subjects[3])
							{
								this.NoteLocker.Success = true;
							}
						}
						else if (this.NoteLocker.Student.Persona == PersonaType.Coward && this.SlotLabels[1].text == this.Subjects[4])
						{
							this.NoteLocker.Success = true;
						}
						else if (this.NoteLocker.Student.Persona == PersonaType.SocialButterfly)
						{
							if (this.SlotLabels[1].text == this.Subjects[1] || this.SlotLabels[1].text == this.Subjects[5])
							{
								this.NoteLocker.Success = true;
							}
						}
						else if (this.NoteLocker.Student.Persona == PersonaType.PhoneAddict && this.SlotLabels[1].text == this.Subjects[6])
						{
							this.NoteLocker.Success = true;
						}
						else if (this.NoteLocker.Student.StudentID == 2 || this.NoteLocker.Student.StudentID == 3 || this.NoteLocker.Student.Club == ClubType.Occult)
						{
							if (this.SlotLabels[1].text == this.Subjects[8])
							{
								this.NoteLocker.Success = true;
							}
						}
						else if (this.NoteLocker.Student.Club == ClubType.Bully && (this.SlotLabels[1].text == this.Subjects[5] || this.SlotLabels[1].text == this.Subjects[9]))
						{
							this.NoteLocker.Success = true;
						}
						this.NoteLocker.FindStudentLocker.Prompt.Hide();
						this.NoteLocker.FindStudentLocker.Prompt.enabled = false;
						this.NoteLocker.FindStudentLocker.enabled = false;
						this.NoteLocker.transform.GetChild(0).gameObject.SetActive(false);
					}
					this.Exit();
				}
			}
			else
			{
				this.SubMenu.transform.localScale = Vector3.Lerp(this.SubMenu.transform.localScale, new Vector3(1f, 1f, 1f), t);
				if (this.InputManager.TappedDown)
				{
					this.SubSlot++;
					if (this.LifeNote && this.Slot == 2)
					{
						if (this.SubSlot > 6)
						{
							this.SubSlot = 1;
						}
					}
					else if (this.SubSlot > 10)
					{
						this.SubSlot = 1;
					}
					this.SubHighlight.localPosition = new Vector3(this.SubHighlight.localPosition.x, 550f - 100f * (float)this.SubSlot, this.SubHighlight.localPosition.z);
				}
				if (this.InputManager.TappedUp)
				{
					this.SubSlot--;
					if (this.LifeNote && this.Slot == 2)
					{
						if (this.SubSlot < 1)
						{
							this.SubSlot = 6;
						}
					}
					else if (this.SubSlot < 1)
					{
						this.SubSlot = 10;
					}
					this.SubHighlight.localPosition = new Vector3(this.SubHighlight.localPosition.x, 550f - 100f * (float)this.SubSlot, this.SubHighlight.localPosition.z);
				}
				if (Input.GetButtonDown("A") && this.SubLabels[this.SubSlot].color.a > 0.5f && this.SubLabels[this.SubSlot].text != string.Empty && this.SubLabels[this.SubSlot].text != "??????????")
				{
					this.SlotLabels[this.Slot].text = this.SubLabels[this.SubSlot].text;
					this.SlotsFilled[this.Slot] = true;
					if (this.Slot == 2)
					{
						this.MeetID = this.SubSlot;
					}
					if (this.Slot == 3)
					{
						this.TimeID = this.Hours[this.SubSlot];
					}
					this.CheckForCompletion();
					this.Selecting = false;
					this.SubSlot = 1;
					this.SubHighlight.localPosition = new Vector3(this.SubHighlight.localPosition.x, 450f, this.SubHighlight.localPosition.z);
				}
				if (Input.GetButtonDown("B"))
				{
					this.CheckForCompletion();
					this.Selecting = false;
					this.SubSlot = 1;
					this.SubHighlight.localPosition = new Vector3(this.SubHighlight.localPosition.x, 450f, this.SubHighlight.localPosition.z);
				}
			}
			UISprite uisprite = this.SlotHighlights[this.Slot];
			if (!this.Fade)
			{
				uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, uisprite.color.a + 0.0166666675f);
				if (uisprite.color.a >= 0.5f)
				{
					this.Fade = true;
				}
			}
			else
			{
				uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, uisprite.color.a - 0.0166666675f);
				if (uisprite.color.a <= 0f)
				{
					this.Fade = false;
				}
			}
		}
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x00120FF0 File Offset: 0x0011F3F0
	private void UpdateHighlights()
	{
		for (int i = 1; i < this.SlotHighlights.Length; i++)
		{
			UISprite uisprite = this.SlotHighlights[i];
			uisprite.color = new Color(uisprite.color.r, uisprite.color.g, uisprite.color.b, 0f);
		}
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x0012105C File Offset: 0x0011F45C
	private void UpdateSubLabels()
	{
		if (this.Slot == 1)
		{
			this.ID = 1;
			while (this.ID < this.SubLabels.Length)
			{
				UILabel uilabel = this.SubLabels[this.ID];
				uilabel.text = this.Subjects[this.ID];
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
				this.ID++;
			}
			if (!EventGlobals.Event1)
			{
				this.SubLabels[10].text = "??????????";
			}
		}
		else if (this.Slot == 2)
		{
			this.ID = 1;
			while (this.ID < this.SubLabels.Length)
			{
				UILabel uilabel2 = this.SubLabels[this.ID];
				uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 1f);
				if (this.LifeNote)
				{
					uilabel2.text = this.MurderMethods[this.ID];
				}
				else
				{
					uilabel2.text = this.Locations[this.ID];
				}
				this.ID++;
			}
		}
		else if (this.Slot == 3)
		{
			this.ID = 1;
			while (this.ID < this.SubLabels.Length)
			{
				UILabel uilabel3 = this.SubLabels[this.ID];
				uilabel3.text = this.Times[this.ID];
				uilabel3.color = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, 1f);
				this.ID++;
			}
			this.DisableOptions();
		}
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x00121288 File Offset: 0x0011F688
	public void CheckForCompletion()
	{
		if (this.SlotsFilled[1] && this.SlotsFilled[2] && this.SlotsFilled[3])
		{
			this.PromptBar.Label[2].text = "Finish";
			this.PromptBar.UpdateButtons();
		}
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x001212E0 File Offset: 0x0011F6E0
	private void Exit()
	{
		this.UpdateHighlights();
		if (!this.Yandere.WritingName)
		{
			this.Yandere.CanMove = true;
		}
		this.Yandere.RPGCamera.enabled = true;
		this.Yandere.Blur.enabled = false;
		this.Yandere.HUD.alpha = 1f;
		Time.timeScale = 1f;
		this.Show = false;
		this.Slot = 1;
		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.Label[1].text = string.Empty;
		this.PromptBar.Label[2].text = string.Empty;
		this.PromptBar.Label[4].text = string.Empty;
		this.PromptBar.Show = false;
		this.PromptBar.UpdateButtons();
		this.SlotLabels[1].text = this.OriginalText[1];
		this.SlotLabels[2].text = this.OriginalText[2];
		this.SlotLabels[3].text = this.OriginalText[3];
		this.SlotsFilled[1] = false;
		this.SlotsFilled[2] = false;
		this.SlotsFilled[3] = false;
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x0012142C File Offset: 0x0011F82C
	private void DisableOptions()
	{
		if (this.Clock.HourTime >= 7.25f)
		{
			UILabel uilabel = this.SubLabels[1];
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 7.5f)
		{
			UILabel uilabel2 = this.SubLabels[2];
			uilabel2.color = new Color(uilabel2.color.r, uilabel2.color.g, uilabel2.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 7.75f)
		{
			UILabel uilabel3 = this.SubLabels[3];
			uilabel3.color = new Color(uilabel3.color.r, uilabel3.color.g, uilabel3.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 8f)
		{
			UILabel uilabel4 = this.SubLabels[4];
			uilabel4.color = new Color(uilabel4.color.r, uilabel4.color.g, uilabel4.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 8.25f)
		{
			UILabel uilabel5 = this.SubLabels[5];
			uilabel5.color = new Color(uilabel5.color.r, uilabel5.color.g, uilabel5.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 15.5f)
		{
			UILabel uilabel6 = this.SubLabels[6];
			uilabel6.color = new Color(uilabel6.color.r, uilabel6.color.g, uilabel6.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 16f)
		{
			UILabel uilabel7 = this.SubLabels[7];
			uilabel7.color = new Color(uilabel7.color.r, uilabel7.color.g, uilabel7.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 16.5f)
		{
			UILabel uilabel8 = this.SubLabels[8];
			uilabel8.color = new Color(uilabel8.color.r, uilabel8.color.g, uilabel8.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 17f)
		{
			UILabel uilabel9 = this.SubLabels[9];
			uilabel9.color = new Color(uilabel9.color.r, uilabel9.color.g, uilabel9.color.b, 0.5f);
		}
		if (this.Clock.HourTime >= 17.5f)
		{
			UILabel uilabel10 = this.SubLabels[10];
			uilabel10.color = new Color(uilabel10.color.r, uilabel10.color.g, uilabel10.color.b, 0.5f);
		}
	}

	// Token: 0x040025E5 RID: 9701
	public InputManagerScript InputManager;

	// Token: 0x040025E6 RID: 9702
	public NoteLockerScript NoteLocker;

	// Token: 0x040025E7 RID: 9703
	public PromptBarScript PromptBar;

	// Token: 0x040025E8 RID: 9704
	public YandereScript Yandere;

	// Token: 0x040025E9 RID: 9705
	public ClockScript Clock;

	// Token: 0x040025EA RID: 9706
	public Transform SubHighlight;

	// Token: 0x040025EB RID: 9707
	public Transform SubMenu;

	// Token: 0x040025EC RID: 9708
	public UISprite[] SlotHighlights;

	// Token: 0x040025ED RID: 9709
	public UILabel[] SlotLabels;

	// Token: 0x040025EE RID: 9710
	public UILabel[] SubLabels;

	// Token: 0x040025EF RID: 9711
	public string[] OriginalText;

	// Token: 0x040025F0 RID: 9712
	public string[] Subjects;

	// Token: 0x040025F1 RID: 9713
	public string[] Locations;

	// Token: 0x040025F2 RID: 9714
	public string[] Times;

	// Token: 0x040025F3 RID: 9715
	public float[] Hours;

	// Token: 0x040025F4 RID: 9716
	public bool[] SlotsFilled;

	// Token: 0x040025F5 RID: 9717
	public int SubSlot;

	// Token: 0x040025F6 RID: 9718
	public int MeetID;

	// Token: 0x040025F7 RID: 9719
	public int Slot = 1;

	// Token: 0x040025F8 RID: 9720
	public float Rotation;

	// Token: 0x040025F9 RID: 9721
	public float TimeID;

	// Token: 0x040025FA RID: 9722
	public int ID;

	// Token: 0x040025FB RID: 9723
	public bool Selecting;

	// Token: 0x040025FC RID: 9724
	public bool Fade;

	// Token: 0x040025FD RID: 9725
	public bool Show;

	// Token: 0x040025FE RID: 9726
	public UITexture Stationery;

	// Token: 0x040025FF RID: 9727
	public UISprite Background1;

	// Token: 0x04002600 RID: 9728
	public UISprite Background2;

	// Token: 0x04002601 RID: 9729
	public Texture LifeNoteTexture;

	// Token: 0x04002602 RID: 9730
	public UILabel[] Labels;

	// Token: 0x04002603 RID: 9731
	public bool LifeNote;

	// Token: 0x04002604 RID: 9732
	public int TargetStudent;

	// Token: 0x04002605 RID: 9733
	public string[] MurderMethods;
}
