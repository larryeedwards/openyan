using System;
using UnityEngine;

// Token: 0x02000552 RID: 1362
public class TitleSponsorScript : MonoBehaviour
{
	// Token: 0x060021A0 RID: 8608 RVA: 0x00197864 File Offset: 0x00195C64
	private void Start()
	{
		base.transform.localPosition = new Vector3(1050f, base.transform.localPosition.y, base.transform.localPosition.z);
		this.UpdateHighlight();
		if (GameGlobals.LoveSick)
		{
			this.TurnLoveSick();
		}
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x001978C2 File Offset: 0x00195CC2
	public int GetSponsorIndex()
	{
		return this.Column + this.Row * this.Columns;
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x001978D8 File Offset: 0x00195CD8
	public bool SponsorHasWebsite(int index)
	{
		return !string.IsNullOrEmpty(this.SponsorURLs[index]);
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x001978EC File Offset: 0x00195CEC
	private void Update()
	{
		if (!this.Show)
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 1050f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(Mathf.Lerp(base.transform.localPosition.x, 0f, Time.deltaTime * 10f), base.transform.localPosition.y, base.transform.localPosition.z);
			if (this.InputManager.TappedUp)
			{
				this.Row = ((this.Row <= 0) ? (this.Rows - 1) : (this.Row - 1));
			}
			if (this.InputManager.TappedDown)
			{
				this.Row = ((this.Row >= this.Rows - 1) ? 0 : (this.Row + 1));
			}
			if (this.InputManager.TappedRight)
			{
				this.Column = ((this.Column >= this.Columns - 1) ? 0 : (this.Column + 1));
			}
			if (this.InputManager.TappedLeft)
			{
				this.Column = ((this.Column <= 0) ? (this.Columns - 1) : (this.Column - 1));
			}
			bool flag = this.InputManager.TappedUp || this.InputManager.TappedDown || this.InputManager.TappedRight || this.InputManager.TappedLeft;
			if (flag)
			{
				this.UpdateHighlight();
			}
			if (Input.GetButtonDown("A"))
			{
				int sponsorIndex = this.GetSponsorIndex();
				if (this.SponsorHasWebsite(sponsorIndex))
				{
					Application.OpenURL(this.SponsorURLs[sponsorIndex]);
				}
			}
		}
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x00197B24 File Offset: 0x00195F24
	private void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(-384f + (float)this.Column * 256f, 128f - (float)this.Row * 256f, this.Highlight.localPosition.z);
		this.SponsorName.text = this.Sponsors[this.GetSponsorIndex()];
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x00197B94 File Offset: 0x00195F94
	private void TurnLoveSick()
	{
		this.BlackSprite.color = Color.black;
		foreach (UISprite uisprite in this.RedSprites)
		{
			uisprite.color = new Color(1f, 0f, 0f, uisprite.color.a);
		}
		foreach (UILabel uilabel in this.Labels)
		{
			uilabel.color = new Color(1f, 0f, 0f, uilabel.color.a);
		}
	}

	// Token: 0x04003683 RID: 13955
	public InputManagerScript InputManager;

	// Token: 0x04003684 RID: 13956
	public string[] SponsorURLs;

	// Token: 0x04003685 RID: 13957
	public string[] Sponsors;

	// Token: 0x04003686 RID: 13958
	public UILabel SponsorName;

	// Token: 0x04003687 RID: 13959
	public Transform Highlight;

	// Token: 0x04003688 RID: 13960
	public bool Show;

	// Token: 0x04003689 RID: 13961
	public int Columns;

	// Token: 0x0400368A RID: 13962
	public int Rows;

	// Token: 0x0400368B RID: 13963
	private int Column;

	// Token: 0x0400368C RID: 13964
	private int Row;

	// Token: 0x0400368D RID: 13965
	public UISprite BlackSprite;

	// Token: 0x0400368E RID: 13966
	public UISprite[] RedSprites;

	// Token: 0x0400368F RID: 13967
	public UILabel[] Labels;
}
