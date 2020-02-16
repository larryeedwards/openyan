using System;
using UnityEngine;

// Token: 0x02000503 RID: 1283
public class ServicesScript : MonoBehaviour
{
	// Token: 0x06001FDF RID: 8159 RVA: 0x001467EC File Offset: 0x00144BEC
	private void Start()
	{
		for (int i = 1; i < this.ServiceNames.Length; i++)
		{
			SchemeGlobals.SetServicePurchased(i, false);
			this.NameLabels[i].text = this.ServiceNames[i];
		}
	}

	// Token: 0x06001FE0 RID: 8160 RVA: 0x00146830 File Offset: 0x00144C30
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = this.ServiceNames.Length - 1;
			}
			this.UpdateDesc();
		}
		if (this.InputManager.TappedDown)
		{
			this.Selected++;
			if (this.Selected > this.ServiceNames.Length - 1)
			{
				this.Selected = 1;
			}
			this.UpdateDesc();
		}
		AudioSource component = base.GetComponent<AudioSource>();
		if (Input.GetButtonDown("A"))
		{
			if (!SchemeGlobals.GetServicePurchased(this.Selected) && (double)this.NameLabels[this.Selected].color.a == 1.0)
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.ServiceCosts[this.Selected])
					{
						if (this.Selected == 1)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.GettingInfo = true;
							this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;
							base.gameObject.SetActive(false);
						}
						if (this.Selected == 2)
						{
							this.Reputation.PendingRep += 5f;
							this.Purchase();
						}
						else if (this.Selected == 3)
						{
							StudentGlobals.SetStudentReputation(this.StudentManager.RivalID, StudentGlobals.GetStudentReputation(this.StudentManager.RivalID) - 5);
							this.Purchase();
						}
						else if (this.Selected == 4)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							SchemeGlobals.DarkSecret = true;
							this.Purchase();
						}
						else if (this.Selected == 5)
						{
							this.Yandere.PauseScreen.StudentInfoMenu.SendingHome = true;
							this.Yandere.PauseScreen.StudentInfoMenu.gameObject.SetActive(true);
							base.StartCoroutine(this.Yandere.PauseScreen.StudentInfoMenu.UpdatePortraits());
							this.Yandere.PauseScreen.StudentInfoMenu.Column = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.Row = 0;
							this.Yandere.PauseScreen.StudentInfoMenu.UpdateHighlight();
							this.Yandere.PauseScreen.Sideways = true;
							this.Yandere.PromptBar.ClearButtons();
							this.Yandere.PromptBar.Label[1].text = "Cancel";
							this.Yandere.PromptBar.UpdateButtons();
							this.Yandere.PromptBar.Show = true;
							base.gameObject.SetActive(false);
						}
						else if (this.Selected == 6)
						{
							this.Police.Timer += 300f;
							this.Police.Delayed = true;
							this.Purchase();
						}
						else if (this.Selected == 7)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							CounselorGlobals.CounselorTape = 1;
							this.Purchase();
						}
						else if (this.Selected == 8)
						{
							SchemeGlobals.SetServicePurchased(this.Selected, true);
							for (int i = 1; i < 26; i++)
							{
								ConversationGlobals.SetTopicLearnedByStudent(i, 11, true);
							}
							this.Purchase();
						}
					}
				}
				else if (this.Inventory.PantyShots < this.ServiceCosts[this.Selected])
				{
					component.clip = this.InfoAfford;
					component.Play();
				}
				else
				{
					component.clip = this.InfoUnavailable;
					component.Play();
				}
			}
			else
			{
				component.clip = this.InfoUnavailable;
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

	// Token: 0x06001FE1 RID: 8161 RVA: 0x00146D68 File Offset: 0x00145168
	public void UpdateList()
	{
		this.ID = 1;
		while (this.ID < this.ServiceNames.Length)
		{
			this.CostLabels[this.ID].text = this.ServiceCosts[this.ID].ToString();
			bool servicePurchased = SchemeGlobals.GetServicePurchased(this.ID);
			this.ServiceAvailable[this.ID] = false;
			if (this.ID == 1 || this.ID == 2 || this.ID == 3)
			{
				this.ServiceAvailable[this.ID] = true;
			}
			else if (this.ID == 4)
			{
				if (!SchemeGlobals.DarkSecret)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 5)
			{
				if (!this.ServicePurchased[this.ID])
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 6)
			{
				if (this.Police.Show && !this.Police.Delayed)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 7)
			{
				if (CounselorGlobals.CounselorTape == 0)
				{
					this.ServiceAvailable[this.ID] = true;
				}
			}
			else if (this.ID == 8 && !SchemeGlobals.GetServicePurchased(8))
			{
				this.ServiceAvailable[this.ID] = true;
			}
			UILabel uilabel = this.NameLabels[this.ID];
			uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, (!this.ServiceAvailable[this.ID] || servicePurchased) ? 0.5f : 1f);
			this.ID++;
		}
	}

	// Token: 0x06001FE2 RID: 8162 RVA: 0x00146F74 File Offset: 0x00145374
	public void UpdateDesc()
	{
		if (this.ServiceAvailable[this.Selected] && !SchemeGlobals.GetServicePurchased(this.Selected))
		{
			this.PromptBar.Label[0].text = ((this.Inventory.PantyShots < this.ServiceCosts[this.Selected]) ? string.Empty : "Purchase");
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.Selected, this.Highlight.localPosition.z);
		this.ServiceIcon.mainTexture = this.ServiceIcons[this.Selected];
		this.ServiceLimit.text = this.ServiceLimits[this.Selected];
		this.ServiceDesc.text = this.ServiceDescs[this.Selected];
		if (this.Selected == 5)
		{
			this.ServiceDesc.text = this.ServiceDescs[this.Selected] + "\n\nIf student portraits don't appear, back out of the menu, load the Student Info menu, then return to this screen.";
		}
		this.UpdatePantyCount();
	}

	// Token: 0x06001FE3 RID: 8163 RVA: 0x001470D8 File Offset: 0x001454D8
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x06001FE4 RID: 8164 RVA: 0x001470FC File Offset: 0x001454FC
	public void Purchase()
	{
		this.ServicePurchased[this.Selected] = true;
		this.TextMessageManager.SpawnMessage(this.Selected);
		this.Inventory.PantyShots -= this.ServiceCosts[this.Selected];
		AudioSource.PlayClipAtPoint(this.InfoPurchase, base.transform.position);
		this.UpdateList();
		this.UpdateDesc();
		this.PromptBar.Label[0].text = string.Empty;
		this.PromptBar.Label[1].text = "Back";
		this.PromptBar.UpdateButtons();
	}

	// Token: 0x04002BCD RID: 11213
	public TextMessageManagerScript TextMessageManager;

	// Token: 0x04002BCE RID: 11214
	public StudentManagerScript StudentManager;

	// Token: 0x04002BCF RID: 11215
	public InputManagerScript InputManager;

	// Token: 0x04002BD0 RID: 11216
	public ReputationScript Reputation;

	// Token: 0x04002BD1 RID: 11217
	public InventoryScript Inventory;

	// Token: 0x04002BD2 RID: 11218
	public PromptBarScript PromptBar;

	// Token: 0x04002BD3 RID: 11219
	public SchemesScript Schemes;

	// Token: 0x04002BD4 RID: 11220
	public YandereScript Yandere;

	// Token: 0x04002BD5 RID: 11221
	public GameObject FavorMenu;

	// Token: 0x04002BD6 RID: 11222
	public Transform Highlight;

	// Token: 0x04002BD7 RID: 11223
	public PoliceScript Police;

	// Token: 0x04002BD8 RID: 11224
	public UITexture ServiceIcon;

	// Token: 0x04002BD9 RID: 11225
	public UILabel ServiceLimit;

	// Token: 0x04002BDA RID: 11226
	public UILabel ServiceDesc;

	// Token: 0x04002BDB RID: 11227
	public UILabel PantyCount;

	// Token: 0x04002BDC RID: 11228
	public UILabel[] CostLabels;

	// Token: 0x04002BDD RID: 11229
	public UILabel[] NameLabels;

	// Token: 0x04002BDE RID: 11230
	public Texture[] ServiceIcons;

	// Token: 0x04002BDF RID: 11231
	public string[] ServiceLimits;

	// Token: 0x04002BE0 RID: 11232
	public string[] ServiceDescs;

	// Token: 0x04002BE1 RID: 11233
	public string[] ServiceNames;

	// Token: 0x04002BE2 RID: 11234
	public bool[] ServiceAvailable;

	// Token: 0x04002BE3 RID: 11235
	public bool[] ServicePurchased;

	// Token: 0x04002BE4 RID: 11236
	public int[] ServiceCosts;

	// Token: 0x04002BE5 RID: 11237
	public int Selected = 1;

	// Token: 0x04002BE6 RID: 11238
	public int ID = 1;

	// Token: 0x04002BE7 RID: 11239
	public AudioClip InfoUnavailable;

	// Token: 0x04002BE8 RID: 11240
	public AudioClip InfoPurchase;

	// Token: 0x04002BE9 RID: 11241
	public AudioClip InfoAfford;
}
