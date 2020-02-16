using System;
using UnityEngine;

// Token: 0x0200044B RID: 1099
public class LeaveGiftScript : MonoBehaviour
{
	// Token: 0x06001D66 RID: 7526 RVA: 0x0011374C File Offset: 0x00111B4C
	private void Start()
	{
		this.Box.SetActive(false);
		this.EndOfDay.SenpaiGifts = CollectibleGlobals.SenpaiGifts;
		if (this.EndOfDay.SenpaiGifts == 0)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x001137A4 File Offset: 0x00111BA4
	private void Update()
	{
		Debug.Log(Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position));
		if (this.Prompt.InView)
		{
			if (Vector3.Distance(this.Prompt.Yandere.transform.position, this.Prompt.Yandere.Senpai.position) > 10f)
			{
				if (this.Prompt.Circle[0].fillAmount == 0f)
				{
					this.EndOfDay.SenpaiGifts--;
					this.Prompt.Hide();
					this.Prompt.enabled = false;
					this.Box.SetActive(true);
					base.enabled = false;
				}
			}
			else
			{
				this.Prompt.Hide();
			}
		}
	}

	// Token: 0x0400246E RID: 9326
	public EndOfDayScript EndOfDay;

	// Token: 0x0400246F RID: 9327
	public PromptScript Prompt;

	// Token: 0x04002470 RID: 9328
	public GameObject Box;
}
