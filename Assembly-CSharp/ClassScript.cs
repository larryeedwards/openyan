using System;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class ClassScript : MonoBehaviour
{
	// Token: 0x060017DB RID: 6107 RVA: 0x000BE5F8 File Offset: 0x000BC9F8
	private void Start()
	{
		this.GradeUpWindow.localScale = Vector3.zero;
		this.Subject[1] = ClassGlobals.Biology;
		this.Subject[2] = ClassGlobals.Chemistry;
		this.Subject[3] = ClassGlobals.Language;
		this.Subject[4] = ClassGlobals.Physical;
		this.Subject[5] = ClassGlobals.Psychology;
		this.DescLabel.text = this.Desc[this.Selected];
		this.UpdateSubjectLabels();
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		this.UpdateBars();
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x000BE6C8 File Offset: 0x000BCAC8
	private void Update()
	{
		if (this.Show)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a - Time.deltaTime);
			if (this.Darkness.color.a <= 0f)
			{
				if (Input.GetKeyDown(KeyCode.Backslash))
				{
					this.GivePoints();
				}
				if (Input.GetKeyDown(KeyCode.P))
				{
					this.MaxPhysical();
				}
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 0f);
				if (this.InputManager.TappedDown)
				{
					this.Selected++;
					if (this.Selected > 5)
					{
						this.Selected = 1;
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 375f - 125f * (float)this.Selected, this.Highlight.localPosition.z);
					this.DescLabel.text = this.Desc[this.Selected];
					this.UpdateSubjectLabels();
				}
				if (this.InputManager.TappedUp)
				{
					this.Selected--;
					if (this.Selected < 1)
					{
						this.Selected = 5;
					}
					this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 375f - 125f * (float)this.Selected, this.Highlight.localPosition.z);
					this.DescLabel.text = this.Desc[this.Selected];
					this.UpdateSubjectLabels();
				}
				if (this.InputManager.TappedRight && this.StudyPoints > 0 && this.Subject[this.Selected] + this.SubjectTemp[this.Selected] < 100)
				{
					this.SubjectTemp[this.Selected]++;
					this.StudyPoints--;
					this.UpdateLabel();
					this.UpdateBars();
				}
				if (this.InputManager.TappedLeft && this.SubjectTemp[this.Selected] > 0)
				{
					this.SubjectTemp[this.Selected]--;
					this.StudyPoints++;
					this.UpdateLabel();
					this.UpdateBars();
				}
				if (Input.GetButtonDown("A") && this.StudyPoints == 0)
				{
					this.Show = false;
					this.PromptBar.ClearButtons();
					this.PromptBar.Show = false;
					ClassGlobals.Biology = this.Subject[1] + this.SubjectTemp[1];
					ClassGlobals.Chemistry = this.Subject[2] + this.SubjectTemp[2];
					ClassGlobals.Language = this.Subject[3] + this.SubjectTemp[3];
					ClassGlobals.Physical = this.Subject[4] + this.SubjectTemp[4];
					ClassGlobals.Psychology = this.Subject[5] + this.SubjectTemp[5];
					for (int i = 0; i < 6; i++)
					{
						this.Subject[i] += this.SubjectTemp[i];
						this.SubjectTemp[i] = 0;
					}
					this.CheckForGradeUp();
				}
			}
		}
		else
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
			if (this.Darkness.color.a >= 1f)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
				if (!this.GradeUp)
				{
					if (this.GradeUpWindow.localScale.x > 0.1f)
					{
						this.GradeUpWindow.localScale = Vector3.Lerp(this.GradeUpWindow.localScale, Vector3.zero, Time.deltaTime * 10f);
					}
					else
					{
						this.GradeUpWindow.localScale = Vector3.zero;
					}
					if (this.GradeUpWindow.localScale.x < 0.01f)
					{
						this.GradeUpWindow.localScale = Vector3.zero;
						this.CheckForGradeUp();
						if (!this.GradeUp)
						{
							if (ClassGlobals.ChemistryGrade > 0 && this.Poison != null)
							{
								this.Poison.SetActive(true);
							}
							Debug.Log("CutsceneManager.Scheme is: " + this.CutsceneManager.Scheme);
							if (this.CutsceneManager.Scheme > 0)
							{
								SchemeGlobals.SetSchemeStage(this.CutsceneManager.Scheme, 100);
								this.PromptBar.ClearButtons();
								this.PromptBar.Label[0].text = "Continue";
								this.PromptBar.UpdateButtons();
								this.CutsceneManager.gameObject.SetActive(true);
								this.Schemes.UpdateInstructions();
								base.gameObject.SetActive(false);
							}
							else if (!this.Portal.FadeOut)
							{
								this.PromptBar.Show = false;
								this.Portal.Proceed = true;
								base.gameObject.SetActive(false);
							}
						}
					}
				}
				else
				{
					if (this.GradeUpWindow.localScale.x == 0f)
					{
						if (this.GradeUpSubject == 1)
						{
							this.GradeUpName.text = "BIOLOGY RANK UP";
							this.GradeUpDesc.text = this.Subject1GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 2)
						{
							this.GradeUpName.text = "CHEMISTRY RANK UP";
							this.GradeUpDesc.text = this.Subject2GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 3)
						{
							this.GradeUpName.text = "LANGUAGE RANK UP";
							this.GradeUpDesc.text = this.Subject3GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 4)
						{
							this.GradeUpName.text = "PHYSICAL RANK UP";
							this.GradeUpDesc.text = this.Subject4GradeText[this.Grade];
						}
						else if (this.GradeUpSubject == 5)
						{
							this.GradeUpName.text = "PSYCHOLOGY RANK UP";
							this.GradeUpDesc.text = this.Subject5GradeText[this.Grade];
						}
						this.PromptBar.ClearButtons();
						this.PromptBar.Label[0].text = "Continue";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
					}
					else if (this.GradeUpWindow.localScale.x > 0.99f && Input.GetButtonDown("A"))
					{
						this.PromptBar.ClearButtons();
						this.GradeUp = false;
					}
					this.GradeUpWindow.localScale = Vector3.Lerp(this.GradeUpWindow.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
				}
			}
		}
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x000BEF1C File Offset: 0x000BD31C
	private void UpdateSubjectLabels()
	{
		for (int i = 1; i < 6; i++)
		{
			this.SubjectLabels[i].color = new Color(0f, 0f, 0f, 1f);
		}
		this.SubjectLabels[this.Selected].color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x000BEF8C File Offset: 0x000BD38C
	public void UpdateLabel()
	{
		this.StudyPointsLabel.text = "STUDY POINTS: " + this.StudyPoints;
		if (this.StudyPoints == 0)
		{
			this.PromptBar.Label[0].text = "Confirm";
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x000BF010 File Offset: 0x000BD410
	private void UpdateBars()
	{
		for (int i = 1; i < 6; i++)
		{
			Transform transform = this.Subject1Bars[i];
			if (this.Subject[1] + this.SubjectTemp[1] > (i - 1) * 20)
			{
				transform.localScale = new Vector3(-((float)((i - 1) * 20 - (this.Subject[1] + this.SubjectTemp[1])) / 20f), transform.localScale.y, transform.localScale.z);
				if (transform.localScale.x > 1f)
				{
					transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
				}
			}
			else
			{
				transform.localScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);
			}
		}
		for (int j = 1; j < 6; j++)
		{
			Transform transform2 = this.Subject2Bars[j];
			if (this.Subject[2] + this.SubjectTemp[2] > (j - 1) * 20)
			{
				transform2.localScale = new Vector3(-((float)((j - 1) * 20 - (this.Subject[2] + this.SubjectTemp[2])) / 20f), transform2.localScale.y, transform2.localScale.z);
				if (transform2.localScale.x > 1f)
				{
					transform2.localScale = new Vector3(1f, transform2.localScale.y, transform2.localScale.z);
				}
			}
			else
			{
				transform2.localScale = new Vector3(0f, transform2.localScale.y, transform2.localScale.z);
			}
		}
		for (int k = 1; k < 6; k++)
		{
			Transform transform3 = this.Subject3Bars[k];
			if (this.Subject[3] + this.SubjectTemp[3] > (k - 1) * 20)
			{
				transform3.localScale = new Vector3(-((float)((k - 1) * 20 - (this.Subject[3] + this.SubjectTemp[3])) / 20f), transform3.localScale.y, transform3.localScale.z);
				if (transform3.localScale.x > 1f)
				{
					transform3.localScale = new Vector3(1f, transform3.localScale.y, transform3.localScale.z);
				}
			}
			else
			{
				transform3.localScale = new Vector3(0f, transform3.localScale.y, transform3.localScale.z);
			}
		}
		for (int l = 1; l < 6; l++)
		{
			Transform transform4 = this.Subject4Bars[l];
			if (this.Subject[4] + this.SubjectTemp[4] > (l - 1) * 20)
			{
				transform4.localScale = new Vector3(-((float)((l - 1) * 20 - (this.Subject[4] + this.SubjectTemp[4])) / 20f), transform4.localScale.y, transform4.localScale.z);
				if (transform4.localScale.x > 1f)
				{
					transform4.localScale = new Vector3(1f, transform4.localScale.y, transform4.localScale.z);
				}
			}
			else
			{
				transform4.localScale = new Vector3(0f, transform4.localScale.y, transform4.localScale.z);
			}
		}
		for (int m = 1; m < 6; m++)
		{
			Transform transform5 = this.Subject5Bars[m];
			if (this.Subject[5] + this.SubjectTemp[5] > (m - 1) * 20)
			{
				transform5.localScale = new Vector3(-((float)((m - 1) * 20 - (this.Subject[5] + this.SubjectTemp[5])) / 20f), transform5.localScale.y, transform5.localScale.z);
				if (transform5.localScale.x > 1f)
				{
					transform5.localScale = new Vector3(1f, transform5.localScale.y, transform5.localScale.z);
				}
			}
			else
			{
				transform5.localScale = new Vector3(0f, transform5.localScale.y, transform5.localScale.z);
			}
		}
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000BF548 File Offset: 0x000BD948
	private void CheckForGradeUp()
	{
		if (ClassGlobals.Biology >= 20 && ClassGlobals.BiologyGrade < 1)
		{
			ClassGlobals.BiologyGrade = 1;
			this.GradeUpSubject = 1;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if (ClassGlobals.Chemistry >= 20 && ClassGlobals.ChemistryGrade < 1)
		{
			ClassGlobals.ChemistryGrade = 1;
			this.GradeUpSubject = 2;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if (ClassGlobals.Language >= 20 && ClassGlobals.LanguageGrade < 1)
		{
			ClassGlobals.LanguageGrade = 1;
			this.GradeUpSubject = 3;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if (ClassGlobals.Physical >= 20 && ClassGlobals.PhysicalGrade < 1)
		{
			ClassGlobals.PhysicalGrade = 1;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 1;
		}
		else if (ClassGlobals.Physical >= 40 && ClassGlobals.PhysicalGrade < 2)
		{
			ClassGlobals.PhysicalGrade = 2;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 2;
		}
		else if (ClassGlobals.Physical >= 60 && ClassGlobals.PhysicalGrade < 3)
		{
			ClassGlobals.PhysicalGrade = 3;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 3;
		}
		else if (ClassGlobals.Physical >= 80 && ClassGlobals.PhysicalGrade < 4)
		{
			ClassGlobals.PhysicalGrade = 4;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 4;
		}
		else if (ClassGlobals.Physical == 100 && ClassGlobals.PhysicalGrade < 5)
		{
			ClassGlobals.PhysicalGrade = 5;
			this.GradeUpSubject = 4;
			this.GradeUp = true;
			this.Grade = 5;
		}
		else if (ClassGlobals.Psychology >= 20 && ClassGlobals.PsychologyGrade < 1)
		{
			ClassGlobals.PsychologyGrade = 1;
			this.GradeUpSubject = 5;
			this.GradeUp = true;
			this.Grade = 1;
		}
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000BF740 File Offset: 0x000BDB40
	private void GivePoints()
	{
		ClassGlobals.BiologyGrade = 0;
		ClassGlobals.ChemistryGrade = 0;
		ClassGlobals.LanguageGrade = 0;
		ClassGlobals.PhysicalGrade = 0;
		ClassGlobals.PsychologyGrade = 0;
		ClassGlobals.Biology = 19;
		ClassGlobals.Chemistry = 19;
		ClassGlobals.Language = 19;
		ClassGlobals.Physical = 19;
		ClassGlobals.Psychology = 19;
		this.Subject[1] = ClassGlobals.Biology;
		this.Subject[2] = ClassGlobals.Chemistry;
		this.Subject[3] = ClassGlobals.Language;
		this.Subject[4] = ClassGlobals.Physical;
		this.Subject[5] = ClassGlobals.Psychology;
		this.UpdateBars();
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x000BF7D5 File Offset: 0x000BDBD5
	private void MaxPhysical()
	{
		ClassGlobals.PhysicalGrade = 0;
		ClassGlobals.Physical = 99;
		this.Subject[4] = ClassGlobals.Physical;
		this.UpdateBars();
	}

	// Token: 0x040017FE RID: 6142
	public CutsceneManagerScript CutsceneManager;

	// Token: 0x040017FF RID: 6143
	public InputManagerScript InputManager;

	// Token: 0x04001800 RID: 6144
	public PromptBarScript PromptBar;

	// Token: 0x04001801 RID: 6145
	public SchemesScript Schemes;

	// Token: 0x04001802 RID: 6146
	public PortalScript Portal;

	// Token: 0x04001803 RID: 6147
	public GameObject Poison;

	// Token: 0x04001804 RID: 6148
	public UILabel StudyPointsLabel;

	// Token: 0x04001805 RID: 6149
	public UILabel[] SubjectLabels;

	// Token: 0x04001806 RID: 6150
	public UILabel GradeUpDesc;

	// Token: 0x04001807 RID: 6151
	public UILabel GradeUpName;

	// Token: 0x04001808 RID: 6152
	public UILabel DescLabel;

	// Token: 0x04001809 RID: 6153
	public UISprite Darkness;

	// Token: 0x0400180A RID: 6154
	public Transform[] Subject1Bars;

	// Token: 0x0400180B RID: 6155
	public Transform[] Subject2Bars;

	// Token: 0x0400180C RID: 6156
	public Transform[] Subject3Bars;

	// Token: 0x0400180D RID: 6157
	public Transform[] Subject4Bars;

	// Token: 0x0400180E RID: 6158
	public Transform[] Subject5Bars;

	// Token: 0x0400180F RID: 6159
	public string[] Subject1GradeText;

	// Token: 0x04001810 RID: 6160
	public string[] Subject2GradeText;

	// Token: 0x04001811 RID: 6161
	public string[] Subject3GradeText;

	// Token: 0x04001812 RID: 6162
	public string[] Subject4GradeText;

	// Token: 0x04001813 RID: 6163
	public string[] Subject5GradeText;

	// Token: 0x04001814 RID: 6164
	public Transform GradeUpWindow;

	// Token: 0x04001815 RID: 6165
	public Transform Highlight;

	// Token: 0x04001816 RID: 6166
	public int[] SubjectTemp;

	// Token: 0x04001817 RID: 6167
	public int[] Subject;

	// Token: 0x04001818 RID: 6168
	public string[] Desc;

	// Token: 0x04001819 RID: 6169
	public int GradeUpSubject;

	// Token: 0x0400181A RID: 6170
	public int StudyPoints;

	// Token: 0x0400181B RID: 6171
	public int Selected;

	// Token: 0x0400181C RID: 6172
	public int Grade;

	// Token: 0x0400181D RID: 6173
	public bool GradeUp;

	// Token: 0x0400181E RID: 6174
	public bool Show;
}
