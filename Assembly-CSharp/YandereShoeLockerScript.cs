using System;
using UnityEngine;

// Token: 0x0200059C RID: 1436
public class YandereShoeLockerScript : MonoBehaviour
{
	// Token: 0x060022EC RID: 8940 RVA: 0x001B70A0 File Offset: 0x001B54A0
	private void Update()
	{
		if (this.Yandere.Schoolwear == 1 && !this.Yandere.ClubAttire && !this.Yandere.Egg)
		{
			if (this.Label == 2)
			{
				this.Prompt.Label[0].text = "     Change Shoes";
				this.Label = 1;
			}
			if (this.Prompt.Circle[0].fillAmount == 0f)
			{
				this.Prompt.Circle[0].fillAmount = 1f;
				this.Yandere.Casual = !this.Yandere.Casual;
				this.Yandere.ChangeSchoolwear();
				this.Yandere.CanMove = true;
			}
		}
		else
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (this.Label == 1)
			{
				this.Prompt.Label[0].text = "     Not Available";
				this.Label = 2;
			}
		}
	}

	// Token: 0x04003BD2 RID: 15314
	public YandereScript Yandere;

	// Token: 0x04003BD3 RID: 15315
	public PromptScript Prompt;

	// Token: 0x04003BD4 RID: 15316
	public int Label = 1;
}
