using System;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
public class SchemesScript : MonoBehaviour
{
	// Token: 0x06001FBF RID: 8127 RVA: 0x0014551C File Offset: 0x0014391C
	private void Start()
	{
		for (int i = 1; i < this.SchemeNames.Length; i++)
		{
			if (!SchemeGlobals.GetSchemeStatus(i))
			{
				this.SchemeDeadlineLabels[i].text = this.SchemeDeadlines[i];
				this.SchemeNameLabels[i].text = this.SchemeNames[i];
			}
		}
		this.SchemeNameLabels[1].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[2].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[3].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[4].color = new Color(0f, 0f, 0f, 0.5f);
		this.SchemeNameLabels[5].color = new Color(0f, 0f, 0f, 0.5f);
		if (DateGlobals.Weekday == DayOfWeek.Monday)
		{
			this.SchemeNameLabels[1].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Tuesday)
		{
			this.SchemeNameLabels[2].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Wednesday)
		{
			this.SchemeNameLabels[3].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Thursday)
		{
			this.SchemeNameLabels[4].color = new Color(0f, 0f, 0f, 1f);
		}
		if (DateGlobals.Weekday == DayOfWeek.Friday)
		{
			this.SchemeNameLabels[5].color = new Color(0f, 0f, 0f, 1f);
		}
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x0014572C File Offset: 0x00143B2C
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.ID--;
			if (this.ID < 1)
			{
				this.ID = this.SchemeNames.Length - 1;
			}
			this.UpdateSchemeInfo();
		}
		if (this.InputManager.TappedDown)
		{
			this.ID++;
			if (this.ID > this.SchemeNames.Length - 1)
			{
				this.ID = 1;
			}
			this.UpdateSchemeInfo();
		}
		if (Input.GetButtonDown("A"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (this.PromptBar.Label[0].text != string.Empty)
			{
				if (this.SchemeNameLabels[this.ID].color.a == 1f)
				{
					this.SchemeManager.enabled = true;
					if (this.ID == 5)
					{
						this.SchemeManager.ClockCheck = true;
					}
					if (!SchemeGlobals.GetSchemeUnlocked(this.ID))
					{
						if (this.Inventory.PantyShots >= this.SchemeCosts[this.ID])
						{
							this.Inventory.PantyShots -= this.SchemeCosts[this.ID];
							SchemeGlobals.SetSchemeUnlocked(this.ID, true);
							SchemeGlobals.CurrentScheme = this.ID;
							if (SchemeGlobals.GetSchemeStage(this.ID) == 0)
							{
								SchemeGlobals.SetSchemeStage(this.ID, 1);
							}
							this.UpdateSchemeDestinations();
							this.UpdateInstructions();
							this.UpdateSchemeList();
							this.UpdateSchemeInfo();
							component.clip = this.InfoPurchase;
							component.Play();
						}
					}
					else
					{
						if (SchemeGlobals.CurrentScheme == this.ID)
						{
							SchemeGlobals.CurrentScheme = 0;
							this.SchemeManager.enabled = false;
						}
						else
						{
							SchemeGlobals.CurrentScheme = this.ID;
						}
						this.UpdateSchemeDestinations();
						this.UpdateInstructions();
						this.UpdateSchemeInfo();
					}
				}
			}
			else if (SchemeGlobals.GetSchemeStage(this.ID) != 100 && this.Inventory.PantyShots < this.SchemeCosts[this.ID])
			{
				component.clip = this.InfoAfford;
				component.Play();
			}
		}
		if (Input.GetButtonDown("B"))
		{
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[0].text = "Accept";
			this.PromptBar.Label[1].text = "Exit";
			this.PromptBar.Label[5].text = "Choose";
			this.PromptBar.UpdateButtons();
			this.FavorMenu.SetActive(true);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x001459F4 File Offset: 0x00143DF4
	public void UpdateSchemeList()
	{
		for (int i = 1; i < this.SchemeNames.Length; i++)
		{
			if (SchemeGlobals.GetSchemeStage(i) == 100)
			{
				UILabel uilabel = this.SchemeNameLabels[i];
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
				this.Exclamations[i].enabled = false;
				this.SchemeCostLabels[i].text = string.Empty;
			}
			else
			{
				this.SchemeCostLabels[i].text = (SchemeGlobals.GetSchemeUnlocked(i) ? string.Empty : this.SchemeCosts[i].ToString());
				if (SchemeGlobals.GetSchemeStage(i) > SchemeGlobals.GetSchemePreviousStage(i))
				{
					SchemeGlobals.SetSchemePreviousStage(i, SchemeGlobals.GetSchemeStage(i));
					this.Exclamations[i].enabled = true;
				}
				else
				{
					this.Exclamations[i].enabled = false;
				}
			}
		}
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x00145B08 File Offset: 0x00143F08
	public void UpdateSchemeInfo()
	{
		if (SchemeGlobals.GetSchemeStage(this.ID) != 100)
		{
			if (!SchemeGlobals.GetSchemeUnlocked(this.ID))
			{
				this.Arrow.gameObject.SetActive(false);
				this.PromptBar.Label[0].text = ((this.Inventory.PantyShots < this.SchemeCosts[this.ID]) ? string.Empty : "Purchase");
				this.PromptBar.UpdateButtons();
			}
			else if (SchemeGlobals.CurrentScheme == this.ID)
			{
				this.Arrow.gameObject.SetActive(true);
				this.Arrow.localPosition = new Vector3(this.Arrow.localPosition.x, -10f - 20f * (float)SchemeGlobals.GetSchemeStage(this.ID), this.Arrow.localPosition.z);
				this.PromptBar.Label[0].text = "Stop Tracking";
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.Arrow.gameObject.SetActive(false);
				this.PromptBar.Label[0].text = "Start Tracking";
				this.PromptBar.UpdateButtons();
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.ID, this.Highlight.localPosition.z);
		this.SchemeIcon.mainTexture = this.SchemeIcons[this.ID];
		this.SchemeDesc.text = this.SchemeDescs[this.ID];
		if (SchemeGlobals.GetSchemeStage(this.ID) == 100)
		{
			this.SchemeInstructions.text = "This scheme is no longer available.";
		}
		else
		{
			this.SchemeInstructions.text = (SchemeGlobals.GetSchemeUnlocked(this.ID) ? this.SchemeSteps[this.ID] : ("Skills Required:\n" + this.SchemeSkills[this.ID]));
		}
		this.UpdatePantyCount();
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x00145D70 File Offset: 0x00144170
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x00145D94 File Offset: 0x00144194
	public void UpdateInstructions()
	{
		this.Steps = this.SchemeSteps[SchemeGlobals.CurrentScheme].Split(new char[]
		{
			'\n'
		});
		if (SchemeGlobals.CurrentScheme > 0)
		{
			if (SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) < 100)
			{
				this.HUDIcon.SetActive(true);
				this.HUDInstructions.text = this.Steps[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) - 1].ToString();
			}
			else
			{
				this.Arrow.gameObject.SetActive(false);
				this.HUDIcon.gameObject.SetActive(false);
				this.HUDInstructions.text = string.Empty;
				SchemeGlobals.CurrentScheme = 0;
			}
		}
		else
		{
			this.HUDIcon.SetActive(false);
			this.HUDInstructions.text = string.Empty;
		}
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x00145E6C File Offset: 0x0014426C
	public void UpdateSchemeDestinations()
	{
		if (this.StudentManager.Students[this.StudentManager.RivalID] != null)
		{
			this.Scheme1Destinations[3] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme1Destinations[7] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme4Destinations[5] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
			this.Scheme4Destinations[6] = this.StudentManager.Students[this.StudentManager.RivalID].transform;
		}
		if (this.StudentManager.Students[2] != null)
		{
			this.Scheme2Destinations[1] = this.StudentManager.Students[2].transform;
		}
		if (this.StudentManager.Students[97] != null)
		{
			this.Scheme5Destinations[3] = this.StudentManager.Students[97].transform;
		}
		if (SchemeGlobals.CurrentScheme == 1)
		{
			this.SchemeDestinations = this.Scheme1Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 2)
		{
			this.SchemeDestinations = this.Scheme2Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 3)
		{
			this.SchemeDestinations = this.Scheme3Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 4)
		{
			this.SchemeDestinations = this.Scheme4Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 5)
		{
			this.SchemeDestinations = this.Scheme5Destinations;
		}
	}

	// Token: 0x04002B88 RID: 11144
	public StudentManagerScript StudentManager;

	// Token: 0x04002B89 RID: 11145
	public SchemeManagerScript SchemeManager;

	// Token: 0x04002B8A RID: 11146
	public InputManagerScript InputManager;

	// Token: 0x04002B8B RID: 11147
	public InventoryScript Inventory;

	// Token: 0x04002B8C RID: 11148
	public PromptBarScript PromptBar;

	// Token: 0x04002B8D RID: 11149
	public GameObject FavorMenu;

	// Token: 0x04002B8E RID: 11150
	public Transform Highlight;

	// Token: 0x04002B8F RID: 11151
	public Transform Arrow;

	// Token: 0x04002B90 RID: 11152
	public UILabel SchemeInstructions;

	// Token: 0x04002B91 RID: 11153
	public UITexture SchemeIcon;

	// Token: 0x04002B92 RID: 11154
	public UILabel PantyCount;

	// Token: 0x04002B93 RID: 11155
	public UILabel SchemeDesc;

	// Token: 0x04002B94 RID: 11156
	public UILabel[] SchemeDeadlineLabels;

	// Token: 0x04002B95 RID: 11157
	public UILabel[] SchemeCostLabels;

	// Token: 0x04002B96 RID: 11158
	public UILabel[] SchemeNameLabels;

	// Token: 0x04002B97 RID: 11159
	public UISprite[] Exclamations;

	// Token: 0x04002B98 RID: 11160
	public Texture[] SchemeIcons;

	// Token: 0x04002B99 RID: 11161
	public int[] SchemeCosts;

	// Token: 0x04002B9A RID: 11162
	public Transform[] SchemeDestinations;

	// Token: 0x04002B9B RID: 11163
	public string[] SchemeDeadlines;

	// Token: 0x04002B9C RID: 11164
	public string[] SchemeSkills;

	// Token: 0x04002B9D RID: 11165
	public string[] SchemeDescs;

	// Token: 0x04002B9E RID: 11166
	public string[] SchemeNames;

	// Token: 0x04002B9F RID: 11167
	[Multiline]
	[SerializeField]
	public string[] SchemeSteps;

	// Token: 0x04002BA0 RID: 11168
	public int ID = 1;

	// Token: 0x04002BA1 RID: 11169
	public string[] Steps;

	// Token: 0x04002BA2 RID: 11170
	public AudioClip InfoPurchase;

	// Token: 0x04002BA3 RID: 11171
	public AudioClip InfoAfford;

	// Token: 0x04002BA4 RID: 11172
	public Transform[] Scheme1Destinations;

	// Token: 0x04002BA5 RID: 11173
	public Transform[] Scheme2Destinations;

	// Token: 0x04002BA6 RID: 11174
	public Transform[] Scheme3Destinations;

	// Token: 0x04002BA7 RID: 11175
	public Transform[] Scheme4Destinations;

	// Token: 0x04002BA8 RID: 11176
	public Transform[] Scheme5Destinations;

	// Token: 0x04002BA9 RID: 11177
	public GameObject HUDIcon;

	// Token: 0x04002BAA RID: 11178
	public UILabel HUDInstructions;
}
