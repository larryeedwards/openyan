using System;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class DropsScript : MonoBehaviour
{
	// Token: 0x060018F7 RID: 6391 RVA: 0x000E6658 File Offset: 0x000E4A58
	private void Start()
	{
		this.ID = 1;
		while (this.ID < this.DropNames.Length)
		{
			this.NameLabels[this.ID].text = this.DropNames[this.ID];
			this.ID++;
		}
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x000E66B4 File Offset: 0x000E4AB4
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			this.Selected--;
			if (this.Selected < 1)
			{
				this.Selected = this.DropNames.Length - 1;
			}
			this.UpdateDesc();
		}
		if (this.InputManager.TappedDown)
		{
			this.Selected++;
			if (this.Selected > this.DropNames.Length - 1)
			{
				this.Selected = 1;
			}
			this.UpdateDesc();
		}
		if (Input.GetButtonDown("A"))
		{
			AudioSource component = base.GetComponent<AudioSource>();
			if (!this.Purchased[this.Selected])
			{
				if (this.PromptBar.Label[0].text != string.Empty)
				{
					if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
					{
						this.Inventory.PantyShots -= this.DropCosts[this.Selected];
						this.Purchased[this.Selected] = true;
						this.InfoChanWindow.Orders++;
						this.InfoChanWindow.ItemsToDrop[this.InfoChanWindow.Orders] = this.Selected;
						this.InfoChanWindow.DropObject();
						this.UpdateList();
						this.UpdateDesc();
						component.clip = this.InfoPurchase;
						component.Play();
						if (this.Selected == 2)
						{
							SchemeGlobals.SetSchemeStage(3, 2);
							this.Schemes.UpdateInstructions();
						}
					}
				}
				else if (this.Inventory.PantyShots < this.DropCosts[this.Selected])
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

	// Token: 0x060018F9 RID: 6393 RVA: 0x000E6930 File Offset: 0x000E4D30
	public void UpdateList()
	{
		this.ID = 1;
		while (this.ID < this.DropNames.Length)
		{
			UILabel uilabel = this.NameLabels[this.ID];
			if (!this.Purchased[this.ID])
			{
				this.CostLabels[this.ID].text = this.DropCosts[this.ID].ToString();
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 1f);
			}
			else
			{
				this.CostLabels[this.ID].text = string.Empty;
				uilabel.color = new Color(uilabel.color.r, uilabel.color.g, uilabel.color.b, 0.5f);
			}
			this.ID++;
		}
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x000E6A4C File Offset: 0x000E4E4C
	public void UpdateDesc()
	{
		if (!this.Purchased[this.Selected])
		{
			if (this.Inventory.PantyShots >= this.DropCosts[this.Selected])
			{
				this.PromptBar.Label[0].text = "Purchase";
				this.PromptBar.UpdateButtons();
			}
			else
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.Selected, this.Highlight.localPosition.z);
		this.DropIcon.mainTexture = this.DropIcons[this.Selected];
		this.DropDesc.text = this.DropDescs[this.Selected];
		this.UpdatePantyCount();
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x000E6B77 File Offset: 0x000E4F77
	public void UpdatePantyCount()
	{
		this.PantyCount.text = this.Inventory.PantyShots.ToString();
	}

	// Token: 0x04001CDD RID: 7389
	public InfoChanWindowScript InfoChanWindow;

	// Token: 0x04001CDE RID: 7390
	public InputManagerScript InputManager;

	// Token: 0x04001CDF RID: 7391
	public InventoryScript Inventory;

	// Token: 0x04001CE0 RID: 7392
	public PromptBarScript PromptBar;

	// Token: 0x04001CE1 RID: 7393
	public SchemesScript Schemes;

	// Token: 0x04001CE2 RID: 7394
	public GameObject FavorMenu;

	// Token: 0x04001CE3 RID: 7395
	public Transform Highlight;

	// Token: 0x04001CE4 RID: 7396
	public UILabel PantyCount;

	// Token: 0x04001CE5 RID: 7397
	public UITexture DropIcon;

	// Token: 0x04001CE6 RID: 7398
	public UILabel DropDesc;

	// Token: 0x04001CE7 RID: 7399
	public UILabel[] CostLabels;

	// Token: 0x04001CE8 RID: 7400
	public UILabel[] NameLabels;

	// Token: 0x04001CE9 RID: 7401
	public bool[] Purchased;

	// Token: 0x04001CEA RID: 7402
	public Texture[] DropIcons;

	// Token: 0x04001CEB RID: 7403
	public int[] DropCosts;

	// Token: 0x04001CEC RID: 7404
	public string[] DropDescs;

	// Token: 0x04001CED RID: 7405
	public string[] DropNames;

	// Token: 0x04001CEE RID: 7406
	public int Selected = 1;

	// Token: 0x04001CEF RID: 7407
	public int ID = 1;

	// Token: 0x04001CF0 RID: 7408
	public AudioClip InfoUnavailable;

	// Token: 0x04001CF1 RID: 7409
	public AudioClip InfoPurchase;

	// Token: 0x04001CF2 RID: 7410
	public AudioClip InfoAfford;
}
